import './index.less'
import React, { useEffect, useState } from 'react'
import Bieu01TKKK from './DanhSachBieuMau/Bieu01TKKK'
import Bieu02TKKK from './DanhSachBieuMau/Bieu02TKKK';
import Bieu03TKKK from './DanhSachBieuMau/Bieu03TKKK';
import Bieu04TKKK from './DanhSachBieuMau/Bieu04TKKK';
import Bieu05TKKK from './DanhSachBieuMau/Bieu05TKKK';
import Bieu06TKKKQPAN from './DanhSachBieuMau/Bieu06TKKKQPAN';
import Bieu01KKSL from './DanhSachBieuMau/Bieu01KKSL';
import Bieu02KKSL from './DanhSachBieuMau/Bieu02KKSL';
import Bieu01aKKNLT from './DanhSachBieuMau/Bieu01aKKNLT';
import Bieu01bKKNLT from './DanhSachBieuMau/Bieu01bKKNLT';
import Bieu01cKKNLT from './DanhSachBieuMau/Bieu01cKKNLT';
import Bieu02KKKNLT from './DanhSachBieuMau/Bieu02KKKNLT';
import bieuMauService from '../../services/bieuMau/bieuMauService';
import ChiTietBieuMauRequest from '../../models/BieuMau/ChiTietBieuMauRequest';
import PhuLucIII from './DanhSachBieuMau/PhuLucIII';
import PhuLucIV from './DanhSachBieuMau/PhuLucIV';




interface IBieuMauProps {
  bieuMauRequest?: ChiTietBieuMauRequest;
  isReload: boolean
}


const BieuMauPage = (props: IBieuMauProps) => {

  const { capDVHC, loaiBieuMau, maDVHC, year } = props.bieuMauRequest!

  const [isFetchingData, setIsFetchingData] = useState(false)
  const [reportData, setReportData] = useState<any>()

  const getBieuMauDetail = async () => {
    setIsFetchingData(true)
    const response = await bieuMauService.getDetailBieuMau(loaiBieuMau, capDVHC, year, maDVHC);
    if (!response || response.code != 1) {
      setIsFetchingData(false)
      setReportData(null)
      return
    };
    setReportData(response.returnValue)
    setIsFetchingData(false)

  }

  useEffect(() => {
    if (props.isReload) {
      getBieuMauDetail();
    }
  }, [props.isReload])

  if (!props.bieuMauRequest) return null;

  const genBieuMau = () => {
    switch (loaiBieuMau) {
      case "01/TKKK":
        return <Bieu01TKKK
          isFetching={isFetchingData} reportData={reportData} />
      case "02/TKKK":
        return <Bieu02TKKK
          isFetching={isFetchingData} reportData={reportData} />
      case "03/TKKK":
        return <Bieu03TKKK
          isFetching={isFetchingData} reportData={reportData} />
      case "04/TKKK":
        return <Bieu04TKKK isFetching={isFetchingData} reportData={reportData} />
      case "05/TKKK":
        return <Bieu05TKKK isFetching={isFetchingData} reportData={reportData} />
      case "06/TKKKQPAN":
        return <Bieu06TKKKQPAN isFetching={isFetchingData} reportData={reportData} />
      case "01/KKSL":
        return <Bieu01KKSL />
      case "02/KKSL":
        return <Bieu02KKSL />
      case "01a/KKNLT":
        return <Bieu01aKKNLT />
      case "01b/KKNLT":
        return <Bieu01bKKNLT />
      case "01c/KKNLT":
        return <Bieu01cKKNLT />
      case "01c/KKNLT":
        return <Bieu02KKKNLT />
      case "PL.III":
        return <PhuLucIII isFetching={isFetchingData} reportData={reportData} />
      case "PL.IV":
        return <PhuLucIV isFetching={isFetchingData} reportData={reportData} />

      default:
        return null;
    }
  }

  return (
    <div className='bieu-mau-page-wrapper'>
      {genBieuMau()}
    </div>
  )
}

export default BieuMauPage;
