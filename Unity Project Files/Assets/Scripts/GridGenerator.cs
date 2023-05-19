using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject gridCellPrefab; // Drag and drop your Grid Cell Prefab here in Inspector
    public GameObject alternateGridCellPrefab; // The alternate grid cell prefab
    public int width = 10; // Width of the grid
    public int height = 10; // Height of the grid

    void Start()
    {
        GenerateGrid();
    }
    
    void GenerateGrid()
    {
        // Adjust the loops to start in negative range
        for (int x = -width / 2; x <= width / 2; x++)
        {
            for (int y = -height / 2; y <= height / 2; y++)
            {
                GameObject cellPrefabToUse;

                // Check if the sum of x and y is even
                if ((x + y) % 2 == 0)
                {
                    cellPrefabToUse = gridCellPrefab;
                }
                else
                {
                    cellPrefabToUse = alternateGridCellPrefab;
                }

                // Instantiate a new Grid Cell
                GameObject newGridCell = Instantiate(cellPrefabToUse, new Vector3(x, y, 0), Quaternion.identity);

                // Make the new Grid Cell a child of this GameObject
                newGridCell.transform.parent = transform;
            }
        }
    }

}
