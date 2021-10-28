# timecodes
Timecode arithmetic (add, subtract, sort) with c#

A Timecode consists of hours, minutes, seconds and frames, in the following form: hh:mm:ss:ff
where:
hh is the number of hours
mm is the number of minutes
ss is the number of seconds
ff is the number of frames

1 second has 25 frames

# Testcases

1A. Given 2 timecodes, provide a method to do an addition and return the result. Example: 13:30:00:20 + 01:15:00:10 = 14:45:01:05

1B. Given 2 timecodes, provide a method to do a subtraction and return the difference. Example: 13:30:00:10 - 01:15:00:20 = 12:14:59:15

1C. Provide a method that can be used to sort an array/list of timecodes

