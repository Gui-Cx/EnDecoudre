using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{ 
    [SerializeField] int indexOfPrefab;
    public static event Action<int> ThePlayerSpawns;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        ThePlayerSpawns?.Invoke(indexOfPrefab);

    }

    // Update is called once per frame
    void Update()
    {
        print(this.GetComponent<PlayerInput>().currentControlScheme);
    }
}
