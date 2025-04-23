import React, { useEffect, useState } from 'react'
import fileService from '../../../services/files/fileService';
import { Button, Empty, notification, Table } from 'antd';
import { CloudDownloadOutlined } from '@ant-design/icons';
import moment from 'moment';
import utils from '../../../utils/utils';
var FileSaver = require('file-saver');

export default function SyncFileTable(props: any) {
    const donViHanhChinhSelected = props.donViHanhChinhSelected;
    const isLoading = props.isLoading;
    const [files, setFiles] = useState<any[]>([])
    const [isDowloading, setIsDowloading] = useState({
        loading: false,
        id: null
    })

    const getListDanhSachTepDinhKem = async () => {
        const response = await fileService.getDanhSachDongBo({
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
    }, [donViHanhChinhSelected?.id, props.isRefresh])

    const onDownloadFile = async (record: any) => {
        if (!utils.checkQuyenAction("Pages.Report.DownloadFile")) {
            return
        }

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
            dataIndex: 'createName',
            render: (text: string, record: any) => <span>{record?.createName}</span>
        },
        {
            title: 'Tải xuống', dataIndex: 'download', key: 'download', width: 120, align: "center",
            render: (text: string, record: any) =>
                <Button loading={isDowloading.loading && isDowloading.id == record.id} onClick={() => {
                    onDownloadFile(record)
                }} type='primary' icon={<CloudDownloadOutlined />} />


        }
    ];

    return (
        <div>
            <Table
                key="id"
                size='small'
                pagination={false}
                loading={isLoading}
                columns={columns}
                locale={{
                    emptyText: (
                        <Empty description="Không có dữ liệu"> </Empty>
                    ),
                }}
                scroll={{ y: 200 }}
                dataSource={files} onChange={() => { }} />
        </div>
    )
}
