using ASPCoreIdent.Models;
using ASPCoreIdent.Models.Good;
using AutoMapper;
using Contracts.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreIdent.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        IGoodsService _goodsServise;

        public HomeController(IGoodsService goodsServise, ILogger<HomeController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _goodsServise = goodsServise;
        }

        public IActionResult Index()
        {
            var goods = _goodsServise.GetAll();
            var model = _mapper.Map<IEnumerable<GoodVM>>(goods);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
