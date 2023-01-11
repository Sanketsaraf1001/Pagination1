using DataTablesInDotNetCore.DbModels;
using DataTablesInDotNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pagination.DbModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace DataTablesInDotNetCore.Controllers
{
    public class HomeController : Controller
    {

        private EmpContext _context;
        public HomeController(EmpContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Productlist()
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var draw = Request.Form["draw"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
            int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
            var data = _context.Set<Product>().AsQueryable();
            totalRecord = data.Count();
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x =>
                  x.ProductId.ToString().Contains(searchValue.ToLower())
                  || x.ProductName.ToLower().Contains(searchValue.ToLower())
                  || x.CategoryId.ToString().Contains(searchValue.ToLower())
                  || x.CategoryName.ToString().ToLower().Contains(searchValue.ToLower())

                );
            }
            
            // get total count of records after search 
            filterRecord = data.Count();

            //sort data
            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                data = data.OrderBy(sortColumn + " " + sortColumnDirection);


            //pagination
           var  empList= data.Skip(skip).Take(pageSize).ToList();

            var returnObj = new {draw= draw ,recordsTotal = totalRecord, recordsFiltered = filterRecord, data = empList };
            return Json(returnObj);
        }

    }
}
