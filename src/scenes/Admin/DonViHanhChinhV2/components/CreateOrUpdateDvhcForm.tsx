import { Button, Col, Empty, Form, Input, Modal, notification, Row } from 'antd'
import React, { useEffect, useState } from 'react'
import SelectItem from '../../../../components/Select/SelectItem'
import dvhcService from '../../../../services/dvhc/dvhcService'
import { FormInstance } from 'antd/lib/form';
import '../index.less'
import rules from '../../../../constants/index.validation';
import { CAP_DVHC_ENUM, getDvhcNameByLevel } from '../../../../models/enum';
import { handleCommontResponse } from '../../../../services/common/handleResponse';
const confirm = Modal.confirm;

const UPDATE = "UPDATE";
const CREATE = "CREATE";

export default function CreateOrUpdateDvhcForm(props: any) {
    const { entity } = props

    // const [formStatus, setFormStatus] = useState(UPDATE)
    const [formState, setFormState] = useState<any>({
        action: UPDATE,
        createCapDVHC: ''
    })


    useEffect(() => {
        if (entity) {
            formRef.current?.setFieldsValue(entity);
            setFormState({
                action: UPDATE,
                createCapDVHC: ''
            })
        }
    }, [entity])

    const formRef = React.createRef<FormInstance>();

    const onCreateOrUpdateDVHC = async () => {
        if (formState.action == CREATE) {
            const response = await dvhcService.createOrUpdateDVHC({
                ...formRef.current?.getFieldsValue(),
                year: props.year,
                parent_id: entity?.id,
                parent_Code: entity?.ma,
            });

            if (response.code == 1) {
                notification.success({ message: response.message })
                setFormState({
                    action: UPDATE,
                    createCapDVHC: ''
                })
                props.onCreateDvhc()

            } else {
                notification.error({
                    message: response?.message ? response?.message : "Thất bại. Vui lòng thử lại"
                })
            }
        }
        if (formState.action == UPDATE) {
            const response = await dvhcService.createOrUpdateDVHC({
                ...formRef.current?.getFieldsValue(),
                parent_Code: entity?.parent_Code,
                parent_id: entity?.parent_id,
            });
            props.onUpdateDvhc({
                ...formRef.current?.getFieldsValue(),
            })
            handleCommontResponse(response)
        }
    }

    const onCreate = () => {
        formRef.current?.resetFields(["ma", "name", "id"]);

        if (props.donViHanhChinhList?.length == 0) {

            formRef.current?.setFieldsValue({
                capDVHCId: 0
            })
            setFormState({
                action: CREATE,
                createCapDVHC: getDvhcNameByLevel(0)
            })

        } else {
            formRef.current?.setFieldsValue({
                capDVHCId: entity?.capDVHCId + 1
            })
            setFormState({
                action: CREATE,
                createCapDVHC: getDvhcNameByLevel(entity?.capDVHCId + 1)
            })
        }
    }

    const genCreateBtn = () => {
        if (formState.action == CREATE) {
            return <Button type='text' size='small' onClick={onCancelCreate}>Hủy bỏ</Button>
        }
        if (formState.action == UPDATE) {
            return <Button type='primary'
                disabled={entity?.capDVHCId == CAP_DVHC_ENUM.XA}
                ghost size='small'
                onClick={onCreate}>Tạo mới</Button>
        }
        return null

    }

    const onDelete = async (id: any) => {
        const response = await dvhcService.deleteDVHC(id);
        handleCommontResponse(response, () => {
            props.onDeleteDvhc(id)
        });
    }

    const onConfirmDelete = () => {

        confirm({
            title: 'Bạn muốn xóa đơn vị hành chính này',
            onOk() {
                onDelete(entity?.id)
            },
            onCancel() {
                console.log('Cancel');
            },
        });
    }

    const onCancelCreate = () => {
        setFormState({
            action: UPDATE,
        })
        formRef.current?.setFieldsValue(entity);
    }

    if (!entity && props.donViHanhChinhList?.length != 0) {
        return <div className='right-component-empty'>
            <Empty image={Empty.PRESENTED_IMAGE_SIMPLE} description="Vui lòng chọn đơn vị hành chính" />
        </div>
    }


    return (
        <div className='right-component-wrapper'>
            <Row justify='space-between'>
                <h1 style={{ marginBottom: 18 }}>
                    {formState.action == UPDATE ? 'Thông tin đơn vị hành chính' : `Tạo mới đơn vị hành chính cấp ${formState.createCapDVHC}`}
                </h1>
                {genCreateBtn()}
            </Row>

            <Form layout='vertical' ref={formRef} style={{ minHeight: 300 }} >
                <Row gutter={12}>
                    <Col span={12}>
                        <Form.Item label="Mã đơn vị hành chính" name="ma" rules={[{ required: true }]}>
                            <Input />
                        </Form.Item>
                    </Col>
                    <SelectItem
                        formItemProps={{ label: "Cấp đơn vị hành chính", name: "capDVHCId", rules: rules.required }}
                        remoteSource={dvhcService.getDropDownCapDVHC.bind(null)}
                        selectProps={{ disabled: true }}
                        refresing={true}
                        className='mb-0'
                        span={12}
                    />
                </Row>
                {
                    entity?.capDVHCId > CAP_DVHC_ENUM.VUNG && (
                        <Form.Item label="Tên Vùng" name="tenVung" rules={[{ required: true }]}>
                            <Input disabled={true} />
                        </Form.Item>
                    )
                }
                {
                    entity?.capDVHCId > CAP_DVHC_ENUM.TINH && (
                        <Form.Item label="Tên Tỉnh/Thành phố" name="tenTinh" rules={[{ required: true }]}>
                            <Input disabled={true} />
                        </Form.Item>
                    )
                }
                {
                    entity?.capDVHCId > CAP_DVHC_ENUM.HUYEN && (
                        <Form.Item label="Tên Quận/Huyện" name="tenHuyen" rules={[{ required: true }]}>
                            <Input disabled={true} />
                        </Form.Item>
                    )
                }

                <Form.Item label={formState.action == UPDATE ? "Tên đơn vị hành chính" : `Tên đơn vị hành chính cấp ${formState.createCapDVHC}`} name="name" rules={[{ required: true }]}>
                    <Input />
                </Form.Item>
                <Form.Item hidden name={'id'} />
            </Form>
            <div className='button-wrapper'>
                <Button type='primary' onClick={onCreateOrUpdateDVHC}>Lưu</Button>
                {
                    formState.action === UPDATE && (<Button type='ghost' style={{ marginLeft: 8 }} onClick={onConfirmDelete}>Xoá</Button>)
                }
            </div>
        </div>
    )
}
