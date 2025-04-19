import React, { useEffect, useState } from 'react'
import './bieu06Style.less'
import { Button, Divider, Form, notification, Popconfirm, Table } from 'antd'
import { Bieu06EditTableCell, ItemBieu06 } from './Bieu06EditTableCell';
import { CheckCircleOutlined, CloseSquareOutlined, DeleteOutlined, EditOutlined, PlusOutlined } from '@ant-design/icons';
import bieuMauService from '../../../services/bieuMau/bieuMauService';
import { handleCommontResponse } from '../../../services/common/handleResponse';
import { BieuInputProps } from './Bieu02Input';


export default function Bieu06Input(props: BieuInputProps) {
    const [form] = Form.useForm();
    const [data, setData] = useState<ItemBieu06[]>([]);
    const [editingKey, setEditingKey] = useState('');

    const isEditing = (record: ItemBieu06) => record.id === editingKey;

    const edit = (record: Partial<ItemBieu06> & { id: React.Key }) => {
        form.setFieldsValue({ name: '', age: '', address: '', ...record });
        setEditingKey(record.id);
    };

    const cancel = (id: any) => {
        if (id == 'NEW') {
            setData(data.filter(data => data.id != "NEW"))
        }

        setEditingKey('');
        form.resetFields();
    };

    const save = async (key: React.Key) => {
        try {
            const row = (await form.validateFields()) as ItemBieu06;

            const newData = [...data];
            const index = newData.findIndex(item => key === item.id);

            if (index > -1) {
                const item = newData[index];
                newData.splice(index, 1, {
                    ...item,
                    ...row,
                });
                setData(newData);
                setEditingKey('');
                const dataNew = { ...item, ...row, id: item.id == "NEW" ? undefined : item.id }
                onCreateOrUpdate(dataNew)
            } else {
                newData.push(row);
                setData(newData);
                setEditingKey('');
                const dataNew = { ...row, id: undefined }
                onCreateOrUpdate(dataNew)
            }
        } catch (errInfo) {
            console.log('Validate Failed:', errInfo);
        }
    };

    const onAddRow = () => {
        const newRow = {
            id: "NEW",
            year: props.year,
            tinhId: props.dvhcId
        }
        setData((value: any) => ([...value, newRow]))
        setEditingKey(newRow.id)
    }

    const columns = [
        {
            title: 'Đơn vị sử dụng đất',
            dataIndex: 'donVi',
            editable: true,
        },
        {
            title: 'Địa chỉ sử dụng đất',
            dataIndex: 'diaChi',
            editable: true,
        },
        {
            title: 'Diện tích đất quốc phòng,an ninh',
            dataIndex: 'dienTichDatQuocPhong',
            editable: true,
        },
        {
            title: 'Diện tích',
            dataIndex: 'dienTichKetHopKhac',
            editable: true,
        },
        {
            title: 'Loại đất kết hợp',
            dataIndex: 'loaiDatKetHopKhac',
            editable: true,
        },
        {
            title: 'Diện tích đã đo đạc',
            dataIndex: 'dienTichDaDoDac',
            editable: true,
        },
        {
            title: 'Số giấy chứng nhận đã cấp',
            dataIndex: 'soGCNDaCap',
            editable: true,
        },
        {
            title: 'Diện tích đã cấp chứng nhận',
            dataIndex: 'dienTichDaCapGCN',
            editable: true,
        },
        {
            title: 'Ghi chú',
            dataIndex: 'ghiChu',
            editable: true,
        },
        {
            title: 'Hành động',
            dataIndex: 'operation',
            render: (_: any, record: ItemBieu06) => {
                const editable = isEditing(record);
                return editable ? (
                    <span>

                        <Button
                            type='primary'
                            ghost
                            size='small'
                            icon={<CheckCircleOutlined />}
                            onClick={() => save(record.id)}
                            style={{ marginRight: 4 }}
                        />

                        <Popconfirm title="Bạn muốn hủy bỏ?" onConfirm={() => cancel(record.id)}>
                            <Button
                                type='default'
                                size='small'
                                icon={<CloseSquareOutlined />}
                            />
                        </Popconfirm>
                    </span>
                ) : (
                    <span>
                        <Button
                            type='primary'
                            ghost
                            size='small'
                            icon={<EditOutlined />}
                            disabled={editingKey !== ''} onClick={() => edit(record)}
                            style={{ marginRight: 4 }}
                        />
                        <Popconfirm title="Bạn muốn xóa hàng này?" onConfirm={() => onDeleteItem(record.id)}>
                            <Button
                                type='default'
                                size='small'
                                disabled={editingKey !== ''}
                                icon={<DeleteOutlined />}
                            />
                        </Popconfirm>

                    </span>

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
            onCell: (record: ItemBieu06) => ({
                record,
                inputType: col.dataIndex === 'age' ? 'number' : 'text',
                dataIndex: col.dataIndex,
                title: col.title,
                editing: isEditing(record),
            }),
        };
    });

    const onCreateOrUpdate = async (data: any) => {
        const response = await bieuMauService.onCreateOrUpdateBieu06(data)
        handleCommontResponse(response)
    }

    const onGetData = async () => {
        const response = await bieuMauService.getBieu06ByDvhc(props.dvhcId, props.year);
        setData(response.returnValue ? response.returnValue : [])
    }

    const onDeleteItem = async (id: any) => {
        const response = await bieuMauService.onDeleteItemBieu06ById(id);
        if (response.code == 1) {
            setData(data.filter(data => data.id != id))
        }
        notification.info({ message: response.message })
    }

    useEffect(() => {
        if (props.isRefresh && props.dvhcId && props.year)
            onGetData()
    }, [props.isRefresh, props.dvhcId, props.year])


    return (
        <div className='input-bieu-06-container'>
            <h1 className='header'> Biểu mẫu: THỐNG KÊ, KIỂM KÊ ĐẤT QUỐC PHÒNG, ĐẤT AN NINH</h1>
            <Divider />

            <Form form={form} component={false}>
                <Table
                    components={{
                        body: {
                            cell: Bieu06EditTableCell,
                        },
                    }}
                    title={() => {
                        return <Button
                            onClick={() => onAddRow()}
                            disabled={editingKey != ''}
                            icon={<PlusOutlined />}
                            type='primary'>
                            Thêm hàng
                        </Button>
                    }}
                    bordered
                    key={"id"}
                    dataSource={data}
                    columns={mergedColumns}
                    rowClassName="editable-row"
                    pagination={false}
                />
            </Form>
        </div>
    )
}
