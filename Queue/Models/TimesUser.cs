using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Queue.Models
{
    public class TimesUser
    {
        public Nullable<double> Productivo
        {
            get;
            set;
        } = 0;
        public Nullable<double> Improductivo
        {
            get;
            set;
        } = 0;
        public Nullable<double> Neutro
        {
            get;
            set;
        } = 0;
        public Nullable<double> Total
        {
            get;
            set;
        } = 1440;
    }
}