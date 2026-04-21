using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

public class BannerImages
{
    public int Id { get; set; }
    public string BannerTitle { get; set; }
    public string Link { get; set; }
    public string DesktopImage { get; set; }
    public string MobImage { get; set; }
    public string DisplayOrder { get; set; }
    public string Status { get; set; }
    public string AddedIp { get; set; }
    public DateTime AddedOn { get; set; }
    public string UpdatedIp { get; set; }
    public DateTime UpdatedOn { get; set; }

    #region Get All Banner Images

    public static List<BannerImages> GetBannerImage(SqlConnection con)
    {
        List<BannerImages> lst = new List<BannerImages>();
        try
        {
            string query = "SELECT * FROM BannerImages WHERE Status != 'Deleted' ORDER BY CAST(DisplayOrder AS INT) ASC";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        BannerImages bi = new BannerImages();

                        bi.Id = Convert.ToInt32(dr["Id"]);
                        bi.BannerTitle = dr["BannerTitle"].ToString();
                        bi.Link = dr["Link"].ToString();
                        bi.DesktopImage = dr["DesktopImage"].ToString();
                        bi.MobImage = dr["MobImage"].ToString();
                        bi.DisplayOrder = dr["DisplayOrder"].ToString();
                        bi.Status = dr["Status"].ToString();
                        bi.AddedIp = dr["AddedIp"].ToString();
                        bi.AddedOn = Convert.ToDateTime(dr["AddedOn"]);
                        bi.UpdatedIp = dr["UpdatedIp"].ToString();
                        bi.UpdatedOn = Convert.ToDateTime(dr["UpdatedOn"]);

                        lst.Add(bi);
                    }
                }
                con.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "GetBannerImage", ex.Message);
        }
        return lst;
    }

    #endregion

    #region Insert Banner Image

    public static int InsertBannerImage(SqlConnection con, BannerImages banner)
    {
        int result = 0;
        try
        {
            string query = @"INSERT INTO BannerImages
                                (BannerTitle, Link, DesktopImage, MobImage,
                                 DisplayOrder, Status, AddedIp, AddedOn, UpdatedIp, UpdatedOn)
                             VALUES
                                (@BannerTitle, @Link, @DesktopImage, @MobImage,
                                 @DisplayOrder, 'Active', @AddedIp, @AddedOn, @AddedIp, @AddedOn)";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@BannerTitle", SqlDbType.NVarChar).Value = banner.BannerTitle ?? "";
                cmd.Parameters.AddWithValue("@Link", SqlDbType.NVarChar).Value = banner.Link ?? "";
                cmd.Parameters.AddWithValue("@DesktopImage", SqlDbType.NVarChar).Value = banner.DesktopImage ?? "";
                cmd.Parameters.AddWithValue("@MobImage", SqlDbType.NVarChar).Value = banner.MobImage ?? "";
                cmd.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = banner.DisplayOrder ?? "1000";
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = banner.AddedIp ?? "";
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value = banner.AddedOn;

                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "InsertBannerImage", ex.Message);
        }
        return result;
    }

    #endregion

    #region Update Banner Image

    public static int UpdateBannerImage(SqlConnection con, BannerImages banner)
    {
        int result = 0;
        try
        {
            string query = @"UPDATE BannerImages
                             SET BannerTitle  = @BannerTitle,
                                 Link         = @Link,
                                 DesktopImage = @DesktopImage,
                                 MobImage     = @MobImage,
                                 DisplayOrder = @DisplayOrder,
                                 UpdatedOn    = @UpdatedOn,
                                 UpdatedIp    = @UpdatedIp
                             WHERE Id = @Id";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = banner.Id;
                cmd.Parameters.AddWithValue("@BannerTitle", SqlDbType.NVarChar).Value = banner.BannerTitle ?? "";
                cmd.Parameters.AddWithValue("@Link", SqlDbType.NVarChar).Value = banner.Link ?? "";
                cmd.Parameters.AddWithValue("@DesktopImage", SqlDbType.NVarChar).Value = banner.DesktopImage ?? "";
                cmd.Parameters.AddWithValue("@MobImage", SqlDbType.NVarChar).Value = banner.MobImage ?? "";
                cmd.Parameters.AddWithValue("@DisplayOrder", SqlDbType.NVarChar).Value = banner.DisplayOrder ?? "1000";
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.DateTime).Value = banner.UpdatedOn;
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = banner.UpdatedIp ?? "";

                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "UpdateBannerImage", ex.Message);
        }
        return result;
    }

    #endregion

    #region Delete Banner Image (Soft Delete)

    public static int DeleteBannerImage(SqlConnection con, BannerImages banner)
    {
        int result = 0;
        try
        {
            string query = @"UPDATE BannerImages
                             SET Status    = 'Deleted',
                                 UpdatedOn = @UpdatedOn,
                                 UpdatedIp = @UpdatedIp
                             WHERE Id = @Id";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = banner.Id;
                cmd.Parameters.AddWithValue("@UpdatedOn", SqlDbType.DateTime).Value = banner.UpdatedOn;
                cmd.Parameters.AddWithValue("@UpdatedIp", SqlDbType.NVarChar).Value = banner.UpdatedIp ?? "";

                con.Open();
                result = cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "DeleteBannerImage", ex.Message);
        }
        return result;
    }

    #endregion
}