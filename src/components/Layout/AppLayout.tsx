import './AppLayout.less';

import * as React from 'react';

import DocumentTitle from 'react-document-title';
import Header from '../../components/Header';
import { Layout } from 'antd';
import ProtectedRoute from '../../components/Router/ProtectedRoute';
import { appRouters, ROUTER_PATH } from '../Router/router.config';
import utils from '../../utils/utils';
import NotFoundRoute from '../Router/NotFoundRoute';
import { Redirect, Route, Switch } from 'react-router-dom';

const { Content } = Layout;

class AppLayout extends React.Component<any> {
  state = {
    collapsed: false,
  };

  toggle = () => {
    this.setState({
      collapsed: !this.state.collapsed,
    });
  };

  onCollapse = (collapsed: any) => {
    this.setState({ collapsed });
  };

  render() {
    const {
      location: { pathname },
    } = this.props;
    const layout = (
      <Layout style={{ minHeight: '100vh', }}>
        <Layout>
          <Header collapsed={this.state.collapsed} toggle={this.toggle} />
          <Content className='app-content-layout'>
            <Switch>
              {pathname === '/' && <Redirect from="/" to={ROUTER_PATH.HOME} />}
              {appRouters
                .filter((item: any) => !item.isLayout)
                .map((route: any, index: any) => (
                  <Route
                    exact
                    key={index}
                    path={route.path}
                    render={(props) => <ProtectedRoute component={route.component} permission={route.permission} />}
                  />
                ))}
              {pathname !== '/' && <NotFoundRoute />}
            </Switch>
          </Content>
        </Layout>
      </Layout>
    );

    return <DocumentTitle title={utils.getPageTitle(pathname)}>{layout}</DocumentTitle>;
  }
}

export default AppLayout;
