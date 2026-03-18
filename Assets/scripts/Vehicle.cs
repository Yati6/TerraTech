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
        // CRITICAL: Update the block's internal position before calculating its cells
        block.gridPosition = placePos;

        blocks.Add(block);

        // This now uses the updated gridPosition to fill the dictionary
        foreach (var cell in block.GetCells()) {
            grid[cell] = block;
        }
    }

    public static Vector3Int calculatePlacePosition(Block blockToPlace, ConnectionFace myFace, Block targetBlock, ConnectionFace targetFace) {
        // 1. Get the actual face positions/directions based on current rotations
        ConnectionFace rotatedMyFace = blockToPlace.GetRotatedFace(myFace, blockToPlace.rotation);
        ConnectionFace rotatedTargetFace = targetBlock.GetRotatedFace(targetFace, targetBlock.rotation);

        // 2. Find the world cell where the target face is located
        Vector3Int targetFaceWorldCell = targetBlock.gridPosition + rotatedTargetFace.localCell;

        // 3. Move one step out in the direction the target face is pointing
        Vector3Int newFaceWorldCell = targetFaceWorldCell + rotatedTargetFace.direction;

        // 4. Subtract the rotated local position of our new block's face to find the pivot
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