using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("GameGrid")]
    public Vector2Int GameGridSize;
    public Vector3 GridCellSize;
    public float GameGridSpaceSize;
    public int MaximumBuildingLines;
    public int BasePrice;
    public int MaximumUnitsOnLine;
}
