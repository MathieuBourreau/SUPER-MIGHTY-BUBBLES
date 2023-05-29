using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BatoBehavior : MonoBehaviour
{
    public static BatoBehavior Instance;

    public int m_enemyKilled = 0;
    public int m_nbEnnemyToKill = 5;

    [SerializeField] private GameObject m_getOnObject;
    [SerializeField] private GameObject m_BackToObject;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void AddCount()
    {
        m_enemyKilled++;
        if (m_enemyKilled >= m_nbEnnemyToKill)
        {

            if (m_getOnObject != null)
            {
                m_getOnObject.SetActive(true);
                m_BackToObject.SetActive(true);

            }
            //active le bato
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_enemyKilled >= m_nbEnnemyToKill)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene("VICTORY SCREEN" +
                    "");
            }
        }

    }
}
