using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using P3D.Legacy.Core.Entities;
using P3D.Legacy.Core.Entities.Other;
using P3D.Legacy.Core.Extensions;
using P3D.Legacy.Core.Resources;
using P3D.Legacy.Core.Resources.Sound;
using P3D.Legacy.Core.Screens;
using P3D.Legacy.Core.Security;
using P3D.Legacy.Core.World;
using PCLExt.FileStorage;

namespace P3D.Legacy.Core.Pokemon
{
    public class PokemonInteractions
    {

        //Procedure:
        //1. If has a status condition, return that
        //1.1 If a Pokémon has low HP, return reaction (40%)
        //2. If the Pokémon holds a pickup item, give that (if friendship is high enough, else say 50% that it doesn't want to give item, 50% proceed)
        //3. Return Special Location Text (found in Content\Data\interactions.dat) (60%)
        //4. Friendship based/random text

        //Friendship levels:
        //1: <= 50 (hate)
        //2: >50 <= 150 (neutral)
        //3: >150 <= 200 (like)
        //4: >200 <=250 (loyal)
        //5: >250 (love)

        public enum FriendshipLevels
        {
            Hate,
            Neutral,
            Likes,
            Loyal,
            Love
        }

        public static string GetScriptString(BasePokemon p, Vector3 cPosition, int facing)
        {
            if (PickupItemID > -1)
            {
                if (PickupIndividualValue == p.IndividualValue)
                {
                    return GenerateItemReaction(p, cPosition, facing);
                }
                else
                {
                    PickupItemID = -1;
                    PickupIndividualValue = "";
                }
            }

            ReactionContainer reaction = GetReaction(p);

            Vector2 offset = new Vector2(0, 0);
            switch (facing)
            {
                case 0:
                    offset.Y = -0.02f;
                    break;
                case 1:
                    offset.X = -0.02f;
                    break;
                case 2:
                    offset.Y = 0.01f;
                    break;
                case 3:
                    offset.X = 0.01f;
                    break;
            }

            Vector2 newPosition = new Vector2(0, 1);

            string s = "version=2" + Environment.NewLine + "@pokemon.cry(" + p.Number + ")" + Environment.NewLine;

            if (((BaseOverworldCamera)Screen.Camera).ThirdPerson == false)
            {
                if (reaction.HasNotification == true)
                {
                    s += "@camera.activatethirdperson" + Environment.NewLine + "@camera.setposition(" + newPosition.X + ",1," + newPosition.Y + ")" + Environment.NewLine;

                    s += "@entity.showmessagebulb(" + Convert.ToInt32(reaction.Notification).ToString(NumberFormatInfo.InvariantInfo) + "|" + cPosition.X + offset.X + "|" + cPosition.Y + 0.7f + "|" + cPosition.Z + offset.Y + ")" + Environment.NewLine;

                    s += "@camera.deactivatethirdperson" + Environment.NewLine;
                }
                s += "@text.show(" + reaction.GetMessage(p) + ")" + Environment.NewLine;
            }
            else
            {
                float preYaw = Screen.Camera.Yaw;
                if (reaction.HasNotification)
                {
                    s += "@camera.setyaw(" + ((BaseOverworldCamera)Screen.Camera).GetAimYawFromDirection(Screen.Camera.GetPlayerFacingDirection()) + ")" + Environment.NewLine;
                    s += "@camera.setposition(" + newPosition.X + ",1," + newPosition.Y + ")" + Environment.NewLine;
                    s += "@entity.showmessagebulb(" + Convert.ToInt32(reaction.Notification) + "|" + cPosition.X + offset.X + "|" + cPosition.Y + 0.7f + "|" + cPosition.Z + offset.Y + ")" + Environment.NewLine;

                    s += "@camera.deactivatethirdperson" + Environment.NewLine;
                }
                s += "@text.show(" + reaction.GetMessage(p) + ")" + Environment.NewLine;
                s += "@camera.activatethirdperson" + Environment.NewLine;
                s += "@camera.setyaw(" + preYaw + ")" + Environment.NewLine;
            }
            s += ":end";

            return s;
        }

        private static string GenerateItemReaction(BasePokemon p, Vector3 cPosition, int facing)
        {
            string message = "It looks like your Pokémon~holds on to something.*Do you want to~take it?";

            Vector2 offset = new Vector2(0, 0);
            switch (facing)
            {
                case 0:
                    offset.Y = -0.02f;
                    break;
                case 1:
                    offset.X = -0.02f;
                    break;
                case 2:
                    offset.Y = 0.01f;
                    break;
                case 3:
                    offset.X = 0.01f;
                    break;
            }

            Vector2 newPosition = new Vector2(0, 1);

            Item item = Item.GetItemByID(PickupItemID);

            string s = "version=2" + Environment.NewLine + "@pokemon.cry(" + p.Number + ")" + Environment.NewLine;

            if (((BaseOverworldCamera)Screen.Camera).ThirdPerson == false)
            {
                s += "@camera.activatethirdperson" + Environment.NewLine + "@camera.setposition(" + newPosition.X + ",1," + newPosition.Y + ")" + Environment.NewLine;

                s += "@entity.showmessagebulb(" + Convert.ToInt32(BaseMessageBulb.NotifcationTypes.Question).ToString(NumberFormatInfo.InvariantInfo) + "|" + cPosition.X + offset.X + "|" + cPosition.Y + 0.7f + "|" + cPosition.Z + offset.Y + ")" + Environment.NewLine;

                s += "@camera.deactivatethirdperson" + Environment.NewLine;
                s += "@text.show(" + message + ")" + Environment.NewLine + "@options.show(Yes,No)" + Environment.NewLine + ":when:Yes" + Environment.NewLine + "@text.show(Your Pokémon handed over~the " + item.Name + "!)" + Environment.NewLine + "@item.give(" + PickupItemID + ",1)" + Environment.NewLine + "@item.messagegive(" + PickupItemID + ",1)" + Environment.NewLine + ":when:No" + Environment.NewLine + "@text.show(Your Pokémon kept~the item happily.)" + Environment.NewLine + "@pokemon.addfriendship(0,10)" + Environment.NewLine + ":endwhen" + Environment.NewLine;
            }
            else
            {
                s += "@camera.setposition(" + newPosition.X + ",1," + newPosition.Y + ")" + Environment.NewLine;
                s += "@entity.showmessagebulb(" + Convert.ToInt32(BaseMessageBulb.NotifcationTypes.Question).ToString(NumberFormatInfo.InvariantInfo) + "|" + cPosition.X + offset.X + "|" + cPosition.Y + 0.7f + "|" + cPosition.Z + offset.Y + ")" + Environment.NewLine;

                s += "@camera.deactivatethirdperson" + Environment.NewLine;

                s += "@text.show(" + message + ")" + Environment.NewLine + "@options.show(Yes,No)" + Environment.NewLine + ":when:Yes" + Environment.NewLine + "@text.show(Your Pokémon handed over~the " + item.Name + "!)" + Environment.NewLine + "@item.give(" + PickupItemID + ",1)" + Environment.NewLine + "@item.messagegive(" + PickupItemID + ",1)" + Environment.NewLine + ":when:No" + Environment.NewLine + "@text.show(Your Pokémon kept~the item happily.)" + Environment.NewLine + "@pokemon.addfriendship(0,10)" + Environment.NewLine + ":endwhen" + Environment.NewLine;
                s += "@camera.activatethirdperson" + Environment.NewLine;
            }
            s += ":end";

            PickupItemID = -1;
            PickupIndividualValue = "";

            return s;
        }

        private static ReactionContainer GetReaction(BasePokemon p)
        {
            FriendshipLevels FriendshipLevel = GetFriendshipLevel(p);

            ReactionContainer reaction = null;

            if (p.Status != BasePokemon.StatusProblems.None)
            {
                //Return status condition text
                reaction = GetStatusConditionReaction(p);
            }

            if (reaction == null && ((p.HP / p.MaxHP) * 100) <= 15)
            {
                if (Core.Random.Next(0, 100) < 40)
                {
                    reaction = GetLowHPReaction(p);
                }
            }

            //Get special place reaction  (60%)
            if (reaction == null)
            {
                if (Core.Random.Next(0, 100) < 60)
                {
                    reaction = GetSpecialReaction(p);
                }
            }

            //Friendship based:
            //If friendship level is hate, return hate.
            //If friendship level is above hate, never return hate.
            //If friendship level is neutral, only return neutral.
            //If friendship level is like, return 60% like, 40% neutral
            //If friendship level is loyal, return 60% loyal, 15% like, 25% neutral
            //If friendship level is love, return 55% love, 10% loyal, 10% like, 25% neutral
            if (reaction == null)
            {
                int r = Core.Random.Next(0, 100);
                switch (FriendshipLevel)
                {
                    case FriendshipLevels.Hate:
                        reaction = GetHateReaction(p);
                        break;
                    case FriendshipLevels.Neutral:
                        reaction = GetNeutralReaction(p);
                        break;
                    case FriendshipLevels.Likes:
                        if (r < 60)
                        {
                            reaction = GetLikeReaction(p);
                        }
                        else
                        {
                            reaction = GetNeutralReaction(p);
                        }
                        break;
                    case FriendshipLevels.Loyal:
                        if (r < 60)
                        {
                            reaction = GetLoyalReaction(p);
                        }
                        else if (r >= 60 && r < 75)
                        {
                            reaction = GetLikeReaction(p);
                        }
                        else
                        {
                            reaction = GetNeutralReaction(p);
                        }
                        break;
                    case FriendshipLevels.Love:
                        if (r < 55)
                        {
                            reaction = GetLoveReaction(p);
                        }
                        else if (r >= 55 && r < 65)
                        {
                            reaction = GetLoyalReaction(p);
                        }
                        else if (r >= 65 && r < 75)
                        {
                            reaction = GetLikeReaction(p);
                        }
                        else
                        {
                            reaction = GetNeutralReaction(p);
                        }
                        break;
                }
            }

            return reaction;
        }

        private static FriendshipLevels GetFriendshipLevel(BasePokemon p)
        {
            int f = p.Friendship;
            if (f <= 50)
            {
                return FriendshipLevels.Hate;
            }
            else if (f > 50 && f <= 120)
            {
                return FriendshipLevels.Neutral;
            }
            else if (f > 120 && f <= 200)
            {
                return FriendshipLevels.Likes;
            }
            else if (f > 200 && f <= 245)
            {
                return FriendshipLevels.Loyal;
            }
            else
            {
                return FriendshipLevels.Love;
            }
        }

        private static ReactionContainer GetStatusConditionReaction(BasePokemon p)
        {
            switch (p.Status)
            {
                case BasePokemon.StatusProblems.BadPoison:
                case BasePokemon.StatusProblems.Poison:
                    return new ReactionContainer("<name> is shivering~with the effects of being~poisoned.", BaseMessageBulb.NotifcationTypes.Poisoned);
                case BasePokemon.StatusProblems.Burn:
                    return new ReactionContainer("<name>'s burn~looks painful!", BaseMessageBulb.NotifcationTypes.Poisoned);
                case BasePokemon.StatusProblems.Freeze:
                    switch (Core.Random.Next(0, 2))
                    {
                        case 0:
                            return new ReactionContainer("<name> seems very cold!", BaseMessageBulb.NotifcationTypes.Poisoned);
                        case 1:
                            return new ReactionContainer(".....Your Pokémon seems~a little cold.", BaseMessageBulb.NotifcationTypes.Poisoned);
                    }
                    break;
                case BasePokemon.StatusProblems.Paralyzed:
                    return new ReactionContainer("<name> is trying~very hard to keep~up with you...", BaseMessageBulb.NotifcationTypes.Poisoned);
                case BasePokemon.StatusProblems.Sleep:
                    switch (Core.Random.Next(0, 3))
                    {
                        case 0:
                            return new ReactionContainer("<name> seems~a little tiered.", BaseMessageBulb.NotifcationTypes.Poisoned);
                        case 1:
                            return new ReactionContainer("<name> is somehow~fighting off sleep...", BaseMessageBulb.NotifcationTypes.Poisoned);
                        case 2:
                            return new ReactionContainer("<name> yawned~very loudly!", BaseMessageBulb.NotifcationTypes.Poisoned);
                    }
                    break;
            }
            return new ReactionContainer("<name> is trying~very hard to keep~up with you...", BaseMessageBulb.NotifcationTypes.Poisoned);
        }

        private static ReactionContainer GetLowHPReaction(BasePokemon p)
        {
            switch (Core.Random.Next(0, 2))
            {
                case 0:
                    return new ReactionContainer("<name> is going~to fall down!", BaseMessageBulb.NotifcationTypes.Exclamation);
                case 1:
                    return new ReactionContainer("<name> seems to~be about to fall over!", BaseMessageBulb.NotifcationTypes.Exclamation);
            }
            return new ReactionContainer("<name> seems to~be about to fall over!", BaseMessageBulb.NotifcationTypes.Exclamation);
        }

        private static ReactionContainer GetSpecialReaction(BasePokemon p)
        {
            List<ReactionContainer> matching = new List<ReactionContainer>();

            foreach (ReactionContainer spReaction in SpecialReactionList)
            {
                if (spReaction.Match(p) == true)
                {
                    matching.Add(spReaction);
                }
            }

            if (matching.Count > 0)
            {
                List<int> chances = new List<int>();
                foreach (var r_loopVariable in matching)
                {
                    var r = r_loopVariable;
                    chances.Add(r.Probability);
                }

                int index = GetRandomChance(chances);

                return matching[index];
            }

            return null;
        }
        // TODO
        private static int GetRandomChance(List<int> chances)
        {
            int totalNumber = 0;
            foreach (int c in chances)
            {
                totalNumber += c;
            }

            int r = Core.Random.Next(0, totalNumber + 1);

            int x = 0;
            for (var i = 0; i <= chances.Count - 1; i++)
            {
                x += chances[i];
                if (r <= x)
                {
                    return i;
                }
            }

            return -1;
        }

        private static ReactionContainer GetHateReaction(BasePokemon p)
        {
            ReactionContainer r = null;
            while (r == null)
            {
                switch (Core.Random.Next(0, 17))
                {
                    case 0:
                        r = new ReactionContainer("<name> is doing~it's best to keep up~with you.", BaseMessageBulb.NotifcationTypes.Unhappy);
                        break;
                    case 1:
                        r = new ReactionContainer("<name> is somehow~forcing itself to keep going.", BaseMessageBulb.NotifcationTypes.Unsure);
                        break;
                    case 2:
                        r = new ReactionContainer("<name> is staring~patiantly at nothing at all.", BaseMessageBulb.NotifcationTypes.Unsure);
                        break;
                    case 3:
                        r = new ReactionContainer("<name> is staring~intently into the distance.", BaseMessageBulb.NotifcationTypes.Waiting);
                        break;
                    case 4:
                        r = new ReactionContainer("<name> is dizzy...", BaseMessageBulb.NotifcationTypes.Unhappy);
                        break;
                    case 5:
                        r = new ReactionContainer("<name> is stepping~on your feet!", BaseMessageBulb.NotifcationTypes.Waiting);
                        break;
                    case 6:
                        r = new ReactionContainer("<name> seems~unhappy somehow...", BaseMessageBulb.NotifcationTypes.Unhappy);
                        break;
                    case 7:
                        r = new ReactionContainer("<name> is making~an unhappy face.", BaseMessageBulb.NotifcationTypes.Unhappy);
                        break;
                    case 8:
                        r = new ReactionContainer("<name> seems~uneasy and is poking~<player.name>.", BaseMessageBulb.NotifcationTypes.Unsure);
                        break;
                    case 9:
                        r = new ReactionContainer("<name> is making~a face like its angry!", BaseMessageBulb.NotifcationTypes.Angry);
                        break;
                    case 10:
                        r = new ReactionContainer("<name> seems to be~angry for some reason.", BaseMessageBulb.NotifcationTypes.Angry);
                        break;
                    case 11:
                        r = new ReactionContainer("Your Pokémon turned to face~the other way, showing a~defiant expression. ", BaseMessageBulb.NotifcationTypes.Unsure);
                        break;
                    case 12:
                        r = new ReactionContainer("<name> is looking~down steadily...", BaseMessageBulb.NotifcationTypes.Waiting);
                        break;
                    case 13:
                        r = new ReactionContainer("Your Pokémon is staring~intently at nothing...", BaseMessageBulb.NotifcationTypes.Waiting);
                        break;
                    case 14:
                        r = new ReactionContainer("Your Pokémon turned to~face the other way,~showing a defiant expression.", BaseMessageBulb.NotifcationTypes.Unhappy);
                        break;
                    case 15:
                        r = new ReactionContainer("<name> seems~a bit nervous...", BaseMessageBulb.NotifcationTypes.Unsure);
                        break;
                    case 16:
                        r = new ReactionContainer("Your Pokémon stumbled~and nearly fell!", BaseMessageBulb.NotifcationTypes.Exclamation);
                        break;
                    case 17:
                        r = new ReactionContainer("<name> is having~a hard time keeping up.", BaseMessageBulb.NotifcationTypes.Unsure);
                        break;
                }
            }

            return r;
        }

        private static ReactionContainer GetNeutralReaction(BasePokemon p)
        {
            ReactionContainer r = null;
            while (r == null)
            {
                switch (Core.Random.Next(0, 53))
                {
                    case 0:
                        r = new ReactionContainer("<name> is happy~but shy.", BaseMessageBulb.NotifcationTypes.Friendly);
                        break;
                    case 1:
                        r = new ReactionContainer("<name> puts in~extra effort.", BaseMessageBulb.NotifcationTypes.Friendly);
                        break;
                    case 2:
                        if (IsOutside() == true)
                        {
                            r = new ReactionContainer("<name> is smelling~the scents of the~surrounding air", BaseMessageBulb.NotifcationTypes.Friendly);
                        }
                        break;
                    case 3:
                        if (IsOutside() == true)
                        {
                            r = new ReactionContainer("Your Pokémon has caught~the scent of smoke.", BaseMessageBulb.NotifcationTypes.Friendly);
                        }
                        break;
                    case 4:
                        if (NPCAround() == true)
                        {
                            r = new ReactionContainer("<name> greeted everyone!", BaseMessageBulb.NotifcationTypes.CatFace);
                        }
                        break;
                    case 5:
                        if (IsOutside() == true)
                        {
                            r = new ReactionContainer("<name> is wandering~around and listening~to the different sounds.", BaseMessageBulb.NotifcationTypes.Note);
                        }
                        break;
                    case 6:
                        r = new ReactionContainer("<name> looks very~interested!", BaseMessageBulb.NotifcationTypes.Friendly);
                        break;
                    case 7:
                        if (IsOutside() == true)
                        {
                            r = new ReactionContainer("<name> is steadily~poking at the ground.", BaseMessageBulb.NotifcationTypes.Waiting);
                        }
                        break;
                    case 8:
                        r = new ReactionContainer("Your Pokémon is looking~around restlessly.", BaseMessageBulb.NotifcationTypes.Note);
                        break;
                    case 9:
                        if (IsOutside() == true)
                        {
                            r = new ReactionContainer("<name> seems dazzled~after seeing the sky.", BaseMessageBulb.NotifcationTypes.Waiting);
                        }
                        break;
                    case 10:
                        r = new ReactionContainer("<name> is gazing~around restlessly!", BaseMessageBulb.NotifcationTypes.Waiting);
                        break;
                    case 11:
                        if (TrainerAround() == true)
                        {
                            r = new ReactionContainer("<name> let out~a battle cry!", BaseMessageBulb.NotifcationTypes.Shouting);
                        }
                        break;
                    case 12:
                        if (TrainerAround() == true && p.IsType(Element.Types.Fire) == true)
                        {
                            r = new ReactionContainer("<name> is vigorously~breathing fire!", BaseMessageBulb.NotifcationTypes.Exclamation);
                        }
                        break;
                    case 13:
                        if (TrainerAround() == true || NPCAround() == true)
                        {
                            r = new ReactionContainer("<name> is on~the lookout!", BaseMessageBulb.NotifcationTypes.Exclamation);
                        }
                        break;
                    case 14:
                        if (TrainerAround() == true)
                        {
                            r = new ReactionContainer("<name> roared!", BaseMessageBulb.NotifcationTypes.Exclamation);
                        }
                        break;
                    case 15:
                        if (TrainerAround() == true)
                        {
                            r = new ReactionContainer("<name> let out a roar!", BaseMessageBulb.NotifcationTypes.Exclamation);
                        }
                        break;
                    case 16:
                        r = new ReactionContainer("<name> is surveying~the area...", BaseMessageBulb.NotifcationTypes.Waiting);
                        break;
                    case 17:
                        if (IsInside() == true)
                        {
                            r = new ReactionContainer("<name> is sniffing~at the floor.", BaseMessageBulb.NotifcationTypes.Question);
                        }
                        break;
                    case 18:
                        r = new ReactionContainer("<name> is peering~down.", BaseMessageBulb.NotifcationTypes.Question);
                        break;
                    case 19:
                        r = new ReactionContainer("<name> seems~to be wandering around.", BaseMessageBulb.NotifcationTypes.Note);
                        break;
                    case 20:
                        r = new ReactionContainer("<name> is looking~around absentmindedly.", BaseMessageBulb.NotifcationTypes.Waiting);
                        break;
                    case 21:
                        r = new ReactionContainer("<name> is relaxing~comfortably.", BaseMessageBulb.NotifcationTypes.Friendly);
                        break;
                    case 22:
                        if (IsInside() == true)
                        {
                            r = new ReactionContainer("<name> is sniffing~at the floor.", BaseMessageBulb.NotifcationTypes.Waiting);
                        }
                        break;
                    case 23:
                        if (IsOutside() == true)
                        {
                            r = new ReactionContainer("<name> seems to~relax as it hears the~sound of rustling leaves...", BaseMessageBulb.NotifcationTypes.Friendly);
                        }
                        break;
                    case 24:
                        if (IsOutside() == true)
                        {
                            r = new ReactionContainer("<name> seems to~be listening to the~sound of rustling leaves...", BaseMessageBulb.NotifcationTypes.Friendly);
                        }
                        break;
                    case 25:
                        if (WaterAround() == true)
                        {
                            r = new ReactionContainer("Your Pokémon is playing around~and splashing in the water!", BaseMessageBulb.NotifcationTypes.Happy);
                        }
                        break;
                    case 26:
                        if (IsOutside() == true)
                        {
                            r = new ReactionContainer("<name> is looking~up at the sky.", BaseMessageBulb.NotifcationTypes.Waiting);
                        }
                        break;
                    case 27:
                        if (IsOutside() == true && BaseWorld.GetTime == DayTime.Night && BaseWorld.GetCurrentRegionWeather() == WeatherEnum.Clear)
                        {
                            r = new ReactionContainer("Your Pokémon is happily~gazing at the beautiful,~starry sky!", BaseMessageBulb.NotifcationTypes.Waiting);
                        }
                        break;
                    case 28:
                        r = new ReactionContainer("<name> seems to be~enjoying this a little bit!", BaseMessageBulb.NotifcationTypes.Note);
                        break;
                    case 29:
                        if (IsInside() == true)
                        {
                            r = new ReactionContainer("<name> is looking~up at the ceiling.", BaseMessageBulb.NotifcationTypes.Note);
                        }
                        break;
                    case 30:
                        if (IsOutside() == true && BaseWorld.GetTime == DayTime.Night)
                        {
                            r = new ReactionContainer("Your Pokémon is staring~spellbound at the night sky!", BaseMessageBulb.NotifcationTypes.Friendly);
                        }
                        break;
                    case 31:
                        r = new ReactionContainer("<name> is in~danger of falling over!", BaseMessageBulb.NotifcationTypes.Exclamation);
                        break;
                    case 32:
                        if (!string.IsNullOrEmpty(p.NickName))
                        {
                            r = new ReactionContainer("<name> doesn't~seem to be used to its~own name yet.", BaseMessageBulb.NotifcationTypes.Exclamation);
                        }
                        break;
                    case 33:
                        if (IsInside() == true)
                        {
                            r = new ReactionContainer("<name> slipped~on the floor and seems~likely to fall!", BaseMessageBulb.NotifcationTypes.Exclamation);
                        }
                        break;
                    case 34:
                        if (TrainerAround() == true || GrassAround() == true)
                        {
                            r = new ReactionContainer("<name> feels something~and is howling!", BaseMessageBulb.NotifcationTypes.Exclamation);
                        }
                        break;
                    case 35:
                        if (p.HP == p.MaxHP && p.Status == BasePokemon.StatusProblems.None)
                        {
                            r = new ReactionContainer("<name> seems~refreshed!", BaseMessageBulb.NotifcationTypes.Friendly);
                        }
                        break;
                    case 36:
                        if (p.HP == p.MaxHP && p.Status == BasePokemon.StatusProblems.None)
                        {
                            r = new ReactionContainer("<name> feels~refreshed.", BaseMessageBulb.NotifcationTypes.Friendly);
                        }
                        break;
                    case 37:
                        if (ItemAround() == true)
                        {
                            r = new ReactionContainer("<name> seems to~have found something!", BaseMessageBulb.NotifcationTypes.Exclamation);
                        }
                        break;
                    case 38:
                        if (TrainerAround() == true || GrassAround() == true)
                        {
                            r = new ReactionContainer("<name> suddenly~turned around and~started barking!", BaseMessageBulb.NotifcationTypes.Exclamation);
                        }
                        break;
                    case 39:
                        if (TrainerAround() == true || GrassAround() == true)
                        {
                            r = new ReactionContainer("<name> suddenly~turned around!", BaseMessageBulb.NotifcationTypes.Exclamation);
                        }
                        break;
                    case 40:
                        if (IsOutside() == true)
                        {
                            r = new ReactionContainer("<name> looked up~at the sky and shouted loudly!", BaseMessageBulb.NotifcationTypes.Exclamation);
                        }
                        break;
                    case 41:
                        r = new ReactionContainer("Your Pokémon was surprised~that you suddenly spoke to it!", BaseMessageBulb.NotifcationTypes.Exclamation);
                        break;
                    case 42:
                        if ((p.Item != null))
                        {
                            r = new ReactionContainer("<name> almost forgot~it was holding~that " + p.Item.Name + "!", BaseMessageBulb.NotifcationTypes.Question);
                        }
                        break;
                    case 43:
                        if (IceAround() == true)
                        {
                            r = new ReactionContainer("Oh!~its slipping and came~over here for support.", BaseMessageBulb.NotifcationTypes.Exclamation);
                        }
                        break;
                    case 44:
                        if (IceAround() == true)
                        {
                            r = new ReactionContainer("Your Pokémon almost slipped~and fell over!", BaseMessageBulb.NotifcationTypes.Exclamation);
                        }
                        break;
                    case 45:
                        r = new ReactionContainer("<name> sensed something~strange and was surprised!", BaseMessageBulb.NotifcationTypes.Question);
                        break;
                    case 46:
                        r = new ReactionContainer("Your Pokémon is looking~around restlessly for~something.", BaseMessageBulb.NotifcationTypes.Question);
                        break;
                    case 47:
                        r = new ReactionContainer("Your Pokémon wasn't watching~where it was going and~ran into you!", BaseMessageBulb.NotifcationTypes.Friendly);
                        break;
                    case 48:
                        if (ItemAround() == true)
                        {
                            r = new ReactionContainer("Sniff, sniff!~Is there something nearby?", BaseMessageBulb.NotifcationTypes.Question);
                        }
                        break;
                    case 49:
                        r = new ReactionContainer("<name> is wandering~around and searching~for something.", BaseMessageBulb.NotifcationTypes.Question);
                        break;
                    case 50:
                        r = new ReactionContainer("<name> is sniffing~at <player.name>.", BaseMessageBulb.NotifcationTypes.Friendly);
                        break;
                    case 51:
                        if (IsOutside() == true && BaseWorld.GetCurrentRegionWeather() == WeatherEnum.Rain && GrassAround() == true)
                        {
                            r = new ReactionContainer("<name> is taking shelter in the grass from the rain!", BaseMessageBulb.NotifcationTypes.Waiting);
                        }
                        break;
                    case 52:
                        if (IsOutside() == true && BaseWorld.GetCurrentRegionWeather() == WeatherEnum.Rain && GrassAround() == true)
                        {
                            r = new ReactionContainer("<name> is splashing~around in the wet grass", BaseMessageBulb.NotifcationTypes.Note);
                        }
                        break;
                }
            }
            return r;
        }

        private static ReactionContainer GetLikeReaction(BasePokemon p)
        {
            ReactionContainer r = null;
            while (r == null)
            {
                switch (Core.Random.Next(0, 28))
                {
                    case 0:
                        if (IsOutside() == true && BaseWorld.GetCurrentRegionWeather() == WeatherEnum.Clear)
                        {
                            r = new ReactionContainer("Your Pokémon seems happy~about the great weather!", BaseMessageBulb.NotifcationTypes.Happy);
                        }
                        break;
                    case 1:
                        r = new ReactionContainer("<name> is coming along~happily.", BaseMessageBulb.NotifcationTypes.Friendly);
                        break;
                    case 2:
                        r = new ReactionContainer("<name> is composed!", BaseMessageBulb.NotifcationTypes.Friendly);
                        break;
                    case 3:
                        if (p.HP == p.MaxHP)
                        {
                            r = new ReactionContainer("<name> is glowing~with health!", BaseMessageBulb.NotifcationTypes.Note);
                        }
                        break;
                    case 4:
                        r = new ReactionContainer("<name> looks~very happy!", BaseMessageBulb.NotifcationTypes.Happy);
                        break;
                    case 5:
                        if (p.HP == p.MaxHP)
                        {
                            r = new ReactionContainer("<name> is full~of life!", BaseMessageBulb.NotifcationTypes.Note);
                        }
                        break;
                    case 6:
                        r = new ReactionContainer("<name> is very~eager!", BaseMessageBulb.NotifcationTypes.Friendly);
                        break;
                    case 7:
                        r = new ReactionContainer("<name> gives you~a happy look and a smile!", BaseMessageBulb.NotifcationTypes.Happy);
                        break;
                    case 8:
                        r = new ReactionContainer("<name> seems very~happy to see you!", BaseMessageBulb.NotifcationTypes.Friendly);
                        break;
                    case 9:
                        r = new ReactionContainer("<name> faced this~way and grinned!", BaseMessageBulb.NotifcationTypes.CatFace);
                        break;
                    case 10:
                        r = new ReactionContainer("<name> spun around~in a circle!", BaseMessageBulb.NotifcationTypes.Note);
                        break;
                    case 11:
                        r = new ReactionContainer("<name> is looking~this way and smiling.", BaseMessageBulb.NotifcationTypes.Friendly);
                        break;
                    case 12:
                        r = new ReactionContainer("<name> is very~eager...", BaseMessageBulb.NotifcationTypes.Waiting);
                        break;
                    case 13:
                        r = new ReactionContainer("<name> is focusing~its attention on you!", BaseMessageBulb.NotifcationTypes.Exclamation);
                        break;
                    case 14:
                        r = new ReactionContainer("<name> focused~with a sharp gaze!", BaseMessageBulb.NotifcationTypes.Exclamation);
                        break;
                    case 15:
                        r = new ReactionContainer("<name> is looking~at <player.name>'s footprints.", BaseMessageBulb.NotifcationTypes.Question);
                        break;
                    case 16:
                        r = new ReactionContainer("<name> is staring~straight into <player.name>'s~eyes.", BaseMessageBulb.NotifcationTypes.Friendly);
                        break;
                    case 17:
                        if (p.BaseSpeed >= 100)
                        {
                            r = new ReactionContainer("<name> is showing off~its agility!", BaseMessageBulb.NotifcationTypes.Note);
                        }
                        break;
                    case 18:
                        r = new ReactionContainer("<name> is moving~around happily!", BaseMessageBulb.NotifcationTypes.Note);
                        break;
                    case 19:
                        r = new ReactionContainer("<name> is steadily~keeping up with you!", BaseMessageBulb.NotifcationTypes.Friendly);
                        break;
                    case 20:
                        r = new ReactionContainer("<name> seems to~want to play with~<player.name>!", BaseMessageBulb.NotifcationTypes.Note);
                        break;
                    case 21:
                        r = new ReactionContainer("<name> is singing~and humming.", BaseMessageBulb.NotifcationTypes.Note);
                        break;
                    case 22:
                        r = new ReactionContainer("<name> is playfully~nibbling at the ground.", BaseMessageBulb.NotifcationTypes.Friendly);
                        break;
                    case 23:
                        r = new ReactionContainer("<name> is nipping~at your feet!", BaseMessageBulb.NotifcationTypes.Note);
                        break;
                    case 24:
                        if (p.BaseAttack >= 100 || p.BaseSpAttack >= 100)
                        {
                            r = new ReactionContainer("<name> is working~hard to show off~its mighty power!", BaseMessageBulb.NotifcationTypes.Exclamation);
                        }
                        break;
                    case 25:
                        r = new ReactionContainer("<name> is cheerful!", BaseMessageBulb.NotifcationTypes.Note);
                        break;
                    case 26:
                        r = new ReactionContainer("<name> bumped~into <player.name>!", BaseMessageBulb.NotifcationTypes.Exclamation);
                        break;
                    case 27:
                        if (IsCave() == true && p.Level < 20)
                        {
                            r = new ReactionContainer("<name> is scared~and snuggled up~to <player.name>!", BaseMessageBulb.NotifcationTypes.Exclamation);
                        }
                        break;
                }
            }
            return r;
        }

        private static ReactionContainer GetLoyalReaction(BasePokemon p)
        {
            ReactionContainer r = null;
            while (r == null)
            {
                switch (Core.Random.Next(0, 21))
                {
                    case 0:
                        r = new ReactionContainer("<name> began poking~you in the stomach!", BaseMessageBulb.NotifcationTypes.CatFace);
                        break;
                    case 1:
                        r = new ReactionContainer("<name> seems to be~feeling great about~walking with you!", BaseMessageBulb.NotifcationTypes.Heart);
                        break;
                    case 2:
                        r = new ReactionContainer("<name> is still~feeling great!", BaseMessageBulb.NotifcationTypes.Happy);
                        break;
                    case 3:
                        r = new ReactionContainer("<name> is poking~at your belly.", BaseMessageBulb.NotifcationTypes.Heart);
                        break;
                    case 4:
                        if (p.Level > 30)
                        {
                            r = new ReactionContainer("<name> looks like~it wants to lead!", BaseMessageBulb.NotifcationTypes.Note);
                        }
                        break;
                    case 5:
                        r = new ReactionContainer("<name> seems to be~very happy!", BaseMessageBulb.NotifcationTypes.Happy);
                        break;
                    case 6:
                        r = new ReactionContainer("<name> nodded slowly.", BaseMessageBulb.NotifcationTypes.Friendly);
                        break;
                    case 7:
                        r = new ReactionContainer("<name> gave you~a sunny look!", BaseMessageBulb.NotifcationTypes.Happy);
                        break;
                    case 8:
                        r = new ReactionContainer("<name> is very~composed and sure of itself!", BaseMessageBulb.NotifcationTypes.Waiting);
                        break;
                    case 9:
                        if (p.BaseDefense >= 100)
                        {
                            r = new ReactionContainer("<name> is~standing guard!", BaseMessageBulb.NotifcationTypes.Exclamation);
                        }
                        break;
                    case 10:
                        r = new ReactionContainer("<name> danced a~wonderful dance!", BaseMessageBulb.NotifcationTypes.Note);
                        break;
                    case 11:
                        r = new ReactionContainer("<name> is staring~steadfastly at~<player.name>'s face.", BaseMessageBulb.NotifcationTypes.Waiting);
                        break;
                    case 12:
                        r = new ReactionContainer("<name> is staring~intently at~<player.name>'s face.", BaseMessageBulb.NotifcationTypes.Waiting);
                        break;
                    case 13:
                        r = new ReactionContainer("<name> is concentrating.", BaseMessageBulb.NotifcationTypes.Unsure);
                        break;
                    case 14:
                        r = new ReactionContainer("<name> faced this~way and nodded.", BaseMessageBulb.NotifcationTypes.Friendly);
                        break;
                    case 15:
                        r = new ReactionContainer("<name> suddenly~started walking closer!", BaseMessageBulb.NotifcationTypes.Heart);
                        break;
                    case 16:
                        r = new ReactionContainer("Woah!*<name> is suddenly~playful!", BaseMessageBulb.NotifcationTypes.Heart);
                        break;
                    case 17:
                        r = new ReactionContainer("<name> blushes.", BaseMessageBulb.NotifcationTypes.Happy);
                        break;
                    case 18:
                        r = new ReactionContainer("Woah!*<name> suddenly started~dancing in happiness!", BaseMessageBulb.NotifcationTypes.Note);
                        break;
                    case 19:
                        r = new ReactionContainer("<name> is happily~skipping about.", BaseMessageBulb.NotifcationTypes.Note);
                        break;
                    case 20:
                        r = new ReactionContainer("Woah!*<name> suddenly~danced in happiness!", BaseMessageBulb.NotifcationTypes.Note);
                        break;
                }
            }
            return r;
        }

        private static ReactionContainer GetLoveReaction(BasePokemon p)
        {
            ReactionContainer r = null;
            while (r == null)
            {
                switch (Core.Random.Next(0, 13))
                {
                    case 0:
                        r = new ReactionContainer("<name> is jumping~for joy!", BaseMessageBulb.NotifcationTypes.Heart);
                        break;
                    case 1:
                        r = new ReactionContainer("Your Pokémon stretched out~its body and is relaxing.", BaseMessageBulb.NotifcationTypes.Happy);
                        break;
                    case 2:
                        r = new ReactionContainer("<name> is happily~cuddling up to you!", BaseMessageBulb.NotifcationTypes.Heart);
                        break;
                    case 3:
                        r = new ReactionContainer("<name> is so happy~that it can't stand still!", BaseMessageBulb.NotifcationTypes.Heart);
                        break;
                    case 4:
                        if (p.PokedexEntry.Height <= 1.6f)
                        {
                            r = new ReactionContainer("<name> happily~cuddled up to you!", BaseMessageBulb.NotifcationTypes.Heart);
                        }
                        break;
                    case 5:
                        r = new ReactionContainer("<name>'s cheeks are~becoming rosy!", BaseMessageBulb.NotifcationTypes.Heart);
                        break;
                    case 6:
                        r = new ReactionContainer("Woah!*<name> suddenly~hugged you!", BaseMessageBulb.NotifcationTypes.Heart);
                        break;
                    case 7:
                        if (p.PokedexEntry.Height <= 0.7f)
                        {
                            r = new ReactionContainer("<name> is rubbing~against your legs!", BaseMessageBulb.NotifcationTypes.Heart);
                        }
                        break;
                    case 8:
                        r = new ReactionContainer("Ah!~<name> cuddles you!", BaseMessageBulb.NotifcationTypes.Heart);
                        break;
                    case 9:
                        r = new ReactionContainer("<name> is regarding~you with adoration!", BaseMessageBulb.NotifcationTypes.Heart);
                        break;
                    case 10:
                        r = new ReactionContainer("<name> got~closer to <player.name>!", BaseMessageBulb.NotifcationTypes.Heart);
                        break;
                    case 11:
                        r = new ReactionContainer("<name> is keeping~close to your feet.", BaseMessageBulb.NotifcationTypes.Heart);
                        break;
                    case 12:
                        r = new ReactionContainer("<name> is jumping~around in a carefree way!", BaseMessageBulb.NotifcationTypes.Note);
                        break;
                }
            }
            return r;
        }


        static List<ReactionContainer> SpecialReactionList = new List<ReactionContainer>();
        public static void Load()
        {
            SpecialReactionList.Clear();

            var file = GameModeManager.GetContentFile("Data\\interactions.dat");
            FileValidation.CheckFileValid(file, false, "PokemonInteractions.vb");

            foreach (string line in file.ReadAllLines())
            {
                if (line.StartsWith("{") && line.EndsWith("}"))
                {
                    if (line.CountSeperators("|") >= 8)
                    {
                        ReactionContainer r = new ReactionContainer(line);
                        SpecialReactionList.Add(r);
                    }
                }
            }
        }

        private class ReactionContainer
        {

            public string Message;
            public BaseMessageBulb.NotifcationTypes Notification = BaseMessageBulb.NotifcationTypes.AFK;
            public bool HasNotification = true;
            public List<string> MapFiles = new List<string>();
            public List<int> PokemonIDs = new List<int>();
            public List<int> ExcludeIDs = new List<int>();
            public int Daytime = -1;
            public int Weather = -1;
            public int Season = -1;
            public List<Element.Types> Types = new List<Element.Types>();

            public int Probability = 100;
            public ReactionContainer(string dataLine)
            {
                dataLine = dataLine.Remove(dataLine.Length - 1, 1).Remove(0, 1);

                string[] dataParts = dataLine.Split(Convert.ToChar("|"));

                this.MapFiles = dataParts[0].Split(Convert.ToChar(",")).ToList();

                if (dataParts[1] != "-1")
                {
                    for (var i = 0; i < dataParts[1].Split(Convert.ToChar(",")).Length; i++)
                    {
                        var pokePart = dataParts[1].Split(Convert.ToChar(","))[i];
                        List<int> lReference = PokemonIDs;
                        if (pokePart.StartsWith("!") == true)
                        {
                            pokePart = pokePart.Remove(0, 1);
                            lReference = ExcludeIDs;
                        }
                        if (pokePart.IsNumeric())
                        {
                            if (lReference.Contains(Convert.ToInt32(pokePart)) == false)
                            {
                                lReference.Add(Convert.ToInt32(pokePart));
                            }
                        }
                    }
                }

                if (dataParts[2] != "-1")
                {
                    Daytime = Convert.ToInt32(dataParts[2]).Clamp(0, 3);
                }
                if (dataParts[3] != "-1")
                {
                    Weather = Convert.ToInt32(dataParts[3]).Clamp(0, 9);
                }
                if (dataParts[4] != "-1")
                {
                    Season = Convert.ToInt32(dataParts[4]).Clamp(0, 3);
                }

                if (dataParts[5] != "-1")
                {
                    foreach (string typePart in dataParts[5].Split(Convert.ToChar(",")))
                    {
                        this.Types.Add(new Element(typePart).Type);
                    }
                }

                this.Probability = Convert.ToInt32(dataParts[6]);

                if (dataParts[7] == "-1")
                {
                    this.HasNotification = false;
                }
                else
                {
                    this.HasNotification = true;
                    this.Notification = this.ConvertEmoji(dataParts[7]);
                }

                this.Message = dataParts[8];
            }

            public ReactionContainer(string Message, BaseMessageBulb.NotifcationTypes Notification)
            {
                this.Message = Message;
                this.Notification = Notification;
            }

            private BaseMessageBulb.NotifcationTypes ConvertEmoji(string s)
            {
                switch (s.ToLower())
                {
                    case "...":
                        return BaseMessageBulb.NotifcationTypes.Waiting;
                    case "!":
                        return BaseMessageBulb.NotifcationTypes.Exclamation;
                    case ">:(":
                        return BaseMessageBulb.NotifcationTypes.Shouting;
                    case "?":
                        return BaseMessageBulb.NotifcationTypes.Question;
                    case "note":
                        return BaseMessageBulb.NotifcationTypes.Note;
                    case "<3":
                        return BaseMessageBulb.NotifcationTypes.Heart;
                    case ":(":
                        return BaseMessageBulb.NotifcationTypes.Unhappy;
                    case "ball":
                        return BaseMessageBulb.NotifcationTypes.Battle;
                    case ":D":
                        return BaseMessageBulb.NotifcationTypes.Happy;
                    case ":)":
                        return BaseMessageBulb.NotifcationTypes.Friendly;
                    case "bad":
                        return BaseMessageBulb.NotifcationTypes.Poisoned;
                    case ";)":
                        return BaseMessageBulb.NotifcationTypes.Wink;
                    case "afk":
                        return BaseMessageBulb.NotifcationTypes.AFK;
                    case "/:(":
                        return BaseMessageBulb.NotifcationTypes.Angry;
                    case ":3":
                        return BaseMessageBulb.NotifcationTypes.CatFace;
                    case ":/":
                        return BaseMessageBulb.NotifcationTypes.Unsure;
                }

                return BaseMessageBulb.NotifcationTypes.Waiting;
            }

            public bool Match(BasePokemon p)
            {
                if (MapFiles.Count > 0)
                {
                    if (MapFiles.Any((string m) => { return m.ToLowerInvariant() == Screen.Level.LevelFile.ToLowerInvariant(); }))
                    {
                        return false;

                    }
                }

                if (PokemonIDs.Count > 0)
                {
                    if (PokemonIDs.Contains(p.Number) == false)
                    {
                        return false;
                    }
                }

                if (ExcludeIDs.Count > 0)
                {
                    if (ExcludeIDs.Contains(p.Number) == true)
                    {
                        return false;
                    }
                }

                if (Daytime > -1)
                {
                    if (Daytime != Convert.ToInt32(BaseWorld.GetTime))
                    {
                        return false;
                    }
                }

                if (Weather > -1)
                {
                    if (Weather != Convert.ToInt32(BaseWorld.GetCurrentRegionWeather()))
                    {
                        return false;
                    }
                }

                if (Season > -1)
                {
                    if (Season != Convert.ToInt32(BaseWorld.CurrentSeason))
                    {
                        return false;
                    }
                }

                if (this.Types.Count > 0)
                {
                    foreach (Element.Types t in this.Types)
                    {
                        if (p.IsType(t) == false)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            public string GetMessage(BasePokemon p)
            {
                return this.Message.Replace("<name>", p.GetDisplayName());
            }

        }

        //This value holds the individual value of the Pokémon that picked up the item.
        static string PickupIndividualValue = "";
        //This is the Item ID of the item that the Pokémon picked up. -1 means no item got picked up.
        static int PickupItemID = -1;

        public static void CheckForRandomPickup()
        {
            //Checks if the first Pokémon in the party is following the player:
            if (Screen.Level.ShowOverworldPokemon == true && GameModeManager.GetActiveGameRuleValueOrDefault("ShowFollowPokemon", true) == true)
            {
                //Checks if the player has a Pokémon:
                if (Core.Player.Pokemons.Count > 0 && Screen.Level.IsSurfing == false && Screen.Level.IsRiding == false && Screen.Level.ShowOverworldPokemon == true && (Core.Player.GetWalkPokemon() != null))
                {
                    if (Core.Player.GetWalkPokemon().Status == BasePokemon.StatusProblems.None)
                    {
                        //If the player switched the Pokémon, reset the item ID.
                        if (PickupIndividualValue != Core.Player.GetWalkPokemon().IndividualValue)
                        {
                            PickupItemID = -1;
                        }

                        //Check if an item should be generated:
                        if (Core.Random.Next(0, 270) < Core.Player.GetWalkPokemon().Friendship)
                        {
                            int newItemID = -1;
                            //creates a temp value to hold the new Item ID.

                            //Checks if the player is outside:
                            if (IsOutside() == true)
                            {
                                //Checks if the leading Pokémon is holding a sticky feather, which ensures a 90% feather pickup outside:
                                if ((Core.Player.GetWalkPokemon().Item != null) && Core.Player.GetWalkPokemon().Item.Id == 261 && Core.Random.Next(0, 100) < 90)
                                {
                                    newItemID = Core.Random.Next(254, 261);
                                }
                                else
                                {
                                    //Checks if ice is around:
                                    if (IceAround() == true)
                                    {
                                        //20%: NeverMeltIce, 80% Aspear Berry
                                        if (Core.Random.Next(0, 100) < 20)
                                        {
                                            newItemID = 107;
                                        }
                                        else
                                        {
                                            newItemID = 2004;
                                        }
                                    }
                                    else
                                    {
                                        //Checks if loamy soil is around, if so, give a random berry (only 50% activation)
                                        if (LoamySoilAround() == true && Core.Random.Next(0, 100) < 50)
                                        {
                                            newItemID = Core.Random.Next(2000, 2064);
                                        }
                                        else
                                        {
                                            //Checks if grass is around, if so, give a grass item (only 50% activation)
                                            if (GrassAround() == true && Core.Random.Next(0, 100) < 50)
                                            {
                                                //Leaf Stone (10%), energy root (50%), revival herb (15%), heal powder (25%):
                                                int r = Core.Random.Next(0, 100);
                                                if (r < 10)
                                                {
                                                    newItemID = 34;
                                                }
                                                else if (r >= 10 && r < 60)
                                                {
                                                    newItemID = 122;
                                                }
                                                else if (r >= 60 && r < 75)
                                                {
                                                    newItemID = 124;
                                                }
                                                else
                                                {
                                                    newItemID = 123;
                                                }
                                            }
                                            else
                                            {
                                                //Checks if water is around, if so, give a water item (only 50% activation)
                                                if (WaterAround() == true && Core.Random.Next(0, 100) < 50)
                                                {
                                                    //Water Stone (10%), pearl (50%), big pearl (10%), heart scale (40%):
                                                    int r = Core.Random.Next(0, 100);
                                                    if (r < 10)
                                                    {
                                                        newItemID = 24;
                                                    }
                                                    else if (r >= 10 && r < 50)
                                                    {
                                                        newItemID = 110;
                                                    }
                                                    else if (r >= 50 && r < 60)
                                                    {
                                                        newItemID = 190;
                                                    }
                                                    else
                                                    {
                                                        newItemID = 111;
                                                    }
                                                }
                                                else
                                                {
                                                    //No special conditions apply:
                                                    //(general): first 10 berries (45%), wings (45%), gold leaf (2%), silver leaf (8%):
                                                    int r = Core.Random.Next(0, 100);
                                                    if (r < 45)
                                                    {
                                                        newItemID = Core.Random.Next(2000, 2011);
                                                    }
                                                    else if (r >= 45 && r < 90)
                                                    {
                                                        newItemID = Core.Random.Next(254, 262);
                                                    }
                                                    else if (r >= 90 && r < 92)
                                                    {
                                                        newItemID = 75;
                                                    }
                                                    else
                                                    {
                                                        newItemID = 60;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                //Player is inside:
                            }
                            else if (IsInside() == true)
                            {
                                //leftovers (5%), lavacookie (20%), super potion (10%), ether (20%), elixier (20%), potion (10%), quick claw (8%), gold leaf (2%), silver leaf (5%)
                                int r = Core.Random.Next(0, 100);
                                if (r < 5)
                                {
                                    newItemID = 146;
                                    //Leftovers
                                }
                                else if (r >= 5 && r < 25)
                                {
                                    newItemID = 7;
                                    //Lavacookie
                                }
                                else if (r >= 25 && r < 45)
                                {
                                    //0-1 badge: potion
                                    //2-4 badges: super potion
                                    //5-7 badges: hyper potion
                                    //8 and above: max potion
                                    int b = Core.Player.Badges.Count;
                                    if (b <= 1)
                                    {
                                        newItemID = 18;
                                    }
                                    else if (b >= 2 && b <= 4)
                                    {
                                        newItemID = 17;
                                    }
                                    else if (b >= 5 && b <= 7)
                                    {
                                        newItemID = 16;
                                    }
                                    else
                                    {
                                        newItemID = 15;
                                    }
                                }
                                else if (r >= 45 && r < 65)
                                {
                                    newItemID = 63;
                                    //Ether
                                }
                                else if (r >= 65 && r < 85)
                                {
                                    newItemID = 65;
                                    //Elixier
                                }
                                else if (r >= 85 && r < 93)
                                {
                                    newItemID = 73;
                                    //Quick Claw
                                }
                                else if (r >= 93 && r < 95)
                                {
                                    newItemID = 75;
                                    //Gold leaf
                                }
                                else
                                {
                                    newItemID = 60;
                                    //Silver leaf
                                }
                                //Player is in cave:
                            }
                            else if (IsCave() == true)
                            {
                                //Checks if the leading Pokémon is holding a sticky rock, which ensures a 90% feather pickup in a cave:
                                if ((Core.Player.GetWalkPokemon().Item != null) && Core.Player.GetWalkPokemon().Item.Id == 262 && Core.Random.Next(0, 100) < 90)
                                {
                                    //Thunderstone(20%),Firestone(20%),Waterstone(20%),Leafstone(20%),Moonstone(10%),Sunstone(10%)
                                    int r1 = Core.Random.Next(0, 100);
                                    if (r1 < 20)
                                    {
                                        newItemID = 22;
                                    }
                                    else if (r1 >= 20 && r1 < 40)
                                    {
                                        newItemID = 23;
                                    }
                                    else if (r1 >= 40 && r1 < 60)
                                    {
                                        newItemID = 24;
                                    }
                                    else if (r1 >= 60 && r1 < 80)
                                    {
                                        newItemID = 34;
                                    }
                                    else if (r1 >= 80 && r1 < 90)
                                    {
                                        newItemID = 8;
                                    }
                                    else
                                    {
                                        newItemID = 169;
                                    }
                                }
                                else
                                {
                                    //Checks if water is around, if so, give a water item (only 65% activation)
                                    if (WaterAround() == true && Core.Random.Next(0, 100) < 65)
                                    {
                                        //Water Stone (30%), pearl (40%), big pearl (10%), heart scale (20%):
                                        int r = Core.Random.Next(0, 100);
                                        if (r < 30)
                                        {
                                            newItemID = 24;
                                        }
                                        else if (r >= 30 && r < 70)
                                        {
                                            newItemID = 110;
                                        }
                                        else if (r >= 70 && r < 80)
                                        {
                                            newItemID = 190;
                                        }
                                        else
                                        {
                                            newItemID = 111;
                                        }
                                    }
                                    else
                                    {
                                        //Fire Stone (10%), Thunder Stone (10%), pearl (30%), hard stone (10%), everstone (10%), first 10 berries (20%)
                                        int r = Core.Random.Next(0, 100);
                                        if (r < 10)
                                        {
                                            newItemID = 22;
                                        }
                                        else if (r >= 10 && r < 20)
                                        {
                                            newItemID = 23;
                                        }
                                        else if (r >= 20 && r < 50)
                                        {
                                            newItemID = 110;
                                        }
                                        else if (r >= 50 && r < 60)
                                        {
                                            newItemID = 125;
                                        }
                                        else if (r >= 60 && r < 70)
                                        {
                                            newItemID = 112;
                                        }
                                        else if (r >= 70 && r < 90)
                                        {
                                            newItemID = Core.Random.Next(2000, 2011);
                                        }
                                        else
                                        {
                                            newItemID = 262;
                                        }
                                    }
                                }
                            }

                            //If an item got generated, assign it to the global value to store it until the player interacts with the Pokémon. Also store the individual value.
                            if (newItemID > -1)
                            {
                                Logger.Debug("Pokémon picks up item (" + Item.GetItemByID(newItemID).Name + ")");
                                PickupItemID = newItemID;
                                PickupIndividualValue = Core.Player.GetWalkPokemon().IndividualValue;
                                SoundManager.PlaySound("pickup");
                            }
                        }
                    }
                }
                else
                {
                    //Reset the system if no Pokémon:
                    PickupItemID = -1;
                    PickupIndividualValue = "";
                }
            }
        }

        #region "Checks"

        private static bool IsOutside()
        {
            if (Screen.Level.CanFly == true && Screen.Level.CanDig == false && Screen.Level.CanTeleport == true)
            {
                return true;
            }
            return false;
        }

        private static bool IsCave()
        {
            if (Screen.Level.CanFly == false && Screen.Level.CanDig == true && Screen.Level.CanTeleport == false)
            {
                return true;
            }
            return false;
        }

        private static bool IsInside()
        {
            if (Screen.Level.CanFly == false && Screen.Level.CanDig == false && Screen.Level.CanTeleport == false)
            {
                return true;
            }
            return false;
        }

        private static bool WildPokemon()
        {
            if (Screen.Level.WildPokemonFloor == true || Screen.Level.WildPokemonGrass == true || Screen.Level.WildPokemonWater == true)
            {
                return true;
            }
            return false;
        }

        private static bool WaterAround()
        {
            foreach (Entity e in Screen.Level.Entities)
            {
                if (e.EntityID.ToLower() == "water")
                {
                    if (Vector3.Distance(e.Position, Screen.Camera.Position) <= 5f)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool NPCAround()
        {
            foreach (Entity e in Screen.Level.Entities)
            {
                if (e.EntityID.ToLower() == "npc")
                {
                    if (Vector3.Distance(e.Position, Screen.Camera.Position) <= 4f)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool TrainerAround()
        {
            // TODO
            foreach (Entity e in Screen.Level.Entities)
            {
                if (e.EntityID.ToLower() == "npc")
                {
                    if (((BaseNPC)e).IsTrainer)
                    {
                        if (Vector3.Distance(e.Position, Screen.Camera.Position) <= 3f)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private static bool GrassAround()
        {
            foreach (Entity e in Screen.Level.Entities)
            {
                if (e.EntityID.ToLower() == "grass")
                {
                    if (Vector3.Distance(e.Position, Screen.Camera.Position) <= 5f)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool ItemAround()
        {
            foreach (Entity e in Screen.Level.Entities)
            {
                if (e.EntityID.ToLower() == "itemobject")
                {
                    if (Vector3.Distance(e.Position, Screen.Camera.Position) <= 5f)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool IceAround()
        {
            // TODO
            /*
            foreach (Entity e in Screen.Level.Entities)
            {
                if (e.EntityID.ToLower() == "floor")
                {
                    if (((Floor)e).IsIce == true)
                    {
                        if (Vector3.Distance(e.Position, Screen.Camera.Position) <= 2f)
                        {
                            return true;
                        }
                    }
                }
            }
            */

            return false;
        }

        private static bool LoamySoilAround()
        {
            foreach (Entity e in Screen.Level.Entities)
            {
                if (e.EntityID.ToLower() == "loamysoil")
                {
                    if (Vector3.Distance(e.Position, Screen.Camera.Position) <= 5f)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion

    }
}
