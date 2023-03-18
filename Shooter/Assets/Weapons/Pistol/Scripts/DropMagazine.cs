using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMagazine : MonoBehaviour
{
    [SerializeField] private GunAnimationEventListener animationEventListener;

    [SerializeField] private GameObject magazinePrefab;
    private Rigidbody magazineRigidbody;

    [SerializeField] private Transform magazineSpawnTransform;

    [SerializeField] private WeaponController weaponController;

    void Start()
    {
        animationEventListener = transform.gameObject.GetComponent<GunAnimationEventListener>();
        animationEventListener.OnDropMagazineEvent += OnDropMagazine;
        animationEventListener.OnAttachMagazineEvent += OnAttachMagazine;

        magazineRigidbody = GameObject.Instantiate(magazinePrefab, magazineSpawnTransform).GetComponent<Rigidbody>();
        ReturnToSpawn();

        weaponController = transform.gameObject.GetComponent<WeaponController>();
    }

    private void OnDropMagazine()
    {
        StopCoroutine("DespawnMagazineCorourtine");

        ReturnToSpawn();
        Enable();

        StartCoroutine("DespawnMagazineCorourtine");
    }

    private void OnAttachMagazine()
    {
        StopCoroutine("DespawnMagazineCorourtine");
        
        ReturnToSpawn();
        Disable();
    }

    private IEnumerator DespawnMagazineCorourtine()
    {
        yield return new WaitForSeconds(3f);

        ReturnToSpawn();
        Disable();
    }

    private void ReturnToSpawn()
    {
        magazineRigidbody.transform.position = magazineSpawnTransform.position;
        magazineRigidbody.transform.rotation = magazineSpawnTransform.rotation;
        magazineRigidbody.velocity = Vector3.zero;
        magazineRigidbody.angularVelocity = Vector3.zero;
    }

    private void Disable()
    {
        print("Disable");
        magazineRigidbody.isKinematic = true;
        magazineRigidbody.useGravity = false;
        magazineRigidbody.transform.parent = magazineSpawnTransform;
    }

    private void Enable()
    {
        print("Enable");
        magazineRigidbody.transform.parent = null;
        magazineRigidbody.isKinematic = false;
        magazineRigidbody.useGravity = true;
    }
}
