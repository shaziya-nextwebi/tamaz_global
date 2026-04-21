using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Contact
/// </summary>
public class ProductEnquiry
{
    public ProductEnquiry()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Quantity { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string SourcePage { get; set; }
    public string TypeofEnquiry { get; set; }
    public string Message { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIp { get; set; }
    public string Status { get; set; }


    #region Contact region

    /// <summary>
    /// Get all Contact from db 
    /// </summary>
    /// <param name="conT">DB connection</param>
    /// <returns>All list</returns>
    public static List<ProductEnquiry> GetAllContact(SqlConnection conT)
    {
        List<ProductEnquiry> zips = new List<ProductEnquiry>();
        try
        {
            string query = "Select * from ProductEnquiry where Status!='Deleted' Order by Id Desc ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                zips = (from DataRow dr in dt.Rows
                        select new ProductEnquiry()
                        {
                            Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            SourcePage = Convert.ToString(dr["SourcePage"]),
                            Name = Convert.ToString(dr["Name"]),
                            Email = Convert.ToString(dr["Email"]),
                            Mobile = Convert.ToString(dr["Mobile"]),
                            TypeofEnquiry = Convert.ToString(dr["TypeofEnquiry"]),
                            City = Convert.ToString(dr["City"]),
                            Message = Convert.ToString(dr["Message"]),
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
    public static int InsertProductEnquiry(SqlConnection conT, ProductEnquiry con)
    {
        int result = 0;
        try
        {
            string query = "Insert Into ProductEnquiry (TypeofEnquiry,SourcePage,Email,ProductName,Name,Mobile,City,Message,AddedOn,AddedIp,Status) " +
                "values(@TypeofEnquiry,@SourcePage,@Email,@ProductName,@Name,@Mobile,@City,@Message,@AddedOn,@AddedIp,@Status)";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@ProductName", SqlDbType.NVarChar).Value = con.ProductName;
                cmd.Parameters.AddWithValue("@TypeofEnquiry", SqlDbType.NVarChar).Value = con.TypeofEnquiry;
                cmd.Parameters.AddWithValue("@Name", SqlDbType.NVarChar).Value = con.Name;
                cmd.Parameters.AddWithValue("@Mobile", SqlDbType.NVarChar).Value = con.Mobile;
                cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = con.Email;
                cmd.Parameters.AddWithValue("@SourcePage", SqlDbType.NVarChar).Value = con.SourcePage;
                cmd.Parameters.AddWithValue("@City", SqlDbType.NVarChar).Value = con.City;
                cmd.Parameters.AddWithValue("@Message", SqlDbType.NVarChar).Value = con.Message;
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
    public static int DeleteContact(SqlConnection conT, ProductEnquiry con)
    {
        int result = 0;
        try
        {
            string query = "Update ProductEnquiry Set Status=@Status, AddedOn=@AddedOn, AddedIp=@AddedIp Where Id=@Id ";
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


    public static int DeleteEnq(SqlConnection conT, string oGuid)
    {
        int x = 0;
        try
        {
            SqlCommand cmd = new SqlCommand("Update ProductEnquiry Set Status=@Status,AddedOn=@AddedOn Where Id=@Id", conT);
            cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = oGuid;
            cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Deleted";
            cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
            cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = TimeStamps.UTCTime();
            conT.Open();
            x = cmd.ExecuteNonQuery();
            conT.Close();
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteEnq", ex.Message);
        }
        return x;
    }
    #endregion
}



public class CartlistEnq
{
    public CartlistEnq()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public string City { get; set; }
    public string Message { get; set; }
    public string ProductName { get; set; }
    public string Quantity { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIp { get; set; }
    public string Status { get; set; }

    #region Enquiry region


    public static List<CartlistEnq> GetAllcartEnquiry(SqlConnection conT)
    {
        List<CartlistEnq> zips = new List<CartlistEnq>();
        try
        {
            string query = "Select * from CartProductEnquiry where Status !='Deleted' Order by Id Desc ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                zips = (from DataRow dr in dt.Rows
                        select new CartlistEnq()
                        {
                            Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                            Name = Convert.ToString(dr["Name"]),
                            Email = Convert.ToString(dr["Email"]),
                            ProductName = Convert.ToString(dr["ProductName"]),
                            Mobile = Convert.ToString(dr["Mobile"]),
                            Message = Convert.ToString(dr["Message"]),
                            City = Convert.ToString(dr["City"]),
                            // Quantity = Convert.ToString(dr["Quantity"]),
                            AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                            AddedIp = Convert.ToString(dr["AddedIp"]),
                            Status = Convert.ToString(dr["Status"])
                        }).ToList();

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllcartEnquiry", ex.Message);
        }
        return zips;
    }

    public static int InsertcartlstEnquiry(SqlConnection conT, CartlistEnq con)
    {
        int result = 0;
        try
        {
            string query = "Insert Into CartProductEnquiry(ProductName,Message,Name,Mobile,Email,City,AddedOn,AddedIp,Status)" +
                " values(@ProductName,@Message,@Name,@Mobile,@Email,@City,@AddedOn,@AddedIp,@Status) ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Name", SqlDbType.NVarChar).Value = con.Name;
                cmd.Parameters.AddWithValue("@ProductName", SqlDbType.NVarChar).Value = con.ProductName;
                cmd.Parameters.AddWithValue("@Message", SqlDbType.NVarChar).Value = con.Message;
                cmd.Parameters.AddWithValue("@City", SqlDbType.NVarChar).Value = con.City;
                cmd.Parameters.AddWithValue("@Mobile", SqlDbType.NVarChar).Value = con.Mobile;
                cmd.Parameters.AddWithValue("@Email", SqlDbType.NVarChar).Value = con.Email;
                // cmd.Parameters.AddWithValue("@Quantity", SqlDbType.NVarChar).Value = con.Quantity;
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertcartlstEnquiry", ex.Message);
        }
        return result;
    }




    public static int DeleteEnq(SqlConnection conT, CartlistEnq con)
    {
        int result = 0;
        try
        {
            string query = "Update CartProductEnquiry Set Status=@Status,AddedOn=@AddedOn Where Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = con.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Deleted";
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = con.AddedOn;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteEnq", ex.Message);
        }
        return result;
    }
    #endregion
}