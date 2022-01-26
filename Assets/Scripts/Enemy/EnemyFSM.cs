using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour, IAudible {
    
    public EnemyState currentState;

    void Awake() {
        currentState.gameObject.SetActive(true);
    }

    public void Hear(GameObject player) {
        currentState.Hear(player);
    }
    
    public void SwitchState(EnemyState nextState) {
        currentState.gameObject.SetActive(false);
        nextState.gameObject.SetActive(true);
        currentState = nextState;
    }
}
