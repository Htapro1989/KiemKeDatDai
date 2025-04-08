import { Button, Card } from 'antd';
import './index.less';
import React, { useEffect, useState } from 'react'
import DashBoadComponent from '../dashboad';
import { LinkOutlined } from '@ant-design/icons';
import Footer from '../../../components/Footer';
import newsService from '../../../services/news/newsService';

interface IGdlaHomePageProps {
    onLogin: () => void;
}


export default function GdlaHomePage(props: IGdlaHomePageProps) {
    const [dataType, setDataType] = useState({
        '1': [],
        '2': [],
        '3': [],
    });

    const getNewsByType = async (type: string) => {
        const response = await newsService.getNewsByType(type);
        setDataType((prevState) => ({
            ...prevState,
            [type]: response.returnValue
        }))
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
                    <img src='https://tk.gdla.gov.vn/Images/monre-logo2023.png' />
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
                <Card style={{ maxWidth: 1400, marginTop: 32 }}>
                    <div className='gdla-home-page__body'>
                        <div className='gdla-home-page__body__left'>
                            <div className='body-header'> VĂN BẢN CHỈ ĐẠO</div>
                            <div className='body-content'>
                                {
                                    dataType['1']
                                        .sort((a: any, b: any) => a.orderLabel - b.orderLabel)
                                        .map((item: any) => (
                                            <a key={item.id} className='body-text-content' href={item.link} target="_blank">{item.title}</a>
                                        ))
                                }
                            </div>
                            <div className='body-bottom'>
                                <Button type='text' className='body-button'>Chi tiết</Button>
                            </div>
                        </div>
                        <div className='gdla-home-page__body__center'>
                            <div className='body-header'>HỆ THỐNG THỐNG KÊ KIỂM KÊ ĐẤT ĐAI</div>
                            <div className='body-content'>
                                {
                                    dataType['2']
                                        .sort((a: any, b: any) => a.orderLabel - b.orderLabel)
                                        .map((item: any) => (
                                            <a key={item.id} className='body-text-content' href={item.link} target="_blank">{item.title}</a>
                                        ))
                                }
                            </div>
                            <div className='body-bottom'>
                                <Button
                                    type='text'
                                    className='body-button button-center'
                                    onClick={props.onLogin}
                                    icon={<LinkOutlined />}>
                                    Tổng hợp số liệu kiểm kê online</Button>
                            </div>
                        </div>
                        <div className='gdla-home-page__body__right'>
                            <div className='body-header'>TRAO ĐỔI, THẢO LUẬN</div>
                            <div className='body-content'>
                                {
                                    dataType['3']
                                        .sort((a: any, b: any) => a.orderLabel - b.orderLabel)
                                        .map((item: any) => (
                                            <a key={item.id} className='body-text-content' href={item.link} target="_blank">{item.title}</a>
                                        ))
                                }
                            </div>
                            <div className='body-bottom'>
                                <Button type='text' className='body-button'> Gửi ý kiến</Button>
                            </div>
                        </div>
                    </div>
                </Card>
            </div>

            <DashBoadComponent />
            <Footer />
        </div>
    )
}
