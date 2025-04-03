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

    const genBackgroundColor = (data: any, name: string) => {
        return {
            backgroundColor: data?.ma == name ? 'yellow' : '#ffffff',
        }
    }

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
                <td style={genBackgroundColor(data, 'LUA')}>{utils.convertBieuDataViewer(data.lua)}</td>
                <td style={genBackgroundColor(data, 'HNK')}>{utils.convertBieuDataViewer(data.hnk)}</td>
                <td style={genBackgroundColor(data, 'CLN')}>{utils.convertBieuDataViewer(data.cln)}</td>
                <td style={genBackgroundColor(data, 'RDD')}>{utils.convertBieuDataViewer(data.rdd)}</td>
                <td style={genBackgroundColor(data, 'RPH')}>{utils.convertBieuDataViewer(data.rph)}</td>
                <td style={genBackgroundColor(data, 'RSX')}>{utils.convertBieuDataViewer(data.rsx)}</td>
                <td style={genBackgroundColor(data, 'NTS')}>{utils.convertBieuDataViewer(data.nts)}</td>
                <td style={genBackgroundColor(data, 'CNT')}>{utils.convertBieuDataViewer(data.cnt)}</td>
                <td style={genBackgroundColor(data, 'LMU')}>{utils.convertBieuDataViewer(data.lmu)}</td>
                <td style={genBackgroundColor(data, 'NKH')}>{utils.convertBieuDataViewer(data.nkh)}</td>
                <td style={genBackgroundColor(data, 'ONT')}>{utils.convertBieuDataViewer(data.ont)}</td>
                <td style={genBackgroundColor(data, 'ODT')}>{utils.convertBieuDataViewer(data.odt)}</td>
                <td style={genBackgroundColor(data, 'TSC')}>{utils.convertBieuDataViewer(data.tsc)}</td>
                <td style={genBackgroundColor(data, 'CQP')}>{utils.convertBieuDataViewer(data.cqp)}</td>
                <td style={genBackgroundColor(data, 'CAN')}>{utils.convertBieuDataViewer(data.can)}</td>
                <td style={genBackgroundColor(data, 'DVH')}>{utils.convertBieuDataViewer(data.dvh)}</td>
                <td style={genBackgroundColor(data, 'DXH')}>{utils.convertBieuDataViewer(data.dxh)}</td>
                <td style={genBackgroundColor(data, 'DYT')}>{utils.convertBieuDataViewer(data.dyt)}</td>
                <td style={genBackgroundColor(data, 'DGD')}>{utils.convertBieuDataViewer(data.dgd)}</td>
                <td style={genBackgroundColor(data, 'DTT')}>{utils.convertBieuDataViewer(data.dtt)}</td>
                <td style={genBackgroundColor(data, 'DKH')}>{utils.convertBieuDataViewer(data.dkh)}</td>
                <td style={genBackgroundColor(data, 'DMT')}>{utils.convertBieuDataViewer(data.dmt)}</td>
                <td style={genBackgroundColor(data, 'DKT')}>{utils.convertBieuDataViewer(data.dkt)}</td>
                <td style={genBackgroundColor(data, 'DNG')}>{utils.convertBieuDataViewer(data.dng)}</td>
                <td style={genBackgroundColor(data, 'DSK')}>{utils.convertBieuDataViewer(data.dsk)}</td>
                <td style={genBackgroundColor(data, 'SKK')}>{utils.convertBieuDataViewer(data.skk)}</td>
                <td style={genBackgroundColor(data, 'SKN')}>{utils.convertBieuDataViewer(data.skn)}</td>
                <td style={genBackgroundColor(data, 'SCT')}>{utils.convertBieuDataViewer(data.sct)}</td>
                <td style={genBackgroundColor(data, 'TMD')}>{utils.convertBieuDataViewer(data.tmd)}</td>
                <td style={genBackgroundColor(data, 'SKC')}>{utils.convertBieuDataViewer(data.skc)}</td>
                <td style={genBackgroundColor(data, 'SKS')}>{utils.convertBieuDataViewer(data.sks)}</td>
                <td style={genBackgroundColor(data, 'DGT')}>{utils.convertBieuDataViewer(data.dgt)}</td>
                <td style={genBackgroundColor(data, 'DTL')}>{utils.convertBieuDataViewer(data.dtl)}</td>
                <td style={genBackgroundColor(data, 'DCT')}>{utils.convertBieuDataViewer(data.dct)}</td>
                <td style={genBackgroundColor(data, 'DPC')}>{utils.convertBieuDataViewer(data.dpc)}</td>
                <td style={genBackgroundColor(data, 'DDD')}>{utils.convertBieuDataViewer(data.ddd)}</td>
                <td style={genBackgroundColor(data, 'DRA')}>{utils.convertBieuDataViewer(data.dra)}</td>
                <td style={genBackgroundColor(data, 'DNL')}>{utils.convertBieuDataViewer(data.dnl)}</td>
                <td style={genBackgroundColor(data, 'DBV')}>{utils.convertBieuDataViewer(data.dbv)}</td>
                <td style={genBackgroundColor(data, 'DCH')}>{utils.convertBieuDataViewer(data.dch)}</td>
                <td style={genBackgroundColor(data, 'DKV')}>{utils.convertBieuDataViewer(data.dkv)}</td>
                <td style={genBackgroundColor(data, 'TON')}>{utils.convertBieuDataViewer(data.ton)}</td>
                <td style={genBackgroundColor(data, 'TIN')}>{utils.convertBieuDataViewer(data.tin)}</td>
                <td style={genBackgroundColor(data, 'NTD')}>{utils.convertBieuDataViewer(data.ntd)}</td>
                <td style={genBackgroundColor(data, 'MNC')}>{utils.convertBieuDataViewer(data.mnc)}</td>
                <td style={genBackgroundColor(data, 'SON')}>{utils.convertBieuDataViewer(data.son)}</td>
                <td style={genBackgroundColor(data, 'PNK')}>{utils.convertBieuDataViewer(data.pnk)}</td>
                <td style={genBackgroundColor(data, 'CGT')}>{utils.convertBieuDataViewer(data.cgt)}</td>
                <td style={genBackgroundColor(data, 'BCS')}>{utils.convertBieuDataViewer(data.bcs)}</td>
                <td style={genBackgroundColor(data, 'DCS')}>{utils.convertBieuDataViewer(data.dcs)}</td>
                <td style={genBackgroundColor(data, 'NCS')}>{utils.convertBieuDataViewer(data.ncs)}</td>
                <td style={genBackgroundColor(data, 'MCS')}>{utils.convertBieuDataViewer(data.mcs)}</td>
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
