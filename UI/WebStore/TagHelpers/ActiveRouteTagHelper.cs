using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebStore.TagHelpers
{
    //[HtmlTargetElement(Attributes = "имена,атрибутов,через,запятую")]
    [HtmlTargetElement(Attributes = AttributeName)]
    public class ActiveRouteTagHelper : TagHelper // TagHelper обязательный суффикс имени класса
    {                                             // Имя таг-хелпера "ActiveRoute". Имя тега <active-route></active-route>
        public const string AttributeName = "is-active-route";


        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        private IDictionary<string, string> _RouteValues;

        [HtmlAttributeName("asp-all-route-data", DictionaryAttributePrefix = "asp-route-")]
        public IDictionary<string, string> RouteValues
        {
            get => _RouteValues ?? (_RouteValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase));
            set => _RouteValues = value;
        }

        [HtmlAttributeNotBound, ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //if (IsActive())
            //    MakeActive(output);

            output.Attributes.RemoveAll(AttributeName);
        }
    }
}
