EventLog Analyzer
=================

This is a utility I wrote a few years ago for automating analysis of Event Log files exported from production machines.
The idea was to automate identifying and classifying similar Event Log records to get a quick overview of all the failure types and to focus on and fix the most frequent failure types.

It reads a bunch of Event Log files (*.evt) and automatically groups related Event Log records based on their similarity.
Once the analysis is complete, it writes out a CSV each for every group of related events found, and a summary text with a bunch of statistics.
You can provide it some filters that tell it to look at only the Event Log records that match a certain criteria.

Warning: It can be quite slow if you're processing a large number of Event Log records.

Hopefully, I'll get around to fixing the performance when I get some time (now that I've reopened the project after over 3 years :D).

### Building the Project
This project has now been migrated to use .NET 6. To build it:
1. Clone the repository.
2. Navigate to the root directory of the cloned repository.
3. Run the command: `dotnet build EventLogAnalyzer.sln`

The main executable will be found in `Console Client/bin/Debug/net6.0/` or `Console Client/bin/Release/net6.0/` depending on the build configuration.

### Commandline Options:
  Pre-filters::
    Time: -after -before -at
    Type: -type :: Error|FailureAudit|Information|SuccessAudit|Warning
    Message: -startsWith -contains
    Computer: -computer
    Source: -source
    EventId: -eventId

  Processing options:
    -tolerance :: Tolerance threshold for determining similarity of two Event log messages.
                  Defaults to 0. i.e. Maximum accuracy => Least fuzzy => More groups
    
  Output options: 
    Fields in output reports: -include (FieldNames|All) :: FieldNames:= Type|DateTime|Source|Category|EventId|User|Computer|Message
    Output Folder: -output :: Folder must exist

References
----------
* CodeProject article on parsing Event Log files: [http://www.codeproject.com/KB/string/EventLogParser.aspx](http://www.codeproject.com/KB/string/EventLogParser.aspx), by [J a a n s](http://www.codeproject.com/Members/J-a-a-n-s)
* MSDN Reference on Event Log file format: [http://msdn.microsoft.com/en-us/library/bb309026(VS.85).aspx]

TODO:
-----
* Improve performance (Parallelize log record comparisons?)
* Bring back/rewrite the GUI client
* Refactor/cleanup.
