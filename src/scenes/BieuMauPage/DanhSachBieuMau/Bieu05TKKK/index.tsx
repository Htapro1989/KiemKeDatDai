import React from 'react'
import '../components/index.less'
import HeaderBieuMau from '../components/Header'


export default function Bieu05TKKK() {
    return (
        <div className='bieu-mau__layout-wrapper'>
            <HeaderBieuMau maBieu='Biểu 05/TKKK' tenBieu='CHU CHUYỂN DIỆN TÍCH CỦA CÁC LOẠI ĐẤT' />
            <table className="report-table">
                <thead>
                    <tr>
                        <th>STT</th>
                        <th>Loại đất</th>
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
                </tbody>
            </table>
        </div>
    )
}
