using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject shootPoint;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float fireRate = 1.0f;

    private bool bCanShoot = true;

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started && bCanShoot)
        {
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
            bCanShoot = false;
            StartCoroutine(FireDelay());
        }
    }

    IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(fireRate);
        bCanShoot = true;
    }
}
