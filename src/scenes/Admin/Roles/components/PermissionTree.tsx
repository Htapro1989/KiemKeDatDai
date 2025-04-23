import { Tree } from 'antd';
import React, { useEffect, useState } from 'react';

const res = [
    {
        "name": "Pages.Administration.System.BieuMau",
        "displayName": "[Bieu mau]",
        "description": null
    },
    {
        "name": "Pages.Administration.System.CapDvhc",
        "displayName": "[Cap dvhc]",
        "description": null
    },
    {
        "name": "Pages.Administration.System.ConfigSystem",
        "displayName": "[Config system]",
        "description": null
    },
    {
        "name": "Pages.Report.DownloadBaoCao",
        "displayName": "[Download bao cao]",
        "description": null
    },
    {
        "name": "Pages.Report.DownloadFile",
        "displayName": "[Download file]",
        "description": null
    },
    {
        "name": "Pages.Report.DuyetBaoCao",
        "displayName": "[Duyet bao cao]",
        "description": null
    },
    {
        "name": "Pages.Administration.System.Dvhc",
        "displayName": "[Dvhc]",
        "description": null
    },
    {
        "name": "Pages.Report.HuyBaoCao",
        "displayName": "[Huy bao cao]",
        "description": null
    },
    {
        "name": "Pages.Administration.System.KyKiemKe",
        "displayName": "[Ky kiem ke]",
        "description": null
    },
    {
        "name": "Pages.Administration.System.News",
        "displayName": "[News]",
        "description": null
    },
    {
        "name": "Pages.Report.NhapBieu",
        "displayName": "[Nhap bieu]",
        "description": null
    },
    {
        "name": "Pages.Report.NopBaoCao",
        "displayName": "[Nop bao cao]",
        "description": null
    },
    {
        "name": "Pages",
        "displayName": "[Pages]",
        "description": null
    },
    {
        "name": "Pages.Report",
        "displayName": "[Report]",
        "description": null
    },
    {
        "name": "Pages.Administration.System",
        "displayName": "[System manager]",
        "description": null
    },
    {
        "name": "Pages.Report.UploadAPI",
        "displayName": "[Upload aPI]",
        "description": null
    },
    {
        "name": "Pages.Report.Upload",
        "displayName": "[Upload]",
        "description": null
    },
    {
        "name": "Pages.Report.XemBaoCao",
        "displayName": "[Xem bao cao]",
        "description": null
    },
    {
        "name": "Pages.Administration.System.YKien",
        "displayName": "[YKien]",
        "description": null
    },
    {
        "name": "Pages.Administration",
        "displayName": "Administration",
        "description": null
    },
    {
        "name": "Pages.Roles",
        "displayName": "Roles",
        "description": null
    },
    {
        "name": "Pages.Administration.Roles",
        "displayName": "Roles",
        "description": null
    },
    {
        "name": "Pages.Administration.Users",
        "displayName": "Users",
        "description": null
    }
]

export function buildTreeFromFlatTreeData(data: any) {
    const keyMap = new Map();

    // Bước 1: map key => node
    data.forEach((item: any) => {
        keyMap.set(item.key, { ...item, children: [] });
    });

    const tree: any[] = [];

    // Bước 2: gắn children vào cha
    keyMap.forEach((node, key) => {
        const parentKey = key.split('.').slice(0, -1).join('.');
        const parentNode = keyMap.get(parentKey);

        if (parentNode) {
            parentNode.children.push(node);
        } else {
            tree.push(node); // là node root
        }
    });

    return tree;
}

const x = res.map(permission => {
    return {
        title: permission.displayName,
        key: permission.name,
        children: []
    }
})
const x3 = buildTreeFromFlatTreeData(x)
console.log(x3)

const PermissionTree = (props: any) => {
    const [checkedKeys, setCheckedKeys] = useState<React.Key[]>([]);
    const [selectedKeys, setSelectedKeys] = useState<React.Key[]>([]);

    const onCheck = (checkedKeysValue: any) => {
        setCheckedKeys(checkedKeysValue);
        props.onCheck(checkedKeysValue)
    };

    const onSelect = (selectedKeysValue: React.Key[], info: any) => {
        setSelectedKeys(selectedKeysValue);
    };

    useEffect(() => {
        if (props.selectedKeys) {
            setCheckedKeys(props.selectedKeys)
        }
    }, [props.selectedKeys])



    return (
        <Tree
            checkable
            defaultExpandAll={true}
            onCheck={onCheck}
            checkedKeys={checkedKeys}
            onSelect={onSelect}
            selectedKeys={selectedKeys}
            treeData={props.treeData}
        />
    );
};

export default PermissionTree;