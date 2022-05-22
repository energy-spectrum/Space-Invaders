using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletData _bulletData;
    private float _lifeTime;
    private Rigidbody2D _rb;

    //public BulletData BulletData { get { return _bulletData; } }
    public string Name { get { return _bulletData.Name; } }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _lifeTime = 0f;
        float angle = transform.eulerAngles.z;
        Vector2 direction = new Vector2(Mathf.Cos((angle + 90) * Mathf.Deg2Rad), Mathf.Sin((angle + 90) * Mathf.Deg2Rad));
        _rb.velocity = direction * _bulletData.BulletSpeed;
    }

    private void Update()
    {
        _lifeTime += Time.deltaTime;
        if (_bulletData.DestroyTime <= _lifeTime)
        {
            DestroyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.TryGetComponent<LifeHandler>(out LifeHandler lifeHandler))
        {
            lifeHandler.TakeDamage(_bulletData.Damage);
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        gameObject.SetActive(false);
    }
}