import { Layout, Tabs } from 'antd';
import './index.less';
import React from 'react'
import SiderMenu from '../../components/SiderMenu';
import { inject, observer } from 'mobx-react';
import Stores from '../../stores/storeIdentifier';
import DonViHanhChinhStore from '../../stores/donViHanhChinhStore';
import SessionStore from '../../stores/sessionStore';
import BieuDoTab from './components/BieuDoTab';

export interface IHomePageProps {
  donViHanhChinhStore?: DonViHanhChinhStore;
  sessionStore?: SessionStore;

  path: any;
  collapsed: boolean;
  onCollapse: any;
  history: any;
}

export interface IHomePageState {

}

@inject(Stores.DonViHanhChinhStore, Stores.SessionStore)
@observer
export class HomePage extends React.Component<IHomePageProps, IHomePageState> {

  render(): React.ReactNode {
    const { donViHanhChinhSelected } = this.props.donViHanhChinhStore!
    console.log(donViHanhChinhSelected)
    return (
      <Layout>

        <SiderMenu path={""}
          onCollapse={() => { }}
          history={null} collapsed={false} />

        <div className='home-page-wrapper'>
          <h1 className='txt-page-header'>{donViHanhChinhSelected?.title}</h1>

          <Tabs defaultActiveKey="1">
            <Tabs.TabPane tab="Biểu đồ tổng hợp" key="1">
              <BieuDoTab donViHanhChinhSelected={donViHanhChinhSelected} />
            </Tabs.TabPane>
            <Tabs.TabPane tab="Quản lý file" key="2">
              Content of Tab Pane 2
            </Tabs.TabPane>

          </Tabs>

        </div>

      </Layout>)
  }

}

export default HomePage;