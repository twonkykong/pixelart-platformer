using UnityEngine;

public class CameraClampPosition : MonoBehaviour
{
    [SerializeField] private Transform minPosition, maxPosition;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float smoothMoveSpeed = 0.25f;

    private Transform _thisObjectTransform;

    private void Awake()
    {
        _thisObjectTransform = transform;
    }

    private void Update()
    {
        ClampPosition();
    }

    private void ClampPosition()
    {
        Vector3 currentPos = _thisObjectTransform.position;
        Vector3 nextPos = maxPosition.position;
        float time = smoothMoveSpeed;

        if (Physics.Linecast(minPosition.position, maxPosition.position, out RaycastHit hitInfo, layerMask))
        {
            nextPos = hitInfo.point + _thisObjectTransform.forward * 0.75f;
            time = 1;
        }

        _thisObjectTransform.position = Vector3.Lerp(currentPos, nextPos, time);
    }
}
