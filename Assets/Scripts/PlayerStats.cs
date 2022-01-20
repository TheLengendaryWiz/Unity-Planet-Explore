﻿using UnityEngine;
public class PlayerStats:MonoBehaviour
{
    public static PlayerStats instance;
    public int maxHealth = 100;
    public float HealthregenRate = 2;
    private int _curHealth;
    public int curHealth
    {
        get { return _curHealth; }
        set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
    }
    public float movementSpeed=10f;
    void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        
    }

}