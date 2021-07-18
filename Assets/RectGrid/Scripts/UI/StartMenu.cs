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

        private int count = 0;

        [SerializeField] private Player player;

        private void Awake()
        {
            versionGUI.text = Application.version;
            gridLoader.GridLoaded += GridLoader_GridLoaded;
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
            count++;
        }

        private void Player_UnitDied(TileUnit obj)
        {
            lastLevelGUI.text = count.ToString();
            endScreen.gameObject.SetActive(true);
        }

        public void StartGame()
        {

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