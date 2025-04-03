import React from 'react'
import ReactECharts from 'echarts-for-react';

export default function CircleChart(props: any) {
    const { baoCaoData } = props;
    
    const circleOptions = {
        title: {
            text: 'Phần trăm DVHC đã nộp báo cáo',
            subtext: '2024',
            left: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{b}: {c}%"
        },
        legend: {
            bottom: '0%',
            left: 'center'
        },
        series: [
            {
                name: 'Báo cáo số liệu',
                type: 'pie',
                radius: ['35%', '60%'],
                avoidLabelOverlap: false,
                label: {
                    show: false,
                    position: 'center'
                },
                emphasis: {
                    label: {
                        show: true,
                        fontSize: 20,
                        fontWeight: 'bold'
                    }
                },
                labelLine: {
                    show: false
                },
                data: [
                    { value: baoCaoData?.phanTramVungMienNuiPhiaBac, name: 'Vùng trung du và miền núi phía Bắc' },
                    { value: baoCaoData?.phanTramVungDuyenHaiMienTrung, name: 'Vùng Bắc Trung Bộ và Duyên hải miền Trung' },
                    { value: baoCaoData?.phanTramVungDongBangSongHong, name: 'Vùng đồng bắng sông Hồng' },
                    { value: baoCaoData?.phanTramVungTayNguyen, name: 'Vùng Tây Nguyên' },
                    { value: baoCaoData?.phanTramVungDongBangSongCuuLong, name: 'Vùng đồng bằng sông Cửu Long' },
                    { value: baoCaoData?.phanTramVungDongNamBo, name: 'Vùng Đông Nam Bộ' },
                    { value: baoCaoData?.chuaNopBaoCao, name: 'Chưa nộp báo cáo' }
                ]
            }
        ]
    }
    return <ReactECharts style={{ height: 500 }} option={circleOptions} />;
};