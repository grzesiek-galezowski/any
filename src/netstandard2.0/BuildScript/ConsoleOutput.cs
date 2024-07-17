using System;
using NScan.SharedKernel.WritingProgramOutput.Ports;

namespace BuildScript;

public class ConsoleOutput : INScanOutput
{
  public void WriteAnalysisReport(string analysisReport)
  {
    Console.WriteLine(analysisReport);
  }

  public void WriteVersion(string coreVersion)
  {
    Console.WriteLine(coreVersion);
  }
}
