using System;

namespace Azure_DocumentDB_WebApiApp
{
    public static class StringExtensionMethods
    {
        public static void Check(this string stringtocheck, string msg)
        {
            if (String.IsNullOrEmpty(stringtocheck))
            {
                throw new ArgumentNullException(msg);
            }
        }
    }
}