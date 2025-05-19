import { Avatar, Dropdown, Menu } from 'antd';
import './index.less';

import * as React from 'react';

// import profilePicture from '../../images/user.png';
import profileDefault from '../../images/avatar-default.png';
import { KeyOutlined, LogoutOutlined } from '@ant-design/icons';
import { Link, useLocation } from 'react-router-dom';

import DownIcon from '../../images/icons/down.svg';
import { inject, observer } from 'mobx-react';
import Stores from '../../stores/storeIdentifier';
import SessionStore from '../../stores/sessionStore';
import { ROUTER_PATH } from '../Router/router.config';
import { isGranted } from '../../lib/abpUtility';

export interface IHeaderProps {
  collapsed?: any;
  toggle?: any;
  sessionStore?: SessionStore;
  hidden?: boolean
}

const userDropdownMenu = (
  <Menu>
    <Menu.Item key="1">
      <Link to={ROUTER_PATH.CHANGEPASSWORD}>
        <KeyOutlined />
        <span> Đổi mật khẩu</span>
      </Link>
    </Menu.Item>
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
        <Avatar style={{ height: 32, width: 32 }} shape="circle" alt={'profile'} src={profileDefault} />
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
      {
        isGranted("Pages.Report.NhapBieu") && (
          <Menu.Item key={ROUTER_PATH.NHAP_DL_KIEM_KE}>
            <Link to={ROUTER_PATH.NHAP_DL_KIEM_KE}>
              Nhập dữ liệu kiểm kê
            </Link>
          </Menu.Item>
        )
      }
      {
        isGranted('Pages.Administration') && (
          <Menu.SubMenu title='Quản lý hệ thống'>
            <Menu.Item key={ROUTER_PATH.CAPDVHC}>
              <Link to={ROUTER_PATH.CAPDVHC}>
                Quản lý Cấp đơn vị hành chính
              </Link>
            </Menu.Item>
            <Menu.Item key={ROUTER_PATH.KYKIEMKE}>
              <Link to={ROUTER_PATH.KYKIEMKE}>
                Quản lý Kỳ thống kê, kiểm kê
              </Link>
            </Menu.Item>
            <Menu.Item key={ROUTER_PATH.DONVIHANHCHINH}>
              <Link to={ROUTER_PATH.DONVIHANHCHINH}>
                Quản lý Đơn vị hành chính
              </Link>
            </Menu.Item>
            <Menu.Item key={ROUTER_PATH.BIEU}>
              <Link to={ROUTER_PATH.BIEU}>
                Quản lý Biểu mẫu
              </Link>
            </Menu.Item>
            <Menu.Item key={ROUTER_PATH.USERS}>
              <Link to={ROUTER_PATH.USERS}>
                Quản lý Người dùng
              </Link>
            </Menu.Item>
            <Menu.Item key={ROUTER_PATH.ROLES}>
              <Link to={ROUTER_PATH.ROLES}>
                Quản lý Vai trò
              </Link>
            </Menu.Item>
            <Menu.Item key={ROUTER_PATH.CAUHINH}>
              <Link to={ROUTER_PATH.CAUHINH}>
                Cấu hình hệ thống
              </Link>
            </Menu.Item>
            <Menu.Item key={ROUTER_PATH.NEWS}>
              <Link to={ROUTER_PATH.NEWS}>
                Cấu hình tin tức
              </Link>
            </Menu.Item>
            <Menu.Item key={ROUTER_PATH.YKIEN}>
              <Link to={ROUTER_PATH.YKIEN}>
                Ý kiến người dùng
              </Link>
            </Menu.Item>
            <Menu.Item key={ROUTER_PATH.THONG_TIN_TONG_HOP}>
              <Link to={ROUTER_PATH.THONG_TIN_TONG_HOP}>
                Thông tin tổng hợp
              </Link>
            </Menu.Item>
          </Menu.SubMenu>
        )
      }
      
      

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
        <div style={{ width: 294 }}>
          <img className='header_logo_layout' src='https://tk24.vbdlis.vn/assets/LandingPage/logo-de43ecf7.png' />
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
