using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFace {
    public Vector3Int getDirection { get; }
}

public class FaceUtils : MonoBehaviour
{
    public static bool AreOpposite(IFace a, IFace b) {
        return a.getDirection == -b.getDirection;
    }
}
