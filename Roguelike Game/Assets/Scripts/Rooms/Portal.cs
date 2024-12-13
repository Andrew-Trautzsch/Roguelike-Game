using UnityEngine;

public class Portal : Interactable
{

    protected override void Interact()
    {
        Debug.Log("Player interacted with the portal!");
        
        // Trigger the level transition
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.GenerateNextLevel();
        }
        else
        {
            Debug.LogError("LevelManager not found!");
        }
    }
}
