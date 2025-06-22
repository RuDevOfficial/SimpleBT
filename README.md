
# SimpleBT - Simple Behavior Tree for Unity 6

SBT is a basic graph view tool to develop behaviors for AI focused on easy shareable behaviors with JsonUtility. 

**Note**: This is a tool developed as a final assignment for University in 2025. This asset comes with a minimal set of features.






## What are Behavior Trees?

Behavior Trees (shortened as “BTs”) are trees of hierarchical nodes that control the flow of decision making of an AI entity. Compared to FSMs these do not require programmed transitions between states, but rather flows from one node to another in a branching tree fashion.

This flow is decided by the interaction between the **composite** nodes, **decorator** nodes and **execution** nodes. Composite nodes can branch and determine the order in which their child nodes are executed, decorator nodes alter the result of their child node and execution nodes run actions (like moving to a new position) or conditions (checking if something is true or not).

Ticking is an essential part of this model, and every time the behavior ticks (updates) it has to return 1 of 3 states: Success, Failure or Running.

For further information you can read *Michele Colledanchise and Petter Ögren's Behavior Trees in Robotics and AI An Introduction* here: https://arxiv.org/pdf/1709.00084
## How to Install the Package

1. Head over the "releases" section and download the unity package. 
2. Open your Unity 6 project and then open the package
3. Select all files and import.

## Creating a Behavior

First, open the window located at the top bar called *SimpleBT*->*Window*.

![SBTEditorWindow](https://github.com/RuDevOfficial/SimpleBT/blob/Beta/Screenshots/SBTEditorWindow%20Window.png?raw=true)

Once open you can write a new name for the behavior  and start adding nodes by pressing **space** or **right-clicking**->**Create Node**. A search window will show up.

**Note**: 
- It is required to create a Root node first before any other, and there cannot be more than 1. If you try to add another you'll get a popup
- Behavior names **can only contain** numbers and '_' characters

![SBTSearchWindow](https://github.com/RuDevOfficial/SimpleBT/blob/Beta/Screenshots/SBTSearchWindow.png?raw=true)

![Populating the Behavior](https://github.com/RuDevOfficial/SimpleBT/blob/Beta/Screenshots/BehaviorExample.png?raw=true)

At any point you can press the button **Save** to save the behavior onto a JSON file which will be located in *Assets->SimpleBT->GraphData*. 

**To load the behavior** you can:
- Type the name manually first (Not recommended)
- Select the JSON File (.simple) (Recommended)

## Using the Blackboard

To create a new variable press the **+** icon, you can rename it by double-click. Modify its value type or content by accessing the expanded section (">" icon on the left).

![Blackboard](https://github.com/RuDevOfficial/SimpleBT/blob/Beta/Screenshots/Blackboard.png?raw=true)

To delete a variable select the respective field and press **delete** (not backspace).

### Currently Supported Types

| Type       | Syntax                                 | Blackboard Method                 |
|------------|----------------------------------------|-----------------------------------|
| int        | 3                                      | GetValue<<Type>**int**>               |
| float      | 3.2                                    | GetValue<<Type>**float**>             |
| bool       | True                                   | GetValue<<Type>**bool**>              |
| string     | something                              | GetValue<<Type>**string**>            |
| Vector2    | 3, 3.9                                 | GetValue<<Type>**Vector2**>           |
| Vector3    | 21.1, 3, 90                            | GetValue<<Type>**Vector3**>           |
| GameObject | Tomato, Untagged, 391290 (Instance id) | GetComplexValue<<Type>**GameObject**> |

**Note**: Currently supported types *in* the SBTBlackboardGraph, any type can be stored on the Blackboard's dictionary at behavior runtime.

## Generating the Tree and Blackboard

To generate the tree select a GameObject and then press "*Generate*". If the behavior has changed you can press *"Save & Regenerate*".
If you want to get rid of the components you can press "*Remove Components*".

![Generation](https://github.com/RuDevOfficial/SimpleBT/blob/Beta/Screenshots/Saving%20and%20Regenerating.png?raw=true)

## Available Nodes (In Sections)

<details>
<summary>Ungrouped</summary>

| Name     | Description                                                           |
|----------|-----------------------------------------------------------------------|
| Root     | Acts as the tree root <br/> Must be added first before any other node |
| Behavior | References a different behavior. Used for behavior nesting.           |

</details>

<details>
<summary>Blackboard</summary>

| Name                     | Description                                                          |
|--------------------------|----------------------------------------------------------------------|
| Invert Numerical Value   | Inverts a numerical blackboard value and stores it                   |
| Remove Key               | Removes a key from the dictionary.                                   |
| Store Random Position 3D | Stores a random position given a distance, layer. Stores the result. |

</details>

<details>
<summary>Action</summary>

| Name                            | Description                                                                                            | Section              |
|---------------------------------|--------------------------------------------------------------------------------------------------------|----------------------|
| Destroy GameObject              | Self explanatory                                                                                       | GameObject           |
| Get Random Child from Parent    | Self explanatory                                                                                       | GameObject           |
| Send Message                    | Sends a [message](https://docs.unity3d.com/ScriptReference/GameObject.SendMessage.html) to all methods | GameObject           |
| Send Message with Value         | Just like message, but with values inside parameters                                                   | GameObject           |
| Set Active                      | Self explanatory                                                                                       | GameObject           |
| Set Active (Toggle)             | Self explanatory                                                                                       | GameObject           |
| Set GameObject Parent to Null   | Self explanatory                                                                                       | GameObject           |
| Override Tag                    | Self explanatory                                                                                       | GameObject           |
| Parent GameObject to Self       | Self explanatory                                                                                       | GameObject           |
| Move Navmesh Agent to Target 3D | Self explanatory                                                                                       | NavMesh              |
| Any Action                      | Action that can transform to any other given the correct name and values                               | Movement/General     |
| Stop                            | Self explanatory                                                                                       | Movement/General     |
| Flee 2D                         | Self explanatory                                                                                       | Movement/2D          |
| Follow 2D                       | Self explanatory                                                                                       | Movement/2D          |
| Go to Position 2D               | Self explanatory                                                                                       | Movement/2D          |
| Linear Move 2D                  | Self explanatory                                                                                       | Movement/2D          |
| Flee 3D                         | Self explanatory                                                                                       | Movement/3D          |
| Follow 3D                       | Self explanatory                                                                                       | Movement/3D          |
| Go to Position 3D               | Self explanatory                                                                                       | Movement/3D          |
| Override GameObject Position 3D | Self explanatory                                                                                       | Movement/3D          |
| Look At Target                  | Self explanatory                                                                                       | Rotation             |
| Rotate Degrees 2D               | Self explanatory                                                                                       | Rotation/2D Specific |
| Rotate Constantly 2D            | Self explanatory                                                                                       | Rotation/2D Specific |
| Rotate Degrees 3D               | Self explanatory                                                                                       | Rotation/3D Specific |
| Rotate Constantly 3D            | Self explanatory                                                                                       | Rotation/3D Specific |
| Wait X Seconds                  | Self explanatory                                                                                       | Time                 |
| Debug                           | Prints a debug message                                                                                 | Other                |

</details>

<details>
<summary>Conditions</summary>

| Name                      | Section     |
|---------------------------|-------------|
| Compare Blackboard Values | Comparisons |
| Compare Bool              | Comparisons |
| Compare Bools             | Comparisons |
| Compare Float             | Comparisons |
| Compare String            | Comparisons |
| Compare Vector2           | Comparisons |
| Compare Vector3           | Comparisons |
| Is GameObject Close 2D    | 2D Specific |
| Is Near Ledge 2D          | 2D Specific |
| Can See Target 3D         | 3D Specific |
| Is GameObject Close 3D    | 3D Specific |
| Always Succeed            | Other       |
| Always Fail               | Other       |
| Is At Minimum Distance    | Other       |

</details>


<details>
<summary>Composites</summary>

| Name              | Description                                                                                                                                                                                                  |
|-------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Sequence          | Ticks each children in order, fails if one fails, succeeds if all succeed                                                                                                                                    |
| Selector          | Ticks each children in order, succeeds if one succeeds, fails if all fail                                                                                                                                    |
| Parallel Sequence | Ticks all children in parallel, if any fail all fail                                                                                                                                                         |
| Random Sequence   | Selects a random children to tick until all are ticked (Follows Sequence Logic)                                                                                                                              |
| Priority          | Prioritizes nodes on the left and aborts lesser important nodes <br/> Requires Composites as children and each composite must contain a Condition <br/>AlwaysSucceed recommended to the least important node |

</details>


<details>
<summary>Decorators</summary>

| Name                           | Description                                                                        |
|--------------------------------|------------------------------------------------------------------------------------|
| Execute Once With Delay        | Executes the child once and doesn't repeat until certain seconds have passed       |
| Repeat Forever                 | Always returns "Running", preventing the behavior from finishing                   |
| Repeat Until Success           | Ticks the child until the child returns success                                    |
| Repeat Until Failure           | Ticks the child until the child returns failure                                    |
| Repeat X Times                 | Ticks the child x times, then returns success                                      |
| Repeat X Times w/ Child Result | Ticks the child x times, then returns whatever the child returned at the last time |

</details>


<details>
<summary>Custom</summary>

This section is exclusive for your custom nodes.

</details>

## Creating Custom Nodes

1. Right-click on the graph view to open the contextual menu
2. Select "Create Custom Node"

![Step 1 & 2](https://github.com/RuDevOfficial/SimpleBT/blob/Beta/Screenshots/Create%20Custom%20Node%20Step%201_1.png?raw=true)

3. Choose type, give it a name and select "Create"

![Step 3](https://github.com/RuDevOfficial/SimpleBT/blob/Beta/Screenshots/Create%20Custom%20Node%20Step1_X.png?raw=true)

4. Modify the *Title* and *ClassReference* parameters inside the constructor, then add your logic

![Step 4](https://github.com/RuDevOfficial/SimpleBT/blob/Beta/Screenshots/Example%20GraphAction.png?raw=true)

5. (If no CustomEntrySO exists) Right-click on the graph view's contextual menu and select "Create Custom Entries SO",
6. Open your Custom Entries Scriptable Object script and add a new entry inside the *GetEntries()* method. Userdata must start with "Custom_"

![Step 6](https://github.com/RuDevOfficial/SimpleBT/blob/Beta/Screenshots/Example%20Entries.png?raw=true)

7. Create an asset from your Custom Entries SO and drag it to the "Custom Entries SO" Object Field.

![Step7](https://github.com/RuDevOfficial/SimpleBT/blob/Beta/Screenshots/Custom%20Entries.png?raw=true)

**If your class reference is the same name as your non-editor node it should appear on the graph view!**

## Saving and Loading on other Folders

If you decide you want to save the editor, graph data and style somewhere else make sure to...
1. Select "Settings" below the SimpleBT section
2. Fill the new path to those files
3. Press save

![Settings Window](https://github.com/RuDevOfficial/SimpleBT/blob/main/Screenshots/SettingsWindow.PNG?raw=true)

Make sure to NEVER change the directory where SettingsData is located or everything will break!

## Common Questions

- **What happens if I made custom nodes? Do I need to send them besides the .simple behavior file to my friend?**
  - **Answer**: Yes, make sure to import those custom nodes onto your project BEFORE loading the behavior.


- **Which custom node types can I make?**
  - **Answer**: Actions, conditions, composites and decorators. Pretty much all you will need.


- **Can I fork this project?**
  - **Answer**: Yes, you can fork this project since it's license is MIT. Just give me credit.


- **My behaviors are not loading when I select them. What's wrong?**
  - **Answer**: Make sure you have set the necessary paths on the settings window data.


- **My behavior is still not loading after setting the paths**
  - **Answer**: You probably moved the SettingsData folder, that folder MUST stay there at all times.

## Special Thanks

- Indie Wafflus on “Unity Dialogue System - Creating the Search Window”: https://www.youtube.com/c/IndieWafflus
- Mert Kirimgeri: UNITY DIALOGUE GRAPH TUTORIAL - Variables and Search Window: https://youtu.be/F4cTWOxMjMY?si=_NuLrvaJIA7bOOhd
