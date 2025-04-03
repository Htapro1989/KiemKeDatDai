export const CAP_DVHC_ENUM = {
    TRUNG_UONG: '0',
    VUNG: '1',
    TINH: '2',
    HUYEN: '3',
    XA: '4',
}

export const getDvhcNameByLevel = (level: any) => {
    switch (level) {
        case 0:
            return "Trung ương"
        case 1:
            return "Vùng"
        case 2:
            return "Tỉnh"
        case 3:
            return "Huyện"
        case 4:
            return "Xã"
    }
    return "";
}