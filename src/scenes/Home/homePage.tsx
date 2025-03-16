import { Card, Layout, Tabs } from 'antd';
import './index.less';
import React from 'react'
import SiderMenu from '../../components/SiderMenu';
import { inject, observer } from 'mobx-react';
import Stores from '../../stores/storeIdentifier';
import DonViHanhChinhStore from '../../stores/donViHanhChinhStore';
import SessionStore from '../../stores/sessionStore';
import BieuDoTab from './components/BieuDoTab';
import TableBaoCao from '../BaoCaoSoLieu/components/TableBaoCao';
// import TrangThaiDVHCTab from './components/TrangThaiDVHCTab';

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

          <Card style={{ flex: 1 }}>
            <Tabs defaultActiveKey="1">
              <Tabs.TabPane tab="Biểu tổng hợp" key="1">
                <BieuDoTab donViHanhChinhSelected={donViHanhChinhSelected} />
              </Tabs.TabPane>
              <Tabs.TabPane tab="Tài liệu" key="2">
                Content of Tab Pane 2
              </Tabs.TabPane>
              <Tabs.TabPane tab="Báo cáo" key="3">
                <TableBaoCao
                  capDVHCID={donViHanhChinhSelected?.capDVHCId}
                  maDVHC={donViHanhChinhSelected?.ma}
                  year={donViHanhChinhSelected?.year} />
                {/* <TrangThaiDVHCTab dvhcRoot={donViHanhChinhSelected} /> */}
              </Tabs.TabPane>
            </Tabs>
          </Card>

        </div>

      </Layout>)
  }

}

export default HomePage;