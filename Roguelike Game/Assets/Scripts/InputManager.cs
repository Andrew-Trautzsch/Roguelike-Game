using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.NormalActions normalActions;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = new PlayerInput();
        normalActions = playerInput.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }
    private void OnDisable()
    {
        playerInput.Disable();
    }
}
