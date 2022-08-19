using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.Manager
{
    using Utilitys;
    using Core;
    using MoveStopMove.ContentCreation.Weapon;

    
    public enum Color
    {
        Red = 0,
        Blue = 1,
        Green = 2,
        Yellow = 3,
        Purple = 4,
        Orange = 5,
        Brown = 6,
        Aqua = 7
    }

    public enum PantSkin
    {
        Batman = 0,
        Chambi = 1,
        Comy = 2,
        Dabao = 3,
        Onion = 4,
        Pokemon = 5,
        Rainbow = 6,
        Skull = 7,
        Vantim = 8
    }
    [DefaultExecutionOrder(-2)]
    public class GameplayManager : Singleton<GameplayManager>
    {
        public GameObject Player;
        public BaseCharacter PlayerScript;
        public Camera PlayerCamera;
        [SerializeField]
        CameraMove cameraMove;
        [SerializeField]
        List<Material> materials;
        [SerializeField]
        List<Material> pantSkins;
        
        public readonly List<PoolID> hairSkins = new List<PoolID>() { PoolID.Hair_Arrow, PoolID.Hair_Cowboy, PoolID.Hair_Headphone,PoolID.Hair_Ear, PoolID.Hair_Crown, PoolID.Hair_Horn, PoolID.Hair_Beard ,PoolID.None };
        public readonly List<PoolID> WeaponNames = new List<PoolID>() { PoolID.Weapon_Axe1, PoolID.Weapon_Knife1, PoolID.Weapon_Axe2, PoolID.Weapon_Arrow };


        public UnityEngine.Color GetColor(Color color)
        {
            return GetMaterial(color).color;
        }
        public Material GetMaterial(Color color)
        {
            return materials[(int)color];
        }

        public Material GetMaterial(PantSkin name)
        {
            return pantSkins[(int)name];
        }
        public PantSkin GetRandomPantSkin()
        {
            int index = Random.Range(0, pantSkins.Count);
            return (PantSkin)index;
        }
        public Color GetRandomColor()
        {
            int index = Random.Range(0, materials.Count);
            return (Color)index;
        }

        public PoolID GetRandomHair()
        {
            int index = Random.Range(0, hairSkins.Count);
            return hairSkins[index];
        }

        public BaseWeapon GetRandomWeapon()
        {
            int index = Random.Range(0, WeaponNames.Count);
            PoolID weaponName = WeaponNames[index];
            GameObject weapon = PrefabManager.Inst.PopFromPool(weaponName);
            return Cache.GetBaseWeapon(weapon);
        }

        public void SetCameraPosition(CameraPosition position)
        {
            cameraMove.MoveTo(position);
        }
    }
}