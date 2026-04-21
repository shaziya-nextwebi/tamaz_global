using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

public class Gallery
{
    public int Id { get; set; }
    public string MediaType { get; set; }   // Image / Video
    public string MediaUrl { get; set; }
    public int DisplayOrder { get; set; }
    public string Status { get; set; }

    public DateTime AddedOn { get; set; }
    public string AddedIp { get; set; }
    public string AddedBy { get; set; }

    public DateTime UpdatedOn { get; set; }
    public string UpdatedIp { get; set; }
    public string UpdatedBy { get; set; }


    #region Admin Methods

    public static int InsertGallery(SqlConnection con, Gallery gal)
    {
        int result = 0;
        try
        {
            string query = @"INSERT INTO Gallery
(MediaType,MediaUrl,DisplayOrder,Status,
 AddedOn,AddedIp,AddedBy,
 UpdatedOn,UpdatedIp,UpdatedBy)
 VALUES
(@MediaType,@MediaUrl,@DisplayOrder,@Status,
 @AddedOn,@AddedIp,@AddedBy,
 @UpdatedOn,@UpdatedIp,@UpdatedBy)";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@MediaType", gal.MediaType);
                cmd.Parameters.AddWithValue("@MediaUrl", gal.MediaUrl);
                cmd.Parameters.AddWithValue("@Status", gal.Status);
                cmd.Parameters.AddWithValue("@AddedOn", gal.AddedOn);
                cmd.Parameters.AddWithValue("@AddedIp", gal.AddedIp);
                cmd.Parameters.AddWithValue("@AddedBy", gal.AddedBy);
                cmd.Parameters.AddWithValue("@UpdatedOn", gal.UpdatedOn);
                cmd.Parameters.AddWithValue("@DisplayOrder", gal.DisplayOrder);
                cmd.Parameters.AddWithValue("@UpdatedIp", gal.UpdatedIp);
                cmd.Parameters.AddWithValue("@UpdatedBy", gal.UpdatedBy);

                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertGallery", ex.Message);
        }
        return result;
    }
    public static int DeleteGallery(SqlConnection con, int id)
    {
        int result = 0;
        try
        {
            string query = @"UPDATE Gallery
                         SET Status='Deleted',
                             UpdatedOn=@UpdatedOn
                         WHERE Id=@Id";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@UpdatedOn", CommonModel.UTCTime());

                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteGallery", ex.Message);
        }

        return result;
    }
    public static DataTable GetGalleryByType(SqlConnection con, string type)
    {
        DataTable dt = new DataTable();
        try
        {
            string query = @"SELECT * FROM Gallery
                         WHERE MediaType=@MediaType
                         AND Status!='Deleted'
                         ORDER BY DisplayOrder ASC, Id DESC";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@MediaType", type);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
        }
        catch (Exception ex)
        {
            CommonModel.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetGalleryByType", ex.Message);
        }

        return dt;
    }
    public static void SwapOrder(SqlConnection con, int id1, int id2)
    {
        string query = @"
    DECLARE @Order1 INT, @Order2 INT

    SELECT @Order1 = DisplayOrder FROM Gallery WHERE Id=@Id1
    SELECT @Order2 = DisplayOrder FROM Gallery WHERE Id=@Id2

    UPDATE Gallery SET DisplayOrder=@Order2 WHERE Id=@Id1
    UPDATE Gallery SET DisplayOrder=@Order1 WHERE Id=@Id2";

        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue("@Id1", id1);
            cmd.Parameters.AddWithValue("@Id2", id2);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    #endregion
}

