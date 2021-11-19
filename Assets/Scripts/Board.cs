using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    int matches;
    List<int> choices;

    public Board()
    {
        matches = 0;
        choices = new List<int>();

        for(int i = 0; i < 6; i++)
        {
            choices.Add(i);
            choices.Add(i);
        }

        for(int i = 0; i < choices.Count; i++)
        {
            int temp = choices[i];
            int index = Random.Range(0, choices.Count);
            choices[i] = choices[index];
            choices[index] = temp;
        }
    }

    public int GetIndex(int index)
    {
        return choices[index];
    }

    public bool UpdateChoice()
    {
        matches++;
        return matches == 6;
    }
    
}
