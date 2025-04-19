import '../components/index.less'
import './style.less'
import React from 'react';
import HeaderBieuMau from '../components/Header';
import { IBieuMauProps } from '../Bieu01TKKK';
import ReportLoading from '../components/ReportLoading';
import ReportEmptyData from '../components/ReportEmptyData';
import utils from '../../../../utils/utils';


export default function Bieu06TKKKQPAN(props: IBieuMauProps) {

    const reportData = props.reportData;
    const isFetchingData = props.isFetching

    const reportDataComponent = () => {
        if (!reportData) {
            return null;
        }
        return reportData.data.map((data: any) => {
            return (<tr key={data.id}>
                <td>{utils.convertBieuDataViewer(data.stt)}</td>
                <td>{utils.convertBieuDataViewer(data.donVi)}</td>
                <td>{utils.convertBieuDataViewer(data.diaChi)}</td>
                <td>{utils.convertBieuDataViewer(data.dienTichDatQuocPhong)}</td>
                <td>{utils.convertBieuDataViewer(data.dienTichKetHopKhac)}</td>
                <td>{utils.convertBieuDataViewer(data.loaiDatKetHopKhac)}</td>
                <td>{utils.convertBieuDataViewer(data.dienTichDaDoDac)}</td>
                <td>{utils.convertBieuDataViewer(data.soGCNDaCap)}</td>
                <td>{utils.convertBieuDataViewer(data.dienTichDaCapGCN)}</td>
                <td>{utils.convertBieuDataViewer(data.ghiChu)}</td>
            </tr>)
        })
    }

    return (
        <div className='bieu-mau__layout-wrapper'>
            <HeaderBieuMau maBieu='Biểu 06/TKKKQPAN' tenBieu='THỐNG KÊ, KIỂM KÊ ĐẤT QUỐC PHÒNG, ĐẤT AN NINH' />
            <table className="report-table">
                <thead>
                    <tr>
                        <th rowSpan={2}>Thứ tự</th>
                        <th rowSpan={2}>Đơn vị (hoặc điểm) sử dụng đất</th>
                        <th rowSpan={2}>Địa chỉ sử dụng đất</th>
                        <th rowSpan={2}>Diện tích đất quốc phòng/đất an ninh</th>
                        <th rowSpan={1} colSpan={2}>Trong đó diện tích kết hợp vào mục đích khác</th>
                        <th rowSpan={1} colSpan={3}>Tình hình đo đạc lập bản đồ địa chính hoặc trích đo địa chính, cấp Giấy chứng nhận</th>
                        <th rowSpan={2}>Ghi chú</th>
                    </tr>
                    <tr>
                    <th rowSpan={1}>Diện tích</th>
                    <th rowSpan={1}>Loại đất kết hợp</th>
                    <th rowSpan={1}>Diện tích đã đo đạc	</th>
                    <th rowSpan={1}>Số Giấy chứng nhận đã cấp</th>
                    <th rowSpan={1}>Diện tích đã cấp giấy chứng nhận</th>
                    </tr>

                </thead>
                <tbody>
                    <tr>
                        {Array.from({ length: 10 }, (_, index) => index + 1).map(a => {
                            return (<td key={a}>({a})</td>)
                        })}
                    </tr>
                    {reportDataComponent()}
                </tbody>
            </table>
            <ReportEmptyData isEmpty={(!reportData || reportData?.data?.length <= 0) && !isFetchingData} />
            <ReportLoading isLoading={isFetchingData} />
        </div>

    );
};

