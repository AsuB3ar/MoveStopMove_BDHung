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
    public class GameplayManager : Singleton<GameplayManager>
    {
        [SerializeField]
        List<Material> materials;

        public Material GetMaterial(Color color)
        {
            return materials[(int)color];
        }

        public Color GetRandomColor()
        {
            int index = Random.Range(0, materials.Count);
            return (Color)index;
        }
    }
}