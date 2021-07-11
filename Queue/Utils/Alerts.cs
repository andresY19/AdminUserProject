using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queue.Utils
{
    public static class Alerts
    {
        public const string SUCCESS = "success";
        public const string WARNING = "warning";
        public const string ERROR = "error";
        public const string INFORMATION = "info";

        public static string[] ALL
        {
            get { return new[] { SUCCESS, WARNING, INFORMATION, ERROR }; }
        }
    }
}
