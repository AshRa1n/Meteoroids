using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderTeleport : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.tag == "Top")
        {
            other.transform.position = new Vector2(other.transform.position.x*-1,-4.5f);
        }
        if (gameObject.tag == "Bottom")
        {
            other.transform.position = new Vector2(other.transform.position.x*-1,4.5f);
        }
        if (gameObject.tag == "Left")
        {
            other.transform.position = new Vector2(8.5f, other.transform.position.y*-1);
        }
        if (gameObject.tag == "Right")
        {
            other.transform.position = new Vector2(-8.5f, other.transform.position.y*-1);
        }
    }
}
