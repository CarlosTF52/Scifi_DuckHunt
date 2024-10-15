using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreCount;

    [SerializeField]
    private TextMeshProUGUI _enemyCount;

    [SerializeField]
    private TextMeshProUGUI _timerCount;

    [SerializeField]
    private float _timerCountTotal;

    [SerializeField]
    private TextMeshProUGUI _winText;

    private bool _stopTimer;

    private float _winTimer = 2.0f;

    private int _enemyCountTotal;

    private static UIManager _instance;

    public static UIManager Instance

  
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager Null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

    }

    private void Update()
    {
        if (!_stopTimer)
        {
            _timerCountTotal += Time.deltaTime;
            _timerCount.text = _timerCountTotal.ToString();
        }
        if( _stopTimer )
        {
            _winTimer -= Time.deltaTime;
        }
        if(_winTimer < 0)
        {
            _winText.gameObject.SetActive(true);
        }
       
    }

    private void Start()
    {
        _winText.gameObject.SetActive(false);
    }

    public void UpdateScore(int playerScore)
    {
        _scoreCount.text = playerScore.ToString();
    }

    public void SetEnemyCount(int enemyCountTotal)
    {
        _enemyCountTotal = enemyCountTotal;
        _enemyCount.text = enemyCountTotal.ToString(); 
    }

    public void UpdateEnemyCount(int enemyCount)
    {
        enemyCount = enemyCount + _enemyCountTotal;
        if(enemyCount < 0)
        {
            enemyCount = 0;
        }
        _enemyCount.text = enemyCount.ToString();
    }

    public void Win()
    {
      
      GameManager.Instance.GameOver();
      _stopTimer = true;
}

    

}
