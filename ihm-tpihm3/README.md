# IHM3 - Debugging 
## The topic
We were handed a glitchy code which was meant to select a file, display its text, and display it as only uppercase or only lowercase.
We had to fix it.

Here's what we should have:

<img src="img/filereader.JPG" width="40%"/>
<img src="img/filereadercomplete-US.jpg" width="45%"/>

This is a very simple application which should offer the following functionalities:
- Enter the name of a file in the application's current directory and click on the "Open" button to display the contents on the right (for the moment, you can use the "ToBeOrNotToBe.txt" file already supplied, but you could use another).
    - If no file name is entered, the program should display a MessageBox with a corresponding error message.
    - If a file name is entered but not found, the program should display a MessageBox with a corresponding error message.

- Alternatively, click on the "Choose File..." button, which opens a file selection dialog box. The result is the same, with the file displayed on the right.
- Display the number of words in the file that has just been loaded at the bottom.
- Use the ComboBox at top right to change the text displayed to either upper or lower case, or to its default value.