import { ResponseDto } from "../dto/ResponseDto";
import http from '../httpService';

class BieuMauService {

    public async getBieuMauByDvhcId(dvhcId: String): Promise<ResponseDto<any>> {
        let result = await http.get(`/api/services/app/DMBieuMau/GetByDVHC?dvhcId=${dvhcId}`);
        return result.data.result;
    }

    public async getDetailBieuMau(loaiBieuMau: String, capDvhc: String, year: String, maDVHC: String): Promise<ResponseDto<any>> {
        let result = await http.get(`/api/services/app/DMBieuMau/GetDetailBieuByKyHieu?KyHieu=${loaiBieuMau}&CapDVHC=${capDvhc}&Year=${year}&MaDVHC=${maDVHC}`);
        return result.data.result;
    }

}

export default new BieuMauService();