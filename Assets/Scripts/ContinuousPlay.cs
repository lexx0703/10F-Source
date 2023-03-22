using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousPlay : MonoBehaviour
{
    //AudioSource audioData;
    // Start is called before the first frame update
    static bool loaded;

    void Awake()
    {
        if (!loaded)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);

        loaded = true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
