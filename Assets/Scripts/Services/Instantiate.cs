using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate : MonoBehaviour
{
    public GameObject AudioSourcePrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("AudioSources");

        if (gameObjects.Length == 0){
            Instantiate(AudioSourcePrefab, new Vector3(0, 0, 0), Quaternion.identity);      
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
