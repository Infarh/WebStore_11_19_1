using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.ViewModels.BreadCrumbs
{
    public enum BreadCrumbsType : byte
    {
        None,
        Section,
        Brand,
        Product
    }
}
