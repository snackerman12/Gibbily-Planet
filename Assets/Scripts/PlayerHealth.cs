using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    private bool isDead = false;

    private void Start()
    {
        // Make sure all Rigidbodies are kinematic by default
        EnableRagdoll(false);
    }

    public void TakeDamage(float damage, Vector3 force)
    {
        if (isDead) return;

        health -= damage;
        if (health <= 0)
        {
            Die(force);
        }
    }

    private void Die(Vector3 force)
    {
        isDead = true;
        EnableRagdoll(true);
        
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.AddForce(force, ForceMode.Impulse);
        }

        Debug.Log("Player has been killed!");
    }

    private void EnableRagdoll(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = !state;
        }

        Animator animator = GetComponent<Animator>();
        if (animator) animator.enabled = !state; // Disable animations when ragdoll is on
    }
}
