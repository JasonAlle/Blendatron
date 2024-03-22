using System;
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
    public CircleCollider2D OrbCollider { get { return collide; } }
    [SerializeField]
    private Rigidbody2D body;
    public Rigidbody2D OrbBody { get { return body; } }
    [SerializeField]
    private SpriteRenderer spriteRend;
    [SerializeField]
    private OrbExplosion explosion;
    private bool isHighTier;
    private int orbID;
    private bool isblended = false;
    public bool IsBlended { get { return isblended; } set { isblended = value; } }
    public int OrbID { get { return orbID; } set { orbID = value; } }
    public bool IsHighTier { get { return isHighTier; } set { isHighTier = value; } }
    private bool isSpawnedEvolved = false;
    public bool IsSpawnedEvolved { get { return isSpawnedEvolved; } set { isSpawnedEvolved = value; } }
    private Vector3 scaleIncrement = new Vector3(0.1f, 0.1f, 0.0f);
    [SerializeField]
    private float speedOfExpansion;
    private float affectorAmount;
    public float AffectorAmount { get { return affectorAmount; } set { affectorAmount = value; } }
    private bool isFinishedExpanding = false;
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
        body.mass = data.OrbWeight;
        collide.enabled = false;
        if (data.Tier >= OrbTiers.tier6)
        {
            isHighTier = true;
        }
        SetAffectorAmount();
    }

    private void SetAffectorAmount()
    {
        //switch (data.Tier)
        //{
        //    case OrbTiers.tier1:
        //        affectorAmount = data.OrbWeight - 
        //        break;
        //    case OrbTiers.tier2:
        //        break;
        //    case OrbTiers.tier3:
        //        break;
        //    case OrbTiers.tier4:
        //        break;
        //    case OrbTiers.tier5:
        //        break;
        //    case OrbTiers.tier6:
        //        break;
        //    case OrbTiers.tier7:
        //        break;
        //    case OrbTiers.tier8:
        //        break;
        //    default:
        //        break;
        //}
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
    }
    public void PrepareForBlend()
    {
        this.tag = "HighOrb";
        this.gameObject.layer = LayerMask.NameToLayer("HighOrb");
    }
    public void PrepareForGame()
    {
        this.tag = "Orb";
        this.gameObject.layer = LayerMask.NameToLayer("Orb");
    }
    public void DropTheFucker()
    {
        OrbManager.AddOrb(this);
        SetConstraints(RigidbodyConstraints2D.None);
        collide.enabled = true;
    }
    public void HasEvolved()
    {
        isSpawnedEvolved = true;
        this.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.0f);
        DropTheFucker();
        explosion.IsEvolved();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isFinishedExpanding)
            return;
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
                OrbManager.RemoveOrb(this);
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
    private void FixedUpdate()
    {
        if (isFinishedExpanding == false)
        {
            if (isSpawnedEvolved && this.gameObject.transform.localScale.x < data.RenderScale)
            {
                this.gameObject.transform.localScale += scaleIncrement * speedOfExpansion * Time.fixedDeltaTime;
            }
            if (this.gameObject.transform.localScale.x >= data.RenderScale)
            {
                isFinishedExpanding = true;
                InitBounds();
            }
        }
    }
}
