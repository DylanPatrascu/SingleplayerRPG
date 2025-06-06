using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-2)]
public class ThirdPersonInput : MonoBehaviour, PlayerControls.IThirdPersonMapActions
{
    public Vector2 ScrollInput { get; private set; }

    [SerializeField] private CinemachineCamera _cinemachineCamera;
    [SerializeField] private float _cameraZoomSpeed = 0.1f;
    [SerializeField] private float _cameraMinZoom = 1f;
    [SerializeField] private float _cameraMaxZoom = 5f;

    private CinemachineThirdPersonFollow _thirdPersonFollow;

    private void Awake()
    {
        _thirdPersonFollow = _cinemachineCamera.GetComponent<CinemachineThirdPersonFollow>();
    }

    private void Update()
    {
        _thirdPersonFollow.CameraDistance = Mathf.Clamp(_thirdPersonFollow.CameraDistance + ScrollInput.y, _cameraMinZoom, _cameraMaxZoom);
    }
    private void LateUpdate()
    {
        ScrollInput = Vector2.zero;
    }
    private void OnEnable()
    {
        if (PlayerInputManager.Instance?.PlayerControls == null)
        {
            Debug.LogError("Player controls is not initialized, cannot enable");
            return;
        }

        PlayerInputManager.Instance.PlayerControls.ThirdPersonMap.Enable();
        PlayerInputManager.Instance.PlayerControls.ThirdPersonMap.SetCallbacks(this);
    }

    private void OnDisable()
    {
        if (PlayerInputManager.Instance?.PlayerControls == null)
        {
            Debug.LogError("Player controls is not initialized, cannot disable");
            return;
        }

        PlayerInputManager.Instance.PlayerControls.ThirdPersonMap.Disable();
        PlayerInputManager.Instance.PlayerControls.ThirdPersonMap.RemoveCallbacks(this);
    }

    public void OnScrollCamera(InputAction.CallbackContext context)
    {
        if(!context.performed)
        {
            return;
        }
        Vector2 scrollInput = context.ReadValue<Vector2>();
        ScrollInput = scrollInput.normalized * _cameraZoomSpeed * -1f;
    }
}
