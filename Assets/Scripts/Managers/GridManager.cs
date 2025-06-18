using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    public Dictionary<Vector2Int, TerrainScript> tiles = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void RegisterTile(Vector2Int coord, TerrainScript tile)
    {
        if (!tiles.ContainsKey(coord))
            tiles.Add(coord, tile);
    }

    public TerrainScript GetTileAt(Vector2Int coord)
    {
        tiles.TryGetValue(coord, out TerrainScript tile);
        return tile;
    }

    public List<TerrainScript> GetAllTiles() => new List<TerrainScript>(tiles.Values);
}
