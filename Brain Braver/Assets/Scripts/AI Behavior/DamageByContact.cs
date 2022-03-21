using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageByContact : MonoBehaviour
{
    [SerializeField] Collider2D col = null;
    [SerializeField] float contactDamage = 1f;
    [SerializeField] float continuousDamage = 1f;
    [SerializeField] bool destroyOnContact = false;
    [SerializeField] EnemyHealth enemyHealth = null;

    bool isTouchingPlayer = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!col.isTrigger)
            return;
        if (collision.CompareTag("Player"))
        {
            CollideWithPlayer();
            if (destroyOnContact)
            {
                if (enemyHealth)
                    enemyHealth.Die();
                else
                    Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!col.isTrigger)
            return;
        if (collision.CompareTag("Player"))
            isTouchingPlayer = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (col.isTrigger)
            return;
        if (collision.gameObject.CompareTag("Player"))
        {
            CollideWithPlayer();
            if (destroyOnContact)
            {
                if (enemyHealth)
                    enemyHealth.Die();
                else
                    Destroy(gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (col.isTrigger)
            return;
        if (collision.gameObject.CompareTag("Player"))
            isTouchingPlayer = false;
    }

    void CollideWithPlayer()
    {
        isTouchingPlayer = true;
        Player.Instance.ChangeHealth(-contactDamage);
    }
    // Update is called once per frame
    void Update()
    {
        if (isTouchingPlayer)
            Player.Instance.ChangeHealth(-continuousDamage * Time.deltaTime);
    }
}
