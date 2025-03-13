import { Select } from 'antd'
import React, { useEffect, useState } from 'react'
import dvhcService from '../../../services/dvhc/dvhcService';
import { DmKyKiemKeMapper } from '../../../mapper/DMKyKiemKeMapper';
import { DonViHanhChinhMapper } from '../../../mapper/DonViHanhChinhMapper';

interface ISelectKyKiemKeProps {
    userId: any
    onSelectKyKiemKe: (dvhcSelected: any) => void
}

export default function SelectKyKiemKe(props: ISelectKyKiemKeProps) {
    const [kyKiemKeList, setKyKiemKeList] = useState([]);
    const [dmKyKiemKeSelected, setDmKyKiemKeSelected] = useState();

    const handleChange = (value: string) => {
        console.log(`selected ${value}`);
    };

    const getKyKiemKeAndDvhc = async () => {
        const dmKyKiemKeResponse = await dvhcService.getDMKyKiemKe()
        if (!dmKyKiemKeResponse || dmKyKiemKeResponse.code != 1 || dmKyKiemKeResponse.returnValue.length <= 0) return;
        const dmKyKiemKe: any = dmKyKiemKeResponse.returnValue.map(DmKyKiemKeMapper.toDmKyKiemKeDto);

        setKyKiemKeList(dmKyKiemKe)
        setDmKyKiemKeSelected(dmKyKiemKe[0].value)

        const dvhcByUserResponse = await dvhcService.getByUser(props.userId, dmKyKiemKe[0].value)
        if (!dvhcByUserResponse || dvhcByUserResponse.code != 1 || dvhcByUserResponse.returnValue.length <= 0) return;
        const currentDvhcList = dvhcByUserResponse.returnValue
            .map(DonViHanhChinhMapper.toDonViHanhChinhMenu);

        props.onSelectKyKiemKe(currentDvhcList?.[0]);
    }

    useEffect(() => {
        if (!props.userId) return;
        getKyKiemKeAndDvhc()
    }, [props.userId])


    return (
        <Select
            value={dmKyKiemKeSelected}
            onChange={handleChange}
            options={kyKiemKeList}
        />
    )
}
