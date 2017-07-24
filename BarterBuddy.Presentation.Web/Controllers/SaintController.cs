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
    public class SaintController : Controller
    {
        // GET: Saint
        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        [ActionName("GetSaintByID")]
        public string Get(int id)
        {
            SaintDAL objSaintDal = new SaintDAL();

            DataSet dsSaint = new DataSet();

            dsSaint = objSaintDal.GetSaint(id);

            Saint snt = null;

            if (dsSaint.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsSaint.Tables[0].Rows[0];

                snt = new Saint();
                snt.SaintId = Convert.ToInt32(dr["SaintId"]);
                snt.SaintName = Convert.ToString(dr["SaintName"]);
                snt.SaintContactNo = Convert.ToString(dr["SaintContactNo"]);
                snt.SaintCurrentLocation = Convert.ToString(dr["SaintCurrentLocation"]);
                snt.SaintSynopsis = Convert.ToString(dr["SaintSynopsis"]);
                snt.SaintLat = Convert.ToDecimal(dr["SaintLat"]);
                snt.SaintLag = Convert.ToDecimal(dr["SaintLag"]);

            }

            return JsonConvert.SerializeObject(snt, Formatting.Indented);

            // return  tmpl;

        }


        [HttpGet]
        [ActionName("GetSaintByKM")]
        public string GetByKM(int ReligionId, decimal CurrentLang, decimal CurrentLat, int WithinXKm)
        {



            SaintDAL objSaintDal = new SaintDAL();

            DataSet dsTemple = new DataSet();

            dsTemple = objSaintDal.GetSaint(ReligionId, CurrentLang, CurrentLat, WithinXKm);

            DataTable dt = dsTemple.Tables[0];
            return JsonConvert.SerializeObject(dt);

        }



        [HttpPost]
        [ActionName("AddSaint")]
        public Boolean AddSaint(int ReligionId, string SaintName, string SaintContactNo, string SaintCurrentLocation, string SaintSynopsis, decimal SaintLat, decimal SaintLng)
        {

            try
            {
                


                SaintDAL objSaintDal = new SaintDAL();

                DataSet dsSaint = new DataSet();

                objSaintDal.AddSaint(ReligionId, SaintName, SaintContactNo, SaintCurrentLocation, SaintSynopsis, SaintLat, SaintLng);

                return true;

            }

            catch (Exception ex)
            {
              


                return false;
            }
        }



    }
}