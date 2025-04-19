import { Form, Input, InputNumber } from 'antd';
import React from 'react'


export interface ItemBieu06 {
    id: any;
    donVi: string;
    diaChi: string;
    dienTichDatQuocPhong: number;
    dienTichKetHopKhac: string;
    loaiDatKetHopKhac: string;
    dienTichDaDoDac: string;
    soGCNDaCap: string;
    dienTichDaCapGCN: string;
    ghiChu: string;
}

interface EditableCellProps extends React.HTMLAttributes<HTMLElement> {
    editing: boolean;
    dataIndex: string;
    title: any;
    inputType: 'number' | 'text';
    record: ItemBieu06;
    index: number;
    children: React.ReactNode;
}

export const Bieu06EditTableCell: React.FC<EditableCellProps> = ({
    editing,
    dataIndex,
    title,
    inputType,
    record,
    index,
    children,
    ...restProps
}) => {
    const inputNode = inputType === 'number' ? <InputNumber /> : <Input />;

    return (
        <td {...restProps}>
            {editing ? (
                <Form.Item
                    name={dataIndex}
                    style={{ margin: 0 }}
                    rules={[
                        {
                            required: true,
                            message: `Vui lòng điền đầy đủ thông tin`,
                        },
                    ]}
                >
                    {inputNode}
                </Form.Item>
            ) : (
                children
            )}
        </td>
    );
};
