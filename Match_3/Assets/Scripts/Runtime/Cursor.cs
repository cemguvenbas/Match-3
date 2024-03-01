using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Cursor : MonoSingleton<Cursor>
{
    private SpriteRenderer spriteRenderer;
    private Matchable[] selectedMatchable;
    [SerializeField] private Vector2Int verticalStretch = new Vector2Int(1,2);
    [SerializeField] private Vector2Int horizontalStretch = new Vector2Int(2,1);

    [SerializeField]
    private Vector3 halfUp = Vector3.up / 2,
                    halfDown = Vector3.down / 2,
                    halfLeft = Vector3.left / 2,
                    halfRight = Vector3.right / 2;

    protected override void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.enabled = false;

        selectedMatchable = new Matchable[2];
    }

    public void SelectFirst(Matchable toSelect)
    {
        selectedMatchable[0] = toSelect;

        if (!enabled || selectedMatchable[0] == null)
            return;

        transform.position = toSelect.transform.position;

        spriteRenderer.size = Vector2.one;
        spriteRenderer.enabled = true;
    }

    public void SelectSecond(Matchable toSelect)
    {
        selectedMatchable[1] = toSelect;

        if (!enabled || selectedMatchable[0] == null || selectedMatchable[1] == null || !selectedMatchable[0].Idle || !selectedMatchable[1].Idle || selectedMatchable[0] == selectedMatchable[1])
            return;

        if (SelectedAreAdjacent())
            print("Swapping!");

        SelectFirst(null);
    }

    private bool SelectedAreAdjacent()
    {
        if(selectedMatchable[0].position.x == selectedMatchable[1].position.x)
        {
            if (selectedMatchable[0].position.y == selectedMatchable[1].position.y + 1)
            {
                spriteRenderer.size = verticalStretch;
                transform.position += halfDown;
                return true;
            }
            else if (selectedMatchable[0].position.y == selectedMatchable[1].position.y - 1)
            {
                spriteRenderer.size = verticalStretch;
                transform.position += halfUp;
                return true;
            }
        }
        else if (selectedMatchable[0].position.y == selectedMatchable[1].position.y)
        {
            if (selectedMatchable[0].position.x == selectedMatchable[1].position.x + 1)
            {
                spriteRenderer.size = horizontalStretch;
                transform.position += halfLeft;
                return true;
            }
            else if (selectedMatchable[0].position.x == selectedMatchable[1].position.x - 1)
            {
                spriteRenderer.size = horizontalStretch;
                transform.position += halfRight;
                return true;
            }
        }
        return false;
    }


}
