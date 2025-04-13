import React from 'react'
import './index.less';
import { Button, Card, Form, Input, notification } from 'antd';
import accountService from '../../services/account/accountService';
import { useHistory } from 'react-router-dom';
import { ROUTER_PATH } from '../../components/Router/router.config';

export default function ChangePasswordPage() {
    const [form] = Form.useForm();
    const history = useHistory();

    const query = new URLSearchParams(window.location.search);
    const isFirstLogin = query.get('firstLogin');

    const onFinish = async () => {
        const values = await form.validateFields();
        if (values) {
            const resp = await accountService.changePassword({
                currentPassword: values.oldPassword,
                newPassword: values.newPassword,
            })
            if (resp.success) {
                notification.success({
                    message: 'Đổi mật khẩu thành công',
                })
                form.resetFields();
                history.push(ROUTER_PATH.HOME);
            } else {
                notification.error({
                    message: 'Đổi mật khẩu thất bại',
                })
            }
        }

    };
    return (
        <div className='changepass-page-wrapper'>
            <h1 className='txt-page-header'>Đổi mật khẩu</h1>
            <Card>

                {isFirstLogin && <div
                    style={{
                        color: '#F4600C', fontSize: 16, marginBottom: 16
                    }}>
                    * Để bảo mật thông tin vui lòng đổi mật khẩu cho lần đầu đăng nhập
                </div>}

                <Form form={form} name="changePassword" onFinish={onFinish} layout="vertical">
                    <Form.Item
                        label="Mật khẩu cũ"
                        name="oldPassword"
                        rules={[{ required: true, message: 'Vui lòng nhập mật khẩu cũ!' }]}
                    >
                        <Input type="password" />
                    </Form.Item>
                    <Form.Item
                        label="Mật khẩu mới"
                        name="newPassword"
                        rules={[{ required: true, message: 'Vui lòng nhập mật khẩu mới!' }]}
                    >
                        <Input type="password" />
                    </Form.Item>
                    <Form.Item
                        label="Xác nhận mật khẩu mới"
                        name="confirmPassword"
                        rules={[{ required: true, message: 'Vui lòng xác nhận mật khẩu mới!' }]}
                    >
                        <Input type="password" />
                    </Form.Item>
                    <Form.Item>
                        <Button type='primary' onClick={onFinish}>Đổi mật khẩu</Button>
                    </Form.Item>
                </Form>
            </Card>
        </div>
    )
}
