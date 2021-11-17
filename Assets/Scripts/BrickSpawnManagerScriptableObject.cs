using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public struct BrickSpawn
{
    // This will be the row offset relative to the center spawn reference point
    public int RowOffset;
    public int LevelOffset;
    public BrickController BrickPrefab;
}

[CreateAssetMenu(fileName = "Brick Data", menuName = "ScriptableObjects/BrickSpawnManagerScriptableObject", order = 1)]
public class BrickSpawnManagerScriptableObject : ScriptableObject
{
    public Vector2 SpawnReferencePosition;
    public List<BrickSpawn> BrickSpawns;
    public Vector2 BrickGap;
}