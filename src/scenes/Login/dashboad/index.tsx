import React, { useEffect, useState } from 'react'
import SoLieuThongKeBar from './components/SoLieuThongKeBar'
import { Card, Divider } from 'antd'
import BaoCaoSoLieuTheoKy from './components/BaoCaoSoLieuTheoKy'
import baoCaoService from '../../../services/baoCao/baoCaoService'
import './index.less'

export default function DashBoadComponent() {

    const [baoCaoData, setBaoCaoData] = useState<any>()

    const getDataDashboad = async () => {
        const response = await baoCaoService.baoCaoSoLieu();
        if (response.code == 1) {
            setBaoCaoData(response.returnValue)
        }
    }

    useEffect(() => {
        getDataDashboad();
    }, [])

    return (
        <div style={{ display: 'flex', flex: 1, flexDirection: 'row', justifyContent: 'center' }}>
            <div className='dashboad-component'>
                <Card title='Thống kê kiểm kê đất đai năm 2024'>
                    <SoLieuThongKeBar baoCaoData={baoCaoData} />
                    <Divider />
                    <BaoCaoSoLieuTheoKy baoCaoData={baoCaoData} />
                </Card>
            </div>
        </div>
    )
}
