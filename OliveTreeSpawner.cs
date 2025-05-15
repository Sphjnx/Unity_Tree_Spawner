using UnityEngine;

public class OliveTreeSpawner : MonoBehaviour
{
    public GameObject oliveTreePrefab; // prefab you want to use
    public int treeCount = 100; // number of trees
    public Vector3 areaSize = new Vector3(50, 0, 50); // area 
    public float minDistance = 3f; // min dist between trees

    void Start()
    {
        int placedTrees = 0;

        while (placedTrees < treeCount)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-areaSize.x / 2, areaSize.x / 2),
                0,
                Random.Range(-areaSize.z / 2, areaSize.z / 2)
            );

            Vector3 worldPosition = transform.position + randomPosition;

            if (IsValidPosition(worldPosition))
            {
                GameObject tree = Instantiate(oliveTreePrefab, worldPosition, Quaternion.identity);
                tree.transform.parent = transform; 
                placedTrees++;
            }
        }
    }

    bool IsValidPosition(Vector3 position)
    {
        foreach (Transform child in transform)
        {
            if (Vector3.Distance(child.position, position) < minDistance)
            {
                return false;
            }
        }
        return true;
    }
}
