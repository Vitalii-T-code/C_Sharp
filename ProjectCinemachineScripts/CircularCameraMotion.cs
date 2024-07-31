using UnityEngine;

public class CircularCameraMotion : MonoBehaviour
{
    [SerializeField] private Transform _centerPoint;
    [SerializeField] private float _radius = 10f;
    [SerializeField] private float _speed = 1f;

    private float _angle;

    void Update()
    {
        _angle += _speed * Time.deltaTime;

        float x = _centerPoint.position.x + Mathf.Cos(_angle) * _radius;
        float z = _centerPoint.position.z + Mathf.Sin(_angle) * _radius;

        transform.position = new Vector3(x, transform.position.y, z);
        transform.LookAt(_centerPoint);
    }
}
