using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingPlayer : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Vector3 cameraPosition;
    [SerializeField] private Vector3 ref_Velocity;
    [SerializeField] private float smoothtime;
    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        cameraPosition = transform.position;
        Vector3 playerPosition = new(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z - 10f);
        transform.position = Vector3.SmoothDamp(cameraPosition, playerPosition, ref ref_Velocity, smoothtime);
    }
}
