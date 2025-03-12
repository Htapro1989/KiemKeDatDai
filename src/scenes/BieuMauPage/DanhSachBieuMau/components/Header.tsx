import './index.less'
import React from 'react'

interface HeaderBieuMauProps {
    maBieu: string;
    tenBieu: string;

}

export default function HeaderBieuMau(props: HeaderBieuMauProps) {
    return (
        <div className='bieu-mau__header-wrapper'>
            <p className='bieu-mau__header-ten-bieu'>
                <span>{props.maBieu}</span>
            </p>
            <div className='bieu-mau__header-quoc-hieu'>
                <p>CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM</p>
                <p style={{ textDecoration: "underline" }}>Độc Lập - Tự Do - Hạnh Phúc</p>
                <p style={{ fontSize: 12 }}>{props.tenBieu}</p>
                <p style={{ fontSize: 12 }}>(Đến ngày 31/12/2024)</p>
            </div>
            <div className='bieu-mau__header-don-vi'>
                <p>Đơn vị báo cáo:</p>
                <p>Xã:</p>
                <p>Huyện:</p>
                <p>Tỉnh:</p>
            </div>
        </div>
    )
}
