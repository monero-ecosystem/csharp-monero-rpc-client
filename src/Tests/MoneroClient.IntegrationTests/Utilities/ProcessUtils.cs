using MoneroClient.IntegrationTests.Constants;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MoneroClient.IntegrationTests.Utilities
{
    public static class BackgroundProcessUtility
    {
        /// <summary>
        /// Invokes the host process for test service
        /// </summary>
        public static void Start()
        {
           // StartDaemon();

            StartWalletRpc();
        }

        private static void StartWalletRpc()
        {
            var walletDirectory = Directory.CreateDirectory("monero-wallets").FullName;
            var fullPath = Path.Combine(TestingConstants.GuiWalletDirectory, $"{TestingConstants.WalletRpcProcessName}.exe");

            if (!IsProcessRunning(TestingConstants.WalletRpcProcessName))
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = @"powershell.exe",
                    Arguments = $"& '{fullPath}' --wallet-dir '{walletDirectory}' --rpc-bind-port {TestingConstants.DefaultPort} --disable-rpc-login --testnet",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,

                };
                Process process = new Process
                {
                    StartInfo = startInfo
                };
                process.Start();

            }
        }

            private static void StartDaemon()
        {
            if (!IsProcessRunning(TestingConstants.DaemonProcessName))
            {
                ProcessStartInfo info = new ProcessStartInfo(Path.Combine(TestingConstants.GuiWalletDirectory, $"{TestingConstants.DaemonProcessName}.exe"))
                {
                    UseShellExecute = true,
                    WorkingDirectory = TestingConstants.GuiWalletDirectory,
                    Arguments = $"--testnet --rpc-bind-port {TestingConstants.DefaultPort}"

                };
                Process.Start(info);




               
            }
        }

        /// <summary>
        /// Kills the process of service host
        /// </summary>
        public static void Stop()
        {
            if (IsProcessRunning(TestingConstants.WalletRpcProcessName))
            {
                Process.GetProcessesByName(TestingConstants.WalletRpcProcessName).ToList().ForEach(x => x.Kill());
            }

            if (IsProcessRunning(TestingConstants.WalletRpcProcessName))
            {
                Process.GetProcessesByName(TestingConstants.DaemonProcessName).ToList().ForEach(x => x.Kill());
            }
        }

        private static bool IsProcessRunning(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length != 0;
        }
    }
}
