import React from 'react'
import FormModal from '../../../../components/Modal/FormModal'
import { Col, Form, Input, Row } from 'antd'
import rules from '../../../../constants/index.validation'

export default function CreatePhanHoiModal(props: any) {
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

            <Form.Item label={"Ý kiến"} name={'noiDungYKien'} rules={rules.required}>
                <Input.TextArea
                    rows={4}
                    placeholder='Nhập ý kiến' />
            </Form.Item>



        </FormModal>
    )
}
