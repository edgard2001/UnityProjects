using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCaseController : MonoBehaviour
{
    [SerializeField] private GunAnimationEventListener animationEventListener;

    [SerializeField] private GameObject casePrefab;
    private Rigidbody caseRigidbody;

    [SerializeField] private Transform caseSpawnTransform;
    [SerializeField] private Transform caseEjectTargetTransform;
    [SerializeField] private float ejectForceMultiplier;
    private Vector3 ejectForce;

    [SerializeField] private WeaponController weaponController;

    void Start()
    {
        animationEventListener = transform.gameObject.GetComponent<GunAnimationEventListener>();
        animationEventListener.OnEjectCaseEvent += OnEjectCase;

        caseRigidbody = GameObject.Instantiate(casePrefab, caseSpawnTransform).GetComponent<Rigidbody>();
        Disable();

        weaponController = transform.gameObject.GetComponent<WeaponController>();
    }

    private void OnEjectCase()
    {

        StopCoroutine("DespawnCaseCorourtine");

        ReturnToSpawn();
        Enable();

        Vector3 localDir = (caseEjectTargetTransform.position - caseSpawnTransform.position).normalized;
        ejectForce = caseRigidbody.transform.InverseTransformDirection(transform.TransformDirection(localDir)) * ejectForceMultiplier;
        caseRigidbody.AddForce(ejectForce, ForceMode.Impulse);

        StartCoroutine("DespawnCaseCorourtine");
    }

    private IEnumerator DespawnCaseCorourtine()
    {
        yield return new WaitForSeconds(3f);
        ReturnToSpawn();
        Disable();
    }

    private void ReturnToSpawn()
    {
        caseRigidbody.transform.position = caseSpawnTransform.position;
        caseRigidbody.transform.rotation = caseSpawnTransform.rotation;
        caseRigidbody.velocity = Vector3.zero;
        caseRigidbody.angularVelocity = Vector3.zero;
    }

    private void Disable()
    {
        caseRigidbody.isKinematic = true;
        caseRigidbody.useGravity = false;
        caseRigidbody.transform.parent = caseSpawnTransform;
    }

    private void Enable()
    {
        caseRigidbody.transform.parent = null;
        caseRigidbody.isKinematic = false;
        caseRigidbody.useGravity = true;
    }

}
