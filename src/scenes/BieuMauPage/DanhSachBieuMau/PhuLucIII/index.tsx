import React from 'react'
import '../components/index.less'
import HeaderBieuMau from '../components/Header'
import { IBieuMauProps } from '../Bieu01TKKK'
import ReportEmptyData from '../components/ReportEmptyData';
import ReportLoading from '../components/ReportLoading';
import utils from '../../../../utils/utils';

export default function PhuLucIII(props: IBieuMauProps) {
    const reportData = props.reportData;
    const isFetchingData = props.isFetching

    const reportDataComponent = () => {
        const tableData = reportData?.data
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
            <HeaderBieuMau
                donViBaoCao={reportData}
                maBieu='Phụ lục III'
                tenBieu='DANH SÁCH CÁC KHOANH ĐẤT THỐNG KÊ, KIỂM KÊ ĐẤT ĐAI' />
            <table className="report-table">
                <thead>
                    <tr>
                        <th rowSpan={2} className='vertical-align-center-text'>Thứ tự khoanh đất</th>
                        <th rowSpan={2} className='vertical-align-center-text'>Diện tích (m2)</th>
                        <th rowSpan={1} colSpan={2} className='vertical-align-center-text'>Mã loại đất</th>
                        <th rowSpan={2} className='vertical-align-center-text'>Mã loại đất sử dụng kết hợp </th>
                        <th rowSpan={1} colSpan={2} className='vertical-align-center-text'>Mã đối tượng</th>
                        <th rowSpan={2} className='vertical-align-center-text'>Mã khu vực tổng hợp</th>
                        <th rowSpan={2} className='vertical-align-center-text'>Ghi chú</th>
                    </tr>
                    <tr>
                        <th rowSpan={1} className='vertical-align-center-text'>Hiện trạng</th>
                        <th rowSpan={1} className='vertical-align-center-text'>Kỳ trước</th>

                        <th rowSpan={1} className='vertical-align-center-text'>Hiện trạng</th>
                        <th rowSpan={1} className='vertical-align-center-text'>Kỳ trước</th>

                    </tr>
                </thead>
                <tbody>
                    <tr>
                        {Array.from({ length: 9 }, (_, index) => index + 1).map(a => {
                            return (<td key={a}>({a})</td>)
                        })}
                    </tr>
                    {reportDataComponent()}
                </tbody>
            </table>
            <ReportEmptyData isEmpty={(!reportData || reportData?.data?.length <= 0) && !isFetchingData} />
            <ReportLoading isLoading={isFetchingData} />
        </div>
    )
}
