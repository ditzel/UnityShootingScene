using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Enemy EnemyGoal;

    public bool Occupied { get { return EnemyGoal != null && EnemyGoal.target == this; } }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}
