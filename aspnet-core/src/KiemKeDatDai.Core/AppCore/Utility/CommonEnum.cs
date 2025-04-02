using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KiemKeDatDai
{
    public static class CommonEnum
    {
        public const string FILE_ATTACHMENT = "FILE_ATTACHMENT";
        public const string FILE_KYTHONGKE = "FILE_KYTHONGKE";
        public const string FILE_NEWS = "FILE_NEWS";

        public enum RESPONSE_CODE
        {
            SUCCESS = 200,
            ERROR = 500,
            TOO_REQUEST = 429,
        }

        #region  Khai báo Enum 
        public enum ResponseCodeStatus
        {
            CanhBao = 2,
            ThanhCong = 1,
            ThatBai = 0,
        }
        #endregion

        public enum STATUS_CODE_ERROR
        {
            IsExistEmail = -1,
        }
        public enum TRANG_THAI_DUYET
        {
            CHUA_GUI = 0,
            CHO_DUYET = 1,
            DA_DUYET = 2
        }
        public enum CAP_DVHC
        {
            TRUNG_UONG = 0,
            VUNG = 1,
            TINH = 2,
            HUYEN = 3,
            XA = 4
        }
        public enum VUNG_MIEN
        {
            VUNG_MIEN_NUI_PHIA_BAC = 1,
            VUNG_DONG_BANG_SONG_HONG = 2,
            VUNG_DUYEN_HAI_MIEN_TRUNG = 3,
            VUNG_TAY_NGUYEN = 4,
            VUNG_DONG_NAM_BO = 5,
            VUNG_DONG_BANG_SONG_CUU_LONG = 6
        }

        public enum HAM_DUYET
        {
            DUYET = 1,
            HUY = 2
        }
        public enum LOAI_DE_NGHI_DUYET_TAI_KHOAN_DOANH_NGHIEP
        {
            [EnumDisplayString("Đề nghị tạo tài khoản")]
            DE_NGHI_KHOI_TAO_TAI_KHOAN = 1,
            [EnumDisplayString("Đề nghị thay đổi thông tin")]
            DE_NGHI_THAY_DOI_THONG_TIN = 2
        }

        public class ComboBoxDto
        {
            public long Id { get; set; }
            public string Name { get; set; }
        }
        public static string GetEnumDisplayString(Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumDisplayString), false);

                if (attrs != null && attrs.Length > 0)
                    return ((EnumDisplayString)attrs[0]).DisplayString;
            }
            return en.ToString();
        }
    }

    public static class UtilityCommonEnum
    {
        public static string GetEnumDescription(Enum en)
        {
            Type type = en.GetType();
            try
            {
                MemberInfo[] memInfo = type.GetMember(en.ToString());

                if (memInfo != null && memInfo.Length > 0)
                {
                    object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumDisplayString), false);

                    if (attrs != null && attrs.Length > 0)
                        return ((EnumDisplayString)attrs[0]).DisplayString;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
            return en.ToString();
        }
    }

    public class EnumDisplayString : Attribute
    {
        public string DisplayString { get; set; }

        public EnumDisplayString(string text)
        {
            this.DisplayString = text;
        }
    }
}
