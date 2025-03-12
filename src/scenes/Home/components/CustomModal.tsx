import React from 'react'

interface ICustomModal {
    visible: boolean,
    children: any
}

export default function CustomModal(props: ICustomModal) {
    if (!props.visible) return null;
    return (
        <div
            style={{
                position: 'fixed',
                top: 0,
                width:1024,
                background: '#FFF',
                overflow:'scroll',
                overflowX:'scroll',
                overflowY:'scroll'
            }}
        >{props.children}</div>
    )
}
