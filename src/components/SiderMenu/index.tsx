import './index.less';

import * as React from 'react';

import { Layout, Select, Skeleton, Tree } from 'antd';
import { inject, observer } from 'mobx-react';
import Stores from '../../stores/storeIdentifier';
import DonViHanhChinhStore from '../../stores/donViHanhChinhStore';
import SessionStore from '../../stores/sessionStore';

const { Sider } = Layout;

export interface ISiderMenuProps {
  path?: any;
  collapsed?: boolean;
  onCollapse: any;
  history: any;
}

export interface ISideMenuProps {
  donViHanhChinhStore?: DonViHanhChinhStore;
  sessionStore?: SessionStore;

  path: any;
  collapsed: boolean;
  onCollapse: any;
  history: any;
}

export interface ISideMenuState {

}

@inject(Stores.DonViHanhChinhStore, Stores.SessionStore)
@observer
export class SiderMenu extends React.Component<ISideMenuProps, ISideMenuState> {


  public handleChange = (value: string) => {
    console.log(`selected ${value}`);
  };

  onLoadData = async ({ key, children }: any) => {
    if (children) {
      return;
    }
    await this.props.donViHanhChinhStore?.fetchDonViHanhChinhListByParentKey(key);
  }


  render(): React.ReactNode {
    const { donViHanhChinhList, isFetchingDonViHanhChinh, dmKyKiemKe, dmKyKiemKeSelected, donViHanhChinhSelected } = this.props.donViHanhChinhStore!
    return (
      <Sider trigger={null} className={'sidebar'} width={326} collapsible collapsed={false}
        theme='light'>
        <div
          className='sidebar-content-layout'>
          <Select
            value={dmKyKiemKeSelected}
            onChange={this.handleChange}
            options={dmKyKiemKe}
          />
          <div className='sidebar-content-layout-divider'></div>
          {isFetchingDonViHanhChinh ? <Skeleton active /> : <div className='tree-menu-layout'>
            <Tree
              treeData={donViHanhChinhList as any}
              loadData={this.onLoadData}
              selectedKeys={[donViHanhChinhSelected?.key]}
              onSelect={(selectedKeys, info) => {
                console.log("Menu select ", info)
                this.props.donViHanhChinhStore?.selectDonViHanhChinh(info.node);
              }
              }
            />
          </div>}
        </div>
      </Sider>
    )
  }
}

export default SiderMenu;
