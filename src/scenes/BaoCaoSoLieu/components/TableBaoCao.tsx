import { Button, notification, Row, Table } from 'antd';
import { ColumnsType } from 'antd/lib/table';
import moment from 'moment';
import React, { useEffect, useState } from 'react'
import dvhcService from '../../../services/dvhc/dvhcService';
import baoCaoService from '../../../services/baoCao/baoCaoService';
import ConfirmButton from '../../../components/AppComponentBase/ConfirmButton';
import { CAP_DVHC_ENUM } from '../../../models/enum';

interface ITableBaoCaoProps {
    maDVHC: any;
    year: any;
    capDVHCID: any
}

export default function TableBaoCao(props: ITableBaoCaoProps) {
    const [listDvhc, setListDvhc] = useState([])
    const [isActioning, setIsActioning] = useState<any>()
    const [expandedKeys, setExpandedKeys] = useState<any[]>([])

    const renderTrangThaiBaoCao: any = (text: string, item: any) => {

        if (item.root && item.isNopBaoCao)
            return <Button
                type="primary"
                size='small'
                loading={isActioning?.maDv == item.maDVHC && isActioning?.loading}
                onClick={onNopBaoCao}>Nộp</Button>
        if (!item.root && (item.trangThaiDuyet == 1 || item.trangThaiDuyet == 2))
            return (
                <Row>
                    <Button
                        size='small'
                        type="primary"
                        disabled={item.trangThaiDuyet == 2}
                        loading={isActioning?.maDv == item.maDVHC && isActioning?.loading && isActioning.action == 'DUYET'}
                        onClick={() => onDuyetBaoCaoDonViBenDuoi(item.maDVHC)}
                        style={{ marginRight: 4 }}>Duyệt</Button>
                    <ConfirmButton
                        btnTitle='Trả lại'
                        confirmTitle='Trả lại báo cáo của đơn vị này?'
                        onConfirm={() => {
                            onHuyDuyetBaoCaoDonViBenDuoi(item.maDVHC)
                        }}
                        buttonProps={{
                            type: "ghost",
                            size: 'small',
                            loading: isActioning?.maDv == item.maDVHC && isActioning?.loading
                        }}
                    />
                </Row>
            )
        return null;
    }

    const onDuyetBaoCaoDonViBenDuoi = async (maDVHCNopBaoCao: string) => {
        setIsActioning({
            maDv: maDVHCNopBaoCao,
            loading: true,
            action: 'DUYET'
        })
        const response = await baoCaoService.duyetBaoCao(props.capDVHCID, maDVHCNopBaoCao, props.year);
        if (response?.code == 1) {
            //thanh cong
            notification.success({
                message: response.message
            })
            getAllBaoCao()
        } else {
            notification.error({
                message: response?.message ? response?.message : "Thất bại. Vui lòng thử lại"
            })
        }
        setIsActioning({
            loading: false
        })
    }
    const onHuyDuyetBaoCaoDonViBenDuoi = async (maDVHCNopBaoCao: string) => {
        setIsActioning({
            maDv: maDVHCNopBaoCao,
            loading: true
        })
        let response = await baoCaoService.huyDuyetBaoCao(props.capDVHCID, maDVHCNopBaoCao, props.year);
        if (response?.code == 1) {
            //thanh cong
            notification.success({
                message: response?.message
            })
            getAllBaoCao()
        } else {
            notification.error({
                message: response?.message ? response?.message : "Thất bại. Vui lòng thử lại"
            })
        }
        setIsActioning({
            loading: false
        })
    }

    const onNopBaoCao = async () => {
        setIsActioning({
            maDv: props.maDVHC,
            loading: true
        })
        const response = await baoCaoService.nopBaoCao(props.year);
        if (response?.code == 1) {
            //thanh cong
            notification.success({
                message: response.message
            })
            getAllBaoCao()
        } else {
            notification.error({
                message: response?.message ? response?.message : "Thất bại. Vui lòng thử lại"
            })
        }
        setIsActioning({
            loading: false
        })
    }

    const columns: ColumnsType<any> = [
        { title: 'Đơn vị hành chính', dataIndex: 'ten', key: 'ten' },
        {
            title: 'Ngày cập nhật', dataIndex: 'ngayCapNhat', key: 'ngayCapNhat', align: "center",
            render: (text: string, item: any) => (<div>{item?.ngayCapNhat ? moment(item?.ngayCapNhat, 'YYYY-MM-DD').format("DD/MM/YYYY") : ''}</div>)
        },
        {
            title: 'Tổng nộp/Tổng xã', dataIndex: 'tongNopTrenTongXa', key: 'tongNop',
            align: "center",
            render: (text: string, item: any) => (<div>{item?.tongNop}/{item?.tong}</div>)
        },
        {
            title: 'Tổng duyệt/Tổng xã', dataIndex: 'tongDuyetTrenTongXa', key: 'tongDuyet', align: "center",
            render: (text: string, item: any) => (<div>{item?.tongDuyet}/{item?.tong}</div>)
        },
        {
            title: 'Trạng thái', dataIndex: 'status', key: 'status', align: "center",
            render: renderTrangThaiBaoCao,
            //Vùng ko đc phép duyệt/nộp báo cáo
            className: CAP_DVHC_ENUM.VUNG == props.capDVHCID ? "hidden-column" : '',
        },
    ];

    const onRefactorBaoCaoList = (maDVHCCha: string, responseList: any[]) => {
        let baoCaoCha = responseList.find(e => e.maDVHC == maDVHCCha);
        baoCaoCha = {
            ...baoCaoCha,
            children: responseList.filter(e => e.maDVHC != maDVHCCha)
                .map(e => ({ ...e, children: e.childStatus == 1 ? [] : null }))
        }
        return [baoCaoCha];
    }

    const getAllBaoCao = async () => {
        const baoCaoResponse = await dvhcService.getBaoCaoDVHC(props.maDVHC, props.year);
        if (!baoCaoResponse || baoCaoResponse.code != 1 || baoCaoResponse.returnValue.length <= 0) return;
        const newListDvhc: any = onRefactorBaoCaoList(props.maDVHC, baoCaoResponse.returnValue);
        setListDvhc(newListDvhc)
        setExpandedKeys([props.maDVHC])
    }

    const updateChild = (list: any, id: any, children: any) => {
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
                    children: updateChild(dvhc.children, id, children),
                };
            }
            return dvhc;
        });
    }

    const getChildBaoCao = async (ma: string, year: string, parentId: any) => {
        const baoCaoResponse = await dvhcService.getBaoCaoDVHC(ma, year);
        if (!baoCaoResponse || baoCaoResponse.code != 1 || baoCaoResponse.returnValue.length <= 0) return;
        const newList = baoCaoResponse.returnValue
            .filter(e => e.maDVHC != ma)
            .map(e => ({ ...e, trangThaiDuyet: null }));
        const newListDvhc: any = updateChild(listDvhc, parentId, newList);
        setListDvhc(newListDvhc)
    }

    useEffect(() => {
        if (props.maDVHC && props.year) {
            getAllBaoCao()
        }
    }, [props.maDVHC, props.year])

    return (
        <div>
            <Table
                rowKey="maDVHC"
                bordered={true}
                columns={columns}
                // 

                expandable={{
                    onExpand: (expanded, record) => {
                        if (record.children.length > 0) return
                        getChildBaoCao(record.maDVHC, props.year, record.id);
                    },
                    onExpandedRowsChange: (key) => {
                        setExpandedKeys(key)
                    },
                    expandedRowKeys: expandedKeys
                }}
                dataSource={listDvhc}
                pagination={false}
            />
        </div>
    )
}
