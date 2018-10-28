### INFO 5340 / CS 5650
### Virtual and Augmented Reality 
# Assignment 0

Read: [Assignment Instructions](https://docs.google.com/document/d/1u9eCKspERhOQdH5Uw3BRDCHFSyydmr2SG5bcwxrp5YQ/edit?usp=sharing "Detailed Assignment Instructions")

<hr>

### Student Name:

[Sungseo Park]

### Student Email:

[sp2528@cornell.edu]

### Solution (Screen Recording):

[https://youtu.be/LyxvkPv7lqY]

### Work Summary:

[In SceneController, I set up references for SimpleAnimator and Button, and added a listener to the Button in Start(). When the button listens to click inputs, it calls MoveTowardsOnClick() and triggers SimpleAnimator with SimpleAnimationData. After then, SimpleAnimator moves or rotates the TextMesh according to its AnimationState. In addition, I did a bonus task: The “Set New Position” button is not interactable in the middle of an active animation. The most difficult part was the use of Lerp(). If I use MoveTowards(), the TextMesh moves towards at constant speed. However, I have not found a way to maintain constant speed using Lerp().]
