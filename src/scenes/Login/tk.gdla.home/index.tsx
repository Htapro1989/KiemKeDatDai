import { Button, Card } from 'antd';
import './index.less';
import React from 'react'
import DashBoadComponent from '../dashboad';
import { LinkOutlined } from '@ant-design/icons';
import Footer from '../../../components/Footer';

interface IGdlaHomePageProps {
    onLogin: () => void;
}

export default function GdlaHomePage(props: IGdlaHomePageProps) {

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
                <div>
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
                                <a className='body-text-content' href="https://tk.gdla.gov.vn/Data/Luat_DD_2024.pdf" target="_blank">1. Luật Đất đai 2024</a>
                                <a className='body-text-content' href="https://tk.gdla.gov.vn/Data/102_2024_ND-CP_603982/pdf" target="_blank">2. Nghị định số 102/2024/NĐ-CP của Chính phủ: Quy định chi tiết thi hành một số điều của Luật Đất đai</a>
                                <a className='body-text-content' href="https://tk.gdla.gov.vn/Data/Luat_DD_2024.pdf" target="_blank">3. Chỉ thị số 22/CT-TTg  ngày 23 tháng 7 năm 2024 của Thủ tướng Chính phủ về việc kiểm kê đất đai năm 2024</a>
                                <a className='body-text-content' href="https://tk.gdla.gov.vn/Data/Luat_DD_2024.pdf" target="_blank">4. Quyết định số 817/QĐ-TTg ngày 09 tháng 8 năm 2024 của Thủ tướng Chính phủ về việc Phê duyệt Đề án “Kiểm kê đất đai, lập bản đồ hiện trạng sử dụng đất năm 2024”.</a>
                                <a className='body-text-content' href="https://tk.gdla.gov.vn/Data/Luat_DD_2024.pdf" target="_blank">5. Thông tư số 08/2024/TT-BTNMT ngày 31 tháng 7 năm 2024 của Bộ trưởng Bộ Tài nguyên và Môi trường quy định về thống kê, kiểm kê đất đai và lập bản đồ hiện trạng sử dụng đất</a>
                                <a className='body-text-content' href="https://tk.gdla.gov.vn/Data/Luat_DD_2024.pdf" target="_blank">6. Công văn số 6188/BTNMT-ĐKDLTTĐĐ ngày 12 tháng 9 năm 2024 của Bộ Tài nguyên và Môi trường về lập dự toán kinh phí kiểm kê đất đai, lập bản đồ hiện trạng sử dụng đất năm 2024.</a>
                                <a className='body-text-content' href="https://tk.gdla.gov.vn/Data/Luat_DD_2024.pdf" target="_blank">7.Công văn số 6237/BTNMT-ĐKDLTT ngày 16 tháng 9 năm 2024 của Bộ Tài nguyên và Môi trường về kiểm kê đất đai năm 2024.</a>
                                <a className='body-text-content' href="https://tk.gdla.gov.vn/Data/Luat_DD_2024.pdf" target="_blank">8. Công văn số 6647/BTNMT-QHPTTNĐ ngày 20 tháng 9 năm 2024 của Bộ Tài nguyên và Môi trường về ban hành Định mức kinh tế - kỹ thuật thuộc phạm vi quản lý đất đai tại địa phương.</a>
                                <a className='body-text-content' href="https://tk.gdla.gov.vn/Data/Luat_DD_2024.pdf" target="_blank">9. Công văn 619/BTNMT-ĐKDLTTĐĐ - V/v thực hiện Kiểm kê đất đai năm 2024 </a>
                            </div>
                            <div className='body-bottom'>
                                <Button type='text' className='body-button'>Chi tiết</Button>
                            </div>
                        </div>
                        <div className='gdla-home-page__body__center'>
                            <div className='body-header'>HỆ THỐNG THỐNG KÊ KIỂM KÊ ĐẤT ĐAI</div>
                            <div className='body-content'>
                                <a className='body-text-content' href="https://tk24.vbdlis.vn/4dfe067a-8f8b-417c-a5ee-b72d409e53ee" target="_blank">1. Quy trình thực hiện Thống kê kiểm kê đất đai năm 2024 </a>
                                <a className='body-text-content' href="../Data/619-btnmt-dkdlttdd_Signed.pdf" target="_blank">1.1. Tài liệu thực hiện kiểm kê đất đai năm 2024 cấp xã</a>
                                <a className='body-text-content' href="../Data/619-btnmt-dkdlttdd_Signed.pdf" target="_blank">2. Tài liệu hướng dẫn sử dụng phần mềm kiểm kê đất đai cấp xã</a>
                                <a className='body-text-content' href="../Data/619-btnmt-dkdlttdd_Signed.pdf" target="_blank">3. Tài liệu hướng dẫn kiểm tra và giao nộp kết quả kiểm kê</a>
                                <a className='body-text-content' href="../Data/619-btnmt-dkdlttdd_Signed.pdf" target="_blank">5. Tài liệu khác </a>
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
                                <a className='body-text-content' href="../Data/619-btnmt-dkdlttdd_Signed.pdf" target="_blank">1. Xác định loại đất</a>
                                <a className='body-text-content' href="../Data/619-btnmt-dkdlttdd_Signed.pdf" target="_blank">2. Địa giới đơn vị hành chính</a>
                                <a className='body-text-content' href="../Data/619-btnmt-dkdlttdd_Signed.pdf" target="_blank">3. Bản đồ kiểm kê đất đai</a>
                                <a className='body-text-content' href="../Data/619-btnmt-dkdlttdd_Signed.pdf" target="_blank">4. Bản đồ hiện trạng sử dụng đất</a>
                                <a className='body-text-content' href="../Data/619-btnmt-dkdlttdd_Signed.pdf" target="_blank">5. Kiểm kê đất có nguồn gốc nông, lâm trường </a>
                                <a className='body-text-content' href="../Data/619-btnmt-dkdlttdd_Signed.pdf" target="_blank">6. Kiểm kê đất khu vực sạt lở, bồi đắp</a>
                                <a className='body-text-content' href="../Data/619-btnmt-dkdlttdd_Signed.pdf" target="_blank">8. Hỏi đáp khác</a>
                            </div>
                            <div className='body-bottom'>
                                <Button type='text' className='body-button'> Gửi ý kiến</Button>
                            </div>
                        </div>
                    </div>
                </Card>
            </div>

            <DashBoadComponent />
            <Footer/>
        </div>
    )
}
