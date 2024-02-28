using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    [Header("Private Variables")]
    private Vector3 from, to;
    private float howFar;
    private bool idle = true;

    public bool Idle
    {
        get
        {
            return idle;
        }
    }

    [Header("Serialized Variables")]
    [SerializeField] private float speed = 1;

    // coroutine move from current position to new position
    public IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        if (speed <= 0)
            Debug.LogWarning("Speed must be a positive number.");

        from = transform.position;
        to = targetPosition;
        howFar = 0;
        idle = false;

        do
        {
            howFar += speed * Time.deltaTime;
            if (howFar > 1)
                howFar = 1;

            transform.position = Vector3.LerpUnclamped(from, to, Easing(howFar));
            yield return null;

        } while (howFar != 1);
        idle = true;
    }

    private float Easing(float t)
    {
        // Quadratic easing
        return t * t;
    }
}
