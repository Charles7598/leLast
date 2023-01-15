using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMoves: MonoBehaviour
{   
    // Variables
    public float playerSpeed;
    CharacterController ch;

    private float lastXVal;
    private float lastZVal;

    void Start()
    {
        ch = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;
        ch.Move(new Vector3(x,0,z));

        if (transform.position.x < lastXVal)
        {
            transform.localEulerAngles = new Vector3(0, 90, 0);
            lastXVal = transform.position.x;
        }
        else if (transform.position.x > lastXVal)
        {
            transform.localEulerAngles = new Vector3(0, -90, 0);
            lastXVal = transform.position.x;
        }
        else if (transform.position.z < lastZVal)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            lastZVal = transform.position.z;
        }
        else if (transform.position.z > lastZVal)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            lastZVal = transform.position.z;
        }
    }
}