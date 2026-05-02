using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

public partial class Cart : System.Web.UI.Page
{
    SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCart();
        }
    }
    void BindCart()
    {
        List<AddtoCart> products = AddtoCart.GetAllcartproducts(conT);

        rptCart.DataSource = products;
        rptCart.DataBind();

        decimal total = 0;
        foreach (var item in products)
        {
            if (item.RetailPrice > 0)
                total += item.RetailPrice * item.Qty;
        }

        ViewState["Total"] = total;

        string subtotalHtml = total <= 0
            ? "<a href='/contact-us.aspx' style='font-weight:600;color:#333333;text-decoration:none;font-size:12px'>Price On Request</a>"
            : "<span class='text-rupee'>₹</span>" + total.ToString("N2");

        string totalHtml = total <= 0
            ? "<a href='/contact-us.aspx' style='font-weight:600;color:#000000;text-decoration:none; font-size:16px;'>Price On Request</a>"
            : "<span class='text-rupee'>₹</span>" + total.ToString("N2");

        lblSubtotal.Text = subtotalHtml;
        lblTotal.Text = totalHtml;
        //mobile
        Label1.Text = subtotalHtml;
        Label2.Text = totalHtml;

        if (products.Count == 0)
        {
            pnlEmpty.Visible = true;
            pnlCart.Visible = false;
        }
        else
        {
            pnlEmpty.Visible = false;
            pnlCart.Visible = true;
        }
    }
    protected void btnEnquiry_Click(object sender, EventArgs e)
    {
        try
        {
            List<AddtoCart> products = AddtoCart.GetAllcartproducts(conT);

            decimal grandTotal = 0;
            foreach (var item in products)
            {
                if (item.RetailPrice > 0)
                    grandTotal += item.RetailPrice * item.Qty;
            }

            string grandTotalDisplay = grandTotal > 0
                ? "&#8377;" + grandTotal.ToString("0.00")
                : "Price On Request";

            string cartdata = "";
            foreach (var item in products)
            {
                string priceDisplay = item.RetailPrice > 0
                    ? "&#8377;" + item.RetailPrice.ToString("0.00")
                    : "Price On Request";
                string subtotalDisplay = item.RetailPrice > 0
                    ? "&#8377;" + (item.RetailPrice * item.Qty).ToString("0.00")
                    : "Price On Request";

                cartdata += item.ProductName + " (" + priceDisplay + ") #Qty-" + item.Qty
                         + " = " + subtotalDisplay + " | ";
            }
            cartdata = cartdata.TrimEnd(' ', '|');

            string productRows = "";
            foreach (var item in products)
            {
                string priceDisplay = item.RetailPrice > 0
                    ? "&#8377;" + item.RetailPrice.ToString("0.00")
                    : "Price On Request";
                string subtotalDisplay = item.RetailPrice > 0
                    ? "&#8377;" + (item.RetailPrice * item.Qty).ToString("0.00")
                    : "Price On Request";

                string imgUrl = "";
                if (item.SmallImage != null)
                {
                    imgUrl = ConfigurationManager.AppSettings["domain"] + item.SmallImage;
                }

                productRows +=
                    "<tr style='border-bottom:1px solid #eee;'>" +
                        "<td style='padding:12px 8px;vertical-align:middle;'>" +
                            "<img src='" + imgUrl + "' width='70' height='70'" +
                            " style='object-fit:cover;border-radius:6px;border:1px solid #e5e7eb;display:block;'" +
                            " alt='" + item.ProductName + "' />" +
                        "</td>" +
                        "<td style='padding:12px 8px;vertical-align:middle;font-size:14px;color:#1e293b;font-weight:600;'>" +
                            item.ProductName +
                        "</td>" +
                        "<td style='padding:12px 8px;vertical-align:middle;font-size:14px;color:#475569;text-align:center;'>" +
                            priceDisplay +
                        "</td>" +
                        "<td style='padding:12px 8px;vertical-align:middle;font-size:14px;color:#475569;text-align:center;'>" +
                            item.Qty.ToString() +
                        "</td>" +
                        "<td style='padding:12px 8px;vertical-align:middle;font-size:14px;font-weight:700;color:#0f172a;text-align:right;'>" +
                            subtotalDisplay +
                        "</td>" +
                    "</tr>";
            }

            string productTable =
                "<table width='100%' cellpadding='0' cellspacing='0'" +
                " style='border:1px solid #e2e8f0;border-radius:8px;overflow:hidden;margin-bottom:24px;'>" +
                    "<thead>" +
                        "<tr style='background:#f8fafc;'>" +
                            "<th style='padding:10px 8px;font-size:12px;color:#64748b;font-weight:600;text-align:left;'>IMAGE</th>" +
                            "<th style='padding:10px 8px;font-size:12px;color:#64748b;font-weight:600;text-align:left;'>PRODUCT</th>" +
                            "<th style='padding:10px 8px;font-size:12px;color:#64748b;font-weight:600;text-align:center;'>PRICE</th>" +
                            "<th style='padding:10px 8px;font-size:12px;color:#64748b;font-weight:600;text-align:center;'>QTY</th>" +
                            "<th style='padding:10px 8px;font-size:12px;color:#64748b;font-weight:600;text-align:right;'>SUBTOTAL</th>" +
                        "</tr>" +
                    "</thead>" +
                    "<tbody>" +
                        productRows +
                    "</tbody>" +
                "</table>";

            string totalsTable =
                "<table width='100%' cellpadding='0' cellspacing='0'" +
                " style='margin-bottom:24px;border:1px solid #e2e8f0;border-radius:8px;overflow:hidden;'>" +
                    "<tr style='background:#f8fafc;'>" +
                        "<td style='padding:10px 16px;font-size:14px;color:#475569;'>Subtotal</td>" +
                        "<td style='padding:10px 16px;font-size:14px;font-weight:600;color:#0f172a;text-align:right;'>" + grandTotalDisplay + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td style='padding:10px 16px;font-size:14px;color:#475569;'>Shipping</td>" +
                        "<td style='padding:10px 16px;font-size:14px;font-weight:600;color:#0f172a;text-align:right;'>Free</td>" +
                    "</tr>" +
                    "<tr style='background:#eff6ff;'>" +
                        "<td style='padding:12px 16px;font-size:16px;font-weight:700;color:#0a1b50;'>Total</td>" +
                        "<td style='padding:12px 16px;font-size:16px;font-weight:700;color:#0a1b50;text-align:right;'>" + grandTotalDisplay + "</td>" +
                    "</tr>" +
                "</table>";

            string customerTable =
                "<table width='100%' cellpadding='0' cellspacing='0'" +
                " style='border:1px solid #e2e8f0;border-radius:8px;overflow:hidden;'>" +
                    "<tr style='background:#f8fafc;'>" +
                        "<td colspan='2' style='padding:10px 16px;font-size:13px;font-weight:700;color:#0a1b50;letter-spacing:0.5px;'>CUSTOMER DETAILS</td>" +
                    "</tr>" +
                    "<tr style='border-bottom:1px solid #f1f5f9;'>" +
                        "<td style='padding:10px 16px;font-size:13px;color:#64748b;width:120px;'>Name</td>" +
                        "<td style='padding:10px 16px;font-size:13px;color:#1e293b;font-weight:600;'>" + txtName.Text.Trim() + "</td>" +
                    "</tr>" +
                    "<tr style='border-bottom:1px solid #f1f5f9;background:#fafafa;'>" +
                        "<td style='padding:10px 16px;font-size:13px;color:#64748b;'>Email</td>" +
                        "<td style='padding:10px 16px;font-size:13px;color:#1e293b;font-weight:600;'>" + txtEmail.Text.Trim() + "</td>" +
                    "</tr>" +
                    "<tr style='border-bottom:1px solid #f1f5f9;'>" +
                        "<td style='padding:10px 16px;font-size:13px;color:#64748b;'>Phone</td>" +
                        "<td style='padding:10px 16px;font-size:13px;color:#1e293b;font-weight:600;'>" + txtPhone.Text.Trim() + "</td>" +
                    "</tr>" +
                    "<tr style='border-bottom:1px solid #f1f5f9;background:#fafafa;'>" +
                        "<td style='padding:10px 16px;font-size:13px;color:#64748b;'>City</td>" +
                        "<td style='padding:10px 16px;font-size:13px;color:#1e293b;font-weight:600;'>" + txtCity.Text.Trim() + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td style='padding:10px 16px;font-size:13px;color:#64748b;'>Message</td>" +
                        "<td style='padding:10px 16px;font-size:13px;color:#1e293b;'>" + txtMessage.Text.Trim() + "</td>" +
                    "</tr>" +
                "</table>";

            string emailHeader =
                "<table width='100%' cellpadding='0' cellspacing='0' style='background:#f1f5f9;padding:32px 0;'>" +
                "<tr><td align='center'>" +
                "<table width='620' cellpadding='0' cellspacing='0'" +
                " style='background:#ffffff;border-radius:12px;overflow:hidden;box-shadow:0 2px 12px rgba(0,0,0,0.08);'>" +
                    "<tr>" +
                        "<td style='background:#0a1b50;padding:28px 32px;text-align:center;'>" +
                            "<img src='" + ConfigurationManager.AppSettings["domain"] + "/assests/Images/logo.png'" +
                            " height='50' alt='TAMAZ Global' style='display:block;margin:0 auto 10px;' />";

            string emailFooter =
                        "<tr>" +
                            "<td style='background:#f8fafc;padding:20px 32px;text-align:center;border-top:1px solid #e2e8f0;'>" +
                                "<p style='margin:0 0 6px;font-size:13px;color:#94a3b8;'>" +
                                    "&copy; " + DateTime.Now.Year.ToString() + " TAMAZ Global Trading Co. All rights reserved." +
                                "</p>" +
                                "<a href='https://www.tamazglobal.com'" +
                                " style='font-size:13px;color:#0a1b50;font-weight:600;text-decoration:none;'>" +
                                    "www.tamazglobal.com" +
                                "</a>" +
                            "</td>" +
                        "</tr>" +
                    "</table>" +
                "</td></tr>" +
                "</table>";

            string adminBody =
                emailHeader +
                    "<h1 style='color:#ffffff;margin:0;font-size:20px;letter-spacing:0.5px;'>New Cart Enquiry</h1>" +
                "</td></tr>" +
                "<tr><td style='padding:28px 32px;'>" +
                    "<p style='margin:0 0 20px;font-size:15px;color:#334155;'>A new cart enquiry has been submitted. Details below:</p>" +
                    productTable +
                    totalsTable +
                    customerTable +
                "</td></tr>" +
                emailFooter;

            string userBody =
                emailHeader +
                    "<h1 style='color:#ffffff;margin:0;font-size:20px;letter-spacing:0.5px;'>Enquiry Confirmation</h1>" +
                "</td></tr>" +
                "<tr><td style='padding:28px 32px;'>" +
                    "<p style='margin:0 0 20px;font-size:15px;color:#334155;'>Dear <strong>" + txtName.Text.Trim() + "</strong>,<br/>" +
                    "Thank you for your enquiry! We have received your cart details and our team will get back to you within <strong>24 hours</strong>.</p>" +
                    productTable +
                    totalsTable +
                    customerTable +
                "</td></tr>" +
                emailFooter;

            CartlistEnq enq = new CartlistEnq
            {
                Name = txtName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Mobile = txtPhone.Text.Trim(),
                City = txtCity.Text.Trim(),
                Message = txtMessage.Text.Trim(),
                ProductName = cartdata,
                AddedIp = CommonModel.IPAddress(),
                AddedOn = TimeStamps.UTCTime(),
                Status = "Active",
            };

            int result = CartlistEnq.InsertcartlstEnquiry(conT, enq);

            if (result > 0)
            {
                // ── Send to Admin ──────────────────────────────────────────
                MailMessage mail = new MailMessage();
                mail.To.Add(ConfigurationManager.AppSettings["ToMail"]);
                string cc = ConfigurationManager.AppSettings["CCMail"];
                string bcc = ConfigurationManager.AppSettings["BCCMail"];
                if (!string.IsNullOrWhiteSpace(cc)) mail.CC.Add(cc);
                if (!string.IsNullOrWhiteSpace(bcc)) mail.Bcc.Add(bcc);
                mail.From = new MailAddress(
                    ConfigurationManager.AppSettings["from"],
                    ConfigurationManager.AppSettings["fromName"]);
                mail.Subject = "TAMAZ Global - New Cart Enquiry from " + enq.Name;
                mail.Body = adminBody;
                mail.IsBodyHtml = true;
                BuildSmtp().Send(mail);

                // ── Send to User ───────────────────────────────────────────
                if (!string.IsNullOrWhiteSpace(enq.Email))
                {
                    MailMessage userMail = new MailMessage();
                    userMail.To.Add(enq.Email);
                    userMail.From = new MailAddress(
                        ConfigurationManager.AppSettings["from"],
                        ConfigurationManager.AppSettings["fromName"]);
                    userMail.Subject = "Your Enquiry Received - TAMAZ Global";
                    userMail.Body = userBody;
                    userMail.IsBodyHtml = true;
                    BuildSmtp().Send(userMail);
                }


                AddtoCart.Deletecartlistafterenq(conT);
                txtName.Text = txtEmail.Text = txtPhone.Text =
                txtCity.Text = txtMessage.Text = "";
                Response.Redirect("thank-you.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err",
                    "Snackbar.show({pos:'top-right',text:'Oops! Something went wrong. Please try again.',actionTextColor:'#fff',backgroundColor:'#ea1c1c'});", true);
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery, "btnEnquiry_Click", ex.Message);
        }
    }
    string GetUID()
    {
        string uid = HttpContext.Current.Request.Cookies["t_new_vi"] != null
            ? HttpContext.Current.Request.Cookies["t_new_vi"].Value
            : "";

        if (!string.IsNullOrEmpty(uid))
            uid = CommonModel.Decrypt(uid);

        return uid;
    }
    protected void rptCart_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string pid = e.CommandArgument.ToString();

        var cartItem = AddtoCart.GetUserArpcartByUid(conT, GetUID(), pid).FirstOrDefault();

        if (cartItem != null)
        {
            if (e.CommandName == "IncQty")
                cartItem.Qty += 1;

            if (e.CommandName == "DecQty" && cartItem.Qty > 1)
                cartItem.Qty -= 1;

            AddtoCart.Updatecartdetails(conT, cartItem);
        }

        if (e.CommandName == "RemoveItem")
            AddtoCart.Deletecartlist(conT, pid);

        BindCart();

        // ✅ Force cart count span to reflect updated count immediately
        string newCount = AddtoCart.GetcartlistQunatity(conT);
        var masterPage = this.Master as MasterPage;
        if (masterPage != null)
            masterPage.strCartCount = newCount;
    }
    protected string FormatCartPrice(object retailPriceObj)
    {
        decimal price = 0;
        string val = retailPriceObj != null ? retailPriceObj.ToString() : "";
        decimal.TryParse(val, out price);
        if (price <= 0)
            return "<span style='font-weight:600; color:#000;'>-</span>";
        return "<span style='font-weight:600; color:#000;'> " + "<span class='text-rupee'>₹</span>" + price.ToString("N2") + "</span>";
    }
    protected string FormatSubtotal(object retailPriceObj, object qtyObj)
    {
        decimal price = 0;
        int qty = 0;
        string val = retailPriceObj != null ? retailPriceObj.ToString() : "";
        decimal.TryParse(val, out price);
        int.TryParse(qtyObj != null ? qtyObj.ToString() : "0", out qty);

        if (price <= 0)
            return "<span style='font-weight:600; color:#000;'><a href='/contact-us.aspx' style='color:#000;text-decoration:none;'>Price On Request</a></span>";


        return "<span class='text-rupee'>₹</span>" + (price * qty).ToString("N2");
    }

    [System.Web.Services.WebMethod]
    public static object UpdateQty(int productId, string action)
    {
        SqlConnection conT = new SqlConnection(ConfigurationManager.ConnectionStrings["conT"].ConnectionString);

        string uid = HttpContext.Current.Request.Cookies["t_new_vi"] != null
            ? CommonModel.Decrypt(HttpContext.Current.Request.Cookies["t_new_vi"].Value)
            : "";

        var cartItem = AddtoCart.GetUserArpcartByUid(conT, uid, productId.ToString()).FirstOrDefault();

        int qty = 1;

        if (cartItem != null)
        {
            if (action == "inc") cartItem.Qty++;
            if (action == "dec" && cartItem.Qty > 1) cartItem.Qty--;

            AddtoCart.Updatecartdetails(conT, cartItem);
            qty = cartItem.Qty;
        }

        List<AddtoCart> products = AddtoCart.GetAllcartproducts(conT);

        decimal total = 0;
        decimal itemSubtotal = 0;
        bool itemIsPriceOnRequest = false; // ← NEW

        foreach (var item in products)
        {
            if (item.RetailPrice > 0)
            {
                decimal lineTotal = item.RetailPrice * item.Qty;
                total += lineTotal;

                if (item.ProductId == productId)
                    itemSubtotal = lineTotal;
            }
            else if (item.ProductId == productId)
            {
                itemIsPriceOnRequest = true; // ← NEW
            }
        }

        return new
        {
            success = true,
            qty = qty,
            subtotal = total.ToString("0.00"),
            total = total.ToString("0.00"),
            itemSubtotal = itemSubtotal.ToString("0.00"),
            itemIsPriceOnRequest = itemIsPriceOnRequest // ← NEW
        };
    }
    SmtpClient BuildSmtp()
    {
        return new SmtpClient
        {
            Host = ConfigurationManager.AppSettings["host"],
            Port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]),
            EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"]),
            Credentials = new System.Net.NetworkCredential(
                ConfigurationManager.AppSettings["userName"],
                ConfigurationManager.AppSettings["password"])
        };
    }
}