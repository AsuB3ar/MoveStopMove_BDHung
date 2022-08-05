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
    using ContentCreation.Weapon;
    using System;

    public class BaseCharacter : MonoBehaviour
    {
        protected bool isDie = false;
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

        public BaseWeapon Weapon;
        public bool IsDie => isDie;
        

        private void Awake()
        {
            WorldInterfaceSystem = new CharacterWorldInterfaceSystem(WorldInterfaceModule);
            NavigationSystem = new CharacterNavigationSystem(NavigationModule);
            LogicSystem = new CharacterLogicSystem(LogicModule);
            PhysicSystem = new CharacterPhysicSystem(PhysicModule);

            Data = ScriptableObject.CreateInstance(typeof(CharacterData)) as CharacterData;
            LogicSystem.SetCharacterInformation(Data, gameObject.transform);
            WorldInterfaceSystem.SetCharacterInformation(Data);
            Weapon.Character = this;
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
            LogicSystem.Event.SetBool_Anim += AnimModule.SetBool;
            LogicSystem.Event.SetFloat_Anim += AnimModule.SetFloat;
            LogicSystem.Event.SetInt_Anim += AnimModule.SetInt;
            LogicSystem.Event.DealDamage += DealDamage;

            AnimModule.UpdateEventAnimationState += LogicSystem.ReceiveInformation;
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
            LogicSystem.Event.SetBool_Anim -= AnimModule.SetBool;
            LogicSystem.Event.SetFloat_Anim -= AnimModule.SetFloat;
            LogicSystem.Event.SetInt_Anim -= AnimModule.SetInt;
            LogicSystem.Event.DealDamage -= DealDamage;

            AnimModule.UpdateEventAnimationState -= LogicSystem.ReceiveInformation;
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

        protected virtual void DealDamage(Vector3 direction, float range)
        {
            Weapon.DealDamage(direction,range);
        }

        public void TakeDamage(int damage)
        {
            Data.Hp -= damage;
            if(Data.Hp <= 0)
            {
                isDie = true;
            }
        }

        public void IncreaseSize()
        {
            Data.Size *= 1.1f;
        }
        //TODO: Combat Function(Covert to a system
    }
}
