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
using System.Data;
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
        [HttpGet]
        public IActionResult Index()
        {
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
                    Product product = _productService.GetProduct(vm.ProductId);
                    entity = _agreementService.GetAgreement(vm.Id);
                    entity.ProductGroupId = vm.ProductGroupId;
                    entity.ProductId = vm.ProductId;
                    entity.EffectiveDate = vm.EffectiveDate;
                    entity.ExpirationDate = vm.ExpirationDate;
                    entity.ProductPrice = product.Price;
                    entity.NewPrice = vm.NewPrice;
                    _agreementService.UpdateAgreement(entity);
                    rez = 1;
                }
            }
            return new JsonResult(rez);
        }

        [HttpPost]
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
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                string search = requestFormData["search[value]"][0].ToString();

                IEnumerable<Agreement> agreements = _agreementService.GetAgreements();
                if (agreements != null)
                {
                    if (!string.IsNullOrEmpty(search))
                    {
                        agreements = agreements.Where(m => m.Product.ProductNumber.ToString().Contains(search) || (User.Identity.Name.ToLower().Contains(search.ToLower())) || (m.ProductGroup.GroupCode.ToString().Contains(search)));
                    }

                    var listAgrement = agreements.ToList();
                    var list = _mapper.Map<List<Agreement>, List<AgreementViewModel>>(agreements.ToList());
                    
                    for(int i = 0; i < list.Count(); i++)
                    {
                        list[i].ProductNumber = listAgrement[i].Product.ProductNumber;
                        list[i].ProductDescription = listAgrement[i].Product.ProductDescription;
                        list[i].GroupCode = listAgrement[i].ProductGroup.GroupCode;
                        list[i].ProductGroupDescription =  listAgrement[i].ProductGroup.GroupDescription;
                        list[i].User = User.Identity.Name;
                        list[i].Srn = i + 1;
                    }

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
            Agreement agree = _agreementService.GetAgreement(id);
            if(agree != null)
            {
                try
                {
                    _agreementService.DeleteAgreement(id);
                    rez = "1";
                }
                catch (Exception ex)
                {
                    var exception = ex;
                }
                return new JsonResult(rez);
            }
            return new JsonResult(rez);
        }

        [HttpGet]
        public IActionResult ProductSelect(int? id)
        {
            try
            {
                IEnumerable<Product> products = _productService.GetProducts(id);
                var list = _mapper.Map<List<Product>, List<ProductViewModel>>(products.ToList());

                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("ProductSelect error " + ex);
                return Json(null);
            }
        }
    }
}