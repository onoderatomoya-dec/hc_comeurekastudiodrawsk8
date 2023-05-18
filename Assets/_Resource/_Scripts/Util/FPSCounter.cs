using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace OTCode
{
    public class FPSCounter : SingletonMonoBehaviour<FPSCounter>
    {
        [SerializeField]
        private float updateInterval = 0.5f;
        private float accum;
        private int frames;
        private float timeleft;
        private float fps;
        private GUIStyle _guiStyle;

        void Awake()
        {
            if (!base.Awake())
            {
                return;
            }
            // ここから初期化処理を行う
        }

        void OnEnable()
        {
            base.OnEnable();
        }

        void Start()
        {
            base.Start();
            _guiStyle = new GUIStyle();
            _guiStyle.fontSize = 35;
        }

        void Update()
        {
            base.Update();
            timeleft -= Time.deltaTime;
            accum += Time.timeScale / Time.deltaTime;
            frames++;

            if (0 < timeleft) return;

            fps = accum / frames;
            timeleft = updateInterval;
            accum = 0;
            frames = 0;
        }

        private void OnGUI()
        {
            if (PlayingDataManager._isDebug)
            {
                GUILayout.Label("FPS: " + fps.ToString("f2"), _guiStyle);
            }
        }
    }
}