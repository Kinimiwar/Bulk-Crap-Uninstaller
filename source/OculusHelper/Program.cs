﻿/*
    Copyright (c) 2018 Marcin Szeniak (https://github.com/Klocman/)
    Apache License Version 2.0
*/

using System;
using System.IO;
using Klocman;

namespace OculusHelper
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            if (args.Length == 0)
                return HelperTools.InvalidArgumentCode;

            HelperTools.SetupEncoding();

            if (args.Length == 1 && string.Equals(args[0], @"/query", StringComparison.OrdinalIgnoreCase))
                try
                {
                    var result = OculusManager.QueryOculusApps();
                    foreach (var app in result)
                        Console.WriteLine(HelperTools.ObjectToConsoleOutput(app));
                    return HelperTools.OkCode;
                }
                catch (IOException ex)
                {
                    LogWriter.WriteExceptionToLog(ex);
                    return HelperTools.HandleHrefMessage(ex);
                }
                catch (Exception ex)
                {
                    LogWriter.WriteExceptionToLog(ex);
                    return HelperTools.FunctionFailedCode;
                }

            if (args.Length == 2 && string.Equals(args[0], @"/uninstall", StringComparison.OrdinalIgnoreCase))
                try
                {
                    OculusManager.RemoveApp(args[1]);
                    return HelperTools.OkCode;
                }
                catch (IOException ex)
                {
                    return HelperTools.HandleHrefMessage(ex);
                }
                catch (Exception ex)
                {
                    LogWriter.WriteExceptionToLog(ex);
                    return HelperTools.FunctionFailedCode;
                }

            return HelperTools.InvalidArgumentCode;
        }
    }
}