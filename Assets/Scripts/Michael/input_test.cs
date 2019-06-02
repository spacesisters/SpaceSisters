using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class input_test : MonoBehaviour
{
    private InputManager input_manager;

    // Start is called before the first frame update
    void Start()
    {
        input_manager = new InputManager(1, "DS4");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
