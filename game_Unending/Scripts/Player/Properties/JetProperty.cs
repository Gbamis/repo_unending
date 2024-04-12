using System.Collections;
using System.Collections.Generic;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

namespace UE
{
    [System.Serializable]
    public class Flight
    {
        [Range(0, 1)] public float checker;
        public float maxSpeed;
        public float flySpeed;
        public float oldSpeed;
        public float steering;
        public float steerAngle;
        public float acceleration;
        public float maxHealth;
        private float currentHealth;
        private float side;
        private float up;
        //public bool isMobile;
        public bool isShieldUse;
        private bool takingHit;
        private float halfLife;

        private float nextHealthSpawn;

        public void Init()
        {
            ResetValues();
            nextHealthSpawn = 0;
            halfLife = maxHealth;
        }
        public void ResetValues()
        {
            flySpeed = 0;
            oldSpeed = 0;
            currentHealth = maxHealth;
        }

        public void OnUpdate(Transform body)
        {
            if (currentHealth < 0.1f)
            {
                GameManager.Instance.isGameRunning = false;
                GameManager.Instance.gameEventManager.currentGameEvent.QuitEvent();
            }
            if (flySpeed <= maxSpeed) { flySpeed += Time.deltaTime * acceleration; }

            if (!GameManager.Instance.isMobile)
            {
                side = Input.GetAxis("Horizontal");
                up = Input.GetAxis("Vertical");
            }
            else
            {
                side = GameManager.Instance.uIManager.virtualStick.direction.x;
                up = GameManager.Instance.uIManager.virtualStick.direction.z;
            }
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            float healthDisplay = currentHealth / maxHealth;
            float speed = flySpeed / maxSpeed;
            GameManager.Instance.uIManager.gameplaySelection.SetDetails(speed, healthDisplay);

            Vector3 movement = new Vector3(side * steering, up * steering, flySpeed * checker);
            body.Translate(movement * Time.deltaTime);

            Vector3 pos = body.transform.localPosition;

            float xClamp = GameManager.Instance.gameEventManager.currentGameEvent.eventConfig.horizontalClamp;
            float yClampmin = GameManager.Instance.gameEventManager.currentGameEvent.eventConfig.verticalClamp.x;
            float yClampmax = GameManager.Instance.gameEventManager.currentGameEvent.eventConfig.verticalClamp.y;

            pos.x = Mathf.Clamp(pos.x, -xClamp, xClamp);
            pos.y = Mathf.Clamp(pos.y, yClampmin, yClampmax);

            body.transform.localPosition = pos;
            Tilt(side, up, body.GetChild(0));

            GameManager.Instance.playerContext.playerSpeed = flySpeed;

            flySpeed = Mathf.Clamp(flySpeed, 0, 170);

            GameManager.Instance.uIManager.gameplaySelection.damageUI.Update(body.transform);

            if (currentHealth < halfLife && Time.time > nextHealthSpawn)
            {
                Vector3 healthPos = body.position;
                healthPos.z += 500;
                GameManager.Instance.fXManager.SpawnHelathPack(healthPos);
                nextHealthSpawn = Time.time + 20;
            }
        }
        public void Tilt(float side, float up, Transform model)
        {
            model.transform.localRotation = Quaternion.Euler(-up * steerAngle / 2, 0, -side * steerAngle);
        }
        public void JetHit(float impact)
        {
            if (isShieldUse) { return; }
            takingHit = true;
            if (currentHealth < 0)
            {
                GameManager.Instance.uIManager.gameplaySelection.damageUI.Enable(false); return;
            }
            currentHealth -= impact;
            GameManager.Instance.uIManager.gameplaySelection.damageUI.Enable(true);
            GameManager.Instance.StartCoroutine(HideDamage());
            GameManager.Instance.sfxManager.HitFX();
        }
        public void BoostHealth()
        {
            halfLife = currentHealth;
            currentHealth += 20; 
        }

        private IEnumerator HideDamage()
        {
            yield return new WaitForSeconds(.1f);
            takingHit = false;
            yield return new WaitForSeconds(.8f);
            if (takingHit == false) { GameManager.Instance.uIManager.gameplaySelection.damageUI.Enable(false); }
        }
    }

    [System.Serializable]
    public struct RefillItem
    {
        [Range(0, 1)] public float refilRate;
        [Range(0, 1)] public float useRate;
        [Range(0, 1)] public float currentAmount;
        [Range(0, 300)] public float multiplier;
        public bool canUse;
        public UnityEngine.UI.Image image;
    }

    [System.Serializable]
    public class Refilling
    {
        public RefillItem nitro;
        public RefillItem shield;
        public RefillItem attractor;
        private bool allocated;

        private Flight flightProperty;

        public void Init(Flight m_prop)
        {
            flightProperty = m_prop;
            nitro.currentAmount = 0;
            shield.currentAmount = 0;
            attractor.currentAmount = 0;
            allocated = false;
        }

        public void OnUpdate(Transform body)
        {
            UseShield();
            Refill();

            GameManager.Instance.uIManager.gameplaySelection.refillUI.SetValues(shield.currentAmount, nitro.currentAmount, attractor.currentAmount);
        }
        private void UseShield()
        {
            if ((GameManager.Instance.playerContext.isShieldPressed || Input.GetKey(KeyCode.E)) && shield.currentAmount > 0.1f)
            {
                shield.currentAmount -= shield.useRate;
                GameManager.Instance.uIManager.gameplaySelection.refillUI.shieldLevel.color = Color.red;
                flightProperty.isShieldUse = true;

                GameManager.Instance.playerContext.shieldFX.gameObject.SetActive(true);
                GameManager.Instance.playerContext.shieldFX.Play();
            }
            else
            {
                GameManager.Instance.uIManager.gameplaySelection.refillUI.shieldLevel.color = Color.white;
                flightProperty.isShieldUse = false;
                GameManager.Instance.playerContext.shieldFX.gameObject.SetActive(false);
            }
        }


        /*private void UseNitro()
        {
            if ((GameManager.Instance.playerContext.isNitroPressed || Input.GetKey(KeyCode.R)) && nitro.currentAmount > 0.1f)
            {
                allocated = true;
                flightProperty.oldSpeed = flightProperty.flySpeed;
                flightProperty.flySpeed = nitro.multiplier;
                nitro.currentAmount -= nitro.useRate;
                GameManager.Instance.uIManager.gameplaySelection.refillUI.nitroLevel.color = Color.red;
                GameManager.Instance.Trail.SetActive(true);

            }
            else
            {
                GameManager.Instance.uIManager.gameplaySelection.refillUI.nitroLevel.color = Color.white;
                if (allocated) { flightProperty.flySpeed = flightProperty.maxSpeed; }
                GameManager.Instance.Trail.SetActive(false);
            }
        }*/
        private void Refill()
        {
            if (nitro.currentAmount < 1) { nitro.currentAmount += nitro.refilRate * Time.deltaTime; }
            if (shield.currentAmount < 1) { shield.currentAmount += shield.refilRate * Time.deltaTime; }
            if (attractor.currentAmount < 1) { attractor.currentAmount += attractor.refilRate * Time.deltaTime; }

            nitro.currentAmount = Mathf.Clamp(nitro.currentAmount, 0, 1);
            shield.currentAmount = Mathf.Clamp(shield.currentAmount, 0, 1);
            attractor.currentAmount = Mathf.Clamp(attractor.currentAmount, 0, 1);
        }

        /*private void UseAttractor()
        {
            if ((GameManager.Instance.playerContext.isAttractorPressed || Input.GetKey(KeyCode.T)) && attractor.currentAmount > 0.1f)
            {
                attractor.currentAmount -= attractor.useRate;
                GameManager.Instance.uIManager.gameplaySelection.refillUI.attractorLevel.color = Color.red;
                GameManager.Instance.playerContext.isAttractionMode = true;
                GameManager.Instance.playerContext.attarctionFX.Play();

            }
            else
            {
                GameManager.Instance.playerContext.isAttractionMode = false;
                GameManager.Instance.uIManager.gameplaySelection.refillUI.attractorLevel.color = Color.white;
                GameManager.Instance.playerContext.attarctionFX.Stop();

            }
        }*/
    }

    [CreateAssetMenu(fileName = "Jet Property", menuName = " Axxon/Player/Jet Properties")]
    public class JetProperty : Property
    {
        public Sprite backgroundImage;
        public Sprite thumbnail;
        public string JetName;
        public Flight flightDetails;
        public Weapon weaponDetails;
        public Refilling refillDetails;

        public override void OnPropertyInit()
        {
            flightDetails.Init();
            weaponDetails.Init();
            refillDetails.Init(flightDetails);
        }
        public override void OnPropertyProcess(Transform body)
        {
            flightDetails.OnUpdate(body);
            weaponDetails.OnUpdate(body);
            refillDetails.OnUpdate(body);

            UpdateWeapon();
        }

        public void SwitchWeapon(Weapon wep) { weaponDetails = wep; }

        private void UpdateWeapon()
        {
            GameManager.Instance.uIManager.gameplaySelection.refillUI.currentWeaponIcon.sprite = weaponDetails.icon;
            GameManager.Instance.uIManager.gameplaySelection.refillUI.currentWeaponLevel.fillAmount = weaponDetails.magazine / 10000;
        }
    }
}
