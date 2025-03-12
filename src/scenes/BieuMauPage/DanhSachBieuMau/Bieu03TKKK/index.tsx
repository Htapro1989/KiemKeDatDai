import React from 'react'
import '../components/index.less'
import HeaderBieuMau from '../components/Header'


export default function Bieu03TKKK() {
    return (
        <div className='bieu-mau__layout-wrapper'>
            <HeaderBieuMau maBieu='Biểu 03/TKKK' tenBieu='THỐNG KÊ, KIỂM KÊ DIỆN TÍCH ĐẤT ĐAI THEO ĐƠN VỊ HÀNH CHÍNH' />
            <table className="report-table">
                <thead>
                    <tr>
                        <th className='vertical-align-center-text'>Thứ tự</th>
                        <th className='vertical-align-center-text'>Loại đất</th>
                        <th className='vertical-align-center-text'>Mã</th>
                        <th className='vertical-align-center-text'>Tổng diện tích</th>
                        <th className='vertical-align-center-text'>Diện tích theo từng đơn vị hành chính trực thuộc</th>

                    </tr>

                </thead>
                <tbody>
                    <tr>
                        <td>(1)</td>
                        <td>(2)</td>
                        <td>(3)</td>
                        <td>(4)</td>
                    </tr>
                </tbody>
            </table>
        </div>
    )
}
