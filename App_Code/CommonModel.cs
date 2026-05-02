using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

public class CommonModel
{
    public static string IPAddress()
    {
        string ipadd = "";
        try
        {
            String ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {

                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                string ipStr = ip.Replace("::ffff:", "");
                ip = ipStr;
            }
            ipadd = ip;
        }
        catch (Exception ex)
        {

        }
        return ipadd;
    }
    public static string GetCountryFromIP()
    {
        string Country = "";
        try
        {
            string ipaddr = IPAddress();
            HttpWebRequest objreq = (HttpWebRequest)WebRequest.Create("http://ip-api.com/json/" + ipaddr);
            HttpWebResponse objres = (HttpWebResponse)objreq.GetResponse();
            StreamReader resreader = new StreamReader(objres.GetResponseStream());
            string responseread = resreader.ReadToEnd();
            resreader.Close();
            resreader.Dispose();
            dynamic dynObj = JsonConvert.DeserializeObject(responseread);
            if (dynObj.country != null) 
            {
                Country = dynObj.country; 
            }
        }
        catch (Exception ex)
        {
        }
        return Country;
    }
    public static void SaveJpeg(string path, System.Drawing.Image img, int quality)
    {
        if (quality < 0 || quality > 100)
            throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");

        // Encoder parameter for image quality 
        EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
        // JPEG image codec 
        ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
        EncoderParameters encoderParams = new EncoderParameters(1);
        encoderParams.Param[0] = qualityParam;

        //img.Width = 800;
        img.Save(path, jpegCodec, encoderParams);
    }
    public static void SavePNG(string path, System.Drawing.Image img, int quality)
    {
        if (quality < 0 || quality > 100)
            throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");

        // Encoder parameter for image quality 
        EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
        // JPEG image codec 
        ImageCodecInfo jpegCodec = GetEncoderInfo("image/png");
        EncoderParameters encoderParams = new EncoderParameters(1);
        encoderParams.Param[0] = qualityParam;

        //img.Width = 800;
        img.Save(path, jpegCodec, encoderParams);
    }
    public static System.Drawing.Image ScaleImageBig(System.Drawing.Image image, int height, int width)
    {
        var newImage = new Bitmap(width, height);
        using (var g = Graphics.FromImage(newImage))
        {
            g.DrawImage(image, 0, 0, width, height);
        }
        return newImage;
    }
    private static ImageCodecInfo GetEncoderInfo(string mimeType)
    {
        // Get image codecs for all image formats 
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

        // Find the correct image codec 
        for (int i = 0; i < codecs.Length; i++)
            if (codecs[i].MimeType == mimeType)
                return codecs[i];

        return null;
    }
    public static string Encrypt(string clearText)
    { 
        string EncryptionKey = "GV1001234567890$%^&*MYQ";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
    public static string Decrypt(string cipherText)
    {
        string EncryptionKey = "GV1001234567890$%^&*MYQ";
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
    public static void ReassignCurrencyCookie(string val)
    {
        if (HttpContext.Current.Request.Cookies["t_ctry"] == null)
        {
            HttpCookie cookie = new HttpCookie("t_ctry");
            cookie.Value = val;
            cookie.Expires = DateTime.Now.AddDays(10);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        else
        {
            var cookie = HttpContext.Current.Response.Cookies["t_ctry"];
            cookie.Value = val;
            cookie.Expires = DateTime.Now.AddDays(10);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
    public static void AddRecentProductCookie(string cid, string pdid)
    {
        try
        {
            if (HttpContext.Current.Request.Cookies["t_rv"] == null)
            {
                HttpCookie rv = new HttpCookie("t_rv");
                rv.Value = CommonModel.Encrypt(pdid);
                rv.Expires = TimeStamps.UTCTime().AddDays(10);
                HttpContext.Current.Response.Cookies.Add(rv);
            }
            else
            {
                string actval = CommonModel.Decrypt(HttpContext.Current.Request.Cookies["t_rv"].Value);
                List<string> val = actval.Split(',').ToList();
                string exist = val.Where(s => s == pdid).SingleOrDefault();
                if (val.Count > 9)
                {
                    HttpCookie rv = new HttpCookie("t_rv");
                    if (exist == null)
                    {
                        val.RemoveAt(val.Count - 1);
                        val.Reverse();
                        val.Add(pdid);
                        val.Reverse();
                    }
                    string retval = "";
                    foreach (var lp in val)
                    {
                        retval += lp + ",";
                    }
                    rv.Value = CommonModel.Encrypt(retval.Substring(0, retval.Length - 1));
                    rv.Expires = TimeStamps.UTCTime().AddDays(10);
                    HttpContext.Current.Response.Cookies.Add(rv);
                }
                else
                {
                    HttpCookie rv = new HttpCookie("t_rv");
                    if (exist == null)
                    {
                        actval = pdid + "," + actval;
                    }
                    rv.Value = CommonModel.Encrypt(actval);
                    rv.Expires = TimeStamps.UTCTime().AddDays(10);
                    HttpContext.Current.Response.Cookies.Add(rv);
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "AddRecentProduct", ex.Message);
        }
    }
    public static string urlval(string url)
    {
        string[] strArr = url.Split('-');
        string resulturl = string.Empty;
        for (int i = 0; i < strArr.Length; i++)
        {
            if (resulturl == "")
            {
                resulturl = strArr[i];
            }
            else
            {
                resulturl = resulturl + " " + strArr[i];
            }
        }
        return resulturl;
    }
    public static string seourl(string url)
    {
        string[] strArr = url.Split(' ');
        string resulturl = string.Empty;
        for (int i = 0; i < strArr.Length; i++)
        {
            if (resulturl == "")
            {
                resulturl = strArr[i];
            }
            else
            {
                resulturl = resulturl + "-" + strArr[i];
            }
        }
        return resulturl;
    }
    public static DateTime UTCTime()
    {
        DateTime localTime;
        DateTime utcTime = DateTime.UtcNow;

        TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi); // convert from utc to local
        return localTime;

        //DateTime utcTime = DateTime.UtcNow;
        //return utcTime;
    }
    public static void CaptureException(string pg, string block, string excep)
    {
        try
        {
            SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
            SqlCommand cmd = new SqlCommand("insert into CaptureException (Ex_Page, Ex_Block, Excep, Added_On, Added_IP) values(@Ex_Page, @Ex_Block, @Excep, @Added_On, @Added_IP)", conT);
            cmd.Parameters.AddWithValue("@Ex_Page", SqlDbType.NVarChar).Value = pg;
            cmd.Parameters.AddWithValue("@Ex_Block", SqlDbType.NVarChar).Value = block;
            cmd.Parameters.AddWithValue("@Excep", SqlDbType.NVarChar).Value = excep;
            cmd.Parameters.AddWithValue("@Added_On", SqlDbType.DateTime).Value = UTCTime();
            cmd.Parameters.AddWithValue("@Added_IP", SqlDbType.NVarChar).Value = IPAddress();
            conT.Open();
            int res = cmd.ExecuteNonQuery();
            conT.Close();
        }
        catch (Exception ex)
        {

        }
    }
}
