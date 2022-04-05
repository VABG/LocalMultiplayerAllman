using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractTrigger : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            Vector3 direction = (transform.forward + transform.up * .3f).normalized;
            other.attachedRigidbody.AddForce(direction * 1000);
        }
        audioSource.Play();

        if (other.gameObject.tag == "Player")
        {
            Player p = other.gameObject.GetComponent<Player>();
            if (p != null)
            {
                p.TakeDamage(10);
            }
        }

        // Deactivate trigger
        this.gameObject.SetActive(false);
    }
}
