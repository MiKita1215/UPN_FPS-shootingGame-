using UnityEngine;
using Photon.Pun;

public class Gun : MonoBehaviourPunCallbacks
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 20f;

    private PlayerMovement playerMovement;

    void Start()
    {
        if (photonView.IsMine)
        {
            playerMovement = GetComponentInParent<PlayerMovement>();
        }
    }

    void Update()
    {
        if (photonView.IsMine && Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        playerMovement.Shoot();

        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, bulletSpawn.position, bulletSpawn.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = bulletSpawn.forward * bulletSpeed;
        Destroy(bullet, 2f);
    }
}
