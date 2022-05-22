using System.Collections.Generic;
using UnityEngine;
using Zenject;

abstract public class Shooting : MonoBehaviour
{
    [SerializeField] protected Bullet _bullet;
    [SerializeField] protected float _firingDalay;
    [SerializeField] private List<Transform> _gunPoints;
    [SerializeField] private AudioClip[] _shootSounds;

    private SoundProvider _soundProvider;
    private BulletsSpawner _bulletsSpawner;
    [Inject]
    private void Construct(SoundProvider soundProvider, BulletsSpawner bulletsSpawner)
    {
        this._soundProvider = soundProvider;
        this._bulletsSpawner = bulletsSpawner;
    }

    protected float _currentDelay;
    private void Update()
    {
        _currentDelay += Time.deltaTime;
        Shoot();
    }

    protected virtual void Shoot()
    {
        if (CanShoot() == false)
        {
            return;
        }

        _currentDelay = 0;

        if (_shootSounds.Length != 0)
        {
            _soundProvider.PlayRandomSound(SoundType.Shooting, _shootSounds);
        }

        float angle = transform.eulerAngles.z;
        for (int i = 0; i < _gunPoints.Count; i++)
        {
            _bulletsSpawner.SpawnBullet(_bullet, gameObject.layer, _gunPoints[i].position, angle);
        }
    }

    protected virtual bool CanShoot()
    {
        if (_currentDelay > _firingDalay)
        {
            return true;
        }
        return false;
    }
}