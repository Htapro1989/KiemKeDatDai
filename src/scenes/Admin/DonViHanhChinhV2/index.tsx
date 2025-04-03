import { Card, Select, Tree } from 'antd'
import React, { useEffect, useState } from 'react'
import dvhcService from '../../../services/dvhc/dvhcService'
import './index.less'
import { DonViHanhChinhMapper } from '../../../mapper/DonViHanhChinhMapper'
import { FormInstance } from 'antd/lib/form'
import CreateOrUpdateDvhcForm from './components/CreateOrUpdateDvhcForm'

interface IStateDVHC {
    donViHanhChinhList?: any[]
    isFetchingDonViHanhChinh?: boolean
    kyKiemKeOptions?: any[]
    dmKyKiemKeSelected?: string
    donViHanhChinhSelected?: any
    sideMenuExpanedKeys?: any
}

export default function DonViHanhChinhV2(props: any) {

    const formRef = React.createRef<FormInstance>();

    const [dvhcState, setDvhcState] = useState<IStateDVHC>()
    const [editFormState, setEditFormState] = useState<any>()

    const updateState = (newState: Partial<IStateDVHC>) => {
        setDvhcState((prevState) => ({
            ...prevState,
            ...newState
        }))
    }

    const updateEditFormState = (newState: any) => {
        const newEditFormState = {
            ...editFormState,
            ...newState
        }
        setEditFormState(newEditFormState)
        formRef?.current?.setFieldsValue(newEditFormState)
    }

    const fetchKyKiemKe = async () => {
        const ky = await dvhcService.getKyKiemKeAsOption();
        const kyKiemKeSelected = ky[0]?.value;
        const dvhcByUserResponse = await dvhcService.getByUser(props.userId, kyKiemKeSelected)
        if (!dvhcByUserResponse || dvhcByUserResponse.code != 1 || dvhcByUserResponse.returnValue.length <= 0) return;
        const donViHanhChinhList = dvhcByUserResponse.returnValue
            .map(DonViHanhChinhMapper.toDonViHanhChinhMenu);

        const donViHanhChinhSelected = donViHanhChinhList?.[0];
        updateEditFormState(donViHanhChinhSelected)
        const newlist = await fetchDonViHanhChinhListByParentKey(donViHanhChinhSelected?.id, donViHanhChinhList)
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
        const children = dvhcByParentIdResponse.returnValue.map(DonViHanhChinhMapper.toDonViHanhChinhMenu);
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

    const onHandleChangeKyKiemKe = () => {


    }

    const onDeleteDvhc = (id: any) => {
        const removeItemById = (list: any, id: any) => {
            return list
                .filter((item: any) => item.id !== id) // Lọc bỏ phần tử có id = 1
                .map((item: any) => ({
                    ...item,
                    children: item.children ? removeItemById(item.children, id) : [], // Đệ quy vào children
                }));
        };

        const newList = removeItemById(dvhcState?.donViHanhChinhList, id)
        updateState({
            donViHanhChinhList: newList,
            donViHanhChinhSelected: null
        })
        setEditFormState(null)
    }

    const onUpdateDvhc = (newItem: any) => {
        const updateDvhcById = (list: any, updateItem: any) => {
            return list
                .map((item: any) => {
                    if (item.id == updateItem.id) {
                        return {
                            ...item,
                            ma: updateItem.ma,
                            title: updateItem.name,
                            name: updateItem.name,
                        }
                    }
                    return {
                        ...item,
                        children: item.children ? updateDvhcById(item.children, updateItem) : [], // Đệ quy vào children
                    }
                });
        };

        const newList = updateDvhcById(dvhcState?.donViHanhChinhList, newItem)
        console.log("NEW LIST ", newList)
        updateState({
            donViHanhChinhList: newList
        })
    }

    useEffect(() => {
        if (props.userId)
            fetchKyKiemKe();
    }, [props.userId])


    return (
        <Card>
            <div className='card-wrapper'>
                <div className='left-component-wrapper'>
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
                                updateEditFormState(info.node)
                                updateState({
                                    donViHanhChinhSelected: info.node
                                })
                            }
                            }
                        />
                    </div>
                </div>
                <CreateOrUpdateDvhcForm entity={editFormState}
                    onUpdateDvhc={onUpdateDvhc}
                    onDeleteDvhc={onDeleteDvhc} />
            </div>
        </Card>
    )
}
