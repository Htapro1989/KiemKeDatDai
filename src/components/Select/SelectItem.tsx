import { Col, Form, Select } from "antd"
import { SelectProps } from "antd/lib/select"
import React, { forwardRef, useEffect, useImperativeHandle, useState } from "react"


interface SelectItemProps {
    formItemProps?: any
    selectProps?: SelectProps<any>
    span?: number
    children?: any
    className?: string
    remoteSource?: () => Promise<any>;
    refresing?: any;
    onFilter?: any;
}

export default forwardRef((props: SelectItemProps, ref: any) => {
    const { span = 6, formItemProps, className, remoteSource, selectProps, refresing } = props
    const [options, setOptions] = useState<any>(selectProps?.options);
    const [isLoading, setIsLoading] = useState(false)
    const isHiden = formItemProps?.hidden

    useEffect(() => {
        if (remoteSource && refresing) {
            setIsLoading(true)
            remoteSource().then(data => {
                if (data) setOptions(data)
            }).finally(() => {
                setIsLoading(false)
            })
        }
    }, [refresing])

    useImperativeHandle(ref, () => {
        return {
            getOptions() {
                return options;
            }
        };
    }, [options]);

    if (isHiden) {
        return (
            <Form.Item
                className={`${className}`}
                {...formItemProps}
            >
                <Select
                    options={options}
                    loading={isLoading}
                    placeholder="Chọn"
                    {...selectProps}
                />
            </Form.Item>
        )
    }

    return (
        <Col span={span}>
            <Form.Item
                className={`${className}`}
                {...formItemProps}
            >
                <Select
                    options={options}
                    loading={isLoading}
                    placeholder="Chọn"
                    {...selectProps}
                />
            </Form.Item>
        </Col>
    )
})
