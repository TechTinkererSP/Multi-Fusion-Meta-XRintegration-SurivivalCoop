using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;  // Array of item prefabs
    public int itemCount = 20;       // Total items to spawn
    private Vector3 spawnArea;        // Size of the spawn area
    public GameObject cube; // Reference to the cube in the scene

    private void Start()
    {
        if (cube != null) { // Get the size of the cube and set spawnArea accordingly 
        spawnArea = cube.GetComponent<Renderer>().bounds.size; 
        } else 
        
        { Debug.LogError("Cube reference is not set in the Inspector."); }

        for (int i = 0; i < itemCount; i++)
        {
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        // Pick a random item prefab
        int randomIndex = Random.Range(0, itemPrefabs.Length);
        GameObject item = itemPrefabs[randomIndex];

        // Generate a random position within the spawn area
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
            Random.Range(-spawnArea.y / 2, spawnArea.y / 2),
            Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
        );

        // Instantiate the item
        Instantiate(item, randomPosition, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the spawn area in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnArea);
    }
}
