using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProductPrices
/// </summary>
public class ProductGallery
{
    public ProductGallery()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int Id { get; set; }
    public string Pid { get; set; }
    public string Images { get; set; }
    public string AddedBy { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIp { get; set; }
    public string Status { get; set; }
    public string GalleryType { get; set; }
    public int GalleryOrder { get; set; }

    #region Admin category region

    /// <summary>
    /// Get all  category from db 
    /// </summary>
    /// <param name="conT">DB connection</param>
    /// <returns>All list</returns>
    public static List<ProductGallery> GetAllProductGallery(SqlConnection conT, string Pid)
    {
        List<ProductGallery> categories = new List<ProductGallery>();
        try
        {
            string query = "Select *, (Select Top 1 UserName From CreateUser Where UserGuid=ProductGallery.AddedBy) as AddedBy1 from ProductGallery where Status='Active' and Pid=@Pid Order by GalleryOrder";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Pid", SqlDbType.NVarChar).Value = Pid;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new ProductGallery()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  Pid = Convert.ToString(dr["Pid"]),
                                  Images = Convert.ToString(dr["Images"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
                                  AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                  AddedIp = Convert.ToString(dr["AddedIp"]),
                                  Status = Convert.ToString(dr["Status"]),
                                  GalleryType = Convert.ToString(dr["GalleryType"]),
                                  GalleryOrder = Convert.ToInt32(dr["GalleryOrder"])
                              }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductGallery", ex.Message);
        }
        return categories;
    }



    /// <summary>
    /// Insert an category to database
    /// </summary>
    /// <param name="conT">DB Connection</param>
    /// <param name="cat">Category properties</param>
    /// <returns>No of rows excuted</returns>
    public static int InsertProductGallery(SqlConnection conT, ProductGallery cat)
    {
        int result = 0;
        try
        {
            string query = "Insert Into ProductGallery (Pid,Images,AddedBy,AddedOn,AddedIp,Status,GalleryType,GalleryOrder) values(@Pid,@Images,@AddedBy,@AddedOn,@AddedIp,@Status,@GalleryType,@GalleryOrder)";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Pid", SqlDbType.NVarChar).Value = cat.Pid;
                cmd.Parameters.AddWithValue("@Images", SqlDbType.NVarChar).Value = cat.Images;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                cmd.Parameters.AddWithValue("@GalleryType", SqlDbType.NVarChar).Value = cat.GalleryType;
                cmd.Parameters.AddWithValue("@GalleryOrder", SqlDbType.NVarChar).Value = cat.GalleryOrder;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertProductGallery", ex.Message);
        }
        return result;
    }

    /// <summary>
    /// Update an specific category
    /// </summary>
    /// <param name="conT">DB Connection</param>
    /// <param name="cat">Category properties</param>
    /// <returns>No of rows executed</returns>
    public static int UpdateProductGallery(SqlConnection conT, ProductGallery cat)
    {
        int result = 0;

        try
        {
            string query = "Update ProductGallery Set Pid=@Pid,Images=@Images,AddedBy=@AddedBy,AddedOn=@AddedOn,AddedIp=@AddedIp,Status=@Status,GalleryType=@GalleryType Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Pid", SqlDbType.NVarChar).Value = cat.Pid;
                cmd.Parameters.AddWithValue("@Question", SqlDbType.NVarChar).Value = cat.Images;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                cmd.Parameters.AddWithValue("@GalleryType", SqlDbType.NVarChar).Value = cat.GalleryType;
                //cmd.Parameters.AddWithValue("@GalleryOrder", SqlDbType.NVarChar).Value = cat.GalleryOrder;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateProductGallery", ex.Message);
        }
        return result;
    }

    /// <summary>
    /// Delete a specific category (Update status as delete)
    /// </summary>
    /// <param name="conT">DB Connection</param>
    /// <param name="cat">Category properties</param>
    /// <returns>No of rows executed</returns>
    public static int DeleteProductGallery(SqlConnection conT, ProductGallery cat)
    {
        int result = 0;

        try
        {
            string query = "Update ProductGallery Set Status=@Status, AddedOn=@AddedOn, AddedIp=@AddedIp,AddedBy=@AddedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteProductGallery", ex.Message);
        }
        return result;
    }
    public static int UpdateProductGalleryOrder(SqlConnection conT, ProductGallery pro)
    {
        int result = 0;
        try
        {
            string query = "Update ProductGallery Set  GalleryOrder=@GalleryOrder  Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@GalleryOrder", SqlDbType.NVarChar).Value = pro.GalleryOrder;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = pro.Id;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateProductGalleryOrder", ex.Message);
        }
        return result;
    }
    #endregion
}