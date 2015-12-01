# Quail

***Welcome to Quail!***

Quail is a real-time log viewing program that
supports large files with color highlighting
and search functionality.  Quail also provides
a front end to LogParser enabling SQL query
support for your logs.

## Usage
```
Usage: 	 Quail.exe filename
Hot Keys:
		CTRL-M: Mark with date and time
		CTRL-H: Help / Home screen
		CTRL-O: Open file
		CTRL-C: Copy selected lines, CTRL-Q: Copy selected without blanks
		CTRL-P: Toggle pause
		CTRL-L: Open default log configured in options.xml
		CTRL-F: Find
		CTRL-T: Toggle Transparent
		F3: Find next, Shift-F3: Find Previous
		F4: View last error
		F5: Show all errors
		F9: Edit Log: Run edit log command
		F6: Clear window only
		CTRL-1: Position Window in top 3rd 
		CTRL-2: Top half
		CTRL-3: Bottom 3rd
		CTRL-4: Bottom half
		F11: Fullscreen
		Drag and drop a file to start tailing.
		
		In bottom view panel select text and:
		CTRL-C to copy just a part of a line
		Or Right click for more options like:
			Edit file
		
		Go to Tools / Options to customize settings...   
```
		
##Advanced Options Tab

###Bayesian Filter
Turning on the Bayesian Filter is like turning on a spam filter. It will
decide if it should show you log messages or not based on how you have trained it. You can
right click on log messages and choose to either train them as hide or show. You may modify these if
you wish. Viewing the selected score will show you the probability that the message will be
shown with Bayesian Filtering on. A score of .99 or higher is required.
            