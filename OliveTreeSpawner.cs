using UnityEngine;

public class    OliveTreeSpawner: MonoBehaviour
{   
    public GameObject TreePrefab; // tree you want to place
    public int treeCount = 100; // Number of trees 
    public Vector3 areaSize = new Vector3(50, 0, 50); // area
    public float minDistance = 3f; // distance between t

    void Start()
    {
        //place trees
        for(int i = 0;i< treeCount; i++)
        {
            //random position
            Vector3 randomPosition = new Vector3(
                Random.Range(-areaSize.x / 2, areaSize.x / 2), // X
                0, // Y
                Random.Range(-areaSize.z / 2, areaSize.z / 2) // Z
            );

            // check the distance
            if (IsValidPosition(randomPosition))
            {
                //place tree on accurate position
                Instantiate(TreePrefab, randomPosition, Quaternion.identity);

            }
            else
            {
                i--; // if accurate position couldn't found try again
            }
        }
    }

    //distance between trees
    bool IsValidPosition(Vector3 position)
    {
        foreach (Transform child in transform)
        {
            if (Vector3.Distance(child.position, position) < minDistance)
            {
                return false
            }
        }
        return true;
    }
}