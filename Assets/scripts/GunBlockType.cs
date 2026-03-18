using UnityEngine;

[CreateAssetMenu(menuName = "Blocks/Gun")]
public class GunBlockType : BlockType {
    public float fireRate;
    public GameObject bulletPrefab;
}