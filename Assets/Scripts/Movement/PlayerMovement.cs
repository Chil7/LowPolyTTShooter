using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using FSM;

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
    [SerializeField] private float currSpeed;
    private float velocity = 0f;

    [SerializeField] private float walkSpeed = 4f;
    [SerializeField] private float sprintSpeed = 6f;
    [SerializeField] private float hurtSpeed = 2.5f;
    [SerializeField] private float reloadWalkSpeed = 1.5f;

    //StateMachine / FSM
    public StateMachine StateMachine { get; private set; }

    private void Awake()
    {
        //StateMachine
        StateMachine = new StateMachine();

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
        StateMachine.SetState(new NormalState(this));
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        motion = Vector3.zero;
        ApplyPlayerMovement();

        StateMachine.OnUpdate();
    }

    public abstract class PlayerMovementState : IState
    {
        protected PlayerMovement instance;
        public PlayerMovementState(PlayerMovement _instance)
        {
            instance = _instance;
        }

        public virtual void OnEnter()
        {

        }
        public virtual void OnUpdate()
        {

        }

        public virtual void OnExit()
        {

        }

    }

    public class NormalState : PlayerMovementState
    {
        public NormalState(PlayerMovement _instance) : base(_instance)
        {
        }

        public override void OnEnter()
        {
            instance.currSpeed = instance.walkSpeed;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }

    public class HurtState : PlayerMovementState
    {
        public HurtState(PlayerMovement _instance) : base(_instance)
        {
        }

        public override void OnEnter()
        {
            instance.currSpeed = instance.hurtSpeed;
            Debug.Log("Hurt");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }

    public class ReloadState : PlayerMovementState
    {
        public ReloadState(PlayerMovement _instance) : base(_instance)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }

    void ApplyPlayerMovement()
    {
        //This converts the value that is read from the inputs to movement
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
