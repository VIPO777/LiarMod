using LiarMod.OnGui;
using LiarMod.Player;
using UnityEngine;
using static UnityEngine.Screen;

namespace LiarMod.OnGui
{
    internal static class GUIMenu
    {
        public enum MenuPage { Functions, NetObjectPage, ExploreGameObject };
        public enum NetPage { Player, NetObjects };

        public static void InitializeWindowSize()
        {
            GUIMenu.MainWindowRect = new Rect(150, 150, width * .40f, height * .53f);
        }
        public static void ApplySkins()
        {

            GUIMenu.WindowBG.normal.background = GUIMenu.backgroundTexture;
            GUIMenu.Box.normal.background = GUIMenu.backgroundTexture;

            GUIMenu.WindowBG.active.background = GUIMenu.WindowT;
            GUIMenu.WindowBG.hover.background = GUIMenu.WindowT;

            GUIMenu.Box.fontSize = 24;

            GUIMenu.Box2.normal.background = GUIMenu.PlayerlistBG;

            GUIMenu.Box2.fontSize = 20;

            GUIMenu.Label.normal.background = GUIMenu.Background4;

            GUIMenu.Label.fontSize = 20;

            GUIMenu.Label2.normal.background = GUIMenu.PlayerlistBG;
            GUIMenu.Label2.alignment = TextAnchor.MiddleCenter;

            GUIMenu.Label2.fontSize = 20;

            GUIMenu.Label3.normal.background = GUIMenu.PlayerlistBG;
            GUIMenu.Label3.alignment = TextAnchor.MiddleLeft;

            GUIMenu.Label3.fontSize = 20;

            GUIMenu.Button.normal.background = GUIMenu.PlayerlistBG;
            GUIMenu.Button.hover.background = GUIMenu.PlayerlistBGLite;
            GUIMenu.Button.hover.textColor = GUIMenu.ButtonLite2;

            GUIMenu.Button.fontSize = 20;

            GUIMenu.Button2.normal.background = GUIMenu.Background;
            GUIMenu.Button2.active.textColor = GUIMenu.ButtonLite;
            GUIMenu.Button2.active.background = GUIMenu.Background2;
            GUIMenu.Button2.hover.background = GUIMenu.BackgroundLite;
            GUIMenu.Button2.hover.textColor = GUIMenu.ButtonLite;
            GUIMenu.Button2.fontSize = 20;
            GUIMenu.Button2.fontStyle = FontStyle.Bold;

            GUIMenu.Button3.normal.background = GUIMenu.Background3;
            GUIMenu.Button3.fontSize = 20;

            GUIMenu.ESPRED.normal.background = GUIMenu.TexColorRed;
            GUIMenu.ESPRED.hover.background = GUIMenu.TexColorRed;
            GUIMenu.ESPRED.fontSize = 20;
            GUIMenu.ESPRED.fontStyle = FontStyle.Bold;

            GUIMenu.ESPGREEN.normal.background = GUIMenu.TexColorGreen;
            GUIMenu.ESPGREEN.hover.background = GUIMenu.TexColorGreen;
            GUIMenu.ESPGREEN.fontSize = 20;
            GUIMenu.ESPGREEN.fontStyle = FontStyle.Bold;

            GUIMenu.ESPBLUE.normal.background = GUIMenu.TexColorBlue;
            GUIMenu.ESPBLUE.hover.background = GUIMenu.TexColorBlue;
            GUIMenu.ESPBLUE.fontSize = 20;
            GUIMenu.ESPBLUE.fontStyle = FontStyle.Bold;

            GUIMenu.HorizontalSlider.normal.background = GUIMenu.Background;
            GUIMenu.HorizontalSlider.active.background = GUIMenu.BackgroundLite;

            GUIMenu.HorizontalSliderThumb.normal.background = GUIMenu.Background2;
            GUIMenu.HorizontalSliderThumb.active.background = GUIMenu.PlayerlistBGLite;
            GUIMenu.HorizontalSliderThumb.hover.background = GUIMenu.Background2;

            GUIMenu.MainMenuS.normal.background = GUIMenu.MenuBackground;
            GUIMenu.MainMenuS.active.background = GUIMenu.MenuBackground;
            GUIMenu.MainMenuS.hover.background = GUIMenu.MenuBackground;

            GUIMenu.MainLabel.fontSize = 30;
            GUIMenu.MainLabel.fontStyle = FontStyle.Bold;
            GUIMenu.MainLabel.alignment = TextAnchor.MiddleCenter;

            GUIMenu.MainLabel2.fontSize = 26;
            GUIMenu.MainLabel2.alignment = TextAnchor.UpperCenter;

            GUIMenu.Label.normal.background = GUIMenu.MenuBackground;
            GUIMenu.Label.fontSize = 20;

            GUIMenu.Label2.normal.background = GUIMenu.MenuBackground;
            GUIMenu.Label2.alignment = TextAnchor.MiddleCenter;
            GUIMenu.Label2.fontSize = 20;

            GUIMenu.Label3.normal.background = GUIMenu.MenuBackground;
            GUIMenu.Label3.alignment = TextAnchor.MiddleLeft;
            GUIMenu.Label3.fontSize = 20;

            GUIMenu.Button.normal.background = GUIMenu.ButtonBG;
            GUIMenu.Button.hover.background = GUIMenu.ButtonHoverBG;
            GUIMenu.Button.active.background = GUIMenu.ButtonSelectedBG;

            GUIMenu.MainToolbar.normal.background = GUIMenu.Empty;
            GUIMenu.MainToolbar.onNormal.background = GUIMenu.ToolbarBackground;
            GUIMenu.MainToolbar.onActive.background = GUIMenu.ToolbarSelectedBG;
            GUIMenu.MainToolbar.active.background = GUIMenu.ToolbarSelectedBG;
            GUIMenu.MainToolbar.hover.background = GUIMenu.ToolbarHoverBG;
            GUIMenu.MainToolbar.onHover.background = GUIMenu.ToolbarHoverBG;
            GUIMenu.MainToolbar.fontSize = 20;
            GUIMenu.MainToolbar.fontStyle = FontStyle.Bold;

        }

        public static void ApplyBGs()
        {

            GUIMenu.backgroundTexture = new Texture2D(1, 1);
            GUIMenu.backgroundTexture.SetPixels(new[] { new Color(0.15f, 0.15f, 0.15f, 0.7f) });
            GUIMenu.backgroundTexture.Apply();

            GUIMenu.WindowT = new Texture2D(1, 1);
            GUIMenu.WindowT.SetPixels(new[] { new Color(0.1f, 0.1f, 0.1f, 0.75f) });
            GUIMenu.WindowT.Apply();

            GUIMenu.Background = new Texture2D(1, 1);
            GUIMenu.Background.SetPixels(new[] { new Color(0.11f, 0.53f, 0.93f, 0.25f) });
            GUIMenu.Background.Apply();

            GUIMenu.Background2 = new Texture2D(1, 1);
            GUIMenu.Background2.SetPixels(new[] { new Color(0f, 0.93f, 0f, 0.25f) });
            GUIMenu.Background2.Apply();

            GUIMenu.Background3 = new Texture2D(1, 1);
            GUIMenu.Background3.SetPixels(new[] { new Color(1f, 0.19f, 0.19f, 0.25f) });
            GUIMenu.Background3.Apply();

            GUIMenu.Background4 = new Texture2D(1, 1);
            GUIMenu.Background4.SetPixels(new[] { new Color(1f, 1f, 0.05f, 0.35f) });
            GUIMenu.Background4.Apply();

            GUIMenu.PlayerlistBG = new Texture2D(1, 1);
            GUIMenu.PlayerlistBG.SetPixels(new[] { new Color(0.35f, 0.35f, 0.35f, 0.35f) });
            GUIMenu.PlayerlistBG.Apply();


            GUIMenu.PlayerlistBGLite = new Texture2D(1, 1);
            GUIMenu.PlayerlistBGLite.SetPixels(new[] { new Color(0.40f, 0.40f, 0.40f, 0.35f) });
            GUIMenu.PlayerlistBGLite.Apply();

            GUIMenu.BackgroundLite = new Texture2D(1, 1);
            GUIMenu.BackgroundLite.SetPixels(new[] { new Color(0.15f, 0.57f, 0.98f, 0.26f) });
            GUIMenu.BackgroundLite.Apply();

            GUIMenu.TexColorRed = new Texture2D(1, 1);
            GUIMenu.TexColorRed.SetPixels(new[] { new Color(1f, 0f, 0f, 0.5f) });
            GUIMenu.TexColorRed.Apply();

            GUIMenu.TexColorGreen = new Texture2D(1, 1);
            GUIMenu.TexColorGreen.SetPixels(new[] { new Color(0f, 1f, 0f, 0.5f) });
            GUIMenu.TexColorGreen.Apply();

            GUIMenu.TexColorBlue = new Texture2D(1, 1);
            GUIMenu.TexColorBlue.SetPixels(new[] { new Color(0f, 0f, 1f, 0.5f) });
            GUIMenu.TexColorBlue.Apply();

            GUIMenu.ButtonLite = new Color(0.75f, 0.94f, 1f, 1f);
            GUIMenu.ButtonLite2 = new Color(1f, 0.42f, 0.42f, 1f);
            GUIMenu.ButtonLite3 = new Color(0.56f, 0.93f, 0.56f, 1f);

            GUIMenu.BoxESP2DC = Color.green;
            GUIMenu.BoxESP3DC = Color.green;
            GUIMenu.LineESPC = Color.green;


            // Project x UI
            GUIMenu.ToolbarBackground = new Texture2D(1, 1);
            UIHelper.TextureApplier(GUIMenu.ToolbarBackground, HexDodgerblue);

            GUIMenu.MenuBackground = new Texture2D(1, 1);
            UIHelper.TextureApplier(GUIMenu.MenuBackground, HexBlack);

            GUIMenu.ToolbarSelectedBG = new Texture2D(1, 1);
            UIHelper.TextureApplier(GUIMenu.ToolbarSelectedBG, HexGreen);

            GUIMenu.ToolbarHoverBG = new Texture2D(1, 1);
            UIHelper.TextureApplier(GUIMenu.ToolbarHoverBG, HexBlue);

            GUIMenu.ButtonBG = new Texture2D(1, 1);

            GUIMenu.ButtonBG.SetPixels(new[] { new Color(0.35f, 0.35f, 0.35f, 0.35f) });
            GUIMenu.ButtonBG.Apply();

            GUIMenu.ButtonHoverBG = new Texture2D(1, 1);
            GUIMenu.ButtonHoverBG.SetPixels(new[] { new Color(0.40f, 0.40f, 0.40f, 0.35f) });
            GUIMenu.ButtonHoverBG.Apply();

            GUIMenu.ButtonSelectedBG = new Texture2D(1, 1);
            GUIMenu.ButtonSelectedBG.SetPixels(new[] { new Color(0.15f, 0.57f, 0.98f, 0.26f) });
            GUIMenu.ButtonSelectedBG.Apply();

            GUIMenu.Empty = new Texture2D(1, 1);
            UIHelper.TextureApplier(GUIMenu.Empty, HexTransparent);


        }


        public static void InitializeUI()
        {
            GUIMenu.Button = new GUIStyle(GUI.skin.button);
            GUIMenu.Button2 = new GUIStyle(GUI.skin.button);
            GUIMenu.Button3 = new GUIStyle(GUI.skin.button);
            GUIMenu.Label = new GUIStyle(GUI.skin.label);
            GUIMenu.Label2 = new GUIStyle(GUI.skin.label);
            GUIMenu.Label3 = new GUIStyle(GUI.skin.label);
            GUIMenu.Box = new GUIStyle(GUI.skin.box);
            GUIMenu.Box2 = new GUIStyle(GUI.skin.box);
            GUIMenu.Box3 = new GUIStyle(GUI.skin.box);
            GUIMenu.WindowBG = new GUIStyle(GUI.skin.box);
            GUIMenu.ESPRED = new GUIStyle(GUI.skin.button);
            GUIMenu.ESPGREEN = new GUIStyle(GUI.skin.button);
            GUIMenu.ESPBLUE = new GUIStyle(GUI.skin.button);
            GUIMenu.HorizontalSlider = new GUIStyle(GUI.skin.horizontalSlider);
            GUIMenu.HorizontalSliderThumb = new GUIStyle(GUI.skin.horizontalSliderThumb);

            GUIMenu.MainLabel = new GUIStyle(GUI.skin.label);
            GUIMenu.MainLabel2 = new GUIStyle(GUI.skin.label);
            GUIMenu.MainToolbar = new GUIStyle(GUI.skin.button);
            GUIMenu.MainMenuS = new GUIStyle(GUI.skin.box);
            GUIMenu.Label = new GUIStyle(GUI.skin.label);
            GUIMenu.Label2 = new GUIStyle(GUI.skin.label);
            GUIMenu.Label3 = new GUIStyle(GUI.skin.label);
            GUIMenu.Button = new GUIStyle(GUI.skin.button);
            GUIMenu.ConsoleText = new GUIStyle(GUI.skin.textArea);
        }


        public static Rect MainWindowRect;
        public static bool ShowMenu;
        public static float oldMainWindowRectHeight;
        public static MenuPage menuPage;
        public static NetPage netpage;
        public static GUIStyle Button, Button2, MainLabel2, Button3, Label, Label2, Label3, Box, Box2, Box3, WindowBG, ESPRED, ESPGREEN, ESPBLUE, HorizontalSlider, MainLabel, MainToolbar,
           MainMenuS, ConsoleText, HorizontalSliderThumb;
        public static Texture2D backgroundTexture, Background, BackgroundLite, Background2, Background3, Background4, PlayerlistBG,
           PlayerlistBGLite, ToolbarBackground, MenuBackground, ToolbarSelectedBG, ToolbarHoverBG, ButtonBG, ButtonHoverBG, ButtonSelectedBG, Empty, WindowT, TexColorRed, TexColorGreen, TexColorBlue;
        public static Color ButtonLite, ButtonLite2, ButtonLite3, BoxESP2DC, BoxESP3DC, LineESPC;
        public static Vector2 scrollPlayers, scrollFunctions, scrollNetObject, scrollCharGameObjects;


        public static string HexRed = "#ff00007f";
        public static string HexGreen = "#00ff007f";
        public static string HexBlue = "#0000ff7f";
        public static string HexDodgerblue = "#1c86ee7f";
        public static string HexBlack = "#1a1a1acf";
        public static string HexSkyblue = "#7ec0ee7f";
        public static string HexDodgerblueF = "#1c86eeff";
        public static string HexTransparent = "#00000000";

    }
}
