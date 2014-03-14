using System;
using System.Text;
namespace Freeria
{
	public class messageBuffer
	{
		public const int readBufferMax = 65535;
		public const int writeBufferMax = 65535;
		public bool broadcast;
		public byte[] readBuffer = new byte[65535];
		public byte[] writeBuffer = new byte[65535];
		public bool writeLocked;
		public int messageLength;
		public int totalData;
		public int whoAmI;
		public int spamCount;
		public int maxSpam;
		public bool checkBytes;
		public void Reset()
		{
			this.writeBuffer = new byte[65535];
			this.writeLocked = false;
			this.messageLength = 0;
			this.totalData = 0;
			this.spamCount = 0;
			this.broadcast = false;
			this.checkBytes = false;
		}
		public void GetData(int start, int length)
		{
			if (this.whoAmI < 256)
			{
				Netplay.serverSock[this.whoAmI].timeOut = 0;
			}
			else
			{
				Netplay.clientSock.timeOut = 0;
			}
			int num = 0;
			num = start + 1;
			byte b = this.readBuffer[start];
			if (Main.netMode == 1 && Netplay.clientSock.statusMax > 0)
			{
				Netplay.clientSock.statusCount++;
			}
			if (Main.verboseNetplay)
			{
				for (int i = start; i < start + length; i++)
				{
				}
				for (int j = start; j < start + length; j++)
				{
					byte arg_85_0 = this.readBuffer[j];
				}
			}
			if (Main.netMode == 2 && b != 38 && Netplay.serverSock[this.whoAmI].state == -1)
			{
				NetMessage.SendData(2, this.whoAmI, -1, "Incorrect password.", 0, 0f, 0f, 0f, 0);
				return;
			}
			if (Main.netMode == 2 && Netplay.serverSock[this.whoAmI].state < 10 && b > 12 && b != 16 && b != 42 && b != 50 && b != 38)
			{
				NetMessage.BootPlayer(this.whoAmI, "Invalid operation at this state.");
			}
			if (b == 1 && Main.netMode == 2)
			{
				if (Main.dedServ && Netplay.CheckBan(Netplay.serverSock[this.whoAmI].tcpClient.Client.RemoteEndPoint.ToString()))
				{
					NetMessage.SendData(2, this.whoAmI, -1, "You are banned from this server.", 0, 0f, 0f, 0f, 0);
					return;
				}
				if (Netplay.serverSock[this.whoAmI].state == 0)
				{
					string @string = Encoding.ASCII.GetString(this.readBuffer, start + 1, length - 1);
					if (!(@string == "Freeria" + Main.curRelease))
					{
						NetMessage.SendData(2, this.whoAmI, -1, "You are not using the same version as this server.", 0, 0f, 0f, 0f, 0);
						return;
					}
					if (Netplay.password == null || Netplay.password == "")
					{
						Netplay.serverSock[this.whoAmI].state = 1;
						NetMessage.SendData(3, this.whoAmI, -1, "", 0, 0f, 0f, 0f, 0);
						return;
					}
					Netplay.serverSock[this.whoAmI].state = -1;
					NetMessage.SendData(37, this.whoAmI, -1, "", 0, 0f, 0f, 0f, 0);
					return;
				}
			}
			else
			{
				if (b == 2 && Main.netMode == 1)
				{
					Netplay.disconnect = true;
					Main.statusText = Encoding.ASCII.GetString(this.readBuffer, start + 1, length - 1);
					return;
				}
				if (b == 3 && Main.netMode == 1)
				{
					if (Netplay.clientSock.state == 1)
					{
						Netplay.clientSock.state = 2;
					}
					int num2 = (int)this.readBuffer[start + 1];
					if (num2 != Main.myPlayer)
					{
						Main.player[num2] = (Player)Main.player[Main.myPlayer].Clone();
						Main.player[Main.myPlayer] = new Player();
						Main.player[num2].whoAmi = num2;
						Main.myPlayer = num2;
					}
					NetMessage.SendData(4, -1, -1, Main.player[Main.myPlayer].name, Main.myPlayer, 0f, 0f, 0f, 0);
					NetMessage.SendData(16, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
					NetMessage.SendData(42, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
					NetMessage.SendData(50, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
					for (int k = 0; k < 44; k++)
					{
						NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].inventory[k].name, Main.myPlayer, (float)k, 0f, 0f, 0);
					}
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[0].name, Main.myPlayer, 44f, 0f, 0f, 0);
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[1].name, Main.myPlayer, 45f, 0f, 0f, 0);
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[2].name, Main.myPlayer, 46f, 0f, 0f, 0);
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[3].name, Main.myPlayer, 47f, 0f, 0f, 0);
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[4].name, Main.myPlayer, 48f, 0f, 0f, 0);
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[5].name, Main.myPlayer, 49f, 0f, 0f, 0);
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[6].name, Main.myPlayer, 50f, 0f, 0f, 0);
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[7].name, Main.myPlayer, 51f, 0f, 0f, 0);
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[8].name, Main.myPlayer, 52f, 0f, 0f, 0);
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[9].name, Main.myPlayer, 53f, 0f, 0f, 0);
					NetMessage.SendData(5, -1, -1, Main.player[Main.myPlayer].armor[10].name, Main.myPlayer, 54f, 0f, 0f, 0);
					NetMessage.SendData(6, -1, -1, "", 0, 0f, 0f, 0f, 0);
					if (Netplay.clientSock.state == 2)
					{
						Netplay.clientSock.state = 3;
						return;
					}
				}
				else
				{
					if (b == 4)
					{
						bool flag = false;
						int num3 = (int)this.readBuffer[start + 1];
						if (Main.netMode == 2)
						{
							num3 = this.whoAmI;
						}
						if (num3 == Main.myPlayer)
						{
							return;
						}
						int num4 = (int)this.readBuffer[start + 2];
						if (num4 >= 36)
						{
							num4 = 0;
						}
						Main.player[num3].hair = num4;
						Main.player[num3].whoAmi = num3;
						num += 2;
						byte b2 = this.readBuffer[num];
						num++;
						if (b2 == 1)
						{
							Main.player[num3].male = true;
						}
						else
						{
							Main.player[num3].male = false;
						}
						Main.player[num3].hairColor.R = this.readBuffer[num];
						num++;
						Main.player[num3].hairColor.G = this.readBuffer[num];
						num++;
						Main.player[num3].hairColor.B = this.readBuffer[num];
						num++;
						Main.player[num3].skinColor.R = this.readBuffer[num];
						num++;
						Main.player[num3].skinColor.G = this.readBuffer[num];
						num++;
						Main.player[num3].skinColor.B = this.readBuffer[num];
						num++;
						Main.player[num3].eyeColor.R = this.readBuffer[num];
						num++;
						Main.player[num3].eyeColor.G = this.readBuffer[num];
						num++;
						Main.player[num3].eyeColor.B = this.readBuffer[num];
						num++;
						Main.player[num3].shirtColor.R = this.readBuffer[num];
						num++;
						Main.player[num3].shirtColor.G = this.readBuffer[num];
						num++;
						Main.player[num3].shirtColor.B = this.readBuffer[num];
						num++;
						Main.player[num3].underShirtColor.R = this.readBuffer[num];
						num++;
						Main.player[num3].underShirtColor.G = this.readBuffer[num];
						num++;
						Main.player[num3].underShirtColor.B = this.readBuffer[num];
						num++;
						Main.player[num3].pantsColor.R = this.readBuffer[num];
						num++;
						Main.player[num3].pantsColor.G = this.readBuffer[num];
						num++;
						Main.player[num3].pantsColor.B = this.readBuffer[num];
						num++;
						Main.player[num3].shoeColor.R = this.readBuffer[num];
						num++;
						Main.player[num3].shoeColor.G = this.readBuffer[num];
						num++;
						Main.player[num3].shoeColor.B = this.readBuffer[num];
						num++;
						byte difficulty = this.readBuffer[num];
						Main.player[num3].difficulty = difficulty;
						num++;
						string text = Encoding.ASCII.GetString(this.readBuffer, num, length - num + start);
						text = text.Trim();
						Main.player[num3].name = text.Trim();
						if (Main.netMode == 2)
						{
							if (Netplay.serverSock[this.whoAmI].state < 10)
							{
								for (int l = 0; l < 255; l++)
								{
									if (l != num3 && text == Main.player[l].name && Netplay.serverSock[l].active)
									{
										flag = true;
									}
								}
							}
							if (flag)
							{
								NetMessage.SendData(2, this.whoAmI, -1, text + " is already on this server.", 0, 0f, 0f, 0f, 0);
								return;
							}
							if (text.Length > Player.nameLen)
							{
								NetMessage.SendData(2, this.whoAmI, -1, "Name is too long.", 0, 0f, 0f, 0f, 0);
								return;
							}
							if (text == "")
							{
								NetMessage.SendData(2, this.whoAmI, -1, "Empty name.", 0, 0f, 0f, 0f, 0);
								return;
							}
							Netplay.serverSock[this.whoAmI].oldName = text;
							Netplay.serverSock[this.whoAmI].name = text;
							NetMessage.SendData(4, -1, this.whoAmI, text, num3, 0f, 0f, 0f, 0);
							return;
						}
					}
					else
					{
						if (b == 5)
						{
							int num5 = (int)this.readBuffer[start + 1];
							if (Main.netMode == 2)
							{
								num5 = this.whoAmI;
							}
							if (num5 == Main.myPlayer)
							{
								return;
							}
							lock (Main.player[num5])
							{
								int num6 = (int)this.readBuffer[start + 2];
								int stack = (int)this.readBuffer[start + 3];
								string string2 = Encoding.ASCII.GetString(this.readBuffer, start + 4, length - 4);
								if (num6 < 44)
								{
									Main.player[num5].inventory[num6] = new Item();
									Main.player[num5].inventory[num6].SetDefaults(string2);
									Main.player[num5].inventory[num6].stack = stack;
								}
								else
								{
									Main.player[num5].armor[num6 - 44] = new Item();
									Main.player[num5].armor[num6 - 44].SetDefaults(string2);
									Main.player[num5].armor[num6 - 44].stack = stack;
								}
								if (Main.netMode == 2 && num5 == this.whoAmI)
								{
									NetMessage.SendData(5, -1, this.whoAmI, string2, num5, (float)num6, 0f, 0f, 0);
								}
								return;
							}
						}
						if (b == 6)
						{
							if (Main.netMode == 2)
							{
								if (Netplay.serverSock[this.whoAmI].state == 1)
								{
									Netplay.serverSock[this.whoAmI].state = 2;
								}
								NetMessage.SendData(7, this.whoAmI, -1, "", 0, 0f, 0f, 0f, 0);
								return;
							}
						}
						else
						{
							if (b == 7)
							{
								if (Main.netMode == 1)
								{
									Main.time = (double)BitConverter.ToInt32(this.readBuffer, num);
									num += 4;
									Main.dayTime = false;
									if (this.readBuffer[num] == 1)
									{
										Main.dayTime = true;
									}
									num++;
									Main.moonPhase = (int)this.readBuffer[num];
									num++;
									int num7 = (int)this.readBuffer[num];
									num++;
									if (num7 == 1)
									{
										Main.bloodMoon = true;
									}
									else
									{
										Main.bloodMoon = false;
									}
									Main.maxTilesX = BitConverter.ToInt32(this.readBuffer, num);
									num += 4;
									Main.maxTilesY = BitConverter.ToInt32(this.readBuffer, num);
									num += 4;
									Main.spawnTileX = BitConverter.ToInt32(this.readBuffer, num);
									num += 4;
									Main.spawnTileY = BitConverter.ToInt32(this.readBuffer, num);
									num += 4;
									Main.worldSurface = (double)BitConverter.ToInt32(this.readBuffer, num);
									num += 4;
									Main.rockLayer = (double)BitConverter.ToInt32(this.readBuffer, num);
									num += 4;
									Main.worldID = BitConverter.ToInt32(this.readBuffer, num);
									num += 4;
									byte b3 = this.readBuffer[num];
									if ((b3 & 1) == 1)
									{
										WorldGen.shadowOrbSmashed = true;
									}
									if ((b3 & 2) == 2)
									{
										NPC.downedBoss1 = true;
									}
									if ((b3 & 4) == 4)
									{
										NPC.downedBoss2 = true;
									}
									if ((b3 & 8) == 8)
									{
										NPC.downedBoss3 = true;
									}
									num++;
									Main.worldName = Encoding.ASCII.GetString(this.readBuffer, num, length - num + start);
									if (Netplay.clientSock.state == 3)
									{
										Netplay.clientSock.state = 4;
										return;
									}
								}
							}
							else
							{
								if (b == 8)
								{
									if (Main.netMode == 2)
									{
										int num8 = BitConverter.ToInt32(this.readBuffer, num);
										num += 4;
										int num9 = BitConverter.ToInt32(this.readBuffer, num);
										num += 4;
										bool flag3 = true;
										if (num8 == -1 || num9 == -1)
										{
											flag3 = false;
										}
										else
										{
											if (num8 < 10 || num8 > Main.maxTilesX - 10)
											{
												flag3 = false;
											}
											else
											{
												if (num9 < 10 || num9 > Main.maxTilesY - 10)
												{
													flag3 = false;
												}
											}
										}
										int num10 = 1350;
										if (flag3)
										{
											num10 *= 2;
										}
										if (Netplay.serverSock[this.whoAmI].state == 2)
										{
											Netplay.serverSock[this.whoAmI].state = 3;
										}
										NetMessage.SendData(9, this.whoAmI, -1, "Receiving tile data", num10, 0f, 0f, 0f, 0);
										Netplay.serverSock[this.whoAmI].statusText2 = "is receiving tile data";
										Netplay.serverSock[this.whoAmI].statusMax += num10;
										int sectionX = Netplay.GetSectionX(Main.spawnTileX);
										int sectionY = Netplay.GetSectionY(Main.spawnTileY);
										for (int m = sectionX - 2; m < sectionX + 3; m++)
										{
											for (int n = sectionY - 1; n < sectionY + 2; n++)
											{
												NetMessage.SendSection(this.whoAmI, m, n);
											}
										}
										if (flag3)
										{
											num8 = Netplay.GetSectionX(num8);
											num9 = Netplay.GetSectionY(num9);
											for (int num11 = num8 - 2; num11 < num8 + 3; num11++)
											{
												for (int num12 = num9 - 1; num12 < num9 + 2; num12++)
												{
													NetMessage.SendSection(this.whoAmI, num11, num12);
												}
											}
											NetMessage.SendData(11, this.whoAmI, -1, "", num8 - 2, (float)(num9 - 1), (float)(num8 + 2), (float)(num9 + 1), 0);
										}
										NetMessage.SendData(11, this.whoAmI, -1, "", sectionX - 2, (float)(sectionY - 1), (float)(sectionX + 2), (float)(sectionY + 1), 0);
										for (int num13 = 0; num13 < 200; num13++)
										{
											if (Main.item[num13].active)
											{
												NetMessage.SendData(21, this.whoAmI, -1, "", num13, 0f, 0f, 0f, 0);
												NetMessage.SendData(22, this.whoAmI, -1, "", num13, 0f, 0f, 0f, 0);
											}
										}
										for (int num14 = 0; num14 < 1000; num14++)
										{
											if (Main.npc[num14].active)
											{
												NetMessage.SendData(23, this.whoAmI, -1, "", num14, 0f, 0f, 0f, 0);
											}
										}
										NetMessage.SendData(49, this.whoAmI, -1, "", 0, 0f, 0f, 0f, 0);
										return;
									}
								}
								else
								{
									if (b == 9)
									{
										if (Main.netMode == 1)
										{
											int num15 = BitConverter.ToInt32(this.readBuffer, start + 1);
											string string3 = Encoding.ASCII.GetString(this.readBuffer, start + 5, length - 5);
											Netplay.clientSock.statusMax += num15;
											Netplay.clientSock.statusText = string3;
											return;
										}
									}
									else
									{
										if (b == 10 && Main.netMode == 1)
										{
											short num16 = BitConverter.ToInt16(this.readBuffer, start + 1);
											int num17 = BitConverter.ToInt32(this.readBuffer, start + 3);
											int num18 = BitConverter.ToInt32(this.readBuffer, start + 7);
											num = start + 11;
											for (int num19 = num17; num19 < num17 + (int)num16; num19++)
											{
												if (Main.tile[num19, num18] == null)
												{
													Main.tile[num19, num18] = new Tile();
												}
												byte b4 = this.readBuffer[num];
												num++;
												bool active = Main.tile[num19, num18].active;
												if ((b4 & 1) == 1)
												{
													Main.tile[num19, num18].active = true;
												}
												else
												{
													Main.tile[num19, num18].active = false;
												}
												if ((b4 & 2) == 2)
												{
													Main.tile[num19, num18].lighted = true;
												}
												if ((b4 & 4) == 4)
												{
													Main.tile[num19, num18].wall = 1;
												}
												else
												{
													Main.tile[num19, num18].wall = 0;
												}
												if ((b4 & 8) == 8)
												{
													Main.tile[num19, num18].liquid = 1;
												}
												else
												{
													Main.tile[num19, num18].liquid = 0;
												}
												if (Main.tile[num19, num18].active)
												{
													int type = (int)Main.tile[num19, num18].type;
													Main.tile[num19, num18].type = this.readBuffer[num];
													num++;
													if (Main.tileFrameImportant[(int)Main.tile[num19, num18].type])
													{
														Main.tile[num19, num18].frameX = BitConverter.ToInt16(this.readBuffer, num);
														num += 2;
														Main.tile[num19, num18].frameY = BitConverter.ToInt16(this.readBuffer, num);
														num += 2;
													}
													else
													{
														if (!active || (int)Main.tile[num19, num18].type != type)
														{
															Main.tile[num19, num18].frameX = -1;
															Main.tile[num19, num18].frameY = -1;
														}
													}
												}
												if (Main.tile[num19, num18].wall > 0)
												{
													Main.tile[num19, num18].wall = this.readBuffer[num];
													num++;
												}
												if (Main.tile[num19, num18].liquid > 0)
												{
													Main.tile[num19, num18].liquid = this.readBuffer[num];
													num++;
													byte b5 = this.readBuffer[num];
													num++;
													if (b5 == 1)
													{
														Main.tile[num19, num18].lava = true;
													}
													else
													{
														Main.tile[num19, num18].lava = false;
													}
												}
											}
											if (Main.netMode == 2)
											{
												NetMessage.SendData((int)b, -1, this.whoAmI, "", (int)num16, (float)num17, (float)num18, 0f, 0);
												return;
											}
										}
										else
										{
											if (b == 11)
											{
												if (Main.netMode == 1)
												{
													int startX = (int)BitConverter.ToInt16(this.readBuffer, num);
													num += 4;
													int startY = (int)BitConverter.ToInt16(this.readBuffer, num);
													num += 4;
													int endX = (int)BitConverter.ToInt16(this.readBuffer, num);
													num += 4;
													int endY = (int)BitConverter.ToInt16(this.readBuffer, num);
													num += 4;
													WorldGen.SectionTileFrame(startX, startY, endX, endY);
													return;
												}
											}
											else
											{
												if (b == 12)
												{
													int num20 = (int)this.readBuffer[num];
													if (Main.netMode == 2)
													{
														num20 = this.whoAmI;
													}
													num++;
													Main.player[num20].SpawnX = BitConverter.ToInt32(this.readBuffer, num);
													num += 4;
													Main.player[num20].SpawnY = BitConverter.ToInt32(this.readBuffer, num);
													num += 4;
													Main.player[num20].Spawn();
													if (Main.netMode == 2 && Netplay.serverSock[this.whoAmI].state >= 3)
													{
														if (Netplay.serverSock[this.whoAmI].state == 3)
														{
															Netplay.serverSock[this.whoAmI].state = 10;
															NetMessage.greetPlayer(this.whoAmI);
															NetMessage.buffer[this.whoAmI].broadcast = true;
															NetMessage.syncPlayers();
															NetMessage.SendData(12, -1, this.whoAmI, "", this.whoAmI, 0f, 0f, 0f, 0);
															return;
														}
														NetMessage.SendData(12, -1, this.whoAmI, "", this.whoAmI, 0f, 0f, 0f, 0);
														return;
													}
												}
												else
												{
													if (b == 13)
													{
														int num21 = (int)this.readBuffer[num];
														if (num21 == Main.myPlayer)
														{
															return;
														}
														if (Main.netMode == 1)
														{
															bool arg_163F_0 = Main.player[num21].active;
														}
														if (Main.netMode == 2)
														{
															num21 = this.whoAmI;
														}
														num++;
														int num22 = (int)this.readBuffer[num];
														num++;
														int selectedItem = (int)this.readBuffer[num];
														num++;
														float x = BitConverter.ToSingle(this.readBuffer, num);
														num += 4;
														float num23 = BitConverter.ToSingle(this.readBuffer, num);
														num += 4;
														float x2 = BitConverter.ToSingle(this.readBuffer, num);
														num += 4;
														float y = BitConverter.ToSingle(this.readBuffer, num);
														num += 4;
														Main.player[num21].selectedItem = selectedItem;
														Main.player[num21].position.X = x;
														Main.player[num21].position.Y = num23;
														Main.player[num21].velocity.X = x2;
														Main.player[num21].velocity.Y = y;
														Main.player[num21].oldVelocity = Main.player[num21].velocity;
														Main.player[num21].fallStart = (int)(num23 / 16f);
														Main.player[num21].controlUp = false;
														Main.player[num21].controlDown = false;
														Main.player[num21].controlLeft = false;
														Main.player[num21].controlRight = false;
														Main.player[num21].controlJump = false;
														Main.player[num21].controlUseItem = false;
														Main.player[num21].direction = -1;
														if ((num22 & 1) == 1)
														{
															Main.player[num21].controlUp = true;
														}
														if ((num22 & 2) == 2)
														{
															Main.player[num21].controlDown = true;
														}
														if ((num22 & 4) == 4)
														{
															Main.player[num21].controlLeft = true;
														}
														if ((num22 & 8) == 8)
														{
															Main.player[num21].controlRight = true;
														}
														if ((num22 & 16) == 16)
														{
															Main.player[num21].controlJump = true;
														}
														if ((num22 & 32) == 32)
														{
															Main.player[num21].controlUseItem = true;
														}
														if ((num22 & 64) == 64)
														{
															Main.player[num21].direction = 1;
														}
														if (Main.netMode == 2 && Netplay.serverSock[this.whoAmI].state == 10)
														{
															NetMessage.SendData(13, -1, this.whoAmI, "", num21, 0f, 0f, 0f, 0);
															return;
														}
													}
													else
													{
														if (b == 14)
														{
															if (Main.netMode == 1)
															{
																int num24 = (int)this.readBuffer[num];
																num++;
																int num25 = (int)this.readBuffer[num];
																if (num25 == 1)
																{
																	if (!Main.player[num24].active)
																	{
																		Main.player[num24] = new Player();
																	}
																	Main.player[num24].active = true;
																	return;
																}
																Main.player[num24].active = false;
																return;
															}
														}
														else
														{
															if (b == 15)
															{
																if (Main.netMode == 2)
																{
																	return;
																}
															}
															else
															{
																if (b == 16)
																{
																	int num26 = (int)this.readBuffer[num];
																	num++;
																	if (num26 == Main.myPlayer)
																	{
																		return;
																	}
																	int statLife = (int)BitConverter.ToInt16(this.readBuffer, num);
																	num += 2;
																	int statLifeMax = (int)BitConverter.ToInt16(this.readBuffer, num);
																	if (Main.netMode == 2)
																	{
																		num26 = this.whoAmI;
																	}
																	Main.player[num26].statLife = statLife;
																	Main.player[num26].statLifeMax = statLifeMax;
																	if (Main.player[num26].statLife <= 0)
																	{
																		Main.player[num26].dead = true;
																	}
																	if (Main.netMode == 2)
																	{
																		NetMessage.SendData(16, -1, this.whoAmI, "", num26, 0f, 0f, 0f, 0);
																		return;
																	}
																}
																else
																{
																	if (b == 17)
																	{
																		byte b6 = this.readBuffer[num];
																		num++;
																		int num27 = BitConverter.ToInt32(this.readBuffer, num);
																		num += 4;
																		int num28 = BitConverter.ToInt32(this.readBuffer, num);
																		num += 4;
																		byte b7 = this.readBuffer[num];
																		num++;
																		int num29 = (int)this.readBuffer[num];
																		bool flag4 = false;
																		if (b7 == 1)
																		{
																			flag4 = true;
																		}
																		if (Main.tile[num27, num28] == null)
																		{
																			Main.tile[num27, num28] = new Tile();
																		}
																		if (Main.netMode == 2)
																		{
																			if (!flag4)
																			{
																				if (b6 == 0 || b6 == 2 || b6 == 4)
																				{
																					Netplay.serverSock[this.whoAmI].spamDelBlock += 1f;
																				}
																				else
																				{
																					if (b6 == 1 || b6 == 3)
																					{
																						Netplay.serverSock[this.whoAmI].spamAddBlock += 1f;
																					}
																				}
																			}
																			if (!Netplay.serverSock[this.whoAmI].tileSection[Netplay.GetSectionX(num27), Netplay.GetSectionY(num28)])
																			{
																				flag4 = true;
																			}
																		}
																		if (b6 == 0)
																		{
																			WorldGen.KillTile(num27, num28, flag4, false, false);
																		}
																		else
																		{
																			if (b6 == 1)
																			{
																				WorldGen.PlaceTile(num27, num28, (int)b7, false, true, -1, num29);
																			}
																			else
																			{
																				if (b6 == 2)
																				{
																					WorldGen.KillWall(num27, num28, flag4);
																				}
																				else
																				{
																					if (b6 == 3)
																					{
																						WorldGen.PlaceWall(num27, num28, (int)b7, false);
																					}
																					else
																					{
																						if (b6 == 4)
																						{
																							WorldGen.KillTile(num27, num28, flag4, false, true);
																						}
																					}
																				}
																			}
																		}
																		if (Main.netMode == 2)
																		{
																			NetMessage.SendData(17, -1, this.whoAmI, "", (int)b6, (float)num27, (float)num28, (float)b7, num29);
																			if (b6 == 1 && b7 == 53)
																			{
																				NetMessage.SendTileSquare(-1, num27, num28, 1);
																				return;
																			}
																		}
																	}
																	else
																	{
																		if (b == 18)
																		{
																			if (Main.netMode == 1)
																			{
																				byte b8 = this.readBuffer[num];
																				num++;
																				int num30 = BitConverter.ToInt32(this.readBuffer, num);
																				num += 4;
																				short sunModY = BitConverter.ToInt16(this.readBuffer, num);
																				num += 2;
																				short moonModY = BitConverter.ToInt16(this.readBuffer, num);
																				num += 2;
																				if (b8 == 1)
																				{
																					Main.dayTime = true;
																				}
																				else
																				{
																					Main.dayTime = false;
																				}
																				Main.time = (double)num30;
																				Main.sunModY = sunModY;
																				Main.moonModY = moonModY;
																				if (Main.netMode == 2)
																				{
																					NetMessage.SendData(18, -1, this.whoAmI, "", 0, 0f, 0f, 0f, 0);
																					return;
																				}
																			}
																		}
																		else
																		{
																			if (b == 19)
																			{
																				byte b9 = this.readBuffer[num];
																				num++;
																				int num31 = BitConverter.ToInt32(this.readBuffer, num);
																				num += 4;
																				int num32 = BitConverter.ToInt32(this.readBuffer, num);
																				num += 4;
																				int num33 = (int)this.readBuffer[num];
																				int direction = 0;
																				if (num33 == 0)
																				{
																					direction = -1;
																				}
																				if (b9 == 0)
																				{
																					WorldGen.OpenDoor(num31, num32, direction);
																				}
																				else
																				{
																					if (b9 == 1)
																					{
																						WorldGen.CloseDoor(num31, num32, true);
																					}
																				}
																				if (Main.netMode == 2)
																				{
																					NetMessage.SendData(19, -1, this.whoAmI, "", (int)b9, (float)num31, (float)num32, (float)num33, 0);
																					return;
																				}
																			}
																			else
																			{
																				if (b == 20)
																				{
																					short num34 = BitConverter.ToInt16(this.readBuffer, start + 1);
																					int num35 = BitConverter.ToInt32(this.readBuffer, start + 3);
																					int num36 = BitConverter.ToInt32(this.readBuffer, start + 7);
																					num = start + 11;
																					for (int num37 = num35; num37 < num35 + (int)num34; num37++)
																					{
																						for (int num38 = num36; num38 < num36 + (int)num34; num38++)
																						{
																							if (Main.tile[num37, num38] == null)
																							{
																								Main.tile[num37, num38] = new Tile();
																							}
																							byte b10 = this.readBuffer[num];
																							num++;
																							bool active2 = Main.tile[num37, num38].active;
																							if ((b10 & 1) == 1)
																							{
																								Main.tile[num37, num38].active = true;
																							}
																							else
																							{
																								Main.tile[num37, num38].active = false;
																							}
																							if ((b10 & 2) == 2)
																							{
																								Main.tile[num37, num38].lighted = true;
																							}
																							if ((b10 & 4) == 4)
																							{
																								Main.tile[num37, num38].wall = 1;
																							}
																							else
																							{
																								Main.tile[num37, num38].wall = 0;
																							}
																							if ((b10 & 8) == 8)
																							{
																								Main.tile[num37, num38].liquid = 1;
																							}
																							else
																							{
																								Main.tile[num37, num38].liquid = 0;
																							}
																							if (Main.tile[num37, num38].active)
																							{
																								int type2 = (int)Main.tile[num37, num38].type;
																								Main.tile[num37, num38].type = this.readBuffer[num];
																								num++;
																								if (Main.tileFrameImportant[(int)Main.tile[num37, num38].type])
																								{
																									Main.tile[num37, num38].frameX = BitConverter.ToInt16(this.readBuffer, num);
																									num += 2;
																									Main.tile[num37, num38].frameY = BitConverter.ToInt16(this.readBuffer, num);
																									num += 2;
																								}
																								else
																								{
																									if (!active2 || (int)Main.tile[num37, num38].type != type2)
																									{
																										Main.tile[num37, num38].frameX = -1;
																										Main.tile[num37, num38].frameY = -1;
																									}
																								}
																							}
																							if (Main.tile[num37, num38].wall > 0)
																							{
																								Main.tile[num37, num38].wall = this.readBuffer[num];
																								num++;
																							}
																							if (Main.tile[num37, num38].liquid > 0)
																							{
																								Main.tile[num37, num38].liquid = this.readBuffer[num];
																								num++;
																								byte b11 = this.readBuffer[num];
																								num++;
																								if (b11 == 1)
																								{
																									Main.tile[num37, num38].lava = true;
																								}
																								else
																								{
																									Main.tile[num37, num38].lava = false;
																								}
																							}
																						}
																					}
																					WorldGen.RangeFrame(num35, num36, num35 + (int)num34, num36 + (int)num34);
																					if (Main.netMode == 2)
																					{
																						NetMessage.SendData((int)b, -1, this.whoAmI, "", (int)num34, (float)num35, (float)num36, 0f, 0);
																						return;
																					}
																				}
																				else
																				{
																					if (b == 21)
																					{
																						short num39 = BitConverter.ToInt16(this.readBuffer, num);
																						num += 2;
																						float num40 = BitConverter.ToSingle(this.readBuffer, num);
																						num += 4;
																						float num41 = BitConverter.ToSingle(this.readBuffer, num);
																						num += 4;
																						float x3 = BitConverter.ToSingle(this.readBuffer, num);
																						num += 4;
																						float y2 = BitConverter.ToSingle(this.readBuffer, num);
																						num += 4;
																						byte stack2 = this.readBuffer[num];
																						num++;
																						string string4 = Encoding.ASCII.GetString(this.readBuffer, num, length - num + start);
																						if (Main.netMode == 1)
																						{
																							if (string4 == "0")
																							{
																								Main.item[(int)num39].active = false;
																								return;
																							}
																							Main.item[(int)num39].SetDefaults(string4);
																							Main.item[(int)num39].stack = (int)stack2;
																							Main.item[(int)num39].position.X = num40;
																							Main.item[(int)num39].position.Y = num41;
																							Main.item[(int)num39].velocity.X = x3;
																							Main.item[(int)num39].velocity.Y = y2;
																							Main.item[(int)num39].active = true;
																							Main.item[(int)num39].wet = Collision.WetCollision(Main.item[(int)num39].position, Main.item[(int)num39].width, Main.item[(int)num39].height);
																							return;
																						}
																						else
																						{
																							if (string4 == "0")
																							{
																								if (num39 < 200)
																								{
																									Main.item[(int)num39].active = false;
																									NetMessage.SendData(21, -1, -1, "", (int)num39, 0f, 0f, 0f, 0);
																									return;
																								}
																							}
																							else
																							{
																								bool flag5 = false;
																								if (num39 == 200)
																								{
																									flag5 = true;
																								}
																								if (flag5)
																								{
																									Item item = new Item();
																									item.SetDefaults(string4);
																									num39 = (short)Item.NewItem((int)num40, (int)num41, item.width, item.height, item.type, (int)stack2, true);
																								}
																								Main.item[(int)num39].SetDefaults(string4);
																								Main.item[(int)num39].stack = (int)stack2;
																								Main.item[(int)num39].position.X = num40;
																								Main.item[(int)num39].position.Y = num41;
																								Main.item[(int)num39].velocity.X = x3;
																								Main.item[(int)num39].velocity.Y = y2;
																								Main.item[(int)num39].active = true;
																								Main.item[(int)num39].owner = Main.myPlayer;
																								if (flag5)
																								{
																									NetMessage.SendData(21, -1, -1, "", (int)num39, 0f, 0f, 0f, 0);
																									Main.item[(int)num39].ownIgnore = this.whoAmI;
																									Main.item[(int)num39].ownTime = 100;
																									Main.item[(int)num39].FindOwner((int)num39);
																									return;
																								}
																								NetMessage.SendData(21, -1, this.whoAmI, "", (int)num39, 0f, 0f, 0f, 0);
																								return;
																							}
																						}
																					}
																					else
																					{
																						if (b == 22)
																						{
																							short num42 = BitConverter.ToInt16(this.readBuffer, num);
																							num += 2;
																							byte b12 = this.readBuffer[num];
																							if (Main.netMode == 2 && Main.item[(int)num42].owner != this.whoAmI)
																							{
																								return;
																							}
																							Main.item[(int)num42].owner = (int)b12;
																							if ((int)b12 == Main.myPlayer)
																							{
																								Main.item[(int)num42].keepTime = 15;
																							}
																							else
																							{
																								Main.item[(int)num42].keepTime = 0;
																							}
																							if (Main.netMode == 2)
																							{
																								Main.item[(int)num42].owner = 255;
																								Main.item[(int)num42].keepTime = 15;
																								NetMessage.SendData(22, -1, -1, "", (int)num42, 0f, 0f, 0f, 0);
																								return;
																							}
																						}
																						else
																						{
																							if (b == 23 && Main.netMode == 1)
																							{
																								short num43 = BitConverter.ToInt16(this.readBuffer, num);
																								num += 2;
																								float x4 = BitConverter.ToSingle(this.readBuffer, num);
																								num += 4;
																								float y3 = BitConverter.ToSingle(this.readBuffer, num);
																								num += 4;
																								float x5 = BitConverter.ToSingle(this.readBuffer, num);
																								num += 4;
																								float y4 = BitConverter.ToSingle(this.readBuffer, num);
																								num += 4;
																								int target = (int)BitConverter.ToInt16(this.readBuffer, num);
																								num += 2;
																								int direction2 = (int)(this.readBuffer[num] - 1);
																								num++;
																								int directionY = (int)(this.readBuffer[num] - 1);
																								num++;
																								int num44 = (int)BitConverter.ToInt16(this.readBuffer, num);
																								num += 2;
																								float[] array = new float[NPC.maxAI];
																								for (int num45 = 0; num45 < NPC.maxAI; num45++)
																								{
																									array[num45] = BitConverter.ToSingle(this.readBuffer, num);
																									num += 4;
																								}
																								string string5 = Encoding.ASCII.GetString(this.readBuffer, num, length - num + start);
																								if (!Main.npc[(int)num43].active || Main.npc[(int)num43].name != string5)
																								{
																									Main.npc[(int)num43].active = true;
																									Main.npc[(int)num43].SetDefaults(string5);
																								}
																								Main.npc[(int)num43].position.X = x4;
																								Main.npc[(int)num43].position.Y = y3;
																								Main.npc[(int)num43].velocity.X = x5;
																								Main.npc[(int)num43].velocity.Y = y4;
																								Main.npc[(int)num43].target = target;
																								Main.npc[(int)num43].direction = direction2;
																								Main.npc[(int)num43].directionY = directionY;
																								Main.npc[(int)num43].life = num44;
																								if (num44 <= 0)
																								{
																									Main.npc[(int)num43].active = false;
																								}
																								for (int num46 = 0; num46 < NPC.maxAI; num46++)
																								{
																									Main.npc[(int)num43].ai[num46] = array[num46];
																								}
																								return;
																							}
																							if (b == 24)
																							{
																								short num47 = BitConverter.ToInt16(this.readBuffer, num);
																								num += 2;
																								byte b13 = this.readBuffer[num];
																								if (Main.netMode == 2)
																								{
																									b13 = (byte)this.whoAmI;
																								}
																								Main.npc[(int)num47].StrikeNPC(Main.player[(int)b13].inventory[Main.player[(int)b13].selectedItem].damage, Main.player[(int)b13].inventory[Main.player[(int)b13].selectedItem].knockBack, Main.player[(int)b13].direction, false);
																								if (Main.netMode == 2)
																								{
																									NetMessage.SendData(24, -1, this.whoAmI, "", (int)num47, (float)b13, 0f, 0f, 0);
																									NetMessage.SendData(23, -1, -1, "", (int)num47, 0f, 0f, 0f, 0);
																									return;
																								}
																							}
																							else
																							{
																								if (b == 25)
																								{
																									int num48 = (int)this.readBuffer[start + 1];
																									if (Main.netMode == 2)
																									{
																										num48 = this.whoAmI;
																									}
																									byte b14 = this.readBuffer[start + 2];
																									byte b15 = this.readBuffer[start + 3];
																									byte b16 = this.readBuffer[start + 4];
																									if (Main.netMode == 2)
																									{
																										b14 = 255;
																										b15 = 255;
																										b16 = 255;
																									}
																									string string6 = Encoding.ASCII.GetString(this.readBuffer, start + 5, length - 5);
																									if (Main.netMode == 1)
																									{
																										string newText = string6;
																										if (num48 < 255)
																										{
																											newText = "<" + Main.player[num48].name + "> " + string6;
																											Main.player[num48].chatText = string6;
																											Main.player[num48].chatShowTime = Main.chatLength / 2;
																										}
																										Main.NewText(newText, b14, b15, b16);
																										return;
																									}
																									if (Main.netMode == 2)
																									{
																										string text2 = string6.ToLower();
																										if (text2 == "/playing")
																										{
																											string text3 = "";
																											for (int num49 = 0; num49 < 255; num49++)
																											{
																												if (Main.player[num49].active)
																												{
																													if (text3 == "")
																													{
																														text3 += Main.player[num49].name;
																													}
																													else
																													{
																														text3 = text3 + ", " + Main.player[num49].name;
																													}
																												}
																											}
																											NetMessage.SendData(25, this.whoAmI, -1, "Current players: " + text3 + ".", 255, 255f, 240f, 20f, 0);
																											return;
																										}
																										if (text2.Length >= 4 && text2.Substring(0, 4) == "/me ")
																										{
																											NetMessage.SendData(25, -1, -1, "*" + Main.player[this.whoAmI].name + " " + string6.Substring(4), 255, 200f, 100f, 0f, 0);
																											return;
																										}
																										if (text2.Length >= 3 && text2.Substring(0, 3) == "/p ")
																										{
																											if (Main.player[this.whoAmI].team != 0)
																											{
																												for (int num50 = 0; num50 < 255; num50++)
																												{
																													if (Main.player[num50].team == Main.player[this.whoAmI].team)
																													{
																														NetMessage.SendData(25, num50, -1, string6.Substring(3), num48, (float)Main.teamColor[Main.player[this.whoAmI].team].R, (float)Main.teamColor[Main.player[this.whoAmI].team].G, (float)Main.teamColor[Main.player[this.whoAmI].team].B, 0);
																													}
																												}
																												return;
																											}
																											NetMessage.SendData(25, this.whoAmI, -1, "You are not in a party!", 255, 255f, 240f, 20f, 0);
																											return;
																										}
																										else
																										{
																											if (Main.player[this.whoAmI].difficulty == 2)
																											{
																												b14 = Main.hcColor.R;
																												b15 = Main.hcColor.G;
																												b16 = Main.hcColor.B;
																											}
																											else
																											{
																												if (Main.player[this.whoAmI].difficulty == 1)
																												{
																													b14 = Main.mcColor.R;
																													b15 = Main.mcColor.G;
																													b16 = Main.mcColor.B;
																												}
																											}
																											NetMessage.SendData(25, -1, -1, string6, num48, (float)b14, (float)b15, (float)b16, 0);
																											if (Main.dedServ)
																											{
																												Console.WriteLine("<" + Main.player[this.whoAmI].name + "> " + string6);
																												return;
																											}
																										}
																									}
																								}
																								else
																								{
																									if (b == 26)
																									{
																										byte b17 = this.readBuffer[num];
																										if (Main.netMode == 2 && this.whoAmI != (int)b17 && (!Main.player[(int)b17].hostile || !Main.player[this.whoAmI].hostile))
																										{
																											return;
																										}
																										num++;
																										int num51 = (int)(this.readBuffer[num] - 1);
																										num++;
																										short num52 = BitConverter.ToInt16(this.readBuffer, num);
																										num += 2;
																										byte b18 = this.readBuffer[num];
																										num++;
																										bool pvp = false;
																										byte b19 = this.readBuffer[num];
																										num++;
																										bool crit = false;
																										string string7 = Encoding.ASCII.GetString(this.readBuffer, num, length - num + start);
																										if (b18 != 0)
																										{
																											pvp = true;
																										}
																										if (b19 != 0)
																										{
																											crit = true;
																										}
																										Main.player[(int)b17].Hurt((int)num52, num51, pvp, true, string7, crit);
																										if (Main.netMode == 2)
																										{
																											NetMessage.SendData(26, -1, this.whoAmI, string7, (int)b17, (float)num51, (float)num52, (float)b18, 0);
																											return;
																										}
																									}
																									else
																									{
																										if (b == 27)
																										{
																											short num53 = BitConverter.ToInt16(this.readBuffer, num);
																											num += 2;
																											float x6 = BitConverter.ToSingle(this.readBuffer, num);
																											num += 4;
																											float y5 = BitConverter.ToSingle(this.readBuffer, num);
																											num += 4;
																											float x7 = BitConverter.ToSingle(this.readBuffer, num);
																											num += 4;
																											float y6 = BitConverter.ToSingle(this.readBuffer, num);
																											num += 4;
																											float knockBack = BitConverter.ToSingle(this.readBuffer, num);
																											num += 4;
																											short damage = BitConverter.ToInt16(this.readBuffer, num);
																											num += 2;
																											byte b20 = this.readBuffer[num];
																											num++;
																											byte b21 = this.readBuffer[num];
																											num++;
																											float[] array2 = new float[Projectile.maxAI];
																											for (int num54 = 0; num54 < Projectile.maxAI; num54++)
																											{
																												array2[num54] = BitConverter.ToSingle(this.readBuffer, num);
																												num += 4;
																											}
																											int num55 = 1000;
																											for (int num56 = 0; num56 < 1000; num56++)
																											{
																												if (Main.projectile[num56].owner == (int)b20 && Main.projectile[num56].identity == (int)num53 && Main.projectile[num56].active)
																												{
																													num55 = num56;
																													break;
																												}
																											}
																											if (num55 == 1000)
																											{
																												for (int num57 = 0; num57 < 1000; num57++)
																												{
																													if (!Main.projectile[num57].active)
																													{
																														num55 = num57;
																														break;
																													}
																												}
																											}
																											if (!Main.projectile[num55].active || Main.projectile[num55].type != (int)b21)
																											{
																												Main.projectile[num55].SetDefaults((int)b21);
																												if (Main.netMode == 2)
																												{
																													Netplay.serverSock[this.whoAmI].spamProjectile += 1f;
																												}
																											}
																											Main.projectile[num55].identity = (int)num53;
																											Main.projectile[num55].position.X = x6;
																											Main.projectile[num55].position.Y = y5;
																											Main.projectile[num55].velocity.X = x7;
																											Main.projectile[num55].velocity.Y = y6;
																											Main.projectile[num55].damage = (int)damage;
																											Main.projectile[num55].type = (int)b21;
																											Main.projectile[num55].owner = (int)b20;
																											Main.projectile[num55].knockBack = knockBack;
																											for (int num58 = 0; num58 < Projectile.maxAI; num58++)
																											{
																												Main.projectile[num55].ai[num58] = array2[num58];
																											}
																											if (Main.netMode == 2)
																											{
																												NetMessage.SendData(27, -1, this.whoAmI, "", num55, 0f, 0f, 0f, 0);
																												return;
																											}
																										}
																										else
																										{
																											if (b == 28)
																											{
																												short num59 = BitConverter.ToInt16(this.readBuffer, num);
																												num += 2;
																												short num60 = BitConverter.ToInt16(this.readBuffer, num);
																												num += 2;
																												float num61 = BitConverter.ToSingle(this.readBuffer, num);
																												num += 4;
																												int num62 = (int)(this.readBuffer[num] - 1);
																												num++;
																												int num63 = (int)this.readBuffer[num];
																												if (num60 >= 0)
																												{
																													if (num63 == 1)
																													{
																														Main.npc[(int)num59].StrikeNPC((int)num60, num61, num62, true);
																													}
																													else
																													{
																														Main.npc[(int)num59].StrikeNPC((int)num60, num61, num62, false);
																													}
																												}
																												else
																												{
																													Main.npc[(int)num59].life = 0;
																													Main.npc[(int)num59].HitEffect(0, 10.0);
																													Main.npc[(int)num59].active = false;
																												}
																												if (Main.netMode == 2)
																												{
																													NetMessage.SendData(28, -1, this.whoAmI, "", (int)num59, (float)num60, num61, (float)num62, num63);
																													NetMessage.SendData(23, -1, -1, "", (int)num59, 0f, 0f, 0f, 0);
																													return;
																												}
																											}
																											else
																											{
																												if (b == 29)
																												{
																													short num64 = BitConverter.ToInt16(this.readBuffer, num);
																													num += 2;
																													byte b22 = this.readBuffer[num];
																													if (Main.netMode == 2)
																													{
																														b22 = (byte)this.whoAmI;
																													}
																													for (int num65 = 0; num65 < 1000; num65++)
																													{
																														if (Main.projectile[num65].owner == (int)b22 && Main.projectile[num65].identity == (int)num64 && Main.projectile[num65].active)
																														{
																															Main.projectile[num65].Kill();
																															break;
																														}
																													}
																													if (Main.netMode == 2)
																													{
																														NetMessage.SendData(29, -1, this.whoAmI, "", (int)num64, (float)b22, 0f, 0f, 0);
																														return;
																													}
																												}
																												else
																												{
																													if (b == 30)
																													{
																														byte b23 = this.readBuffer[num];
																														if (Main.netMode == 2)
																														{
																															b23 = (byte)this.whoAmI;
																														}
																														num++;
																														byte b24 = this.readBuffer[num];
																														if (b24 == 1)
																														{
																															Main.player[(int)b23].hostile = true;
																														}
																														else
																														{
																															Main.player[(int)b23].hostile = false;
																														}
																														if (Main.netMode == 2)
																														{
																															NetMessage.SendData(30, -1, this.whoAmI, "", (int)b23, 0f, 0f, 0f, 0);
																															string str = " has enabled PvP!";
																															if (b24 == 0)
																															{
																																str = " has disabled PvP!";
																															}
																															NetMessage.SendData(25, -1, -1, Main.player[(int)b23].name + str, 255, (float)Main.teamColor[Main.player[(int)b23].team].R, (float)Main.teamColor[Main.player[(int)b23].team].G, (float)Main.teamColor[Main.player[(int)b23].team].B, 0);
																															return;
																														}
																													}
																													else
																													{
																														if (b == 31)
																														{
																															if (Main.netMode == 2)
																															{
																																int x8 = BitConverter.ToInt32(this.readBuffer, num);
																																num += 4;
																																int y7 = BitConverter.ToInt32(this.readBuffer, num);
																																num += 4;
																																int num66 = Chest.FindChest(x8, y7);
																																if (num66 > -1 && Chest.UsingChest(num66) == -1)
																																{
																																	for (int num67 = 0; num67 < Chest.maxItems; num67++)
																																	{
																																		NetMessage.SendData(32, this.whoAmI, -1, "", num66, (float)num67, 0f, 0f, 0);
																																	}
																																	NetMessage.SendData(33, this.whoAmI, -1, "", num66, 0f, 0f, 0f, 0);
																																	Main.player[this.whoAmI].chest = num66;
																																	return;
																																}
																															}
																														}
																														else
																														{
																															if (b == 32)
																															{
																																int num68 = (int)BitConverter.ToInt16(this.readBuffer, num);
																																num += 2;
																																int num69 = (int)this.readBuffer[num];
																																num++;
																																int stack3 = (int)this.readBuffer[num];
																																num++;
																																string string8 = Encoding.ASCII.GetString(this.readBuffer, num, length - num + start);
																																if (Main.chest[num68] == null)
																																{
																																	Main.chest[num68] = new Chest();
																																}
																																if (Main.chest[num68].item[num69] == null)
																																{
																																	Main.chest[num68].item[num69] = new Item();
																																}
																																Main.chest[num68].item[num69].SetDefaults(string8);
																																Main.chest[num68].item[num69].stack = stack3;
																																return;
																															}
																															if (b == 33)
																															{
																																int num70 = (int)BitConverter.ToInt16(this.readBuffer, num);
																																num += 2;
																																int chestX = BitConverter.ToInt32(this.readBuffer, num);
																																num += 4;
																																int chestY = BitConverter.ToInt32(this.readBuffer, num);
																																if (Main.netMode == 1)
																																{
																																	if (Main.player[Main.myPlayer].chest == -1)
																																	{
																																		Main.playerInventory = true;
																																		Main.PlaySound(10, -1, -1, 1);
																																	}
																																	else
																																	{
																																		if (Main.player[Main.myPlayer].chest != num70 && num70 != -1)
																																		{
																																			Main.playerInventory = true;
																																			Main.PlaySound(12, -1, -1, 1);
																																		}
																																		else
																																		{
																																			if (Main.player[Main.myPlayer].chest != -1 && num70 == -1)
																																			{
																																				Main.PlaySound(11, -1, -1, 1);
																																			}
																																		}
																																	}
																																	Main.player[Main.myPlayer].chest = num70;
																																	Main.player[Main.myPlayer].chestX = chestX;
																																	Main.player[Main.myPlayer].chestY = chestY;
																																	return;
																																}
																																Main.player[this.whoAmI].chest = num70;
																																return;
																															}
																															else
																															{
																																if (b == 34)
																																{
																																	if (Main.netMode == 2)
																																	{
																																		int num71 = BitConverter.ToInt32(this.readBuffer, num);
																																		num += 4;
																																		int num72 = BitConverter.ToInt32(this.readBuffer, num);
																																		if (Main.tile[num71, num72].type == 21)
																																		{
																																			WorldGen.KillTile(num71, num72, false, false, false);
																																			if (!Main.tile[num71, num72].active)
																																			{
																																				NetMessage.SendData(17, -1, -1, "", 0, (float)num71, (float)num72, 0f, 0);
																																				return;
																																			}
																																		}
																																	}
																																}
																																else
																																{
																																	if (b == 35)
																																	{
																																		int num73 = (int)this.readBuffer[num];
																																		if (Main.netMode == 2)
																																		{
																																			num73 = this.whoAmI;
																																		}
																																		num++;
																																		int num74 = (int)BitConverter.ToInt16(this.readBuffer, num);
																																		num += 2;
																																		if (num73 != Main.myPlayer)
																																		{
																																			Main.player[num73].HealEffect(num74);
																																		}
																																		if (Main.netMode == 2)
																																		{
																																			NetMessage.SendData(35, -1, this.whoAmI, "", num73, (float)num74, 0f, 0f, 0);
																																			return;
																																		}
																																	}
																																	else
																																	{
																																		if (b == 36)
																																		{
																																			int num75 = (int)this.readBuffer[num];
																																			if (Main.netMode == 2)
																																			{
																																				num75 = this.whoAmI;
																																			}
																																			num++;
																																			int num76 = (int)this.readBuffer[num];
																																			num++;
																																			int num77 = (int)this.readBuffer[num];
																																			num++;
																																			int num78 = (int)this.readBuffer[num];
																																			num++;
																																			int num79 = (int)this.readBuffer[num];
																																			num++;
																																			if (num76 == 0)
																																			{
																																				Main.player[num75].zoneEvil = false;
																																			}
																																			else
																																			{
																																				Main.player[num75].zoneEvil = true;
																																			}
																																			if (num77 == 0)
																																			{
																																				Main.player[num75].zoneMeteor = false;
																																			}
																																			else
																																			{
																																				Main.player[num75].zoneMeteor = true;
																																			}
																																			if (num78 == 0)
																																			{
																																				Main.player[num75].zoneDungeon = false;
																																			}
																																			else
																																			{
																																				Main.player[num75].zoneDungeon = true;
																																			}
																																			if (num79 == 0)
																																			{
																																				Main.player[num75].zoneJungle = false;
																																			}
																																			else
																																			{
																																				Main.player[num75].zoneJungle = true;
																																			}
																																			if (Main.netMode == 2)
																																			{
																																				NetMessage.SendData(36, -1, this.whoAmI, "", num75, 0f, 0f, 0f, 0);
																																				return;
																																			}
																																		}
																																		else
																																		{
																																			if (b == 37)
																																			{
																																				if (Main.netMode == 1)
																																				{
																																					if (Main.autoPass)
																																					{
																																						NetMessage.SendData(38, -1, -1, Netplay.password, 0, 0f, 0f, 0f, 0);
																																						Main.autoPass = false;
																																						return;
																																					}
																																					Netplay.password = "";
																																					Main.menuMode = 31;
																																					return;
																																				}
																																			}
																																			else
																																			{
																																				if (b == 38)
																																				{
																																					if (Main.netMode == 2)
																																					{
																																						string string9 = Encoding.ASCII.GetString(this.readBuffer, num, length - num + start);
																																						if (string9 == Netplay.password)
																																						{
																																							Netplay.serverSock[this.whoAmI].state = 1;
																																							NetMessage.SendData(3, this.whoAmI, -1, "", 0, 0f, 0f, 0f, 0);
																																							return;
																																						}
																																						NetMessage.SendData(2, this.whoAmI, -1, "Incorrect password.", 0, 0f, 0f, 0f, 0);
																																						return;
																																					}
																																				}
																																				else
																																				{
																																					if (b == 39 && Main.netMode == 1)
																																					{
																																						short num80 = BitConverter.ToInt16(this.readBuffer, num);
																																						Main.item[(int)num80].owner = 255;
																																						NetMessage.SendData(22, -1, -1, "", (int)num80, 0f, 0f, 0f, 0);
																																						return;
																																					}
																																					if (b == 40)
																																					{
																																						byte b25 = this.readBuffer[num];
																																						if (Main.netMode == 2)
																																						{
																																							b25 = (byte)this.whoAmI;
																																						}
																																						num++;
																																						int talkNPC = (int)BitConverter.ToInt16(this.readBuffer, num);
																																						num += 2;
																																						Main.player[(int)b25].talkNPC = talkNPC;
																																						if (Main.netMode == 2)
																																						{
																																							NetMessage.SendData(40, -1, this.whoAmI, "", (int)b25, 0f, 0f, 0f, 0);
																																							return;
																																						}
																																					}
																																					else
																																					{
																																						if (b == 41)
																																						{
																																							byte b26 = this.readBuffer[num];
																																							if (Main.netMode == 2)
																																							{
																																								b26 = (byte)this.whoAmI;
																																							}
																																							num++;
																																							float itemRotation = BitConverter.ToSingle(this.readBuffer, num);
																																							num += 4;
																																							int itemAnimation = (int)BitConverter.ToInt16(this.readBuffer, num);
																																							Main.player[(int)b26].itemRotation = itemRotation;
																																							Main.player[(int)b26].itemAnimation = itemAnimation;
																																							Main.player[(int)b26].channel = Main.player[(int)b26].inventory[Main.player[(int)b26].selectedItem].channel;
																																							if (Main.netMode == 2)
																																							{
																																								NetMessage.SendData(41, -1, this.whoAmI, "", (int)b26, 0f, 0f, 0f, 0);
																																								return;
																																							}
																																						}
																																						else
																																						{
																																							if (b == 42)
																																							{
																																								int num81 = (int)this.readBuffer[num];
																																								if (Main.netMode == 2)
																																								{
																																									num81 = this.whoAmI;
																																								}
																																								num++;
																																								int statMana = (int)BitConverter.ToInt16(this.readBuffer, num);
																																								num += 2;
																																								int statManaMax = (int)BitConverter.ToInt16(this.readBuffer, num);
																																								if (Main.netMode == 2)
																																								{
																																									num81 = this.whoAmI;
																																								}
																																								Main.player[num81].statMana = statMana;
																																								Main.player[num81].statManaMax = statManaMax;
																																								if (Main.netMode == 2)
																																								{
																																									NetMessage.SendData(42, -1, this.whoAmI, "", num81, 0f, 0f, 0f, 0);
																																									return;
																																								}
																																							}
																																							else
																																							{
																																								if (b == 43)
																																								{
																																									int num82 = (int)this.readBuffer[num];
																																									if (Main.netMode == 2)
																																									{
																																										num82 = this.whoAmI;
																																									}
																																									num++;
																																									int num83 = (int)BitConverter.ToInt16(this.readBuffer, num);
																																									num += 2;
																																									if (num82 != Main.myPlayer)
																																									{
																																										Main.player[num82].ManaEffect(num83);
																																									}
																																									if (Main.netMode == 2)
																																									{
																																										NetMessage.SendData(43, -1, this.whoAmI, "", num82, (float)num83, 0f, 0f, 0);
																																										return;
																																									}
																																								}
																																								else
																																								{
																																									if (b == 44)
																																									{
																																										byte b27 = this.readBuffer[num];
																																										if ((int)b27 == Main.myPlayer)
																																										{
																																											return;
																																										}
																																										if (Main.netMode == 2)
																																										{
																																											b27 = (byte)this.whoAmI;
																																										}
																																										num++;
																																										int num84 = (int)(this.readBuffer[num] - 1);
																																										num++;
																																										short num85 = BitConverter.ToInt16(this.readBuffer, num);
																																										num += 2;
																																										byte b28 = this.readBuffer[num];
																																										num++;
																																										string string10 = Encoding.ASCII.GetString(this.readBuffer, num, length - num + start);
																																										bool pvp2 = false;
																																										if (b28 != 0)
																																										{
																																											pvp2 = true;
																																										}
																																										Main.player[(int)b27].KillMe((double)num85, num84, pvp2, string10);
																																										if (Main.netMode == 2)
																																										{
																																											NetMessage.SendData(44, -1, this.whoAmI, string10, (int)b27, (float)num84, (float)num85, (float)b28, 0);
																																											return;
																																										}
																																									}
																																									else
																																									{
																																										if (b == 45)
																																										{
																																											int num86 = (int)this.readBuffer[num];
																																											if (Main.netMode == 2)
																																											{
																																												num86 = this.whoAmI;
																																											}
																																											num++;
																																											int num87 = (int)this.readBuffer[num];
																																											num++;
																																											int team = Main.player[num86].team;
																																											Main.player[num86].team = num87;
																																											if (Main.netMode == 2)
																																											{
																																												NetMessage.SendData(45, -1, this.whoAmI, "", num86, 0f, 0f, 0f, 0);
																																												string str2 = "";
																																												if (num87 == 0)
																																												{
																																													str2 = " is no longer on a party.";
																																												}
																																												else
																																												{
																																													if (num87 == 1)
																																													{
																																														str2 = " has joined the red party.";
																																													}
																																													else
																																													{
																																														if (num87 == 2)
																																														{
																																															str2 = " has joined the green party.";
																																														}
																																														else
																																														{
																																															if (num87 == 3)
																																															{
																																																str2 = " has joined the blue party.";
																																															}
																																															else
																																															{
																																																if (num87 == 4)
																																																{
																																																	str2 = " has joined the yellow party.";
																																																}
																																															}
																																														}
																																													}
																																												}
																																												for (int num88 = 0; num88 < 255; num88++)
																																												{
																																													if (num88 == this.whoAmI || (team > 0 && Main.player[num88].team == team) || (num87 > 0 && Main.player[num88].team == num87))
																																													{
																																														NetMessage.SendData(25, num88, -1, Main.player[num86].name + str2, 255, (float)Main.teamColor[num87].R, (float)Main.teamColor[num87].G, (float)Main.teamColor[num87].B, 0);
																																													}
																																												}
																																												return;
																																											}
																																										}
																																										else
																																										{
																																											if (b == 46)
																																											{
																																												if (Main.netMode == 2)
																																												{
																																													int i2 = BitConverter.ToInt32(this.readBuffer, num);
																																													num += 4;
																																													int j2 = BitConverter.ToInt32(this.readBuffer, num);
																																													num += 4;
																																													int num89 = Sign.ReadSign(i2, j2);
																																													if (num89 >= 0)
																																													{
																																														NetMessage.SendData(47, this.whoAmI, -1, "", num89, 0f, 0f, 0f, 0);
																																														return;
																																													}
																																												}
																																											}
																																											else
																																											{
																																												if (b == 47)
																																												{
																																													int num90 = (int)BitConverter.ToInt16(this.readBuffer, num);
																																													num += 2;
																																													int x9 = BitConverter.ToInt32(this.readBuffer, num);
																																													num += 4;
																																													int y8 = BitConverter.ToInt32(this.readBuffer, num);
																																													num += 4;
																																													string string11 = Encoding.ASCII.GetString(this.readBuffer, num, length - num + start);
																																													Main.sign[num90] = new Sign();
																																													Main.sign[num90].x = x9;
																																													Main.sign[num90].y = y8;
																																													Sign.TextSign(num90, string11);
																																													if (Main.netMode == 1 && Main.sign[num90] != null && num90 != Main.player[Main.myPlayer].sign)
																																													{
																																														Main.playerInventory = false;
																																														Main.player[Main.myPlayer].talkNPC = -1;
																																														Main.editSign = false;
																																														Main.PlaySound(10, -1, -1, 1);
																																														Main.player[Main.myPlayer].sign = num90;
																																														Main.npcChatText = Main.sign[num90].text;
																																														return;
																																													}
																																												}
																																												else
																																												{
																																													if (b == 48)
																																													{
																																														int num91 = BitConverter.ToInt32(this.readBuffer, num);
																																														num += 4;
																																														int num92 = BitConverter.ToInt32(this.readBuffer, num);
																																														num += 4;
																																														byte liquid = this.readBuffer[num];
																																														num++;
																																														byte b29 = this.readBuffer[num];
																																														num++;
																																														if (Main.netMode == 2 && Netplay.spamCheck)
																																														{
																																															int num93 = this.whoAmI;
																																															int num94 = (int)(Main.player[num93].position.X + (float)(Main.player[num93].width / 2));
																																															int num95 = (int)(Main.player[num93].position.Y + (float)(Main.player[num93].height / 2));
																																															int num96 = 10;
																																															int num97 = num94 - num96;
																																															int num98 = num94 + num96;
																																															int num99 = num95 - num96;
																																															int num100 = num95 + num96;
																																															if (num94 < num97 || num94 > num98 || num95 < num99 || num95 > num100)
																																															{
																																																NetMessage.BootPlayer(this.whoAmI, "Cheating attempt detected: Liquid spam");
																																																return;
																																															}
																																														}
																																														if (Main.tile[num91, num92] == null)
																																														{
																																															Main.tile[num91, num92] = new Tile();
																																														}
																																														lock (Main.tile[num91, num92])
																																														{
																																															Main.tile[num91, num92].liquid = liquid;
																																															if (b29 == 1)
																																															{
																																																Main.tile[num91, num92].lava = true;
																																															}
																																															else
																																															{
																																																Main.tile[num91, num92].lava = false;
																																															}
																																															if (Main.netMode == 2)
																																															{
																																																WorldGen.SquareTileFrame(num91, num92, true);
																																															}
																																															return;
																																														}
																																													}
																																													if (b == 49)
																																													{
																																														if (Netplay.clientSock.state == 6)
																																														{
																																															Netplay.clientSock.state = 10;
																																															Main.player[Main.myPlayer].Spawn();
																																															return;
																																														}
																																													}
																																													else
																																													{
																																														if (b == 50)
																																														{
																																															int num101 = (int)this.readBuffer[num];
																																															num++;
																																															if (Main.netMode == 2)
																																															{
																																																num101 = this.whoAmI;
																																															}
																																															else
																																															{
																																																if (num101 == Main.myPlayer)
																																																{
																																																	return;
																																																}
																																															}
																																															for (int num102 = 0; num102 < 10; num102++)
																																															{
																																																Main.player[num101].buffType[num102] = (int)this.readBuffer[num];
																																																if (Main.player[num101].buffType[num102] > 0)
																																																{
																																																	Main.player[num101].buffTime[num102] = 60;
																																																}
																																																else
																																																{
																																																	Main.player[num101].buffTime[num102] = 0;
																																																}
																																																num++;
																																															}
																																															if (Main.netMode == 2)
																																															{
																																																NetMessage.SendData(50, -1, this.whoAmI, "", num101, 0f, 0f, 0f, 0);
																																																return;
																																															}
																																														}
																																														else
																																														{
																																															if (b == 51)
																																															{
																																																byte b30 = this.readBuffer[num];
																																																num++;
																																																byte b31 = this.readBuffer[num];
																																																if (b31 == 1)
																																																{
																																																	NPC.SpawnSkeletron();
																																																	return;
																																																}
																																																if (b31 == 2)
																																																{
																																																	if (Main.netMode != 2)
																																																	{
																																																		Main.PlaySound(2, (int)Main.player[(int)b30].position.X, (int)Main.player[(int)b30].position.Y, 1);
																																																		return;
																																																	}
																																																	if (Main.netMode == 2)
																																																	{
																																																		NetMessage.SendData(51, -1, this.whoAmI, "", (int)b30, (float)b31, 0f, 0f, 0);
																																																		return;
																																																	}
																																																}
																																															}
																																															else
																																															{
																																																if (b == 52)
																																																{
																																																	byte number = this.readBuffer[num];
																																																	num++;
																																																	byte b32 = this.readBuffer[num];
																																																	num++;
																																																	int num103 = BitConverter.ToInt32(this.readBuffer, num);
																																																	num += 4;
																																																	int num104 = BitConverter.ToInt32(this.readBuffer, num);
																																																	num += 4;
																																																	if (b32 == 1)
																																																	{
																																																		Chest.Unlock(num103, num104);
																																																		if (Main.netMode == 2)
																																																		{
																																																			NetMessage.SendData(52, -1, this.whoAmI, "", (int)number, (float)b32, (float)num103, (float)num104, 0);
																																																			NetMessage.SendTileSquare(-1, num103, num104, 2);
																																																			return;
																																																		}
																																																	}
																																																}
																																																else
																																																{
																																																	if (b == 53)
																																																	{
																																																		short num105 = BitConverter.ToInt16(this.readBuffer, num);
																																																		num += 2;
																																																		byte type3 = this.readBuffer[num];
																																																		num++;
																																																		short time = BitConverter.ToInt16(this.readBuffer, num);
																																																		num += 2;
																																																		Main.npc[(int)num105].AddBuff((int)type3, (int)time, true);
																																																		if (Main.netMode == 2)
																																																		{
																																																			NetMessage.SendData(54, -1, -1, "", (int)num105, 0f, 0f, 0f, 0);
																																																			return;
																																																		}
																																																	}
																																																	else
																																																	{
																																																		if (b == 54)
																																																		{
																																																			if (Main.netMode == 1)
																																																			{
																																																				short num106 = BitConverter.ToInt16(this.readBuffer, num);
																																																				num += 2;
																																																				for (int num107 = 0; num107 < 5; num107++)
																																																				{
																																																					Main.npc[(int)num106].buffType[num107] = (int)this.readBuffer[num];
																																																					num++;
																																																					Main.npc[(int)num106].buffTime[num107] = (int)BitConverter.ToInt16(this.readBuffer, num);
																																																					num += 2;
																																																				}
																																																				return;
																																																			}
																																																		}
																																																		else
																																																		{
																																																			if (b == 55)
																																																			{
																																																				byte b33 = this.readBuffer[num];
																																																				num++;
																																																				byte b34 = this.readBuffer[num];
																																																				num++;
																																																				short num108 = BitConverter.ToInt16(this.readBuffer, num);
																																																				num += 2;
																																																				if (Main.netMode == 1 && (int)b33 == Main.myPlayer)
																																																				{
																																																					Main.player[(int)b33].AddBuff((int)b34, (int)num108, true);
																																																					return;
																																																				}
																																																				if (Main.netMode == 2)
																																																				{
																																																					NetMessage.SendData(55, (int)b33, -1, "", (int)b33, (float)b34, (float)num108, 0f, 0);
																																																				}
																																																			}
																																																		}
																																																	}
																																																}
																																															}
																																														}
																																													}
																																												}
																																											}
																																										}
																																									}
																																								}
																																							}
																																						}
																																					}
																																				}
																																			}
																																		}
																																	}
																																}
																															}
																														}
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}
}
