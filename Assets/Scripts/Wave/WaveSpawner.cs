using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private Transform _waveContainer;
    [SerializeField] private EnemiesSpawner _enemiesSpawner;

    [SerializeField] private float _waveStartDelay = 1.5f;

    private Queue<Wave> _waves = new Queue<Wave>();
    private Wave _currentWave;
    
    private void Start()
    {
        foreach (Transform wavesChild in _waveContainer)
        {
            _waves.Enqueue(wavesChild.GetComponent<Wave>());
        }

        _enemiesSpawner.onAllEnemiesOnWaveAreDestroyed += TryStartNextWave;
        TryStartNextWave();
    }

    public event Action onLastWaveFinished;
    private void StartNextWave()
    {
        _currentWave = _waves.Peek();
        _waves.Dequeue();

        List<Vector3> spawnPlaces = new List<Vector3>();
        foreach (Transform place in _currentWave.transform)
        {
            spawnPlaces.Add(place.position);
        }

        _enemiesSpawner.StartSpawnEnemis(new Queue<GameObject>(_currentWave.WaveEnemies), spawnPlaces);
    }

    private void TryStartNextWave()
    {
        if (_enemiesSpawner.AreAllEnemiesDestroyed() == false)
        {
            return;
        }

        if (_waves.Count == 0)
        {
            onLastWaveFinished.Invoke();
            return;
        }

        Invoke(nameof(StartNextWave), _waveStartDelay);
    }
}