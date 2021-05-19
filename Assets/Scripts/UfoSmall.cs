using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoSmall : BaseUnit
{
    #region SerializeFields

    [SerializeField] private float _speed;
    [SerializeField] private float _fireRate;

    #endregion

    #region Private Fields

    private GameObject _bullet;
    private GameObject _shot;
    private float _fireCooldown;
    private GameObject _player;
    private Quaternion _rotation;
    private GameController _controller;

    #endregion

    #region Unity Functions

    public override void Awake()
    {
        base.Awake();
        _bullet = Resources.Load<GameObject>("Prefabs/Ufo_Bullet");
        _player = GameObject.FindGameObjectWithTag("Player");
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

        transform.Rotate(Vector3.forward * _rotation.z);
        _fireCooldown = _fireRate;
    }

    void FixedUpdate()
    {
        Move();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            _controller.GetScore(100);
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
        if (Vector2.Distance(transform.position, _player.transform.position) < 3f)
        {
            Vector2 difference = _player.transform.position - transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
            Shoot();
        }
        else
        {
            transform.rotation = Quaternion.Euler(0,0,_rotation.z);
        }
        GetRigidbody.velocity = transform.up * _speed * Time.deltaTime;
    }

    void Shoot()
    {
        if (Time.time > _fireCooldown)
        {
            _fireCooldown = Time.time + _fireRate;
            if (_shot != null)
                Destroy(_shot);
            _shot = (GameObject)Instantiate(_bullet, transform.position + (transform.up * 0.4f), transform.rotation);
        }
    }

    #endregion
}