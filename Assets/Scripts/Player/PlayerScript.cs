using Unity.VisualScripting;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float PlayerHealth;
    public bool grounded = false;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
    {
        GetComponent<Rigidbody>().AddForce(transform.up * 3, ForceMode.Impulse);
    }
    }
    void Attack()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
}