using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Components
{
    public class BreadCrumbsViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke() => View();
    }
}
