using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Storage;
using P3D.Legacy.Core.Storage.Folders;
using PCLExt.FileStorage;

// IO:
namespace P3D.Legacy.Core
{
    public static class Localization
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

        public static CultureInfo Language = new CultureInfo("en");

        public static Dictionary<string, Token> LocalizationTokens { get; } = new Dictionary<string, Token>();
        public static void Load(CultureInfo language)
        {
            LocalizationTokens.Clear();

            Language = language;

            Logger.Debug($"Loaded language [{Language.Name}]");

            LoadTokenFile(StorageInfo.LocalizationFolder, false); // -- Load Game Translation.

            if (GameModeManager.GameModeCount > 0)
            {
                var gameModeLocalizationPath = GameModeManager.ActiveGameMode.LocalizationFolder;
                if (gameModeLocalizationPath != StorageInfo.LocalizationFolder)
                    LoadTokenFile(gameModeLocalizationPath, true); // -- Load GameMode Translation.
            }
        }

        public static void ReloadGameModeTokens()
        {
            var keysArray = LocalizationTokens.Keys.ToArray();
            var valuesArray = LocalizationTokens.Values.ToArray();
            for (var i = 0; i <= LocalizationTokens.Count - 1; i++)
            {
                if (i <= LocalizationTokens.Count - 1)
                {
                    var token = valuesArray[i];

                    if (token.IsGameModeToken)
                    {
                        LocalizationTokens.Remove(keysArray[i]);
                        i -= 1;
                    }
                }
            }

            if (GameModeManager.GameModeCount > 0)
            {
                var gameModeLocalizationPath = GameModeManager.ActiveGameMode.LocalizationFolder;
                if (gameModeLocalizationPath != StorageInfo.LocalizationFolder)
                    LoadTokenFile(gameModeLocalizationPath, true);
            }

            Logger.Debug("---Reloaded GameMode Tokens---");
        }

        private static void LoadTokenFile(ILocalizationFolder localizationFolder, bool isGameModeFile)
        {
            var defaultLanguage = new CultureInfo("en");

            if (localizationFolder.GetFilesAsync().Result.Count > 0)
            {
                if (!localizationFolder.CheckTranslationExistsAsync(Language).Result)
                    Language = defaultLanguage;

                {
                    var translationFile = localizationFolder.GetTranslationFileAsync(Language).Result;
                    var data = translationFile.ReadAllTextAsync()
                        .Result.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                    foreach (var tokenLine in data)
                    {
                        if (tokenLine.Contains(","))
                        {
                            var splitIdx = tokenLine.IndexOf(",", StringComparison.Ordinal);

                            var tokenName = tokenLine.Substring(0, splitIdx);
                            var tokenContent = "";
                            if (tokenLine.Length > tokenName.Length + 1)
                                tokenContent = tokenLine.Substring(splitIdx + 1, tokenLine.Length - splitIdx - 1);

                            if (!LocalizationTokens.ContainsKey(tokenName))
                                LocalizationTokens.Add(tokenName,
                                    new Token(tokenName, tokenContent, Language, isGameModeFile));
                        }
                    }
                }


                // -- Load default definitions, the current language may not have everything translated             
                if (Equals(Language, defaultLanguage))
                {
                    if (localizationFolder.CheckTranslationExistsAsync(defaultLanguage).Result)
                    {
                        var translationFile = localizationFolder.GetTranslationFileAsync(defaultLanguage).Result;
                        var data = translationFile.ReadAllTextAsync().Result.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                        foreach (var tokenLine in data)
                        {
                            if (tokenLine.Contains(","))
                            {
                                var splitIdx = tokenLine.IndexOf(",", StringComparison.Ordinal);

                                var tokenName = tokenLine.Substring(0, splitIdx);
                                var tokenContent = "";
                                if (tokenLine.Length > tokenName.Length + 1)
                                    tokenContent = tokenLine.Substring(splitIdx + 1, tokenLine.Length - splitIdx - 1);

                                if (!LocalizationTokens.ContainsKey(tokenName))
                                    LocalizationTokens.Add(tokenName, new Token(tokenName, tokenContent, defaultLanguage, isGameModeFile));
                            }
                        }
                    }
                }
            }
        }

        public static string GetString(string s, string defaultValue = "")
        {
            if (!LocalizationTokens.ContainsKey(s))
                return string.IsNullOrEmpty(defaultValue) ? s : defaultValue;
            Token resultToken;
            if (!LocalizationTokens.TryGetValue(s, out resultToken))
                return s;
            string result = resultToken.TokenContent;
            if (Core.Player != null)
            {
                result = result.Replace("<playername>", Core.Player.Name);
                result = result.Replace("<rivalname>", Core.Player.RivalName);
            }
            return result;
        }

        public static bool TokenExists(string tokenName) => LocalizationTokens.ContainsKey(tokenName);
    }
}