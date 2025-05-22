import { Button, Card, Col, notification, Row } from 'antd';
import './index.less';
import React, { useEffect, useState } from 'react'
import DashBoadComponent from '../dashboad';
import Footer from '../../../components/Footer';
import newsService from '../../../services/news/newsService';
import NewModals from '../components/NewModals';
import CreatePhanHoiModal from '../../Admin/YKienNguoiDung/components/CreatePhanHoiModal';
import { FormInstance } from 'antd/lib/form';
import ykienService from '../../../services/ykien/ykienService';
import FloatingChat from '../components/FloatingChat';
import { LinkOutlined } from '@ant-design/icons';

interface IGdlaHomePageProps {
    onLogin: () => void;
}


export default function GdlaHomePage(props: IGdlaHomePageProps) {
    const [dataType, setDataType] = useState({
        '1': [],
        '2': [],
        '3': [],
    });
    const [newModalData, setNewModalData] = useState({
        visible: false,
        data: null,
        type: ''
    })

    const formRef = React.createRef<FormInstance>();
    const [isShowGuiYKienModal, setIsShowGuiYKienModal] = useState(false)

    const getNewsByType = async (type: string) => {
        const response = await newsService.getNewsByType(type);
        setDataType((prevState) => ({
            ...prevState,
            [type]: response.returnValue
        }))
    }

    const onShowModal = (data: any, type: string) => {
        setNewModalData({
            visible: true,
            data: data,
            type: type
        })
    }

    const handleCreate = async () => {
        const form = formRef.current;
        form!.validateFields().then(async (values: any) => {
            const response = await ykienService.createOrUpdate(values)
            if (response.code == 1) {
                notification.success({ message: response.message })
                form!.resetFields();
                setIsShowGuiYKienModal(false)
            } else {
                notification.error({ message: response.message })
            }
        });
    }

    useEffect(() => {
        getNewsByType('1');
        getNewsByType('2');
        getNewsByType('3');
    }, [])

    return (
        <div className='gdla-home-page'>
            <div className='gdla-home-page__header'>
                <div className='header_logo_layout'>
                    <img style={{ width: 64, height: 64 }} src='https://tk24.vbdlis.vn/assets/LandingPage/logo-de43ecf7.png' />
                </div>
                <div className='header_text_layout'>
                    <p className='header_text_top'>CỤC QUẢN LÝ ĐẤT ĐAI - BỘ NÔNG NGHIỆP VÀ MÔI TRƯỜNG</p>
                    <p className='header_text_bottom'>HỆ THỐNG THỐNG KÊ, KIỂM KÊ ĐẤT ĐAI</p>
                </div>
                <div className='header_text_layout-right'>
                    <div className="header_text_right">
                        HOTLINE : 0366.101.028
                    </div>
                </div>
            </div>
            <div style={{ display: 'flex', flex: 1, flexDirection: 'row', justifyContent: 'center' }}>
                <Card style={{ minWidth: 1400, marginTop: 32 }}>
                    <div className='gdla-home-page__body'>
                        <div className='gdla-home-page__body__left'>
                            <div className='body-header'> VĂN BẢN CHỈ ĐẠO</div>
                            <div className='body-content2'>
                                {
                                    dataType['1']
                                        .filter((item: any) => item.status == 1)
                                        .sort((a: any, b: any) => a.orderLabel - b.orderLabel)
                                        .map((item: any) => (
                                            <a
                                                key={item.id}
                                                className='body-text-content'
                                                onClick={() => onShowModal(item, 'VĂN BẢN CHỈ ĐẠO')}
                                                target="_blank">{item.title}</a>
                                        ))
                                }
                            </div>
                            {/* <div className='body-bottom'>
                                <Button type='text' className='body-button'> </Button>
                            </div> */}
                        </div>
                        <div className='gdla-home-page__body__center'>
                            <div className='body-header'>HỆ THỐNG THỐNG KÊ KIỂM KÊ ĐẤT ĐAI</div>
                            <div className='body-content'>
                                {
                                    dataType['2']
                                        .filter((item: any) => item.status == 1)
                                        .sort((a: any, b: any) => a.orderLabel - b.orderLabel)
                                        .map((item: any) => (
                                            <a key={item.id}
                                                className='body-text-content'
                                                onClick={() => onShowModal(item, 'HỆ THỐNG THỐNG KÊ KIỂM KÊ ĐẤT ĐAI')}
                                                target="_blank">{item.title}</a>
                                        ))
                                }
                            </div>
                            <div>
                                <Row style={{ width: '100%' }}>
                                    <Col flex={1}>
                                        <div>
                                            <div className='button-top'>Phần mềm xử lý số liệu TKKK2024</div>
                                            <div className="arrow-down"></div>
                                            <div className='body-bottom'>
                                                <Button
                                                    type='text'
                                                    className='body-button button-center'
                                                    onClick={props.onLogin}
                                                    icon={<LinkOutlined />}>
                                                    TKKK2024 TỔNG HỢP</Button>
                                            </div>
                                        </div>
                                    </Col>
                                    <Col flex={1}>
                                        <div className='button-top'>
                                            <div>Phần mềm xử lý số kiệu kiểm kê</div>
                                            <div style={{ textAlign: 'center' }}>(TKDestop Basic)</div>
                                        </div>
                                        <div className="arrow-down"></div>
                                        <div className='body-bottom'>
                                            <Button
                                                type='text'
                                                className='body-button button-center'
                                                onClick={() => window.open('https://tk24.vbdlis.vn', '_blank')}
                                                icon={<LinkOutlined />}>
                                                TK ONLINE</Button>
                                        </div>
                                    </Col>
                                </Row>
                            </div>
                        </div>
                        <div className='gdla-home-page__body__right'>
                            <div className='body-header'>TRAO ĐỔI, THẢO LUẬN</div>
                            <div className='body-content'>
                                {
                                    dataType['3']
                                        .filter((item: any) => item.status == 1)
                                        .sort((a: any, b: any) => a.orderLabel - b.orderLabel)
                                        .map((item: any) => (
                                            <a key={item.id}
                                                className='body-text-content'
                                                onClick={() => onShowModal(item, 'TRAO ĐỔI, THẢO LUẬN')}
                                                target="_blank">{item.title}</a>
                                        ))
                                }
                            </div>
                            <div style={{ flex: 1 }}>

                            </div>
                            <div className='bottom-goi-y'>
                                <a
                                    onClick={() => setIsShowGuiYKienModal(true)}
                                    className='body-button'> Gửi ý kiến</a>
                            </div>
                        </div>
                    </div>
                </Card>
            </div>
            <NewModals
                visible={newModalData.visible}
                onClose={() => setNewModalData({ ...newModalData, visible: false })}
                data={newModalData.data}
                type={newModalData.type}
            />
            <DashBoadComponent />
            <CreatePhanHoiModal
                title='Gửi ý kiến'
                formRef={formRef}
                visible={isShowGuiYKienModal}
                confirmLoading={false}
                onCancel={() => {
                    setIsShowGuiYKienModal(false)
                }}
                onCreate={handleCreate}
            />
            <FloatingChat />
            <Footer />
        </div>
    )
}
