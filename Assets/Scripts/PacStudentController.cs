using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PacStudentController : MonoBehaviour
{
   
    private Vector2 startpos;
    private Vector2 targetpos;
    private float t = 0f;
    private float speed = 5f;
    private Animator anim;
    private bool isMoving = false; 
    private Vector2 lastInput; 
    private Vector2 currentInput;
    //Input a list of the quadrant walls to do the boundaries for the pacman in inspector
    public List<Tilemap> wallTilemaps;
    //Inspector insert audios
    public AudioClip movesound;
    public AudioClip pelletsound;
    private AudioSource audioSource;
    // Dust trail inspector field input
    public GameObject trail;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        startpos = transform.position;
        targetpos = startpos;
        lastInput = Vector2.zero;
        currentInput = Vector2.zero;
    }


    void Update()
    {
        // Update input every frame
        if (!isMoving)
        {
            PlayerInput();
            TryToMove();
        }

        if (isMoving)
        {
            PacMove();
        }
    }

    void PlayerInput()
    {
        // Capture input and set Direction accordingly
        if (Input.GetKey(KeyCode.W)) lastInput = Vector2.up;
        if (Input.GetKey(KeyCode.S)) lastInput = Vector2.down;
        if (Input.GetKey(KeyCode.A)) lastInput = Vector2.left;
        if (Input.GetKey(KeyCode.D)) lastInput = Vector2.right;
        //UpdateAnimationDirection when key input
        AnimationDirection();
    }

    void TryToMove()
    {
        Vector2 direction = lastInput.normalized;
        Vector2 nextPos = (Vector2)transform.position + direction;

        // Check for wall collisions using Raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f); 
        if (hit.collider == null)
        {
            if (isnotWall(nextPos))
            {
                currentInput = lastInput;
                StartMovement(direction);
                if (!trail.activeSelf)
                {
                    trail.SetActive(true);
                }
            }
        }
        else if (currentInput != Vector2.zero)
        {
            nextPos = (Vector2)transform.position + currentInput;
            if (isnotWall(nextPos))
            {  
                StartMovement(currentInput);
            }
        }
    }

    void StartMovement(Vector2 direction)
    {
        startpos = transform.position;
        targetpos = (Vector2)transform.position + direction;
        t = 0f;
        isMoving = true;
        // Play movement audio and activate trail
        PlayMoveAudio();
        AnimationDirection();
        ParticleDirection(direction);
    }

    void PacMove()
    {
        t += Time.deltaTime * speed / Vector2.Distance(startpos, targetpos);
        transform.position = Vector2.Lerp(startpos, targetpos, t);

        if (t >= 1f)
        {
            transform.position = targetpos;
            isMoving = false;
            CutMoveAudio();
        }
    }

    void CutMoveAudio()
    {
        audioSource.Stop();
    }


    void AnimationDirection()
    {
        // Set animation parameters based on the current Direction, even if the character is not moving
        anim.SetFloat("Horizontal", currentInput.x);
        anim.SetFloat("Vertical", currentInput.y);
    }

    void ParticleDirection(Vector2 direction)
    {
        if (direction == Vector2.up)
        {
            trail.transform.rotation = Quaternion.Euler(90, 0, 0);// Emit particles downwards by rotating the trail obj
            trail.transform.localPosition = new Vector3(0, 0.1f, 0); // updating position of the trail obj to the middle of tail
        }
        else if (direction == Vector2.down)
        {
            trail.transform.rotation = Quaternion.Euler(-90, 0, 0); // Emit particles upwards by rotating the trail obj
            trail.transform.localPosition = new Vector3(0, -0.1f, 0);// updating position of the trail obj to the middle of tail
        }
        else if (direction == Vector2.left)
        {
            trail.transform.rotation = Quaternion.Euler(0, 90, 0);// Emit particles to the right by rotating the trail obj
            trail.transform.localPosition = new Vector3(-0.1f, 0, 0); // updating position of the trail obj to the middle of tail
        }
        else if (direction == Vector2.right)
        {
            trail.transform.rotation = Quaternion.Euler(0, -90, 0);// Emit particles to the left by rotating the trail obj
            trail.transform.localPosition = new Vector3(0.1f, 0, 0);// updating position of the trail obj to the middle of tail
        }
    }

    bool isnotWall(Vector2 position)
    {
        foreach (var tilemap in wallTilemaps)
        {
            Vector3Int gridPos = tilemap.WorldToCell(position);
            if (tilemap.GetTile(gridPos) != null)
            {
                if (trail.activeSelf)
                {
                    trail.SetActive(false);
                }
                return false;
            }
        }
        return true;
    }



    private void PlayMoveAudio()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }


}