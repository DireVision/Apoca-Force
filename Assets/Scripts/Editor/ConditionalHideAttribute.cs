using System.Collections;
using System;
using UnityEngine;

/*
 * The whole purpose of this script is construct our member variables for customisability.
 * It gets the inheritance from the PropertyAttribute class, so any extra options should go here
 * 
 * Source:
 * http://www.brechtos.com/hiding-or-disabling-inspector-properties-using-propertydrawers-within-unity-5/
 */
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class ConditionalHideAttribute : PropertyAttribute
{
    ///<summary>
    ///The name of the bool field that will be in control
    ///</summary>
    public string ConditionalSourceField = "";

    ///<summary>
    ///TRUE = Hide in inspector 
    ///FALSE = Disable in inspector 
    ///</summary>
    public bool HideInInspector = false;

    //By default, hide in inspector if no bool is passed in
    public ConditionalHideAttribute(string conditionalSourceField)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.HideInInspector = false;
    }

    //If argument "Hide In Inspector is passed, set hideininspector to the passed value.
    public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector)
    {
        this.ConditionalSourceField = conditionalSourceField;
        this.HideInInspector = hideInInspector;
    }
}
