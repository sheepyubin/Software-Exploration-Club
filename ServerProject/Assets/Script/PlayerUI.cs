using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace Com.MyCompany.MyGame
{
    public class PlayerUI : MonoBehaviour
    {
        #region Private Fields

        [Tooltip("UI Text to display Player's Name")]
        [SerializeField]
        private TMP_Text playerNameText;

        [Tooltip("UI Slider to display Player's Health")]
        [SerializeField]
        private Slider playerHealthSlider;

        #endregion

        #region MonoBehaviour CallBacks

        #endregion

        #region Public Methods

        #endregion

    }
}
