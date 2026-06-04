using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _initialSize;

    private readonly Queue<GameObject> _pool = new();

    public void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        Instance = this;

        for (int i = 0; i < _initialSize; i++)
            CreateBullet();
    }

    public GameObject GetBullet()
    {
        if (_pool.Count == 0)
            CreateBullet();

        var bullet = _pool.Dequeue();
        bullet.SetActive(true);
        return bullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        _pool.Enqueue(bullet);
    }

    private void CreateBullet()
    {
        var bullet = Instantiate(_bulletPrefab, transform);
        _bulletPrefab.SetActive(false);
        _pool.Enqueue(bullet);
    }
}
