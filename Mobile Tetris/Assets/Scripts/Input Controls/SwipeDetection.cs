using System.Collections;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField]
    private float minimumSwipeDistance = .25f;
    [SerializeField]
    private float maximumSwipeDuration = 1f;
    [SerializeField]
    private InputManager inputManager;
    [SerializeField, Range(0f, 1f)]
    private float directionThreshold = 0.9f;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if(Vector3.Distance(startPosition, endPosition) >= minimumSwipeDistance &&
            (endTime - startTime) <= maximumSwipeDuration)
        {
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {

        if (Vector2.Dot(Vector2.left, direction) >= directionThreshold)
        {
            Debug.Log("LEFT");
        }
        else if (Vector2.Dot(Vector2.right, direction) >= directionThreshold)
        {
            Debug.Log("RIGHT");
        }
        else if (Vector2.Dot(Vector2.down, direction) >= directionThreshold)
        {
            Debug.Log("DOWN");
        }
    }
}
