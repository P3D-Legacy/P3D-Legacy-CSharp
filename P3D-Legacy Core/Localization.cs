using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

using P3D.Legacy.Core.Data;
using P3D.Legacy.Core.Resources;

namespace P3D.Legacy.Core
{
    public static class Localization
    {
        public static CultureInfo Language = new CultureInfo("en");

        public static Dictionary<string, Token> LocalizationTokens { get; } = new Dictionary<string, Token>();
        public static void Load(CultureInfo language)
        {
            LocalizationTokens.Clear();

            Language = language;

            Logger.Debug($"Loaded language [{Language.Name}]");

            LoadTokenFile(GameMode.DefaultLocalizationsPath, false);

            if (GameModeManager.GameModeCount > 0)
            {
                var gameModeLocalizationPath = GameModeManager.ActiveGameMode.LocalizationsPath;
                if (gameModeLocalizationPath != GameMode.DefaultLocalizationsPath)
                    LoadTokenFile(gameModeLocalizationPath, true);
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
                var gameModeLocalizationPath = GameModeManager.ActiveGameMode.LocalizationsPath;
                if (gameModeLocalizationPath != GameMode.DefaultLocalizationsPath)
                    LoadTokenFile(gameModeLocalizationPath, true);
            }

            Logger.Debug("---Reloaded GameMode Tokens---");
        }

        private static void LoadTokenFile(string path, bool isGameModeFile)
        {
            var fullpath = Path.Combine(GameController.GamePath, path);

            if (Directory.GetFiles(fullpath).Length > 0)
            {
                var datPath = Path.Combine(fullpath, "Tokens_" + Language.Name + ".dat");

                if (!File.Exists(datPath))
                    Language = new CultureInfo("en");

                if (File.Exists(datPath))
                {
                    var tokensFile = File.ReadAllLines(datPath);
                    foreach (var tokenLine in tokensFile)
                    {
                        if (tokenLine.Contains(","))
                        {
                            var splitIdx = tokenLine.IndexOf(",", StringComparison.Ordinal);

                            var tokenName = tokenLine.Substring(0, splitIdx);
                            var tokenContent = "";
                            if (tokenLine.Length > tokenName.Length + 1)
                                tokenContent = tokenLine.Substring(splitIdx + 1, tokenLine.Length - splitIdx - 1);

                            if (LocalizationTokens.ContainsKey(tokenName) == false)
                                LocalizationTokens.Add(tokenName, new Token(tokenName, tokenContent, Language, isGameModeFile));
                        }
                    }
                }

                if (Language.Name != "en")
                {
                    var defPath = Path.Combine(fullpath, "Tokens_en.dat");
                    if (File.Exists(defPath))
                    {
                        var fallbackTokensFile = File.ReadAllLines(defPath);
                        foreach (var tokenLine in fallbackTokensFile)
                        {
                            if (tokenLine.Contains(","))
                            {
                                var splitIdx = tokenLine.IndexOf(",", StringComparison.Ordinal);

                                var tokenName = tokenLine.Substring(0, splitIdx);
                                var tokenContent = "";
                                if (tokenLine.Length > tokenName.Length + 1)
                                    tokenContent = tokenLine.Substring(splitIdx + 1, tokenLine.Length - splitIdx - 1);

                                if (!LocalizationTokens.ContainsKey(tokenName))
                                    LocalizationTokens.Add(tokenName, new Token(tokenName, tokenContent, new CultureInfo("en"), isGameModeFile));
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