using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState {

    public EnemyFSM stateMachine;
    public EnemyState chaseState;
    public bool canSeeThePlayer;

    void Update() {
    }

    public override void Hear(GameObject player) {
        Debug.Log("I heard something");
        stateMachine.SwitchState(chaseState);
    }
}