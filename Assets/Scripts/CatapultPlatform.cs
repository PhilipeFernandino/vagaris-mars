using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultPlatform : MonoBehaviour {
    
    public float goingSpeed, returningSpeed;
    public Transform finalPosition; 
    public float acceptableDistance = 1f;

    private Vector3 startPosition;
    private Rigidbody rb;
    private bool isGoing = true;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    private void FixedUpdate() {
        if (isGoing) {
            rb.MovePosition(transform.position + (finalPosition.position - transform.position).normalized * goingSpeed * Time.deltaTime);
            if (Vector3.Distance(rb.transform.position, finalPosition.position) < acceptableDistance) isGoing = false;
        } else {
            rb.MovePosition(transform.position + (startPosition - transform.position).normalized * returningSpeed * Time.deltaTime);
            if (Vector3.Distance(rb.transform.position, startPosition) < acceptableDistance) isGoing = true;
        }
    }

    private void OnCollisionStay(Collision other) {
        other.transform.parent = transform;
    }

    private void OnCollisionExit(Collision other) {
        other.transform.parent = null;
    }
}
