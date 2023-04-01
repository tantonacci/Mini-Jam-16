using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    public GameObject player;
    private PlayerInput input;

    // Start is called before the first frame update
    void Start()
    {
        input = player.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input.resetPressed) {
            ResetLevel();
        }
    }

    void ResetLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
