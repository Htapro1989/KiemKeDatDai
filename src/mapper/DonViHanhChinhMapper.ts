import DonViHanhChinhMenu from "../models/DonViHanhChinh/DonViHanhChinhMenu";
import { DonViHanhChinh } from "../services/dvhc/dto/DonViHanhChinh";

export const DonViHanhChinhMapper = {
    toDonViHanhChinhMenu: (dvhc: DonViHanhChinh): DonViHanhChinhMenu => {
        return {
            key: String(dvhc.id),
            title: dvhc.name,
            isLeaf: dvhc.childStatus !== 1,
            // className: `tree-menu-item-${dvhc.trangThaiDuyet}`,
            ...dvhc
        };
    }
};

