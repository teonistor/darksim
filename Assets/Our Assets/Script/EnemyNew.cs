// MoveTo.cs
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNew : MonoBehaviour {

    private Transform goal;
    private NavMeshAgent agent;

    void Start () {
        //goal = new
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(updateDest());
        //agent.destination = new Vector3(20, 0, 20);
    }

    public void Setup (Transform goal) {
        this.goal = goal;
        // TODO
    }

    private IEnumerator updateDest () {
        WaitForSeconds w = new WaitForSeconds(0.1f);
        while (true) {
            yield return w;
            agent.destination = goal.position;
        }
    }
}