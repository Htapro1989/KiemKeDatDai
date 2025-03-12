import React from 'react'
import '../components/index.less'
import HeaderBieuMau from '../components/Header'

export default function Bieu02KKSL() {
    return (
        <div className='bieu-mau__layout-wrapper'>
            <HeaderBieuMau maBieu='Biểu số 02/KKSL' tenBieu='DANH SÁCH ĐIỂM BỊ SẠT LỞ, BỒI ĐẮP TRONG 5 NĂM (2020-2024)' />
            <table className="report-table">
                <thead>
                    <tr>
                        <th>STT</th>
                        <th>Tên điểm sạt lở, bồi đắp</th>
                        <th>Diện tích <i>(ha)</i></th>
                        <th>
                            Địa điểm sạt lở, bồi đắp <br /> <i>(thôn, xóm, .../xã/huyện/tỉnh)</i>
                        </th>
                        <th>Năm sạt lở, bồi đắp</th>
                        <th>Ghi chú</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        {Array.from({ length: 6 }, (_, index) => index + 1).map(a => {
                            return (<td key={a}>({a})</td>)
                        })}
                    </tr>
                </tbody>
            </table>
        </div>
    )
}
