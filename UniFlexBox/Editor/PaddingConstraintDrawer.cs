using System;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Feko.UniFlexBox
{
    [CustomPropertyDrawer(typeof(PaddingConstraint))]
    public class PaddingConstraintDrawer : PropertyDrawer
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
                EditorGUI.LabelField(labelRect, $"Padding Constraint {matches[0].Groups[1].Value}");
            }
            else
            {
                EditorGUI.LabelField(labelRect, label);
            }

            EditorGUI.indentLevel++;

            // Calculate rects
            var edgeRect = new Rect(position.x, position.y + lineHeight, position.width,
                EditorGUIUtility.singleLineHeight);
            var unitRect = new Rect(position.x, position.y + 2 * lineHeight, position.width,
                EditorGUIUtility.singleLineHeight);
            var valueRect = new Rect(position.x, position.y + 3 * lineHeight, position.width,
                EditorGUIUtility.singleLineHeight);

            // Get properties
            SerializedProperty edgeProperty = property.FindPropertyRelative(nameof(PaddingConstraint.Edge));
            SerializedProperty unitProperty = property.FindPropertyRelative(nameof(PaddingConstraint.Unit));
            SerializedProperty valueProperty = property.FindPropertyRelative(nameof(PaddingConstraint.Value));

            edgeProperty.enumValueIndex =
                (int)(YGEdge)EditorGUI.EnumPopup(
                    edgeRect,
                    nameof(PaddingConstraint.Edge),
                    (YGEdge)edgeProperty.enumValueIndex);

            var unit = (ConstraintUnit)unitProperty.enumValueIndex;

            if (unit != ConstraintUnit.Points && unit != ConstraintUnit.Percent)
            {
                unitProperty.enumValueIndex = (int)ConstraintUnit.Points; // Default to Pixels
            }

            string[] enumValues = Enum.GetNames(typeof(ConstraintUnit));
            string[] unitOptions = { ConstraintUnit.Points.ToString(), ConstraintUnit.Percent.ToString() };

            int selectedIndex = Array.IndexOf(unitOptions, enumValues[unitProperty.enumValueIndex]);
            int newSelectedIndex =
                EditorGUI.Popup(unitRect, nameof(PaddingConstraint.Unit), selectedIndex, unitOptions);
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