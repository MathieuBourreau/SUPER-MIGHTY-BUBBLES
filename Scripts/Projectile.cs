using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float damage;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.TryGetComponent(out StatePlateform statePlateform))
        {
            statePlateform.isPlatform = true;
            statePlateform.BecomePlatformer();
            other.gameObject.SendMessage ("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }


    public void setSpeed (float newSpeed)
    {
        speed = newSpeed;
    }

    public void setDamage (float newDamage)
    {
        damage = newDamage;
    }
}
