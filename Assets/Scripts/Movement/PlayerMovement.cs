using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    //Audio
    private AudioSource audioSource;

    //PlayerControls & Movement
    PlayerControls controls;
    private Vector3 motion;
    private Vector2 motionValue;

    private CharacterController controller;

    //Variations of speed
    private float currSpeed;
    private float velocity = 0f;

    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float hurtSpeed = 2.5f;
    [SerializeField] private float reloadWalkSpeed = 1.5f;

    private void Awake()
    {
        //Movement
        controls = new PlayerControls();

        controls.Player.Movement.performed += ctx => motionValue = ctx.ReadValue<Vector2>();
        controls.Player.Movement.canceled += ctx => motionValue = Vector2.zero;

        if (!TryGetComponent<CharacterController>(out controller))
        {
            Debug.LogError("This object needs a Character Controller attached");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        motion = Vector3.zero;
        ApplyPlayerMovement();
    }

    void ApplyPlayerMovement()
    {
        motion += transform.forward * motionValue.y * currSpeed * Time.deltaTime;
        motion += transform.right * motionValue.x * currSpeed * Time.deltaTime;
        motion.y += velocity;

        if (controller.enabled)
        {
            controller.Move(motion);
        }
    }

    public void OnEnable()
    {
        controls.Player.Enable();
    }

    public void OnDisable()
    {
        controls.Player.Disable();
    }
}
