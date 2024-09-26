using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour
{
    private Vector2[] positions;
    private Vector2 startPos;
    private Vector2 targetPos;

    private float dist = 1f;
    private int currentInd = 0;
    private float t = 0f;

    private Animator animator; // Reference to the Animator component

    // Start is called before the first frame update
    void Start()
    {
        positions = new Vector2[]{
            new Vector2(0, 0),
            new Vector2(-5, 0),
            new Vector2(-5, 4),
            new Vector2(0, 4)
        };
        startPos = positions[0];
        targetPos = positions[currentInd];

        animator = GetComponent<Animator>(); // Assign the Animator component
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime / dist;
        transform.position = Vector2.Lerp(startPos, targetPos, t);

        if (t >= 1f)
        {
            t = 0f;
            startPos = transform.position;
            currentInd = (currentInd + 1) % positions.Length;
            targetPos = positions[currentInd];

            // Determine the movement direction
            Vector2 moveDirection = targetPos - startPos;

            // Set the Animator parameters based on direction
            if (moveDirection.x != 0)
            {
                // Moving horizontally
                animator.SetFloat("Horizontal", moveDirection.x);
                animator.SetFloat("Vertical", 0f);  // Reset vertical axis
            }
            else if (moveDirection.y != 0)
            {
                // Moving vertically
                animator.SetFloat("Vertical", moveDirection.y);
                animator.SetFloat("Horizontal", 0f); // Reset horizontal axis
            }
        }
    }
}

