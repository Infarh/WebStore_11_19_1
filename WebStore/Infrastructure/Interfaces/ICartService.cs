using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Interfaces
{
    public interface ICartService
    {
        void AddToCart(int id);

        void DecrementFromCart(int id);

        void RemoveFromCart(int id);

        void RemoveAll();

        CartViewModel TransformFromCart();
    }
}
