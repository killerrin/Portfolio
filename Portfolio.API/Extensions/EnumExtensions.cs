using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.API.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<EnumModel> GetNameValuePair<T>()
        {
            List<EnumModel> enums = new List<EnumModel>();

            Type genericType = typeof(T);
            foreach (T obj in Enum.GetValues(genericType))
            {
                Enum test = Enum.Parse(typeof(T), obj.ToString()) as Enum;
                int x = Convert.ToInt32(test);
                enums.Add(new EnumModel(obj.ToString(), x));
            }

            return enums;
        }
    }
    public class EnumModel
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public EnumModel(string name, int value)
        {
            Name = name;
            Value = value;
        }
        public EnumModel(Enum e)
        {
            Name = e.ToString();
            Value = Convert.ToInt32(e);
        }
    }
}
