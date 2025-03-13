import { CAP_DVHC_ENUM } from "../../models/enum";
import { ResponseDto } from "../dto/ResponseDto";
import http from '../httpService';

class BaoCaoService {
    public async nopBaoCao(year: String): Promise<ResponseDto<any[]>> {
        let result = await http.post(`/api/services/app/BaoCao/NopBaoCao?year=${year}`);
        return result.data.result;
    }


    public async huyDuyetBaoCao(capDVHCId: string, ma: string, year: String): Promise<ResponseDto<any> | undefined> {

        if (capDVHCId == CAP_DVHC_ENUM.HUYEN) {
            return await this.huyenHuyDuyetBaoCaoXa(ma, year);
        } else if (capDVHCId == CAP_DVHC_ENUM.TINH) {
            return await this.tinhHuyDuyetBaoCaoHuyen(ma, year);
        } else if (capDVHCId == CAP_DVHC_ENUM.TRUNG_UONG) {
            return await this.trungUongHuyDuyetBaoCaoTinh(ma, year);
        }
        return undefined;
    }
    public async duyetBaoCao(capDVHCId: string, ma: string, year: String): Promise<ResponseDto<any> | undefined> {

        if (capDVHCId == CAP_DVHC_ENUM.HUYEN) {
            return await this.huyenDuyetBaoCaoXa(ma, year);
        } else if (capDVHCId == CAP_DVHC_ENUM.TINH) {
            return await this.tinhDuyetBaoCaoHuyen(ma, year);
        } else if (capDVHCId == CAP_DVHC_ENUM.TRUNG_UONG) {
            return await this.trungUongDuyetBaoCaoTinh(ma, year);
        }
        return undefined;
    }



    public async huyenDuyetBaoCaoXa(ma: string, year: String): Promise<ResponseDto<any[]>> {
        let result = await http.post(`/api/services/app/Huyen/DuyetBaoCaoXa?ma=${ma}&year=${year}`);
        return result.data.result;
    }

    public async huyenHuyDuyetBaoCaoXa(ma: string, year: String): Promise<ResponseDto<any[]>> {
        let result = await http.post(`/api/services/app/Huyen/HuyDuyetBaoCaoXa?ma=${ma}&year=${year}`);
        return result.data.result;
    }


    public async tinhDuyetBaoCaoHuyen(ma: string, year: String): Promise<ResponseDto<any[]>> {
        let result = await http.post(`/api/services/app/Tinh/DuyetBaoCaoHuyen?ma=${ma}&year=${year}`);
        return result.data.result;
    }

    public async tinhHuyDuyetBaoCaoHuyen(ma: string, year: String): Promise<ResponseDto<any[]>> {
        let result = await http.post(`/api/services/app/Tinh/HuyDuyetBaoCaoHuyen?ma=${ma}&year=${year}`);
        return result.data.result;
    }

    public async trungUongDuyetBaoCaoTinh(ma: string, year: String): Promise<ResponseDto<any[]>> {
        let result = await http.post(`/api/services/app/TrungUong/DuyetBaoCaoTinh?ma=${ma}&year=${year}`);
        return result.data.result;
    }

    public async trungUongHuyDuyetBaoCaoTinh(ma: string, year: String): Promise<ResponseDto<any[]>> {
        let result = await http.post(`/api/services/app/TrungUong/HuyDuyetBaoCaoTinh?ma=${ma}&year=${year}`);
        return result.data.result;
    }


}

export default new BaoCaoService();