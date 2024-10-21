using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController player;

    [SerializeField] Sprite _PlayerMoveSprite;
    [SerializeField] Sprite _PlayerDefaultSprite;
    bool _isMoving = false;

    [SerializeField] GameObject _BulletSpawn;
    [SerializeField] GameObject _BulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (player != null)
        {
            Destroy(this);
        }
        player = this;

        if(_PlayerDefaultSprite == null)
        {
            _PlayerDefaultSprite = GetComponent<SpriteRenderer>().sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMoving)
        {
            GetComponent<SpriteRenderer>().sprite = _PlayerMoveSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = _PlayerDefaultSprite;
        }
    }
}
