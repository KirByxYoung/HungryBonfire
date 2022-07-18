using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Translate(new Vector3(_joystick.Horizontal * _speed * Time.deltaTime, 0, _joystick.Vertical * _speed * Time.deltaTime), Space.World);
    }
}