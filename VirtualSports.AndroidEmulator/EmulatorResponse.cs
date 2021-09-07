using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualSports.AndroidEmulator
{
    public class EmulatorResponse
    {
        public bool Successful { get; init; }
        public string Error { get; init; } = string.Empty;
        public string Raw { get; init; }

        public EmulatorResponse(string raw)
        {
            Raw = raw;
            if (raw.StartsWith("OK") || raw.Contains("Android Console: type 'help' for a list of commands"))
            {
                Successful = true;
            }
            else
            {
                Successful = false;
                Error = raw.Substring(4).Trim();
            }
        }

    }
}
