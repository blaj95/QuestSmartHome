using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Q42.HueApi;
using Q42.HueApi.ColorConverters;
using Q42.HueApi.ColorConverters.HSB;
using Q42.HueApi.Interfaces;
using TMPro;
using UnityEngine;
using Light = Q42.HueApi.Light;


public class LightTest : MonoBehaviour
{
    private ILocalHueClient client;
    IEnumerable<Light> lights;
    private string appKey = "Z14CdQeVzfIPWqL9Myr9la863YgugHYxXGTfFscN";
    public HueSettings hueSettings;
    public List<Color> colors;
    public int index;
    public int strobeDelay;
    public Color pink, orange, yellow, black, purple;
    async void Start()
    {
        Debug.Log("Start");
        await RegisterAppWithHueBridge();
        Debug.Log("Done!");
    }

    async void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            Debug.Log("on");
            await TurnOn();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) 
        {
            Debug.Log("off");
            await TurnOff();
        }
        else if (Input.GetKeyDown(KeyCode.Return)) 
        {
            Debug.Log("STROBE");
            await Strobe("user");
        }
    }

    public async void TurnOffButton()
    {
        await TurnOff();
    }
    
    public async void TurnOnButton()
    {
        await TurnOn();
    }

    public async Task RegisterAppWithHueBridge()
    {
        Debug.Log("Registering");
        IBridgeLocator locator = new HttpBridgeLocator(); //Or: LocalNetworkScanBridgeLocator, MdnsBridgeLocator, MUdpBasedBridgeLocator
        var bridges  = await locator.LocateBridgesAsync(TimeSpan.FromSeconds(5));
        var bridge = bridges.First();
        client = new LocalHueClient(bridge.IpAddress);
        
        // Get the key
        // appKey = await client.RegisterAsync(
        //     hueSettings.AppName, 
        //     hueSettings.DeviceName);
        //
        // hueSettings.AppKey = appKey;
        
        client.Initialize(appKey);
        
        lights = await client.GetLightsAsync();
        
        var lightToChange = lights.FirstOrDefault();
        Debug.Log(lightToChange.Name);
        
        Debug.Log("Registered or not");
    }
    
    public async Task ChangeLight(UnityEngine.Color color1, UnityEngine.Color color2)
    {
        if (client == null)
        {
            return;
        }

        var lightToChange = lights.FirstOrDefault();
        var lightToChange2 = lights.ElementAt(1);
        var command = new LightCommand();
        var lightColor = new RGBColor(color1.r, color1.g, color1.b);
        command.TurnOn().SetColor(lightColor);

        var lightsToAlter = new string[] { lightToChange.Id};
        var lightsToAlter2 = new string[] { lightToChange2.Id};
        // await client.SendCommandAsync(command, lightsToAlter);
        await client.SendCommandAsync(command,lightsToAlter);
        await client.SendCommandAsync(command,lightsToAlter2);
    }
    
    public async Task TurnOn() 
    {
        if (client != null)
        {
            var command = new LightCommand();
            command.On = true;
            await client.SendCommandAsync(command);
        }
    }

    public async Task TurnOn(string lightID) 
    {
        if (client != null)
        {
            var lightsToAlter = new string[] {lightID};
            var command = new LightCommand();
            command.On = true;
            await client.SendCommandAsync(command, lightsToAlter);
        }
    }

    public async Task Strobe(string lightID)
    {
        await TurnOff();
        await Task.Delay(strobeDelay);
        await TurnOn(lightID);
        await Task.Delay(strobeDelay);
        await TurnOff();
        await Task.Delay(strobeDelay);
        await TurnOn(lightID);
        await Task.Delay(strobeDelay);
        await TurnOff();
        await Task.Delay(strobeDelay);
        await TurnOn(lightID);
        await Task.Delay(strobeDelay);
        await TurnOff();
        await Task.Delay(strobeDelay);
        await TurnOn(lightID);
        await Task.Delay(strobeDelay);
        await TurnOff();
        await Task.Delay(strobeDelay);
        await TurnOn(lightID);
        await Task.Delay(strobeDelay);
        await TurnOff();
        await Task.Delay(strobeDelay);
        await TurnOn(lightID);
        await Task.Delay(strobeDelay);
    }
    
    
    public async Task TurnOff() 
    {
        if (client != null)
        {
            LightCommand command = new LightCommand();
            command.On = false;
            await client.SendCommandAsync(command);
        }
    }
    
    public async Task TurnOff(string lightID) 
    {
        if (client != null)
        {
            var lightsToAlter = new string[] { lightID};
            var command = new LightCommand();
            command.On = true;
            await client.SendCommandAsync(command, lightsToAlter);
        }
    }

    async void OnDestroy()
    {
        if (client != null)
        {
            // // Debug.Log("Lights off");
            // // var command = new LightCommand();
            // // command.TurnOff();
            // await client.SendCommandAsync(command);
        }
    }

    #region Pison
// private async void PisonEvents_OnGesture(ImuGesture gesture)
    // {
    //     switch (gesture)
    //     {
    //         case ImuGesture.SwipeUp:
    //             await TurnOn();
    //             break;
    //         case ImuGesture.SwipeDown:
    //             await TurnOff();
    //             break;
    //         case ImuGesture.SwipeLeft:
    //             if (index < 0) {
    //                 index = 4;
    //             }
    //             else
    //                 index--;
    //             await ChangeLight(colors[index], Color.green);
    //             break;
    //         case ImuGesture.SwipeRight:
    //             if (index == 4) {
    //                 index = 0;
    //             }
    //             else
    //                 index++;
    //             await ChangeLight(colors[index], Color.green);
    //             break;
    //     }
    // }

    // private async void OnGesture(object sender, PisonGestureArgs e)
    // {
    //     if (e.GestureType == PisonGestureRecognizer.Gesture.UpSwipe) 
    //     {
    //         await TurnOn();
    //     }
    //     else if (e.GestureType == PisonGestureRecognizer.Gesture.DownSwipe) 
    //     {
    //         await TurnOff();
    //     }
    // }
    

    #endregion
}
