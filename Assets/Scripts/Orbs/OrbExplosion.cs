using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbExplosion : MonoBehaviour
{
    [SerializeField]
    private GameObject explosionEffect;
    [SerializeField]
    private float expForce;
    [SerializeField]
    private float expRadius;

    public void IsEvolved()
    {
        Explode();
    }

    private void Explode()
    {
        LayerMask orbsLayer = LayerMask.GetMask("Orb");
        Vector3 pos = this.gameObject.transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, expRadius);
        Debug.Log("Checking for colliders!");
        foreach (Collider2D hit in colliders)
        {
            if (hit.gameObject == this.gameObject.transform.parent.gameObject)
                continue;
            Debug.Log("Collider Hit!");
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                GameObject explosion = Instantiate(explosionEffect, this.gameObject.transform);
                Debug.Log("Collided with Orb!");
                rb.AddExplosionForce(expForce, pos, expRadius);
            }
        }
    }
}
