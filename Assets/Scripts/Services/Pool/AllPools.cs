using UnityEngine;

[CreateAssetMenu(fileName = "AllPools", menuName = "Pools/AllPools")]
public class AllPools : ScriptableObject
{
#region ObjectPools
    [Header("ObjectPool")]
    public Pool FriendlyUnitPool;
    public Pool DonutPool;
    public Pool BlueSpotPool;
    public Pool RedSpotPool;
#endregion

#region EffectPools
    public Pool UnitFightEffectPool;
    public Pool DiamondPoofEffectPool;
#endregion
}
