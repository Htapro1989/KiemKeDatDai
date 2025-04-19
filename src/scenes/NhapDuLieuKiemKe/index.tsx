import React, { useContext, useEffect, useState } from 'react'
import './index.less'
import { Button, Card, Empty, Select, Table } from 'antd'
import CustomModal from '../Home/components/CustomModal';
import Bieu06Input from './components/Bieu06Input';
import Bieu02Input from './components/Bieu02Input';
import { MobXProviderContext } from 'mobx-react';
import dvhcService from '../../services/dvhc/dvhcService';
import { CAP_DVHC_ENUM } from '../../models/enum';

function useStores() {
    return useContext(MobXProviderContext);
}

export default function NhapDuLieuKiemKePage() {

    const [isShowModal, setIsShowModal] = useState(false)
    const [isShowModal02, setIsShowModal02] = useState(false)
    const [tableData, setTableData] = useState<any>([])

    const { donViHanhChinhStore } = useStores();
    const dvhcId = donViHanhChinhStore?.donViHanhChinhOfUser?.id
    const capDVHCId = donViHanhChinhStore?.donViHanhChinhOfUser?.capDVHCId

    const [dvhcState, setDvhcState] = useState<any>()
    const updateState = (newState: Partial<any>) => {
        setDvhcState((prevState: any) => ({
            ...prevState,
            ...newState
        }))
    }
    const fetchKyKiemKe = async (year?: any) => {
        const kyOptions = await dvhcService.getKyKiemKeAsOption();
        const kyKiemKeSelected = kyOptions.find((item: any) => item.value == year)?.value || kyOptions[0]?.value;

        updateState({
            kyKiemKeOptions: kyOptions,
            dmKyKiemKeSelected: kyKiemKeSelected,
        })
    }


    const columns: any = [
        { title: 'Ký hiệu', dataIndex: 'kyHieu', key: 'kyHieu' },
        { title: 'Tên', dataIndex: 'name', key: 'name' },
        {
            title: 'Nhập báo cáo', dataIndex: 'input', key: 'input',
            render: (text: string, item: any) => (
                <div>
                    <Button type='primary' style={{ marginRight: 4 }}
                        onClick={() => {
                            if (item.id == 1)
                                setIsShowModal(true)
                            else setIsShowModal02(true)
                        }}
                    >Biểu mẫu</Button>
                    <Button type='primary' ghost>File Excel</Button>
                </div>
            ),
        },

    ];



    useEffect(() => {
        fetchKyKiemKe()
    }, [])

    useEffect(() => {
        if (capDVHCId == CAP_DVHC_ENUM.TINH) {
            setTableData([
                {
                    "id": 1,
                    "kyHieu": "06/TKKKQPAN",
                    'name': 'Thống kê, kiểm kê đất quốc phòng, đất an ninh',
                },
                {
                    "id": 2,
                    "kyHieu": "02/aKKNLT",
                    'name': 'Kiểm kê tình hình đo đạc, cấp giấy chứng nhận và hình thức sử dụng đất của các công ty nông, lâm nghiệp',
                }
            ])
        }
    }, [capDVHCId])


    const onHandleChangeKyKiemKe = (value: any) => {
        updateState({
            dmKyKiemKeSelected: value,
        })
    }

    return (
        <div className='dulieukiemke-page-wrapper'>
            <h1 className='txt-page-header'>Nhập dữ kiệu kiểm kê</h1>
            <Card title={(<Select
                value={dvhcState?.dmKyKiemKeSelected}
                onChange={onHandleChangeKyKiemKe}
                options={dvhcState?.kyKiemKeOptions}
            />)}>
                <Table
                    rowKey={"id"}
                    bordered={true}
                    columns={columns}
                    size='small'
                    pagination={false}
                    loading={false}
                    dataSource={tableData}
                    locale={{
                        emptyText: (
                            <Empty description="Không có dữ liệu"> </Empty>
                        ),
                    }}
                />
            </Card>

            <CustomModal
                onClose={() => setIsShowModal(false)}
                visible={isShowModal} >
                <Bieu06Input isRefresh={isShowModal} dvhcId={dvhcId} year={dvhcState?.dmKyKiemKeSelected} />
            </CustomModal>
            <CustomModal
                onClose={() => setIsShowModal02(false)}
                visible={isShowModal02} >
                <Bieu02Input isRefresh={isShowModal02} dvhcId={dvhcId} year={dvhcState?.dmKyKiemKeSelected} />
            </CustomModal>

        </div>
    )
}
