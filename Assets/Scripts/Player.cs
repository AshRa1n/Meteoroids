using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : BaseUnit
{
    #region SerializeFields

    [SerializeField] private float _accelerationSpeed;
    [SerializeField] private float _rotationSpeed;

    #endregion

    #region Private Fields

    private GameObject _bullet;
    private Queue<GameObject> _bulletQueue;
    private float _rotateInput=0f;
    private bool _accelerateInput = false;
    private GameController _controller;

    #endregion

    #region Unity Functions

    public override void Awake()
    {
        base.Awake();
        _bullet = Resources.Load<GameObject>("Prefabs/Bullet");
        _controller = GameObject.FindObjectOfType<GameController>();
        transform.position = new Vector3(0, 0, 0);
        _bulletQueue = new Queue<GameObject>();
    }
    void Update()
    {
        _accelerateInput = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        _rotateInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }
    void FixedUpdate()
    {
        if (_accelerateInput)
            GetRigidbody.AddForce(transform.up*_accelerationSpeed*Time.deltaTime);
        if (_rotateInput!=0)
            transform.Rotate(0,0,-1*_rotateInput*_rotationSpeed*Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "UFO" || other.tag == "UFObullet")
        {
            Explode(other);
            _controller.Die();
        }
        if (other.tag == "MBig" || other.tag == "MMid" || other.tag == "MSmall")
        {
            Explode(other);
            _controller.Die();
        }
    }

    #endregion

    #region Methods

    void Shoot()
    {
        if (_bulletQueue.Count > 3)
        {
            Destroy(_bulletQueue.Peek());
            _bulletQueue.Dequeue();

        } 
        _bulletQueue.Enqueue(Instantiate(_bullet, transform.position+(transform.up*0.4f),transform.rotation));
    }

    #endregion
}
