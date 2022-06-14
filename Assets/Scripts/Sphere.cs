using UnityEngine;

public class Sphere : MonoBehaviour
{
    [Header("Required Assets")]
    [SerializeField] private WMG_Series _amplitudeSeries;

    [Header("Parameters")]
    public bool IsTouched = false;
    public OscilationModes OscilationMode = OscilationModes.Normal;
    public Vector3 CursorPosition;
    public float Mass = 1.0f;
    public float SpringResistance = 200.0f;
    public float DryFriction = 10.0f;
    public float ViscousFriction = 1.0f;
    public float DeltaTime = 0.05f;
    [SerializeField] private float _oscillationRadius = 2.0f;

    [HideInInspector]
    public enum OscilationModes
    {
        Normal,
        DryFriction,
        ViscousFriction
    }
    
    public float CurHeight { get; private set; } = 0.0f;

    private float _velocity = 0.0f;

    private const float _maxSeriesTime = 25;
    private float _seriesTime = 0;

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
            Reset();
            Drag();
        }
        else
        {
            Oscillate();
        }

        AddSeries();

        transform.position = _startPos + new Vector3(_spherePositionX, CurHeight, _spherePositionZ);
    }

    private void Reset()
    {
        _amplitudeSeries.pointValues.Clear();
        _velocity = 0;
        _seriesTime = 0;
    }

    private void AddSeries() 
    {
        if (_seriesTime < _maxSeriesTime)
        {
            _amplitudeSeries.pointValues.Add(new Vector2(_seriesTime, CurHeight));
            _seriesTime += DeltaTime;
        }
    }

    private void Drag()
    {
        CurHeight = Mathf.Clamp(CursorPosition.y - _startPos.y, -_oscillationRadius, _oscillationRadius);
    }

    private void Oscillate()
    {
        float acceleration = -CurHeight * SpringResistance / Mass;
        switch (OscilationMode)
        {
            case OscilationModes.Normal:
                break;
            case OscilationModes.DryFriction:
                if (Mathf.Abs(CurHeight) < 0.1f && Mathf.Abs(_velocity) < 0.1f)
                {
                    _velocity = 0;
                    acceleration = 0;
                    CurHeight -= CurHeight * DeltaTime;
                }
                else
                {
                    acceleration -= DryFriction * Mathf.Sign(_velocity) / Mass;
                }
                break;
            case OscilationModes.ViscousFriction:
                acceleration -= ViscousFriction * _velocity / Mass;
                break;
        }

        float prVelocity = _velocity;
        _velocity += acceleration * DeltaTime;

        CurHeight += _velocity * DeltaTime;

        CurHeight = Mathf.Clamp(CurHeight, -_oscillationRadius, _oscillationRadius);
    }
}
