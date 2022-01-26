using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ChaseState : EnemyState {

    public EnemyFSM stateMachine;
    public NavMeshAgent navMeshAgent;
    public EnemyState attackState; 
    public bool isPlayerInRange;
    public Transform targetTransform;

    public void Update() {
        if (targetTransform != null) navMeshAgent.destination = targetTransform.position;
        if (isPlayerInRange) stateMachine.SwitchState(attackState);
    }

    public override void Hear(GameObject player) {
        targetTransform = player.transform;
    }
}