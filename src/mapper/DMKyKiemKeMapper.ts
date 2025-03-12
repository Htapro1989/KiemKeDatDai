import { DMKyKiemKeDto } from "../models/DMKyKiemKe/DmKyKiemKeDto";
import { DMKyKiemKe } from "../services/dvhc/dto/DMKyKiemKe";

export const DmKyKiemKeMapper = {
    toDmKyKiemKeDto: (kyKiemKe: DMKyKiemKe): DMKyKiemKeDto => {
        return {
            value: kyKiemKe.year,
            label: kyKiemKe.name
        };
    }

}