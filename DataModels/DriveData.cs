using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCommander.DataModels
{
    public class DriveData
    {
        public string Name { get; set; }
        public string VolumeLetter { get; set; }
        public string DriveDesc
        {
            get
            {
                return $"{Name} ({VolumeLetter})";
            }
        }
        public DriveData(string name, string letter)
        {
            this.Name = name;
            this.VolumeLetter = letter;
        }
    }
}
