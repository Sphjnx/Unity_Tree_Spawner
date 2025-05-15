using UnityEngine;

public class OliveTreeSpawner : MonoBehaviour
{
    [Header("prefab and area settings")]

    public GameObject oliveTreePrefab
    public int treeCount = 100;
    public Vector3 areaSize = new Vector3(50, 0, 50);
    public float minDistance = 3f;

    [Header("random position and scale")]

    public Vector2 scaleRange = new Vector2(0.9f, 1.2f);
    public bool randomRotation = true;

    [Header("surface height (Terrain support)")]

    public LayerMask groundLayer = ~0; // search for all layers

    private bool hasSpawned = false;

    void Start()
    {
        if (hasSpawned || oliveTreePrefab == null)
            return;

        int placed = 0;

        while (placed < treeCount)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
                50f, // comes through above
                Random.Range(-areaSize.z / 2f, areaSize.z / 2f)
            );

            Vector3 worldPosition = transform.position + randomOffset;

            // find ground with raycast
            if (Physics.Raycast(worldPosition, Vector3.down, out RaycastHit hit, 100f, groundLayer))
            {
                Vector3 finalPosition = hit.point;

                if (IsValidPosition(finalPosition))
                {
                    GameObject tree = Instantiate(oliveTreePrefab, finalPosition, Quaternion.identity, transform);

                    // random rotation
                    if (randomRotation)
                        tree.transform.Rotate(0, Random.Range(0f, 360f), 0);

                    // random scale
                    float scale = Random.Range(scaleRange.x, scaleRange.y);
                    tree.transform.localScale = Vector3.one * scale;

                    placed++;
                }
            }
        }

        //execute only once
        hasSpawned = true;
        this.enabled = false;
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