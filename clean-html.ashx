<%@ WebHandler Language="C#" Class="CleanHtml" %>
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

public class CleanHtml : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";

        try
        {
            string rawHtml = "";
            using (var reader = new StreamReader(context.Request.InputStream, Encoding.UTF8))
                rawHtml = reader.ReadToEnd();

            if (string.IsNullOrWhiteSpace(rawHtml))
            {
                context.Response.Write("{\"error\":\"Empty content\"}");
                return;
            }

            string apiKey = System.Configuration.ConfigurationManager.AppSettings["MistralApiKey"];

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                context.Response.Write("{\"error\":\"MistralApiKey is missing from Web.config\"}");
                return;
            }

            string prompt = "You are an HTML cleaner. Clean the following HTML content:\n\n" +
                            "RULES:\n" +
                            "- Remove ALL inline styles (style=\"...\")\n" +
                            "- Remove ALL class attributes\n" +
                            "- Remove ALL id attributes\n" +
                            "- Remove ALL data-* attributes\n" +
                            "- Remove ALL span tags but KEEP their inner text/content\n" +
                            "- Remove empty tags like <p></p>, <div></div>, <span></span>\n" +
                            "- Collapse multiple &nbsp; or extra whitespace into single spaces\n" +
                            "- Keep semantic tags: <p>,<h1>-<h6>,<ul>,<ol>,<li>,<table>,<thead>,<tbody>,<tr>,<td>,<th>,<strong>,<em>,<a>,<img>,<br>,<hr>\n" +
                            "- Keep href, src, alt, target, rel, colspan, rowspan attributes\n" +
                            "- Fix any broken or unclosed tags\n" +
                            "- Do NOT change the actual text content or meaning\n" +
                            "- Do NOT add any explanation or commentary\n" +
                            "- Return ONLY the cleaned HTML, nothing else\n\n" +
                            "HTML TO CLEAN:\n" + rawHtml;

            string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                model = "mistral-large-latest",
                messages = new[] {
                    new { role = "user", content = prompt }
                },
                max_tokens = 4096,
                temperature = 0.1
            });

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                                                 | SecurityProtocolType.Tls11
                                                 | SecurityProtocolType.Tls;

            var req = (HttpWebRequest)WebRequest.Create("https://api.mistral.ai/v1/chat/completions");
            req.Method = "POST";
            req.ContentType = "application/json";
            req.Timeout = 120000;
            req.Headers.Add("Authorization", "Bearer " + apiKey);

            byte[] bytes = Encoding.UTF8.GetBytes(jsonPayload);
            req.ContentLength = bytes.Length;
            using (var stream = req.GetRequestStream())
                stream.Write(bytes, 0, bytes.Length);

            string resultJson = "";
            using (var response = (HttpWebResponse)req.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                resultJson = reader.ReadToEnd();

            var parsed = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(resultJson);
            string cleaned = parsed.choices[0].message.content;

            string cleanedStr = cleaned.ToString()
                .Replace("```html", "")
                .Replace("```", "")
                .Trim();

            context.Response.Write(
                Newtonsoft.Json.JsonConvert.SerializeObject(new { html = cleanedStr })
            );
        }
        catch (WebException wex)
        {
            string errorBody = "";
            if (wex.Response != null)
            {
                using (var errReader = new StreamReader(wex.Response.GetResponseStream(), Encoding.UTF8))
                    errorBody = errReader.ReadToEnd();
            }
            context.Response.Write(
                Newtonsoft.Json.JsonConvert.SerializeObject(new { error = wex.Message + " | " + errorBody })
            );
        }
        catch (Exception ex)
        {
            context.Response.Write(
                Newtonsoft.Json.JsonConvert.SerializeObject(new { error = ex.Message })
            );
        }
    }

    public bool IsReusable { get { return false; } }
}
//using System;
//using System.IO;
//using System.Net;
//using System.Text;
//using System.Web;

//public class CleanHtml : IHttpHandler, System.Web.SessionState.IRequiresSessionState
//{
//    public void ProcessRequest(HttpContext context)
//    {
//        context.Response.ContentType = "application/json";

//        try
//        {
//            string rawHtml = "";
//            using (var reader = new StreamReader(context.Request.InputStream, Encoding.UTF8))
//                rawHtml = reader.ReadToEnd();

//            if (string.IsNullOrWhiteSpace(rawHtml))
//            {
//                context.Response.Write("{\"error\":\"Empty content\"}");
//                return;
//            }

//            string apiKey = System.Configuration.ConfigurationManager.AppSettings["grokAPI"];

//            if (string.IsNullOrWhiteSpace(apiKey))
//            {
//                context.Response.Write("{\"error\":\"grokAPI is missing from Web.config\"}");
//                return;
//            }

//            string prompt = "You are an HTML cleaner. Clean the following HTML content:\n\n" +
//                            "RULES:\n" +
//                            "- Remove ALL inline styles (style=\"...\")\n" +
//                            "- Remove ALL class attributes\n" +
//                            "- Remove ALL id attributes\n" +
//                            "- Remove ALL data-* attributes\n" +
//                            "- Remove ALL span tags but KEEP their inner text/content\n" +
//                            "- Remove empty tags like <p></p>, <div></div>, <span></span>\n" +
//                            "- Collapse multiple &nbsp; or extra whitespace into single spaces\n" +
//                            "- Keep semantic tags: <p>,<h1>-<h6>,<ul>,<ol>,<li>,<table>,<thead>,<tbody>,<tr>,<td>,<th>,<strong>,<em>,<a>,<img>,<br>,<hr>\n" +
//                            "- Keep href, src, alt, target, rel, colspan, rowspan attributes\n" +
//                            "- Fix any broken or unclosed tags\n" +
//                            "- Do NOT change the actual text content or meaning\n" +
//                            "- Do NOT add any explanation or commentary\n" +
//                            "- Return ONLY the cleaned HTML, nothing else\n\n" +
//                            "HTML TO CLEAN:\n" + rawHtml;

//            string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(new
//            {
//                model = "llama-3.3-70b-versatile",
//                messages = new[] {
//                    new { role = "user", content = prompt }
//                },
//                max_tokens = 4096,
//                temperature = 0.1
//            });

//            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
//                                                 | SecurityProtocolType.Tls11
//                                                 | SecurityProtocolType.Tls;

//            var req = (HttpWebRequest)WebRequest.Create("https://api.groq.com/openai/v1/chat/completions");
//            req.Method = "POST";
//            req.ContentType = "application/json";
//            req.Timeout = 30000;
//            req.Headers.Add("Authorization", "Bearer " + apiKey);

//            byte[] bytes = Encoding.UTF8.GetBytes(jsonPayload);
//            req.ContentLength = bytes.Length;
//            using (var stream = req.GetRequestStream())
//                stream.Write(bytes, 0, bytes.Length);

//            string resultJson = "";
//            using (var response = (HttpWebResponse)req.GetResponse())
//            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
//                resultJson = reader.ReadToEnd();

//            var parsed = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(resultJson);
//            string cleaned = parsed.choices[0].message.content;

//            string cleanedStr = cleaned.ToString()
//                .Replace("```html", "")
//                .Replace("```", "")
//                .Trim();

//            context.Response.Write(
//                Newtonsoft.Json.JsonConvert.SerializeObject(new { html = cleanedStr })
//            );
//        }
//        catch (WebException wex)
//        {
//            string errorBody = "";
//            if (wex.Response != null)
//            {
//                using (var errReader = new StreamReader(wex.Response.GetResponseStream(), Encoding.UTF8))
//                    errorBody = errReader.ReadToEnd();
//            }
//            context.Response.Write(
//                Newtonsoft.Json.JsonConvert.SerializeObject(new { error = wex.Message + " | " + errorBody })
//            );
//        }
//        catch (Exception ex)
//        {
//            context.Response.Write(
//                Newtonsoft.Json.JsonConvert.SerializeObject(new { error = ex.Message })
//            );
//        }
//    }

//    public bool IsReusable { get { return false; } }
//}