using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yoki.Comm;

namespace Yoki.View
{
    static class ObjectOutput
    {
        public static string Print(this object obj)
        {
            try
            {
                if (obj == null)
                    return "null";
                var type = obj.GetType();
                if (type.IsArray || typeof(IList).IsAssignableFrom(type))
                {
                    return ParseArray(obj as IList, 0);
                }
                return PrintObject(obj, 0);
            }
            catch (Exception ex)
            {
                LogUtil.WriteLine("{" + DateTime.Now.ToString() + "}" + "[ObjectOutput] Print  Exception = " + ex.Message);
                return null;
            }
        }

        public static string ParseArray(IList list, int depth)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                sb.AppendLine(list[i].PrintObject(depth));
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public static string PrintObject(this object obj, int depth)
        {
            var type = obj.GetType();
            var ps = type.GetProperties();
            if (!ps.Any())
                return obj.ToString();
            StringBuilder builder = new StringBuilder();
            foreach (var p in ps)
            {
                try
                {
                    var value = p.GetValue(obj, null);
                    if (value == null)
                    {
                        builder.AppendLine(string.Format("{0}{1} :  {2}", new string(' ', depth * 3), p.Name, "null"));
                        continue;
                    }
                    if (p.PropertyType.IsArray || typeof(IList).IsAssignableFrom(p.PropertyType))
                    {

                        builder.AppendLine(string.Format("{0}:", new string(' ', depth * 3) + p.Name));
                        builder.AppendLine(ParseArray(value as IList, depth + 1));
                    }
                    else if (p.PropertyType.IsClass && !p.PropertyType.IsSealed)
                    {
                        builder.AppendLine(string.Format("{0}:", new string(' ', depth * 3) + p.Name));
                        var iv = value.PrintObject(depth + 1);
                        builder.AppendLine(iv);

                    }
                    else
                    {
                        builder.AppendLine(string.Format("{0}{1} :  {2}", new string(' ', depth * 3), p.Name, value.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    builder.AppendLine("*******[PrintObject] Error msg = " + ex.Message);
                }
            }


            return builder.ToString();
        }


    }
}
