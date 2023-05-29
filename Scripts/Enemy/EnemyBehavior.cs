using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 3f;        // vitesse de déplacement de l'ennemi
    public int damage = 1;         // dégâts infligés au joueur
    private StatePlateform slateP;

    private Rigidbody2D rb;
    private bool movingRight = true;

    private void Awake()
    {
        slateP = GetComponent<StatePlateform>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
   
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (slateP.isPlatform) return;
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            Debug.Log("pourquo il fait des degtsq ce fdp");
        }
    }
}



//if (movingRight)
//{
  //rb.velocity = new Vector2(speed, rb.velocity.y);
  //transform.localScale = new Vector3(1f, 1f, 1f);
//}
//else
//{
  //rb.velocity = new Vector2(-speed, rb.velocity.y);
  //transform.localScale = new Vector3(-1f, 1f, 1f);
//}

//if (col.gameObject.CompareTag("Platform"))
      //{
        //  movingRight = !movingRight;
      //}