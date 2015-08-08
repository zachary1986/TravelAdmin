using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Clone
    {
        public void CloneProperty(object source, object target, IEnumerable<string> ignoreProperties = null)
        {
            if (source == null || target == null)
            {
                return;
            }
            PropertyInfo[] sourceProperties = source.GetType().GetProperties();
            PropertyInfo[] targetProperties = target.GetType().GetProperties();

           ignoreProperties=ignoreProperties??new string[0];

           Dictionary<string, PropertyInfo> dicSourceProperty = sourceProperties.Where(p => p.GetCustomAttributes(typeof(IgnoreCloneAttribute), false).Length == 0).ToDictionary(p => p.Name);

           foreach (var p in targetProperties)
           {
               if (ignoreProperties.Contains(p.Name)) continue;
               if (dicSourceProperty.ContainsKey(p.Name) && p.GetType() == dicSourceProperty[p.Name].GetType())
               {
                   object value = dicSourceProperty[p.Name].GetValue(source,null);
                   try { p.SetValue(target, value, null); }
                   catch { };
               }
           }
        }

        
    }
    public class IgnoreCloneAttribute
    { }
}
