import { Avatar, Dropdown, Menu } from 'antd';
import './index.less';

import * as React from 'react';

import profilePicture from '../../images/user.png';
import { LogoutOutlined } from '@ant-design/icons';
import { Link, useLocation } from 'react-router-dom';

import DownIcon from '../../images/icons/down.svg';
import { inject, observer } from 'mobx-react';
import Stores from '../../stores/storeIdentifier';
import SessionStore from '../../stores/sessionStore';
import { ROUTER_PATH } from '../Router/router.config';

export interface IHeaderProps {
  collapsed?: any;
  toggle?: any;
  sessionStore?: SessionStore;
  hidden?: boolean
}

const userDropdownMenu = (
  <Menu>
    <Menu.Item key="2">
      <Link to="/logout">
        <LogoutOutlined />
        <span> Đăng xuất</span>
      </Link>
    </Menu.Item>
  </Menu>
);
const UserAvatar = (props: any) => {
  return (
    <Dropdown overlay={userDropdownMenu} trigger={['click']}>
      <div className={'user-avatar'}>
        <Avatar style={{ height: 32, width: 32 }} shape="circle" alt={'profile'} src={profilePicture} />
        <div className={'user-info'}>
          <div className='user-name'>{props.userName}</div>
        </div>
        <img src={DownIcon} width={16} />
      </div>
    </Dropdown>

  );
}

const MenuBar = () => {
  const location = useLocation();
  const [selectedKey, setSelectedKey] = React.useState<string>(location.pathname);
  React.useEffect(() => {
    setSelectedKey(location.pathname);
  }, [location.pathname]);

  return (
    <Menu mode="horizontal"
      selectedKeys={[selectedKey]}
      defaultSelectedKeys={[ROUTER_PATH.HOME]}>
      <Menu.Item key={ROUTER_PATH.HOME}>
        <Link to={ROUTER_PATH.HOME}>
          Trang chủ
        </Link>
      </Menu.Item>
      <Menu.Item key="mail1">
        Nhập dữ liệu thống kê
      </Menu.Item>
      <Menu.Item key={ROUTER_PATH.REPORT}>
        <Link to={ROUTER_PATH.REPORT}>
          Báo cáo
        </Link>
      </Menu.Item>
      <Menu.SubMenu title='Quản lý hệ thống'>
        <Menu.Item key="kyKiemKe">
          Quản lý kỳ kiểm kê
        </Menu.Item>
        <Menu.Item key="nguoiDung">
          Quản lý người dùng
        </Menu.Item>
        <Menu.Item key="quyenNguoiDung">
          Quản lý quyền
        </Menu.Item>
      </Menu.SubMenu>
      <Menu.Item key="banDo">
        Bản đồ
      </Menu.Item>
    </Menu>
  )
}

@inject(Stores.SessionStore)
@observer
export class Header extends React.Component<IHeaderProps> {
  render() {
    const { currentLogin } = this.props.sessionStore!
    if (this.props.hidden) return null;

    return (
      <div className={'header-container'}>
        <div style={{ width: 310 }}>
          <img className='header_logo_layout' src='https://tk.gdla.gov.vn/Images/monre-logo2023.png' />
        </div>

        <div className='header__menu_layout'>
          <MenuBar />
          <UserAvatar userName={currentLogin?.user?.name} />
        </div>
      </div>
    );
  }
}

export default Header;
