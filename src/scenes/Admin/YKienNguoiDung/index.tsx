import React, { useEffect, useState } from 'react'
import "./index.less";
import { Button, Card, Empty, notification, Table } from 'antd';
import ykienService from '../../../services/ykien/ykienService';
import { CloudDownloadOutlined, MessageOutlined } from '@ant-design/icons';
import PhanHoiModal from './components/PhanHoiModal';
import { FormInstance } from 'antd/lib/form';
var FileSaver = require('file-saver');

export default function YKienNguoiDungPage() {
    const [yKienList, setYKienList] = useState<any>([])
    const formRef = React.createRef<FormInstance>();
    const [isDowloading, setIsDowloading] = useState(false)

    const [phanHoiModal, setPhanHoiModal] = useState({
        visible: false,
        entity: null
    })


    const getAllYKien = async () => {
        const response = await ykienService.getAll({
            maxResultCount: 100,
            skipCount: 0
        })
        setYKienList(response.returnValue)
    }

    const handleCreate = async () => {
        const form = formRef.current;
        form!.validateFields().then(async (values: any) => {
            const response = await ykienService.createOrUpdate(values)
            if (response.code == 1) {
                notification.success({ message: response.message })
                await getAllYKien();
                form!.resetFields();
                setPhanHoiModal({ visible: false, entity: null })
            } else {
                notification.error({ message: response.message })
            }
        });
    }

    const createOrUpdateModalOpen = (item?: any) => {
        setPhanHoiModal({
            visible: true,
            entity: item
        })
    }

    const onDownloadFile = async (item: any) => {
        setIsDowloading(true)
        const response = await ykienService.downloadFileByID(item.fileId)
        if (response) {
            FileSaver.saveAs(response);
        } else {
            notification.error({ message: "Thất bại. Vui lòng thử lại sau" })
        }
        setIsDowloading(false)
    }


    useEffect(() => {
        getAllYKien()
    }, [])

    const columns: any = [
        { title: 'Người gửi', dataIndex: 'name', key: 'name' },
        { title: 'Email', dataIndex: 'email', key: 'email' },
        { title: 'Đơn vị công tác', dataIndex: 'donViCongTac', key: 'donViCongTac' },
        { title: 'Nội dung ý kiến', dataIndex: 'noiDungYKien', key: 'noiDungYKien' },
        { title: 'Nội dung phản hồi', dataIndex: 'noiDungTraLoi', key: 'noiDungTraLoi' },
        {
            title: 'Tệp đính kèm', dataIndex: 'fileName', key: 'fileName', align: 'center',
            width: 120,
            render: (text: string, item: any) => (
                <div>
                    {
                        text && (<Button
                            loading={isDowloading}
                            onClick={() => { onDownloadFile(item) }}
                            type="primary" ghost icon={<CloudDownloadOutlined />} />)
                    }
                </div>
            ),
        },
        {
            title: 'Gửi phản hồi',
            width: 120,
            align: 'center',
            render: (text: string, item: any) => (
                <div>
                    <Button
                        onClick={() => { createOrUpdateModalOpen(item) }}
                        type="primary" icon={<MessageOutlined />} />
                </div>
            ),
        },
    ];

    return (
        <div className='ykien-page-wrapper'>
            <h1 className='txt-page-header'>Ý kiến người dùng</h1>

            <Card title={
                <div className='table-header-layout'>
                    <div style={{ flex: 1 }}>
                    </div>
                    <div className='table-header-layout-right'>
                        {/* <Button type="primary" icon={<PlusOutlined />} onClick={() => { createOrUpdateModalOpen() }}>
                            Tạo mới
                        </Button> */}
                    </div>
                </div>
            }>
                <Table
                    rowKey={"id"}
                    bordered={true}
                    columns={columns}
                    size='small'
                    scroll={{ y: 600 }}
                    // pagination={{ pageSize: 10, total: news === undefined ? 0 : news.totalCount, defaultCurrent: 1 }}
                    pagination={false}
                    loading={false}
                    dataSource={yKienList === undefined ? [] : yKienList.items}
                    locale={{
                        emptyText: (
                            <Empty description="Không có dữ liệu"> </Empty>
                        ),
                    }}
                />
            </Card>

            <PhanHoiModal
                title='Quản lý ý kiến người dùng'
                formRef={formRef}
                visible={phanHoiModal.visible}
                entity={phanHoiModal.entity}
                confirmLoading={false}
                onCancel={() => {
                    setPhanHoiModal({ visible: false, entity: null })
                }}
                onCreate={handleCreate}
            />
        </div>
    )
}
