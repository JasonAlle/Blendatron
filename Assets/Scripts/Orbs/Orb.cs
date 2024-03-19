using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField]
    private OrbData data;
    public OrbData Data { get { return data; } set { data = value; } }
    [SerializeField]
    private CircleCollider2D collide;
    [SerializeField]
    private Rigidbody2D body;
    [SerializeField]
    private SpriteRenderer spriteRend;
    [SerializeField]
    private OrbExplosion explosion;
   // [SerializeField]
    //private CircleCollider2D triggerCollider;

    public event System.Action<Vector2, OrbTiers> EvolutionEvent;
    private void OnEnable()
    {
        if (data != null)
        {
        spriteRend.sprite = data.Tex;
        InitBounds();
        SetConstraints(RigidbodyConstraints2D.None, true);
        collide.enabled = false;
        }
    }
    public void SetData(OrbData orbData)
    {
        data = orbData;
        spriteRend.sprite = data.Tex;
        InitBounds();
        SetConstraints(RigidbodyConstraints2D.None, true);
        collide.enabled = false;
    }
    public void SetConstraints(RigidbodyConstraints2D constraint, bool isInit = false)
    {
        if (isInit)
        {
            body.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            body.isKinematic = true;
            return;
        }
        body.isKinematic = false;
        body.constraints = constraint;
    }
    private void InitBounds()
    {
        transform.localScale = new Vector3(data.RenderScale, data.RenderScale, 1.0f);
        collide.radius = data.CollideRad;

       // triggerCollider.radius = data.CollideRad;
    }
    public void DropTheFucker()
    {
        SetConstraints(RigidbodyConstraints2D.None);
        collide.enabled = true;
    }

    public void HasEvolved()
    {
        DropTheFucker();
        explosion.IsEvolved();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Orb"))
        {
            return;
        }
        else
        {
            Orb orbcollision = collision.gameObject.GetComponentInChildren<Orb>();
            if (orbcollision.Data.Tier != data.Tier || data.Tier >= OrbTiers.tier8)
            {
                return;
            }
            else
            {
                
                EvolutionHandler.Evolve(collision.GetContact(0).point, data.Tier);
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}
