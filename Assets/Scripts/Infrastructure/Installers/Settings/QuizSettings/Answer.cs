﻿using UnityEngine;

[System.Serializable]
public struct Answer
{
    [SerializeField] private string description;
    [SerializeField] private bool rightVariant;
    
    public string Description=>description;
    public bool RightVariant=>rightVariant;
}