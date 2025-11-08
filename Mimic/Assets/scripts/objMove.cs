using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objMove : MonoBehaviour
{
    private Rigidbody _rb;

    public bool movin;
    public bool rotatin;
    public bool inspected;
    public bool isMimic;
    public bool waitOver;
    public float speed;
    public float rotateSpeed;
    public float waitTime;

    public GameObject mimic;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        _rb = GetComponent<Rigidbody>();
        movin = true;
        rotatin = false;
        isMimic = false;
        if (speed == 0)
        {
            speed = .0005f;
        }
        if (rotateSpeed == 0)
        {
            rotateSpeed = .1f;
        }
        if (waitTime == 0)
        {
            waitTime = 15f;
        }
        mimic = GameObject.FindGameObjectWithTag("mimic");
    }

    // Update is called once per frame
    void Update()
    {
        if (movin)
        {
            _rb.transform.position += new Vector3(0, 0, 1f) * speed;
        }
        else
        {
            rotatin = true;
        }

        if (rotatin)
        {
            _rb.transform.Rotate(0, rotateSpeed, 0);
        }

        if (Input.GetKeyUp(KeyCode.Space) && movin && !inspected)
        {
            StartCoroutine(Inspect());
            StartCoroutine(Waiting());
        }

        if (Input.GetKeyUp(KeyCode.Space) && rotatin)
        {
            isMimic = true;
        }

        if (isMimic)
        {
            Destroy(mimic);
        }
    }

    private IEnumerator Inspect()
    {
        movin = false;
        yield return new WaitForSeconds(.5f);
        rotatin = true;
        yield return new WaitWhile(() => !isMimic && !waitOver);
        rotatin = false;
        _rb.transform.rotation = Quaternion.Euler(0, 0, 0);
        movin = true;
        inspected = true;
    }

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(waitTime);
        waitOver = true;
    }
}
