using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.Manager
{
    using Utilitys;

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
    public class GameplayManager : Singleton<GameplayManager>
    {
        [SerializeField]
        List<Material> materials;
        [SerializeField]
        List<Material> pantSkins;
        [SerializeField]
        CameraMove cameraMove;
        public readonly List<PoolName> hairSkins = new List<PoolName>() { PoolName.Hair_Arrow, PoolName.Hair_Cowboy, PoolName.Hair_Headphone, PoolName.None };


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

        public PoolName GetRandomHair()
        {
            int index = Random.Range(0, hairSkins.Count);
            return hairSkins[index];
        }

        public void SetCameraPosition(CameraPosition position)
        {
            cameraMove.MoveTo(position);
        }
    }
}