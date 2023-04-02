using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorkboardManager : MonoBehaviour
{

    public bool open;
    private bool openLastFrame;

    [SerializeField] private GameObject arrow;
    [SerializeField] private Animator animator;
    public LayerMask openCloseMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, openCloseMask);
            if (hit) {
                open = !open;
            }
        }

        if (open && !openLastFrame) {
            SendAnimation("Open", true);
            FlipArrow();
        } else if (!open && openLastFrame) {
            SendAnimation("Open", false);
            FlipArrow();
        }

        openLastFrame = open;
    }

    void FlipArrow() {
        arrow.transform.Rotate(0,0,180);
    }

    void SendAnimation(string anim, bool val) {
        animator.SetBool(anim, val);
    }
}
