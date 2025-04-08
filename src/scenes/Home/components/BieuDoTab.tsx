import { Button, Empty, notification, Table } from 'antd'
import React, { useEffect, useState } from 'react'
import bieuMauService from '../../../services/bieuMau/bieuMauService';
import CustomModal from './CustomModal';
import BieuMauPage from '../../BieuMauPage/bieuMauPage';
import ChiTietBieuMauRequest from '../../../models/BieuMau/ChiTietBieuMauRequest';
import fileService from '../../../services/files/fileService';
import { CloudDownloadOutlined, EyeOutlined } from '@ant-design/icons';
var FileSaver = require('file-saver');

export interface IBieuDoTabProps {
    donViHanhChinhSelected: any;
}

export default function BieuDoTab(props: IBieuDoTabProps) {

    const [listBaoCao, setListBaoCao] = useState([])
    const [isFetchBaoCao, setIsFetchBaoCao] = useState(false)
    const [reportDataInfo, setReportDataInfo] = useState<{ visible: boolean, reportDetailRequest?: ChiTietBieuMauRequest }>({
        visible: false
    })
    const [isDowloading, setIsDowloading] = useState({
        loading: false,
        id: null
    })

    const onDownloadBieu = async (record: any) => {
        try {
            setIsDowloading({ loading: true, id: record.id })
            const response = await fileService.donwloadBieu({
                "kyHieu": record.kyHieu,
                "capDVHC": props.donViHanhChinhSelected?.capDVHCId,
                "year": props.donViHanhChinhSelected?.year,
                "maDVHC": props.donViHanhChinhSelected?.ma
            })

            if (response) {
                FileSaver.saveAs(response, `Bieu ${record.kyHieu}`);
            } else {
                notification.error({ message: "Thất bại. Vui lòng thử lại sau" })
            }
        } finally {
            setIsDowloading({ loading: false, id: record.id })
        }
    }

    const columns: any = [
        { title: 'Ký hiệu', dataIndex: 'kyHieu', key: 'kyHieu', width: 150, render: (text: string) => <div>{text}</div> },
        { title: 'Tên', dataIndex: 'noiDung', key: 'noiDung', render: (text: string) => <div>{text}</div> },
        {
            title: 'Tải báo cáo', dataIndex: 'taiBaoCao', key: 'taiBaoCao', width: 120,
            align: "center",
            render: (text: string, record: any) => <Button
                type='primary'
                onClick={() => onDownloadBieu(record)}
                icon={<CloudDownloadOutlined />}
                loading={isDowloading.loading && isDowloading.id == record.id}>
            </Button>

        },
        {
            title: 'Xem báo cáo', dataIndex: 'xemBaoCao', key: 'xemBaoCao', width: 120, align: "center",
            render: (text: string, record: any) =>
                <Button
                    onClick={() => openReport(record)}
                    icon={<EyeOutlined />}>
                </Button>
        },
    ]
    const openReport = (record: any) => {
        const reportObject = {
            loaiBieuMau: record?.kyHieu,
            capDVHC: props.donViHanhChinhSelected?.capDVHCId,
            maDVHC: props.donViHanhChinhSelected?.ma,
            year: props.donViHanhChinhSelected.year
        }

        setReportDataInfo({
            visible: true,
            reportDetailRequest: reportObject
        })

    }


    const getBieuMau = async () => {
        if (!props.donViHanhChinhSelected) return;
        setIsFetchBaoCao(true)
        const baoCaoResponse = await bieuMauService.getBieuMauByDvhcId(props.donViHanhChinhSelected.key);
        if (!baoCaoResponse || baoCaoResponse.code != 1 || baoCaoResponse.returnValue.length <= 0) {
            setIsFetchBaoCao(false)
            setListBaoCao([]);
            return
        };
        setListBaoCao(baoCaoResponse.returnValue);
        setIsFetchBaoCao(false)
    }

    useEffect(() => {
        getBieuMau();
    }, [props.donViHanhChinhSelected?.key])

    if (!props.donViHanhChinhSelected) return <Empty image={Empty.PRESENTED_IMAGE_SIMPLE} description="Không có dữ liệu" />;
    return (
        <div>
            <Table
                rowKey="id"
                bordered={true}
                columns={columns}
                loading={isFetchBaoCao}
                dataSource={listBaoCao}
                pagination={false}
            />

            <CustomModal
                onClose={() => setReportDataInfo({ visible: false })}
                visible={reportDataInfo.visible} >
                <BieuMauPage isReload={reportDataInfo.visible} bieuMauRequest={reportDataInfo.reportDetailRequest} />
            </CustomModal>
        </div>
    )
}
