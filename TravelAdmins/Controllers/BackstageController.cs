using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelAdmins.Controllers
{
    public class BackstageController : Controller
    {

        /// <summary>
        /// 后台视图模板
        /// </summary>
        /// <param name="MenuId">打开显示ID</param>
        /// <returns></returns>
        public ActionResult Index(string MenuId)
        {



            return View();
        }



    }
}