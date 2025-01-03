﻿using System.Collections;
using LevelSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BootstrapSystem
{
    public class GameRoot : MonoBehaviour
    {
        public static GameRoot Instance { get; private set; }
        
        [SerializeField] private LevelConfig _levelConfig;

        public LevelConfig LevelConfig => _levelConfig;

        private void Awake()
        {
            if (Instance != null)
            {
                LoadFirstScene();
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this);
        }

        private IEnumerator Start()
        {
#if UNITY_EDITOR
            if (string.IsNullOrEmpty(BootstrapInitiator.lastLoadScene) == false)
            {
                SceneManager.LoadScene(BootstrapInitiator.lastLoadScene);
                yield break;
            }
#endif
            
            LoadFirstScene();
            yield return null;
        }

        private void LoadFirstScene()
        {
            var nextLeve = _levelConfig.GetNextLevel(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(nextLeve);
        }
    }
}