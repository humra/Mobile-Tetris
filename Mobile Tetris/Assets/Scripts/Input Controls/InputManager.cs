using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private TouchControls touchControls;
    private Camera mainCamera;

    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;

    private void Awake()
    {
        touchControls = new TouchControls();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }

    private void Start()
    {
        touchControls.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        touchControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if(OnStartTouch != null)
        {
            OnStartTouch(ScreenToWorld(touchControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
        }
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null)
        {
            OnEndTouch(ScreenToWorld(touchControls.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
        }
    }

    private Vector3 ScreenToWorld(Vector3 position)
    {
        position.z = mainCamera.nearClipPlane;
        return mainCamera.ScreenToWorldPoint(position);
    }
}
