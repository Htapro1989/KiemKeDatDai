import { Modal, notification } from 'antd'
import React from 'react'
import newsService from '../../../services/news/newsService';
var FileSaver = require('file-saver');

interface IModalProps {
    visible: boolean
    onClose: () => void
    data: any;
    type: string;
}

export default function NewModals(props: IModalProps) {

    const onDownloadFile = async () => {
        if (!props.data?.fileId) return;
        const response = await newsService.downloadNewsFileById(props.data?.fileId);
        if (response) {
            FileSaver.saveAs(response, props.data?.fileName);
        } else {
            notification.error({ message: "Thất bại. Vui lòng thử lại sau" })
        }
    }

    return (
        <Modal
            visible={props.visible}
            onCancel={props.onClose}
            footer={null}
            title={props.type}
            width={1000}
        >
            <div>
                <h3>{props.data?.title}</h3>
                <div style={{ marginTop: 16, marginBottom: 16 }}>{props.data?.content}</div>
                {
                    props.data?.fileId && <div>
                        <div>Tệp đính kèm: <span
                            onClick={onDownloadFile}
                            style={{
                                color: '#F4600C',
                                cursor: 'pointer',
                                textDecoration: 'underline'
                            }}
                        >{props.data?.fileName}</span></div>
                    </div>
                }

            </div>
        </Modal>
    )
}
