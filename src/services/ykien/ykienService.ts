import { ResponseDto } from "../dto/ResponseDto";
import http from "../httpService";

class YKienService {
    public async getAll(params: any): Promise<ResponseDto<any[]>> {
        let result = await http.get(`/api/services/app/YKien/GetAll`, { params });
        return result.data.result;
    }

    public async createOrUpdate(data: any): Promise<ResponseDto<any[]>> {
        console.log('data', data);
        const formData = new FormData();
        formData.append('name', data.name);
        formData.append('email', data.email);
        formData.append('donViCongTac', data.donViCongTac);
        formData.append('phone', data.phone);
        formData.append('noiDungYKien', data.noiDungYKien);
        formData.append('file', data.file);
        formData.append('year', data.year);

        let result = await http.post(`/api/services/app/YKien/CreateOrUpdate`, formData);
        return result.data.result;
    }
    public async downloadFileByID(fileId: any): Promise<ResponseDto<any[]>> {
        let result = await http.get(`/api/services/app/YKien/DownloadFileById`, {
            params: { fileId },
            responseType: 'blob'
        });
        return result.data;
    }
}

export default new YKienService();