using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public float Speed;
    private Vector3 _velocity;
    void Start()
    {
        _velocity = Vector3.down;
        _velocity = new Vector3(0, -1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _velocity * Speed * Time.deltaTime;
    }
}
