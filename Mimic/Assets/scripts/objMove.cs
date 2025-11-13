using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class objMove : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private GameObject _obj;

    public bool movin;
    public bool rotatin;
    public bool inspected;
    public bool isMimic;
    public bool waitOver;
    public bool needNewObj;
    public float speed;
    public float rotateSpeed;
    public float waitTime;
    public int score;
    private int _jankCount;

    private int _rInt;
    private string _rString;

    public Vector3 _prevPos;

    public GameObject fire;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        score = 0;
        fire.transform.position = new Vector3(2.52f, 10.28f, .57f);
        needNewObj = true;
        movin = true;
        rotatin = false;
        isMimic = false;
        if (speed == 0)
        {
            speed = .009f;
        }
        if (rotateSpeed == 0)
        {
            rotateSpeed = .5f;
        }
        if (waitTime == 0)
        {
            waitTime = 10f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (needNewObj)
        {
            //Also need a way to call a new object to come in after an object disappears- whether mimic or not.
            Random r = new Random();
            _rInt = r.Next(1, 15);
            _rString = _rInt.ToString();
            _obj = GameObject.Find("Object"+_rString);
            _rb = _obj.GetComponent<Rigidbody>();
            //Also to teleport the objects to the right spot
            _rb.transform.position = new Vector3(0, 0.222f, -6.5f);
            needNewObj = false;
            inspected = false;
        }
        
        if (movin)
        {
            _rb.transform.position += new Vector3(0, 0, 1f) * speed;
        }

        if (rotatin && !isMimic)
        {
            _prevPos = _rb.transform.position;
            _rb.transform.position = new Vector3(2.13f, 1.64f, 0.53f);
            _rb.transform.Rotate(0, rotateSpeed, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && movin && !inspected)
        {
            movin = false;
            rotatin = true;
            // StartCoroutine(Inspect());
            // StartCoroutine(Waiting());
        }

        if (Input.GetKeyUp(KeyCode.Space) && rotatin)
        {
            isMimic = true;
        }

        if (isMimic)
        {
            // StartCoroutine(Fire());
            _rb.transform.position = new Vector3(5, 5, -35);
            isMimic = false;
            rotatin = false;
            inspected = true;
            needNewObj = true;
            score++;
            movin = true;
        }

        if (_rb.transform.position.z > 5.9f)
        {
            _rb.transform.position = new Vector3(5, 5, -35);
            needNewObj = true;
        }

        if (score >= 15)
        {
            SceneManager.LoadScene("END");
        }
    }

    private IEnumerator Inspect()
    {
        movin = false;
        yield return new WaitForSeconds(.5f);
        rotatin = true;
        yield return new WaitForSeconds(waitTime);
        rotatin = false;
        _rb.transform.rotation = Quaternion.Euler(0, 0, 0);
        _rb.transform.position = _prevPos;
        movin = true;
        inspected = true;
        score++;
    }

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(waitTime);
        waitOver = true;
    }

    private IEnumerator Fire()
    {
        fire.transform.position = new Vector3(2.52f, 5.87f, .57f);
        yield return new WaitForSeconds(2);
        
        fire.transform.position = new Vector3(2.52f, 10.28f, .57f);
        
    }
}
