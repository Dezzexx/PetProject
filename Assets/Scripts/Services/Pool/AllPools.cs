using UnityEngine;

[CreateAssetMenu(fileName = "AllPools", menuName = "Pools/AllPools")]
public class AllPools : ScriptableObject
{
#region ObjectPools
    [Header("ObjectPool")]
    public Pool FriendlyUnitPool;
    public Pool DonutPool;
#endregion

// #region EffectPools

// #endregion
}
