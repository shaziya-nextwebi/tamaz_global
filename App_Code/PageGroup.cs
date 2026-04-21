using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PageGroup
/// </summary>
public class PageGroup
{
    public PageGroup()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int Id { get; set; }
    public string GroupName { get; set; }
    public string GroupOrder { get; set; }
    public string Status { get; set; }
    public string Icon { get; set; }
    public DateTime UpdatedOn { get; set; }

    public static List<PageGroup> GetAllPageGroup(SqlConnection conT)
    {
        List<PageGroup> categories = new List<PageGroup>();
        try
        {
            string query = "Select * from PageGroup where Status='Active'";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new PageGroup()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  GroupName = Convert.ToString(dr["GroupName"]),
                                  GroupOrder = Convert.ToString(dr["GroupOrder"]),
                                  Icon = Convert.ToString(dr["Icon"]),
                                  UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                                  Status = Convert.ToString(dr["Status"])
                              }).ToList();

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllPageGroup", ex.Message);
        }
        return categories;
    }
    public static int InsertPageGroup(SqlConnection conT, PageGroup cat)
    {
        int result = 0;

        try
        {
            string query = "Insert Into PageGroup (GroupOrder,Icon,GroupName,Status,UpdatedOn) values(@GroupOrder,@Icon,@GroupName,@Status,@UpdatedOn) ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Icon", SqlDbType.NVarChar).Value = cat.Icon;
                cmd.Parameters.AddWithValue("@GroupOrder", SqlDbType.NVarChar).Value = cat.GroupOrder;
                cmd.Parameters.AddWithValue("@GroupName", SqlDbType.NVarChar).Value = cat.GroupName;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = cat.UpdatedOn;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertPageGroup", ex.Message);
        }
        return result;
    }
    public static int UpdatePageGroup(SqlConnection conT, PageGroup cat)
    {
        int result = 0;

        try
        {
            string query = "Update PageGroup Set  Icon=@Icon,GroupName=@GroupName,GroupOrder=@GroupOrder, Status=@Status, UpdatedOn=@UpdatedOn Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Icon", SqlDbType.NVarChar).Value = cat.Icon;
                cmd.Parameters.AddWithValue("@GroupOrder", SqlDbType.NVarChar).Value = cat.GroupOrder;
                cmd.Parameters.AddWithValue("@GroupName", SqlDbType.NVarChar).Value = cat.GroupName;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = cat.UpdatedOn;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdatePageGroup", ex.Message);
        }
        return result;
    }
    public static int DeletePageGroup(SqlConnection conT, PageGroup cat)
    {
        int result = 0;

        try
        {
            string query = "Update PageGroup Set Status=@Status, UpdatedOn=@UpdatedOn  Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Deleted";
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = cat.UpdatedOn;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeletePageGroup", ex.Message);
        }
        return result;
    }

}