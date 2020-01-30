using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.ViewModels.BreadCrumbs
{
    public class BreadCrumbViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public BreadCrumbsType BreadCrumbsType { get; set; }
    }
}
