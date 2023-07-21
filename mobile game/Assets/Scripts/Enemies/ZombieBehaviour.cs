using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehaviour : MonoBehaviour
{
    public int Damage;
    [SerializeField] int chaseRange = 5;
    [SerializeField]float distanceToTarget = Mathf.Infinity;

    Transform Target;
    public Animator animator;
    public NavMeshAgent agent;

    public bool Triggered = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Target = FindObjectOfType<WeaponPickup>().gameObject.transform;
    }

    private void Update()
    {
        distanceToTarget = Vector3.Distance(Target.position, transform.position);

        if (distanceToTarget <= chaseRange)
        {
            Triggered = true;
        }
        
        if (Triggered)
        {
            Debug.Log("triggered 222");
            EngageTarget();
        }

    }

    void EngageTarget()
    {
        distanceToTarget = Vector3.Distance(Target.position, transform.position);
        if (distanceToTarget >= agent.stoppingDistance)
        {
            transform.LookAt(Target.transform.position);
            agent.SetDestination(Target.transform.position);
            animator.SetTrigger("Walk");
        }
        else if (distanceToTarget <= agent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    void AttackTarget()
    {
        transform.LookAt(Target.transform.position);
        animator.SetTrigger("Attack");
    }

    public void DoDamage()
    {
        distanceToTarget = Vector3.Distance(Target.position, transform.position);
        if (distanceToTarget <= agent.stoppingDistance)
        {
            Target.GetComponent<PlayerBehaviour>().PlayerTakeDamage(Damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
