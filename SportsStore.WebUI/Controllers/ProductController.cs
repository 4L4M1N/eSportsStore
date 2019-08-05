using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        private IProductRepository _productRepository;
        public int PageSize = 2;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public ViewResult List(int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel {
                Products = _productRepository.Products
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _productRepository.Products.Count()
            }
            };
            return View(model);
         
        }
    }
}
