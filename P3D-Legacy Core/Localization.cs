using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Resources.Managers;
using P3D.Legacy.Core.Storage;
using P3D.Legacy.Core.Storage.Folders;
using P3D.Legacy.Shared.Data;
using P3D.Legacy.Shared.Storage.Files;

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
        private static List<BaseTranslationFile> LocalizationFiles { get; set; } = new List<BaseTranslationFile>();

        public static void Load(CultureInfo language)
        {
            LocalizationTokens.Clear();

            Language = language;

            Logger.Debug($"Loaded language [{Language.Name}]");

            LoadTokenFile(new LocalizationsFolder(), false); // -- Load Game Translation.

            if (GameModeManager.GameModeCount > 0)
            {
                var gameModeLocalizationPath = GameModeManager.ActiveGameMode.LocalizationFolder;
                if (gameModeLocalizationPath != new LocalizationsFolder())
                    LoadTokenFile(gameModeLocalizationPath, true); // -- Load GameMode Translation.
            }
        }

        public static void ReloadGameModeTokens()
        {
            return;
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
                if (gameModeLocalizationPath != new LocalizationsFolder())
                    LoadTokenFile(gameModeLocalizationPath, true);
            }

            Logger.Debug("---Reloaded GameMode Tokens---");
        }

        private static void LoadTokenFile(LocalizationsFolder localizationsFolder, bool isGameModeFile)
        {
            var defaultLanguage = new CultureInfo("en");
            var localizationInfo = new LocalizationInfo(Language);

            if (!localizationsFolder.CheckTranslationExists(localizationInfo))
                Language = defaultLanguage;

            LocalizationFiles.AddRange(localizationsFolder.GetTranslationFolder(localizationInfo).GetTranslationFiles().ToList());
            if (!Equals(Language, defaultLanguage))
                LocalizationFiles.AddRange(localizationsFolder.GetTranslationFolder(new LocalizationInfo(defaultLanguage)).GetTranslationFiles().ToList());


            /*
            {
                foreach (var tokenLine in localizationsFolder.GetTranslationFolder(localizationInfo).GetTranslationFile(Language).ReadAllLines())
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
            if (!Equals(Language, defaultLanguage))
            {
                if (localizationFolder.CheckTranslationExists(defaultLanguage))
                {
                    foreach (var tokenLine in localizationFolder.GetTranslationFile(defaultLanguage).ReadAllLines())
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
            */
        }

        public static string GetString(string s, string defaultValue = "")
        {
            foreach (var localizationFile in LocalizationFiles)
            {
                var token = localizationFile.GetString(s);


                if (Core.Player != null)
                {
                    token = token.Replace("<playername>", Core.Player.Name);
                    token = token.Replace("<rivalname>", Core.Player.RivalName);
                }
                if (token == "")
                    break;
                return token;
            }
            return string.IsNullOrEmpty(defaultValue) ? s : defaultValue;

            /*
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
            */
        }

        //public static bool TokenExists(string tokenName) => LocalizationTokens.ContainsKey(tokenName);
        public static bool TokenExists(string tokenName) => false;
    }
}