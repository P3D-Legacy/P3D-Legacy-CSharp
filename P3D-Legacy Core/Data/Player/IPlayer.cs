using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using P3D.Legacy.Core.Data;
using P3D.Legacy.Core.Pokemon;
using P3D.Legacy.Core.Server;

namespace P3D.Legacy.Core.Interfaces
{
    // How does this even works, Player doesn't implements IPlayer
    public interface IPlayer
    {
        string Name { get; set; }
        string RivalName { get; set; }
        bool IsGamejoltSave { get; set; }
        string startMap { get; set; }
        void DrawLevelUp();

        string GameMode { get; set; }
        List<BasePokemon> Pokemons { get; set; }
        int ShowBattleAnimations { get; set; }
        bool SandBoxMode { get; set; }
        IPlayerInventory Inventory { get; set; }
        int DifficultyMode { get; set; }
        string ApricornData { get; set; }
        int SurfPokemon { get; }
        string TempSurfSkin { get; set; }
        string RegisterData { get; set; }
        Texture2D Texture { get; set; }
        string Skin { get; set; }
        float Opacity { get; set; }
        string ItemData { get; set; }
        string BerryData { get; set; }
        string NPCData { get; set; }
        bool IsRunning { get; }
        Vector3 LastPokemonPosition { get; set; }
        BasePokemon GetWalkPokemon();
        float CameraDistance { get; set; }
        bool loadedSave { get; set; }
        TimeSpan PlayTime { get; set; }
        DateTime GameStart { get; set; }
        int GTSStars { get; set; }
        Vector3 startPosition { get; set; }
        bool startThirdPerson { get; set; }
        double startRotationSpeed { get; set; }
        float startFOV { get; set; }
        float startRotation { get; set; }
        bool startFreeCameraMode { get; set; }
        bool DoAnimation { get; set; }
        bool HasPokegear { get; set; }
        string DaycareData { get; set; }
        string SkinName { get; set; }
        bool startSurfing { get; set; }
        string TempRideSkin { get; set; }
        string LastRestPlace { get; set; }
        string LastRestPlacePosition { get; set; }
        string LastSavePlace { get; set; }
        string LastSavePlacePosition { get; set; }
        int CountFightablePokemon { get; }
        string OT { get; set; }
        string PokedexData { get; set; }
        List<BasePokedex> Pokedexes { get; set; }
        List<int> Badges { get; set; }
        int Money { get; set; }
        List<string> PokeFiles { get; set; }
        bool ShowModelsInBattle { get; set; }
        string RoamingPokemonData { get; set; }
        int Coins { get; set; }
        bool hasPokedex { get; set; }
        string BoxData { get; set; }
        string HallOfFameData { get; set; }
        bool Male { get; set; }
        string HistoryData { get; set; }
        int RepelSteps { get; set; }
        PlayerTemp PlayerTemp { get; set; }
        int BP { get; set; }
        List<string> EarnedAchievements { get; set; }
        int DaycareSteps { get; set; }
        int Points { get; set; }
        bool IsFlying { get; set; }
        string VisitedMaps { get; set; }
        bool DiagonalMovement { get; set; }
        int BattleStyle { get; set; }
        List<MailItem.MailData> Mails { get; set; }
        int BoxAmount { get; set; }
        int ServersID { get; set; }
        string GameJoltId { get; set; }
        int BusyType { get; set; }
        Vector3 Position { get; set; }
        bool Initialized { get; set; }
        Vector3 PokemonPosition { get; set; }
        string PokemonSkin { get; set; }
        bool PokemonVisible { get; set; }
        int PokemonFacing { get; set; }
        int Facing { get; set; }
        string LevelFile { get; set; }
        bool Moving { get; set; }
        bool UsingGameJoltTexture { get; set; }
        string SecretBaseData { get; set; }
        string Statistics { get; set; }
        string SaveCreated { get; set; }
        string EmblemBackground { get; set; }
        bool CanCatchPokémon { get; }
        bool startRiding { get; set; }

        void AddPoints(int integer, string pickedBerries);
        void ChangeTexture();
        void SaveGame(bool boolean);

        void TakeStep(int amount);
        void SetTexture(string tempSurfSkin, bool boolean);
        string ApplyShaders();
        void AddVisitedMap(string levelFile);
        void UpdateEntity();
        void LoadGame(string fileName);
        void HealParty();
        void HealParty(int[] toArray);
        void Unload();
        void ApplyNewData(IPackage package);
        string GetItemsData();
        string GetOptionsData();
        string GetPartyData();
        string GetPlayerData(bool boolean);
        string GetBerriesData();
        string GetApricornsData();
        string GetDaycareData();
        string GetPokedexData();
        string GetRegisterData();
        string GetItemDataData();
        string GetBoxData();
        string GetNPCDataData();
        string GetHallOfFameData();
        string GetSecretBaseData();
        string GetRoamingPokemonData();
        string GetStatisticsData();
        int GetValidPokemonCount();
        void ResetNewLevel();
    }
}
