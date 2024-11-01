using LiarMod.Player;
using MelonLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LiarMod.freecam
{
    // https://github.com/oiyl/unity-freecam-melonloader
    public static class freecam
    {
        private static void cache_freecam()
        {
            main_camera = Camera.main;
            if (main_camera != null)
                cached_camera = main_camera;
            else
                MelonLogger.Warning("no camera found");
        }

        private static void enable_freecam()
        {
            using_freecam = true;
            using_move = true;
            cache_freecam();

            if (cached_camera != null)
            {
                cached_camera.enabled = false;

                if (our_camera == null)
                {
                    our_camera = new GameObject("freecam").AddComponent<Camera>();
                    our_camera.gameObject.tag = "MainCamera";

                    /* camera should persist on new scenes */
                    GameObject.DontDestroyOnLoad(our_camera.gameObject);

                    our_camera.gameObject.hideFlags = HideFlags.HideAndDontSave;
                }
                /* set pos and rot to cached camera */
                our_camera.transform.position = cached_camera.transform.position;
                our_camera.transform.rotation = cached_camera.transform.rotation;

                our_camera.gameObject.SetActive(true);
                our_camera.enabled = true;
                PlayerObject.ShowHeadMeshes = true;
            }
            else
            {
                MelonLogger.Error("error: no camera");
            }
        }

        private static void disable_freecam()
        {
            using_freecam = false;
            PlayerObject.ShowHeadMeshes = false;

            if (cached_camera != null)
            {
                cached_camera.enabled = true;
            }

            if (our_camera != null)
            {
                UnityEngine.Object.Destroy(our_camera.gameObject);
            }
        }

        public static void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
            if (sceneName == "Game")
            {
                disable_freecam();
            }
        }

        public static void OnUpdate()
        {
            if (SceneManager.GetActiveScene().name == "Game")
            {
                if (Input.GetKeyDown(KeyCode.F5))
                {
                    if (!using_freecam)
                        enable_freecam();
                    else
                        disable_freecam();
                }

                if (Input.GetKeyDown(KeyCode.F10))
                {
                    using_move = !using_move;
                }

                if (using_freecam && using_move)
                {
                    float speed = 0.1f;
                    float rotation_speed = 1.0f;


                    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                        our_camera.transform.position += our_camera.transform.right * -speed;

                    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                        our_camera.transform.position += our_camera.transform.right * speed;

                    if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                        our_camera.transform.position += our_camera.transform.forward * speed;

                    if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                        our_camera.transform.position += our_camera.transform.forward * -speed;

                    if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.PageUp))
                        our_camera.transform.position += our_camera.transform.up * speed;

                    if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.PageDown))
                        our_camera.transform.position += our_camera.transform.up * -speed;

                    /* mouse movement */
                    our_camera.transform.Rotate(new UnityEngine.Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * rotation_speed);

                    /* lock z rotation */
                    if (our_camera.transform.eulerAngles.z != 0)
                        our_camera.transform.eulerAngles = new UnityEngine.Vector3(our_camera.transform.eulerAngles.x, our_camera.transform.eulerAngles.y, 0);
                }
            }

        }

        private static Camera main_camera;
        private static Camera cached_camera;
        private static Camera our_camera;
        private static bool using_freecam = false;
        private static bool using_move = false;
    }
}
