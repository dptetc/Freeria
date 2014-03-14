using System;
using System.IO;
using System.Windows.Forms;
namespace Freeria
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			using (Main main = new Main())
			{
				try
				{
					for (int i = 0; i < args.Length; i++)
					{
						if (args[i].ToLower() == "-join" || args[i].ToLower() == "-j")
						{
							i++;
							try
							{
								main.AutoJoin(args[i]);
							}
							catch
							{
							}
						}
						if (args[i].ToLower() == "-pass" || args[i].ToLower() == "-password")
						{
							i++;
							Netplay.password = args[i];
							main.AutoPass();
						}
						if (args[i].ToLower() == "-host")
						{
							main.AutoHost();
						}
						if (args[i].ToLower() == "-loadlib")
						{
							i++;
							string path = args[i];
							main.loadLib(path);
						}
						if (args[i].ToLower() == "-dedicated")
						{
							main.DedServ();
						}
						if (args[i].ToLower() == "-config")
						{
							i++;
							main.LoadDedConfig(args[i]);
						}
						if (args[i].ToLower() == "-port")
						{
							i++;
							try
							{
								int serverPort = Convert.ToInt32(args[i]);
								Netplay.serverPort = serverPort;
							}
							catch
							{
							}
						}
						if (args[i].ToLower() == "-players" || args[i].ToLower() == "-maxplayers")
						{
							i++;
							try
							{
								int netPlayers = Convert.ToInt32(args[i]);
								main.SetNetPlayers(netPlayers);
							}
							catch
							{
							}
						}
						if (args[i].ToLower() == "-pass" || args[i].ToLower() == "-password")
						{
							i++;
							Netplay.password = args[i];
						}
						if (args[i].ToLower() == "-world")
						{
							i++;
							main.SetWorld(args[i]);
						}
						if (args[i].ToLower() == "-worldname")
						{
							i++;
							main.SetWorldName(args[i]);
						}
						if (args[i].ToLower() == "-motd")
						{
							i++;
							main.NewMOTD(args[i]);
						}
						if (args[i].ToLower() == "-banlist")
						{
							i++;
							Netplay.banFile = args[i];
						}
						if (args[i].ToLower() == "-autoshutdown")
						{
							main.autoShut();
						}
						if (args[i].ToLower() == "-secure")
						{
							Netplay.spamCheck = true;
						}
						if (args[i].ToLower() == "-autocreate")
						{
							i++;
							string newOpt = args[i];
							main.autoCreate(newOpt);
						}
					}
					main.Run();
				}
				catch (Exception ex)
				{
					try
					{
						using (StreamWriter streamWriter = new StreamWriter("crashlog.txt", true))
						{
							streamWriter.WriteLine(DateTime.Now);
							streamWriter.WriteLine(ex);
							streamWriter.WriteLine("");
						}
						MessageBox.Show(ex.ToString(), "Freeria: Error");
					}
					catch
					{
					}
				}
			}
		}
	}
}
