import { ResponseDto } from "../dto/ResponseDto";
import http from "../httpService";

class NewsService {
    public async getNewsByType(type: any): Promise<ResponseDto<any[]>> {
        let result = await http.get(`/api/services/app/News/GetAll?type=${type}&MaxResultCount=1000`);
        return result.data.result;
    }
    public async getNewsPagging(data: any): Promise<ResponseDto<any[]>> {
        let result = await http.get(`/api/services/app/News/GetAllPaging`, { params: data });
        return result.data.result;
    }
    public async deleteNews(id: any): Promise<ResponseDto<any[]>> {
        let result = await http.delete(`/api/services/app/News/Delete?id=${id}`);
        return result.data.result;
    }
    public async createOrUpdateNews(data: any): Promise<ResponseDto<any[]>> {
        const formData = new FormData();
        formData.append('Title', data?.title);
        formData.append('OrderLabel', data?.orderLabel || '1');
        formData.append('Summary', data?.summary || '1');
        formData.append('Content', data?.content || '1');
        formData.append('Type', data?.type);
        formData.append('Status', data?.status);
        if (data?.fileData) {
            formData.append('File', data?.fileData);
        }
        if (data?.id) {
            formData.append('id', data?.id);
        }

        let result = await http.post(`/api/services/app/News/CreateOrUpdate`, formData);
        return result.data.result;
    }

    public async downloadNewsFileById(fileId: any): Promise<ResponseDto<any[]>> {
        let result = await http.get(`/api/services/app/News/DownloadFileNewsByID`, {
            params: { fileId: fileId },
            responseType: 'blob'
        });
        return result.data;
    }
}

export default new NewsService();