using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject OrbProjectileprefab;
    [SerializeField] SpriteRenderer OrbProjectileGFX;
    [SerializeField] Slider OrbPowerSlider;
    [SerializeField] Transform Orb;

    [Range(0, 10)]
    [SerializeField] float OrbPower;

    [Range(0, 3)]
    [SerializeField] float MaxOrbCharge;

    float OrbCharge;

    bool CanFire = true;

    private void Start()
    {
        OrbPowerSlider.value = 0f;
        OrbPowerSlider.maxValue = MaxOrbCharge;
    }


    private void Update()
    {
        if (Input.GetMouseButton(0) && CanFire)
        {
            ChargeOrb();
        }
        else if (Input.GetMouseButtonUp(0) && CanFire)
        {
            FireOrb();
        }
        else
        {
            if (OrbCharge > 0f)
            {
                OrbCharge -= 1f * Time.deltaTime;
            }
            else
            {
                OrbCharge = 0f;
                CanFire = true;
            }

            OrbPowerSlider.value = OrbCharge;
        }
    }

    void ChargeOrb()
    {
        OrbProjectileGFX.enabled = true;

        OrbCharge += Time.deltaTime;

        OrbPowerSlider.value = OrbCharge;

        if (OrbCharge > MaxOrbCharge)
        {
            OrbPowerSlider.value = MaxOrbCharge;
        }
    }

    void FireOrb()
    {
        if (OrbCharge > MaxOrbCharge) OrbCharge = MaxOrbCharge;

        float OrbprojectileSpeed = OrbCharge + OrbPower;
        float OrbDamage = OrbCharge * OrbPower;

        float angle = Utility.AngleTowardsMouse(Orb.position);
        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));

        OrbProjectile OrbProjectile = Instantiate(OrbProjectileprefab, Orb.position, rot).GetComponent<OrbProjectile>();
        OrbProjectile.OrbVelocity = OrbprojectileSpeed;
        OrbProjectile.OrbDamage = OrbDamage;

        CanFire = false;
        OrbProjectileGFX.enabled = false;
    }
 }
