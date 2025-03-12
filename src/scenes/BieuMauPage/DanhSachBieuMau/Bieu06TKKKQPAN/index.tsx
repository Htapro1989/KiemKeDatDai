import React from 'react'
import '../components/index.less'
import HeaderBieuMau from '../components/Header'


export default function Bieu06TKKKQPAN() {
    return (
        <div className='bieu-mau__layout-wrapper'>
            <HeaderBieuMau maBieu='Biểu 06/TKKKQPAN' tenBieu='THỐNG KÊ, KIỂM KÊ ĐẤT QUỐC PHÒNG, ĐẤT AN NINH' />
            <table className="report-table">
                <thead>
                    <tr>
                        <th rowSpan={2} className='vertical-align-center-text'>Thứ tự</th>
                        <th rowSpan={2} className='vertical-align-center-text'>Đơn vị (hoặc điểm) sử dụng đất</th>
                        <th rowSpan={2} className='vertical-align-center-text'>Địa chỉ sử dụng đất</th>
                        <th rowSpan={2} className='vertical-align-center-text'>Diện tích đất quốc phòng/đất an ninh</th>
                        <th colSpan={2} className='vertical-align-center-text'>Trong đó diện tích kết hợp vào mục đích khác	</th>
                        <th colSpan={3} className='vertical-align-center-text'>Tình hình đo đạc lập bản đồ địa chính hoặc trích đo địa chính, cấp Giấy chứng nhận</th>
                        <th rowSpan={2} className='vertical-align-center-text'>Ghi chú</th>
                    </tr>
                    <tr>
                        <th className='vertical-align-center-text'>Diện tích</th>
                        <th className='vertical-align-center-text'>Loại đất kết hợp</th>
                        <th className='vertical-align-center-text'>Diện tích đã đo đạc	</th>
                        <th className='vertical-align-center-text'>Số Giấy chứng nhận đã cấp</th>
                        <th className='vertical-align-center-text'>Diện tích đã cấp giấy chứng nhận</th>
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
