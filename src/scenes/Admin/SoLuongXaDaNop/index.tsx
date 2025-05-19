import React from 'react'
import "./index.less";
import AppComponentBase from '../../../components/AppComponentBase';
import { Card, Col, DatePicker, Row, Spin } from 'antd';
import { FormInstance } from 'antd/lib/form';
import baoCaoService from '../../../services/baoCao/baoCaoService';
import moment from 'moment';
const dateFormat = 'MM-DD-YYYY';


export interface IQuanLyKyKiemKeProps {
}

export interface IQuanLyKyKiemKeState {
    isLoading: boolean;
    loading: boolean;
    soLuongXaDaNop: any;
    startDate: any;
    endDate: any
}


class CauHinhXaDaNopPage extends AppComponentBase<IQuanLyKyKiemKeProps, IQuanLyKyKiemKeState> {
    formRef = React.createRef<FormInstance>();
    state = {
        isLoading: false,
        loading: false,
        soLuongXaDaNop: 0,
        startDate: "01-01-2024",
        endDate: "01-01-2026"
    }
    async componentDidMount() {
        this.getAllData()
    }

    async getAllData(startDate?: any, endDate?: any) {
        let start = startDate ? startDate : this.state.startDate;
        let end = endDate ? endDate : this.state.endDate;
        this.setState({ isLoading: true })

        const resp = await baoCaoService.soLuongXaDaNop(start, end);
        if (resp.code == 1) {
            this.setState({
                soLuongXaDaNop: resp.returnValue
            })
        }
        this.setState({ isLoading: false })
    }

    onChange = (value: any, dateString: any) => {
        this.setState({
            endDate: dateString?.[1],
            startDate: dateString?.[0],
        })
        if (dateString) {
            this.getAllData(dateString?.[0], dateString?.[1])
        }
    }

    public render() {

        return (
            <div className='capdvhc-page-wrapper'>
                <h1 className='txt-page-header'>Thông tin tổng hợp</h1>

                <Card title={(<DatePicker.RangePicker
                    defaultValue={[moment(this.state.startDate, dateFormat), moment(this.state.endDate, dateFormat)]}
                    onChange={this.onChange}
                    format={dateFormat}
                />)}>
                    <div>
                        <Row align='middle' gutter={24}>
                            <Col span={8}> <span>Số lượng xã đã nộp: </span></Col>
                            <Col span={16}>
                                {this.state.isLoading ? <Spin /> : <div>{this.state.soLuongXaDaNop}</div>}
                            </Col>
                        </Row>

                    </div>
                </Card>


            </div>
        )
    }
}

export default CauHinhXaDaNopPage;
