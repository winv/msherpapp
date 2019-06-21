using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MshErp.Infrastructure
{
    public class Helper
    {
        /// <summary>
        /// 针对没有getset访问器的类 ，根据类成员进行映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T MapNoProperty<T, S>(S s)
                where T : class, new()
                where S : class, new()
        {
            var sProperties = typeof(S).GetProperties();
            var tItem = new T();
            var type = tItem.GetType();
            foreach (var item in type.GetFields())
            {
                var spinfo = sProperties.Where(x => x.Name == item.Name).FirstOrDefault();
                if (spinfo != null)
                {
                    try
                    {
                        var value = spinfo.GetValue(s);
                        if (value == null)
                            continue;
                        if (value.GetType() == typeof(DateTime))
                        {
                            if (DateTime.Parse(value.ToString()) == DateTime.MinValue)
                            {
                                continue;
                            }
                        }
                       
                        item.SetValue(tItem, value);
                    }
                    catch { }
                }
            }
            return tItem;
        }

        public static List<T> MapNoProperty<T, S>(List<S> slist)
                where T : class, new()
                where S : class, new()
        {
            var tItemList = new List<T>();
            foreach (var s in slist)
            {
                tItemList.Add(MapNoProperty<T, S>(s));
            }
            
            return tItemList;
        }
    }
}
