using System;

using Microsoft.Xna.Framework;

using P3D.Legacy.Core.Input;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Resources.Managers;
using P3D.Legacy.Core.Screens;
using P3D.Legacy.Core.Screens.GUI;
using P3D.Legacy.Core.Server;

namespace P3D.Legacy.Core.Network
{
    public class OnlineStatus
    {
        public static void Draw()
        {
            if (BaseJoinServerScreen.Online && BaseConnectScreen.Connected)
            {
                if (KeyBoardHandler.KeyDown(Core.KeyBindings.OnlineStatus))
                {
                    var playerList = Core.ServersManager.PlayerCollection;

                    int width = 1;
                    int height = playerList.Count;

                    if (height > 10)
                    {
                        width = Convert.ToInt32(Math.Ceiling(height / 10d));
                    }
                    height = 10;

                    int startX = Convert.ToInt32(Core.WindowSize.Width / 2 - ((width * 256) / 2));
                    int startY = 120;

                    for (var x = 1; x <= width; x++)
                    {
                        for (var y = 1; y <= height; y++)
                        {
                            Canvas.DrawRectangle(new Rectangle(startX + (x - 1) * 256, startY + (y - 1) * 40, 256, 40), new Color(0, 0, 0, 150));
                            if (playerList.Count - 1 >= (x - 1) * 10 + (y - 1))
                            {
                                string name = playerList[(x - 1) * 10 + (y - 1)].Name;
                                Color c = Color.White;
                                if (playerList[(x - 1) * 10 + (y - 1)].ServersID == Core.ServersManager.ID)
                                {
                                    name = Core.Player.Name;
                                    c = BaseChat.OwnColor;
                                }
                                else
                                {
                                    if (Core.Player.IsGameJoltSave)
                                    {
                                        string GJID = playerList[(x - 1) * 10 + (y - 1)].GameJoltId;
                                        if (!string.IsNullOrEmpty(GJID) && Core.GameJoltSave.Friends.Contains(GJID))
                                        {
                                            c = BaseChat.FriendColor;
                                        }
                                    }
                                }
                                Core.SpriteBatch.DrawString(FontManager.MainFont, name, new Vector2(startX + (x - 1) * 256 + 4, startY + (y - 1) * 40 + 6), c);

                                switch (playerList[(x - 1) * 10 + (y - 1)].BusyType)
                                {
                                    case 1:
                                        //Battle
                                        Core.SpriteBatch.Draw(TextureManager.GetTexture("Textures|emoticons", new Rectangle(48, 16, 16, 16), ""), new Rectangle(startX + (x - 1) * 256 + 222, startY + (y - 1) * 40 + 6, 32, 32), Color.White);
                                        break;
                                    case 2:
                                        //Chat
                                        Core.SpriteBatch.Draw(TextureManager.GetTexture("Textures|emoticons", new Rectangle(0, 0, 16, 16), ""), new Rectangle(startX + (x - 1) * 256 + 222, startY + (y - 1) * 40 + 6, 32, 32), Color.White);
                                        break;
                                    case 3:
                                        //AFK
                                        Core.SpriteBatch.Draw(TextureManager.GetTexture("Textures|emoticons", new Rectangle(0, 48, 16, 16), ""), new Rectangle(startX + (x - 1) * 256 + 222, startY + (y - 1) * 40 + 6, 32, 32), Color.White);
                                        break;
                                }
                            }
                            Canvas.DrawBorder(3, new Rectangle(startX + (x - 1) * 256, startY + (y - 1) * 40, 256, 40), new Color(220, 220, 220));
                        }
                    }

                    string serverName = BaseJoinServerScreen.SelectedServer.GetName();
                    int plateLength = 256;
                    if (FontManager.MainFont.MeasureString(serverName).X > 230f)
                    {
                        plateLength = 26 + Convert.ToInt32(FontManager.MainFont.MeasureString(serverName).X);
                    }

                    Canvas.DrawRectangle(new Rectangle(Convert.ToInt32(Core.WindowSize.Width / 2 - plateLength / 2), 80, plateLength, 40), new Color(0, 0, 0, 150));
                    Core.SpriteBatch.DrawString(FontManager.MainFont, serverName, new Vector2(Convert.ToInt32(Core.WindowSize.Width / 2 - plateLength / 2) + 4, 80 + 6), BaseChat.ServerColor);
                    Canvas.DrawBorder(3, new Rectangle(Convert.ToInt32(Core.WindowSize.Width / 2 - plateLength / 2), 80, plateLength, 40), new Color(220, 220, 220));
                }
            }
        }

    }
}
