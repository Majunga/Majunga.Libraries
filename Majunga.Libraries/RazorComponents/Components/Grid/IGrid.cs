namespace Majunga.Libraries.RazorComponents.Components.Grid
{
    public interface IGrid<TModelItem>
    {
        void AddColumn(IColumn<TModelItem> gridColumn);
    }
}
