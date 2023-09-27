using System;
using System.Text;

namespace ClassHypertext
{
	public class Hypertext
    {
		private string _PlantUML { get; set; }
        public String PlantUMLHypertext()
        {
            _PlantUML = @"@startuml
	class Program
	    {
			- _PlantUML : string
	        Hypertext()
            [[mainDiagram.svg]]
	    }
@enduml";
            return _PlantUML;
        }
    }
}
