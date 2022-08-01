using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.Core
{
    using MoveStopMove.Core.Data;
    using MoveStopMove.Core.Character.WorldInterfaceSystem;
    using MoveStopMove.Core.Character.NavigationSystem;
    using MoveStopMove.Core.Character.PhysicSystem;
    using MoveStopMove.Core.Character.LogicSystem;
    using System;

    public class BaseCharacter : MonoBehaviour
    {

        [SerializeField]
        protected SkinnedMeshRenderer mesh;
        [SerializeField]
        protected Transform SensorTF;
        protected CharacterData Data;

        [SerializeField]
        WorldInterfaceModule WorldInterfaceModule;
        [SerializeField]
        AbstractNavigationModule NavigationModule;
        [SerializeField]
        AbstractLogicModule LogicModule;
        [SerializeField]
        AbstractPhysicModule PhysicModule;
        [SerializeField]
        AnimationModule AnimModule;

        protected CharacterWorldInterfaceSystem WorldInterfaceSystem;
        protected CharacterNavigationSystem NavigationSystem;
        protected CharacterLogicSystem LogicSystem;
        protected CharacterPhysicSystem PhysicSystem;


        private void Awake()
        {
            WorldInterfaceSystem = new CharacterWorldInterfaceSystem(WorldInterfaceModule);
            NavigationSystem = new CharacterNavigationSystem(NavigationModule);
            LogicSystem = new CharacterLogicSystem(LogicModule);
            PhysicSystem = new CharacterPhysicSystem(PhysicModule);

            Data = ScriptableObject.CreateInstance(typeof(CharacterData)) as CharacterData;
            LogicSystem.SetCharacterInformation(Data);
        }
        protected virtual void OnEnable()
        {
            #region Update Data Event
            WorldInterfaceSystem.OnUpdateData += NavigationSystem.ReceiveInformation;
            WorldInterfaceSystem.OnUpdateData += LogicSystem.ReceiveInformation;

            NavigationSystem.OnUpdateData += LogicSystem.ReceiveInformation;
            PhysicSystem.OnUpdateData += LogicSystem.ReceiveInformation;

            LogicSystem.Event.SetVelocity += PhysicSystem.SetVelocity;
            LogicSystem.Event.SetRotation += PhysicSystem.SetRotation;
            LogicSystem.Event.SetSmoothRotation += PhysicSystem.SetSmoothRotation;
            LogicSystem.Event.SetBool_Anim += SetBool_Anim;
            LogicSystem.Event.SetFloat_Anim += SetFloat_Anim;
            LogicSystem.Event.SetInt_Anim += SetInt_Anim;

            ((CharacterLogicModule)LogicModule).StartStateMachine();
            #endregion
        }

        protected virtual void OnDisable()
        {
            #region Update Data Event
            WorldInterfaceSystem.OnUpdateData -= NavigationSystem.ReceiveInformation;
            WorldInterfaceSystem.OnUpdateData -= LogicSystem.ReceiveInformation;

            NavigationSystem.OnUpdateData -= LogicSystem.ReceiveInformation;
            PhysicSystem.OnUpdateData -= LogicSystem.ReceiveInformation;

            LogicSystem.Event.SetVelocity -= PhysicSystem.SetVelocity;
            LogicSystem.Event.SetRotation -= PhysicSystem.SetRotation;
            LogicSystem.Event.SetSmoothRotation -= PhysicSystem.SetSmoothRotation;
            LogicSystem.Event.SetBool_Anim -= SetBool_Anim;
            LogicSystem.Event.SetFloat_Anim -= SetFloat_Anim;
            LogicSystem.Event.SetInt_Anim -= SetInt_Anim;
            #endregion
        }

        protected virtual void Update()
        {
            WorldInterfaceSystem.Run();
            NavigationSystem.Run();
            LogicSystem.Run();
            PhysicSystem.Run();
        }

        protected virtual void FixedUpdate()
        {
            LogicSystem.FixedUpdateData();
        }

        protected virtual void SetInt_Anim(string animName, int value)
        {
            
        }

        protected virtual void SetFloat_Anim(string animName, float value)
        {
            
        }

        protected virtual void SetBool_Anim(string animName, bool value)
        {
            
        }



        //TODO: Combat Function(Covert to a system
    }
}
