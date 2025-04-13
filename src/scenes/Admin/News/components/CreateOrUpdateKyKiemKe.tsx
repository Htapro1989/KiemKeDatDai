import React, { useEffect } from 'react'
import { Col, Form, Input, Row, Select } from "antd";
import FormModal, { IFormModalProps } from '../../../../components/Modal/FormModal';
import rules from '../../../../constants/index.validation';
import UploadFileButton from '../../../../components/Upload';


export interface ICreateOrUpdateCapDVHCProps extends IFormModalProps {
    entity: any
}
function CreateOrUpdateNewsModal(props: ICreateOrUpdateCapDVHCProps) {

    const onUploadFile = async (file: any) => {
        props.formRef.current?.setFieldsValue({ fileName: file.name, fileData: file });
    }

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
            <Form.Item label={"Cột"} name={'type'} rules={rules.required}>
                <Select
                    options={[
                        { label: 'Văn bản chỉ đạo', value: 1 },
                        { label: 'Hệ thống thống kê, kiểm kê đất đai', value: 2 },
                        { label: 'Trao đổi, thảo luận', value: 3 },]}
                />
            </Form.Item>
            <Form.Item label={"Tiêu đề"} name={'title'} rules={rules.required}>
                <Input.TextArea
                    rows={2}
                    placeholder='Nhập tiêu đề tin tức' />
            </Form.Item>
            <Form.Item label={"Nội dung"} name={'content'} rules={rules.required}>
                <Input.TextArea
                    rows={8}
                    placeholder='Nhập nội dung' />
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

            <Row gutter={16}>
                <Col span={12}>
                    <Form.Item label={"Trạng thái"} name={'status'} rules={rules.required}>
                        <Select
                            options={[
                                { label: 'Ẩn', value: 0 },
                                { label: 'Hiển thị', value: 1 }
                            ]}
                        />
                    </Form.Item>
                </Col>

                <Col span={12}>
                    <Form.Item label={"Thứ tự"} name={'orderLabel'} rules={rules.required}>
                        <Input placeholder='Nhập thứ tự' />
                    </Form.Item>
                </Col>
            </Row>

            <Form.Item hidden name={'id'} />
            <Form.Item hidden name={'fileData'} />
        </FormModal>

    )
}
export default CreateOrUpdateNewsModal