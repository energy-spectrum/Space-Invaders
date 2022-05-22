using System;
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    private int _currentScore;

    public event Action<int> onChangedScore;
    private void IncreaseScore(int value)
    {
        if(value <= 0)
        {
            return;
        }

        _currentScore += value;
        onChangedScore?.Invoke(_currentScore);
    }

    public void SubscribeToEnemyDeathEvent(EnemyLifeHandler enemyLifeHandler)
    {
        enemyLifeHandler.onDied += (Vector3 missing) => IncreaseScore(1);
    }
}