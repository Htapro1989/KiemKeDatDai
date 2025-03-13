import React from 'react'
import './index.less';
import SessionStore from '../../stores/sessionStore';
import DonViHanhChinhStore from '../../stores/donViHanhChinhStore';
import Stores from '../../stores/storeIdentifier';
import { inject, observer } from 'mobx-react';
import { Card } from 'antd';
import TableBaoCao from './components/TableBaoCao';
import SelectKyKiemKe from './components/SelectKyKiemKe';


export interface IBaoCaoSoLieuProps {
    sessionStore?: SessionStore;
    donViHanhChinhStore?: DonViHanhChinhStore;
}
export interface IBaoCaoSoLieuState {
    dvhcSelected: any
}




@inject(Stores.SessionStore, Stores.DonViHanhChinhStore)
@observer
export class BaoCaoSoLieu extends React.Component<IBaoCaoSoLieuProps, IBaoCaoSoLieuState> {

    state = {
        dvhcSelected: {
            ma: null,
            year: null,
            capDVHCId: null
        }
    }
    componentDidMount(): void {
    }

    public onSelectKyKiemKe = (dvhc: any) => {
        this.setState({ dvhcSelected: dvhc })
    }

    render() {
        return (
            <div className='baocao-page-wrapper'>
                <h1 className='txt-page-header'>Báo cáo số liệu</h1>
                <Card style={{ marginTop: 24 }} title={
                    <div>
                        <span style={{ marginRight: 24 }}> Bảng số liệu báo cáo: </span>
                        <SelectKyKiemKe userId={this.props.sessionStore?.currentLogin?.user?.id} onSelectKyKiemKe={this.onSelectKyKiemKe} />
                    </div>
                }>
                    <TableBaoCao
                        capDVHCID={this.state?.dvhcSelected?.capDVHCId}
                        maDVHC={this.state?.dvhcSelected?.ma}
                        year={this.state?.dvhcSelected?.year} />
                </Card>

            </div>
        )
    }
}

export default BaoCaoSoLieu;