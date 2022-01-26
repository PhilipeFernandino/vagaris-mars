using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour {

    public NavMeshAgent navMeshAgent;
    public Transform movePositionTransform;

    void Update() {
        navMeshAgent.destination = movePositionTransform.position;
    }
}
