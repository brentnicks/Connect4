using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerVsAi : MonoBehaviour
{
    public GameObject player1Disc;
    public GameObject player2Disc;
    public GameObject[] spawners;
    public GameObject player1Choice;
    public GameObject player2Choice;
    public TextMeshProUGUI text;
    public GameObject MainMenuButton;
    public GameObject PlayAgainButton;
    GameObject fallingDisc;
    bool isPlayer1Turn = true;
    static int length = 7;
    static int height = 6;
    float delay = 2.0f;
    bool gameOver = false;
    int aiChoice;

    int[,] State = new int [length,height];
    private void Start()
    {
        Time.timeScale = 1;
        isPlayer1Turn = true;
        player1Choice.SetActive(false);
        player2Choice.SetActive(false);
        gameOver = false;
        delay = 2.0f;
        int[,] State = new int[length, height];
    }

    public void HoverColumn(int column)
    {
        if ((State[column - 1, height - 1] == 0 && (fallingDisc == null || fallingDisc.GetComponent<Rigidbody>().velocity == Vector3.zero)) && gameOver == false)
        {
            if (isPlayer1Turn)
            {
                player1Choice.SetActive(true);
                player1Choice.transform.position = spawners[column - 1].transform.position;
            }
            else
            {
                //player2Choice.SetActive(true);
                //player2Choice.transform.position = spawners[column - 1].transform.position;
            }
        }
    }

    public void ColumnSelect(int column)
    {
        if ((fallingDisc == null || fallingDisc.GetComponent<Rigidbody>().velocity == Vector3.zero) && gameOver == false)
        {
            if (UpdateState(column))
            {
                player1Choice.SetActive(false);
                player2Choice.SetActive(false);
                if (isPlayer1Turn)
                {
                    fallingDisc = Instantiate(player1Disc, spawners[column - 1].transform.position, Quaternion.identity);
                    fallingDisc.GetComponent<Rigidbody>().velocity = new Vector3(0, 0.1f, 0);
                    isPlayer1Turn = false;

                    if (Win(1))
                    {
                        text.text = "Player 1 Wins!";
                        gameOver = true;
                    }
                }
                StartCoroutine(newTimer());

                /*if (!isPlayer1Turn)
                {
                    aiChoice = Random.Range(1, 7);
                    while (!UpdateState(aiChoice))
                    {
                        aiChoice = Random.Range(1, 7);
                    }
                    fallingDisc = Instantiate(player2Disc, spawners[aiChoice - 1].transform.position, Quaternion.identity);
                    fallingDisc.GetComponent<Rigidbody>().velocity = new Vector3(0, 0.1f, 0);
                    isPlayer1Turn = true;

                    if (Win(2))
                    {
                        text.text = "Player 2 Wins!";
                        gameOver = true;
                    }
                }*/

                if(Draw())
                {
                    text.text = "Draw!";
                    gameOver = true;
                }
            }
        }
    }

    bool UpdateState(int column)
    {
        for (int row = 0; row < height; row++)
        {
            if (State[column - 1, row] == 0)
            {
                if (isPlayer1Turn)
                {
                    State[column - 1, row] = 1;
                }
                else
                {
                    State[column - 1, row] = 2;
                }
                return true;
            }
        }
        return false;
    }

    bool Win(int player)
    {
        for (int column = 0; column < length - 3; column++)
        {
            for (int row = 0; row < height; row++)
            {
                if (State[column, row] == player && State[column + 1, row] == player && State[column + 2, row] == player && State[column + 3, row] == player)
                {
                    return true;
                }
            }
        }

        for (int column = 0; column < length; column++)
        {
            for (int row = 0; row < height - 3; row++)
            {
                if (State[column, row] == player && State[column, row + 1] == player && State[column, row + 2] == player && State[column, row + 3] == player)
                {
                    return true;
                }
            }
        }

        for (int column = 0; column < length - 3; column++)
        {
            for (int row = 0; row < height - 3; row++)
            {
                if (State[column, row] == player && State[column + 1, row + 1] == player && State[column + 2, row + 2] == player && State[column + 3, row + 3] == player)
                {
                    return true;
                }
            }
        }

        for (int column = 0; column < length - 3; column++)
        {
            for (int row = 0; row < height - 3; row++)
            {
                if (State[column, row + 3] == player && State[column + 1, row + 2] == player && State[column + 2, row + 1] == player && State[column + 3, row] == player)
                {
                    return true;
                }
            }
        }
        return false;
    }

    bool Draw()
    {
        for (int column = 0; column < length; column++)
        {
            if (State[column, height -1] == 0)
            {
                return false;
            }
        }
        return true;
    }

    private void Update()
    {
        if (gameOver == true)
        {
            if (delay > 0)
            {
                delay -= Time.deltaTime;
            }
            if (delay <= 0)
            {
                Time.timeScale = 0;
                PlayAgainButton.SetActive(true);
                MainMenuButton.SetActive(true);

            }
        }
    }

    IEnumerator newTimer()
    {
        yield return new WaitForSeconds(1.8f);
        aiChoice = Random.Range(1, 7);
        while (!UpdateState(aiChoice))
        {
            aiChoice = Random.Range(1, 7);
        }
        fallingDisc = Instantiate(player2Disc, spawners[aiChoice - 1].transform.position, Quaternion.identity);
        fallingDisc.GetComponent<Rigidbody>().velocity = new Vector3(0, 0.1f, 0);
        isPlayer1Turn = true;

        if (Win(2))
        {
            text.text = "Player 2 Wins!";
            gameOver = true;
        }
    }
}
