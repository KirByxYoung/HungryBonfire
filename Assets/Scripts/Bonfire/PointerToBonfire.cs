using System.Collections;
using UnityEngine;

public class PointerToBonfire : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _icon;

    private Camera _camera;
    private Vector3 _distanceToBonfire;
    private Ray _ray;
    private Plane[] _planes;
    private float _minDistance;
    private Vector3 _worldPositionPoint;
    private bool _isNearBonfire = false;
    private WaitForSeconds _delay;

    private void Awake()
    {
        _camera = Camera.main;
        _delay = new WaitForSeconds(0.015f);
    }

    private void Start()
    {
        _isNearBonfire = false;
        _icon.gameObject.SetActive(false);

        StartCoroutine(ShowPointer());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _isNearBonfire = false;
            _icon.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _isNearBonfire = true;
            _icon.gameObject.SetActive(true);
        }
    }

    private IEnumerator ShowPointer()
    {
        while (true)
        {
            if (_isNearBonfire)
            {
                _distanceToBonfire = transform.position - _player.position;
                _ray = new Ray(_player.position, _distanceToBonfire);
                _minDistance = float.MaxValue;
                _planes = GeometryUtility.CalculateFrustumPlanes(_camera);

                for (int i = 0; i < _planes.Length; i++)
                {
                    if (_planes[i].Raycast(_ray, out float distance))
                    {
                        if (distance < _minDistance)
                        {
                            _minDistance = distance;
                        }
                    }
                }

                _minDistance = Mathf.Clamp(_minDistance, 0, _distanceToBonfire.magnitude);
                _worldPositionPoint = _ray.GetPoint(_minDistance);
                _icon.position = _camera.WorldToScreenPoint(_worldPositionPoint);
            }

            yield return _delay;
        }
    }
}