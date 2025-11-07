using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objMove : MonoBehaviour
{
    private Rigidbody _rb;
    public float moveSpeed = 5f;
    private Vector3 _movementVector;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _movementVector = new Vector3(0, 0, 1).normalized * moveSpeed;
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector3(_movementVector.x, _rb.velocity.y, _movementVector.z);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
