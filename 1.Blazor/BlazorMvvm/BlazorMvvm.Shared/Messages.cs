using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorMvvm.Shared
{
    public record class ConvertHexToAsciiMessage(string hexToConvert);
    public record class ConvertAsciiToHexMessage(string asciiToConvert);
}
