//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace StarForce {
    public class MainForm : UGuiForm {

        private ProcedureMain m_ProcedureMain = null;
        private Text logger = null;
        private Image loggerBg = null;

        private Text outText = null;
        private Image outBg = null;

        public void OnResetButtonClick() {
            m_ProcedureMain.OnReset();
        }

        public void OnStartButtonClick() {
            m_ProcedureMain.Start();
        }

        public void UpdateLogger(string s, InfoTypes infoType) {
            logger.text = s;
            if (infoType == InfoTypes.Error) loggerBg.color = Color.red;
            else if(infoType == InfoTypes.Wram) loggerBg.color = Color.yellow;
            else if(infoType == InfoTypes.Info) loggerBg.color = Color.white;
        }

        public void UpdateOutPut(string s, InfoTypes infoType = InfoTypes.Info) {
            outText.text = s;
            if (infoType == InfoTypes.Error) outBg.color = Color.red;
            else if (infoType == InfoTypes.Wram) outBg.color = Color.yellow;
            else if (infoType == InfoTypes.Info) outBg.color = Color.white;
        }


#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);
            var curProcedure = GameEntry.Procedure.CurrentProcedure;
            m_ProcedureMain = (ProcedureMain)curProcedure;
            if (m_ProcedureMain == null) {
                Log.Warning("m_ProcedureMain is invalid when open MainForm.");
                return;
            }
            logger = GameObject.Find("MyLogText").GetComponent<Text>();
            loggerBg = GameObject.Find("MyLogger").GetComponent<Image>();

            outText = GameObject.Find("MyOutText").GetComponent<Text>();
            outBg = GameObject.Find("OutPuts").GetComponent<Image>();
        }
    }
}
