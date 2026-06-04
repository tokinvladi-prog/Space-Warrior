using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;

    private Camera _cam;

    public void Start()
    {
        _cam = Camera.main;
    }

    public void Update()
    {
        transform.Translate(Vector3.up * (_speed * Time.deltaTime));

        var screenPos = _cam.WorldToViewportPoint(transform.position);
        if (screenPos.x > 1.1f || screenPos.x < -0.1f ||
            screenPos.y > 1.1f || screenPos.y < -0.1f)
        {
            BulletPool.Instance.ReturnBullet(gameObject);
        }
    }
}
