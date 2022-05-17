using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Sanicball.Data;
using SanicballCore;

namespace Sanicball.UI
{
    public class WaitingUI : MonoBehaviour
    {
        [SerializeField]
        private Text stageNameField;

        [SerializeField]
        private Text infoField;
        [SerializeField]
        private CanvasGroup controlsPanel;

        private void Start()
        {
            controlsPanel.alpha = ActiveData.GameSettings.showControlsWhileWaiting ? 1 : 0;
        }

        private void Update()
        {
            if (GameInput.IsRespawning(ControlType.Joystick1))
            {
                ActiveData.GameSettings.showControlsWhileWaiting = !ActiveData.GameSettings.showControlsWhileWaiting;
            }

            controlsPanel.alpha = Mathf.Lerp(controlsPanel.alpha, ActiveData.GameSettings.showControlsWhileWaiting ? 1 : 0, Time.deltaTime * 20);
        }

        public string StageNameToShow
        {
            set
            {
                stageNameField.text = value;
            }
        }

        public string InfoToShow
        {
            set
            {
                infoField.text = value;
            }
        }
    }
}