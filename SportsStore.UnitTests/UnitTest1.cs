﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.WebUI.Models;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.HtmlHelpers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"}
            });
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            //Act
            IEnumerable<Product> result = (IEnumerable<Product>)controller.List(2).Model;
            //Assert
            Product[] prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }
        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //Arrange-define an html helper to test
            HtmlHelper myHelper = null;
            //Arrange - create pageinfo data
            PagingInfo pageInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            //Arrange-setup the delegate using a lambda expression
            Func<int, string> pageUrlDelegate = i => "Page" + i;
            //Act
            MvcHtmlString result = myHelper.PageLinks(pageInfo, pageUrlDelegate);
            // Assert            
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>" 
                    + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>" 
                        + @"<a class=""btn btn-default"" href=""Page3"">3</a>",                
                            result.ToString());
        }
    }
}
