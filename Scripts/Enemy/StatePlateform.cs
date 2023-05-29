using System.Threading.Tasks;
using UnityEngine;

public class StatePlateform : MonoBehaviour
{
    [SerializeField] public bool isPlatform;
    [SerializeField] private float speedUp = 5f;
    [SerializeField] private EnemyBehavior enemyBehavior;
    [SerializeField] private EnemyPatrol enemyPatrol;
    [SerializeField] private Shoot shootScript;
    [SerializeField] private Rigidbody2D rb;
    public Animator rekringeEmbulled;
    public GameObject Rekringe;

    bool m_canTask = false;

    bool m_canSendCount = true;
    private void Awake()
    {
        enemyBehavior = GetComponent<EnemyBehavior>();
        enemyPatrol = GetComponent<EnemyPatrol>();
        shootScript = GetComponent<Shoot>();
        rb = GetComponent<Rigidbody2D>();
        rekringeEmbulled = GetComponent<Animator>();
        Rekringe = gameObject;

        m_canTask = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BecomePlatformer();
    }

    public void BecomePlatformer()
    {
        if (isPlatform)
        {
            enemyBehavior.enabled = !isPlatform;
            enemyPatrol.enabled = !isPlatform;
            if(shootScript != null) shootScript.enabled = !isPlatform;
            rb.velocity = new(rb.velocity.x, speedUp);
            rekringeEmbulled.SetBool("isEmbuled", true);



        }
    }

    private async void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            rekringeEmbulled.SetBool("isExplosed", true);
            if (m_canSendCount)
            {
                Debug.Log("fdsfdsfds");
                BatoBehavior.Instance.AddCount();
                m_canSendCount = false;

                await Task.Delay(1000 * 2);
                if (!m_canTask) return;
                Destroy(gameObject);
            }
            //Destroy(Rekringe);
        }

    }
}
