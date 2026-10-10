using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_4 : Enemy
{
    [Header("Enemy_4 Inscribed Fields")]
    [Tooltip("Time between direction changes")]
    public float changeDirectionTime = 1.5f;
    [Tooltip("How much the enemy moves left/right")]
    public float horizontalSpeed = 5f;
    
    private float nextDirectionChange;
    private int direction = 1; // 1 for right, -1 for left
    
    void Start() {
        nextDirectionChange = Time.time + changeDirectionTime;
        direction = Random.value > 0.5f ? 1 : -1;
    }
    
    // Zigzag while falling. Does not call base.Move().
    public override void Move() {
        if (Time.time > nextDirectionChange) {
            direction *= -1;
            nextDirectionChange = Time.time + changeDirectionTime;
        }
        
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        tempPos.x += direction * horizontalSpeed * Time.deltaTime;
        pos = tempPos;
    }
}
