import { Form, Input, InputNumber } from 'antd';
import React from 'react'


export interface ItemBieu02 {
    tenDonVi?: string;
    dienTichTheoQDGiaoThue?: number;
    dienTichGiaoDat?: number;
    dienTichChoThueDat?: number;
    dienTichChuaXacDinhGiaoThue?: number;
    dienTichDoDacTL1000?: number;
    dienTichDoDacTL2000?: number;
    dienTichDoDacTL5000?: number;
    dienTichDoDacTL10000?: number;
    soGCNDaCap?: number;
    dienTichGCNDaCap?: number;
    dienTichDaBanGiao?: number;
    ghiChu?: string;
    id: string;
}

interface EditableCellProps extends React.HTMLAttributes<HTMLElement> {
    editing: boolean;
    dataIndex: string;
    title: any;
    inputType: 'number' | 'text';
    record: ItemBieu02;
    index: number;
    children: React.ReactNode;
}

export const Bieu02EditTableCell: React.FC<EditableCellProps> = ({
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
