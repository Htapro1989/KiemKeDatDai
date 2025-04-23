import * as React from 'react';

import { Input, Modal, Tabs, Form } from 'antd';
import { GetAllPermissionsOutput } from '../../../../services/role/dto/getAllPermissionsOutput';
import { L } from '../../../../lib/abpUtility';
import RoleStore from '../../../../stores/roleStore';
import rules from './createOrUpdateRole.validation';
import { FormInstance } from 'antd/lib/form';
import PermissionTree, { buildTreeFromFlatTreeData } from './PermissionTree';

const TabPane = Tabs.TabPane;

export interface ICreateOrUpdateRoleProps {
  roleStore: RoleStore;
  visible: boolean;
  onCancel: () => void;
  modalType: string;
  onOk: () => void;
  permissions: GetAllPermissionsOutput[];
  formRef: React.RefObject<FormInstance>;
  grandPermission: any
}

class CreateOrUpdateRole extends React.Component<ICreateOrUpdateRoleProps> {
  state = {
    confirmDirty: false,
  };

  render() {
    const { permissions } = this.props;

    const options = permissions.map((x: GetAllPermissionsOutput) => {
      return { label: x.displayName, value: x.name };
    });
    console.log("o", options)

    const formItemLayout = {
      labelCol: {
        xs: { span: 6 },
        sm: { span: 6 },
        md: { span: 6 },
        lg: { span: 6 },
        xl: { span: 6 },
        xxl: { span: 6 },
      },
      wrapperCol: {
        xs: { span: 18 },
        sm: { span: 18 },
        md: { span: 18 },
        lg: { span: 18 },
        xl: { span: 18 },
        xxl: { span: 18 },
      },
    };

    const tailFormItemLayout = {
      labelCol: {
        xs: { span: 6 },
        sm: { span: 6 },
        md: { span: 6 },
        lg: { span: 6 },
        xl: { span: 6 },
        xxl: { span: 6 },
      },
      wrapperCol: {
        xs: { span: 18 },
        sm: { span: 18 },
        md: { span: 18 },
        lg: { span: 18 },
        xl: { span: 18 },
        xxl: { span: 18 },
      },
    };

    const permissionMap = permissions.map(permission => {
      return {
        title: permission.displayName,
        key: permission.name,
        children: []
      }
    })
    const treeData = buildTreeFromFlatTreeData(permissionMap)

    return (
      <Modal
        visible={this.props.visible}
        cancelText={L('Cancel')}
        okText={L('OK')}
        onCancel={this.props.onCancel}
        title={'Quản lý vai trò'}
        onOk={this.props.onOk}
        destroyOnClose={true}
      >
        <Form ref={this.props.formRef} layout='vertical'>
          <Tabs defaultActiveKey={'role'} size={'small'} tabBarGutter={64}>
            <TabPane tab={'Vai trò'} key={'role'}>
              <Form.Item label={'Vai trò'} name={'name'} rules={rules.name} {...formItemLayout}>
                <Input />
              </Form.Item>
              <Form.Item label={'Tên hiển thị'} name={'displayName'} rules={rules.displayName} {...formItemLayout}>
                <Input />
              </Form.Item>
              <Form.Item label={'Mô tả'} name={'description'} {...formItemLayout}>
                <Input />
              </Form.Item>
            </TabPane>
            <TabPane tab={"Quyền"} key={'permission'} forceRender={true}>
              <Form.Item {...tailFormItemLayout} name={'grantedPermissions'}
                style={{ width: '100%', }}
              // valuePropName={'value'}
              >
                {/* <Checkbox.Group
                  style={{
                    width: '100%',
                    display: 'grid',
                    gridTemplateColumns: 'repeat(2, 1fr)', // 3 cột đều nhau
                    gap: '8px 8px', // khoảng cách hàng 8px, cột 16px (tuỳ chỉnh)
                  }}
                  options={options} /> */}
                <PermissionTree
                  treeData={treeData}
                  selectedKeys={this.props.grandPermission}
                  onCheck={(listChecked: any) => {
                    this.props.formRef.current?.setFieldsValue({ grantedPermissions: listChecked })
                  }}
                />
              </Form.Item>
            </TabPane>
          </Tabs>
        </Form>
      </Modal>
    );
  }
}

export default CreateOrUpdateRole;
