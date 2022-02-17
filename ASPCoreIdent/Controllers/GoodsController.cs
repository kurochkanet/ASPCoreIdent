using AutoMapper;
using BLL;
using Contracts.Business;
using DAL;
using DAL.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ASPCoreIdent.Models;
using ASPCoreIdent.Models.Good;
using ASPCoreIdent.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace ASPCoreIdent.Controllers
{
    [Authorize(Roles = "admin")]
    public class GoodsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IGoodsService _goodsServise;
        private readonly ICategoryService _categoryService;

        //rivate DataContext _context;

        public GoodsController(IGoodsService goodsServise, ICategoryService categoryService, ILogger<HomeController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _goodsServise = goodsServise;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var goods = _goodsServise.GetAll();
            var model = _mapper.Map<IEnumerable<GoodVM>>(goods);
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateGood()
        {
            var categories = _categoryService.GetAll();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult CreateGood([FromForm] GoodVM good)
        {
            if (!ModelState.IsValid)
            {
            
                return View(good);
            }
            try
            {
                IFormFile formFile = null;
                if (this.HttpContext.Request.Form.Files.Count > 0)
                {
                    formFile = this.HttpContext.Request.Form.Files[0];
                }
                var createGoodDto = _mapper.Map<GoodDTO>(good);
                 _goodsServise.CreateGood(createGoodDto, formFile);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Source, ex.Message);
                return View(good);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditGood(int id )
        {            
            var categories = _categoryService.GetAll();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name");

            var good = _goodsServise.GoodByID(id);
            var model =_mapper.Map<GoodVM>(good);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditGood([FromForm] GoodVM good)
        {
            if (!ModelState.IsValid)
            {
                return View(good);
            }
            try
            {
                IFormFile formFile = null;
                if (this.HttpContext.Request.Form.Files.Count > 0)
                {
                    formFile = this.HttpContext.Request.Form.Files[0];
                }
                
                var editGoodDto = _mapper.Map<GoodDTO>(good);
              
                _goodsServise.EditGood(editGoodDto, formFile);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Source, ex.Message);
                return View(good);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteGood(int id)
        {
            var good = _goodsServise.GoodByID(id);
            var model = _mapper.Map<GoodVM>(good);
            return View(model);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }
            try
            {
                var deleteGoodDto = id;
                _goodsServise.DeleteGood(deleteGoodDto);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
