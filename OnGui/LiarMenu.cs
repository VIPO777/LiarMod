using LiarMod.Net;
using LiarMod.Player;
using MelonLoader;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

using static LiarMod.OnGui.GUIMenu;
namespace LiarMod.OnGui
{
    internal static class LiarMenu
    {
        public static void OnMainMenu(int windowId)
        {
            GUIMenu.MainWindowRect.width = 600f;
            GUIMenu.MainWindowRect.height = 665f; 

            GUILayout.BeginVertical();

            GUILayout.Label("<b>LiarMod</b>", GUIMenu.MainLabel);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Functions", GUIMenu.Button))
                GUIMenu.menuPage = MenuPage.Functions;

            if (GUILayout.Button("NetObjects", GUIMenu.Button))
                GUIMenu.menuPage = MenuPage.NetObjectPage;

            if (GUILayout.Button("ExploreObject", GUIMenu.Button))
                GUIMenu.menuPage = MenuPage.ExploreGameObject;

            GUILayout.EndHorizontal();

            CustomNetworkManager net = NetworkManager.singleton as CustomNetworkManager;

            switch (GUIMenu.menuPage)
            {
                case MenuPage.Functions:
                    ShowFunctionsPage();
                    break;

                case MenuPage.NetObjectPage:

                    if (net == null)
                    {
                        GUILayout.Label("CustomNetworkManager is not available!", GUILayout.ExpandWidth(true));
                        break;
                    }

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Players", GUIMenu.Button2))
                    {
                        GUIMenu.netpage = NetPage.Player;
                    }
                    if (GUILayout.Button("Net Objects", GUIMenu.Button2))
                    {
                        GUIMenu.netpage = NetPage.NetObjects;
                    }

                    GUILayout.EndHorizontal();

                    switch (GUIMenu.netpage)
                    {
                        case NetPage.Player:
                            ShowPlayersPage();
                            break;
                        case NetPage.NetObjects:
                            ShowNetObjectsPage();
                            break;

                    }

                    GUILayout.Label("Net GameMode: " + net.Mode.ToString(), GUIMenu.Label3);
                    GUILayout.Label("PlayGameMode: " + net.mode.ToString(), GUIMenu.Label3);
                    break;

                case MenuPage.ExploreGameObject:
                    ShowGameObjectsPage();
                    break;
            }

            GUILayout.EndVertical();
            GUI.DragWindow(new Rect(0, 0, 10000, 25));
        }

        private static void ShowFunctionsPage()
        {
            CustomNetworkManager net = NetworkManager.singleton as CustomNetworkManager;

            GUIMenu.scrollFunctions = GUILayout.BeginScrollView(GUIMenu.scrollFunctions, GUILayout.Width(GUIMenu.MainWindowRect.width));

            NewToggleButton("Bitches GUI", ref BitchesGuiToggle);
            NewToggleButton("FreePos", ref freePlayerPosToggle);
            NewToggleButton("FreeNetObjPos", ref freeNetObjPosToggle);

            if (Manager.Instance != null)
            {
                switch (net.Mode)
                {
                    case CustomNetworkManager.GameMode.LiarsDeck:
                    case CustomNetworkManager.GameMode.LiarsChaos:
                        NewToggleButton("FreeCountDown", ref FreeCountDown);
                        NewToggleButton("FreeTurn", ref FreeLiarsDeckTurn);
#if DEBUG
                        if (net.Mode == CustomNetworkManager.GameMode.LiarsDeck)
                        {
                            NewToggleButton("FreeThrowCard", ref FreeThrowCard);

                            NewToggleButton("ShowLiarsDeckCards [Don't fucking use]", ref ShowLiarsDeckCards);

                            if (Manager.Instance.GameStarted && ShowLiarsDeckCards)
                            {
                                PlayerStats pStats2 = Manager.Instance.GetLocalPlayer();

                                if (GUILayout.Button("Set all cards as <color=red>Kings</color>", GUIMenu.Button2))
                                {
                                    PlayerCards.EditCards(pStats2, 1);
                                }

                                if (GUILayout.Button("Set all cards as <color=purple>Queens</color>", GUIMenu.Button2))
                                {
                                    PlayerCards.EditCards(pStats2, 2);
                                }

                                if (GUILayout.Button("Set all cards as <color=orange>Aces</color>", GUIMenu.Button2))
                                {
                                    PlayerCards.EditCards(pStats2, 3);
                                }

                                if (GUILayout.Button("Set all cards as Jokers", GUIMenu.Button2))
                                {
                                    PlayerCards.EditCards(pStats2, 4);
                                }

                                if (GUILayout.Button("Set all cards as <color=red>Devil</color>", GUIMenu.Button2))
                                {
                                    PlayerCards.EditCards(pStats2, -1);
                                }
                            }
                        }
#endif

                        if (GUILayout.Button("TEST RPC", GUIMenu.Button2))
                        {
                            var Blorf = PlayerObject.CharController as BlorfGamePlay;
                            var UserCode_PlayLiarCMD = typeof(BlorfGamePlay).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(method => method.Name == "UserCode_PlayLiarCMD");

                            if (UserCode_PlayLiarCMD != null)
                            {
                                UserCode_PlayLiarCMD.Invoke(Blorf, null);

                            }
                            else
                            {
                                MelonLogger.Error("UserCode_PlayLiarCMD is null");
                            }

                        }

                        break;
                }

                if (Manager.Instance.GameStarted)
                {
                    if (GUILayout.Button("Print current position", GUIMenu.Button2))
                    {
                        if (PlayerObject.CharController != null)
                        {
                            MelonLogger.Msg("Pos: " + PlayerObject.CharController.transform.localPosition.ToString());
                        }
                    }
                }
            }

            if (GUILayout.Button("Change Username [HOST ONLY]", GUIMenu.Button2))
            {
                Console.Write("CustomName: ");
                NetObject.CustomNetName = Console.ReadLine();
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label("FreePos", GUIMenu.Label3, GUILayout.Width(GUIMenu.MainWindowRect.width * .250f));
            FreePosSpeed = GUILayout.HorizontalSlider(FreePosSpeed, 0.01f, 1.0f, GUIMenu.HorizontalSlider, GUIMenu.HorizontalSliderThumb);
            GUILayout.Label(" " + FreePosSpeed.ToString("0.00") + " ", GUIMenu.Label2, GUILayout.Width(50));
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("FreePosScale", GUIMenu.Label3, GUILayout.Width(GUIMenu.MainWindowRect.width * .250f));
            FreePosScale = GUILayout.HorizontalSlider(FreePosScale, 0.01f, 5.0f, GUIMenu.HorizontalSlider, GUIMenu.HorizontalSliderThumb);
            GUILayout.Label(" " + FreePosScale.ToString("0.00") + " ", GUIMenu.Label2, GUILayout.Width(50));
            GUILayout.EndHorizontal();

            ToggleCharTransform();

            NewToggleButton("CustomLocalPlayerSize [HOST ONLY]", ref LocalPlayerSize);
            if (LocalPlayerSize)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Scale", GUIMenu.Label3, GUILayout.Width(GUIMenu.MainWindowRect.width * 0.250f));
                LocalPlayerSizeF = GUILayout.HorizontalSlider(LocalPlayerSizeF, 0.01f, 5.0f, GUIMenu.HorizontalSlider, GUIMenu.HorizontalSliderThumb);
                GUILayout.Label(" " + LocalPlayerSizeF.ToString("0.00") + " ", GUIMenu.Label2, GUILayout.Width(50));
                GUILayout.EndHorizontal();
            }

            NewToggleButton("CustomNetPlayersSize [HOST ONLY]", ref NetPlayersSize);
            if (NetPlayersSize)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Scale", GUIMenu.Label3, GUILayout.Width(GUIMenu.MainWindowRect.width * 0.250f));
                NetPlayersSizeF = GUILayout.HorizontalSlider(NetPlayersSizeF, 0.01f, 5.0f, GUIMenu.HorizontalSlider, GUIMenu.HorizontalSliderThumb);
                GUILayout.Label(" " + NetPlayersSizeF.ToString("0.00") + " ", GUIMenu.Label2, GUILayout.Width(50));
                GUILayout.EndHorizontal();
            }

            NewToggleButton("MiniPigMod [HOST ONLY]", ref MiniPigMod);

            if (MiniPigMod)
            {
                NewToggleButton("MiniPigModShare ", ref MiniPigModShare);
                NewToggleButton("MiniPigModPapa ", ref MiniPigModPapa);
            }

            NewToggleButton("FreeButtonActive", ref FreeButtonActive);

            GUILayout.EndScrollView();

            //  NewToggleButton("LargeLobby", ref LargeLobby);
        }

        private static void ShowPlayersPage()
        {
            CustomNetworkManager net = NetworkManager.singleton as CustomNetworkManager;

            GUIMenu.scrollPlayers = GUILayout.BeginScrollView(GUIMenu.scrollPlayers, GUILayout.Width(GUIMenu.MainWindowRect.width));

            if (net != null && net.GamePlayers.Count > 0)
            {
                foreach (PlayerObjectController player in net.GamePlayers)
                {
                    ListPlayer(player);
                }
            }
            else
            {
                GUILayout.Label("No players connected.", GUILayout.ExpandWidth(true));
            }

            GUILayout.EndScrollView();
        }


        private static void ShowNetObjectsPage()
        {
            CustomNetworkManager net = NetworkManager.singleton as CustomNetworkManager;

            GUIMenu.scrollNetObject = GUILayout.BeginScrollView(GUIMenu.scrollNetObject, GUILayout.Width(GUIMenu.MainWindowRect.width));

            if (net != null && NetworkClient.spawned.Count > 0)
            {
                foreach (var entry in NetworkClient.spawned)
                {
                    NetworkIdentity networkObject = entry.Value;
                    ListNetworkIdentities(networkObject);
                }
            }
            else
            {
                GUILayout.Label("No NetObjects.", GUILayout.ExpandWidth(true));
            }

            GUILayout.EndScrollView();
        }


        private static void ShowGameObjectsPage()
        {
            GUIMenu.scrollCharGameObjects = GUILayout.BeginScrollView(GUIMenu.scrollCharGameObjects, GUILayout.Width(GUIMenu.MainWindowRect.width));

            if (ExploreGameObject != null && ExploreGameObject.transform.childCount > 0)
            {
                ShowChildObjectsRecursive(ExploreGameObject.transform, 0);
            }
            else if (ExploreGameObject != null && ExploreGameObject.transform.childCount == 0)
            {
                GUILayout.Label("No child objects found under the specified ExploreGameObject.", GUILayout.ExpandWidth(true));
            }
            else
            {
                GUILayout.Label("ExploreGameObject is not set. Please assign a valid GameObject.", GUILayout.ExpandWidth(true));
            }

            GUILayout.EndScrollView();

            GUILayout.Label("SelectedObject: " + (SelectedExploreGameObject?.name ?? "No Selected Object"), GUIMenu.Label3);

            if (SelectedExploreGameObject != null)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("ScaleObject", GUIMenu.Label3, GUILayout.Width(GUIMenu.MainWindowRect.width * .250f));

                float currentScale = SelectedExploreGameObject.transform.localScale.x;
                float newScale = GUILayout.HorizontalSlider(currentScale, 0.01f, 25.0f, GUIMenu.HorizontalSlider, GUIMenu.HorizontalSliderThumb);

                SelectedExploreGameObject.transform.localScale = Vector3.one * newScale;

                GUILayout.Label(" " + newScale.ToString("0.00") + " ", GUIMenu.Label2, GUILayout.Width(50));
                GUILayout.EndHorizontal();

                //if (GUILayout.Button("Set CustomTransform for Spawn", GUIMenu.Button2))
                //{
                //    TransformInits[SelectedExploreGameObject.name] = SelectedExploreGameObject.transform.localScale;
                //    MelonLogger.Msg($"Stored {SelectedExploreGameObject.name} transform localScale as a placeholder.");
                //}

            }

            //if (TransformInits.Count > 0)
            //{
            //    if (GUILayout.Button("Clear stored Scales", GUIMenu.Button2))
            //    {
            //        TransformInits.Clear();
            //        MelonLogger.Msg("Cleared stored Scales");
            //    }
            //}


        }

        private static void ShowChildObjectsRecursive(Transform parent, int depth)
        {
            foreach (Transform child in parent)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(new string(' ', depth * 4) + child.gameObject.name, GUILayout.ExpandWidth(true));
                if (GUILayout.Button("S", GUIMenu.Button2, GUILayout.Width(28)))
                {
                    SelectedExploreGameObject = child.gameObject;
                }
                GUILayout.EndHorizontal();

                if (child.childCount > 0)
                {
                    ShowChildObjectsRecursive(child, depth + 1);
                }
            }
        }


        // Utility methods for UI buttons
        public static void NewToggleButton(string text, ref bool toggle)
        {
            string isEnabled = toggle ? "<color=green>Enabled</color>" : "<color=red>Disabled</color>";

            GUILayout.BeginHorizontal();
            GUILayout.Label(text, GUILayout.Width(GUIMenu.MainWindowRect.width * 0.4f));

            if (GUILayout.Button(isEnabled, GUIMenu.Button))
            {
                toggle = !toggle;
            }

            GUILayout.EndHorizontal();
        }

        public static void ListPlayer(PlayerObjectController player)
        {
            LobbyController lobby = LobbyController.Instance;

            GUIStyle playerStyle = new GUIStyle(GUI.skin.box)
            {
                alignment = TextAnchor.MiddleLeft,
                padding = new RectOffset(10, 10, 5, 5),
                normal = { textColor = Color.white }
            };


            GUILayout.BeginHorizontal();

            try
            {
                GUILayout.Label("ID: " + player.netId + " " + player.NetworkPlayerName, GUILayout.ExpandWidth(true));

                //tags
                if (player.isLocalPlayer)
                {
                    GUILayout.Label("<b><color=yellow>L</color></b>", GUIMenu.Label2, GUILayout.Width(30));
                }
            }
            finally
            {
                GUILayout.EndHorizontal();

            }
        }


        public static void ListNetworkIdentities(NetworkIdentity netobj)
        {
            GUIStyle playerStyle = new GUIStyle(GUI.skin.box)
            {
                alignment = TextAnchor.MiddleLeft,
                padding = new RectOffset(10, 10, 5, 5),
                normal = { textColor = Color.white },
                richText = true
            };


            GUILayout.BeginHorizontal();

            try
            {

                var ownerName = "No Owner";
                var net = NetworkManager.singleton as CustomNetworkManager;

                if (netobj.connectionToClient != null && net != null && net.GamePlayers != null)
                {
                    var ownerPlayer = net.GamePlayers.FirstOrDefault(player => player.connectionToClient == netobj.connectionToClient);

                    if (ownerPlayer != null)
                    {
                        ownerName = ownerPlayer.NetworkPlayerName;
                    }
                }

                var nameColor = (netobj.gameObject.transform == ExploreGameObject) ? "<color=yellow>" : "<color=white>";
                GUILayout.Label("ID: " + netobj.netId + $" {nameColor}" + netobj.name + "</color> [owner: " + ownerName + "]", GUILayout.ExpandWidth(true));


                if (netobj.isServer)
                {
                    if (GUILayout.Button("R", GUIMenu.Button, GUILayout.Width(30)))
                    {
                        NetObject.RespawnNetworkObject(netobj);
                    }

                    if (GUILayout.Button("EX", GUIMenu.Button, GUILayout.Width(42)))
                    {
                        ExploreGameObject = netobj.gameObject.transform;
                    }

                    if (GUILayout.Button("M", GUIMenu.Button, GUILayout.Width(30)))
                    {
                        NetObject.SetNetFreeTranformObject(netobj);
                    }


                    if (GUILayout.Button("P", GUIMenu.Button, GUILayout.Width(30)))
                    {
                        MelonLogger.Msg("POS: " + netobj.transform.localPosition.ToString());
                    }
                }

            }
            finally
            {
                GUILayout.EndHorizontal();

            }
        }

        internal static void OnSceneWasInitialized(int buildindex, string sceneName)
        {
            ExploreGameObject = null;
            SelectedExploreGameObject = null;
        }

        public static void ToggleCharTransform()
        {
            GUILayout.BeginHorizontal();
            try
            {
                GUILayout.Label("CharTransform", GUIMenu.Label3, GUILayout.Width(GUIMenu.MainWindowRect.width * .4f));

                if (GUILayout.Button($"{PlayerObject.CharTransform.ToString()}", GUIMenu.Button))
                {
                    PlayerObject.CharTransform = (PlayerObject.CharControllerTransform)(((int)PlayerObject.CharTransform + 1) % Enum.GetValues(typeof(PlayerObject.CharControllerTransform)).Length);
                }

                if (GUILayout.Button("EX", GUIMenu.Button, GUILayout.Width(42)))
                {
                    if(PlayerObject.TransformObject != null)
                    {
                        ExploreGameObject = PlayerObject.TransformObject;
                        MelonLogger.Msg("CharTransform explore is set: " + ExploreGameObject.ToString());
                    } else
                    {
                        MelonLogger.Error("PlayerObject.TransformObject is null!");

                    }
                }

            }
            finally
            {
                GUILayout.EndHorizontal();
            }
        }

        public static bool BitchesGuiToggle = true;
        public static bool freePlayerPosToggle = false;
        public static bool freeNetObjPosToggle = false;
        public static bool ShowLiarsDeckCards = false;
        public static bool FreeLiarsDeckTurn = false;
        public static bool FreeCountDown = false;

        public static bool LocalPlayerSize = false;
        public static float LocalPlayerSizeF = 1.00f;

        public static bool NetPlayersSize = false;
        public static float NetPlayersSizeF = 1.00f;

        public static float FreePosSpeed = 0.05f;
        public static float FreePosScale = 0.01f;

        public static bool MiniPigMod = false;
        public static bool MiniPigModShare = false;
        public static bool MiniPigModPapa = false;

        public static bool FreeButtonActive = true;
        public static bool FreeThrowCard = false;

        public static bool LargeLobby = false;

        private static Transform ExploreGameObject;
        private static GameObject SelectedExploreGameObject;

        public static Dictionary<string, Vector3> TransformInits = new Dictionary<string, Vector3>();
    }
}
