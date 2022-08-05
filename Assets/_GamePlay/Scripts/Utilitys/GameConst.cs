using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GameConst
{

    public enum Type
    {
        Character = 0,
        Sensor = 1,
        Model = 2
    }

    public static readonly string ANIM_IS_IDLE = "IsIdle";
    public static readonly string ANIM_IS_DEAD = "IsDead";
    public static readonly string ANIM_IS_ATTACK = "IsAttack";
    public static readonly string ANIM_IS_WIN = "IsWin";
    public static readonly string ANIM_IS_DANCE = "IsDance";
    public static readonly string ANIM_IS_ULTI = "IsUlti";

    public static readonly float ANIM_IS_DIE_TIME = 1.02f;
}
