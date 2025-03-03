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

        public enum TRANG_THAI_SUYET
        {
            CHUA_GUI = 0,
            CHUA_DUYET = 1,
            DA_DUYET = 2
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
