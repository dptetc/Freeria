using System;
using System.Text;
namespace Freeria
{
	public class NetMessage
	{
		public static messageBuffer[] buffer = new messageBuffer[257];
		public static void SendData(int msgType, int remoteClient = -1, int ignoreClient = -1, string text = "", int number = 0, float number2 = 0f, float number3 = 0f, float number4 = 0f, int number5 = 0)
		{
			int num = 256;
			if (Main.netMode == 2 && remoteClient >= 0)
			{
				num = remoteClient;
			}
			lock (NetMessage.buffer[num])
			{
				int num2 = 5;
				int num3 = num2;
				if (msgType == 1)
				{
					byte[] bytes = BitConverter.GetBytes(msgType);
					byte[] bytes2 = Encoding.ASCII.GetBytes("Freeria" + Main.curRelease);
					num2 += bytes2.Length;
					byte[] bytes3 = BitConverter.GetBytes(num2 - 4);
					Buffer.BlockCopy(bytes3, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
					Buffer.BlockCopy(bytes, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
					Buffer.BlockCopy(bytes2, 0, NetMessage.buffer[num].writeBuffer, 5, bytes2.Length);
				}
				else
				{
					if (msgType == 2)
					{
						byte[] bytes4 = BitConverter.GetBytes(msgType);
						byte[] bytes5 = Encoding.ASCII.GetBytes(text);
						num2 += bytes5.Length;
						byte[] bytes6 = BitConverter.GetBytes(num2 - 4);
						Buffer.BlockCopy(bytes6, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
						Buffer.BlockCopy(bytes4, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
						Buffer.BlockCopy(bytes5, 0, NetMessage.buffer[num].writeBuffer, 5, bytes5.Length);
						if (Main.dedServ)
						{
							Console.WriteLine(Netplay.serverSock[num].tcpClient.Client.RemoteEndPoint.ToString() + " was booted: " + text);
						}
					}
					else
					{
						if (msgType == 3)
						{
							byte[] bytes7 = BitConverter.GetBytes(msgType);
							byte[] bytes8 = BitConverter.GetBytes(remoteClient);
							num2 += bytes8.Length;
							byte[] bytes9 = BitConverter.GetBytes(num2 - 4);
							Buffer.BlockCopy(bytes9, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
							Buffer.BlockCopy(bytes7, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
							Buffer.BlockCopy(bytes8, 0, NetMessage.buffer[num].writeBuffer, 5, bytes8.Length);
						}
						else
						{
							if (msgType == 4)
							{
								byte[] bytes10 = BitConverter.GetBytes(msgType);
								byte b = (byte)number;
								byte b2 = (byte)Main.player[(int)b].hair;
								byte b3 = 0;
								if (Main.player[(int)b].male)
								{
									b3 = 1;
								}
								byte[] bytes11 = Encoding.ASCII.GetBytes(text);
								num2 += 24 + bytes11.Length + 1;
								byte[] bytes12 = BitConverter.GetBytes(num2 - 4);
								Buffer.BlockCopy(bytes12, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
								Buffer.BlockCopy(bytes10, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
								NetMessage.buffer[num].writeBuffer[5] = b;
								num3++;
								NetMessage.buffer[num].writeBuffer[6] = b2;
								num3++;
								NetMessage.buffer[num].writeBuffer[7] = b3;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].hairColor.R;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].hairColor.G;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].hairColor.B;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].skinColor.R;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].skinColor.G;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].skinColor.B;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].eyeColor.R;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].eyeColor.G;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].eyeColor.B;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].shirtColor.R;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].shirtColor.G;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].shirtColor.B;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].underShirtColor.R;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].underShirtColor.G;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].underShirtColor.B;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].pantsColor.R;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].pantsColor.G;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].pantsColor.B;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].shoeColor.R;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].shoeColor.G;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].shoeColor.B;
								num3++;
								NetMessage.buffer[num].writeBuffer[num3] = Main.player[(int)b].difficulty;
								num3++;
								Buffer.BlockCopy(bytes11, 0, NetMessage.buffer[num].writeBuffer, num3, bytes11.Length);
							}
							else
							{
								if (msgType == 5)
								{
									byte[] bytes13 = BitConverter.GetBytes(msgType);
									byte b4 = (byte)number;
									byte b5 = (byte)number2;
									byte b6;
									if (number2 < 44f)
									{
										b6 = (byte)Main.player[number].inventory[(int)number2].stack;
										if (Main.player[number].inventory[(int)number2].stack < 0)
										{
											b6 = 0;
										}
									}
									else
									{
										b6 = (byte)Main.player[number].armor[(int)number2 - 44].stack;
										if (Main.player[number].armor[(int)number2 - 44].stack < 0)
										{
											b6 = 0;
										}
									}
									string text2 = text;
									if (text2 == null)
									{
										text2 = "";
									}
									byte[] bytes14 = Encoding.ASCII.GetBytes(text2);
									num2 += 3 + bytes14.Length;
									byte[] bytes15 = BitConverter.GetBytes(num2 - 4);
									Buffer.BlockCopy(bytes15, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
									Buffer.BlockCopy(bytes13, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
									NetMessage.buffer[num].writeBuffer[5] = b4;
									num3++;
									NetMessage.buffer[num].writeBuffer[6] = b5;
									num3++;
									NetMessage.buffer[num].writeBuffer[7] = b6;
									num3++;
									Buffer.BlockCopy(bytes14, 0, NetMessage.buffer[num].writeBuffer, num3, bytes14.Length);
								}
								else
								{
									if (msgType == 6)
									{
										byte[] bytes16 = BitConverter.GetBytes(msgType);
										byte[] bytes17 = BitConverter.GetBytes(num2 - 4);
										Buffer.BlockCopy(bytes17, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
										Buffer.BlockCopy(bytes16, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
									}
									else
									{
										if (msgType == 7)
										{
											byte[] bytes18 = BitConverter.GetBytes(msgType);
											byte[] bytes19 = BitConverter.GetBytes((int)Main.time);
											byte b7 = 0;
											if (Main.dayTime)
											{
												b7 = 1;
											}
											byte b8 = (byte)Main.moonPhase;
											byte b9 = 0;
											if (Main.bloodMoon)
											{
												b9 = 1;
											}
											byte[] bytes20 = BitConverter.GetBytes(Main.maxTilesX);
											byte[] bytes21 = BitConverter.GetBytes(Main.maxTilesY);
											byte[] bytes22 = BitConverter.GetBytes(Main.spawnTileX);
											byte[] bytes23 = BitConverter.GetBytes(Main.spawnTileY);
											byte[] bytes24 = BitConverter.GetBytes((int)Main.worldSurface);
											byte[] bytes25 = BitConverter.GetBytes((int)Main.rockLayer);
											byte[] bytes26 = BitConverter.GetBytes(Main.worldID);
											byte[] bytes27 = Encoding.ASCII.GetBytes(Main.worldName);
											byte b10 = 0;
											if (WorldGen.shadowOrbSmashed)
											{
												b10 += 1;
											}
											if (NPC.downedBoss1)
											{
												b10 += 2;
											}
											if (NPC.downedBoss2)
											{
												b10 += 4;
											}
											if (NPC.downedBoss3)
											{
												b10 += 8;
											}
											num2 += bytes19.Length + 1 + 1 + 1 + bytes20.Length + bytes21.Length + bytes22.Length + bytes23.Length + bytes24.Length + bytes25.Length + bytes26.Length + 1 + bytes27.Length;
											byte[] bytes28 = BitConverter.GetBytes(num2 - 4);
											Buffer.BlockCopy(bytes28, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
											Buffer.BlockCopy(bytes18, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
											Buffer.BlockCopy(bytes19, 0, NetMessage.buffer[num].writeBuffer, 5, bytes19.Length);
											num3 += bytes19.Length;
											NetMessage.buffer[num].writeBuffer[num3] = b7;
											num3++;
											NetMessage.buffer[num].writeBuffer[num3] = b8;
											num3++;
											NetMessage.buffer[num].writeBuffer[num3] = b9;
											num3++;
											Buffer.BlockCopy(bytes20, 0, NetMessage.buffer[num].writeBuffer, num3, bytes20.Length);
											num3 += bytes20.Length;
											Buffer.BlockCopy(bytes21, 0, NetMessage.buffer[num].writeBuffer, num3, bytes21.Length);
											num3 += bytes21.Length;
											Buffer.BlockCopy(bytes22, 0, NetMessage.buffer[num].writeBuffer, num3, bytes22.Length);
											num3 += bytes22.Length;
											Buffer.BlockCopy(bytes23, 0, NetMessage.buffer[num].writeBuffer, num3, bytes23.Length);
											num3 += bytes23.Length;
											Buffer.BlockCopy(bytes24, 0, NetMessage.buffer[num].writeBuffer, num3, bytes24.Length);
											num3 += bytes24.Length;
											Buffer.BlockCopy(bytes25, 0, NetMessage.buffer[num].writeBuffer, num3, bytes25.Length);
											num3 += bytes25.Length;
											Buffer.BlockCopy(bytes26, 0, NetMessage.buffer[num].writeBuffer, num3, bytes26.Length);
											num3 += bytes26.Length;
											NetMessage.buffer[num].writeBuffer[num3] = b10;
											num3++;
											Buffer.BlockCopy(bytes27, 0, NetMessage.buffer[num].writeBuffer, num3, bytes27.Length);
											num3 += bytes27.Length;
										}
										else
										{
											if (msgType == 8)
											{
												byte[] bytes29 = BitConverter.GetBytes(msgType);
												byte[] bytes30 = BitConverter.GetBytes(number);
												byte[] bytes31 = BitConverter.GetBytes((int)number2);
												num2 += bytes30.Length + bytes31.Length;
												byte[] bytes32 = BitConverter.GetBytes(num2 - 4);
												Buffer.BlockCopy(bytes32, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
												Buffer.BlockCopy(bytes29, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
												Buffer.BlockCopy(bytes30, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
												num3 += 4;
												Buffer.BlockCopy(bytes31, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
											}
											else
											{
												if (msgType == 9)
												{
													byte[] bytes33 = BitConverter.GetBytes(msgType);
													byte[] bytes34 = BitConverter.GetBytes(number);
													byte[] bytes35 = Encoding.ASCII.GetBytes(text);
													num2 += bytes34.Length + bytes35.Length;
													byte[] bytes36 = BitConverter.GetBytes(num2 - 4);
													Buffer.BlockCopy(bytes36, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
													Buffer.BlockCopy(bytes33, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
													Buffer.BlockCopy(bytes34, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
													num3 += 4;
													Buffer.BlockCopy(bytes35, 0, NetMessage.buffer[num].writeBuffer, num3, bytes35.Length);
												}
												else
												{
													if (msgType == 10)
													{
														short num4 = (short)number;
														int num5 = (int)number2;
														int num6 = (int)number3;
														byte[] bytes37 = BitConverter.GetBytes(msgType);
														Buffer.BlockCopy(bytes37, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
														byte[] bytes38 = BitConverter.GetBytes(num4);
														Buffer.BlockCopy(bytes38, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
														num3 += 2;
														byte[] bytes39 = BitConverter.GetBytes(num5);
														Buffer.BlockCopy(bytes39, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
														num3 += 4;
														byte[] bytes40 = BitConverter.GetBytes(num6);
														Buffer.BlockCopy(bytes40, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
														num3 += 4;
														for (int i = num5; i < num5 + (int)num4; i++)
														{
															byte b11 = 0;
															if (Main.tile[i, num6].active)
															{
																b11 += 1;
															}
															if (Main.tile[i, num6].lighted)
															{
																b11 += 2;
															}
															if (Main.tile[i, num6].wall > 0)
															{
																b11 += 4;
															}
															if (Main.tile[i, num6].liquid > 0)
															{
																b11 += 8;
															}
															NetMessage.buffer[num].writeBuffer[num3] = b11;
															num3++;
															byte[] bytes41 = BitConverter.GetBytes(Main.tile[i, num6].frameX);
															byte[] bytes42 = BitConverter.GetBytes(Main.tile[i, num6].frameY);
															byte wall = Main.tile[i, num6].wall;
															if (Main.tile[i, num6].active)
															{
																NetMessage.buffer[num].writeBuffer[num3] = Main.tile[i, num6].type;
																num3++;
																if (Main.tileFrameImportant[(int)Main.tile[i, num6].type])
																{
																	Buffer.BlockCopy(bytes41, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
																	num3 += 2;
																	Buffer.BlockCopy(bytes42, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
																	num3 += 2;
																}
															}
															if (wall > 0)
															{
																NetMessage.buffer[num].writeBuffer[num3] = wall;
																num3++;
															}
															if (Main.tile[i, num6].liquid > 0)
															{
																NetMessage.buffer[num].writeBuffer[num3] = Main.tile[i, num6].liquid;
																num3++;
																byte b12 = 0;
																if (Main.tile[i, num6].lava)
																{
																	b12 = 1;
																}
																NetMessage.buffer[num].writeBuffer[num3] = b12;
																num3++;
															}
														}
														byte[] bytes43 = BitConverter.GetBytes(num3 - 4);
														Buffer.BlockCopy(bytes43, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
														num2 = num3;
													}
													else
													{
														if (msgType == 11)
														{
															byte[] bytes44 = BitConverter.GetBytes(msgType);
															byte[] bytes45 = BitConverter.GetBytes(number);
															byte[] bytes46 = BitConverter.GetBytes((int)number2);
															byte[] bytes47 = BitConverter.GetBytes((int)number3);
															byte[] bytes48 = BitConverter.GetBytes((int)number4);
															num2 += bytes45.Length + bytes46.Length + bytes47.Length + bytes48.Length;
															byte[] bytes49 = BitConverter.GetBytes(num2 - 4);
															Buffer.BlockCopy(bytes49, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
															Buffer.BlockCopy(bytes44, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
															Buffer.BlockCopy(bytes45, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
															num3 += 4;
															Buffer.BlockCopy(bytes46, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
															num3 += 4;
															Buffer.BlockCopy(bytes47, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
															num3 += 4;
															Buffer.BlockCopy(bytes48, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
															num3 += 4;
														}
														else
														{
															if (msgType == 12)
															{
																byte[] bytes50 = BitConverter.GetBytes(msgType);
																byte b13 = (byte)number;
																byte[] bytes51 = BitConverter.GetBytes(Main.player[(int)b13].SpawnX);
																byte[] bytes52 = BitConverter.GetBytes(Main.player[(int)b13].SpawnY);
																num2 += 1 + bytes51.Length + bytes52.Length;
																byte[] bytes53 = BitConverter.GetBytes(num2 - 4);
																Buffer.BlockCopy(bytes53, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																Buffer.BlockCopy(bytes50, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																NetMessage.buffer[num].writeBuffer[num3] = b13;
																num3++;
																Buffer.BlockCopy(bytes51, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
																num3 += 4;
																Buffer.BlockCopy(bytes52, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
																num3 += 4;
															}
															else
															{
																if (msgType == 13)
																{
																	byte[] bytes54 = BitConverter.GetBytes(msgType);
																	byte b14 = (byte)number;
																	byte b15 = 0;
																	if (Main.player[(int)b14].controlUp)
																	{
																		b15 += 1;
																	}
																	if (Main.player[(int)b14].controlDown)
																	{
																		b15 += 2;
																	}
																	if (Main.player[(int)b14].controlLeft)
																	{
																		b15 += 4;
																	}
																	if (Main.player[(int)b14].controlRight)
																	{
																		b15 += 8;
																	}
																	if (Main.player[(int)b14].controlJump)
																	{
																		b15 += 16;
																	}
																	if (Main.player[(int)b14].controlUseItem)
																	{
																		b15 += 32;
																	}
																	if (Main.player[(int)b14].direction == 1)
																	{
																		b15 += 64;
																	}
																	byte b16 = (byte)Main.player[(int)b14].selectedItem;
																	byte[] bytes55 = BitConverter.GetBytes(Main.player[number].position.X);
																	byte[] bytes56 = BitConverter.GetBytes(Main.player[number].position.Y);
																	byte[] bytes57 = BitConverter.GetBytes(Main.player[number].velocity.X);
																	byte[] bytes58 = BitConverter.GetBytes(Main.player[number].velocity.Y);
																	num2 += 3 + bytes55.Length + bytes56.Length + bytes57.Length + bytes58.Length;
																	byte[] bytes59 = BitConverter.GetBytes(num2 - 4);
																	Buffer.BlockCopy(bytes59, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																	Buffer.BlockCopy(bytes54, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																	NetMessage.buffer[num].writeBuffer[5] = b14;
																	num3++;
																	NetMessage.buffer[num].writeBuffer[6] = b15;
																	num3++;
																	NetMessage.buffer[num].writeBuffer[7] = b16;
																	num3++;
																	Buffer.BlockCopy(bytes55, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
																	num3 += 4;
																	Buffer.BlockCopy(bytes56, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
																	num3 += 4;
																	Buffer.BlockCopy(bytes57, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
																	num3 += 4;
																	Buffer.BlockCopy(bytes58, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
																}
																else
																{
																	if (msgType == 14)
																	{
																		byte[] bytes60 = BitConverter.GetBytes(msgType);
																		byte b17 = (byte)number;
																		byte b18 = (byte)number2;
																		num2 += 2;
																		byte[] bytes61 = BitConverter.GetBytes(num2 - 4);
																		Buffer.BlockCopy(bytes61, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																		Buffer.BlockCopy(bytes60, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																		NetMessage.buffer[num].writeBuffer[5] = b17;
																		NetMessage.buffer[num].writeBuffer[6] = b18;
																	}
																	else
																	{
																		if (msgType == 15)
																		{
																			byte[] bytes62 = BitConverter.GetBytes(msgType);
																			byte[] bytes63 = BitConverter.GetBytes(num2 - 4);
																			Buffer.BlockCopy(bytes63, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																			Buffer.BlockCopy(bytes62, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																		}
																		else
																		{
																			if (msgType == 16)
																			{
																				byte[] bytes64 = BitConverter.GetBytes(msgType);
																				byte b19 = (byte)number;
																				byte[] bytes65 = BitConverter.GetBytes((short)Main.player[(int)b19].statLife);
																				byte[] bytes66 = BitConverter.GetBytes((short)Main.player[(int)b19].statLifeMax);
																				num2 += 1 + bytes65.Length + bytes66.Length;
																				byte[] bytes67 = BitConverter.GetBytes(num2 - 4);
																				Buffer.BlockCopy(bytes67, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																				Buffer.BlockCopy(bytes64, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																				NetMessage.buffer[num].writeBuffer[5] = b19;
																				num3++;
																				Buffer.BlockCopy(bytes65, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
																				num3 += 2;
																				Buffer.BlockCopy(bytes66, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
																			}
																			else
																			{
																				if (msgType == 17)
																				{
																					byte[] bytes68 = BitConverter.GetBytes(msgType);
																					byte b20 = (byte)number;
																					byte[] bytes69 = BitConverter.GetBytes((int)number2);
																					byte[] bytes70 = BitConverter.GetBytes((int)number3);
																					byte b21 = (byte)number4;
																					num2 += 1 + bytes69.Length + bytes70.Length + 1 + 1;
																					byte[] bytes71 = BitConverter.GetBytes(num2 - 4);
																					Buffer.BlockCopy(bytes71, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																					Buffer.BlockCopy(bytes68, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																					NetMessage.buffer[num].writeBuffer[num3] = b20;
																					num3++;
																					Buffer.BlockCopy(bytes69, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
																					num3 += 4;
																					Buffer.BlockCopy(bytes70, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
																					num3 += 4;
																					NetMessage.buffer[num].writeBuffer[num3] = b21;
																					num3++;
																					NetMessage.buffer[num].writeBuffer[num3] = (byte)number5;
																				}
																				else
																				{
																					if (msgType == 18)
																					{
																						byte[] bytes72 = BitConverter.GetBytes(msgType);
																						BitConverter.GetBytes((int)Main.time);
																						byte b22 = 0;
																						if (Main.dayTime)
																						{
																							b22 = 1;
																						}
																						byte[] bytes73 = BitConverter.GetBytes((int)Main.time);
																						byte[] bytes74 = BitConverter.GetBytes(Main.sunModY);
																						byte[] bytes75 = BitConverter.GetBytes(Main.moonModY);
																						num2 += 1 + bytes73.Length + bytes74.Length + bytes75.Length;
																						byte[] bytes76 = BitConverter.GetBytes(num2 - 4);
																						Buffer.BlockCopy(bytes76, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																						Buffer.BlockCopy(bytes72, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																						NetMessage.buffer[num].writeBuffer[num3] = b22;
																						num3++;
																						Buffer.BlockCopy(bytes73, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
																						num3 += 4;
																						Buffer.BlockCopy(bytes74, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
																						num3 += 2;
																						Buffer.BlockCopy(bytes75, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
																						num3 += 2;
																					}
																					else
																					{
																						if (msgType == 19)
																						{
																							byte[] bytes77 = BitConverter.GetBytes(msgType);
																							byte b23 = (byte)number;
																							byte[] bytes78 = BitConverter.GetBytes((int)number2);
																							byte[] bytes79 = BitConverter.GetBytes((int)number3);
																							byte b24 = 0;
																							if (number4 == 1f)
																							{
																								b24 = 1;
																							}
																							num2 += 1 + bytes78.Length + bytes79.Length + 1;
																							byte[] bytes80 = BitConverter.GetBytes(num2 - 4);
																							Buffer.BlockCopy(bytes80, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																							Buffer.BlockCopy(bytes77, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																							NetMessage.buffer[num].writeBuffer[num3] = b23;
																							num3++;
																							Buffer.BlockCopy(bytes78, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
																							num3 += 4;
																							Buffer.BlockCopy(bytes79, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
																							num3 += 4;
																							NetMessage.buffer[num].writeBuffer[num3] = b24;
																						}
																						else
																						{
																							if (msgType == 20)
																							{
																								short num7 = (short)number;
																								int num8 = (int)number2;
																								int num9 = (int)number3;
																								byte[] bytes81 = BitConverter.GetBytes(msgType);
																								Buffer.BlockCopy(bytes81, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																								byte[] bytes82 = BitConverter.GetBytes(num7);
																								Buffer.BlockCopy(bytes82, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
																								num3 += 2;
																								byte[] bytes83 = BitConverter.GetBytes(num8);
																								Buffer.BlockCopy(bytes83, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
																								num3 += 4;
																								byte[] bytes84 = BitConverter.GetBytes(num9);
																								Buffer.BlockCopy(bytes84, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
																								num3 += 4;
																								for (int j = num8; j < num8 + (int)num7; j++)
																								{
																									for (int k = num9; k < num9 + (int)num7; k++)
																									{
																										byte b25 = 0;
																										if (Main.tile[j, k].active)
																										{
																											b25 += 1;
																										}
																										if (Main.tile[j, k].lighted)
																										{
																											b25 += 2;
																										}
																										if (Main.tile[j, k].wall > 0)
																										{
																											b25 += 4;
																										}
																										if (Main.tile[j, k].liquid > 0 && Main.netMode == 2)
																										{
																											b25 += 8;
																										}
																										NetMessage.buffer[num].writeBuffer[num3] = b25;
																										num3++;
																										byte[] bytes85 = BitConverter.GetBytes(Main.tile[j, k].frameX);
																										byte[] bytes86 = BitConverter.GetBytes(Main.tile[j, k].frameY);
																										byte wall2 = Main.tile[j, k].wall;
																										if (Main.tile[j, k].active)
																										{
																											NetMessage.buffer[num].writeBuffer[num3] = Main.tile[j, k].type;
																											num3++;
																											if (Main.tileFrameImportant[(int)Main.tile[j, k].type])
																											{
																												Buffer.BlockCopy(bytes85, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
																												num3 += 2;
																												Buffer.BlockCopy(bytes86, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
																												num3 += 2;
																											}
																										}
																										if (wall2 > 0)
																										{
																											NetMessage.buffer[num].writeBuffer[num3] = wall2;
																											num3++;
																										}
																										if (Main.tile[j, k].liquid > 0 && Main.netMode == 2)
																										{
																											NetMessage.buffer[num].writeBuffer[num3] = Main.tile[j, k].liquid;
																											num3++;
																											byte b26 = 0;
																											if (Main.tile[j, k].lava)
																											{
																												b26 = 1;
																											}
																											NetMessage.buffer[num].writeBuffer[num3] = b26;
																											num3++;
																										}
																									}
																								}
																								byte[] bytes87 = BitConverter.GetBytes(num3 - 4);
																								Buffer.BlockCopy(bytes87, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																								num2 = num3;
																							}
																							else
																							{
																								if (msgType == 21)
																								{
																									byte[] bytes88 = BitConverter.GetBytes(msgType);
																									byte[] bytes89 = BitConverter.GetBytes((short)number);
																									byte[] bytes90 = BitConverter.GetBytes(Main.item[number].position.X);
																									byte[] bytes91 = BitConverter.GetBytes(Main.item[number].position.Y);
																									byte[] bytes92 = BitConverter.GetBytes(Main.item[number].velocity.X);
																									byte[] bytes93 = BitConverter.GetBytes(Main.item[number].velocity.Y);
																									byte b27 = (byte)Main.item[number].stack;
																									string text3 = "0";
																									if (Main.item[number].active && Main.item[number].stack > 0)
																									{
																										text3 = Main.item[number].name;
																									}
																									if (text3 == null)
																									{
																										text3 = "0";
																									}
																									byte[] bytes94 = Encoding.ASCII.GetBytes(text3);
																									num2 += bytes89.Length + bytes90.Length + bytes91.Length + bytes92.Length + bytes93.Length + 1 + bytes94.Length;
																									byte[] bytes95 = BitConverter.GetBytes(num2 - 4);
																									Buffer.BlockCopy(bytes95, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																									Buffer.BlockCopy(bytes88, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																									Buffer.BlockCopy(bytes89, 0, NetMessage.buffer[num].writeBuffer, num3, bytes89.Length);
																									num3 += 2;
																									Buffer.BlockCopy(bytes90, 0, NetMessage.buffer[num].writeBuffer, num3, bytes90.Length);
																									num3 += 4;
																									Buffer.BlockCopy(bytes91, 0, NetMessage.buffer[num].writeBuffer, num3, bytes91.Length);
																									num3 += 4;
																									Buffer.BlockCopy(bytes92, 0, NetMessage.buffer[num].writeBuffer, num3, bytes92.Length);
																									num3 += 4;
																									Buffer.BlockCopy(bytes93, 0, NetMessage.buffer[num].writeBuffer, num3, bytes93.Length);
																									num3 += 4;
																									NetMessage.buffer[num].writeBuffer[num3] = b27;
																									num3++;
																									Buffer.BlockCopy(bytes94, 0, NetMessage.buffer[num].writeBuffer, num3, bytes94.Length);
																								}
																								else
																								{
																									if (msgType == 22)
																									{
																										byte[] bytes96 = BitConverter.GetBytes(msgType);
																										byte[] bytes97 = BitConverter.GetBytes((short)number);
																										byte b28 = (byte)Main.item[number].owner;
																										num2 += bytes97.Length + 1;
																										byte[] bytes98 = BitConverter.GetBytes(num2 - 4);
																										Buffer.BlockCopy(bytes98, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																										Buffer.BlockCopy(bytes96, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																										Buffer.BlockCopy(bytes97, 0, NetMessage.buffer[num].writeBuffer, num3, bytes97.Length);
																										num3 += 2;
																										NetMessage.buffer[num].writeBuffer[num3] = b28;
																									}
																									else
																									{
																										if (msgType == 23)
																										{
																											byte[] bytes99 = BitConverter.GetBytes(msgType);
																											byte[] bytes100 = BitConverter.GetBytes((short)number);
																											byte[] bytes101 = BitConverter.GetBytes(Main.npc[number].position.X);
																											byte[] bytes102 = BitConverter.GetBytes(Main.npc[number].position.Y);
																											byte[] bytes103 = BitConverter.GetBytes(Main.npc[number].velocity.X);
																											byte[] bytes104 = BitConverter.GetBytes(Main.npc[number].velocity.Y);
																											byte[] bytes105 = BitConverter.GetBytes((short)Main.npc[number].target);
																											byte[] bytes106 = BitConverter.GetBytes((short)Main.npc[number].life);
																											if (!Main.npc[number].active)
																											{
																												bytes106 = BitConverter.GetBytes(0);
																											}
																											byte[] bytes107 = Encoding.ASCII.GetBytes(Main.npc[number].name);
																											num2 += bytes100.Length + bytes101.Length + bytes102.Length + bytes103.Length + bytes104.Length + bytes105.Length + bytes106.Length + NPC.maxAI * 4 + bytes107.Length + 1 + 1;
																											byte[] bytes108 = BitConverter.GetBytes(num2 - 4);
																											Buffer.BlockCopy(bytes108, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																											Buffer.BlockCopy(bytes99, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																											Buffer.BlockCopy(bytes100, 0, NetMessage.buffer[num].writeBuffer, num3, bytes100.Length);
																											num3 += 2;
																											Buffer.BlockCopy(bytes101, 0, NetMessage.buffer[num].writeBuffer, num3, bytes101.Length);
																											num3 += 4;
																											Buffer.BlockCopy(bytes102, 0, NetMessage.buffer[num].writeBuffer, num3, bytes102.Length);
																											num3 += 4;
																											Buffer.BlockCopy(bytes103, 0, NetMessage.buffer[num].writeBuffer, num3, bytes103.Length);
																											num3 += 4;
																											Buffer.BlockCopy(bytes104, 0, NetMessage.buffer[num].writeBuffer, num3, bytes104.Length);
																											num3 += 4;
																											Buffer.BlockCopy(bytes105, 0, NetMessage.buffer[num].writeBuffer, num3, bytes105.Length);
																											num3 += 2;
																											NetMessage.buffer[num].writeBuffer[num3] = (byte)(Main.npc[number].direction + 1);
																											num3++;
																											NetMessage.buffer[num].writeBuffer[num3] = (byte)(Main.npc[number].directionY + 1);
																											num3++;
																											Buffer.BlockCopy(bytes106, 0, NetMessage.buffer[num].writeBuffer, num3, bytes106.Length);
																											num3 += 2;
																											for (int l = 0; l < NPC.maxAI; l++)
																											{
																												byte[] bytes109 = BitConverter.GetBytes(Main.npc[number].ai[l]);
																												Buffer.BlockCopy(bytes109, 0, NetMessage.buffer[num].writeBuffer, num3, bytes109.Length);
																												num3 += 4;
																											}
																											Buffer.BlockCopy(bytes107, 0, NetMessage.buffer[num].writeBuffer, num3, bytes107.Length);
																										}
																										else
																										{
																											if (msgType == 24)
																											{
																												byte[] bytes110 = BitConverter.GetBytes(msgType);
																												byte[] bytes111 = BitConverter.GetBytes((short)number);
																												byte b29 = (byte)number2;
																												num2 += bytes111.Length + 1;
																												byte[] bytes112 = BitConverter.GetBytes(num2 - 4);
																												Buffer.BlockCopy(bytes112, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																												Buffer.BlockCopy(bytes110, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																												Buffer.BlockCopy(bytes111, 0, NetMessage.buffer[num].writeBuffer, num3, bytes111.Length);
																												num3 += 2;
																												NetMessage.buffer[num].writeBuffer[num3] = b29;
																											}
																											else
																											{
																												if (msgType == 25)
																												{
																													byte[] bytes113 = BitConverter.GetBytes(msgType);
																													byte b30 = (byte)number;
																													byte[] bytes114 = Encoding.ASCII.GetBytes(text);
																													byte b31 = (byte)number2;
																													byte b32 = (byte)number3;
																													byte b33 = (byte)number4;
																													num2 += 1 + bytes114.Length + 3;
																													byte[] bytes115 = BitConverter.GetBytes(num2 - 4);
																													Buffer.BlockCopy(bytes115, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																													Buffer.BlockCopy(bytes113, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																													NetMessage.buffer[num].writeBuffer[num3] = b30;
																													num3++;
																													NetMessage.buffer[num].writeBuffer[num3] = b31;
																													num3++;
																													NetMessage.buffer[num].writeBuffer[num3] = b32;
																													num3++;
																													NetMessage.buffer[num].writeBuffer[num3] = b33;
																													num3++;
																													Buffer.BlockCopy(bytes114, 0, NetMessage.buffer[num].writeBuffer, num3, bytes114.Length);
																												}
																												else
																												{
																													if (msgType == 26)
																													{
																														byte[] bytes116 = BitConverter.GetBytes(msgType);
																														byte b34 = (byte)number;
																														byte b35 = (byte)(number2 + 1f);
																														byte[] bytes117 = BitConverter.GetBytes((short)number3);
																														byte[] bytes118 = Encoding.ASCII.GetBytes(text);
																														byte b36 = (byte)number4;
																														byte b37 = (byte)number5;
																														num2 += 2 + bytes117.Length + 1 + bytes118.Length + 1;
																														byte[] bytes119 = BitConverter.GetBytes(num2 - 4);
																														Buffer.BlockCopy(bytes119, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																														Buffer.BlockCopy(bytes116, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																														NetMessage.buffer[num].writeBuffer[num3] = b34;
																														num3++;
																														NetMessage.buffer[num].writeBuffer[num3] = b35;
																														num3++;
																														Buffer.BlockCopy(bytes117, 0, NetMessage.buffer[num].writeBuffer, num3, bytes117.Length);
																														num3 += 2;
																														NetMessage.buffer[num].writeBuffer[num3] = b36;
																														num3++;
																														NetMessage.buffer[num].writeBuffer[num3] = b37;
																														num3++;
																														Buffer.BlockCopy(bytes118, 0, NetMessage.buffer[num].writeBuffer, num3, bytes118.Length);
																													}
																													else
																													{
																														if (msgType == 27)
																														{
																															byte[] bytes120 = BitConverter.GetBytes(msgType);
																															byte[] bytes121 = BitConverter.GetBytes((short)Main.projectile[number].identity);
																															byte[] bytes122 = BitConverter.GetBytes(Main.projectile[number].position.X);
																															byte[] bytes123 = BitConverter.GetBytes(Main.projectile[number].position.Y);
																															byte[] bytes124 = BitConverter.GetBytes(Main.projectile[number].velocity.X);
																															byte[] bytes125 = BitConverter.GetBytes(Main.projectile[number].velocity.Y);
																															byte[] bytes126 = BitConverter.GetBytes(Main.projectile[number].knockBack);
																															byte[] bytes127 = BitConverter.GetBytes((short)Main.projectile[number].damage);
																															Buffer.BlockCopy(bytes120, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																															Buffer.BlockCopy(bytes121, 0, NetMessage.buffer[num].writeBuffer, num3, bytes121.Length);
																															num3 += 2;
																															Buffer.BlockCopy(bytes122, 0, NetMessage.buffer[num].writeBuffer, num3, bytes122.Length);
																															num3 += 4;
																															Buffer.BlockCopy(bytes123, 0, NetMessage.buffer[num].writeBuffer, num3, bytes123.Length);
																															num3 += 4;
																															Buffer.BlockCopy(bytes124, 0, NetMessage.buffer[num].writeBuffer, num3, bytes124.Length);
																															num3 += 4;
																															Buffer.BlockCopy(bytes125, 0, NetMessage.buffer[num].writeBuffer, num3, bytes125.Length);
																															num3 += 4;
																															Buffer.BlockCopy(bytes126, 0, NetMessage.buffer[num].writeBuffer, num3, bytes126.Length);
																															num3 += 4;
																															Buffer.BlockCopy(bytes127, 0, NetMessage.buffer[num].writeBuffer, num3, bytes127.Length);
																															num3 += 2;
																															NetMessage.buffer[num].writeBuffer[num3] = (byte)Main.projectile[number].owner;
																															num3++;
																															NetMessage.buffer[num].writeBuffer[num3] = (byte)Main.projectile[number].type;
																															num3++;
																															for (int m = 0; m < Projectile.maxAI; m++)
																															{
																																byte[] bytes128 = BitConverter.GetBytes(Main.projectile[number].ai[m]);
																																Buffer.BlockCopy(bytes128, 0, NetMessage.buffer[num].writeBuffer, num3, bytes128.Length);
																																num3 += 4;
																															}
																															num2 += num3;
																															byte[] bytes129 = BitConverter.GetBytes(num2 - 4);
																															Buffer.BlockCopy(bytes129, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																														}
																														else
																														{
																															if (msgType == 28)
																															{
																																byte[] bytes130 = BitConverter.GetBytes(msgType);
																																byte[] bytes131 = BitConverter.GetBytes((short)number);
																																byte[] bytes132 = BitConverter.GetBytes((short)number2);
																																byte[] bytes133 = BitConverter.GetBytes(number3);
																																byte b38 = (byte)(number4 + 1f);
																																byte b39 = (byte)number5;
																																num2 += bytes131.Length + bytes132.Length + bytes133.Length + 1 + 1;
																																byte[] bytes134 = BitConverter.GetBytes(num2 - 4);
																																Buffer.BlockCopy(bytes134, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																Buffer.BlockCopy(bytes130, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																Buffer.BlockCopy(bytes131, 0, NetMessage.buffer[num].writeBuffer, num3, bytes131.Length);
																																num3 += 2;
																																Buffer.BlockCopy(bytes132, 0, NetMessage.buffer[num].writeBuffer, num3, bytes132.Length);
																																num3 += 2;
																																Buffer.BlockCopy(bytes133, 0, NetMessage.buffer[num].writeBuffer, num3, bytes133.Length);
																																num3 += 4;
																																NetMessage.buffer[num].writeBuffer[num3] = b38;
																																num3++;
																																NetMessage.buffer[num].writeBuffer[num3] = b39;
																															}
																															else
																															{
																																if (msgType == 29)
																																{
																																	byte[] bytes135 = BitConverter.GetBytes(msgType);
																																	byte[] bytes136 = BitConverter.GetBytes((short)number);
																																	byte b40 = (byte)number2;
																																	num2 += bytes136.Length + 1;
																																	byte[] bytes137 = BitConverter.GetBytes(num2 - 4);
																																	Buffer.BlockCopy(bytes137, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																	Buffer.BlockCopy(bytes135, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																	Buffer.BlockCopy(bytes136, 0, NetMessage.buffer[num].writeBuffer, num3, bytes136.Length);
																																	num3 += 2;
																																	NetMessage.buffer[num].writeBuffer[num3] = b40;
																																}
																																else
																																{
																																	if (msgType == 30)
																																	{
																																		byte[] bytes138 = BitConverter.GetBytes(msgType);
																																		byte b41 = (byte)number;
																																		byte b42 = 0;
																																		if (Main.player[(int)b41].hostile)
																																		{
																																			b42 = 1;
																																		}
																																		num2 += 2;
																																		byte[] bytes139 = BitConverter.GetBytes(num2 - 4);
																																		Buffer.BlockCopy(bytes139, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																		Buffer.BlockCopy(bytes138, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																		NetMessage.buffer[num].writeBuffer[num3] = b41;
																																		num3++;
																																		NetMessage.buffer[num].writeBuffer[num3] = b42;
																																	}
																																	else
																																	{
																																		if (msgType == 31)
																																		{
																																			byte[] bytes140 = BitConverter.GetBytes(msgType);
																																			byte[] bytes141 = BitConverter.GetBytes(number);
																																			byte[] bytes142 = BitConverter.GetBytes((int)number2);
																																			num2 += bytes141.Length + bytes142.Length;
																																			byte[] bytes143 = BitConverter.GetBytes(num2 - 4);
																																			Buffer.BlockCopy(bytes143, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																			Buffer.BlockCopy(bytes140, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																			Buffer.BlockCopy(bytes141, 0, NetMessage.buffer[num].writeBuffer, num3, bytes141.Length);
																																			num3 += 4;
																																			Buffer.BlockCopy(bytes142, 0, NetMessage.buffer[num].writeBuffer, num3, bytes142.Length);
																																		}
																																		else
																																		{
																																			if (msgType == 32)
																																			{
																																				byte[] bytes144 = BitConverter.GetBytes(msgType);
																																				byte[] bytes145 = BitConverter.GetBytes((short)number);
																																				byte b43 = (byte)number2;
																																				byte b44 = (byte)Main.chest[number].item[(int)number2].stack;
																																				byte[] bytes146;
																																				if (Main.chest[number].item[(int)number2].name == null)
																																				{
																																					bytes146 = Encoding.ASCII.GetBytes("");
																																				}
																																				else
																																				{
																																					bytes146 = Encoding.ASCII.GetBytes(Main.chest[number].item[(int)number2].name);
																																				}
																																				num2 += bytes145.Length + 1 + 1 + bytes146.Length;
																																				byte[] bytes147 = BitConverter.GetBytes(num2 - 4);
																																				Buffer.BlockCopy(bytes147, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																				Buffer.BlockCopy(bytes144, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																				Buffer.BlockCopy(bytes145, 0, NetMessage.buffer[num].writeBuffer, num3, bytes145.Length);
																																				num3 += 2;
																																				NetMessage.buffer[num].writeBuffer[num3] = b43;
																																				num3++;
																																				NetMessage.buffer[num].writeBuffer[num3] = b44;
																																				num3++;
																																				Buffer.BlockCopy(bytes146, 0, NetMessage.buffer[num].writeBuffer, num3, bytes146.Length);
																																			}
																																			else
																																			{
																																				if (msgType == 33)
																																				{
																																					byte[] bytes148 = BitConverter.GetBytes(msgType);
																																					byte[] bytes149 = BitConverter.GetBytes((short)number);
																																					byte[] bytes150;
																																					byte[] bytes151;
																																					if (number > -1)
																																					{
																																						bytes150 = BitConverter.GetBytes(Main.chest[number].x);
																																						bytes151 = BitConverter.GetBytes(Main.chest[number].y);
																																					}
																																					else
																																					{
																																						bytes150 = BitConverter.GetBytes(0);
																																						bytes151 = BitConverter.GetBytes(0);
																																					}
																																					num2 += bytes149.Length + bytes150.Length + bytes151.Length;
																																					byte[] bytes152 = BitConverter.GetBytes(num2 - 4);
																																					Buffer.BlockCopy(bytes152, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																					Buffer.BlockCopy(bytes148, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																					Buffer.BlockCopy(bytes149, 0, NetMessage.buffer[num].writeBuffer, num3, bytes149.Length);
																																					num3 += 2;
																																					Buffer.BlockCopy(bytes150, 0, NetMessage.buffer[num].writeBuffer, num3, bytes150.Length);
																																					num3 += 4;
																																					Buffer.BlockCopy(bytes151, 0, NetMessage.buffer[num].writeBuffer, num3, bytes151.Length);
																																				}
																																				else
																																				{
																																					if (msgType == 34)
																																					{
																																						byte[] bytes153 = BitConverter.GetBytes(msgType);
																																						byte[] bytes154 = BitConverter.GetBytes(number);
																																						byte[] bytes155 = BitConverter.GetBytes((int)number2);
																																						num2 += bytes154.Length + bytes155.Length;
																																						byte[] bytes156 = BitConverter.GetBytes(num2 - 4);
																																						Buffer.BlockCopy(bytes156, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																						Buffer.BlockCopy(bytes153, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																						Buffer.BlockCopy(bytes154, 0, NetMessage.buffer[num].writeBuffer, num3, bytes154.Length);
																																						num3 += 4;
																																						Buffer.BlockCopy(bytes155, 0, NetMessage.buffer[num].writeBuffer, num3, bytes155.Length);
																																					}
																																					else
																																					{
																																						if (msgType == 35)
																																						{
																																							byte[] bytes157 = BitConverter.GetBytes(msgType);
																																							byte b45 = (byte)number;
																																							byte[] bytes158 = BitConverter.GetBytes((short)number2);
																																							num2 += 1 + bytes158.Length;
																																							byte[] bytes159 = BitConverter.GetBytes(num2 - 4);
																																							Buffer.BlockCopy(bytes159, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																							Buffer.BlockCopy(bytes157, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																							NetMessage.buffer[num].writeBuffer[5] = b45;
																																							num3++;
																																							Buffer.BlockCopy(bytes158, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
																																						}
																																						else
																																						{
																																							if (msgType == 36)
																																							{
																																								byte[] bytes160 = BitConverter.GetBytes(msgType);
																																								byte b46 = (byte)number;
																																								byte b47 = 0;
																																								if (Main.player[(int)b46].zoneEvil)
																																								{
																																									b47 = 1;
																																								}
																																								byte b48 = 0;
																																								if (Main.player[(int)b46].zoneMeteor)
																																								{
																																									b48 = 1;
																																								}
																																								byte b49 = 0;
																																								if (Main.player[(int)b46].zoneDungeon)
																																								{
																																									b49 = 1;
																																								}
																																								byte b50 = 0;
																																								if (Main.player[(int)b46].zoneJungle)
																																								{
																																									b50 = 1;
																																								}
																																								num2 += 5;
																																								byte[] bytes161 = BitConverter.GetBytes(num2 - 4);
																																								Buffer.BlockCopy(bytes161, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																								Buffer.BlockCopy(bytes160, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																								NetMessage.buffer[num].writeBuffer[num3] = b46;
																																								num3++;
																																								NetMessage.buffer[num].writeBuffer[num3] = b47;
																																								num3++;
																																								NetMessage.buffer[num].writeBuffer[num3] = b48;
																																								num3++;
																																								NetMessage.buffer[num].writeBuffer[num3] = b49;
																																								num3++;
																																								NetMessage.buffer[num].writeBuffer[num3] = b50;
																																								num3++;
																																							}
																																							else
																																							{
																																								if (msgType == 37)
																																								{
																																									byte[] bytes162 = BitConverter.GetBytes(msgType);
																																									byte[] bytes163 = BitConverter.GetBytes(num2 - 4);
																																									Buffer.BlockCopy(bytes163, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																									Buffer.BlockCopy(bytes162, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																								}
																																								else
																																								{
																																									if (msgType == 38)
																																									{
																																										byte[] bytes164 = BitConverter.GetBytes(msgType);
																																										byte[] bytes165 = Encoding.ASCII.GetBytes(text);
																																										num2 += bytes165.Length;
																																										byte[] bytes166 = BitConverter.GetBytes(num2 - 4);
																																										Buffer.BlockCopy(bytes166, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																										Buffer.BlockCopy(bytes164, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																										Buffer.BlockCopy(bytes165, 0, NetMessage.buffer[num].writeBuffer, num3, bytes165.Length);
																																									}
																																									else
																																									{
																																										if (msgType == 39)
																																										{
																																											byte[] bytes167 = BitConverter.GetBytes(msgType);
																																											byte[] bytes168 = BitConverter.GetBytes((short)number);
																																											num2 += bytes168.Length;
																																											byte[] bytes169 = BitConverter.GetBytes(num2 - 4);
																																											Buffer.BlockCopy(bytes169, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																											Buffer.BlockCopy(bytes167, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																											Buffer.BlockCopy(bytes168, 0, NetMessage.buffer[num].writeBuffer, num3, bytes168.Length);
																																										}
																																										else
																																										{
																																											if (msgType == 40)
																																											{
																																												byte[] bytes170 = BitConverter.GetBytes(msgType);
																																												byte b51 = (byte)number;
																																												byte[] bytes171 = BitConverter.GetBytes((short)Main.player[(int)b51].talkNPC);
																																												num2 += 1 + bytes171.Length;
																																												byte[] bytes172 = BitConverter.GetBytes(num2 - 4);
																																												Buffer.BlockCopy(bytes172, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																												Buffer.BlockCopy(bytes170, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																												NetMessage.buffer[num].writeBuffer[num3] = b51;
																																												num3++;
																																												Buffer.BlockCopy(bytes171, 0, NetMessage.buffer[num].writeBuffer, num3, bytes171.Length);
																																												num3 += 2;
																																											}
																																											else
																																											{
																																												if (msgType == 41)
																																												{
																																													byte[] bytes173 = BitConverter.GetBytes(msgType);
																																													byte b52 = (byte)number;
																																													byte[] bytes174 = BitConverter.GetBytes(Main.player[(int)b52].itemRotation);
																																													byte[] bytes175 = BitConverter.GetBytes((short)Main.player[(int)b52].itemAnimation);
																																													num2 += 1 + bytes174.Length + bytes175.Length;
																																													byte[] bytes176 = BitConverter.GetBytes(num2 - 4);
																																													Buffer.BlockCopy(bytes176, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																													Buffer.BlockCopy(bytes173, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																													NetMessage.buffer[num].writeBuffer[num3] = b52;
																																													num3++;
																																													Buffer.BlockCopy(bytes174, 0, NetMessage.buffer[num].writeBuffer, num3, bytes174.Length);
																																													num3 += 4;
																																													Buffer.BlockCopy(bytes175, 0, NetMessage.buffer[num].writeBuffer, num3, bytes175.Length);
																																												}
																																												else
																																												{
																																													if (msgType == 42)
																																													{
																																														byte[] bytes177 = BitConverter.GetBytes(msgType);
																																														byte b53 = (byte)number;
																																														byte[] bytes178 = BitConverter.GetBytes((short)Main.player[(int)b53].statMana);
																																														byte[] bytes179 = BitConverter.GetBytes((short)Main.player[(int)b53].statManaMax);
																																														num2 += 1 + bytes178.Length + bytes179.Length;
																																														byte[] bytes180 = BitConverter.GetBytes(num2 - 4);
																																														Buffer.BlockCopy(bytes180, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																														Buffer.BlockCopy(bytes177, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																														NetMessage.buffer[num].writeBuffer[5] = b53;
																																														num3++;
																																														Buffer.BlockCopy(bytes178, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
																																														num3 += 2;
																																														Buffer.BlockCopy(bytes179, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
																																													}
																																													else
																																													{
																																														if (msgType == 43)
																																														{
																																															byte[] bytes181 = BitConverter.GetBytes(msgType);
																																															byte b54 = (byte)number;
																																															byte[] bytes182 = BitConverter.GetBytes((short)number2);
																																															num2 += 1 + bytes182.Length;
																																															byte[] bytes183 = BitConverter.GetBytes(num2 - 4);
																																															Buffer.BlockCopy(bytes183, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																															Buffer.BlockCopy(bytes181, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																															NetMessage.buffer[num].writeBuffer[5] = b54;
																																															num3++;
																																															Buffer.BlockCopy(bytes182, 0, NetMessage.buffer[num].writeBuffer, num3, 2);
																																														}
																																														else
																																														{
																																															if (msgType == 44)
																																															{
																																																byte[] bytes184 = BitConverter.GetBytes(msgType);
																																																byte b55 = (byte)number;
																																																byte b56 = (byte)(number2 + 1f);
																																																byte[] bytes185 = BitConverter.GetBytes((short)number3);
																																																byte b57 = (byte)number4;
																																																byte[] bytes186 = Encoding.ASCII.GetBytes(text);
																																																num2 += 2 + bytes185.Length + 1 + bytes186.Length;
																																																byte[] bytes187 = BitConverter.GetBytes(num2 - 4);
																																																Buffer.BlockCopy(bytes187, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																																Buffer.BlockCopy(bytes184, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																																NetMessage.buffer[num].writeBuffer[num3] = b55;
																																																num3++;
																																																NetMessage.buffer[num].writeBuffer[num3] = b56;
																																																num3++;
																																																Buffer.BlockCopy(bytes185, 0, NetMessage.buffer[num].writeBuffer, num3, bytes185.Length);
																																																num3 += 2;
																																																NetMessage.buffer[num].writeBuffer[num3] = b57;
																																																num3++;
																																																Buffer.BlockCopy(bytes186, 0, NetMessage.buffer[num].writeBuffer, num3, bytes186.Length);
																																																num3 += bytes186.Length;
																																															}
																																															else
																																															{
																																																if (msgType == 45)
																																																{
																																																	byte[] bytes188 = BitConverter.GetBytes(msgType);
																																																	byte b58 = (byte)number;
																																																	byte b59 = (byte)Main.player[(int)b58].team;
																																																	num2 += 2;
																																																	byte[] bytes189 = BitConverter.GetBytes(num2 - 4);
																																																	Buffer.BlockCopy(bytes189, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																																	Buffer.BlockCopy(bytes188, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																																	NetMessage.buffer[num].writeBuffer[5] = b58;
																																																	num3++;
																																																	NetMessage.buffer[num].writeBuffer[num3] = b59;
																																																}
																																																else
																																																{
																																																	if (msgType == 46)
																																																	{
																																																		byte[] bytes190 = BitConverter.GetBytes(msgType);
																																																		byte[] bytes191 = BitConverter.GetBytes(number);
																																																		byte[] bytes192 = BitConverter.GetBytes((int)number2);
																																																		num2 += bytes191.Length + bytes192.Length;
																																																		byte[] bytes193 = BitConverter.GetBytes(num2 - 4);
																																																		Buffer.BlockCopy(bytes193, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																																		Buffer.BlockCopy(bytes190, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																																		Buffer.BlockCopy(bytes191, 0, NetMessage.buffer[num].writeBuffer, num3, bytes191.Length);
																																																		num3 += 4;
																																																		Buffer.BlockCopy(bytes192, 0, NetMessage.buffer[num].writeBuffer, num3, bytes192.Length);
																																																	}
																																																	else
																																																	{
																																																		if (msgType == 47)
																																																		{
																																																			byte[] bytes194 = BitConverter.GetBytes(msgType);
																																																			byte[] bytes195 = BitConverter.GetBytes((short)number);
																																																			byte[] bytes196 = BitConverter.GetBytes(Main.sign[number].x);
																																																			byte[] bytes197 = BitConverter.GetBytes(Main.sign[number].y);
																																																			byte[] bytes198 = Encoding.ASCII.GetBytes(Main.sign[number].text);
																																																			num2 += bytes195.Length + bytes196.Length + bytes197.Length + bytes198.Length;
																																																			byte[] bytes199 = BitConverter.GetBytes(num2 - 4);
																																																			Buffer.BlockCopy(bytes199, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																																			Buffer.BlockCopy(bytes194, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																																			Buffer.BlockCopy(bytes195, 0, NetMessage.buffer[num].writeBuffer, num3, bytes195.Length);
																																																			num3 += bytes195.Length;
																																																			Buffer.BlockCopy(bytes196, 0, NetMessage.buffer[num].writeBuffer, num3, bytes196.Length);
																																																			num3 += bytes196.Length;
																																																			Buffer.BlockCopy(bytes197, 0, NetMessage.buffer[num].writeBuffer, num3, bytes197.Length);
																																																			num3 += bytes197.Length;
																																																			Buffer.BlockCopy(bytes198, 0, NetMessage.buffer[num].writeBuffer, num3, bytes198.Length);
																																																			num3 += bytes198.Length;
																																																		}
																																																		else
																																																		{
																																																			if (msgType == 48)
																																																			{
																																																				byte[] bytes200 = BitConverter.GetBytes(msgType);
																																																				byte[] bytes201 = BitConverter.GetBytes(number);
																																																				byte[] bytes202 = BitConverter.GetBytes((int)number2);
																																																				byte liquid = Main.tile[number, (int)number2].liquid;
																																																				byte b60 = 0;
																																																				if (Main.tile[number, (int)number2].lava)
																																																				{
																																																					b60 = 1;
																																																				}
																																																				num2 += bytes201.Length + bytes202.Length + 1 + 1;
																																																				byte[] bytes203 = BitConverter.GetBytes(num2 - 4);
																																																				Buffer.BlockCopy(bytes203, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																																				Buffer.BlockCopy(bytes200, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																																				Buffer.BlockCopy(bytes201, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
																																																				num3 += 4;
																																																				Buffer.BlockCopy(bytes202, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
																																																				num3 += 4;
																																																				NetMessage.buffer[num].writeBuffer[num3] = liquid;
																																																				num3++;
																																																				NetMessage.buffer[num].writeBuffer[num3] = b60;
																																																				num3++;
																																																			}
																																																			else
																																																			{
																																																				if (msgType == 49)
																																																				{
																																																					byte[] bytes204 = BitConverter.GetBytes(msgType);
																																																					byte[] bytes205 = BitConverter.GetBytes(num2 - 4);
																																																					Buffer.BlockCopy(bytes205, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																																					Buffer.BlockCopy(bytes204, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																																				}
																																																				else
																																																				{
																																																					if (msgType == 50)
																																																					{
																																																						byte[] bytes206 = BitConverter.GetBytes(msgType);
																																																						byte b61 = (byte)number;
																																																						num2 += 11;
																																																						byte[] bytes207 = BitConverter.GetBytes(num2 - 4);
																																																						Buffer.BlockCopy(bytes207, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																																						Buffer.BlockCopy(bytes206, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																																						NetMessage.buffer[num].writeBuffer[num3] = b61;
																																																						num3++;
																																																						for (int n = 0; n < 10; n++)
																																																						{
																																																							NetMessage.buffer[num].writeBuffer[num3] = (byte)Main.player[(int)b61].buffType[n];
																																																							num3++;
																																																						}
																																																					}
																																																					else
																																																					{
																																																						if (msgType == 51)
																																																						{
																																																							byte[] bytes208 = BitConverter.GetBytes(msgType);
																																																							num2 += 2;
																																																							byte b62 = (byte)number;
																																																							byte b63 = (byte)number2;
																																																							byte[] bytes209 = BitConverter.GetBytes(num2 - 4);
																																																							Buffer.BlockCopy(bytes209, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																																							Buffer.BlockCopy(bytes208, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																																							NetMessage.buffer[num].writeBuffer[num3] = b62;
																																																							num3++;
																																																							NetMessage.buffer[num].writeBuffer[num3] = b63;
																																																						}
																																																						else
																																																						{
																																																							if (msgType == 52)
																																																							{
																																																								byte[] bytes210 = BitConverter.GetBytes(msgType);
																																																								byte b64 = (byte)number;
																																																								byte b65 = (byte)number2;
																																																								byte[] bytes211 = BitConverter.GetBytes((int)number3);
																																																								byte[] bytes212 = BitConverter.GetBytes((int)number4);
																																																								num2 += 2 + bytes211.Length + bytes212.Length;
																																																								byte[] bytes213 = BitConverter.GetBytes(num2 - 4);
																																																								Buffer.BlockCopy(bytes213, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																																								Buffer.BlockCopy(bytes210, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																																								NetMessage.buffer[num].writeBuffer[num3] = b64;
																																																								num3++;
																																																								NetMessage.buffer[num].writeBuffer[num3] = b65;
																																																								num3++;
																																																								Buffer.BlockCopy(bytes211, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
																																																								num3 += 4;
																																																								Buffer.BlockCopy(bytes212, 0, NetMessage.buffer[num].writeBuffer, num3, 4);
																																																								num3 += 4;
																																																							}
																																																							else
																																																							{
																																																								if (msgType == 53)
																																																								{
																																																									byte[] bytes214 = BitConverter.GetBytes(msgType);
																																																									byte[] bytes215 = BitConverter.GetBytes((short)number);
																																																									byte b66 = (byte)number2;
																																																									byte[] bytes216 = BitConverter.GetBytes((short)number3);
																																																									num2 += bytes215.Length + 1 + bytes216.Length;
																																																									byte[] bytes217 = BitConverter.GetBytes(num2 - 4);
																																																									Buffer.BlockCopy(bytes217, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																																									Buffer.BlockCopy(bytes214, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																																									Buffer.BlockCopy(bytes215, 0, NetMessage.buffer[num].writeBuffer, num3, bytes215.Length);
																																																									num3 += bytes215.Length;
																																																									NetMessage.buffer[num].writeBuffer[num3] = b66;
																																																									num3++;
																																																									Buffer.BlockCopy(bytes216, 0, NetMessage.buffer[num].writeBuffer, num3, bytes216.Length);
																																																									num3 += bytes216.Length;
																																																								}
																																																								else
																																																								{
																																																									if (msgType == 54)
																																																									{
																																																										byte[] bytes218 = BitConverter.GetBytes(msgType);
																																																										byte[] bytes219 = BitConverter.GetBytes((short)number);
																																																										num2 += bytes219.Length + 15;
																																																										byte[] bytes220 = BitConverter.GetBytes(num2 - 4);
																																																										Buffer.BlockCopy(bytes220, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																																										Buffer.BlockCopy(bytes218, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																																										Buffer.BlockCopy(bytes219, 0, NetMessage.buffer[num].writeBuffer, num3, bytes219.Length);
																																																										num3 += bytes219.Length;
																																																										for (int num10 = 0; num10 < 5; num10++)
																																																										{
																																																											NetMessage.buffer[num].writeBuffer[num3] = (byte)Main.npc[(int)((short)number)].buffType[num10];
																																																											num3++;
																																																											byte[] bytes221 = BitConverter.GetBytes(Main.npc[(int)((short)number)].buffTime[num10]);
																																																											Buffer.BlockCopy(bytes221, 0, NetMessage.buffer[num].writeBuffer, num3, bytes221.Length);
																																																											num3 += bytes221.Length;
																																																										}
																																																									}
																																																									else
																																																									{
																																																										if (msgType == 55)
																																																										{
																																																											byte[] bytes222 = BitConverter.GetBytes(msgType);
																																																											byte b67 = (byte)number;
																																																											byte b68 = (byte)number2;
																																																											byte[] bytes223 = BitConverter.GetBytes((short)number3);
																																																											num2 += 2 + bytes223.Length;
																																																											byte[] bytes224 = BitConverter.GetBytes(num2 - 4);
																																																											Buffer.BlockCopy(bytes224, 0, NetMessage.buffer[num].writeBuffer, 0, 4);
																																																											Buffer.BlockCopy(bytes222, 0, NetMessage.buffer[num].writeBuffer, 4, 1);
																																																											NetMessage.buffer[num].writeBuffer[num3] = b67;
																																																											num3++;
																																																											NetMessage.buffer[num].writeBuffer[num3] = b68;
																																																											num3++;
																																																											Buffer.BlockCopy(bytes223, 0, NetMessage.buffer[num].writeBuffer, num3, bytes223.Length);
																																																											num3 += bytes223.Length;
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
					}
				}
				if (Main.netMode != 1)
				{
					goto IL_3900;
				}
				if (Netplay.clientSock.tcpClient.Connected)
				{
					try
					{
						NetMessage.buffer[num].spamCount++;
						Netplay.clientSock.networkStream.BeginWrite(NetMessage.buffer[num].writeBuffer, 0, num2, new AsyncCallback(Netplay.clientSock.ClientWriteCallBack), Netplay.clientSock.networkStream);
						goto IL_3A40;
					}
					catch
					{
						goto IL_3A40;
					}
					goto IL_3900;
				}
				IL_3A40:
				if (Main.verboseNetplay)
				{
					for (int num11 = 0; num11 < num2; num11++)
					{
					}
					for (int num12 = 0; num12 < num2; num12++)
					{
						byte arg_3A77_0 = NetMessage.buffer[num].writeBuffer[num12];
					}
					goto IL_3A89;
				}
				goto IL_3A89;
				IL_3900:
				if (remoteClient == -1)
				{
					for (int num13 = 0; num13 < 256; num13++)
					{
						if (num13 != ignoreClient && (NetMessage.buffer[num13].broadcast || (Netplay.serverSock[num13].state >= 3 && msgType == 10)) && Netplay.serverSock[num13].tcpClient.Connected)
						{
							try
							{
								NetMessage.buffer[num13].spamCount++;
								Netplay.serverSock[num13].networkStream.BeginWrite(NetMessage.buffer[num].writeBuffer, 0, num2, new AsyncCallback(Netplay.serverSock[num13].ServerWriteCallBack), Netplay.serverSock[num13].networkStream);
							}
							catch
							{
							}
						}
					}
					goto IL_3A40;
				}
				if (Netplay.serverSock[remoteClient].tcpClient.Connected)
				{
					try
					{
						NetMessage.buffer[remoteClient].spamCount++;
						Netplay.serverSock[remoteClient].networkStream.BeginWrite(NetMessage.buffer[num].writeBuffer, 0, num2, new AsyncCallback(Netplay.serverSock[remoteClient].ServerWriteCallBack), Netplay.serverSock[remoteClient].networkStream);
					}
					catch
					{
					}
					goto IL_3A40;
				}
				goto IL_3A40;
				IL_3A89:
				NetMessage.buffer[num].writeLocked = false;
				if (msgType == 19 && Main.netMode == 1)
				{
					int size = 5;
					NetMessage.SendTileSquare(num, (int)number2, (int)number3, size);
				}
				if (msgType == 2 && Main.netMode == 2)
				{
					Netplay.serverSock[num].kill = true;
				}
			}
		}
		public static void RecieveBytes(byte[] bytes, int streamLength, int i = 256)
		{
			lock (NetMessage.buffer[i])
			{
				try
				{
					Buffer.BlockCopy(bytes, 0, NetMessage.buffer[i].readBuffer, NetMessage.buffer[i].totalData, streamLength);
					NetMessage.buffer[i].totalData += streamLength;
					NetMessage.buffer[i].checkBytes = true;
				}
				catch
				{
					if (Main.netMode == 1)
					{
						Main.menuMode = 15;
						Main.statusText = "Bad header lead to a read buffer overflow.";
						Netplay.disconnect = true;
					}
					else
					{
						Netplay.serverSock[i].kill = true;
					}
				}
			}
		}
		public static void CheckBytes(int i = 256)
		{
			lock (NetMessage.buffer[i])
			{
				int num = 0;
				if (NetMessage.buffer[i].totalData >= 4)
				{
					if (NetMessage.buffer[i].messageLength == 0)
					{
						NetMessage.buffer[i].messageLength = BitConverter.ToInt32(NetMessage.buffer[i].readBuffer, 0) + 4;
					}
					while (NetMessage.buffer[i].totalData >= NetMessage.buffer[i].messageLength + num && NetMessage.buffer[i].messageLength > 0)
					{
						if (!Main.ignoreErrors)
						{
							NetMessage.buffer[i].GetData(num + 4, NetMessage.buffer[i].messageLength - 4);
						}
						else
						{
							try
							{
								NetMessage.buffer[i].GetData(num + 4, NetMessage.buffer[i].messageLength - 4);
							}
							catch
							{
							}
						}
						num += NetMessage.buffer[i].messageLength;
						if (NetMessage.buffer[i].totalData - num >= 4)
						{
							NetMessage.buffer[i].messageLength = BitConverter.ToInt32(NetMessage.buffer[i].readBuffer, num) + 4;
						}
						else
						{
							NetMessage.buffer[i].messageLength = 0;
						}
					}
					if (num == NetMessage.buffer[i].totalData)
					{
						NetMessage.buffer[i].totalData = 0;
					}
					else
					{
						if (num > 0)
						{
							Buffer.BlockCopy(NetMessage.buffer[i].readBuffer, num, NetMessage.buffer[i].readBuffer, 0, NetMessage.buffer[i].totalData - num);
							NetMessage.buffer[i].totalData -= num;
						}
					}
					NetMessage.buffer[i].checkBytes = false;
				}
			}
		}
		public static void BootPlayer(int plr, string msg)
		{
			NetMessage.SendData(2, plr, -1, msg, 0, 0f, 0f, 0f, 0);
		}
		public static void SendTileSquare(int whoAmi, int tileX, int tileY, int size)
		{
			int num = (size - 1) / 2;
			NetMessage.SendData(20, whoAmi, -1, "", size, (float)(tileX - num), (float)(tileY - num), 0f, 0);
		}
		public static void SendSection(int whoAmi, int sectionX, int sectionY)
		{
			if (Main.netMode != 2)
			{
				return;
			}
			try
			{
				if (sectionX >= 0 && sectionY >= 0 && sectionX < Main.maxSectionsX && sectionY < Main.maxSectionsY)
				{
					Netplay.serverSock[whoAmi].tileSection[sectionX, sectionY] = true;
					int num = sectionX * 200;
					int num2 = sectionY * 150;
					for (int i = num2; i < num2 + 150; i++)
					{
						NetMessage.SendData(10, whoAmi, -1, "", 200, (float)num, (float)i, 0f, 0);
					}
				}
			}
			catch
			{
			}
		}
		public static void greetPlayer(int plr)
		{
			if (Main.motd == "")
			{
				NetMessage.SendData(25, plr, -1, "Welcome to " + Main.worldName + "!", 255, 255f, 240f, 20f, 0);
			}
			else
			{
				NetMessage.SendData(25, plr, -1, Main.motd, 255, 255f, 240f, 20f, 0);
			}
			string text = "";
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					if (text == "")
					{
						text += Main.player[i].name;
					}
					else
					{
						text = text + ", " + Main.player[i].name;
					}
				}
			}
			NetMessage.SendData(25, plr, -1, "Current players: " + text + ".", 255, 255f, 240f, 20f, 0);
		}
		public static void sendWater(int x, int y)
		{
			if (Main.netMode == 1)
			{
				NetMessage.SendData(48, -1, -1, "", x, (float)y, 0f, 0f, 0);
				return;
			}
			for (int i = 0; i < 256; i++)
			{
				if ((NetMessage.buffer[i].broadcast || Netplay.serverSock[i].state >= 3) && Netplay.serverSock[i].tcpClient.Connected)
				{
					int num = x / 200;
					int num2 = y / 150;
					if (Netplay.serverSock[i].tileSection[num, num2])
					{
						NetMessage.SendData(48, i, -1, "", x, (float)y, 0f, 0f, 0);
					}
				}
			}
		}
		public static void syncPlayers()
		{
			bool flag = false;
			for (int i = 0; i < 255; i++)
			{
				int num = 0;
				if (Main.player[i].active)
				{
					num = 1;
				}
				if (Netplay.serverSock[i].state == 10)
				{
					if (Main.autoShutdown && !flag)
					{
						string text = Netplay.serverSock[i].tcpClient.Client.RemoteEndPoint.ToString();
						string a = text;
						for (int j = 0; j < text.Length; j++)
						{
							if (text.Substring(j, 1) == ":")
							{
								a = text.Substring(0, j);
							}
						}
						if (a == "127.0.0.1")
						{
							flag = true;
						}
					}
					NetMessage.SendData(14, -1, i, "", i, (float)num, 0f, 0f, 0);
					NetMessage.SendData(4, -1, i, Main.player[i].name, i, 0f, 0f, 0f, 0);
					NetMessage.SendData(13, -1, i, "", i, 0f, 0f, 0f, 0);
					NetMessage.SendData(16, -1, i, "", i, 0f, 0f, 0f, 0);
					NetMessage.SendData(30, -1, i, "", i, 0f, 0f, 0f, 0);
					NetMessage.SendData(45, -1, i, "", i, 0f, 0f, 0f, 0);
					NetMessage.SendData(42, -1, i, "", i, 0f, 0f, 0f, 0);
					NetMessage.SendData(50, -1, i, "", i, 0f, 0f, 0f, 0);
					for (int k = 0; k < 44; k++)
					{
						NetMessage.SendData(5, -1, i, Main.player[i].inventory[k].name, i, (float)k, 0f, 0f, 0);
					}
					NetMessage.SendData(5, -1, i, Main.player[i].armor[0].name, i, 44f, 0f, 0f, 0);
					NetMessage.SendData(5, -1, i, Main.player[i].armor[1].name, i, 45f, 0f, 0f, 0);
					NetMessage.SendData(5, -1, i, Main.player[i].armor[2].name, i, 46f, 0f, 0f, 0);
					NetMessage.SendData(5, -1, i, Main.player[i].armor[3].name, i, 47f, 0f, 0f, 0);
					NetMessage.SendData(5, -1, i, Main.player[i].armor[4].name, i, 48f, 0f, 0f, 0);
					NetMessage.SendData(5, -1, i, Main.player[i].armor[5].name, i, 49f, 0f, 0f, 0);
					NetMessage.SendData(5, -1, i, Main.player[i].armor[6].name, i, 50f, 0f, 0f, 0);
					NetMessage.SendData(5, -1, i, Main.player[i].armor[7].name, i, 51f, 0f, 0f, 0);
					NetMessage.SendData(5, -1, i, Main.player[i].armor[8].name, i, 52f, 0f, 0f, 0);
					NetMessage.SendData(5, -1, i, Main.player[i].armor[9].name, i, 53f, 0f, 0f, 0);
					NetMessage.SendData(5, -1, i, Main.player[i].armor[10].name, i, 54f, 0f, 0f, 0);
					if (!Netplay.serverSock[i].announced)
					{
						Netplay.serverSock[i].announced = true;
						NetMessage.SendData(25, -1, i, Main.player[i].name + " has joined.", 255, 255f, 240f, 20f, 0);
						if (Main.dedServ)
						{
							Console.WriteLine(Main.player[i].name + " has joined.");
						}
					}
				}
				else
				{
					num = 0;
					NetMessage.SendData(14, -1, i, "", i, (float)num, 0f, 0f, 0);
					if (Netplay.serverSock[i].announced)
					{
						Netplay.serverSock[i].announced = false;
						NetMessage.SendData(25, -1, i, Netplay.serverSock[i].oldName + " has left.", 255, 255f, 240f, 20f, 0);
						if (Main.dedServ)
						{
							Console.WriteLine(Netplay.serverSock[i].oldName + " has left.");
						}
					}
				}
			}
			if (Main.autoShutdown && !flag)
			{
				WorldGen.saveWorld(false);
				Netplay.disconnect = true;
			}
		}
	}
}
