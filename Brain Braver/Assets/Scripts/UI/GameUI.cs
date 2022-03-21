using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] PlayerSO playerSO = null;
    [SerializeField] Text infoText = null;
    [SerializeField] Text ammoText = null;
    [SerializeField] Slider healthSlider = null;
    [SerializeField] RectTransform[] gunTransforms = null;
    [SerializeField] float activeGunScale = 1f, 
        inactiveGunScale = 0.5f, absentGunScale = 0.2f;
    [SerializeField] string saveMessage = "GAME SAVED", healthBoost = "HEALTH UP", 
        newGun = "GUN ADDED", brakeBoost = "BRAKE UP", speedBoost = "SPEED UP", 
        slideBoost = "SLIDE UP";

    private void OnEnable()
    {
        Player.OnGunPickUp += PickGunUp;
        Player.OnGunChange += ChangeGun;
        Player.OnAmmoChange += UpdateAmmo;
        Player.OnHealthChange += UpdateHealth;
        SavePoint.OnSaveComplete += InformSave;
        HealthBoost.OnPickUp += InformHPBoost;
        BrakeBoost.OnPickUp += informBrakeBoost;
        SpeedBoost.OnPickUp += informSpeedBoost;
        PickableGun.OnPickUp += InformNewGun;
    }

    private void OnDisable()
    {
        Player.OnGunPickUp -= PickGunUp;
        Player.OnGunChange -= ChangeGun;
        Player.OnAmmoChange -= UpdateAmmo;
        Player.OnHealthChange -= UpdateHealth;
        SavePoint.OnSaveComplete -= InformSave;
        HealthBoost.OnPickUp -= InformHPBoost;
        BrakeBoost.OnPickUp -= informBrakeBoost;
        SpeedBoost.OnPickUp -= informSpeedBoost;
        PickableGun.OnPickUp -= InformNewGun;
    }

    private void Start()
    {
        SetGunUI();
        ChangeGun(0, playerSO);
        UpdateHealth();
    }

    

    void ChangeGun(int formerGunIndex, PlayerSO sO)
    {
        gunTransforms[formerGunIndex].localScale = new Vector2
            (gunTransforms[formerGunIndex].localScale.x, inactiveGunScale);
        gunTransforms[sO.weaponIndex].localScale = new Vector2
            (gunTransforms[sO.weaponIndex].localScale.x, activeGunScale);
        Vector2Int v2 = (sO.equippedGun.isInfinite) 
            ? Vector2Int.zero : sO.equippedGun.Ammo;
        UpdateAmmo(v2);
    }

    void SetGunUI()
    {
        if (gunTransforms.Length != playerSO.Guns.Count)
        {
            print("Amount of guns doesn't match.");
            return;
        }
        for(int i = 0; i < gunTransforms.Length; i++)
        {
            float size = (playerSO.Guns[i]) ? inactiveGunScale : absentGunScale;
            gunTransforms[i].localScale = new Vector2
                (gunTransforms[i].localScale.x, size);
        }
    }
    void PickGunUp()
    {
        SetGunUI();
        ChangeGun(0, playerSO);
    }

    void UpdateAmmo(Vector2Int ammo)
    {
        ammoText.text = (ammo.y == 0) ? "Inf." : string.Format("{0:D3} / {1:D3}", ammo.x, ammo.y);
    }

    public void UpdateHealth()
    {
        healthSlider.value = playerSO.Health.x;
        healthSlider.maxValue = playerSO.Health.y;
    }

    void InformSave() => StartCoroutine(DisplayInfoText(saveMessage));
    void InformNewGun() => StartCoroutine(DisplayInfoText(newGun));
    void InformHPBoost() => StartCoroutine(DisplayInfoText(healthBoost));
    void informBrakeBoost() => StartCoroutine(DisplayInfoText(brakeBoost));
    void informSpeedBoost() => StartCoroutine(DisplayInfoText(speedBoost));
    void informSlideBoost() => StartCoroutine(DisplayInfoText(slideBoost));
    public IEnumerator DisplayInfoText(string text)
    {
        infoText.text = text;
        infoText.gameObject.SetActive(true);
        StartCoroutine(Generic.Fade(1f, infoText, 0.5f, true));
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(Generic.Fade(0f, infoText, 0.5f, true));
        yield return new WaitForSecondsRealtime(0.5f);
        infoText.gameObject.SetActive(false);
    }
}
