using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public GameObject projectilePrefab; // Le prefab du projectile que le personnage va tirer
    public Transform shootPoint; // Le point à partir duquel le projectile sera tiré
    public float projectileSpeed = 10f; // La vitesse du projectile
    private SpriteRenderer sr;
    private GameObject Player;
    [SerializeField] private float timeMax = 2f;
    private float timeCurrent;
    public float decalage = 1f;
    public bool canShoot;

    private void Start()

    {
        sr = GetComponent<SpriteRenderer>();

    }
    // Update est appelé à chaque frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)) // Si la touche espace est pressée
        {
            Shoot(); // Appelle la méthode Shoot()
        }
        CoolDown();
    }

    private void Shoot()
    {
        if (canShoot)
        {
            if (sr.flipX)
            {
                GameObject projectile = Instantiate(projectilePrefab, new Vector3(shootPoint.position.x - decalage, shootPoint.position.y, shootPoint.position.z), shootPoint.rotation); // Crée une instance du prefab du projectile
                Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>(); // Récupère le Rigidbody2D du projectile
                projectileRigidbody.velocity = Vector2.left * projectileSpeed; // Donne au projectile une vitesse dans la direction du point de tir
            }
            else
            {
                GameObject projectile = Instantiate(projectilePrefab, new Vector3(shootPoint.position.x + decalage, shootPoint.position.y, shootPoint.position.z), shootPoint.rotation); // Crée une instance du prefab du projectile
                Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>(); // Récupère le Rigidbody2D du projectile
                projectileRigidbody.velocity = Vector2.right * projectileSpeed; // Donne au projectile une vitesse dans la direction du point de tir
            }
            canShoot = false;
        }
    }

    private void CoolDown()
    {
        if (timeCurrent >= 0 && !canShoot)
        {
            timeCurrent -= Time.deltaTime;
            //calcul de l'angle entre les 2vect en degré, 
            //float angle = Vector2.SignedAngle(Vector2.right, );
            //GameObject projectilePrefab = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, angle));

            //projectilePrefab.GetComponent<Projectile>().setSpeed(projectileSpeed);
        }
        else
        {
            timeCurrent = timeMax;
            canShoot=true;
        }
    }
}




