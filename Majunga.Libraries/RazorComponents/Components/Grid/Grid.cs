using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Routing;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Majunga.Libraries.RazorComponents.Components.Grid
{
    public class Grid<TModelItem> : ComponentBase, IGrid
    {
        private List<IColumn> InitialisedColumns { get; set; } = new List<IColumn>();

        [Parameter]
        public IEnumerable<TModelItem>? Data { get; set; }

        [Parameter]
        public RenderFragment? Columns { get; set; }

        public void AddColumn(IColumn gridColumn)
        {
            if (InitialisedColumns.Any(c => c.Field == gridColumn.Field))
                return;

            InitialisedColumns.Add(gridColumn);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var seq = 0;
            builder.OpenElement(seq++, "div");
            builder.OpenElement(seq++, "table");
            builder.AddAttribute(seq++, "class", "table");
            builder.OpenElement(seq++, "thead");
            builder.OpenElement(seq++, "tr");

            foreach (var column in InitialisedColumns)
            {
                builder.OpenElement(seq++, "th");
                builder.AddContent(seq++, column.Title);
                builder.CloseElement();//th
            }

            builder.CloseElement();//tr
            builder.CloseElement();//thead

            if (InitialisedColumns.Count > 0 && Data != null && Data.Any())
            {
                var data = Data.ToList();
                foreach (var row in Data)
                {
                    builder.OpenElement(seq++, "tr");

                    foreach (var column in InitialisedColumns)
                    {
                        builder.OpenElement(seq++, "td");
                        builder.AddContent(seq++, GetValue(row, column));
                        builder.CloseElement();//td
                    }

                    builder.CloseElement();//tr
                }
            }

            builder.CloseElement();//table

            builder.OpenComponent(seq++, typeof(CascadingValue<IGrid>));
            builder.AddAttribute(seq++, "Value", this);
            builder.AddAttribute(seq++, "IsFixed", true);
            builder.AddAttribute(seq++, "ChildContent", (RenderFragment)(b =>
            {
                b.AddMarkupContent(seq++, "\r\n    ");
                b.AddContent(seq++, Columns);
                b.AddMarkupContent(seq++, "\r\n    ");
            }));

            builder.CloseComponent();//CascadingValue
            builder.CloseElement();

            base.BuildRenderTree(builder);
        }

        private string? GetValue(TModelItem model, IColumn column)
        {
            if (string.IsNullOrWhiteSpace(column.Field)) return string.Empty;
            var modelType = model!.GetType();
            var property = modelType.GetProperty(column.Field);
            var value = property?.GetValue(model) ?? null;

            if (string.IsNullOrWhiteSpace(column.DisplayFormat) == false && value != null)
            {
                var convertedValue = Convert.ChangeType(value, property!.PropertyType);
                return string.Format(column.DisplayFormat, convertedValue);
            }

            return value?.ToString() ?? string.Empty;
        }
    }

    public interface IGrid
    {
        void AddColumn(IColumn gridColumn);
    }
}
