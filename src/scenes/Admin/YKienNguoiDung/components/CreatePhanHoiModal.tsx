import React from 'react'
import FormModal from '../../../../components/Modal/FormModal'
import { Col, Form, Input, Row } from 'antd'
import rules from '../../../../constants/index.validation'
import UploadFileButton from '../../../../components/Upload'

export default function CreatePhanHoiModal(props: any) {

    const onUploadFile = async (file: any) => {
        props.formRef.current?.setFieldsValue({ fileName: file.name, file: file });
    }

    return (
        <FormModal
            {...props}
        >
            <Row gutter={16}>
                <Col span={12}>
                    <Form.Item label={"Người gửi"} name={'name'} rules={rules.required}>
                        <Input placeholder='Họ và tên' />
                    </Form.Item>
                </Col>
                <Col span={12}>
                    <Form.Item label={"Email"} name={'email'} rules={rules.required}>
                        <Input placeholder='Email' />
                    </Form.Item>
                </Col>
            </Row>
            <Row gutter={16}>
                <Col span={12}>
                    <Form.Item label={"Đơn vị công tác"} name={'donViCongTac'} rules={rules.required}>
                        <Input placeholder='Đơn vị công tác' />
                    </Form.Item>
                </Col>
                <Col span={12}>
                    <Form.Item label={"Kỳ kiểm kê"} name={'year'} rules={rules.required}>
                        <Input placeholder='Năm kiểm kê' />
                    </Form.Item>
                </Col>
            </Row>
            <Form.Item label={"Số điện thoại"} name={'phone'} rules={rules.required}>
                <Input placeholder='Số điện thoại' />
            </Form.Item>

            <Form.Item label={"Ý kiến"} name={'noiDungYKien'} rules={rules.required}>
                <Input.TextArea
                    rows={4}
                    placeholder='Nhập ý kiến' />
            </Form.Item>
            <Row align='bottom' gutter={16}>
                <Col flex={1}>
                    <Form.Item label={"Tên tệp"} name={'fileName'}>
                        <Input placeholder='Tên tệp' disabled={true} />
                    </Form.Item>
                </Col>
                <UploadFileButton
                    style={{ marginBottom: 24 }}
                    title='' type='primary' ghost
                    hideFileSelected={true}
                    accept="*"
                    onUpload={onUploadFile} />
            </Row>


            <Form.Item label={"Tên tệp"} name={'file'} hidden={true}>
            </Form.Item>
        </FormModal>
    )
}
