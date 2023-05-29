using UnityEngine;

public class Killzone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // v�rifie si l'objet entrant est le joueur
        {
            // r�initialise la position du joueur � sa position de d�part
            collision.transform.position = new Vector2(-27.57f, -5.97f); // ici on r�initialise la position � (0,0), mais il faut adapter selon la position de d�part souhait�e
        }
    }
}


