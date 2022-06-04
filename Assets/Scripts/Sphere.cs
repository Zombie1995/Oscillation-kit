using UnityEngine;

public class Sphere : MonoBehaviour
{
    public bool IsTouched = false;
    public Vector3 CursorPosition;

    public float Mass = 1.0f;

    public float CurHeight { get; private set; } = 0.0f;

    private float _velocity = 0.0f;

    [SerializeField] private float _springResistance = 1.0f;
    [SerializeField] private float _dryFriction = 1.0f;
    [SerializeField] private float _viscousFriction = 1.0f;
    [SerializeField] private float _oscillationRadius = 1.0f;

    public enum OscilationModes
    {
        Normal,
        DryFriction,
        ViscousFriction
    }
    public OscilationModes oscilationMode = OscilationModes.Normal;

    private Vector3 _startPos;
    private float _spherePositionX;
    private float _spherePositionZ;

    private void Start()
    {
        _startPos = transform.position;

        _spherePositionX = _startPos.x;
        _spherePositionZ = _startPos.z;
    }

    private void Update()
    {
        if (IsTouched)
        {
            _velocity = 0;
            Drag();
        }
        else
        {
            Oscillate();
        }

        transform.position = _startPos + new Vector3(_spherePositionX, CurHeight, _spherePositionZ);
    }

    private void Drag()
    {
        CurHeight = Mathf.Clamp(CursorPosition.y - _startPos.y, -_oscillationRadius, _oscillationRadius);
    }

    private void Oscillate()
    {
        float acceleration = 0;
        switch (oscilationMode)
        {
            case OscilationModes.Normal:
                acceleration = (-CurHeight * _springResistance) / Mass;
                break;
            case OscilationModes.DryFriction:
                acceleration = (-CurHeight * _springResistance - _dryFriction * Mathf.Sign(_velocity)) / Mass;
                break;
            case OscilationModes.ViscousFriction:
                acceleration = (-CurHeight * _springResistance - _viscousFriction * _velocity) / Mass;
                break;
        }

        _velocity += acceleration * Time.deltaTime;

        CurHeight += (1 / 2f) * _velocity * Time.deltaTime;

        CurHeight = Mathf.Clamp(CurHeight, -_oscillationRadius, _oscillationRadius);
    }
}
