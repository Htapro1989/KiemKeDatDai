import React, { useEffect, useState } from 'react'
import { Checkbox, Form, Input } from "antd";
import FormModal, { IFormModalProps } from '../../../../components/Modal/FormModal';
import rules from '../../../../constants/index.validation';
import SelectItem from '../../../../components/Select/SelectItem';
import dvhcService from '../../../../services/dvhc/dvhcService';


export interface ICreateOrUpdateCapDVHCProps extends IFormModalProps {
    entity: any;
}

function CreateOrUpdateBieuModal(props: ICreateOrUpdateCapDVHCProps) {
    const [capDvhcOption, setCapDvhcOption] = useState([])


    useEffect(() => {
        if (props.visible) {
            setTimeout(() => {
                props.formRef.current?.setFieldsValue({ ...props.entity });
            }, 100);
        }
    }, [props.visible])

    useEffect(() => {
        dvhcService.getDropDownCapDVHC().then((res: any) => {
            setCapDvhcOption(res);
        })
    }, [])


    return (
        <FormModal {...props}>
            <Form.Item label={"Ký hiệu"} name={'kyHieu'} rules={rules.required}>
                <Input placeholder='Nhập' />
            </Form.Item>
            <Form.Item label={"Tên biểu"} name={'noiDung'} rules={rules.required}>
                <Input.TextArea rows={3} placeholder='Nhập' />
            </Form.Item>
            <SelectItem
                formItemProps={{ label: "Kỳ thống kê/kiểm kê", name: "year", rules: rules.required }}
                remoteSource={dvhcService.getKyKiemKeAsOption.bind(null)}
                refresing={true}
                className='mb-0'
                span={24}
            />
            <Form.Item label={"Cấp DVHC"} name={'capDVHC'} rules={rules.required}>

                <Checkbox.Group
                    style={{ display: 'flex', flexDirection: 'column' }}
                    options={capDvhcOption} />


            </Form.Item>
            <Form.Item hidden name={'id'} />

        </FormModal>

    )
}
export default CreateOrUpdateBieuModal