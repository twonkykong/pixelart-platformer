using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;

    [SerializeField] private UnityEvent onEnter;
    [SerializeField] private UnityEvent onExit;
    [SerializeField] private UnityEvent onStay;

    private List<Collider> _collidersInTrigger = new List<Collider>();
    private Transform _thisObjectTransform;

    private void Awake()
    {
        _thisObjectTransform = transform;
    }

    private void Update()
    {
        CheckTrigger();
    }

    private void CheckTrigger()
    {
        Collider[] colliders = Physics.OverlapBox(_thisObjectTransform.position, _thisObjectTransform.localScale / 2, _thisObjectTransform.rotation, targetLayer);
        if (colliders.Length == 0) return;

        List<Collider> currentColliders = new List<Collider>();

        foreach (Collider collider in colliders)
        {
            if (_collidersInTrigger.Contains(collider)) onStay?.Invoke();
            else onEnter?.Invoke();

            currentColliders.Add(collider);
        }

        foreach (Collider collider in _collidersInTrigger)
        {
            if (!currentColliders.Contains(collider))
            {
                onExit?.Invoke();
                _collidersInTrigger.Remove(collider);
            }
        }
    }
}
