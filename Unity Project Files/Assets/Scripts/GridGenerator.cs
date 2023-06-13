using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject gridTilePrefabA;
    public GameObject gridTilePrefabB;
    public GameObject gridTilePrefabC; // The new prefab for the perimeter tiles
    public bool generatePerimeter; // The boolean to control if the perimeter will be generated or not
    public int radius = 5;
    public int perimeterThickness = 1; // The thickness of the perimeter
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

        for (int x = -radius - perimeterThickness; x <= radius + perimeterThickness; x++)
        {
            for (int y = -radius - perimeterThickness; y <= radius + perimeterThickness; y++)
            {
                Vector3 spawnPosition = centerObject.position + new Vector3(x, y, 0f);
                GameObject gridTilePrefab;

                if (generatePerimeter && (x < -radius || x > radius || y < -radius || y > radius))
                {
                    gridTilePrefab = gridTilePrefabC;
                }
                else
                {
                    gridTilePrefab = (x + y) % 2 == 0 ? gridTilePrefabA : gridTilePrefabB;
                }

                Instantiate(gridTilePrefab, spawnPosition, Quaternion.identity, transform);
            }
        }
    }
}
