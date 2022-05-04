using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class MonsterInstance : MonoBehaviour {
    [Header("Basic Information")]
    public int personalID = -1;
    public Monster species;
    public int level;
    public string nickname;
    public int exp;
    public int neededExp;

    [Header("Stats")]
    public int hp;
    public int maxHp;
    public int attack;
    public int defence;
    public int attackSpecial;
    public int defenceSpecial;
    public int speed;

    [Header("Move Data")]
    public List<MonsterMove> learnedMoves;

    [Header("Personal Bonuses")]
    public int hpBonus;
    public int attackBonus;
    public int defenceBonus;
    public int attackSpecialBonus;
    public int defenceSpecialBonus;
    public int speedBonus;

    private void Awake() {
        Initialize();
    }

    private void Update() {
        // Update the monster's stats.
        UpdateStats();

        // Has the monster gaind enough exp to level up?
        if (exp >= neededExp) {
            // Update the exp to match the new level.
            neededExp = Mathf.RoundToInt(Mathf.Pow(4 * level, 3) / 5);
            exp = 0;
            
            // Check for a progression on the current level.
            if (species.progressionLevel >= level) {
                species = species.progression;
            }
        }
    }

    private void Initialize() {
        // Generate a personal ID for the Monster, this number is used to generate all of the monster's stats.
        personalID = Random.Range(0, 9999999);

        // Assign the appropriate sprite.
        gameObject.GetComponent<SpriteRenderer>().sprite = species.sprite;

        // Generate the monster's genetic bonuses using the previously generated ID.
        RollGeneticsBonuses();

        // Set the monster's level.
        level = 5;

        // Calculate the current stats accounting for level, bonuses etc.
        UpdateStats();
        hp = maxHp;

        // Provide the Monster with the last four moves it would have learned.
        learnedMoves = new List<MonsterMove>();
        for (int x = 0; x < level; x++) {
            for (int i = species.moves.Count - 1; i >= 0; i--) {
                if (species.moves[i].level == x) {
                    LearnMove(species.moves[i].move);
                }
            }
        }

        // Generate the experience needed to reach the next level.
        neededExp = Mathf.RoundToInt(Mathf.Pow(4 * level, 3)/5);
    }

    private void RollGeneticsBonuses() {
        Random.seed = personalID;
        hpBonus = Random.Range(0, 31);
        attackBonus = Random.Range(0, 31);
        defenceBonus = Random.Range(0, 31);
        attackSpecialBonus = Random.Range(0, 31);
        defenceSpecialBonus = Random.Range(0, 31);
        speedBonus = Random.Range(0, 31);
    }

    private void UpdateStats() {
        maxHp = CalculateStats(species.hp, hpBonus) + level + 10;
        attack = CalculateStats(species.attack, attackBonus) + 5;
        defence = CalculateStats(species.defence, defenceBonus) + 5;
        attackSpecial = CalculateStats(species.attackSpecial, attackSpecialBonus) + 5;
        defenceSpecial = CalculateStats(species.defenceSpecial, defenceSpecialBonus) + 5;
        speed = CalculateStats(species.speed, speedBonus) + 5;
    }

    private int CalculateStats(int speciesBase, int geneticBonus) {
        return Mathf.RoundToInt(((2 * speciesBase + geneticBonus) * level) / 100);
    }

    private void LearnMove(MonsterMove move) {
        bool learned = false;
        if (learnedMoves.Count != 4) {
            learnedMoves.Add(move);
        } else {
            for (int i = 0; i < learnedMoves.Count; i++) {
                if (learnedMoves[i] == null && !learned) {
                    learnedMoves.RemoveAt(i);
                    learnedMoves.Insert(i, move);
                    learned = true;
                }
            }
        }
    }

    private void Progress() {
        species = species.progression;
    }
}
