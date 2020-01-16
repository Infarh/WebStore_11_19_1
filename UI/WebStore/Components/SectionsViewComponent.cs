using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Components
{
    //[ViewComponent(Name = "Sections")]
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;
        public SectionsViewComponent(IProductData ProductData) => _ProductData = ProductData;

        //public async Task<IViewComponentResult> InvokeAsync() => View();
        public IViewComponentResult Invoke() => View(GetSections());

        private IEnumerable<SectionViewModel> GetSections()
        {
            var sections = _ProductData.GetSections();

            var parent_sections = sections.Where(section => section.ParentId is null).ToArray();

            var parent_sections_views = parent_sections
               .Select(parent_section => new SectionViewModel
               {
                   Id = parent_section.Id,
                   Name = parent_section.Name,
                   Order = parent_section.Order
               })
               .ToList();

            foreach (var parent_section_view in parent_sections_views)
            {
                var childs = sections.Where(section => section.ParentId == parent_section_view.Id);
                foreach (var child_section in childs)
                    parent_section_view.ChildSections.Add(
                        new SectionViewModel
                        {
                            Id = child_section.Id,
                            Name = child_section.Name,
                            Order = child_section.Order,
                            ParentSection = parent_section_view
                        });
                parent_section_view.ChildSections.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            }

            parent_sections_views.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            return parent_sections_views;
        }
    }
}
