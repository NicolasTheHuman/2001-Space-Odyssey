using System;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    [SerializeField] protected LayerMask _playerLayer;
    
    public bool CanInteract { get; protected set; }

    private void Awake()
    {
        CanInteract = true;
    }
}
