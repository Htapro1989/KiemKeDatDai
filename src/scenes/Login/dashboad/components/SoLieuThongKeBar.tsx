import React from 'react'
import './index.less'
import { Col, Row } from 'antd'

export default function SoLieuThongKeBar(props: any) {
    const { baoCaoData } = props;
    console.log(baoCaoData)
    return (
        <div className='so-lieu-thong-ke-bar'>
            <Row gutter={16}>
                <Col span={8}>
                    <div className='box-wrapper'>
                        <div className='header-text'>{baoCaoData?.tongSoTinhHoanThanh}/{baoCaoData?.tongSoTinh}</div>
                        <div className='bottom-text'>Tỉnh đã được duyệt kết quả báo cáo</div>
                    </div>
                </Col>
                <Col span={8}>
                    <div className='box-wrapper'>
                        <div className='header-text'>{baoCaoData?.tongSoHuyenHoanThanh}/{baoCaoData?.tongSoHuyen}</div>
                        <div className='bottom-text'>Huyện đã được duyệt kết quả báo cáo</div>
                    </div>
                </Col>
                <Col span={8}>
                    <div className='box-wrapper'>
                        <div className='header-text'>{baoCaoData?.tongSoXaHoanThanh}/{baoCaoData?.tongSoXa}</div>
                        <div className='bottom-text'>Xã đã hoàn thành báo cáo thống kê</div>
                    </div>
                </Col>
            </Row>
        </div>
    )
}
