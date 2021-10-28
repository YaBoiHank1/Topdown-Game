using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] Canvas victoryCanvas;
    [SerializeField] Canvas deathCanvas;
    [SerializeField] Canvas UICanvas;
    [SerializeField] List<GameObject> capturePoints;
    [SerializeField] public int cappedPoints;
    [SerializeField] Text objectiveText;
    [SerializeField] AudioClip victory;
    public int pointsRemaining;


    // Start is called before the first frame update
    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        pointsRemaining = capturePoints.Count - cappedPoints;
        objectiveText.text = "Points Remaining: " + pointsRemaining;
        victoryCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        pointsRemaining = capturePoints.Count - cappedPoints;
        objectiveText.text = "Capture " + pointsRemaining + " Points";
        if (cappedPoints >= capturePoints.Count)
        {
            victoryCanvas.enabled = true;
            var hasPlayed = false;
            if (!hasPlayed)
            {
                AudioSource.PlayClipAtPoint(victory, Camera.main.transform.position);
                hasPlayed = true;
                Destroy(gameObject);
            }
            
            deathCanvas.enabled = false;
            UICanvas.enabled = false;
        }
        
    }

    
}
