using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{

    public bool onCorkboard;
    public bool isHeld;

    [SerializeField] private LayerMask corkboardLayer;

    public GameObject corkboard;
    private Collider2D collider;

    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.GetComponent<Collider2D>();

        SetPos();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeld) {
            collider.isTrigger = true;
            followMouse();
        } else if (onCorkboard) {
            collider.isTrigger = true;
            followCorkboard();
        } else {
            collider.isTrigger = false;
        }
    }

    void followMouse() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = onCorkboard ? -1 : -.1f;
        transform.position = mousePosition;
    }

    void followCorkboard() {
        Vector3 corkboardPosition = corkboard.transform.position;
        transform.position = corkboardPosition + pos;
    }

    void OnMouseDown() {
        isHeld = !isHeld;

        if (!isHeld) {
            SetPos();
        }
    }

    void SetPos() {
        pos = transform.position - corkboard.transform.position;
    }

    void OnTriggerStay2D(Collider2D col) {
        if (isHeld && col.gameObject.layer == (col.gameObject.layer | 1 << corkboardLayer)) {
            onCorkboard = true;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.layer == (col.gameObject.layer | 1 << corkboardLayer)) {
            onCorkboard = false;
        }
    }
}
