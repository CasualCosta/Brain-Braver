using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToggler : MonoBehaviour
{
    [Tooltip("I scripted it so the game won't cause an error if this is null.")]
    [SerializeField] SpriteRenderer spriteRenderer = null;
    [Tooltip("Behaviors that are activated when the player is closed and de-activated when the player is far.")]
    [SerializeField] List<MonoBehaviour> proximityBehaviours = new List<MonoBehaviour>();
    [Tooltip("Behaviors that are enabled when the player is far and activated when the player is close.")]
    [SerializeField] List<MonoBehaviour> distanceBehaviours = new List<MonoBehaviour>();
    [SerializeField] float activationDistance = 10f, deactivationDistance = 25f, switchInterval = 2.5f;
    [SerializeField] bool activatesByProximity = true, deactivatesByDistance = true, 
        isActive = false;
    [SerializeField] Color activeColor = Color.white, inactiveColor = Color.grey;


    float currentInterval;
    private void Start()
    {
        ToggleActivation(isActive);
    }
    // Update is called once per frame
    void Update()
    {
        if (currentInterval > 0)
        {
            currentInterval -= Time.deltaTime;
            return;
        }
        if (isActive && deactivatesByDistance)
        {
            if (Vector2.Distance(transform.position, Player.Instance.transform.position) > deactivationDistance)
                ToggleActivation(false);
        }
        else if (!isActive && activatesByProximity)
            if (Vector2.Distance(transform.position, Player.Instance.transform.position) < activationDistance)
                ToggleActivation(true);
    }

    public void ToggleActivation(bool activating)
    {
        if(spriteRenderer)
            spriteRenderer.color = activating ? activeColor : inactiveColor;
        foreach (MonoBehaviour mono in proximityBehaviours)
            mono.enabled = activating;
        foreach (MonoBehaviour mono in distanceBehaviours)
            mono.enabled = !activating;
        isActive = activating;
        currentInterval = switchInterval;
    }
}
