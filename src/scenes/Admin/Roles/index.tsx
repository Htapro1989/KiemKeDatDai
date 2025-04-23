import * as React from 'react';
import './index.less'

import { Button, Card, Col, Dropdown, Input, Menu, Modal, Row, Table } from 'antd';
import { inject, observer } from 'mobx-react';

import AppComponentBase from '../../../components/AppComponentBase';
import CreateOrUpdateRole from './components/createOrUpdateRole';
import { EntityDto } from '../../../services/dto/entityDto';
import RoleStore from '../../../stores/roleStore';
import Stores from '../../../stores/storeIdentifier';
import { PlusOutlined, SettingOutlined } from '@ant-design/icons';
import { FormInstance } from 'antd/lib/form';

export interface IRoleProps {
  roleStore: RoleStore;
}

export interface IRoleState {
  modalVisible: boolean;
  maxResultCount: number;
  skipCount: number;
  roleId: number;
  filter: string;
}

const confirm = Modal.confirm;
const Search = Input.Search;

@inject(Stores.RoleStore)
@observer
class Role extends AppComponentBase<IRoleProps, IRoleState> {
  formRef = React.createRef<FormInstance>();

  state = {
    modalVisible: false,
    maxResultCount: 10,
    skipCount: 0,
    roleId: 0,
    filter: ''
  };

  async componentDidMount() {
    await this.getAll();
  }

  async getAll() {
    await this.props.roleStore.getAll({ maxResultCount: this.state.maxResultCount, skipCount: this.state.skipCount, keyword: this.state.filter });
  }

  handleTableChange = (pagination: any) => {
    this.setState({ skipCount: (pagination.current - 1) * this.state.maxResultCount! }, async () => await this.getAll());
  };

  Modal = () => {
    this.setState({
      modalVisible: !this.state.modalVisible,
    });
  };

  async createOrUpdateModalOpen(entityDto: EntityDto) {
    if (entityDto.id === 0) {
      this.props.roleStore.createRole();
      await this.props.roleStore.getAllPermissions();
    } else {
      await this.props.roleStore.getRoleForEdit(entityDto);
      await this.props.roleStore.getAllPermissions();
    }

    this.setState({ roleId: entityDto.id });
    this.Modal();

    setTimeout(() => {
      this.formRef.current?.setFieldsValue({
        ...this.props.roleStore.roleEdit.role,
        grantedPermissions: this.props.roleStore.roleEdit.grantedPermissionNames,
      });
    }, 100);
  }

  delete(input: EntityDto) {
    const self = this;
    confirm({
      title: 'Bạn muốn xóa quyền này?',
      onOk() {
        self.props.roleStore.delete(input);
      },
      onCancel() { },
    });
  }

  handleCreate = () => {
    const form = this.formRef.current;
    form!.validateFields().then(async (values: any) => {
      if (this.state.roleId === 0) {
        await this.props.roleStore.create(values);
      } else {
        await this.props.roleStore.update({ id: this.state.roleId, ...values });
      }

      await this.getAll();
      this.setState({ modalVisible: false });
      form!.resetFields();
    });
  };

  handleSearch = (value: string) => {
    this.setState({ filter: value }, async () => await this.getAll());
  };

  public render() {
    const { allPermissions, roles, roleEdit } = this.props.roleStore;
    const columns = [
      { title: 'Vai trò', dataIndex: 'name', key: 'name', width: 150, render: (text: string) => <div>{text}</div> },
      { title: 'Tên hiển thị', dataIndex: 'displayName', key: 'displayName', width: 150, render: (text: string) => <div>{text}</div> },
      {
        title: "",
        width: 150,
        render: (text: string, item: any) => (
          <div>
            <Dropdown
              trigger={['click']}
              overlay={
                <Menu>
                  <Menu.Item onClick={() => this.createOrUpdateModalOpen({ id: item.id })}>Chỉnh sửa</Menu.Item>
                  <Menu.Item onClick={() => this.delete({ id: item.id })}>Xóa</Menu.Item>
                </Menu>
              }
              placement="bottomLeft"
            >
              <Button type="primary" icon={<SettingOutlined />}>
              </Button>
            </Dropdown>
          </div>
        ),
      },
    ];

    return (
      <div className='roles-page-wrapper'>
        <h1 className='txt-page-header'>Quản lý vai trò</h1>
        <Card title={
          <div className='table-header-layout'>
            <div style={{ flex: 1 }}>
              <span style={{ marginRight: 24 }}> Tìm kiếm: </span>
              <Search placeholder={'Tìm kiếm vai trò...'} onSearch={this.handleSearch} />
            </div>
            <div className='table-header-layout-right'>
              <Button type="primary" icon={<PlusOutlined />} onClick={() => this.createOrUpdateModalOpen({ id: 0 })}>
                Tạo mới
              </Button>
            </div>
          </div>
        }>
          <Row style={{ marginTop: 20 }}>
            <Col
              xs={{ span: 24, offset: 0 }}
              sm={{ span: 24, offset: 0 }}
              md={{ span: 24, offset: 0 }}
              lg={{ span: 24, offset: 0 }}
              xl={{ span: 24, offset: 0 }}
              xxl={{ span: 24, offset: 0 }}
            >
              <Table
                rowKey="id"
                bordered={true}
                pagination={{ pageSize: this.state.maxResultCount, total: roles === undefined ? 0 : roles.totalCount, defaultCurrent: 1 }}
                columns={columns}
                loading={roles === undefined ? true : false}
                dataSource={roles === undefined ? [] : roles.items}
                onChange={this.handleTableChange}
              />
            </Col>
          </Row>

          <CreateOrUpdateRole
            visible={this.state.modalVisible}
            onCancel={() =>
              this.setState({
                modalVisible: false,
              })
            }
            modalType={this.state.roleId === 0 ? 'edit' : 'create'}
            onOk={this.handleCreate}
            permissions={allPermissions}
            roleStore={this.props.roleStore}
            formRef={this.formRef}
            grandPermission = {roleEdit?.grantedPermissionNames}
          />
        </Card>
      </div>
    );
  }
}

export default Role;
