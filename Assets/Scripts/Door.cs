using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private void Awake()
    {
        Spawner.wavesFinish += open;
    }
    private void OnDestroy()
    {
        Spawner.wavesFinish -= open;
    }

    private void open()
    {
        //open
    }

}
