using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Required Assets")]
    [SerializeField] private Sphere _sphere;
    [SerializeField] private InputField _springResistance;
    [SerializeField] private InputField _mass;
    [SerializeField] private InputField _dryFriction;
    [SerializeField] private InputField _viscousFriction;
    [SerializeField] private Toggle _normalToggle;
    [SerializeField] private Toggle _dryFrictionToggle;
    [SerializeField] private Toggle _viscousFrictionToggle;

    private void Start()
    {
        _springResistance.text = _sphere.SpringResistance.ToString();
        _mass.text = _sphere.Mass.ToString();
        _dryFriction.text = _sphere.DryFriction.ToString();
        _viscousFriction.text = _sphere.ViscousFriction.ToString();
    }

    private void Update()
    {
        float.TryParse(_springResistance.text, out _sphere.SpringResistance);
        float.TryParse(_mass.text, out _sphere.Mass);
        float.TryParse(_dryFriction.text, out _sphere.DryFriction);
        float.TryParse(_viscousFriction.text, out _sphere.ViscousFriction);
    }

    public void EnableNormalMode(bool isOn)
    {
        if (!isOn)
        {
            if (!(_dryFrictionToggle.isOn || _viscousFrictionToggle.isOn))
            {
                _normalToggle.isOn = true;
            }
            else return;
        }

        _dryFrictionToggle.isOn = false;
        _viscousFrictionToggle.isOn = false;

        _sphere.OscilationMode = Sphere.OscilationModes.Normal;
    }

    public void EnableDryFrictionMode(bool isOn) 
    {
        if (!isOn)
        {
            if (!(_normalToggle.isOn || _viscousFrictionToggle.isOn))
            {
                _dryFrictionToggle.isOn = true;
            }
            else return;
        }

        _normalToggle.isOn = false;
        _viscousFrictionToggle.isOn = false;

        _sphere.OscilationMode = Sphere.OscilationModes.DryFriction;
    }

    public void EnableViscousFrictionMode(bool isOn)
    {
        if (!isOn) 
        {
            if (!(_normalToggle.isOn || _dryFrictionToggle.isOn || _viscousFrictionToggle.isOn))
            {
                _viscousFrictionToggle.isOn = true;
            }
            else return;
        }

        _normalToggle.isOn = false;
        _dryFrictionToggle.isOn = false;
        
        _sphere.OscilationMode = Sphere.OscilationModes.ViscousFriction;
    }
}
