import React from 'react'
import '../components/index.less'
import HeaderBieuMau from '../components/Header'

export default function Bieu01bKKNLT() {
    return (
        <div className='bieu-mau__layout-wrapper'>
            <HeaderBieuMau maBieu='Biểu số 01b/KKNLT' tenBieu='KIỂM KÊ TÌNH HÌNH QUẢN LÝ, SỬ DỤNG ĐẤT CỦA CÁC CÔNG TY NÔNG, LÂM NGHIỆP' />
            <table className="report-table">
                <thead>
                    <tr>
                        <th rowSpan={2}>STT</th>
                        <th rowSpan={2}>Tên đơn vị sử dụng đất</th>
                        <th rowSpan={2}>Tổng diện tích đang quản lý, sử dụng</th>
                        <th colSpan={2}>
                            Đất đang sử dụng đúng mục đích <i>(DMD)</i>
                        </th>
                        <th rowSpan={2}>
                            Đất sử dụng không đúng mục đích <i>(KDM)</i>
                        </th>
                        <th colSpan={6}>
                            Đất đang giao, giao khoán, khoán trắng, cho thuê, cho mượn, liên doanh, liên kết, hợp tác đầu tư, bị lấn, bị
                            chiếm, đang có tranh chấp
                        </th>
                        <th rowSpan={2}>
                            Đất giao quản lý nhưng chưa sử dụng <i>(DQC)</i>
                        </th>
                    </tr>
                    <tr>
                        <th>Nông nghiệp</th>
                        <th>Phi nông nghiệp</th>
                        <th>Tổng diện tích</th>
                        <th>
                            Đất đang giao, giao khoán, khoán trắng <i>(DGK)</i>
                        </th>
                        <th>
                            Đất đang cho thuê, cho mượn <i>(DCM)</i>
                        </th>
                        <th>
                            Đất đang liên doanh, liên kết, hợp tác đầu tư <i>(DLD)</i>
                        </th>
                        <th>
                            Đất đang bị lấn, bị chiếm <i>(DLC)</i>
                        </th>
                        <th>
                            Đất đang có tranh chấp <i>(DTC)</i>
                        </th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        {Array.from({ length: 13 }, (_, index) => index + 1).map(a => {
                            return (<td key={a}>({a})</td>)
                        })}
                    </tr>
                </tbody>
            </table>
        </div>
    )
}
