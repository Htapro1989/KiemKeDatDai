export const circleOptions = {
    title: {
    text: 'Phần trăm báo cáo số liệu',
    subtext: '2024',
    left: 'center'
  },
  tooltip: {
    trigger: 'item'
  },
  legend: {
    bottom: '0%',
    left: 'center'
  },
  series: [
    {
      name: 'Access From',
      type: 'pie',
      radius: ['30%', '60%'],
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
         { value: 580, name: 'Vùng trung du và miền núi phía Bắc' },
        { value: 1048, name: 'Vùng Bắc Trung Bộ và Duyên hải miền Trung' },
        { value: 735, name: 'Vùng đồng bắng sông Hồng' },
        { value: 484, name: 'Vùng Tây Nguyên' },
        { value: 300, name: 'Vùng đồng bằng sông Cửu Long' },
          { value: 300, name: 'Vùng Đông Nam Bộ' },
            { value: 300, name: 'Chưa nộp báo cáo' }
      ]
    }
  ]
}