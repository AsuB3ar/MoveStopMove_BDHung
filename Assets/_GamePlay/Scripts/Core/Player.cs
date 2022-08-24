using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.Core
{
    using MoveStopMove.Core.Data;
    using MoveStopMove.Manager;
    public class Player : BaseCharacter,IPersistentData
    {
        public const string P_SPEED = "PlayerSpeed";
        public const string P_WEAPON = "PlayerWeapon";

        public const string P_COLOR = "PlayerColor";
        public const string P_HAIR = "PlayerHair";
        public const string P_PANT = "PlayerPant";       
        public const string P_SET = "PlayerSet";

        protected override void Awake()
        {
            base.Awake();
        }

        protected void Start()
        {
            
        }

        public override void OnInit()
        {
            base.OnInit();
            Data.Hp = 10;
        }

        public override void OnDespawn()
        {
            
        }

        public void LoadGame(GameData gameData)
        {
            if(Data == null)
            {
                Data = ScriptableObject.CreateInstance(typeof(CharacterData)) as CharacterData;
                LogicSystem.SetCharacterInformation(Data, gameObject.transform);
                WorldInterfaceSystem.SetCharacterInformation(Data);
            }

            Data.Speed = gameData.Speed;
            Data.Weapon = gameData.Weapon;

            Data.Color = gameData.Color;
            Data.Pant = gameData.Pant;
            Data.Hair = gameData.Hair;
            Data.Set = gameData.Set;

            if(VFX_Hit == null)
            {
                VFX_Hit = Cache.GetVisualEffectController(VisualEffectManager.Inst.PopFromPool(VisualEffect.VFX_Hit));
            }
            
            if(VFX_AddStatus == null)
            {
                VFX_AddStatus = Cache.GetVisualEffectController(VisualEffectManager.Inst.PopFromPool(VisualEffect.VFX_AddStatus));
            }

            VFX_Hit.SetColor(GameplayManager.Inst.GetColor((GameColor)Data.Color));
            VFX_Hit.Init(transform, Vector3.up * 0.5f, Quaternion.Euler(Vector3.zero), Vector3.one * 0.3f);
            VFX_AddStatus.Init(transform, Vector3.up * -0.5f, Quaternion.Euler(-90, 0, 0), Vector3.one);

            ChangeColor((GameColor)Data.Color);
            ChangePant((PantSkin)Data.Pant);
            ChangeHair((PoolID)Data.Hair);

            GameObject weapon = PrefabManager.Inst.PopFromPool((PoolID)Data.Weapon);
            ChangeWeapon(Cache.GetBaseWeapon(weapon));
            //Change Set
    }

        public void SaveGame(ref GameData gameData)
        {
            gameData.Speed = Data.Speed;
            gameData.Weapon = Data.Weapon;

            gameData.Color = Data.Color;
            gameData.Pant = Data.Pant;
            gameData.Hair = Data.Hair;
            gameData.Set = Data.Set;

            
            
        }
    }
}