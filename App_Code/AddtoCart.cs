using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class AddtoCart
{
    public int Id { get; set; }
    public string Userguid { get; set; }
    public string ProductName { get; set; }
    public string Status { get; set; }

    public string ProductUrl { get; set; }
    public string Category { get; set; }
    public string SmallImage { get; set; }
    public bool Uguid { get; set; }
    public int ProductId { get; set; }
    public int Qty { get; set; }
    public DateTime Addedon { get; set; }
    public string AddedIp { get; set; }

    public decimal RetailPrice { get; set; }
    //public decimal FinalPrice { get; set; }
    //public decimal SubTotal { get; set; }


    #region Admin AddtoCart region
    public static string GetcartlistQunatity(SqlConnection con)
    {
        string qty = "";
        try
        {
            string uid = HttpContext.Current.Request.Cookies["t_new_vi"] != null ? HttpContext.Current.Request.Cookies["t_new_vi"].Value : "";
            if (uid != "")
            {
                uid = CommonModel.Decrypt(uid);
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("select  count(UserGuid) cnt from AddCartDetails where Userguid=@uid", con);
                cmd.Parameters.AddWithValue("@uid", SqlDbType.NVarChar).Value = uid;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    qty = Convert.ToString(dt.Rows[0]["cnt"]) == "0" ? "" : Convert.ToString(dt.Rows[0]["cnt"]);
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetcartlistQunatity", ex.Message);
        }
        return qty;
    }
    public static List<AddtoCart> GetUserArpcartByUid(SqlConnection conT, string uid, string pid)
    {
        List<AddtoCart> categories = new List<AddtoCart>();
        try
        {

            string query = "Select * from AddCartDetails where Userguid=@uid and ProductId=@pid";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@uid", SqlDbType.NVarChar).Value = uid;
                cmd.Parameters.AddWithValue("@pid", SqlDbType.NVarChar).Value = pid;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new AddtoCart()
                              {
                                  ProductId = Convert.ToInt32(dr["ProductId"]),
                                  Qty = Convert.ToInt32(Convert.ToString(dr["Qty"])),
                                  Userguid = Convert.ToString(dr["Userguid"]),
                                  Addedon = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                  AddedIp = Convert.ToString(dr["AddedIp"]),
                              }).ToList();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetUserArpcartByUid", ex.Message);
        }
        return categories;
    }
    public static AddtoCart GetUserArpWishlistByUidPid(SqlConnection conT, string pid)
    {
        AddtoCart categories = null;
        try
        {
            string uid = HttpContext.Current.Request.Cookies["t_new_vi"] != null ? HttpContext.Current.Request.Cookies["t_new_vi"].Value : "";
            if (uid != "")
            {
                uid = CommonModel.Decrypt(uid);
                string query = "Select * from AddCartDetails where Userguid=@uid and pid=@pid";
                using (SqlCommand cmd = new SqlCommand(query, conT))
                {
                    cmd.Parameters.AddWithValue("@uid", SqlDbType.NVarChar).Value = uid;
                    cmd.Parameters.AddWithValue("@pid", SqlDbType.NVarChar).Value = pid;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        categories = new AddtoCart();
                        categories.Qty = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Qty"]));
                        categories.Userguid = Convert.ToString(dt.Rows[0]["Userguid"]);
                        categories.Addedon = Convert.ToDateTime(Convert.ToString(dt.Rows[0]["AddedOn"]));
                        categories.AddedIp = Convert.ToString(dt.Rows[0]["AddedIp"]);
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetUserArpWishlists", ex.Message);
        }
        return categories;
    }
    public static List<AddtoCart> GetwishDetails(SqlConnection conT)
    {
        List<AddtoCart> cart = new List<AddtoCart>();
        try
        {
            string uid = HttpContext.Current.Request.Cookies["t_new_vi"] != null ? HttpContext.Current.Request.Cookies["t_new_vi"].Value : "";
            if (uid != "")
            {
                uid = CommonModel.Decrypt(uid);
            }
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("Select * from AddCartDetails where Userguid=@Userguid", conT);
            cmd.Parameters.AddWithValue("@Userguid", SqlDbType.NVarChar).Value = uid;
            // cmd.Parameters.AddWithValue("@ProductId", SqlDbType.NVarChar).Value = pid;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                AddtoCart product = new AddtoCart();
                product.Userguid = Convert.ToString(dr["Userguid"]);
                product.ProductId = Convert.ToInt32(dr["ProductId"]);
                product.Qty = Convert.ToInt32(Convert.ToString(dr["Qty"]));
                product.Addedon = Convert.ToDateTime(Convert.ToString(dr["AddedOn"]));
                product.AddedIp = Convert.ToString(dr["AddedIp"]);
                cart.Add(product);
            }
            // }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetwishDetails", ex.Message);
        }
        return cart;
    }
    public static int Insertcartdetails(SqlConnection conT, AddtoCart cat)
    {
        int result = 0;
        try
        {
            string query = "Insert Into AddCartDetails (ProductId,Qty,Userguid,AddedOn,AddedIp) values (@ProductId,@Qty,@Userguid,@AddedOn,@AddedIp)";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@ProductId", SqlDbType.NVarChar).Value = cat.ProductId;
                cmd.Parameters.AddWithValue("@Qty", SqlDbType.NVarChar).Value = cat.Qty;
                cmd.Parameters.AddWithValue("@Userguid", SqlDbType.NVarChar).Value = cat.Userguid;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = TimeStamps.UTCTime();
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Insertcartdetails", ex.Message);
        }
        return result;
    }


    public static int Updatecartdetails(SqlConnection conT, AddtoCart Wish)
    {
        int x = 0;
        try
        {
            SqlCommand cmd = new SqlCommand("Update AddCartDetails Set Qty=@Qty,AddedOn=@AddedOn,AddedIp=@AddedIp where Userguid=@Userguid and ProductId=@ProductId", conT);
            cmd.Parameters.AddWithValue("@Qty", SqlDbType.NVarChar).Value = Wish.Qty;
            cmd.Parameters.AddWithValue("@Userguid", SqlDbType.NVarChar).Value = Wish.Userguid;
            cmd.Parameters.AddWithValue("@ProductId", SqlDbType.NVarChar).Value = Wish.ProductId;
            cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.DateTime).Value = TimeStamps.UTCTime();
            cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = CommonModel.IPAddress();
            conT.Open();
            x = cmd.ExecuteNonQuery();
            conT.Close();

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Updatecartdetails", ex.Message);
        }
        return x;
    }

    public static int Deletecartlist(SqlConnection conT, string pid)
    {
        int x = 0;
        try
        {
            SqlCommand cmd = new SqlCommand("Delete from AddCartDetails where ProductId=@id", conT);
            cmd.Parameters.AddWithValue("@id", SqlDbType.NVarChar).Value = pid;
            conT.Open();
            x = cmd.ExecuteNonQuery();
            conT.Close();
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Deletecartlist", ex.Message);
        }
        return x;
    }

    public static int Deletecartlistafterenq(SqlConnection conT)
    {
        int x = 0;
        try
        {
            string uid = HttpContext.Current.Request.Cookies["t_new_vi"] != null ? HttpContext.Current.Request.Cookies["t_new_vi"].Value : "";
            if (uid != "")
            {
                uid = CommonModel.Decrypt(uid);
                SqlCommand cmd = new SqlCommand("Delete from AddCartDetails where  Userguid=@Userguid", conT);
                cmd.Parameters.AddWithValue("@Userguid", SqlDbType.NVarChar).Value = uid;
                conT.Open();
                x = cmd.ExecuteNonQuery();
                conT.Close();
            }

        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "Deletecartlistafterenq", ex.Message);
        }
        return x;
    }

    public static List<AddtoCart> GetAllcartproducts(SqlConnection conT)
    {
        List<AddtoCart> products = new List<AddtoCart>();
        try
        {
            string uid = "";

            if (HttpContext.Current.Request.Cookies["t_new_vi"] != null)
            {
                uid = HttpContext.Current.Request.Cookies["t_new_vi"].Value;

                try
                {
                    uid = CommonModel.Decrypt(uid);
                }
                catch
                {
                    // ignore if not encrypted
                }
            }

            string query = @"
SELECT  
    v.Id,
    v.Category,
    v.ProductName,
    v.ProductUrl,
    v.SmallImage,
    v.RetailPrice,
    s.ProductId,
    s.Userguid,
    s.Qty
FROM ProductDetails v
INNER JOIN AddCartDetails s ON s.ProductId = v.Id
WHERE v.Status = 'Active' AND s.Userguid = @Userguid";

            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Userguid", uid);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                products = (from DataRow dr in dt.Rows
                            select new AddtoCart()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                ProductName = Convert.ToString(dr["ProductName"]),
                                ProductUrl = Convert.ToString(dr["ProductUrl"]),
                                SmallImage = Convert.ToString(dr["SmallImage"]),
                                Category = Convert.ToString(dr["Category"]),
                                ProductId = Convert.ToInt32(dr["ProductId"]),
                                Qty = Convert.ToInt32(dr["Qty"]),
                                RetailPrice = GetDecimal(dr["RetailPrice"])
                                //FinalPrice = Convert.ToDecimal(dr["final_price"]),
                                //SubTotal = Convert.ToDecimal(dr["final_price"]) * Convert.ToInt32(dr["Qty"])
                            }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllcartproducts", ex.Message);
        }
        return products;
    }
    private static decimal GetDecimal(object val)
    {
        if (val == null || val == DBNull.Value) return 0m;
        decimal result = 0m;
        decimal.TryParse(val.ToString().Trim(), out result);
        return result;
    }
    #endregion
}