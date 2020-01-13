using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace WebStore.Infrastructure.Conventions
{
    public class CustomControllerConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            //controller.ControllerName
        }
    }
}
