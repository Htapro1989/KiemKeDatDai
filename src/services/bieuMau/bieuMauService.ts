import { ResponseDto } from "../dto/ResponseDto";
import http from '../httpService';

class BieuMauService {

    public async getBieuMauByDvhcId(dvhcId: String): Promise<ResponseDto<any>> {
        let result = await http.get(`/api/services/app/DMBieuMau/GetByDVHC?dvhcId=${dvhcId}`);
        return result.data.result;
    }

    public async getDetailBieuMau(loaiBieuMau: String, capDvhc: String, year: String, maDVHC: String): Promise<ResponseDto<any>> {
        let result = await http.get(`/api/services/app/DMBieuMau/GetDetailBieuByKyHieu`, {
            params: {
                KyHieu: loaiBieuMau,
                CapDVHC: capDvhc,
                Year: year,
                MaDVHC: maDVHC,
                MaxResultCount: 1000,
                SkipCount: 0
            }
        });
        return result.data.result;
    }

    public async getBieu06ByDvhc(dvhcId: any, year: any): Promise<ResponseDto<any>> {
        let result = await http.get(`/api/services/app/BieuMau06/GetByDVHC`, { params: { dvhcId, year } });
        return result.data.result;
    }
    public async onCreateOrUpdateBieu06(data: any): Promise<ResponseDto<any>> {
        let result = await http.post(`/api/services/app/BieuMau06/CreateOrUpdate`, data);
        return result.data.result;
    }
    public async onDeleteItemBieu06ById(id: any): Promise<ResponseDto<any>> {
        let result = await http.delete(`/api/services/app/BieuMau06/Delete`, { params: { id } });
        return result.data.result;
    }

    public async getBieu02ByDvhc(dvhcId: any, year: any): Promise<ResponseDto<any>> {
        let result = await http.get(`/api/services/app/BieuMau02aKKNLT/GetByDVHC`, { params: { dvhcId, year } });
        return result.data.result;
    }
    public async onCreateOrUpdateBieu02(data: any): Promise<ResponseDto<any>> {
        let result = await http.post(`/api/services/app/BieuMau02aKKNLT/CreateOrUpdate`, data);
        return result.data.result;
    }
    public async onDeleteItemBieu02ById(id: any): Promise<ResponseDto<any>> {
        let result = await http.delete(`/api/services/app/BieuMau02aKKNLT/Delete`, { params: { id } });
        return result.data.result;
    }

}

export default new BieuMauService();