import React from 'react'
import "./index.less";
import AppComponentBase from '../../../components/AppComponentBase';
import { Button, Card, Dropdown, Menu, Modal, notification, Table } from 'antd';
import { PlusOutlined, SettingOutlined } from '@ant-design/icons';
import { EntityDto } from '../../../services/dto/entityDto';
import { FormInstance } from 'antd/lib/form';
import dvhcService from '../../../services/dvhc/dvhcService';
import CreateOrUpdateKyKiemKeModal from './components/CreateOrUpdateKyKiemKe';

const confirm = Modal.confirm;

export interface IQuanLyKyKiemKeProps {
}

export interface IQuanLyKyKiemKeState {
    isLoading: boolean;
    listKyKiemKe: any;
    modalVisible: boolean;
    loading: boolean;
}


class QuanLyKyKiemKePage extends AppComponentBase<IQuanLyKyKiemKeProps, IQuanLyKyKiemKeState> {
    formRef = React.createRef<FormInstance>();
    state = {
        isLoading: false,
        listKyKiemKe: [],
        modalVisible: false,
        loading: false
    }
    async componentDidMount() {
        this.getAll();
    }

    async getAll() {
        this.setState({ isLoading: true })
        const listdvhcResponse = await dvhcService.getAllKyKiemKe();
        if (listdvhcResponse.code == 1) {
            this.setState({
                listKyKiemKe: listdvhcResponse.returnValue,
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

    delete(input: EntityDto) {
        confirm({
            title: 'Bạn muốn xóa hàng này',
            onOk() {
                dvhcService.deleteKyKiemKe(input.id);
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
            const response = await dvhcService.createOrUpdateKyKiemKe(values)
            if (response.code == 1) {
                notification.success({ message: response.message })
            } else {
                notification.error({ message: response.message })
            }
            this.setState({ modalVisible: false });
            await this.getAll();
            form!.resetFields();
            this.setState({ loading: false })
        });
    }


    public render() {
        const columns = [
            { title: 'Mã', dataIndex: 'ma', key: 'ma' },
            { title: 'Tên', dataIndex: 'name', key: 'name' },
            { title: 'Năm', dataIndex: 'year', key: 'year' },
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
                <h1 className='txt-page-header'>Kỳ thống kê, kiểm kê</h1>

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
                        dataSource={this.state.listKyKiemKe}
                    />
                </Card>
                <CreateOrUpdateKyKiemKeModal
                    title='Kỳ thống kê, kiểm kê'
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

export default QuanLyKyKiemKePage;
