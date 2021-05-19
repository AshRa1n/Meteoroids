using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnit : MonoBehaviour
{
    #region Private Fields

    private Rigidbody2D _rigidbody;
    private GameObject _explosion;

    #endregion

    #region Public Props

    public Rigidbody2D GetRigidbody => _rigidbody;
    public GameObject Explosion => _explosion;

    #endregion

    #region Unity Functions

    public virtual void Awake()
    {
        _explosion = Resources.Load<GameObject>("Prefabs/Shader");
        if (GetComponent<Rigidbody2D>())
            _rigidbody = GetComponent<Rigidbody2D>();
    }

    #endregion

    #region Methods

    public virtual void Explode(Collider2D other)
    {
        GameObject explosion = Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(explosion.gameObject, 1);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

    #endregion
}
