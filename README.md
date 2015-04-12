# TwinEdit
An editor to make organising your Twine Projects easier and some smaller additional features.

This project is inspired by Twee and Twine, and allows you to produce Twee files that can be reused in other Twine projects. It features the ability to categorise passages into meaningful groups. Even when these groups get large, you will be able to search for Titles, Tags and the Contents of passages from within that category.

Each category has tags that can be assigned, which will automatically apply to every passage you create in that category, making it even easier to organise your project.

This project also comes with the [Fast Colored Text Box](https://github.com/PavelTorgashov/FastColoredTextBox), written by Pavel Torgashov. This means that Passages use HTML syntax highlighting, and script files come with JavaScript highlighting, making it easier for you to produce content for your game.

The interface is designed to be easy and simple to use. This project requires the .NET Framework 4.0 or later.

It optionally requires Python 2.7 or later and the Twee source engine, should you choose to use the Build function in the Project menu at the top of the main window. This will produce a batch file that can be run if Python 2.7 has been installed correctly. Unfortunately, attempting to automatically run this file (as you may notice in the code files) will produce errors that seems to be irreparable.

It is recommended to use this project to produce Twee files and reuse in Twine itself as the Twee source engine I believe to be bugged: 

executing functions in the link part of a double brackets doesn't seem to parse correctly by the engine; Sugarcane does not like returning to the Start passage when you have a link to it. There are probably other bugs to it as well. At the end of the day it's not been touched for 3 years.

To execute the project, simply download the ZIP file and follow the source code path to bin/debug/TwinEdit.exe
