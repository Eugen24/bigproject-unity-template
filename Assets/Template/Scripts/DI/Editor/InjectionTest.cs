using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Template.Scripts.DI.Editor
{
    public class InjectionTest
    {
        [Test]
        public void TestInjection()
        {
            var installer = new GeneralInstaller();
            var to = new GameObject().AddComponent<ToInject_Test>();
            var intoInject = new GameObject().AddComponent<InToInject_Test>();
            
            installer.Inject(intoInject, new List<System.Object>
            {
                to
            });
            
            Assert.IsNotNull(intoInject.injectTest);
        }
        
        [Test]
        public void TestGet()
        {
            var installer = new GeneralInstaller();
            var to = new GameObject().AddComponent<ToInject_Test>();
            var intoInject = new GameObject().AddComponent<InToInject_Test>();
            var comp = intoInject.gameObject.AddComponent<GetCompoenent_Test>();
            
            installer.Inject(intoInject, new List<System.Object>
            {
                to
            });
            
            Assert.AreEqual(intoInject.injectTest, to);
            Assert.AreEqual(intoInject.Com, comp);
        }
        
                
        [Test]
        public void TestGetChild()
        {
            var installer = new GeneralInstaller();
            var to = new GameObject().AddComponent<ToInject_Test>();
            var intoInject = new GameObject().AddComponent<InToInject_Test>();
            var comp = intoInject.gameObject.AddComponent<GetCompoenent_Test>();
            var comp2 = new GameObject().AddComponent<GetCompoenentChild_Test>();
            comp2.transform.parent = comp.transform;
            
            installer.Inject(intoInject, new List<System.Object>
            {
                to
            });
            
            Assert.AreEqual(intoInject.injectTest, to);
            Assert.AreEqual(intoInject.Com, comp);
            Assert.AreEqual(intoInject.ComChild, comp2);
        }
        
        

    }
}
