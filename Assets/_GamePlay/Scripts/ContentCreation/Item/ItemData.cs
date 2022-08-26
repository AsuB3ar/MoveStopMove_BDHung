using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.ContentCreation
{
    [CreateAssetMenu(fileName = "newItemData", menuName = "Data/Item")]
    public class ItemData : ScriptableObject
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;

        public Sprite icon;
        public PoolID poolID;
        public PantSkin pant;
        public UIItemType type;
    }
}