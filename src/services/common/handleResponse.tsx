import { notification } from "antd"

export const handleCommontResponse = (response: any, onCallBackSuccess?: any) => {
    if (response.code == 1) {
        notification.success({ message: response.message })
        if (onCallBackSuccess) onCallBackSuccess()
    } else {
        notification.error({
            message: response?.message ? response?.message : "Thất bại. Vui lòng thử lại"
        })
    }
}