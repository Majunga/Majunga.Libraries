using Microsoft.AspNetCore.Components;

namespace Majunga.Libraries.RazorComponents.Components.Grid
{
    public class GridColumn : ComponentBase, IColumn
    {
        [CascadingParameter]
        public IGrid Container { get; set; }
        
        [Parameter]
        public string? Title { get; set; }
        
        [Parameter]
        public string? Field { get; set; }

        [Parameter]
        public string? DisplayFormat { get; set; }

        protected override Task OnInitializedAsync()
        {
            Container?.AddColumn(this);
            return base.OnInitializedAsync();
        }
    }
    public interface IColumn
    {
        string? Title { get; set; }
        string? Field { get; set; }
        string? DisplayFormat { get; set; }
    }
}
