import { Table } from 'antd';
import { ColumnsType } from 'antd/lib/table';
import React, { useEffect, useState } from 'react'
import dvhcService from '../../../services/dvhc/dvhcService';
import './style.less'

interface ITableBaoCaoProps {
    maDVHC: any;
    year: any;
    capDVHCID: any
}

export default function TableBaoCaoLogin(props: ITableBaoCaoProps) {
    const [listDvhc, setListDvhc] = useState([])
    const [expandedKeys, setExpandedKeys] = useState<any[]>([])
    const [isFetchingData, setIsFetchingData] = useState(false)


    const columns: ColumnsType<any> = [
        { title: 'Đơn vị hành chính', dataIndex: 'ten', key: 'ten' },
        {
            title: 'Tổng nộp/Tổng số', dataIndex: 'tongNopTrenTongXa', key: 'tongNop',
            align: "center",
            render: (text: string, item: any) => (<div>{item?.tongNop}/{item?.tong}</div>)
        },
        {
            title: 'Tổng duyệt/Tổng số', dataIndex: 'tongDuyetTrenTongXa', key: 'tongDuyet', align: "center",
            render: (text: string, item: any) => (<div>{item?.tongDuyet}/{item?.tong}</div>)
        }
    ];

    const onRefactorBaoCaoList = (maDVHCCha: string, responseList: any[]) => {
        let listBaoCao = responseList.filter(bc => bc.capDVHC != 0)
            .map(bc => {
                return {
                    ...bc,
                    children: bc.childStatus == 1 ? [] : null
                }
            })

        // let baoCaoCha = responseList.find(e => e.maDVHC == maDVHCCha);
        // baoCaoCha = {
        //     ...baoCaoCha,
        //     children: responseList.filter(e => e.maDVHC != maDVHCCha)
        //         .map(e => ({ ...e, children: e.childStatus == 1 ? [] : null }))
        // }
        return listBaoCao;
    }

    const getAllBaoCao = async () => {
        setIsFetchingData(true)
        const baoCaoResponse = await dvhcService.getBaoCaoDVHCForDashboard("0", "2024");
        if (!baoCaoResponse || baoCaoResponse.code != 1 || baoCaoResponse.returnValue.length <= 0) return;
        const newListDvhc: any = onRefactorBaoCaoList(props.maDVHC, baoCaoResponse.returnValue);
        setListDvhc(newListDvhc)
        setExpandedKeys([props.maDVHC])
        setIsFetchingData(false)
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
        const baoCaoResponse = await dvhcService.getBaoCaoDVHCForDashboard(ma, year);
        if (!baoCaoResponse || baoCaoResponse.code != 1 || baoCaoResponse.returnValue.length <= 0) return;
        const newList = baoCaoResponse.returnValue
            .filter(e => e.maDVHC != ma)
            .map(e => ({ ...e, children: e.childStatus == 1 ? [] : null }));
        const newListDvhc: any = updateChild(listDvhc, parentId, newList);
        setListDvhc(newListDvhc)
    }

    useEffect(() => {
        getAllBaoCao()
    }, [])

    return (
        <div style={{ marginTop: 24 }}>
            <Table
                rowKey="maDVHC"
                bordered={true}
                columns={columns}
                loading={isFetchingData}
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
