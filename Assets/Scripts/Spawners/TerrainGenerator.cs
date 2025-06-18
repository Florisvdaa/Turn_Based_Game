using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [Header("Terrain Settings")]
    [SerializeField] private Transform terrainStartPos;
    [SerializeField] private GameObject presetStartChunk; // First chunk preset
    [SerializeField] private GameObject[] terrainPrefabs; // Random terrain chunks
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;

    private float terrainPrefabSize = 3f; // Adjust for chunk size (3x3)

    private void Start()
    {
        GenerateTerrain();
    }

    private void GenerateTerrain()
    {
        if (terrainStartPos == null)
        {
            Debug.LogError("Start position has not been set! Please assign a starting Transform");
            return;
        }

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 pos = terrainStartPos.position + new Vector3(x * terrainPrefabSize, 0, z * terrainPrefabSize);

                GameObject selectedTerrain;

                if (x == 0 && z == 0)
                {
                    // Place the preset starting chunk
                    selectedTerrain = presetStartChunk;
                }
                else
                {
                    // Select a random chunk for other placements
                    selectedTerrain = terrainPrefabs[Random.Range(0, terrainPrefabs.Length)];
                }

                Instantiate(selectedTerrain, pos, Quaternion.identity, transform);
            }
        }
    }

}