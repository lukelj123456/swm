using UnityEngine;
using System.Collections;
using System.Collections.Generic;

    public class app : MonoBehaviour
    {
        public const string prefixOfVar = "@";
        public const string prefixOfExp = "=";
        public const string root_field_tag = "Root_Field";
        public const string root_task_tag = "Root_Task";
        public const string root_ui_tag = "Root_UI";
        public const string glass_dk_tag = "Glass_DK";
        public const string metal_dk_tag = "Metal_DK";
        public const string stone_dk_tag = "Stone_DK";
        public const string wood_dk_tag = "Wood_DK";
        public const string sofa_dk_tag = "Sofa_DK";
        public const string paper_dk_tag = "Paper_DK";

        public const string hit_hand_tag = "Hit_hand";
        public const string hit_head_tag = "Hit_head";
        public const string hit_bodyup_tag = "Hit_body_up";
        public const string hit_bodydown_tag = "Hit_body_down";
        public const string hit_leg_tag = "Hit_leg";

        public const string player_tag = "Player";

        public static bool debug = true;
        public static app instance;
 

        private static GameObject inputManagerObj;
        private static GameObject audioManager;
        public static void Init()
        {
            if (instance != null)
                return;
        }

        void OnGUI()
        {
            Color color = GUI.color;
            if (fadingTexture != null && fadingAlpha > 0)
            {
                GUI.color = new Color(fadingColor.r, fadingColor.g, fadingColor.b, fadingAlpha);
                GUI.depth = -1000;
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadingTexture);
            }

            for (int i = 0; debug && i < Mathf.Min(logContent.Count, 40); i++)
            {
                GUI.Label(new Rect(0, 32 * (i), Screen.width, 32), logContent[i]);
            }

            if (debug)
            {
                //GUI.Box(new Rect(Screen.width - 64, 0, 64, 32), string.Format("fps:{0}", app.state.fps));
            }

            GUI.color = color;


        }

        #region Log Functions
        List<string> logContent = new List<string>();
        public static void Log(object format, params object[] args)
        {
            if (format == null)
            {
                return;
            }

#if UNITY_EDITOR
            if (args.Length <= 0)
            {
                Debug.Log(format);
                LogToContent(format);
            }
            else
            {
                Debug.LogFormat(format.ToString(), args);
                LogToContent(format, args);
            }
#endif
        }

        public static void LogError(object format, params object[] args)
        {
            if (format == null)
            {
                return;
            }

            if (args.Length <= 0)
            {
                Debug.LogError(format);
                LogToContent(format);
            }
            else
            {
                Debug.LogErrorFormat(format.ToString(), args);
                LogToContent(format, args);
            }
        }

        public static void LogWarning(object format, params object[] args)
        {
            if (format == null)
            {
                return;
            }


            if (args.Length <= 0)
            {
                Debug.LogWarning(format);
                LogToContent(format);
            }
            else
            {
                Debug.LogWarningFormat(format.ToString(), args);
                LogToContent(format, args);
            }
        }

        static void LogToContent(object format, params object[] args)
        {
            if (!debug)
                return;

            instance.logContent.Insert(0, (args.Length <= 0) ? format.ToString() : string.Format(format.ToString(), args));
            if (instance.logContent.Count > 32)
            {
                int delCount = instance.logContent.Count - 32;
                instance.logContent.RemoveRange(instance.logContent.Count - delCount, delCount);
            }
        }
        #endregion

        #region Fading Functions

        private Color fadingColor;
        private Texture2D fadingTexture;
        private float fadingAlpha = 0;

        protected void FadeInit()
        {
            fadingColor = Color.white;
            fadingAlpha = 0;
            fadingTexture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            fadingTexture.SetPixel(0, 0, Color.black);
            fadingTexture.Apply();
        }

        public Coroutine FadeIn(float seconds)
        {
            return StartCoroutine(FadeInner(-1, seconds));
        }

        public Coroutine FadeOut(float seconds)
        {
            return StartCoroutine(FadeInner(1, seconds));
        }

        private IEnumerator FadeInner(int dir, float seconds)
        {
            float fadeSpeed = seconds <= 0 ? 1 : 1.0f / seconds;
            if (dir < 0)
            {
                while (fadingAlpha > 0)
                {
                    fadingAlpha = Mathf.Clamp01(fadingAlpha + dir * fadeSpeed * Time.deltaTime);
                    yield return 1;
                }
            }
            else
            {
                while (fadingAlpha < 1)
                {
                    fadingAlpha = Mathf.Clamp01(fadingAlpha + dir * fadeSpeed * Time.deltaTime);
                    yield return 1;
                }
            }
        }
        #endregion


    }

