using System;
//using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BarterBuddy.Presentation.Web.Models;
using BarterBuddy.Presentation.Web.DAL;

using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace BarterBuddy.Presentation.Web.Controllers
{
    public class TempleController : Controller
    {
        // GET: Temple
        public ActionResult Index()
        {
            return View();
        }




        [HttpGet]
        [ActionName("GetTempleByID")]
        public string Get(int id)
        {
            TempleDAL objTempleDal = new TempleDAL();

            DataSet dsTemple = new DataSet();

            dsTemple = objTempleDal.GetTemple(id);

            Temple tmpl = null;

            if (dsTemple.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsTemple.Tables[0].Rows[0];

                tmpl = new Temple();
                tmpl.TempleId = Convert.ToInt32(dr["TempleId"]);
                tmpl.TempleName = Convert.ToString(dr["TempleName"]);
                tmpl.TempleOfficeAddress = Convert.ToString(dr["TempleOfficeAddress"]);
                tmpl.TempleOfficeNo = Convert.ToString(dr["TempleOfficeNo"]);
                tmpl.TempleSynopsis = Convert.ToString(dr["TempleSynopsis"]);
                tmpl.TempleLat = Convert.ToDecimal(dr["TempleLat"]);
                tmpl.TempleLng = Convert.ToDecimal(dr["TempleLng"]);

            }

            return JsonConvert.SerializeObject(tmpl, Formatting.Indented);

            // return  tmpl;

        }


        [HttpGet]
        [ActionName("GetTempleByKM")]
        public string GetByKM(int ReligionId, decimal CurrentLang, decimal CurrentLat, int WithinXKm)
        {

       

            TempleDAL objTempleDal = new TempleDAL();

            DataSet dsTemple = new DataSet();

            dsTemple = objTempleDal.GetTemple(ReligionId, CurrentLang, CurrentLat, WithinXKm);

            DataTable dt = dsTemple.Tables[0];
            return JsonConvert.SerializeObject(dt);

        }

        [HttpPost]
        [ActionName("AddTemple")]
        public Boolean AddTemple(int ReligionId, string TempleName, string TempleOfficeNo, string TempleOfficeAddress, string TempleSynopsis, decimal CurrentLang, decimal CurrentLat)
        {

            try
            {

                //if (!EventLog.SourceExists("dotNET"))
                //    EventLog.CreateEventSource("dotNET", "Application");

                //EventLog.WriteEntry("dotNET", "inside Add Temple controller");



                TempleDAL objTempleDal = new TempleDAL();

                DataSet dsTemple = new DataSet();

                objTempleDal.AddTemple(ReligionId, TempleName, TempleOfficeNo, TempleOfficeAddress, TempleSynopsis, CurrentLang, CurrentLat);

                return true;

            }

            catch (Exception ex)
            {
                //if (!EventLog.SourceExists("dotNET"))
                //    EventLog.CreateEventSource("dotNET", "Application");

                //EventLog.WriteEntry("dotNET", ex.Message);


                return false;
            }
        }


    }
}