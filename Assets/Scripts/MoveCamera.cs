using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveCamera : MonoBehaviour
{
    public float speed, yBound;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < yBound)
        {
            SceneManager.LoadScene(0);
        }
    }

    void LateUpdate()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}
