using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using P3D.Legacy.Core.Resources;

namespace P3D.Legacy.Core.Security
{
    public class FileValidation
    {
        private static bool _validated = false;
        private static bool _valid = false;

        private const bool Runvalidation = false;
        private const int Expectedsize = 42289480;
        private const string Metahash = "RkRCMUM2QjQyOUQ3Qzk3MUEyNDIyRDc4REMyNDAwNTI=";

        public static bool IsValid(bool forceResult)
        {
            if (!_validated)
            {
                _validated = true;
                _valid = FilesValid();
            }

            return GameController.IS_DEBUG_ACTIVE == true && !forceResult || _valid;
        }

        private static bool FilesValid()
        {
            long measuredSize = 0;

            List<string> files = new List<string>();
            string[] paths = { "Content", "maps", "Scripts" };
            string[] includeExt = { ".dat", ".poke", ".lua", ".trainer" };

            if (Runvalidation)
            {
                Logger.Log(Logger.LogTypes.Debug, "FileValidation.vb: WARNING! FILE VALIDATION IS RUNNING!");
                foreach (string subFolder in paths)
                {
                    foreach (string file in System.IO.Directory.GetFiles(GameController.GamePath + "\\" + subFolder, "*.*", SearchOption.AllDirectories))
                    {
                        if (file.Contains("\\Content\\Localization\\") == false)
                        {
                            string ext = System.IO.Path.GetExtension(file);
                            if (includeExt.Contains(ext.ToLower()))
                            {
                                files.Add(file.Remove(0, GameController.GamePath.Length + 1));
                            }
                        }
                    }
                }

                string s = "";
                foreach (string f in files)
                {
                    long i = new System.IO.FileInfo(GameController.GamePath + "\\" + f).Length;
                    string hash = GetMd5FromFile(GameController.GamePath + "\\" + f);

                    FileDictionary.Add((GameController.GamePath + "\\" + f).ToLower(), new ValidationStorage(i, hash));
                    measuredSize += i;

                    if (!string.IsNullOrEmpty(s))
                    {
                        s += ",";
                    }
                    s += f + ":" + hash;
                }

                System.IO.File.WriteAllText(GameController.GamePath + "\\meta", s);
                Logger.Log(Logger.LogTypes.Debug, "FileValidation.vb: Meta created! Expected Size: " + measuredSize + "|MetaHash: " + StringObfuscation.Obfuscate(GetMd5FromFile(GameController.GamePath + "\\meta")));

                Core.GameInstance.Exit();
            }
            else
            {
                if (System.IO.File.Exists(GameController.GamePath + "\\meta") == true)
                {
                    if (GetMd5FromFile(GameController.GamePath + "\\meta") == StringObfuscation.DeObfuscate(Metahash))
                    {
                        files = System.IO.File.ReadAllText(GameController.GamePath + "\\meta").Split(Convert.ToChar(",")).ToList();
                        Logger.Debug("Meta loaded. Files to check: " + files.Count);
                    }
                    else
                    {
                        Logger.Log(Logger.LogTypes.Warning, "FileValidation.vb: Failed to load Meta (Hash incorrect)! File Validation will fail!");
                    }
                }
                else
                {
                    Logger.Log(Logger.LogTypes.Warning, "FileValidation.vb: Failed to load Meta (File not found)! File Validation will fail!");
                }

                foreach (string f in files)
                {
                    string fileName = f.Split(Convert.ToChar(":"))[0];
                    string fileHash = f.Split(Convert.ToChar(":"))[1];

                    if (System.IO.File.Exists(GameController.GamePath + "\\" + fileName))
                    {
                        long i = new System.IO.FileInfo(GameController.GamePath + "\\" + fileName).Length;
                        FileDictionary.Add((GameController.GamePath + "\\" + fileName).ToLower(), new ValidationStorage(i, fileHash));
                        measuredSize += i;
                    }
                }
            }

            if (measuredSize == Expectedsize)
            {
                return true;
            }
            return false;
        }
        
        private static Dictionary<string, ValidationStorage> FileDictionary { get; } = new Dictionary<string, ValidationStorage>();
        public static void CheckFileValid(string file, bool relativePath, string origin)
        {
            string validationResult = ValidateFile(file, relativePath);
            if (!string.IsNullOrEmpty(validationResult))
            {
                Logger.Log(Logger.LogTypes.ErrorMessage, "FileValidation.vb: Detected invalid files in a sensitive game environment. Stopping execution...");

                Exception ex = new Exception("The File Validation system detected invalid files in a sensitive game environment.");
                ex.Data.Add("File", file);
                ex.Data.Add("File Validation result", validationResult);
                ex.Data.Add("Call Origin", origin);
                ex.Data.Add("Relative Path", relativePath);

                throw ex;
            }
        }

        private static string ValidateFile(string file, bool relativePath)
        {
            if (Core.Player.IsGamejoltSave && !GameController.IS_DEBUG_ACTIVE)
            {
                string filePath = file.Replace("/", "\\");
                if (relativePath)
                    filePath = GameController.GamePath + "\\" + file;
                long i = new FileInfo(filePath).Length;

                if (File.Exists(filePath))
                {
                    if (FileDictionary.ContainsKey(filePath.ToLower()))
                    {
                        if (i != FileDictionary[filePath.ToLower()].FileSize)
                            return "File Validation rendered the file invalid. Array length invalid.";

                        if (GetMd5FromFile(filePath) != FileDictionary[filePath.ToLower()].Hash)
                            return "File Validation rendered the file invalid. File has been edited.";
                    }
                    else
                        return "The File Validation system couldn't find the requested file.";
                }
            }
            return "";
        }

        private static string GetMd5FromFile(string file)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = null;
            var sb = new StringBuilder();

            using (var st = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                hash = md5.ComputeHash(st);
            }

            foreach (var bLoopVariable in hash)
            {
                var b = bLoopVariable;
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

        private class ValidationStorage
        {
            public string Hash { get; }
            public long FileSize { get; }

            public ValidationStorage(long fileSize, string hash)
            {
                FileSize = fileSize;
                Hash = hash;
            }

            public bool CheckValidation(long fileSize) => fileSize == FileSize;
            public bool CheckValidation(long fileSize, string hash) => FileSize == fileSize && Hash == hash;
        }

    }
}
