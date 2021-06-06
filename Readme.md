# Pachy Picture Frame
by pachipon@VRC
https://pachipawnshop.booth.pm/
A picture slide show made in UdonSharp. It is meant to be used in a VRChat SDK3 world.

## Features
 - Ordered and shuffle options
 - Picture sync between players
 - Multiple picture frames in one world supported

## Installation
 - Grab the Prefab and place it into your scene.
	 - You may scale the prefab on the X and Y axes
 - Locate the prefab script settings under the Udon Behavior component
 - Set the Picture List size
 - Add pictures to the list
 - If you want a randomized list per instance, select Shuffle List
 - Set the time between pictures
 - Done!

## Quirks
Picture Material in the Mesh Renderer and Screen Material in the Udon Behavior script must be the same.
There is no aspect ratio lock or auto resizing of different orientations. This prefab comes with landscape and portrait orientations in a 16:9 ratio

## Changelog
- 2021/06/04 first release (VRCSDK3 2021.04.21.11.57)

## Contact
 - @kdingo on Twitter
 - pachipon on VRC

## Credit

[Shuffle algorithm](https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle)
[Source code on Github](https://github.com/kdingo)

## Thanks to

My wonderful VRC friends

## License
This work is licensed under Creative Commons BY-SA
- EN: https://creativecommons.org/licenses/by-sa/4.0/
- JP: https://creativecommons.org/licenses/by-sa/4.0/deed.ja

> Written with [StackEdit](https://stackedit.io/).
