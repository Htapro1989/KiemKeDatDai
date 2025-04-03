import React, { useEffect, useState } from 'react'
import dvhcService from '../../../../services/dvhc/dvhcService'
import { Button, Card, Col, Dropdown, Input, Menu, Modal, notification, Row, Select, Table } from 'antd'
// import SelectItem from '../../../../components/Select/SelectItem'
import { PlusOutlined, SettingOutlined } from '@ant-design/icons'
import CreateOrUpdateDVHC from './CreateOrUpdateDVHC'
import { FormInstance } from 'antd/lib/form'
const confirm = Modal.confirm;
interface DonViHanhChinhTableProps {
    userId: any
}
interface DVHCState {
    maVung?: any;
    maTinh?: any;
    maxResultCount?: any;
    skipCount?: any;
    year?: any;
    filter?: any;

}
const { Search } = Input;

export default function DonViHanhChinhTable(props: DonViHanhChinhTableProps) {
    const [state, setstate] = useState<DVHCState>({
        maxResultCount: 10,
        filter: '',
        skipCount: 0
    })
    const [dataDvhc, setDataDvhc] = useState<any>()
    const [kyKiemKeOptions, setKyKiemKeOptions] = useState([])
    const [selectedValue, setSelectedValue] = useState<any>(0);

    // const [editingEntity, setEditingEntity] = useState()
    // const [modalVisible, setModalVisible] = useState(false)

    const [editingModalData, setEditingModalData] = useState<any>({
        editingEntity: null,
        modalVisible: false
    })
    const formRef = React.createRef<FormInstance>();



    const getAll = async (params?: DVHCState) => {
        const requestParams: any = params ? params : state
        const response = await dvhcService.getAllDVHC(requestParams);
        setDataDvhc(response?.returnValue)
    }

    const onChangeState = (commingState: any) => {
        const newState = { ...state, ...commingState }
        setstate(newState)
    }

    const handleTableChange = (pagination: any) => {
        const newSkipCount = { skipCount: (pagination.current - 1) * state.maxResultCount! }
        const newState = { ...state, ...newSkipCount }
        setstate(newState)
        getAll(newState)
    };


    const fetchData = async () => {
        const options = await dvhcService.getKyKiemKeAsOption();
        setKyKiemKeOptions(options)
        if (options.length > 0) {
            setSelectedValue(options[0].value);
            const newState = { ...state, year: options[0].value }
            setstate(newState)
            getAll(newState)
        }

    }
    const onSearch = (value: string) => {
        const newFilter = { filter: value }
        const newState = { ...state, ...newFilter, skipCount: 0 }
        setstate(newState)
        getAll(newState)
    }

    const createOrUpdateModalOpen = async (entity?: any) => {
        setEditingModalData({
            editingEntity: entity,
            modalVisible: true
        })
    }
    const onDelete = async (id: any) => {
        const response = await dvhcService.deleteDVHC(id);
        if (response.code == 1) {
            notification.success({ message: response.message })
            getAll(state)
        } else {
            notification.error({ message: response.message })
        }
    }

    const onConfirmDelete = (id: any) => {
        confirm({
            title: 'Bạn muốn xóa hàng này',
            onOk() {
                onDelete(id)
            },
            onCancel() {
                console.log('Cancel');
            },
        });
    }

    useEffect(() => {
        fetchData()
    }, [])

    const columns = [
        { title: 'Tên đơn vị hành chính', dataIndex: 'name', key: 'name', render: (text: string) => <div>{text}</div> },
        { title: 'Tên Vùng', dataIndex: 'tenVung', key: 'tenVung', render: (text: string) => <div>{text}</div> },
        { title: 'Tên Tỉnh', dataIndex: 'tenTinh', key: 'tenTinh', render: (text: string) => <div>{text}</div> },
        { title: 'Tên Huyện', dataIndex: 'tenHuyen', key: 'tenHuyen', render: (text: string) => <div>{text}</div> },
        { title: 'Tên Xã', dataIndex: 'tenXa', key: 'tenXa', render: (text: string) => <div>{text}</div> },
        {
            title: "",
            width: 70,
            render: (text: string, item: any) => (
                <div>
                    <Dropdown
                        trigger={['click']}
                        overlay={
                            <Menu>
                                <Menu.Item onClick={() => { createOrUpdateModalOpen(item) }}>Chỉnh sửa</Menu.Item>
                                <Menu.Item onClick={() => { onConfirmDelete(item.id) }}>Xóa</Menu.Item>
                            </Menu>
                        }
                        placement="bottomLeft"
                    >
                        <Button type="primary" icon={<SettingOutlined />}>
                        </Button>
                    </Dropdown>
                </div>
            ),
        }
    ];

    const onCreateOrUpdateDvhc = async () => {
        const form = formRef.current;
        form!.validateFields().then(async (values: any) => {
            const response = await dvhcService.createOrUpdateDVHC(formRef.current?.getFieldsValue());
            if (response.code == 1) {
                notification.success({ message: response.message })
                getAll(state)
            } else {
                notification.error({ message: response.message })
            }
            form!.resetFields();
            setEditingModalData({
                modalVisible: false
            })
        });

    }

    return (
        <div>
            <Card title={
                <div>
                    <Row gutter={12}>
                        <Col>
                            <h3>Tìm kiếm theo:</h3>
                        </Col>
                        <Col span={6}>
                            <Select
                                style={{ width: '100%' }}
                                options={kyKiemKeOptions}
                                loading={false}
                                placeholder="Chọn kỳ kiểm kê"
                                value={selectedValue}
                                onChange={(value) => {
                                    onChangeState({ year: value })
                                }
                                }
                            />
                        </Col>
                        <Col span={6}>
                            <Search placeholder="Tên đơn vị hành chính" onSearch={onSearch} style={{ width: '100%' }} />
                        </Col>

                    </Row>
                </div>
            }
                extra={<Button
                    onClick={createOrUpdateModalOpen}
                    type="primary"
                    icon={<PlusOutlined />}>
                    Tạo mới
                </Button>}>
                <Table
                    rowKey="id"
                    bordered={true}
                    pagination={{
                        pageSize: state?.maxResultCount,
                        total: dataDvhc === undefined ? 0 : dataDvhc.totalCount,
                        defaultCurrent: 1,
                    }}
                    columns={columns}
                    loading={false}
                    dataSource={dataDvhc === undefined ? [] : dataDvhc?.items}
                    onChange={handleTableChange}
                />
                <CreateOrUpdateDVHC
                    title='Cấp đơn vị hành chính'
                    formRef={formRef}
                    visible={editingModalData.modalVisible}
                    confirmLoading={false}
                    entity={editingModalData.editingEntity}
                    onCancel={() => {
                        setEditingModalData({
                            modalVisible: false
                        })
                    }}
                    onCreate={onCreateOrUpdateDvhc}
                />
            </Card>
        </div>
    )
}
