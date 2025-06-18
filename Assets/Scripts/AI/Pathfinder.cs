using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{
    public static List<TerrainScript> FindPath(TerrainScript start, TerrainScript end, int maxSteps)
    {
        Queue<List<TerrainScript>> frontier = new Queue<List<TerrainScript>>();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

        frontier.Enqueue(new List<TerrainScript> { start });
        visited.Add(start.GetGridPos());

        while (frontier.Count > 0)
        {
            List<TerrainScript> currentPath = frontier.Dequeue();
            TerrainScript currentTile = currentPath[currentPath.Count - 1];

            if (currentTile == end)
                return currentPath;

            if (currentPath.Count > maxSteps + 1)
                continue;

            foreach (Vector2Int dir in new Vector2Int[] {Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right })
            {
                Vector2Int neighborCoord = currentTile.GetGridPos() + dir;
                var neighborTile = GridManager.Instance.GetTileAt(neighborCoord);

                if (neighborTile == null || neighborTile.GetTerrainType() != TerrainType.Grass)
                    continue;

                if (visited.Contains(neighborCoord))
                    continue;

                visited.Add(neighborCoord);
                List<TerrainScript> newPath = new(currentPath) { neighborTile };
                frontier.Enqueue(newPath);
            }
        }

        return null;

    }
}
