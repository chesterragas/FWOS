using ROHM_WOS.Controllers;
using ROHM_WOS.Models;
using ROHM_WOS.Models.MasterEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using static ROHM_WOS.Controllers.Session;

namespace ROHM_WOS.Areas.MasterModule.Controllers
{
    public class MachinesController : Controller
    {
        // GET: MasterModule/Machines
        public ActionResult Machines()
        {
            return View();
        }
    }
}