using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour { 

    public int maxHealth = 3;
    public int health;
    public GameObject Player;
    public GameObject EmptyHeart_1;
    public GameObject EmptyHeart_2;
    public GameObject EmptyHeart_3;
    public GameObject Heart_1;
    public GameObject Heart_2;
    public GameObject Heart_3;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = maxHealth;
        EmptyHeart_1.SetActive (true);
        EmptyHeart_2.SetActive (true);
        EmptyHeart_3.SetActive (true);
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        health -= 1;
        animator.SetTrigger("hurt");


        if (health <= 0)
        {
            //Destroy(gameObject); Ne pas détruire juste cacher le gaem object et reload la scène
            Debug.Log("GameOver");
            Heart_1.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if(health == 1)
        {
            Heart_2.SetActive(false);
        }

        if(health == 2)
        {
            Heart_3.SetActive(false);
        }
     }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            TakeDamage(1);
        }

    }

}
