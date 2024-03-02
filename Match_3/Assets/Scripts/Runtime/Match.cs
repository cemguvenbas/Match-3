using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match
{
    private List<Matchable> matchables;
    public List<Matchable> Matchables => matchables;
    public int Count => matchables.Count;

    public Match()
    {
        matchables = new List<Matchable>(5);
    }

    public Match(Matchable original) : this()
    {
        AddMatchable(original);
    }

    public void AddMatchable(Matchable toAdd)
    {
        matchables.Add(toAdd);
    }

    public void Merge(Match toMerge)
    {
        matchables.AddRange(toMerge.Matchables);
    }

    public string ToString()
    {
        string s = "Match of type " +  matchables[0].Type + " : ";

        foreach (Matchable m in matchables)
        {
            s += "(" + m.position.x + ", " + m.position.y + ") ";
        }
        return s;
    }
}
