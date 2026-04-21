using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Contact
/// </summary>
public class Contact
{
    public Contact()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public int Id { get; set; }
    public string UserName { get; set; }
    public string City { get; set; }
    public string PhoneNo { get; set; }
    public string ProductUrl { get; set; }
    public string SourcePage { get; set; }
    public string Comments { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIp { get; set; }
    public string Status { get; set; }


    #region Contact region

    /// <summary>
    /// Get all Contact from db 
    /// </summary>
    /// <param name="conT">DB connection</param>
    /// <returns>All list</returns>
    public static List<Contact> GetAllContact(SqlConnection conT)
    {
        List<Contact> zips = new List<Contact>();
        try
        {
            string query = "Select * from ContactUs where Status !='Deleted' Order by Id Desc ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                zips = (from DataRow dr in dt.Rows
                        select new Contact()
                        {
                            Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                            UserName = Convert.ToString(dr["UserName"]),
                            PhoneNo = Convert.ToString(dr["PhoneNo"]),
                            SourcePage = Convert.ToString(dr["SourcePage"]),
                            City = Convert.ToString(dr["City"]),
                            Comments = Convert.ToString(dr["Comments"]),
                            AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                            AddedIp = Convert.ToString(dr["AddedIp"]),
                            Status = Convert.ToString(dr["Status"])
                        }).ToList();

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllContact", ex.Message);
        }
        return zips;
    }
    /// <summary>
    /// Insert an Contact to database
    /// </summary>
    /// <param name="conT">DB Connection</param>
    /// <param name="cat">Contact properties</param>
    /// <returns>No of rows excuted</returns>
    public static int InsertContact(SqlConnection conT, Contact con)
    {
        int result = 0;
        try
        {
            string query = "Insert Into ContactUs (SourcePage,UserName,PhoneNo,City,Comments,AddedOn,AddedIp,Status) values(@SourcePage,@UserName,@PhoneNo,@City,@Comments,@AddedOn,@AddedIp,@Status) ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@UserName", SqlDbType.NVarChar).Value = con.UserName;
                cmd.Parameters.AddWithValue("@PhoneNo", SqlDbType.NVarChar).Value = con.PhoneNo;
                cmd.Parameters.AddWithValue("@City", SqlDbType.NVarChar).Value = con.City;
                cmd.Parameters.AddWithValue("@Comments", SqlDbType.NVarChar).Value = con.Comments;
                cmd.Parameters.AddWithValue("@SourcePage", SqlDbType.NVarChar).Value = con.SourcePage;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = con.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = con.AddedIp;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = con.Status;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertContact", ex.Message);
        }
        return result;
    }



    /// <summary>
    /// Delete a specific Contact (Update Contact as delete)
    /// </summary>
    /// <param name="conT">DB Connection</param>
    /// <param name="cat">zip properties</param>
    /// <returns>No of rows executed</returns>
    public static int DeleteContact(SqlConnection conT, Contact con)
    {
        int result = 0;

        try
        {
            string query = "Update ContactUs Set Status=@Status, AddedOn=@AddedOn, AddedIp=@AddedIp Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = con.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = con.Status;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = con.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = con.AddedIp;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteContact", ex.Message);
        }
        return result;
    }
    #endregion
}