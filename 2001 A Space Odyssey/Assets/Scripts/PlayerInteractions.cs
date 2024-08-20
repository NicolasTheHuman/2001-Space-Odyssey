using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private LayerMask _interactableLayer;

    [SerializeField] private float _interactionRange;

    private bool _canInteract;

    [SerializeField] private Transform _handPosition;
    private GameObject _objectInHand;

    private Collider[] _interactables;

    private void Update()
    {
        if(!_canInteract)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckInteraction();
        }
    }

    private void CheckInteraction()
    {
        Collider[] colliders_ = Interlocked.Exchange(ref _interactables, null) ?? new Collider[10];
        
        var count = Physics.OverlapSphereNonAlloc(gameObject.transform.position, _interactionRange, colliders_, _interactableLayer);

        if (count <= 0)
        {
            _canInteract = false;
            return;
        }

        for (int i = 0; i < count; i++)
        {
            var interactableObject = colliders_[i].gameObject.GetComponent<Interactables>();
            
            if(!interactableObject)
                continue;

            if (interactableObject.CanInteract && interactableObject is IInteractable interactable)
            {
                interactable.Interact();
                return;
            }
        }

        _canInteract = false;
    }

    public void CanInteract()
    {
        print("Player can interact");
        _canInteract = true;
    }

    public void GrabInteraction(GameObject objectToGrab)
    {
        if (_objectInHand)
        {
            //release object
        }

        _objectInHand = objectToGrab;
        _objectInHand.transform.parent = _handPosition;
        _objectInHand.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    public void GrabInteraction(GameObject objectToGrab, Vector3 rotation)
    {
        if (_objectInHand)
        {
            //release object
        }

        _objectInHand = objectToGrab;
        _objectInHand.transform.parent = _handPosition;
        _objectInHand.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(rotation));
    }
}
