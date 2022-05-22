using UnityEngine;

public class LootsSpawner : MonoBehaviour
{
    [SerializeField] private Loot[] _typesBullets;
    [SerializeField] private float _spawnChancePercentBullet;
    [SerializeField] private Transform _bottomLimiter;

    public void TrySpawnLoot(Vector3 position)
    {
        int percent = UnityEngine.Random.Range(1, 101);
        if (percent <= _spawnChancePercentBullet)
        {
            int rand = UnityEngine.Random.Range(0, _typesBullets.Length);
            SpawnLoot(_typesBullets[rand], position);
        }
    }

    private void SpawnLoot(Loot loot, Vector3 position)
    {
        Instantiate(loot, position, Quaternion.identity).Initialize(_bottomLimiter.position.y);
    }
}