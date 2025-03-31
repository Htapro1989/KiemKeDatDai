import { ResponseDto } from '../dto/ResponseDto';
import http from '../httpService';
import { CapDVHCRequest } from './dto/CapDVHCRequest';
import { DMKyKiemKe } from './dto/DMKyKiemKe';
import { DonViHanhChinh } from './dto/DonViHanhChinh';
import { GetAllDVHCParams } from './dto/GetAllDVHCParams';

class DvhcService {

  public async getDMKyKiemKe(): Promise<ResponseDto<DMKyKiemKe[]>> {
    let result = await http.get(`/api/services/app/DMKyKiemKe/GetAll?MaxResultCount=100&SkipCount=0`);
    return result.data.result;
  }

  public async getByUser(userId: any, year: String): Promise<ResponseDto<DonViHanhChinh[]>> {
    let result = await http.get(`/api/services/app/DanhMucDVHC/GetByUser?userId=${userId}&year=${year}`);
    return result.data.result;
  }


  public async getByParentId(parentId: String): Promise<ResponseDto<DonViHanhChinh[]>> {
    let result = await http.get(`/api/services/app/DanhMucDVHC/GetById?id=${parentId}`);
    return result.data.result;
  }

  public async getAllDVHC(params: GetAllDVHCParams): Promise<ResponseDto<DonViHanhChinh[]>> {
    let result = await http.get(`/api/services/app/DanhMucDVHC/GetAll`, { params });
    return result.data.result;
  }


  public async getBaoCaoDVHC(ma: String, year: String): Promise<ResponseDto<any[]>> {
    const body = {
      ma, year
    }
    let result = await http.post(`/api/services/app/DanhMucDVHC/BaoCaoDVHC`, body);
    return result.data.result;
  }

  //Cấp đơn vị hành chính

  public async getAllCapDVHC(): Promise<ResponseDto<any[]>> {
    let result = await http.get(`/api/services/app/CapDonViHanhChinh/GetAll`);
    return result.data.result;
  }
  public async createOrUpdateCapDVHC(body: CapDVHCRequest): Promise<ResponseDto<any[]>> {
    let result = await http.post(`/api/services/app/CapDonViHanhChinh/CreateOrUpdate`, body);
    return result.data.result;
  }
  public async deleteCapDVHC(id: any): Promise<ResponseDto<any[]>> {
    let result = await http.delete(`/api/services/app/CapDonViHanhChinh/Delete`, { params: { id } });
    return result.data.result;
  }

  //Kỳ kiểm kê
  public async getAllKyKiemKe(): Promise<ResponseDto<any[]>> {
    let result = await http.get(`/api/services/app/DMKyKiemKe/GetAll`);
    return result.data.result;
  }

  public async deleteKyKiemKe(id: any): Promise<ResponseDto<any[]>> {
    let result = await http.delete(`/api/services/app/DMKyKiemKe/Delete`, { params: { id } });
    return result.data.result;
  }
  public async createOrUpdateKyKiemKe(body: any): Promise<ResponseDto<any[]>> {
    let result = await http.post(`/api/services/app/DMKyKiemKe/CreateOrUpdate`, body);
    return result.data.result;
  }

  public async getKyKiemKeAsOption(): Promise<any> {
    let result = await http.get(`/api/services/app/DMKyKiemKe/GetAll`);

    if (result.data.result.returnValue) {
      return result.data.result.returnValue.map((unit: any) => {
        return {
          value: unit.year,
          label: unit.name
        }
      })
    }
    return []
  }

  //Cấu hình hệ thống
  public async getAllCauHinh(): Promise<ResponseDto<any[]>> {
    let result = await http.get(`/api/services/app/ConfigSystem/GetAll`);
    return result.data.result;
  }
  public async updateCauHinh(body: any): Promise<ResponseDto<any[]>> {
    let result = await http.post(`/api/services/app/ConfigSystem/CreateOrUpdate`, body);
    return result.data.result;
  }

  //DON VI HANH CHINH DROPDOWN
  public async getDropDownVung(): Promise<any[]> {
    let result = await http.get(`/api/services/app/DanhMucDVHC/GetDropDownVung`);
    if (result.data.result.returnValue) {
      return result.data.result.returnValue.map((unit: any) => {
        return {
          value: unit.ma,
          label: unit.name
        }
      })
    }
    return [];
  }
  public async getDropDownTinh(maVung: string): Promise<any[]> {
    let result = await http.get(`/api/services/app/DanhMucDVHC/GetDropDownTinhByMaVung`, { params: { ma: maVung } });
    if (result.data.result.returnValue) {
      return result.data.result.returnValue.map((unit: any) => {
        return {
          value: unit.ma,
          label: unit.name
        }
      })
    }
    return [];
  }
  public async getDropDownHuyen(maTinh: any): Promise<any[]> {
    let result = await http.get(`/api/services/app/DanhMucDVHC/GetDropDownHuyenByMaTinh`, { params: { ma:maTinh } });
    if (result.data.result.returnValue) {
      return result.data.result.returnValue.map((unit: any) => {
        return {
          value: unit.ma,
          label: unit.name
        }
      })
    }
    return [];
  }
  public async getDropDownXa(maHuyen: any): Promise<any[]> {
    let result = await http.get(`/api/services/app/DanhMucDVHC/GetDropDownXaByMaHuyen`, { params: { ma:maHuyen } });
    if (result.data.result.returnValue) {
      return result.data.result.returnValue.map((unit: any) => {
        return {
          value: unit.ma,
          label: unit.name
        }
      })
    }
    return [];
  }
  public async getDropDownCapDVHC(): Promise<any> {
    let result = await http.get(`/api/services/app/CapDonViHanhChinh/GetAll`);
    if (result.data.result.returnValue) {
      return result.data.result.returnValue.map((unit: any) => {
        return {
          value: unit.maCapDVHC,
          label: unit.name
        }
      })
    }
    return [];
  }
}

export default new DvhcService();