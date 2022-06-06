using UnityEngine;

public class Spring : MonoBehaviour
{
    [Header("Required Assets")]
    [SerializeField] private Sphere _sphere;

    private float _size = 3.735f;

    private float _localScaleX;
    private float _localScaleY;
    private float _localScaleZ;

    private void Start()
    {
        _localScaleX = transform.localScale.x;
        _localScaleY = transform.localScale.y;
        _localScaleZ = transform.localScale.z;
    }

    private void Update()
    {
        transform.localScale = new Vector3(_localScaleX, (_size + _sphere.CurHeight)/(_size)*_localScaleY, _localScaleZ);
    }
}
