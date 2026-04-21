using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

public class ContactUs
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string EmailId { get; set; }
    public string Message { get; set; }
    public string Phone { get; set; }
    public DateTime CreatedOn { get; set; }
    public string Status { get; set; }
    public static int InsertContactUs(SqlConnection conT, ContactUs obj)
    {
        try
        {
            string query = @"INSERT INTO ContactUs
                         (Name, EmailId, Message,Phone)
                         VALUES
                         (@Name, @EmailId, @Message,@Phone)";

            SqlCommand cmd = new SqlCommand(query, conT);

            cmd.Parameters.AddWithValue("@Name", obj.Name);
            cmd.Parameters.AddWithValue("@EmailId", obj.EmailId);
            cmd.Parameters.AddWithValue("@Phone", obj.Phone);
            cmd.Parameters.AddWithValue("@Message", obj.Message);

            if (conT.State == ConnectionState.Closed)
                conT.Open();

            return cmd.ExecuteNonQuery();
        }
        catch
        {
            return 0;
        }
    }
    public static List<ContactUs> GetAllContactRequests(SqlConnection conT)
    {
        List<ContactUs> list = new List<ContactUs>();

        try
        {
            string query = @"SELECT Id, Name, EmailId, Message, Phone, CreatedOn, Status 
                         FROM ContactUs where Status='Active'
                         ORDER BY Id DESC";

            SqlCommand cmd = new SqlCommand(query, conT);

            if (conT.State == ConnectionState.Closed)
                conT.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                ContactUs obj = new ContactUs();

                obj.Id = Convert.ToInt32(dr["Id"]);
                obj.Name = dr["Name"].ToString();
                obj.EmailId = dr["EmailId"].ToString();
                obj.Message = dr["Message"].ToString();
                obj.Phone = dr["Phone"].ToString();
                obj.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                obj.Status = dr["Status"] == DBNull.Value ? "" : dr["Status"].ToString();

                list.Add(obj);
            }

            dr.Close();
        }
        catch
        {
        }

        return list;
    }
    public static async Task<int> ContactRequest(ContactUs con)
    {
        try
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(ConfigurationManager.AppSettings["ToMail"]);
            if (ConfigurationManager.AppSettings["CCMail"] != "")
            {
                mail.CC.Add(ConfigurationManager.AppSettings["CCMail"]);
            }
            if (ConfigurationManager.AppSettings["BCCMail"] != "")
            {
                mail.Bcc.Add(ConfigurationManager.AppSettings["BCCMail"]);
            }
            mail.From = new MailAddress(ConfigurationManager.AppSettings["from"], ConfigurationManager.AppSettings["fromName"]);
            mail.Subject = "ContactUs Request - Sportico";
            mail.Body = "Hi Admin, <br><br>You have received a contactus request from " + con.Name + ".<br><br>" +
                "<u><b>" +
                "<i>Details : </i>" +
                "</b>" +
                "</u><br>" +

                "Name : " + con.Name + "<br>" +
                "Email-Id : " + con.EmailId + "<br>" +           
                "Message : " + con.Message + "<br><br><br>" +

                "Regards,<br>Sportico";
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["host"];
            smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            smtp.Credentials = new System.Net.NetworkCredential
                           (ConfigurationManager.AppSettings["userName"], ConfigurationManager.AppSettings["password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]);
            await Task.Run(() => smtp.Send(mail));
            return 1;

        }
        catch (Exception ex)
        {
            return 0;
        }
    }
    public static int Delete(SqlConnection con, ContactUs rev)
    {
        SqlCommand cmd = new SqlCommand(
            "UPDATE ContactUs SET Status='Deleted', CreatedOn=@CreatedOn WHERE Id=@Id",
            con);

        cmd.Parameters.AddWithValue("@Id", rev.Id);
        cmd.Parameters.AddWithValue("@CreatedOn", rev.CreatedOn);

        con.Open();
        int i = cmd.ExecuteNonQuery();
        con.Close();

        return i;
    }

}

