using System.Collections.Generic;
using UnityEngine;

public enum BlockRotation {
    Rot0,
    Rot90,
    Rot180,
    Rot270
}


[System.Serializable]
public class Block : MonoBehaviour {
    public Vector3Int gridPosition;

    public BlockType type;

    public List<NeighborConnection> neighbors = new List<NeighborConnection>();

    public BlockRotation rotation; // rotation around the connected face

    public List<Vector3Int> GetCells() {
        List<Vector3Int> cells = new List<Vector3Int>();

        List<Vector3Int> occupiedCells = type.occupiedCells;

        foreach (Vector3Int cell in occupiedCells) {
            cells.Add(GetRotatedCell(cell, rotation) + gridPosition);
        }

        return cells;
    }


    public List<Vector3Int> GetCells(Vector3Int basePos) {
        List<Vector3Int> cells = new List<Vector3Int>();

        List<Vector3Int> occupiedCells = type.occupiedCells;

        foreach (Vector3Int cell in occupiedCells) {
            cells.Add(GetRotatedCell(cell, rotation) + basePos);
        }

        return cells;
    }

    public Vector3Int GetRotatedCell(Vector3Int localCell, BlockRotation rot) {
        // This assumes 'rotation' is 0, 90, 180, or 270 on the Y axis
        switch (rot) {
            case BlockRotation.Rot90: return new Vector3Int(localCell.z, localCell.y, -localCell.x);
            case BlockRotation.Rot180: return new Vector3Int(-localCell.x, localCell.y, -localCell.z);
            case BlockRotation.Rot270: return new Vector3Int(-localCell.z, localCell.y, localCell.x);
            default: return localCell; // Rot0
        }
    }

    public ConnectionFace GetRotatedFace(ConnectionFace face, BlockRotation rot) {
        return new ConnectionFace {
            // Rotate the cell position AND the direction vector
            localCell = GetRotatedCell(face.localCell, rot),
            direction = GetRotatedCell(face.direction, rot)
        };
    }
}