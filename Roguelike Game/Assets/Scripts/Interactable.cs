using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool useEvent;
    [SerializeField] public string promptMessage;

    public virtual string OnLook()
    {
        return promptMessage;
    }

    public void BaseInteract()
    {
        Interact();
    }

    protected abstract void Interact();
}
