using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance; // ½Ì±ÛÅæ
    public static CharacterManager Instance  // ÇÁ·ÎÆÛÆ¼·Î Ä¸½¶È­
    {  
        get
        { 
            if (_instance == null) 
            { 
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return _instance; 
        } 
    }

    private Player _player; // ÇÁ·ÎÆÛÆ¼·Î Ä¸½¶È­
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance=this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(_instance == this)
            {
                Destroy(gameObject);
            }
        }
    }
}
