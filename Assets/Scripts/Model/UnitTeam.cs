using System;

namespace Model
{
    [Flags]
    public enum UnitTeam
    {
        Neutral = 1 << 0,
        Player = 1 << 1,
        Enemy = 1 << 2
    }
}