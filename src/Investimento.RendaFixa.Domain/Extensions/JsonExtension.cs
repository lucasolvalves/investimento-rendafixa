using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investimento.RendaFixa.Domain.Extensions
{
    public static class JsonExtension
    {
        public static JObject ToJObject(this string str)
        {
            try
            {
                return JObject.Parse(str);
            }
            catch
            {
                return null;
            }
        }

        public static string JsonGetByName(this string json, string item)
        {
            try
            {
                var obj = ToJObject(json);
                return obj?[item]?.ToString();
            }
            catch
            {
                return null;
            }
        }
    }
}
