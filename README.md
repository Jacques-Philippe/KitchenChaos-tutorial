# Purpose

Follow along with [this tutorial](https://youtu.be/AmGSEH7QcDg) to try and see what a full clean-code game could look like.

# Steps taken

I prefer to watch the video for a few minutes, jot down some general notes about what to do, and then pause the video and catch up myself. The following is my complete list of steps

1. get started in Unity
   1. Create Unity project
   1. Remove unneeded rendering profiles
   1. Ensure Visual Studio Community 2022 is associated to the project
   1. Install Visual Studio unity package in Package manager
   1. Install viasfora extension
1. Add post processing to the game
   1. Add to global volume profile
      1. Tonemapping
      1. Color Adjustments
      1. Bloom
      1. Vignette
1. Change Camera
1. Update rendering profile SSAO
1. Define player
   1. Define player gameobject
   1. Define player movement script
   1. Define idle animation
   1. Define walk animation
   1. Create PlayerAnimator script
1. Add cinemachine camera
   1. Top-down camera
1. New input system
   1. Create GameInput class
   1. Install InputSystem package
   1. Enable both input systems in project `Other settings`
   1. Create and define PlayerInputActions
   1. Generate C# Class for PlayerInputActions
   1. Create and enable PlayerInputActions instance
   1. Create movement input for WASD, arrows, and controller
1. Collision detection
   1. Obstruct player movement with capsulecast
   1. If player collides with wall in conjunction with perpendicular movement, conserve only perpendicular input
1. Add interacton with counter
   1. Create Counters layer
   1. Create class ClearCounter
   1. Create ClearCounter prefab
   1. Create PlayerInputAction interact
   1. Use raycast to detect whether we're in interaction distance
1. Add visual indication of selected counter for interaction
   1. Create Selected counter prefab
   1. Make prefab a bit bigger than non-selected
   1. Add selectedCounter to Player
   1. Add singleton pattern to Player class
   1. Create SelectedCounterVisual script on selected counter prefab
   1. Subscribe to Player SelectedCounterChanged
1. Add kitchen objects to interacted counter top
   1. Create tomato prefab
   1. Spawn tomato on counter top for interaction
   1. Create KitchenObject Scriptable Object
   1. Create scriptable object for tomato and cheese block
1. Kitchen Object parent
   1. Define KitchenObject class
   1. Make spawned kitchen objects children of the clear counter
   1. Prevent multiple spawning of kitchen objects
   1. Transfer a KitchenObject from one counter to another
1. Allow player to pick up KitchenObjects
   1. Define IKitchenObjectParent interface
   1. Refactor ClearCounter to implement IKitchenObjectParent
   1. Refactor KitchenObject to consume IKitchenObjectParent
   1. Refactor Player to implement IKitchenObjectParent
1. Remove test code
1. Define ContainerCounter
   1. Create BaseCounter prefab
   1. Create ClearCounter prefab variant
   1. Create ContainerCounter prefab variant
   1. Create BaseCounter abstract base class
   1. ClearCounter inherits from BaseCounter
   1. Define ContainerCounter class, who inherits from BaseCounter
   1. Fix counter selection issue
   1. Remove spawn/pick up behaviour from ClearCounter
   1. Add spawn and pick up behaviour to ContainerCounter (it's a single actions)
   1. Define ContainerCounterVisual script to consume Container animations for player grabs an object
1. Player pick up, drop objects
   1. Define container counters prefab variants for
      - cheese
      - tomato
      - bread
      - cabbage
      - Meat paddy uncooked
1. Cutting counter
   1. Fix player rotation issue
   1. Create cutting counter prefab
   1. Define alternate input action
   1. Add alternate input to BaseCounter
   1. Define CuttingCounter
   1. Define sliced object SO
   1. Refactor KitchenObject spawn
1. Cutting Recipe SO
   1. Define Cutting Recipe SO
   1. Define KitchenObject SO for all slices
   1. Define SO for Tomato -> Tomato slices, and so on for cheese slices, and cabbage slices
   1. Only allow player to place sliceable kitchen objects on the cutting board
   1. Only cut kitchen objects that have a sliceable output
1. Cutting progress, world canvas
   1. Add cutting progress to cut
   1. ProgressBarUI
   1. Add cutting animation
1. Make progress bar look at camera
1. Trash counter
   1. Create trash counter prefab
   1. Create Trash counter script
1. Stove counter
   1. Create stove prefab
   1. Create FryingRecipeSO
   1. Create StoveCounter script
   1. Stove counter state machine
   1. Add stove visuals given state machine
   1. Create Progress interface
1. Plate counter
   1. Define Plate SO
   1. Define Plate Kitchen Object
   1. Define Plate Counter
   1. Define Plate Counter Visual
1. Define plate able to pick up certain Kitchen Objects SOs
   1. Define list of valid KitchenObjectSOs
   1. Define TryGetPlate
   1. Add plate behaviour to other counters
   1. Add logic for adding kitchen object onto plate on counter
   1. Add visual for each element added to the plate
1. Add icons to represent plate contents
   1. Install sprite package
   1. Create UI canvas
   1. Create UI icon
   1. Create UI scripts to update icons
1. Create DeliveryCounter
   1. Create prefab
   1. Create DeliveryCounter w/ delivery on interact
   1. Create shader for arrow
1. Delivery Manager
   1. Create SO for RecipeSO
   1. Create SO for RecipeListSO
   1. Create script for DeliveryManager
1. Delivery Manager UI
1. Add sound
   1. Background music
   1. SFX
      1. AudioClipReferencesSO
      1. Sound for
         1. chop
         1. Object picked up/dropped
         1. Sizzle
         1. Trash counter
         1. Footsteps
1. Game manager
   1. Create GameManager script with game state
   1. Prevent player from interacting/alt interact for game starting and game over
   1. Add game start UI with countdown
   1. Add game over UI
   1. Create timer
1. Main Menu, Loading
   1. Main menu scene
   1. Play button
   1. Quit button
   1. Loading scene
   1. Static loader class
1. Pause
   1. Add Pause player input
   1. Listen to pause in Gamemanager
   1. Create Pause UI
   1. Create Buttons
      - Resume
      - Main menu
1. Clear statics
   1. Unsubscribe from events and dispose of player inputs object in GameInput
   1. Create ResetStaticData empty object and script
   1. Create static functions to clear static listeners
      - See which in SoundManager
        - BaseCounter
        - use new keyword to hide inherited static function
      - Is there a way we can test for this?
1. Options, Audio levels
   1. Create OptionsUI
      1. Title
      1. Create buttons
         - SoundEffectsButton
         - MusicButton
         - CloseButton
      1. Increment volume by .1 for
         - button click on SoundEffectsButton
         - button click on MusicButton
   1. Show OptionsUI from OptionsButton in PauseUI
   1. Hide OptionsUI for game unpaused
   1. Save and load volume preferences with PlayerPrefs
1. Options, key rebindings
   1. Have UpdateVisual function in OptionsUI to update all UI elements
   1. Define UI elements for input rebindings
   1. Update UpdateVisuals to include bindings text
   1. Define PressToRebindKey UI
   1. Redefine RebindBinding function
1. Controller Input, Menu Navigation
   1. Add controller bindings for
      - Interact
      - Alt Interact
      - Pause
   1. Add dead zone to controller movement binding
   1. Add controller bindings for GameInput
   1. Add gamepad rebinding interact buttons to Options UI
   1. Switch input system for EventSystem
   1. Fix broken selection order
      - Music -> W on DOWN - done
      - Close -> Pause on UP - done
      - X -> F on RIGHT - done
      - Start -> Escape on RIGHT - done
   1. Ensure on all UI Show there's a selected button
1. Polish
   1. Add walls
   1. Add Hiders
   1. Add player moving particles
   1. Create TutorialUI
   1. Add dynamically bound key text to tutorial UI
   1. Add tutorial before countdown
   1. Prevent PlatesCounter spawning plates before tutorial interacted
   1. Countdown UI
      1. Animate (Tip: CanvasGroup)
         - Enter: rotate in, scale up
         - Exit: fade out, scale down
      1. Add sound to countdown
   1. Add warning for food burning
      1. Add icon
      1. for food burning past .5 threshold
         1. Add flashing icon animation
         1. Add warning sound
         1. Progress bar flash red
   1. DeliveryResultUI

# Notes

Subscribers to class static events aren't cleaned up automatically by Unity, we need to manually clean them up. So for instance, given the following

```
class CuttingCounter : BaseCounter {
   public static event EventHandler OnAnyCut;
}
```

For anybody subscribing to the above (i.e., sound effects), we need to manually clean them up on awake using a ResetStaticData component, like the following

```
class ResetStaticData : MonoBehaviour {
   void Awake(){
      CuttingCounter.ResetStaticData();
   }
}
```

# Other references

See:

- `How to make Unity GLOW`
- Shader videos
- Lerp/Slerp video
- Splines are awesome
- Learn about the animation rigging package
- Learn more about cinemachine
  - `Easily control cameras with cinemachine`
  - `COMPLETE Camera System in Unity`
- New input system
  - `How to use NEW Input System Package`
- CodeMonkey Shader playlist
- `Three ways to find a target`
- `Three ways to fire projectiles`
- `How to talk to NPCs`
- `Make your games designer friendly (Scriptable Objects)`
- `Awesome UNIQUE Crafting System! (Max Immersion, No Inventory, Hydroneer)`
