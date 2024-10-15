using GameDevHQ.FileBase.Plugins.FPS_Character_Controller;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Shoot _player;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Game Manager Null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        
    }

    public void GameOver()
    {
       if(_player != null)
        {
            _player.GetComponent<FPS_Controller>().enabled = false;
            _player.GetComponent<Shoot>().enabled = false;
        }
        Cursor.lockState = CursorLockMode.None;

    }

}
