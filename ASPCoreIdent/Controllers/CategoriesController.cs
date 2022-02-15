using AutoMapper;
using Contracts.Business;
using DAL.Models;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCoreIdent.Models.Category;
using BLL;

namespace ASPCoreIdent.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService, ILogger<HomeController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _categoryService = categoryService;
        }


        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();
            var model = _mapper.Map<IEnumerable<CategoryVM>>(categories);
            return View(model);
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            var categories = _categoryService.GetAll();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory([FromForm] CategoryVM category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            try
            {
                var createCategoryDto = _mapper.Map<CategoryDTO>(category);
                _categoryService.CreateCategory(createCategoryDto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Source, ex.Message);
                return View(category);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var category = _categoryService.CategoryByID(id);
            var model = _mapper.Map<CategoryVM>(category);
            return View(model);
        }

        [HttpPost]
        public IActionResult EditCategory([FromForm] CategoryVM category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            try
            {
                var editCategoryDto = _mapper.Map<CategoryDTO>(category);
                _categoryService.EditCategory(editCategoryDto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Source, ex.Message);
                return View(category);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryService.CategoryByID(id);
            var model = _mapper.Map<CategoryVM>(category);
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
                var deleteCategoryDto = id;
                _categoryService.DeleteCategory(deleteCategoryDto);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
            return Ok();
        }
    }
}
