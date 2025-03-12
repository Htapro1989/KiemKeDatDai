import { Table } from 'antd'
import React, { useEffect, useState } from 'react'
import bieuMauService from '../../../services/bieuMau/bieuMauService';
import { useHistory } from 'react-router-dom';


export interface IBieuDoTabProps {
    donViHanhChinhSelected: any;
}

export default function BieuDoTab(props: IBieuDoTabProps) {

    const [listBaoCao, setListBaoCao] = useState([])
    const [isFetchBaoCao, setIsFetchBaoCao] = useState(false)
    const history = useHistory()
    // const [isShowReportModal, setIsShowReportModal] = useState(false)

    const columns = [
        { title: 'Ký hiệu', dataIndex: 'kyHieu', key: 'kyHieu', width: 150, render: (text: string) => <div>{text}</div> },
        { title: 'Tên', dataIndex: 'noiDung', key: 'noiDung', render: (text: string) => <div>{text}</div> },
        { title: 'Tải báo cáo', dataIndex: 'taiBaoCao', key: 'taiBaoCao', width: 120 },
        {
            title: 'Xem báo cáo', dataIndex: 'xemBaoCao', key: 'xemBaoCao', width: 120,
            render: (text: string, record: any) => <a onClick={() => openReport(record)} target="_blank">Xem</a>
        },
    ]

    const openReport = (record: any) => {
        const reportObject = {
            loaiBieuMau: record?.kyHieu,
            capDVHC: '4',
            maDVHC: '06493',
            year: props.donViHanhChinhSelected.year
        }

        // loaiBieuMau: record?.kyHieu,
        // capDVHC: props.donViHanhChinhSelected.capDVHCId,
        // maDVHC: props.donViHanhChinhSelected.ma,
        // year: props.donViHanhChinhSelected.year

        // setIsShowReportModal(true)
        // console.log("xxx", history.length, reportObject, props)
        const encodedData = btoa(JSON.stringify(reportObject));
        history.push(`/report/${encodedData}`)

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

    if (!props.donViHanhChinhSelected) return <div />;
    return (
        <div>
            <Table
                rowKey="id"
                bordered={true}
                // pagination={{ pageSize: this.state.maxResultCount, total: roles === undefined ? 0 : roles.totalCount, defaultCurrent: 1 }}
                columns={columns}
                loading={isFetchBaoCao}
                dataSource={listBaoCao}
                scroll={{ y: 420 }}
                size={"small"}
                pagination={{ pageSize: 50 }}
            // onChange={this.handleTableChange}
            />
            {/* <CustomModal visible={false} >
                <Bieu04TKKK />
            </CustomModal>
            <Modal visible={false}
                width="100vw"
                style={{ top: 0, }}
                bodyStyle={{ height: "100vh" }}
                footer={null}
                getContainer={false}
            >
                <Bieu04TKKK />
            </Modal> */}
        </div>
    )
}
