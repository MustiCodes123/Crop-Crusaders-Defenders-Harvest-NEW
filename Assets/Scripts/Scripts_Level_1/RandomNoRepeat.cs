using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RandomNoRepeat
{
    private List<int> values;
    private int currentIndex;

    public RandomNoRepeat(int minValue, int maxValue)
    {
        // Initialize the list with values from minValue to maxValue (inclusive)
        values = new List<int>();
        for (int i = minValue; i <= maxValue; i++)
        {
            values.Add(i);
        }

        // Shuffle the list to get a random order
        Shuffle(values);

        currentIndex = 0;
    }

    public int GetNextValue()
    {
        if (currentIndex >= values.Count)
        {
            // If we've used all the values, reshuffle the list
            Shuffle(values);
            currentIndex = 0;
        }

        // Get the next value from the shuffled list
        int nextValue = values[currentIndex];
        currentIndex++;

        return nextValue;
    }

    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
