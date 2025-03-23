import React from 'react'
import '../components/index.less'
import HeaderBieuMau from '../components/Header'
import { IBieuMauProps } from '../Bieu01TKKK';
import ReportEmptyData from '../components/ReportEmptyData';
import ReportLoading from '../components/ReportLoading';


export default function Bieu03TKKK(props: IBieuMauProps) {
    const reportData = props.reportData;
    const isFetchingData = props.isFetching
    const reportDataComponent = () => {
        if (!reportData) {
            return null;
        }
        return reportData.data.map((data: any) => {
            const genItem = () => {
                try {
                    const listDienTich = JSON.parse(data.dienTichTheoDVHC)
                    if (listDienTich.length > 0) {
                        return (<>
                            {listDienTich.map((e: any) => {
                                return (<td>{e.DienTich}</td>)
                            })}
                        </>)
                    }
                } catch (error) {
                    return null;
                }
                return null;
            }

            return (<tr key={data.id}>
                <td>{data.stt}</td>
                <td>{data.loaiDat}</td>
                <td>{data.ma}</td>
                <td>{data.tongDienTich}</td>
                {genItem()}
            </tr>)
        })
    }

    const genReportHeader = () => {
        if (!reportData?.data) {
            return null;
        }
        try {
            const listDienTich = JSON.parse(reportData.data[0].dienTichTheoDVHC)
            if (listDienTich.length > 0) {
                return (<tr>
                    {listDienTich.map((e: any) => {
                        return (<th className='vertical-align-center-text'>{e.TenDVHC}</th>)
                    })}
                </tr>)
            }
        } catch (error) {
            console.log(error)
            return null;
        }
        return null;
    }
    const genReportIndex = () => {
        if (!reportData?.data) {
            return null;
        }
        try {
            const listDienTich = JSON.parse(reportData.data[0].dienTichTheoDVHC)
            if (listDienTich.length > 0) {
                return (<>
                    {listDienTich.map((e: any, index: any) => {
                        return (<td>({4 + index + 1})</td>)
                    })}
                </>)
            }
        } catch (error) {
            console.log(error)
            return null;
        }
        return null;
    }

    return (
        <div className='bieu-mau__layout-wrapper'>
            <HeaderBieuMau
                donViBaoCao={reportData}
                maBieu='Biểu 03/TKKK'
                tenBieu='THỐNG KÊ, KIỂM KÊ DIỆN TÍCH ĐẤT ĐAI THEO ĐƠN VỊ HÀNH CHÍNH' />
            <table className="report-table">
                <thead>
                    <tr>
                        <th rowSpan={2} className='vertical-align-center-text'>Thứ tự</th>
                        <th rowSpan={2} className='vertical-align-center-text'>Loại đất</th>
                        <th rowSpan={2} className='vertical-align-center-text'>Mã</th>
                        <th rowSpan={2} className='vertical-align-center-text'>Tổng diện tích</th>
                        <th rowSpan={1} className='vertical-align-center-text'>Diện tích theo từng đơn vị hành chính trực thuộc</th>
                    </tr>
                    {genReportHeader()}

                </thead>
                <tbody>
                    <tr>
                        <td>(1)</td>
                        <td>(2)</td>
                        <td>(3)</td>
                        <td>(4)</td>
                        {genReportIndex()}
                    </tr>
                    {reportDataComponent()}
                </tbody>
            </table>
            <ReportEmptyData isEmpty={(!reportData || reportData?.data?.length <= 0) && !isFetchingData} />
            <ReportLoading isLoading={isFetchingData} />
        </div>
    )
}
