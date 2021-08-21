using AgreementManagement.Data.Entities;
using AgreementManagement.Service;
using AgreementManagement.Service.Services.Interfaces;
using AgreementManagement.Web.Controllers;
using AgreementManagement.Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class AgreementController : Controller
    {
        private readonly ILogger<AgreementController> _logger;
        private readonly IAgreementService _agreementService;
        private readonly IProductService _productService;
        private readonly IProductGroupService _productGroupService; 
        private readonly IMapper _mapper;

        public AgreementController(ILogger<AgreementController> logger, IAgreementService agreementService, IProductService productService, IProductGroupService productGroupService, IMapper mapper)
        {
            _logger = logger;
            _agreementService = agreementService;
            _mapper = mapper;
            _productService = productService;
            _productGroupService = productGroupService;
        }

        [ServiceFilter(typeof(AuhtActionFilterAttribute))]
        //[Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            //GRESKU DA SE DESI DA UPISE U LOG FAJL
            //int p = 10;
            //for(int i = 0; i <20; i++)
            //{
            //    if(p == i)
            //    {
            //        throw new Exception("This is error message");
            //    }
            //    else
            //    {
            //        _logger.LogInformation("The value of i is {LoopCountValue}", i);
            //    }
            //}

            IEnumerable<ProductGroup> listProductGroups = _productGroupService.GetProductGroups();
            var list = _mapper.Map<List<ProductGroup>, List<ProductGroupViewModel>>(listProductGroups.ToList());

            var productGroupList = new List<SelectListItem>();
            bool selected = true;

            foreach (var item in list)
            {
                if (selected)
                {
                    productGroupList.Add(new SelectListItem() { Text = item.GroupDescription, Value = item.Id.ToString(), Selected = true });
                }
                else
                {
                    productGroupList.Add(new SelectListItem() { Text = item.GroupDescription, Value = item.Id.ToString(), Selected = false });
                    selected = false;
                }
            }

            ViewBag.ProductGroup = productGroupList;
            return View();
        }

        [HttpPost]
        public IActionResult AddAgreement(AgreementViewModel vm)
        {
            var rez = 0;
            if (ModelState.IsValid)
            {
                Agreement entity = new Agreement();
                if (vm.Id == 0)
                {
                    entity = _mapper.Map<AgreementViewModel, Agreement>(vm);
                    _agreementService.AddAgreement(entity);
                    rez = 1;
                }
                else
                {
                    entity = _agreementService.GetAgreement(vm.Id);
                    entity.ProductGroupId = vm.ProductGroupId;
                    entity.ProductId = vm.ProductId;
                    entity.EffectiveDate = vm.EffectiveDate;
                    entity.ExpirationDate = vm.ExpirationDate;
                    entity.ProductPrice = vm.ProductPrice;
                    _agreementService.UpdateAgreement(entity);
                    rez = 1;
                }
            }
            return new JsonResult(rez);
            //return RedirectToAction(nameof(AgreementController.Index));
        }

        [HttpGet]
        public JsonResult AgreementData()
        {
            try
            {
                var requestFormData = Request.Form;
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                string search = requestFormData["search[value]"][0].ToString().ToUpper();

                var agreements = _agreementService.GetAgreements();
                if (agreements != null)
                {
                    if (!string.IsNullOrEmpty(search))
                    {
                        agreements = agreements.Where(m => m.ProductId.ToString().Contains(search) || m.User.Email.ToLower().Contains(search.ToLower()) || m.ProductGroup.ToString().Contains(search));
                    }

                    var list = _mapper.Map<List<Agreement>, List<AgreementViewModel>>(agreements.ToList());
                    recordsTotal = list.Count();
                    var data = list.Skip(skip).Take(pageSize).ToList();
                    var response = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                    return Json(response);
                }
                else
                {
                    return Json(null);
                }
            }
            catch(Exception ex)
            {
                return Json(null);
            }
        }

        public IActionResult DeleteAgrement(int id)
        {
            var rez = "0";
            try
            {
                _agreementService.DeleteAgreement(id);
                rez = "1";
            }
            catch(Exception ex)
            {
                var exception = ex;
            }
            return new JsonResult(rez);
        }

        [HttpGet]
        public IActionResult ProductSelect(AgreementViewModel vm)
        {
            try
            {
                IEnumerable<Product> products = _productService.GetProducts(vm.ProductGroupId);
                var list = _mapper.Map<List<Product>, List<ProductViewModel>>(products.ToList());


                //IEnumerable<Product> nesto = null;
                //var pom = nesto.FirstOrDefault();
                //var productList = new List<SelectListItem>();
                //bool selected = true;

                //foreach (var item in list)
                //{
                //    if (selected)
                //    {
                //        productList.Add(new SelectListItem() { Text = item.ProductDescription, Value = item.Id.ToString(), Selected = true });
                //    }*
                //    else
                //    {
                //        productList.Add(new SelectListItem() { Text = item.ProductDescription, Value = item.Id.ToString(), Selected = false });
                //        selected = false;
                //    }
                //}

                //ViewBag.Product = productList; }
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ProductSelect error " + ex);
                return Ok(ex);

            }

        }
    }
}