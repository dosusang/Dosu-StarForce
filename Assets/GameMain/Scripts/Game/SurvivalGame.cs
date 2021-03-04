//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.DataTable;
using GameFramework.Event;
using System.Collections.Generic;
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
        private DRStage curStageInfo = null;

        private List<Box> InputBoxs = new List<Box>();
        private string[] inputSplits;
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
            GameEntry.Entity.ShowRobot();
            GameEntry.Entity.ShowOutPutBox();

            int stage = 1;
            IDataTable<DRStage> dtStage = GameEntry.DataTable.GetDataTable<DRStage>();
            curStageInfo = dtStage.GetDataRow(stage);


            //生成对应箱子    设置答案  
            inputSplits = GameEntry.Entity.ShowBox(curStageInfo.Input);

        }


        protected override void OnShowEntitySuccess(object sender, GameEventArgs e) {
            ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
            if (ne.EntityLogicType == typeof(OutPutBox)) {
                outPutBox = (OutPutBox)ne.Entity.Logic;
                outPutBox.formId = mainFormId;
                var outputpos = GameObject.Find("PointOutput").transform.position;
                outPutBox.gameObject.transform.position = outputpos + 2 * Vector3.down;
                //Log.Info("GetoutPutBox ----");

            } else if (ne.EntityLogicType == typeof(OrderExcuer)) {
                var temp = (OrderExcuer)ne.Entity.Logic;
                if (temp != null) {
                    excuer = temp;
                    excuer.mainFormId = mainFormId;
                    //Log.Info("GetOrderExcuer ----");
                }
            } else if (ne.EntityLogicType == typeof(Box)) {
                InputBoxs.Add((Box)ne.Entity.Logic);
                if (InputBoxs.Count == inputSplits.Length) {
                    float posx = ne.Entity.gameObject.transform.position.x;
                    Log.Info("size = " + inputSplits.Length);
                    for(int i = 0; i < InputBoxs.Count; i++){
                        InputBoxs[i].boxPram = inputSplits[i];
                        InputBoxs[i].gameObject.transform.position = new Vector3(posx, i, -1);
                    }
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

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            base.Update(elapseSeconds, realElapseSeconds);
        }
    }
}
