using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSkyBox : MonoBehaviour
{
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * moveSpeed);
    }
}
