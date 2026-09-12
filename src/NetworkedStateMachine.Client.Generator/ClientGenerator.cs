namespace NetworkedStateMachine.Client.Generator;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

[Generator(LanguageNames.CSharp)]
public partial class ClientGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        Console.WriteLine("hi");
    }
}
