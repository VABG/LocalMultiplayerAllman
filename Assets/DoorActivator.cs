using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorActivator : MonoBehaviour
{
    int doorSignalCounter = 0;
    Door[] doors;
    // Start is called before the first frame update
    void Start()
    {
        doors = FindObjectsOfType<Door>();
    }

    // Update is called once per frame
    void Update()
    {
        if (doorSignalCounter == 2)
        {
            //Open doors
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].OpenDoor();
            }
        }
    }

    public void SendDoorOpenSignal()
    {
        doorSignalCounter++;
    }

    public void UnSendDoorOpenSignal()
    {
        doorSignalCounter--;
    }

    public void Reset()
    {
       // doorSignalCounter = 0;
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].ResetDoor();
        }
    }
}
