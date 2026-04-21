using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

public class NewsDetails
{
    public int Id { get; set; }
    public string NewsTitle { get; set; }
    public string NewsUrl { get; set; }
    public string Author { get; set; }
    public string PostedOn { get; set; }
    public string ShortDesc { get; set; }
    public string FullDesc { get; set; }
    public string Tags { get; set; }
    public bool Featured { get; set; }
    public string ThumbImage { get; set; }
    public string Status { get; set; }
    public string PageTitle { get; set; }
    public string MetaKeys { get; set; }
    public string MetaDesc { get; set; }
    public string AddedBy { get; set; }
    public string AddedIP { get; set; }
    public DateTime AddedOn { get; set; }
    public string Html { get; set; }
    public int TotalCount { get; set; }

    #region Fetch News by ID
    public static List<NewsDetails> GetAllNewsDetailsWithId(SqlConnection con, int id)
    {
        List<NewsDetails> lst = new List<NewsDetails>();

        try
        {
            string query = "Select * From NewsDetails Where Id=@Id";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        NewsDetails ND = new NewsDetails();

                        ND.Id = Convert.ToInt32(dr["Id"]);
                        ND.NewsTitle = dr["NewsTitle"].ToString();
                        ND.NewsUrl = dr["NewsUrl"].ToString();
                        ND.Author = dr["Author"].ToString();
                        ND.PostedOn = dr["PostedOn"].ToString();
                        ND.ShortDesc = dr["ShortDesc"].ToString();
                        ND.FullDesc = dr["FullDesc"].ToString();
                        ND.Tags = dr["Tags"].ToString();
                        ND.Featured = Convert.ToBoolean(dr["Featured"]);
                        ND.ThumbImage = dr["ThumbImage"].ToString();
                        ND.PageTitle = dr["PageTitle"].ToString();
                        ND.MetaKeys = dr["MetaKeys"].ToString();
                        ND.MetaDesc = dr["MetaDesc"].ToString();
                        ND.AddedBy = dr["AddedBy"].ToString();
                        ND.AddedIP = dr["AddedIP"].ToString();
                        ND.AddedOn = Convert.ToDateTime(dr["AddedOn"]);

                        lst.Add(ND);
                    }
                }
                con.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllNewsDetailsWithId", ex.Message);
        }

        return lst;
    }

    #endregion
    public static List<NewsDetails> GetFeaturedNews(SqlConnection con)
    {
        List<NewsDetails> lst = new List<NewsDetails>();

        try
        {
            string query = "SELECT * FROM NewsDetails WHERE Featured = @Featured and Status='Active' ORDER BY AddedOn DESC";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Featured", true);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        NewsDetails ND = new NewsDetails();

                        ND.Id = Convert.ToInt32(dr["Id"]);
                        ND.NewsTitle = dr["NewsTitle"].ToString();
                        ND.NewsUrl = dr["NewsUrl"].ToString();
                        ND.Author = dr["Author"].ToString();
                        ND.PostedOn = dr["PostedOn"].ToString();
                        ND.ShortDesc = dr["ShortDesc"].ToString();
                        ND.FullDesc = dr["FullDesc"].ToString();
                        ND.Tags = dr["Tags"].ToString();
                        ND.Featured = Convert.ToBoolean(dr["Featured"]);
                        ND.ThumbImage = dr["ThumbImage"].ToString();
                        ND.PageTitle = dr["PageTitle"].ToString();
                        ND.MetaKeys = dr["MetaKeys"].ToString();
                        ND.MetaDesc = dr["MetaDesc"].ToString();
                        ND.AddedBy = dr["AddedBy"].ToString();
                        ND.AddedIP = dr["AddedIP"].ToString();
                        ND.AddedOn = Convert.ToDateTime(dr["AddedOn"]);

                        lst.Add(ND);
                    }
                }
                con.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery,
                "GetFeaturedNews",
                ex.Message);
        }

        return lst;
    }

    #region Insert News
    public static int InsertNewsDetails(SqlConnection con, NewsDetails ND)
    {
        int result = 0;

        try
        {
            string query = "Insert Into NewsDetails " +
                           "(NewsTitle,NewsUrl,Author,PostedOn,ShortDesc,FullDesc,Tags,ThumbImage,PageTitle,MetaKeys,MetaDesc,AddedBy,AddedIP,AddedOn,Featured) values " +
                           "(@NewsTitle,@NewsUrl,@Author,@PostedOn,@ShortDesc,@FullDesc,@Tags,@ThumbImage,@PageTitle,@MetaKeys,@MetaDesc,@AddedBy,@AddedIP,@AddedOn,@Featured)";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@NewsTitle", SqlDbType.NVarChar).Value = ND.NewsTitle;
                cmd.Parameters.AddWithValue("@NewsUrl", SqlDbType.NVarChar).Value = ND.NewsUrl;
                cmd.Parameters.AddWithValue("@Author", SqlDbType.NVarChar).Value = ND.Author;
                cmd.Parameters.AddWithValue("@PostedOn", SqlDbType.NVarChar).Value = ND.PostedOn;
                cmd.Parameters.AddWithValue("@ShortDesc", SqlDbType.NVarChar).Value = ND.ShortDesc;
                cmd.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = ND.FullDesc;
                cmd.Parameters.AddWithValue("@Tags", SqlDbType.NVarChar).Value = ND.Tags ?? "";
                cmd.Parameters.AddWithValue("@ThumbImage", SqlDbType.NVarChar).Value = ND.ThumbImage ?? "";
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = ND.PageTitle ?? "";
                cmd.Parameters.AddWithValue("@MetaKeys", SqlDbType.NVarChar).Value = ND.MetaKeys ?? "";
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = ND.MetaDesc ?? "";
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = ND.AddedBy;
                cmd.Parameters.AddWithValue("@AddedIP", SqlDbType.NVarChar).Value = ND.AddedIP;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value = ND.AddedOn;
                cmd.Parameters.AddWithValue("@Featured", SqlDbType.Bit).Value = ND.Featured;

                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertNewsDetails", ex.Message);
        }

        return result;
    }
    #endregion

    #region Update News
    public static int UpdateNewsDetails(SqlConnection con, NewsDetails ND)
    {
        int result = 0;

        try
        {
            string query = @"UPDATE NewsDetails SET
                 NewsTitle=@NewsTitle,
                 NewsUrl=@NewsUrl,
                 Author=@Author,
                 PostedOn=@PostedOn,
                 ShortDesc=@ShortDesc,
                 FullDesc=@FullDesc,
                 Tags=@Tags,
                 ThumbImage=@ThumbImage,
                 PageTitle=@PageTitle,
                 MetaKeys=@MetaKeys,
                 MetaDesc=@MetaDesc,
                 Featured=@Featured
                 WHERE Id=@Id";


            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = ND.Id;
                cmd.Parameters.AddWithValue("@NewsTitle", SqlDbType.NVarChar).Value = ND.NewsTitle;
                cmd.Parameters.AddWithValue("@NewsUrl", SqlDbType.NVarChar).Value = ND.NewsUrl;
                cmd.Parameters.AddWithValue("@Author", SqlDbType.NVarChar).Value = ND.Author;
                cmd.Parameters.AddWithValue("@PostedOn", SqlDbType.NVarChar).Value = ND.PostedOn;
                cmd.Parameters.AddWithValue("@ShortDesc", SqlDbType.NVarChar).Value = ND.ShortDesc;
                cmd.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = ND.FullDesc;
                cmd.Parameters.AddWithValue("@Tags", SqlDbType.NVarChar).Value = ND.Tags ?? "";
                cmd.Parameters.AddWithValue("@Featured", SqlDbType.Bit).Value = ND.Featured;
                cmd.Parameters.AddWithValue("@ThumbImage", SqlDbType.NVarChar).Value = ND.ThumbImage ?? "";
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = ND.PageTitle ?? "";
                cmd.Parameters.AddWithValue("@MetaKeys", SqlDbType.NVarChar).Value = ND.MetaKeys ?? "";
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = ND.MetaDesc ?? "";

                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateNewsDetails", ex.Message);
        }

        return result;
    }

    #endregion

    #region Get All News
    public static List<NewsDetails> GetAllNewsDetails(SqlConnection con)
    {
        List<NewsDetails> lst = new List<NewsDetails>();
        try
        {
            string query = "SELECT * FROM NewsDetails WHERE Status='Active' ORDER BY AddedOn DESC";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        NewsDetails ND = new NewsDetails
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            NewsTitle = dr["NewsTitle"].ToString(),
                            NewsUrl = dr["NewsUrl"].ToString(),
                            Author = dr["Author"].ToString(),
                            PostedOn = dr["PostedOn"].ToString(),
                            ShortDesc = dr["ShortDesc"].ToString(),
                            FullDesc = dr["FullDesc"].ToString(),
                            Tags = dr["Tags"].ToString(),
                            ThumbImage = dr["ThumbImage"].ToString(),
                            PageTitle = dr["PageTitle"].ToString(),
                            MetaKeys = dr["MetaKeys"].ToString(),
                            MetaDesc = dr["MetaDesc"].ToString(),
                            AddedBy = dr["AddedBy"].ToString(),
                            AddedIP = dr["AddedIP"].ToString(),
                            AddedOn = Convert.ToDateTime(dr["AddedOn"])
                        };

                        lst.Add(ND);
                    }
                }
                con.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllNewsDetails", ex.Message);
        }
        return lst;
    }
    #endregion

    #region Delete News (Soft Delete)
    public static int DeleteNewsById(SqlConnection con, NewsDetails news)
    {
        int result = 0;
        try
        {
            string query = "UPDATE NewsDetails SET Status=@Status, AddedOn=@AddedOn, AddedIP=@AddedIP WHERE Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = news.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Deleted";
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value = news.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIP", SqlDbType.NVarChar).Value = news.AddedIP;

                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(System.Web.HttpContext.Current.Request.Url.PathAndQuery, "DeleteNewsById", ex.Message);
        }
        return result;
    }
    #endregion

    #region Get All News With Pagination

    public static List<NewsDetails> GetAllNewsDetailsWithPagination(SqlConnection con, int pNo, int pSize)
    {
        List<NewsDetails> lst = new List<NewsDetails>();

        try
        {
            string query = @"
        SELECT *,
               COUNT(*) OVER() AS TotalCount
        FROM NewsDetails
        WHERE Status='Active'
        ORDER BY AddedOn DESC
        OFFSET (@PageNo - 1) * @PageSize ROWS
        FETCH NEXT @PageSize ROWS ONLY";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@PageNo", pNo);
                cmd.Parameters.AddWithValue("@PageSize", pSize);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        NewsDetails ND = new NewsDetails();

                        ND.Id = Convert.ToInt32(dr["Id"]);
                        ND.NewsTitle = dr["NewsTitle"].ToString();
                        ND.NewsUrl = dr["NewsUrl"].ToString();
                        ND.Author = dr["Author"].ToString();
                        ND.PostedOn = dr["PostedOn"].ToString();
                        ND.ShortDesc = dr["ShortDesc"].ToString();
                        ND.FullDesc = dr["FullDesc"].ToString();
                        ND.Tags = dr["Tags"].ToString();
                        ND.ThumbImage = dr["ThumbImage"].ToString();
                        ND.PageTitle = dr["PageTitle"].ToString();
                        ND.MetaKeys = dr["MetaKeys"].ToString();
                        ND.MetaDesc = dr["MetaDesc"].ToString();
                        ND.AddedBy = dr["AddedBy"].ToString();
                        ND.AddedIP = dr["AddedIP"].ToString();
                        ND.AddedOn = Convert.ToDateTime(dr["AddedOn"]);
                        ND.TotalCount = Convert.ToInt32(dr["TotalCount"]);

                        lst.Add(ND);
                    }
                }

                con.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "GetAllNewsDetailsWithPagination", ex.Message);
        }

        return lst;
    }

    #endregion


}
