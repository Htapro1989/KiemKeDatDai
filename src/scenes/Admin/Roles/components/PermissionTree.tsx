import { Tree } from 'antd';
import React, { useEffect, useState } from 'react';

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