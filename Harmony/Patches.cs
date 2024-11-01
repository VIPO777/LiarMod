using Dissonance.Integrations.MirrorIgnorance;
using HarmonyLib;
using LiarMod.Net;
using LiarMod.OnGui;
using LiarMod.Player;
using MelonLoader;
using Mirror;
using System;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LiarMod.Harmony
{
    public static class Patches
    {
        [HarmonyPatch(typeof(Intro), "Start")]
        private static class PatchIntro
        {

            private static void Postfix()
            {
                SceneManager.LoadScene("SteamTest");
                MelonLogger.Warning("SKIP Intro");
            }
        }

        [HarmonyPatch(typeof(CharController), "Start")]
        private static class CharControllerStart
        {
            private static void Prefix()
            {

            }

            private static void Postfix(ref CharController __instance)
            {
                if (__instance.isOwned)
                {
                    PlayerObject.CharController = __instance;
                    MelonLogger.Warning("CharController setted: " + __instance);
                }

                //NetObject.ChildObjectsRecursive(__instance.transform, 0);
            }
        }

        [HarmonyPatch(typeof(CharController), "Update")]
        private static class CharControllerUpdate
        {
            private static void Prefix(ref CharController __instance)
            {

                if (__instance.isOwned)
                {
                    var playerStats = typeof(CharController).GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(field => field.Name == "playerStats").GetValue(__instance) as PlayerStats;

                    if (playerStats != null)
                    {
                        if (playerStats.HaveTurn == false && LiarMenu.FreeLiarsDeckTurn)
                        {
                            playerStats.HaveTurn = true;
                        }
                    }
                }
            }

            private static void Postfix(ref CharController __instance)
            {
                if (!__instance.isOwned)
                    return;

                if (PlayerObject.ShowHeadMeshes)
                {
                    __instance.HeadMeshes.ForEach(delegate (GameObject x)
                    {
                        x.SetActive(true);
                    });
                }
            }
        }

        [HarmonyPatch]
        // https://github.com/dogdie233/LiarsBarEnhance/blob/master/LiarsBarEnhance/Features/BigMouthPatch.cs
        private static class BigMouthPatch
        {
            [HarmonyPatch(typeof(FaceAnimator), "Update")]
            private static void Postfix(FaceAnimator __instance)
            {
                if (!__instance.isOwned)
                    return;

                Vector3 pos = __instance.Mouth.transform.localPosition;
                if (Input.GetKey(KeyCode.O))
                    __instance.Mouth.transform.localEulerAngles = new Vector3(pos.x, pos.y, 300f);
            }
        }


        [HarmonyPatch(typeof(BlorfGamePlay), "CardSelectInput")]
        private static class BlorfGameCardSelectInput
        {
            private static void Prefix(ref BlorfGamePlay __instance)
            {
                if (__instance.isOwned)
                {
                    var manager = typeof(CharController).GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(field => field.Name == "manager").GetValue(__instance) as Manager;

                    // Not recommend.
                    if (LiarMenu.FreeThrowCard)
                    {
                        var throwedField = typeof(BlorfGamePlay).GetField("throwed", BindingFlags.NonPublic | BindingFlags.Instance);
                        var currentcard = typeof(BlorfGamePlay).GetField("currentcard", BindingFlags.NonPublic | BindingFlags.Instance);
                        var sfxsource = typeof(BlorfGamePlay).GetField("sfxsource", BindingFlags.NonPublic | BindingFlags.Instance);
                        var SelectEffect = typeof(BlorfGamePlay).GetField("SelectEffect", BindingFlags.NonPublic | BindingFlags.Instance);


                        if (Input.GetKeyDown(KeyCode.Space) && !(bool)throwedField.GetValue(__instance))
                        {
                            if (__instance.Cards[(int)currentcard.GetValue(__instance)].GetComponent<Card>().Selected)
                            {
                                __instance.Cards[(int)currentcard.GetValue(__instance)].GetComponent<Card>().Selected = false;
                            }
                            else
                            {
                                if (__instance.Cards.Where((GameObject x) => x.GetComponent<Card>().Selected && x.GetComponent<Card>().Devil).Count<GameObject>() == 0)
                                {
                                    __instance.Cards[(int)currentcard.GetValue(__instance)].GetComponent<Card>().Selected = true;
                                    goto IL_0499;
                                    manager.BlorfGame.DevilCardInfo.SetTrigger("Oynat");
                                    goto IL_0499;
                                }

                                if (manager.BlorfGame.DeckMode == BlorfGamePlayManager.deckmode.Devil)
                                {
                                    if (__instance.Cards.Where((GameObject x) => x.GetComponent<Card>().Selected && x.GetComponent<Card>().Devil).Count<GameObject>() == 0)
                                    {
                                        manager.BlorfGame.DevilCardInfo.SetTrigger("Oynat");
                                    }
                                }
                            }
                        IL_0499:
                            ((AudioSource)sfxsource.GetValue(__instance)).clip = (AudioClip)sfxsource.GetValue(__instance);
                            ((AudioSource)sfxsource.GetValue(__instance)).Play();
                        }
                    }

                    if (manager != null)
                    {
                        if (LiarMenu.FreeCountDown || LiarMenu.FreeLiarsDeckTurn)
                        {
                            manager.CountDown = 30f;
                        }
                    }

                }
            }
        }


        [HarmonyPatch(typeof(NetworkServer), "SendSpawnMessage")]
        private static class CharControllerAwake
        {
            private static void Prefix(object[] __args)
            {
                if (__args == null || __args.Length == 0) return;

                var identity = __args[0] as NetworkIdentity;
                var net = NetworkManager.singleton as CustomNetworkManager;

                if (identity != null)
                {
                    if (identity.connectionToClient != null)
                    {
                        var localplayer = net.GamePlayers.FirstOrDefault(player => player.isLocalPlayer);

                        if (LiarMenu.LocalPlayerSize && (identity.isLocalPlayer || identity.connectionToClient == localplayer.connectionToClient))
                        {
                            identity.transform.localScale = Vector3.one * LiarMenu.LocalPlayerSizeF;

                            if (LiarMenu.MiniPigMod && LiarMenu.MiniPigModPapa)
                                identity.transform.localScale = Vector3.one * 0.53f;

                        }
                        else if (LiarMenu.NetPlayersSize && !identity.isLocalPlayer)
                        {
                            identity.transform.localScale = Vector3.one * LiarMenu.NetPlayersSizeF;
                        }

                        if (LiarMenu.MiniPigMod)
                        {
                            identity.transform.localScale = Vector3.one * 0.42f;
                            NetObject.UpdatePositionMiniPig(identity);
                        }
                    }
                }
            }

            private static void Postfix(object[] __args)
            {
                //if (__args == null || __args.Length == 0) return;

                //var identity = __args[0] as NetworkIdentity;
                //var conn = __args[1] as NetworkConnection;

                //if (identity.serverOnly)
                //    return;

            }

        }

        [HarmonyPatch]
        // https://github.com/dogdie233/LiarsBarEnhance/blob/master/LiarsBarEnhance/Features/RemoveHeadRotationlimitPatch.cs
        public class RemoveHeadRotationlimitPatch
        {
            [HarmonyPatch(typeof(CharController), "ClampAngle")]
            private static void Prefix(float lfAngle, ref float __result, ref bool __runOriginal)
            {
                __runOriginal = false;

                var newAngle = lfAngle;

                if (newAngle < -360.0)
                    newAngle += 360f;

                if (newAngle > 360.0)
                    newAngle -= 360f;

                __result = newAngle;
            }
        }


        [HarmonyPatch]
        // https://github.com/dogdie233/LiarsBarEnhance/blob/master/LiarsBarEnhance/Features/CrazyShakeHeadPatch.cs
        private class CrazyShakeHeadPatch
        {
            public static bool IsEnabled { get; private set; }

            private static FieldInfo _cinemachineTargetYaw = typeof(CharController).GetField("_cinemachineTargetYaw", BindingFlags.NonPublic | BindingFlags.Instance);
            private static FieldInfo _cinemachineTargetPitch = typeof(CharController).GetField("_cinemachineTargetPitch", BindingFlags.NonPublic | BindingFlags.Instance);
            private static float _savedYaw, _savedPitch;

            public static void SetEnabled(CharController instance, bool value)
            {
                if (value == IsEnabled)
                    return;
                IsEnabled = value;

                if (value)
                {
                    _savedYaw = (float)_cinemachineTargetYaw.GetValue(instance);
                    _savedPitch = (float)_cinemachineTargetPitch.GetValue(instance);
                }
                else
                {
                    _cinemachineTargetYaw.SetValue(instance, _savedYaw);
                    _cinemachineTargetPitch.SetValue(instance, _savedPitch);
                }
            }

            public static void ToggleEnabled(CharController charController)
                => SetEnabled(charController, !IsEnabled);

            [HarmonyPatch(typeof(CharController), "RotateInFrame")]
            [HarmonyPostfix]
            private static void RotateInFramePostfix(CharController __instance, float ___MinX, float ___MaxX, float ___MinY, float ___MaxY)
            {
                if (Input.GetKeyDown(KeyCode.I))
                    ToggleEnabled(__instance);

                if (!IsEnabled)
                    return;

                var limited = Input.GetKey(KeyCode.I);
                var x = UnityEngine.Random.Range(limited ? ___MinX : 0, limited ? ___MaxX : 360);
                var y = UnityEngine.Random.Range(limited ? ___MinY : 0, limited ? ___MaxY : 360);

                _cinemachineTargetYaw.SetValue(__instance, x);
                _cinemachineTargetPitch.SetValue(__instance, y);
                __instance.HeadPivot.transform.localRotation = Quaternion.Euler(x, 0f, y);
            }
        }

        [HarmonyPatch]
        // https://github.com/dogdie233/LiarsBarEnhance/blob/master/LiarsBarEnhance/Features/ChineseNameFixPatch.cs
        public class ChineseNameFixPatch
        {
            [HarmonyPatch(typeof(FontChanger), "Update")]
            [HarmonyPostfix]
            public static void UpdatePostfix(FontChanger __instance, Fonts ___fonts)
            {
                __instance.GetComponent<TMP_Text>().font = ___fonts.getCurrentFont(__instance.deffaultfont);
            }

            [HarmonyPatch(typeof(BlorfGamePlayManager), "Start")]
            [HarmonyPostfix]
            public static void BlorfGamePlayManagerStartPostfix(BlorfGamePlayManager __instance)
            {
                if (__instance.LastBidName1.gameObject.GetComponent<FontChanger>() == null) {
                    __instance.LastBidName1.gameObject.AddComponent<FontChanger>();
                }
                else {
                    __instance.LastBidName1.gameObject.GetComponent<FontChanger>();
                }
            }
        }

        [HarmonyPatch(typeof(Debug))]
        public static class DebugLogPatch
        {
            [HarmonyPatch("Log", new Type[] { typeof(object) })]
            [HarmonyPrefix]
            public static bool LogPrefix(object message)
            {
                MelonLogger.Msg("[UnityLog] " + message);
                return true; 
            }

            [HarmonyPatch("LogWarning", new Type[] { typeof(object) })]
            [HarmonyPrefix]
            public static bool LogWarningPrefix(object message)
            {
                MelonLogger.Warning("[UnityLog] [Warning] " + message);
                return true; 
            }

            [HarmonyPatch("LogError", new Type[] { typeof(object) })]
            [HarmonyPrefix]
            public static bool LogErrorPrefix(object message)
            {
                MelonLogger.Error("[UnityLog] [Error] " + message);
                return true;
            }
        }

        [HarmonyPatch(typeof(MirrorIgnorancePlayer), "CmdSetPlayerName")]
        private static class MirrorIgnorancePlayerPlayerNameSet
        {
            private static void Prefix(ref MirrorIgnorancePlayer __instance, ref string playerName)
            {
                if (__instance.isOwned)
                {
                    if (NetObject.CustomNetName != null)
                    {
                        playerName = NetObject.CustomNetName;
                        MelonLogger.Warning("MirrorIgnorancePlayerName Set Custom: " + playerName);
                    }
                }
            }
        }


        [HarmonyPatch(typeof(SteamLobby), "HostLobby")]
        private static class SteamLobbyHostLobby
        {
            private static void Prefix(ref SteamLobby __instance)
            {
                var lobbyname = typeof(SteamLobby).GetField("lobbyname", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance) as TMP_InputField;

                if (LiarMenu.MiniPigMod && LiarMenu.MiniPigModShare)
                {
                    lobbyname.text = "MINI PIG";
                }

            }
        }

        //[HarmonyPatch(typeof(SteamMatchmaking), "CreateLobby")]
        //private static class SteamMatchmakingCreateLobby
        //{
        //    private static void Prefix(object[] __args)
        //    {
        //        int cMaxMembers = (int)__args[1];
        //        if (LiarMenu.LargeLobby)
        //        {
        //            cMaxMembers = 100;
        //        }

        //        MelonLogger.Msg("CreateLobby " + cMaxMembers);

        //    }
        //}

        [HarmonyPatch(typeof(PlayerObjectController), "SerializeSyncVars")]
        // HOST ONLY
        private static class PlayerObjectSerializeSyncVars
        {
            private static void Prefix(ref PlayerObjectController __instance)
            {
                if (__instance.isOwned)
                {
                    if (NetObject.CustomNetName != null)
                    {
                        __instance.PlayerName = NetObject.CustomNetName;
                        //MelonLogger.Warning("NetworkPlayerName SerializeSyncVar: " + __instance.PlayerName);
                    }
                }

                var net = NetworkManager.singleton as CustomNetworkManager;

                if (LiarMenu.MiniPigMod && LiarMenu.MiniPigModShare)
                {
                    var client = __instance.connectionToClient;

                    var playerdef = net.GamePlayers.FirstOrDefault(player => player.connectionToClient == client);

                    if (playerdef != null)
                    {
                        int playerIndex = net.GamePlayers.IndexOf(playerdef) + 1;
                        __instance.PlayerName = "MINI PIG " + (playerIndex == 1 ? " PAPA" : playerIndex.ToString());
                    }
                    else
                    {
                        __instance.PlayerName = "MINI PIG";
                    }

                    __instance.PlayerSkin = 2; // MY PIG
                    __instance.Ready = true;
                }

            }
        }


        [HarmonyPatch(typeof(LobbyController), "Update")]
        private static class LobbyControllerUpdate
        {
            private static void Postfix(ref LobbyController __instance)
            {
                if (LiarMenu.FreeButtonActive)
                {
                    __instance.StartButtonActive.SetActive(true);
                    __instance.StartButtonPassive.SetActive(false);
                    return;
                }
            }

        }

        /*
        [HarmonyPatch(typeof(LobbyController), "Start")]
        private static class LobbyControllerStart
        {
            private static void Postfix(ref LobbyController __instance)
            {
                if (__instance.SpawnSlots != null && __instance.SpawnSlots.Count > 0)
                {

                    var slot = __instance.SpawnSlots.FirstOrDefault();
                    GameObject clonedObject = UnityEngine.Object.Instantiate(slot.gameObject);
                    clonedObject.name = slot.name + "_Clone_" + Guid.NewGuid().ToString("N");

                }
                else
                {
                    MelonLogger.Error("LobbyController.SpawnSlots is null or empty!");
                }
            }

        }
         */

    }
}
