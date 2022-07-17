using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public int currentRoom;
    public Spawner[] spawners;

    public void SpawnEnemiesFromRoom(int roomIndex) {
        spawners[roomIndex].Decompt();
    }
}
