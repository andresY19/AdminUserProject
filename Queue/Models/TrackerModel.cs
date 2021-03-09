using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Queue.Models;


namespace Queue.Models
{
    public class TrackerModel: Basic
    {
        public List<AutomaticTakeTimeModel> AutomaticTakeTimeModel = new List<AutomaticTakeTimeModel>();
    }
}