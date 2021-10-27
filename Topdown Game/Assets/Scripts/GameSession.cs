using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] List<GameObject> capturePoints;
    [SerializeField] public int cappedPoints;
    [SerializeField] Text objectiveText;
    public int pointsRemaining;

    // Start is called before the first frame update
    void Start()
    {
        pointsRemaining = capturePoints.Count - cappedPoints;
        objectiveText.text = "Points Remaining: " + pointsRemaining;
    }

    // Update is called once per frame
    void Update()
    {
        pointsRemaining = capturePoints.Count - cappedPoints;
        objectiveText.text = "Capture " + pointsRemaining + " Points";
        if (cappedPoints >= capturePoints.Count)
        {
            SceneManager.LoadScene(0);
        }
    }
}
