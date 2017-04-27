using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace TK.Common
{
    public static class Helper
    {
        public static string AsJsonList<T>(List<T> tt)
        {
            return new JavaScriptSerializer().Serialize(tt);
        }
        public static string AsJson<T>(T t)
        {
            return new JavaScriptSerializer().Serialize(t);
        }
        public static List<T> AsObjectList<T>(string tt)
        {
            return new JavaScriptSerializer().Deserialize<List<T>>(tt);
        }
        public static T AsObject<T>(string t)
        {
            return new JavaScriptSerializer().Deserialize<T>(t);
        }
        public static int RandNumber(int low, int high)
        {
            Random rndNum = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));

            int rnd = rndNum.Next(low, high);

            return rnd;
        }
        public static string RandomString(int size, bool lowerCase = false)
        {


            var charCollection = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                builder.Append(charCollection[RandNumber(0, charCollection.Length - 1)]);
            }
            if (lowerCase)
            {
                return builder.ToString().ToLower();
            }
            else
            {
                return builder.ToString();
            }

        }
    }
}
