import React from 'react'
import '../components/index.less'
import HeaderBieuMau from '../components/Header'
import ReportEmptyData from '../components/ReportEmptyData';
import ReportLoading from '../components/ReportLoading';
import { IBieuMauProps } from '../Bieu01TKKK';
import utils from '../../../../utils/utils';

export default function Bieu02TKKK(props: IBieuMauProps) {
    const reportData = props.reportData;
    const isFetchingData = props.isFetching
    const reportDataComponent = () => {
        if (!reportData) {
            return null;
        }
        return reportData.data.map((data: any) => {
            return (<tr key={data.id}>
                <td>{utils.convertBieuDataViewer(data.stt)}</td>
                <td>{utils.convertBieuDataViewer(data.loaiDat)}</td>
                <td>{utils.convertBieuDataViewer(data.ma)}</td>
                <td>{utils.convertBieuDataViewer(data.tongSo)}</td>
                <td>{utils.convertBieuDataViewer(data.caNhanTrongNuoc_CNV)}</td>
                <td>{utils.convertBieuDataViewer(data.nguoiVietNamONuocNgoai_CNN)}</td>
                <td>{utils.convertBieuDataViewer(data.coQuanNhaNuoc_TCN)}</td>
                <td>{utils.convertBieuDataViewer(data.donViSuNghiep_TSN)}</td>
                <td>{utils.convertBieuDataViewer(data.toChucXaHoi_TXH)}</td>
                <td>{utils.convertBieuDataViewer(data.toChucKinhTe_TKT)}</td>
                <td>{utils.convertBieuDataViewer(data.toChucKhac_TKH)}</td>
                <td>{utils.convertBieuDataViewer(data.toChucTonGiao_TTG)}</td>
                <td>{utils.convertBieuDataViewer(data.congDongDanCu_CDS)}</td>
                <td>{utils.convertBieuDataViewer(data.toChucNuocNgoai_TNG)}</td>
                <td>{utils.convertBieuDataViewer(data.nguoiVietNamONuocNgoai_CNN)}</td>
                <td>{utils.convertBieuDataViewer(data.toChucKinhTeVonNuocNgoai_TVN)}</td>
                <td>{utils.convertBieuDataViewer(data.coQuanNhaNuoc_TCQ)}</td>
                <td>{utils.convertBieuDataViewer(data.donViSuNghiep_TSQ)}</td>
                <td>{utils.convertBieuDataViewer(data.toChucKinhTe_KTQ)}</td>
                <td>{utils.convertBieuDataViewer(data.congDongDanCu_CDQ)}</td>
            </tr>)
        })
    }


    return (
        <div className='bieu-mau__layout-wrapper'>
            <HeaderBieuMau
                donViBaoCao={reportData}
                maBieu='Biểu 02/TKKK'
                tenBieu='THỐNG KÊ, KIỂM KÊ ĐỐI TƯỢNG SỬ DỤNG ĐẤT VÀ ĐỐI TƯỢNG ĐƯỢC GIAO QUẢN LÝ ĐẤT' />
            <table className="report-table">
                <thead>
                    <tr>
                        <th rowSpan={3}>Thứ tự</th>
                        <th rowSpan={3}>Loại đất</th>
                        <th rowSpan={3}>Mã</th>
                        <th rowSpan={3}>Tổng số</th>
                        <th rowSpan={1} colSpan={13} className='vertical-align-center-text'>Số lượng người sử dụng đất</th>
                        <th rowSpan={1} colSpan={3} className='vertical-align-center-text'>Số lượng người được giao quản lý đất</th>
                    </tr>
                    <tr>
                        <th rowSpan={1} colSpan={2} className='vertical-align-center-text'>Cá nhân trong nước, người Việt Nam định cư ở nước ngoài (CNC)</th>
                        <th rowSpan={1} colSpan={5} className='vertical-align-center-text'>Tổ chức trong nước (TCC)</th>
                        <th rowSpan={2}>Tổ chức tôn giáo, tổ chức tôn giáo trực thuộc (TTG)</th>
                        <th rowSpan={2}>Cộng đồng dân cư (CDS)</th>
                        <th rowSpan={2}>Tổ chức nước ngoài có chức năng ngoại giao (TNG)</th>
                        <th rowSpan={2}>Người gốc Việt Nam định cư ở nước ngoài (CNN)</th>
                        <th rowSpan={2}>Tổ chức kinh tế có vốn đầu tư nước ngoài (TVN)</th>
                        <th rowSpan={2}>Cơ quan nhà nước, cơ quan đảng và đơn vị vũ trang nhân dân (TCQ)</th>
                        <th rowSpan={2}>Đơn vị sự nghiệp công lập (TSQ)</th>
                        <th rowSpan={2}>Tổ chức kinh tế (KTQ)</th>
                        <th rowSpan={2}>Cộng đồng dân cư (CDQ)</th>
                    </tr>
                    <tr>
                        <th rowSpan={1}>Cá nhân trong nước (CNV)</th>
                        <th rowSpan={1}>Người Việt Nam định cư ở nước ngoài (CNN)</th>
                        <th rowSpan={1}>Cơ quan nhà nước, cơ quan đảng và đơn vị vũ trang nhân dân (TCN)</th>
                        <th rowSpan={1}>Đơn vị sự nghiệp công lập (TSN)	</th>
                        <th rowSpan={1}>Tổ chức xã hội, tổ chức xã hội - nghề nghiệp (TXH)</th>
                        <th rowSpan={1}>Tổ chức kinh tế (TKT)</th>
                        <th rowSpan={1}>Tổ chức khác (TKH)</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>(1)</td>
                        <td>(2)</td>
                        <td>(3)</td>
                        <td>(4)=(5)+...</td>
                        <td>(5)</td>
                        <td>(6)</td>
                        <td>(7)</td>
                        <td>(8)</td>
                        <td>(9)</td>
                        <td>(10)</td>
                        <td>(11)</td>
                        <td>(12)</td>
                        <td>(13)</td>
                        <td>(14)</td>
                        <td>(15)</td>
                        <td>(16)</td>
                        <td>(17)</td>
                        <td>(18)</td>
                        <td>(19)</td>
                        <td>(20)</td>
                    </tr>
                    {reportDataComponent()}
                </tbody>
            </table>
            <ReportEmptyData isEmpty={(!reportData || reportData?.data?.length <= 0) && !isFetchingData} />
            <ReportLoading isLoading={isFetchingData} />
        </div>
    )
}
