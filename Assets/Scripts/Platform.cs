using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Platform : MonoBehaviour
{
    
    [SerializeField] private Transform platformCenter;
    [SerializeField] private GameObject buildTurretPanel;
    [SerializeField] private Vector3 centerPosition;
    [SerializeField] private Vector2 screenPosition;
    [SerializeField] private Vector2 offset = new Vector2(0,15);
    [SerializeField] private InputActionReference mouseClick, mousePosition;
    [SerializeField] private static float screenBounds = 15f;
    [SerializeField] private bool activeUI = false;
 
    void Awake()
    {
        platformCenter = transform.Find("Platform_Center");
        centerPosition = platformCenter.position;
        screenPosition = Camera.main.WorldToScreenPoint(centerPosition);
        screenPosition += offset;

        buildTurretPanel = GameObject.Find("BuildTurret_Panel");
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buildTurretPanel.SetActive(false);
    }

    

    // Update is called once per frame
    void Update()
    {
        if(activeUI)
        {
            UpdateUIPosition();
        }
    }

    public void UpdateUIPosition()
    {
        //Ricavo la posizione sullo schermo del punto in cui deve comparire la UI
        screenPosition = Camera.main.WorldToScreenPoint(centerPosition);
        screenPosition += offset;
        //Se la UI è fuori dai bounds
        //Forse devo modificare in modo da capire se il rect transform è fuori dai bounds
        //oppure uno dei bordi sta andando fuori dal bound.
        if(screenPosition.x > Screen.width - screenBounds || screenPosition.x < screenBounds || screenPosition.y > Screen.height - screenBounds || screenPosition.y < screenBounds )
        {
            buildTurretPanel.SetActive(false);
            return;
        }

        //Set UI Position
        buildTurretPanel.GetComponent<RectTransform>().position = screenPosition;
    }

    public void SetUI()
    {

        if(buildTurretPanel.activeInHierarchy)
        {
            buildTurretPanel.SetActive(false);
            Debug.Log("Disattivo pannello");
        }

        //Set UI Position
        buildTurretPanel.GetComponent<RectTransform>().position = screenPosition;
        //Activate UI gameobject
        buildTurretPanel.SetActive(true);
        activeUI = true;
    }

    public void ResetUI()
    {
        activeUI = false;
        buildTurretPanel.SetActive(activeUI);
    }

    
    
}
