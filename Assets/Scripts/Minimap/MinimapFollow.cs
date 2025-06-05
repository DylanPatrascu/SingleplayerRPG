using UnityEngine;
using UnityEngine.UI;

public class MinimapFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _icon;
    
    [Range(8f, 50f)] public float _cameraZoom = 15f;
    [Range(20f, 50f)] public float _cameraHeight = 20f;

    public bool _rotateWithCamera = false;

    private Camera _camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 camPosition = new Vector3(_player.transform.position.x, _player.transform.position.y + _cameraHeight, _player.transform.position.z);
        transform.position = camPosition;
        _camera.orthographicSize = _cameraZoom;
        if(_rotateWithCamera)
        {
            _icon.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            transform.rotation = Quaternion.Euler(90f, _player.eulerAngles.y, 0f);
        }
        else
        {
            _icon.transform.rotation = Quaternion.Euler(0f, 0f, -_player.eulerAngles.y);
        }
    }
}
