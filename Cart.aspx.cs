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
        lblSubtotal.Text = "₹ " + total.ToString("0.00");
        lblTotal.Text = "₹ " + total.ToString("0.00");
        // Price not in AddtoCart model — subtotal shown per item via repeater
        // Total will be calculated client-side via JS or you can join Price in GetAllcartproducts
        //lblSubtotal.Text = "&#8377; " + total.ToString("0.00");
        //lblTotal.Text = "&#8377; " + total.ToString("0.00");

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
            var loop = "";
            var cartdata = "";

            List<AddtoCart> products = AddtoCart.GetAllcartproducts(conT);

            foreach (var item in products)
            {
                loop += "<tr><td>Product</td><td>:</td><td>" + item.ProductName + "</td></tr>" +
                            "<tr><td>Quantity</td><td>:</td><td>" + item.Qty + "</td></tr>";
                cartdata += item.ProductName +
                      " (₹" + item.RetailPrice + ") #Qty-" + item.Qty +
                      " = ₹" + (item.RetailPrice * item.Qty) + " | ";
            }

            cartdata = cartdata.TrimEnd(' ', '|');

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
                // ── Email to Admin ──────────────────────────────────────
                MailMessage mail = new MailMessage();
                mail.To.Add(ConfigurationManager.AppSettings["ToMail"]);

                string cc = ConfigurationManager.AppSettings["CCMail"];
                string bcc = ConfigurationManager.AppSettings["BCCMail"];
                if (!string.IsNullOrWhiteSpace(cc)) mail.CC.Add(cc);
                if (!string.IsNullOrWhiteSpace(bcc)) mail.Bcc.Add(bcc);

                mail.From = new MailAddress(
                    ConfigurationManager.AppSettings["from"],
                    ConfigurationManager.AppSettings["fromName"]);
                mail.Subject = "TAMAZ Global - Cart Enquiry Request";
                mail.Body =
                    "<table width='100%' border='0' cellspacing='3' cellpadding='3' bgcolor='#F9F9F9'>" +
                    "<tr><td colspan='3' align='center' bgcolor='#CCCCCC'><strong>Cart Enquiry Request</strong></td></tr>" +
                    loop +
                    "<tr><td>Name</td><td>:</td><td>" + enq.Name + "</td></tr>" +
                    "<tr><td>Email</td><td>:</td><td>" + enq.Email + "</td></tr>" +
                    "<tr><td>Phone</td><td>:</td><td>" + enq.Mobile + "</td></tr>" +
                    "<tr><td>City</td><td>:</td><td>" + enq.City + "</td></tr>" +
                    "<tr><td>Message</td><td>:</td><td>" + enq.Message + "</td></tr>" +
                    "</table><br/>Thanks,<br/>Team TamazGlobal<br/>https://www.tamazglobal.com/";
                mail.IsBodyHtml = true;
                BuildSmtp().Send(mail);

                // ── Email to User ───────────────────────────────────────
                if (!string.IsNullOrWhiteSpace(enq.Email))
                {
                    MailMessage userMail = new MailMessage();
                    userMail.To.Add(enq.Email);
                    userMail.From = new MailAddress(
                        ConfigurationManager.AppSettings["from"],
                        ConfigurationManager.AppSettings["fromName"]);
                    userMail.Subject = "Thank You for Your Enquiry - TAMAZ Global";
                    userMail.Body = "Dear " + enq.Name + ",<br><br>Thank you for your enquiry. Our team will get back to you within 24 hours.<br><br>Regards,<br>Team TAMAZ Global<br>https://www.tamazglobal.com/";
                    userMail.IsBodyHtml = true;
                    BuildSmtp().Send(userMail);
                }

                // ── Clear cart after enquiry ────────────────────────────
                AddtoCart.Deletecartlistafterenq(conT);

                // ── Clear form ──────────────────────────────────────────
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
    //protected void rptCart_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    if (e.CommandName == "RemoveItem")
    //    {
    //        string pid = e.CommandArgument.ToString();
    //        AddtoCart.Deletecartlist(conT, pid);
    //        BindCart();
    //    }
    //}
    protected string FormatCartPrice(object retailPriceObj)
    {
        decimal price = 0;
        string val = retailPriceObj != null ? retailPriceObj.ToString() : "";
        decimal.TryParse(val, out price);
        if (price <= 0)
            return "<span style='font-weight:600; color:#64748B;'>-</span>";
        return "<span style='font-weight:600; color:#0F172A;'>&#8377; " + price.ToString("0.00") + "</span>";
    }
    protected string FormatSubtotal(object retailPriceObj, object qtyObj)
    {
        decimal price = 0;
        int qty = 0;
        string val = retailPriceObj != null ? retailPriceObj.ToString() : "";
        decimal.TryParse(val, out price);
        int.TryParse(qtyObj != null ? qtyObj.ToString() : "0", out qty);

        if (price <= 0)
            return "<span style='font-weight:600; color:#64748B;'>-</span>";


        return "&#8377; " + (price * qty).ToString("0.00");
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