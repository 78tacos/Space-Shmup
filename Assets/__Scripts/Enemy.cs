using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(BoundsCheck) )]
public class Enemy : MonoBehaviour
{
    [Header("Inscribed")]
    public float speed      = 10f;      // Meters per second
    public float fireRate   = 0.3f;     // Seconds/shot
    public float health     = 10;       // Damage needed to destroy this enemy
    public int   score      = 100;      // Points earned for destroying this
    public GameObject projectilePrefab;
    public float projectileSpeed = 20f;
    public bool canShoot = false;

    protected BoundsCheck bndCheck;
    private float lastFireTime = 0f;
    
    void Awake() {
        bndCheck = GetComponent<BoundsCheck>();
    }
    
    void Start() {
        // Randomize first shot time to avoid all enemies shooting in sync
        lastFireTime = Time.time + Random.Range(0f, fireRate);
    }

    public Vector3 pos {
        get{
            return this.transform.position;
        }
        set {
            this.transform.position = value;
        }
    }
    void Update() {
        Move();
        
        if (canShoot && projectilePrefab != null && bndCheck.isOnScreen) {
            if (Time.time - lastFireTime > fireRate) {
                Fire();
                lastFireTime = Time.time;
            }
        }
        
        if ( bndCheck.LocIs( BoundsCheck.eScreenLocs.offDown ) ) {
            Destroy( gameObject );
        }
    }
    
    public virtual void Fire() {
        GameObject projGO = Instantiate<GameObject>( projectilePrefab );
        projGO.transform.position = transform.position;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        
        // Fire toward the hero
        Vector3 direction = Vector3.down; // Default downward
        if (Hero.S != null) {
            direction = (Hero.S.transform.position - transform.position).normalized;
        }
        rigidB.velocity = direction * projectileSpeed;
    }

    public virtual void Move() {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    void OnCollisionEnter( Collision coll ) {
        GameObject otherGO = coll.gameObject;
        if ( otherGO.GetComponent<ProjectileHero>() != null ) {
            Destroy( otherGO );
            Destroy( gameObject );
            Main.ENEMY_DIED(score);
        } else {
            Debug.Log( "Enemy hit by non-ProjectileHero: " + otherGO.name );
        }
    }
}
