# Hexr SandBox Demo - HTX

## Installation

### Prerequisites:
- Ensure you are using **Unity 2021.3.26f1** or newer.
  
### Steps to Get Started:
1. **Clone this repository:**
   [HexR Developer Tutorial Repository](https://github.com/MicrotubeTechnologies/HexR-Developer-Tutorial.git)

2. **Open the HexR SandBox HTX project in Unity. Ensure the project platform is in android.**
   
3. **Open the scene 'HexR SandBox' located in Asset/Scenes.**
   


<details>
  <summary>🔍 HexR Code Structure</summary>

### Learn more about the HexR code structure and architecture 💡

<details>
  <summary>1. Hand Tracking (PhysicsHandTracking)</summary>

#### The HexR hand supports both the **OpenXR** and **Meta OVR** hand skeleton structure.  
Here’s a summary of the differences in hand structure:
- **OpenXR Hand Skeleton**
- **Meta OVR Hand Skeleton**  
The `PhysicsHandTracking` script mimics the position/rotation of either the OpenXR or Meta OVR hands, the script is attached to the Left/Right hand physics component under HexR Main.

![Hand Skeleton](https://github.com/user-attachments/assets/2585a044-ae44-4814-88e5-abe61c876f8e)

If a custom hand structure is used, you will have to recreate the `PhysicsHandTracking` to track each joint.

</details>

<details>
  <summary>2. HexR Overall Manager (HaptGloveManager)</summary>

#### The `HaptGloveManager` simplifies the setup process.  
- In the inspector, ensure the XR framework is set to Meta OVR and click the **"Auto Set Up HexR"** button.
- If Set up is successfull, there should be no missing links in the inspector for HexR main, Left Hand Physics and Right hand Physics.
- Check the debug log to ensure the setup is successful. 

![Setup Image](https://github.com/user-attachments/assets/f09f713f-fa81-484e-8646-bbe830ecce35)

#### HaptGloveManager Settings:
- **XR Framework:**  
  - Do select only the meta OVR Framework as there will be missing assets if OpenXR is selected, for projects using OpenXR refer to the OpenXR developer tutorial in the link above.

- **HexR Panel Component:**  
  - The floating HexR Panel controls the connection to the HexR glove.  
  
</details>

<details>
  <summary>3. Haptics Controller (PressureTrackerMain)</summary>

#### The `PressureTrackerMain` script contains all of the functions to trigger haptics.
#### There is 6 Channels in the HexR glove allowing haptics to be triggered for each finger and the palm

- Overview
  - Functions are categorized by **single-channel** or **multi-channel** triggers.
  - Haptics intensity range from 0.1 (no haptics) to 1 (Max haptics).
  - Refer to the demo scene to see examples of how these functions are used.

- Function : IsHandNear()
  - This is use to check if the user left or right hand is grabbing or near the target object, so that haptics is correctly triggered at the right timme and by the right hand.
    
- Function : CustomSingleHaptics ( Haptics.Finger finger, bool states, float intensity, float speed, bool ByPassHandCheck )
  - Haptics.Finger = which finger is to be triggered: index,middle,ring,pinky,thumb,palm
  - states : true = haptics in , false = haptics out
  - intensity : 0.1 - 1 , min haptics - max haptics
  - speed : 0.1 - 1 , slowly increase haptics vs fast increase haptics
  - ByPassHandCheck : true = will trigger haptics without checking IsHandNear()

- Function : CustomSingleVibrations(Haptics.Finger finger, bool states, float intensity, float frequency, bool ByPassHandCheck)
  - Haptics.Finger = which finger is to be triggered: index,middle,ring,pinky,thumb,palm
  - states : true = haptics in , false = haptics out
  - frequency : 0.1 - 2 
  - intensity : 0.1 - 1 , min haptics - max haptics
  - ByPassHandCheck : true = will trigger haptics without checking IsHandNear()
</details>

<details>
  <summary>4. HexR Grab and Pinch (HexRGrabbable)</summary>

#### The `HexRGrabbable` script enables objects to be picked up by the HexR hands.
#### This is optional as you can also use the grab/pinch provided by **Meta OVR**, however the haptics trigger and physics of grab will be different. Give both a try to see which is more suitable for you.
To set up `HexRGrabbable`:
1. Ensure the object has a **Collider (Trigger)** and **Rigidbody** attached to the same GameObject.
2. Since the interaction is physics-based, adjust the size of the collider to improve grab/pinch behavior.
3. Optionally, attach an additional collider if you want the object to interact with other GameObjects.

![Grabbable Example](https://github.com/user-attachments/assets/3fadad3e-80d7-4f57-9186-a63d4ebc125f)

#### HexRGrabbable Settings:
- **Type of Grab:**  
  - **Palm Grab:** Requires the palm and at least one finger to touch the object (thumb not required).
  - **Pinch Grab:** Requires the thumb and at least one finger to touch the object (palm not required).

- **Gravity Bool:**  
  If enabled, gravity will affect the object when released.

- **Haptic Slider:**  
  Controls the strength of the haptic feedback during grab or pinch.  
  - `0`: No haptics  
  - `60`: Maximum haptics strength

- **On Grab Event:**  
  Trigger an event when the object is grabbed or pinched.

- **On Release Event:**  
  Trigger an event when the object is released.

</details>

<details>
  <summary>5. Creating Haptic Zones (SpecialHaptics)</summary>

#### The `SpecialHaptics` script enables objects to trigger a custom haptic effect when touch.

![image](https://github.com/user-attachments/assets/15bc96c7-db42-452c-adeb-68b657984802)

To set up `SpecialHaptics`:
1. Ensure the object has a **Collider (Trigger)** attached to the same GameObject.
2. Since the interaction is physics-based, adjust the size of the collider for the haptic zone.
3. Select the type of Haptics in the inspector.

#### SpecialHaptics Settings:
- **Custom Vibrations:**  
  - When activated will create the vibration effects.
  - *Frequency Speed:* the frequency of the vibrations.
  - *Haptic Strength:* the strength of the vibrations.
- **Custom Haptics:**
  - When activated/touch will trigger a constant haptic.
  - *Haptic Pressure:* slider to adjust strength of haptic. 10 = weakest, 60 = strongest.
- **Fountain Effect:**  
  - When activated will simulate running water.
 
- **Raindrop Effect:**  
  - When activated will simulate raindrops with random haptics trigger.
    
- **Heart Beat Effect:**  
  - When activated will simulate beating heart, but only affects fingers and not palm.
    
- **Hand Squeeze Effect:**  
  - When activated will allows the player to trigger an event by squeezing the hand
  - `0.1`: Fully closed hand  
  - `1`: Fully open hand
</details> 

<details>
  <summary>6. Determine if hand is near (ProximityCheck)</summary>

### (Optional for Meta OVR)
#### If you are using Meta OVR, you can use the native handgrabinteractor/handpokeinteractor to check which hand is grabbing/hovering instead of this ProximityCheck.

#### The `ProximityCheck` script checks if the left or right hand is near the target object.
#### Haptics is only trigger when the hand is near the object.
#### Place the `ProximityCheck` prefab as a child of the target object and click the auto set up.
#### You should adjust the size of your trigger collider to ensure that it is optimise.


</details> 
</details>

&nbsp;

<details>
<summary> Demo Scene : HexR Sandbox </summary>
 
## **Demo Scene : HexR Sandbox **

#### The **HexR Sandbox** demo scene contains the basic grab, rain/fountain, button puzzle and emergency training scenario.

![image](https://github.com/user-attachments/assets/05c85795-9bdd-4ebf-842d-5baaffc2699d)

![image](https://github.com/user-attachments/assets/a2172546-02ff-4f2f-ab1e-ed8446b38f53)


</details>
