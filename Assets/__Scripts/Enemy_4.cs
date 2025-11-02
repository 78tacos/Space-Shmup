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
        // Schedule first direction change
        nextDirectionChange = Time.time + changeDirectionTime;
        // Randomize initial direction
        direction = Random.value > 0.5f ? 1 : -1;
    }
    
    // Override the Move function to add zigzag movement
    public override void Move() {
        // Check if it's time to change direction
        if (Time.time > nextDirectionChange) {
            direction *= -1; // Reverse direction
            nextDirectionChange = Time.time + changeDirectionTime;
        }
        
        // Move horizontally while also moving down
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        tempPos.x += direction * horizontalSpeed * Time.deltaTime;
        pos = tempPos;
    }
}
