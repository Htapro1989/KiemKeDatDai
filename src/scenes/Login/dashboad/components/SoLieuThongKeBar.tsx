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
                        <div className='header-text'>{baoCaoData?.tongSoTinhHoanThanh}/{baoCaoData?.tongSoTinh} <span style={{fontSize:12}}>Tỉnh đã nhận kết quả cấp huyện và chưa duyệt</span></div>
                        <div className='bottom-text'>{baoCaoData?.tongSoTinhHoanThanh}/{baoCaoData?.tongSoTinh} Tỉnh đã được duyệt kết quả báo cáo</div>
                    </div>
                </Col>
                <Col span={8}>
                    <div className='box-wrapper'>
                        <div className='header-text'>{baoCaoData?.tongSoHuyenHoanThanh}/{baoCaoData?.tongSoHuyen} <span style={{fontSize:12}}>Huyện đã nhận và chưa duyệt kết quả</span></div>
                        <div className='bottom-text'>Huyện đã được duyệt kết quả báo cáo</div>
                    </div>
                </Col>
                <Col span={8}>
                    <div className='box-wrapper'>
                        <div className='header-text'>{baoCaoData?.tongSoXaHoanThanh}/{baoCaoData?.tongSoXa} <span style={{fontSize:12}}>Xã đã hoàn thành báo cáo thống kê</span></div>
                        <div className='bottom-text'>Xã đã hoàn thành báo cáo thống kê</div>
                    </div>
                </Col>
            </Row>
        </div>
    )
}
