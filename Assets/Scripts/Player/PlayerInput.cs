using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public enum AimSetting
    {
        Toggle,
        Hold
    }
    public AimSetting aimSetting;
    
    public event Action OnWalk;
    public event Action OnShoot;
    public event Action OnAimToggle;
    public event Action OnAimDown;
    public event Action OnAimUp;
    public event Action OnReload;
    public event Action OnPickup;

    [Header("Keys")]
    public KeyCode walkKey = KeyCode.Mouse0;
    public KeyCode shootKey = KeyCode.Mouse0;
    public KeyCode aimToggleKey = KeyCode.LeftShift;
    public KeyCode reloadKey = KeyCode.R;
    public KeyCode pickupKey = KeyCode.E;

    // Start is called before the first frame update
    void Start()
    {
        //Grab keybinds from settings
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(walkKey))
        {
            OnWalk?.Invoke();
        }

        if (Input.GetKeyDown(shootKey))
        {
            OnShoot?.Invoke();
        }

        if(aimSetting == AimSetting.Toggle)
        {
            if (Input.GetKeyDown(aimToggleKey))
            {
                OnAimToggle?.Invoke();
            }
        }
        else if(aimSetting == AimSetting.Hold)
        {
            if (Input.GetKeyDown(aimToggleKey))
            {
                OnAimDown?.Invoke();
            }
            if (Input.GetKeyUp(aimToggleKey))
            {
                OnAimUp?.Invoke();
            }
        }
        

        if (Input.GetKeyDown(reloadKey))
        {
            OnReload?.Invoke();
        }

        if (Input.GetKeyDown(pickupKey))
        {
            OnPickup?.Invoke();
        }
    }
}
