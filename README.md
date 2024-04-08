# TextToInput
Tool to convert a text string into libTAS input

Though not possible through usual means, libTAS is able to send an arbitrary amount of keystrokes in a single frame. 
Though not useful in "normal" gameplay, any section with typing can be sped up a good amount by specifying exactly which keys to press in which order.
Until now this was done manually by editing the input file inside the .ltm, this app aims to make the process easier by automatically generating the inputs from a string of text, which you can then paste into the input editor.

Only upper and lower case letters and numbers have been tested and are confirmed to work, though other symbols might. 
