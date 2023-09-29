using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Contable.Web.Configuration
{
    public class AppControllerDocumentationConvention : IControllerModelConvention
    {
        void IControllerModelConvention.Apply(ControllerModel controller)
        {
            if (controller == null)
                return;

            foreach (var attribute in controller.Attributes)
            {
                if (attribute.GetType() == typeof(RouteAttribute))
                {
                    var routeAttribute = (RouteAttribute)attribute;
                    if (!string.IsNullOrWhiteSpace(routeAttribute.Name))
                        controller.ControllerName = routeAttribute.Name;
                }
            }

        }
    }
}
