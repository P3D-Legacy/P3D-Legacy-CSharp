using System.Globalization;

namespace P3D.Legacy.Core.Data
{
    public class Token
    {
        public string TokenName { get; }
        public string TokenContent { get; }
        public CultureInfo Language { get; }
        public bool IsGameModeToken { get; }

        public Token(string name, string content, CultureInfo language, bool isGameModeToken)
        {
            TokenName = name;
            TokenContent = content;
            Language = language;
            IsGameModeToken = isGameModeToken;
        }
    }
}