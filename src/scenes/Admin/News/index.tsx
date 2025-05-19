import React, { useEffect, useState } from 'react'
import './index.less'
import { Button, Card, Col, Dropdown, Empty, Input, Menu, Modal, notification, Row, Table, Tag } from 'antd'
import { PlusOutlined, SettingOutlined } from '@ant-design/icons'
import newsService from '../../../services/news/newsService'
import CreateOrUpdateNewsModal from './components/CreateOrUpdateKyKiemKe'
import { FormInstance } from 'antd/lib/form'
const confirm = Modal.confirm;
const Search = Input.Search;

export default function NewsPage() {
    const formRef = React.createRef<FormInstance>();
    const [news, setnews] = useState<any>([])
    const [isOpenDrawer, setIsOpenDrawer] = useState({
        visible: false,
        entity: null
    })
    const [isLoading, setIsLoading] = useState(false)
    const [filterData, setFilterData] = useState({
        filter: '',
        maxResultCount: 10,
        skipCount: 0
    })


    const getNews = async (dataFilter?: any) => {
        const response = await newsService.getNewsPagging(dataFilter || filterData);
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
            setIsLoading(true)

            const response = await newsService.createOrUpdateNews({
                ...values,
                fileData: values.fileData
            })
            if (response.code == 1) {
                notification.success({ message: response.message })
                await getNews();
                form!.resetFields();
                setIsOpenDrawer({ visible: false, entity: null })
            } else {
                notification.error({ message: response.message })
            }
            setIsLoading(false)
        });
    }
    const createOrUpdateModalOpen = (item?: any) => {
        setIsOpenDrawer({
            visible: true,
            entity: item
        })
    }
    const handleTableChange = (pagination: any) => {
        const newFilterData = {
            ...filterData,
            skipCount: (pagination.current - 1) * filterData.maxResultCount!
        }
        setFilterData(newFilterData)
        getNews(newFilterData)
    };

    const handleSearch = (value: string) => {
        const newFilterData = {
            ...filterData,
            filter: value
        }
        setFilterData(newFilterData)
        getNews(newFilterData)
    };

    const columns: any = [
        {
            title: 'Cột', dataIndex: 'type', key: 'type',
            render: (text: string) => {
                return <span>
                    {
                        text == '1' ? 'VĂN BẢN CHỈ ĐẠO' : text == '2'
                            ? 'HỆ THỐNG THỐNG KÊ KIỂM KÊ ĐẤT ĐAI' : text == '3'
                                ? 'TRAO ĐỔI, THẢO LUẬN' : ''
                    }
                </span>
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
        { title: 'Nội dung', dataIndex: 'content', key: 'content' },
        { title: 'Tên tệp', dataIndex: 'fileName', key: 'fileName' },
        {
            title: 'Thứ tự',
            dataIndex: 'orderLabel', key: 'orderLabel',
            width: 100
        },
        {
            title: 'Trạng thái', dataIndex: 'status', key: 'status',
            width: 100,
            align: "center",
            render: (text: string, item: any) => (
                <div>
                    {
                        text == '1' ? <Tag color="success">Hiển thị</Tag> : <Tag color="error">Ẩn</Tag>
                    }
                </div>
            )
        },
        {
            title: 'Hành động',
            width: 70,
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
                    <div className='table-header-layout-right'>
                        <Row style={{ width: '100%' }}>
                            <Col>
                                <Search placeholder={'Tìm kiếm...'} onSearch={handleSearch} />
                            </Col>
                            <Col flex={1} />
                            <Col>
                                <Button type="primary" icon={<PlusOutlined />} onClick={() => { createOrUpdateModalOpen() }}>
                                    Tạo mới
                                </Button>
                            </Col>
                        </Row>

                    </div>
                </div>
            }>
                <Table
                    rowKey={"id"}
                    bordered={true}
                    columns={columns}
                    size='small'
                    scroll={{ y: 600 }}
                    pagination={{ pageSize: 10, total: news === undefined ? 0 : news.totalCount, defaultCurrent: 1 }}
                    loading={false}
                    onChange={handleTableChange}
                    dataSource={news === undefined ? [] : news.items}
                    locale={{
                        emptyText: (
                            <Empty description="Không có dữ liệu"> </Empty>
                        ),
                    }}
                />
            </Card>

            <CreateOrUpdateNewsModal
                title='Quản lý tin tức'
                formRef={formRef}
                visible={isOpenDrawer.visible}
                entity={isOpenDrawer.entity}
                confirmLoading={isLoading}
                onCancel={() => {
                    setIsOpenDrawer({ visible: false, entity: null })
                }}
                onCreate={handleCreate}
            />

        </div>
    )
}
