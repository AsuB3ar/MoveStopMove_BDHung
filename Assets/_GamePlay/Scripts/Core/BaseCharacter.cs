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
        protected SkinnedMeshRenderer meshCharacter;
        [SerializeField]
        protected SkinnedMeshRenderer meshPant;
        [SerializeField]
        protected GameObject hair;
        [SerializeField]
        protected Transform SensorTF;
        [SerializeField]
        protected Transform ContainWeaponTF;
        [SerializeField]
        protected Transform ContainHairTF;
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
        public float AttackRange => Data.AttackRange;
        public int Level => Data.Level;
        public Color Color => (Color)Data.Color;

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
            if(Weapon != null)
            {
                Weapon.Character = this;
                Data.Weapon = (int)Weapon.Name;
            }
            
        }

        public void OnInit()
        {
            PhysicModule.SetActive(true);
            transform.localScale = Vector3.one * Data.Size;
            PhysicModule.SetRotation(GameConst.Type.Model, Quaternion.Euler(0, 0, 0));
            PhysicModule.SetRotation(GameConst.Type.Sensor, Quaternion.Euler(0, 0, 0));
            //TEST: Test Player Have Hp = 10
            if(type == CharacterType.Player)
            {
                Data.Hp = 10;
            }
            else
            {
                Data.Hp = 1;
            }
            
            Data.AttackCount = 1;

            ((CharacterLogicModule)LogicModule).StartStateMachine();

            if (type == CharacterType.Enemy)
            {
                ((CharacterAI)NavigationModule).StartStateMachine();
            }
        }
        public void Reset()
        {
            SetLevel(1);
            SetPosition(new Vector3(0, GameConst.INIT_CHARACTER_HEIGHT, 0));            
            OnInit();
        }

        public void SetPosition(Vector3 position)
        {
            PhysicModule.SetActive(false);
            transform.localPosition = position;
            PhysicModule.SetActive(true);
        }

        public void SetLevel(int level)
        {
            Data.Level = level;
            transform.localScale = Vector3.one * Data.Size;
        }

        public void OnDespawn()
        {

            if (type == CharacterType.Player) return;

            ((CharacterLogicModule)LogicModule).StopStateMachine();
            PrefabManager.Inst.PushToPool(this.gameObject, PoolID.Character);
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

            #endregion           
            
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
            meshCharacter.material = mat;
            Data.Color = (int)color;
        }

        public void ChangePant(PantSkin name)
        {
            Material mat = GameplayManager.Inst.GetMaterial(name);
            meshPant.material = mat;
            Data.Pant = (int)name;
        }
        public void ChangeHair(PoolID hair)
        {
            if(hair != PoolID.None)
            {
                GameObject hairObject = PrefabManager.Inst.PopFromPool(hair);
                hairObject.transform.parent = ContainHairTF;
                Cache.GetItem(hairObject).SetTranformData();

                if(this.hair != null)
                {
                    Cache.GetItem(this.hair).OnDespawn();
                }
                this.hair = hairObject;
            }
            
        }
        public void ChangeWeapon(BaseWeapon weapon)
        {
            if(weapon != null)
            {
                if(Weapon != null)
                {
                    Weapon.OnDespawn();
                }
                
                Weapon = weapon;
                Weapon.Character = this;
                Weapon.gameObject.transform.parent = ContainWeaponTF;
                Weapon.SetTranformData();
                Data.Weapon = (int)Weapon.Name;
            }          
        }
        public void TakeDamage(int damage)
        {
            Data.Hp -= damage;
            if(Data.Hp <= 0)
            {
                timerDie.Start(GameConst.ANIM_IS_DEAD_TIME, 0);
                if (type == CharacterType.Enemy)
                {
                    ((CharacterAI)NavigationModule).StopStateMachine();
                }
            }
        }

        //TODO: Combat Function(Covert to a system)
        public void AddStatus()
        {
            Data.Level += 1;

            //TODO: Increase Size of character
            //TODO: Increase Size of Attack Range Indicator
            PhysicModule.SetScale(GameConst.Type.Character, 1.1f);                        
        }

        private void Die()
        {
            OnDie?.Invoke(this);
            timerDie.Start(2f, 1);
        }

        private void TimerEvent(int code)
        {
            if(code == 0)
            {
                Die();
            }
            else if(code == 1)
            {
                OnDespawn();             
            }
        }
        
    }
}
