using System;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private float _movementSpeed;

    private Camera _mainCamera;
    private Action Move;
    private const float MAX_MOVE_TIME = 1;

    private Vector2 _bottomLeft, _topRight;
    private void Start()
    {
        #if UNITY_ANDROID
            Move = MoveMobile;
        #else
            Move = MovePC;
        #endif

        _movementSpeed = _movementSpeed < MAX_MOVE_TIME ? MAX_MOVE_TIME : _movementSpeed;
        _mainCamera = Camera.main;

        float width = _mainCamera.pixelWidth;
        float height = _mainCamera.pixelHeight;

        _bottomLeft = _mainCamera.ScreenToWorldPoint(new Vector2(0, 0));
        _topRight = _mainCamera.ScreenToWorldPoint(new Vector2(width, height));
    }

    private void FixedUpdate()
    {
        Move();
    }

    private Vector3 Clamp(Vector3 targetPosition)
    {
        targetPosition.x = Math.Max(_bottomLeft.x, targetPosition.x);
        targetPosition.x = Math.Min(_topRight.x, targetPosition.x);

        targetPosition.y = Math.Max(_bottomLeft.y, targetPosition.y);
        targetPosition.y = Math.Min(_topRight.y, targetPosition.y);

        return targetPosition;
    }

    private void MovePC()
    {
        Vector3 targetPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;
        targetPosition = Clamp(targetPosition);

        transform.DOMove(targetPosition, MAX_MOVE_TIME / _movementSpeed);
    }

    private void MoveMobile()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        Vector3 targetPosition = _mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
        targetPosition.z = transform.position.z;
        targetPosition = Clamp(targetPosition);

        transform.DOMove(targetPosition, MAX_MOVE_TIME / _movementSpeed);
    }
}