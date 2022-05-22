public class PlayerShooting : Shooting
{
    private bool _enemiesSet = false;
    private void StartShoot(bool condition)
    {
        _enemiesSet = condition;
    }

    private void Start()
    {
        EnemiesSpawner.onEnemiesSetEvent += StartShoot;
    }

    protected override bool CanShoot()
    {
        if (_currentDelay > _firingDalay && _enemiesSet)
        {
            return true;
        }
        return false;
    }

    public void ChangeBullet(Bullet bullet)
    {
        this._bullet = bullet;
    }
}