using System;

namespace ENUMS
{
    [Serializable]
    public enum WeaponType
    {
        PISTOL,
        ASSULT_RIFLE,
        SHOTGUN,
        SNIPER_RIFLE,
        SMG,
        LMG
    }
    [Serializable]
    public enum CollectableType
    {
        FIRST_AID,
        GOLD,
        AMMO,
        WEAPON
    }
    [Serializable]
    public enum EnemyBehaviourType
    {
        BASE_RANGE,
        BASE_MELEE
    }
}
