using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    [SerializeField] private GameObject button;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private ButtonPress buttonPress;

    public bool open;

    // Start is called before the first frame update
    void Start()
    {
        buttonPress = button.GetComponent<ButtonPress>();
        
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        open = buttonPress.buttonPressed;
        boxCollider.enabled = !open;

        SendAnimation("Open", open);
    }

    void SendAnimation(string anim, bool val) {
        animator.SetBool(anim, val);
    }
}
