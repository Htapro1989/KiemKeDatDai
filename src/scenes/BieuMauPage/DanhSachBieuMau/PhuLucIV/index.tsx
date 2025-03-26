import React from 'react'
import '../components/index.less'
import HeaderBieuMau from '../components/Header'
import { IBieuMauProps } from '../Bieu01TKKK'
import ReportEmptyData from '../components/ReportEmptyData';
import ReportLoading from '../components/ReportLoading';
import utils from '../../../../utils/utils';


export default function PhuLucIV(props: IBieuMauProps) {
    const reportData = props.reportData;
    const isFetchingData = props.isFetching
    const reportDataComponent = () => {
        const tableData = reportData?.data?.bieuPhuLucIVDtos
        if (!tableData) {
            return null;
        }

        return tableData.map((data: any, index: any) => {
            return (<tr key={index}>
                <td>{utils.convertBieuDataViewer(data.stt)}</td>
                <td>{utils.convertBieuDataViewer(data.dienTich)}</td>
                <td>{utils.convertBieuDataViewer(data.maLoaiDatHienTrang)}</td>
                <td>{utils.convertBieuDataViewer(data.maLoaiDatKyTruoc)}</td>
                <td>{utils.convertBieuDataViewer(data.maLoaiDatSuDungKetHop)}</td>
                <td>{utils.convertBieuDataViewer(data.maDoiTuongHienTrang)}</td>
                <td>{utils.convertBieuDataViewer(data.maDoiTuongKyTruoc)}</td>
                <td>{utils.convertBieuDataViewer(data.maKhuVucTongHop)}</td>
                <td>{data.ghiChu}</td>

            </tr>)
        })
    }

    return (
        <div className='bieu-mau__layout-wrapper'>
            <HeaderBieuMau maBieu='Phụ lục IV' tenBieu='DANH SÁCH CÁC TRƯỜNG HỢP BIẾN ĐỘNG TRONG NĂM THỐNG KÊ ĐẤT ĐAI VÀ KỲ KIỂM KÊ ĐẤT ĐAI' />
            <table className="report-table">
                <thead>
                    <tr>
                        <th rowSpan={1} colSpan={9} className='vertical-align-center-text'>Thông tin do Cơ quan có chức năng quản lý đất đai cấp huyện/Văn phòng Đăng ký đất đai xác định</th>
                        <th rowSpan={1} colSpan={2} className='vertical-align-center-text'>Thông tin khoanh đất</th>
                        <th rowSpan={1} colSpan={1} className='vertical-align-center-text'>Kết quả kiểm tra thực địa của cấp xã</th>
                    </tr>
                    <tr>
                        <th rowSpan={1} colSpan={2} className='vertical-align-center-text'> Số hiệu thửa đất</th>
                        <th rowSpan={2} colSpan={1} className='vertical-align-center-text'> Tên người sử dụng đất</th>
                        <th rowSpan={2} colSpan={1} className='vertical-align-center-text'>Địa chỉ thửa đất</th>
                        <th rowSpan={2} colSpan={1} className='vertical-align-center-text'>Diện tích có biến động (m2)</th>
                        <th rowSpan={1} colSpan={2} className='vertical-align-center-text'>Mã loại đất</th>
                        <th rowSpan={1} colSpan={2} className='vertical-align-center-text'>Mã loại đối tượng</th>
                        <th rowSpan={2} colSpan={1} className='vertical-align-center-text'>Trước biến động</th>
                        <th rowSpan={2} colSpan={1} className='vertical-align-center-text'>Sau biến động </th>
                        <th rowSpan={2} colSpan={1} className='vertical-align-center-text'>Nội dung thay đổi</th>
                    </tr>
                    <tr>
                        <th rowSpan={1} className='vertical-align-center-text'>Trước biến động</th>
                        <th rowSpan={1} className='vertical-align-center-text'>Sau biến động </th>
                        <th rowSpan={1} className='vertical-align-center-text'>Trước biến động</th>
                        <th rowSpan={1} className='vertical-align-center-text'>Sau biến động </th>
                        <th rowSpan={1} className='vertical-align-center-text'>Trước biến động</th>
                        <th rowSpan={1} className='vertical-align-center-text'>Sau biến động </th>

                    </tr>
                </thead>
                <tbody>
                    <tr>
                        {Array.from({ length: 12 }, (_, index) => index + 1).map(a => {
                            return (<td key={a}>({a})</td>)
                        })}
                    </tr>
                    {reportDataComponent()}
                </tbody>
            </table>
            <ReportEmptyData isEmpty={(!reportData || reportData?.data?.bieuPhuLucIVDtos?.length <= 0) && !isFetchingData} />
            <ReportLoading isLoading={isFetchingData} />
        </div>
    )
}
