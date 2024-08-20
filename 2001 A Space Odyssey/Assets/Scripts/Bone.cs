using UnityEngine;

public class Bone : Interactables, IInteractable
{
    private PlayerInteractions _playerInteractions;

    [SerializeField]
    private Collider _triggerCollider;

    [SerializeField]
    private Vector3 _onGrabbedRotation;
    
    public void Interact()
    {
        if(!_playerInteractions)
            return;
        
        _playerInteractions.GrabInteraction(gameObject, _onGrabbedRotation);
        CanInteract = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((_playerLayer.value & (1 << other.gameObject.layer)) == 0)
        {
            return;
        }

        _playerInteractions = other.GetComponent<PlayerInteractions>();
        
        if(!_playerInteractions)
            return;
        
        _playerInteractions.CanInteract();
    }
}
