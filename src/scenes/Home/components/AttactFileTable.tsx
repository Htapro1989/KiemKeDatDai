import { Button, Empty, notification, Popconfirm, Table } from 'antd';
import React, { useEffect, useState } from 'react'
import fileService from '../../../services/files/fileService';
import { CloudDownloadOutlined, DeleteOutlined } from '@ant-design/icons';
import moment from 'moment';
import { handleCommontResponse } from '../../../services/common/handleResponse';

var FileSaver = require('file-saver');

export default function AttactFileTable(props: any) {
    const donViHanhChinhSelected = props.donViHanhChinhSelected;
    const isLoading = props.isLoading;
    const [files, setFiles] = useState<any[]>([])
    const [isRefresh, setIsRefresh] = useState(props.isRefresh)
    const [isDowloading, setIsDowloading] = useState({
        loading: false,
        id: null
    })

    const getListDanhSachTepDinhKem = async () => {
        const response = await fileService.getDanhSachTepDinhKem({
            id: donViHanhChinhSelected?.id,
            year: donViHanhChinhSelected?.year,
            maxResultCount: 100,
            skipCount: 0
        })
        if (response.code == 1) {
            setFiles(response.returnValue)
        }
    }
    useEffect(() => {
        if (donViHanhChinhSelected?.id) {
            getListDanhSachTepDinhKem()
        }
    }, [donViHanhChinhSelected?.id, props.isRefresh, isRefresh])

    const onDeleteFile = async (id: any) => {
        const response = await fileService.deleteFileAttact(id);
        handleCommontResponse(response);
        setIsRefresh(Date.now())
    }

    const onDownloadFile = async (record: any) => {
        setIsDowloading({ loading: true, id: record.id })
        const response = await fileService.downloadAttactFileById(record.id)
        setIsDowloading({ loading: false, id: record.id })
        if (response) {
            FileSaver.saveAs(response, record.fileName);
        } else {
            notification.error({ message: "Thất bại. Vui lòng thử lại sau" })
        }

    }


    const columns: any = [
        {
            title: 'Tên tệp',
            dataIndex: 'fileName',
        },
        {
            title: 'Ngày tạo',
            dataIndex: 'creationTime',
            render: (text: string, record: any) => <span>{moment(text).format("DD/MM/YYYY")}</span>
        },
        {
            title: 'Người tạo',
            dataIndex: 'name',
            render: (text: string, record: any) => <span>{donViHanhChinhSelected?.name}</span>
        },
        {
            title: 'Tải xuống', dataIndex: 'download', key: 'download', width: 120, align: "center",
            render: (text: string, record: any) =>
                <Button loading={isDowloading.loading && isDowloading.id == record.id} onClick={() => {
                    onDownloadFile(record)
                }} type='primary' icon={<CloudDownloadOutlined />} />


        },
        {
            title: 'Xóa', dataIndex: 'delete', key: 'delete', width: 120, align: "center",
            render: (text: string, record: any) => <Popconfirm
                title="Bạn muốn xóa tệp này?"
                onConfirm={() => { onDeleteFile(record.id) }}
                onCancel={() => { }}
                okText="Xóa"
                cancelText="Hủy bỏ"
            >
                <Button type='ghost' icon={<DeleteOutlined />} />
            </Popconfirm>


        },

    ];

    return (
        <div>
            <Table
                key="id"
                size='small'
                pagination={false}
                loading={isLoading}
                columns={columns}
                scroll={{ y: 200 }}
                locale={{
                    emptyText: (
                        <Empty description="Không có dữ liệu"> </Empty>
                    ),
                }}
                dataSource={files} onChange={() => { }} />
        </div>
    )
}
