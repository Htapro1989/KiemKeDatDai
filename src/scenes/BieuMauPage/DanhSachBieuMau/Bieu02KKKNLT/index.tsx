import React from 'react'
import '../components/index.less'
import HeaderBieuMau from '../components/Header'

export default function Bieu02KKKNLT() {
    return (
        <div className='bieu-mau__layout-wrapper'>
            <HeaderBieuMau maBieu='Biểu số 02/KKNLT' tenBieu='KIỂM KÊ TÌNH HÌNH ĐO ĐẠC, CẤP GIẤY CHỨNG NHẬN VÀ HÌNH THỨC SỬ DỤNG ĐẤT CỦA CÁC CÔNG TY NÔNG, LÂM NGHIỆP' />
            <table className="report-table">
                <thead>
                    <tr>
                        <th rowSpan={2}>STT</th>
                        <th rowSpan={2}>Tên đơn vị sử dụng đất</th>
                        <th rowSpan={2}>Tổng diện tích đang quản lý, sử dụng</th>
                        <th colSpan={3}>Hình thức sử dụng đất</th>
                        <th colSpan={5}>Diện tích đã đo đạc lập bản đồ địa chính</th>
                        <th colSpan={2}>Cấp Giấy chứng nhận</th>
                        <th rowSpan={2} style={{ color: "red" }}>Diện tích đã bàn giao về địa phương</th>
                        <th rowSpan={2}>Ghi chú</th>
                    </tr>
                    <tr>
                        <th>Nhà nước giao đất</th>
                        <th>Nhà nước cho thuê đất</th>
                        <th>Chưa xác định hình thức</th>
                        <th>Tổng diện tích</th>
                        <th style={{ color: "red" }}>Tỷ lệ 1:1.000</th>
                        <th style={{ color: "red" }}>Tỷ lệ 1:2.000</th>
                        <th style={{ color: "red" }}>Tỷ lệ 1:5.000</th>
                        <th style={{ color: "red" }}>Tỷ lệ 1:10.000</th>
                        <th>Số Giấy chứng nhận đã cấp</th>
                        <th>Diện tích đã cấp Giấy chứng nhận</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        {Array.from({ length: 15 }, (_, index) => index + 1).map(a => {
                            return (<td key={a}>({a})</td>)
                        })}
                    </tr>
                </tbody>
            </table>
        </div>
    )
}
