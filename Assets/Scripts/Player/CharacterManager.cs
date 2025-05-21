using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance; // �̱���
    public static CharacterManager Instance  // ������Ƽ�� ĸ��ȭ
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

    private Player _player; // ������Ƽ�� ĸ��ȭ
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
