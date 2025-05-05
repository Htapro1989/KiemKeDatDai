import * as React from 'react';

import { Button, Card, Empty, Input, Modal, Table, Tag } from 'antd';
import { inject, observer } from 'mobx-react';
import AppComponentBase from '../../../components/AppComponentBase';
import { EntityDto } from '../../../services/dto/entityDto';
import Stores from '../../../stores/storeIdentifier';
import UserStore from '../../../stores/userStore';
import { FormInstance } from 'antd/lib/form';
import './index.less'
import CreateOrUpdateUserDrawer from './components/createOrUpdateUserDrawer';
import DvhcTree from './components/DvhcTree';
import SessionStore from '../../../stores/sessionStore';
import { PlusOutlined } from '@ant-design/icons';

export interface IUserProps {
  userStore: UserStore;
  sessionStore: SessionStore;
}

interface MaDvhcSelected {
  id?: string;
  ma?: string
}
export interface IUserState {
  modalVisible: boolean;
  maxResultCount: number;
  skipCount: number;
  userId: number;
  filter: string;
  entitySelected?: any;
  maDvhcSelected: MaDvhcSelected
}

const confirm = Modal.confirm;
const Search = Input.Search;

@inject(Stores.UserStore, Stores.SessionStore)
@observer
class User extends AppComponentBase<IUserProps, IUserState> {
  formRef = React.createRef<FormInstance>();

  state = {
    modalVisible: false,
    maxResultCount: 10,
    skipCount: 0,
    userId: 0,
    filter: '',
    entitySelected: null,
    maDvhcSelected: {
      ma: "0",
      id: "0"
    }
  };

  async componentDidMount() {
    await this.getAll();
  }

  async getAll(dvhcIdSelected?: any) {
    const ma = dvhcIdSelected ? dvhcIdSelected : this.state.maDvhcSelected?.ma
    if (!ma) { return };
    await this.props.userStore.getAll({
      maxResultCount: this.state.maxResultCount,
      skipCount: this.state.skipCount,
      keyword: this.state.filter,
      ma
    });
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
      await this.props.userStore.createUser();
      await this.props.userStore.getRoles();
    } else {
      await this.props.userStore.get(entityDto);
      await this.props.userStore.getRoles();
    }

    this.setState({
      userId: entityDto.id,
      entitySelected: { ...this.props.userStore.editUser }

    });
    this.Modal();

    setTimeout(() => {
      this.formRef.current?.setFieldsValue({ ...this.props.userStore.editUser });
    }, 100);
  }

  delete(input: EntityDto) {
    const self = this;
    confirm({
      title: 'Bạn muốn xóa người dùng này',
      onOk() {
        self.props.userStore.delete(input);
      },
      onCancel() {
        console.log('Cancel');
      },
    });
  }

  handleCreate = (dvhcId: any) => {
    const form = this.formRef.current;
    const userSeleted: any = this.state.entitySelected;
    form!.validateFields().then(async (values: any) => {
      if (this.state.userId === 0) {
        await this.props.userStore.create({
          ...values,
          donViHanhChinhId: this.state.maDvhcSelected.id,
          donViHanhChinhCode: this.state.maDvhcSelected.ma,
        });
      } else {
        await this.props.userStore.update({
          ...values,
          donViHanhChinhId: userSeleted?.donViHanhChinhId,
          donViHanhChinhCode:userSeleted?.donViHanhChinhCode,
          id: this.state.userId
        });
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
    const { users } = this.props.userStore;
    const userId = this.props.sessionStore?.currentLogin?.user?.id
    const columns = [
      { title: 'Tên đăng nhập', dataIndex: 'userName', key: 'userName', width: 150, render: (text: string) => <div>{text}</div> },
      { title: 'Tên hiển thị', dataIndex: 'name', key: 'name', width: 150, render: (text: string) => <div>{text}</div> },
      { title: 'Email', dataIndex: 'emailAddress', key: 'emailAddress', width: 150, render: (text: string) => <div>{text}</div> },
      {
        title: 'Trạng thái',
        dataIndex: 'isActive',
        key: 'isActive',
        width: 150,
        render: (text: boolean) => (text === true ? <Tag color="#2db7f5">Hoạt động</Tag> : <Tag color="red">Không hoạt động</Tag>),
      },
    ];

    return (
      <div className='user-page-wrapper'>
        <h1 className='txt-page-header'>Quản lý người dùng</h1>
        <Card>
          <div className='user-page-content'>
            <div className='user-page-content-left'>
              <DvhcTree
                onSelect={(dvhc: any) => {
                  this.getAll(dvhc.ma || -1)
                  this.setState({
                    maDvhcSelected: {
                      id: dvhc.id,
                      ma: dvhc.ma
                    }
                  })
                }}
                userId={userId} />
            </div>
            <div className='user-page-content-right'>
              <Table
                style={{ flex: 1 }}
                rowKey={(record) => record.id.toString()}
                bordered={true}
                columns={columns}
                pagination={{ pageSize: 10, total: users === undefined ? 0 : users.totalCount, defaultCurrent: 1 }}
                loading={users === undefined ? true : false}
                dataSource={users === undefined ? [] : users.items}
                onChange={this.handleTableChange}
                locale={{
                  emptyText: (
                    <Empty description="Không có dữ liệu"> </Empty>
                  ),
                }}
                title={
                  () => {
                    return (
                      <div className='table-header-layout'>
                        <div className='table-search-header'>
                          <Search placeholder={'Tìm kiếm người dùng...'} onSearch={this.handleSearch} />
                        </div>
                        <div className='table-header-layout-right'>
                          <Button
                            type="primary"
                            icon={<PlusOutlined />} onClick={() => this.createOrUpdateModalOpen({ id: 0 })}>
                          </Button>
                        </div>
                      </div>
                    )
                  }
                }
                onRow={(record) => ({
                  onClick: () => {
                    this.createOrUpdateModalOpen({ id: record.id })
                  },
                })}
              />
            </div>
          </div>
        </Card>
        <CreateOrUpdateUserDrawer
          visible={this.state.modalVisible}
          onClose={() => { this.setState({ modalVisible: false }) }}
          formRef={this.formRef}
          onCancel={() => {
            this.setState({
              modalVisible: false,
            });
            this.formRef.current?.resetFields();
          }}
          modalType={this.state.userId === 0 ? 'edit' : 'create'}
          onCreate={this.handleCreate}
          roles={this.props.userStore.roles}
          onDelete={(id: any) => this.delete({ id })}
          entitySelected={this.state.entitySelected}
        />
      </div>

    );
  }
}

export default User;
