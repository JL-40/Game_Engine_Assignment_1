using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _Instance;

    [SerializeField] List<Sprite> _AsteroidSprites;

    // Start is called before the first frame update
    void Start()
    {
        if (_Instance != null)
        {
            Destroy(this);
        }
        _Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
