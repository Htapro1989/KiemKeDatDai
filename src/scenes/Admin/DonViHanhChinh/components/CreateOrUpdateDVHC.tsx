import React, { useEffect, useState } from 'react'
import FormModal, { IFormModalProps } from '../../../../components/Modal/FormModal'
import { Col, Form, Input, Row } from 'antd'
// import { FormInstance } from 'antd/lib/form';
import rules from '../../../../constants/index.validation';
import SelectItem from '../../../../components/Select/SelectItem';
import dvhcService from '../../../../services/dvhc/dvhcService';
import { CAP_DVHC_ENUM } from '../../../../models/enum';

export interface ICreateOrUpdateDVHCProps extends IFormModalProps {
    entity?: any
}

export default function CreateOrUpdateDVHC(props: ICreateOrUpdateDVHCProps) {

    const [formState, setFormState] = useState<any>();
    const updateState = (newState: any) => { setFormState({ ...formState, ...newState }) }

    const clearFields = (capDVHCId: any) => {
        // let fieldNames;
        // if (capDVHCId == CAP_DVHC_ENUM.TRUNG_UONG || capDVHCId == CAP_DVHC_ENUM.VUNG) {
        // } else if (capDVHCId == CAP_DVHC_ENUM.TINH) {
        //     props.formRef.current?.resetFields(['maVung']);
        // }else if(capDVHCId == CAP_DVHC_ENUM.HUYEN){

        // }



    }

    const setFormData = () => {
        props.formRef.current?.setFieldsValue({
            name: props.entity?.name,
            year: props.entity?.year,
            capDVHCId: props.entity?.capDVHCId,
            maVung: props.entity?.maVung,
            maTinh: props.entity?.maTinh,
            maHuyen: props.entity?.maHuyen,
            id: props.entity?.id,
        });

    }

    useEffect(() => {
        setFormState({
            maVung: props.entity?.maVung,
            maTinh: props.entity?.maTinh,
            maHuyen: props.entity?.maHuyen,
            maXa: props.entity?.maXa,
            capDVHCId: props.entity?.capDVHCId,
        })
        if (props.visible) {
            setTimeout(() => {
                setFormData()
            }, 500);
        }

    }, [props.visible])

    const genKyKiemKe = () => {
        return (
            <SelectItem
                formItemProps={{ label: "Kỳ thống kê/kiểm kê", name: "year", rules: rules.required }}
                remoteSource={dvhcService.getKyKiemKeAsOption.bind(null)}
                refresing={true}
                className='mb-0'
                span={12}
            />
        )
    }
    const genCapDonViHanhChinh = () => {
        return (
            <SelectItem
                formItemProps={{ label: "Cấp đơn vị hành chính", name: "capDVHCId", rules: rules.required }}
                remoteSource={dvhcService.getDropDownCapDVHC.bind(null)}
                refresing={true}
                className='mb-0'
                span={12}
                selectProps={{
                    onChange: (value: any) => {
                        clearFields(value);
                        updateState({ capDVHCId: value });
                    },
                }}
            />
        )
    }
    const genVung = () => {
        if (formState?.capDVHCId == CAP_DVHC_ENUM.TRUNG_UONG || formState?.capDVHCId == CAP_DVHC_ENUM.VUNG) return null;
        return (
            <SelectItem
                formItemProps={{ label: "Chọn Vùng", name: "maVung", rules: rules.required }}
                remoteSource={dvhcService.getDropDownVung.bind(null)}
                refresing={true}
                className='mb-0'
                span={12}
                selectProps={{
                    onChange: (value: any) => {
                        updateState({ maVung: value });
                    },
                }}
            />
        )
    }
    const genTinh = () => {
        if (formState?.capDVHCId <= CAP_DVHC_ENUM.TINH) return null;
        return (
            <SelectItem
                formItemProps={{
                    label: "Chọn Tỉnh", name: "maTinh", rules: rules.required,
                }}
                remoteSource={dvhcService.getDropDownTinh.bind(null, formState?.maVung)}
                refresing={formState?.maVung}
                className='mb-0'
                span={12}
                selectProps={{
                    onChange: (value: any) => {
                        updateState({ maTinh: value });
                    },
                }}
            />
        )
    }
    const genHuyen = () => {
        if (formState?.capDVHCId <= CAP_DVHC_ENUM.HUYEN) return null;
        return (
            <SelectItem
                formItemProps={{ label: "Chọn Huyện", name: "maHuyen", rules: rules.required }}
                remoteSource={dvhcService.getDropDownHuyen.bind(null, formState?.maTinh)}
                refresing={formState?.maTinh}
                className='mb-0'
                span={12}
            />
        )
    }
    return (
        <FormModal {...props}>
            <Row gutter={[12, 12]}>

                {genKyKiemKe()}
                {genCapDonViHanhChinh()}
                {genVung()}
                {genTinh()}
                {genHuyen()}
                <Col span={12}>
                    <Form.Item label={"Tên đơn vị hành chính"} name={'name'} rules={rules.required}>
                        <Input placeholder='Nhập' />
                    </Form.Item>
                </Col>
                <Form.Item hidden name={'id'} />
            </Row>
        </FormModal>
    )
}
