using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudentJourney.Controllers
{
	[Authorize(Roles = "User")]
	public class SalaryController : Controller
	{
		public IActionResult Payslip() =>
						Content("User");
	}
}
