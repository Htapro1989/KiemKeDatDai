import { Col, Row } from 'antd';
import './index.less'
import React from 'react'

interface HeaderBieuMauProps {
    maBieu: string;
    tenBieu: string;
    donViBaoCao?: any

}

export default function HeaderBieuMau(props: HeaderBieuMauProps) {

    const tenXa = props.donViBaoCao?.tenXa || '..................'
    const tenHuyen = props.donViBaoCao?.tenHuyen || '..................'
    const tenTinh = props.donViBaoCao?.tenTinh || '..................'

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
                <Row>
                    <Col><p style={{ width: 50 }}>Xã:</p></Col>
                    <Col><p>{tenXa}</p></Col>
                </Row>
                <Row>
                    <Col><p style={{ width: 50 }}>Huyện:</p></Col>
                    <Col><p>{tenHuyen}</p></Col>
                </Row>
                <Row>
                    <Col><p style={{ width: 50 }}>Tỉnh:</p></Col>
                    <Col><p >{tenTinh}</p></Col>
                </Row>
            </div>
        </div>
    )
}
