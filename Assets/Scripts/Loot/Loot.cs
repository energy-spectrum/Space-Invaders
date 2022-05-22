using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
abstract public class Loot : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private float _bottomLimiterY;
    private Rigidbody2D _rigidbody2D;

    private bool isInitialized;
    public void Initialize(float bottomLimiterY)
    {
        if (isInitialized)
        {
            return;
        }

        this._bottomLimiterY = bottomLimiterY;
        isInitialized = true;
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = new Vector2(0, -_moveSpeed);
    }

    private void Update()
    {
        if (transform.position.y < _bottomLimiterY)
        {
            Destroy(this.gameObject);
        }
    }
}