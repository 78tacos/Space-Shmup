using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(BoundsCheck) )]
public class ProjectileEnemy : MonoBehaviour
{
    private BoundsCheck bndCheck;
    public float damage = 1f;

    void Awake() {
        bndCheck = GetComponent<BoundsCheck>();
    }

    void Update() {
        // Destroy projectile if it goes off the bottom of the screen
        if( bndCheck.LocIs(BoundsCheck.eScreenLocs.offDown) ) {
            Destroy( gameObject );
        }
    }
}
