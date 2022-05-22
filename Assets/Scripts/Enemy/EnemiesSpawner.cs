using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private Transform _initialSpawnPlace;
    [SerializeField] private Transform _leftBottomLimiter;
    [SerializeField] private Transform _rightTopLimiter;
    
    [SerializeField] private float _moveTime = 0.5f;

    private DiContainer _container;
    private LootsSpawner _bulletLootsSpawner;
    private PlayerStatistics _playerStatistics;
    [Inject]
    public void Construct(DiContainer container, LootsSpawner bulletLootsSpawner, PlayerStatistics playerStatistics)
    {
        this._container = container;
        this._bulletLootsSpawner = bulletLootsSpawner;
        this._playerStatistics = playerStatistics;
    }

    private int _numEnemies = 0;
    public int NumEnemis { get { return _numEnemies; } }
    private Queue<GameObject> _waveEnemies;

    static public event Action<bool> onEnemiesSetEvent;
    public event Action onAllEnemiesOnWaveAreDestroyed;

    public void StartSpawnEnemis(Queue<GameObject> waveEnemies, List<Vector3> spawnPlaces)
    {
        this._waveEnemies = waveEnemies;
        StartCoroutine(SetEnemies(spawnPlaces, delay: 0.1f));
        onEnemiesSetEvent.Invoke(true);
    }

    private int _numEnemiesRequiredForSetEnemies;
    private IEnumerator<WaitForSeconds> SetEnemies(List<Vector3> spawnPlaces, float delay)
    {
        int spawnCount = Math.Min(spawnPlaces.Count, _waveEnemies.Count);
        _numEnemiesRequiredForSetEnemies = spawnCount;
        for (int i = 0; i < spawnCount; i++)
        {
            SetEnemy(spawnPlaces[i], _moveTime);
            _numEnemiesRequiredForSetEnemies--;
            yield return new WaitForSeconds(delay);
        }
    }

    public void TrySpawnEnemy(Vector3 spawnPosition)
    {
        if (_waveEnemies.Count != 0)
        {
            if (_numEnemiesRequiredForSetEnemies < _waveEnemies.Count)
            {
                SetEnemy(spawnPosition, _moveTime);
                return;
            }
        }
    }

    private void SetEnemy(Vector3 spawnPosition, float moveTime)
    {
        GameObject enemy = _container.InstantiatePrefab(_waveEnemies.Peek(), _initialSpawnPlace.position, Quaternion.Euler(0, 0, 180), null);
        _waveEnemies.Dequeue();
        _numEnemies++;

        EnemyLifeHandler enemyLifeHandler = enemy.GetComponent<EnemyLifeHandler>();
        enemyLifeHandler.SetSpawnPosition(spawnPosition);
        enemyLifeHandler.onDied += (Vector3 spawnPosition) =>
        {
            _numEnemies--;
            _bulletLootsSpawner.TrySpawnLoot(enemyLifeHandler.transform.position);
            TrySpawnEnemy(spawnPosition);

            if (AreAllEnemiesDestroyed())
            {
                onEnemiesSetEvent.Invoke(false);
                onAllEnemiesOnWaveAreDestroyed.Invoke();
            }
        };
        _playerStatistics.SubscribeToEnemyDeathEvent(enemyLifeHandler);

        enemy.transform.DOMove(spawnPosition, moveTime).onComplete += () =>
        {
            enemy.GetComponent<EnemyMovement>().StartMove(_leftBottomLimiter.position, _rightTopLimiter.position);
        };
    }

    public bool AreAllEnemiesDestroyed()
    {
        if (_numEnemies == 0 && _numEnemiesRequiredForSetEnemies == 0)
        {
            return true;
        }

        return false;
    }
}