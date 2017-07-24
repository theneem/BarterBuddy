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
    public class TempleDAL
    {
        Database objDB;
        static string FMGConnString;

        public TempleDAL()
        {

            FMGConnString = ConfigurationManager.ConnectionStrings["FMG"].ToString();
        }

        public DataSet GetTemple(int TempleId)
        {
            objDB = new SqlDatabase(FMGConnString);
            DbCommand dbcTemple = objDB.GetStoredProcCommand("dbo.FMG_GetTemple");
            objDB.AddInParameter(dbcTemple, "@TempleId", DbType.Int32, TempleId);

            return objDB.ExecuteDataSet(dbcTemple);
        }

        public DataSet GetTemple(int ReligionId, decimal CurrentLang, decimal CurrentLat, int WithinXKm)
        {
            objDB = new SqlDatabase(FMGConnString);
            DbCommand dbcTemple = objDB.GetStoredProcCommand("dbo.FMG_GetTempleWithinXKm");
            objDB.AddInParameter(dbcTemple, "@ReligionId", DbType.Int32, ReligionId);
            objDB.AddInParameter(dbcTemple, "@CurrentLang", DbType.Int32, CurrentLang);
            objDB.AddInParameter(dbcTemple, "@CurrentLat", DbType.Int32, CurrentLat);
            objDB.AddInParameter(dbcTemple, "@WithinXKm", DbType.Int32, WithinXKm);
            
            return objDB.ExecuteDataSet(dbcTemple);
        }


        public int  AddTemple(int ReligionId, string TempleName, string TempleOfficeNo, string TempleOfficeAddress, string TempleSynopsis , decimal CurrentLang, decimal CurrentLat)
        {
            try
            {

                //if (!EventLog.SourceExists("dotNET"))
                //    EventLog.CreateEventSource("dotNET", "Application");

                //EventLog.WriteEntry("dotNET", "inside Add Temple");

                objDB = new SqlDatabase(FMGConnString);
                DbCommand dbcTemple = objDB.GetStoredProcCommand("dbo.FMG_AddTemple");
                objDB.AddInParameter(dbcTemple, "@ReligionId", DbType.Int32, ReligionId);
                objDB.AddInParameter(dbcTemple, "@TempleName", DbType.String, TempleName);
                objDB.AddInParameter(dbcTemple, "@TempleOfficeNo", DbType.String, TempleOfficeNo);
                objDB.AddInParameter(dbcTemple, "@TempleOfficeAddress", DbType.String, TempleOfficeAddress);
                objDB.AddInParameter(dbcTemple, "@TempleSynopsis", DbType.String, TempleSynopsis);

                objDB.AddInParameter(dbcTemple, "@CurrentLang", DbType.Decimal, CurrentLang);
                objDB.AddInParameter(dbcTemple, "@CurrentLat", DbType.Decimal, CurrentLat);

                return objDB.ExecuteNonQuery(dbcTemple);
            }
            catch(Exception ex)
            {
                //EventLog.WriteEntry("dotNET", ex.Message);
                return 0; 
            }
        }

    }
}