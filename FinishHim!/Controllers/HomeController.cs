using FinishHim_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FinishHim_.Service;
using FinishHim_;
using FinishHim_.Controllers;
using System.Security.Cryptography.X509Certificates;

namespace FinishHim_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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

        public async Task<IActionResult> GetPlayer()
        {
            var listWithPlayers = await Service.Service.GetPlayer();
            return View(listWithPlayers.OrderBy(q => q.PlayerId));
        }

        public async Task<IActionResult> GetTeam()
        {
            var listWithTeams = await Service.Service.GetTeam();
            return View(listWithTeams.OrderBy(q => q.TeamNumber));
        }

        public async Task<IActionResult> GetManager()
        {
            var listWithPlayers = await Service.Service.GetPlayer();
            var listWithTeams = await Service.Service.GetTeam();
            var relatedList = listWithPlayers.Join(listWithTeams, q => q.PlayerTeamNumber, w => w.TeamNumber, (q, w) => new ManagerModel
            {
                TeamNumber = w.TeamNumber,
                TeamName = w.Name,
                TeamAge = w.Age,
                TeamPrice = w.Price,
                CreateDate = w.CreateDate,
                TeamId = q.PlayerId,
                PlayerFirstName = q.FirstName,
                PlayerLastName = q.LastName,
                PlayerEmail = q.Email,
                PlayerAge = q.Age,
                PlayerTeamNumber = q.PlayerTeamNumber

            }).OrderBy(q => q.TeamId);
            return View(relatedList);
        }
            [HttpPost]
            public async Task<IActionResult> AddPlayer(PlayerModel player)
            {
            
                await Service.Service.AddPlayer(player);
                return View();
            }
            [HttpGet]
            public IActionResult AddPlayer()
            {
                return View();
            }
            [HttpPost]
            public async Task<IActionResult> AddTeam(TeamModel team)
            {
            await Service.Service.AddTeam(team);
            return View();
            }
            [HttpGet]
            public IActionResult AddTeam()
            {
            return View();
            }
            public async Task<IActionResult> DeletePlayer(int playerId)
            {
            await Service.Service.DeletePlayer(playerId);
            return RedirectToAction("GetPlayer");
            }
            public async Task<IActionResult> DeleteTeam(int teamNumber)
            {
            await Service.Service.DeleteTeam(teamNumber);
            return RedirectToAction("GetTeam");
            }

    }
    }
