using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Monster Type", menuName = "MiniMonsters/ New Monster Type")]
public class MonsterType : ScriptableObject {
    public new string name;

    public List<MonsterType> strong;
    public List<MonsterType> weak;
    public List<MonsterType> immune;

    public float GetMultiplier(MonsterType type) {
        float returnMult = 1;

        for (int i = 0; i < strong.Count; i++) {
            if (type == strong[i]) returnMult *= 2;
        }

        for (int i = 0; i < weak.Count; i++) {
            if (type == weak[i]) returnMult *= 0.5f;
        }

        for (int i = 0; i < immune.Count; i++) {
            if (type == immune[i]) returnMult = 0;
        }

        return returnMult;
    }
}
