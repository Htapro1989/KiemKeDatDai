import './App.css';

import * as React from 'react';

import Router from './components/Router';
import SessionStore from './stores/sessionStore';
import SignalRAspNetCoreHelper from './lib/signalRAspNetCoreHelper';
import Stores from './stores/storeIdentifier';
import { inject } from 'mobx-react';
import DonViHanhChinhStore from './stores/donViHanhChinhStore';

export interface IAppProps {
  sessionStore?: SessionStore;
  donViHanhChinhStore?: DonViHanhChinhStore;
}

@inject(Stores.SessionStore, Stores.DonViHanhChinhStore)
class App extends React.Component<IAppProps> {
  async componentDidMount() {
    const currentLogin = await this.props.sessionStore!.getCurrentLoginInformations();
    if (currentLogin?.user?.id)
      await this.props.donViHanhChinhStore?.fetchDonViHanhChinhList(currentLogin.user.id);

    if (!!this.props.sessionStore!.currentLogin.user && this.props.sessionStore!.currentLogin.application.features['SignalR']) {
      if (this.props.sessionStore!.currentLogin.application.features['SignalR.AspNetCore']) {
        SignalRAspNetCoreHelper.initSignalR();
      }
    }
  }

  public render() {
    return <Router />;
  }
}

export default App;
