using Application.DTO;
using Application.Services;
using Core.Entities;
using Core.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shop.Models;
using System.Diagnostics;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRedisCacheService cacheService;
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService userService;
        private RedisKeysOptions redisKeysOptions;
        public HomeController(ILogger<HomeController> logger, IUserService userService, IRedisCacheService cacheService, IOptions<RedisKeysOptions> redisKeysOptions)
        {
            _logger = logger;
            this.userService = userService;
            this.cacheService = cacheService;
            this.redisKeysOptions = redisKeysOptions.Value;
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

            var rediswatch = Stopwatch.StartNew();
            var allUser = redisKeysOptions.AllUser;
            var lstRedis = await cacheService.GetAsync<List<User>>(allUser);
            rediswatch.Stop();
            var redisTime = rediswatch.ElapsedMilliseconds;
            Console.WriteLine($"Redis time: {redisTime} ms");


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
