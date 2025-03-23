import React from 'react'
import { Form, Input } from "antd";
import FormModal, { IFormModalProps } from '../../../../components/Modal/FormModal';
import rules from '../../../../constants/index.validation';


export interface ICreateOrUpdateCapDVHCProps extends IFormModalProps {
}

function CreateOrUpdateKyKiemKeModal(props: ICreateOrUpdateCapDVHCProps) {

    return (
        <FormModal {...props}>
            <Form.Item label={"Tên kỳ"} name={'name'} rules={rules.required}>
                <Input placeholder='Nhập' />
            </Form.Item>
            <Form.Item label={"Mã"} name={'ma'} rules={rules.required}>
                <Input placeholder='Nhập' />
            </Form.Item>
            <Form.Item label={"Năm"} name={'year'} rules={rules.requiredAndNumberOnly}>
                <Input placeholder='Nhập' />
            </Form.Item>
            <Form.Item hidden name={'id'} />

        </FormModal>

    )
}
export default CreateOrUpdateKyKiemKeModal