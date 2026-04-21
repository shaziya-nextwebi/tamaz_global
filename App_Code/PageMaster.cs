using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PageMaster
/// </summary>
public class PageMaster
{
    public PageMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int Id { get; set; }
    public string PageGroup { get; set; }
    public string GroupName { get; set; }
    public string PageName { get; set; }
    public string PageLink { get; set; }
    public string PageDesc { get; set; }
    public string ShowInMenu { get; set; }
    public string Status { get; set; }
    public string Icon { get; set; }
    public DateTime UpdatedOn { get; set; }

    public static List<PageMaster> GetAllPageMaster(SqlConnection conT)
    {
        List<PageMaster> categories = new List<PageMaster>();
        try
        {
            string query = "Select m.*, p.GroupName from PageMaster as m left join PageGroup as p on p.Id= m.PageGroup where m.Status='Active'";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new PageMaster()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  PageDesc = Convert.ToString(dr["PageDesc"]),
                                  GroupName = Convert.ToString(dr["GroupName"]),
                                  PageGroup = Convert.ToString(dr["PageGroup"]),
                                  ShowInMenu = Convert.ToString(dr["ShowInMenu"]),
                                  PageLink = Convert.ToString(dr["PageLink"]),
                                  PageName = Convert.ToString(dr["PageName"]),
                                  Icon = Convert.ToString(dr["Icon"]),
                                  UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                                  Status = Convert.ToString(dr["Status"])
                              }).ToList();

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllPageMaster", ex.Message);
        }
        return categories;
    }
    public static int InsertPageMaster(SqlConnection conT, PageMaster cat)
    {
        int result = 0;

        try
        {
            string query = "Insert Into PageMaster (ShowInMenu,Icon,PageDesc,PageGroup,PageLink,PageName,Status,UpdatedOn) values(@ShowInMenu,@Icon,@PageDesc,@PageGroup,@PageLink,@PageName,@Status,@UpdatedOn) ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Icon", SqlDbType.NVarChar).Value = cat.Icon;
                cmd.Parameters.AddWithValue("@PageName", SqlDbType.NVarChar).Value = cat.PageName;
                cmd.Parameters.AddWithValue("@ShowInMenu", SqlDbType.NVarChar).Value = cat.ShowInMenu;
                cmd.Parameters.AddWithValue("@PageDesc", SqlDbType.NVarChar).Value = cat.PageDesc;
                cmd.Parameters.AddWithValue("@PageGroup", SqlDbType.NVarChar).Value = cat.PageGroup;
                cmd.Parameters.AddWithValue("@PageLink", SqlDbType.NVarChar).Value = cat.PageLink; 
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = cat.UpdatedOn;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertPageMaster", ex.Message);
        }
        return result;
    }
    public static int UpdatePageMaster(SqlConnection conT, PageMaster cat)
    {
        int result = 0;

        try
        {
            string query = "Update PageMaster Set Icon=@Icon,ShowInMenu=@ShowInMenu, PageDesc=@PageDesc,PageGroup=@PageGroup,PageLink=@PageLink,PageName=@PageName,Status=@Status, UpdatedOn=@UpdatedOn  Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Icon", SqlDbType.NVarChar).Value = cat.Icon;
                cmd.Parameters.AddWithValue("@ShowInMenu", SqlDbType.NVarChar).Value = cat.ShowInMenu;
                cmd.Parameters.AddWithValue("@PageDesc", SqlDbType.NVarChar).Value = cat.PageDesc;
                cmd.Parameters.AddWithValue("@PageGroup", SqlDbType.NVarChar).Value = cat.PageGroup;
                cmd.Parameters.AddWithValue("@PageLink", SqlDbType.NVarChar).Value = cat.PageLink;
                cmd.Parameters.AddWithValue("@PageName", SqlDbType.NVarChar).Value = cat.PageName;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = cat.UpdatedOn;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdatePageMaster", ex.Message);
        }
        return result;
    }
    public static int DeletePageMaster(SqlConnection conT, PageMaster cat)
    {
        int result = 0;

        try
        {
            string query = "Update PageMaster Set Status=@Status, UpdatedOn=@UpdatedOn  Where Id=@Id ";
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeletePageMaster", ex.Message);
        }
        return result;
    }

}