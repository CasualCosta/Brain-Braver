using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingArea : MonoBehaviour
{
    [SerializeField] float dps = 60f;
    [SerializeField] bool cycle = false, isDamaging = true;
    [SerializeField] List<IDamageable> Damageables = new List<IDamageable>();
    [SerializeField] HashSet<IDamageable> Damageabless = new HashSet<IDamageable>();

    [Header("Cycle Variables")]
    [SerializeField] float onTime = 100f;
    [SerializeField] float offTime = 0f, transitionOnTime = 0f, transitionOffTime = 0f, 
        startDelay = 0f;

    float originalScale;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale.x;
        if (cycle)
            StartCoroutine(CycleEffect());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable dam = collision.GetComponent<IDamageable>();
        if (dam != null && !Damageables.Contains(dam))
        {
            Damageables.Add(dam);
            Damageabless.Add(dam);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IDamageable dam = collision.GetComponent<IDamageable>();
        if (dam != null && Damageables.Contains(dam))
        {
            Damageables.Remove(dam);
            Damageabless.Remove(dam);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Damageables.Count == 0 || !isDamaging)
            return;
        //foreach (IDamageable dam in Damageables)
        //    dam.ChangeHealth(-dps * Time.deltaTime);
        for (int i = 0; i < Damageables.Count; i++)
        {
            Damageables[i].ChangeHealth(-dps * Time.deltaTime);
        }
    }

    //It always starts off and then goes to on.
    IEnumerator CycleEffect()
    {
        isDamaging = false;
        transform.localScale = Vector2.zero;
        yield return new WaitForSeconds(startDelay);
        while(true)
        {
            isDamaging = true;
            while(transform.localScale.x != originalScale)
            {
                transform.localScale = Vector2.MoveTowards
                    (transform.localScale, new Vector2(originalScale, originalScale),
                    originalScale / transitionOnTime * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(onTime);
            while (transform.localScale.x != 0)
            {
                transform.localScale = Vector2.MoveTowards
                    (transform.localScale, Vector2.zero,
                    originalScale / transitionOffTime * Time.deltaTime);
                yield return null;
            }
            isDamaging = false;
            yield return new WaitForSeconds(offTime);
        }
    }
}
