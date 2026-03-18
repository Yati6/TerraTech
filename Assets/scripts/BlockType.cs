using UnityEngine;
using System.Collections.Generic;

public enum BlockCategory {
    Core,
    Armor,
    Weapon,
    Movement,
    Utility
}

[System.Serializable]
public struct ConnectionFace : IFace {
    public Vector3Int localCell;
    public Vector3Int direction;

    // Explicitly implement the interface property
    public Vector3Int getDirection => direction;
}

[CreateAssetMenu(fileName = "NewBlockType", menuName = "TerraTech/Block Type")]
public class BlockType : ScriptableObject {
    [Header("General Info")]
    public string displayName;
    public BlockCategory category;
    public GameObject prefab;

    [Header("Physics & Stats")]
    public float mass = 1f;
    public float maxHealth = 100f;

    [Header("Shape & Connectivity")]
    // Instead of 'size', this defines every cell the block occupies.
    // For an L-shape, you'd add (0,0,0), (1,0,0), and (0,1,0).
    public List<Vector3Int> occupiedCells = new List<Vector3Int> { new Vector3Int(0,0,0) };

    // This defines where other blocks can "stick". 
    // If you only want one side to connect, only put one Face in this list.
    public List<ConnectionFace> connectionFaces = new List<ConnectionFace>();
}