import { notification } from "antd"

export const handleCommontResponse = (response: any) => {
    if (response.code == 1) {
        notification.success({ message: response.message })
    } else {
        notification.error({
            message: response?.message ? response?.message : "Thất bại. Vui lòng thử lại"
        })
    }
}