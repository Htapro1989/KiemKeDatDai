import { Button } from 'antd';
import React from 'react'

interface ICustomModal {
    visible: boolean,
    children: any,
    onClose: () => void
}

export default function CustomModal(props: ICustomModal) {
    if (!props.visible) return null;
    return (
        <div
            style={{
                position: 'fixed',
                top: 0,
                left: 0,
                right: 0,
                bottom: 0,
                display: 'flex'
            }}
        >
            <div style={{
                position: 'fixed',
                top: 10,
                right: 10,
                background: '#FFF',
                zIndex: 1000
            }}>
                <Button type='primary' ghost onClick={props.onClose}>Đóng</Button>
            </div>
            <div style={{
                flex: 1,
                background: '#FFF',
                overflow: 'scroll'
            }}>
                {props.children}
            </div>
        </div>
    )
}
