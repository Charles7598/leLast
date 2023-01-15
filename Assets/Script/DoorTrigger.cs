using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private Animator myDoor;

    [SerializeField] private string openDoor = "OpenDoor";
    
    public GameObject objectToFind;

    string theTag = "TheTag";

    public BoxCollider coll;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myDoor.Play(openDoor, 0, 0.0f);
            objectToFind = GameObject.FindGameObjectWithTag(theTag);
            objectToFind.GetComponent<BoxCollider>().enabled = false;
            Destroy(this); 
        }
    }
}
