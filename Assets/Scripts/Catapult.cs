using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour {
    public float catapultStrength = 100f;

    private void OnCollisionEnter(Collision other) {
        Rigidbody otherRb = other.gameObject.GetComponent<Rigidbody>();
        if (otherRb != null) {
            otherRb.AddForce(transform.up * catapultStrength, ForceMode.VelocityChange);
        }
    }
}
