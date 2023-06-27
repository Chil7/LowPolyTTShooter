using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FSM;

public class WeaponManager : MonoBehaviour
{
    PlayerControls controls;

    private GameObject player;

    [SerializeField] private int selectedWeapon = 0;

    //StateMachine / FSM
    public StateMachine StateMachine { get; private set; }

    private void Awake()
    {
        controls = new PlayerControls();

        //StateMachine
        StateMachine = new StateMachine();

        //Shoot / Attack
        controls.Player.Shoot.performed += ctx => Attack();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract class WeaponState : IState
    {
        protected WeaponManager instance;
        public WeaponState(WeaponManager _instance)
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

    public class Knife : WeaponState
    {
        public Knife(WeaponManager _instance) : base(_instance)
        {
        }

        public override void OnEnter()
        {

        }

        public override void OnUpdate()
        {

        }

        public override void OnExit()
        {

        }
    }

    public class Pistol : WeaponState
    {
        public Pistol (WeaponManager _instance) : base(_instance)
        {
        }

        public override void OnEnter()
        {
           
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }

    public class Shotgun : WeaponState
    {
        public Shotgun (WeaponManager _instance) : base(_instance)
        {
        }

        public override void OnEnter()
        {

        }

        public override void OnUpdate()
        {

        }

        public override void OnExit()
        {

        }
    }

    public class AssaultRifle : WeaponState
    {
        public AssaultRifle (WeaponManager _instance) : base(_instance)
        {
        }

        public override void OnEnter()
        {

        }

        public override void OnUpdate()
        {

        }

        public override void OnExit()
        {

        }
    }

    public class SniperRifle : WeaponState
    {
        public SniperRifle (WeaponManager _instance) : base(_instance)
        {
        }

        public override void OnEnter()
        {

        }

        public override void OnUpdate()
        {

        }

        public override void OnExit()
        {

        }
    }

    public void Attack()
    {
        player = GameObject.FindWithTag("Player");
        PlayerMovement _player = player.GetComponent<PlayerMovement>();

        _player.StateMachine.SetState(new PlayerMovement.HurtState(_player));
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
