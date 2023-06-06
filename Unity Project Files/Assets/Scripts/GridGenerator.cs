using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject gridTilePrefabA;
    public GameObject gridTilePrefabB;
    public int radius = 5;
    public Transform centerObject;

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        if (centerObject == null)
        {
            Debug.LogError("No center object assigned for grid generation.");
            return;
        }

        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                Vector3 spawnPosition = centerObject.position + new Vector3(x, y, 0f);

                GameObject gridTilePrefab = (x + y) % 2 == 0 ? gridTilePrefabA : gridTilePrefabB;
                Instantiate(gridTilePrefab, spawnPosition, Quaternion.identity, transform);
            }
        }
    }
}
