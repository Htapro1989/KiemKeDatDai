import './index.less';

import * as React from 'react';

import { Button, Input, Layout, Modal, Row } from 'antd';
// import { UserOutlined, LockOutlined } from '@ant-design/icons';
import { inject, observer } from 'mobx-react';

import AccountStore from '../../stores/accountStore';
import AuthenticationStore from '../../stores/authenticationStore';
import Form, { FormInstance } from 'antd/lib/form';
import { L } from '../../lib/abpUtility';
import { Redirect } from 'react-router-dom';
import SessionStore from '../../stores/sessionStore';
import Stores from '../../stores/storeIdentifier';
import TenantAvailabilityState from '../../services/account/dto/tenantAvailabilityState';
import rules from './index.validation';
import GdlaHomePage from './tk.gdla.home';


declare var abp: any;

export interface ILoginProps {
  authenticationStore?: AuthenticationStore;
  sessionStore?: SessionStore;
  accountStore?: AccountStore;
  history: any;
  location: any;
}
export interface ILoginState {
  modalVisible: boolean;
  loading: boolean
}

@inject(Stores.AuthenticationStore, Stores.SessionStore, Stores.AccountStore)
@observer
class Login extends React.Component<ILoginProps, ILoginState> {
  formRef = React.createRef<FormInstance>();

  state = {
    modalVisible: false,
    loading: false
  };

  changeTenant = async () => {
    let tenancyName = this.formRef.current?.getFieldValue('tenancyName');
    const { loginModel } = this.props.authenticationStore!;

    if (!tenancyName) {
      abp.multiTenancy.setTenantIdCookie(undefined);
      window.location.href = '/';
      return;
    } else {
      await this.props.accountStore!.isTenantAvailable(tenancyName);
      const { tenant } = this.props.accountStore!;
      switch (tenant.state) {
        case TenantAvailabilityState.Available:
          abp.multiTenancy.setTenantIdCookie(tenant.tenantId);
          loginModel.tenancyName = tenancyName;
          loginModel.toggleShowModal();
          window.location.href = '/';
          return;
        case TenantAvailabilityState.InActive:
          Modal.error({ title: L('Error'), content: L('TenantIsNotActive') });
          break;
        case TenantAvailabilityState.NotFound:
          Modal.error({ title: L('Error'), content: L('ThereIsNoTenantDefinedWithName{0}', tenancyName) });
          break;
      }
    }
  };

  handleSubmit = async (values: any) => {
    const { loginModel } = this.props.authenticationStore!;
    this.setState({ loading: true })
    try {
      await this.props.authenticationStore!.login(values);
      sessionStorage.setItem('rememberMe', loginModel.rememberMe ? '1' : '0');
      const { state } = this.props.location;
      window.location = state ? state.from.pathname : '/';
    } finally {
      this.setState({ loading: false })
    }
  };


  loginForm = () => {
    return (<div className='login-form'>
      <h3 className='lable-login-form'>Đăng nhập</h3>
      <p className='sub-lable-login-form'>Đăng nhập để sử dụng hệ thống</p>
      <Form
        labelCol={{ span: 24 }}
        wrapperCol={{ span: 24 }}
        onFinish={this.handleSubmit} ref={this.formRef} layout="vertical">
        <Form.Item
          label={"Tên đăng nhập"}
          name={'userNameOrEmailAddress'}
          labelAlign='right'
          rules={rules.userNameOrEmailAddress}>
          <Input
            placeholder={"Nhập tên đăng nhập"}
            size="large" />
        </Form.Item>

        <Form.Item
          style={{ marginTop: 20 }}
          label={"Mật khẩu"}
          name={'password'}
          rules={rules.password}>
          <Input placeholder={"Nhập mật khẩu"} type="password" size="large" />
        </Form.Item>
        <Button
          loading={this.state.loading}
          style={{ marginTop: 40, width: '100%' }}
          htmlType={'submit'} type='primary'>
          {L('LogIn')}
        </Button>

      </Form>
    </div>)
  }

  onShowPopupLogin = () => {
    this.setState({ modalVisible: true });
  }
  onHidePopupLogin = () => {
    this.setState({ modalVisible: false });
  }


  public render() {
    let { from } = this.props.location.state || { from: { pathname: '/' } };
    if (this.props.authenticationStore!.isAuthenticated) return <Redirect to={from} />;
    const a = 10;
    // const { loginModel } = this.props.authenticationStore!;
    if (a > 3) {
      return <Layout>
        <GdlaHomePage onLogin={this.onShowPopupLogin} />
        <Modal
          visible={this.state.modalVisible}
          onCancel={this.onHidePopupLogin}
          onOk={this.changeTenant}
          cancelText={L('Cancel')}
          footer={null}
        >
          {this.loginForm()}
        </Modal>
      </Layout>
    }
    return (
      <Layout>
        <Row justify='center' align='middle' style={{ height: '100vh' }}>
          {this.loginForm()}
        </Row>
      </Layout>
    )
    // return (
    //   <Form className="" onFinish={this.handleSubmit} ref={this.formRef}>
    //       <Row style={{ marginTop: 100 }}>
    //         <Col span={8} offset={8}>
    //           <Card>
    //             <Row>
    //               {!!this.props.sessionStore!.currentLogin.tenant ? (
    //                 <Col span={24} offset={0} style={{ textAlign: 'center' }}>
    //                   <Button type="link" onClick={loginModel.toggleShowModal}>
    //                     {L('CurrentTenant')} : {this.props.sessionStore!.currentLogin.tenant.tenancyName}
    //                   </Button>
    //                 </Col>
    //               ) : (
    //                 <Col span={24} offset={0} style={{ textAlign: 'center' }}>
    //                   <Button type="link" onClick={loginModel.toggleShowModal}>
    //                     {L('NotSelected')}
    //                   </Button>
    //                 </Col>
    //               )}
    //             </Row>
    //           </Card>
    //         </Col>
    //       </Row>

    //       <Row>
    //         <Modal
    //           visible={loginModel.showModal}
    //           onCancel={loginModel.toggleShowModal}
    //           onOk={this.changeTenant}
    //           title={L('ChangeTenant')}
    //           okText={L('OK')}
    //           cancelText={L('Cancel')}
    //         >
    //           <Row>
    //             <Col span={8} offset={8}>
    //               <h3>{L('TenancyName')}</h3>
    //             </Col>
    //             <Col>
    //               <FormItem name={'tenancyName'}>
    //                 <Input placeholder={L('TenancyName')} prefix={<UserOutlined style={{ color: 'rgba(0,0,0,.25)' }} />} size="large" />
    //               </FormItem>
    //               {!this.formRef.current?.getFieldValue('tenancyName') ? <div>{L('LeaveEmptyToSwitchToHost')}</div> : ''}
    //             </Col>
    //           </Row>
    //         </Modal>
    //       </Row>
    //       <Row style={{ marginTop: 10 }}>
    //         <Col span={8} offset={8}>
    //           <Card>
    //             <div style={{ textAlign: 'center' }}>
    //               <h3>{L('WellcomeMessage')}</h3>
    //             </div>
    //             <FormItem name={'userNameOrEmailAddress'} rules={rules.userNameOrEmailAddress}>
    //               <Input placeholder={L('UserNameOrEmail')} prefix={<UserOutlined style={{ color: 'rgba(0,0,0,.25)' }} />} size="large" />
    //             </FormItem>

    // <FormItem name={'password'} rules={rules.password}>
    //   <Input placeholder={L('Password')} prefix={<LockOutlined style={{ color: 'rgba(0,0,0,.25)' }} />} type="password" size="large" />
    // </FormItem>
    //             <Row style={{ margin: '0px 0px 10px 15px ' }}>
    //               <Col span={12} offset={0}>
    //                 <Checkbox checked={loginModel.rememberMe} onChange={loginModel.toggleRememberMe} style={{ paddingRight: 8 }} />
    //                 {L('RememberMe')}
    //                 <br />
    //                 <a>{L('ForgotPassword')}</a>
    //               </Col>

    //               <Col span={8} offset={4}>
    // <Button  htmlType={'submit'} type='primary'>
    //   {L('LogIn')}
    // </Button>
    //               </Col>
    //             </Row>
    //           </Card>
    //         </Col>
    //       </Row>
    //   </Form>
    // );
  }
}

export default Login;
