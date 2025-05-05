import { Select, Tree } from 'antd'
import React, { useEffect, useState } from 'react'
import dvhcService from '../../../../services/dvhc/dvhcService';
import { DonViHanhChinhMapper } from '../../../../mapper/DonViHanhChinhMapper';
import '../index.less'

export default function DvhcTree(props: any) {
    const [dvhcState, setDvhcState] = useState<any>()
    const updateState = (newState: Partial<any>) => {
        setDvhcState((prevState: any) => ({
            ...prevState,
            ...newState
        }))
    }

    const onSelectDvhc = (dvhcSelected: any) => {
        props.onSelect(dvhcSelected)

    }

    const fetchKyKiemKe = async (year?: any) => {
        const ky = await dvhcService.getKyKiemKeAsOption();
        const kyKiemKeSelected = ky.find((item: any) => item.value == year)?.value || ky[0]?.value;

        const dvhcByUserResponse = await dvhcService.getByUser(props.userId, kyKiemKeSelected)
        if (!dvhcByUserResponse || dvhcByUserResponse.code != 1) {
            return
        };

        const donViHanhChinhList = dvhcByUserResponse.returnValue.map(DonViHanhChinhMapper.toDonViHanhChinhMenuByUser);

        const donViHanhChinhSelected = donViHanhChinhList?.[0] || [];
        const newlist = await fetchDonViHanhChinhListByParentKey(donViHanhChinhSelected?.id, donViHanhChinhList) || []
        onSelectDvhc(donViHanhChinhSelected)
        updateState({
            kyKiemKeOptions: ky,
            dmKyKiemKeSelected: kyKiemKeSelected,
            donViHanhChinhList: newlist,
            donViHanhChinhSelected,
            sideMenuExpanedKeys: [donViHanhChinhSelected?.key]
        })
    }

    const fetchDonViHanhChinhListByParentKey = async (parentId: any, list: any[]) => {
        if (!parentId) return;
        const dvhcByParentIdResponse = await dvhcService.getByParentId(parentId)
        if (!dvhcByParentIdResponse || dvhcByParentIdResponse.code != 1 || dvhcByParentIdResponse.returnValue.length <= 0) return;
        const children = dvhcByParentIdResponse.returnValue.map(DonViHanhChinhMapper.toDonViHanhChinhMenuByUser);
        const updateTreeData = (list: any[], key: string, children: any[]): any[] =>
            list.map(node => {
                if (node.key == key) {
                    return {
                        ...node,
                        children,
                    };
                }
                if (node.children) {
                    return {
                        ...node,
                        children: updateTreeData(node.children, key, children),
                    };
                }
                return node;
            });
        const newList = updateTreeData(list, parentId, children);
        return newList;
    }
    const onLoadMoreData = async ({ key, children }: any) => {
        if (children) {
            return;
        }
        const res = await fetchDonViHanhChinhListByParentKey(key, dvhcState?.donViHanhChinhList as any)
        if (!res || res.length <= 0) return;
        updateState({
            donViHanhChinhList: res
        })
    }

    const onHandleChangeKyKiemKe = (year: any) => {
        fetchKyKiemKe(year)
    }

    useEffect(() => {
        if (props.userId)
            fetchKyKiemKe();
    }, [props.userId])

    return (
        <>
            <Select
                value={dvhcState?.dmKyKiemKeSelected}
                onChange={onHandleChangeKyKiemKe}
                options={dvhcState?.kyKiemKeOptions}
                style={{ marginBottom: 24 }}
            />
            <div className='right-menu-layout'>
                <Tree
                    autoExpandParent={true}
                    treeData={dvhcState?.donViHanhChinhList as any}
                    loadData={onLoadMoreData}
                    expandedKeys={dvhcState?.sideMenuExpanedKeys}
                    onExpand={(keys) => {
                        updateState({
                            sideMenuExpanedKeys: keys
                        })
                    }}
                    selectedKeys={[dvhcState?.donViHanhChinhSelected?.key]}
                    onSelect={(selectedKeys, info) => {
                        updateState({
                            donViHanhChinhSelected: info.node
                        })
                        onSelectDvhc(info.node)
                    }
                    }
                />
            </div>
        </>
    )
}
