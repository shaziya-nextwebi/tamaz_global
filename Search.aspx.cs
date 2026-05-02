using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string query = Request.QueryString["q"];

            if (!string.IsNullOrEmpty(query))
            {
                litTitle.Text = "<div class='search-title'>Search results for: <b>" + query + "</b></div>";
                LoadProducts(query);
            }
        }
    }

    private void LoadProducts(string query)
    {
        string cs = ConfigurationManager.ConnectionStrings["conT"].ConnectionString;

        using (SqlConnection con = new SqlConnection(cs))
        {
            string sql = @"SELECT Name, Image, Url 
                           FROM ProductDetails 
                           WHERE Name LIKE @q";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@q", "%" + query + "%");

            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                rptProducts.DataSource = dr;
                rptProducts.DataBind();
            }
            else
            {
                litNoResult.Text = "<div class='no-result'>No products found</div>";
            }
        }
    }
}