# Unity FSM

Tool for Unity that lets you create and edit Finite State Machines (FSM) from the Unity editor.

This system applies Unity's internal logic to the state machine pattern. This means that `StateEnter`, `StateUpdate` and `StateExit` are driven by Unity's callbacks: `OnEnable`, `Update` and `OnDisable`. Therefore, states inherit from `MonoBehaviour` and are Unity components, so their capabilities can be extended to `FixedUpdate` and potentially to more `MonoBehaviour` event functions.

## Editor features

Currently, from the FSM editor you can:

- Choose the initial State of the FSM.
- Define the FSM's global parameters. These parameters:
  - Can be of type Bool, Int, Float or Trigger.
  - Have a configurable initial value.
  - Are created by defining their type and a name.
- Create transitions from *Any State*. These transitions:
  - Are created with a name.
  - Have a target State to transition to.
  - Have the conditions that must be met for the transition to happen. There can be more than one condition.
- Conditions:
  - Are created by choosing a global parameter.
  - Use a comparison method (except for Bool and Trigger).
  - Have an expected value (except for Trigger, which is always true).

<img width="577" height="302" alt="Player_UFSM" src="https://github.com/user-attachments/assets/f1b03ac8-f253-4f73-a051-ff589d2867bf" />

In the editor of the classes that inherit from `State`, you can also create and edit transitions to other States. In fact, this is where most transitions will be created. You can also, of course, edit the conditions of these transitions and see which global parameters the FSM has.

<img width="577" height="232" alt="Player_State" src="https://github.com/user-attachments/assets/fb2b6e68-8068-44f4-9d7c-857bb560e3d4" />

## Status

This Unity tool is incomplete. The final goal was to create an FSM that could be edited visually in its own window, similar to how Unity's Animator works. However, due to its technical complexity and lack of time to continue, I stopped its development.

Even so, the tool is 100% functional in its current state and allows editing from the Unity editor.

## Demo

The repository only includes the code for the system and the editor. I have also added a Unity Package with a demo that includes all the assets and a test scene. The scene contains two FSMs: one on the Player and one on an NPC.

![FSM demo](docs/fsm-demo.gif)

The demo uses code from my repositories:

- [Character System](https://github.com/YOUR-USERNAME/Character-System)
- [Weapon System](https://github.com/YOUR-USERNAME/Weapon-System)
- [Unity Utils](https://github.com/YOUR-USERNAME/Unity-Utils)

The following third-party assets are also used:

- TextMeshPro, Unity
- Weapon Stylized Pack, nappin
- Cartoon FX Remaster, JMO Assets

The demo is built with Unity 2021.3.45f2 using the Built-in Render Pipeline.

## AI Disclaimer

AI assistance was used in this repository for:

- The code comments, which were written with AI assistance.
- A final cleanup pass over the code (formatting consistency and dead code removal).

The rest of the code was written by me.
