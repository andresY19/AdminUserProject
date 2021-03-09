using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Queue.Controllers;

namespace Queue.Utils
{
    public static class AuditCompare
    {
        static AuditController ac = new AuditController();
        public static List<Variance> Compare<T>(this T val1, T val2, string actiontype)
        {
            var variances = new List<Variance>();

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                var v = new Variance
                {
                    FieldName = property.Name,
                    OldValue = property.GetValue(val1),
                    NewValue = property.GetValue(val2)
                };
                if (v.OldValue == null && v.NewValue == null)
                {
                    continue;
                }
                if (v.NewValue == null)
                {
                    continue;
                }
                if (
                (v.OldValue == null && v.NewValue != null)
                ||
                (v.OldValue != null && v.NewValue == null)
            )
                {
                    variances.Add(v);
                    continue;
                }
                if (!v.OldValue.Equals(v.NewValue))
                {
                    variances.Add(v);
                }
            }
            AuditController ac = new AuditController();
            //ac.InsertAudit(variances, GetDisplayName<T>(), actiontype);
            return variances;
        }

        public static void SaveAudit<T>(this T val1, T val2, string table)
        {
            object t1 = new object();
            //Copier.CopyPropertiesTo(val1, t1);
            //AuditCompare.Compare(t1, val2, ActionType.Action.Edit.ToString());
            //ac.InserAuditAction(table, action.ToString());
            //Copier.CopyPropertiesTo(val2, val1);

            //return val1;
        }
        public static string GetDisplayName<T>()
        {
            var displayName = typeof(T)
              .GetCustomAttributes(typeof(DisplayNameAttribute), true)
              .FirstOrDefault() as DisplayNameAttribute;

            if (displayName != null)
                return displayName.DisplayName;

            return "";
        }
    }
    
}