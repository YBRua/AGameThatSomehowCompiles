using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject[] _systemPrefabs;
    private List<GameObject> _initializedSystemPrefabs;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        for(int i = 0; i < _systemPrefabs.Length; ++i)
        {
            Instantiate(_systemPrefabs[i]).SetActive(true);
        }
    }

    public void SwitchScene(string nextScene)
    {
        SceneManager.LoadScene(nextScene);
    }
}
