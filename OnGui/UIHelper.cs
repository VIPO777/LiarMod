using MelonLoader;
using UnityEngine;

namespace LiarMod.OnGui
{
    public static class UIHelper
    {
        public static void TextureApplier(Texture2D tex2D, float r, float g, float b, float a)
        {
            string colorS = string.Format("({0},{1},{2},{3})", r, g, b, a);
            tex2D.SetPixels(new[] { new Color(r, g, b, a) });
            tex2D.Apply();
        }

        public static void TextureApplier(Texture2D tex2D, Color color)
        {
            tex2D.SetPixels(new[] { color });
            tex2D.Apply();
        }

        public static void TextureApplier(Texture2D tex2D, string hex)
        {
            Color color;
            if (ColorUtility.TryParseHtmlString(hex, out color))
            {
                tex2D.SetPixels(new[] { color });
                tex2D.Apply();
            }
            else
                MelonLogger.Msg("Couldn't apply texture!");
        }

        public static void NewButtonValue(string content, ref float value, float min = 0, float max = 255)
        {
            GUILayout.BeginVertical();
            try
            {
                GUILayout.Label("<b>" + content + "</b>", GUIMenu.Label2);
                GUILayout.BeginHorizontal();
                try
                {
                    if (GUILayout.Button("<b> << </b>", GUIMenu.Button, GUILayout.Width(35)))
                        value--;
                    GUILayout.Box(value.ToString(), GUIMenu.Box2);
                    if (GUILayout.Button("<b> >> </b>", GUIMenu.Button, GUILayout.Width(35)))
                        value++;

                    value = Mathf.Clamp(value, min, max);
                }
                finally
                {
                    GUILayout.EndHorizontal();
                }
            }
            finally
            {
                GUILayout.EndVertical();
            }
        }

        public static void NewButtonValue(string content, ref int value, int min = 0, int max = 255)
        {
            GUILayout.BeginVertical();
            try
            {
                GUILayout.Label("<b>" + content + "</b>", GUIMenu.Label2);
                GUILayout.BeginHorizontal();
                try
                {
                    if (GUILayout.Button("<b> << </b>", GUIMenu.Button, GUILayout.Width(35)))
                        value--;
                    GUILayout.Box(value.ToString(), GUIMenu.Box2);
                    if (GUILayout.Button("<b> >> </b>", GUIMenu.Button, GUILayout.Width(35)))
                        value++;

                    value = Mathf.Clamp(value, min, max);
                }
                finally
                {
                    GUILayout.EndHorizontal();
                }
            }
            finally
            {
                GUILayout.EndVertical();
            }
        }

        public static void NewDoMethodButton(string text, ExecuteMethod method)
        {
            if (GUILayout.Button(text))
                method();
        }

        public delegate void ExecuteMethod();
    }
}
