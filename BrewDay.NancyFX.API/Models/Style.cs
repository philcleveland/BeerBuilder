using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrewDay.NancyFX.API.Models
{
    /// <summary>
    /// See http://www.bjcp.org/styles04/
    /// Also apply attribution.
    /// </summary>
    public class Style
    {
        public string Name { get; set; }
        public double OGLower { get; set; }
        public double OGUpper { get; set; }
        public double FGLower { get; set; }
        public double FGUpper { get; set; }
        public double IBULower { get; set; }
        public double IBUUpper { get; set; }
        public double SRMLower { get; set; }
        public double SRMUpper { get; set; }
        public double ABVLower { get; set; }
        public double ABVUpper { get; set; }
        public string Description { get; set; }
    }
}