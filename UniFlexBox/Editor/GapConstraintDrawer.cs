using System;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Feko.UniFlexBox
{
    [CustomPropertyDrawer(typeof(GapConstraint))]
    public class GapConstraintDrawer : PropertyDrawer
    {
        private static readonly Regex _arrayIndexRegex = new Regex(@"^.*\.Array\..*\[(\d+)\]$");

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Start the property drawer
            EditorGUI.BeginProperty(position, label, property);

            float lineHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            MatchCollection matches = _arrayIndexRegex.Matches(property.propertyPath);
            var labelRect = new Rect(position.x, position.y, position.width, lineHeight);
            if (matches.Count > 0)
            {
                EditorGUI.LabelField(labelRect, $"Gap Constraint {matches[0].Groups[1].Value}");
            }
            else
            {
                EditorGUI.LabelField(labelRect, label);
            }

            EditorGUI.indentLevel++;

            // Calculate rects
            var gutterRect = new Rect(position.x, position.y + lineHeight, position.width,
                EditorGUIUtility.singleLineHeight);
            var unitRect = new Rect(position.x, position.y + 2 * lineHeight, position.width,
                EditorGUIUtility.singleLineHeight);
            var valueRect = new Rect(position.x, position.y + 3 * lineHeight, position.width,
                EditorGUIUtility.singleLineHeight);

            // Get properties
            SerializedProperty gutterProperty = property.FindPropertyRelative(nameof(GapConstraint.Gutter));
            SerializedProperty unitProperty = property.FindPropertyRelative(nameof(GapConstraint.Unit));
            SerializedProperty valueProperty = property.FindPropertyRelative(nameof(GapConstraint.Value));

            gutterProperty.enumValueIndex =
                (int)(YGGutter)EditorGUI.EnumPopup(
                    gutterRect,
                    nameof(GapConstraint.Gutter),
                    (YGGutter)gutterProperty.enumValueIndex);

            var unit = (ConstraintUnit)unitProperty.enumValueIndex;

            if (unit != ConstraintUnit.Pixels && unit != ConstraintUnit.Percent)
            {
                unitProperty.enumValueIndex = (int)ConstraintUnit.Pixels; // Default to Pixels
            }

            string[] enumValues = Enum.GetNames(typeof(ConstraintUnit));
            string[] unitOptions = { ConstraintUnit.Pixels.ToString(), ConstraintUnit.Percent.ToString() };

            int selectedIndex = Array.IndexOf(unitOptions, enumValues[unitProperty.enumValueIndex]);
            int newSelectedIndex =
                EditorGUI.Popup(unitRect, nameof(GapConstraint.Unit), selectedIndex, unitOptions);
            unitProperty.enumValueIndex = Array.IndexOf(enumValues, unitOptions[newSelectedIndex]);

            EditorGUI.PropertyField(valueRect, valueProperty);

            EditorGUI.indentLevel--;

            // End the property drawer
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 4
                   + EditorGUIUtility.standardVerticalSpacing * 3;
        }
    }
}