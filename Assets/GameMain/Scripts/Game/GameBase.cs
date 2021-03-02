//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public abstract class GameBase
    {
        public abstract GameMode GameMode
        {
            get;
        }

        protected ScrollableBackground SceneBackground
        {
            get;
            private set;
        }

        public bool GameOver
        {
            get;
            protected set;
        }


        public virtual void Initialize()
        {
            GameEntry.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            GameEntry.Event.Subscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);
            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUISucc);


            //SceneBackground = Object.FindObjectOfType<ScrollableBackground>();
            //if (SceneBackground == null)
            //{
            //    Log.Warning("Can not find scene background.");
            //    return;
            //}

            //SceneBackground.VisibleBoundary.gameObject.GetOrAddComponent<HideByBoundary>();
            //GameEntry.Entity.ShowMyAircraft(new MyAircraftData(GameEntry.Entity.GenerateSerialId(), 10000)
            //{
            //    Name = "My Aircraft",
            //    Position = Vector3.zero,
            //});
            GameEntry.UI.OpenUIForm("Assets/GameMain/UI/UIForms/MainForm.prefab", "Default");
            GameEntry.Entity.ShowEntity(11001, typeof(OrderExcuer), "Assets/GameMain/Entities/Robot.prefab", "Aircraft");


            GameEntry.Entity.ShowEntity(11002, typeof(Box), "Assets/GameMain/Entities/Box.prefab", "Aircraft");
            GameEntry.Entity.ShowEntity(11003, typeof(Box), "Assets/GameMain/Entities/Box.prefab", "Aircraft");
            GameEntry.Entity.ShowEntity(11004, typeof(Box), "Assets/GameMain/Entities/Box.prefab", "Aircraft");
            GameEntry.Entity.ShowEntity(11005, typeof(Box), "Assets/GameMain/Entities/Box.prefab", "Aircraft");
            GameEntry.Entity.ShowEntity(11006, typeof(Box), "Assets/GameMain/Entities/Box.prefab", "Aircraft");
            GameEntry.Entity.ShowEntity(11007, typeof(Box), "Assets/GameMain/Entities/Box.prefab", "Aircraft");


            GameOver = false;
        }

        private void OnOpenUISucc(object sender, GameEventArgs e) {
            
        }

        public virtual void Shutdown()
        {
            GameEntry.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
            GameEntry.Event.Unsubscribe(ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);
        }

        public virtual void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (false) //fixme need a gameover triggle
            {
                GameOver = true;
                return;
            }
        }

        protected virtual void OnShowEntitySuccess(object sender, GameEventArgs e)
        {
            ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs)e;
            
        }

        protected virtual void OnOpenUISucc(Object sender, GameEventArgs e) {
            var ne = e as OpenUIFormSuccessEventArgs;
            Log.Warning(ne.UIForm);
        }

        protected virtual void OnShowEntityFailure(object sender, GameEventArgs e)
        {
            ShowEntityFailureEventArgs ne = (ShowEntityFailureEventArgs)e;
            Log.Warning("Show entity failure with error message '{0}'.", ne.ErrorMessage);
        }
    }
}
