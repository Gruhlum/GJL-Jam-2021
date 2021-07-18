using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exile
{
	public class PlayerDataDisplay : MonoBehaviour
	{
        [SerializeField] private PlayerData playerData = default;

        [SerializeField] private IntDisplay[] displays;

        private void Start()
        {
            if (playerData != null)
            {
                displays[0].Setup(playerData.Health);
                displays[1].Setup(playerData.Bombs);
                displays[2].Setup(playerData.Coins);
            }
        }
    }
}