using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState {

    public EnemyFSM stateMachine;
    public EnemyState chaseState;
    public bool isPlayerInRange;

    public void Update() {
        if (!isPlayerInRange) stateMachine.SwitchState(chaseState);
    }
}