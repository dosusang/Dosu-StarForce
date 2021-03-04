//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.DataTable;
using System;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public static class EntityExtension
    {
        // 关于 EntityId 的约定：
        // 0 为无效
        // 正值用于和服务器通信的实体（如玩家角色、NPC、怪等，服务器只产生正值）
        // 负值用于本地生成的临时实体（如特效、FakeObject等）
        private static int s_SerialId = 0;

        public static Entity GetGameEntity(this EntityComponent entityComponent, int entityId)
        {
            UnityGameFramework.Runtime.Entity entity = entityComponent.GetEntity(entityId);
            if (entity == null)
            {
                return null;
            }

            return (Entity)entity.Logic;
        }

        public static void HideEntity(this EntityComponent entityComponent, Entity entity)
        {
            entityComponent.HideEntity(entity.Entity);
        }

        public static void AttachEntity(this EntityComponent entityComponent, Entity entity, int ownerId, string parentTransformPath = null, object userData = null)
        {
            entityComponent.AttachEntity(entity.Entity, ownerId, parentTransformPath, userData);
        }
 
        public static void ShowEffect(this EntityComponent entityComponent, EffectData data)
        {
            entityComponent.ShowEntity(typeof(Effect), "Effect", Constant.AssetPriority.EffectAsset);
        }

        public static void ShowRobot(this EntityComponent entityComponent) {
            ShowEntity(entityComponent, typeof(OrderExcuer), "Robot", 100, 10001);
        }

        public static string[] ShowBox(this EntityComponent entityComponent, string inputs) {
            var arry = inputs.Split(',');
            for (int i = 0; i < arry.Length; i++) { 
                ShowEntity(entityComponent, typeof(Box), "Box", 100, 20001);
            }
            return arry;
        }

        public static void ShowOutPutBox(this EntityComponent entityComponent) {
            ShowEntity(entityComponent, typeof(OutPutBox), "OutPut", 100, 30001);
        }
        private static void ShowEntity(this EntityComponent entityComponent, Type logicType, string entityGroup, int priority, int entityId = -1)
        {

            IDataTable<DREntity> dtEntity = GameEntry.DataTable.GetDataTable<DREntity>();
            DREntity drEntity = dtEntity.GetDataRow(entityId);
            if (drEntity == null)
            {
                Log.Warning("Can not load entity id '{0}' from data table.", entityId);
                return;
            }

            entityComponent.ShowEntity(GenerateSerialId(entityComponent), logicType, AssetUtility.GetEntityAsset(drEntity.AssetName), entityGroup, priority);
        }

        public static int GenerateSerialId(this EntityComponent entityComponent)
        {
            return --s_SerialId;
        }
    }
}
