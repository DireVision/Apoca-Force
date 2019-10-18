#if UNITY_EDITOR

/* To use this property drawer, label [ConditionalHide("HeaderName", Hide in Inspector True/False)]
   Then declare values under it
   */

using UnityEngine;
using UnityEditor;

/* This script is used to draw the the variables depending on the HideInInspector variable passed.
 * If any attributes need to be added, edit ConditionalHideAttribute.cs
 * */
    [CustomPropertyDrawer(typeof(ConditionalHideAttribute))]
    public class ConditionalHidePropertyDrawer : PropertyDrawer
    {
        //Fetch the properties that are suppposed to show, and check whether the stuff contained should be showing. If so, display them.
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //Cache the attribute and get the hideininspector variable
            ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
            bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

            //Enable the GUI Element if HideInInspector != true
            bool wasEnabled = GUI.enabled;
            GUI.enabled = enabled;
            if (!condHAtt.HideInInspector || enabled)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }

            //Reflect the enable status on the GUI Element
            GUI.enabled = wasEnabled;
        }

        /*Calculate the height of our property so that(when the property needs to be hidden) the following properties that are being drawn don’t overlap
         * Returns the vertical spacing needed to draw said property, else disables the spacing reserved for said property.
         */
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ConditionalHideAttribute condHAtt = (ConditionalHideAttribute)attribute;
            bool enabled = GetConditionalHideAttributeResult(condHAtt, property);

            if (!condHAtt.HideInInspector || enabled)
            {
                return EditorGUI.GetPropertyHeight(property, label);
            }
            else
            {
                return -EditorGUIUtility.standardVerticalSpacing;
            }
        }

        //This bool returns whether the tray should be active or not for each individual property.
        private bool GetConditionalHideAttributeResult(ConditionalHideAttribute condHAtt, SerializedProperty property)
        {
            //Grab the property path and serializes it so that it can be shown in Inspector
            bool enabled = true;
            string propertyPath = property.propertyPath; //returns the property path of the property we want to apply the attribute to
            string conditionPath = propertyPath.Replace(property.name, condHAtt.ConditionalSourceField); //changes the path to the conditionalsource property path
            SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

            if (sourcePropertyValue != null)
            {
                enabled = sourcePropertyValue.boolValue;
            }
            else
            {
                Debug.LogWarning("Attempting to use a ConditionalHideAttribute but no matching SourcePropertyValue found in object: " + condHAtt.ConditionalSourceField);
            }

            return enabled;
        }
    }
#endif