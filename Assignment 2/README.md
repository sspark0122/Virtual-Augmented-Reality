### INFO 5340 / CS 5650
### Virtual and Augmented Reality 
# Assignment 2

Read: [Assignment Instructions](https://docs.google.com/document/d/10haIk-vWfOI48PyhqAlYiqWCeNn-MRB0OG1s6akqwGA/edit?usp=sharing "Detailed Assignment Instructions")

<hr>

### Student Name:

[Sungseo Park]

### Student Email:

[sp2528@cornell.edu]

### Solution (Screen Recording):

[https://youtu.be/4isDcFZ_ya8]

### Work Summary:

[In Start(), I added listerners for a button and ContentPositioningBehaviour. In SpawnNewMarker(), 
I created a counter to see how many clicks I had, and stored a first marker and a second marker in GameObjects called start_marker and end_marker. Then, I set m_nowState to AnimatingLineDraw to draw a line between the two markers. In Update(), I drew a line and display the distance in meters between the two markers. Then, I set m_nowState to ReadyToSpawnAstronaut to spawn an astronaut. When m_nowState is ReadyToSpawnAstronaut, I spawned an astronaut, then spawned a cube above the astronaut’s head when the astronaut is selected. In addtion, Whenever an user clicks "Clear" button, the sscene is reset. At last, I did a bonus task: Adding sound-effects when placing the astronaut on the ground, and when the cubes hit the astronaut.]
