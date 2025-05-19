import React, { useEffect, useState } from 'react'
import './index.less'
import { Button, Card, Dropdown, Empty, Menu, notification, Table } from 'antd'
import bieuMauService from '../../../services/bieuMau/bieuMauService';
import { PlusOutlined, SettingOutlined } from '@ant-design/icons';
import CreateOrUpdateBieuModal from './components/CreateOrUpdateBieu';
import { FormInstance } from 'antd/lib/form';
import confirm from 'antd/lib/modal/confirm';

export default function QuanLyBieuPage() {
    const formRef = React.createRef<FormInstance>();
    const [listBieu, setListBieu] = useState([])
    const [isLoading, setIsLoading] = useState(false)
    const [bieuModal, setBieuModal] = useState({
        visible: false,
        entity: null
    })

    const columns = [
        { title: 'Ký hiệu', dataIndex: 'kyHieu', key: 'kyHieu' },
        { title: 'Tên biểu', dataIndex: 'noiDung', key: 'noiDung' },
        { title: 'Kỳ kiểm kê', dataIndex: 'year', key: 'year' },
        { title: 'Cấp DVHC', dataIndex: 'capDVHC', key: 'capDVHC' },
        {
            title: 'Hành động',
            width: 120,
            render: (text: string, item: any) => (
                <div>
                    <Dropdown
                        trigger={['click']}
                        overlay={
                            <Menu>
                                <Menu.Item onClick={() => { createOrUpdateModalOpen(item) }}>Chỉnh sửa</Menu.Item>
                                <Menu.Item onClick={() => { onDelete(item) }}>Xóa</Menu.Item>
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

    const onDelete = (input: any) => {
        confirm({
            title: 'Bạn muốn xóa hàng này',
            onOk() {
                onDeleteRow(input.id);
            },
            onCancel() {
                console.log('Cancel');
            },
        });
    }

    const onDeleteRow = async (id: any) => {
        const response = await bieuMauService.onDeleteById(id);
        if (response.code == 1) {
            notification.success({ message: "Xóa thành công" })
            getAll()
        } else {
            notification.error({ message: "Xóa thất bại" })
        }
    }

    const createOrUpdateModalOpen = async (entity?: any) => {
        setBieuModal({
            visible: true,
            entity: entity
        })
    }

    const getAll = async () => {
        const listdvhcResponse = await bieuMauService.getAll();
        if (listdvhcResponse.code == 1) {
            setListBieu(listdvhcResponse.returnValue?.items);
        }
    }
    const handleCreate = () => {
        const form = formRef.current;
        form!.validateFields().then(async (values: any) => {
            setIsLoading(true)
            const entity = {
                ...values,
                capDVHC: values.capDVHC.join(","),
            }
            const response = await bieuMauService.createOrUpdate(entity);
            if (response.code == 1) {
                notification.success({ message: response.message })
            } else {
                notification.error({ message: response.message })
            }
            setBieuModal({ visible: false, entity: null })
            await getAll();
            form!.resetFields();
            setIsLoading(false)
        });
    }

    useEffect(() => {
        getAll();
    }, [])


    return (
        <div className='bieu-page-wrapper'>
            <h1 className='txt-page-header'>Quản lý biểu</h1>

            <Card title={
                <div className='table-header-layout'>
                    <div style={{ flex: 1 }}>
                    </div>
                    <div className='table-header-layout-right'>
                        <Button type="primary" icon={<PlusOutlined />} onClick={() => createOrUpdateModalOpen()}>
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
                    dataSource={listBieu}
                    locale={{
                        emptyText: (
                            <Empty description="Không có dữ liệu"> </Empty>
                        ),
                    }}
                />
            </Card>
            <CreateOrUpdateBieuModal
                title='Quản lý biểu'
                formRef={formRef}
                visible={bieuModal.visible}
                confirmLoading={isLoading}
                onCancel={() => {
                    setBieuModal({ visible: false, entity: null })
                    formRef.current?.resetFields();
                }}
                entity={bieuModal.entity}
                onCreate={handleCreate}
            />

        </div>
    )
}
