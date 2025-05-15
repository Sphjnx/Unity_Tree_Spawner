using UnityEngine;

public class OliveTreeSpawner : MonoBehaviour
{
    [Header("Prefab and Area Settings")]
    public GameObject oliveTreePrefab;
    public int treeCount = 100;
    public Vector3 areaSize = new Vector3(50, 0, 50);
    public float minDistance = 3f;

    [Header("Random Position and Scale")]
    public Vector2 scaleRange = new Vector2(0.9f, 1.2f);
    public bool randomRotation = true;

    [Header("Surface Height (Terrain Support)")]
    public LayerMask groundLayer = ~0; // all layers

    private bool hasSpawned = false;

    void Start()
    {
        if (Application.isPlaying) // only spawn when game starts
        {
            if (hasSpawned || oliveTreePrefab == null)
                return;

            SpawnTrees();

            hasSpawned = true;
            enabled = false;
        }
    }

    public void SpawnTrees()
    {
        int placed = 0;
        int attempts = 0;
        int maxAttempts = treeCount * 10; // prevents infinite loop

        while (placed < treeCount && attempts < maxAttempts)
        {
            attempts++;

            Vector3 randomOffset = new Vector3(
                Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
                50f,  // ray assigns from above
                Random.Range(-areaSize.z / 2f, areaSize.z / 2f)
            );

            Vector3 worldPosition = transform.position + randomOffset;

            if (Physics.Raycast(worldPosition, Vector3.down, out RaycastHit hit, 100f, groundLayer))
            {
                Vector3 finalPosition = hit.point;

                if (IsValidPosition(finalPosition))
                {
                    GameObject tree = Instantiate(oliveTreePrefab, finalPosition, Quaternion.identity, transform);

                    if (randomRotation)
                        tree.transform.Rotate(0, Random.Range(0f, 360f), 0);

                    float scale = Random.Range(scaleRange.x, scaleRange.y);
                    tree.transform.localScale = Vector3.one * scale;

                    placed++;
                }
            }
        }
    }

    bool IsValidPosition(Vector3 pos)
    {
        foreach (Transform child in transform)
        {
            if (Vector3.Distance(child.position, pos) < minDistance)
                return false;
        }
        return true;
    }
}
