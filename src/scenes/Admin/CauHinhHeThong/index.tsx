import React from 'react'
import "./index.less";
import AppComponentBase from '../../../components/AppComponentBase';
import { Button, Card, Checkbox, Col, Divider, InputNumber, Radio, Row } from 'antd';
import { FormInstance } from 'antd/lib/form';
import dvhcService from '../../../services/dvhc/dvhcService';
import { RadioChangeEvent } from 'antd/lib/radio';
import { handleCommontResponse } from '../../../services/common/handleResponse';
// import CreateOrUpdateKyKiemKeModal from './components/CreateOrUpdateKyKiemKe';


export interface IQuanLyKyKiemKeProps {
}

export interface IQuanLyKyKiemKeState {
    isLoading: boolean;
    config: {
        id?: any,
        active?: boolean,
        expired_auth?: any,
        isRequiredFileDGN?: any,
        timeUpload?: any

    };
    modalVisible: boolean;
    loading: boolean;
}


class CauHinhHeThongPage extends AppComponentBase<IQuanLyKyKiemKeProps, IQuanLyKyKiemKeState> {
    formRef = React.createRef<FormInstance>();
    state = {
        isLoading: false,
        config: {
            id: -1,
            active: false,
            expired_auth: 1,
            isRequiredFileDGN: false,
            timeUpload: 0
        },
        modalVisible: false,
        loading: false
    }
    async componentDidMount() {
        this.getAll();
    }

    async getAll() {
        this.setState({ isLoading: true })
        const configsResponse = await dvhcService.getAllCauHinh();
        if (configsResponse.code == 1 && configsResponse.returnValue?.length > 0) {
            const configs = configsResponse.returnValue[0];

            let isRequiredFileDGN = false;
            let timeUpload = 0;
            try {
                const jsonConfigSystem = JSON.parse(configs?.jsonConfigSystem || "{}");
                isRequiredFileDGN = jsonConfigSystem?.IsRequiredFileDGN;
                timeUpload = jsonConfigSystem?.TimeUpload;
            } catch (error) {

            }

            this.setState({
                config: {
                    ...configs,
                    isRequiredFileDGN,
                    timeUpload
                },
                isLoading: false
            })
        } else {
            this.setState({ isLoading: false })
        }
    }

    onRadioChange = (e: RadioChangeEvent) => {
        this.setState((oldState) => ({ ...oldState, config: { ...oldState.config, active: e.target.value } }))
    };

    onSave = async () => {
        this.setState({ isLoading: true })
        const response = await dvhcService.updateCauHinh(this.state.config);
        handleCommontResponse(response);
        this.setState({ isLoading: false })
    }
    onChange = (e: any) => {
        this.setState((oldState) => ({ ...oldState, config: { ...oldState.config, isRequiredFileDGN: e.target.checked } }))
    };

    public render() {

        return (
            <div className='capdvhc-page-wrapper'>
                <h1 className='txt-page-header'>Cấu hình hệ thống</h1>

                <Card>
                    <div>
                        <Row align='middle' gutter={24}>
                            <Col span={8}> <span>Thời gian hết hạn Token(Phút): </span></Col>
                            <Col span={16}>
                                <InputNumber min={1} value={this.state.config.expired_auth}
                                    onChange={(value) => { this.setState((oldState) => ({ ...oldState, config: { ...oldState.config, expired_auth: value } })) }} />
                            </Col>
                        </Row>
                        <Row align='middle' gutter={24} style={{ marginTop: 24 }}>
                            <Col span={8}> <span>Trạng thái: </span></Col>
                            <Col span={16}>
                                <Radio.Group onChange={this.onRadioChange} value={this.state.config.active}>
                                    <Radio value={true}>Hoạt động</Radio>
                                    <Radio value={false}>Không hoạt động</Radio>
                                </Radio.Group>
                            </Col>
                        </Row>

                        <Row align='middle' gutter={24} style={{ marginTop: 24 }}>
                            <Col span={8}> <span>Nộp file .dgn: </span></Col>
                            <Col span={16}>
                                <Checkbox
                                    onChange={this.onChange}
                                    checked={this.state.config.isRequiredFileDGN} />
                            </Col>
                        </Row>
                        <Row align='middle' gutter={24} style={{ marginTop: 24 }}>
                            <Col span={8}> <span>Thời gian giữa các lần được tải lên(Phút): </span></Col>
                            <Col span={16}>
                                <InputNumber min={1} value={this.state.config.timeUpload}
                                    onChange={(value) => { this.setState((oldState) => ({ ...oldState, config: { ...oldState.config, timeUpload: value } })) }} />

                            </Col>
                        </Row>
                        <Divider />
                        <Row align='middle' gutter={24} style={{ marginTop: 24 }}>
                            <Col span={6}>
                                <Button loading={this.state.isLoading}
                                    onClick={this.onSave} type='primary'>Lưu</Button>
                            </Col>

                        </Row>

                    </div>
                </Card>


            </div>
        )
    }
}

export default CauHinhHeThongPage;
