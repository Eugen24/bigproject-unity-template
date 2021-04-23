using System.Collections;
using System.Collections.Generic;
using Template.Scripts.Utils.SettingsTemplate;
using UnityEngine;

public static class HapticHandler 
{
    public static void SmallHaptic()
    {
        if (SettingsHandler.IsHaptic)
        {
            //Vibration.VibratePeek();
        }
    }
}
