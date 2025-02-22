﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GoogleHomePageWebScraper.Data
{
    class ScrapeCriteria
    {
        public ScrapeCriteria() // Constructer
        {
            Parts = new List<ScrapeCriteriaPart>();
        }

        public string Data { get; set; }
        public string Regex { get; set; }
        public RegexOptions RegexOption { get; set; } // using Regex Directive
        public List<ScrapeCriteriaPart> Parts { get; set; }
    }
}
