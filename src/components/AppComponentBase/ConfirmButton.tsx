import { Button, Popconfirm } from 'antd'
import { ButtonProps } from 'antd/lib/button'
import React from 'react'

interface IConfirmButtonProps {
    confirmTitle: string
    onConfirm: any
    onCancel?: any
    btnTitle: string
    buttonProps?: ButtonProps
}

export default function ConfirmButton(props: IConfirmButtonProps) {

    return (
        <Popconfirm
            title={props.confirmTitle}
            onConfirm={props.onConfirm}
            onCancel={props?.onCancel ? props?.onCancel : () => { }}
            okText="Có"
            cancelText="Không"
            
        >
            <Button {...props.buttonProps}>
                {props.btnTitle}
            </Button>
        </Popconfirm>
    )
}
