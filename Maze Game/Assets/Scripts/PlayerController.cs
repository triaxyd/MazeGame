using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;
    private GameManager gameManager;
    private PlayerInventory inventory;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gameManager = FindAnyObjectByType<GameManager>();
        inventory = FindAnyObjectByType<PlayerInventory>();
    }

    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            Debug.Log("In trigger enter");
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("In die method");
        if (isDead) return;

        isDead = true;
        animator.SetTrigger("PlayerDie"); // Trigger the death animation

        gameManager.PlayerDied();
    }

}
