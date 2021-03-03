//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.DataTable;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class SurvivalGame : GameBase
    {
        private float m_ElapseSeconds = 0f;
        private OrderExcuer excuer;
        private OutPutBox outPutBox;
        private int mainFormId = 0;
        public override GameMode GameMode
        {
            get
            {
                return GameMode.Survival;
            }
        }
        override
        public void Initialize() {

            GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);

            mainFormId = GameEntry.UI.OpenUIForm("Assets/GameMain/UI/UIForms/MainForm.prefab", "Default");
            GameEntry.Entity.ShowEntity(11001, typeof(OrderExcuer), "Assets/GameMain/Entities/Robot.prefab", "Robot");
            GameEntry.Entity.ShowEntity(21001, typeof(OutPutBox), "Assets/GameMain/Entities/OutPutBox.prefab", "OutPut");


            CreateBoxs();
        }

        
        protected override void OnShowEntitySuccess(object sender, GameEventArgs e) {
            ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
            if (ne.Entity.Id == 21001) {
                outPutBox = (OutPutBox)ne.Entity.Logic;
                outPutBox.formId = mainFormId;
                var outputpos = GameObject.Find("PointOutput").transform.position;
                outPutBox.gameObject.transform.position = outputpos + 2*Vector3.down;
            } else {
                var gameobj = ne.Entity.gameObject;
                var temp = gameobj.GetComponent<OrderExcuer>();
                if (temp != null) {
                    excuer = temp;
                    excuer.mainFormId = mainFormId;
                }
            }
        }
        public override void Reset() {
            var inputPos = GameObject.Find("PointInput").transform.position;
            var boxs = GameEntry.Entity.GetEntityGroup("Box");
            var allbox = boxs.GetAllEntities();
            foreach (var box in allbox) {
                var entity = GameEntry.Entity.GetEntity(box.Id);
                entity.gameObject.transform.position = inputPos;
                entity.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
            Debug.Log("ResetBoxs");
            outPutBox.outPuts.Clear();
        }

        public override void StartExcute() {
            excuer.GetComponent<OrderExcuer>().StartExcute();
        }

        private void CreateBoxs() {
            GameEntry.Entity.ShowEntity(11002, typeof(Box), "Assets/GameMain/Entities/Box.prefab", "Box");
            GameEntry.Entity.ShowEntity(11003, typeof(Box), "Assets/GameMain/Entities/Box.prefab", "Box");
            GameEntry.Entity.ShowEntity(11004, typeof(Box), "Assets/GameMain/Entities/Box.prefab", "Box");
            GameEntry.Entity.ShowEntity(11005, typeof(Box), "Assets/GameMain/Entities/Box.prefab", "Box");
            GameEntry.Entity.ShowEntity(11006, typeof(Box), "Assets/GameMain/Entities/Box.prefab", "Box");
            GameEntry.Entity.ShowEntity(11007, typeof(Box), "Assets/GameMain/Entities/Box.prefab", "Box");
        }

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            base.Update(elapseSeconds, realElapseSeconds);
        }
    }
}
