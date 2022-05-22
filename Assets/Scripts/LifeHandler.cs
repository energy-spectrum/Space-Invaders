using System;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class LifeHandler : MonoBehaviour
{
    [SerializeField] protected float _maxHealth;
    [SerializeField] protected Color _hitColor = Color.red;
    [SerializeField] protected AudioClip[] _deathSounds;

    [Inject] protected SoundProvider _soundProvider;
    protected float _health;
    protected SpriteRenderer _spriteRenderer;

    protected virtual void Start()
    {
        _health = _maxHealth;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<LifeHandler>(out LifeHandler lifeHandlerOther))
        {
            TakeDamage(_maxHealth);
        }
    }

    protected virtual void DisplayHit()
    {
        Sequence s = DOTween.Sequence();
        s.Append(_spriteRenderer.DOColor(_hitColor, 0.032f));
        s.Join(_spriteRenderer.DOFade(0.6f, 0.032f));
        s.Append(_spriteRenderer.DOFade(1f, 0.032f));
        s.Append(_spriteRenderer.DOColor(Color.white, 0.032f));
    }

    public virtual void TakeDamage(float reduceValue)
    {
        if (IsDead)
        {
            return;
        }

        _health -= reduceValue;
        DisplayHit();
        if (_health <= 0)
        {
            Death();
        }
    }

    public event Action onDied;
    public bool IsDead { get; protected set; }
    virtual protected void Death()
    {
        if (IsDead)
        {
            return;
        }
        IsDead = true;

        if (_deathSounds?.Length != 0)
        {
            _soundProvider.PlayRandomSound(SoundType.Death, _deathSounds);
        }

        Destroy(this.gameObject);
        onDied?.Invoke();
    }
}