using Fusion;
using UnityEngine;

public class SharedMode_PlayerSpawner : SimulationBehaviour, IPlayerJoined
{

    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private Transform[] spawnPoints; // Array of spawn points

    private int nextSpawnIndex = 0; // Tracks the next available spawn point

    public void PlayerJoined(PlayerRef player)
    {
        // Ensure there's at least one spawn point
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }

        // Get the spawn point and update the index
        Transform spawnPoint = spawnPoints[nextSpawnIndex];
        nextSpawnIndex = (nextSpawnIndex + 1) % spawnPoints.Length;

        // Spawn the player at the selected spawn point
        Runner.Spawn(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }


    //  [SerializeField]
    // private GameObject playerPrefab;

    //  [SerializeField]
    // private Transform spawnPoint;

    // public void PlayerJoined(PlayerRef player)
    // {

    //     Runner.Spawn(playerPrefab, spawnPoint.position, spawnPoint.rotation);
      
    // }
    
}
