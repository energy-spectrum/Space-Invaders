using UnityEngine;
using Zenject;

public class GameFlow : MonoBehaviour
{
    [SerializeField] private float _waitingTimeToWin;

    private SceneLoader _sceneLoader;

    [Inject]
    public void Construct(PlayerLifeHandler playerLifeHandler, SceneLoader sceneLoader, WaveSpawner waveSpawner)
    {
        playerLifeHandler.onDied += () => Invoke(nameof(Lost), 1.5f);
        this._sceneLoader = sceneLoader;
        waveSpawner.onLastWaveFinished += () => _areAllEnemiesDestroyed = true;
    }

    private bool _areAllEnemiesDestroyed;
    private float _timeFromBeginningDeathAllEnemies;
    private bool _isGameFinished;
    private void Update()
    {
        if (_areAllEnemiesDestroyed == true && _isGameFinished == false)
        {
            _timeFromBeginningDeathAllEnemies += Time.deltaTime;
            if (_timeFromBeginningDeathAllEnemies >= _waitingTimeToWin)
            {
                Win();
            }
        }
    }

    private void Win()
    {
        _isGameFinished = true;
        _sceneLoader.LoadNextLevel();
    }

    private void Lost()
    {
        _isGameFinished = true;
        _sceneLoader.ReloadCurrentLevel();
    }
}