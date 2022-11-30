using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class ChangeGrabablePose : XRGrabInteractable
{
    [SerializeField] XRGrabInteractable grabInteractable;

    [SerializeField] Transform attachTransform1Right;
    [SerializeField] Transform attachTransform2Right;

    [SerializeField] Transform attachTransform1Left;
    [SerializeField] Transform attachTransform2Left;

    Transform attachTransformAux;

    bool rightHand = false;


    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if(args.interactorObject.transform.CompareTag("RightHand"))
        {
            rightHand = true;
        }
        else if (args.interactorObject.transform.CompareTag("LeftHand"))
        {
            rightHand = false;
        }

        ChangeHandAttachTransform();

        base.OnSelectEntered(args);
    }

    // Called when the player activates the knife in hand, changing its pose
    public void ChangeAttachTransform()
    {
        attachTransformAux = grabInteractable.attachTransform;

        if (rightHand)  // If the grabber (interactor) is the RIGHT hand
        {
            attachTransformAux = attachTransformAux == attachTransform1Right ? attachTransform2Right : attachTransform1Right;
        }
        else // If the grabber (interactor) is the LEFT hand
        {
           attachTransformAux = attachTransformAux == attachTransform1Left ? attachTransform2Left : attachTransform1Left;
        }

        grabInteractable.attachTransform = attachTransformAux;
    }

    // Called when enters select, changes the hand attach transform
    public void ChangeHandAttachTransform()
    {
        attachTransformAux = grabInteractable.attachTransform;

        if (rightHand)  // If the grabber (interactor) is the RIGHT hand
        {
            if(attachTransform != attachTransform1Right && attachTransform != attachTransform2Right)
            {
                attachTransformAux = attachTransformAux == attachTransform1Left ? attachTransform1Right : attachTransform2Right;
            }
        }
        else  // If the grabber (interactor) is the RIGHT hand
        {
            if (attachTransform != attachTransform1Left && attachTransform != attachTransform2Left)
            {
                attachTransformAux = attachTransformAux == attachTransform1Right ? attachTransform1Left : attachTransform2Left;
            }
        }

        grabInteractable.attachTransform = attachTransformAux;
    }

    public void ResetTransform()
    {
        transform.localPosition = Vector3.zero;

        transform.forward = transform.parent.forward;
        transform.Rotate(new Vector3(-90, 0, 0));

    }
}
