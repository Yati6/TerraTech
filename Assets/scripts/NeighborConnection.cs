using System.Collections.Generic;
using UnityEngine;

public class NeighborConnection {
    public Block neighbor;  // the connected block
    public ConnectionFace face;      // which face of THIS block the neighbor is attached to

    public NeighborConnection(Block n, ConnectionFace f) {
        neighbor = n;
        face = f;
    }
}