using System.Collections.Generic;
using UnityEngine;

public class BulletsSpawner : MonoBehaviour
{
    [SerializeField] private int _initialNumBullets = 10;

    private Dictionary<string, Pool<Bullet>> _bulletsPools;
    private void Start()
    {
        _bulletsPools = new Dictionary<string, Pool<Bullet>>();
    }

    private void CreateNewPool(Bullet bullet)
    {
        if (bullet == null)
        {
            throw new System.Exception("Pool creation Error (bullet is null)");
        }
        if (_bulletsPools.ContainsKey(bullet.Name))
        {
            throw new System.Exception("Pool creation Error (bullet is already in Dictionary)");
        }

        Transform newPoolContainer = new GameObject().transform;
        newPoolContainer.name = $"{bullet.name} CONTAINER";
        Pool<Bullet> newPool = new Pool<Bullet>(bullet, _initialNumBullets, newPoolContainer);

        _bulletsPools[bullet.Name] = newPool;
    }

    public void SpawnBullet(Bullet bullet, int layer, Vector3 position, float angle)
    {
        if(_bulletsPools.ContainsKey(bullet.Name) == false)
        {
            CreateNewPool(bullet);
        }

        bullet = _bulletsPools[bullet.Name].GetElement();

        bullet.transform.position = position;
        bullet.transform.eulerAngles = new Vector3(0, 0, angle);
        bullet.gameObject.layer = layer;

        bullet.gameObject.SetActive(true);
    }
}