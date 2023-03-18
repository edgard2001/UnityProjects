using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunAnimationEventListener : MonoBehaviour
{

    public delegate void OnFireEndDelegate();
    public event OnFireEndDelegate OnFireEndEvent;
    public void OnFireAnimationEnd() { OnFireEndEvent?.Invoke(); }

    public delegate void OnReloadEndDelegate();
    public event OnReloadEndDelegate OnReloadEndEvent;
    public void OnReloadAnimationEnd() { OnReloadEndEvent?.Invoke(); }

    public delegate void OnEjectCaseDelegate();
    public event OnEjectCaseDelegate OnEjectCaseEvent;
    public void OnEjectCaseAnimationEvent() { OnEjectCaseEvent?.Invoke(); }

    public delegate void OnDropMagazineDelegate();
    public event OnDropMagazineDelegate OnDropMagazineEvent;
    public void OnDropMagazineAnimationEvent() { OnDropMagazineEvent?.Invoke(); }

    public delegate void OnAttachMagazineDelegate();
    public event OnAttachMagazineDelegate OnAttachMagazineEvent;
    public void OnAttachMagazineAnimationEvent() { OnAttachMagazineEvent?.Invoke(); }

}