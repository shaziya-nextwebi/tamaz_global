using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DashBoard
/// </summary>
public class DashBoard
{
    public DashBoard()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Get all  orders from db 
    /// </summary>
    /// <param name="conT">DB connection</param>
    /// <returns>All list</returns>
    //public static List<Orders> GetLast10Orders(SqlConnection conT)
    //{
    //    List<Orders> orders = new List<Orders>();
    //    try
    //    {
    //        string query = "Select top 10 o.*, " +
    //           " b.FirstName+' '+b.LastName as Name1, b.EmailId, b.Mobile as Mobile1,  b.Landline as Landline1, b.Address1 as Address11, b.Address2 as Address21, b.City as City1, b.Country as Country1, b.Zip as Zip1, b.VATNo as VATNo1, b.Company as Company1," +
    //           " d.FirstName+' '+d.LastName as Name2,   d.Mobile as Mobile2,  d.Landline as Landline2, d.Address1 as Address12, d.Address2 as Address22, d.City as City2, d.Country as Country2, d.Zip as Zip2, d.VATNo as VATNo2, d.Company as Company2 " +
    //           "  from Orders as o inner join UserBillingAddress as b on b.OrderGuid = o.OrderGuid inner join UserDeliveryAddress as d on d.OrderGuid = o.OrderGuid Where (o.Status != 'Deleted'  or o.Status is  null) order by o.id desc";

    //        using (SqlCommand cmd = new SqlCommand(query, conT))
    //        {
    //            SqlDataAdapter sda = new SqlDataAdapter(cmd);
    //            DataTable dt = new DataTable();
    //            sda.Fill(dt);
    //            orders = (from DataRow dr in dt.Rows
    //                      select new Orders()
    //                      {
    //                          Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
    //                          CODCharges = Convert.ToString(dr["CODCharges"]),
    //                          Discount = Convert.ToString(dr["Discount"]),
    //                          OrderOn = Convert.ToDateTime(Convert.ToString(dr["OrderOn"])),
    //                          LastUpdatedOn = Convert.ToString(dr["LastUpdatedOn"]) == "" ? Convert.ToDateTime(Convert.ToString(dr["OrderOn"])) : Convert.ToDateTime(Convert.ToString(dr["LastUpdatedOn"])),
    //                          OrderedIP = Convert.ToString(dr["OrderedIP"]),
    //                          OrderGuid = Convert.ToString(dr["OrderGuid"]),
    //                          OrderId = Convert.ToString(dr["OrderId"]),
    //                          OrderMax = Convert.ToString(dr["OrderMax"]),
    //                          OrderStatus = Convert.ToString(dr["OrderStatus"]),
    //                          OtherInfo = Convert.ToString(dr["OtherInfo"]),
    //                          PaymentId = Convert.ToString(dr["PaymentId"]),
    //                          PaymentMode = Convert.ToString(dr["PaymentMode"]),
    //                          PaymentStatus = Convert.ToString(dr["PaymentStatus"]),
    //                          PromoCode = Convert.ToString(dr["PromoCode"]),
    //                          ReceiptNo = Convert.ToString(dr["ReceiptNo"]),
    //                          GreetingText = Convert.ToString(dr["GreetingText"]),
    //                          EstDelDate = Convert.ToString(dr["EstDelDate"]) == "" ? TimeStamps.UTCTime() :  Convert.ToDateTime(Convert.ToString(dr["EstDelDate"])),
    //                          RMax = Convert.ToString(dr["RMax"]),
    //                          Shipping = Convert.ToString(dr["Shipping"]),
    //                          ShippingType = Convert.ToString(dr["ShippingType"]),
    //                          SubTotal = Convert.ToString(dr["SubTotal"]),
    //                          SubTotalWithoutTax = Convert.ToString(dr["SubTotalWithoutTax"]),
    //                          Tax = Convert.ToString(dr["Tax"]),
    //                          TotalPrice = Convert.ToString(dr["TotalPrice"]),
    //                          UserGuid = Convert.ToString(dr["UserGuid"]),
    //                          UserType = Convert.ToString(dr["UserType"]),
    //                          UserName = Convert.ToString(dr["Name1"]),
    //                          EmailId = Convert.ToString(dr["EmailId"]),
    //                          Contact = Convert.ToString(dr["Mobile1"]),
    //                          LandLine = Convert.ToString(dr["Landline1"]),
    //                          BillingAddress = Convert.ToString(dr["Address11"]) + "| " + Convert.ToString(dr["Address21"]) + "| " + Convert.ToString(dr["City1"]) + "| " + Convert.ToString(dr["Country1"]) + " - " + Convert.ToString(dr["Zip1"]) + "| " + Convert.ToString(dr["VATNo1"]) + "| " + Convert.ToString(dr["Company1"]),
    //                          DeliveryAddress = Convert.ToString(dr["Name2"]) + "| " + Convert.ToString(dr["Mobile2"]) + "| " + Convert.ToString(dr["Landline2"]) + "| " + Convert.ToString(dr["Address12"]) + "| " + Convert.ToString(dr["Address22"]) + "| " + Convert.ToString(dr["City2"]) + "| " + Convert.ToString(dr["Country2"]) + " - " + Convert.ToString(dr["Zip2"]) + "| " + Convert.ToString(dr["VATNo1"]) + "| " + Convert.ToString(dr["Company1"]),
    //                      }).ToList();

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetLast10Orders", ex.Message);
    //    }
    //    return orders;
    //}

    //public static List<UserDeliveryAddress> GetDeliveryAddress(SqlConnection conT, string oGuid)
    //{
    //    List<UserDeliveryAddress> address = new List<UserDeliveryAddress>();
    //    try
    //    {
    //        string query = " Select * from UserBillingAddress Where OrderGuid = @OrderGuid";

    //        using (SqlCommand cmd = new SqlCommand(query, conT))
    //        {
    //            cmd.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = oGuid;
    //            SqlDataAdapter sda = new SqlDataAdapter(cmd);
    //            DataTable dt = new DataTable();
    //            sda.Fill(dt);
    //            address = (from DataRow dr in dt.Rows
    //                       select new UserDeliveryAddress()
    //                       {
    //                           Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
    //                           AddedDateTime = Convert.ToDateTime(Convert.ToString(dr["AddedDateTime"])),
    //                           AddlInfo = Convert.ToString(dr["AddlInfo"]),
    //                           Address1 = Convert.ToString(dr["Address1"]),
    //                           Address2 = Convert.ToString(dr["Address2"]),
    //                           Company = Convert.ToString(dr["Company"]),
    //                           City = Convert.ToString(dr["City"]),
    //                           Country = Convert.ToString(dr["Country"]),
    //                           FirstName = Convert.ToString(dr["FirstName"]),
    //                           Landline = Convert.ToString(dr["Landline"]),
    //                           LastName = Convert.ToString(dr["LastName"]),
    //                           Mobile = Convert.ToString(dr["Mobile"]),
    //                           OrderGuid = Convert.ToString(dr["OrderGuid"]),
    //                           VATNo = Convert.ToString(dr["VATNo"]),
    //                           Zip = Convert.ToString(dr["Zip"]),
    //                       }).ToList();

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetDeliveryAddress", ex.Message);
    //    }
    //    return address;
    //}

    //public static List<UserBillingAddress> GetBillingAddress(SqlConnection conT, string oGuid)
    //{
    //    List<UserBillingAddress> address = new List<UserBillingAddress>();
    //    try
    //    {
    //        string query = " Select * from UserBillingAddress Where OrderGuid = @OrderGuid";

    //        using (SqlCommand cmd = new SqlCommand(query, conT))
    //        {
    //            cmd.Parameters.AddWithValue("@OrderGuid", SqlDbType.NVarChar).Value = oGuid;
    //            SqlDataAdapter sda = new SqlDataAdapter(cmd);
    //            DataTable dt = new DataTable();
    //            sda.Fill(dt);
    //            address = (from DataRow dr in dt.Rows
    //                       select new UserBillingAddress()
    //                       {
    //                           Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
    //                           AddedDateTime = Convert.ToDateTime(Convert.ToString(dr["AddedDateTime"])),
    //                           AddlInfo = Convert.ToString(dr["AddlInfo"]),
    //                           Address1 = Convert.ToString(dr["Address1"]),
    //                           Address2 = Convert.ToString(dr["Address2"]),
    //                           Company = Convert.ToString(dr["Company"]),
    //                           City = Convert.ToString(dr["City"]),
    //                           Country = Convert.ToString(dr["Country"]),
    //                           EmailId = Convert.ToString(dr["EmailId"]),
    //                           FirstName = Convert.ToString(dr["FirstName"]),
    //                           Landline = Convert.ToString(dr["Landline"]),
    //                           LastName = Convert.ToString(dr["LastName"]),
    //                           Mobile = Convert.ToString(dr["Mobile"]),
    //                           OrderGuid = Convert.ToString(dr["OrderGuid"]),
    //                           OtherInfo = Convert.ToString(dr["OtherInfo"]),
    //                           VATNo = Convert.ToString(dr["VATNo"]),
    //                           Zip = Convert.ToString(dr["Zip"]),

    //                       }).ToList();

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetBillingAddress", ex.Message);
    //    }
    //    return address;
    //}

    public static int GetProductCount(SqlConnection conT)
    {
        int x = 0;
        try
        {
            string query = " Select * from ProductDetails Where  Status= 'Active'";
            SqlCommand cmd = new SqlCommand(query, conT);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            x = dt.Rows.Count;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetOrderCount", ex.Message);
        }
        return x;
    }


    public static int GetCategoryCount(SqlConnection conT)
    {
        int x = 0;
        try
        {
            string query = "Select * from Category Where  Status= 'Active'";
            SqlCommand cmd = new SqlCommand(query, conT);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            x = dt.Rows.Count;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetCategoryCount", ex.Message);
        }
        return x;
    }
    public static int GetBrnadCount(SqlConnection conT)
    {
        int x = 0;
        try
        {
            string query = "Select * from Brand Where  Status='Active'";
            SqlCommand cmd = new SqlCommand(query, conT);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            x = dt.Rows.Count;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetBrnadCount", ex.Message);
        }
        return x;
    }
    public static int GetStudentCount(SqlConnection conT)
    {
        int x = 0;
        try
        {
            string query = " Select * from CartProductEnquiry Where  Status!= 'Deleted'";
            SqlCommand cmd = new SqlCommand(query, conT);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            x = dt.Rows.Count;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetStudentCount", ex.Message);
        }
        return x;
    }

    public static int GetCourseCount(SqlConnection conT)
    {
        int x = 0;
        try
        {
            string query = "Select * from CourseDetails Where  Status!= 'Deleted'";
            SqlCommand cmd = new SqlCommand(query, conT);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            x = dt.Rows.Count;
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetCourseCount", ex.Message);
        }
        return x;
    }

    public static decimal GetTotalSales(SqlConnection conT)
    {
        decimal x = 0;
        try
        {
            string query = " Select Sum(try_convert(decimal, FinalPrice)) as TotalPrice from Orders Where  OrderStatus = 'Success' and  Status != 'Deleted'";
            SqlCommand cmd = new SqlCommand(query, conT);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                decimal cnt = 0;
                decimal.TryParse(Convert.ToString(dt.Rows[0]["TotalPrice"]), out cnt);
                x = cnt;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetTotalSales", ex.Message);
        }
        return x;
    }

    public static decimal NoOfBlogs(SqlConnection conT)
    {
        decimal x = 0;
        try
        {
            string query = " Select Count(Id) as cntB from Blogs Where  Status != 'Deleted'";
            SqlCommand cmd = new SqlCommand(query, conT);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                decimal cnt = 0;
                decimal.TryParse(Convert.ToString(dt.Rows[0]["cntB"]), out cnt);
                x = cnt;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetTotalSales", ex.Message);
        }
        return x;
    }


    public static decimal NoOfReviews(SqlConnection conT)
    {
        decimal x = 0;
        try
        {
            string query = " Select Count(Id) as cntB from ProductReview Where  Status != 'Deleted'";
            SqlCommand cmd = new SqlCommand(query, conT);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                decimal cnt = 0;
                decimal.TryParse(Convert.ToString(dt.Rows[0]["cntB"]), out cnt);
                x = cnt;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetTotalSales", ex.Message);
        }
        return x;
    }

    public static decimal NoOfEmailSubscribed(SqlConnection conT)
    {
        decimal x = 0;
        try
        {
            string query = " Select Count(Id) as cntB from EmailSubscribe Where  Status != 'Deleted'";
            SqlCommand cmd = new SqlCommand(query, conT);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                decimal cnt = 0;
                decimal.TryParse(Convert.ToString(dt.Rows[0]["cntB"]), out cnt);
                x = cnt;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "NoOfEmailSubscribed", ex.Message);
        }
        return x;
    }

    public static decimal ContactUs(SqlConnection conT)
    {
        decimal x = 0;
        try
        {
            string query = " Select Count(Id) as cntB from ContactUs Where  Status != 'Deleted'  and Status !='Archived'";
            SqlCommand cmd = new SqlCommand(query, conT);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                decimal cnt = 0;
                decimal.TryParse(Convert.ToString(dt.Rows[0]["cntB"]), out cnt);
                x = cnt;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ContactUs", ex.Message);
        }
        return x;
    }

    public static decimal ProductEnquiry(SqlConnection conT)
    {
        decimal x = 0;
        try
        {
            string query = " Select Count(Id) as cntB from ProductEnquiry Where  Status != 'Deleted'";
            SqlCommand cmd = new SqlCommand(query, conT);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                decimal cnt = 0;
                decimal.TryParse(Convert.ToString(dt.Rows[0]["cntB"]), out cnt);
                x = cnt;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ContactUs", ex.Message);
        }
        return x;
    }
    public static decimal QuickEnroll(SqlConnection conT)
    {
        decimal x = 0;
        try
        {
            string query = " Select Count(Id) as cntB from QuickEnroll Where  Status != 'Deleted' ";
            SqlCommand cmd = new SqlCommand(query, conT);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                decimal cnt = 0;
                decimal.TryParse(Convert.ToString(dt.Rows[0]["cntB"]), out cnt);
                x = cnt;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ContactUs", ex.Message);
        }
        return x;
    }

    public static decimal TodaysRequest(SqlConnection conT, string dateD)
    {
        decimal x = 0;
        try
        {
            string query = " Select Count(Id) as cntB from ContactUs Where  TRY_CONVERT(date,EnqiredOn)=@Dts and (Status != 'Deleted'  and Status !='Archived')";
            SqlCommand cmd = new SqlCommand(query, conT);
            cmd.Parameters.AddWithValue("@Dts", SqlDbType.NVarChar).Value = dateD;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                decimal cnt = 0;
                decimal.TryParse(Convert.ToString(dt.Rows[0]["cntB"]), out cnt);
                x = cnt;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ContactUs", ex.Message);
        }
        return x;
    }

    public static decimal MonthRequest(SqlConnection conT, string _month, string _yr)
    {
        decimal x = 0;
        try
        {
            string query = " Select Count(Id) as cntB from ContactUs Where month(EnqiredOn) = @_month and year(EnqiredOn) = @_yr and (Status != 'Deleted'  and Status !='Archived')";
            SqlCommand cmd = new SqlCommand(query, conT);
            cmd.Parameters.AddWithValue("@_month", SqlDbType.NVarChar).Value = _month;
            cmd.Parameters.AddWithValue("@_yr", SqlDbType.NVarChar).Value = _yr;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                decimal cnt = 0;
                decimal.TryParse(Convert.ToString(dt.Rows[0]["cntB"]), out cnt);
                x = cnt;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ContactUs", ex.Message);
        }
        return x;
    }


    public static MonthlyChart GetMonthlyValue(SqlConnection conT)
    {
        MonthlyChart chart = new MonthlyChart();
        try
        {
            int[] lSource = new int[6];
            int[] lStatus = new int[4];

            int WebsiteEnquiry = 0, PopupEnquiry = 0, WhatsApp = 0, Facebook = 0, Referral = 0, Call = 0, Closed = 0, NotClosed = 0, InProcess = 0, Pending = 0;
            DateTime toDayDate = TimeStamps.UTCTime();
            DateTime stDate = toDayDate.AddMonths(-1);
            DateTime newStDate = toDayDate.AddMonths(-1);



            string query1 = @"Select top 1 (Select Count(Id) from ContactUs Where  ESource ='Website Enquiry' and ((Status !='Deleted' or Status is  null)  and Status !='Archived')) As WebsiteEnquiry,
	(Select Count(Id) from ContactUs Where  ESource ='Popup Enquiry' and ((Status !='Deleted' or Status is  null)  and Status !='Archived')) As PopupEnquiry, 
	(Select Count(Id) From ContactUs Where  ESource ='WhatsApp' and ((Status !='Deleted' or Status is  null)  and Status !='Archived')) As WhatsApp, 
		(Select Count(Id) from ContactUs Where  ESource ='Facebook' and ((Status !='Deleted' or Status is  null)  and Status !='Archived')) As Facebook, 
		(Select Count(Id) from ContactUs Where  ESource ='Referral' and ((Status !='Deleted' or Status is  null) and Status !='Archived') ) As Referral, 
(Select Count(Id) from ContactUs Where  ESource ='Call' and ((Status !='Deleted' or Status is  null)  and Status !='Archived')) As Call  from ContactUs ";
            SqlCommand cmd1 = new SqlCommand(query1, conT);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                int.TryParse(Convert.ToString(dt1.Rows[0]["WebsiteEnquiry"]), out WebsiteEnquiry);
                int.TryParse(Convert.ToString(dt1.Rows[0]["PopupEnquiry"]), out PopupEnquiry);
                int.TryParse(Convert.ToString(dt1.Rows[0]["WhatsApp"]), out WhatsApp);
                int.TryParse(Convert.ToString(dt1.Rows[0]["Facebook"]), out Facebook);
                int.TryParse(Convert.ToString(dt1.Rows[0]["Referral"]), out Referral);
                int.TryParse(Convert.ToString(dt1.Rows[0]["Call"]), out Call);

                lSource[0] = WebsiteEnquiry;
                lSource[1] = PopupEnquiry;
                lSource[2] = WhatsApp;
                lSource[3] = Facebook;
                lSource[4] = Referral;
                lSource[5] = Call;
            }
            else
            {
                lSource[0] = 0;
                lSource[1] = 0;
                lSource[2] = 0;
                lSource[3] = 0;
                lSource[4] = 0;
                lSource[5] = 0;
            }
            chart.LeadSource = lSource;


            string query2 = @"Select top 1 (Select Count(Id) from ContactUs Where  Status ='Pending') As Pending,
	(Select Count(Id) from ContactUs Where Status ='In Process') As InProcess, 
	(Select Count(Id) From ContactUs Where Status ='Won') As Won, 
		(Select Count(Id) from ContactUs Where Status ='Not Closed') As NotClosed from ContactUs ";
            SqlCommand cmd2 = new SqlCommand(query2, conT);
            SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            if (dt2.Rows.Count > 0)
            {
                int.TryParse(Convert.ToString(dt2.Rows[0]["Pending"]), out Pending);
                int.TryParse(Convert.ToString(dt2.Rows[0]["InProcess"]), out InProcess);
                int.TryParse(Convert.ToString(dt2.Rows[0]["Won"]), out Closed);
                int.TryParse(Convert.ToString(dt2.Rows[0]["NotClosed"]), out NotClosed);

                lStatus[0] = Pending;
                lStatus[1] = InProcess;
                lStatus[2] = Closed;
                lStatus[3] = NotClosed;
            }
            else
            {
                lStatus[0] = 0;
                lStatus[1] = 0;
                lStatus[2] = 0;
                lStatus[3] = 0;
            }
            chart.LeadStatus = lStatus;

            string query = "Select Count(Id) LeadCount,Day(EnqiredOn) Day_, Month(EnqiredOn) Month_, Year(EnqiredOn) Year_ from ContactUs Where (EnqiredOn Between @StartDate and @EndDate) and ((Status !='Deleted' or Status is null) and Status !='Archived')  Group By Day(EnqiredOn),Month(EnqiredOn),  Year(EnqiredOn)  order by Year(EnqiredOn) Desc, Month(EnqiredOn) desc";
            SqlCommand cmd = new SqlCommand(query, conT);
            cmd.Parameters.AddWithValue("@StartDate", SqlDbType.NVarChar).Value = newStDate.ToString("dd/MMM/yyyy");
            cmd.Parameters.AddWithValue("@EndDate", SqlDbType.NVarChar).Value = toDayDate.AddDays(1).ToString("dd/MMM/yyyy");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                int i = 0, dateDiff = 0;

                dateDiff = Convert.ToInt32(Math.Ceiling((toDayDate.AddDays(1) - newStDate).TotalDays));

                string[] dmons = new string[dateDiff];
                decimal[] sMts = new decimal[dateDiff];
                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {
                    var calls = dt.AsEnumerable().Where(v => ((v.Field<int>("Day_") == dtts.Day) && (v.Field<int>("Month_") == dtts.Month) && (v.Field<int>("Year_") == dtts.Year)));
                    if (calls.Count() > 0)
                    {
                        var sl = calls.Select(x => new { LeadCount = x.Field<int>("LeadCount") }).FirstOrDefault();

                        int ldCnt = sl != null ? sl.LeadCount : 0;
                        dmons[i] = dtts.ToString("dd MMM yy");
                        sMts[i] = ldCnt;
                    }
                    else
                    {
                        dmons[i] = dtts.ToString("dd MMM yy");
                        sMts[i] = 0;
                    }
                    i++;
                    dtts = dtts.AddDays(1);
                }

                chart.DaysNMonth = dmons;
                chart.NoOfLeads = sMts;


            }
            else
            {


                int i = 0, dateDiff = 0;
                dateDiff = Convert.ToInt32(Math.Ceiling((toDayDate.AddDays(1) - newStDate).TotalDays));
                string[] dmons = new string[dateDiff];
                decimal[] sMts = new decimal[dateDiff];

                for (DateTime dtts = newStDate; dtts <= toDayDate;)
                {
                    dmons[i] = dtts.ToString("dd MMM yy");
                    sMts[i] = 0;
                    i++;
                    dtts = dtts.AddDays(1);
                }
                chart.DaysNMonth = dmons;
                chart.NoOfLeads = sMts;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetMonthlyValue", ex.Message);
        }
        return chart;
    }


}


public class MonthlyChart
{
    public string[] DaysNMonth { get; set; }
    public decimal[] NoOfLeads { get; set; }
    public int[] LeadStatus { get; set; }
    public int[] LeadSource { get; set; }

}