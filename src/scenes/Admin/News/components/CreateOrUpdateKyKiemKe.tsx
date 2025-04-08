import React, { useEffect } from 'react'
import { Form, Input, Select } from "antd";
import FormModal, { IFormModalProps } from '../../../../components/Modal/FormModal';
import rules from '../../../../constants/index.validation';


export interface ICreateOrUpdateCapDVHCProps extends IFormModalProps {
    entity: any
}
function CreateOrUpdateNewsModal(props: ICreateOrUpdateCapDVHCProps) {

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
            <Form.Item label={"Nội dung"} name={'title'} rules={rules.required}>
                <Input.TextArea
                    rows={8}
                    placeholder='Nhập tiêu đề tin tức' />
            </Form.Item>
            <Form.Item label={"Thứ tự"} name={'orderLabel'} rules={rules.required}>
                <Input placeholder='Nhập thứ tự' />
            </Form.Item>
            <Form.Item hidden name={'id'} />
        </FormModal>

    )
}
export default CreateOrUpdateNewsModal