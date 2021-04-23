using System;
using System.Collections.Generic;
using Castle.DynamicProxy.Internal;
using UnityEngine;

namespace Template.Scripts.DI
{
    public class GeneralInstaller
    {
        public void Inject(InjectedMono to, List<System.Object> injection)
        {
            var fields = to.GetType().GetAllFields();
            foreach (var info in fields)
            {
                if (info != null)
                {
                    if (info.GetCustomAttributes(typeof(In), false).Length > 0)
                    {
                        foreach (var inj in injection)
                        {
                            if (inj != null)
                            {
                                if (info.FieldType == inj.GetType())
                                {
                                    info.SetValue(to, inj);
                                    break;
                                }
                            }
                        }
                    }
                    else if (info.GetCustomAttributes(typeof(Get), false).Length > 0)
                    {
                        var g = to;
                        if (g != null)
                        {
                            var type = info.FieldType;
                            var c = g.GetComponent(type);
                            info.SetValue(to, c);
                        }
                    }
                    else if (info.GetCustomAttributes(typeof(GetChild), false).Length > 0)
                    {
                        var g = to;
                        if (g != null)
                        {
                            var type = info.FieldType;
                            var c = g.GetComponentInChildren(type, true);
                            info.SetValue(to, c);
                        }
                    }
                }
            }
        }
    }
    
    public class In : Attribute
    {
        
    }

    public class Get : Attribute
    {
        
    }
    
    public class GetChild : Attribute
    {
        
    }
    
    //need for test
    #if UNITY_EDITOR
    public class ToInject_Test : MonoBehaviour
    {
            
    }
        
    public class GetCompoenent_Test : MonoBehaviour
    {
            
    }
    
    public class GetCompoenentChild_Test : MonoBehaviour
    {
            
    }
        
    public class InToInject_Test : InjectedMono
    {
        [In] public ToInject_Test injectTest;
        [Get] public GetCompoenent_Test Com;
        [GetChild] public GetCompoenentChild_Test ComChild;
    }
    #endif
    
}
