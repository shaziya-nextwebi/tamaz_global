using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Category
/// </summary>
public class Category
{
    public Category()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string CategoryUrl { get; set; }
    public string DisplayHome { get; set; }
    public int DisplayOrder { get; set; }
    public string MetaKeys { get; set; }
    public string MetaDesc { get; set; }
    public string PageTitle { get; set; }
    public string CCount { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIP { get; set; }
    public string Status { get; set; }
    public string AddedBy { get; set; }
    public string ShortDesc { get; set; }
    public string FullDesc { get; set; }
    public string BannerImage { get; set; }
    public string MobileImage { get; set; }

    #region Admin category region

    /// <summary>
    /// Get all  category from db 
    /// </summary>
    /// <param name="conT">DB connection</param>
    /// <returns>All list</returns>
    public static List<Category> GetAllCategory(SqlConnection conT)
    {
        List<Category> categories = new List<Category>();
        try
        {
            string query = "Select *, (Select Top 1 UserName From CreateUser Where UserGuid=Category.AddedBy) as AddedBy1 from Category where Status='Active' Order by CategoryName ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new Category()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  CategoryName = Convert.ToString(dr["CategoryName"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
                                  PageTitle = Convert.ToString(dr["PageTitle"]),
                                  CategoryUrl = Convert.ToString(dr["CategoryUrl"]),
                                  ShortDesc = Convert.ToString(dr["ShortDesc"]),
                                  FullDesc = Convert.ToString(dr["FullDesc"]),
                                  MetaKeys = Convert.ToString(dr["MetaKeys"]),
                                  MetaDesc = Convert.ToString(dr["MetaDesc"]),
                                  DisplayHome = Convert.ToString(dr["DisplayHome"]),
                                  DisplayOrder = Convert.ToString(dr["DisplayOrder"]) == "" ? 0 : Convert.ToInt32(Convert.ToString(dr["DisplayOrder"])),
                                  AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                  AddedIP = Convert.ToString(dr["AddedIP"]),
                                  Status = Convert.ToString(dr["Status"]),
                                  BannerImage = Convert.ToString(dr["BannerImage"]),
                                  MobileImage = Convert.ToString(dr["MobileImage"])
                              }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllCategory", ex.Message);
        }
        return categories;
    }
    public static List<Category> GetTopCategory(SqlConnection conT)
    {
        List<Category> categories = new List<Category>();
        try
        {
            string query = @"SELECT c.*, 
                        (SELECT TOP 1 UserName 
                         FROM CreateUser 
                         WHERE UserGuid = c.AddedBy) AS AddedBy1
                        FROM Category c
                        WHERE c.Status = 'Active'
                          AND c.DisplayHome = 'Yes'
                        ORDER BY c.CategoryName";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new Category()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  CategoryName = Convert.ToString(dr["CategoryName"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
                                  PageTitle = Convert.ToString(dr["PageTitle"]),
                                  CategoryUrl = Convert.ToString(dr["CategoryUrl"]),
                                  ShortDesc = Convert.ToString(dr["ShortDesc"]),
                                  FullDesc = Convert.ToString(dr["FullDesc"]),
                                  MetaKeys = Convert.ToString(dr["MetaKeys"]),
                                  MetaDesc = Convert.ToString(dr["MetaDesc"]),
                                  DisplayHome = Convert.ToString(dr["DisplayHome"]),
                                  DisplayOrder = Convert.ToString(dr["DisplayOrder"]) == "" ? 0 : Convert.ToInt32(Convert.ToString(dr["DisplayOrder"])),
                                  AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                  AddedIP = Convert.ToString(dr["AddedIP"]),
                                  Status = Convert.ToString(dr["Status"]),
                                  BannerImage = Convert.ToString(dr["BannerImage"]),
                                  MobileImage = Convert.ToString(dr["MobileImage"])
                              }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllCategory", ex.Message);
        }
        return categories;
    }



    /// <summary>
    /// Insert an category to database
    /// </summary>
    /// <param name="conT">DB Connection</param>
    /// <param name="cat">Category properties</param>
    /// <returns>No of rows excuted</returns>
    public static int InsertCategory(SqlConnection conT, Category cat)
    {
        int result = 0;

        try
        {
            string query = "Insert Into Category (BannerImage,MobileImage,CategoryUrl,ShortDesc,FullDesc,DisplayHome,DisplayOrder,CategoryName,PageTitle,MetaKeys,MetaDesc,AddedOn, AddedIP,Status,AddedBy) values(@BannerImage,@MobileImage,@CategoryUrl,@ShortDesc,@FullDesc,@DisplayHome,@DisplayOrder,@CategoryName,@PageTitle,@MetaKeys,@MetaDesc,@AddedOn, @AddedIP,@Status,@AddedBy) ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@CategoryUrl", SqlDbType.NVarChar).Value = cat.CategoryUrl;
                cmd.Parameters.AddWithValue("@CategoryName", SqlDbType.NVarChar).Value = cat.CategoryName;
                cmd.Parameters.AddWithValue("@ShortDesc", SqlDbType.NVarChar).Value = cat.ShortDesc;
                cmd.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = cat.FullDesc;
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = cat.PageTitle;
                cmd.Parameters.AddWithValue("@DisplayHome", SqlDbType.NVarChar).Value = cat.DisplayHome;
                cmd.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                cmd.Parameters.AddWithValue("@MetaKeys", SqlDbType.NVarChar).Value = cat.MetaKeys;
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = cat.MetaDesc;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIP", SqlDbType.NVarChar).Value = cat.AddedIP;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@BannerImage", SqlDbType.NVarChar).Value = cat.BannerImage;
                cmd.Parameters.AddWithValue("@MobileImage", SqlDbType.NVarChar).Value = cat.MobileImage;

                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertCategory", ex.Message);
        }
        return result;
    }

    /// <summary>
    /// Update an specific category
    /// </summary>
    /// <param name="conT">DB Connection</param>
    /// <param name="cat">Category properties</param>
    /// <returns>No of rows executed</returns>
    public static int UpdateCategory(SqlConnection conT, Category cat)
    {
        int result = 0;

        try
        {
            string query = "Update Category Set BannerImage=@BannerImage,MobileImage=@MobileImage,ShortDesc =@ShortDesc,CategoryUrl=@CategoryUrl,FullDesc=@FullDesc,CategoryName=@CategoryName,DisplayHome=@DisplayHome,DisplayOrder=@DisplayOrder,PageTitle=@PageTitle,MetaKeys=@MetaKeys,MetaDesc=@MetaDesc, AddedOn=@AddedOn, AddedIP=@AddedIP,AddedBy=@AddedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@CategoryName", SqlDbType.NVarChar).Value = cat.CategoryName;
                cmd.Parameters.AddWithValue("@CategoryUrl", SqlDbType.NVarChar).Value = cat.CategoryUrl;
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = cat.PageTitle;
                cmd.Parameters.AddWithValue("@ShortDesc", SqlDbType.NVarChar).Value = cat.ShortDesc;
                cmd.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = cat.FullDesc;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@DisplayHome", SqlDbType.NVarChar).Value = cat.DisplayHome;
                cmd.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = cat.DisplayOrder;
                cmd.Parameters.AddWithValue("@MetaKeys", SqlDbType.NVarChar).Value = cat.MetaKeys;
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = cat.MetaDesc;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIP", SqlDbType.NVarChar).Value = cat.AddedIP;
                cmd.Parameters.AddWithValue("@BannerImage", SqlDbType.NVarChar).Value = cat.BannerImage;
                cmd.Parameters.AddWithValue("@MobileImage", SqlDbType.NVarChar).Value = cat.MobileImage;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateCategory", ex.Message);
        }
        return result;
    }

    /// <summary>
    /// Delete a specific category (Update status as delete)
    /// </summary>
    /// <param name="conT">DB Connection</param>
    /// <param name="cat">Category properties</param>
    /// <returns>No of rows executed</returns>
    public static int DeleteCategory(SqlConnection conT, Category cat)
    {
        int result = 0;

        try
        {
            string query = "Update Category Set Status=@Status, AddedOn=@AddedOn, AddedIP=@AddedIP,AddedBy=@AddedBy Where Id=@Id ";
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteCategory", ex.Message);
        }
        return result;
    }
    #endregion

}

public class CategoryOrder
{
    public int Id { get; set; }
    public string Category { get; set; }
    public string ProductName { get; set; }
    public string Image { get; set; }
    public string DiscountPrice { get; set; }
}