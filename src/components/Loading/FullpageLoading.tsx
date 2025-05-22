import * as React from 'react';
import { Spin } from 'antd';
import './fullpagestyle.less';

const FullPageLoading = (props: any) => {
    if (!props.isLoading) return null;
    return (
        <div className="full-page-loading">
            <Spin size="large" tip="Đang tải..." />
        </div>
    );
};
export default FullPageLoading;