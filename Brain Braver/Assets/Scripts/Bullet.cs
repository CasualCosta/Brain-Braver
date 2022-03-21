using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public float startingForce, lifeTime, damage;
    [SerializeField] Rigidbody2D rb = null;

    bool firedFromOrbot = false;
    private void Start()
    {
        Destroy(gameObject, lifeTime);
        rb.AddRelativeForce(Vector2.up * startingForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            if (collision.CompareTag("Player") && firedFromOrbot)
                return;
            damageable.ChangeHealth(-damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Wall"))
            Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.ChangeHealth(-damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
            Destroy(gameObject);
    }

    public void AttributeBulletValues(GunSO gunSO)
    {
        damage = gunSO.damage;
        lifeTime = gunSO.bulletLifeTime;
        startingForce = gunSO.bulletForce;
    }

    public void AttributeBulletValues(GunSO gunSO, bool orbot)
    {
        damage = orbot ? gunSO.damage / 2 : gunSO.damage;
        lifeTime = gunSO.bulletLifeTime;
        startingForce = gunSO.bulletForce;
        firedFromOrbot = orbot;
    }
}
