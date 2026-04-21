using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BlogTags
/// </summary>
public class BlogTags
{
    public BlogTags()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int Id { get; set; }
    public string TagName { get; set; }
    public string TagUrl { get; set; }
    public int DisplayOrder { get; set; }
    public string MetaKeys { get; set; }
    public string MetaDesc { get; set; }
    public string PageTitle { get; set; }
    public string CCount { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIP { get; set; }
    public string Status { get; set; }
    public string AddedBy { get; set; }

    #region Admin blog tags region

    /// <summary>
    /// Get all  BlogTags from db 
    /// </summary>
    /// <param name="conT">DB connection</param>
    /// <returns>All list</returns>
    public static List<BlogTags> GetAllBlogTags(SqlConnection conT)
    {
        List<BlogTags> categories = new List<BlogTags>();
        try
        {
            string query = "Select *,(Select Top 1 UserName From CreateUser Where UserGuid=BlogTags.AddedBy) as AddedBy1 from BlogTags where Status='Active' Order by TagName ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new BlogTags()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  TagName = Convert.ToString(dr["TagName"]),
                                  TagUrl = Convert.ToString(dr["TagUrl"]),
                                  PageTitle = Convert.ToString(dr["PageTitle"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
                                  MetaKeys = Convert.ToString(dr["MetaKeys"]),
                                  MetaDesc = Convert.ToString(dr["MetaDesc"]),
                                  DisplayOrder = Convert.ToString(dr["DisplayOrder"]) == "" ? 0 : Convert.ToInt32(Convert.ToString(dr["DisplayOrder"])),
                                  AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                  AddedIP = Convert.ToString(dr["AddedIP"]),
                                  Status = Convert.ToString(dr["Status"])
                              }).ToList();

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllBlogTags", ex.Message);
        }
        return categories;
    }



    /// <summary>
    /// Insert an BlogTags to database
    /// </summary>
    /// <param name="conT">DB Connection</param>
    /// <param name="cat">BlogTags properties</param>
    /// <returns>No of rows excuted</returns>
    public static int InsertBlogTags(SqlConnection conT, BlogTags cat)
    {
        int result = 0;

        try
        {
            string query = "Insert Into BlogTags (TagUrl,DisplayOrder,TagName,PageTitle,MetaKeys,MetaDesc,AddedOn, AddedIP,Status,AddedBy) values(@TagUrl,@DisplayOrder,@TagName,@PageTitle,@MetaKeys,@MetaDesc,@AddedOn, @AddedIP,@Status,@AddedBy) ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@TagName", SqlDbType.NVarChar).Value = cat.TagName;
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = cat.PageTitle;
                cmd.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                cmd.Parameters.AddWithValue("@TagUrl", SqlDbType.NVarChar).Value = cat.TagUrl;
                cmd.Parameters.AddWithValue("@MetaKeys", SqlDbType.NVarChar).Value = cat.MetaKeys;
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = cat.MetaDesc;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIP", SqlDbType.NVarChar).Value = cat.AddedIP;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertBlogTags", ex.Message);
        }
        return result;
    }

    /// <summary>
    /// Update an specific BlogTags
    /// </summary>
    /// <param name="conT">DB Connection</param>
    /// <param name="cat">BlogTags properties</param>
    /// <returns>No of rows executed</returns>
    public static int UpdateBlogTags(SqlConnection conT, BlogTags cat)
    {
        int result = 0;

        try
        {
            string query = "Update BlogTags Set TagUrl=@TagUrl, TagName=@TagName,DisplayOrder=@DisplayOrder,PageTitle=@PageTitle,MetaKeys=@MetaKeys,MetaDesc=@MetaDesc, AddedOn=@AddedOn, AddedIP=@AddedIP,AddedBy=@AddedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@TagName", SqlDbType.NVarChar).Value = cat.TagName;
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = cat.PageTitle;
                cmd.Parameters.AddWithValue("@TagUrl", SqlDbType.NVarChar).Value = cat.TagUrl;
                cmd.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                cmd.Parameters.AddWithValue("@MetaKeys", SqlDbType.NVarChar).Value = cat.MetaKeys;
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = cat.MetaDesc;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIP", SqlDbType.NVarChar).Value = cat.AddedIP;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateBlogTags", ex.Message);
        }
        return result;
    }

    /// <summary>
    /// Delete a specific BlogTags (Update status as delete)
    /// </summary>
    /// <param name="conT">DB Connection</param>
    /// <param name="cat">BlogTags properties</param>
    /// <returns>No of rows executed</returns>
    public static int DeleteBlogTags(SqlConnection conT, BlogTags cat)
    {
        int result = 0;

        try
        {
            string query = "Update BlogTags Set Status=@Status, AddedOn=@AddedOn, AddedIP=@AddedIP,AddedBy=@AddedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Deleted";
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIP", SqlDbType.NVarChar).Value = cat.AddedIP;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteBlogTags", ex.Message);
        }
        return result;
    }
    #endregion

}