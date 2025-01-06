using Application.Services;
using Core.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using System.Diagnostics;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService userService;
        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {

            var dapperwatch = Stopwatch.StartNew();
            var lstDapper = await userService.GetAllAsync(true);
            dapperwatch.Stop();
            var DapperTime = dapperwatch.ElapsedMilliseconds;
            Console.WriteLine($"Dapper time: {DapperTime} ms");

            var efwatch = Stopwatch.StartNew();
            var lst = await userService.GetAllAsync();
            efwatch.Stop();
            var EfTime = efwatch.ElapsedMilliseconds;
            Console.WriteLine($"EF time: {EfTime} ms");
            return View();
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
