﻿using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace WinverUWP
{
    public class CultureInfoHelper
    {
        [DllImport("api-ms-win-core-localization-l1-2-1.dll", CharSet = CharSet.Unicode)]
        private static extern int GetLocaleInfoEx(string lpLocaleName, uint LCType, StringBuilder lpLCData, int cchData);


        private const uint LOCALE_SNAME = 0x0000005c;
        private const string LOCALE_NAME_USER_DEFAULT = null;
        private const string LOCALE_NAME_SYSTEM_DEFAULT = "!x-sys-default-locale";


        private const int BUFFER_SIZE = 530;


        public static CultureInfo GetCurrentCulture()
        {
            var name = InvokeGetLocaleInfoEx(LOCALE_NAME_USER_DEFAULT, LOCALE_SNAME);


            if (name == null)
            {
                name = InvokeGetLocaleInfoEx(LOCALE_NAME_SYSTEM_DEFAULT, LOCALE_SNAME);


                if (name == null)
                {
                    // If system default doesn't work, use invariant
                    return CultureInfo.InvariantCulture;
                }
            }


            return new CultureInfo(name);
        }


        private static string InvokeGetLocaleInfoEx(string lpLocaleName, uint LCType)
        {
            var buffer = new StringBuilder(BUFFER_SIZE);


            var resultCode = GetLocaleInfoEx(lpLocaleName, LCType, buffer, BUFFER_SIZE);


            if (resultCode > 0)
            {
                return buffer.ToString();
            }


            return null;
        }
    }
}
