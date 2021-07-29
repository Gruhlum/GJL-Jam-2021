using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Exile
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI versionGUI = default;

        [SerializeField] private GridLoader gridLoader = default;

        [SerializeField] private GameObject endScreen = default;
        [SerializeField] private TextMeshProUGUI lastLevelGUI = default;

        [SerializeField] private Player player;

        [SerializeField] private LevelList levels = default;
        private int currentLevel;
        public static bool showStartScreen = true;
        [SerializeField] private GameObject startScreen = default;

        private void Awake()
        {
            if (showStartScreen == false)
            {
                startScreen.gameObject.SetActive(false);
            }
            showStartScreen = false;
            versionGUI.text = Application.version;
            gridLoader.GridLoaded += GridLoader_GridLoaded;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
        }

        private void OnDisable()
        {
            gridLoader.GridLoaded -= GridLoader_GridLoaded;
            player.UnitDied -= Player_UnitDied;
        }

        private void GridLoader_GridLoaded(GridLoadData data)
        {
            if (player == null)
            {
                player = data.Player;
                player.UnitDied += Player_UnitDied;
            }
            currentLevel = levels.items.IndexOf(data.SavedGrid);
        }

        private void Player_UnitDied(TileUnit obj)
        {
            StartCoroutine(DisplayGameOver());
        }
        private IEnumerator DisplayGameOver()
        {
            GameController.blockingScripts.Add(this);
            lastLevelGUI.text = (1 + currentLevel).ToString();
            yield return new WaitForSeconds(1.25f);
            GameController.blockingScripts.Remove(this);
            endScreen.gameObject.SetActive(true);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(0);
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}