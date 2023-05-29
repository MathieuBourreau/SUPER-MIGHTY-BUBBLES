using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 4f;
    [SerializeField] private float speed;
    [SerializeField] private bool pingPongMode;

    // Une liste contenant les checkpoints
    [SerializeField] private Transform[] listeDestinations;

    // Sens dans lequel on va passer d'un checkpoint � un autre (-1 pour aller � l'envers)
    [SerializeField] private int sens = 1;
    [SerializeField] private bool isRight;
    public bool IsRight => isRight; 

    // Le num�ro du checkPoint que l'on vise
    private int indexDestination;

    // Distance � laquelle on consid�re qu'on a atteint un checkpoint
    [SerializeField] private float seuil = 0.1f;

    private void Awake()
    {
        speed = initialSpeed;
    }

    public void pauseWalking(float tempsPause)
    {
        speed = 0;
        Invoke("resumeWalking", tempsPause);
    }

    public void resumeWalking()
    {
        speed = initialSpeed;
    }

    void Update()
    {
        // Une variable � laquelle j'assigne le Transform de notre checkpoint de destination
        Transform destination = listeDestinations[indexDestination];
        float distance = transform.position.x - destination.position.x;

        if (Mathf.Abs(distance)<seuil)
        {
            // On est assez proche du checkpoint pour considerer qu'on est arriv�

            // On passe au checkpoint suivant en ajoutant 1 ou -1 en fonction du sens
            indexDestination += sens;

            // SI l'index de destination est superieur ou �gale au nombre d'elements de la liste OU inferieur � 0
            if (indexDestination >= listeDestinations.Length || indexDestination < 0)
            {
                // Si on est en mode pingpong
                if (pingPongMode)
                {
                    // On inverse le sens
                    sens = -sens;
                    // On rechange l'index de destination pour revenir dans la plage de la liste
                    indexDestination += sens;
                }
                else // SINON
                {
                    // On change l'index de destination pour revenir au premier �l�ment
                    indexDestination = 0;
                }
            }
        }
        else
        {
            if (distance > 0)
            {
                // Je dois aller � gauche
                transform.Translate(Vector3.left * Time.deltaTime * speed);
                isRight = false;
            }

            if (distance < 0)
            {
                // Je dois aller � droite
                transform.Translate(Vector3.right * Time.deltaTime * speed);
                isRight = true;
            }
        }
    }
}
