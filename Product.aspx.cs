using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

public partial class ProductDetails_Page : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(
        ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

    public string strProductUrl = "";
    public string strProductId = "";   
    public string strProductName = "";
    public string strBrand = "";
    public string strCategory = "";
    public string strCategoryUrl = "";
    public string strPlaceOfOrigin = "";
    public string strKeyIngredient = "";
    public string strShortDesc = "";
    public string strFullDesc = "";
    public string strBenefitsDesc = "";
    public string strIngredientsDesc = "";
    public string strUsageDesc = "";
    public string strRetailPrice = "";
    public string strSmallImage = "";
    public string strAvailability = "";
    public string strLabelName = "";
    public string strIngredientTags = "";
    public string strAvailBadge = "";
    public string strLabelBadge = "";
    public string strProdFaqs = "";   

    protected void Page_Load(object sender, EventArgs e)
    {
        strProductUrl = Convert.ToString(RouteData.Values["producturl"]);

        if (!string.IsNullOrEmpty(strProductUrl))
        {
            BindProductDetail();
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    public void BindProductDetail()
    {
        try
        {
            ProductDetails pd = ProductDetails.GetProductDetailsByUrl(conT, strProductUrl);

            if (pd != null)
            {
                // SEO
                Page.Title = !string.IsNullOrEmpty(pd.PageTitle)
                    ? pd.PageTitle
                    : pd.ProductName + " - TAMAZ Global";

                if (!string.IsNullOrEmpty(pd.MetaDesc)) Page.MetaDescription = pd.MetaDesc;
                if (!string.IsNullOrEmpty(pd.MetaKeys)) Page.MetaKeywords = pd.MetaKeys;

                // Expose the numeric ID for the gallery AJAX call
                strProductId = pd.Id.ToString();

                strProductName = pd.ProductName;
                strBrand = pd.Brand;
                strCategory = pd.Category;
                strCategoryUrl = pd.CategoryUrl;
                strPlaceOfOrigin = pd.PlaceOfOrigin;
                strKeyIngredient = pd.KeyIngredient;
                strShortDesc = pd.ProductShortDesc;
                strFullDesc = pd.FullDesc;
                strBenefitsDesc = pd.BenefitsDesc;
                strIngredientsDesc = pd.IngredientsDesc;
                strUsageDesc = pd.UsageDesc;
                strRetailPrice = pd.RetailPrice;
                strSmallImage = !string.IsNullOrEmpty(pd.SmallImage)
                                        ? pd.SmallImage
                                        : "assests/Images/placeholder.jpg";
                strAvailability = pd.ProductAvailability;
                strLabelName = pd.ProductLabelName;

                // ---- Ingredient tags ----
                string[] tagColors = {
                    "bg-[#dbeafe] text-sky-800 border-sky-200",
                    "bg-green-100 text-green-800 border-green-200",
                    "bg-purple-100 text-purple-800 border-purple-200",
                    "bg-yellow-100 text-yellow-800 border-yellow-200",
                    "bg-pink-100 text-pink-800 border-pink-200"
                };
                if (!string.IsNullOrEmpty(strKeyIngredient))
                {
                    string[] ings = strKeyIngredient.Split(',');
                    for (int i = 0; i < ings.Length; i++)
                    {
                        string ing = ings[i].Trim();
                        if (ing != "")
                            strIngredientTags += "<span class='px-3 py-1.5 " +
                                tagColors[i % tagColors.Length] +
                                " text-xs font-semibold rounded-lg border'>" + ing + "</span>";
                    }
                }

                // ---- Availability badge ----
                strAvailBadge = (pd.ProductAvailability == "Available" || pd.InStock == "Yes")
                    ? @"<span class='inline-flex items-center gap-2 bg-[#2E7D32] text-white text-xs font-bold px-4 py-1.5 rounded-full shadow-sm'>
                            <svg width='14' height='14' viewBox='0 0 23 17' fill='none' xmlns='http://www.w3.org/2000/svg'>
                                <path d='M19.0312 4.03125H16.0312V0H2.01562C0.9375 0 0 0.9375 0 2.01562V13.0312H2.01562C2.01562 14.6719 3.375 16.0312 5.01562 16.0312C6.65625 16.0312 8.01562 14.6719 8.01562 13.0312H14.0156C14.0156 14.6719 15.375 16.0312 17.0156 16.0312C18.6562 16.0312 20.0156 14.6719 20.0156 13.0312H22.0312V8.01562L19.0312 4.03125ZM18.5156 5.53125L20.4844 8.01562H16.0312V5.53125H18.5156ZM5.01562 14.0156C4.45312 14.0156 4.03125 13.5469 4.03125 13.0312C4.03125 12.4688 4.45312 12 5.01562 12C5.57812 12 6 12.4688 6 13.0312C6 13.5469 5.57812 14.0156 5.01562 14.0156ZM7.21875 11.0156C6.70312 10.4062 5.90625 10.0312 5.01562 10.0312C4.125 10.0312 3.32812 10.4062 2.8125 11.0156H2.01562V2.01562H14.0156V11.0156H7.21875ZM17.0156 14.0156C16.4531 14.0156 16.0312 13.5469 16.0312 13.0312C16.0312 12.4688 16.4531 12 17.0156 12C17.5781 12 18 12.4688 18 13.0312C18 13.5469 17.5781 14.0156 17.0156 14.0156Z' fill='currentColor'/>
                            </svg>
                            Available
                       </span>"
                    : "<span class='inline-flex items-center gap-2 bg-gray-400 text-white text-xs font-bold px-4 py-1.5 rounded-full shadow-sm'>Out of Stock</span>";

                // ---- Label badge ----
                strLabelBadge = !string.IsNullOrEmpty(strLabelName)
                    ? "<span class='absolute top-4 left-4 bg-[#B91C1C] text-white text-xs font-bold px-3 py-1 rounded-full'>" + strLabelName + "</span>"
                    : "";

                // ---- FAQs ----
                BuildFaqHtml(strProductId);
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery, "BindProductDetail", ex.Message);
        }
    }

    private void BuildFaqHtml(string pid)
    {
        try
        {
            var faqs = FAQs.GetAllProductFAQ(conT)
                           .Where(x => x.Pid.Trim() == pid.Trim())
                           .ToList();

            foreach (FAQs faq in faqs)
            {
                strProdFaqs += @"
                <div class='faq-item border border-gray-200 rounded-xl overflow-hidden'>
                    <span class='faq-question w-full flex items-center justify-between p-5 text-left font-semibold text-[#0F172A] hover:bg-gray-50 transition-colors'>
                        <span>" + faq.Question + @"</span>
                        <svg class='faq-icon w-5 h-5 transform transition-transform flex-shrink-0' fill='none' stroke='currentColor' viewBox='0 0 24 24'>
                            <path stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='M19 9l-7 7-7-7'/>
                        </svg>
                    </span>
                    <div class='faq-answer hidden px-5 pb-5 text-[#64748B]'>" + faq.Answer + @"</div>
                </div>";
            }

            if (string.IsNullOrEmpty(strProdFaqs))
                strProdFaqs = "<p class='text-gray-400 text-sm'>No FAQs available for this product.</p>";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery, "BuildFaqHtml", ex.Message);
        }
    }

    [System.Web.Services.WebMethod]
    public static List<ProductGallery> GetProductGallery(string id)
    {
        var list = new List<ProductGallery>();
        try
        {
            SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
            list = ProductGallery.GetAllProductGallery(con, id)
                                 .OrderBy(x => x.GalleryOrder)
                                 .ToList();
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery, "GetProductGallery", ex.Message);
        }
        return list;
    }
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static string SubmitEnquiry(string name, string city, string phone,
                                       string email, string message, string product)
    {
        try
        {
            SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            ProductEnquiry obj = new ProductEnquiry();
            obj.ProductName = product;
            obj.TypeofEnquiry = "Retail";
            obj.Name = name;
            obj.Mobile = phone;
            obj.Email = email;
            obj.SourcePage = "Product Page (Retail Enquiry)";
            obj.City = city;
            obj.Message = message;
            obj.AddedOn = DateTime.Now;
            obj.AddedIp = HttpContext.Current.Request.UserHostAddress;
            obj.Status = "Active";

            ProductEnquiry.InsertProductEnquiry(con, obj);

            // ✅ SEND EMAIL
            Emails.ProductWholesalepriceRequest(obj);

            return "Success";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery,
                "SubmitEnquiry",
                ex.Message
            );

            return "Error";
        }
    }
    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static string SubmitWholesaleEnquiry(string name, string city, string phone,
                                                string email, string message, string product)
    {
        try
        {
            SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

            ProductEnquiry obj = new ProductEnquiry();
            obj.ProductName = product;
            obj.TypeofEnquiry = "Wholesale";
            obj.Name = name;
            obj.Mobile = phone;
            obj.Email = email;
            obj.SourcePage = "Product Page (Wholesale Enquiry)";
            obj.City = city;
            obj.Message = message;
            obj.AddedOn = DateTime.Now;
            obj.AddedIp = HttpContext.Current.Request.UserHostAddress;
            obj.Status = "Active";

            ProductEnquiry.InsertProductEnquiry(con, obj);

            // ✅ SEND EMAIL
            Emails.ProductWholeENQRequest(obj);

            return "Success";
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery,
                "SubmitWholesaleEnquiry",
                ex.Message
            );

            return "Error";
        }
    }
}