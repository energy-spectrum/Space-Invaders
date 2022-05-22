using UnityEngine;

[CreateAssetMenu(fileName = "New BulletData", menuName = "Bullet Data", order = 51)]
public class BulletData : ScriptableObject
{
    [SerializeField] private string _name;

    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _damage;
    [SerializeField] private float _destroyTime;

    public string Name { get { return _name; } }
    public float BulletSpeed { get { return _bulletSpeed; } }
    public float Damage { get { return _damage; } }
    public float DestroyTime { get { return _destroyTime; } }
}