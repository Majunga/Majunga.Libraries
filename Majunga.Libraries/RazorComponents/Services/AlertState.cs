namespace Majunga.Libraries.RazorComponents.Services
{
    public class AlertState
    {
        private void NotifyStateChanged() => OnChange?.Invoke();

        public AlertState()
        {
            InfoMessage = string.Empty;
            WarningMessage = string.Empty;
            ErrorMessage = string.Empty;
            SuccessMessage = string.Empty;
        }

        public event Action? OnChange;

        public string? InfoMessage { get; private set; }
        public string? WarningMessage { get; private set; }
        public string? ErrorMessage { get; private set; }
        public string? SuccessMessage { get; private set; }


        public void ClearAll()
        {
            InfoMessage = string.Empty;
            WarningMessage = string.Empty;
            ErrorMessage = string.Empty;
            SuccessMessage = string.Empty;

            NotifyStateChanged();
        }

        public void Info(string message)
        {
            InfoMessage = message;
            NotifyStateChanged();
        }

        public void Warning(string message)
        {
            WarningMessage = message;
            NotifyStateChanged();
        }

        public void Error(string message)
        {
            ErrorMessage = message;
            NotifyStateChanged();
        }

        public void Success(string message)
        {
            SuccessMessage = message;
            NotifyStateChanged();
        }
    }
}