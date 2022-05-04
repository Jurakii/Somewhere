using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Monster", menuName = "MiniMonsters/ New Monster")]
public class Monster : ScriptableObject {
    [Header("Basic Information")]
    public new string name;
    public Sprite sprite;
    public MonsterType primaryType;
    public MonsterType secondaryType;

    [Header("Base Stats")]
    public int hp;
    public int attack;
    public int defence;
    public int attackSpecial;
    public int defenceSpecial;
    public int speed;

    [Header("Progression")]
    public Monster progression;
    public int progressionLevel;

    [Header("Learnset")]
    public List<LearnableMove> moves;
}

[System.Serializable]
public class LearnableMove {
    public int level;
    public MonsterMove move;
}