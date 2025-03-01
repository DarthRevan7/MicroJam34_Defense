using UnityEngine;

[CreateAssetMenu(fileName = "TowerChoice", menuName = "Scriptable Objects/TowerChoice")]
public class TowerChoice : ScriptableObject
{
    //Tower Prefab
    public GameObject towerPrefab;
    //Tower cost
    public int cost;
    //Tower damage per shot
    public int damage;
    //Tower attack speed
    public float attackSpeed = 1.0f;
}
