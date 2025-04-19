import { CloudUploadOutlined } from "@ant-design/icons"
import { Button } from "antd"
import { useRef } from "react";
import * as React from 'react';
import { ButtonProps } from "antd/lib/button";
import './index.less'
interface UploadFileProps extends ButtonProps {
    onUpload: (file: any) => any;
    accept?: string;
    hideFileSelected?: boolean;
    title?: string;
}

function UploadFileButton(props: UploadFileProps) {
    const { onUpload, accept, hideFileSelected, title = "Tải lên" } = props

    const inputFileElement = useRef<HTMLInputElement>(null);

    const [fileSelected, setFileSelected] = React.useState<any>()


    const openSelectFile = (event: any) => {
        event.preventDefault();
        if (inputFileElement.current) {
            if (inputFileElement.current.value) {
                inputFileElement.current.value = '';
            }
            inputFileElement.current.click();
        }
    };

    const onSelectFile = async (event: any) => {
        event.preventDefault();
        const file = event.target.files[0];
        setFileSelected(file)
        onUpload(file)
    };

    return (
        <React.Fragment>
            <Button type="primary" onClick={openSelectFile} icon={<CloudUploadOutlined />} {...props}>{title}</Button>
            <input
                className="d-none"
                type="file"
                accept={accept}
                onChange={onSelectFile}
                multiple={false}
                ref={inputFileElement}
            />
            <div className={`mt-12 ${hideFileSelected ? 'd-none' : ''}`}>{fileSelected?.name}</div>
        </React.Fragment>
    )
}

export default UploadFileButton