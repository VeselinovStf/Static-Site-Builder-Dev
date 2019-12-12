using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminSiteTypeTemplatesController : Controller
    {
        //Add widgets ???
    }
}
