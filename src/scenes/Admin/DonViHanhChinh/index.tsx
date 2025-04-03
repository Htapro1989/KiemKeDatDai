import React from 'react'
import "./index.less";
import AppComponentBase from '../../../components/AppComponentBase';
import { inject, observer } from 'mobx-react';
import Stores from '../../../stores/storeIdentifier';
import DonViHanhChinhStore from '../../../stores/donViHanhChinhStore';
import { FormInstance } from 'antd/lib/form';
import SessionStore from '../../../stores/sessionStore';
import DonViHanhChinhV2 from '../DonViHanhChinhV2';

export interface IQuanLyDVHCProps {
    donViHanhChinhStore: DonViHanhChinhStore;
    sessionStore: SessionStore;
}

export interface IQuanLyDVHCState {
    isLoading: boolean;
    listCapDvhc: any;
    modalVisible: boolean;
    loading: boolean;
}

@inject(Stores.DonViHanhChinhStore, Stores.SessionStore)
@observer
class QuanLyDonViHanhChinh extends AppComponentBase<IQuanLyDVHCProps, IQuanLyDVHCState> {
    formRef = React.createRef<FormInstance>();
    state = {
        isLoading: false,
        listCapDvhc: [],
        modalVisible: false,
        loading: false
    }

    public render() {
        const id = this.props.sessionStore?.currentLogin?.user?.id
        return (
            <div className='capdvhc-page-wrapper'>
                <h1 className='txt-page-header'>Đơn vị hành chính</h1>
                {/* <DonViHanhChinhTable userId={id} /> */}
                <DonViHanhChinhV2 userId={id}/>
            </div>
        )
    }
}

export default QuanLyDonViHanhChinh;
