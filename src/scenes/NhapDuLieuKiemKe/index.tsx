import React, { useContext, useEffect, useState } from 'react'
import './index.less'
import { Button, Card, Empty, notification, Select, Table } from 'antd'
import CustomModal from '../Home/components/CustomModal';
import Bieu06Input from './components/Bieu06Input';
import Bieu02Input from './components/Bieu02Input';
import { MobXProviderContext } from 'mobx-react';
import dvhcService from '../../services/dvhc/dvhcService';
// import { CAP_DVHC_ENUM } from '../../models/enum';
import utils from '../../utils/utils';
import UploadFileButton from '../../components/Upload';
import confirm from 'antd/lib/modal/confirm';
import { ExclamationCircleOutlined } from '@ant-design/icons';
import fileService from '../../services/files/fileService';
import { CAP_DVHC_ENUM } from '../../models/enum';

function useStores() {
    return useContext(MobXProviderContext);
}

export default function NhapDuLieuKiemKePage() {

    const [isShowModal, setIsShowModal] = useState(false)
    const [isShowModal02, setIsShowModal02] = useState(false)
    const [tableData, setTableData] = useState<any>([])

    const { donViHanhChinhStore } = useStores();
    const dvhcId = donViHanhChinhStore?.donViHanhChinhOfUser?.id
    const capDVHCId = donViHanhChinhStore?.donViHanhChinhOfUser?.capDVHCId

    const [isLoadingBieuId, setIsLoadingBieuId] = useState("")

    const [dvhcState, setDvhcState] = useState<any>()
    const updateState = (newState: Partial<any>) => {
        setDvhcState((prevState: any) => ({
            ...prevState,
            ...newState
        }))
    }
    const fetchKyKiemKe = async (year?: any) => {
        const kyOptions = await dvhcService.getKyKiemKeAsOption();
        const kyKiemKeSelected = kyOptions.find((item: any) => item.value == year)?.value || kyOptions[0]?.value;

        updateState({
            kyKiemKeOptions: kyOptions,
            dmKyKiemKeSelected: kyKiemKeSelected,
        })
    }

    const onUploadFile = async (file: any, id: any) => {
        confirm({
            title: `Nhập mới dữ liệu kiểm kê cho biểu này?`,
            icon: <ExclamationCircleOutlined />,
            content: 'Dữ liệu cũ sẽ bị xóa và tạo mới lại.',
            okText: 'Có',
            okType: 'primary',
            cancelText: 'Hủy',
            onOk() {
                onUploadFileDvhc(file, id)
            }
        });
    }
    const onUploadFileDvhc = async (file: any, id: any) => {
        setIsLoadingBieuId(id)
        const response = await fileService.uploadBieuDvhc(
            id, dvhcId, dvhcState.dmKyKiemKeSelected, file
        );
        if (response) {
            notification.success({ message: "Upload thành công" })
        } else {
            notification.error({ message: "Thất bại. Vui lòng thử lại sau" })
        }
        setIsLoadingBieuId("")
    }


    const columns: any = [
        { title: 'Ký hiệu', dataIndex: 'kyHieu', key: 'kyHieu' },
        { title: 'Tên', dataIndex: 'name', key: 'name' },
        {
            title: 'Nhập báo cáo', dataIndex: 'input', key: 'input',
            render: (text: string, item: any) => (
                <div>
                    <Button type='primary' style={{ marginRight: 4 }}
                        onClick={() => {
                            if (!utils.checkQuyenAction("Pages.Report.NhapBieu")) {
                                return
                            }
                            if (item.id == 1)
                                setIsShowModal(true)
                            else setIsShowModal02(true)
                        }}
                    >Biểu mẫu</Button>
                    <UploadFileButton
                        loading={isLoadingBieuId == item.kyHieu}
                        style={{ marginBottom: 24 }}
                        title='Excel' type='primary' ghost
                        hideFileSelected={true}
                        accept="*"
                        onUpload={(file) => onUploadFile(file, item.kyHieu)} />
                </div>
            ),
        },

    ];



    useEffect(() => {
        fetchKyKiemKe()
    }, [])
    console.log("CAP ", donViHanhChinhStore?.donViHanhChinhOfUser)
    useEffect(() => {
        
        if (capDVHCId == CAP_DVHC_ENUM.TINH) {
            setTableData([
                {
                    "id": 1,
                    "kyHieu": "06/TKKKQPAN",
                    'name': 'Thống kê, kiểm kê đất quốc phòng, đất an ninh',
                },
                {
                    "id": 2,
                    "kyHieu": "02a/KKNLT",
                    'name': 'Kiểm kê tình hình đo đạc, cấp giấy chứng nhận và hình thức sử dụng đất của các công ty nông, lâm nghiệp',
                }
            ])
        }
    }, [capDVHCId])


    const onHandleChangeKyKiemKe = (value: any) => {
        updateState({
            dmKyKiemKeSelected: value,
        })
    }

    return (
        <div className='dulieukiemke-page-wrapper'>
            <h1 className='txt-page-header'>Nhập dữ kiệu kiểm kê</h1>
            <Card title={(<Select
                value={dvhcState?.dmKyKiemKeSelected}
                onChange={onHandleChangeKyKiemKe}
                options={dvhcState?.kyKiemKeOptions}
            />)}>
                <Table
                    rowKey={"id"}
                    bordered={true}
                    columns={columns}
                    size='small'
                    pagination={false}
                    loading={false}
                    dataSource={tableData}
                    locale={{
                        emptyText: (
                            <Empty description="Chỉ đơn vị hành chính cấp tỉnh mới được nhập dữ liệu này"> </Empty>
                        ),
                    }}
                />
            </Card>

            <CustomModal
                onClose={() => setIsShowModal(false)}
                visible={isShowModal} >
                <Bieu06Input isRefresh={isShowModal} dvhcId={dvhcId} year={dvhcState?.dmKyKiemKeSelected} />
            </CustomModal>
            <CustomModal
                onClose={() => setIsShowModal02(false)}
                visible={isShowModal02} >
                <Bieu02Input isRefresh={isShowModal02} dvhcId={dvhcId} year={dvhcState?.dmKyKiemKeSelected} />
            </CustomModal>

        </div>
    )
}
