using System;
using System.IO;
using System.Net;

using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Resources;

namespace P3D.Legacy.Core.Network
{
    public sealed class DownloadTexture2D
    {
        const string n_tempPath = "\\Temp\\";
        private static string[] n_tempfiles = new string[1];

        private static FileStream[] n_tempstreams = new FileStream[1];
        private static Stream n_RemoteStream(string URL)
        {
            WebRequest Request = WebRequest.Create(URL);
            Request.Method = "GET";
            return Request.GetResponse().GetResponseStream();
        }

        public static Texture2D n_Remote_Texture2D(GraphicsDevice Graphics, string URL, bool logError)
        {
            try
            {
                if (Directory.Exists(GameController.GamePath + n_tempPath) == false)
                {
                    Directory.CreateDirectory(GameController.GamePath + n_tempPath);
                }

                string Filename = GameController.GamePath + n_tempPath + DateTime.Now.Ticks.ToString() + ".tmp";

                int tI = 1;
                if (File.Exists(Filename))
                {
                    while (true)
                    {
                        if (!File.Exists(Filename + tI))
                        {
                            Filename += tI;
                        }
                        else
                        {
                            tI += 1;
                        }
                    }
                }

                var S = n_RemoteStream(URL);
                var F = File.Open(Filename, FileMode.CreateNew);

                byte[] Buffer = new byte[1];

                try
                {
                    Int32 I = 0;
                    while (true)
                    {
                        var II = S.ReadByte();
                        if (II == -1)
                            break; // TODO: might not be correct. Was : Exit While
                        Array.Resize(ref Buffer, Buffer.Length + 1);
                        Buffer[I] = Convert.ToByte(II);
                        I += 1;
                    }
                }
                catch
                {
                }

                F.Write(Buffer, 0, Buffer.Length);

                S.Close();
                F.Close();

                Array.Resize(ref n_tempstreams, n_tempstreams.Length + 1);
                n_tempstreams[n_tempstreams.Length - 1] = new FileStream(Filename, FileMode.Open);

                dynamic Result = Texture2D.FromStream(Graphics, n_tempstreams[n_tempstreams.Length - 1]);

                Array.Resize(ref n_tempfiles, n_tempfiles.Length + 1);
                n_tempfiles[n_tempfiles.Length - 1] = Filename;

                return Result;

            }
            catch (Exception ex)
            {
                if (logError == true)
                {
                    Logger.Log(Logger.LogTypes.ErrorMessage, "DownloadTexture2D.vb: Failed to download image from \"" + URL + "\"!");
                }
                return null;
            }
        }

        public static void n_CleanupTempData()
        {
            for (var i = 0; i <= n_tempstreams.Length - 1; i++)
            {
                try
                {
                    n_tempstreams[i].Close();

                    n_tempstreams[i].Dispose();
                }
                catch
                {
                }
            }

            for (var i = 0; i <= n_tempfiles.Length - 1; i++)
            {
                try
                {
                    File.Delete(n_tempfiles[i]);
                }
                catch
                {
                }
            }

            Array.Resize(ref n_tempfiles, 0);
            Array.Resize(ref n_tempstreams, 0);
        }

    }
}
