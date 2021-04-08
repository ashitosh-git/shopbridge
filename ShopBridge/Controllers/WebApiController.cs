using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopBridge.Controllers
{
    public class WebApiController : ApiController
    {
        ShopBridgeEntities shopBridge = new ShopBridgeEntities();

        [HttpGet]
        [Route("api/webapi/GetData")]
        public IHttpActionResult GetData()
        {
            List<Product> list = shopBridge.Products.ToList();
            return Ok(list);

        }

        [HttpPost]
        public IHttpActionResult Insert(Product product)
        {
            shopBridge.Products.Add(product);
            shopBridge.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("api/webapi/Details")]
        public IHttpActionResult Details(int id)
        {
            Product data = shopBridge.Products.Where(x => x.Id == id).SingleOrDefault();
            return Ok(data);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            Product data = shopBridge.Products.Where(x => x.Id == id).SingleOrDefault();
            shopBridge.Products.Remove(data);
            shopBridge.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Update(Product product)
        {
            Product data = shopBridge.Products.Where(x => x.Id == product.Id).SingleOrDefault();
            data.Name = product.Name;
            data.ProductId = product.ProductId;
            data.Description = product.Description;
            data.Price = product.Price;
            shopBridge.SaveChanges();
            return Ok();
        }
    }
}
