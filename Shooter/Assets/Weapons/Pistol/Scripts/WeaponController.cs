using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private GunAnimationEventListener animationEventListener;

    [SerializeField] public int MaxAmmoCount { get; private set; } = 10;
    [SerializeField] public int AmmoCount { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.gameObject.GetComponent<Animator>();
        animationEventListener = transform.gameObject.GetComponent<GunAnimationEventListener>();
        animationEventListener.OnFireEndEvent += OnFireEnd;
        animationEventListener.OnReloadEndEvent += OnReloadEnd;

        AmmoCount = MaxAmmoCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("Firing"))
            return;

        if (!animator.GetBool("Last Bullet") && Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Firing", true);

            if (AmmoCount == 1)
                animator.SetBool("Last Bullet", true);
        }

        if (!animator.GetBool("Full Ammo") && Input.GetKey(KeyCode.R))
            animator.SetBool("Reloading", true);

    }

    private void OnFireEnd()
    {
        AmmoCount--;
        animator.SetBool("Full Ammo", false);
        animator.SetBool("Firing", false);
    }

    private void OnReloadEnd()
    {
        AmmoCount = MaxAmmoCount;
        animator.SetBool("Reloading", false);
        animator.SetBool("Full Ammo", true);
        animator.SetBool("Last Bullet", false);
    }

}
