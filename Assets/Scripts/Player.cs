using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    // Input
    [SerializeField] KeyCode keyLeft;
    [SerializeField] KeyCode keyRight;
    [SerializeField] KeyCode keyUp;
    [SerializeField] KeyCode keyDown;
    [SerializeField] KeyCode keyAction;

    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float moveLerpSpeed = 20;
    [SerializeField] float health;

    // Leg animators
    [SerializeField] LegWalk leg1;
    [SerializeField] LegWalk leg2;

    [SerializeField] LayerMask raycastLayerMask;
    [SerializeField] float raycastLength = 1.1f;

    [SerializeField] GameObject interactTrigger;


    Rigidbody rb;
    bool onGround = false;
    
    // Holds input info for update in FixedUpdate
    Vector3 lastInput;

    Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        lastInput = Vector3.zero;

        if (Input.GetKey(keyLeft)) lastInput += Vector3.left;
        if (Input.GetKey(keyRight)) lastInput += Vector3.right;
        if (Input.GetKey(keyUp)) lastInput += Vector3.forward;
        if (Input.GetKey(keyDown)) lastInput += Vector3.back;
        lastInput.Normalize();

        // If any input, change target rotation!
        if (lastInput.magnitude > 0) targetRotation = Quaternion.LookRotation(lastInput, Vector3.up);

        // Activate and deactivate interaction trigger GameObject with key press/release
        if (Input.GetKeyDown(keyAction)) interactTrigger.SetActive(true);
        if (Input.GetKeyUp(keyAction)) interactTrigger.SetActive(false);

        // Interpolate rotation for prettier visuals
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveLerpSpeed);
    }

    private void FixedUpdate()
    {
        // Groundcheck
        if (Physics.Raycast(new Ray(transform.position, Vector3.down), out RaycastHit hit, raycastLength, raycastLayerMask))
        {
            onGround = true;
            lastInput.y = rb.velocity.y;
            // Lerp velocity between current rigidbody velocity and input target velocity
            rb.velocity = Vector3.Lerp(rb.velocity, lastInput * moveSpeed, Time.deltaTime * moveLerpSpeed);

            // Make sure player is on ground (fixes issue with player bouncing)
            transform.position = new Vector3(transform.position.x, transform.position.y + (1 - hit.distance), transform.position.z);
            // And also set y-speed to zero just in case!
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            
            //Animate legs!
            leg1.Walk(rb.velocity.magnitude);
            leg2.Walk(rb.velocity.magnitude);
        }
        else
        {
            onGround = false;
            // Don't animate legs!
            leg1.Walk(0);
            leg2.Walk(0);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }
    
    void Die()
    {
        // Disables Player Script
        enabled = false;
    }
}
