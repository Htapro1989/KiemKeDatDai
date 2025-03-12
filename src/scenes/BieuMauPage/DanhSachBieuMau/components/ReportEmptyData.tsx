import { Empty } from 'antd'
import React from 'react'

export default function ReportEmptyData(props: any) {
    if (props.isEmpty)
        return (
            <Empty image={Empty.PRESENTED_IMAGE_SIMPLE} description="Không có dữ liệu" />
        )
    return null;
}
