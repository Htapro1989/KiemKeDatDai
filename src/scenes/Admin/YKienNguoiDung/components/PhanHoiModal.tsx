import React, { useEffect } from 'react'
import FormModal from '../../../../components/Modal/FormModal'
import { Form, Input } from 'antd'
import rules from '../../../../constants/index.validation'

export default function PhanHoiModal(props: any) {

    useEffect(() => {
        if (props.visible) {
            setTimeout(() => {
                props.formRef.current?.setFieldsValue({ ...props.entity });
            }, 100);
        }
    }, [props.visible])

    return (
        <FormModal
            {...props}
        >
            <Form.Item label={"Người gửi"} name={'name'}>
                <Input disabled/>
            </Form.Item>
            <Form.Item hidden name={'id'} />

            <Form.Item label={"Nội dung phản hồi"} name={'noiDungTraLoi'} rules={rules.required}>
                <Input.TextArea
                    rows={8}
                    placeholder='Nhập nội dung phản hồi' />
            </Form.Item>
            <Form.Item hidden name={'id'} />
        </FormModal>
    )
}
