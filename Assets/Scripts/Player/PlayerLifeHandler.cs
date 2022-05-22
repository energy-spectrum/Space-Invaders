using System;

public class PlayerLifeHandler : LifeHandler
{
    public event Action<float> onPlayerHPChanged;
    public override void TakeDamage(float reduceValue)
    {
        if (IsDead)
        {
            return;
        }

        _health -= reduceValue;
        onPlayerHPChanged?.Invoke(_health / _maxHealth * 100);
        DisplayHit();
        if (_health <= 0)
        {
            Death();
        }
    }
}