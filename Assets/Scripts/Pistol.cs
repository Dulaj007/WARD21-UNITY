using System.Collections;
using UnityEngine;
using TMPro;

public class Pistol : MonoBehaviour
{
    public int maxAmmoInMag = 10;           // Maximum ammo capacity in the magazine
    public int maxAmmoInStorage = 30;       // Maximum ammo capacity in the storage
    public float shootCooldown = 0.5f;      // Cooldown time between shots
    public float reloadCooldown = 0.5f;     // Cooldown time for reloading
  
    public float shootRange = 100f;         // Range of the raycast
    public int damage = 15;                 // Damage dealt by the gun

    public ParticleSystem impactEffect;     // Particle effect for impact
    public ParticleSystem EnemyImpactEffect;
    public ParticleSystem muzzleFlash;      // Particle effect for muzzle flash

    public Transform cartridgeEjectionPoint;    // Ejection point of the cartridge
    public GameObject cartridgePrefab;          // Prefab of the cartridge
    public float cartridgeEjectionForce = 5f;   // Force applied to the cartridge

    public Animator gun;                        // Animator for the gun
    public GameObject muzzleFlashLight;         // Light for muzzle flash
    public AudioSource shoot;                   // Audio source for shooting
    public AudioSource reload;                   // Audio source for shooting
    public AudioSource noAmmoSound;             // Audio source for "no ammo" sound

    public TMP_Text ammoDisplay;                // TMP_Text component to display ammo count

    public int currentAmmoInMag;               // Current ammo in the magazine
    public int currentAmmoInStorage;            // Current ammo in the storage
    public bool canShoot = true;                // Flag to check if shooting is allowed
    private bool canSwitch = true;              // Flag to check if switching is allowed
    private bool isReloading = false;           // Flag to check if reloading is in progress
    private float shootTimer;                   // Timer for shoot cooldown

    public bool isHealing = false;


    void Start()
    {
        // Initialize ammo counts
        currentAmmoInMag = maxAmmoInMag;
        currentAmmoInStorage = maxAmmoInStorage;
        muzzleFlashLight.SetActive(false);

        // Initial update of ammo display
        UpdateAmmoDisplay();
    }

    void Update()
    {
        // Ensure ammo counts are within bounds
        currentAmmoInMag = Mathf.Clamp(currentAmmoInMag, 0, maxAmmoInMag);
        currentAmmoInStorage = Mathf.Clamp(currentAmmoInStorage, 0, maxAmmoInStorage);

        // Check for shoot input
        if (Input.GetButtonDown("Fire1") && canShoot && !isReloading && !isHealing)
        {
            Shoot();
        }

        // Check for reload input
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && !isHealing)
        {
            Reload();
        }

        // Update the shoot timer
        if (shootTimer > 0f)
        {
            shootTimer -= Time.deltaTime;
        }


    }
    public void TurnOnHealAnimation() {
      
            isHealing = true;
             gun.SetBool("Heal", true);
                 Debug.Log("turn on animation called confirmed in pistol" );
                 

    }

    public void TurnOffHealAnimation() {
      
            isHealing = false;
             gun.SetBool("Heal", false);
                 Debug.Log("turn on animation called confirmed in pistol" );

    }

    void Shoot()
    {
        // Check if there is ammo in the magazine and if the gun is not cooling down
        if (currentAmmoInMag > 0 && shootTimer <= 0f && canShoot)
        {
            canSwitch = false;

            // Play shoot sound if available
            if (shoot != null)
            {
                shoot.Play();
            }

            // Ensure the muzzle flash particle system resets and plays correctly
            if (muzzleFlash != null)
            {
                muzzleFlash.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                muzzleFlash.Play();
            }

            muzzleFlashLight.SetActive(true);
            gun.SetBool("shoot", true);

            // Perform the shoot action with raycast
            RaycastHit hit;
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * shootRange, Color.red, 1f);

         if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, shootRange))
{
    // Log when the raycast hits something
    Debug.Log("Raycast hit something at: " + hit.point);

    // Check if the hit object has the "enemy" tag
    if (hit.collider.CompareTag("Enemy"))
    {
        Debug.Log("Hit an enemy: " + hit.collider.name); // Log the name of the enemy hit

        // Check if the hit object has a ZombieHealth component
        ZombieHealth zombieHealth = hit.collider.GetComponent<ZombieHealth>();

        // Apply damage if the ZombieHealth component is found
        if (zombieHealth != null)
        {
            zombieHealth.TakeDamage(damage); // Apply damage
            Debug.Log("Damage applied to zombie: " + hit.collider.name); // Log the damage application
             
             if (EnemyImpactEffect != null)
            {
            Instantiate(EnemyImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        
        }
        else
        {
            Debug.Log("ZombieHealth component not found on the hit object.");
              
        }
    }
    else
    {
        Debug.Log("Hit an object that is not an enemy: " + hit.collider.name);
         if (impactEffect != null)
            {
                Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
    }

    // Instantiate impact effect at the hit point

            }

            // Instantiate the empty cartridge
            if (cartridgePrefab != null && cartridgeEjectionPoint != null)
            {
                GameObject cartridge = Instantiate(cartridgePrefab, cartridgeEjectionPoint.position, cartridgeEjectionPoint.rotation);
                Rigidbody cartridgeRigidbody = cartridge.GetComponent<Rigidbody>();
                cartridgeRigidbody.AddForce(cartridgeEjectionPoint.right * cartridgeEjectionForce, ForceMode.Impulse);
            }

            // Reduce ammo count
            currentAmmoInMag--;

            // Update ammo display
            UpdateAmmoDisplay();

            // Start the shoot cooldown
            shootTimer = shootCooldown;

            // End the animations and muzzle flash after a delay
            StartCoroutine(EndAnimations());
            StartCoroutine(EndLight());
            StartCoroutine(CanSwitchShoot());
        }
        else
        {
            // Play the "no ammo" sound if the gun cannot shoot
            if (noAmmoSound != null && currentAmmoInMag <= 0)
            {
                noAmmoSound.Play();
            }
        }
    }

    void Reload()
    {
        // Check if already reloading or out of ammo in the storage
        if (isReloading || currentAmmoInStorage <= 0)
        {
            return;
        }

        // Calculate the number of bullets to reload
        int bulletsToReload = maxAmmoInMag - currentAmmoInMag;

        // Check if there is enough ammo in the storage for reloading
        if (bulletsToReload > 0)
        {
            gun.SetBool("reload", true); // Use SetBool to play the animation
              if (reload != null)
            {
                reload.Play();
            }
            // Determine the actual number of bullets to reload based on available ammo
            int bulletsAvailable = Mathf.Min(bulletsToReload, currentAmmoInStorage);

            // Update ammo counts
            currentAmmoInMag += bulletsAvailable;
            currentAmmoInStorage -= bulletsAvailable;

            // Update ammo display
            UpdateAmmoDisplay();

            // Start the reload cooldown
            StartCoroutine(ReloadCooldown());
        }
    }

    IEnumerator ReloadCooldown()
    {
        isReloading = true;
        canShoot = false;
        canSwitch = false;

        yield return new WaitForSeconds(reloadCooldown);

        isReloading = false;
        canShoot = true;
        canSwitch = true;

        // Reset the reload animation state
        gun.SetBool("reload", false);
    }

    IEnumerator EndAnimations()
    {
        yield return new WaitForSeconds(0.1f);
        gun.SetBool("shoot", false);
    }

    IEnumerator EndLight()
    {
        yield return new WaitForSeconds(0.2f); // Slightly increased duration for visibility
        muzzleFlashLight.SetActive(false);
    }

    IEnumerator CanSwitchShoot()
    {
        yield return new WaitForSeconds(shootCooldown);
        canSwitch = true;
    }

    public void UpdateAmmoDisplay()
    {
        if (ammoDisplay != null)
        {
            ammoDisplay.text = currentAmmoInMag + "/" + currentAmmoInStorage;
        }
    }


}
