using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NultBolts
{
    public class MenuManager : MonoBehaviour
    {
        private void Awake()
        {
            //PlayerPrefs.DeleteAll();
        }
        public void LoadGame()
        {
            SceneManager.LoadScene("NultsBoltsScene");
        }
    }
}
