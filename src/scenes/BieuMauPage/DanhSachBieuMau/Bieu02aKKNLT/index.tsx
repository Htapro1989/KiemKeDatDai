import React from 'react'
import '../components/index.less'
import HeaderBieuMau from '../components/Header'
import { IBieuMauProps } from '../Bieu01TKKK'
import utils from '../../../../utils/utils';
import ReportEmptyData from '../components/ReportEmptyData';
import ReportLoading from '../components/ReportLoading';

export default function Bieu02aKKNLT(props: IBieuMauProps) {
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
                <td>{utils.convertBieuDataViewer(data.tenDonVi)}</td>
                <td>{utils.convertBieuDataViewer(data.dienTichTheoQDGiaoThue)}</td>
                <td>{utils.convertBieuDataViewer(data.dienTichChoThueDat)}</td>
                <td>{utils.convertBieuDataViewer(data.dienTichChuaXacDinhGiaoThue)}</td>
                <td>{utils.convertBieuDataViewer(data.dienTichDoDacTL1000)}</td>
                <td>{utils.convertBieuDataViewer(data.dienTichDoDacTL2000)}</td>
                <td>{utils.convertBieuDataViewer(data.dienTichDoDacTL5000)}</td>
                <td>{utils.convertBieuDataViewer(data.dienTichDoDacTL10000)}</td>
                <td>{utils.convertBieuDataViewer(data.soGCNDaCap)}</td>
                <td>{utils.convertBieuDataViewer(data.dienTichGCNDaCap)}</td>
                <td>{utils.convertBieuDataViewer(data.dienTichDaBanGiao)}</td>
                <td>{utils.convertBieuDataViewer(data.ghiChu)}</td>
            </tr>)
        })
    }

    return (
        <div className='bieu-mau__layout-wrapper'>
            <HeaderBieuMau maBieu='Biểu số 02a/KKNLT' tenBieu='KIỂM KÊ TÌNH HÌNH ĐO ĐẠC, CẤP GIẤY CHỨNG NHẬN VÀ HÌNH THỨC SỬ DỤNG ĐẤT CỦA CÁC CÔNG TY NÔNG, LÂM NGHIỆP' />
            <table className="report-table">
                <thead>
                    <tr>
                        <th>STT</th>
                        <th>Tên đơn vị</th>
                        <th>Diện tích theo QD giao thuê</th>
                        <th>Diện tích cho thuê đất giao đất</th>
                        <th>Diện tích chưa xác định giao thuê</th>
                        <th>Diện tích đo đạc TL1000</th>
                        <th>Diện tích đo đạc TL2000</th>
                        <th>Diện tích đo đạc TL5000</th>
                        <th>Diện tích đo đạc TL10000</th>
                        <th>Số GCN đã cấp</th>
                        <th>Diện tích GCN đã cấp</th>
                        <th>Diện tích đã bàn giao</th>
                        <th>Ghi chú</th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        {Array.from({ length: 13 }, (_, index) => index + 1).map(a => {
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
