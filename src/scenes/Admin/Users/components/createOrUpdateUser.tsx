import * as React from 'react';

import { Checkbox, Input, Modal, Tabs, Form, Row, Col } from 'antd';
import { GetRoles } from '../../../../services/user/dto/getRolesOuput';
import { L } from '../../../../lib/abpUtility';
import rules from './createOrUpdateUser.validation';
import { FormInstance } from 'antd/lib/form';

const TabPane = Tabs.TabPane;

export interface ICreateOrUpdateUserProps {
  visible: boolean;
  onCancel: () => void;
  modalType: string;
  onCreate: () => void;
  roles: GetRoles[];
  formRef: React.RefObject<FormInstance>;
}

class CreateOrUpdateUser extends React.Component<ICreateOrUpdateUserProps> {
  state = {
    confirmDirty: false,
  };

  compareToFirstPassword = (rule: any, value: any, callback: any) => {
    const form = this.props.formRef.current;

    if (value && value !== form!.getFieldValue('password')) {
      return Promise.reject('Two passwords that you enter is inconsistent!');
    }
    return Promise.resolve();
  };

  validateToNextPassword = (rule: any, value: any, callback: any) => {
    const { validateFields, getFieldValue } = this.props.formRef.current!;

    this.setState({
      confirmDirty: true,
    });

    if (value && this.state.confirmDirty && getFieldValue('confirm')) {
      validateFields(['confirm']);
    }

    return Promise.resolve();
  };

  render() {
    const { roles } = this.props;

    const { visible, onCancel, onCreate } = this.props;

    const options = roles.map((x: GetRoles) => {
      var test = { label: x.displayName, value: x.normalizedName };
      return test;
    });

    return (
      <Modal visible={visible} cancelText={L('Cancel')} okText={L('OK')}
        onCancel={onCancel} onOk={onCreate} title={'Người dùng'} destroyOnClose={true}>
        <Form ref={this.props.formRef} layout='vertical'>
          <Tabs defaultActiveKey={'userInfo'} size={'small'} tabBarGutter={64}>
            <TabPane tab={'User'} key={'userInfo'}>
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
                  {this.props.modalType === 'edit' ? (
                    <Form.Item
                      label={'Mật khẩu'}

                      name={'password'}
                      rules={[
                        {
                          required: true,
                          message: 'Vui lòng điền đầy đủ thông tin',
                        },
                        {
                          validator: this.validateToNextPassword,
                        },
                      ]}
                    >
                      <Input type="password" />
                    </Form.Item>
                  ) : null}
                </Col>
                <Col span={12}>
                  {this.props.modalType === 'edit' ? (
                    <Form.Item
                      label={'Nhập lại mật khẩu'}

                      name={'confirm'}
                      rules={[
                        {
                          required: true,
                          message: 'Vui lòng điền đầy đủ thông tin',
                        },
                        {
                          validator: this.compareToFirstPassword,
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
      </Modal>
    );
  }
}

export default CreateOrUpdateUser;
