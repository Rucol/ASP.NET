using BikeRentalSystemGr2B.Models;
using BikeRentalSystemGr2B.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BikeRentalSystemGr2B.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IEnumerable<BikeDetailViewModel> _bikes = new
            List<BikeDetailViewModel>
        {
            new BikeDetailViewModel
            {
                Id = 1,
                Producer = "Giant",
                Model = "TCR56-1",
                Color = "Black",
                BikeType = BikeTypeModel.Male,
                NumberOfBikes = 1,
                NumberOfGears = 24,
            },
            new BikeDetailViewModel
            {
                Id = 2,
                Producer = "Giant",
                Model = "TCR56-2",
                Color = "White",
                BikeType = BikeTypeModel.Female,
                NumberOfBikes = 2,
                NumberOfGears = 24,
            },
            new BikeDetailViewModel
            {
                Id = 3,
                Producer = "Giant",
                Model = "TCR56-3",
                Color = "Green",
                BikeType = BikeTypeModel.Kids,
                NumberOfBikes = 5,
                NumberOfGears = 24,
            },
            new BikeDetailViewModel
            {
                Id = 4,
                Producer = "Cross",
                Model = "Country",
                Color = "Grey",
                BikeType = BikeTypeModel.Female,
                NumberOfBikes = 8,
                NumberOfGears = 24,
            },
            new BikeDetailViewModel
            {
                Id = 5,
                Producer = "Btwin",
                Model = "Rockrider 560",
                Color = "Black",
                BikeType = BikeTypeModel.Male,
                NumberOfBikes = 1,
                NumberOfGears = 21,
            },

        };

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Nazwa = "UBB Bike Rental System";
            ViewBag.Opis = "Opis działania...";
            ViewBag.Zdjecia = new string[] { "https://poradniki-admin.selgros24.pl/uploads/rower_w_lesie_7f5eaee212.jpg", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT5oYjpm02vBLvpIQpbEui8JvVN8moxSvHCOEwZVwTDpA&s" }; 
            return View(_bikes);
        }
        public IActionResult Detail(int id)
        {
            var bike = _bikes
                .FirstOrDefault(x => x.Id == id);
            return View(bike);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    
}
// Podobna aplikacja do wycieczek