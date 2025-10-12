using Unity.Cinemachine;
using UnityEngine;

public class InputManager : MonoBehaviour

{
    public static InputManager Instance { get; private set; }
    public PlayerControls Controls {get; private set; }
    
    [SerializeField] private Movement movement;
    [SerializeField] private MouseLook mouseLook;
    
    private Vector2 horizontalInput;
    private Vector2 mouseInput;

    private PlayerControls.GroundMovementActions groundMovement;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        Controls = new PlayerControls();
        groundMovement = Controls.GroundMovement;
        
        
        groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
        groundMovement.Jump.performed += _ => movement.OnJumpPressed();
        groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;

    }

    private void OnDestroy()
    {
        if (GameStateManager.Instance != null)
            GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }
    
    private void Update()
    {
        movement.ReceiveInput(horizontalInput);
        mouseLook.ReceiveInput(mouseInput);
    }
    
    private void OnEnable() => Controls.Enable();
    private void OnDisable() => Controls.Disable();

    private void OnGameStateChanged(GameState newGameState)
    {
        bool isGameplay = newGameState == GameState.Gameplay;
        
        if (isGameplay)
            Controls.Enable();
        else
            Controls.Disable();

        if (movement != null) movement.enabled = isGameplay;
        if (mouseLook != null) mouseLook.enabled = isGameplay;
        
        Cursor.visible = !isGameplay;
        Cursor.lockState = isGameplay ? CursorLockMode.Locked : CursorLockMode.None;
    }
    
}
