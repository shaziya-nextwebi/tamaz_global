using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Blogs
/// </summary>
public class Blogs
{
    public Blogs()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int Id { get; set; }

    public string BlogName { get; set; }
    public string BlogUrl { get; set; }
    public DateTime PostedOn { get; set; }
    public string PageTitle { get; set; }
    public string MetaKeys { get; set; }
    public string BlogTags { get; set; }
    public string MetaDesc { get; set; }
    public string ShortDesc { get; set; }
    public string FullDesc { get; set; }
    public int RowNumber { get; set; }
    public int TotalBlogNo { get; set; }
    public string SmallImg { get; set; }
    public string BigImg { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIP { get; set; }
    public string AddedBy { get; set; }
    public string Status { get; set; }
    public string PostedBy { get; set; }

    #region Blog region

    /// <summary>
    /// Get all  Blogs from db 
    /// </summary>
    /// <param name="conT">DB connection</param>
    /// <returns>All list</returns>
    public static List<Blogs> GetAllBlog(SqlConnection conT)
    {
        List<Blogs> blogs = new List<Blogs>();
        try
        {
            string query = "Select Id,BlogName,BlogUrl,PostedOn,PageTitle,BlogTags,MetaKeys,PostedBy,MetaDesc,SmallImg,BigImg,AddedBy,AddedOn,AddedIP,Status from Blogs where Status='Active'";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                blogs = (from DataRow dr in dt.Rows
                         select new Blogs()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             BlogName = Convert.ToString(dr["BlogName"]),
                             BlogUrl = Convert.ToString(dr["BlogUrl"]),
                             PostedOn = Convert.ToDateTime(Convert.ToString(dr["PostedOn"])),
                             PageTitle = Convert.ToString(dr["PageTitle"]),
                             BlogTags = Convert.ToString(dr["BlogTags"]),
                             MetaKeys = Convert.ToString(dr["MetaKeys"]),
                             MetaDesc = Convert.ToString(dr["MetaDesc"]),
                             //ShortDesc = Convert.ToString(dr["ShortDesc"]),
                             //FullDesc = Convert.ToString(dr["FullDesc"]),
                             PostedBy = Convert.ToString(dr["PostedBy"]),
                             SmallImg = Convert.ToString(dr["SmallImg"]),
                             BigImg = Convert.ToString(dr["BigImg"]),
                             AddedBy = "",
                             AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                             AddedIP = Convert.ToString(dr["AddedIP"]),
                             Status = Convert.ToString(dr["Status"])
                         }).ToList();

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllBlog", ex.Message);
        }
        return blogs;
    }

    public static List<Blogs> GetAllBlogDetailsById_Url(SqlConnection conT, string blogUrl)
    {
        List<Blogs> blogs = new List<Blogs>();
        try
        {
            string query = "Select top 1 *,'' as AddedBy1  from Blogs where Status='Active' and BlogUrl=@BlogUrl";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@BlogUrl", SqlDbType.NVarChar).Value = blogUrl;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                blogs = (from DataRow dr in dt.Rows
                         select new Blogs()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             BlogName = Convert.ToString(dr["BlogName"]),
                             BlogUrl = Convert.ToString(dr["BlogUrl"]),
                             PostedOn = Convert.ToDateTime(Convert.ToString(dr["PostedOn"])),
                             PageTitle = Convert.ToString(dr["PageTitle"]),
                             BlogTags = Convert.ToString(dr["BlogTags"]),
                             MetaKeys = Convert.ToString(dr["MetaKeys"]),
                             MetaDesc = Convert.ToString(dr["MetaDesc"]),
                             ShortDesc = Convert.ToString(dr["ShortDesc"]),
                             FullDesc = Convert.ToString(dr["FullDesc"]),
                             PostedBy = Convert.ToString(dr["PostedBy"]),
                             SmallImg = Convert.ToString(dr["SmallImg"]),
                             BigImg = Convert.ToString(dr["BigImg"]),
                             AddedBy = Convert.ToString(dr["AddedBy1"]),
                             AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                             AddedIP = Convert.ToString(dr["AddedIP"]),
                             Status = Convert.ToString(dr["Status"])
                         }).ToList();

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllBlog", ex.Message);
        }
        return blogs;
    }

    /// <summary>
    /// Get a single  blog for a partcular id
    /// </summary>
    /// <param name="conT">DB Connection</param>
    /// <param name="id">Blog Id</param>
    /// <returns>Get a single blog</returns>
    public static List<Blogs> GetBlog(SqlConnection conT, int id)
    {
        List<Blogs> blogs = new List<Blogs>();
        try
        {
            string query = "Select * from Blogs Where Status='Active' and Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                blogs = (from DataRow dr in dt.Rows
                         select new Blogs()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             BlogName = Convert.ToString(dr["BlogName"]),
                             BlogUrl = Convert.ToString(dr["BlogUrl"]),
                             PostedOn = Convert.ToDateTime(Convert.ToString(dr["PostedOn"])),
                             PageTitle = Convert.ToString(dr["PageTitle"]),
                             PostedBy = Convert.ToString(dr["PostedBy"]),
                             MetaKeys = Convert.ToString(dr["MetaKeys"]),
                             BlogTags = Convert.ToString(dr["BlogTags"]),
                             MetaDesc = Convert.ToString(dr["MetaDesc"]),
                             ShortDesc = Convert.ToString(dr["ShortDesc"]),
                             FullDesc = Convert.ToString(dr["FullDesc"]),
                             SmallImg = Convert.ToString(dr["SmallImg"]),
                             BigImg = Convert.ToString(dr["BigImg"]),
                             AddedBy = Convert.ToString(dr["AddedBy"]),
                             AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                             AddedIP = Convert.ToString(dr["AddedIP"]),
                             Status = Convert.ToString(dr["Status"])
                         }).ToList();

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetBlog", ex.Message);
        }
        return blogs;
    }

    public static List<Blogs> GetAllBlogsd(SqlConnection conT, int rowNo)
    {
        List<Blogs> Blogs = new List<Blogs>();
        try
        {
            string query = "Select top 6 * from ( SELECT ROW_NUMBER() OVER (ORDER BY Id desc) RowNo, Count(Id) Over() as TotalBlogNo, Id,BlogName,BlogUrl,PostedOn,PageTitle,BlogTags,MetaKeys,PostedBy,MetaDesc,SmallImg,BigImg,AddedBy,AddedOn,AddedIP,Status FROM Blogs where Status!='Deleted') as Blgs Where RowNo > @RowNo";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@RowNo", SqlDbType.NVarChar).Value = rowNo;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Blogs = (from DataRow dr in dt.Rows
                         select new Blogs()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             BlogName = Convert.ToString(dr["BlogName"]),
                             RowNumber = Convert.ToInt32(Convert.ToString(dr["RowNo"])),
                             TotalBlogNo = Convert.ToInt32(Convert.ToString(dr["TotalBlogNo"])),
                             BlogUrl = Convert.ToString(dr["BlogUrl"]),
                             PostedOn = Convert.ToDateTime(Convert.ToString(dr["PostedOn"])),
                             PageTitle = Convert.ToString(dr["PageTitle"]),
                             BlogTags = Convert.ToString(dr["BlogTags"]),
                             MetaKeys = Convert.ToString(dr["MetaKeys"]),
                             PostedBy = Convert.ToString(dr["PostedBy"]),
                             MetaDesc = Convert.ToString(dr["MetaDesc"]),
                             ShortDesc = "",
                             FullDesc = "",
                             SmallImg = Convert.ToString(dr["SmallImg"]),
                             BigImg = Convert.ToString(dr["BigImg"]),
                             AddedBy = Convert.ToString(dr["AddedBy"]),
                             AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                             AddedIP = Convert.ToString(dr["AddedIP"]),
                             Status = Convert.ToString(dr["Status"])
                         }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllBlogsd", ex.Message);
        }
        return Blogs;
    }


    /// <summary>
    /// Get a single  blog for a partcular blog name or blog url
    /// </summary>
    /// <param name="conT">DB Connection</param>
    /// <param name="id">Blog Id</param>
    /// <returns>Get a single blog</returns>
    public static List<Blogs> GetBlogByName(SqlConnection conT, string blog)
    {
        List<Blogs> blogs = new List<Blogs>();
        try
        {
            string query = "Select top 1 * from Blogs Where Status='Active' and (BlogUrl=@BlogUrl or BlogName=@BlogName)";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@BlogName", SqlDbType.NVarChar).Value = blog;
                cmd.Parameters.AddWithValue("@BlogUrl", SqlDbType.NVarChar).Value = blog;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                blogs = (from DataRow dr in dt.Rows
                         select new Blogs()
                         {
                             Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                             BlogName = Convert.ToString(dr["BlogName"]),
                             BlogUrl = Convert.ToString(dr["BlogUrl"]),
                             PostedOn = Convert.ToDateTime(Convert.ToString(dr["PostedOn"])),
                             PageTitle = Convert.ToString(dr["PageTitle"]),
                             BlogTags = Convert.ToString(dr["BlogTags"]),
                             MetaKeys = Convert.ToString(dr["MetaKeys"]),
                             PostedBy = Convert.ToString(dr["PostedBy"]),
                             MetaDesc = Convert.ToString(dr["MetaDesc"]),
                             ShortDesc = Convert.ToString(dr["ShortDesc"]),
                             FullDesc = Convert.ToString(dr["FullDesc"]),
                             SmallImg = Convert.ToString(dr["SmallImg"]),
                             BigImg = Convert.ToString(dr["BigImg"]),
                             AddedBy = Convert.ToString(dr["AddedBy"]),
                             AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                             AddedIP = Convert.ToString(dr["AddedIP"]),
                             Status = Convert.ToString(dr["Status"])
                         }).ToList();

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetBlog", ex.Message);
        }
        return blogs;
    }

    /// <summary>
    /// Insert an blog to database
    /// </summary>
    /// <param name="conT">DB Connection</param>
    /// <param name="cat">blog properties</param>
    /// <returns>No of rows excuted</returns>
    public static int WriteBlog(SqlConnection conT, Blogs blog)
    {
        int result = 0;

        try
        {
            string query = "Insert Into Blogs (BlogTags,PostedBy,BlogName,BlogUrl,PostedOn,PageTitle,MetaKeys,MetaDesc,ShortDesc,FullDesc,SmallImg,BigImg,AddedOn,AddedIP,AddedBy,Status) values(@BlogTags,@PostedBy,@BlogName,@BlogUrl,@PostedOn,@PageTitle,@MetaKeys,@MetaDesc,@ShortDesc,@FullDesc,@SmallImg,@BigImg,@AddedOn,@AddedIP,@AddedBy,@Status)";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {

                cmd.Parameters.AddWithValue("@BlogName", SqlDbType.NVarChar).Value = blog.BlogName;

                cmd.Parameters.AddWithValue("@BlogUrl", SqlDbType.NVarChar).Value = blog.BlogUrl;
                cmd.Parameters.AddWithValue("@PostedOn", SqlDbType.NVarChar).Value = blog.PostedOn;
                cmd.Parameters.AddWithValue("@MetaKeys", SqlDbType.NVarChar).Value = blog.MetaKeys;
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = blog.MetaDesc;
                cmd.Parameters.AddWithValue("@PostedBy", SqlDbType.NVarChar).Value = blog.PostedBy;
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = blog.PageTitle;
                cmd.Parameters.AddWithValue("@BlogTags", SqlDbType.NVarChar).Value = blog.BlogTags;
                cmd.Parameters.AddWithValue("@ShortDesc", SqlDbType.NVarChar).Value = blog.ShortDesc;
                cmd.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = blog.FullDesc;
                cmd.Parameters.AddWithValue("@SmallImg", SqlDbType.NVarChar).Value = blog.SmallImg;
                cmd.Parameters.AddWithValue("@BigImg", SqlDbType.NVarChar).Value = blog.BigImg;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = blog.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = blog.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIP", SqlDbType.NVarChar).Value = blog.AddedIP;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertWriteBlogs", ex.Message);
        }
        return result;
    }

    /// <summary>
    /// Update an specific blog
    /// </summary>
    /// <param name="conT">DB Connection</param>
    /// <param name="cat">blog properties</param>
    /// <returns>No of rows executed</returns>
    public static int UpdateBlog(SqlConnection conT, Blogs blog)
    {
        int result = 0;

        try
        {
            string query = "Update Blogs Set PostedBy=@PostedBy,BlogTags=@BlogTags,BlogName=@BlogName,BlogUrl=@BlogUrl,PostedOn=@PostedOn,PageTitle=@PageTitle,MetaKeys=@MetaKeys,MetaDesc=@MetaDesc,ShortDesc=@ShortDesc,FullDesc=@FullDesc,SmallImg=@SmallImg,BigImg=@BigImg,AddedOn=@AddedOn,AddedIP=@AddedIP,AddedBy=@AddedBy,Status=@Status Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = blog.Id;
                cmd.Parameters.AddWithValue("@BlogTags", SqlDbType.NVarChar).Value = blog.BlogTags;
                cmd.Parameters.AddWithValue("@BlogName", SqlDbType.NVarChar).Value = blog.BlogName;
                cmd.Parameters.AddWithValue("@BlogUrl", SqlDbType.NVarChar).Value = blog.BlogUrl;
                cmd.Parameters.AddWithValue("@PostedOn", SqlDbType.NVarChar).Value = blog.PostedOn;
                cmd.Parameters.AddWithValue("@PostedBy", SqlDbType.NVarChar).Value = blog.PostedBy;
                cmd.Parameters.AddWithValue("@MetaKeys", SqlDbType.NVarChar).Value = blog.MetaKeys;
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = blog.MetaDesc;
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = blog.PageTitle;
                cmd.Parameters.AddWithValue("@ShortDesc", SqlDbType.NVarChar).Value = blog.ShortDesc;
                cmd.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = blog.FullDesc;
                cmd.Parameters.AddWithValue("@SmallImg", SqlDbType.NVarChar).Value = blog.SmallImg;
                cmd.Parameters.AddWithValue("@BigImg", SqlDbType.NVarChar).Value = blog.BigImg;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = blog.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = blog.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIP", SqlDbType.NVarChar).Value = blog.AddedIP;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateBlog", ex.Message);
        }
        return result;
    }

    /// <summary>
    /// Delete a specific blog (Update status as delete)
    /// </summary>
    /// <param name="conT">DB Connection</param>
    /// <param name="cat">blog properties</param>
    /// <returns>No of rows executed</returns>
    public static int DeleteBlog(SqlConnection conT, Blogs cat)
    {
        int result = 0;

        try
        {
            string query = "Update Blogs Set Status=@Status,AddedBy=@AddedBy, AddedOn=@AddedOn, AddedIP=@AddedIP Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Deleted";
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@AddedIP", SqlDbType.NVarChar).Value = cat.AddedIP;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteBlog", ex.Message);
        }
        return result;
    }
    #endregion
}