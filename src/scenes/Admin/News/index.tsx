import React, { useEffect, useState } from 'react'
import './index.less'
import { Button, Card, Dropdown, Menu, Modal, notification, Table } from 'antd'
import { PlusOutlined, SettingOutlined } from '@ant-design/icons'
import newsService from '../../../services/news/newsService'
import CreateOrUpdateNewsModal from './components/CreateOrUpdateKyKiemKe'
import { FormInstance } from 'antd/lib/form'
const confirm = Modal.confirm;
export default function NewsPage() {
    const formRef = React.createRef<FormInstance>();
    const [news, setnews] = useState<any>([])
    const [isOpenDrawer, setIsOpenDrawer] = useState({
        visible: false,
        entity: null
    })


    const getNews = async () => {
        const response = await newsService.getNewsPagging()
        setnews(response.returnValue)
    }

    const onDelete = async (id: any) => {
        const response = await newsService.deleteNews(id);
        if (response.code == 1) {
            notification.success({ message: "Xóa thành công" })
            getNews()
        } else {
            notification.error({ message: "Xóa thất bại" })
        }
    }

    const onDeleteRow = (id: any) => {
        confirm({
            title: 'Bạn muốn xóa hàng này',
            onOk() {
                onDelete(id);
            },
            onCancel() {
                console.log('Cancel');
            },
        });
    }

    useEffect(() => {
        getNews()
    }, [])

    const handleCreate = async () => {
        const form = formRef.current;
        form!.validateFields().then(async (values: any) => {
            const response = await newsService.createOrUpdateNews(values)
            if (response.code == 1) {
                notification.success({ message: response.message })
                await getNews();
                form!.resetFields();
                setIsOpenDrawer({ visible: false, entity: null })
            } else {
                notification.error({ message: response.message })
            }
        });
    }
    const createOrUpdateModalOpen = (item?: any) => {
        setIsOpenDrawer({
            visible: true,
            entity: item
        })
    }

    const columns = [
        {
            title: 'Cột', dataIndex: 'type', key: 'type',
            width: 350,
            render: (text: string) => {
                return <span>{text == '1' ? 'VĂN BẢN CHỈ ĐẠO' : text == '2' ? 'HỆ THỐNG THỐNG KÊ KIỂM KÊ ĐẤT ĐAI' : text == '3' ? 'TRAO ĐỔI, THẢO LUẬN' : ''}</span>
            },
            filters: [
                {
                    text: 'VĂN BẢN CHỈ ĐẠO',
                    value: '1',
                },
                {
                    text: 'HỆ THỐNG THỐNG KÊ KIỂM KÊ ĐẤT ĐAI',
                    value: '2',
                },
                {
                    text: 'TRAO ĐỔI, THẢO LUẬN',
                    value: '3',
                },
            ],
            onFilter: (value: any, record: any) => record.type == value,
        },
        { title: 'Tiêu đề tin tức', dataIndex: 'title', key: 'title' },
        {
            title: 'Thứ tự',
            width: 250,
            dataIndex: 'orderLabel', key: 'orderLabel'
        },
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
                                <Menu.Item onClick={() => onDeleteRow(item.id)}>Xóa</Menu.Item>
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
            <h1 className='txt-page-header'>Cấu hình tin tức</h1>

            <Card title={
                <div className='table-header-layout'>
                    <div style={{ flex: 1 }}>
                    </div>
                    <div className='table-header-layout-right'>
                        <Button type="primary" icon={<PlusOutlined />} onClick={() => { createOrUpdateModalOpen() }}>
                            Tạo mới
                        </Button>
                    </div>
                </div>
            }>
                <Table
                    rowKey={"id"}
                    bordered={true}
                    columns={columns}
                    scroll={{ y: 600 }}
                    // pagination={{ pageSize: 10, total: news === undefined ? 0 : news.totalCount, defaultCurrent: 1 }}
                    pagination={false}
                    loading={false}
                    dataSource={news === undefined ? [] : news.items}
                />
            </Card>

            <CreateOrUpdateNewsModal
                title='Quản lý tin tức'
                formRef={formRef}
                visible={isOpenDrawer.visible}
                entity={isOpenDrawer.entity}
                confirmLoading={false}
                onCancel={() => {
                    setIsOpenDrawer({ visible: false, entity: null })
                }}
                onCreate={handleCreate}
            />

        </div>
    )
}
