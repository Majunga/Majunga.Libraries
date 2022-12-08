using Microsoft.AspNetCore.Components;

namespace Majunga.Libraries.RazorComponents.Components.Grid
{
    public interface IColumn<TModelItem>
    {
        string? Title { get; set; }
        string? Field { get; set; }
        string? DisplayFormat { get; set; }
        bool Hidden { get; set; }
        bool IsKey { get; set; }
        RenderFragment<TModelItem>? ChildContent { get; set; } 
    }
}
