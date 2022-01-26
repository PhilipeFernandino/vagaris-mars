using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour, IAudible {
    
    public virtual void Hear(GameObject player) { return; }
}
