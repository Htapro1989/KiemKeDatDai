import React from 'react'
import './index.less';
import SessionStore from '../../stores/sessionStore';
import DonViHanhChinhStore from '../../stores/donViHanhChinhStore';
import Stores from '../../stores/storeIdentifier';
import { inject, observer } from 'mobx-react';
import { Button, Card, Select, Table } from 'antd';
import { ColumnsType } from 'antd/lib/table';
import dvhcService from '../../services/dvhc/dvhcService';


export interface IBaoCaoSoLieuProps {
    sessionStore?: SessionStore;
    donViHanhChinhStore?: DonViHanhChinhStore;
}
export interface IBaoCaoSoLieuState {
    listDvhc: any
}




@inject(Stores.SessionStore, Stores.DonViHanhChinhStore)
@observer
export class BaoCaoSoLieu extends React.Component<IBaoCaoSoLieuProps, IBaoCaoSoLieuState> {
    state = {
        listDvhc: []
    }


    public handleChange = (value: string) => {
        console.log(`selected ${value}`);
    };

    componentDidMount(): void {
        this.getAllBaoCao();
    }

    onRefactorBaoCaoList = (maDVHCCha: string, responseList: any[]) => {
        let baoCaoCha = responseList.find(e => e.maDVHC == maDVHCCha);
        baoCaoCha = {
            ...baoCaoCha,
            children: responseList.filter(e => e.maDVHC != maDVHCCha)
                .map(e => ({ ...e, children: e.childStatus == 1 ? [] : null }))
        }
        return [baoCaoCha];
    }

    public getAllBaoCao = async () => {

        const baoCaoResponse = await dvhcService.getBaoCaoDVHC("11", "2024");
        if (!baoCaoResponse || baoCaoResponse.code != 1 || baoCaoResponse.returnValue.length <= 0) return;

        this.setState({ listDvhc: this.onRefactorBaoCaoList("11", baoCaoResponse.returnValue) })
    }

    updateChild = (list: any, id: any, children: any) => {
        return list.map((dvhc: any) => {
            if (dvhc.id == id) {
                return {
                    ...dvhc,
                    children,
                };
            }
            if (dvhc.children) {
                return {
                    ...dvhc,
                    children: this.updateChild(dvhc.children, id, children),
                };
            }
            return dvhc;
        });
    }

    getChildBaoCao = async (ma: string, year: string, parentId: any) => {
        const baoCaoResponse = await dvhcService.getBaoCaoDVHC(ma, year);
        if (!baoCaoResponse || baoCaoResponse.code != 1 || baoCaoResponse.returnValue.length <= 0) return;
        const newList = baoCaoResponse.returnValue.filter(e => e.maDVHC != ma);
        this.setState({ listDvhc: this.updateChild(this.state.listDvhc, parentId, newList) })
    }


    render() {
        const columns: ColumnsType<any> = [
            { title: 'Đơn vị hành chính', dataIndex: 'ten', key: 'ten' },
            { title: 'Ngày cập nhật', dataIndex: 'ngayCapNhat', key: 'ngayCapNhat' },
            {
                title: 'Tổng nộp/Tổng xã', dataIndex: 'tongNopTrenTongXa', key: 'tongNop',
                render: (text: string, item: any) => (<div>{item?.tongNop}/{item?.tong}</div>)
            },
            {
                title: 'Tổng duyệt/Tổng xã', dataIndex: 'tongDuyetTrenTongXa', key: 'tongDuyet',
                render: (text: string, item: any) => (<div>{item?.tongDuyet}/{item?.tong}</div>)
            },
            {
                title: 'Trạng thái', dataIndex: 'status', key: 'status',
                render: (text: string, item: any) => {
                    if (item.root && item.isNopBaoCao)
                        return <Button type="primary">Nộp báo cáo</Button>
                    if (item.trangThaiDuyet == 2 && !item.root)
                        return <Button>Trả lại báo cáo</Button>
                    if (!item.root && item.trangThaiDuyet == 1)
                        return <Button size='small' type="primary">Duyệt báo cáo</Button>
                    return null;
                }
            },
        ];
        const { dmKyKiemKe, dmKyKiemKeSelected } = this.props.donViHanhChinhStore!
        return (
            <div className='baocao-page-wrapper'>
                <h1 className='txt-page-header'>Báo cáo số liệu</h1>
                <Card style={{ marginTop: 24 }} title={
                    <div>
                        <span style={{ marginRight: 24 }}> Bảng số liệu báo cáo: </span>
                        <Select
                            value={dmKyKiemKeSelected}
                            onChange={this.handleChange}
                            options={dmKyKiemKe}
                        />
                    </div>
                }>
                    <Table
                        rowKey="maDVHC"
                        bordered={true}
                        columns={columns}
                        expandable={{
                            defaultExpandedRowKeys: ["11"],
                            onExpand: (expanded, record) => {
                                if (record.children.length > 0) return
                                this.getChildBaoCao(record.maDVHC, '2024', record.id);
                            },
                        }}
                        dataSource={this.state.listDvhc}
                        pagination={{ pageSize: 1000 }}
                    />
                </Card>

            </div>
        )
    }
}

export default BaoCaoSoLieu;