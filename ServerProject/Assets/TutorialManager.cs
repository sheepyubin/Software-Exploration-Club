using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI tutorialText;
    public Movement_Tutorial movement;
    public RopeLauncher_Tutorial ropeLauncher;

    private void Update()
    {
        if (movement.step1)
            tutorialText.text = "Press 'L Shift' to 'Dash'";
        if (movement.step2)
            tutorialText.text = "Press 'Space Bar' to 'Jump'";
        if (movement.step3)
            tutorialText.text = "Click 'Mouse' to 'Use Rope'";
        if (ropeLauncher.step4)
            tutorialText.text = "Hold 'Mouse' and Press 'Space Bar' to 'Climb'";
        if (movement.step5)
            tutorialText.text = "Reach EndPoint";
    }
}
