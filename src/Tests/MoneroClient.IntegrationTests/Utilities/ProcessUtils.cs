using Monero.Client.Daemon;
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
            //StartDaemon();

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
                    Arguments = $"& '{fullPath}' --wallet-dir '{walletDirectory}' --rpc-bind-port {TestingConstants.DefaultTestNetPort} --testnet --daemon-address http://testnet.community.rino.io:28081 --untrusted-daemon --disable-rpc-login --log-level 1",
                    //Arguments = $"& '{fullPath}' --wallet-dir '{walletDirectory}' --rpc-bind-port 18082  --daemon-address http://node.community.rino.io:18081 --untrusted-daemon --disable-rpc-login",
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

                // todo: don't sleep on a thread, check another way
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            }
        }

            public static async void StartDaemon()
        {

            
               
            //using IMoneroDaemonClient nodeClient = await MoneroDaemonClient.CreateAsync(Monero.Client.Network.MoneroNetwork.Testnet);
            using IMoneroDaemonClient nodeClient = await MoneroDaemonClient.CreateAsync("testnet.community.rino.io", 28081);
             
            try
            {
                //var connections = await nodeClient.GetConnectionsAsync();

                //foreach (var con in connections)
                //{
                //    Debug.WriteLine(con);
                //}



            }
            catch { }

                //if (!IsProcessRunning(TestingConstants.DaemonProcessName))
                //{
                //    ProcessStartInfo info = new ProcessStartInfo(Path.Combine(TestingConstants.GuiWalletDirectory, $"{TestingConstants.DaemonProcessName}.exe"))
                //    {
                //        UseShellExecute = true,
                //        WorkingDirectory = TestingConstants.GuiWalletDirectory,
                //        Arguments = $"--testnet --rpc-bind-port {TestingConstants.DefaultPort}"

                //    };
                //    Process.Start(info);
                //}
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
