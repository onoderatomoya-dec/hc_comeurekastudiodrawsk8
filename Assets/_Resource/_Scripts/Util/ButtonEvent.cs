using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Globalization;
namespace OTCode
{
    public class ButtonEvent : MonoBehaviour
    {
        // 定数

        // GameObject

        // Prefab


        // 変数
        public static bool _isEvent = false;
        private Action _event = null;
        private int _id = 0;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SelectButton()
        {
            if (_event == null || _isEvent)
            {
                return;
            }
            _event();
        }
        public void SetEvent(Action action)
        {
            _event = action;
        }

        public void SetId(int id)
        {
            _id = id;
        }
        public int GetId()
        {
            return _id;
        }
    }
}