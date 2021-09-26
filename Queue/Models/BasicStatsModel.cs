using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Queue.Models
{
    public class BasicStatsModel
    {
        public List<string> labels = new List<string>();
        public List<double?> data = new List<double?>();
    }

    public class BasicUserModel
    {
        public List<string> User = new List<string>();
        public List<string> Application = new List<string>();
        public List<double> Time = new List<double>();
    }

    public class ArrayUsersModel
    {
        public string User;
        public List<string> Application = new List<string>();
        public List<double> Time = new List<double>();
        //public List<ApplicationModel> Data = new List<ApplicationModel>();
    }

    public class ApplicationModel
    {
        public string Application;
        public double Time;
    }
}