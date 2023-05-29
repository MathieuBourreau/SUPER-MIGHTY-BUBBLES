using UnityEngine;

public class PlayerMoveJump : MonoBehaviour
{
    private float horizontalMove;

    // bool --> true ou false
    // Pour mémoriser des etats de notre blob
    [SerializeField] private bool isJumpingRequired;
    private bool isFalling;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isWalking;
    [SerializeField] private bool isDashing;
    private bool isCeiled;

    private Vector2 zeroVelocity = Vector2.zero;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float initialGravityScale;
    //public Animator animator;

    [SerializeField] private float speed = 6f;
    [SerializeField] private float movementSmoothing = 0.2f;
    [SerializeField] private float jumpForce = 6.5f;
    [SerializeField] private float jumpForceCeiling = 2f;
    [SerializeField] private float velocityThreshold = 0.15f;
    [SerializeField] private float fallGravityMultiplier = 2.2f;
    [SerializeField] private float lowJumpGravityMultiplier = 2.5f;
    [SerializeField] private float timeToJump;
    [SerializeField] private float timeToJumpMax = 0.1f;
    private float m_jumpCount = 1;

    [SerializeField] private LayerMask groundLayers;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckWidth;
    [SerializeField] private float groundCheckHeight;

    public GameObject Player;
    public Animator animPlayer;


    private void Awake()
    {
        // On assigne à la variable rb le composant RigidBody2D de notre player (le gameObject qui possède ce script)
        rb = GetComponent<Rigidbody2D>();

        // On assigne à la variable initialGravityScale l'echelle de gravité de départ de notre Player
        initialGravityScale = rb.gravityScale;

        // ADD -- Animator Speed Attribute
       //animator = GetComponent<Animator>();

        // On assigne à la variable rb le composant SpriteRenderer de notre player (le gameObject qui possède ce script)
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Jump();
        // On assigne à la variable horizontalMove une valeur comprise entre -1 et 1 récupérée depuis le clavier ou le joystick
        horizontalMove = Input.GetAxisRaw("Horizontal");
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        //animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        // SI horizontalMove est supérieur à zéro, le joueur veut aller à droite 
        if (horizontalMove > 0)
        {
            // On NE FAIT PAS de symétrie horizontale sur le sprite du Player
            // En partant du principe que le sprite du personnage est dessiné tourné vers la droite
            spriteRenderer.flipX = false;
        }
        else if (horizontalMove < 0)
        {
            // On FAIT une symétrie horizontale sur le sprite du Player
            spriteRenderer.flipX = true;
        }

        if (CheckGround())
        {
            if (Input.GetButtonDown("Jump"))
            {
                // Je passe la variable isJumpingRequired à VRAI pour dire qu'un saut est demandé
                isJumpingRequired = true;

                //animator.SetBool("Jumping", true);
            }
            // SI le Player ne touchait pas le sol à la frame précédente
            // On vient d'atterir !

            // animator.SetBool("Jumping", false);

            // Je passe la variable isGrounded à VRAI car on est au sol
            isGrounded = true;

            //animator.SetBool("Falling", false);

            // Je passe la variable isFalling à FAUX si on est au sol alors on ne tombe plus
            isFalling = false;
            m_jumpCount = 1;
        }
        else if(m_jumpCount > 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                // Je passe la variable isJumpingRequired à VRAI pour dire qu'un saut est demandé
                isJumpingRequired = true;

                //animator.SetBool("Jumping", true);
                Debug.Log("vfdvvfd");
                m_jumpCount--;
            }
        }
        else
        {
            // Puisqu'on ne touche pas le sol, on passe isGrounded à FAUX
            isGrounded = false;

        }
        if (Player == isGrounded && rb.velocity.x != 0)
        {
            animPlayer.SetBool("isWalking", true);
            animPlayer.SetBool("isGrounded", true);
        }
        else
        {
            animPlayer.SetBool("isWalking", false);
        }

        if (Player != isGrounded && rb.velocity.y != 0)
        {
            animPlayer.SetBool("isJumping", true);
        }
        else
        {
            animPlayer.SetBool("isJumping", false);
        }

        if(Player != isGrounded && rb.velocity.y != 0 && rb.velocity.x != 0)
        {
            animPlayer.SetBool("isJumping", true);
            animPlayer.SetBool("isGrounded", false);
            animPlayer.SetBool("isWalking", true);
        }

        if(transform.position.y < -30)
        {
            transform.position = Vector3.zero;
        }

    }

    void FixedUpdate()
    {
        float tempSpeed = speed;

        // Si le saut est requis
        if (isJumpingRequired)
        {
            if (true)
            {
                rb.velocity = Vector2.up * jumpForce;
                GetComponent<AudioSource>().Play();
                isJumpingRequired = false;
            }
        }

        // Si le player chute, on lance l'animation de chute
        if (rb.velocity.y < -velocityThreshold)
        {
            //animator.SetBool("Falling", true);
            isFalling = true;
            rb.gravityScale = initialGravityScale * fallGravityMultiplier;
        }
        else
        {
            //animator.SetBool("Falling", false);
            isFalling = false;
        }

        // SI la méthode Physics2D.OverlapBox nous renvoi quelque chose (Different de null) --> les pieds du player touchent le sol
        // Physics2D.OverlapBox va chercher et renvoyer le premier objet qui chevauche la zone rectangulaire et qui appartient au layer Ground
        Debug.Log(CheckGround());
     

        Vector2 targetVelocity = new Vector2(horizontalMove * tempSpeed, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref zeroVelocity, movementSmoothing);
    }

    private void Jump()
    {
        if(isJumpingRequired )
        {
            if (timeToJump > 0f)
            {
                timeToJump -= Time.deltaTime;
            } else
            {
                timeToJump = timeToJumpMax;
                isJumpingRequired = false;
            }
        }
    }
    private bool CheckGround() => Physics2D.OverlapBox(groundCheck.position, new Vector2(groundCheckWidth, groundCheckHeight), 0f, groundLayers);

   /*void OnDrawGizmos()
    {
        Draw a yellow sphere at the transform's position
        Gizmos.color = new Color32(0, 255, 0, 90);
        Gizmos.DrawCube(groundCheck.position, new Vector2(groundCheckWidth, groundCheckHeight));
    }*/
  

}
