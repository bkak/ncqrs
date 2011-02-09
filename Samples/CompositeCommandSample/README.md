COMPOSITE COMMAND SAMPLE
===========================

This sample application contains a sample to create Composite Commands in NCQRS.
Composite Command is a collection of Commands.
Composite Command will be issued from the Client and Command Service will identify whether it is a Composite Command and execute 
each command of the collection in same UOW on same AR.
UOW is handled by IOC Castle Windsor and its lifestyle is "PerWebRequest".
This way we can manage to create a single UOW for each web request.
First Command will either Create the AR or get the AR from DomainRepository and all subsequent Commands of the CompositeCommand will be
executed on the same AR.

Sample
-------------------------

A small test is created to demonstrate this.

Media Plan is the AR, and 2 Commands CreateMediaPlan and SetBudgetForMediaPlan are bundled in one Composite Command.

CONTACT
-------

If you have any feedback, contact me via:
	- Twitter: <http://twitter.com/bhoomikakaiya>
	- Mail: bhoomi.kakaiya@gmail.com