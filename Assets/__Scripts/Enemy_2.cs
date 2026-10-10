using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    [Header("Enemy_2 Inscribed Fields")]
    public float            lifeTime = 10;
    // Enemy_2 uses a Sine wave to modify a 2-point linear interpolation
    [Tooltip("Determines how much the Sine wave will ease the Interpolation")]
    public float            sinEccentricity = 0.6f;
    public AnimationCurve   rotCurve;

    [Header("Enemy_2 Private Fields")]
    [SerializeField] private float  birthTime; // Interpolation start time
    [SerializeField] private Vector3 p0, p1;   // Lerp_points

    private Quaternion  baseRotation;

    void Start () {
        // Pick any point on the left side of the screen
        p0 = Vector3.zero;
        p0.x = -bndCheck.camWidth - bndCheck.radius;
        p0.y = Random.Range( -bndCheck.camHeight, bndCheck.camHeight );

        // Possibly swap sides
        if(Random.value > 0.5f) {
            // Negating x moves the point to the other side of the screen
            p0.x *= -1;
            p1.x *= -1;
        }

        birthTime = Time.time;

        // Face p1. Vector3.back keeps the sprite upright.
        transform.position = p0;
        transform.LookAt(p1, Vector3.back);
        baseRotation = transform.rotation;
    }

    public override void Move() {
        // u runs from 0 to 1 across lifeTime
        float u = (Time.time - birthTime) / lifeTime;

        if (u > 1) {
            Destroy( this.gameObject );
            return;
        }

        // Roll along rotCurve over the flight
        float shipRot = rotCurve.Evaluate( u ) * 360;
        transform.rotation = baseRotation * Quaternion.Euler(-shipRot, 0, 0);

        // Ease the lerp so speed is not constant
        u = u + sinEccentricity*( Mathf.Sin(u*Mathf.PI*2) );

        pos = (1 - u) * p0 + u*p1;

        // Note that Enemy_2 does NOT call the base.Move() method
    }
}
