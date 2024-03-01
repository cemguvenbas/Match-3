using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private MatchablePool pool;
    private MatchableGrid grid;

    [SerializeField] private Vector2Int dimensions = Vector2Int.one;
    [SerializeField] private Text gridOutput;

    private void GetReferences()
    {
        pool = (MatchablePool)MatchablePool.Instance;
        grid = (MatchableGrid)MatchableGrid.Instance;
    }

    private void Start()
    {
        GetReferences();

        StartCoroutine(Setup());
    }

    private IEnumerator Setup()
    {
        // loading screen

        // pool the matchables
        pool.PoolObjects(dimensions.x * dimensions.y * 2);

        // create the grid
        grid.InitializeGrid(dimensions);

        yield return null;

        StartCoroutine(grid.PopulateGrid());

        // removing loading screen
    }
}
