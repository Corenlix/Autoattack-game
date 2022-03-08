using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class IntRange
{
    [SerializeField]
    int min, max;

    public int Min => min;

    public int Max => max;
	
    public int RandomValueInRange => Random.Range(min, max);

    public IntRange(int value) {
        min = max = value;
    }

    public IntRange (int min, int max) {
        this.min = min;
        this.max = max < min ? min : max;
    }
}

public class IntRangeSliderAttribute : PropertyAttribute {

    public int Min { get; private set; }

    public int Max { get; private set; }

    public IntRangeSliderAttribute (int min, int max) {
        Min = min;
        Max = max < min ? min : max;
    }
}
