import { Table, Tag } from 'antd';
import { ColumnsType } from 'antd/lib/table';
import React, { useEffect, useState } from 'react'
import dvhcService from '../../../services/dvhc/dvhcService';


interface ITrangThaiDVHCTabProps {
  dvhcRoot: any;
}

export default function TrangThaiDVHCTab(props: ITrangThaiDVHCTabProps) {
  const [listDvhc, setListDvhc] = useState<any[]>([])

  const columns: ColumnsType<any> = [
    { title: 'Tên đơn vị hành chính', dataIndex: 'name', key: 'name' },
    { title: 'Kỳ kiểm kê', dataIndex: 'year', key: 'year' },
    {
      title: 'Trạng thái duyệt báo cáo', dataIndex: 'trangThaiDuyet', key: 'trangThaiDuyet',
      render(value, record, index) {
        if (value == 1) return <Tag color="warning">Chờ duyệt</Tag>
        if (value == 2) return <Tag color="success">Đã duyệt</Tag>
        return <Tag color="error">Chưa duyệt</Tag>
      },
    },
  ];


  const getChildDVHC = async (parentId: any) => {
    const dvhcByParentIdResponse = await dvhcService.getByParentId(parentId)
    if (!dvhcByParentIdResponse || dvhcByParentIdResponse.code != 1 ||
      dvhcByParentIdResponse.returnValue.length <= 0) return;
    const newLisMapped = dvhcByParentIdResponse.returnValue.map(e => ({ ...e, children: props.dvhcRoot.childStatus == 1 ? [] : null }))

    const children = updateChild(listDvhc, parentId, newLisMapped);
    setListDvhc(children);
  }

  const updateChild = (list: any, id: any, children: any) => {
    return list.map((dvhc: any) => {
      if (dvhc.id == id) {
        return {
          ...dvhc,
          children,
        };
      }
      if (dvhc.children) {
        return {
          ...dvhc,
          children: updateChild(dvhc.children, id, children),
        };
      }
      return dvhc;
    });
  }


  useEffect(() => {

    if (props.dvhcRoot) {
      if (listDvhc.find(e => e.id == props.dvhcRoot.id)) {
        return
      }
      setListDvhc([{
        ...props.dvhcRoot,
        children: props.dvhcRoot.childStatus == 1 ? [] : null
      }])
    }
  }, [props.dvhcRoot])


  return (
    <div>
      <Table
        rowKey="ma"
        bordered={true}
        columns={columns}
        scroll={{ y: 620 }}
        expandable={{
          onExpand: (expanded, record) => {
            if (record.children.length > 0) return
            getChildDVHC(record.id);
          },
        }}
        dataSource={listDvhc}
        pagination={false}
      />
    </div>
  )
}
