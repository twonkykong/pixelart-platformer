using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDrawing : MonoBehaviour
{
    private InputMaster _inputMaster;

    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private Color currentColor;
    [SerializeField] private Transform rayPoint;

    private bool _canDraw = true;

    public bool CanDraw
    {
        set { _canDraw = value; }
    }

    private void Awake()
    {
        _inputMaster = new InputMaster();
        _inputMaster.Actions.Draw.performed += _ => Draw();
    }

    public void SetCurrentColor(Color newColor)
    {
        currentColor = newColor;
    }

    private void Draw()
    {
        if (!_canDraw) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Ray ray = new Ray(rayPoint.position, -Vector3.up);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 2f))
        {
            if (!hitInfo.transform.TryGetComponent(out Picture picture)) return;
            picture.Draw(hitInfo.textureCoord, currentColor);

            playerAnimator.DrawAnimation();
        }
    }

    private void OnEnable()
    {
        _inputMaster.Enable();
    }

    private void OnDisable()
    {
        _inputMaster.Actions.Draw.performed -= _ => Draw();
        _inputMaster.Disable();
    }
}
