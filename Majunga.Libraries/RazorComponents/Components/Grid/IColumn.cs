using Microsoft.AspNetCore.Components;

namespace Majunga.Libraries.RazorComponents.Components.Grid
{
    public interface IDataColumn : IColumn
    {
        string? Field { get; set; }
        string? DisplayFormat { get; set; }
        bool Hidden { get; set; }
        bool IsKey { get; set; }
        RenderFragment? ChildContent { get; set; }
    }

    public interface ICommandColumn<TModelItem> : IColumn
    {
        RenderFragment<TModelItem>? ChildContent { get; set; }
    }

    public interface IColumn
    {
        string? Title { get; set; }
    }
}
