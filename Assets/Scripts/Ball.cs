using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float playerCollisionImpulseAdditionMultiplier = 1.0f;
    [SerializeField] LayerMask raycastLayerMask;
    [SerializeField] GameObject groundPositionVisuals;
    Rigidbody rb;
    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(new Ray(transform.position, Vector3.down), out RaycastHit hit, 200, raycastLayerMask))
        {
            groundPositionVisuals.transform.position = hit.point;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
        if (collision.gameObject.tag == "Player")
        {
            rb.AddForce(-collision.impulse.normalized * playerCollisionImpulseAdditionMultiplier);
        }
    }
}
