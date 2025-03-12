import React from 'react'
import '../components/index.less'
import HeaderBieuMau from '../components/Header'

export default function Bieu01aKKNLT() {
    return (
        <div className='bieu-mau__layout-wrapper'>
            <HeaderBieuMau maBieu='Biểu số 01a/KKNLT' tenBieu='KIỂM KÊ HÌNH QUẢN LÝ, SỬ DỤNG ĐẤT CỦA CÁC CÔNG TY NÔNG, LÂM NGHIỆP' />
            <table className="report-table">
                <thead>
                    <tr>
                        <th rowSpan={3}>TT</th>
                        <th rowSpan={3}>Tên đơn vị sử dụng đất</th>
                        <th rowSpan={3}>Tổng diện tích đang quản lý, sử dụng</th>
                        <th colSpan={9}>Đất nông nghiệp</th>
                        <th colSpan={7}>Đất phi nông nghiệp</th>
                        <th rowSpan={3}>Diện tích đất chưa sử dụng</th>
                    </tr>
                    <tr>
                        <th rowSpan={2}>Tổng số</th>
                        <th rowSpan={2}>Đất trồng lúa</th>
                        <th rowSpan={2}>Đất trồng cây hàng năm khác</th>
                        <th rowSpan={2}>Đất trồng cây lâu năm</th>
                        <th colSpan={3}>Đất lâm nghiệp</th>
                        <th rowSpan={2}>Đất nuôi trồng thủy sản</th>
                        <th rowSpan={2}>Các loại đất nông nghiệp khác còn lại</th>
                        <th rowSpan={2}>Tổng số</th>
                        <th rowSpan={2}>Đất ở</th>
                        <th rowSpan={2}>Đất sản xuất, kinh doanh phi nông nghiệp</th>
                        <th rowSpan={2}>Đất sử dụng vào mục đích công cộng</th>
                        <th rowSpan={2}>
                            Đất nghĩa trang, nhà tang lễ, cơ sở hỏa táng; đất cơ sở lưu trữ tro cốt
                        </th>
                        <th rowSpan={2}>Đất có mặt nước chuyên dùng</th>
                        <th rowSpan={2}>Các loại đất phi nông nghiệp khác còn lại</th>
                    </tr>
                    <tr>
                        <th>Đất rừng đặc dụng</th>
                        <th>Đất rừng phòng hộ</th>
                        <th>Đất rừng sản xuất</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        {Array.from({ length: 20 }, (_, index) => index + 1).map(a => {
                            return (<td key={a}>({a})</td>)
                        })}
                    </tr>
                </tbody>
            </table>
        </div>
    )
}
