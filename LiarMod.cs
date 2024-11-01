using LiarMod.Net;
using LiarMod.OnGui;
using MelonLoader;
using UnityEngine;

namespace LiarMod
{
    public static class BuildInfo
    {
        public const string Name = "LiarMod";
        public const string Description = "Fun functions for the Liar's Bar";
        public const string Author = "VIPO777";
        public const string Company = null;
        public const string Version = "1.0.0";
        public const string DownloadLink = "https://github.com/VIPO777/LiarMod/releases";
    }

    public class LiarMod : MelonMod
    {
        public override void OnLateInitializeMelon()
        {
            MelonLogger.Msg("OnApplicationLateStart");

            GUIMenu.InitializeWindowSize();
            GUIMenu.ApplyBGs();

            MelonEvents.OnUpdate.Subscribe(freecam.freecam.OnUpdate);
            MelonEvents.OnUpdate.Subscribe(Player.PlayerObject.OnUpdate);
            MelonEvents.OnUpdate.Subscribe(NetObject.OnUpdate);

            MelonEvents.OnSceneWasInitialized.Subscribe(Player.PlayerObject.OnSceneWasInitialized);
            MelonEvents.OnSceneWasInitialized.Subscribe(LiarMenu.OnSceneWasInitialized);

            MelonEvents.OnSceneWasUnloaded.Subscribe(freecam.freecam.OnSceneWasUnloaded);

        #if DEBUG
            MelonEvents.OnSceneWasUnloaded.Subscribe(PlayerCards.OnSceneWasUnloaded);
            MelonEvents.OnFixedUpdate.Subscribe(PlayerCards.FixedUpdate);
            MelonEvents.OnGUI.Subscribe(PlayerCards.OnGUI);
        #endif

        }

        public override void OnSceneWasInitialized(int buildindex, string sceneName)
        {
            MelonLogger.Msg("OnSceneWasInitialized: " + buildindex.ToString() + " | " + sceneName);
            needsUIReinitialization = true;
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                GUIMenu.ShowMenu = !GUIMenu.ShowMenu;
                GUIMenu.InitializeWindowSize();
                if (GUIMenu.ShowMenu)
                    GUI.FocusWindow(0);
            }
        }

        public override void OnGUI()
        {
            if (needsUIReinitialization)
            {
                GUIMenu.InitializeUI();
                GUIMenu.ApplySkins();
                GUIMenu.ApplyBGs();
                needsUIReinitialization = false;
                MelonLogger.Msg("Reinitialized UI in OnGUI!");
            }

            if (LiarMenu.BitchesGuiToggle)
            {
                GUI.Label(new Rect(20, 20, 1000, 200), "<b><color=cyan><size=100>BITCHES</size></color></b>");
            }

            if (GUIMenu.MainWindowRect != null && GUIMenu.ShowMenu && GUIMenu.WindowBG != null)
            {
                GUI.backgroundColor = Color.white;
                GUIMenu.MainWindowRect = GUI.Window(0, GUIMenu.MainWindowRect, LiarMenu.OnMainMenu, string.Empty, GUIMenu.WindowBG);

            }

        }

        private bool needsUIReinitialization = false;
        public static GameObject modGameObjects;
    }

}