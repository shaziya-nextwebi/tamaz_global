using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserRoles
/// </summary>
public class UserRoles
{
    public UserRoles()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int Id { get; set; }
    public string RoleName { get; set; }
    public string Status { get; set; }
   
    public DateTime UpdatedOn { get; set; }

    public static List<UserRoles> GetAllUserRoles(SqlConnection conT)
    {
        List<UserRoles> categories = new List<UserRoles>();
        try
        {
            string query = "Select * from UserRoles where Status='Active'";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new UserRoles()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  RoleName = Convert.ToString(dr["RoleName"]),

                                  UpdatedOn = Convert.ToDateTime(Convert.ToString(dr["UpdatedOn"])),
                                  Status = Convert.ToString(dr["Status"])
                              }).ToList();

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllUserRoles", ex.Message);
        }
        return categories;
    }
    public static int InsertUserRoles(SqlConnection conT, UserRoles cat)
    {
        int result = 0;

        try
        {
            string query = "Insert Into UserRoles (RoleName,Status,UpdatedOn) values(@RoleName,@Status,@UpdatedOn) ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {

                cmd.Parameters.AddWithValue("@RoleName", SqlDbType.NVarChar).Value = cat.RoleName;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = cat.UpdatedOn;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertUserRoles", ex.Message);
        }
        return result;
    }
    public static int UpdateUserRoles(SqlConnection conT, UserRoles cat)
    {
        int result = 0;

        try
        {
            string query = "Update UserRoles Set  RoleName=@RoleName, Status=@Status, UpdatedOn=@UpdatedOn Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@RoleName", SqlDbType.NVarChar).Value = cat.RoleName;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = cat.UpdatedOn;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateUserRoles", ex.Message);
        }
        return result;
    }
    public static int DeleteUserRoles(SqlConnection conT, UserRoles cat)
    {
        int result = 0;

        try
        {
            string query = "Update UserRoles Set Status=@Status, UpdatedOn=@UpdatedOn  Where Id=@Id ";
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteUserRoles", ex.Message);
        }
        return result;
    }


    public static List<UserRolesAccess> GetAllUserRoles(SqlConnection conT, string roleId)
    {
        List<UserRolesAccess> categories = new List<UserRolesAccess>();
        try
        {
            string query = "GetUserRoleAccess";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RoleId", SqlDbType.NVarChar).Value = roleId;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new UserRolesAccess()
                              {
                                  AddAccess = Convert.ToString(dr["AddAccess"]),
                                  DeleteAccess = Convert.ToString(dr["DeleteAccess"]),
                                  EditAccess = Convert.ToString(dr["EditAccess"]),
                                  PageDesc = Convert.ToString(dr["PageDesc"]),
                                  PageGroupId = Convert.ToString(dr["PageGroupId"]),
                                  PageGroupName = Convert.ToString(dr["PageGroupName"]),
                                  PageId = Convert.ToString(dr["PageId"]),
                                  PageLink = Convert.ToString(dr["PageLink"]),
                                  PageName = Convert.ToString(dr["PageName"]),
                                  GOrderBy = Convert.ToString(dr["GroupOrder"]) == "" ? 0: Convert.ToInt32(Convert.ToString(dr["GroupOrder"])),
                                  ViewAccess = Convert.ToString(dr["ViewAccess"])
                              }).ToList();

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllUserRoles", ex.Message);
        }
        return categories;
    }

    public static List<UserRolesAccess> GetAllUserRolesCheck(SqlConnection conT, string roleId, string pid)
    {
        List<UserRolesAccess> categories = new List<UserRolesAccess>();
        try
        {
            string query = "Select top 1 * from UserRolesAccess Where PageId=@PageId and RoleId=@RoleId";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            { 
                cmd.Parameters.AddWithValue("@RoleId", SqlDbType.NVarChar).Value = roleId;
                cmd.Parameters.AddWithValue("@PageId", SqlDbType.NVarChar).Value = pid;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new UserRolesAccess()
                              {
                                  AddAccess = Convert.ToString(dr["AddAccess"]),
                                  DeleteAccess = Convert.ToString(dr["DeleteAccess"]),
                                  EditAccess = Convert.ToString(dr["EditAccess"]), 
                                  PageId = Convert.ToString(dr["PageId"])
                              }).ToList();

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllUserRoles", ex.Message);
        }
        return categories;
    }

    public static int InsertToRoleAccess(SqlConnection conT, UserRolesAccess cat)
    {
        int result = 0;

        try
        {
            string query = "Insert Into UserRolesAccess (RoleId,PageId,PageGroupId,AddAccess,EditAccess,DeleteAccess,ViewAccess,Status,UpdatedOn) values(@RoleId,@PageId,@PageGroupId,@AddAccess,@EditAccess,@DeleteAccess,@ViewAccess,@Status,@UpdatedOn) ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@RoleId", SqlDbType.NVarChar).Value = cat.RoleId;
                cmd.Parameters.AddWithValue("@PageId", SqlDbType.NVarChar).Value = cat.PageId;
                cmd.Parameters.AddWithValue("@PageGroupId", SqlDbType.NVarChar).Value = cat.PageGroupId;
                cmd.Parameters.AddWithValue("@AddAccess", SqlDbType.NVarChar).Value = cat.AddAccess;
                cmd.Parameters.AddWithValue("@EditAccess", SqlDbType.NVarChar).Value = cat.EditAccess;
                cmd.Parameters.AddWithValue("@DeleteAccess", SqlDbType.NVarChar).Value = cat.DeleteAccess;
                cmd.Parameters.AddWithValue("@ViewAccess", SqlDbType.NVarChar).Value = cat.ViewAccess;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = cat.UpdatedOn;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertUserRoles", ex.Message);
        }
        return result;
    }

    public static int UpdateRoleAccess(SqlConnection conT, UserRolesAccess cat)
    {
        int result = 0;

        try
        {
            string query = "Update UserRolesAccess Set AddAccess=@AddAccess,EditAccess=@EditAccess,DeleteAccess=@DeleteAccess,ViewAccess=@ViewAccess, UpdatedOn=@UpdatedOn Where RoleId=@RoleId and PageId=@PageId ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@RoleId", SqlDbType.NVarChar).Value = cat.RoleId;
                cmd.Parameters.AddWithValue("@PageId", SqlDbType.NVarChar).Value = cat.PageId; 
                cmd.Parameters.AddWithValue("@AddAccess", SqlDbType.NVarChar).Value = cat.AddAccess;
                cmd.Parameters.AddWithValue("@EditAccess", SqlDbType.NVarChar).Value = cat.EditAccess;
                cmd.Parameters.AddWithValue("@DeleteAccess", SqlDbType.NVarChar).Value = cat.DeleteAccess;
                cmd.Parameters.AddWithValue("@ViewAccess", SqlDbType.NVarChar).Value = cat.ViewAccess; 
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.NVarChar).Value = cat.UpdatedOn;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertUserRoles", ex.Message);
        }
        return result;
    }
}

public class UserRolesAccess
{
    public int Id { get; set; }
    public string RoleId { get; set; }
    public string PageId { get; set; }
    public string PageGroupName { get; set; }
    public string PageName { get; set; }
    public string PageLink { get; set; }
    public string PageDesc { get; set; }
    public string PageGroupId { get; set; }
    public string AddAccess { get; set; }
    public string EditAccess { get; set; }
    public string DeleteAccess { get; set; }
    public string ViewAccess { get; set; }
    public string Status { get; set; }
    public DateTime UpdatedOn { get; set; }
    public int GOrderBy { get; set; }

}