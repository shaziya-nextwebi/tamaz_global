using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class DashBoard
{
    public DashBoard() { }

    public static int GetProductCount(SqlConnection conT)
    {
        int x = 0;
        try
        {
            string query = "SELECT COUNT(Id) AS cnt FROM ProductDetails WHERE Status = 'Active'";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                conT.Open();
                x = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProductCount", ex.Message);
        }
        finally
        {
            if (conT.State == ConnectionState.Open) conT.Close();
        }
        return x;
    }

    public static int GetCategoryCount(SqlConnection conT)
    {
        int x = 0;
        try
        {
            string query = "SELECT COUNT(Id) AS cnt FROM Category WHERE Status = 'Active'";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                conT.Open();
                x = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetCategoryCount", ex.Message);
        }
        finally
        {
            if (conT.State == ConnectionState.Open) conT.Close();
        }
        return x;
    }

    public static int GetBrnadCount(SqlConnection conT)
    {
        int x = 0;
        try
        {
            string query = "SELECT COUNT(Id) AS cnt FROM Brand WHERE Status = 'Active'";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                conT.Open();
                x = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetBrnadCount", ex.Message);
        }
        finally
        {
            if (conT.State == ConnectionState.Open) conT.Close();
        }
        return x;
    }

    public static int GetStudentCount(SqlConnection conT)
    {
        int x = 0;
        try
        {
            string query = "SELECT COUNT(Id) AS cnt FROM CartProductEnquiry WHERE Status != 'Deleted'";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                conT.Open();
                x = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetStudentCount", ex.Message);
        }
        finally
        {
            if (conT.State == ConnectionState.Open) conT.Close();
        }
        return x;
    }

    public static decimal NoOfBlogs(SqlConnection conT)
    {
        decimal x = 0;
        try
        {
            string query = "SELECT COUNT(Id) AS cnt FROM Blogs WHERE Status != 'Deleted'";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                conT.Open();
                x = Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "NoOfBlogs", ex.Message);
        }
        finally
        {
            if (conT.State == ConnectionState.Open) conT.Close();
        }
        return x;
    }

    public static decimal ContactUs(SqlConnection conT)
    {
        decimal x = 0;
        try
        {
            string query = "SELECT COUNT(Id) AS cnt FROM ContactUs WHERE Status != 'Deleted' AND Status != 'Archived'";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                conT.Open();
                x = Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ContactUs", ex.Message);
        }
        finally
        {
            if (conT.State == ConnectionState.Open) conT.Close();
        }
        return x;
    }

    public static decimal ProductEnquiry(SqlConnection conT)
    {
        decimal x = 0;
        try
        {
            string query = "SELECT COUNT(Id) AS cnt FROM ProductEnquiry WHERE Status != 'Deleted'";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                conT.Open();
                x = Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "ProductEnquiry", ex.Message);
        }
        finally
        {
            if (conT.State == ConnectionState.Open) conT.Close();
        }
        return x;
    }

    public static decimal GetTotalSales(SqlConnection conT)
    {
        decimal x = 0;
        try
        {
            string query = "SELECT ISNULL(SUM(TRY_CONVERT(decimal, FinalPrice)), 0) AS TotalPrice FROM Orders WHERE OrderStatus = 'Success' AND Status != 'Deleted'";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                conT.Open();
                x = Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetTotalSales", ex.Message);
        }
        finally
        {
            if (conT.State == ConnectionState.Open) conT.Close();
        }
        return x;
    }

    public static decimal NoOfReviews(SqlConnection conT)
    {
        decimal x = 0;
        try
        {
            string query = "SELECT COUNT(Id) AS cnt FROM ProductReview WHERE Status != 'Deleted'";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                conT.Open();
                x = Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "NoOfReviews", ex.Message);
        }
        finally
        {
            if (conT.State == ConnectionState.Open) conT.Close();
        }
        return x;
    }

    public static decimal NoOfEmailSubscribed(SqlConnection conT)
    {
        decimal x = 0;
        try
        {
            string query = "SELECT COUNT(Id) AS cnt FROM EmailSubscribe WHERE Status != 'Deleted'";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                conT.Open();
                x = Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "NoOfEmailSubscribed", ex.Message);
        }
        finally
        {
            if (conT.State == ConnectionState.Open) conT.Close();
        }
        return x;
    }

    public static decimal TodaysRequest(SqlConnection conT, string dateD)
    {
        decimal x = 0;
        try
        {
            string query = "SELECT COUNT(Id) AS cnt FROM ContactUs WHERE TRY_CONVERT(date, EnqiredOn) = @Dts AND Status != 'Deleted' AND Status != 'Archived'";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Dts", dateD);
                conT.Open();
                x = Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "TodaysRequest", ex.Message);
        }
        finally
        {
            if (conT.State == ConnectionState.Open) conT.Close();
        }
        return x;
    }

    public static decimal MonthRequest(SqlConnection conT, string _month, string _yr)
    {
        decimal x = 0;
        try
        {
            string query = "SELECT COUNT(Id) AS cnt FROM ContactUs WHERE MONTH(EnqiredOn) = @_month AND YEAR(EnqiredOn) = @_yr AND Status != 'Deleted' AND Status != 'Archived'";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@_month", _month);
                cmd.Parameters.AddWithValue("@_yr", _yr);
                conT.Open();
                x = Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "MonthRequest", ex.Message);
        }
        finally
        {
            if (conT.State == ConnectionState.Open) conT.Close();
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

            DateTime toDayDate = TimeStamps.UTCTime();
            DateTime newStDate = toDayDate.AddMonths(-1);

            // Lead Source counts
            string query1 = @"SELECT 
                (SELECT COUNT(Id) FROM ContactUs WHERE ESource='Website Enquiry' AND Status NOT IN ('Deleted','Archived')) AS WebsiteEnquiry,
                (SELECT COUNT(Id) FROM ContactUs WHERE ESource='Popup Enquiry'   AND Status NOT IN ('Deleted','Archived')) AS PopupEnquiry,
                (SELECT COUNT(Id) FROM ContactUs WHERE ESource='WhatsApp'        AND Status NOT IN ('Deleted','Archived')) AS WhatsApp,
                (SELECT COUNT(Id) FROM ContactUs WHERE ESource='Facebook'        AND Status NOT IN ('Deleted','Archived')) AS Facebook,
                (SELECT COUNT(Id) FROM ContactUs WHERE ESource='Referral'        AND Status NOT IN ('Deleted','Archived')) AS Referral,
                (SELECT COUNT(Id) FROM ContactUs WHERE ESource='Call'            AND Status NOT IN ('Deleted','Archived')) AS Call";

            using (SqlCommand cmd1 = new SqlCommand(query1, conT))
            {
                conT.Open();
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                conT.Close();

                if (dt1.Rows.Count > 0)
                {
                    int.TryParse(Convert.ToString(dt1.Rows[0]["WebsiteEnquiry"]), out lSource[0]);
                    int.TryParse(Convert.ToString(dt1.Rows[0]["PopupEnquiry"]), out lSource[1]);
                    int.TryParse(Convert.ToString(dt1.Rows[0]["WhatsApp"]), out lSource[2]);
                    int.TryParse(Convert.ToString(dt1.Rows[0]["Facebook"]), out lSource[3]);
                    int.TryParse(Convert.ToString(dt1.Rows[0]["Referral"]), out lSource[4]);
                    int.TryParse(Convert.ToString(dt1.Rows[0]["Call"]), out lSource[5]);
                }
            }
            chart.LeadSource = lSource;

            // Lead Status counts
            string query2 = @"SELECT
                (SELECT COUNT(Id) FROM ContactUs WHERE Status='Pending')    AS Pending,
                (SELECT COUNT(Id) FROM ContactUs WHERE Status='In Process') AS InProcess,
                (SELECT COUNT(Id) FROM ContactUs WHERE Status='Won')        AS Won,
                (SELECT COUNT(Id) FROM ContactUs WHERE Status='Not Closed') AS NotClosed";

            using (SqlCommand cmd2 = new SqlCommand(query2, conT))
            {
                conT.Open();
                SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);
                conT.Close();

                if (dt2.Rows.Count > 0)
                {
                    int.TryParse(Convert.ToString(dt2.Rows[0]["Pending"]), out lStatus[0]);
                    int.TryParse(Convert.ToString(dt2.Rows[0]["InProcess"]), out lStatus[1]);
                    int.TryParse(Convert.ToString(dt2.Rows[0]["Won"]), out lStatus[2]);
                    int.TryParse(Convert.ToString(dt2.Rows[0]["NotClosed"]), out lStatus[3]);
                }
            }
            chart.LeadStatus = lStatus;

            // Daily chart data
            string query3 = @"SELECT COUNT(Id) LeadCount, DAY(EnqiredOn) Day_, MONTH(EnqiredOn) Month_, YEAR(EnqiredOn) Year_ 
                FROM ContactUs 
                WHERE EnqiredOn BETWEEN @StartDate AND @EndDate 
                AND Status NOT IN ('Deleted','Archived')
                GROUP BY DAY(EnqiredOn), MONTH(EnqiredOn), YEAR(EnqiredOn)
                ORDER BY YEAR(EnqiredOn) DESC, MONTH(EnqiredOn) DESC";

            using (SqlCommand cmd3 = new SqlCommand(query3, conT))
            {
                cmd3.Parameters.AddWithValue("@StartDate", newStDate.ToString("dd/MMM/yyyy"));
                cmd3.Parameters.AddWithValue("@EndDate", toDayDate.AddDays(1).ToString("dd/MMM/yyyy"));

                conT.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd3);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                conT.Close();

                int dateDiff = Convert.ToInt32(Math.Ceiling((toDayDate.AddDays(1) - newStDate).TotalDays));
                string[] dmons = new string[dateDiff];
                decimal[] sMts = new decimal[dateDiff];
                int i = 0;

                for (DateTime dtts = newStDate; dtts <= toDayDate; dtts = dtts.AddDays(1))
                {
                    var calls = dt.AsEnumerable().Where(v =>
                        v.Field<int>("Day_") == dtts.Day &&
                        v.Field<int>("Month_") == dtts.Month &&
                        v.Field<int>("Year_") == dtts.Year);

                    dmons[i] = dtts.ToString("dd MMM yy");
                    sMts[i] = calls.Any() ? calls.First().Field<int>("LeadCount") : 0;
                    i++;
                }

                chart.DaysNMonth = dmons;
                chart.NoOfLeads = sMts;
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetMonthlyValue", ex.Message);
        }
        finally
        {
            if (conT.State == ConnectionState.Open) conT.Close();
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