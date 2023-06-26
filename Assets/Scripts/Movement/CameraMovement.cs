using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    PlayerControls controls;

    Vector2 rotationValue;
    private float rotY = 0.0f;
    private float rotX = 0.0f;

    private Transform character;

    [SerializeField] private float rotationSensitivity = 100f;
    [HideInInspector] private float clampAngle = 80.0f;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Rotation.performed += ctx => rotationValue = ctx.ReadValue<Vector2>();
        controls.Player.Rotation.canceled += ctx => rotationValue = Vector2.zero;
    }

    private void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;

        character = transform.root;
    }

    private void LateUpdate()
    {
        float mouseX = rotationValue.x;
        float mouseY = -rotationValue.y;

        rotY += mouseX * rotationSensitivity * Time.deltaTime;
        rotX += mouseY * rotationSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
        Quaternion characterRotation = Quaternion.Euler(0.0f, rotY, 0.0f);
        character.rotation = characterRotation;
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
