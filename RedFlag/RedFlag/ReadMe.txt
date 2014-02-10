RedFlag Debugger version 1.5
============================

   Red Flag is a special-purpose debugger for .NET 2.0 and 4.0 managed-code
   applications. It is designed to trap managed exceptions, then dump the stack
   frame from the exception thread and dump the local variables of each function
   in the stack trace. The results can then be output to an XML file for later analysis.

   The debugger can either start a new process or attach to an existing one. The latter
method is more suitable when the application being debugged throws many exceptions
on startup. To start a debugging session, choose either "Launch process" or "Attach"
from the "Actions" menu.

   You may also get Red Flag to launch a program for debugging as soon as it starts.
This is useful when using the "Image file execution options" key to trap a managed
crash that happens as soon as a program loads, not giving you a chance to hook in a
debugger. Red Flag accepts the following launch arguments:

	RedFlag /LAUNCH:"<program path>" /ARGUMENTS:"<string of program arguments>"

   Once an exception occurs, you may highlight the exception in the "Exceptions" list,
then the "Stack Trace" and "local variables" lists will switch to the stack frame
on which the exception had occurred. Choosing a different method in the "stack trace"
list will change the "local variables" list to reflect the local variables that were
in memory for that particular method.

	The "stack objects" tab shows information about the methods in the currently-selected
exception's stack trace. You can work your way up and down the methods in the stack
trace using the "Next" and "Previous" buttons. For each method, you can see the arguments
supplied to the method and the local variables. You may sort local variables by name,
type, and value, and double-clicking a variable will show the hierarchy that the object
has inherited all the way to the root object.

	The "Assemblies" tab will show the list of assemblies that had been loaded into
the process being debugged. The list will include only .NET modules, listed by FullName,
which includes the name, version, and public key token.

   All lists in Red Flag support copy (CTRL+C) operations so data can be exported.
Copying from the stack trace window will copy the whole stack trace, while all
other lists will only copy the currently-selected item.

   The entire set of exceptions can be saved to an XML file using the "file" menu. 
This allows Red Flag to be used off-site so that the customer can save the exceptions
and email them to a support technician, who can analyze the data by loading it into
his or her copy of Red Flag.

	In addition to exceptions, RedFlag will allow you to dump the call stacks from all 
threads of the application currently being debugged. This is useful for troubleshooting
program hangs, reverse-engineering, and satisfying your curiosity in general. This 
functionality is in the Action menu->"Dump Stacks".

	Advanced - not for the faint-hearted! Increasing these values will affect performance
and/or run you out of memory. You can specify some of the debugger options on the command line
as well as in the Settings menu:
/OBJECTDEPTH:<n> The recursion level of reporting dependent objects (default is 4)
/STACKLENGTH:<n> How many frames to report in the stack trace (default is 5)
/PROCESSARRAYS Gather memory values for array and list members (default is false)

/BREAKPOINT:<source(line)> You can enter a breakpoint that will stop the code and dump the stacks.
Note that you need the PDB in the assembly folder. If the breakpoint is reached, an exception called
"RedFlag.BreakpointReached" will appear in the Exceptions list.

NOTES: Launching ASP .NET applications is a shortcut to debugging W3wp.exe. Of course you can always 
attach to w3wp.exe and Flag is nice enough to list the app pool name. Some web apps like SharePoint
can restrict resources to a certain app pool - in that case the app pool started by RedFlag is called
"RedFlagAppPool".

There are two separate programs for x86 and x64 - I can't seem to consolidate the two, but if
you launch the wrong one you will be warned and then you can launch the other version.

v1.0 (initial release)
v1.1 - UI changes (add stack trace tab and stack objects sortable list, clipboard support)
v1.2 - Added "launch at start", Exception time, sort Exceptions, ability to detach from process
v1.3 - Added stack dumps, fix problem with command args not dealing with quotes around them
		added "Assemblies" tab.
v1.3.0.1 - Added exception "messages" to the exception window and function arguments to the 
			local variable list (although they only nest 1 level).
v1.3.0.2 - Worked around inability to serialize exception time (TimeSpan). Added detection of
32/64 bit applications. Added command-line support for objectdepth, stacklength, and processarrays
v1.3.0.3 - Removed attempt to detach on process exit that resulted in a debug assert, fixed 32-bit
detection on launched processes, and added IIS Application Pool detection to attach to process.
v1.3.0.4 - Added reporting of processes contesting for locked files. This appears in the exception
message. Also disabled JIT optimizations where possible so it's less likely to show stack object
values as "N/A".
v1.3.0.5 - Fixed hang in locked file detection. Also set the DebugEngine to a background thread
because SmartAssembly will not exit properly if all threads are foreground threads.
v1.3.0.6 - Added breakpoint support.
v1.3.0.7 - Bugfixes - breakpoints always monitored even when no breakpoint set. Attempt to disable
JIT inlining when the process failed to launch.
v1.3.0.8 - Started collecting assembly file path in addition to full name and make the list sortable.
v1.3.0.9 - Add ability to launch IIS 6/7 web applications
v1.4.0.0 - Add support for debugging .NET 4.0, add settings dialog in UI. Bugs - delete all breakpoints
before detaching, ensure variables are serializable, allow settings change while debugging, correctly
identify "User entry breakpoint" (automatically thrown by the debugger)
v1.4.0.1 - Fix for crash enuming processes when processes exit, Add "clear" menu item, moved serialization
check from the stackobject class to the results-saving function (for speed).
v1.4.0.2 - Fix for ASP .NET on Win7, IISADMIN service no longer exists.
v1.5.0.0 - Add ad-hoc process stop, change exception handling in Engine, add Debug.Trace monitor,
changing settings no longer stops debugging, add source code locator
v1.5.0.1 - Add more tooltips. Save application info with results, Diagnose PDB load failures.
v1.5.0.5 - Fix for launching SSms.exe throwing invalid value exceptions.
v1.5.0.6 - Add support for ignoring certain types of exceptions (saves memory).
v1.5.0.7 - Fix .NET 2 assembly runtime version detection (worked on mixed f/w).
v1.5.0.8 - Double-clicking a method opens source file (or Reflector, if Reflector is installed
and .NET Reflector shell integration is enabled).
v1.5.0.9 - Added heap statistics
v1.5.0.10 - Add search for variables
v1.5.0.11 - Fix broken hierarchy window
v1.5.0.12 - Show process handles
v1.5.0.13 - Remove user entry breakpoint, because what's the point?