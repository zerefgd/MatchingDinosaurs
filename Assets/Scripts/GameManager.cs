using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Board myboard;

    bool hasGameFinished, isFirstTurn;
    Card first;

    public void GameRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        myboard = new Board();
        hasGameFinished = false;
        isFirstTurn = true;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //if Game Has Finished
            if (hasGameFinished) return;

            //Raycase
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            //if not hit
            if (!hit.collider) return;

            if(hit.collider.CompareTag("Card"))
            {
                if (hit.collider.gameObject.GetComponent<Card>().hasTurnFinished) return;

                if(isFirstTurn)
                {
                    first = hit.collider.gameObject.GetComponent<Card>();
                    first.UpdateTurn();
                }
                else
                {
                    Card second = hit.collider.gameObject.GetComponent<Card>();
                    if(second.hasClicked)
                    {
                        first.RemoveTurn();
                        second.RemoveTurn();
                        isFirstTurn = !isFirstTurn;
                        return;
                    }
                    second.UpdateTurn();

                    if(first.dino == second.dino)
                    {
                        first.hasTurnFinished = true;
                        second.hasTurnFinished = true;
                        if(myboard.UpdateChoice())
                        {
                            hasGameFinished = true;
                            return;
                        }
                        isFirstTurn = !isFirstTurn;
                        return;
                    }

                    first.RemoveTurn();
                    second.RemoveTurn();
                }
                isFirstTurn = !isFirstTurn;
            }
        }
    }
}
