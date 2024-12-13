using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject generator; // Prefab reference
    public GameObject player;
    private GameObject currentLevel;
    public int currentLevelNumber = 0;

    void Start()
    {
        // Ensure time scale is normal if returning from a paused state
        Time.timeScale = 1f;

        Debug.Log("LevelManager: Start called, generating level...");
        GenerateNextLevel();
    }

    public void GenerateNextLevel()
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }

        currentLevelNumber++;
        Debug.Log($"Generating Level {currentLevelNumber}");

        currentLevel = Instantiate(generator);

        DungeonGenerator dungeonGen = currentLevel.GetComponent<DungeonGenerator>();
        if (dungeonGen != null)
        {
            // Apply chosen room size from GameSettings
            dungeonGen.size = new Vector2Int(GameSettings.levelWidth, GameSettings.levelHeight);
            dungeonGen.InitDungeon();
        }
        else
        {
            Debug.LogError("No DungeonGenerator found on the generator prefab!");
        }

        PlacePlayerInStartRoom();

        // Heal player to full
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.HealToFull();
        }

        IncreaseDifficulty(currentLevelNumber);
    }

    private void PlacePlayerInStartRoom()
    {
        if (currentLevel == null)
        {
            Debug.LogWarning("No current level to place player in. Aborting.");
            return;
        }

        RoomBehaviour[] rooms = currentLevel.GetComponentsInChildren<RoomBehaviour>();
        RoomBehaviour startRoom = null;
        foreach (RoomBehaviour room in rooms)
        {
            if (room.roomType == RoomBehaviour.RoomType.Start)
            {
                startRoom = room;
                break;
            }
        }

        if (startRoom != null)
        {
            CharacterController characterController = player.GetComponent<CharacterController>();
            if (characterController != null) characterController.enabled = false;

            player.transform.position = startRoom.transform.position;
            player.transform.rotation = startRoom.transform.rotation;

            if (characterController != null) characterController.enabled = true;

            Debug.Log("Player placed in the start room.");
        }
        else
        {
            Debug.LogWarning("No start room found, placing player at (0,0).");
            player.transform.position = Vector3.zero;
            player.transform.rotation = Quaternion.identity;
        }
    }

    private void IncreaseDifficulty(int level)
    {
        if (currentLevel == null) return;

        Enemy[] enemies = currentLevel.GetComponentsInChildren<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            enemy.maxHealth += level * 10;
            enemy.health = enemy.maxHealth;
        }
    }
}
