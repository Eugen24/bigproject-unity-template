using NUnit.Framework;
using Template.Scripts.Signal;

namespace Template.Scripts.UI.Progress.Editor
{
    public class UiProgressTest
    {
        
        [Test]
        public void SimpleInitAndSendSignal()
        {
            var views =  EditorUtils.Editor.EditorUtils.GetAllInstancesMonoByName<UiProgressView>("Progress-Canvas");
            foreach (var view in views)
            {
                UnityEngine.Object.Instantiate(view);
                /*
                Signal<UiProgressData>.Fire(new UiProgressData
                {
                    CurrentLevel = 0,
                    NextLevel = 1,
                    IsActiveView = true,
                    ProgressProc = 0.1f,
                });
            
                Signal<UiProgressData>.Fire(new UiProgressData
                {
                    CurrentLevel = 1,
                    NextLevel = 2,
                    IsActiveView = false,
                    ProgressProc = 0.1f,
                });
                */
            }

        }

    }
}
