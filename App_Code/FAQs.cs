using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProductPrices
/// </summary>
public class FAQs
{
    public FAQs()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int Id { get; set; }
    public string Pid { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
    public string AddedBy { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIp { get; set; }
    public string Status { get; set; }


    #region Admin category region

    /// <summary>
    /// Get all  category from db 
    /// </summary>
    /// <param name="conT">DB connection</param>
    /// <returns>All list</returns>
    public static List<FAQs> GetAllProductFAQ(SqlConnection conT)
    {
        List<FAQs> categories = new List<FAQs>();
        try
        {
            string query = "Select *, (Select Top 1 UserName From CreateUser Where UserGuid=ProductFAQs.AddedBy) as AddedBy1 from ProductFAQs where Status='Active' Order by Id";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                categories = (from DataRow dr in dt.Rows
                              select new FAQs()
                              {
                                  Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                  Pid = Convert.ToString(dr["Pid"]),
                                  Question = Convert.ToString(dr["Question"]),
                                  Answer = Convert.ToString(dr["Answer"]),
                                  AddedBy = Convert.ToString(dr["AddedBy1"]),
                                  AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                  AddedIp = Convert.ToString(dr["AddedIp"]),
                                  Status = Convert.ToString(dr["Status"])
                              }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductFAQ", ex.Message);
        }
        return categories;
    }



    /// <summary>
    /// Insert an category to database
    /// </summary>
    /// <param name="conT">DB Connection</param>
    /// <param name="cat">Category properties</param>
    /// <returns>No of rows excuted</returns>
    public static int InsertProductFAQs(SqlConnection conT, FAQs cat)
    {
        int result = 0;

        try
        {
            string query = "Insert Into ProductFAQs (Pid,Question,Answer,AddedBy,AddedOn,AddedIp,Status) values(@Pid,@Question,@Answer,@AddedBy,@AddedOn,@AddedIp,@Status)";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Pid", SqlDbType.NVarChar).Value = cat.Pid;
                cmd.Parameters.AddWithValue("@Question", SqlDbType.NVarChar).Value = cat.Question;
                cmd.Parameters.AddWithValue("@Answer", SqlDbType.NVarChar).Value = cat.Answer;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertProductFAQs", ex.Message);
        }
        return result;
    }

    /// <summary>
    /// Update an specific category
    /// </summary>
    /// <param name="conT">DB Connection</param>
    /// <param name="cat">Category properties</param>
    /// <returns>No of rows executed</returns>
    public static int UpdateProductFaq(SqlConnection conT, FAQs cat)
    {
        int result = 0;

        try
        {
            string query = "Update ProductFAQs Set Question=@Question,Answer=@Answer,AddedBy=@AddedBy,AddedOn=@AddedOn,AddedIp=@AddedIp,Status=@Status Where Id=@Id and Pid=@Pid";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Pid", SqlDbType.NVarChar).Value = cat.Pid;
                cmd.Parameters.AddWithValue("@Question", SqlDbType.NVarChar).Value = cat.Question;
                cmd.Parameters.AddWithValue("@Answer", SqlDbType.NVarChar).Value = cat.Answer;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateProductFaq", ex.Message);
        }
        return result;
    }

    /// <summary>
    /// Delete a specific category (Update status as delete)
    /// </summary>
    /// <param name="conT">DB Connection</param>
    /// <param name="cat">Category properties</param>
    /// <returns>No of rows executed</returns>
    public static int DeleteProductFaq(SqlConnection conT, FAQs cat)
    {
        int result = 0;

        try
        {
            string query = "Update ProductFAQs Set Status=@Status, AddedOn=@AddedOn, AddedIp=@AddedIp,AddedBy=@AddedBy Where Id=@Id ";
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
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteProductFaq", ex.Message);
        }
        return result;
    }
    #endregion
}