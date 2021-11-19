using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [HideInInspector]
    public bool hasClicked, hasTurnFinished;

    [SerializeField]
    int index;

    [HideInInspector]
    public int dino;

    [SerializeField]
    Sprite unrevealed;

    [SerializeField]
    List<Sprite> dinos;

    SpriteRenderer renderer;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        hasClicked = false;
        hasTurnFinished = false;
        dino = GameManager.instance.myboard.GetIndex(index);
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        renderer.sprite = unrevealed;
    }

    public void UpdateTurn()
    {
        hasClicked = true;
        //UpdateImage();
        animator.Play("Reveal", -1, 0f);
    }

    public void UpdateImage()
    {
        renderer.sprite = dinos[dino];
    }

    public void RemoveTurn()
    {
        hasClicked = false;
        //RemoveImage();
        animator.Play("Unreveal", -1, 0f);
    }

    public void RemoveImage()
    {
        renderer.sprite = unrevealed;
    }
}
