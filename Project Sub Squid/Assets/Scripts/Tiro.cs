using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour
{

    public float speedTiro = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * speedTiro * Time.deltaTime;
        Destroy(this.gameObject, 5.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject, 0.1f);
            Debug.Log("Tiro acertou");
        }

    }
}
