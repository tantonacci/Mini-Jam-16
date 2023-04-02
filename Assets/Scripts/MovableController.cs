using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableController : MonoBehaviour
{

    private Vector2 totalForce = new Vector2(0,0); 
    [SerializeField] private Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void externalForce(Vector2 force) {
        rbody.AddForce(totalForce);
        totalForce.Set(0,0);
    }
}
