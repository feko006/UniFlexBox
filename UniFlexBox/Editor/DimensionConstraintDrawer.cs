using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Feko.UniFlexBox
{
    [CustomPropertyDrawer(typeof(DimensionConstraint))]
    public class DimensionConstraintDrawer : PropertyDrawer
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
                EditorGUI.LabelField(labelRect, $"Dimension Constraint {matches[0].Groups[1].Value}");
            }
            else
            {
                EditorGUI.LabelField(labelRect, label);
            }

            EditorGUI.indentLevel++;

            // Calculate rects
            var typeRect = new Rect(position.x, position.y + lineHeight, position.width,
                EditorGUIUtility.singleLineHeight);
            var unitRect = new Rect(position.x, position.y + 2 * lineHeight, position.width,
                EditorGUIUtility.singleLineHeight);
            var valueRect = new Rect(position.x, position.y + 3 * lineHeight, position.width,
                EditorGUIUtility.singleLineHeight);

            // Get properties
            SerializedProperty typeProperty = property.FindPropertyRelative(nameof(DimensionConstraint.Type));
            SerializedProperty unitProperty = property.FindPropertyRelative(nameof(DimensionConstraint.Unit));
            SerializedProperty valueProperty = property.FindPropertyRelative(nameof(DimensionConstraint.Value));

            // Draw ConstraintType dropdown
            EditorGUI.PropertyField(typeRect, typeProperty);

            // Conditional logic for hiding/displaying Unit and Value
            var type = (ConstraintType)typeProperty.enumValueIndex;
            var unit = (ConstraintUnit)unitProperty.enumValueIndex;

            // Modify Unit dropdown to exclude Auto based on type
            if (type != ConstraintType.ExactHeight && type != ConstraintType.ExactWidth)
            {
                if (unit == ConstraintUnit.Auto)
                {
                    unitProperty.enumValueIndex = (int)ConstraintUnit.Points; // Default to Pixels
                }

                // Disable Auto option
                string[] enumValues = Enum.GetNames(typeof(ConstraintUnit));
                string[] unitOptions = enumValues
                    .Where(name => name != ConstraintUnit.Auto.ToString())
                    .ToArray();

                int selectedIndex = Array.IndexOf(unitOptions, enumValues[unitProperty.enumValueIndex]);
                int newSelectedIndex =
                    EditorGUI.Popup(unitRect, nameof(DimensionConstraint.Unit), selectedIndex, unitOptions);
                unitProperty.enumValueIndex = Array.IndexOf(enumValues, unitOptions[newSelectedIndex]);
            }
            else
            {
                EditorGUI.PropertyField(unitRect, unitProperty);
            }

            // Draw Value field unless the unit is Stretch or the type is Auto
            if (ShouldValueBeShown(unit))
            {
                EditorGUI.PropertyField(valueRect, valueProperty);
            }

            EditorGUI.indentLevel--;

            // End the property drawer
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty unitProperty = property.FindPropertyRelative(nameof(DimensionConstraint.Unit));

            var unit = (ConstraintUnit)unitProperty.enumValueIndex;

            float height = EditorGUIUtility.singleLineHeight * 3
                           + EditorGUIUtility.standardVerticalSpacing * 2;

            if (ShouldValueBeShown(unit))
            {
                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing; // For Value
            }

            return height;
        }

        private static bool ShouldValueBeShown(ConstraintUnit unit)
        {
            return unit == ConstraintUnit.Points || unit == ConstraintUnit.Percent;
        }
    }
}