using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{

    public bool buttonPressed;

    [SerializeField] private GameObject btnBody;
    private ButtonRaycaster raycaster;

    // Start is called before the first frame update
    void Start()
    {
        raycaster = btnBody.GetComponent<ButtonRaycaster>();
    }

    // Update is called once per frame
    void Update()
    {
        buttonPressed = raycaster.buttonPressed;
    }
}
