import React from 'react'
import "./index.less";
import AppComponentBase from '../../../components/AppComponentBase';
import { inject, observer } from 'mobx-react';
import Stores from '../../../stores/storeIdentifier';
import DonViHanhChinhStore from '../../../stores/donViHanhChinhStore';
import { Button, Card, Dropdown, Menu, Modal, Table } from 'antd';
import { PlusOutlined, SettingOutlined } from '@ant-design/icons';
import { EntityDto } from '../../../services/dto/entityDto';
import CreateOrUpdateCapDVHCModal from './components/CreateOrUpdateCapDvhc';
import { FormInstance } from 'antd/lib/form';
import dvhcService from '../../../services/dvhc/dvhcService';
import { handleCommontResponse } from '../../../services/common/handleResponse';

const confirm = Modal.confirm;

export interface IQuanLyCapDVHCProps {
    donViHanhChinhStore: DonViHanhChinhStore;
}

export interface IQuanLyCapDVHCState {
    isLoading: boolean;
    listCapDvhc: any;
    modalVisible: boolean;
    loading: boolean;
}

@inject(Stores.DonViHanhChinhStore)
@observer
class QuanLyCapDVHC extends AppComponentBase<IQuanLyCapDVHCProps, IQuanLyCapDVHCState> {
    formRef = React.createRef<FormInstance>();
    state = {
        isLoading: false,
        listCapDvhc: [],
        modalVisible: false,
        loading: false
    }
    async componentDidMount() {
        this.getAll();
    }

    async getAll() {
        this.setState({ isLoading: true })
        const listdvhcResponse = await this.props.donViHanhChinhStore.fetchAllCapDonViHanhChinh();
        if (listdvhcResponse.code == 1) {
            this.setState({
                listCapDvhc: listdvhcResponse.returnValue,
                isLoading: false
            })
        } else {
            this.setState({ isLoading: false })
        }
    }
    async createOrUpdateModalOpen(entity?: any) {
        this.Modal();
        setTimeout(() => {
            this.formRef.current?.setFieldsValue({ ...entity });
        }, 100);
    }
    async deleteCapDVHC(id: any) {
        await this.props.donViHanhChinhStore.deleteCapDonViHanhChinh(id);
        this.getAll()
    }

    delete(input: EntityDto) {
        const self = this;
        confirm({
            title: 'Bạn muốn xóa hàng này',
            onOk() {
                self.deleteCapDVHC(input.id)
            },
            onCancel() {
                console.log('Cancel');
            },
        });
    }

    Modal = () => {
        this.setState({
            modalVisible: !this.state.modalVisible,
        });
    };

    handleCreate = () => {
        const form = this.formRef.current;
        form!.validateFields().then(async (values: any) => {
            this.setState({ loading: true })
            const response = await dvhcService.createOrUpdateCapDVHC(values)
            handleCommontResponse(response);
            this.setState({ modalVisible: false });
            await this.getAll();
            form!.resetFields();
            this.setState({ loading: false })
        });
    }


    public render() {
        const columns = [
            { title: 'Tên', dataIndex: 'name', key: 'name' },
            { title: 'Kỳ thống kê/kiểm kê', dataIndex: 'year', key: 'year' },
            {
                title: 'Hành động',
                width: 120,
                render: (text: string, item: any) => (
                    <div>
                        <Dropdown
                            trigger={['click']}
                            overlay={
                                <Menu>
                                    <Menu.Item onClick={() => this.createOrUpdateModalOpen(item)}>Chỉnh sửa</Menu.Item>
                                    <Menu.Item onClick={() => this.delete({ id: item.id })}>Xóa</Menu.Item>
                                </Menu>
                            }
                            placement="bottomLeft"
                        >
                            <Button type="primary" icon={<SettingOutlined />} />
                        </Dropdown>
                    </div>
                ),
            },
        ];

        return (
            <div className='capdvhc-page-wrapper'>
                <h1 className='txt-page-header'>Cấp đơn vị hành chính</h1>

                <Card title={
                    <div className='table-header-layout'>
                        <div style={{ flex: 1 }}>
                        </div>
                        <div className='table-header-layout-right'>
                            <Button type="primary" icon={<PlusOutlined />} onClick={() => this.createOrUpdateModalOpen()}>
                                Tạo mới
                            </Button>
                        </div>
                    </div>
                }>
                    <Table
                        rowKey={"id"}
                        bordered={true}
                        columns={columns}
                        pagination={false}
                        loading={this.state.isLoading}
                        dataSource={this.state.listCapDvhc}
                    />
                </Card>
                <CreateOrUpdateCapDVHCModal
                    title='Cấp đơn vị hành chính'
                    formRef={this.formRef}
                    visible={this.state.modalVisible}
                    confirmLoading={this.state.loading}
                    onCancel={() => {
                        this.setState({
                            modalVisible: false,
                        });
                        this.formRef.current?.resetFields();
                    }}
                    onCreate={this.handleCreate}
                />

            </div>
        )
    }
}

export default QuanLyCapDVHC;
