using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool open = false;
    bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (open && !done)
        {
            transform.position += Vector3.down * Time.deltaTime;
            if (transform.position.y < -1) done = true;
        }
    }

    public void OpenDoor()
    {
        open = true;
    }

    public void ResetDoor()
    {
        transform.position = new Vector3(transform.position.x, .5f, transform.position.z);
        open = false;
        done = false;
    }
}
