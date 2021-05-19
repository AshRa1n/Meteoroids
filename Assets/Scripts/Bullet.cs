using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;

    private bool _teleported=false;

    void FixedUpdate()
    {
        transform.position += transform.up * _bulletSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Top" || other.tag == "Bottom" || other.tag == "Left" || other.tag == "Right")
            if (_teleported==false) 
                _teleported = true;
            else
                Destroy(gameObject);
    }
}
