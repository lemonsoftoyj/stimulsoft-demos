using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report.Web;
using StimulsoftBugDemo.Models;
using System.Diagnostics;

namespace StimulsoftBugDemo.Controllers
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

        public IActionResult GetReport(string file = "")
        {
            StiReport report = new StiReport();

            if(string.IsNullOrEmpty(file))
            {
                report.Load(StiNetCoreHelper.MapPath(this, "Reports/blank.mrt"));
            }
            else
            {
                report.Load(StiNetCoreHelper.MapPath(this, $"Reports/{file}.mrt"));
            }

            return StiNetCoreViewer.GetReportResult(this, report);
        }

        public IActionResult ViewerEvent()
        {
            StiRequestParams requestParams = StiNetCoreViewer.GetRequestParams(this);

            if (requestParams.Action == StiAction.GetReport)
            {
                return GetReport("blank-w-var");
            }
            return StiNetCoreViewer.ViewerEventResult(this);
        }
    }
}
