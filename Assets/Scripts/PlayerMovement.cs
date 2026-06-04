using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 2f;

    private Vector3 _minBound, _maxBound;

    public void Start()
    {
        var cam = Camera.main;
        var half = transform.localScale * 0.5f;

        _minBound = cam.ViewportToWorldPoint(new Vector3(0f, 0f)) + half;
        _maxBound = cam.ViewportToWorldPoint(new Vector3(1f, 1f)) - half;
    }

    public void Update()
    {
        var pos = transform.position;
        float dt = _movementSpeed * Time.deltaTime;

        transform.position = new Vector3(
            Mathf.Clamp(pos.x + dt * Input.GetAxis("Horizontal"), _minBound.x, _maxBound.x),
            Mathf.Clamp(pos.y + dt * Input.GetAxis("Vertical"), _minBound.y, _maxBound.y)
        );
    }
}
