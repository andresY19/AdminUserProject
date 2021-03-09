using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using Microsoft.CSharp.RuntimeBinder;

namespace Queue.Utils
{
    public static class Copier
    {
        public static void CopyPropertiesTo<T, TU>(this T source, TU dest)
        {
            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TU).GetProperties()
                    .Where(x => x.CanWrite)
                    .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    { // check if the property can be set or no.
                        if (source != null)
                            p.SetValue(dest, sourceProp.GetValue(source, null), null);
                    }
                }

            }

        }

        public static object CopyPropertiesToVar<T>(this T source)
        {
            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            dynamic dynamico = new ExpandoObject();
            var dictionary = (IDictionary<string, object>)dynamico;

            foreach (var sourceProp in sourceProps)
            {
                var valor = sourceProp.GetValue(source, null);
                dictionary.Add(sourceProp.Name.Replace("_", "").ToLower(), valor);
            }
            return dynamico;
        }
    }
}