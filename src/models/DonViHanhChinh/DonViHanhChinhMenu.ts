interface DonViHanhChinhMenu {
    key?: string;
    title?: any;
    children?: DonViHanhChinhMenu[];
    isLeaf?: boolean;

    //extra
    childStatus?: number;
    tenTinh?: string;
    maTinh?: string;
    tenHuyen?: string | null;
    maHuyen?: string | null;
    tenXa?: string | null;
    maXa?: string | null;
    name: string;
    parent_id?: number;
    parent_Code?: string | null;
    capDVHCId?: number;
    active?: boolean;
    year_Id?: number;
    trangThaiDuyet?: number;
    id?: number;
    className?: any

}
export default DonViHanhChinhMenu;

