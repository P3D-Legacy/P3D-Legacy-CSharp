using P3D.Legacy.Core.Screens;

namespace P3D.Legacy.Core.Battle.BattleSystemV2
{
    public class BattleAnimationScreenV2 : Screen
    {

        public BattleAnimationScreenV2(Screen currentScreen)
        {
            PreScreen = currentScreen;
            Identification = Identifications.BattleAnimationScreen;
        }

        public override void Draw()
        {
            DrawBackgroundLayer();
            DrawBackLayer();
            DrawMainLayer();
            DrawFrontLayer1();
            DrawFrontLayer2();
        }


        public override void Update()
        {
        }

        #region "Draws"


        private void DrawBackgroundLayer()
        {
        }


        private void DrawBackLayer()
        {
        }


        private void DrawMainLayer()
        {
        }


        private void DrawFrontLayer1()
        {
        }


        private void DrawFrontLayer2()
        {
        }

        #endregion

        #region "ScreenFunction"


        public void SetBackground()
        {
        }

        #endregion

    }
}
