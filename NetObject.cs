using LiarMod.OnGui;
using MelonLoader;
using Mirror;
using System;
using UnityEngine;

namespace LiarMod.Net
{
    public static class NetObject
    {
        public static string CustomNetName
        {
            get { return _CustomNetName; }
            set
            {
                _CustomNetName = value;
                MelonLogger.Msg($"New NetworkName {_CustomNetName}");
            }
        }

        public static bool freeposnetobj
        {
            get { return using_freeposnetobj; }
            private set
            {
                if (TransformNetObject == null)
                {
                    MelonLogger.Error("no TransformNetObject found");
                    return;
                }

                if (value)
                {
                    if (cache_pos == null || cache_rot == null || cache_scale == null)
                    {
                        cache_pos = TransformNetObject.position;
                        cache_rot = TransformNetObject.rotation;
                        cache_scale = TransformNetObject.localScale;
                    }
                    using_freeposnetobj = value;
                    MelonLogger.Msg("freeposnetobj Obj: " + TransformNetObject);
                }
                else
                {
                    using_freeposnetobj = value;
                }

            }
        }

        public static void OnUpdate()
        {
            if (LiarMenu.freeNetObjPosToggle != freeposnetobj)
            {
                freeposnetobj = !freeposnetobj;
            }

            if (TransformNetObject)
            {
                if (Input.GetKeyDown(KeyCode.F9) && freeposnetobj)
                {
                    TransformNetObject.position = cache_pos.Value;
                    TransformNetObject.rotation = cache_rot.Value;
                    TransformNetObject.localScale = cache_scale.Value;
                }

                if (using_freeposnetobj)
                {
                    NetObject.FreeTransformPos(TransformNetObject, LiarMenu.FreePosSpeed);
                    NetObject.FreeTransformScale(TransformNetObject, LiarMenu.FreePosScale);
                }
            }

            LiarMenu.freeNetObjPosToggle = freeposnetobj;
        }

        public static void SetNetFreeTranformObject(NetworkIdentity netobj)
        {
            if (netobj != null && netobj.isServer)
            {
                TransformNetObject = netobj.transform;
                cache_pos = null;
                cache_rot = null;
                cache_scale = null;

                MelonLogger.Msg("NetFreeTranformObject selected: " + TransformNetObject);
            }
        }

        public static void RespawnNetworkObject(NetworkIdentity netobj)
        {
            if (netobj != null && netobj.isServer)
            {
                NetworkConnectionToClient originalOwner = netobj.connectionToClient;

                Vector3 spawnPosition = netobj.transform.position;
                Quaternion spawnRotation = netobj.transform.rotation;
                Vector3 spawnScale = netobj.transform.localScale;

                NetworkServer.Destroy(netobj.gameObject);

                GameObject newObject = UnityEngine.Object.Instantiate(netobj.gameObject, spawnPosition, spawnRotation);
                newObject.transform.localScale = spawnScale;

                NetworkServer.Spawn(newObject, originalOwner);
            }
        }

        public static void CloneNetworkObject(NetworkIdentity originalNetObj)
        {
            if (originalNetObj == null)
            {
                MelonLogger.Msg("Error: The provided NetworkIdentity is null.");
                return;
            }

            GameObject clonedObject = UnityEngine.Object.Instantiate(originalNetObj.gameObject, originalNetObj.transform.position, originalNetObj.transform.rotation);
            clonedObject.transform.localScale = originalNetObj.transform.localScale;

            NetworkIdentity clonedNetIdentity = clonedObject.GetComponent<NetworkIdentity>();
            if (clonedNetIdentity != null)
            {
                UnityEngine.Object.DestroyImmediate(clonedNetIdentity);
            }

            clonedNetIdentity = clonedObject.AddComponent<NetworkIdentity>();
            NetworkServer.Spawn(clonedObject);

            MelonLogger.Msg("Cloned object with a fresh NetworkIdentity successfully: " + clonedObject.name);
        }

        internal static void FreeTransformPos(Transform obj, float speed)
        {
            float rotation_speed = 1.0f;

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                obj.position += obj.right * -speed;

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                obj.position += obj.right * speed;

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                obj.position += obj.forward * speed;

            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                obj.position += obj.forward * -speed;

            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.PageUp))
                obj.position += obj.up * speed;

            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.PageDown))
                obj.position += obj.up * -speed;

            obj.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * rotation_speed);

            if (obj.eulerAngles.z != 0)
                obj.eulerAngles = new Vector3(obj.eulerAngles.x, obj.eulerAngles.y, 0);
        }

        internal static void FreeTransformScale(Transform obj, float speed)
        {
            if (Input.GetKey(KeyCode.Q))
                obj.localScale += obj.localScale * speed;

            if (Input.GetKey(KeyCode.E))
                obj.localScale -= obj.localScale * speed;
        }

        public static void ChildObjectsRecursive(Transform parent, int depth)
        {
            foreach (Transform child in parent)
            {
                if (LiarMenu.TransformInits.ContainsKey(child.name))
                {
                    child.localScale = LiarMenu.TransformInits[child.name];
                    MelonLogger.Warning("Applied custom scale for child " + child.name + ": " + child.localScale);
                }

                if (child.childCount > 0)
                {
                    ChildObjectsRecursive(child, depth + 1);
                }
            }
        }

        public static void UpdatePositionMiniPig(NetworkIdentity identity)
        {
            // Better to work with prefabs :) But not so
            Vector3 currentPosition = identity.transform.localPosition;

            if (IsApproximatelyEqual(currentPosition, new Vector3(0.36f, 0.30f, -9.79f)))
            {
                identity.transform.localPosition = new Vector3(0.37f, 1.10f, -9.20f);
            }
            else if (IsApproximatelyEqual(currentPosition, new Vector3(1.69f, 0.30f, -8.46f)))
            {
                identity.transform.localPosition = new Vector3(1.09f, 1.09f, -8.48f);
            }
            else if (IsApproximatelyEqual(currentPosition, new Vector3(0.36f, 0.30f, -7.13f)))
            {
                identity.transform.localPosition = new Vector3(0.39f, 1.12f, -7.72f);
            }
            else if (IsApproximatelyEqual(currentPosition, new Vector3(-0.97f, 0.30f, -8.46f)))
            {
                identity.transform.localPosition = new Vector3(-0.43f, 1.11f, -8.41f);
            }
        }

        public static bool IsApproximatelyEqual(Vector3 a, Vector3 b, float tolerance = 0.01f)
        {
            return Vector3.Distance(a, b) < tolerance;
        }

        private static Transform TransformNetObject;
        private static bool using_freeposnetobj = false;
        private static string _CustomNetName;

        private static Nullable<Vector3> cache_pos;
        private static Nullable<Quaternion> cache_rot;
        private static Nullable<Vector3> cache_scale;
    }
}
