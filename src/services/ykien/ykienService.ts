import { ResponseDto } from "../dto/ResponseDto";
import http from "../httpService";

class YKienService {
    public async getAll(params: any): Promise<ResponseDto<any[]>> {
        let result = await http.get(`/api/services/app/YKien/GetAll`, { params });
        return result.data.result;
    }

    public async createOrUpdate(data: any): Promise<ResponseDto<any[]>> {
        let result = await http.post(`/api/services/app/YKien/CreateOrUpdate`, data);
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