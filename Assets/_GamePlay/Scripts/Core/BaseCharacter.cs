using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.Core
{
    using Manager;
    using Utilitys.Timer;
    using MoveStopMove.Core.Data;
    using MoveStopMove.Core.Character.WorldInterfaceSystem;
    using MoveStopMove.Core.Character.NavigationSystem;
    using MoveStopMove.Core.Character.PhysicSystem;
    using MoveStopMove.Core.Character.LogicSystem;
    using ContentCreation.Weapon;
    using System;
    public enum CharacterType
    {
        Player = 0,
        Enemy = 1
    }

    public class BaseCharacter : MonoBehaviour,IInit,IDespawn
    {
        public event Action<BaseCharacter> OnDie;
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
        protected STimer timerDie = new STimer();

        public BaseWeapon Weapon;
        [SerializeField]
        CharacterType type;
        public bool IsDie
        {
            get
            {
                if (Data.Hp > 0) return false;
                else return true;
            }
        }
        public float Size => Data.Size;

        private void Awake()
        {
            WorldInterfaceSystem = new CharacterWorldInterfaceSystem(WorldInterfaceModule);
            NavigationSystem = new CharacterNavigationSystem(NavigationModule);
            LogicSystem = new CharacterLogicSystem(LogicModule);
            PhysicSystem = new CharacterPhysicSystem(PhysicModule);

            Data = ScriptableObject.CreateInstance(typeof(CharacterData)) as CharacterData;
            LogicSystem.SetCharacterInformation(Data, gameObject.transform);
            WorldInterfaceSystem.SetCharacterInformation(Data);
            NavigationSystem.SetCharacterInformation(transform, SensorTF, GetInstanceID());

            //NOTE: When change wepon need to set this line of code
            Weapon.Character = this;

        }

        public void OnInit()
        {
            PhysicModule.SetActive(true);
            Data.Hp = 1;
            transform.localScale = Vector3.one * Data.Size;
        }

        public void OnDespawn()
        {
            OnDie?.Invoke(this);
            ((CharacterLogicModule)LogicModule).StopStateMachine();
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
            LogicSystem.Event.SetActive += PhysicModule.SetActive;

            LogicSystem.Event.SetSmoothRotation += PhysicSystem.SetSmoothRotation;
            LogicSystem.Event.SetBool_Anim += AnimModule.SetBool;
            LogicSystem.Event.SetFloat_Anim += AnimModule.SetFloat;
            LogicSystem.Event.SetInt_Anim += AnimModule.SetInt;
            LogicSystem.Event.DealDamage += DealDamage;
            AnimModule.UpdateEventAnimationState += LogicSystem.ReceiveInformation;

            OnInit();
            #endregion


            ((CharacterLogicModule)LogicModule).StartStateMachine();

            if (type == CharacterType.Enemy)
            {
                ((CharacterAI)NavigationModule).StartStateMachine();
            }
            
            timerDie.TimeOut1 += TimerEvent;
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
            LogicSystem.Event.SetActive -= PhysicModule.SetActive;

            LogicSystem.Event.SetSmoothRotation -= PhysicSystem.SetSmoothRotation;
            LogicSystem.Event.SetBool_Anim -= AnimModule.SetBool;
            LogicSystem.Event.SetFloat_Anim -= AnimModule.SetFloat;
            LogicSystem.Event.SetInt_Anim -= AnimModule.SetInt;
            LogicSystem.Event.DealDamage -= DealDamage;

            AnimModule.UpdateEventAnimationState -= LogicSystem.ReceiveInformation;
            #endregion
            
            timerDie.TimeOut1 -= TimerEvent;
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
            Weapon.DealDamage(direction, range ,Data.Size);
        }

        public void ChangeColor(Color color)
        {
            Material mat = GameplayManager.Inst.GetMaterial(color);
            mesh.material = mat;
        }
        public void TakeDamage(int damage)
        {
            Data.Hp -= damage;
            if(Data.Hp <= 0)
            {
                timerDie.Start(GameConst.ANIM_IS_DEAD_TIME + 2f, 0);
                if (type == CharacterType.Enemy)
                {
                    ((CharacterAI)NavigationModule).StopStateMachine();
                }
            }
        }

        //TODO: Combat Function(Covert to a system)
        public void AddStatus()
        {
            Data.Size *= 1.1f;

            //TODO: Increase Size of character
            //TODO: Increase Size of Attack Range Indicator
            PhysicModule.SetScale(GameConst.Type.Character, 1.1f);                        
        }

        private void Die()
        {
            OnDespawn();
        }

        private void TimerEvent(int code)
        {
            if(code == 0)
            {
                Die();
            }
        }
        
    }
}
