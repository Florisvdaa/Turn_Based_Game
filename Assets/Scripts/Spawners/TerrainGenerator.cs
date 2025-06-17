using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [Header("Terrain Settings")]
    [SerializeField] private Transform terrainStartPos;
    [SerializeField] private GameObject[] terrainPrefabs;
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    
    // Private
    private float terrainPrefabSize = 1f;

    private void Start()
    {
        GenerateTerrain();
    }

    private void GenerateTerrain()
    {
        if(terrainStartPos == null)
        {
            Debug.LogError("Start position has not been set! Please assign a starting Transform");
            return;
        }

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 pos = terrainStartPos.position + new Vector3(x * terrainPrefabSize, 0 , z * terrainPrefabSize);
                GameObject selectedTerrain = ChooseTerrainType();
                Instantiate(selectedTerrain, pos, Quaternion.identity, transform);

            }
        }
    }

    private GameObject ChooseTerrainType()
    {
        int randomValue = Random.Range(0, 100);

        switch (randomValue)
        {
            case int n when (n < 70):
                return terrainPrefabs[0]; // Grass 

            case int n when (n < 85):
                return terrainPrefabs[1]; // Water 

            case int n when (n < 100):
                return terrainPrefabs[2]; // High Terrain 

            default:
                return terrainPrefabs[0]; // Fallback (shouldn't be needed)
        }
    }
}
