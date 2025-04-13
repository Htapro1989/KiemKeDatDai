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
import FileManagerComponent from './components/FileManagerComponent';
import { ROUTER_PATH } from '../../components/Router/router.config';
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
    const { donViHanhChinhSelected, donViHanhChinhOfUser } = this.props.donViHanhChinhStore!
    const message_Info = this.props.sessionStore?.currentLogin?.user?.message_Info;
    const isChangePass = this.props.sessionStore?.currentLogin?.user?.isChangePass;

    if (isChangePass === true) {
      this.props.history.replace(ROUTER_PATH.CHANGEPASSWORD + `?firstLogin=true`);
    }

    return (
      <Layout>
        <SiderMenu path={""}
          onCollapse={() => { }}
          history={null} collapsed={false} />

        <div className='home-page-wrapper'>
          {message_Info && (
            <div className='layout-notification'>
              <div className="marquee-container">
                <div className="marquee">
                  📌 {message_Info}
                </div>
              </div>
            </div>
          )}

          <Card style={{ flex: 1, marginTop: 24 }}>
            <Tabs defaultActiveKey="1">
              <Tabs.TabPane tab="Biểu tổng hợp" key="1">
                <BieuDoTab donViHanhChinhSelected={donViHanhChinhSelected} />
              </Tabs.TabPane>
              <Tabs.TabPane tab="Tài liệu" key="2">
                <FileManagerComponent donViHanhChinhSelected={donViHanhChinhSelected} />
              </Tabs.TabPane>
              <Tabs.TabPane tab="Báo cáo" key="3">
                <TableBaoCao
                  capDVHCID={donViHanhChinhOfUser?.capDVHCId}
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