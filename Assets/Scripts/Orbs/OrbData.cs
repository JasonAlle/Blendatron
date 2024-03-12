using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OrbTiers
{
    tier1,
    tier2,
    tier3,
    tier4,
    tier5,
    tier6,
    tier7,
}


[CreateAssetMenu(menuName = "Orb")]
public class OrbData : ScriptableObject
{
    [SerializeField]
    private OrbTiers tier;
    [SerializeField]
    private Sprite sprite;
    private Rigidbody2D rigidbody;
    private CircleCollider2D collider;

    private void OnEnable()
    {
        if (collider == null)
        {
            collider = new CircleCollider2D();
        }
        if (rigidbody == null)
        {
            rigidbody = new Rigidbody2D();
            SetConstraints();
            SetSize();
        }
    } 
    private void SetConstraints()
    {
        if (rigidbody == null)
        {
            rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
            rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    private void SetSize()
    {
        switch (tier)
        {
            case OrbTiers.tier1:
                //collider.radius = 1.0f;
                //sprite.rect.size()
                break;
            case OrbTiers.tier2:
               // collider.radius = 2.0f;
                break;
            case OrbTiers.tier3:
               // collider.radius = 3.0f;
                break;
            case OrbTiers.tier4:
               // collider.radius = 4.0f;
                break;
            case OrbTiers.tier5:
              //  collider.radius = 5.0f;
                break;
            case OrbTiers.tier6:
               // collider.radius = 5.0f;
                break;
            case OrbTiers.tier7:
                //collider.radius = 6.0f;
                break;
            default:
              //  collider.radius = 1.0f;
                break;
        }
    }
}
