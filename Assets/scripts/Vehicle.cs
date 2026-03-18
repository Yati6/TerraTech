using System.Collections.Generic;
using UnityEngine;

public class Vehicle {
    // All blocks in this vehicle
    public List<Block> blocks = new List<Block>();

    // Grid system (VERY important)
    public Dictionary<Vector3Int, Block> grid = new Dictionary<Vector3Int, Block>();


    public bool Placeable(Block blockToPlace, ConnectionFace myFace, Block targetBlock, ConnectionFace targetFace) {
        if (!FaceUtils.AreOpposite(myFace, targetFace))
            return false;

        Vector3Int placePos = calculatePlacePosition(blockToPlace, myFace, targetBlock, targetFace);

        return Placeable(blockToPlace, placePos);
    }
    public bool Placeable(Block blockToPlace, Vector3Int placePos) {

        var cells = blockToPlace.GetCells(placePos);

        foreach (var c in cells) {
            if (grid.ContainsKey(c))
                return false;
        }

        return true;
    }


    public void AddBlock(Block block, Vector3Int placePos) {
        block.gridPosition = placePos;

        blocks.Add(block);

        // Now GetCells() will use the correct gridPosition internally
        foreach (var cell in block.GetCells()) {
            grid[cell] = block;
        }
    }

    public static Vector3Int calculatePlacePosition(Block blockToPlace, ConnectionFace myFace, Block targetBlock, ConnectionFace targetFace) {
        // 1. Get the rotated version of the face on the block we are holding
        ConnectionFace rotatedMyFace = blockToPlace.GetRotatedFace(myFace, blockToPlace.rotation);

        // 2. Get the rotated version of the face on the block already on the vehicle
        ConnectionFace rotatedTargetFace = targetBlock.GetRotatedFace(targetFace, targetBlock.rotation);

        // 3. Find the world cell where the target face sits
        Vector3Int targetFaceWorldCell = targetBlock.gridPosition + rotatedTargetFace.localCell;

        // 4. Step OUT from that cell in the direction the target face is pointing
        Vector3Int newFaceWorldCell = targetFaceWorldCell + rotatedTargetFace.direction;

        // 5. Subtract the rotated local position of our new block's face
        Vector3Int basePos = newFaceWorldCell - rotatedMyFace.localCell;

        return basePos;
    }

    public void place(Block blockToPlace, ConnectionFace myFace, Block targetBlock, ConnectionFace targetFace) {
        if (!FaceUtils.AreOpposite(myFace, targetFace))
            return;

        Vector3Int placePos = calculatePlacePosition(blockToPlace, myFace, targetBlock, targetFace);

        if (Placeable(blockToPlace, placePos)) {
            AddBlock(blockToPlace, placePos);
        }
    }
}