namespace SharedLib.UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {

            //// Opret produkt
            //var product = new Product()
            //{
            //    Name = "Banan",
            //    Price = 10,
            //    ProductId = 1,
            //    ProductNumber = "25",
            //    ProductCategoryId = 5
            //};

            ////Opret PuchasedProduct
            //var pproduct = new PurchasedProduct()
            //{
            //    Name = "Æble",
            //    ProductNumber = "60",
            //    Quantity = 10,
            //    UnitPrice = 5

            //};

            //// Opret Purchase
            //var purchase = new Purchase();
            //purchase.PurchasedProducts = new List<PurchasedProduct>();

            //purchase.PurchasedProducts.Add(pproduct);
            //purchase.PurchasedProducts.Add(pproduct);
            //purchase.PurchasedProducts.Add(pproduct);
            //purchase.PurchasedProducts.Add(pproduct);

            //var productList = new List<Product>();

            //productList.Add(product);
            //productList.Add(product);
            //productList.Add(product);
            //productList.Add(product);
            //productList.Add(product);

            //ProductCategory productCategory = new ProductCategory(productList) { Name = "Frugt", ProductCategoryId = 5 };

            //var catList = new List<ProductCategory>();

            //catList.Add(productCategory);
            //catList.Add(productCategory);

            //// Opret Commands
            //var cmd = new CreateProductCmd(product);
            //var prcmd = new ProductCreatedCmd(product);
            //var gcmd = new GetCatalogueCmd();
            //var cdcmd = new CatalogueDetailsCmd(catList);
            //var rpcmd = new RegisterPurchaseCmd(purchase);
            //var dcmd = new DeleteProductCmd(product);
            //var pdcmd = new ProductDeletedCmd(product);
            //var pccmd = new CreateProductCategoryCmd("Frugt", productList);
            //var tcmd = new ProductCategoryCreatedCmd("Mere Frugt", 5, productList);
            //var dpCcmd = new DeleteProductCategoryCmd(productCategory);
            //var pCdcmd = new ProductCategoryDeletedCmd(productCategory);
            //var ecmd = new EditProductCategoryCmd("Frugt igen", 6, productList);
            //var pCecmd = new ProductCategoryEditedCmd("Frugt igen igen", 7, productList);


            ////cdcmd.Products.Add(product);
            ////cdcmd.Products.Add(product);
            ////cdcmd.Products.Add(product);
            ////cdcmd.Products.Add(product);
            ////cdcmd.Products.Add(product); // 5
            ////cdcmd.Products.Add(product);
            ////cdcmd.Products.Add(product);
            ////cdcmd.Products.Add(product);
            ////cdcmd.Products.Add(product);
            ////cdcmd.Products.Add(product); // 10

            //// Create specific marshal
            //CreateProductMarshal cmarshal = new CreateProductMarshal();
            //ProductCreatedMarshal pmarshal = new ProductCreatedMarshal();
            //GetCatalogueMarshal gcmarshal = new GetCatalogueMarshal();
            //CatalogueDetailsMarshal cdmarshal = new CatalogueDetailsMarshal();
            //RegisterPurchaseMarshal rpmarshal = new RegisterPurchaseMarshal();
            //DeleteProductMarshal dmarshal = new DeleteProductMarshal();
            //ProductDeletedMarshal pdmarshal = new ProductDeletedMarshal();
            //CreateProductCategoryMarshal cpcmarshal = new CreateProductCategoryMarshal();
            //ProductCategoryCreatedMarshal tcmarshal = new ProductCategoryCreatedMarshal();
            //DeleteProductCategoryMarshal dpCmarshal = new DeleteProductCategoryMarshal();
            //ProductCategoryDeletedMarshal pCdmarshal = new ProductCategoryDeletedMarshal();
            //EditProductCategoryMarshal epCmarshal = new EditProductCategoryMarshal();
            //ProductCategoryEditedMarshal PCemarshal = new ProductCategoryEditedMarshal();

            //// Create protocol instance
            //Protocol.Protocol proto = new Protocol.Protocol();

            //// Encode from each
            //string xml = cmarshal.Encode(cmd);
            //string xml2 = proto.Encode(cmd);
            //string xml3 = pmarshal.Encode(prcmd);
            //string xml4 = gcmarshal.Encode(gcmd);
            //string xml5 = cdmarshal.Encode(cdcmd);
            //string xml6 = rpmarshal.Encode(rpcmd);
            //string xml7 = dmarshal.Encode(dcmd);
            //string xml8 = pdmarshal.Encode(pdcmd);
            //string xml9 = cpcmarshal.Encode(pccmd);
            //string xml10 = tcmarshal.Encode(tcmd);
            //string xml11 = dpCmarshal.Encode(dpCcmd);
            //string xml12 = pCdmarshal.Encode(pCdcmd);
            //string xml13 = epCmarshal.Encode(ecmd);
            //string xml14 = PCemarshal.Encode(pCecmd);

            //// Decode from each
            //var ccmd2 = (CreateProductCmd)cmarshal.Decode(xml);
            //var test2 = (CreateProductCmd)proto.Decode(xml2); // Her er man vel nød til at kende hvilken command der kommer til castingen????
            //var test3 = (ProductCreatedCmd)pmarshal.Decode(xml3);
            //var test4 = (GetCatalogueCmd)gcmarshal.Decode(xml4);
            //var test5 = (CatalogueDetailsCmd)cdmarshal.Decode(xml5);
            //var test6 = (RegisterPurchaseCmd)rpmarshal.Decode(xml6);
            //var test7 = (DeleteProductCmd)dmarshal.Decode(xml7);
            //var test8 = (ProductDeletedCmd)pdmarshal.Decode(xml8);
            //var test9 = (CreateProductCategoryCmd)cpcmarshal.Decode(xml9);
            //var test10 = (ProductCategoryCreatedCmd)tcmarshal.Decode(xml10);
            //var test11 = (DeleteProductCategoryCmd)dpCmarshal.Decode(xml11);
            //var test12 = (ProductCategoryDeletedCmd)pCdmarshal.Decode(xml12);
            //var test13 = (EditProductCategoryCmd)epCmarshal.Decode(xml13);
            //var test14 = (ProductCategoryEditedCmd)PCemarshal.Decode(xml14);

            //// Write first test of encode and decode from specific marshal
            //Console.WriteLine(xml);
            //Console.WriteLine("");
            //Console.WriteLine(ccmd2.CmdName);
            //Console.WriteLine(ccmd2.Name);
            //Console.WriteLine(ccmd2.ProductNumber);
            //Console.WriteLine(ccmd2.Price);
            //Console.WriteLine("");

            //// Write second test of encode and decode from protocol
            //Console.WriteLine(xml2);
            //Console.WriteLine("");
            //Console.WriteLine(test2.Name);
            //Console.WriteLine(test2.ProductNumber);
            //Console.WriteLine(test2.Price);
            //Console.WriteLine("");

            //// Write 3rd test of encode and decode from ProductCreateCmd
            //Console.WriteLine(xml3);
            //Console.WriteLine("");
            //Console.WriteLine(test3.Name);
            //Console.WriteLine(test3.ProductNumber);
            //Console.WriteLine(test3.Price);
            //Console.WriteLine(test3.ProductId);
            //Console.WriteLine("");

            //// 4th test GetCatalogueCmd
            //Console.WriteLine(xml4);
            //Console.WriteLine("");
            //Console.WriteLine(test4.CmdName);
            //Console.WriteLine("");

            //Console.WriteLine("----------------------------------------------------------------------------------------");

            //// 5th test CatalogueDetailsCmd
            //Console.WriteLine(xml5);
            //Console.WriteLine("");
            //Console.WriteLine(test5.ProductCategories.ElementAt(1).Products.ElementAt(3).Name);
            //Console.WriteLine("");

            //Console.WriteLine("----------------------------------------------------------------------------------------");

            //// 6th test RegisterPurchaseCmd
            //Console.WriteLine(xml6);
            //Console.WriteLine("");
            //Console.WriteLine(test6.Products.ElementAt(0).Name);
            //Console.WriteLine(test6.Products.ElementAt(1).Name);
            //Console.WriteLine(test6.Products.ElementAt(2).Name);
            //Console.WriteLine(test6.Products.ElementAt(3).Name);

            //// 7th test DeleteProductCmd
            //Console.WriteLine(xml7);
            //Console.WriteLine("");
            //Console.WriteLine(test7.CmdName);
            //Console.WriteLine("");

            //// 8th test ProductDeletedCmd
            //Console.WriteLine(xml8);
            //Console.WriteLine("");
            //Console.WriteLine(test8.CmdName);
            //Console.WriteLine("");

            //// 9th test ProductCategory
            //Console.WriteLine(xml9);
            //Console.WriteLine("");
            //Console.WriteLine(test9.Name);
            //Console.WriteLine("");

            //// 10th test ProductCategoryCreated
            //Console.WriteLine(xml10);
            //Console.WriteLine("");
            //Console.WriteLine(test10.Name);
            //Console.WriteLine("");

            //// 11th test DeleteProductCategory
            //Console.WriteLine(xml11);
            //Console.WriteLine("");
            //Console.WriteLine(test11.ProductCategoryId);
            //Console.WriteLine("");

            //// 12th test ProductCategoryDeleted
            //Console.WriteLine(xml12);
            //Console.WriteLine("");
            //Console.WriteLine(test12.ProductCategoryId);
            //Console.WriteLine("");

            //// 13th test ProductCategoryDeleted
            //Console.WriteLine(xml13);
            //Console.WriteLine("");
            //Console.WriteLine(test13.Products.ElementAt(2).Name);
            //Console.WriteLine("");

            //// 14th test ProductCategoryDeleted
            //Console.WriteLine(xml14);
            //Console.WriteLine("");
            //Console.WriteLine(test14.Name);
            //Console.WriteLine("");

            //// 15th test ProductCategoryCreated
            //Console.WriteLine(xml9);
            //Console.WriteLine("");
            //Console.WriteLine(test9.Name);
            //Console.WriteLine("");
        }
    }
}
