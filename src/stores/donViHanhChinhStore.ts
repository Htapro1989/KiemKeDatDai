import DonViHanhChinhMenu from '../models/DonViHanhChinh/DonViHanhChinhMenu';
import { action, observable } from "mobx";
import dvhcService from '../services/dvhc/dvhcService';
import { DonViHanhChinhMapper } from '../mapper/DonViHanhChinhMapper';
import { DmKyKiemKeMapper } from '../mapper/DMKyKiemKeMapper';
import { DMKyKiemKeDto } from '../models/DMKyKiemKe/DmKyKiemKeDto';

class DonViHanhChinhStore {
    @observable donViHanhChinhList: DonViHanhChinhMenu[] = [];
    @observable isFetchingDonViHanhChinh: boolean = false;
    @observable dmKyKiemKe: DMKyKiemKeDto[] = [];
    @observable dmKyKiemKeSelected: string = '';
    @observable donViHanhChinhSelected: any;
    @observable donViHanhChinhOfUser: any
    @observable sideMenuExpanedKeys: any;

    @action
    async fetchDonViHanhChinhList(userId: any) {

        this.isFetchingDonViHanhChinh = true;

        const dmKyKiemKeResponse = await dvhcService.getDMKyKiemKe()
        if (!dmKyKiemKeResponse || dmKyKiemKeResponse.code != 1 || dmKyKiemKeResponse.returnValue.length <= 0) return;
        this.dmKyKiemKe = dmKyKiemKeResponse.returnValue.map(DmKyKiemKeMapper.toDmKyKiemKeDto);
        this.dmKyKiemKeSelected = this.dmKyKiemKe[0].value;

        const dvhcByUserResponse = await dvhcService.getByUser(userId, this.dmKyKiemKeSelected)
        if (!dvhcByUserResponse || dvhcByUserResponse.code != 1 || dvhcByUserResponse.returnValue.length <= 0) return;

        this.donViHanhChinhList = dvhcByUserResponse.returnValue
            .map(DonViHanhChinhMapper.toDonViHanhChinhMenu);

        this.donViHanhChinhSelected = this.donViHanhChinhList?.[0];
        this.donViHanhChinhOfUser = this.donViHanhChinhList?.[0];
        this.isFetchingDonViHanhChinh = false;

        //load root parent child and default expand
        await this.fetchDonViHanhChinhListByParentKey(this.donViHanhChinhSelected?.id)
    }

    @action
    async fetchDonViHanhChinhListByParentKey(parentId: string) {
        if (!parentId) return;
        const dvhcByParentIdResponse = await dvhcService.getByParentId(parentId)
        if (!dvhcByParentIdResponse || dvhcByParentIdResponse.code != 1 || dvhcByParentIdResponse.returnValue.length <= 0) return;

        const children = dvhcByParentIdResponse.returnValue.map(DonViHanhChinhMapper.toDonViHanhChinhMenu);

        const updateTreeData = (list: DonViHanhChinhMenu[], key: string, children: DonViHanhChinhMenu[]): DonViHanhChinhMenu[] =>
            list.map(node => {
                if (node.key == key) {
                    return {
                        ...node,
                        children,
                    };
                }
                if (node.children) {
                    return {
                        ...node,
                        children: updateTreeData(node.children, key, children),
                    };
                }
                return node;
            });

        this.donViHanhChinhList = updateTreeData(this.donViHanhChinhList, parentId, children);
    }

    @action
    async selectDonViHanhChinh(dvhc: any) {
        this.donViHanhChinhSelected = dvhc;
    }

    @action
    async onSetSideMenuExpanedKey(keys: any[]) {
        this.sideMenuExpanedKeys = keys;
    }

    @action
    async fetchAllCapDonViHanhChinh() {
        return dvhcService.getAllCapDVHC()
    }
    @action
    async deleteCapDonViHanhChinh(id: any) {
        return dvhcService.deleteCapDVHC(id);
    }
}

export default DonViHanhChinhStore;