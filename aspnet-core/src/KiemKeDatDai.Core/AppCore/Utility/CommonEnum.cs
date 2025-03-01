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
        public enum RESPONSE_CODE
        {
            SUCCESS = 200,
            ERROR = 500
        }

        #region  Khai báo Enum 
        public enum ResponseCodeStatus
        {
            ThanhCong = 1,
            ThatBai = 0,
        }
        #endregion

        public enum STATUS_CODE_ERROR
        {
            IsExistEmail = -1,
        }

        public enum DUONG_GIOI_HAN
        {
            MNTL = 0,
            PHAI = 1,
            TRAI = 2
        }
        public enum LOAI_DUONG_GIOI_HAN
        {
            MOT_DUONG = 1,
            HAI_DUONG = 2,
            BA_DUONG = 3,
            BON_DUONG = 4
        }
        public enum LOAI_DANH_GIA
        {
            TCT1 = 1,
            TCT2 = 2,
            TCT3 = 3
        }

        public enum TRAM_QUAN_TRAC
        {
            THU_CONG = 1,
            TU_DONG = 2
        }

        public enum DANH_GIA
        {
            DAM_BAO_AN_TOAN = 1,
            AN_TOAN = 2,
            NGUY_CO_MAT_AN_TOAN = 3
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
