using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using KitKat.Data;
using KitKat.Data.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace KitKat.Controllers.Web
{
    public class HomeController : Controller
    {
        private IConfigurationRoot _config;
        private ILogger<HomeController> _logger;
        private IRoleRepository _repository;

        // private KitKatContext _context;

        public HomeController(IConfigurationRoot config ,
       //     KitKatContext context
                IRoleRepository repository,
                ILogger<HomeController> logger
            )
        {
            _config = config;
            _repository = repository;
            _logger = logger;
         //   _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            //   var data = _context.Roles.ToList();
            try
            {
                var data = _repository.GetAllRoles();

                return View(data);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get roles : {ex.Message}");
                return Redirect("/error");
            }
        }

        [Authorize]
        public IActionResult Roles()
        {
            var data = _repository.GetAllRoles();

            return View(data);
        }
    }
}
