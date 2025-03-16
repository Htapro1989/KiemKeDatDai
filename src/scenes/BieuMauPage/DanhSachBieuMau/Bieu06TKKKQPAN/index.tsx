import '../components/index.less'
import './style.less'
import { Button, Dropdown, Form, Input, Menu, notification, Popconfirm, Table, Typography } from 'antd';
import React, { useState } from 'react';
import { Bieu06Item, EditableCellBieu06 } from './components/EditableCellBieu06';
import HeaderBieuMau from '../components/Header';
import { MoreOutlined, PlusOutlined } from '@ant-design/icons';


const originData: Bieu06Item[] = [
    {
        key: "B06-001",
        stt: "1",
        donVi: "Bộ Quốc Phòng",
        diaChiSuDungDat: "Khu vực X, Huyện Y, Tỉnh Z",
        dienTichDatQP: "5000m2",
        dienTichKetHop: "2000m2",
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
            notification.success({ message: "Thành công" })
        } catch (errInfo) {
            console.log('Validate Failed:', errInfo);
        }
    };

    const onAddRow = () => {
        const newRow = {
            key: "B06-002",
            stt: "",
            donVi: "",
            diaChiSuDungDat: "",
            dienTichDatQP: "",
            dienTichKetHop: "",
            loaiDatKetHop: "",
            dienTichDaDo: "",
            soGiayChungNhan: "",
            dienTichDaCapGiay: "",
            ghiChu: ""
        }
        setData(value => ([...value, newRow]))
        setEditingKey(newRow.key)

    }

    const columns = [
        {
            title: '',
            dataIndex: 'x',
            render: (_: any, record: Bieu06Item) => {
                const editable = isEditing(record);
                const rowMenu = () => {
                    return (
                        <Menu>
                            <Menu.Item onClick={() => onAddRow()}>Thêm hàng</Menu.Item>
                            <Menu.Item onClick={() => edit(record)}>Sửa hàng</Menu.Item>
                            <Menu.Item>Xóa hàng</Menu.Item>
                        </Menu>
                    )
                }
                return editable ? (
                    <span>
                        <Typography.Link onClick={() => save(record.key)} style={{ marginRight: 8 }}>
                            Lưu
                        </Typography.Link>
                        <Popconfirm title="Hủy bỏ sửa dữ liệu?" onConfirm={cancel}>
                            <a>Hủy</a>
                        </Popconfirm>
                    </span>
                ) : (
                    <Dropdown overlay={rowMenu} disabled={editingKey !== ''}>
                        <Button>
                            <MoreOutlined style={{ fontWeight: 'bold' }} />
                        </Button>
                    </Dropdown>
                );
            },
        },
        {
            title: 'Thứ tự',
            dataIndex: 'stt',
            editable: true,
        },
        {
            title: 'Đơn vị (hoặc điểm) sử dụng đất',
            dataIndex: 'donVi',
            editable: true,
        },
        {
            title: 'Địa chỉ sử dụng đất',
            dataIndex: 'diaChiSuDungDat',
            editable: true,
        },

        {
            title: 'Diện tích đất quốc phòng/đất an ninh',
            dataIndex: 'dienTichDatQP',
            editable: true,
        },

        {
            title: 'Trong đó diện tích kết hợp vào mục đích khác',
            children: [
                {
                    title: 'Diện tích',
                    dataIndex: 'dienTichKetHop',
                    render: (_: any, record: any) => {
                        return isEditing(record) ? (
                            <Form.Item
                                name={"dienTichKetHop"}
                                style={{ margin: 0 }}
                                rules={[
                                    {
                                        required: true,
                                        message: `Please Input !`,
                                    },
                                ]}
                            >
                                <Input />
                            </Form.Item>
                        ) : (
                            <span>
                                {record.dienTichKetHop}
                            </span>
                        )
                    }
                },
                {
                    title: 'Loại đất kết hợp',
                    dataIndex: 'loaiDatKetHop',
                    render: (_: any, record: any) => {
                        return isEditing(record) ? (
                            <Form.Item
                                name={"loaiDatKetHop"}
                                style={{ margin: 0 }}
                                rules={[
                                    {
                                        required: true,
                                        message: `Please Input !`,
                                    },
                                ]}
                            >
                                <Input />
                            </Form.Item>
                        ) : (
                            <span>
                                {record.dienTichKetHop}
                            </span>
                        )
                    }
                },
            ]
        },

        {
            title: 'Tình hình đo đạc lập bản đồ địa chính hoặc trích đo địa chính, cấp Giấy chứng nhận',
            children: [
                {
                    title: 'Diện tích đã đo đạc',
                    dataIndex: 'dienTichDoDac',
                    render: (_: any, record: any) => {
                        return isEditing(record) ? (
                            <Form.Item
                                name={"dienTichDoDac"}
                                style={{ margin: 0 }}
                                rules={[
                                    {
                                        required: true,
                                        message: `Please Input !`,
                                    },
                                ]}
                            >
                                <Input />
                            </Form.Item>
                        ) : (
                            <span>
                                {record.dienTichDoDac}
                            </span>
                        )
                    }
                },
                {
                    title: 'Số Giấy chứng nhận đã cấp',
                    dataIndex: 'soGiayChungNhan',
                    render: (_: any, record: any) => {
                        return isEditing(record) ? (
                            <Form.Item
                                name={"soGiayChungNhan"}
                                style={{ margin: 0 }}
                                rules={[
                                    {
                                        required: true,
                                        message: `Please Input !`,
                                    },
                                ]}
                            >
                                <Input />
                            </Form.Item>
                        ) : (
                            <span>
                                {record.soGiayChungNhan}
                            </span>
                        )
                    }
                },
                {
                    title: 'Diện tích đã cấp giấy chứng nhận',
                    dataIndex: 'dienTichDaCapGiay',
                    render: (_: any, record: any) => {
                        return isEditing(record) ? (
                            <Form.Item
                                name={"dienTichDaCapGiay"}
                                style={{ margin: 0 }}
                                rules={[
                                    {
                                        required: true,
                                        message: `Please Input !`,
                                    },
                                ]}
                            >
                                <Input />
                            </Form.Item>
                        ) : (
                            <span>
                                {record.dienTichDaCapGiay}
                            </span>
                        )
                    }
                },
            ]
        },
        {
            title: 'Ghi chú',
            dataIndex: 'ghiChu',
            editable: true,
        }

    ];

    const mergedColumns = columns.map(col => {
        if (!col.editable) {
            return col;
        }
        return {
            ...col,
            onCell: (record: Bieu06Item) => ({
                record,
                hasChild: true,
                dataIndex: col.dataIndex,
                title: col.title,
                editing: isEditing(record),
            }),
        };
    });

    return (
        <div className='bieu-mau__layout-wrapper'>
            <HeaderBieuMau maBieu='Biểu 06/TKKKQPAN' tenBieu='THỐNG KÊ, KIỂM KÊ ĐẤT QUỐC PHÒNG, ĐẤT AN NINH' />
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
                    className='custom-table'
                    size='small'
                    pagination={false}
                    title={() => {
                        return <Button
                            icon={<PlusOutlined />}
                            type='primary'>
                            Nhập dữ liệu mới
                        </Button>
                    }}
                />
            </Form>
        </div>

    );
};

export default Bieu06TKKKQPAN;

{/* <Button
                            type='primary'
                            disabled={editingKey != ''}
                            icon={<PlusOutlined />}
                            onClick={onAddRow} /> */}

