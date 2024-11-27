using LiarMod.Net;
using LiarMod.OnGui;
using MelonLoader;
using Mirror;
using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LiarMod.Player
{
    public static class PlayerObject
    {
        public enum CharControllerTransform { CharController, HeadPivot }

        public static bool freepos
        {
            get { return using_freepos; }
            private set
            {
                if (value)
                {
                    if (!cache_LocalGamePlayer()) return;

                    if (TransformObject != null && (cache_pos == null || cache_rot == null || cache_scale == null))
                    {
                        cache_pos = TransformObject.position;
                        cache_rot = TransformObject.rotation;
                        cache_scale = TransformObject.localScale;
                    }

                    MelonLogger.Msg("FreePos: " + using_freepos);
                }
                using_freepos = value;
            }
        }

        public static bool ShowHeadMeshes
        {
            get { return _ShowHeadMeshes; }
            set
            {
                _ShowHeadMeshes = value;
                MelonLogger.Msg("_ShowHeadMeshes: " + _ShowHeadMeshes);
            }
        }


        private static bool cache_LocalGamePlayer()
        {
            var scene = SceneManager.GetActiveScene();
            CustomNetworkManager net = NetworkManager.singleton as CustomNetworkManager;

            if(scene.name == "SteamLobby")
            {
                GameObject foundObject = GameObject.FindObjectsOfType<GameObject>()
                        .FirstOrDefault(obj => obj.name.Contains("LocalGamePlayer"));

                if (foundObject == null)
                {
                    MelonLogger.Error("no LocalGamePlayer found");
                    return false;
                }
                TransformObject = foundObject.transform;
                return true;
            }

            if (Manager.Instance != null)
            {
                if (Manager.Instance.GameStarted)
                {
                    LocalPlayerController = net.GamePlayers.FirstOrDefault(player => player.isLocalPlayer);

                    if (LocalPlayerController != null && CharController == null)
                    {
                        CharController = LocalPlayerController.GetComponent<CharController>();
                    }

                    if (CharController == null)
                    {
                        MelonLogger.Error("no CharController found");
                        return false;
                    }

                    switch (CharTransform)
                    {
                        case CharControllerTransform.HeadPivot:
                            TransformObject = CharController.HeadPivot.transform;
                            break;
                        default:
                            TransformObject = CharController.transform;
                            break;
                    }

                    return true;

                }
            }

            TransformObject = null;
            LocalPlayerController = null;
            MelonLogger.Error("No Player in the game!");
            return false;
        }

        public static void OnUpdate()
        {
            if (LiarMenu.freePlayerPosToggle != freepos || Input.GetKeyDown(KeyCode.F6))
            {
                freepos = !freepos;
            }

            if (Input.GetKeyDown(KeyCode.F8))
            {
                ShowHeadMeshes = !ShowHeadMeshes;
            }

            if (TransformObject)
            {
                if (Input.GetKeyDown(KeyCode.F9) && freepos)
                {
                    TransformObject.position = cache_pos.Value;
                    TransformObject.rotation = cache_rot.Value;
                    TransformObject.localScale = cache_scale.Value;
                }

                if (using_freepos)
                {
                    NetObject.FreeTransformPos(TransformObject, LiarMenu.FreePosSpeed);
                    NetObject.FreeTransformScale(TransformObject, LiarMenu.FreePosScale);
                }
            }

            LiarMenu.freePlayerPosToggle = freepos;

            
        }

        internal static void OnSceneWasInitialized(int buildindex, string sceneName)
        {
            TransformObject = null;
            LocalPlayerController = null;
            freepos = false;
            cache_pos = null;
            cache_rot = null;
            cache_scale = null;
        }

        public static PlayerObjectController LocalPlayerController;
        public static CharController CharController;
        public static Transform TransformObject;
        public static CharControllerTransform CharTransform;

        private static bool _ShowHeadMeshes = false;
        private static bool using_freepos = false;

        private static Nullable<Vector3> cache_pos;
        private static Nullable<Quaternion> cache_rot;
        private static Nullable<Vector3> cache_scale;
    }
}
