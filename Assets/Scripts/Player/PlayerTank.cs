using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : MonoBehaviour
{
    [Header("Player Tank Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int maxMoveDistance = 4; // Number of terrain it can move per turn

    public void TryMove(Vector3 targetPos)
    {
        TerrainScript start = ClosestTile(transform.position);
        TerrainScript end = ClosestTile(targetPos);

        if (start == null || end == null) return;

        var path = Pathfinder.FindPath(start, end, maxMoveDistance);
        if (path != null)
        {
            StartCoroutine(MovePath(path));
        }
        else
        {
            Debug.Log("No valid path to destination.");
        }
    }

    private TerrainScript ClosestTile(Vector3 worldPos)
    {
        Vector2Int coord = new(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.z));
        return GridManager.Instance.GetTileAt(coord);
    }

    private IEnumerator MovePath(List<TerrainScript> path)
    {
        for (int i = 1; i < path.Count; i++) // skip 0, which is current position
        {
            Vector3 target = path[i].transform.position;
            yield return StartCoroutine(MoveSmoothly(target));
        }

        GridManager.Instance.GetAllTiles().ForEach(t => t.ShowAsReachable(false));
    }
    private IEnumerator MoveSmoothly(Vector3 target)
    {
        float journeyTime = Vector3.Distance(transform.position, target) / moveSpeed;
        float elapsedTime = 0;
        Vector3 startPos = transform.position;

        while (elapsedTime < journeyTime)
        {
            transform.position = Vector3.Lerp(startPos, target, elapsedTime / journeyTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
    }
    public void ShowMoveableTiles()
    {
        TerrainScript start = ClosestTile(transform.position);
        if (start == null) return;

        foreach (var tile in GridManager.Instance.GetAllTiles())
        {
            var path = Pathfinder.FindPath(start, tile, maxMoveDistance);
            bool valid = path != null && tile.GetTerrainType() == TerrainType.Grass;
            tile.ShowAsReachable(valid);
        }
    }
    public void ClearTileHighlights()
    {
        foreach (var tile in GridManager.Instance.GetAllTiles())
        {
            tile.ShowAsReachable(false);
        }
    }

    private void OnMouseDown()
    {
        ShowMoveableTiles();
    }

}
