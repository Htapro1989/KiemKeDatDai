import { ResponseDto } from '../dto/ResponseDto';
import http from '../httpService';
import { GetAttactFileParams } from './dto/GetAttactFileParams';

class FileManagerService {
    public async getDanhSachTepDinhKem(parmas: GetAttactFileParams): Promise<ResponseDto<any[]>> {
        let result = await http.get(`/api/services/app/FileKiemKe/GetFileAttachByDVHC`, { params: parmas });
        return result.data.result;
    }

    public async getDanhSachDongBo(parmas: GetAttactFileParams): Promise<ResponseDto<any[]>> {
        let result = await http.get(`/api/services/app/FileKiemKe/GetFileKyThongKeByDVHC`, { params: parmas });
        return result.data.result;
    }

    public async fileSpecificationsUpload(file: any, DVHCId: any, year: any): Promise<ResponseDto<any[]>> {
        const formData = new FormData();
        formData.append('file', file);
        formData.append('DVHCId', DVHCId);
        formData.append('year', year);
        let result = await http.post(`/api/services/app/FileKiemKe/UploadAttachFile`, formData);
        return result?.data?.result
    }
    public async deleteFileAttact(id: any): Promise<ResponseDto<any[]>> {
        let result = await http.delete(`/api/services/app/FileKiemKe/DeleteAttachFile`, { params: { id } });
        return result?.data?.result
    }
    public async downloadAttactFileById(id: any): Promise<ResponseDto<any[]>> {
        let result = await http.get(`/api/services/app/FileKiemKe/DownloadFileByID`, {
            params: { fileId: id },
            responseType: 'blob'
        });
        return result.data;
    }

    public async donwloadBieu(body: any): Promise<ResponseDto<any[]>> {
        let result = await http.post(`/api/services/app/DMBieuMau/DownloadBieuMau`, body, {
            responseType: 'blob'
        });
        return result.data;
    }
}

export default new FileManagerService();