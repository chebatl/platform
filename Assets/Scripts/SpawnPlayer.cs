using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private Transform spawnZone;
    private Transform player;
    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        player.position = spawnZone.position;
    }
}
