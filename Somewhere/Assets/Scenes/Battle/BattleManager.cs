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
    private bool isLoadingScene; // Flag to check if a scene is currently being loaded
    private List<Scene> loadedScenes; // List of loaded scenes

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
                StopBattle(); // Call StopBattle method here
            }
            else
            {
                if (!isLoadingScene)
                {
                    StartCoroutine(LoadNextSceneAfterDelay());
                }
            }
        }
    }
}


    private void StartBattle()
    {
        battleTimer = battleDuration; // Reset the battle timer
        isBattleActive = true; // Set the battle as active
        isLoadingScene = false; // Reset the scene loading flag
        loadedScenes = new List<Scene>(); // Initialize the list of loaded scenes

        // Shuffle the scene array and create a shuffled list
        shuffledScenes = new List<string>(sceneNames);
        ShuffleList(shuffledScenes);
        currentSceneIndex = 0;

        // Unload any previously loaded scenes
        UnloadAllLoadedScenes();

        LoadCurrentSceneAdditively();
    }
private void StopBattle()
{
    isBattleActive = false; // Set the battle as inactive
    // Perform any additional actions to clean up the battle, reset variables, etc.
}
    private IEnumerator LoadNextSceneAfterDelay()
    {
        isLoadingScene = true;

        yield return new WaitForSeconds(sceneTransitionDelay);

        LoadNextSceneAdditively();
        isLoadingScene = false;
    }

    private void LoadCurrentSceneAdditively()
    {
        string currentSceneName = shuffledScenes[currentSceneIndex];
        SceneManager.LoadScene(currentSceneName, LoadSceneMode.Additive);
        Scene loadedScene = SceneManager.GetSceneByName(currentSceneName);
        loadedScenes.Add(loadedScene);
    }

    private void LoadNextSceneAdditively()
    {
        UnloadCurrentSceneAdditively(); // Unload the current scene first

        currentSceneIndex++;
        if (currentSceneIndex >= shuffledScenes.Count)
        {
            currentSceneIndex = 0; // Start from the beginning if all scenes have been played
            ShuffleList(shuffledScenes); // Shuffle the scene list again for the next cycle
        }
        LoadCurrentSceneAdditively();
    }

    private void UnloadCurrentSceneAdditively()
    {
        if (currentSceneIndex >= 0 && currentSceneIndex < loadedScenes.Count)
        {
            Scene currentScene = loadedScenes[currentSceneIndex];
            if (currentScene.isLoaded)
            {
                SceneManager.UnloadSceneAsync(currentScene);
                loadedScenes.RemoveAt(currentSceneIndex);
            }
        }
    }

    private void UnloadAllLoadedScenes()
    {
        foreach (var scene in loadedScenes)
        {
            if (scene.isLoaded)
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
        loadedScenes.Clear();
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
