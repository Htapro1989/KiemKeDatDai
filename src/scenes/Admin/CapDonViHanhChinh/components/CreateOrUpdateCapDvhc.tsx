import React from 'react'
import { Form, Input } from "antd";
import FormModal, { IFormModalProps } from '../../../../components/Modal/FormModal';
import SelectItem from '../../../../components/Select/SelectItem';
import dvhcService from '../../../../services/dvhc/dvhcService';
import rules from '../../../../constants/index.validation';


export interface ICreateOrUpdateCapDVHCProps extends IFormModalProps {
}

function CreateOrUpdateCapDVHCModal(props: ICreateOrUpdateCapDVHCProps) {
    const renderParentItem = () => {
        return (
            <SelectItem
                formItemProps={{ label: "Kỳ thống kê/kiểm kê", name: "year", rules: rules.required }}
                remoteSource={dvhcService.getKyKiemKeAsOption.bind(null)}
                refresing={true}
                className='mb-0'
                span={24}
            />
        )
    }
    return (
        <FormModal {...props}>
            <Form.Item label={"Tên cấp đơn vị"} name={'name'} rules={rules.required}>
                <Input placeholder='Nhập' />
            </Form.Item>
            <Form.Item label={"Mã cấp đơn vị"} name={'maCapDVHC'} rules={rules.requiredAndNumberOnly}>
                <Input placeholder='Nhập' />
            </Form.Item>
            {renderParentItem()}
            <Form.Item hidden name={'id'} />

        </FormModal>

    )
}
export default CreateOrUpdateCapDVHCModal