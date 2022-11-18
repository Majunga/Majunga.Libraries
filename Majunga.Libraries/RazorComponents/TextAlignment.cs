namespace Majunga.Libraries.RazorComponents
{
    public enum TextAlignment
    {
        Left,
        Center,
        Right,
    }

    public static class TextAlignmentExtensions
    {
        public static string Value(this TextAlignment alignment)
        {
            switch (alignment)
            {
                case (TextAlignment.Center):
                    return "text-center";
                case (TextAlignment.Left):
                    return "text-left";
                default:
                    return "text-right";
            }
        }
    }
}
