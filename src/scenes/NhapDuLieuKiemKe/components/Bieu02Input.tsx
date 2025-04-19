import React, { useEffect, useState } from 'react'
import './bieu06Style.less'
import { Button, Divider, Form, notification, Popconfirm, Table } from 'antd'
import { CheckCircleOutlined, CloseSquareOutlined, DeleteOutlined, EditOutlined, PlusOutlined } from '@ant-design/icons';
import bieuMauService from '../../../services/bieuMau/bieuMauService';
import { handleCommontResponse } from '../../../services/common/handleResponse';
import { Bieu02EditTableCell, ItemBieu02 } from './Bieu02EditTableCell';

export interface BieuInputProps {
    isRefresh: boolean
    year: any;
    dvhcId: any
}

export default function Bieu02Input(props: BieuInputProps) {
    const [form] = Form.useForm();
    const [data, setData] = useState<ItemBieu02[]>([]);
    const [editingKey, setEditingKey] = useState('');

    const isEditing = (record: ItemBieu02) => record.id === editingKey;

    const edit = (record: Partial<ItemBieu02> & { id: React.Key }) => {
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
            const row = (await form.validateFields()) as ItemBieu02;

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
                console.log("VAOD create")
                onCreateOrUpdate(dataNew)
            } else {
                newData.push(row);
                setData(newData);
                setEditingKey('');
                console.log("VAOD UPDATE")
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
            title: 'Tên đơn vị',
            dataIndex: 'tenDonVi',
            editable: true,
        },
        {
            title: 'Diện tích theo QD giao thuế',
            dataIndex: 'dienTichTheoQDGiaoThue',
            editable: true,
        },
        {
            title: 'Diện tích giao đất ',
            dataIndex: 'dienTichGiaoDat',
            editable: true,
        },
        {
            title: 'Diện tích chưa xác định giao thuế',
            dataIndex: 'dienTichChuaXacDinhGiaoThue',
            editable: true,
        },
        {
            title: 'Diện tích đo đạc TL1000',
            dataIndex: 'dienTichDoDacTL1000',
            editable: true,
        },
        {
            title: 'Diện tích đo đạc TL2000',
            dataIndex: 'dienTichDoDacTL2000',
            editable: true,
        },
        {
            title: 'Diện tích đo đạc TL5000',
            dataIndex: 'dienTichDoDacTL5000',
            editable: true,
        },
        {
            title: 'Diện tích đo đạc TL10000',
            dataIndex: 'dienTichDoDacTL10000',
            editable: true,
        },
        {
            title: 'Số giấy chứng nhận đã cấp',
            dataIndex: 'soGCNDaCap',
            editable: true,
        },
        {
            title: 'Diện tích đã cấp chứng nhận',
            dataIndex: 'dienTichGCNDaCap',
            editable: true,
        },
        {
            title: 'Diện tích đã bàn giao',
            dataIndex: 'dienTichDaBanGiao',
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
            render: (_: any, record: ItemBieu02) => {
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
            onCell: (record: ItemBieu02) => ({
                record,
                inputType: col.dataIndex === 'age' ? 'number' : 'text',
                dataIndex: col.dataIndex,
                title: col.title,
                editing: isEditing(record),
            }),
        };
    });

    const onCreateOrUpdate = async (data: any) => {
        const response = await bieuMauService.onCreateOrUpdateBieu02(data)
        handleCommontResponse(response)
    }


    const onGetData = async () => {
        const response = await bieuMauService.getBieu02ByDvhc(props.dvhcId, props.year);
        setData(response.returnValue ? response.returnValue : [])
    }

    const onDeleteItem = async (id: any) => {
        const response = await bieuMauService.onDeleteItemBieu02ById(id);
        if (response.code == 1) {
            setData(data.filter(data => data.id != id))
        }
        notification.info({ message: response.message })
    }

    useEffect(() => {
        if (props.isRefresh)
            onGetData()
    }, [props.isRefresh])


    return (
        <div className='input-bieu-06-container'>
            <h1 className='header'> Kiểm kê tình hình đo đạc, cấp giấy chứng nhận và hình thức sử dụng đất của các công ty nông, lâm nghiệp</h1>
            <Divider />

            <Form form={form} component={false}>
                <Table
                    components={{
                        body: {
                            cell: Bieu02EditTableCell,
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
                    size='small'
                />
            </Form>
        </div>
    )
}
