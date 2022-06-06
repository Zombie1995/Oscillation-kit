using UnityEngine;

public class Dragger : MonoBehaviour
{
    [Header("Required Assets")]
    [SerializeField] private Sphere _sphere;

    private Camera _camera;

    private float _distanceToSphere = 0;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 50f))
                return;
            _distanceToSphere = hit.distance;
            _sphere.IsTouched = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _distanceToSphere = 0;
            _sphere.IsTouched = false;
        }

        if (_distanceToSphere != 0)
        {
            Vector3 mousePositionPlusDepth = Input.mousePosition + new Vector3(0, 0, _distanceToSphere);
            _sphere.CursorPosition = _camera.ScreenToWorldPoint(mousePositionPlusDepth);
        }
    }
}
