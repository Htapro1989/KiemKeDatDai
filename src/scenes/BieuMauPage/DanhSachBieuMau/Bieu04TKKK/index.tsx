import React from 'react'
import '../components/index.less'
import HeaderBieuMau from '../components/Header'
import "../components/ReportTable.less";
import { IBieuMauProps } from '../Bieu01TKKK';
import ReportEmptyData from '../components/ReportEmptyData';
import ReportLoading from '../components/ReportLoading';



export default function Bieu04TKKK(props: IBieuMauProps) {
    const reportData = props.reportData;
    const isFetchingData = props.isFetching
    const reportDataComponent = () => {
        if (!reportData) {
            return null;
        }
        return reportData.data.map((data: any) => {
            return (<tr key={data.id}>
                <td>{data.stt}</td>
                <td>{data.loaiDat}</td>
                <td>{data.ma}</td>

                <td>{data.tongSo_DT}</td>
                <td>{data.tongSo_CC}</td>

                <td>{data.caNhanTrongNuoc_CNV_DT}</td>
                <td>{data.caNhanTrongNuoc_CNV_CC}</td>

                <td>{data.nguoiVietNamONuocNgoai_CNN_DT}</td>
                <td>{data.nguoiVietNamONuocNgoai_CNN_CC}</td>

                <td>{data.coQuanNhaNuoc_TCN_DT}</td>
                <td>{data.coQuanNhaNuoc_TCN_CC}</td>

                <td>{data.donViSuNghiep_TSN_DT}</td>
                <td>{data.donViSuNghiep_TSN_CC}</td>

                <td>{data.toChucXaHoi_TXH_DT}</td>
                <td>{data.toChucXaHoi_TXH_CC}</td>

                <td>{data.toChucKinhTe_TKT_DT}</td>
                <td>{data.toChucKinhTe_TKT_CC}</td>

                <td>{data.toChucKhac_TKH_DT}</td>
                <td>{data.toChucKhac_TKH_CC}</td>

                <td>{data.toChucTonGiao_TTG_DT}</td>
                <td>{data.toChucTonGiao_TTG_CC}</td>

                <td>{data.congDongDanCu_CDS_DT}</td>
                <td>{data.congDongDanCu_CDS_CC}</td>

                <td>{data.toChucNuocNgoai_TNG_DT}</td>
                <td>{data.toChucNuocNgoai_TNG_CC}</td>

                <td>{data.nguoiGocVietNamONuocNgoai_CNN_DT}</td>
                <td>{data.nguoiGocVietNamONuocNgoai_CNN_CC}</td>

                <td>{data.toChucKinhTeVonNuocNgoai_TVN_DT}</td>
                <td>{data.toChucKinhTeVonNuocNgoai_TVN_CC}</td>

                <td>{data.coQuanNhaNuoc_TCQ_DT}</td>
                <td>{data.coQuanNhaNuoc_TCQ_CC}</td>

                <td>{data.donViSuNghiep_TSQ_DT}</td>
                <td>{data.donViSuNghiep_TSQ_CC}</td>

                <td>{data.toChucKinhTe_KTQ_DT}</td>
                <td>{data.toChucKinhTe_KTQ_CC}</td>

                <td>{data.congDongDanCu_CDQ_DT}</td>
                <td>{data.congDongDanCu_CDQ_CC}</td>
            </tr>)
        })
    }
    return (
        <div className='bieu-mau__layout-wrapper'>
            <HeaderBieuMau
                donViBaoCao={reportData}
                maBieu='Biểu 04/TKKK' tenBieu='CƠ CẤU, DIỆN TÍCH THEO LOẠI ĐẤT, ĐỐI TƯỢNG SỬ DỤNG ĐẤT VÀ ĐỐI TƯỢNG ĐƯỢC GIAO QUẢN LÝ ĐẤT' />
            <table className="report-table">
                <thead>
                    <tr>
                        <th rowSpan={4}>Thứ tự</th>
                        <th rowSpan={4}>Loại đất</th>
                        <th rowSpan={4}>Mã</th>
                        <th rowSpan={3} colSpan={2}>Tổng số</th>
                        <th rowSpan={1} colSpan={24} className='vertical-align-center-text'>Cơ cấu, diện tích đất theo đối tượng sử dụng đất</th>
                        <th rowSpan={1} colSpan={8} className='vertical-align-center-text'>Cơ cấu, diện tích theo đối tượng được giao quản lý đất</th>
                    </tr>
                    <tr>
                        <th rowSpan={1} colSpan={4}>Cá nhân trong nước, người Việt Nam định cư ở nước ngoài (CNC)</th>
                        <th rowSpan={1} colSpan={10} className='vertical-align-center-text'>Tổ chức trong nước (TCC)</th>
                        <th rowSpan={2} colSpan={2}>Tổ chức tôn giáo, tổ chức tôn giáo trực thuộc (TTG)</th>
                        <th rowSpan={2} colSpan={2}>Cộng đồng dân cư (CDS)</th>
                        <th rowSpan={2} colSpan={2}>Tổ chức nước ngoài có chức năng ngoại giao (TNG)</th>
                        <th rowSpan={2} colSpan={2}>Người gốc Việt Nam định cư ở nước ngoài (CNN)</th>
                        <th rowSpan={2} colSpan={2}>Tổ chức kinh tế có vốn đầu tư nước ngoài (TVN)</th>
                        <th rowSpan={2} colSpan={2}>Cơ quan nhà nước, cơ quan đảng và đơn vị vũ trang nhân dân (TCQ)</th>
                        <th rowSpan={2} colSpan={2}>Đơn vị sự nghiệp công lập (TSQ)</th>
                        <th rowSpan={2} colSpan={2}>Tổ chức kinh tế (KTQ)</th>
                        <th rowSpan={2} colSpan={2}>Cộng đồng dân cư (CDQ)</th>
                    </tr>
                    <tr>
                        <th rowSpan={1} colSpan={2}>Cá nhân trong nước (CNV)</th>
                        <th rowSpan={1} colSpan={2}>Người Việt Nam định cư ở nước ngoài (CNN)</th>
                        <th rowSpan={1} colSpan={2}>Cơ quan nhà nước, cơ quan đảng và đơn vị vũ trang nhân dân (TCN)</th>
                        <th rowSpan={1} colSpan={2}>Đơn vị sự nghiệp công lập (TSN)</th>
                        <th rowSpan={1} colSpan={2}>Tổ chức xã hội, tổ chức xã hội - nghề nghiệp (TXH)</th>
                        <th rowSpan={1} colSpan={2}>Tổ chức kinh tế (TKT)</th>
                        <th rowSpan={1} colSpan={2}>Tổ chức khác (TKH)</th>
                    </tr>
                    <tr>
                        <th rowSpan={1}>Diện tích (ha)</th>
                        <th rowSpan={1}>Cơ cấu (%)</th>
                        <th rowSpan={1}>Diện tích (ha)</th>
                        <th rowSpan={1}>Cơ cấu (%)</th>
                        <th rowSpan={1}>Diện tích (ha)</th>
                        <th rowSpan={1}>Cơ cấu (%)</th>
                        <th rowSpan={1}>Diện tích (ha)</th>
                        <th rowSpan={1}>Cơ cấu (%)</th>
                        <th rowSpan={1}>Diện tích (ha)</th>
                        <th rowSpan={1}>Cơ cấu (%)</th>
                        <th rowSpan={1}>Diện tích (ha)</th>
                        <th rowSpan={1}>Cơ cấu (%)</th>
                        <th rowSpan={1}>Diện tích (ha)</th>
                        <th rowSpan={1}>Cơ cấu (%)</th>
                        <th rowSpan={1}>Diện tích (ha)</th>
                        <th rowSpan={1}>Cơ cấu (%)</th>
                        <th rowSpan={1}>Diện tích (ha)</th>
                        <th rowSpan={1}>Cơ cấu (%)</th>
                        <th rowSpan={1}>Diện tích (ha)</th>
                        <th rowSpan={1}>Cơ cấu (%)</th>
                        <th rowSpan={1}>Diện tích (ha)</th>
                        <th rowSpan={1}>Cơ cấu (%)</th>
                        <th rowSpan={1}>Diện tích (ha)</th>
                        <th rowSpan={1}>Cơ cấu (%)</th>
                        <th rowSpan={1}>Diện tích (ha)</th>
                        <th rowSpan={1}>Cơ cấu (%)</th>
                        <th rowSpan={1}>Diện tích (ha)</th>
                        <th rowSpan={1}>Cơ cấu (%)</th>
                        <th rowSpan={1}>Diện tích (ha)</th>
                        <th rowSpan={1}>Cơ cấu (%)</th>
                        <th rowSpan={1}>Diện tích (ha)</th>
                        <th rowSpan={1}>Cơ cấu (%)</th>
                        <th rowSpan={1}>Diện tích (ha)</th>
                        <th rowSpan={1}>Cơ cấu (%)</th>
                    </tr>

                </thead>
                <tbody>
                    <tr>
                        {Array.from({ length: 37 }, (_, index) => index + 1).map(a => {
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
