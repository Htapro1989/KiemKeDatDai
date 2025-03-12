import './index.less'
import { Skeleton } from 'antd'
import React from 'react'

export default function ReportLoading(props: any) {
    if (props.isLoading)
        return (
            <div className='loading-wrapper'>
                <Skeleton active />
            </div>
        )
    return null;
}
