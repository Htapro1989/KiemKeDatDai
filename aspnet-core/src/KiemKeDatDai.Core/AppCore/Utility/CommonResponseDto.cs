using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai.ApplicationDto
{
    public class CommonResponseDto
    {
        public CommonEnum.ResponseCodeStatus Code { get; set; }

        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public object ReturnValue { get; set; }

        //Khong them moi bat ki truong nao vao day -> hoi y kien a tung

        public CommonResponseDto()
        {
            Code = CommonEnum.ResponseCodeStatus.ThatBai;
            ErrorCode = "";
            Message = "Có lỗi xảy ra trong quá trình xử lý.";
        }
    }
}
