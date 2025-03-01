using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //Singleton
    public static InputManager instance;

    //Action References
    [SerializeField] private InputActionReference mouseClick, mousePosition;
    [SerializeField] private RaycastHit lastHit;
    [SerializeField] private bool isPointerOverGameObject, hitOnce = false;

    //Camera Movement
    [SerializeField] private float screenOffset = 15f, maxCameraPositionX = 170f, maxCameraPositionY = 380f, cameraSpeed = 5f;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Vector3 startCamPosition;


    void Awake()
    {
        //Singleton implementation
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        mouseClick.action.performed += HandleClick;
        mainCamera = Camera.main;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startCamPosition = mainCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        isPointerOverGameObject = EventSystem.current.IsPointerOverGameObject();
        CameraMovement();
    }

    public void CameraMovement()
    {
        float right, left, forward, back;

        right = Screen.width - screenOffset;
        left = screenOffset;
        forward = Screen.height - screenOffset;
        back = screenOffset;

        Vector2 movement = new Vector2();

        //Right and left movement
        if(mousePosition.action.ReadValue<Vector2>().x > right)
        {
            if(mainCamera.transform.position.x < startCamPosition.x + maxCameraPositionX)
            {
                movement.x = 1f;
            }
        }
        else if(mousePosition.action.ReadValue<Vector2>().x < left)
        {
            if(mainCamera.transform.position.x > startCamPosition.x -maxCameraPositionX)
            {
                movement.x = -1f;
            }
        }
        //Up or down movement
        if(mousePosition.action.ReadValue<Vector2>().y > forward)
        {
            if(mainCamera.transform.position.z < startCamPosition.z + maxCameraPositionY)
            {
                movement.y=1f;
            }
        }
        else if(mousePosition.action.ReadValue<Vector2>().y < back)
        {
            if(mainCamera.transform.position.z > startCamPosition.z - maxCameraPositionY)
            {
                movement.y=-1f;
            }
        }
        if(movement.magnitude > 0f)
        {
            mainCamera.transform.Translate(new Vector3(movement.x,movement.y,0) * cameraSpeed * Time.deltaTime);
        }

    }

    public void HandleClick(InputAction.CallbackContext context)
    {
        if(isPointerOverGameObject)
            return;

        //Raycast x capire cosa ha colpito l'utente col mouse
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition.action.ReadValue<Vector2>());

        
        //Se ho colpito qualcosa col raycast
        if(Physics.Raycast(ray, out hit))
        {
            
            if(hitOnce)
            {
                lastHit.transform.GetComponent<Platform>().ResetUI();
            }
            if(hit.collider.tag == "Platform")
            {
                
                hit.transform.GetComponent<Platform>().SetUI();
                lastHit = hit;
                hitOnce = true;
            }
        }
        
    }
}
