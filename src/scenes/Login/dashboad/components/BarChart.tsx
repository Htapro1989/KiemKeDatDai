import React from 'react'
import ReactECharts from 'echarts-for-react';
export default function BarChart(props: any) {
    const { baoCaoData } = props;
    const option = {
        title: {
            text: 'Số lượng báo cáo đã nộp theo vùng'
        },
        tooltip: {
            trigger: 'item'
        },
        legend: {
            bottom: '0%',
            left: 'center'
        },
        grid: {
            left: '3%',
            right: '4%',
            bottom: '15%',
            top: '15%',
            containLabel: true
        },

        yAxis: {
            type: 'category',
            data: [
                'Trung du và miền núi phía Bắc',
                'Bắc Trung Bộ và Duyên hải miền Trung',
                'Đồng bắng sông Hồng',
                'Tây Nguyên',
                'Đồng bằng sông Cửu Long',
                'Đông Nam Bộ']
        },
        xAxis: {
            type: 'value'
        },
        series: [
            {
                data: [baoCaoData?.vungMienNuiPhiaBac,
                baoCaoData?.vungDuyenHaiMienTrung,
                baoCaoData?.vungDongBangSongHong,
                baoCaoData?.vungTayNguyen,
                baoCaoData?.vungDongBangSongCuuLong,
                baoCaoData?.vungDongNamBo],
                type: 'bar',
                name: 'Báo cáo đã hoàn thành',
                barWidth: "40%"
            }
        ]
    };
    return (
        <div><ReactECharts style={{ height: 500 }} option={option} /></div>
    )
}
