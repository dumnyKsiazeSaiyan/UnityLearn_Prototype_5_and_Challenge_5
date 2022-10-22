using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private TextMeshProUGUI scoreText;

    public float spawnRate = 1.0f;
    private int score;



    private void Start()
    {
        StartCoroutine(SpawnTarget());
        scoreText = GameObject.Find("Canvas").GetComponentInChildren<TextMeshProUGUI>();
        score = 0;
        UpdateScore(0);


    }



    IEnumerator SpawnTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
}
