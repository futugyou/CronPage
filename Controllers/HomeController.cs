using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CronExpress.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public string CalcRunTime(string expression)
        {
            var list = new List<DateTime>();
            try
            {
                CronExpression ce = new CronExpression(expression);
                while (list.Count < 10)
                {
                    var d = ce.GetNextValidTimeAfter(list.Count == 0 ? DateTime.Now : list[list.Count - 1].AddSeconds(1));
                    if (d.HasValue)
                    {
                        list.Add(d.Value.ToLocalTime().DateTime);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return JsonConvert.SerializeObject(list);
        }
    }
}