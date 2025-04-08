import { Button, Checkbox, Col, Drawer, Form, Input, Row, Tabs } from 'antd'
import React, { useState } from 'react'
import rules from './createOrUpdateUser.validation'
import { GetRoles } from '../../../../services/user/dto/getRolesOuput';
const TabPane = Tabs.TabPane;

export default function CreateOrUpdateUserDrawer(props: any) {
    const [confirmDirty, setconfirmDirty] = useState(false)
    // const [formState, setFormState] = useState<any>();


    const { roles } = props;

    const options = roles.map((x: GetRoles) => {
        var test = { label: x.displayName, value: x.normalizedName };
        return test;
    });

    const compareToFirstPassword = (rule: any, value: any, callback: any) => {
        const form = props.formRef.current;

        if (value && value !== form!.getFieldValue('password')) {
            return Promise.reject('Two passwords that you enter is inconsistent!');
        }
        return Promise.resolve();
    };

    const validateToNextPassword = (rule: any, value: any, callback: any) => {
        const { validateFields, getFieldValue } = props.formRef.current!;

        setconfirmDirty(true);

        if (value && confirmDirty && getFieldValue('confirm')) {
            validateFields(['confirm']);
        }

        return Promise.resolve();
    };

    const onDelete = () => {
        const form = props.formRef.current;
        const id = form!.getFieldValue('id')
        props.onDelete(id)
        props.onClose()
    }

    // const fetchDvhc = async (dvchId: any) => {
    //     if (!dvchId) return;
    //     const response = await dvhcService.getById(dvchId)
    //     if (response?.code != 1 || response?.returnValue?.length <= 0) return;

    //     const dvhc = response?.returnValue[0];
    //     console.log("DVHC selected ", dvhc)
    //     if (dvhc) {
    //         updateState({
    //             maHuyen: dvhc.maHuyen,
    //             maTinh: dvhc.maTinh,
    //             maVung: dvhc.maVung,
    //             maXa: dvhc.maXa,
    //         })
    //     }
    // }

    // useEffect(() => {
    //     if (props.visible)
    //         fetchDvhc(props.entitySelected?.donViHanhChinhId)

    // }, [props.visible])

    const onCreateUser = () => {
        // let oldDvhcId = props.entitySelected?.donViHanhChinhId;
        // const dvhcId = formState?.idXa || formState?.idHuyen || formState?.idTinh || formState?.idVung || oldDvhcId
        props.onCreate(null)
    }



    return (
        <div>
            <Drawer
                title={
                    props.modalType == 'create' ? 'Sửa thông tin người dùng' : 'Tạo mới người dùng'
                }
                width={720}
                onClose={props.onClose}
                bodyStyle={{ paddingBottom: 80 }}
                visible={props.visible}
                footer={
                    <Row gutter={16} justify='end'>
                        <Col>
                            <Button type='primary' onClick={onCreateUser}>
                                {props.modalType == 'create' ? 'Lưu' : 'Tạo mới'}
                            </Button>
                        </Col>
                        {
                            props.modalType == 'create' && (
                                <Col>
                                    <Button onClick={onDelete}>Xóa</Button>
                                </Col>
                            )
                        }
                    </Row>
                }
            >
                <Form ref={props.formRef} layout='vertical'>
                    <Tabs defaultActiveKey={'userInfo'} size={'small'} tabBarGutter={64}>
                        <TabPane tab={'Thông tin người dùng'} key={'userInfo'}>
                            <Row gutter={8}>
                                <Col span={12}>
                                    <Form.Item label={'Tên'} name={'name'} rules={rules.name}>
                                        <Input />
                                    </Form.Item>
                                </Col>
                                <Col span={12}>
                                    <Form.Item label={'Họ'} name={'surname'} rules={rules.surname}>
                                        <Input />
                                    </Form.Item>
                                </Col>
                            </Row>

                            <Row gutter={8}>
                                <Col span={12}>
                                    <Form.Item label={'Tên đăng nhập'} name={'userName'} rules={rules.userName}>
                                        <Input />
                                    </Form.Item>
                                </Col>
                                <Col span={12}>
                                    <Form.Item label={'Email'} name={'emailAddress'} rules={rules.emailAddress as []}>
                                        <Input />
                                    </Form.Item>
                                </Col>
                            </Row>

                            <Row gutter={8}>
                                <Col span={12}>
                                    {props.modalType === 'edit' ? (
                                        <Form.Item
                                            label={'Mật khẩu'}

                                            name={'password'}
                                            rules={[
                                                {
                                                    required: true,
                                                    message: 'Vui lòng điền đầy đủ thông tin',
                                                },
                                                {
                                                    validator: validateToNextPassword,
                                                },
                                            ]}
                                        >
                                            <Input type="password" />
                                        </Form.Item>
                                    ) : null}
                                </Col>
                                <Col span={12}>
                                    {props.modalType === 'edit' ? (
                                        <Form.Item
                                            label={'Nhập lại mật khẩu'}

                                            name={'confirm'}
                                            rules={[
                                                {
                                                    required: true,
                                                    message: 'Vui lòng điền đầy đủ thông tin',
                                                },
                                                {
                                                    validator: compareToFirstPassword,
                                                },
                                            ]}
                                        >
                                            <Input type="password" />
                                        </Form.Item>
                                    ) : null}
                                </Col>
                            </Row>

                            <Form.Item label={'Trạng thái'} name={'isActive'} valuePropName={'checked'}>
                                <Checkbox>Hoạt động</Checkbox>
                            </Form.Item>
                        </TabPane>
                        <TabPane tab={'Quyền'} key={'rol'} forceRender={true}>
                            <Form.Item name={'roleNames'}>
                                <Checkbox.Group options={options} />
                            </Form.Item>
                        </TabPane>
                    </Tabs>
                </Form>
            </Drawer>
        </div>
    )
}
