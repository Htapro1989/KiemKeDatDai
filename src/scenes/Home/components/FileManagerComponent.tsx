import { Card, notification } from 'antd'
import React, { useState } from 'react'
import AttactFileTable from './AttactFileTable'
import UploadFileButton from '../../../components/Upload'
import fileService from '../../../services/files/fileService'
import SyncFileTable from './SyncFileTable'

export default function FileManagerComponent(props: any) {
    const donViHanhChinhSelected = props.donViHanhChinhSelected
    const [isLoading, setIsLoading] = useState(false)
    const [isRefresh, setIsRefresh] = useState<any>()

    const onUploadFile = async (file: any) => {
        setIsLoading(true)
        const response = await fileService.fileSpecificationsUpload(file, donViHanhChinhSelected?.id, donViHanhChinhSelected?.year);
        setIsLoading(false)
        if (response.code == 1) {
            notification.success({ message: response.message })
            setIsRefresh(Date.now())
        } else {
            notification.error({
                message: response?.message ? response?.message : "Thất bại. Vui lòng thử lại"
            })
        }
    }
    return (
        <div>
            <Card title="Quản lý dữ liệu tệp đính kèm" size='small'
                extra={
                    <UploadFileButton
                        title='Tải lên' type='primary' ghost
                        hideFileSelected={true}
                        accept="*"
                        onUpload={onUploadFile} />}>
                <AttactFileTable
                    isLoading={isLoading}
                    isRefresh={isRefresh}
                    donViHanhChinhSelected={donViHanhChinhSelected} />
            </Card>

            <Card
            style={{ marginTop: 24 }}
            title="Quản lý dữ liệu đồng bộ" size='small'>
                <SyncFileTable
                    isLoading={isLoading}
                    isRefresh={isRefresh}
                    donViHanhChinhSelected={donViHanhChinhSelected} />
            </Card>


        </div>
    )
}
