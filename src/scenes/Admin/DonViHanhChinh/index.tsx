import React from 'react'
import "./index.less";
import AppComponentBase from '../../../components/AppComponentBase';
import { inject, observer } from 'mobx-react';
import Stores from '../../../stores/storeIdentifier';
import DonViHanhChinhStore from '../../../stores/donViHanhChinhStore';
import { Modal } from 'antd';
import { EntityDto } from '../../../services/dto/entityDto';
import { FormInstance } from 'antd/lib/form';
import dvhcService from '../../../services/dvhc/dvhcService';
import { handleCommontResponse } from '../../../services/common/handleResponse';
import SessionStore from '../../../stores/sessionStore';
import DonViHanhChinhTable from './components/DonViHanhChinhTable';

const confirm = Modal.confirm;

export interface IQuanLyDVHCProps {
    donViHanhChinhStore: DonViHanhChinhStore;
    sessionStore: SessionStore;
}

export interface IQuanLyDVHCState {
    isLoading: boolean;
    listCapDvhc: any;
    modalVisible: boolean;
    loading: boolean;
}

@inject(Stores.DonViHanhChinhStore, Stores.SessionStore)
@observer
class QuanLyDonViHanhChinh extends AppComponentBase<IQuanLyDVHCProps, IQuanLyDVHCState> {
    formRef = React.createRef<FormInstance>();
    state = {
        isLoading: false,
        listCapDvhc: [],
        modalVisible: false,
        loading: false
    }
    async componentDidMount() {
        this.getAll();
    }

    async getAll() {
        this.setState({ isLoading: true })
        const listdvhcResponse = await this.props.donViHanhChinhStore.fetchAllCapDonViHanhChinh();
        if (listdvhcResponse.code == 1) {
            this.setState({
                listCapDvhc: listdvhcResponse.returnValue,
                isLoading: false
            })
        } else {
            this.setState({ isLoading: false })
        }
    }
    async createOrUpdateModalOpen(entity?: any) {
        this.Modal();
        setTimeout(() => {
            this.formRef.current?.setFieldsValue({ ...entity });
        }, 100);
    }
    async deleteCapDVHC(id: any) {
        await this.props.donViHanhChinhStore.deleteCapDonViHanhChinh(id);
        this.getAll()
    }

    delete(input: EntityDto) {
        const self = this;
        confirm({
            title: 'Bạn muốn xóa hàng này',
            onOk() {
                self.deleteCapDVHC(input.id)
            },
            onCancel() {
                console.log('Cancel');
            },
        });
    }

    Modal = () => {
        this.setState({
            modalVisible: !this.state.modalVisible,
        });
    };

    handleCreate = () => {
        const form = this.formRef.current;
        form!.validateFields().then(async (values: any) => {
            this.setState({ loading: true })
            const response = await dvhcService.createOrUpdateCapDVHC(values)
            handleCommontResponse(response);
            this.setState({ modalVisible: false });
            await this.getAll();
            form!.resetFields();
            this.setState({ loading: false })
        });
    }


    public render() {
        const { id } = this.props.sessionStore.currentLogin.user
        return (
            <div className='capdvhc-page-wrapper'>
                <h1 className='txt-page-header'>Đơn vị hành chính</h1>
                <DonViHanhChinhTable userId={id} />
            </div>
        )
    }
}

export default QuanLyDonViHanhChinh;
