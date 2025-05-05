export interface DonViHanhChinh {
    childStatus?: number;
    tenTinh?: string;
    maTinh?: string;
    tenHuyen?: string | null;
    maHuyen?: string | null;
    tenXa?: string | null;
    maXa?: string | null;
    maVung?: string | null;
    name: string;
    parent_id?: number;
    parent_Code?: string | null;
    capDVHCId?: number;
    active?: boolean;
    year_Id?: number;
    trangThaiDuyet?: number;
    isDeleted?: boolean;
    deleterUserId?: number | null;
    deletionTime?: string | null;
    lastModificationTime?: string | null;
    lastModifierUserId?: number | null;
    creationTime?: string;
    creatorUserId?: number | null;
    id?: number;
    isExitsUser?:any
}