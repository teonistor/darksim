// MoveTo.cs
using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNew : MonoBehaviour {
    static readonly float epsilon = 0.01f;

    private Transform goal;
    private NavMeshAgent agent;
    private Vector3 initialPos;
    public bool chasing { get; private set; }

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        initialPos = transform.position;
        chasing = false;
    }

    public void Setup (Transform goal) {
        this.goal = goal;
    }

    void FixedUpdate () {
        Vector3 delta = goal.position - transform.position;
        Debug.DrawRay(transform.position, delta, Color.white);

        // Nothing between enemy and player => chase
        if (!Physics.Raycast(transform.position,
                             delta,
                             Vector3.Magnitude(delta),
                             Physics.DefaultRaycastLayers,
                             QueryTriggerInteraction.Ignore)) {
            agent.destination = goal.position;
            if (!chasing) {
                chasing = true;
                Ambiance.attackCount++;
            }

        // Last target reached and player not visible => give up
        } else if (Vector3.SqrMagnitude(agent.destination - transform.position) < epsilon) {
            agent.destination = WorldGenerator.randomLoc;
            if (chasing) {
                chasing = false;
                Ambiance.attackCount--;
            }
        }

    }

    // Disused
    private IEnumerator updateDest () {
        WaitForSeconds w = new WaitForSeconds(0.1f);
        while (true) {
            yield return w;
            agent.destination = goal.position;
        }
    }
}