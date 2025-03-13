import '../components/index.less'
import { Form, Popconfirm, Table, Typography } from 'antd';
import React, { useState } from 'react';
import { Bieu06Item, EditableCellBieu06 } from './components/EditableCellBieu06';
import HeaderBieuMau from '../components/Header';


const originData: Bieu06Item[] = [
    {
        key: "B06-001",
        stt: "1",
        donVi: "Bộ Quốc Phòng",
        diaChiSuDungDat: "Khu vực X, Huyện Y, Tỉnh Z",
        dienTichDatQP: "5000m2",
        dienTich: "2000m2",
        loaiDatKetHop: "Đất quốc phòng kết hợp thương mại",
        dienTichDaDo: "1800m2",
        soGiayChungNhan: "GCN-123456",
        dienTichDaCapGiay: "1500m2",
        ghiChu: "Đã cấp giấy chứng nhận quyền sử dụng đất một phần."
    }
];


const Bieu06TKKKQPAN: React.FC = () => {
    const [form] = Form.useForm();
    const [data, setData] = useState(originData);
    const [editingKey, setEditingKey] = useState('');

    const isEditing = (record: Bieu06Item) => record.key === editingKey;

    const edit = (record: Partial<Bieu06Item> & { key: React.Key }) => {
        form.setFieldsValue({ name: '', age: '', address: '', ...record });
        setEditingKey(record.key);
    };

    const cancel = () => {
        setEditingKey('');
    };

    const save = async (key: React.Key) => {
        try {
            const row = (await form.validateFields()) as Bieu06Item;
            console.log('save row,', row)

            const newData = [...data];
            const index = newData.findIndex(item => key === item.key);
            if (index > -1) {
                const item = newData[index];
                newData.splice(index, 1, {
                    ...item,
                    ...row,
                });
                setData(newData);
                setEditingKey('');
            } else {
                newData.push(row);
                setData(newData);
                setEditingKey('');
            }
        } catch (errInfo) {
            console.log('Validate Failed:', errInfo);
        }
    };

    const columns = [
        {
            title: 'stt',
            dataIndex: 'stt',
            editable: true,
        },
        {
            title: 'donVi',
            dataIndex: 'donVi',
            editable: true,
        },
        {
            title: 'diaChiSuDungDat',
            dataIndex: 'diaChiSuDungDat',
            editable: true,
        },

        {
            title: 'dienTichDatQP',
            dataIndex: 'dienTichDatQP',
            editable: true,
        },

        {
            title: 'Trong đó diện tích kết hợp vào mục đích khác',
            editable: true,
            hasChild: true,
            children: [
                {
                    title: 'dienTich',
                    dataIndex: 'dienTich',
                    editable: true,
                },
                {
                    title: 'loaiDatKetHop',
                    dataIndex: 'loaiDatKetHop',
                    editable: true,
                },
            ]
        },

        {
            title: 'dienTichDaDo',
            dataIndex: 'dienTichDaDo',
            editable: true,
        },
        {
            title: 'soGiayChungNhan',
            dataIndex: 'soGiayChungNhan',
            editable: true,
        },
        {
            title: 'ghiChu',
            dataIndex: 'ghiChu',
            editable: true,
        },
        {
            title: 'operation',
            dataIndex: 'operation',
            render: (_: any, record: Bieu06Item) => {
                const editable = isEditing(record);
                return editable ? (
                    <span>
                        <Typography.Link onClick={() => save(record.key)} style={{ marginRight: 8 }}>
                            Save
                        </Typography.Link>
                        <Popconfirm title="Sure to cancel?" onConfirm={cancel}>
                            <a>Cancel</a>
                        </Popconfirm>
                    </span>
                ) : (
                    <Typography.Link disabled={editingKey !== ''} onClick={() => edit(record)}>
                        Edit
                    </Typography.Link>
                );
            },
        },
    ];

    const mergedColumns = columns.map(col => {
        if (!col.editable) {
            return col;
        }
        return {
            ...col,
            onCell: (record: Bieu06Item) => ({
                record,
                hasChild: col.hasChild,
                dataIndex: col.dataIndex,
                title: col.title,
                editing: isEditing(record),
            }),
        };
    });

    return (
        <div className='bieu-mau__layout-wrapper'>
            <HeaderBieuMau maBieu='Biểu 05/TKKK' tenBieu='CHU CHUYỂN DIỆN TÍCH CỦA CÁC LOẠI ĐẤT' />
            <Form form={form} component={false}>
                <Table
                    components={{
                        body: {
                            cell: EditableCellBieu06,

                        },
                    }}
                    bordered
                    dataSource={data}
                    columns={mergedColumns}
                    rowClassName="editable-row"
                    pagination={false}
                />
            </Form>
        </div>

    );
};

export default Bieu06TKKKQPAN;

