using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegWalk : MonoBehaviour
{
    [SerializeField] float stepHeight = .25f;
    [SerializeField] float stepSpeedMult = 2.0f;
    [SerializeField] float walkTimeOffset = 3.14f;
    float defaultHeight;
    float time = 0;
    float speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultHeight = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        time += speed * Time.deltaTime * stepSpeedMult;
        if (speed > .25f)
        {
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                Mathf.Lerp(defaultHeight, defaultHeight + stepHeight, (Mathf.Sin(time+walkTimeOffset)+1)*.5f), 
                transform.localPosition.z);
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, defaultHeight, transform.localPosition.z);
            time = 0;
        }
    }

    public void Walk(float speed)
    {
        this.speed = speed;
    }
}
