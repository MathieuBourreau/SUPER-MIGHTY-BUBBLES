using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnnmy : MonoBehaviour
{
    public GameObject pincemoa;
    public GameObject rekringe;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(rekringe.gameObject);
        Destroy(pincemoa.gameObject);
    }
}
