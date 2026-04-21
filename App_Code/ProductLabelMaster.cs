using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProductLabelMaster
/// </summary>
public class ProductLabelMaster
{
    public ProductLabelMaster()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int Id { get; set; }
    public string ProductLabel { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIp { get; set; }
    public string Status { get; set; }
    public string AddedBy { get; set; }

    #region Admin Product Lable region

    public static List<ProductLabelMaster> GetAllProductlabels(SqlConnection conT)
    {
        List<ProductLabelMaster> pl = new List<ProductLabelMaster>();
        try
        {
            string query = "Select *,(Select Top 1 UserName From CreateUser Where UserGuid=ProductLabelMaster.AddedBy) as AddedBy1 from ProductLabelMaster where Status='Active' Order by Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                pl = (from DataRow dr in dt.Rows
                      select new ProductLabelMaster()
                      {
                          Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                          ProductLabel = Convert.ToString(dr["ProductLabel"]),
                          AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                          AddedIp = Convert.ToString(dr["AddedIp"]),
                          Status = Convert.ToString(dr["Status"])
                      }).ToList();

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductlabels", ex.Message);
        }
        return pl;
    }

    public static int InsertProductLabel(SqlConnection conT, ProductLabelMaster cat)
    {
        int result = 0;

        try
        {
            string query = "Insert Into ProductLabelMaster (ProductLabel,AddedOn, AddedIp,Status,AddedBy) values(@ProductLabel,@AddedOn, @AddedIp,@Status,@AddedBy) ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@ProductLabel", SqlDbType.NVarChar).Value = cat.ProductLabel;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertProductLabel", ex.Message);
        }
        return result;
    }

    public static int UpdateProductLabel(SqlConnection conT, ProductLabelMaster cat)
    {
        int result = 0;

        try
        {
            string query = "Update ProductLabelMaster Set  ProductLabel=@ProductLabel,AddedOn=@AddedOn, AddedIp=@AddedIp,AddedBy=@AddedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@ProductLabel", SqlDbType.NVarChar).Value = cat.ProductLabel;
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateProductLabel", ex.Message);
        }
        return result;
    }

    public static int DeleteProductLabel(SqlConnection conT, ProductLabelMaster cat)
    {
        int result = 0;

        try
        {
            string query = "Update ProductLabelMaster Set Status=@Status, AddedOn=@AddedOn, AddedIp=@AddedIp,AddedBy=@AddedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Deleted";
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteProductLabel", ex.Message);
        }
        return result;
    }

    public static List<ProductLabelMaster> GetAllDProductlabel(SqlConnection conT)
    {
        List<ProductLabelMaster> pl = new List<ProductLabelMaster>();
        try
        {
            string query = "Select *, (Select Top 1 UserName From CreateUser Where UserGuid=ProductLabelMaster.AddedBy) as AddedBy1 from ProductLabelMaster where Status='Active' Order by ProductLabel ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                pl = (from DataRow dr in dt.Rows
                      select new ProductLabelMaster()
                      {
                          Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                          ProductLabel = Convert.ToString(dr["ProductLabel"]),
                          AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                          AddedIp = Convert.ToString(dr["AddedIp"]),
                          Status = Convert.ToString(dr["Status"])
                      }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllDProductlabel", ex.Message);
        }
        return pl;
    }

    public static ProductLabelMaster GetProductLabelById(SqlConnection conT, int id)
    {
        ProductLabelMaster label = null;
        try
        {
            string query = "SELECT * FROM ProductLabelMaster WHERE Id = @Id AND Status = 'Active'";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.Int).Value = id;
                conT.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        label = new ProductLabelMaster
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            ProductLabel = dr["ProductLabel"].ToString(),
                            AddedOn = Convert.ToDateTime(dr["AddedOn"]),
                            AddedIp = dr["AddedIp"].ToString(),
                            Status = dr["Status"].ToString(),
                            AddedBy = dr["AddedBy"].ToString()
                        };
                    }
                }
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery,
                "GetProductLabelById", ex.Message);
        }
        return label;
    }



    #endregion
}