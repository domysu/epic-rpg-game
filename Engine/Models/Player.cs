using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    internal class Player
    {
        public string Name {  get; set; }
        public string CharacterClass {  get; set; }
        int HitPoints {  get; set; }
        int ExperiencePoints {  get; set; }
        int Level { get; set; }
        int Gold { get; set; }

    }
}
