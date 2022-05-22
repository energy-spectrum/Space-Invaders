using UnityEngine;
using Zenject;

public class ProvidersInstaller : MonoInstaller
{
    [SerializeField] private PlayerLifeHandler _playerLifeHandler;
    [SerializeField] private PlayerStatistics _playerStatistics;
    [SerializeField] private WaveSpawner _waveSpawner;
    [SerializeField] private LootsSpawner _lootsSpawner;
    [SerializeField] private SoundProvider _soundProvider;
    [SerializeField] private BulletsSpawner _bulletsSpawner;
    [SerializeField] private SceneLoader _sceneLoader;
    
    public override void InstallBindings()
    {
        Container.Bind<PlayerLifeHandler>().FromInstance(_playerLifeHandler).AsSingle();
        Container.Bind<PlayerStatistics>().FromInstance(_playerStatistics).AsSingle();
        Container.Bind<WaveSpawner>().FromInstance(_waveSpawner).AsSingle();
        Container.Bind<LootsSpawner>().FromInstance(_lootsSpawner).AsSingle();
        Container.Bind<SoundProvider>().FromInstance(_soundProvider).AsSingle();
        Container.Bind<BulletsSpawner>().FromInstance(_bulletsSpawner).AsSingle();
        Container.Bind<SceneLoader>().FromInstance(_sceneLoader).AsSingle();
    }
}