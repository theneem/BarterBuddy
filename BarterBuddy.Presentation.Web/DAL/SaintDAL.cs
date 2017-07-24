using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Configuration;
using System.Data;
using System.Data.Common;
//using System.Diagnostics;

namespace BarterBuddy.Presentation.Web.DAL
{
    public class SaintDAL
    {
        Database objDB;
        static string FMGConnString;

        public SaintDAL()
        {

            FMGConnString = ConfigurationManager.ConnectionStrings["FMG"].ToString();
        }

        public DataSet GetSaint(int SaintId)
        {
            objDB = new SqlDatabase(FMGConnString);
            DbCommand dbcSaint = objDB.GetStoredProcCommand("dbo.FMG_GetSaint");
            objDB.AddInParameter(dbcSaint, "@SaintId", DbType.Int32, SaintId);

            return objDB.ExecuteDataSet(dbcSaint);
        }

        public DataSet GetSaint(int ReligionId, decimal CurrentLang, decimal CurrentLat, int WithinXKm)
        {
            objDB = new SqlDatabase(FMGConnString);
            DbCommand dbcSaint = objDB.GetStoredProcCommand("dbo.FMG_GetSaintWithinXKm");
            objDB.AddInParameter(dbcSaint, "@ReligionId", DbType.Int32, ReligionId);
            objDB.AddInParameter(dbcSaint, "@CurrentLang", DbType.Int32, CurrentLang);
            objDB.AddInParameter(dbcSaint, "@CurrentLat", DbType.Int32, CurrentLat);
            objDB.AddInParameter(dbcSaint, "@WithinXKm", DbType.Int32, WithinXKm);

            return objDB.ExecuteDataSet(dbcSaint);
        }



        public int AddSaint(int ReligionId, string SaintName, string SaintContactNo, string SaintCurrentLocation, string SaintSynopsis, decimal SaintLat, decimal SaintLng)
        {
            try
            {

                objDB = new SqlDatabase(FMGConnString);
                DbCommand dbcTemple = objDB.GetStoredProcCommand("dbo.FMG_AddSaint");
                objDB.AddInParameter(dbcTemple, "@ReligionId", DbType.Int32, ReligionId);
                objDB.AddInParameter(dbcTemple, "@SaintName", DbType.String, SaintName);
                objDB.AddInParameter(dbcTemple, "@SaintContactNo", DbType.String, SaintContactNo);
                objDB.AddInParameter(dbcTemple, "@SaintCurrentLocation", DbType.String, SaintCurrentLocation);
                objDB.AddInParameter(dbcTemple, "@SaintSynopsis", DbType.String, SaintSynopsis);

                objDB.AddInParameter(dbcTemple, "@SaintLat", DbType.Decimal, SaintLat);
                objDB.AddInParameter(dbcTemple, "@SaintLng", DbType.Decimal, SaintLng);

                return objDB.ExecuteNonQuery(dbcTemple);
            }
            catch (Exception ex)
            {
                //EventLog.WriteEntry("dotNET", ex.Message);
                return 0;
            }
        }



    }
}