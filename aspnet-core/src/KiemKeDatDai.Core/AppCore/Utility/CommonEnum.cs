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
        public const string FILE_Y_KIEN = "FILE_Y_KIEN";

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
        public enum KY_HIEU
        {
            [EnumDisplayString("01/TKKK")]
             BIEU_01_TKKK = 1,
            [EnumDisplayString("02/TKKK")]
             BIEU_02_TKKK = 2,
            [EnumDisplayString("03/TKKK")]
             BIEU_03_TKKK = 3,
            [EnumDisplayString("04/TKKK")]
             BIEU_04_TKKK = 4,
            [EnumDisplayString("05/TKKK")]
             BIEU_05_TKKK = 5,
            [EnumDisplayString("06/TKKKQPAN")]
            BIEU_06_TKKKQPAN = 6,
            [EnumDisplayString("01/KKSL")]
            BIEU_01_KKSL = 7,
            [EnumDisplayString("02/KKSL")]
            BIEU_02_KKSL = 8,
            [EnumDisplayString("01a/KKNLT")]
            BIEU_01a_KKNLT = 9,
            [EnumDisplayString("01b/KKNLT")]
            BIEU_01b_KKNLT = 10,
            [EnumDisplayString("01c/KKNLT")]
            BIEU_01c_KKNLT = 11,
            [EnumDisplayString("PL.III")]
            BIEU_PL_III = 12,
            [EnumDisplayString("PL.IV")]
            BIEU_PL_IV = 13
        }
        public enum TEN_BIEU
        {
            [EnumDisplayString("Thống kê, kiểm kê diện tích đất đai")]
             BIEU_01_TKKK = 1,
            [EnumDisplayString("Thống kê, kiểm kê đối tượng sử dụng đất và đối tượng được giao quản lý đất")]
             BIEU_02_TKKK = 2,
            [EnumDisplayString("Thống kê, kiểm kê diện tích đất đai theo đơn vị hành chínhK")]
             BIEU_03_TKKK = 3,
            [EnumDisplayString("Cơ cấu, diện tích theo loại đất, đối tượng sử dụng đất và đối tượng được giao quản lý đất")]
             BIEU_04_TKKK = 4,
            [EnumDisplayString("Chu chuyển diện tích của các loại đất")]
             BIEU_05_TKKK = 5,
            [EnumDisplayString("Thống kê, kiểm kê đất quốc phòng, đất an ninh")]
            BIEU_06_TKKKQPAN = 6,
            [EnumDisplayString("Kiểm kê diện tích đất bị sạt lở, bồi đắp trong 5 năm")]
            BIEU_01_KKSL = 7,
            [EnumDisplayString("Danh sách các điểm sạt lở, bồi đắp trong 5 năm")]
            BIEU_02_KKSL = 8,
            [EnumDisplayString("Kiểm kê tình hình quản lý, sử dụng đất của các công ty nông, lâm nghiệp")]
            BIEU_01a_KKNLT = 9,
            [EnumDisplayString("Kiểm kê tình hình quản lý, sử dụng đất của các công ty nông, lâm nghiệp")]
            BIEU_01b_KKNLT = 10,
            [EnumDisplayString("Kiểm kê tình hình, quản lý sử dụng đất của các công ty nông, lâm nghiệp")]
            BIEU_01c_KKNLT = 11,
            [EnumDisplayString("Danh sách các khoanh đất thống kê, kiểm kê đất đai")]
            BIEU_PL_III = 12,
            [EnumDisplayString("Danh sách các trường hợp biến động trong năm thống kê đất đai và kỳ kiểm kê đất đai")]
            BIEU_PL_IV = 13
        }

        public enum HANH_DONG
        {
            [EnumDisplayString("Tạo mới")]
            TAO_MOI = 1,
            [EnumDisplayString("Cập nhật")]
            CAP_NHAT = 2,
            [EnumDisplayString("Xóa")]
            XOA = 3,
            [EnumDisplayString("Đăng nhập")]
            DANG_nHAP = 4,
            [EnumDisplayString("Duyệt")]
            DUYET = 5,
            [EnumDisplayString("Hủy")]
            HUY = 6,
            [EnumDisplayString("Nộp")]
            NOP = 7
        }

        public enum LOAI_CAP_DVHC
        {
            BA_CAP = 3,
            BON_CAP = 4
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
