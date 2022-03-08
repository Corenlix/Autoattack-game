using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(IntRangeSliderAttribute))]
public class IntRangeSliderDrawer : PropertyDrawer {

    public override void OnGUI (
        Rect position, SerializedProperty property, GUIContent label
    ) {
        int originalIndentLevel = EditorGUI.indentLevel;
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(
            position, GUIUtility.GetControlID(FocusType.Passive), label
        );
        EditorGUI.indentLevel = 0;
        SerializedProperty minProperty = property.FindPropertyRelative("min");
        SerializedProperty maxProperty = property.FindPropertyRelative("max");
        float minValue = minProperty.intValue;
        float maxValue = maxProperty.intValue;
        float fieldWidth = position.width / 4f - 4f;
        float sliderWidth = position.width / 2f;
        position.width = fieldWidth;
        minValue = EditorGUI.FloatField(position, minValue);
        position.x += fieldWidth + 4f;
        position.width = sliderWidth;
        IntRangeSliderAttribute limit = attribute as IntRangeSliderAttribute;
        EditorGUI.MinMaxSlider(
            position, ref minValue, ref maxValue, limit.Min, limit.Max
        );
        position.x += sliderWidth + 4f;
        position.width = fieldWidth;
        maxValue = EditorGUI.FloatField(position, maxValue);
        if (minValue < limit.Min) {
            minValue = limit.Min;
        }
        if (maxValue < minValue) {
            maxValue = minValue;
        }
        else if (maxValue > limit.Max) {
            maxValue = limit.Max;
        }
        minProperty.intValue = (int)minValue;
        maxProperty.intValue = (int)maxValue;

        EditorGUI.EndProperty();
        EditorGUI.indentLevel = originalIndentLevel;
    }
}