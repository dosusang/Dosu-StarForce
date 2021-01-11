using UnityEngine;
using UnityGameFramework.Runtime;
namespace StarForce
{
    public abstract class CollidableObj : MyEntity
    {
        private void OnTriggerEnter(Collider other)
        {
            Entity entity = other.gameObject.GetComponent<Entity>();
            if (entity == null)
            {
                return;
            }

            if (entity is TargetableObject && entity.Id >= Id)
            {
                // 碰撞事件由 Id 小的一方处理，避免重复处理
                return;
            }
            Log.Info(this.ToString(), entity.ToString());
            
        }
    }
}

