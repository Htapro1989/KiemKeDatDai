import React from 'react'
import { Form, Input } from "antd";

export interface Bieu06Item {
    // key: string;
    // name: string;
    // age: number;
    // address: string;

    key: string;
    stt: string;
    donVi: string;
    diaChiSuDungDat: string;
    dienTichDatQP: string
    dienTich: string
    loaiDatKetHop: string
    dienTichDaDo: string
    soGiayChungNhan: string
    dienTichDaCapGiay: string
    ghiChu: string
}

interface EditableCellProps extends React.HTMLAttributes<HTMLElement> {
    editing: boolean;
    dataIndex: string;
    title: any;
    inputType: 'number' | 'text';
    record: Bieu06Item;
    index: number;
    children: React.ReactNode;
    hasChild: boolean
}


export const EditableCellBieu06: React.FC<EditableCellProps> = ({
    editing,
    dataIndex,
    title,
    inputType,
    record,
    index,
    children,
    hasChild,
    ...restProps
}) => {
    console.log("=>",
        {
            editing,
            dataIndex,
            title,
            inputType,
            record,
            index,
            children,
            ...restProps
        }
    );


    return (
        <td {...restProps}>
            {editing ? (
                <Form.Item
                    name={dataIndex}
                    style={{ margin: 0 }}
                    rules={[
                        {
                            required: true,
                            message: `Please Input ${title}!`,
                        },
                    ]}
                >
                    <Input />
                </Form.Item>
            ) : (
                children
            )}
        </td>
    );
};