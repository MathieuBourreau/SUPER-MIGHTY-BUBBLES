using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float impulseForce = 50f;
    [SerializeField] private float damages = 1f;


    private void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.CompareTag("Player"))
        {
            var distance = Vector2.Distance(coll.transform.position, transform.position);

            var direction = (coll.transform.position - transform.position).normalized;
            Rigidbody2D rb = coll.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            { 
                rb.AddForceAtPosition(direction * impulseForce, transform.position, ForceMode2D.Impulse);
            }

            coll.gameObject.SendMessage("TakeDamage", damages, SendMessageOptions.DontRequireReceiver);
        }

    }
}
