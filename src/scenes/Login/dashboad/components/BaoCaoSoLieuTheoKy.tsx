import React from 'react'
import CircleChart from './CircleChart'
import BarChart from './BarChart'

export default function BaoCaoSoLieuTheoKy(props: any) {
    return (
        <div>
            <div style={{ display: 'flex', flexDirection: 'row', marginTop: 50 }}>
                <div style={{ flex: 1, marginRight: 10 }}>
                    <CircleChart baoCaoData={props.baoCaoData} />
                </div>
                <div style={{ flex: 1, marginRight: 10 }}>
                    <BarChart baoCaoData={props.baoCaoData} />
                </div>
            </div>
        </div>
    )
}
