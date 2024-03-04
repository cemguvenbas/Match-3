using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Matchable : Movable
{
    private int type;

    public int Type => type;

    private SpriteRenderer spriteRenderer;

    private Cursor cursor;

    private MatchablePool pool;

    // where is this matchable in the grid?
    public Vector2Int position;

    private void Awake()
    {
        cursor = Cursor.Instance;
        pool = (MatchablePool)MatchablePool.Instance;
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void SetType(int type, Sprite sprite, Color color)
    {
        this.type = type;
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = color;
    }

    public IEnumerator Resolve(Transform collectionPoint)
    {
        // draw above others in the grid
        spriteRenderer.sortingOrder = 2;

        // move off the grid to a collection point 
        yield return StartCoroutine(MoveToPosition(collectionPoint.position));

        // reset
        spriteRenderer.sortingOrder = 1;

        // return it back to the pool
        pool.ReturnObjectToPool(this);

        yield return null;
    }

    private void OnMouseDown()
    {
        print("Mouse Down at (" + position.x + " ," + position.y + ")");
        cursor.SelectFirst(this);
    }

    private void OnMouseUp()
    {
        print("Mouse Up at (" + position.x + " ," + position.y + ")");
        cursor.SelectFirst(null);
    }

    private void OnMouseEnter()
    {
        print("Mouse Enter at (" + position.x + " ," + position.y + ")");
        cursor.SelectSecond(this);
    }

    public override string ToString()
    {
        return gameObject.name;
    }
}
