using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _shootRate = 0.2f;

    private Vector3 _minBound, _maxBound;
    private float _nextShootTime;

    public void Start()
    {
        var col = GetComponent<Collider2D>();
        var cam = Camera.main;
        var half = (Vector2)col.bounds.extents;

        _minBound = (Vector2)cam.ViewportToWorldPoint(new Vector3(0f, 0f)) + half;
        _maxBound = (Vector2)cam.ViewportToWorldPoint(new Vector3(1f, 1f)) - half;
    }

    public void Update()
    {
        Move();

        if (Input.GetButton("Fire1") && Time.time >= _nextShootTime)
        {
            Shoot();
            _nextShootTime = Time.time + _shootRate;
        }
    }

    private void Move()
    {
        Vector2 input = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 delta = input * (_speed * Time.deltaTime);
        Vector2 newPos = (Vector2)transform.position + delta;

        newPos.x = Mathf.Clamp(newPos.x, _minBound.x, _maxBound.x);
        newPos.y = Mathf.Clamp(newPos.y, _minBound.y, _maxBound.y);

        transform.position = newPos;
    }

    private void Shoot()
    {
        var bullet = BulletPool.Instance.GetBullet();
        bullet.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
    }
}
