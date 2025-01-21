using Application.Commands_Queries.Users.Commands;
using Application.Services;
using Core.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Extensions;
using Shop.Models;
using System.Diagnostics;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService userService;
        private readonly IMediator mediator;
        public HomeController(ILogger<HomeController> logger, IUserService userService, IMediator mediator)
        {
            _logger = logger;
            this.userService = userService;
            this.mediator = mediator;
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

        public async Task<IActionResult> Insert()
        {
            return View();
        }

        [HttpPost]
        [PreventDoublePost]
        public async Task<IActionResult> Post(UserCommand userCommand)
        { 
            var res = await mediator.Send(userCommand);
            return PartialView("Insert", res);
        }
    }
}
