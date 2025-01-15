using System.Collections.Generic;
using UnityEngine;

public class StorageScoreBox : MonoBehaviour
{
        public int maxItems = 5;  // Maximum number of items to store
    private List<Item> collectedItems = new List<Item>();
    private int totalScore = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object is collectible
        if (other.CompareTag("Collectible"))
        {
            Item item = other.GetComponent<Item>();
            if (item != null && collectedItems.Count < maxItems)
            {
                // Add item to the collection
                collectedItems.Add(item);
                totalScore += item.scoreValue;

                // Optionally: Disable or destroy the collected item
                Destroy(other.gameObject);

                Debug.Log($"Collected {item.itemName}. Total Score: {totalScore}");
            }
            else if (collectedItems.Count >= maxItems)
            {
                Debug.Log("Storage box is full!");
            }
        }
    }

    public int GetTotalScore()
    {
        return totalScore;
    }

    public List<Item> GetCollectedItems()
    {
        return collectedItems;
    }

}
