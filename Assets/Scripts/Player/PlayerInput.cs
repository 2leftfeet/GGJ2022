using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector2 directionalInput;
    public Vector2 DirectionalInput{get {return directionalInput;}}

    public event Action HoldJump = delegate{};

    public event Action HoldPrimaryAttack = delegate{};
    public event Action OnInteract = delegate{};

    void Start()
    {
        LockMouse();
        GameEvents.current.onPauseMenuEvent += UnlockMouse;
        GameEvents.current.onUnpauseMenuEvent += LockMouse;
        GameEvents.current.onPlayerDeathEvent += UnlockMouse;
        GameEvents.current.onSceneRestartEvent += LockMouse;
    }

    void Update()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal -= 1f;
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontal += 1f;
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            vertical -= 1f;
        }
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            vertical += 1f;
        }
        
        if(Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.P))
        {
            GameEvents.current.PauseMenuEvent();
            
        }

        directionalInput = new Vector3(horizontal, vertical);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            HoldJump();
        }

        if(Input.GetMouseButton(0))
        {
            HoldPrimaryAttack();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
    }

    public void UnlockMouse() {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void LockMouse() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}
