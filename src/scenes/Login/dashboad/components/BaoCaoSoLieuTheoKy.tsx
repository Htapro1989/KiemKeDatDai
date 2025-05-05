import React, { useState } from 'react'

import { Button, Row } from 'antd'
import { AppstoreFilled, PieChartFilled } from '@ant-design/icons'
import BaoCaoChart from './BaoCaoChart'
import TableBaoCaoLogin from '../../../BaoCaoSoLieu/components/TableBaoCaoLogin'

export default function BaoCaoSoLieuTheoKy(props: any) {

    const [activeTab, setActiveTab] = useState(1)

    const activeProps = { type: 'primary', ghost: true }
    const inactiveProps = {}

    const tab1Active = activeTab == 1 ? activeProps : inactiveProps
    const tab2Active = activeTab == 2 ? activeProps : inactiveProps

    return (
        <div>
            <Row>
                <h3 style={{ marginRight: 16 }}>Báo cáo số liệu theo kỳ</h3>
                <Button {...tab1Active} style={{ marginRight: 4 }} icon={<PieChartFilled />}
                    onClick={() => { setActiveTab(1) }}
                />
                <Button {...tab2Active} icon={<AppstoreFilled />}
                    onClick={() => { setActiveTab(2) }} />
            </Row>

            {
                activeTab == 1 ? (
                    <BaoCaoChart baoCaoData={props.baoCaoData} />
                ) : (
                    <TableBaoCaoLogin
                        maDVHC={0}
                        year={2024}
                        capDVHCID={0}
                    />
                )
            }


        </div>
    )
}
