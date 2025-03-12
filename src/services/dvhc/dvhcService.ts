import { ResponseDto } from '../dto/ResponseDto';
import http from '../httpService';
import { DMKyKiemKe } from './dto/DMKyKiemKe';
import { DonViHanhChinh } from './dto/DonViHanhChinh';

class DvhcService {

  public async getDMKyKiemKe(): Promise<ResponseDto<DMKyKiemKe[]>> {
    let result = await http.get(`/api/services/app/DMKyKiemKe/GetAll?MaxResultCount=100&SkipCount=0`);
    return result.data.result;
  }

  public async getByUser(userId: String, year: String): Promise<ResponseDto<DonViHanhChinh[]>> {
    let result = await http.get(`api/services/app/DanhMucDVHC/GetByUser?userId=${userId}&year=${year}`);
    return result.data.result;
  }


  public async getByParentId(parentId: String): Promise<ResponseDto<DonViHanhChinh[]>> {
    let result = await http.get(`api/services/app/DanhMucDVHC/GetById?id=${parentId}`);
    return result.data.result;
  }

  public async getBaoCaoDVHC(ma: String, year: String): Promise<ResponseDto<any[]>> {
    const body = {
      ma, year
    }
    let result = await http.post(`/api/services/app/DanhMucDVHC/BaoCaoDVHC`, body);
    return result.data.result;
  }
}

export default new DvhcService();