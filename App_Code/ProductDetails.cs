using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProductDetails
/// </summary>
public class ProductDetails
{
    public ProductDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int Id { get; set; }
    public string ProductId { get; set; }
    public string ProductGuid { get; set; }
    public string Category { get; set; }
    public string CategoryUrl { get; set; }
    public string BrandUrl { get; set; }
    public string Brand { get; set; }
    public string DeliveredBy { get; set; }
    public string RetailPrice { get; set; }
    public string ProductName { get; set; }
    public string ProductUrl { get; set; }
    public string PlaceOfOrigin { get; set; }
    public string KeyIngredient { get; set; }
    public string ProductShortDesc { get; set; }
    public string ProductAvailability { get; set; }
    public string ProductLabel { get; set; }
    public string InStock { get; set; }
    public int RowNumber { get; set; }
    public string DisplayHome { get; set; }
    public string AlternativeProduct { get; set; }
    public string BrandOrder { get; set; }
    public string CategoryOrder { get; set; }
    public string BenefitsDesc { get; set; }
    public string IngredientsDesc { get; set; }
    public string UsageDesc { get; set; }
    public string CategoryId { get; set; }
    public string BrandId { get; set; }
    public string FullDesc { get; set; }
    public string SmallImage { get; set; }
    public string PageTitle { get; set; }
    public string MetaKeys { get; set; }
    public string MetaDesc { get; set; }
    public string AddedBy { get; set; }
    public DateTime AddedOn { get; set; }
    public string AddedIp { get; set; }
    public string Status { get; set; }
    public int ttlProduct { get; set; }
    public string ProductLabelName { get; set; }
    public static List<ProductDetails> GetAllProducts(SqlConnection conT)
    {
        List<ProductDetails> productDetails = new List<ProductDetails>();
        try
        {
            string query = "Select *,(Select Top 1 UserName From CreateUser Where UserGuid=ProductDetails.AddedBy) as AddedBy1,(Select Top 1 ProductLabel from ProductLabelMaster Where ProductLabelMaster.Id=ProductDetails.ProductLabel) as ProductLabelName from ProductDetails where Status!='Deleted' Order by Id desc";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                productDetails = (from DataRow dr in dt.Rows
                                  select new ProductDetails()
                                  {
                                      Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                      ProductId = Convert.ToString(dr["ProductId"]),
                                      ProductGuid = Convert.ToString(dr["ProductGuid"]),
                                      Category = Convert.ToString(dr["Category"]),
                                      Brand = Convert.ToString(dr["Brand"]),
                                      DeliveredBy = Convert.ToString(dr["DeliveredBy"]),
                                      RetailPrice = Convert.ToString(dr["RetailPrice"]),
                                      ProductAvailability = Convert.ToString(dr["ProductAvailability"]),
                                      ProductLabelName = Convert.ToString(dr["ProductLabelName"]),
                                      ProductLabel = Convert.ToString(dr["ProductLabel"]),
                                      ProductName = Convert.ToString(dr["ProductName"]),
                                      BenefitsDesc = Convert.ToString(dr["BenefitsDesc"]),
                                      IngredientsDesc = Convert.ToString(dr["IngredientsDesc"]),
                                      UsageDesc = Convert.ToString(dr["UsageDesc"]),
                                      ProductUrl = Convert.ToString(dr["ProductUrl"]),
                                      PlaceOfOrigin = Convert.ToString(dr["PlaceOfOrigin"]),
                                      KeyIngredient = Convert.ToString(dr["KeyIngredient"]),
                                      ProductShortDesc = Convert.ToString(dr["ProductShortDesc"]),
                                      InStock = Convert.ToString(dr["InStock"]),
                                      DisplayHome = Convert.ToString(dr["DisplayHome"]),
                                      AlternativeProduct = Convert.ToString(dr["AlternativeProduct"]),
                                      BrandOrder = Convert.ToString(dr["BrandOrder"]),
                                      CategoryOrder = Convert.ToString(dr["CategoryOrder"]) == "" ? "1000" : Convert.ToString(dr["CategoryOrder"]),
                                      BrandId = Convert.ToString(dr["BrandId"]),
                                      CategoryId = Convert.ToString(dr["CategoryId"]),
                                      FullDesc = Convert.ToString(dr["FullDesc"]),
                                      SmallImage = Convert.ToString(dr["SmallImage"]),
                                      AddedBy = Convert.ToString(dr["AddedBy1"]),
                                      AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                      AddedIp = Convert.ToString(dr["AddedIp"]),
                                      Status = Convert.ToString(dr["Status"]),
                                      PageTitle = Convert.ToString(dr["PageTitle"]),
                                      MetaKeys = Convert.ToString(dr["MetaKeys"]),
                                      MetaDesc = Convert.ToString(dr["MetaDesc"])
                                  }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductDetails", ex.Message);
        }
        finally
        {
            conT.Close();
        }
        return productDetails;
    }

    public static List<ProductDetails> GetAllProductsd(SqlConnection conT)
    {
        List<ProductDetails> Detail = new List<ProductDetails>();
        try
        {
            string query = "SELECT ROW_NUMBER() OVER (ORDER BY RowNo desc) RowNo,* FROM ProductDetails where Status!='Deleted'";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Detail = (from DataRow dr in dt.Rows
                          select new ProductDetails()
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              ProductId = Convert.ToString(dr["ProductId"]),
                              ProductGuid = Convert.ToString(dr["ProductGuid"]),
                              Category = Convert.ToString(dr["Category"]),
                              Brand = Convert.ToString(dr["Brand"]),
                              RowNumber = Convert.ToInt32(Convert.ToString(dr["RowNo"])),
                              DeliveredBy = Convert.ToString(dr["DeliveredBy"]),
                              RetailPrice = Convert.ToString(dr["RetailPrice"]),
                              ProductAvailability = Convert.ToString(dr["ProductAvailability"]),
                              ProductLabel = Convert.ToString(dr["ProductLabel"]),
                              ProductName = Convert.ToString(dr["ProductName"]),
                              BenefitsDesc = Convert.ToString(dr["BenefitsDesc"]),
                              IngredientsDesc = Convert.ToString(dr["IngredientsDesc"]),
                              UsageDesc = Convert.ToString(dr["UsageDesc"]),
                              ProductUrl = Convert.ToString(dr["ProductUrl"]),
                              PlaceOfOrigin = Convert.ToString(dr["PlaceOfOrigin"]),
                              KeyIngredient = Convert.ToString(dr["KeyIngredient"]),
                              ProductShortDesc = Convert.ToString(dr["ProductShortDesc"]),
                              InStock = Convert.ToString(dr["InStock"]),
                              DisplayHome = Convert.ToString(dr["DisplayHome"]),
                              AlternativeProduct = Convert.ToString(dr["AlternativeProduct"]),
                              BrandOrder = Convert.ToString(dr["BrandOrder"]),
                              CategoryOrder = Convert.ToString(dr["CategoryOrder"]) == "" ? "1000" : Convert.ToString(dr["CategoryOrder"]),
                              BrandId = Convert.ToString(dr["BrandId"]),
                              CategoryId = Convert.ToString(dr["CategoryId"]),
                              FullDesc = Convert.ToString(dr["FullDesc"]),
                              SmallImage = Convert.ToString(dr["SmallImage"]),
                              // AddedBy = Convert.ToString(dr["AddedBy1"]),
                              AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                              AddedIp = Convert.ToString(dr["AddedIp"]),
                              Status = Convert.ToString(dr["Status"]),
                              PageTitle = Convert.ToString(dr["PageTitle"]),
                              MetaKeys = Convert.ToString(dr["MetaKeys"]),
                              MetaDesc = Convert.ToString(dr["MetaDesc"])
                          }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductsd", ex.Message);
        }
        return Detail;
    }
    public static List<ProductDetails> GetAllProductsBasedOnCategory(SqlConnection conT, string catName, int pNo)
    {

        List<ProductDetails> Detail = new List<ProductDetails>();
        try
        {
            string query = @"SELECT TOP 20 * 
FROM (
    SELECT 
        ROW_NUMBER() OVER (ORDER BY TRY_CONVERT(INT, CategoryOrder)) AS RowNo,
        pd.*,
        c.CategoryUrl,
        (
            SELECT COUNT(pd_inner.Id) 
            FROM ProductDetails AS pd_inner
            INNER JOIN Category AS c_inner ON c_inner.CategoryName = pd_inner.Category
            WHERE pd_inner.Status != 'Deleted' 
              AND c_inner.CategoryUrl = @catName 
              AND c_inner.Status != 'Deleted'
        ) AS ttlProduct,
        (
            SELECT ProductLabel 
            FROM ProductLabelMaster 
            WHERE TRY_CONVERT(NVARCHAR, pd.ProductLabel) = TRY_CONVERT(NVARCHAR, Id)
        ) AS ProductLabelName
    FROM ProductDetails AS pd
    INNER JOIN Category AS c ON c.CategoryName = pd.Category
    WHERE pd.Status != 'Deleted' 
      AND c.Status != 'Deleted' 
      AND c.CategoryUrl = @catName
) AS tbl
WHERE tbl.RowNo >  ((TRY_Convert(int,@Pno) - 1) * 20)";
            // and (SELECT ROW_NUMBER() OVER (ORDER BY try_convert(int, CategoryOrder) desc)) > " + ((pNo-1)*15);
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@catname", SqlDbType.NVarChar).Value = catName;
                cmd.Parameters.AddWithValue("@PNo", SqlDbType.Int).Value = pNo;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Detail = (from DataRow dr in dt.Rows
                          select new ProductDetails()
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              ProductId = Convert.ToString(dr["ProductId"]),
                              ProductGuid = Convert.ToString(dr["ProductGuid"]),
                              Category = Convert.ToString(dr["Category"]),
                              Brand = Convert.ToString(dr["Brand"]),
                              RowNumber = Convert.ToInt32(Convert.ToString(dr["RowNo"])),
                              ttlProduct = Convert.ToInt32(Convert.ToString(dr["ttlProduct"])),
                              DeliveredBy = Convert.ToString(dr["DeliveredBy"]),
                              RetailPrice = Convert.ToString(dr["RetailPrice"]),
                              ProductAvailability = Convert.ToString(dr["ProductAvailability"]),
                              ProductLabel = Convert.ToString(dr["ProductLabel"]),
                              ProductLabelName = Convert.ToString(dr["ProductLabelName"]),
                              ProductName = Convert.ToString(dr["ProductName"]),
                              BenefitsDesc = Convert.ToString(dr["BenefitsDesc"]),
                              IngredientsDesc = Convert.ToString(dr["IngredientsDesc"]),
                              UsageDesc = Convert.ToString(dr["UsageDesc"]),
                              ProductUrl = Convert.ToString(dr["ProductUrl"]),
                              PlaceOfOrigin = Convert.ToString(dr["PlaceOfOrigin"]),
                              KeyIngredient = Convert.ToString(dr["KeyIngredient"]),
                              ProductShortDesc = Convert.ToString(dr["ProductShortDesc"]),
                              InStock = Convert.ToString(dr["InStock"]),
                              DisplayHome = Convert.ToString(dr["DisplayHome"]),
                              AlternativeProduct = Convert.ToString(dr["AlternativeProduct"]),
                              BrandOrder = Convert.ToString(dr["BrandOrder"]),
                              CategoryOrder = Convert.ToString(dr["CategoryOrder"]) == "" ? "1000" : Convert.ToString(dr["CategoryOrder"]),
                              BrandId = Convert.ToString(dr["BrandId"]),
                              CategoryId = Convert.ToString(dr["CategoryId"]),
                              FullDesc = Convert.ToString(dr["FullDesc"]),
                              SmallImage = Convert.ToString(dr["SmallImage"]),
                              CategoryUrl = Convert.ToString(dr["CategoryUrl"]),
                              AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                              AddedIp = Convert.ToString(dr["AddedIp"]),
                              Status = Convert.ToString(dr["Status"]),
                              PageTitle = Convert.ToString(dr["PageTitle"]),
                              MetaKeys = Convert.ToString(dr["MetaKeys"]),
                              MetaDesc = Convert.ToString(dr["MetaDesc"])
                          }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductsBasedOnCategory", ex.Message);
        }
        return Detail;
    }

    public static List<ProductDetails> GetAllProductstop5(SqlConnection conT)
    {
        List<ProductDetails> productDetails = new List<ProductDetails>();
        try
        {
            string query = "Select Top 5 * from ProductDetails where Status!='Deleted' and DisplayHome='Yes' Order by Id desc";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                productDetails = (from DataRow dr in dt.Rows
                                  select new ProductDetails()
                                  {
                                      Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                      ProductId = Convert.ToString(dr["ProductId"]),
                                      ProductGuid = Convert.ToString(dr["ProductGuid"]),
                                      Category = Convert.ToString(dr["Category"]),
                                      Brand = Convert.ToString(dr["Brand"]),
                                      DeliveredBy = Convert.ToString(dr["DeliveredBy"]),
                                      RetailPrice = Convert.ToString(dr["RetailPrice"]),
                                      ProductAvailability = Convert.ToString(dr["ProductAvailability"]),
                                      ProductLabel = Convert.ToString(dr["ProductLabel"]),
                                      ProductName = Convert.ToString(dr["ProductName"]),
                                      BenefitsDesc = Convert.ToString(dr["BenefitsDesc"]),
                                      IngredientsDesc = Convert.ToString(dr["IngredientsDesc"]),
                                      UsageDesc = Convert.ToString(dr["UsageDesc"]),
                                      ProductUrl = Convert.ToString(dr["ProductUrl"]),
                                      PlaceOfOrigin = Convert.ToString(dr["PlaceOfOrigin"]),
                                      KeyIngredient = Convert.ToString(dr["KeyIngredient"]),
                                      ProductShortDesc = Convert.ToString(dr["ProductShortDesc"]),
                                      InStock = Convert.ToString(dr["InStock"]),
                                      DisplayHome = Convert.ToString(dr["DisplayHome"]),
                                      AlternativeProduct = Convert.ToString(dr["AlternativeProduct"]),
                                      BrandOrder = Convert.ToString(dr["BrandOrder"]),
                                      CategoryOrder = Convert.ToString(dr["CategoryOrder"]) == "" ? "1000" : Convert.ToString(dr["CategoryOrder"]),
                                      BrandId = Convert.ToString(dr["BrandId"]),
                                      CategoryId = Convert.ToString(dr["CategoryId"]),
                                      FullDesc = Convert.ToString(dr["FullDesc"]),
                                      SmallImage = Convert.ToString(dr["SmallImage"]),
                                      //AddedBy = Convert.ToString(dr["AddedBy1"]),
                                      AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                      AddedIp = Convert.ToString(dr["AddedIp"]),
                                      Status = Convert.ToString(dr["Status"]),
                                      PageTitle = Convert.ToString(dr["PageTitle"]),
                                      MetaKeys = Convert.ToString(dr["MetaKeys"]),
                                      MetaDesc = Convert.ToString(dr["MetaDesc"])
                                  }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductstop5", ex.Message);
        }
        finally
        {
            conT.Close();
        }
        return productDetails;
    }

    public static List<ProductDetails> GetAllProductsBasedOnBrand(SqlConnection conT, string brandurl, int pNo)
    {
        List<ProductDetails> Detail = new List<ProductDetails>();
        try
        {
            string query = @"Select top 20 * from (SELECT ROW_NUMBER() OVER (ORDER BY try_convert(int, BrandOrder)) RowNo,pd.*,c.BrandUrl,
(Select Count(pd.Id) as pp FROM ProductDetails as pd inner join Brand as c on c.BrandName = pd.Brand where pd.Status!='Deleted'  and c.BrandUrl=@brandurl and c.Status != 'Deleted') as ttlProduct
FROM ProductDetails as pd inner join Brand as c on c.BrandName = pd.Brand where pd.Status!='Deleted'  and c.Status != 'Deleted'  and c.BrandUrl=@brandurl
 ) as tbl Where tbl.RowNo >  ((TRY_Convert(int,@Pno) - 1) * 20)";

            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.Parameters.AddWithValue("@brandurl", SqlDbType.NVarChar).Value = brandurl;
                cmd.Parameters.AddWithValue("@Pno", SqlDbType.Int).Value = pNo;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Detail = (from DataRow dr in dt.Rows
                          select new ProductDetails()
                          {
                              Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                              ProductId = Convert.ToString(dr["ProductId"]),
                              ProductGuid = Convert.ToString(dr["ProductGuid"]),
                              Category = Convert.ToString(dr["Category"]),
                              Brand = Convert.ToString(dr["Brand"]),
                              RowNumber = Convert.ToInt32(Convert.ToString(dr["RowNo"])),
                              ttlProduct = Convert.ToInt32(Convert.ToString(dr["ttlProduct"])),
                              DeliveredBy = Convert.ToString(dr["DeliveredBy"]),
                              RetailPrice = Convert.ToString(dr["RetailPrice"]),
                              ProductAvailability = Convert.ToString(dr["ProductAvailability"]),
                              ProductLabel = Convert.ToString(dr["ProductLabel"]),
                              ProductName = Convert.ToString(dr["ProductName"]),
                              BenefitsDesc = Convert.ToString(dr["BenefitsDesc"]),
                              IngredientsDesc = Convert.ToString(dr["IngredientsDesc"]),
                              UsageDesc = Convert.ToString(dr["UsageDesc"]),
                              ProductUrl = Convert.ToString(dr["ProductUrl"]),
                              PlaceOfOrigin = Convert.ToString(dr["PlaceOfOrigin"]),
                              KeyIngredient = Convert.ToString(dr["KeyIngredient"]),
                              ProductShortDesc = Convert.ToString(dr["ProductShortDesc"]),
                              InStock = Convert.ToString(dr["InStock"]),
                              DisplayHome = Convert.ToString(dr["DisplayHome"]),
                              AlternativeProduct = Convert.ToString(dr["AlternativeProduct"]),
                              BrandOrder = Convert.ToString(dr["BrandOrder"]),
                              CategoryOrder = Convert.ToString(dr["CategoryOrder"]) == "" ? "1000" : Convert.ToString(dr["CategoryOrder"]),
                              BrandId = Convert.ToString(dr["BrandId"]),
                              CategoryId = Convert.ToString(dr["CategoryId"]),
                              FullDesc = Convert.ToString(dr["FullDesc"]),
                              SmallImage = Convert.ToString(dr["SmallImage"]),
                              BrandUrl = Convert.ToString(dr["BrandUrl"]),
                              AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                              AddedIp = Convert.ToString(dr["AddedIp"]),
                              Status = Convert.ToString(dr["Status"]),
                              PageTitle = Convert.ToString(dr["PageTitle"]),
                              MetaKeys = Convert.ToString(dr["MetaKeys"]),
                              MetaDesc = Convert.ToString(dr["MetaDesc"])
                          }).ToList();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetAllProductsBasedOnBrand", ex.Message);
        }
        return Detail;
    }


    public static int PublishOrUnPublishProduct(SqlConnection conT, ProductDetails course)
    {
        int result = 0;

        try
        {
            string query = "Update ProductDetails Set Status=@Status, AddedOn=@AddedOn, AddedIp=@AddedIp,AddedBy=@AddedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = course.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = course.Status;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = course.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = course.AddedIp;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = course.AddedBy;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "PublishOrUnPublishCourse", ex.Message);
        }
        finally
        {
            conT.Close();
        }
        return result;
    }

    public static List<ProductDetails> GetProductDetails(SqlConnection conT, int id)
    {
        List<ProductDetails> Packagedetails = new List<ProductDetails>();
        try
        {
            string query = "Select * from ProductDetails Where Status !='Deleted' and Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = id;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                Packagedetails = (from DataRow dr in dt.Rows
                                  select new ProductDetails()
                                  {
                                      Id = Convert.ToInt32(Convert.ToString(dr["Id"])),
                                      ProductId = Convert.ToString(dr["ProductId"]),
                                      ProductGuid = Convert.ToString(dr["ProductGuid"]),
                                      Category = Convert.ToString(dr["Category"]),
                                      Brand = Convert.ToString(dr["Brand"]),
                                      //  RowNumber = Convert.ToInt32(Convert.ToString(dr["RowNo"])),
                                      DeliveredBy = Convert.ToString(dr["DeliveredBy"]),
                                      RetailPrice = Convert.ToString(dr["RetailPrice"]),
                                      ProductAvailability = Convert.ToString(dr["ProductAvailability"]),
                                      ProductLabel = Convert.ToString(dr["ProductLabel"]),
                                      ProductName = Convert.ToString(dr["ProductName"]),
                                      BenefitsDesc = Convert.ToString(dr["BenefitsDesc"]),
                                      IngredientsDesc = Convert.ToString(dr["IngredientsDesc"]),
                                      UsageDesc = Convert.ToString(dr["UsageDesc"]),
                                      ProductUrl = Convert.ToString(dr["ProductUrl"]),
                                      PlaceOfOrigin = Convert.ToString(dr["PlaceOfOrigin"]),
                                      KeyIngredient = Convert.ToString(dr["KeyIngredient"]),
                                      ProductShortDesc = Convert.ToString(dr["ProductShortDesc"]),
                                      InStock = Convert.ToString(dr["InStock"]),
                                      DisplayHome = Convert.ToString(dr["DisplayHome"]),
                                      AlternativeProduct = Convert.ToString(dr["AlternativeProduct"]),
                                      BrandOrder = Convert.ToString(dr["BrandOrder"]),
                                      CategoryOrder = Convert.ToString(dr["CategoryOrder"]) == "" ? "1000" : Convert.ToString(dr["CategoryOrder"]),
                                      BrandId = Convert.ToString(dr["BrandId"]),
                                      CategoryId = Convert.ToString(dr["CategoryId"]),
                                      FullDesc = Convert.ToString(dr["FullDesc"]),
                                      SmallImage = Convert.ToString(dr["SmallImage"]),
                                      // AddedBy = Convert.ToString(dr["AddedBy1"]),
                                      AddedOn = Convert.ToDateTime(Convert.ToString(dr["AddedOn"])),
                                      AddedIp = Convert.ToString(dr["AddedIp"]),
                                      Status = Convert.ToString(dr["Status"]),
                                      PageTitle = Convert.ToString(dr["PageTitle"]),
                                      MetaKeys = Convert.ToString(dr["MetaKeys"]),
                                      MetaDesc = Convert.ToString(dr["MetaDesc"])
                                  }).ToList();

            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetProductDetails", ex.Message);
        }
        return Packagedetails;
    }
    public static int InsertProductDetails(SqlConnection conT, ProductDetails cat)
    {
        int result = 0;
        try
        {
            string query = "Insert Into ProductDetails (ProductAvailability,ProductLabel,BenefitsDesc,IngredientsDesc,UsageDesc,ProductId,ProductGuid,Category,Brand,DeliveredBy,RetailPrice,ProductName,ProductUrl,PlaceOfOrigin,KeyIngredient,ProductShortDesc,InStock,DisplayHome,AlternativeProduct,BrandOrder,CategoryOrder,CategoryId,BrandId,FullDesc,SmallImage,AddedBy,AddedOn,AddedIp,Status) values(@ProductAvailability,@ProductLabel,@BenefitsDesc,@IngredientsDesc,@UsageDesc,@ProductId,@ProductGuid,@Category,@Brand,@DeliveredBy,@RetailPrice,@ProductName,@ProductUrl,@PlaceOfOrigin,@KeyIngredient,@ProductShortDesc,@InStock,@DisplayHome,@AlternativeProduct,@BrandOrder,@CategoryOrder,@CategoryId,@BrandId,@FullDesc,@SmallImage,@AddedBy,@AddedOn,@AddedIp,@Status)";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@ProductId", SqlDbType.VarChar).Value = cat.ProductId;
                cmd.Parameters.AddWithValue("@ProductGuid", SqlDbType.NVarChar).Value = cat.ProductGuid;
                cmd.Parameters.AddWithValue("@Category", SqlDbType.NVarChar).Value = cat.Category;
                cmd.Parameters.AddWithValue("@Brand", SqlDbType.NVarChar).Value = cat.Brand;
                cmd.Parameters.AddWithValue("@DeliveredBy", SqlDbType.NVarChar).Value = cat.DeliveredBy;
                cmd.Parameters.AddWithValue("@RetailPrice", SqlDbType.NVarChar).Value = cat.RetailPrice;
                cmd.Parameters.AddWithValue("@ProductName", SqlDbType.NVarChar).Value = cat.ProductName;
                cmd.Parameters.AddWithValue("@ProductAvailability", SqlDbType.NVarChar).Value = cat.ProductAvailability;
                cmd.Parameters.AddWithValue("@ProductLabel", SqlDbType.NVarChar).Value = cat.ProductLabel;
                cmd.Parameters.AddWithValue("@BenefitsDesc", SqlDbType.NVarChar).Value = cat.BenefitsDesc;
                cmd.Parameters.AddWithValue("@IngredientsDesc", SqlDbType.NVarChar).Value = cat.IngredientsDesc;
                cmd.Parameters.AddWithValue("@UsageDesc", SqlDbType.NVarChar).Value = cat.UsageDesc;
                cmd.Parameters.AddWithValue("@ProductUrl", SqlDbType.NVarChar).Value = cat.ProductUrl;
                cmd.Parameters.AddWithValue("@PlaceOfOrigin", SqlDbType.NVarChar).Value = cat.PlaceOfOrigin;
                cmd.Parameters.AddWithValue("@KeyIngredient", SqlDbType.NVarChar).Value = cat.KeyIngredient;
                cmd.Parameters.AddWithValue("@ProductShortDesc", SqlDbType.NVarChar).Value = cat.ProductShortDesc;
                cmd.Parameters.AddWithValue("@InStock", SqlDbType.NVarChar).Value = cat.InStock;
                cmd.Parameters.AddWithValue("@DisplayHome", SqlDbType.NVarChar).Value = cat.DisplayHome;
                cmd.Parameters.AddWithValue("@AlternativeProduct", SqlDbType.NVarChar).Value = cat.AlternativeProduct;
                cmd.Parameters.AddWithValue("@BrandOrder", SqlDbType.NVarChar).Value = cat.BrandOrder;
                cmd.Parameters.AddWithValue("@CategoryOrder", SqlDbType.NVarChar).Value = cat.CategoryOrder;
                cmd.Parameters.AddWithValue("@CategoryId", SqlDbType.NVarChar).Value = cat.CategoryId;
                cmd.Parameters.AddWithValue("@BrandId", SqlDbType.NVarChar).Value = cat.BrandId;
                cmd.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = cat.FullDesc;
                cmd.Parameters.AddWithValue("@SmallImage", SqlDbType.NVarChar).Value = cat.SmallImage;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "InsertProductDetails", ex.Message);
        }
        finally
        {
            conT.Close();
        }
        return result;
    }
    public static int UpdateProductSeoDetails(SqlConnection conT, ProductDetails cat)
    {
        int result = 0;
        try
        {
            string query = "Update ProductDetails Set  PageTitle=@PageTitle,MetaKeys=@MetaKeys,MetaDesc=@MetaDesc,AddedBy=@AddedBy,AddedOn=@AddedOn,AddedIp=@AddedIp,Status=@Status Where Id =@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@PageTitle", SqlDbType.NVarChar).Value = cat.PageTitle;
                cmd.Parameters.AddWithValue("@MetaKeys", SqlDbType.NVarChar).Value = cat.MetaKeys;
                cmd.Parameters.AddWithValue("@MetaDesc", SqlDbType.NVarChar).Value = cat.MetaDesc;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateProductSeoDetails", ex.Message);
        }
        finally
        {
            conT.Close();
        }
        return result;
    }
    public static int UpdateProductDetails(SqlConnection conT, ProductDetails cat)
    {
        int result = 0;
        try
        {
            string query = "Update ProductDetails Set  BenefitsDesc=@BenefitsDesc,ProductAvailability=@ProductAvailability,ProductLabel=@ProductLabel," +
                           "IngredientsDesc=@IngredientsDesc,UsageDesc=@UsageDesc ,Category=@Category,Brand=@Brand,DeliveredBy=@DeliveredBy,RetailPrice=@RetailPrice," +
                           "ProductName=@ProductName,ProductUrl=@ProductUrl,PlaceOfOrigin=@PlaceOfOrigin,KeyIngredient=@KeyIngredient,ProductShortDesc=@ProductShortDesc," +
                           "InStock=@InStock,DisplayHome=@DisplayHome,CategoryId=@CategoryId,BrandId=@BrandId,FullDesc=@FullDesc,SmallImage=@SmallImage,AddedBy=@AddedBy," +
                           "AddedOn=@AddedOn,AddedIp=@AddedIp,Status=@Status Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                //cmd.Parameters.AddWithValue("@ProductId", SqlDbType.VarChar).Value = cat.ProductId;
                cmd.Parameters.AddWithValue("@ProductAvailability", SqlDbType.NVarChar).Value = cat.ProductAvailability;
                cmd.Parameters.AddWithValue("@ProductLabel", SqlDbType.NVarChar).Value = cat.ProductLabel;
                cmd.Parameters.AddWithValue("@Category", SqlDbType.NVarChar).Value = cat.Category;
                cmd.Parameters.AddWithValue("@Brand", SqlDbType.NVarChar).Value = cat.Brand;
                cmd.Parameters.AddWithValue("@DeliveredBy", SqlDbType.NVarChar).Value = cat.DeliveredBy;
                cmd.Parameters.AddWithValue("@RetailPrice", SqlDbType.NVarChar).Value = cat.RetailPrice;
                cmd.Parameters.AddWithValue("@ProductName", SqlDbType.NVarChar).Value = cat.ProductName;
                cmd.Parameters.AddWithValue("@ProductUrl", SqlDbType.NVarChar).Value = cat.ProductUrl;
                cmd.Parameters.AddWithValue("@PlaceOfOrigin", SqlDbType.NVarChar).Value = cat.PlaceOfOrigin;
                cmd.Parameters.AddWithValue("@KeyIngredient", SqlDbType.NVarChar).Value = cat.KeyIngredient;
                cmd.Parameters.AddWithValue("@ProductShortDesc", SqlDbType.NVarChar).Value = cat.ProductShortDesc;
                cmd.Parameters.AddWithValue("@InStock", SqlDbType.NVarChar).Value = cat.InStock;
                cmd.Parameters.AddWithValue("@DisplayHome", SqlDbType.NVarChar).Value = cat.DisplayHome;
                cmd.Parameters.AddWithValue("@BenefitsDesc", SqlDbType.NVarChar).Value = cat.BenefitsDesc;
                cmd.Parameters.AddWithValue("@IngredientsDesc", SqlDbType.NVarChar).Value = cat.IngredientsDesc;
                cmd.Parameters.AddWithValue("@UsageDesc", SqlDbType.NVarChar).Value = cat.UsageDesc;
                cmd.Parameters.AddWithValue("@CategoryId", SqlDbType.NVarChar).Value = cat.CategoryId;
                cmd.Parameters.AddWithValue("@BrandId", SqlDbType.NVarChar).Value = cat.BrandId;
                cmd.Parameters.AddWithValue("@FullDesc", SqlDbType.NVarChar).Value = cat.FullDesc;
                cmd.Parameters.AddWithValue("@SmallImage", SqlDbType.NVarChar).Value = cat.SmallImage;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = "Active";
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateBlogAuthors", ex.Message);
        }
        finally
        {
            conT.Close();
        }
        return result;
    }
    public static int DeleteProductDetails(SqlConnection conT, ProductDetails cat)
    {
        int result = 0;

        try
        {
            string query = "Update ProductDetails Set Status=@Status, AddedOn=@AddedOn, AddedIp=@AddedIp,AddedBy=@AddedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteProductDetails", ex.Message);
        }
        finally
        {
            conT.Close();
        }
        return result;
    }
    public static int GetLastProductId(SqlConnection conT, string pGuid)
    {
        int lastId = 0;
        try
        {
            string query = "Select Id From ProductDetails Where Status='Active' and ProductGuid=@ProductGuid Order by Id Desc";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@ProductGuid", SqlDbType.NVarChar).Value = pGuid;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lastId = Convert.ToInt32(Convert.ToString(dt.Rows[0]["Id"]));
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "GetLastProductId", ex.Message);
        }
        finally
        {
            conT.Close();
        }
        return lastId;
    }
    public static int UpdateAlternateProducts(SqlConnection conT, ProductDetails cat)
    {
        int result = 0;

        try
        {
            string query = "Update ProductDetails Set  AlternativeProduct=@AlternativeProduct,Status=@Status, AddedOn=@AddedOn, AddedIp=@AddedIp,AddedBy=@AddedBy Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = cat.Id;
                cmd.Parameters.AddWithValue("@AlternativeProduct", SqlDbType.NVarChar).Value = cat.AlternativeProduct;
                cmd.Parameters.AddWithValue("@Status", SqlDbType.NVarChar).Value = cat.Status;
                cmd.Parameters.AddWithValue("@AddedOn", SqlDbType.NVarChar).Value = cat.AddedOn;
                cmd.Parameters.AddWithValue("@AddedIp", SqlDbType.NVarChar).Value = cat.AddedIp;
                cmd.Parameters.AddWithValue("@AddedBy", SqlDbType.NVarChar).Value = cat.AddedBy;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "DeleteProductDetails", ex.Message);
        }
        finally
        {
            conT.Close();
        }
        return result;
    }
    public static int UpdateCategoryOrder(SqlConnection conT, ProductDetails pro)
    {
        int result = 0;
        try
        {
            string query = "Update ProductDetails Set  CategoryOrder=@CategoryOrder Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@CategoryOrder", SqlDbType.NVarChar).Value = pro.CategoryOrder;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = pro.Id;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateProductOrder", ex.Message);
        }
        finally
        {
            conT.Close();
        }
        return result;
    }

    public static int UpdateBrandOrder(SqlConnection conT, ProductDetails pro)
    {
        int result = 0;
        try
        {
            string query = "Update ProductDetails Set  BrandOrder=@BrandOrder Where Id=@Id ";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@BrandOrder", SqlDbType.NVarChar).Value = pro.BrandOrder;
                cmd.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = pro.Id;
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(HttpContext.Current.Request.Url.PathAndQuery, "UpdateProductOrder", ex.Message);
        }
        finally
        {
            conT.Close();
        }
        return result;
    }
    public static int UpdateDisplayOrder(SqlConnection conT, int id, int order)
    {
        int result = 0;
        try
        {
            string query = "Update ProductDetails Set CategoryOrder=@Order Where Id=@Id";
            using (SqlCommand cmd = new SqlCommand(query, conT))
            {
                cmd.Parameters.AddWithValue("@Order", order.ToString());
                cmd.Parameters.AddWithValue("@Id", id);
                conT.Open();
                result = cmd.ExecuteNonQuery();
                conT.Close();
            }
        }
        catch (Exception ex)
        {
            ExceptionCapture.CaptureException(
                HttpContext.Current.Request.Url.PathAndQuery,
                "UpdateDisplayOrder",
                ex.Message);
        }
        finally
        {
            conT.Close();
        }
        return result;
    }
}