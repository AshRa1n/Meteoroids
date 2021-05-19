using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoBig : BaseUnit
{
    #region SerializeFields

    [SerializeField] private float _speed;
    [SerializeField] private float _fireRate;

    #endregion

    #region Private Fields

    private GameObject _bullet;
    private Quaternion _rotation;
    private GameObject _shot;
    private float _fireCooldown;
    private GameController _controller;

    #endregion

    #region Unity Functions

    public override void Awake()
    {
        base.Awake();
        _bullet = Resources.Load<GameObject>("Prefabs/Ufo_Bullet");
        _controller = GameObject.FindObjectOfType<GameController>();
        if (transform.position.x > 0)
        {
            _speed *= -1;
            _rotation.z = 90;
        }
        else
        {
            _rotation.z = -90;
        }

        transform.Rotate(Vector3.forward*_rotation.z);
        _fireCooldown = _fireRate;
    }

    void FixedUpdate()
    {
        Move();
        Shoot();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            _controller.GetScore(50);
            Explode(other);
            _controller.Enemies--;
        }
        if (other.tag == "Player")
        {
            Explode(other);
        }

    }
    #endregion

    #region Methods

    void Move()
    {
        GetRigidbody.velocity = transform.up * _speed * Time.deltaTime;
    }

    void Shoot()
    {
        if (Time.time > _fireCooldown)
        {
            _fireCooldown = Time.time + _fireRate;
            if (_shot != null)
                Destroy(_shot);
            _shot = (GameObject) Instantiate(_bullet, transform.position + (transform.up * 1f),
                Quaternion.Euler(0, 0, Random.Range(-360f, 360f)));
        }
    }

    #endregion
}