using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
class Droppable
{
    public GameObject _droppable;
    [Range(0,100)] public float _odds;
}
[System.Serializable]
class DroppablePickups
{
    public GameObject _pickup;
    public int index;
}
enum DamageAnimation { Color = 0, Sprite = 1 }
public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float _health = 20f;
    [SerializeField] float _armor = 0f;
    [Tooltip("The trail is for objects that have a trail set behind them.")]
    [SerializeField] TrailRenderer _trail = null;
    [Tooltip("The sprite renderer is to adjust the color according to the damage taken.")]
    [SerializeField] SpriteRenderer _sprite = null;
    [Tooltip("The collider is only relevant if I'm using a trail. It can be left null if not.")]
    [SerializeField] Collider2D _collider = null;
    [Tooltip("For enemies that stay down after killed.")]
    [SerializeField] RoomSO _roomSO = null;
    [SerializeField] int _deletedIndex = -1;
    [Tooltip("The enabler in case the player hits the enemy from too far.")]
    [SerializeField] DistanceToggler toggler = null;
    [Tooltip("The dialogue to be triggered on the game object's death.")]
    [SerializeField] DialogueTrigger dialogueTrigger = null;
    [Tooltip("The items dropped when destroyed.")]
    [SerializeField] Droppable[] _droppables = null;
    [SerializeField] DroppablePickups[] _pickUps = null;
    [SerializeField] float _dropplableSpawnPointOffset = 0.5f;
    [SerializeField] bool isBoss = false;
    [SerializeField] float bossDeathTime = 3f;
    [Tooltip("Whether the enemy flashes a different color or a different sprite when hurt.")]
    [SerializeField] DamageAnimation damageAnimation = DamageAnimation.Color;
    [SerializeField] Sprite damageSprite = null;

    Color originalColor;
    Sprite originalSprite;
    bool isDead = false;
    

    public event Action OnBossDeath;
    public event Action OnDeath;
    void IDamageable.ChangeHealth(float value)
    {
        if (value < 0)
            value = Mathf.Clamp(value + _armor, value, 0);
        _health += value;
        if(value < 0)
        {
            if (toggler)
                toggler.ToggleActivation(true);
            if (damageAnimation == DamageAnimation.Color)
                StartCoroutine(FlashColor());
            else
                StartCoroutine(FlashSprite());
            if (_health <= 0)
            {
                if (isDead)
                    return;
                isDead = true;
                Die();
            }
        }
    }

    IEnumerator FlashColor()
    {
        if (!_sprite)
            yield break;
        _sprite.color = Color.red;
        yield return null;
        _sprite.color = originalColor;
    }
    IEnumerator FlashSprite()
    {
        if (!_sprite)
            yield break;
        _sprite.sprite = damageSprite;
        yield return null;
        _sprite.sprite = originalSprite;
    }

    void CheckIfSpawnable()
    {
        if (!CheckForValidRoom())
            return;
        if (_roomSO.deletedEnemies[_deletedIndex].boolean)
            Die();

    }

    bool CheckForValidRoom()
    {
        if (!_roomSO)
            return false;
        if (_deletedIndex < 0 || _deletedIndex >= _roomSO.deletedEnemies.Length)
            return false;
        return true;
    }

    public void Die()
    {
        OnBossDeath?.Invoke();
        OnDeath?.Invoke();

        if(_droppables.Length > 0)
            DropGoods();

        if(CheckForValidRoom())
            _roomSO.deletedEnemies[_deletedIndex].boolean = true;
        if (dialogueTrigger)
            dialogueTrigger.TriggerDialogue();

        if (isBoss)
        {
            //_sprite.enabled = false;
            _collider.enabled = false;
            StartCoroutine(Generic.Fade(0f, _sprite, bossDeathTime, true));
            Destroy(gameObject, bossDeathTime);
        }
        else if (!_trail)
        { Destroy(gameObject); }
        else
        {
            _trail.emitting = false;
            _sprite.enabled = false;
            _collider.enabled = false;
            Destroy(gameObject, _trail.time);
        }
    }


    void DropGoods()
    {
        for (int i = 0; i < _droppables.Length; i++)
        {
            float rng = UnityEngine.Random.Range(0, 100);
            if (rng > _droppables[i]._odds)
                return;
            Vector3 _spawnPoint = Generic.SetRandomCircularPosition(transform.position, _dropplableSpawnPointOffset);
            Vector3 _rotation = new Vector3
                (0, 0, UnityEngine.Random.Range(0, 360));
            Instantiate(_droppables[i]._droppable, _spawnPoint,
                Quaternion.Euler(_rotation));
        }

        for(int i = 0; i < _pickUps.Length; i++)
        {
            print(string.Format("Spawning pick-up {0} of {1} in {2}", i, _pickUps.Length, gameObject));
            Vector3 _spawnPoint = Generic.SetRandomCircularPosition(transform.position, _dropplableSpawnPointOffset);
            Vector3 _rotation = Vector3.forward * UnityEngine.Random.Range(0, 360);
            Instantiate(_pickUps[i]._pickup, _spawnPoint, Quaternion.Euler(_rotation)).
                GetComponent<LastingPickUp>().SetRoomData(_roomSO, _pickUps[i].index);
        }
    }


    private void Start()
    {
        if (damageAnimation == DamageAnimation.Color)
            originalColor = _sprite.color;
        else
            originalSprite = _sprite.sprite;
        CheckIfSpawnable();
    }
}
