using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Monster Move", menuName = "MiniMonsters/ New Monster Move")]
public class MonsterMove : ScriptableObject {
    public enum DamageType {
        Physical,
        Special
    }

    public new string name;
    public MonsterType type;
    public int powerPoints;
    public int power;
    public int accuracy;
    public DamageType damageType;
}
