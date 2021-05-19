using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : BaseUnit
{
    #region SerializeFields

    [SerializeField] private float _speed;
    [SerializeField] private float _rotation;

    #endregion

    #region Private Fields

    private Vector2 _direction;
    private GameObject _meteorMid;
    private GameObject _meteorSmall;
    private GameController _controller;

    #endregion

    #region Unity Functions

    public override void Awake()
    {
        base.Awake();
        _direction = new Vector2(Mathf.Sign(Random.Range(-1f, 1f)), Mathf.Sign(Random.Range(-1f, 1f)));
        _meteorMid = Resources.Load<GameObject>("Prefabs/Meteor_Mid");
        _meteorSmall = Resources.Load<GameObject>("Prefabs/Meteor_Small");
        _controller = GameObject.FindObjectOfType<GameController>();
    }

    void FixedUpdate()
    {
        GetRigidbody.velocity = _direction * _speed * Time.deltaTime;
        GetRigidbody.angularVelocity = _rotation * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            Split();
            Explode(other);
        }

        if (other.tag == "Player")
        {
            Split();
            Explode(other);
        }

    }
    #endregion

    #region Methods

    void Split()
    {
        if (gameObject.tag == "MBig")
        {
            _controller.GetScore(10);
            Instantiate(_meteorMid,transform.position,transform.rotation);
            Instantiate(_meteorMid, transform.position, transform.rotation);
            Destroy(gameObject);
            _controller.Enemies++;
        }
        if (gameObject.tag == "MMid")
        {
            _controller.GetScore(20);
            Instantiate(_meteorSmall, transform.position, transform.rotation);
            Instantiate(_meteorSmall, transform.position, transform.rotation);
            Destroy(gameObject);
            _controller.Enemies++;
        }

        if (gameObject.tag == "MSmall")
        {
            _controller.GetScore(30);
            Destroy(gameObject);
            _controller.Enemies--;
        }
    }

    #endregion
}
