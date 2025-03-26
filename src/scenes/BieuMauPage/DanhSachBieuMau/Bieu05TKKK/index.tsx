import React from 'react'
import '../components/index.less'
import HeaderBieuMau from '../components/Header'
import { IBieuMauProps } from '../Bieu01TKKK'
import ReportEmptyData from '../components/ReportEmptyData';
import ReportLoading from '../components/ReportLoading';
import utils from '../../../../utils/utils';


export default function Bieu05TKKK(props: IBieuMauProps) {
    const reportData = props.reportData;
    const isFetchingData = props.isFetching

    const reportDataComponent = () => {
        if (!reportData) {
            return null;
        }
        return reportData.data.map((data: any, index: any) => {
            return (<tr key={data.id}>
                <td>{utils.convertBieuDataViewer(index + 1)}</td>
                <td style={{ minWidth: 250, textAlign: 'left' }}>{utils.convertBieuDataViewer(data.loaiDat)}</td>
                <td>{utils.convertBieuDataViewer(data.ma)}</td>
                <td>{utils.convertBieuDataViewer(data.nam)}</td>
                <td>{utils.convertBieuDataViewer(data.lua)}</td>
                <td>{utils.convertBieuDataViewer(data.hnk)}</td>
                <td>{utils.convertBieuDataViewer(data.cln)}</td>
                <td>{utils.convertBieuDataViewer(data.rdd)}</td>
                <td>{utils.convertBieuDataViewer(data.rph)}</td>
                <td>{utils.convertBieuDataViewer(data.rsx)}</td>
                <td>{utils.convertBieuDataViewer(data.nts)}</td>
                <td>{utils.convertBieuDataViewer(data.cnt)}</td>
                <td>{utils.convertBieuDataViewer(data.lmu)}</td>
                <td>{utils.convertBieuDataViewer(data.nkh)}</td>
                <td>{utils.convertBieuDataViewer(data.ont)}</td>
                <td>{utils.convertBieuDataViewer(data.odt)}</td>
                <td>{utils.convertBieuDataViewer(data.tsc)}</td>
                <td>{utils.convertBieuDataViewer(data.cqp)}</td>
                <td>{utils.convertBieuDataViewer(data.can)}</td>
                <td>{utils.convertBieuDataViewer(data.dvh)}</td>
                <td>{utils.convertBieuDataViewer(data.dxh)}</td>
                <td>{utils.convertBieuDataViewer(data.dyt)}</td>
                <td>{utils.convertBieuDataViewer(data.dgd)}</td>
                <td>{utils.convertBieuDataViewer(data.dtt)}</td>
                <td>{utils.convertBieuDataViewer(data.dkh)}</td>
                <td>{utils.convertBieuDataViewer(data.dmt)}</td>
                <td>{utils.convertBieuDataViewer(data.dkt)}</td>
                <td>{utils.convertBieuDataViewer(data.dng)}</td>
                <td>{utils.convertBieuDataViewer(data.dsk)}</td>
                <td>{utils.convertBieuDataViewer(data.skk)}</td>
                <td>{utils.convertBieuDataViewer(data.skn)}</td>
                <td>{utils.convertBieuDataViewer(data.sct)}</td>
                <td>{utils.convertBieuDataViewer(data.tmd)}</td>
                <td>{utils.convertBieuDataViewer(data.skc)}</td>
                <td>{utils.convertBieuDataViewer(data.sks)}</td>
                <td>{utils.convertBieuDataViewer(data.dgt)}</td>
                <td>{utils.convertBieuDataViewer(data.dtl)}</td>
                <td>{utils.convertBieuDataViewer(data.dct)}</td>
                <td>{utils.convertBieuDataViewer(data.dpc)}</td>
                <td>{utils.convertBieuDataViewer(data.ddd)}</td>
                <td>{utils.convertBieuDataViewer(data.dra)}</td>
                <td>{utils.convertBieuDataViewer(data.dnl)}</td>
                <td>{utils.convertBieuDataViewer(data.dbv)}</td>
                <td>{utils.convertBieuDataViewer(data.dch)}</td>
                <td>{utils.convertBieuDataViewer(data.dkv)}</td>
                <td>{utils.convertBieuDataViewer(data.ton)}</td>
                <td>{utils.convertBieuDataViewer(data.tin)}</td>
                <td>{utils.convertBieuDataViewer(data.ntd)}</td>
                <td>{utils.convertBieuDataViewer(data.mnc)}</td>
                <td>{utils.convertBieuDataViewer(data.son)}</td>
                <td>{utils.convertBieuDataViewer(data.pnk)}</td>
                <td>{utils.convertBieuDataViewer(data.cgt)}</td>
                <td>{utils.convertBieuDataViewer(data.bcs)}</td>
                <td>{utils.convertBieuDataViewer(data.dcs)}</td>
                <td>{utils.convertBieuDataViewer(data.ncs)}</td>
                <td>{utils.convertBieuDataViewer(data.mcs)}</td>
                <td>{utils.convertBieuDataViewer(data.giamKhac)}</td>

            </tr>)
        })
    }
    return (
        <div className='bieu-mau__layout-wrapper'>
            <HeaderBieuMau donViBaoCao={reportData} maBieu='Biểu 05/TKKK' tenBieu='CHU CHUYỂN DIỆN TÍCH CỦA CÁC LOẠI ĐẤT' />
            <table className="report-table">
                <thead>
                    <tr>
                        <th>STT</th>
                        <th>Loại đất </th>
                        <th>Mã</th>
                        <th>Năm <span key="tuNgay">......</span></th>
                        <th>LUA</th>
                        <th>HNK</th>
                        <th>CLN</th>
                        <th>RDD</th>
                        <th>RPH</th>
                        <th>RSX</th>
                        <th>NTS</th>
                        <th>CNT</th>
                        <th>LMU</th>
                        <th>NKH</th>
                        <th>ONT</th>
                        <th>ODT</th>
                        <th>TSC</th>
                        <th>CQP</th>
                        <th>CAN</th>
                        <th>DVH</th>
                        <th>DXH</th>
                        <th>DYT</th>
                        <th>DGD</th>
                        <th>DTT</th>
                        <th>DKH</th>
                        <th>DMT</th>
                        <th>DKT</th>
                        <th>DNG</th>
                        <th>DSK</th>
                        <th>SKK</th>
                        <th>SKN</th>
                        <th>SCT</th>
                        <th>TMD</th>
                        <th>SKC</th>
                        <th>SKS</th>
                        <th>DGT</th>
                        <th>DTL</th>
                        <th>DCT</th>
                        <th>DPC</th>
                        <th>DDD</th>
                        <th>DRA</th>
                        <th>DNL</th>
                        <th>DBV</th>
                        <th>DCH</th>
                        <th>DKV</th>
                        <th>TON</th>
                        <th>TIN</th>
                        <th>NTD</th>
                        <th>MNC</th>
                        <th>SON</th>
                        <th>PNK</th>
                        <th>CGT</th>
                        <th>BCS</th>
                        <th>DCS</th>
                        <th>NCS</th>
                        <th>MCS</th>
                        <td><b>Giảm khác</b> <i>(2)</i></td>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        {Array.from({ length: 57 }, (_, index) => index + 1).map(a => {
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
