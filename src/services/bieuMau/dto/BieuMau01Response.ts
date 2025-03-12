
export interface BieuMau01Response {
    stt?: string;
    loaiDat?: string;
    ma?: string;
    tongDienTichDVHC?: number;
    tongSoTheoDoiTuongSuDung?: number;
    caNhanTrongNuoc_CNV?: number;
    nguoiVietNamONuocNgoai_CNN?: number;
    coQuanNhaNuoc_TCN?: number;
    donViSuNghiep_TSN?: number;
    toChucXaHoi_TXH?: number;
    toChucKinhTe_TKT?: number;
    toChucKhac_TKH?: number;
    toChucTonGiao_TTG?: number;
    congDongDanCu_CDS?: number;
    toChucNuocNgoai_TNG?: number;
    nguoiGocVietNamONuocNgoai_NGV?: number;
    toChucKinhTeVonNuocNgoai_TVN?: number;
    tongSoTheoDoiTuongDuocGiaoQuanLy?: number;
    coQuanNhaNuoc_TCQ?: number;
    donViSuNghiep_TSQ?: number;
    toChucKinhTe_KTQ?: number;
    congDongDanCu_CDQ?: number;
    maTinh?: string | null;
    tinhId?: number;
    year?: number;
    active?: boolean;
    isDeleted?: boolean;
    deleterUserId?: number | null;
    deletionTime?: string | null;
    lastModificationTime?: string;
    lastModifierUserId?: number;
    creationTime?: string;
    creatorUserId?: number;
    id?: number;
}
