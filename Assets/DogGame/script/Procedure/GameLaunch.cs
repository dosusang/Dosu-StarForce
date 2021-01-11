using GameFramework.Event;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using UnityEngine;

namespace StarForce
{
    public class GameLaunchProcedure : GameFramework.Procedure.ProcedureBase
    {
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            Log.Info("Enter my Launch");
            GameEntry.Entity.AddEntityGroup("Dog", 10,10,10,10);
            GameEntry.Entity.ShowEntity(1280, typeof(Dog), "Assets/DogGame/src/Entity/HDDog.prefab", "Dog", new DogData(110, 10));
        }
    }

}
