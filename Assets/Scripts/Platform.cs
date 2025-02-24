using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class Platform : MonoBehaviour
{
    
    [SerializeField] private Transform platformCenter;
    [SerializeField] private GameObject buildTurretPanel;
    [SerializeField] private Vector3 centerPosition;
    [SerializeField] private Vector2 screenPosition;
    [SerializeField] private Vector2 offset = new Vector2(0,15);
    [SerializeField] private InputActionReference mouseClick, mousePosition;

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
    }

    public void ResetUI()
    {
        buildTurretPanel.SetActive(false);
    }
    
}
