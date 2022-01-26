using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseSimulator : MonoBehaviour {
    public LayerMask hearMask;
    public bool drawNoiseSphere;

    float radius;

    public void MakeNoise(GameObject noiseSource, float r) {
        radius = r;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, hearMask, QueryTriggerInteraction.Collide);
        foreach (var hitCollider in hitColliders) {
            Debug.Log("Alguem me ouviu");
            IAudible audible = hitCollider.gameObject.GetComponent<IAudible>();
            if (audible != null) {
                audible.Hear(noiseSource);
            }
        }
    }

    void OnDrawGizmos() {
        if (drawNoiseSphere) Gizmos.DrawWireSphere(transform.position, radius);
    }

}
