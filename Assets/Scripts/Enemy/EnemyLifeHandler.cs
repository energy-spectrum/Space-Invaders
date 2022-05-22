using System;
using UnityEngine;

public class EnemyLifeHandler : LifeHandler
{
    private Vector3 _spawnPosition;
    public void SetSpawnPosition(Vector3 spawnPosition)
    {
        this._spawnPosition = spawnPosition;
    }

    new public event Action<Vector3> onDied;
    protected override void Death()
    {
        if (IsDead)
        {
            return;
        }

        base.Death();
        onDied?.Invoke(_spawnPosition);
    }
}