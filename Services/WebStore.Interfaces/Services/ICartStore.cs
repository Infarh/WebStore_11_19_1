using System;
using System.Collections.Generic;
using System.Text;
using WebStore.Domain.Models;

namespace WebStore.Interfaces.Services
{
    public interface ICartStore
    {
        Cart Cart { get; set; }
    }
}
