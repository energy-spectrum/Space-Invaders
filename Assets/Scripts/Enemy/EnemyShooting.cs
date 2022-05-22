using UnityEngine;

public class EnemyShooting : Shooting
{
    [SerializeField] private float _minAttackDelay;
    [SerializeField] private float _maxAttackDelay;

    private float _targetDelay;
 
    private void Start()
    {
        SetRandomDelay();
    }

    protected override void Shoot()
    {
        base.Shoot();
        SetRandomDelay();
    }

    protected override bool CanShoot()
    {
        if (_currentDelay >= _targetDelay)
        {
            return true;
        }
        return false;
    }

    private void SetRandomDelay()
    {
        _targetDelay = Random.Range(_minAttackDelay, _maxAttackDelay);
    }
}