using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public float battleDuration = 10f; // Total duration of the battle in seconds
    public string[] sceneNames; // Array of scene names to play during the battle
    public float sceneTransitionDelay = 1f; // Delay between scene transitions in seconds

    private float battleTimer; // Timer variable to keep track of the battle timer
    private bool isBattleActive; // Flag to determine if the battle is active
    private List<string> shuffledScenes; // Shuffled list of scene names
    private int currentSceneIndex; // Index of the current scene in the shuffled array
    private float sceneTransitionTimer; // Timer for the scene transition buffer

    private void Start()
    {
        StartBattle(); // Start the battle when the script is enabled
    }

    private void Update()
    {
        if (isBattleActive)
        {
            // Decrement the timer each frame based on the time delta
            battleTimer -= Time.deltaTime;

            if (battleTimer <= 0f)
            {
                // Timer reached zero, perform actions for both player and enemy attacks
                PerformPlayerAttack();
                PerformEnemyAttack();

                if (PlayerOrEnemyHealthIsZero())
                {
                    StopBattle();
                }
                else
                {
                    sceneTransitionTimer -= Time.deltaTime;
                    if (sceneTransitionTimer <= 0f)
                    {
                        LoadNextSceneAdditively();
                    }
                }
            }
        }
    }

    private void StartBattle()
    {
        battleTimer = battleDuration; // Reset the battle timer
        isBattleActive = true; // Set the battle as active
        sceneTransitionTimer = 0f; // Reset the scene transition timer

        // Shuffle the scene array and create a shuffled list
        shuffledScenes = new List<string>(sceneNames);
        ShuffleList(shuffledScenes);
        currentSceneIndex = 0;

        LoadCurrentSceneAdditively();
    }

    private void LoadCurrentSceneAdditively()
    {
        string currentSceneName = shuffledScenes[currentSceneIndex]; // Get the current scene from the shuffled list
        SceneManager.LoadScene(currentSceneName, LoadSceneMode.Additive); // Load the scene additively
        sceneTransitionTimer = sceneTransitionDelay; // Set the scene transition timer
    }

    private void LoadNextSceneAdditively()
    {
        currentSceneIndex++;
        if (currentSceneIndex >= shuffledScenes.Count)
        {
            currentSceneIndex = 0; // Start from the beginning if all scenes have been played
        }
        UnloadCurrentSceneAdditively();
        LoadCurrentSceneAdditively();
    }

    private void UnloadCurrentSceneAdditively()
    {
        string currentSceneName = shuffledScenes[currentSceneIndex]; // Get the current scene from the shuffled list
        SceneManager.UnloadSceneAsync(currentSceneName); // Unload the scene additively
    }

    private void PerformPlayerAttack()
    {
        // Code for the player's attack logic
        Debug.Log("Player attacks!");
    }

    private void PerformEnemyAttack()
    {
        // Code for the enemy's attack logic
        Debug.Log("Enemy attacks!");
    }

    private bool PlayerOrEnemyHealthIsZero()
    {
        // Implement your logic here to check if the player's health or the enemy's health reaches zero
        return false;
    }

    private void StopBattle()
    {
        isBattleActive = false; // Set the battle as inactive
    }

    // Utility function to shuffle a list using the Fisher-Yates algorithm
    private void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
