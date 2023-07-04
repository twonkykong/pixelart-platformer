using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed = 0.5f;

    private Transform _thisObjectTransform;

    private void Awake()
    {
        _thisObjectTransform = transform;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _thisObjectTransform.position = Vector3.Lerp(_thisObjectTransform.position, target.position + offset, speed);
    }
}
