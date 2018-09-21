using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    public Animator anim;
    public override void Interact()
    {
        // Toggle and animate door
        bool isOpen = anim.GetBool("IsOpen");
        anim.SetBool("IsOpen", !isOpen);
    }
}
