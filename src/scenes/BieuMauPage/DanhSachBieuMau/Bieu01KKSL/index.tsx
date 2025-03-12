import React from 'react'
import '../components/index.less'
import HeaderBieuMau from '../components/Header'

export default function Bieu01KKSL() {
    return (
        <div className='bieu-mau__layout-wrapper'>
            <HeaderBieuMau maBieu='Biểu số 01/KKSL' tenBieu='KIỂM KÊ DIỆN TÍCH ĐẤT BỊ SẠT LỞ, BỒI ĐẮP TRONG 5 NĂM (2020-2024)' />
            <table className="report-table">
                <thead>
                    <tr>
                        <th rowSpan={3}>STT</th>
                        <th rowSpan={3}>Loại đất</th>
                        <th rowSpan={3}>Mã</th>
                        <th colSpan={4}>Diện tích bị sạt lở</th>
                        <th colSpan={3}>Diện tích bồi đắp</th>
                    </tr>
                    <tr>
                        <th rowSpan={2}>Tổng diện tích</th>
                        <th colSpan={3}>
                            <i>Trong đó: </i>
                        </th>
                        <th rowSpan={2}>Tổng diện tích</th>
                        <th colSpan={2}>
                            <i>Trong đó: </i>
                        </th>
                    </tr>
                    <tr>
                        <th>
                            Sạt lở vùng bờ sông <i>(SLS)</i>
                        </th>
                        <th>
                            Sạt lở vùng đồi núi <i>(SLN)</i>
                        </th>
                        <th>
                            Sạt lở vùng bờ biển <i>(SLB)</i>
                        </th>
                        <th>
                            Bồi đắp vùng bờ sông <i>(BDS)</i>
                        </th>
                        <th>
                            Bồi đắp vùng bờ biển <i>(BDB)</i>
                        </th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        {Array.from({ length: 10 }, (_, index) => index + 1).map(a => {
                            return (<td key={a}>({a})</td>)
                        })}
                    </tr>
                </tbody>
            </table>
        </div>
    )
}
