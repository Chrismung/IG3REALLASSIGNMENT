using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour
{
    private Vector2[] positions;
    private Vector2 OriginPac;
    private Vector2 CornerDest;

    private float distance = 1f;
    private int Index = 0;
    private float t = 0f;


    private Animator animator;
    // Animator Component to use the movements

    void Update()
    {
        t += Time.deltaTime / distance;
        transform.position = Vector2.Lerp(OriginPac, CornerDest, t);

        if (t >= 1f)
        {
            t = 0f;
            OriginPac = transform.position;
            Index = (Index + 1) % positions.Length;
            CornerDest = positions[Index];


            Vector2 moveDirection = CornerDest - OriginPac;


            if (moveDirection.x != 0)
            //horizontal Movement
            {
                animator.SetFloat("Horizontal", moveDirection.x);
                animator.SetFloat("Vertical", 0f);//reset affected axis
            }
            else if (moveDirection.y != 0)
            //vertical movement
            {
                animator.SetFloat("Vertical", moveDirection.y);
                animator.SetFloat("Horizontal", 0f); //reset affected axis
            }
        }
    }
    void Start()
    {
        //add the corner position targets that pacman goes through
        positions = new Vector2[]{
            new Vector2(0, 0),
            new Vector2(-5, 0),
            new Vector2(-5, 4),
            new Vector2(0, 4)
        };

        OriginPac = positions[0];
        CornerDest = positions[Index];

        animator = GetComponent<Animator>(); //getting the animator's component
    }

    
    
}

