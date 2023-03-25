using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.Core
{
    using MoveStopMove.Manager;
    using MoveStopMove.Core.Character.LogicSystem;
    using MoveStopMove.Core.Character.NavigationSystem;
    using MoveStopMove.Core.Data;
    using Utilitys.Timer;

    public class NormalEnemy : BaseCharacter
    {
        protected override void Awake()
        {           
            base.Awake();
            LogicSystem.SetCharacterInformation(Data, gameObject.transform);
            WorldInterfaceSystem.SetCharacterInformation(Data);
        }
        public override void OnInit()
        {
            base.OnInit();
            Data.Hp = 1;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            //Debug.Log($"<color=green> On Spawn </color>");
            VFXInit();
        }        
        protected override void OnDisable()
        {
            base.OnDisable();
            dieTimer.Stop();
            //Debug.Log($"<color=yellow> On Disable </color>");
        }
        
        public override void OnDespawn()
        {
            base.OnDespawn();
            //Debug.Log($"<color=red> On Despawn </color>");
            VFXRecalling();
            ((CharacterLogicModule)LogicModule).StopStateMachine();
            PrefabManager.Inst.PushToPool(this.gameObject, PoolID.Character);

            VFX_AddStatus = null;
            VFX_Hit = null;
        }
        private void VFXInit()
        {
            VFX_Hit = Cache.GetVisualEffectController(VisualEffectManager.Inst.PopFromPool(VisualEffect.VFX_Hit));
            VFX_AddStatus = Cache.GetVisualEffectController(VisualEffectManager.Inst.PopFromPool(VisualEffect.VFX_AddStatus));
            VFX_Hit.Init(transform, Vector3.up * 0.5f, Quaternion.Euler(Vector3.zero), Vector3.one * 0.3f);
            VFX_AddStatus.Init(transform, Vector3.up * -0.5f, Quaternion.Euler(-90, 0, 0), Vector3.one);
        }
        private void VFXRecalling()
        {
            if (VFX_Hit)
                VisualEffectManager.Inst.PushToPool(VFX_Hit.gameObject, VisualEffect.VFX_Hit);
            if (VFX_AddStatus)
                VisualEffectManager.Inst.PushToPool(VFX_AddStatus.gameObject, VisualEffect.VFX_AddStatus);
        }

        public override void Run()
        {          
            ((CharacterAI)NavigationModule).StartStateMachine();           
        }

        public override void Stop()
        {
            ((CharacterAI)NavigationModule).StopStateMachine();
        }
    }
}