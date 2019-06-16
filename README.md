
# MOM_Assignment
Technical Assessment

## Author : Anand Ramamoorthy
## Date  : June 16, 2019    

The assignment is written in VS 2017 (.Net Framework 4.6.1 Standard) in C#

The whole is assignment have been breaken into 4 Assembilies 
1. Anand - Console Application - Main Program
1. Anand.Unit - Cosole Application - Simple Unit Test
1. MASClient - MAS Client Module - Class Library (Compiled codebase for this Business Requiremnts)
1. APIBase - Class Library - Base Library to connect to External Websites thru API

### APIBase
1. Provides the necessary mechanism to connect to the external website
2. Retrieves the set of the records based on the Record (Genereic Type) and loads accordingly

### MASClient
1. class MAS (Main Class) - All the needed necessary methods relating to this assignment 
2. Record Class - The JSon data extracted from MAS has been loaded into this format for further processing
3. Uses the APIBase Module to connect

### Anand
 The Main console application that consumes the above two assembilies 

### Anand.Unit
 - A simple console unit test application that consumes the above two assembilies 
 - As of now only used to test date formats

## Build - Please use the MS Build or VS 2017 to build and run this application
 1. Once built please run the console application and follow the prompts.
 2. To quite the console application please enter Q
  
 ### Final Words
 Given the time limit I have done the maximum though there are rooms for enhancements and improvemens.
 
