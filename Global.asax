<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        RegisterProducts(RouteTable.Routes);
    }
    void RegisterProducts(RouteCollection routes)
    {
        routes.Clear();
        routes.Ignore("{resource}.axd/{*pathInfo}");
        routes.MapPageRoute("NewsDetails", "news/{url}", "~/news-detail.aspx");
        routes.MapPageRoute("Products", "products-categories/{caturl}", "~/products.aspx");
        routes.MapPageRoute("BlogList","blog","~/Blog.aspx");
        routes.MapPageRoute("BlogDetail", "blog/{BUrl}", "~/BlogDetail.aspx");
        routes.MapPageRoute("solutions", "solutions", "~/Listing.aspx");
        routes.MapPageRoute("SolutionsCategory", "solutions/{category}", "~/Listing.aspx");

        routes.MapPageRoute("404", "404", "~/404.aspx");
        routes.MapPageRoute("Category", "Category/{categoryurl}", "~/Category.aspx");
        routes.MapPageRoute("Product", "Product/{producturl}", "~/Product.aspx");
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
