using GameFramework.Event;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using UnityEngine;

public class GameLaunchProcedure : GameFramework.Procedure.ProcedureBase
{
    protected override void  OnEnter(ProcedureOwner procedureOwner)
    {
        Log.Info("Enter my Launch");
        StarForce.GameEntry.Entity.AddEntityGroup("Dog", 10, 10, 10, 10);
        //StarForce.GameEntry.Entity.ShowEntity(110, typeof(EntityBase), "Assets/DogGame/src/Entity/dog.prefab", "Dog");
        //.ShowEntity(typeof(MyAircraft), "Aircraft", Constant.AssetPriority.MyAircraftAsset, data);
    }
}
