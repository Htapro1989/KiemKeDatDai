import { Modal } from 'antd';
import { ButtonProps } from 'antd/lib/button';
import Form from 'antd/lib/form';
import React from 'react'

export interface IFormModalProps {
    visible: boolean;
    onCancel: () => void;
    onCreate: () => void;
    formRef: any;

    cancelText?: string;
    okText?: string;
    title?: string;
    destroyOnClose?: boolean;
    children?: any
    confirmLoading?: boolean
    okButtonProps?: ButtonProps
}


function FormModal(props: IFormModalProps) {
    const { formRef, onCancel, onCreate, visible, cancelText = "Hủy bỏ", okText = "Đồng ý",
        title, destroyOnClose = true, children, confirmLoading, okButtonProps } = props
    return (
        <Modal
            visible={visible}
            cancelText={cancelText}
            okText={okText}
            onCancel={onCancel}
            onOk={onCreate}
            title={title}
            closeIcon={false}
            confirmLoading={confirmLoading}
            okButtonProps={okButtonProps}
            destroyOnClose={destroyOnClose}>
            <Form layout='vertical' ref={formRef}>
                {children}
            </Form>
        </Modal>

    )
}

export default FormModal