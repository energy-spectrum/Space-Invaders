using UnityEngine;

public class BulletLoot : Loot
{
    [SerializeField] private Bullet _bullet;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<PlayerLifeHandler>(out PlayerLifeHandler playerLifeHandler))
        {
            col.GetComponent<PlayerShooting>().ChangeBullet(_bullet);
            Destroy(gameObject);
        }
    }
}