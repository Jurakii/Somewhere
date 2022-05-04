using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBattleController : MonoBehaviour {
    public MonsterInstance friendly;
    public MonsterInstance hostile;

    public Button[] moveButtons;
    public Text[] healthDisplay;

    private bool attacking;
    public AudioSource hurtAudio;
    private bool playerTurn = true;
    private bool playerAttackNext = false;
    private int selectedMove = 0;

    private bool battleEnded = false;

    void Update() {
        if (!battleEnded) {
            // Disable all move buttons.
            for (int i = 0; i < moveButtons.Length; i++) {
                moveButtons[i].image.enabled = false;
                moveButtons[i].GetComponentInChildren<Text>().text = "";
                moveButtons[i].interactable = false;
            }

            // Enable any active move buttons on the player's turn.
            if (!attacking && playerTurn && !playerAttackNext) {
                for (int i = 0; i < friendly.learnedMoves.Count; i++) {
                    if (friendly.learnedMoves[i] != null) {
                        moveButtons[i].image.enabled = true;
                        moveButtons[i].GetComponentInChildren<Text>().text = friendly.learnedMoves[i].name;
                        moveButtons[i].interactable = true;
                    }
                }
            }

            // Basic health display.
            healthDisplay[0].text = friendly.hp.ToString() + " / " + friendly.maxHp.ToString();
            healthDisplay[1].text = hostile.hp.ToString() + " / " + hostile.maxHp.ToString();

            if (!playerTurn && !attacking) {
                StartCoroutine(Attack(friendly, hostile, hostile.learnedMoves[0]));
                Debug.Log("The enemy hits you with a deadly " + hostile.learnedMoves[0].name);
                playerTurn = true;
            } else if (playerAttackNext && !attacking) {
                StartCoroutine(Attack(hostile, friendly, friendly.learnedMoves[selectedMove]));
                playerAttackNext = false;
            }
        } else {
            Debug.Log("Battle ended!");
            // Disable all move buttons.
            for (int i = 0; i < moveButtons.Length; i++) {
                moveButtons[i].image.enabled = false;
                moveButtons[i].GetComponentInChildren<Text>().text = "";
                moveButtons[i].interactable = false;
            }
            
            // Basic health display.
            healthDisplay[0].text = friendly.hp.ToString() + " / " + friendly.maxHp.ToString();
            healthDisplay[1].text = hostile.hp.ToString() + " / " + hostile.maxHp.ToString();
        }
    }

    public void UseMove(int move) {
        if (hostile.speed < friendly.speed) {
            Debug.Log("You were faster than your opponent! You attack first!");
            StartCoroutine(Attack(hostile, friendly, friendly.learnedMoves[move]));
            if (hostile.hp <= 0) {
                battleEnded = true;
            }
            playerTurn = false;
        } else {
            Debug.Log("Your opponent was faster than you! They attack first!");
            Debug.Log("The enemy hits you with a deadly " + hostile.learnedMoves[0].name);
            StartCoroutine(Attack(friendly, hostile, hostile.learnedMoves[0]));
            selectedMove = move; if (friendly.hp <= 0) {
                battleEnded = true;
            }
            playerAttackNext = true;
        }
    }

    IEnumerator Attack(MonsterInstance target, MonsterInstance attacker, MonsterMove move) {
        attacking = true;

        float time = 0.05f;
        int loops = 3;

        Color visible = new Color(target.gameObject.GetComponent<SpriteRenderer>().color.r, target.gameObject.GetComponent<SpriteRenderer>().color.g, target.gameObject.GetComponent<SpriteRenderer>().color.b, 1);
        Color invisible = new Color(target.gameObject.GetComponent<SpriteRenderer>().color.r, target.gameObject.GetComponent<SpriteRenderer>().color.g, target.gameObject.GetComponent<SpriteRenderer>().color.b, 0);
        SpriteRenderer ssr = target.gameObject.GetComponent<SpriteRenderer>();

        float damageMult = move.type.GetMultiplier(target.species.primaryType);
        if (target.species.secondaryType != null) {
            damageMult *= move.type.GetMultiplier(target.species.secondaryType);
        }

        switch (move.damageType) {
            case MonsterMove.DamageType.Physical:
                target.hp -= Mathf.RoundToInt((((((2 * attacker.level) / 5) + 2) * attacker.learnedMoves[0].power * (attacker.attack / target.defence) / 50) + 2) * damageMult);
                break;
            case MonsterMove.DamageType.Special:
                target.hp -= Mathf.RoundToInt((((((2 * attacker.level) / 5) + 2) * attacker.learnedMoves[0].power * (attacker.attackSpecial / target.defenceSpecial) / 50) + 2) * damageMult);
                break;
        }
        hurtAudio.Play();
        for (int i = 0; i < loops; i++) {
            yield return new WaitForSeconds(time);
            ssr.color = invisible;
            yield return new WaitForSeconds(time);
            ssr.color = visible;
        }

        yield return new WaitForSeconds(time * 10);

        attacking = false;
    }
}
