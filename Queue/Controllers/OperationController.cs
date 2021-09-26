using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Queue.Models;
using Queue.ViewModels;
using MongoDB.Driver.Linq;

namespace Queue.Controllers
{
    public class OperationController : Controller
    {
        public OperationController()
        {
            MongoHelper.ConnectToMongoService();
        }

        #region AddMethods
        public bool AddHardware(List<InstalledHardwareViewModel> o)
        {
            try
            {
                MongoHelper.HardWareList = MongoHelper.database.GetCollection<InstalledHardwareViewModel>("Hardware");
                MongoHelper.HardWareList.InsertMany(o);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddSoftware(List<InstalledProgramsViewModel> o)
        {
            try
            {
                MongoHelper.SoftWareList = MongoHelper.database.GetCollection<InstalledProgramsViewModel>("Software");
                MongoHelper.SoftWareList.InsertMany(o);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool AddTracker(List<TrakerBase> tb)
        {
            try
            {
                MongoHelper.TrakerBase = MongoHelper.database.GetCollection<TrakerBase>("TrackerTime");
                MongoHelper.TrakerBase.InsertMany(tb);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddCapture(CaptureBase cb)
        {
            try
            {
                MongoHelper.UserCapture = MongoHelper.database.GetCollection<CaptureBase>("WindowsCapture");
                MongoHelper.UserCapture.InsertOne(cb);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region UpdateMethods
        public void UpdateHardware(InstalledHardwareViewModel o)
        {
            try
            {
                var collection = MongoHelper.database.GetCollection<InstalledHardwareViewModel>("Hardware");
                var builder = Builders<InstalledHardwareViewModel>.Filter;
                var filter = builder.Eq("_id", o._id);
                var update = Builders<InstalledHardwareViewModel>.Update.Set("status", false).Set("uninstalldate", DateTime.Now);
                var result = collection.UpdateOne(filter, update);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateSotfware(InstalledProgramsViewModel o)
        {
            try
            {
                var collection = MongoHelper.database.GetCollection<InstalledProgramsViewModel>("Software");
                var builder = Builders<InstalledProgramsViewModel>.Filter;
                var filter = builder.Eq("_id", o._id);
                var update = Builders<InstalledProgramsViewModel>.Update.Set("Status", false).Set("uninstalldate", DateTime.Now);
                var result = collection.UpdateOne(filter, update);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetMethods
        public List<InstalledProgramsViewModel> GetSoftWare(string IdCompany, string Pc)
        {
            try
            {
                MongoHelper.SoftWareList = MongoHelper.database.GetCollection<InstalledProgramsViewModel>("Software");
                var builder = Builders<InstalledProgramsViewModel>.Filter;
                var filter = builder.Eq("IdCompany", IdCompany) & builder.Eq("Pc", Pc) & builder.Eq("Status", true);

                var results = MongoHelper.SoftWareList.Find(filter).ToList();
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<InstalledHardwareViewModel> GetHardware(string IdCompany, string Pc)
        {
            try
            {
                MongoHelper.HardWareList = MongoHelper.database.GetCollection<InstalledHardwareViewModel>("Hardware");
                var builder = Builders<InstalledHardwareViewModel>.Filter;
                var filter = builder.Eq("IdCompany", IdCompany) & builder.Eq("Pc", Pc) & builder.Eq("status", true);

                var results = MongoHelper.HardWareList.Find(filter).ToList();
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CaptureBase> GeImage(string IdCompany)
        {
            try
            {
                MongoHelper.UserCapture = MongoHelper.database.GetCollection<CaptureBase>("WindowsCapture");
                var builder = Builders<CaptureBase>.Filter;
                var filter = builder.Eq("IdCompany", IdCompany);

                var results = MongoHelper.UserCapture.Find(filter).ToList();
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AutomaticTakeTimeModel> GetSoftWareClasification(string IdCompany)
        {
            try
            {
                //MongoHelper.SoftWareList = MongoHelper.database.GetCollection<InstalledProgramsViewModel>("Software");
                //var builder = Builders<InstalledProgramsViewModel>.Filter;

                //var filter = builder.Eq("IdCompany", IdCompany) & builder.Eq("Status", true);

                //var results = MongoHelper.SoftWareList.Find(filter).ToList();


                var query = (from e in MongoHelper.database.GetCollection<AutomaticTakeTimeModel>("TrackerTime").AsQueryable<AutomaticTakeTimeModel>()
                             where e.IdEmpresa == IdCompany
                             select new AutomaticTakeTimeModel
                             {
                                 Application = e.Application

                             }).Distinct().ToList();

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region Stadisticas

        public BasicStatsModel MoreUsedApp(string idcompany, DateTime fromdate, DateTime todate)
        {
            BasicStatsModel bm = new BasicStatsModel();
            var query = (from e in MongoHelper.database.GetCollection<AutomaticTakeTimeModel>("TrackerTime").AsQueryable<AutomaticTakeTimeModel>()
                         where e.IdEmpresa == idcompany
                         && e.Date >= fromdate && e.Date <= todate
                         select new AutomaticTakeTimeModel
                         {
                             Application = e.Application,
                             Time = e.Activity,
                             Date = e.Date
                         }).Distinct().ToList();

            foreach (var grouping in query.OrderByDescending(x => x.Time).GroupBy(g => g.Application).ToList())
            {
                var item = grouping;

                double? time = query.Where(t => t.Application == item.Key).Select(f => f.Time).Sum();
                bm.labels.Add(item.Key);
                double? totalminutes = 0;

                if (time != null && time > 0)
                    totalminutes = (time / 60);

                bm.data.Add(Math.Round(totalminutes.Value, 2));
            }

            return bm;
        }

        public BasicUserModel GetUsers(string idcompany)
        {
            BasicUserModel bm = new BasicUserModel();
            var query = (from e in MongoHelper.database.GetCollection<AutomaticTakeTimeModel>("TrackerTime").AsQueryable<AutomaticTakeTimeModel>()
                         where e.IdEmpresa == idcompany
                         select new AutomaticTakeTimeModel
                         {
                             UserName = e.UserName,
                             Application = e.Application,
                             Time = e.Activity
                         }).Distinct().ToList();

            for (var i = 0; i < query.Count;i++)
            {
                bm.User.Add(query[i].UserName);
                bm.Application.Add(query[i].Application);
                bm.Time.Add((double)query[i].Time);
            }
            return bm;
        }

        public BasicStatsModel TypeApp(string idcompany)
        {
            DateTime dateFrom = DateTime.Today.AddDays(-23);
            DateTime dateTo = dateFrom.AddHours(23);
            BasicStatsModel bm = new BasicStatsModel();
            var query = (from e in MongoHelper.database.GetCollection<AutomaticTakeTimeModel>("TrackerTime").AsQueryable<AutomaticTakeTimeModel>()
                         where e.IdEmpresa == idcompany && e.Date >= dateFrom && e.Date <= dateTo
                         select new AutomaticTakeTimeModel
                         {
                             Application = e.Application,
                             Time = e.Activity,
                             Date = e.Date
                         }).Distinct().ToList();

            foreach (var grouping in query.OrderByDescending(x => x.Time).GroupBy(g => g.Application).ToList())
            {
                var item = grouping;

                double? time = query.Where(t => t.Application == item.Key).Select(f => f.Time).Sum();
                bm.labels.Add(item.Key);
                double? totalminutes = 0;

                if (time != null && time > 0)
                    totalminutes = (time / 60);

                bm.data.Add(Math.Round(totalminutes.Value, 2));
            }

            return bm;
        }

        public BasicStatsModel WebUsedApp(string idcompany, DateTime fromdate, DateTime todate)
        {
            BasicStatsModel bm = new BasicStatsModel();
            var query = (from e in MongoHelper.database.GetCollection<AutomaticTakeTimeModel>("TrackerTime").AsQueryable<AutomaticTakeTimeModel>()
                         where e.IdEmpresa == idcompany && e.Application == "chrome"
                         && e.Date >= fromdate && e.Date <= todate
                         select new AutomaticTakeTimeModel
                         {
                             Application = e.Application,
                             Time = e.Activity,
                             Date = e.Date,
                             Title = e.Title
                         }).Distinct().ToList();

            foreach (var grouping in query.OrderByDescending(x => x.Time).GroupBy(g => g.Title).ToList())
            {
                var item = grouping;

                double? time = query.Where(t => t.Title == item.Key).Select(f => f.Time).Sum();
                bm.labels.Add(item.Key);
                double? totalminutes = 0;

                if (time != null && time > 0)
                    totalminutes = (time / 60);

                bm.data.Add(Math.Round(totalminutes.Value, 2));
            }

            return bm;
        }

        public BasicStatsModel ImproductiveUsedApp(string idcompany, DateTime fromdate, DateTime todate)
        {
            BasicStatsModel bm = new BasicStatsModel();
            var query = (from e in MongoHelper.database.GetCollection<AutomaticTakeTimeModel>("TrackerTime").AsQueryable<AutomaticTakeTimeModel>()
                         where e.IdEmpresa == idcompany
                         && e.Date >= fromdate && e.Date <= todate
                         select new AutomaticTakeTimeModel
                         {
                             Application = e.Application,
                             Time = e.Activity,
                             Date = e.Date
                         }).Distinct().ToList();

            foreach (var grouping in query.OrderByDescending(x => x.Time).GroupBy(g => g.Application).ToList())
            {
                var item = grouping;

                double? time = query.Where(t => t.Application == item.Key).Select(f => f.Time).Sum();
                bm.labels.Add(item.Key);
                double? totalminutes = 0;

                if (time != null && time > 0)
                    totalminutes = (time / 60);

                bm.data.Add(Math.Round(totalminutes.Value, 2));
            }

            return bm;
        }
        #endregion
    }
}