using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class Reviews
{
    public int Id { get; set; }
    public string ReviewerName { get; set; }
    public int Rating { get; set; }
    public string ReviewDesc { get; set; }
    public string ImageUrl { get; set; }
    public string YouTubeLink { get; set; }
    public string Location { get; set; }
    public string TagName { get; set; }
    public DateTime PostedOn { get; set; }

    public string Status { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIp { get; set; }
    public string AddedBy { get; set; }
    public DateTime UpdatedOn { get; set; }
    public string UpdatedIp { get; set; }
    public string UpdatedBy { get; set; }

    #region Admin Methods

    public static int InsertReview(SqlConnection con, Reviews rev)
    {
        int result = 0;
        try
        {
            string query = @"INSERT INTO Reviews
            (ReviewerName,Rating,ReviewDesc,ImageUrl,YouTubeLink,Location,TagName,PostedOn,
             Status,AddedOn,AddedIp,AddedBy,UpdatedOn,UpdatedIp,UpdatedBy)
             VALUES
            (@ReviewerName,@Rating,@ReviewDesc,@ImageUrl,@YouTubeLink,@Location,@TagName,@PostedOn,
             @Status,@AddedOn,@AddedIp,@AddedBy,@UpdatedOn,@UpdatedIp,@UpdatedBy)";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@ReviewerName", rev.ReviewerName);
                cmd.Parameters.AddWithValue("@Rating", rev.Rating);
                cmd.Parameters.AddWithValue("@ReviewDesc", rev.ReviewDesc);
                cmd.Parameters.AddWithValue("@ImageUrl", rev.ImageUrl);
                cmd.Parameters.AddWithValue("@YouTubeLink", rev.YouTubeLink);
                cmd.Parameters.AddWithValue("@Location", rev.Location);
                cmd.Parameters.AddWithValue("@TagName", rev.TagName);
                cmd.Parameters.AddWithValue("@PostedOn", rev.PostedOn);
                cmd.Parameters.AddWithValue("@Status", rev.Status);
                cmd.Parameters.AddWithValue("@AddedOn", rev.AddedOn);
                cmd.Parameters.AddWithValue("@AddedIp", rev.AddedIp);
                cmd.Parameters.AddWithValue("@AddedBy", rev.AddedBy);
                cmd.Parameters.AddWithValue("@UpdatedOn", rev.UpdatedOn);
                cmd.Parameters.AddWithValue("@UpdatedIp", rev.UpdatedIp);
                cmd.Parameters.AddWithValue("@UpdatedBy", rev.UpdatedBy);

                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertReview", ex.Message);
        }
        return result;
    }

    public static int UpdateReview(SqlConnection con, Reviews rev)
    {
        int result = 0;
        try
        {
            string query = @"UPDATE Reviews SET
                ReviewerName=@ReviewerName,
                Rating=@Rating,
                ReviewDesc=@ReviewDesc,
                ImageUrl=@ImageUrl,
                YouTubeLink=@YouTubeLink,
                Location=@Location,
                TagName=@TagName,
                PostedOn=@PostedOn,
                UpdatedOn=@UpdatedOn,
                UpdatedIp=@UpdatedIp,
                UpdatedBy=@UpdatedBy
                WHERE Id=@Id";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Id", rev.Id);
                cmd.Parameters.AddWithValue("@ReviewerName", rev.ReviewerName);
                cmd.Parameters.AddWithValue("@Rating", rev.Rating);
                cmd.Parameters.AddWithValue("@ReviewDesc", rev.ReviewDesc);
                cmd.Parameters.AddWithValue("@ImageUrl", rev.ImageUrl);
                cmd.Parameters.AddWithValue("@YouTubeLink", rev.YouTubeLink);
                cmd.Parameters.AddWithValue("@Location", rev.Location);
                cmd.Parameters.AddWithValue("@TagName", rev.TagName);
                cmd.Parameters.AddWithValue("@PostedOn", rev.PostedOn);
                cmd.Parameters.AddWithValue("@UpdatedOn", CommonModel.UTCTime());
                cmd.Parameters.AddWithValue("@UpdatedIp", CommonModel.IPAddress());
                cmd.Parameters.AddWithValue("@UpdatedBy", rev.UpdatedBy);

                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateReview", ex.Message);
        }
        return result;
    }
    public static List<Reviews> GetAllReviews(SqlConnection con)
    {
        List<Reviews> list = new List<Reviews>();
        try
        {
            string query = "SELECT * FROM Reviews WHERE Status!='Deleted' ORDER BY Id DESC";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                list = (from DataRow dr in dt.Rows
                        select new Reviews
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            ReviewerName = dr["ReviewerName"].ToString(),
                            Rating = Convert.ToInt32(dr["Rating"]),
                            ReviewDesc = dr["ReviewDesc"].ToString(),
                            ImageUrl = dr["ImageUrl"].ToString(),
                            YouTubeLink = dr["YouTubeLink"].ToString(),
                            Location = dr["Location"].ToString(),
                            TagName = dr["TagName"].ToString(),
                            PostedOn = Convert.ToDateTime(dr["PostedOn"]),
                            Status = dr["Status"].ToString()
                        }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllReviews", ex.Message);
        }

        return list;
    }

    public static List<Reviews> GetReviewById(SqlConnection con, int id)
    {
        List<Reviews> list = new List<Reviews>();
        try
        {
            string query = "SELECT * FROM Reviews WHERE Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                list = (from DataRow dr in dt.Rows
                        select new Reviews
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            ReviewerName = dr["ReviewerName"].ToString(),
                            Rating = Convert.ToInt32(dr["Rating"]),
                            ReviewDesc = dr["ReviewDesc"].ToString(),
                            ImageUrl = dr["ImageUrl"].ToString(),
                            YouTubeLink = dr["YouTubeLink"].ToString(),
                            Location = dr["Location"].ToString(),
                            TagName = dr["TagName"].ToString(),
                            PostedOn = Convert.ToDateTime(dr["PostedOn"])
                        }).ToList();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetReviewById", ex.Message);
        }
        return list;
    }
    public static int DeleteReview(SqlConnection con, Reviews rev)
    {
        SqlCommand cmd = new SqlCommand(
            "UPDATE Reviews SET Status='Deleted', UpdatedOn=@UpdatedOn WHERE Id=@Id",
            con);

        cmd.Parameters.AddWithValue("@Id", rev.Id);
        cmd.Parameters.AddWithValue("@UpdatedOn", rev.UpdatedOn);

        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();

        return i;
    }

    #endregion
}
