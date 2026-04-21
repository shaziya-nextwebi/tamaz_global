using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class ExceptionCapture
{
    public string OriginPage { get; set; }
    public string OriginBlock { get; set; }
    public string Excep { get; set; }

    public static void CaptureException(string pg, string block, string excep)
    {
        try
        {
            if (excep.Trim() == "Thread was being aborted.")
            {
                return;
            }
            SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
            SqlCommand cmd = new SqlCommand("insert into CaptureException (Ex_Page, Ex_Block, Excep, Added_On, Added_IP) values(@Ex_Page, @Ex_Block, @Excep, @Added_On, @Added_IP)", conT);
            cmd.Parameters.AddWithValue("@Ex_Page", SqlDbType.NVarChar).Value = pg;
            cmd.Parameters.AddWithValue("@Ex_Block", SqlDbType.NVarChar).Value = block;
            cmd.Parameters.AddWithValue("@Excep", SqlDbType.NVarChar).Value = excep;
            cmd.Parameters.AddWithValue("@Added_On", SqlDbType.DateTime).Value = TimeStamps.UTCTime();
            cmd.Parameters.AddWithValue("@Added_IP", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
            conT.Open();
            int res = cmd.ExecuteNonQuery();
            conT.Close();
        }
        catch (Exception ex)
        {

        }
    }



}
