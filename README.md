# Template for creating HighProject Games Easy

- Download Package
Get latest release here: https://github.com/Eugen24/highproject-template/releases/tag/0.2

## General Conventions
#### Project Conventions:
- Every project should be in Assets/_Project
- In Build settings first scene should be FirstScene.unity
- Every scene should have System.prefab > Note! Only if you are developing Small Non Multiplayer Game
- All scriptable objects should be in Assets/_Project/Scripts/Configs
- Git ignore: https://github.com/Eugen24/highproject-template/blob/main/.gitignore

## Library includes:
- DI 
- Signals
- Scene progression 
- Pool
- Default SDKs for publishing (Facebook, GA, etc...)
- Other small tools for math and animations
- Easy Save - The Complete Save Data & Serialization Tool > Guides To Getting started: https://docs.moodkie.com/product/easy-save-3/es3-guides/
- Effects

## DI
#### Description:
This is simple dependency-injection tool to fit hyper-casual use.
As reference is used Zenject, but is developed with the idea 'Simple-Zenject'.

#### How to use
There is prefab System (Assets/_Project/Prefabs/Core/System.prefab). 
This prefab is present in all scenes. All scripts that are attached to this gameObject(and children) 
is consider Singletons and will be Injected in all scripts in scene (MonoBehaviours).

For example: there is AdsSystem.cs, 
in order to get it from other script, the script requires to 
inherit from InjectedMono.

For example open sample scene in Template/Samples/DI/DI.unity.
In System, there is InputSystem. InputSystem is responsive for Handling Inputs. 
In Player, there is PlayerController, InputSystem is injected in PlayerController.

```c#
using Template.Scripts.DI;
using UnityEngine;

namespace Template.Samples.DI
{
    public class PlayerController : InjectedMono
    {
        [In] private InputSystem _inputSystem;
        void Update()
        {
            if (_inputSystem.InputData.IsUp)
            {
                transform.position += Vector3.up * Time.deltaTime;
            }
            
            if (_inputSystem.InputData.IsDown)
            {
                transform.position += Vector3.down * Time.deltaTime;
            }
            
            if (_inputSystem.InputData.IsLeft)
            {
                transform.position += Vector3.left * Time.deltaTime;
            }
            
            if (_inputSystem.InputData.IsRight)
            {
                transform.position += Vector3.right * Time.deltaTime;
            }
        }
    }
}
```

Attribute [In] is responsive for InjectionFlag.

Try to avoid using Awake. It might give nullref.

This script also applies to Instantiate.

As additional tool, there is attribute [Get], it makes a GetComponent during Injection. Example (Also available in DI scene):

```c#
using Template.Scripts.DI;
using UnityEngine;

namespace Template.Samples.DI
{
    public class GetRigidbody : InjectedMono
    {
        [Get] private Rigidbody _rigidbody;

        private void Start()
        {
            Debug.Log(_rigidbody.name);
        }
    }
}
```

[GetChild] is for GetComponentInChildren.

In case you need a Singleton-Injection but for some reasons it's not reasonable to attached it to System Prefab. You can use Single, example is in Template/Samples/DI/SingleDI.unity.
You just need to inherit from SingleMono. 

In System, there is InputSystem(previous one). And there are 2 more scripts:

```c#
using Template.Scripts.DI;
using UnityEngine;

namespace Template.Samples.DI
{
    public class PlayerSingle : SingleMono
    {
        public void MoveRight()
        {
            transform.position += Vector3.right * Time.deltaTime;
        }

        public void MoveLeft()
        {
            transform.position += Vector3.left * Time.deltaTime;
        }

        public void MoveUp()
        {
            transform.position += Vector3.up * Time.deltaTime;
        }

        public void MoveDown()
        {
            transform.position += Vector3.down * Time.deltaTime;
        }
        
    }
}
```

and

```c#
using System;
using Template.Scripts.DI;
using UnityEngine;

namespace Template.Samples.DI
{
    public class PlayerMoveController : InjectedMono
    {
        [In] private InputSystem _inputSystem;
        [In] private PlayerSingle _player;

        private void Update()
        {
            if (_inputSystem.InputData.IsUp)
            {
                _player.MoveUp();
            }
            
            if (_inputSystem.InputData.IsDown)
            {
                _player.MoveDown();
            }
            
            if (_inputSystem.InputData.IsLeft)
            {
                _player.MoveLeft();
            }
            
            if (_inputSystem.InputData.IsRight)
            {
                _player.MoveRight();
            }
        }
    }
}
```

You CAN NOT attach SingleMono ot System prefab!!!

#
In case you need an ordered events, for example you want to fire a signal at start but you want to be sure that everybody is subscribe. It is possible to override OnSyncStart and OnSyncAfterStart, here is example:
```c#
public class SyncExample : InjectedMono
{
    public override void OnSyncStart()
    {
        Debug.Log("OnSyncStart");
    }

    public override void OnSyncAfterStart()
    {
        Debug.Log("OnSyncAfterStart");
    }
}

```

In case of Instantiate, this methods will be called after instantiating

## Signals

#### Description:

Signals are events where you can add additional data.

#### How to use

First of all you need to inject SignalBus. You just need to inherit from InjectedMono. 
And you will get _signalBus variable.

You can open Template/Samples/Signals/Signals.unity.

Example of sending event:
```c#
using Template.Scripts.DI;
using UnityEngine;

namespace Template.Samples.Signals
{
    public class InputSignalSystem : InjectedMono
    {
        private void Update()
        {
            if (Input.anyKey)
            {
                var signalData = new InputSignalData();
                signalData.IsUp = Input.GetKey(KeyCode.W);
                signalData.IsDown = Input.GetKey(KeyCode.S);
                signalData.IsLeft = Input.GetKey(KeyCode.A);
                signalData.IsRight = Input.GetKey(KeyCode.D);
                _signalBus.Fire<InputSignalData>(signalData);
            }
        }
    }
    
    public struct InputSignalData
    {
        public bool IsUp { get; internal set; }
        public bool IsDown { get; internal set; }
        public bool IsRight { get; internal set; }
        public bool IsLeft { get; internal set; }
    }
}
```

Example of receiving event:

```c#
using Template.Scripts.DI;
using UnityEngine;

namespace Template.Samples.Signals
{
    public class PlayerSignalController : InjectedMono
    {
        private void Start()
        {
            _signalBus.Sub<InputSignalData>(OnInputData);
        }

        private void OnInputData(InputSignalData obj)
        {
            if (obj.IsUp)
            {
                transform.position += Vector3.up * Time.deltaTime;
            }
            
            if (obj.IsDown)
            {
                transform.position += Vector3.down * Time.deltaTime;
            }
            
            if (obj.IsLeft)
            {
                transform.position += Vector3.left * Time.deltaTime;
            }
            
            if (obj.IsRight)
            {
                transform.position += Vector3.right * Time.deltaTime;
            }
        }
    }
}
```
You can use only structs as sending signal data. You can use structs with no fields.
After scene is reloaded unsubscribe is done automatically, if you need to unsubscribe you can use UnSub method.

## Scene Progression > Is Working Only if you use Canvas-Prefab AllUi from Project!

#### Description:
Progression Scene is relevant only for scene. To it is attached analytics, Prefs-Save and UI. 
Prefab-base currently isn't supported.


#### How to use 

Everything is done via signals, here is the example:
```c#
    public class Example : InjectedMono
    {
        [SerializeField] private Text _text;

        public override void OnSyncStart()
        {
            _signalBus.Sub<StartMenu.OnStartLevel>(OnStartLevel);
            _signalBus.Sub<FinishLevelHandler.FinishSignal>(OnFinishLevel);
        }

        private void OnFinishLevel(FinishLevelHandler.FinishSignal obj)
        {
            if (obj.IsWin)
            {
                _text.text = "Win";
            }
            else
            {
                _text.text = "Lose";
            }
        }

        private void OnStartLevel(StartMenu.OnStartLevel obj)
        {
            _text.text = "Game Loop";
        }

        public void OnFail()
        {
            _signalBus.Fire(new FinishLevelHandler.FinishSignal
                { AmountOfPoints = 20, IsWin = false});
        }

        public void OnWin()
        {
            _signalBus.Fire(new FinishLevelHandler.FinishSignal
                { AmountOfPoints = 20, IsWin = true, ShowPoints = true});
        }
    }
```


#### How to use SceneProgression.cs

There is a setup at FirstScene.unity, it should be first in BuildSettings. 
In order to Move to next level you need to call _sceneProgression.LoadNextScene, 
in order to reload you need to call _sceneProgression.ReloadScene. Here are examples:

```c#
public class ExampleMoveScene : InjectedMono
{

    public void MoveToNextLevel()
    {
        _sceneProgression.LoadNextScene();
    }

    public void MoveToNextLevel_FinalRandom()
    {
        LoadNextScene(true);
    }

    public void ReloadLevel()
    {
        _sceneProgression.ReloadScene();
    }
}
```

You can use buttons form Editor, select System-Prefab.

## Pool
Pool is a concept for optimisation of object-reusability. The idea is to store GameObjects in memory instead of destroying them.
The sample of Template implementation is in Template/Samples/Pool

How it works, you need to create a prefab with some scripts (out case PoolObjectMono). PoolObjectMono should implement IPoolObject.

Here is example:
```c#
using Template.Scripts.Utils;
using UnityEngine;

namespace Template.Samples.Pool
{
    public class PoolObjectMono : MonoBehaviour, IPoolObject
    {
        public void OnEnterPool()
        {
            //fake destroy
            gameObject.SetActive(false);
        }

        public void OnExitPool()
        {
            //fake init
            gameObject.SetActive(true);
        }
    }
}
```

And there is a controller that handles memory management:

```c#
using System;
using System.Collections;
using Template.Scripts.Utils;
using UnityEngine;

namespace Template.Samples.Pool
{
    public class PoolController : MonoBehaviour
    {
        public PoolObjectMono Prefab;

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                var obj = Pool<PoolObjectMono>.GetFromPool(() => Instantiate(Prefab));
                StartCoroutine(DestroyObjectAfterTime(obj));
            }
        }

        private IEnumerator DestroyObjectAfterTime(PoolObjectMono obj)
        {
            yield return new WaitForSeconds(2f);
            Pool<PoolObjectMono>.SetInPool(obj);
        }

        private void OnDestroy()
        {
            Pool<PoolObjectMono>.ForceClearAll();
        }
    }
}

```

Note! Pool-Clearing is handled also by controller. Auto-cleaning on change scene probably will be in future releases.

## Ads and Analytics

#### Description:
Because publisher like to use some custom plugins there a need in abstraction of it. 

#### How to use
AdsSystem.cs and AnalyticsHandler.cs are part of InjectedMono, so you need just to inherit from InjectedMono.
Example:
```c#
    public class ExampleScript : InjectedMono
    {
        [In] protected AnalyticsHandler _analyticsHandler;
        [In] protected AdsSystem _adsSystem;

        public void ShowAds()
        {
            if (_adsSystem.IsAvailable())
            {
                _adsSystem.ShowAds(isShown =>
                {
                    if (isShown)
                    {
                        Debug.Log("Ads is shown");
                    }
                    else
                    {
                        Debug.Log("Ads not shown");
                    }
                });
            }
            else
            {
                Debug.Log("At the moment ads isn't available!");
            }
        }

        public void SendAnalyticsEvent()
        {
            _analyticsHandler.SendEvent("Analytic event");
        }
    }
```


## Other Utils

#### Money System > Use if You Develop Simple Non Multiplayer Games (HyperCasual)!
#### Note! Working only if you have System.prefab in scene!
Simple money system. Example in Template/Samples/Monetization/MoneyExample.unity.

Script Example:
```c#
using Template.Scripts.DI;
using Template.Scripts.Monetization;
using UnityEngine.UI;

namespace Template.Samples.Monetization
{
    public class MoneyExample : InjectedMono
    {
        [In] private MoneySystem _moneySystem;
        public Text MoneyText;
        public Button Decrement;
        public void OnIncrepemtnWith10()
        {
            _moneySystem.Add(10);
        }

        public void OnDecrementWith20()
        {
            _moneySystem.Decrement(20);
        }

        private void Update()
        {
            MoneyText.text = _moneySystem.PlayerMoney.ToString();
            Decrement.interactable = _moneySystem.IsPurchasable(20);
        }
    }
}

```

#### InputNormalized
InputNormalized.GetMousePos() - gets normalize Input relative for Input that is independent from resolution

#### TimeServiceMono > Local Utilization Only!
Custom Update for optimisation, please you use it only if there is a need in optimisation. 
Any exception during IUpdate can break other scripts, please don't change deltaTime!!!

```c#
public class ExampleScript : InjectedMono, IUpdater
{
    [In] protected TimeServiceMono _time;

    public void Start()
    {
        _time.Register(this);
    }
    public void OnUpdate(ref float deltaTime)
    {
        //Here should be update code
    }
}
```
#### Effects
Effects are available in Template/Effects. List of effects:
- Outline
- Skybox (Vadim request). Check Template/Samples/GradientSkybox

#### MathUtils
Some basic math that is not available in Unity:
- Rotation Wrap (0-360)

#### BasicCameraController
Default camera controller, add it when you need some camera that can be controlled by touch.

#### Haptic
Need to call HapticHandler.SmallHaptic();

There is a default UI that handles Disable/Enable haptic, 
please check it prefab 'Canvas-Settings' (also present in scene Level1.unity)


#### UiHelper

UiHelper.IsOnUi() can be called to check if current mouse position is touching UI (with rayCastTarget enable).
Ui Raycast also can be called/using from script.

#### StringUtils

Some string utils to work with float and int (in future support for long).
Please check the script is self-explaining. 

#### NaughtyAttributes
This library includes NaughtyAttributes, please check there doc: 
https://github.com/dbrizov/NaughtyAttributes

#### Default Images

In Template/Sprites you can find default logo and some other useful sprites

#### Restart scene in editor
You can restart scene in editor by using CTRL+R

#### Clear Player Pref in editor
You can clear PlayerPrefs(SaveSystem) in editor by using CTRL+DELETE > Note! Delete only you saved data in PlayerPrefs!

#### Camera bounds (area that camera see, ony orthographic support)
Camera.OrthographicBounds()

#### Wait System
You can inject WaitSystem and call WaitRoutine(float seconds, System.Action callBack)
