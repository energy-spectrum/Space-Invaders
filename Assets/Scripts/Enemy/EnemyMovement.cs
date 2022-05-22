using System;
using UnityEngine;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] protected float _moveDelay;
    private bool _canMove = false;
    private Vector3 _leftBottomLimiter;
    private Vector3 _rightTopLimiter;

    public void StartMove(Vector3 leftBottomLimiter, Vector3 rightTopLimiter)
    {
        this._canMove = true;
        this._leftBottomLimiter = leftBottomLimiter;
        this._rightTopLimiter = rightTopLimiter;
    }

    private float _currentDelay;
    private float _moveTime;
    private void Update()
    {
        _currentDelay += Time.deltaTime;
        if (_canMove && _currentDelay >= _moveDelay)
        {
            Vector3 targetPosition = GetStraightTarget();
            StrafeMovement(targetPosition);
            _currentDelay -= _moveTime;
        }
    }

    private void StrafeMovement(Vector3 secondPos)
    {
        Sequence s = DOTween.Sequence();
        _moveTime = 0.5f;
        Move(secondPos, 0.5f, s);
    }

    private Vector3 GetStraightTarget()
    {
        float moveValueX = UnityEngine.Random.Range(-1f, 1f);
        float moveValueY = UnityEngine.Random.Range(-1f, 1f);

        return transform.position + new Vector3(moveValueX, moveValueY, 0);
    }

    private void Move(Vector3 position, float time, Sequence s)
    {
        position = Clamp(position);
        s.Append(transform.DOMove(position, time));
    }
   
    private Vector3 Clamp(Vector3 targetPosition)
    {
        targetPosition.x = Math.Max(_leftBottomLimiter.x, targetPosition.x);
        targetPosition.x = Math.Min(_rightTopLimiter.x, targetPosition.x);

        targetPosition.y = Math.Max(_leftBottomLimiter.y, targetPosition.y);
        targetPosition.y = Math.Min(_rightTopLimiter.y, targetPosition.y);

        return targetPosition;
    }
}