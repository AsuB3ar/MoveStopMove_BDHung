using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.Core.Data
{
    public class GameData
    {
        #region Player Data
        #region Stats
        public float Speed = 3;
        public int Weapon;
        #endregion

        #region Skin
        public int Color;
        public int Pant;
        public int Hair;
        public int Set = 0;
        #endregion
        #endregion

        public GameData()
        {
            InitPlayerStatsData();
            InitPlayerSkinData();
        }

        private void InitPlayerStatsData(int size = 1, int speed = 3, int score = 0, int hp = 1, int weapon = (int)PoolID.Weapon_Arrow)
        {
            this.Speed = speed;
            this.Weapon = weapon;
        }

        private void InitPlayerSkinData(int color = 0, int pant = (int)PantSkin.Pokemon, int hair = (int)PoolID.Hair_Cowboy, int set = 0)
        {
            this.Color = color;
            this.Pant = pant;
            this.Hair = hair;
            this.Set = set;
        }
    }
}