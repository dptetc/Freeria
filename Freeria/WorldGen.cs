using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
namespace Freeria
{
	internal class WorldGen
	{
		private static object padlock = new object();
		public static int lavaLine;
		public static int waterLine;
		public static bool noTileActions = false;
		public static bool spawnEye = false;
		public static bool gen = false;
		public static bool shadowOrbSmashed = false;
		public static int shadowOrbCount = 0;
		public static bool spawnMeteor = false;
		public static bool loadFailed = false;
		public static bool loadSuccess = false;
		public static bool worldCleared = false;
		public static bool worldBackup = false;
		public static bool loadBackup = false;
		private static int lastMaxTilesX = 0;
		private static int lastMaxTilesY = 0;
		public static bool saveLock = false;
		private static bool mergeUp = false;
		private static bool mergeDown = false;
		private static bool mergeLeft = false;
		private static bool mergeRight = false;
		private static int tempMoonPhase = Main.moonPhase;
		private static bool tempDayTime = Main.dayTime;
		private static bool tempBloodMoon = Main.bloodMoon;
		private static double tempTime = Main.time;
		private static bool stopDrops = false;
		private static bool mudWall = false;
		public static bool noLiquidCheck = false;
		[ThreadStatic]
		public static Random genRand = new Random();
		public static string statusText = "";
		private static bool destroyObject = false;
		public static int spawnDelay = 0;
		public static int spawnNPC = 0;
		public static int maxRoomTiles = 1900;
		public static int numRoomTiles;
		public static int[] roomX = new int[WorldGen.maxRoomTiles];
		public static int[] roomY = new int[WorldGen.maxRoomTiles];
		public static int roomX1;
		public static int roomX2;
		public static int roomY1;
		public static int roomY2;
		public static bool canSpawn;
		public static bool[] houseTile = new bool[107];
		public static int bestX = 0;
		public static int bestY = 0;
		public static int hiScore = 0;
		public static int dungeonX;
		public static int dungeonY;
		public static Vector2 lastDungeonHall = default(Vector2);
		public static int maxDRooms = 100;
		public static int numDRooms = 0;
		public static int[] dRoomX = new int[WorldGen.maxDRooms];
		public static int[] dRoomY = new int[WorldGen.maxDRooms];
		public static int[] dRoomSize = new int[WorldGen.maxDRooms];
		private static bool[] dRoomTreasure = new bool[WorldGen.maxDRooms];
		private static int[] dRoomL = new int[WorldGen.maxDRooms];
		private static int[] dRoomR = new int[WorldGen.maxDRooms];
		private static int[] dRoomT = new int[WorldGen.maxDRooms];
		private static int[] dRoomB = new int[WorldGen.maxDRooms];
		private static int numDDoors;
		private static int[] DDoorX = new int[300];
		private static int[] DDoorY = new int[300];
		private static int[] DDoorPos = new int[300];
		private static int numDPlats;
		private static int[] DPlatX = new int[300];
		private static int[] DPlatY = new int[300];
		private static int[] JChestX = new int[100];
		private static int[] JChestY = new int[100];
		private static int numJChests = 0;
		public static int dEnteranceX = 0;
		public static bool dSurface = false;
		private static double dxStrength1;
		private static double dyStrength1;
		private static double dxStrength2;
		private static double dyStrength2;
		private static int dMinX;
		private static int dMaxX;
		private static int dMinY;
		private static int dMaxY;
		private static int numIslandHouses = 0;
		private static int houseCount = 0;
		private static int[] fihX = new int[300];
		private static int[] fihY = new int[300];
		private static int numMCaves = 0;
		private static int[] mCaveX = new int[300];
		private static int[] mCaveY = new int[300];
		private static int JungleX = 0;
		private static int hellChest = 0;
		public static void SpawnNPC(int x, int y)
		{
			if (Main.wallHouse[(int)Main.tile[x, y].wall])
			{
				WorldGen.canSpawn = true;
			}
			if (!WorldGen.canSpawn)
			{
				return;
			}
			if (!WorldGen.StartRoomCheck(x, y))
			{
				return;
			}
			if (!WorldGen.RoomNeeds(WorldGen.spawnNPC))
			{
				return;
			}
			WorldGen.ScoreRoom(-1);
			if (WorldGen.hiScore > 0)
			{
				int num = -1;
				for (int i = 0; i < 1000; i++)
				{
					if (Main.npc[i].active && Main.npc[i].homeless && Main.npc[i].type == WorldGen.spawnNPC)
					{
						num = i;
						break;
					}
				}
				if (num == -1)
				{
					int num2 = WorldGen.bestX;
					int num3 = WorldGen.bestY;
					bool flag = false;
					if (!flag)
					{
						flag = true;
						Rectangle value = new Rectangle(num2 * 16 + 8 - NPC.sWidth / 2 - NPC.safeRangeX, num3 * 16 + 8 - NPC.sHeight / 2 - NPC.safeRangeY, NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
						for (int j = 0; j < 255; j++)
						{
							if (Main.player[j].active)
							{
								Rectangle rectangle = new Rectangle((int)Main.player[j].position.X, (int)Main.player[j].position.Y, Main.player[j].width, Main.player[j].height);
								if (rectangle.Intersects(value))
								{
									flag = false;
									break;
								}
							}
						}
					}
					if (!flag)
					{
						for (int k = 1; k < 500; k++)
						{
							for (int l = 0; l < 2; l++)
							{
								if (l == 0)
								{
									num2 = WorldGen.bestX + k;
								}
								else
								{
									num2 = WorldGen.bestX - k;
								}
								if (num2 > 10 && num2 < Main.maxTilesX - 10)
								{
									int num4 = WorldGen.bestY - k;
									double num5 = (double)(WorldGen.bestY + k);
									if (num4 < 10)
									{
										num4 = 10;
									}
									if (num5 > Main.worldSurface)
									{
										num5 = Main.worldSurface;
									}
									int num6 = num4;
									while ((double)num6 < num5)
									{
										num3 = num6;
										if (Main.tile[num2, num3].active && Main.tileSolid[(int)Main.tile[num2, num3].type])
										{
											if (!Collision.SolidTiles(num2 - 1, num2 + 1, num3 - 3, num3 - 1))
											{
												flag = true;
												Rectangle value2 = new Rectangle(num2 * 16 + 8 - NPC.sWidth / 2 - NPC.safeRangeX, num3 * 16 + 8 - NPC.sHeight / 2 - NPC.safeRangeY, NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
												for (int m = 0; m < 255; m++)
												{
													if (Main.player[m].active)
													{
														Rectangle rectangle2 = new Rectangle((int)Main.player[m].position.X, (int)Main.player[m].position.Y, Main.player[m].width, Main.player[m].height);
														if (rectangle2.Intersects(value2))
														{
															flag = false;
															break;
														}
													}
												}
												break;
											}
											break;
										}
										else
										{
											num6++;
										}
									}
								}
								if (flag)
								{
									break;
								}
							}
							if (flag)
							{
								break;
							}
						}
					}
					int num7 = NPC.NewNPC(num2 * 16, num3 * 16, WorldGen.spawnNPC, 1);
					Main.npc[num7].homeTileX = WorldGen.bestX;
					Main.npc[num7].homeTileY = WorldGen.bestY;
					if (num2 < WorldGen.bestX)
					{
						Main.npc[num7].direction = 1;
					}
					else
					{
						if (num2 > WorldGen.bestX)
						{
							Main.npc[num7].direction = -1;
						}
					}
					Main.npc[num7].netUpdate = true;
					if (Main.netMode == 0)
					{
						Main.NewText(Main.npc[num7].name + " has arrived!", 50, 125, 255);
					}
					else
					{
						if (Main.netMode == 2)
						{
							NetMessage.SendData(25, -1, -1, Main.npc[num7].name + " has arrived!", 255, 50f, 125f, 255f, 0);
						}
					}
				}
				else
				{
					WorldGen.spawnNPC = 0;
					Main.npc[num].homeTileX = WorldGen.bestX;
					Main.npc[num].homeTileY = WorldGen.bestY;
					Main.npc[num].homeless = false;
				}
				WorldGen.spawnNPC = 0;
			}
		}
		public static bool RoomNeeds(int npcType)
		{
			if ((WorldGen.houseTile[15] || WorldGen.houseTile[79] || WorldGen.houseTile[89] || WorldGen.houseTile[102]) && (WorldGen.houseTile[14] || WorldGen.houseTile[18] || WorldGen.houseTile[87] || WorldGen.houseTile[88] || WorldGen.houseTile[90] || WorldGen.houseTile[101]) && (WorldGen.houseTile[4] || WorldGen.houseTile[33] || WorldGen.houseTile[34] || WorldGen.houseTile[35] || WorldGen.houseTile[36] || WorldGen.houseTile[42] || WorldGen.houseTile[49] || WorldGen.houseTile[93] || WorldGen.houseTile[95] || WorldGen.houseTile[98] || WorldGen.houseTile[100]) && (WorldGen.houseTile[10] || WorldGen.houseTile[11] || WorldGen.houseTile[19]))
			{
				WorldGen.canSpawn = true;
			}
			else
			{
				WorldGen.canSpawn = false;
			}
			return WorldGen.canSpawn;
		}
		public static void QuickFindHome(int npc)
		{
			if (Main.npc[npc].homeTileX > 10 && Main.npc[npc].homeTileY > 10 && Main.npc[npc].homeTileX < Main.maxTilesX - 10 && Main.npc[npc].homeTileY < Main.maxTilesY)
			{
				WorldGen.canSpawn = false;
				WorldGen.StartRoomCheck(Main.npc[npc].homeTileX, Main.npc[npc].homeTileY - 1);
				if (!WorldGen.canSpawn)
				{
					for (int i = Main.npc[npc].homeTileX - 1; i < Main.npc[npc].homeTileX + 2; i++)
					{
						int num = Main.npc[npc].homeTileY - 1;
						while (num < Main.npc[npc].homeTileY + 2 && !WorldGen.StartRoomCheck(i, num))
						{
							num++;
						}
					}
				}
				if (!WorldGen.canSpawn)
				{
					int num2 = 10;
					for (int j = Main.npc[npc].homeTileX - num2; j <= Main.npc[npc].homeTileX + num2; j += 2)
					{
						int num3 = Main.npc[npc].homeTileY - num2;
						while (num3 <= Main.npc[npc].homeTileY + num2 && !WorldGen.StartRoomCheck(j, num3))
						{
							num3 += 2;
						}
					}
				}
				if (WorldGen.canSpawn)
				{
					WorldGen.RoomNeeds(Main.npc[npc].type);
					if (WorldGen.canSpawn)
					{
						WorldGen.ScoreRoom(npc);
					}
					if (WorldGen.canSpawn && WorldGen.hiScore > 0)
					{
						Main.npc[npc].homeTileX = WorldGen.bestX;
						Main.npc[npc].homeTileY = WorldGen.bestY;
						Main.npc[npc].homeless = false;
						WorldGen.canSpawn = false;
						return;
					}
					Main.npc[npc].homeless = true;
					return;
				}
				else
				{
					Main.npc[npc].homeless = true;
				}
			}
		}
		public static void ScoreRoom(int ignoreNPC = -1)
		{
			for (int i = 0; i < 1000; i++)
			{
				if (Main.npc[i].active && Main.npc[i].townNPC && ignoreNPC != i && !Main.npc[i].homeless)
				{
					for (int j = 0; j < WorldGen.numRoomTiles; j++)
					{
						if (Main.npc[i].homeTileX == WorldGen.roomX[j] && Main.npc[i].homeTileY == WorldGen.roomY[j])
						{
							bool flag = false;
							for (int k = 0; k < WorldGen.numRoomTiles; k++)
							{
								if (Main.npc[i].homeTileX == WorldGen.roomX[k] && Main.npc[i].homeTileY - 1 == WorldGen.roomY[k])
								{
									flag = true;
									break;
								}
							}
							if (flag)
							{
								WorldGen.hiScore = -1;
								return;
							}
						}
					}
				}
			}
			WorldGen.hiScore = 0;
			int num = 0;
			int num2 = 0;
			int num3 = WorldGen.roomX1 - NPC.sWidth / 2 / 16 - 1 - 21;
			int num4 = WorldGen.roomX2 + NPC.sWidth / 2 / 16 + 1 + 21;
			int num5 = WorldGen.roomY1 - NPC.sHeight / 2 / 16 - 1 - 21;
			int num6 = WorldGen.roomY2 + NPC.sHeight / 2 / 16 + 1 + 21;
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num4 >= Main.maxTilesX)
			{
				num4 = Main.maxTilesX - 1;
			}
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num6 > Main.maxTilesX)
			{
				num6 = Main.maxTilesX;
			}
			for (int l = num3 + 1; l < num4; l++)
			{
				for (int m = num5 + 2; m < num6 + 2; m++)
				{
					if (Main.tile[l, m].active)
					{
						if (Main.tile[l, m].type == 23 || Main.tile[l, m].type == 24 || Main.tile[l, m].type == 25 || Main.tile[l, m].type == 32)
						{
							Main.evilTiles++;
						}
						else
						{
							if (Main.tile[l, m].type == 27)
							{
								Main.evilTiles -= 5;
							}
						}
					}
				}
			}
			if (num2 < 50)
			{
				num2 = 0;
			}
			int num7 = -num2;
			if (num7 <= -250)
			{
				WorldGen.hiScore = num7;
				return;
			}
			num3 = WorldGen.roomX1;
			num4 = WorldGen.roomX2;
			num5 = WorldGen.roomY1;
			num6 = WorldGen.roomY2;
			for (int n = num3 + 1; n < num4; n++)
			{
				for (int num8 = num5 + 2; num8 < num6 + 2; num8++)
				{
					if (Main.tile[n, num8].active)
					{
						num = num7;
						if (Main.tileSolid[(int)Main.tile[n, num8].type] && !Main.tileSolidTop[(int)Main.tile[n, num8].type] && !Collision.SolidTiles(n - 1, n + 1, num8 - 3, num8 - 1) && Main.tile[n - 1, num8].active && Main.tileSolid[(int)Main.tile[n - 1, num8].type] && Main.tile[n + 1, num8].active && Main.tileSolid[(int)Main.tile[n + 1, num8].type])
						{
							for (int num9 = n - 2; num9 < n + 3; num9++)
							{
								for (int num10 = num8 - 4; num10 < num8; num10++)
								{
									if (Main.tile[num9, num10].active)
									{
										if (num9 == n)
										{
											num -= 15;
										}
										else
										{
											if (Main.tile[num9, num10].type == 10 || Main.tile[num9, num10].type == 11)
											{
												num -= 20;
											}
											else
											{
												if (Main.tileSolid[(int)Main.tile[num9, num10].type])
												{
													num -= 5;
												}
												else
												{
													num += 5;
												}
											}
										}
									}
								}
							}
							if (num > WorldGen.hiScore)
							{
								bool flag2 = false;
								for (int num11 = 0; num11 < WorldGen.numRoomTiles; num11++)
								{
									if (WorldGen.roomX[num11] == n && WorldGen.roomY[num11] == num8)
									{
										flag2 = true;
										break;
									}
								}
								if (flag2)
								{
									WorldGen.hiScore = num;
									WorldGen.bestX = n;
									WorldGen.bestY = num8;
								}
							}
						}
					}
				}
			}
		}
		public static bool StartRoomCheck(int x, int y)
		{
			WorldGen.roomX1 = x;
			WorldGen.roomX2 = x;
			WorldGen.roomY1 = y;
			WorldGen.roomY2 = y;
			WorldGen.numRoomTiles = 0;
			for (int i = 0; i < 107; i++)
			{
				WorldGen.houseTile[i] = false;
			}
			WorldGen.canSpawn = true;
			if (Main.tile[x, y].active && Main.tileSolid[(int)Main.tile[x, y].type])
			{
				WorldGen.canSpawn = false;
			}
			WorldGen.CheckRoom(x, y);
			if (WorldGen.numRoomTiles < 60)
			{
				WorldGen.canSpawn = false;
			}
			return WorldGen.canSpawn;
		}
		public static void CheckRoom(int x, int y)
		{
			if (!WorldGen.canSpawn)
			{
				return;
			}
			if (x < 10 || y < 10 || x >= Main.maxTilesX - 10 || y >= WorldGen.lastMaxTilesY - 10)
			{
				WorldGen.canSpawn = false;
				return;
			}
			for (int i = 0; i < WorldGen.numRoomTiles; i++)
			{
				if (WorldGen.roomX[i] == x && WorldGen.roomY[i] == y)
				{
					return;
				}
			}
			WorldGen.roomX[WorldGen.numRoomTiles] = x;
			WorldGen.roomY[WorldGen.numRoomTiles] = y;
			WorldGen.numRoomTiles++;
			if (WorldGen.numRoomTiles >= WorldGen.maxRoomTiles)
			{
				WorldGen.canSpawn = false;
				return;
			}
			if (Main.tile[x, y].active)
			{
				WorldGen.houseTile[(int)Main.tile[x, y].type] = true;
				if (Main.tileSolid[(int)Main.tile[x, y].type] || Main.tile[x, y].type == 11)
				{
					return;
				}
			}
			if (x < WorldGen.roomX1)
			{
				WorldGen.roomX1 = x;
			}
			if (x > WorldGen.roomX2)
			{
				WorldGen.roomX2 = x;
			}
			if (y < WorldGen.roomY1)
			{
				WorldGen.roomY1 = y;
			}
			if (y > WorldGen.roomY2)
			{
				WorldGen.roomY2 = y;
			}
			bool flag = false;
			bool flag2 = false;
			for (int j = -2; j < 3; j++)
			{
				if (Main.wallHouse[(int)Main.tile[x + j, y].wall])
				{
					flag = true;
				}
				if (Main.tile[x + j, y].active && (Main.tileSolid[(int)Main.tile[x + j, y].type] || Main.tile[x + j, y].type == 11))
				{
					flag = true;
				}
				if (Main.wallHouse[(int)Main.tile[x, y + j].wall])
				{
					flag2 = true;
				}
				if (Main.tile[x, y + j].active && (Main.tileSolid[(int)Main.tile[x, y + j].type] || Main.tile[x, y + j].type == 11))
				{
					flag2 = true;
				}
			}
			if (!flag || !flag2)
			{
				WorldGen.canSpawn = false;
				return;
			}
			for (int k = x - 1; k < x + 2; k++)
			{
				for (int l = y - 1; l < y + 2; l++)
				{
					if ((k != x || l != y) && WorldGen.canSpawn)
					{
						WorldGen.CheckRoom(k, l);
					}
				}
			}
		}
		public static void dropMeteor()
		{
			bool flag = true;
			int num = 0;
			if (Main.netMode == 1)
			{
				return;
			}
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					flag = false;
					break;
				}
			}
			int num2 = 0;
			float num3 = (float)(Main.maxTilesX / 4200);
			int num4 = (int)(400f * num3);
			for (int j = 5; j < Main.maxTilesX - 5; j++)
			{
				int num5 = 5;
				while ((double)num5 < Main.worldSurface)
				{
					if (Main.tile[j, num5].active && Main.tile[j, num5].type == 37)
					{
						num2++;
						if (num2 > num4)
						{
							return;
						}
					}
					num5++;
				}
			}
			while (!flag)
			{
				float num6 = (float)Main.maxTilesX * 0.08f;
				int num7 = Main.rand.Next(50, Main.maxTilesX - 50);
				while ((float)num7 > (float)Main.spawnTileX - num6 && (float)num7 < (float)Main.spawnTileX + num6)
				{
					num7 = Main.rand.Next(50, Main.maxTilesX - 50);
				}
				for (int k = Main.rand.Next(100); k < Main.maxTilesY; k++)
				{
					if (Main.tile[num7, k].active && Main.tileSolid[(int)Main.tile[num7, k].type])
					{
						flag = WorldGen.meteor(num7, k);
						break;
					}
				}
				num++;
				if (num >= 100)
				{
					return;
				}
			}
		}
		public static bool meteor(int i, int j)
		{
			if (i < 50 || i > Main.maxTilesX - 50)
			{
				return false;
			}
			if (j < 50 || j > Main.maxTilesY - 50)
			{
				return false;
			}
			int num = 25;
			Rectangle rectangle = new Rectangle((i - num) * 16, (j - num) * 16, num * 2 * 16, num * 2 * 16);
			for (int k = 0; k < 255; k++)
			{
				if (Main.player[k].active)
				{
					Rectangle value = new Rectangle((int)(Main.player[k].position.X + (float)(Main.player[k].width / 2) - (float)(NPC.sWidth / 2) - (float)NPC.safeRangeX), (int)(Main.player[k].position.Y + (float)(Main.player[k].height / 2) - (float)(NPC.sHeight / 2) - (float)NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
					if (rectangle.Intersects(value))
					{
						return false;
					}
				}
			}
			for (int l = 0; l < 1000; l++)
			{
				if (Main.npc[l].active)
				{
					Rectangle value2 = new Rectangle((int)Main.npc[l].position.X, (int)Main.npc[l].position.Y, Main.npc[l].width, Main.npc[l].height);
					if (rectangle.Intersects(value2))
					{
						return false;
					}
				}
			}
			for (int m = i - num; m < i + num; m++)
			{
				for (int n = j - num; n < j + num; n++)
				{
					if (Main.tile[m, n].active && Main.tile[m, n].type == 21)
					{
						return false;
					}
				}
			}
			WorldGen.stopDrops = true;
			num = 15;
			for (int num2 = i - num; num2 < i + num; num2++)
			{
				for (int num3 = j - num; num3 < j + num; num3++)
				{
					if (num3 > j + Main.rand.Next(-2, 3) - 5 && (double)(Math.Abs(i - num2) + Math.Abs(j - num3)) < (double)num * 1.5 + (double)Main.rand.Next(-5, 5))
					{
						if (!Main.tileSolid[(int)Main.tile[num2, num3].type])
						{
							Main.tile[num2, num3].active = false;
						}
						Main.tile[num2, num3].type = 37;
					}
				}
			}
			num = 10;
			for (int num4 = i - num; num4 < i + num; num4++)
			{
				for (int num5 = j - num; num5 < j + num; num5++)
				{
					if (num5 > j + Main.rand.Next(-2, 3) - 5 && Math.Abs(i - num4) + Math.Abs(j - num5) < num + Main.rand.Next(-3, 4))
					{
						Main.tile[num4, num5].active = false;
					}
				}
			}
			num = 16;
			for (int num6 = i - num; num6 < i + num; num6++)
			{
				for (int num7 = j - num; num7 < j + num; num7++)
				{
					if (Main.tile[num6, num7].type == 5 || Main.tile[num6, num7].type == 32)
					{
						WorldGen.KillTile(num6, num7, false, false, false);
					}
					WorldGen.SquareTileFrame(num6, num7, true);
					WorldGen.SquareWallFrame(num6, num7, true);
				}
			}
			num = 23;
			for (int num8 = i - num; num8 < i + num; num8++)
			{
				for (int num9 = j - num; num9 < j + num; num9++)
				{
					if (Main.tile[num8, num9].active && Main.rand.Next(10) == 0 && (double)(Math.Abs(i - num8) + Math.Abs(j - num9)) < (double)num * 1.3)
					{
						if (Main.tile[num8, num9].type == 5 || Main.tile[num8, num9].type == 32)
						{
							WorldGen.KillTile(num8, num9, false, false, false);
						}
						Main.tile[num8, num9].type = 37;
						WorldGen.SquareTileFrame(num8, num9, true);
					}
				}
			}
			WorldGen.stopDrops = false;
			if (Main.netMode == 0)
			{
				Main.NewText("A meteorite has landed!", 50, 255, 130);
			}
			else
			{
				if (Main.netMode == 2)
				{
					NetMessage.SendData(25, -1, -1, "A meteorite has landed!", 255, 50f, 255f, 130f, 0);
				}
			}
			if (Main.netMode != 1)
			{
				NetMessage.SendTileSquare(-1, i, j, 30);
			}
			return true;
		}
		public static void setWorldSize()
		{
			Main.bottomWorld = (float)(Main.maxTilesY * 16);
			Main.rightWorld = (float)(Main.maxTilesX * 16);
			Main.maxSectionsX = Main.maxTilesX / 200;
			Main.maxSectionsY = Main.maxTilesY / 150;
		}
		public static void worldGenCallBack(object threadContext)
		{
			Main.PlaySound(10, -1, -1, 1);
			WorldGen.clearWorld();
			WorldGen.generateWorld(-1);
			WorldGen.saveWorld(true);
			Main.LoadWorlds();
			if (Main.menuMode == 10)
			{
				Main.menuMode = 6;
			}
			Main.PlaySound(10, -1, -1, 1);
		}
		public static void CreateNewWorld()
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(WorldGen.worldGenCallBack), 1);
		}
		public static void SaveAndQuitCallBack(object threadContext)
		{
			Main.menuMode = 10;
			Main.gameMenu = true;
			Player.SavePlayer(Main.player[Main.myPlayer], Main.playerPathName);
			if (Main.netMode == 0)
			{
				WorldGen.saveWorld(false);
				Main.PlaySound(10, -1, -1, 1);
			}
			else
			{
				Netplay.disconnect = true;
				Main.netMode = 0;
			}
			Main.menuMode = 0;
		}
		public static void SaveAndQuit()
		{
			Main.PlaySound(11, -1, -1, 1);
			ThreadPool.QueueUserWorkItem(new WaitCallback(WorldGen.SaveAndQuitCallBack), 1);
		}
		public static void playWorldCallBack(object threadContext)
		{
			if (Main.rand == null)
			{
				Main.rand = new Random((int)DateTime.Now.Ticks);
			}
			for (int i = 0; i < 255; i++)
			{
				if (i != Main.myPlayer)
				{
					Main.player[i].active = false;
				}
			}
			WorldGen.loadWorld();
			if (WorldGen.loadFailed || !WorldGen.loadSuccess)
			{
				WorldGen.loadWorld();
				if (WorldGen.loadFailed || !WorldGen.loadSuccess)
				{
					if (File.Exists(Main.worldPathName + ".bak"))
					{
						WorldGen.worldBackup = true;
					}
					else
					{
						WorldGen.worldBackup = false;
					}
					if (!Main.dedServ)
					{
						if (WorldGen.worldBackup)
						{
							Main.menuMode = 200;
							return;
						}
						Main.menuMode = 201;
						return;
					}
					else
					{
						if (!WorldGen.worldBackup)
						{
							Console.WriteLine("Load failed!  No backup found.");
							return;
						}
						File.Copy(Main.worldPathName + ".bak", Main.worldPathName, true);
						File.Delete(Main.worldPathName + ".bak");
						WorldGen.loadWorld();
						if (WorldGen.loadFailed || !WorldGen.loadSuccess)
						{
							WorldGen.loadWorld();
							if (WorldGen.loadFailed || !WorldGen.loadSuccess)
							{
								Console.WriteLine("Load failed!");
								return;
							}
						}
					}
				}
			}
			WorldGen.EveryTileFrame();
			if (Main.gameMenu)
			{
				Main.gameMenu = false;
			}
			Main.player[Main.myPlayer].Spawn();
			Main.player[Main.myPlayer].UpdatePlayer(Main.myPlayer);
			Main.dayTime = WorldGen.tempDayTime;
			Main.time = WorldGen.tempTime;
			Main.moonPhase = WorldGen.tempMoonPhase;
			Main.bloodMoon = WorldGen.tempBloodMoon;
			Main.PlaySound(11, -1, -1, 1);
			Main.resetClouds = true;
		}
		public static void playWorld()
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(WorldGen.playWorldCallBack), 1);
		}
		public static void saveAndPlayCallBack(object threadContext)
		{
			WorldGen.saveWorld(false);
		}
		public static void saveAndPlay()
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(WorldGen.saveAndPlayCallBack), 1);
		}
		public static void saveToonWhilePlayingCallBack(object threadContext)
		{
			Player.SavePlayer(Main.player[Main.myPlayer], Main.playerPathName);
		}
		public static void saveToonWhilePlaying()
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(WorldGen.saveToonWhilePlayingCallBack), 1);
		}
		public static void serverLoadWorldCallBack(object threadContext)
		{
			WorldGen.loadWorld();
			if (WorldGen.loadFailed || !WorldGen.loadSuccess)
			{
				WorldGen.loadWorld();
				if (WorldGen.loadFailed || !WorldGen.loadSuccess)
				{
					if (File.Exists(Main.worldPathName + ".bak"))
					{
						WorldGen.worldBackup = true;
					}
					else
					{
						WorldGen.worldBackup = false;
					}
					if (!Main.dedServ)
					{
						if (WorldGen.worldBackup)
						{
							Main.menuMode = 200;
							return;
						}
						Main.menuMode = 201;
						return;
					}
					else
					{
						if (!WorldGen.worldBackup)
						{
							Console.WriteLine("Load failed!  No backup found.");
							return;
						}
						File.Copy(Main.worldPathName + ".bak", Main.worldPathName, true);
						File.Delete(Main.worldPathName + ".bak");
						WorldGen.loadWorld();
						if (WorldGen.loadFailed || !WorldGen.loadSuccess)
						{
							WorldGen.loadWorld();
							if (WorldGen.loadFailed || !WorldGen.loadSuccess)
							{
								Console.WriteLine("Load failed!");
								return;
							}
						}
					}
				}
			}
			Main.PlaySound(10, -1, -1, 1);
			Netplay.StartServer();
			Main.dayTime = WorldGen.tempDayTime;
			Main.time = WorldGen.tempTime;
			Main.moonPhase = WorldGen.tempMoonPhase;
			Main.bloodMoon = WorldGen.tempBloodMoon;
		}
		public static void serverLoadWorld()
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(WorldGen.serverLoadWorldCallBack), 1);
		}
		public static void clearWorld()
		{
			Main.trashItem = new Item();
			WorldGen.spawnEye = false;
			WorldGen.spawnNPC = 0;
			WorldGen.shadowOrbCount = 0;
			Main.helpText = 0;
			Main.dungeonX = 0;
			Main.dungeonY = 0;
			NPC.downedBoss1 = false;
			NPC.downedBoss2 = false;
			NPC.downedBoss3 = false;
			WorldGen.shadowOrbSmashed = false;
			WorldGen.spawnMeteor = false;
			WorldGen.stopDrops = false;
			Main.invasionDelay = 0;
			Main.invasionType = 0;
			Main.invasionSize = 0;
			Main.invasionWarn = 0;
			Main.invasionX = 0.0;
			WorldGen.noLiquidCheck = false;
			Liquid.numLiquid = 0;
			LiquidBuffer.numLiquidBuffer = 0;
			if (Main.netMode == 1 || WorldGen.lastMaxTilesX > Main.maxTilesX || WorldGen.lastMaxTilesY > Main.maxTilesY)
			{
				for (int i = 0; i < WorldGen.lastMaxTilesX; i++)
				{
					float num = (float)i / (float)WorldGen.lastMaxTilesX;
					Main.statusText = "Freeing unused resources: " + (int)(num * 100f + 1f) + "%";
					for (int j = 0; j < WorldGen.lastMaxTilesY; j++)
					{
						Main.tile[i, j] = null;
					}
				}
			}
			WorldGen.lastMaxTilesX = Main.maxTilesX;
			WorldGen.lastMaxTilesY = Main.maxTilesY;
			if (Main.netMode != 1)
			{
				for (int k = 0; k < Main.maxTilesX; k++)
				{
					float num2 = (float)k / (float)Main.maxTilesX;
					Main.statusText = "Resetting game objects: " + (int)(num2 * 100f + 1f) + "%";
					for (int l = 0; l < Main.maxTilesY; l++)
					{
						Main.tile[k, l] = new Tile();
					}
				}
			}
			for (int m = 0; m < 1000; m++)
			{
				Main.dust[m] = new Dust();
			}
			for (int n = 0; n < 200; n++)
			{
				Main.gore[n] = new Gore();
			}
			for (int num3 = 0; num3 < 200; num3++)
			{
				Main.item[num3] = new Item();
			}
			for (int num4 = 0; num4 < 1000; num4++)
			{
				Main.npc[num4] = new NPC();
			}
			for (int num5 = 0; num5 < 1000; num5++)
			{
				Main.projectile[num5] = new Projectile();
			}
			for (int num6 = 0; num6 < 1000; num6++)
			{
				Main.chest[num6] = null;
			}
			for (int num7 = 0; num7 < 1000; num7++)
			{
				Main.sign[num7] = null;
			}
			for (int num8 = 0; num8 < Liquid.resLiquid; num8++)
			{
				Main.liquid[num8] = new Liquid();
			}
			for (int num9 = 0; num9 < 10000; num9++)
			{
				Main.liquidBuffer[num9] = new LiquidBuffer();
			}
			WorldGen.setWorldSize();
			WorldGen.worldCleared = true;
		}
		public static void saveWorld(bool resetTime = false)
		{
			if (Main.worldName == "")
			{
				Main.worldName = "World";
			}
			if (WorldGen.saveLock)
			{
				return;
			}
			WorldGen.saveLock = true;
			lock (WorldGen.padlock)
			{
				try
				{
					Directory.CreateDirectory(Main.WorldPath);
				}
				catch
				{
				}
				if (!Main.skipMenu)
				{
					bool value = Main.dayTime;
					WorldGen.tempTime = Main.time;
					WorldGen.tempMoonPhase = Main.moonPhase;
					WorldGen.tempBloodMoon = Main.bloodMoon;
					if (resetTime)
					{
						value = true;
						WorldGen.tempTime = 13500.0;
						WorldGen.tempMoonPhase = 0;
						WorldGen.tempBloodMoon = false;
					}
					if (Main.worldPathName != null)
					{
						Stopwatch stopwatch = new Stopwatch();
						stopwatch.Start();
						string text = Main.worldPathName + ".sav";
						using (FileStream fileStream = new FileStream(text, FileMode.Create))
						{
							using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
							{
								binaryWriter.Write(Main.curRelease);
								binaryWriter.Write(Main.worldName);
								binaryWriter.Write(Main.worldID);
								binaryWriter.Write((int)Main.leftWorld);
								binaryWriter.Write((int)Main.rightWorld);
								binaryWriter.Write((int)Main.topWorld);
								binaryWriter.Write((int)Main.bottomWorld);
								binaryWriter.Write(Main.maxTilesY);
								binaryWriter.Write(Main.maxTilesX);
								binaryWriter.Write(Main.spawnTileX);
								binaryWriter.Write(Main.spawnTileY);
								binaryWriter.Write(Main.worldSurface);
								binaryWriter.Write(Main.rockLayer);
								binaryWriter.Write(WorldGen.tempTime);
								binaryWriter.Write(value);
								binaryWriter.Write(WorldGen.tempMoonPhase);
								binaryWriter.Write(WorldGen.tempBloodMoon);
								binaryWriter.Write(Main.dungeonX);
								binaryWriter.Write(Main.dungeonY);
								binaryWriter.Write(NPC.downedBoss1);
								binaryWriter.Write(NPC.downedBoss2);
								binaryWriter.Write(NPC.downedBoss3);
								binaryWriter.Write(WorldGen.shadowOrbSmashed);
								binaryWriter.Write(WorldGen.spawnMeteor);
								binaryWriter.Write((byte)WorldGen.shadowOrbCount);
								binaryWriter.Write(Main.invasionDelay);
								binaryWriter.Write(Main.invasionSize);
								binaryWriter.Write(Main.invasionType);
								binaryWriter.Write(Main.invasionX);
								for (int i = 0; i < Main.maxTilesX; i++)
								{
									float num = (float)i / (float)Main.maxTilesX;
									Main.statusText = "Saving world data: " + (int)(num * 100f + 1f) + "%";
									for (int j = 0; j < Main.maxTilesY; j++)
									{
										Tile tile = (Tile)Main.tile[i, j].Clone();
										binaryWriter.Write(tile.active);
										if (tile.active)
										{
											binaryWriter.Write(tile.type);
											if (Main.tileFrameImportant[(int)tile.type])
											{
												binaryWriter.Write(tile.frameX);
												binaryWriter.Write(tile.frameY);
											}
										}
										binaryWriter.Write(tile.lighted);
										if (Main.tile[i, j].wall > 0)
										{
											binaryWriter.Write(true);
											binaryWriter.Write(tile.wall);
										}
										else
										{
											binaryWriter.Write(false);
										}
										if (tile.liquid > 0)
										{
											binaryWriter.Write(true);
											binaryWriter.Write(tile.liquid);
											binaryWriter.Write(tile.lava);
										}
										else
										{
											binaryWriter.Write(false);
										}
									}
								}
								for (int k = 0; k < 1000; k++)
								{
									if (Main.chest[k] == null)
									{
										binaryWriter.Write(false);
									}
									else
									{
										Chest chest = (Chest)Main.chest[k].Clone();
										binaryWriter.Write(true);
										binaryWriter.Write(chest.x);
										binaryWriter.Write(chest.y);
										for (int l = 0; l < Chest.maxItems; l++)
										{
											if (chest.item[l].type == 0)
											{
												chest.item[l].stack = 0;
											}
											binaryWriter.Write((byte)chest.item[l].stack);
											if (chest.item[l].stack > 0)
											{
												binaryWriter.Write(chest.item[l].name);
											}
										}
									}
								}
								for (int m = 0; m < 1000; m++)
								{
									if (Main.sign[m] == null || Main.sign[m].text == null)
									{
										binaryWriter.Write(false);
									}
									else
									{
										Sign sign = (Sign)Main.sign[m].Clone();
										binaryWriter.Write(true);
										binaryWriter.Write(sign.text);
										binaryWriter.Write(sign.x);
										binaryWriter.Write(sign.y);
									}
								}
								for (int n = 0; n < 1000; n++)
								{
									NPC nPC = (NPC)Main.npc[n].Clone();
									if (nPC.active && nPC.townNPC)
									{
										binaryWriter.Write(true);
										binaryWriter.Write(nPC.name);
										binaryWriter.Write(nPC.position.X);
										binaryWriter.Write(nPC.position.Y);
										binaryWriter.Write(nPC.homeless);
										binaryWriter.Write(nPC.homeTileX);
										binaryWriter.Write(nPC.homeTileY);
									}
								}
								binaryWriter.Write(false);
								binaryWriter.Write(true);
								binaryWriter.Write(Main.worldName);
								binaryWriter.Write(Main.worldID);
								binaryWriter.Close();
								fileStream.Close();
								if (File.Exists(Main.worldPathName))
								{
									Main.statusText = "Backing up world file...";
									string destFileName = Main.worldPathName + ".bak";
									File.Copy(Main.worldPathName, destFileName, true);
								}
								File.Copy(text, Main.worldPathName, true);
								File.Delete(text);
							}
						}
						WorldGen.saveLock = false;
					}
				}
			}
		}
		public static void loadWorld()
		{
			if (!File.Exists(Main.worldPathName) && Main.autoGen)
			{
				for (int i = Main.worldPathName.Length - 1; i >= 0; i--)
				{
					if (Main.worldPathName.Substring(i, 1) == "\\")
					{
						string path = Main.worldPathName.Substring(0, i);
						Directory.CreateDirectory(path);
						break;
					}
				}
				WorldGen.clearWorld();
				WorldGen.generateWorld(-1);
				WorldGen.saveWorld(false);
			}
			if (WorldGen.genRand == null)
			{
				WorldGen.genRand = new Random((int)DateTime.Now.Ticks);
			}
			using (FileStream fileStream = new FileStream(Main.worldPathName, FileMode.Open))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					try
					{
						WorldGen.loadFailed = false;
						WorldGen.loadSuccess = false;
						int num = binaryReader.ReadInt32();
						if (num > Main.curRelease)
						{
							WorldGen.loadFailed = true;
							WorldGen.loadSuccess = false;
							try
							{
								binaryReader.Close();
								fileStream.Close();
							}
							catch
							{
							}
						}
						else
						{
							Main.worldName = binaryReader.ReadString();
							Main.worldID = binaryReader.ReadInt32();
							Main.leftWorld = (float)binaryReader.ReadInt32();
							Main.rightWorld = (float)binaryReader.ReadInt32();
							Main.topWorld = (float)binaryReader.ReadInt32();
							Main.bottomWorld = (float)binaryReader.ReadInt32();
							Main.maxTilesY = binaryReader.ReadInt32();
							Main.maxTilesX = binaryReader.ReadInt32();
							WorldGen.clearWorld();
							Main.spawnTileX = binaryReader.ReadInt32();
							Main.spawnTileY = binaryReader.ReadInt32();
							Main.worldSurface = binaryReader.ReadDouble();
							Main.rockLayer = binaryReader.ReadDouble();
							WorldGen.tempTime = binaryReader.ReadDouble();
							WorldGen.tempDayTime = binaryReader.ReadBoolean();
							WorldGen.tempMoonPhase = binaryReader.ReadInt32();
							WorldGen.tempBloodMoon = binaryReader.ReadBoolean();
							Main.dungeonX = binaryReader.ReadInt32();
							Main.dungeonY = binaryReader.ReadInt32();
							NPC.downedBoss1 = binaryReader.ReadBoolean();
							NPC.downedBoss2 = binaryReader.ReadBoolean();
							NPC.downedBoss3 = binaryReader.ReadBoolean();
							WorldGen.shadowOrbSmashed = binaryReader.ReadBoolean();
							WorldGen.spawnMeteor = binaryReader.ReadBoolean();
							WorldGen.shadowOrbCount = (int)binaryReader.ReadByte();
							Main.invasionDelay = binaryReader.ReadInt32();
							Main.invasionSize = binaryReader.ReadInt32();
							Main.invasionType = binaryReader.ReadInt32();
							Main.invasionX = binaryReader.ReadDouble();
							for (int j = 0; j < Main.maxTilesX; j++)
							{
								float num2 = (float)j / (float)Main.maxTilesX;
								Main.statusText = "Loading world data: " + (int)(num2 * 100f + 1f) + "%";
								for (int k = 0; k < Main.maxTilesY; k++)
								{
									Main.tile[j, k].active = binaryReader.ReadBoolean();
									if (Main.tile[j, k].active)
									{
										Main.tile[j, k].type = binaryReader.ReadByte();
										if (Main.tileFrameImportant[(int)Main.tile[j, k].type])
										{
											Main.tile[j, k].frameX = binaryReader.ReadInt16();
											Main.tile[j, k].frameY = binaryReader.ReadInt16();
										}
										else
										{
											Main.tile[j, k].frameX = -1;
											Main.tile[j, k].frameY = -1;
										}
									}
									Main.tile[j, k].lighted = binaryReader.ReadBoolean();
									if (binaryReader.ReadBoolean())
									{
										Main.tile[j, k].wall = binaryReader.ReadByte();
									}
									if (binaryReader.ReadBoolean())
									{
										Main.tile[j, k].liquid = binaryReader.ReadByte();
										Main.tile[j, k].lava = binaryReader.ReadBoolean();
									}
								}
							}
							for (int l = 0; l < 1000; l++)
							{
								if (binaryReader.ReadBoolean())
								{
									Main.chest[l] = new Chest();
									Main.chest[l].x = binaryReader.ReadInt32();
									Main.chest[l].y = binaryReader.ReadInt32();
									for (int m = 0; m < Chest.maxItems; m++)
									{
										Main.chest[l].item[m] = new Item();
										byte b = binaryReader.ReadByte();
										if (b > 0)
										{
											string defaults = Item.VersionName(binaryReader.ReadString(), num);
											Main.chest[l].item[m].SetDefaults(defaults);
											Main.chest[l].item[m].stack = (int)b;
										}
									}
								}
							}
							for (int n = 0; n < 1000; n++)
							{
								if (binaryReader.ReadBoolean())
								{
									string text = binaryReader.ReadString();
									int num3 = binaryReader.ReadInt32();
									int num4 = binaryReader.ReadInt32();
									if (Main.tile[num3, num4].active && (Main.tile[num3, num4].type == 55 || Main.tile[num3, num4].type == 85))
									{
										Main.sign[n] = new Sign();
										Main.sign[n].x = num3;
										Main.sign[n].y = num4;
										Main.sign[n].text = text;
									}
								}
							}
							bool flag = binaryReader.ReadBoolean();
							int num5 = 0;
							while (flag)
							{
								Main.npc[num5].SetDefaults(binaryReader.ReadString());
								Main.npc[num5].position.X = binaryReader.ReadSingle();
								Main.npc[num5].position.Y = binaryReader.ReadSingle();
								Main.npc[num5].homeless = binaryReader.ReadBoolean();
								Main.npc[num5].homeTileX = binaryReader.ReadInt32();
								Main.npc[num5].homeTileY = binaryReader.ReadInt32();
								flag = binaryReader.ReadBoolean();
								num5++;
							}
							if (num >= 7)
							{
								bool flag2 = binaryReader.ReadBoolean();
								string a = binaryReader.ReadString();
								int num6 = binaryReader.ReadInt32();
								if (!flag2 || !(a == Main.worldName) || num6 != Main.worldID)
								{
									WorldGen.loadSuccess = false;
									WorldGen.loadFailed = true;
									binaryReader.Close();
									fileStream.Close();
									return;
								}
								WorldGen.loadSuccess = true;
							}
							else
							{
								WorldGen.loadSuccess = true;
							}
							binaryReader.Close();
							fileStream.Close();
							if (!WorldGen.loadFailed && WorldGen.loadSuccess)
							{
								WorldGen.gen = true;
								WorldGen.waterLine = Main.maxTilesY;
								Liquid.QuickWater(2, -1, -1);
								WorldGen.WaterCheck();
								int num7 = 0;
								Liquid.quickSettle = true;
								int num8 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
								float num9 = 0f;
								while (Liquid.numLiquid > 0 && num7 < 100000)
								{
									num7++;
									float num10 = (float)(num8 - (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer)) / (float)num8;
									if (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer > num8)
									{
										num8 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
									}
									if (num10 > num9)
									{
										num9 = num10;
									}
									else
									{
										num10 = num9;
									}
									Main.statusText = "Settling liquids: " + (int)(num10 * 100f / 2f + 50f) + "%";
									Liquid.UpdateLiquid();
								}
								Liquid.quickSettle = false;
								WorldGen.WaterCheck();
								WorldGen.gen = false;
							}
						}
					}
					catch
					{
						WorldGen.loadFailed = true;
						WorldGen.loadSuccess = false;
						try
						{
							binaryReader.Close();
							fileStream.Close();
						}
						catch
						{
						}
					}
				}
			}
		}
		private static void resetGen()
		{
			WorldGen.mudWall = false;
			WorldGen.hellChest = 0;
			WorldGen.JungleX = 0;
			WorldGen.numMCaves = 0;
			WorldGen.numIslandHouses = 0;
			WorldGen.houseCount = 0;
			WorldGen.dEnteranceX = 0;
			WorldGen.numDRooms = 0;
			WorldGen.numDDoors = 0;
			WorldGen.numDPlats = 0;
			WorldGen.numJChests = 0;
		}
		public static void generateWorld(int seed = -1)
		{
			WorldGen.gen = true;
			WorldGen.resetGen();
			if (seed > 0)
			{
				WorldGen.genRand = new Random(seed);
			}
			else
			{
				WorldGen.genRand = new Random((int)DateTime.Now.Ticks);
			}
			Main.worldID = WorldGen.genRand.Next(2147483647);
			int num = 0;
			int num2 = 0;
			double num3 = (double)Main.maxTilesY * 0.3;
			num3 *= (double)WorldGen.genRand.Next(90, 110) * 0.005;
			double num4 = num3 + (double)Main.maxTilesY * 0.2;
			num4 *= (double)WorldGen.genRand.Next(90, 110) * 0.01;
			double num5 = num3;
			double num6 = num3;
			double num7 = num4;
			double num8 = num4;
			int num9 = 0;
			if (WorldGen.genRand.Next(2) == 0)
			{
				num9 = -1;
			}
			else
			{
				num9 = 1;
			}
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				float num10 = (float)i / (float)Main.maxTilesX;
				Main.statusText = "Generating world terrain: " + (int)(num10 * 100f + 1f) + "%";
				if (num3 < num5)
				{
					num5 = num3;
				}
				if (num3 > num6)
				{
					num6 = num3;
				}
				if (num4 < num7)
				{
					num7 = num4;
				}
				if (num4 > num8)
				{
					num8 = num4;
				}
				if (num2 <= 0)
				{
					num = WorldGen.genRand.Next(0, 5);
					num2 = WorldGen.genRand.Next(5, 40);
					if (num == 0)
					{
						num2 *= (int)((double)WorldGen.genRand.Next(5, 30) * 0.2);
					}
				}
				num2--;
				if (num == 0)
				{
					while (WorldGen.genRand.Next(0, 7) == 0)
					{
						num3 += (double)WorldGen.genRand.Next(-1, 2);
					}
				}
				else
				{
					if (num == 1)
					{
						while (WorldGen.genRand.Next(0, 4) == 0)
						{
							num3 -= 1.0;
						}
						while (WorldGen.genRand.Next(0, 10) == 0)
						{
							num3 += 1.0;
						}
					}
					else
					{
						if (num == 2)
						{
							while (WorldGen.genRand.Next(0, 4) == 0)
							{
								num3 += 1.0;
							}
							while (WorldGen.genRand.Next(0, 10) == 0)
							{
								num3 -= 1.0;
							}
						}
						else
						{
							if (num == 3)
							{
								while (WorldGen.genRand.Next(0, 2) == 0)
								{
									num3 -= 1.0;
								}
								while (WorldGen.genRand.Next(0, 6) == 0)
								{
									num3 += 1.0;
								}
							}
							else
							{
								if (num == 4)
								{
									while (WorldGen.genRand.Next(0, 2) == 0)
									{
										num3 += 1.0;
									}
									while (WorldGen.genRand.Next(0, 5) == 0)
									{
										num3 -= 1.0;
									}
								}
							}
						}
					}
				}
				if (num3 < (double)Main.maxTilesY * 0.15)
				{
					num3 = (double)Main.maxTilesY * 0.15;
					num2 = 0;
				}
				else
				{
					if (num3 > (double)Main.maxTilesY * 0.3)
					{
						num3 = (double)Main.maxTilesY * 0.3;
						num2 = 0;
					}
				}
				if ((i < 275 || i > Main.maxTilesX - 275) && num3 > (double)Main.maxTilesY * 0.25)
				{
					num3 = (double)Main.maxTilesY * 0.25;
					num2 = 1;
				}
				while (WorldGen.genRand.Next(0, 3) == 0)
				{
					num4 += (double)WorldGen.genRand.Next(-2, 3);
				}
				if (num4 < num3 + (double)Main.maxTilesY * 0.05)
				{
					num4 += 1.0;
				}
				if (num4 > num3 + (double)Main.maxTilesY * 0.35)
				{
					num4 -= 1.0;
				}
				int num11 = 0;
				while ((double)num11 < num3)
				{
					Main.tile[i, num11].active = false;
					Main.tile[i, num11].lighted = true;
					Main.tile[i, num11].frameX = -1;
					Main.tile[i, num11].frameY = -1;
					num11++;
				}
				for (int j = (int)num3; j < Main.maxTilesY; j++)
				{
					if ((double)j < num4)
					{
						Main.tile[i, j].active = true;
						Main.tile[i, j].type = 0;
						Main.tile[i, j].frameX = -1;
						Main.tile[i, j].frameY = -1;
					}
					else
					{
						Main.tile[i, j].active = true;
						Main.tile[i, j].type = 1;
						Main.tile[i, j].frameX = -1;
						Main.tile[i, j].frameY = -1;
					}
				}
			}
			Main.worldSurface = num6 + 25.0;
			Main.rockLayer = num8;
			double num12 = (double)((int)((Main.rockLayer - Main.worldSurface) / 6.0) * 6);
			Main.rockLayer = Main.worldSurface + num12;
			WorldGen.waterLine = (int)(Main.rockLayer + (double)Main.maxTilesY) / 2;
			WorldGen.waterLine += WorldGen.genRand.Next(-100, 20);
			WorldGen.lavaLine = WorldGen.waterLine + WorldGen.genRand.Next(50, 80);
			int num13 = 0;
			Main.statusText = "Adding sand...";
			int num14 = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.0008), (int)((double)Main.maxTilesX * 0.0025));
			num14 += 2;
			for (int k = 0; k < num14; k++)
			{
				int num15 = WorldGen.genRand.Next(Main.maxTilesX);
				while ((float)num15 > (float)Main.maxTilesX * 0.4f && (float)num15 < (float)Main.maxTilesX * 0.6f)
				{
					num15 = WorldGen.genRand.Next(Main.maxTilesX);
				}
				int num16 = WorldGen.genRand.Next(35, 90);
				if (k == 1)
				{
					float num17 = (float)(Main.maxTilesX / 4200);
					num16 += (int)((float)WorldGen.genRand.Next(20, 40) * num17);
				}
				if (WorldGen.genRand.Next(3) == 0)
				{
					num16 *= 2;
				}
				if (k == 1)
				{
					num16 *= 2;
				}
				int num18 = num15 - num16;
				num16 = WorldGen.genRand.Next(35, 90);
				if (WorldGen.genRand.Next(3) == 0)
				{
					num16 *= 2;
				}
				if (k == 1)
				{
					num16 *= 2;
				}
				int num19 = num15 + num16;
				if (num18 < 0)
				{
					num18 = 0;
				}
				if (num19 > Main.maxTilesX)
				{
					num19 = Main.maxTilesX;
				}
				if (k == 0)
				{
					num18 = 0;
					num19 = WorldGen.genRand.Next(260, 300);
					if (num9 == 1)
					{
						num19 += 40;
					}
				}
				else
				{
					if (k == 2)
					{
						num18 = Main.maxTilesX - WorldGen.genRand.Next(260, 300);
						num19 = Main.maxTilesX;
						if (num9 == -1)
						{
							num18 -= 40;
						}
					}
				}
				int num20 = WorldGen.genRand.Next(50, 100);
				for (int l = num18; l < num19; l++)
				{
					if (WorldGen.genRand.Next(2) == 0)
					{
						num20 += WorldGen.genRand.Next(-1, 2);
						if (num20 < 50)
						{
							num20 = 50;
						}
						if (num20 > 100)
						{
							num20 = 100;
						}
					}
					int num21 = 0;
					while ((double)num21 < Main.worldSurface)
					{
						if (Main.tile[l, num21].active)
						{
							int num22 = num20;
							if (l - num18 < num22)
							{
								num22 = l - num18;
							}
							if (num19 - l < num22)
							{
								num22 = num19 - l;
							}
							num22 += WorldGen.genRand.Next(5);
							for (int m = num21; m < num21 + num22; m++)
							{
								if (l > num18 + WorldGen.genRand.Next(5) && l < num19 - WorldGen.genRand.Next(5))
								{
									Main.tile[l, m].type = 53;
								}
							}
							break;
						}
						num21++;
					}
				}
			}
			for (int n = 0; n < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 8E-06); n++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.worldSurface, (int)Main.rockLayer), (double)WorldGen.genRand.Next(15, 70), WorldGen.genRand.Next(20, 130), 53, false, 0f, 0f, false, true);
			}
			WorldGen.numMCaves = 0;
			Main.statusText = "Generating hills...";
			for (int num23 = 0; num23 < (int)((double)Main.maxTilesX * 0.0008); num23++)
			{
				int num24 = 0;
				bool flag = false;
				bool flag2 = false;
				int num25 = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.25), (int)((double)Main.maxTilesX * 0.75));
				while (!flag2)
				{
					flag2 = true;
					while (num25 > Main.maxTilesX / 2 - 100 && num25 < Main.maxTilesX / 2 + 100)
					{
						num25 = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.25), (int)((double)Main.maxTilesX * 0.75));
					}
					for (int num26 = 0; num26 < WorldGen.numMCaves; num26++)
					{
						if (num25 > WorldGen.mCaveX[num26] - 50 && num25 < WorldGen.mCaveX[num26] + 50)
						{
							num24++;
							flag2 = false;
							break;
						}
					}
					if (num24 >= 200)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					int num27 = 0;
					while ((double)num27 < Main.worldSurface)
					{
						if (Main.tile[num25, num27].active)
						{
							WorldGen.Mountinater(num25, num27);
							WorldGen.mCaveX[WorldGen.numMCaves] = num25;
							WorldGen.mCaveY[WorldGen.numMCaves] = num27;
							WorldGen.numMCaves++;
							break;
						}
						num27++;
					}
				}
			}
			for (int num28 = 1; num28 < Main.maxTilesX - 1; num28++)
			{
				float num29 = (float)num28 / (float)Main.maxTilesX;
				Main.statusText = "Puttin dirt behind dirt: " + (int)(num29 * 100f + 1f) + "%";
				bool flag3 = false;
				num13 += WorldGen.genRand.Next(-1, 2);
				if (num13 < 0)
				{
					num13 = 0;
				}
				if (num13 > 10)
				{
					num13 = 10;
				}
				int num30 = 0;
				while ((double)num30 < Main.worldSurface + 10.0 && (double)num30 <= Main.worldSurface + (double)num13)
				{
					if (flag3)
					{
						Main.tile[num28, num30].wall = 2;
					}
					if (Main.tile[num28, num30].active && Main.tile[num28 - 1, num30].active && Main.tile[num28 + 1, num30].active && Main.tile[num28, num30 + 1].active && Main.tile[num28 - 1, num30 + 1].active && Main.tile[num28 + 1, num30 + 1].active)
					{
						flag3 = true;
					}
					num30++;
				}
			}
			Main.statusText = "Placing rocks in the dirt...";
			for (int num31 = 0; num31 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0002); num31++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next(0, (int)num5 + 1), (double)WorldGen.genRand.Next(4, 15), WorldGen.genRand.Next(5, 40), 1, false, 0f, 0f, false, true);
			}
			for (int num32 = 0; num32 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0002); num32++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num5, (int)num6 + 1), (double)WorldGen.genRand.Next(4, 10), WorldGen.genRand.Next(5, 30), 1, false, 0f, 0f, false, true);
			}
			for (int num33 = 0; num33 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0045); num33++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num6, (int)num8 + 1), (double)WorldGen.genRand.Next(2, 7), WorldGen.genRand.Next(2, 23), 1, false, 0f, 0f, false, true);
			}
			Main.statusText = "Placing dirt in the rocks...";
			for (int num34 = 0; num34 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.005); num34++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num7, Main.maxTilesY), (double)WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(2, 40), 0, false, 0f, 0f, false, true);
			}
			Main.statusText = "Adding clay...";
			for (int num35 = 0; num35 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 2E-05); num35++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next(0, (int)num5), (double)WorldGen.genRand.Next(4, 14), WorldGen.genRand.Next(10, 50), 40, false, 0f, 0f, false, true);
			}
			for (int num36 = 0; num36 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 5E-05); num36++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num5, (int)num6 + 1), (double)WorldGen.genRand.Next(8, 14), WorldGen.genRand.Next(15, 45), 40, false, 0f, 0f, false, true);
			}
			for (int num37 = 0; num37 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 2E-05); num37++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num6, (int)num8 + 1), (double)WorldGen.genRand.Next(8, 15), WorldGen.genRand.Next(5, 50), 40, false, 0f, 0f, false, true);
			}
			for (int num38 = 5; num38 < Main.maxTilesX - 5; num38++)
			{
				int num39 = 1;
				while ((double)num39 < Main.worldSurface - 1.0)
				{
					if (Main.tile[num38, num39].active)
					{
						for (int num40 = num39; num40 < num39 + 5; num40++)
						{
							if (Main.tile[num38, num40].type == 40)
							{
								Main.tile[num38, num40].type = 0;
							}
						}
						break;
					}
					num39++;
				}
			}
			for (int num41 = 0; num41 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0015); num41++)
			{
				float num42 = (float)((double)num41 / ((double)(Main.maxTilesX * Main.maxTilesY) * 0.0015));
				Main.statusText = "Making random holes: " + (int)(num42 * 100f + 1f) + "%";
				int type = -1;
				if (WorldGen.genRand.Next(5) == 0)
				{
					type = -2;
				}
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num6, Main.maxTilesY), (double)WorldGen.genRand.Next(2, 5), WorldGen.genRand.Next(2, 20), type, false, 0f, 0f, false, true);
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num6, Main.maxTilesY), (double)WorldGen.genRand.Next(8, 15), WorldGen.genRand.Next(7, 30), type, false, 0f, 0f, false, true);
			}
			for (int num43 = 0; num43 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 3E-05); num43++)
			{
				float num44 = (float)((double)num43 / ((double)(Main.maxTilesX * Main.maxTilesY) * 3E-05));
				Main.statusText = "Generating small caves: " + (int)(num44 * 100f + 1f) + "%";
				if (num8 <= (double)Main.maxTilesY)
				{
					int type2 = -1;
					if (WorldGen.genRand.Next(6) == 0)
					{
						type2 = -2;
					}
					WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num5, (int)num8 + 1), (double)WorldGen.genRand.Next(5, 15), WorldGen.genRand.Next(30, 200), type2, false, 0f, 0f, false, true);
				}
			}
			for (int num45 = 0; num45 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.00015); num45++)
			{
				float num46 = (float)((double)num45 / ((double)(Main.maxTilesX * Main.maxTilesY) * 0.00015));
				Main.statusText = "Generating large caves: " + (int)(num46 * 100f + 1f) + "%";
				if (num8 <= (double)Main.maxTilesY)
				{
					int type3 = -1;
					if (WorldGen.genRand.Next(10) == 0)
					{
						type3 = -2;
					}
					WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num8, Main.maxTilesY), (double)WorldGen.genRand.Next(6, 20), WorldGen.genRand.Next(50, 300), type3, false, 0f, 0f, false, true);
				}
			}
			Main.statusText = "Generating surface caves...";
			for (int num47 = 0; num47 < (int)((double)Main.maxTilesX * 0.0025); num47++)
			{
				int num48 = WorldGen.genRand.Next(0, Main.maxTilesX);
				int num49 = 0;
				while ((double)num49 < num6)
				{
					if (Main.tile[num48, num49].active)
					{
						WorldGen.TileRunner(num48, num49, (double)WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(5, 50), -1, false, (float)WorldGen.genRand.Next(-10, 11) * 0.1f, 1f, false, true);
						break;
					}
					num49++;
				}
			}
			for (int num50 = 0; num50 < (int)((double)Main.maxTilesX * 0.0007); num50++)
			{
				int num48 = WorldGen.genRand.Next(0, Main.maxTilesX);
				int num51 = 0;
				while ((double)num51 < num6)
				{
					if (Main.tile[num48, num51].active)
					{
						WorldGen.TileRunner(num48, num51, (double)WorldGen.genRand.Next(10, 15), WorldGen.genRand.Next(50, 130), -1, false, (float)WorldGen.genRand.Next(-10, 11) * 0.1f, 2f, false, true);
						break;
					}
					num51++;
				}
			}
			for (int num52 = 0; num52 < (int)((double)Main.maxTilesX * 0.0003); num52++)
			{
				int num48 = WorldGen.genRand.Next(0, Main.maxTilesX);
				int num53 = 0;
				while ((double)num53 < num6)
				{
					if (Main.tile[num48, num53].active)
					{
						WorldGen.TileRunner(num48, num53, (double)WorldGen.genRand.Next(12, 25), WorldGen.genRand.Next(150, 500), -1, false, (float)WorldGen.genRand.Next(-10, 11) * 0.1f, 4f, false, true);
						WorldGen.TileRunner(num48, num53, (double)WorldGen.genRand.Next(8, 17), WorldGen.genRand.Next(60, 200), -1, false, (float)WorldGen.genRand.Next(-10, 11) * 0.1f, 2f, false, true);
						WorldGen.TileRunner(num48, num53, (double)WorldGen.genRand.Next(5, 13), WorldGen.genRand.Next(40, 170), -1, false, (float)WorldGen.genRand.Next(-10, 11) * 0.1f, 2f, false, true);
						break;
					}
					num53++;
				}
			}
			for (int num54 = 0; num54 < (int)((double)Main.maxTilesX * 0.0004); num54++)
			{
				int num48 = WorldGen.genRand.Next(0, Main.maxTilesX);
				int num55 = 0;
				while ((double)num55 < num6)
				{
					if (Main.tile[num48, num55].active)
					{
						WorldGen.TileRunner(num48, num55, (double)WorldGen.genRand.Next(7, 12), WorldGen.genRand.Next(150, 250), -1, false, 0f, 1f, true, true);
						break;
					}
					num55++;
				}
			}
			for (int num56 = 0; num56 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.002); num56++)
			{
				int num57 = WorldGen.genRand.Next(1, Main.maxTilesX - 1);
				int num58 = WorldGen.genRand.Next((int)num5, (int)num6);
				if (num58 >= Main.maxTilesY)
				{
					num58 = Main.maxTilesY - 2;
				}
				if (Main.tile[num57 - 1, num58].active && Main.tile[num57 - 1, num58].type == 0 && Main.tile[num57 + 1, num58].active && Main.tile[num57 + 1, num58].type == 0 && Main.tile[num57, num58 - 1].active && Main.tile[num57, num58 - 1].type == 0 && Main.tile[num57, num58 + 1].active && Main.tile[num57, num58 + 1].type == 0)
				{
					Main.tile[num57, num58].active = true;
					Main.tile[num57, num58].type = 2;
				}
				num57 = WorldGen.genRand.Next(1, Main.maxTilesX - 1);
				num58 = WorldGen.genRand.Next(0, (int)num5);
				if (num58 >= Main.maxTilesY)
				{
					num58 = Main.maxTilesY - 2;
				}
				if (Main.tile[num57 - 1, num58].active && Main.tile[num57 - 1, num58].type == 0 && Main.tile[num57 + 1, num58].active && Main.tile[num57 + 1, num58].type == 0 && Main.tile[num57, num58 - 1].active && Main.tile[num57, num58 - 1].type == 0 && Main.tile[num57, num58 + 1].active && Main.tile[num57, num58 + 1].type == 0)
				{
					Main.tile[num57, num58].active = true;
					Main.tile[num57, num58].type = 2;
				}
			}
			Main.statusText = "Generating jungle: 0%";
			float num59 = (float)(Main.maxTilesX / 4200);
			num59 *= 1.5f;
			int num60 = 0;
			float num61 = (float)WorldGen.genRand.Next(15, 30) * 0.01f;
			if (num9 == -1)
			{
				num61 = 1f - num61;
				num60 = (int)((float)Main.maxTilesX * num61);
			}
			else
			{
				num60 = (int)((float)Main.maxTilesX * num61);
			}
			int num62 = (int)((double)Main.maxTilesY + Main.rockLayer) / 2;
			num60 += WorldGen.genRand.Next((int)(-100f * num59), (int)(101f * num59));
			num62 += WorldGen.genRand.Next((int)(-100f * num59), (int)(101f * num59));
			int num63 = num60;
			int num64 = num62;
			WorldGen.TileRunner(num60, num62, (double)WorldGen.genRand.Next((int)(250f * num59), (int)(500f * num59)), WorldGen.genRand.Next(50, 150), 59, false, (float)(num9 * 3), 0f, false, true);
			int num65 = 0;
			while ((float)num65 < 6f * num59)
			{
				WorldGen.TileRunner(num60 + WorldGen.genRand.Next(-(int)(125f * num59), (int)(125f * num59)), num62 + WorldGen.genRand.Next(-(int)(125f * num59), (int)(125f * num59)), (double)WorldGen.genRand.Next(3, 7), WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(63, 65), false, 0f, 0f, false, true);
				num65++;
			}
			WorldGen.mudWall = true;
			Main.statusText = "Generating jungle: 15%";
			num60 += WorldGen.genRand.Next((int)(-250f * num59), (int)(251f * num59));
			num62 += WorldGen.genRand.Next((int)(-150f * num59), (int)(151f * num59));
			int num66 = num60;
			int num67 = num62;
			int num68 = num60;
			int num69 = num62;
			WorldGen.TileRunner(num60, num62, (double)WorldGen.genRand.Next((int)(250f * num59), (int)(500f * num59)), WorldGen.genRand.Next(50, 150), 59, false, 0f, 0f, false, true);
			WorldGen.mudWall = false;
			int num70 = 0;
			while ((float)num70 < 6f * num59)
			{
				WorldGen.TileRunner(num60 + WorldGen.genRand.Next(-(int)(125f * num59), (int)(125f * num59)), num62 + WorldGen.genRand.Next(-(int)(125f * num59), (int)(125f * num59)), (double)WorldGen.genRand.Next(3, 7), WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(65, 67), false, 0f, 0f, false, true);
				num70++;
			}
			WorldGen.mudWall = true;
			Main.statusText = "Generating jungle: 30%";
			num60 += WorldGen.genRand.Next((int)(-400f * num59), (int)(401f * num59));
			num62 += WorldGen.genRand.Next((int)(-150f * num59), (int)(151f * num59));
			int num71 = num60;
			int num72 = num62;
			WorldGen.TileRunner(num60, num62, (double)WorldGen.genRand.Next((int)(250f * num59), (int)(500f * num59)), WorldGen.genRand.Next(50, 150), 59, false, (float)(num9 * -3), 0f, false, true);
			WorldGen.mudWall = false;
			int num73 = 0;
			while ((float)num73 < 6f * num59)
			{
				WorldGen.TileRunner(num60 + WorldGen.genRand.Next(-(int)(125f * num59), (int)(125f * num59)), num62 + WorldGen.genRand.Next(-(int)(125f * num59), (int)(125f * num59)), (double)WorldGen.genRand.Next(3, 7), WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(67, 69), false, 0f, 0f, false, true);
				num73++;
			}
			WorldGen.mudWall = true;
			Main.statusText = "Generating jungle: 45%";
			num60 = (num63 + num66 + num71) / 3;
			num62 = (num64 + num67 + num72) / 3;
			WorldGen.TileRunner(num60, num62, (double)WorldGen.genRand.Next((int)(400f * num59), (int)(600f * num59)), 10000, 59, false, 0f, -20f, true, true);
			WorldGen.JungleRunner(num60, num62);
			Main.statusText = "Generating jungle: 60%";
			WorldGen.mudWall = false;
			for (int num74 = 0; num74 < Main.maxTilesX / 10; num74++)
			{
				num60 = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
				num62 = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200);
				while (Main.tile[num60, num62].wall != 15)
				{
					num60 = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
					num62 = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200);
				}
				WorldGen.MudWallRunner(num60, num62);
			}
			num60 = num68;
			num62 = num69;
			int num75 = 0;
			while ((float)num75 <= 20f * num59)
			{
				Main.statusText = "Generating jungle: " + (int)(60f + (float)num75 / num59) + "%";
				num60 += WorldGen.genRand.Next((int)(-5f * num59), (int)(6f * num59));
				num62 += WorldGen.genRand.Next((int)(-5f * num59), (int)(6f * num59));
				WorldGen.TileRunner(num60, num62, (double)WorldGen.genRand.Next(40, 100), WorldGen.genRand.Next(300, 500), 59, false, 0f, 0f, false, true);
				num75++;
			}
			int num76 = 0;
			while ((float)num76 <= 10f * num59)
			{
				Main.statusText = "Generating jungle: " + (int)(80f + (float)num76 / num59 * 2f) + "%";
				num60 = num68 + WorldGen.genRand.Next((int)(-600f * num59), (int)(600f * num59));
				num62 = num69 + WorldGen.genRand.Next((int)(-200f * num59), (int)(200f * num59));
				while (num60 < 1 || num60 >= Main.maxTilesX - 1 || num62 < 1 || num62 >= Main.maxTilesY - 1 || Main.tile[num60, num62].type != 59)
				{
					num60 = num68 + WorldGen.genRand.Next((int)(-600f * num59), (int)(600f * num59));
					num62 = num69 + WorldGen.genRand.Next((int)(-200f * num59), (int)(200f * num59));
				}
				int num77 = 0;
				while ((float)num77 < 8f * num59)
				{
					num60 += WorldGen.genRand.Next(-30, 31);
					num62 += WorldGen.genRand.Next(-30, 31);
					int type4 = -1;
					if (WorldGen.genRand.Next(7) == 0)
					{
						type4 = -2;
					}
					WorldGen.TileRunner(num60, num62, (double)WorldGen.genRand.Next(10, 20), WorldGen.genRand.Next(30, 70), type4, false, 0f, 0f, false, true);
					num77++;
				}
				num76++;
			}
			int num78 = 0;
			while ((float)num78 <= 300f * num59)
			{
				num60 = num68 + WorldGen.genRand.Next((int)(-600f * num59), (int)(600f * num59));
				num62 = num69 + WorldGen.genRand.Next((int)(-200f * num59), (int)(200f * num59));
				while (num60 < 1 || num60 >= Main.maxTilesX - 1 || num62 < 1 || num62 >= Main.maxTilesY - 1 || Main.tile[num60, num62].type != 59)
				{
					num60 = num68 + WorldGen.genRand.Next((int)(-600f * num59), (int)(600f * num59));
					num62 = num69 + WorldGen.genRand.Next((int)(-200f * num59), (int)(200f * num59));
				}
				WorldGen.TileRunner(num60, num62, (double)WorldGen.genRand.Next(4, 10), WorldGen.genRand.Next(5, 30), 1, false, 0f, 0f, false, true);
				if (WorldGen.genRand.Next(4) == 0)
				{
					int type5 = WorldGen.genRand.Next(63, 69);
					WorldGen.TileRunner(num60 + WorldGen.genRand.Next(-1, 2), num62 + WorldGen.genRand.Next(-1, 2), (double)WorldGen.genRand.Next(3, 7), WorldGen.genRand.Next(4, 8), type5, false, 0f, 0f, false, true);
				}
				num78++;
			}
			num60 = num68;
			num62 = num69;
			float num79 = (float)WorldGen.genRand.Next(6, 10);
			float num80 = (float)(Main.maxTilesX / 4200);
			num79 *= num80;
			int num81 = 0;
			while ((float)num81 < num79)
			{
				bool flag4 = true;
				while (flag4)
				{
					num60 = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
					num62 = WorldGen.genRand.Next((int)(Main.worldSurface + Main.rockLayer) / 2, Main.maxTilesY - 300);
					if (Main.tile[num60, num62].type == 59)
					{
						flag4 = false;
						int num82 = WorldGen.genRand.Next(2, 4);
						int num83 = WorldGen.genRand.Next(2, 4);
						for (int num84 = num60 - num82 - 1; num84 <= num60 + num82 + 1; num84++)
						{
							for (int num85 = num62 - num83 - 1; num85 <= num62 + num83 + 1; num85++)
							{
								Main.tile[num84, num85].active = true;
								Main.tile[num84, num85].type = 45;
								Main.tile[num84, num85].liquid = 0;
								Main.tile[num84, num85].lava = false;
							}
						}
						for (int num86 = num60 - num82; num86 <= num60 + num82; num86++)
						{
							for (int num87 = num62 - num83; num87 <= num62 + num83; num87++)
							{
								Main.tile[num86, num87].active = false;
							}
						}
						bool flag5 = false;
						int num88 = 0;
						while (!flag5 && num88 < 100)
						{
							num88++;
							int num89 = WorldGen.genRand.Next(num60 - num82, num60 + num82 + 1);
							int num90 = WorldGen.genRand.Next(num62 - num83, num62 + num83 - 2);
							WorldGen.PlaceTile(num89, num90, 4, true, false, -1, 0);
							if (Main.tile[num89, num90].type == 4)
							{
								flag5 = true;
							}
						}
						for (int num91 = num60 - num82 - 1; num91 <= num60 + num82 + 1; num91++)
						{
							for (int num92 = num62 + num83 - 2; num92 <= num62 + num83; num92++)
							{
								Main.tile[num91, num92].active = false;
							}
						}
						for (int num93 = num60 - num82 - 1; num93 <= num60 + num82 + 1; num93++)
						{
							for (int num94 = num62 + num83 - 2; num94 <= num62 + num83 - 1; num94++)
							{
								Main.tile[num93, num94].active = false;
							}
						}
						for (int num95 = num60 - num82 - 1; num95 <= num60 + num82 + 1; num95++)
						{
							int num96 = 4;
							int num97 = num62 + num83 + 2;
							while (!Main.tile[num95, num97].active && num97 < Main.maxTilesY && num96 > 0)
							{
								Main.tile[num95, num97].active = true;
								Main.tile[num95, num97].type = 59;
								num97++;
								num96--;
							}
						}
						num82 -= WorldGen.genRand.Next(1, 3);
						int num98 = num62 - num83 - 2;
						while (num82 > -1)
						{
							for (int num99 = num60 - num82 - 1; num99 <= num60 + num82 + 1; num99++)
							{
								Main.tile[num99, num98].active = true;
								Main.tile[num99, num98].type = 45;
							}
							num82 -= WorldGen.genRand.Next(1, 3);
							num98--;
						}
						WorldGen.JChestX[WorldGen.numJChests] = num60;
						WorldGen.JChestY[WorldGen.numJChests] = num62;
						WorldGen.numJChests++;
					}
				}
				num81++;
			}
			for (int num100 = 0; num100 < Main.maxTilesX; num100++)
			{
				for (int num101 = 0; num101 < Main.maxTilesY; num101++)
				{
					if (Main.tile[num100, num101].active)
					{
						WorldGen.SpreadGrass(num100, num101, 59, 60, true);
					}
				}
			}
			WorldGen.numIslandHouses = 0;
			WorldGen.houseCount = 0;
			Main.statusText = "Generating floating islands...";
			for (int num102 = 0; num102 < (int)((double)Main.maxTilesX * 0.0008); num102++)
			{
				int num103 = 0;
				bool flag6 = false;
				int num104 = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.1), (int)((double)Main.maxTilesX * 0.9));
				bool flag7 = false;
				while (!flag7)
				{
					flag7 = true;
					while (num104 > Main.maxTilesX / 2 - 80 && num104 < Main.maxTilesX / 2 + 80)
					{
						num104 = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.1), (int)((double)Main.maxTilesX * 0.9));
					}
					for (int num105 = 0; num105 < WorldGen.numIslandHouses; num105++)
					{
						if (num104 > WorldGen.fihX[num105] - 80 && num104 < WorldGen.fihX[num105] + 80)
						{
							num103++;
							flag7 = false;
							break;
						}
					}
					if (num103 >= 200)
					{
						flag6 = true;
						break;
					}
				}
				if (!flag6)
				{
					int num106 = 200;
					while ((double)num106 < Main.worldSurface)
					{
						if (Main.tile[num104, num106].active)
						{
							int num107 = num104;
							int num108 = WorldGen.genRand.Next(90, num106 - 100);
							while ((double)num108 > num5 - 50.0)
							{
								num108--;
							}
							WorldGen.FloatingIsland(num107, num108);
							WorldGen.fihX[WorldGen.numIslandHouses] = num107;
							WorldGen.fihY[WorldGen.numIslandHouses] = num108;
							WorldGen.numIslandHouses++;
							break;
						}
						num106++;
					}
				}
			}
			Main.statusText = "Adding mushroom patches...";
			for (int num109 = 0; num109 < Main.maxTilesX / 300; num109++)
			{
				int i2 = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.3), (int)((double)Main.maxTilesX * 0.7));
				int j2 = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 300);
				WorldGen.ShroomPatch(i2, j2);
			}
			for (int num110 = 0; num110 < Main.maxTilesX; num110++)
			{
				for (int num111 = (int)Main.worldSurface; num111 < Main.maxTilesY; num111++)
				{
					if (Main.tile[num110, num111].active)
					{
						WorldGen.SpreadGrass(num110, num111, 59, 70, false);
					}
				}
			}
			Main.statusText = "Placing mud in the dirt...";
			for (int num112 = 0; num112 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.001); num112++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num7, Main.maxTilesY), (double)WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(2, 40), 59, false, 0f, 0f, false, true);
			}
			Main.statusText = "Adding shinies...";
			for (int num113 = 0; num113 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); num113++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num5, (int)num6), (double)WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 6), 7, false, 0f, 0f, false, true);
			}
			for (int num114 = 0; num114 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 8E-05); num114++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num6, (int)num8), (double)WorldGen.genRand.Next(3, 7), WorldGen.genRand.Next(3, 7), 7, false, 0f, 0f, false, true);
			}
			for (int num115 = 0; num115 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0002); num115++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num7, Main.maxTilesY), (double)WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(4, 8), 7, false, 0f, 0f, false, true);
			}
			for (int num116 = 0; num116 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 3E-05); num116++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num5, (int)num6), (double)WorldGen.genRand.Next(3, 7), WorldGen.genRand.Next(2, 5), 6, false, 0f, 0f, false, true);
			}
			for (int num117 = 0; num117 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 8E-05); num117++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num6, (int)num8), (double)WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(3, 6), 6, false, 0f, 0f, false, true);
			}
			for (int num118 = 0; num118 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0002); num118++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num7, Main.maxTilesY), (double)WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(4, 8), 6, false, 0f, 0f, false, true);
			}
			for (int num119 = 0; num119 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 3E-05); num119++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num6, (int)num8), (double)WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(3, 6), 9, false, 0f, 0f, false, true);
			}
			for (int num120 = 0; num120 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.00017); num120++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num7, Main.maxTilesY), (double)WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(4, 8), 9, false, 0f, 0f, false, true);
			}
			for (int num121 = 0; num121 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.00017); num121++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next(0, (int)num5), (double)WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(4, 8), 9, false, 0f, 0f, false, true);
			}
			for (int num122 = 0; num122 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.00012); num122++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num7, Main.maxTilesY), (double)WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(4, 8), 8, false, 0f, 0f, false, true);
			}
			for (int num123 = 0; num123 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.00012); num123++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next(0, (int)num5 - 20), (double)WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(4, 8), 8, false, 0f, 0f, false, true);
			}
			for (int num124 = 0; num124 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 2E-05); num124++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)num7, Main.maxTilesY), (double)WorldGen.genRand.Next(2, 4), WorldGen.genRand.Next(3, 6), 22, false, 0f, 0f, false, true);
			}
			Main.statusText = "Adding webs...";
			for (int num125 = 0; num125 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.001); num125++)
			{
				int num126 = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
				int num127 = WorldGen.genRand.Next((int)num5, Main.maxTilesY - 20);
				if (num125 < WorldGen.numMCaves)
				{
					num126 = WorldGen.mCaveX[num125];
					num127 = WorldGen.mCaveY[num125];
				}
				if (!Main.tile[num126, num127].active)
				{
					if ((double)num127 <= Main.worldSurface)
					{
						if (Main.tile[num126, num127].wall <= 0)
						{
							goto IL_2E75;
						}
					}
					while (!Main.tile[num126, num127].active && num127 > (int)num5)
					{
						num127--;
					}
					num127++;
					int num128 = 1;
					if (WorldGen.genRand.Next(2) == 0)
					{
						num128 = -1;
					}
					while (!Main.tile[num126, num127].active && num126 > 10 && num126 < Main.maxTilesX - 10)
					{
						num126 += num128;
					}
					num126 -= num128;
					if ((double)num127 > Main.worldSurface || Main.tile[num126, num127].wall > 0)
					{
						WorldGen.TileRunner(num126, num127, (double)WorldGen.genRand.Next(4, 13), WorldGen.genRand.Next(2, 5), 51, true, (float)num128, -1f, false, false);
					}
				}
				IL_2E75:;
			}
			Main.statusText = "Creating underworld: 0%";
			int num129 = Main.maxTilesY - WorldGen.genRand.Next(150, 190);
			for (int num130 = 0; num130 < Main.maxTilesX; num130++)
			{
				num129 += WorldGen.genRand.Next(-3, 4);
				if (num129 < Main.maxTilesY - 190)
				{
					num129 = Main.maxTilesY - 190;
				}
				if (num129 > Main.maxTilesY - 160)
				{
					num129 = Main.maxTilesY - 160;
				}
				for (int num131 = num129 - 20 - WorldGen.genRand.Next(3); num131 < Main.maxTilesY; num131++)
				{
					if (num131 >= num129)
					{
						Main.tile[num130, num131].active = false;
						Main.tile[num130, num131].lava = false;
						Main.tile[num130, num131].liquid = 0;
					}
					else
					{
						Main.tile[num130, num131].type = 57;
					}
				}
			}
			int num132 = Main.maxTilesY - WorldGen.genRand.Next(40, 70);
			for (int num133 = 10; num133 < Main.maxTilesX - 10; num133++)
			{
				num132 += WorldGen.genRand.Next(-10, 11);
				if (num132 > Main.maxTilesY - 60)
				{
					num132 = Main.maxTilesY - 60;
				}
				if (num132 < Main.maxTilesY - 100)
				{
					num132 = Main.maxTilesY - 120;
				}
				for (int num134 = num132; num134 < Main.maxTilesY - 10; num134++)
				{
					if (!Main.tile[num133, num134].active)
					{
						Main.tile[num133, num134].lava = true;
						Main.tile[num133, num134].liquid = 255;
					}
				}
			}
			for (int num135 = 0; num135 < Main.maxTilesX; num135++)
			{
				if (WorldGen.genRand.Next(50) == 0)
				{
					int num136 = Main.maxTilesY - 65;
					while (!Main.tile[num135, num136].active && num136 > Main.maxTilesY - 135)
					{
						num136--;
					}
					WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), num136 + WorldGen.genRand.Next(20, 50), (double)WorldGen.genRand.Next(15, 20), 1000, 57, true, 0f, (float)WorldGen.genRand.Next(1, 3), true, true);
				}
			}
			Liquid.QuickWater(-2, -1, -1);
			for (int num137 = 0; num137 < Main.maxTilesX; num137++)
			{
				float num138 = (float)num137 / (float)(Main.maxTilesX - 1);
				Main.statusText = "Creating underworld: " + (int)(num138 * 100f / 2f + 50f) + "%";
				if (WorldGen.genRand.Next(13) == 0)
				{
					int num139 = Main.maxTilesY - 65;
					while ((Main.tile[num137, num139].liquid > 0 || Main.tile[num137, num139].active) && num139 > Main.maxTilesY - 140)
					{
						num139--;
					}
					WorldGen.TileRunner(num137, num139 - WorldGen.genRand.Next(2, 5), (double)WorldGen.genRand.Next(5, 30), 1000, 57, true, 0f, (float)WorldGen.genRand.Next(1, 3), true, true);
					float num140 = (float)WorldGen.genRand.Next(1, 3);
					if (WorldGen.genRand.Next(3) == 0)
					{
						num140 *= 0.5f;
					}
					if (WorldGen.genRand.Next(2) == 0)
					{
						WorldGen.TileRunner(num137, num139 - WorldGen.genRand.Next(2, 5), (double)((int)((float)WorldGen.genRand.Next(5, 15) * num140)), (int)((float)WorldGen.genRand.Next(10, 15) * num140), 57, true, 1f, 0.3f, false, true);
					}
					if (WorldGen.genRand.Next(2) == 0)
					{
						num140 = (float)WorldGen.genRand.Next(1, 3);
						WorldGen.TileRunner(num137, num139 - WorldGen.genRand.Next(2, 5), (double)((int)((float)WorldGen.genRand.Next(5, 15) * num140)), (int)((float)WorldGen.genRand.Next(10, 15) * num140), 57, true, -1f, 0.3f, false, true);
					}
					WorldGen.TileRunner(num137 + WorldGen.genRand.Next(-10, 10), num139 + WorldGen.genRand.Next(-10, 10), (double)WorldGen.genRand.Next(5, 15), WorldGen.genRand.Next(5, 10), -2, false, (float)WorldGen.genRand.Next(-1, 3), (float)WorldGen.genRand.Next(-1, 3), false, true);
					if (WorldGen.genRand.Next(3) == 0)
					{
						WorldGen.TileRunner(num137 + WorldGen.genRand.Next(-10, 10), num139 + WorldGen.genRand.Next(-10, 10), (double)WorldGen.genRand.Next(10, 30), WorldGen.genRand.Next(10, 20), -2, false, (float)WorldGen.genRand.Next(-1, 3), (float)WorldGen.genRand.Next(-1, 3), false, true);
					}
					if (WorldGen.genRand.Next(5) == 0)
					{
						WorldGen.TileRunner(num137 + WorldGen.genRand.Next(-15, 15), num139 + WorldGen.genRand.Next(-15, 10), (double)WorldGen.genRand.Next(15, 30), WorldGen.genRand.Next(5, 20), -2, false, (float)WorldGen.genRand.Next(-1, 3), (float)WorldGen.genRand.Next(-1, 3), false, true);
					}
				}
			}
			for (int num141 = 0; num141 < Main.maxTilesX; num141++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(20, Main.maxTilesX - 20), WorldGen.genRand.Next(Main.maxTilesY - 180, Main.maxTilesY - 10), (double)WorldGen.genRand.Next(2, 7), WorldGen.genRand.Next(2, 7), -2, false, 0f, 0f, false, true);
			}
			for (int num142 = 0; num142 < Main.maxTilesX; num142++)
			{
				if (!Main.tile[num142, Main.maxTilesY - 145].active)
				{
					Main.tile[num142, Main.maxTilesY - 145].liquid = 255;
					Main.tile[num142, Main.maxTilesY - 145].lava = true;
				}
				if (!Main.tile[num142, Main.maxTilesY - 144].active)
				{
					Main.tile[num142, Main.maxTilesY - 144].liquid = 255;
					Main.tile[num142, Main.maxTilesY - 144].lava = true;
				}
			}
			for (int num143 = 0; num143 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0008); num143++)
			{
				WorldGen.TileRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next(Main.maxTilesY - 140, Main.maxTilesY), (double)WorldGen.genRand.Next(2, 7), WorldGen.genRand.Next(3, 7), 58, false, 0f, 0f, false, true);
			}
			WorldGen.AddHellHouses();
			int num144 = WorldGen.genRand.Next(2, (int)((double)Main.maxTilesX * 0.005));
			for (int num145 = 0; num145 < num144; num145++)
			{
				float num146 = (float)num145 / (float)num144;
				Main.statusText = "Adding water bodies: " + (int)(num146 * 100f) + "%";
				int num147 = WorldGen.genRand.Next(300, Main.maxTilesX - 300);
				while (num147 > Main.maxTilesX / 2 - 50 && num147 < Main.maxTilesX / 2 + 50)
				{
					num147 = WorldGen.genRand.Next(300, Main.maxTilesX - 300);
				}
				int num148 = (int)num5 - 20;
				while (!Main.tile[num147, num148].active)
				{
					num148++;
				}
				WorldGen.Lakinater(num147, num148);
			}
			int x = 0;
			if (num9 == -1)
			{
				x = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.05), (int)((double)Main.maxTilesX * 0.2));
				num9 = -1;
			}
			else
			{
				x = WorldGen.genRand.Next((int)((double)Main.maxTilesX * 0.8), (int)((double)Main.maxTilesX * 0.95));
				num9 = 1;
			}
			int y = (int)((Main.rockLayer + (double)Main.maxTilesY) / 2.0) + WorldGen.genRand.Next(-200, 200);
			WorldGen.MakeDungeon(x, y, 41, 7);
			int num149 = 0;
			while ((double)num149 < (double)Main.maxTilesX * 0.00045)
			{
				float num150 = (float)((double)num149 / ((double)Main.maxTilesX * 0.00045));
				Main.statusText = "Making the world evil: " + (int)(num150 * 100f) + "%";
				bool flag8 = false;
				int num151 = 0;
				int num152 = 0;
				int num153 = 0;
				while (!flag8)
				{
					int num154 = 0;
					flag8 = true;
					int num155 = Main.maxTilesX / 2;
					int num156 = 200;
					num151 = WorldGen.genRand.Next(320, Main.maxTilesX - 320);
					num152 = num151 - WorldGen.genRand.Next(200) - 100;
					num153 = num151 + WorldGen.genRand.Next(200) + 100;
					if (num152 < 285)
					{
						num152 = 285;
					}
					if (num153 > Main.maxTilesX - 285)
					{
						num153 = Main.maxTilesX - 285;
					}
					if (num151 > num155 - num156 && num151 < num155 + num156)
					{
						flag8 = false;
					}
					if (num152 > num155 - num156 && num152 < num155 + num156)
					{
						flag8 = false;
					}
					if (num153 > num155 - num156 && num153 < num155 + num156)
					{
						flag8 = false;
					}
					for (int num157 = num152; num157 < num153; num157++)
					{
						for (int num158 = 0; num158 < (int)Main.worldSurface; num158 += 5)
						{
							if (Main.tile[num157, num158].active && Main.tileDungeon[(int)Main.tile[num157, num158].type])
							{
								flag8 = false;
								break;
							}
							if (!flag8)
							{
								break;
							}
						}
					}
					if (num154 < 200 && WorldGen.JungleX > num152 && WorldGen.JungleX < num153)
					{
						num154++;
						flag8 = false;
					}
				}
				int num159 = 0;
				for (int num160 = num152; num160 < num153; num160++)
				{
					if (num159 > 0)
					{
						num159--;
					}
					if (num160 == num151 || num159 == 0)
					{
						int num161 = (int)num5;
						while ((double)num161 < Main.worldSurface - 1.0)
						{
							if (Main.tile[num160, num161].active || Main.tile[num160, num161].wall > 0)
							{
								if (num160 == num151)
								{
									num159 = 20;
									WorldGen.ChasmRunner(num160, num161, WorldGen.genRand.Next(150) + 150, true);
									break;
								}
								if (WorldGen.genRand.Next(35) == 0 && num159 == 0)
								{
									num159 = 30;
									bool makeOrb = true;
									WorldGen.ChasmRunner(num160, num161, WorldGen.genRand.Next(50) + 50, makeOrb);
									break;
								}
								break;
							}
							else
							{
								num161++;
							}
						}
					}
					int num162 = (int)num5;
					while ((double)num162 < Main.worldSurface - 1.0)
					{
						if (Main.tile[num160, num162].active)
						{
							int num163 = num162 + WorldGen.genRand.Next(10, 14);
							for (int num164 = num162; num164 < num163; num164++)
							{
								if ((Main.tile[num160, num164].type == 59 || Main.tile[num160, num164].type == 60) && num160 >= num152 + WorldGen.genRand.Next(5) && num160 < num153 - WorldGen.genRand.Next(5))
								{
									Main.tile[num160, num164].type = 0;
								}
							}
							break;
						}
						num162++;
					}
				}
				double num165 = Main.worldSurface + 40.0;
				for (int num166 = num152; num166 < num153; num166++)
				{
					num165 += (double)WorldGen.genRand.Next(-2, 3);
					if (num165 < Main.worldSurface + 30.0)
					{
						num165 = Main.worldSurface + 30.0;
					}
					if (num165 > Main.worldSurface + 50.0)
					{
						num165 = Main.worldSurface + 50.0;
					}
					int num48 = num166;
					bool flag9 = false;
					int num167 = (int)num5;
					while ((double)num167 < num165)
					{
						if (Main.tile[num48, num167].active)
						{
							if (Main.tile[num48, num167].type == 53 && num48 >= num152 + WorldGen.genRand.Next(5) && num48 <= num153 - WorldGen.genRand.Next(5))
							{
								Main.tile[num48, num167].type = 0;
							}
							if (Main.tile[num48, num167].type == 0 && (double)num167 < Main.worldSurface - 1.0 && !flag9)
							{
								WorldGen.SpreadGrass(num48, num167, 0, 23, true);
							}
							flag9 = true;
							if (Main.tile[num48, num167].type == 1 && num48 >= num152 + WorldGen.genRand.Next(5) && num48 <= num153 - WorldGen.genRand.Next(5))
							{
								Main.tile[num48, num167].type = 25;
							}
							if (Main.tile[num48, num167].type == 2)
							{
								Main.tile[num48, num167].type = 23;
							}
						}
						num167++;
					}
				}
				for (int num168 = num152; num168 < num153; num168++)
				{
					for (int num169 = 0; num169 < Main.maxTilesY - 50; num169++)
					{
						if (Main.tile[num168, num169].active && Main.tile[num168, num169].type == 31)
						{
							int num170 = num168 - 13;
							int num171 = num168 + 13;
							int num172 = num169 - 13;
							int num173 = num169 + 13;
							for (int num174 = num170; num174 < num171; num174++)
							{
								if (num174 > 10 && num174 < Main.maxTilesX - 10)
								{
									for (int num175 = num172; num175 < num173; num175++)
									{
										if (Math.Abs(num174 - num168) + Math.Abs(num175 - num169) < 9 + WorldGen.genRand.Next(11) && WorldGen.genRand.Next(3) != 0 && Main.tile[num174, num175].type != 31)
										{
											Main.tile[num174, num175].active = true;
											Main.tile[num174, num175].type = 25;
											if (Math.Abs(num174 - num168) <= 1 && Math.Abs(num175 - num169) <= 1)
											{
												Main.tile[num174, num175].active = false;
											}
										}
										if (Main.tile[num174, num175].type != 31 && Math.Abs(num174 - num168) <= 2 + WorldGen.genRand.Next(3) && Math.Abs(num175 - num169) <= 2 + WorldGen.genRand.Next(3))
										{
											Main.tile[num174, num175].active = false;
										}
									}
								}
							}
						}
					}
				}
				num149++;
			}
			Main.statusText = "Generating mountain caves...";
			for (int num176 = 0; num176 < WorldGen.numMCaves; num176++)
			{
				int i3 = WorldGen.mCaveX[num176];
				int j3 = WorldGen.mCaveY[num176];
				WorldGen.CaveOpenater(i3, j3);
				WorldGen.Cavinator(i3, j3, WorldGen.genRand.Next(40, 50));
			}
			int num177 = 0;
			int num178 = 0;
			int num179 = 20;
			int num180 = Main.maxTilesX - 20;
			Main.statusText = "Creating beaches...";
			for (int num181 = 0; num181 < 2; num181++)
			{
				int num182 = 0;
				int num183 = 0;
				if (num181 == 0)
				{
					num182 = 0;
					num183 = WorldGen.genRand.Next(125, 200) + 50;
					if (num9 == 1)
					{
						num183 = 275;
					}
					int num184 = 0;
					float num185 = 1f;
					int num186 = 0;
					while (!Main.tile[num183 - 1, num186].active)
					{
						num186++;
					}
					num177 = num186;
					num186 += WorldGen.genRand.Next(1, 5);
					for (int num187 = num183 - 1; num187 >= num182; num187--)
					{
						num184++;
						if (num184 < 3)
						{
							num185 += (float)WorldGen.genRand.Next(10, 20) * 0.2f;
						}
						else
						{
							if (num184 < 6)
							{
								num185 += (float)WorldGen.genRand.Next(10, 20) * 0.15f;
							}
							else
							{
								if (num184 < 9)
								{
									num185 += (float)WorldGen.genRand.Next(10, 20) * 0.1f;
								}
								else
								{
									if (num184 < 15)
									{
										num185 += (float)WorldGen.genRand.Next(10, 20) * 0.07f;
									}
									else
									{
										if (num184 < 50)
										{
											num185 += (float)WorldGen.genRand.Next(10, 20) * 0.05f;
										}
										else
										{
											if (num184 < 75)
											{
												num185 += (float)WorldGen.genRand.Next(10, 20) * 0.04f;
											}
											else
											{
												if (num184 < 100)
												{
													num185 += (float)WorldGen.genRand.Next(10, 20) * 0.03f;
												}
												else
												{
													if (num184 < 125)
													{
														num185 += (float)WorldGen.genRand.Next(10, 20) * 0.02f;
													}
													else
													{
														if (num184 < 150)
														{
															num185 += (float)WorldGen.genRand.Next(10, 20) * 0.01f;
														}
														else
														{
															if (num184 < 175)
															{
																num185 += (float)WorldGen.genRand.Next(10, 20) * 0.005f;
															}
															else
															{
																if (num184 < 200)
																{
																	num185 += (float)WorldGen.genRand.Next(10, 20) * 0.001f;
																}
																else
																{
																	if (num184 < 230)
																	{
																		num185 += (float)WorldGen.genRand.Next(10, 20) * 0.01f;
																	}
																	else
																	{
																		if (num184 < 235)
																		{
																			num185 += (float)WorldGen.genRand.Next(10, 20) * 0.05f;
																		}
																		else
																		{
																			if (num184 < 240)
																			{
																				num185 += (float)WorldGen.genRand.Next(10, 20) * 0.1f;
																			}
																			else
																			{
																				if (num184 < 245)
																				{
																					num185 += (float)WorldGen.genRand.Next(10, 20) * 0.05f;
																				}
																				else
																				{
																					if (num184 < 255)
																					{
																						num185 += (float)WorldGen.genRand.Next(10, 20) * 0.01f;
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
						if (num184 == 235)
						{
							num180 = num187;
						}
						if (num184 == 235)
						{
							num179 = num187;
						}
						int num188 = WorldGen.genRand.Next(15, 20);
						int num189 = 0;
						while ((float)num189 < (float)num186 + num185 + (float)num188)
						{
							if ((float)num189 < (float)num186 + num185 * 0.75f - 3f)
							{
								Main.tile[num187, num189].active = false;
								if (num189 > num186)
								{
									Main.tile[num187, num189].liquid = 255;
								}
								else
								{
									if (num189 == num186)
									{
										Main.tile[num187, num189].liquid = 127;
									}
								}
							}
							else
							{
								if (num189 > num186)
								{
									Main.tile[num187, num189].type = 53;
									Main.tile[num187, num189].active = true;
								}
							}
							Main.tile[num187, num189].wall = 0;
							num189++;
						}
					}
				}
				else
				{
					num182 = Main.maxTilesX - WorldGen.genRand.Next(125, 200) - 50;
					num183 = Main.maxTilesX;
					if (num9 == -1)
					{
						num182 = Main.maxTilesX - 275;
					}
					float num190 = 1f;
					int num191 = 0;
					int num192 = 0;
					while (!Main.tile[num182, num192].active)
					{
						num192++;
					}
					num178 = num192;
					num192 += WorldGen.genRand.Next(1, 5);
					for (int num193 = num182; num193 < num183; num193++)
					{
						num191++;
						if (num191 < 3)
						{
							num190 += (float)WorldGen.genRand.Next(10, 20) * 0.2f;
						}
						else
						{
							if (num191 < 6)
							{
								num190 += (float)WorldGen.genRand.Next(10, 20) * 0.15f;
							}
							else
							{
								if (num191 < 9)
								{
									num190 += (float)WorldGen.genRand.Next(10, 20) * 0.1f;
								}
								else
								{
									if (num191 < 15)
									{
										num190 += (float)WorldGen.genRand.Next(10, 20) * 0.07f;
									}
									else
									{
										if (num191 < 50)
										{
											num190 += (float)WorldGen.genRand.Next(10, 20) * 0.05f;
										}
										else
										{
											if (num191 < 75)
											{
												num190 += (float)WorldGen.genRand.Next(10, 20) * 0.04f;
											}
											else
											{
												if (num191 < 100)
												{
													num190 += (float)WorldGen.genRand.Next(10, 20) * 0.03f;
												}
												else
												{
													if (num191 < 125)
													{
														num190 += (float)WorldGen.genRand.Next(10, 20) * 0.02f;
													}
													else
													{
														if (num191 < 150)
														{
															num190 += (float)WorldGen.genRand.Next(10, 20) * 0.01f;
														}
														else
														{
															if (num191 < 175)
															{
																num190 += (float)WorldGen.genRand.Next(10, 20) * 0.005f;
															}
															else
															{
																if (num191 < 200)
																{
																	num190 += (float)WorldGen.genRand.Next(10, 20) * 0.001f;
																}
																else
																{
																	if (num191 < 230)
																	{
																		num190 += (float)WorldGen.genRand.Next(10, 20) * 0.01f;
																	}
																	else
																	{
																		if (num191 < 235)
																		{
																			num190 += (float)WorldGen.genRand.Next(10, 20) * 0.05f;
																		}
																		else
																		{
																			if (num191 < 240)
																			{
																				num190 += (float)WorldGen.genRand.Next(10, 20) * 0.1f;
																			}
																			else
																			{
																				if (num191 < 245)
																				{
																					num190 += (float)WorldGen.genRand.Next(10, 20) * 0.05f;
																				}
																				else
																				{
																					if (num191 < 255)
																					{
																						num190 += (float)WorldGen.genRand.Next(10, 20) * 0.01f;
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
						if (num191 == 235)
						{
							num180 = num193;
						}
						int num194 = WorldGen.genRand.Next(15, 20);
						int num195 = 0;
						while ((float)num195 < (float)num192 + num190 + (float)num194)
						{
							if ((float)num195 < (float)num192 + num190 * 0.75f - 3f && (double)num195 < Main.worldSurface - 2.0)
							{
								Main.tile[num193, num195].active = false;
								if (num195 > num192)
								{
									Main.tile[num193, num195].liquid = 255;
								}
								else
								{
									if (num195 == num192)
									{
										Main.tile[num193, num195].liquid = 127;
									}
								}
							}
							else
							{
								if (num195 > num192)
								{
									Main.tile[num193, num195].type = 53;
									Main.tile[num193, num195].active = true;
								}
							}
							Main.tile[num193, num195].wall = 0;
							num195++;
						}
					}
				}
			}
			while (!Main.tile[num179, num177].active)
			{
				num177++;
			}
			num177++;
			while (!Main.tile[num180, num178].active)
			{
				num178++;
			}
			num178++;
			Main.statusText = "Adding gems...";
			for (int num196 = 63; num196 <= 68; num196++)
			{
				float num197 = 0f;
				if (num196 == 67)
				{
					num197 = (float)Main.maxTilesX * 0.5f;
				}
				else
				{
					if (num196 == 66)
					{
						num197 = (float)Main.maxTilesX * 0.45f;
					}
					else
					{
						if (num196 == 63)
						{
							num197 = (float)Main.maxTilesX * 0.3f;
						}
						else
						{
							if (num196 == 65)
							{
								num197 = (float)Main.maxTilesX * 0.25f;
							}
							else
							{
								if (num196 == 64)
								{
									num197 = (float)Main.maxTilesX * 0.1f;
								}
								else
								{
									if (num196 == 68)
									{
										num197 = (float)Main.maxTilesX * 0.05f;
									}
								}
							}
						}
					}
				}
				num197 *= 0.2f;
				int num198 = 0;
				while ((float)num198 < num197)
				{
					int num199 = WorldGen.genRand.Next(0, Main.maxTilesX);
					int num200 = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY);
					while (Main.tile[num199, num200].type != 1)
					{
						num199 = WorldGen.genRand.Next(0, Main.maxTilesX);
						num200 = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY);
					}
					WorldGen.TileRunner(num199, num200, (double)WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(3, 7), num196, false, 0f, 0f, false, true);
					num198++;
				}
			}
			for (int num201 = 0; num201 < Main.maxTilesX; num201++)
			{
				float num202 = (float)num201 / (float)(Main.maxTilesX - 1);
				Main.statusText = "Gravitating sand: " + (int)(num202 * 100f) + "%";
				for (int num203 = Main.maxTilesY - 5; num203 > 0; num203--)
				{
					if (Main.tile[num201, num203].active && Main.tile[num201, num203].type == 53)
					{
						int num204 = num203;
						while (!Main.tile[num201, num204 + 1].active && num204 < Main.maxTilesY - 5)
						{
							Main.tile[num201, num204 + 1].active = true;
							Main.tile[num201, num204 + 1].type = 53;
							num204++;
						}
					}
				}
			}
			for (int num205 = 3; num205 < Main.maxTilesX - 3; num205++)
			{
				float num206 = (float)num205 / (float)Main.maxTilesX;
				Main.statusText = "Cleaning up dirt backgrounds: " + (int)(num206 * 100f + 1f) + "%";
				bool flag10 = true;
				int num207 = 0;
				while ((double)num207 < Main.worldSurface)
				{
					if (flag10)
					{
						if (Main.tile[num205, num207].wall == 2)
						{
							Main.tile[num205, num207].wall = 0;
						}
						if (Main.tile[num205, num207].type != 53)
						{
							if (Main.tile[num205 - 1, num207].wall == 2)
							{
								Main.tile[num205 - 1, num207].wall = 0;
							}
							if (Main.tile[num205 - 2, num207].wall == 2 && WorldGen.genRand.Next(2) == 0)
							{
								Main.tile[num205 - 2, num207].wall = 0;
							}
							if (Main.tile[num205 - 3, num207].wall == 2 && WorldGen.genRand.Next(2) == 0)
							{
								Main.tile[num205 - 3, num207].wall = 0;
							}
							if (Main.tile[num205 + 1, num207].wall == 2)
							{
								Main.tile[num205 + 1, num207].wall = 0;
							}
							if (Main.tile[num205 + 2, num207].wall == 2 && WorldGen.genRand.Next(2) == 0)
							{
								Main.tile[num205 + 2, num207].wall = 0;
							}
							if (Main.tile[num205 + 3, num207].wall == 2 && WorldGen.genRand.Next(2) == 0)
							{
								Main.tile[num205 + 3, num207].wall = 0;
							}
							if (Main.tile[num205, num207].active)
							{
								flag10 = false;
							}
						}
					}
					else
					{
						if (Main.tile[num205, num207].wall == 0 && Main.tile[num205, num207 + 1].wall == 0 && Main.tile[num205, num207 + 2].wall == 0 && Main.tile[num205, num207 + 3].wall == 0 && Main.tile[num205, num207 + 4].wall == 0 && Main.tile[num205 - 1, num207].wall == 0 && Main.tile[num205 + 1, num207].wall == 0 && Main.tile[num205 - 2, num207].wall == 0 && Main.tile[num205 + 2, num207].wall == 0 && !Main.tile[num205, num207].active && !Main.tile[num205, num207 + 1].active && !Main.tile[num205, num207 + 2].active && !Main.tile[num205, num207 + 3].active)
						{
							flag10 = true;
						}
					}
					num207++;
				}
			}
			for (int num208 = 0; num208 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 2E-05); num208++)
			{
				float num209 = (float)((double)num208 / ((double)(Main.maxTilesX * Main.maxTilesY) * 2E-05));
				Main.statusText = "Placing altars: " + (int)(num209 * 100f + 1f) + "%";
				bool flag11 = false;
				int num210 = 0;
				while (!flag11)
				{
					int num211 = WorldGen.genRand.Next(1, Main.maxTilesX);
					int num212 = (int)(num6 + 20.0);
					WorldGen.Place3x2(num211, num212, 26);
					if (Main.tile[num211, num212].type == 26)
					{
						flag11 = true;
					}
					else
					{
						num210++;
						if (num210 >= 10000)
						{
							flag11 = true;
						}
					}
				}
			}
			for (int num213 = 0; num213 < Main.maxTilesX; num213++)
			{
				int num48 = num213;
				int num214 = (int)num5;
				while ((double)num214 < Main.worldSurface - 1.0)
				{
					if (Main.tile[num48, num214].active)
					{
						if (Main.tile[num48, num214].type == 60)
						{
							Main.tile[num48, num214 - 1].liquid = 255;
							Main.tile[num48, num214 - 2].liquid = 255;
							break;
						}
						break;
					}
					else
					{
						num214++;
					}
				}
			}
			for (int num215 = 400; num215 < Main.maxTilesX - 400; num215++)
			{
				int num48 = num215;
				int num216 = (int)num5;
				while ((double)num216 < Main.worldSurface - 1.0)
				{
					if (Main.tile[num48, num216].active)
					{
						if (Main.tile[num48, num216].type == 53)
						{
							int num217 = num216;
							while ((double)num217 > num5)
							{
								num217--;
								Main.tile[num48, num217].liquid = 0;
							}
							break;
						}
						break;
					}
					else
					{
						num216++;
					}
				}
			}
			Liquid.QuickWater(3, -1, -1);
			WorldGen.WaterCheck();
			int num218 = 0;
			Liquid.quickSettle = true;
			while (num218 < 10)
			{
				int num219 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
				num218++;
				float num220 = 0f;
				while (Liquid.numLiquid > 0)
				{
					float num221 = (float)(num219 - (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer)) / (float)num219;
					if (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer > num219)
					{
						num219 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
					}
					if (num221 > num220)
					{
						num220 = num221;
					}
					else
					{
						num221 = num220;
					}
					if (num218 == 1)
					{
						Main.statusText = "Settling liquids: " + (int)(num221 * 100f / 3f + 33f) + "%";
					}
					int num222 = 10;
					if (num218 <= num222)
					{
						goto IL_4FA8;
					}
					IL_4FA8:
					Liquid.UpdateLiquid();
				}
				WorldGen.WaterCheck();
				Main.statusText = "Settling liquids: " + (int)((float)num218 * 10f / 3f + 66f) + "%";
			}
			Liquid.quickSettle = false;
			float num223 = (float)(Main.maxTilesX / 4200);
			for (int num224 = 0; num224 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 2E-05); num224++)
			{
				float num225 = (float)((double)num224 / ((double)(Main.maxTilesX * Main.maxTilesY) * 2E-05));
				Main.statusText = "Placing life crystals: " + (int)(num225 * 100f + 1f) + "%";
				bool flag12 = false;
				int num226 = 0;
				while (!flag12)
				{
					if (WorldGen.AddLifeCrystal(WorldGen.genRand.Next(1, Main.maxTilesX), WorldGen.genRand.Next((int)(num6 + 20.0), Main.maxTilesY)))
					{
						flag12 = true;
					}
					else
					{
						num226++;
						if (num226 >= 10000)
						{
							flag12 = true;
						}
					}
				}
			}
			for (int num227 = 0; num227 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 1.6E-05); num227++)
			{
				float num228 = (float)((double)num227 / ((double)(Main.maxTilesX * Main.maxTilesY) * 1.6E-05));
				Main.statusText = "Hiding treasure: " + (int)(num228 * 100f + 1f) + "%";
				bool flag13 = false;
				int num229 = 0;
				while (!flag13)
				{
					int num230 = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
					int num231 = WorldGen.genRand.Next((int)(num6 + 20.0), Main.maxTilesY - 230);
					if ((float)num227 <= 3f * num223)
					{
						num231 = WorldGen.genRand.Next(Main.maxTilesY - 200, Main.maxTilesY - 50);
					}
					while (Main.tile[num230, num231].wall == 7 || Main.tile[num230, num231].wall == 8 || Main.tile[num230, num231].wall == 9)
					{
						num230 = WorldGen.genRand.Next(1, Main.maxTilesX);
						num231 = WorldGen.genRand.Next((int)(num6 + 20.0), Main.maxTilesY - 230);
						if (num227 <= 3)
						{
							num231 = WorldGen.genRand.Next(Main.maxTilesY - 200, Main.maxTilesY - 50);
						}
					}
					if (WorldGen.AddBuriedChest(num230, num231, 0, false, -1))
					{
						flag13 = true;
					}
					else
					{
						num229++;
						if (num229 >= 5000)
						{
							flag13 = true;
						}
					}
				}
			}
			for (int num232 = 0; num232 < (int)((double)Main.maxTilesX * 0.005); num232++)
			{
				float num233 = (float)((double)num232 / ((double)Main.maxTilesX * 0.005));
				Main.statusText = "Hiding more treasure: " + (int)(num233 * 100f + 1f) + "%";
				bool flag14 = false;
				int num234 = 0;
				while (!flag14)
				{
					int num235 = WorldGen.genRand.Next(300, Main.maxTilesX - 300);
					int num236 = WorldGen.genRand.Next((int)num5, (int)Main.worldSurface);
					bool flag15 = false;
					if (Main.tile[num235, num236].wall == 2 && !Main.tile[num235, num236].active)
					{
						flag15 = true;
					}
					if (flag15 && WorldGen.AddBuriedChest(num235, num236, 0, true, -1))
					{
						flag14 = true;
					}
					else
					{
						num234++;
						if (num234 >= 2000)
						{
							flag14 = true;
						}
					}
				}
			}
			int num237 = 0;
			for (int num238 = 0; num238 < WorldGen.numJChests; num238++)
			{
				float num239 = (float)(num238 / WorldGen.numJChests);
				Main.statusText = "Hiding jungle treasure: " + (int)(num239 * 100f + 1f) + "%";
				num237++;
				int contain = 211;
				if (num237 == 1)
				{
					contain = 211;
				}
				else
				{
					if (num237 == 2)
					{
						contain = 212;
					}
					else
					{
						if (num237 == 3)
						{
							contain = 213;
						}
					}
				}
				if (num237 > 3)
				{
					num237 = 0;
				}
				if (!WorldGen.AddBuriedChest(WorldGen.JChestX[num238] + WorldGen.genRand.Next(2), WorldGen.JChestY[num238], contain, false, -1))
				{
					for (int num240 = WorldGen.JChestX[num238]; num240 <= WorldGen.JChestX[num238] + 1; num240++)
					{
						for (int num241 = WorldGen.JChestY[num238]; num241 <= WorldGen.JChestY[num238] + 1; num241++)
						{
							WorldGen.KillTile(num240, num241, false, false, false);
						}
					}
					WorldGen.AddBuriedChest(WorldGen.JChestX[num238], WorldGen.JChestY[num238], contain, false, -1);
				}
			}
			int num242 = 0;
			int num243 = 0;
			while ((float)num243 < 9f * num223)
			{
				float num244 = (float)num243 / (9f * num223);
				Main.statusText = "Hiding water treasure: " + (int)(num244 * 100f + 1f) + "%";
				int contain2 = 0;
				num242++;
				if (num242 == 1)
				{
					contain2 = 186;
				}
				else
				{
					if (num242 == 2)
					{
						contain2 = 277;
					}
					else
					{
						contain2 = 187;
						num242 = 0;
					}
				}
				bool flag16 = false;
				while (!flag16)
				{
					int num245 = WorldGen.genRand.Next(1, Main.maxTilesX);
					int num246 = WorldGen.genRand.Next(1, Main.maxTilesY - 200);
					while (Main.tile[num245, num246].liquid < 200 || Main.tile[num245, num246].lava)
					{
						num245 = WorldGen.genRand.Next(1, Main.maxTilesX);
						num246 = WorldGen.genRand.Next(1, Main.maxTilesY - 200);
					}
					flag16 = WorldGen.AddBuriedChest(num245, num246, contain2, false, -1);
				}
				num243++;
			}
			for (int num247 = 0; num247 < WorldGen.numIslandHouses; num247++)
			{
				WorldGen.IslandHouse(WorldGen.fihX[num247], WorldGen.fihY[num247]);
			}
			for (int num248 = 0; num248 < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 0.0008); num248++)
			{
				float num249 = (float)((double)num248 / ((double)(Main.maxTilesX * Main.maxTilesY) * 0.0008));
				Main.statusText = "Placing breakables: " + (int)(num249 * 100f + 1f) + "%";
				bool flag17 = false;
				int num250 = 0;
				while (!flag17)
				{
					int num251 = WorldGen.genRand.Next((int)num6, Main.maxTilesY - 10);
					if ((double)num249 > 0.93)
					{
						num251 = Main.maxTilesY - 150;
					}
					else
					{
						if ((double)num249 > 0.75)
						{
							num251 = (int)num5;
						}
					}
					int num252 = WorldGen.genRand.Next(1, Main.maxTilesX);
					bool flag18 = false;
					for (int num253 = num251; num253 < Main.maxTilesY; num253++)
					{
						if (!flag18)
						{
							if (Main.tile[num252, num253].active && Main.tileSolid[(int)Main.tile[num252, num253].type] && !Main.tile[num252, num253 - 1].lava)
							{
								flag18 = true;
							}
						}
						else
						{
							if (WorldGen.PlacePot(num252, num253, 28))
							{
								flag17 = true;
								break;
							}
							num250++;
							if (num250 >= 10000)
							{
								flag17 = true;
								break;
							}
						}
					}
				}
			}
			for (int num254 = 0; num254 < Main.maxTilesX / 200; num254++)
			{
				float num255 = (float)(num254 / (Main.maxTilesX / 200));
				Main.statusText = "Placing hellforges: " + (int)(num255 * 100f + 1f) + "%";
				bool flag19 = false;
				int num256 = 0;
				while (!flag19)
				{
					int num257 = WorldGen.genRand.Next(1, Main.maxTilesX);
					int num258 = WorldGen.genRand.Next(Main.maxTilesY - 250, Main.maxTilesY - 5);
					try
					{
						if (Main.tile[num257, num258].wall != 13)
						{
							if (Main.tile[num257, num258].wall != 14)
							{
								continue;
							}
						}
						while (!Main.tile[num257, num258].active)
						{
							num258++;
						}
						num258--;
						WorldGen.PlaceTile(num257, num258, 77, false, false, -1, 0);
						if (Main.tile[num257, num258].type == 77)
						{
							flag19 = true;
						}
						else
						{
							num256++;
							if (num256 >= 10000)
							{
								flag19 = true;
							}
						}
					}
					catch
					{
					}
				}
			}
			Main.statusText = "Spreading grass...";
			for (int num259 = 0; num259 < Main.maxTilesX; num259++)
			{
				int num48 = num259;
				bool flag20 = true;
				int num260 = 0;
				while ((double)num260 < Main.worldSurface - 1.0)
				{
					if (Main.tile[num48, num260].active)
					{
						if (flag20 && Main.tile[num48, num260].type == 0)
						{
							WorldGen.SpreadGrass(num48, num260, 0, 2, true);
						}
						if ((double)num260 > num6)
						{
							break;
						}
						flag20 = false;
					}
					else
					{
						if (Main.tile[num48, num260].wall == 0)
						{
							flag20 = true;
						}
					}
					num260++;
				}
			}
			Main.statusText = "Growing cacti...";
			for (int num261 = 5; num261 < Main.maxTilesX - 5; num261++)
			{
				if (WorldGen.genRand.Next(8) == 0)
				{
					int num262 = 0;
					while ((double)num262 < Main.worldSurface - 1.0)
					{
						if (Main.tile[num261, num262].active && Main.tile[num261, num262].type == 53 && !Main.tile[num261, num262 - 1].active && Main.tile[num261, num262 - 1].wall == 0)
						{
							if (num261 < 250 || num261 > Main.maxTilesX - 250)
							{
								if (Main.tile[num261, num262 - 2].liquid == 255 && Main.tile[num261, num262 - 3].liquid == 255 && Main.tile[num261, num262 - 4].liquid == 255)
								{
									WorldGen.PlaceTile(num261, num262 - 1, 81, true, false, -1, 0);
								}
							}
							else
							{
								if (num261 > 400 && num261 < Main.maxTilesX - 400)
								{
									WorldGen.PlantCactus(num261, num262);
								}
							}
						}
						num262++;
					}
				}
			}
			int num263 = 5;
			bool flag21 = true;
			while (flag21)
			{
				int num264 = Main.maxTilesX / 2 + WorldGen.genRand.Next(-num263, num263 + 1);
				for (int num265 = 0; num265 < Main.maxTilesY; num265++)
				{
					if (Main.tile[num264, num265].active)
					{
						Main.spawnTileX = num264;
						Main.spawnTileY = num265;
						Main.tile[num264, num265 - 1].lighted = true;
						break;
					}
				}
				flag21 = false;
				num263++;
				if ((double)Main.spawnTileY > Main.worldSurface)
				{
					flag21 = true;
				}
				if (Main.tile[Main.spawnTileX, Main.spawnTileY - 1].liquid > 0)
				{
					flag21 = true;
				}
			}
			int num266 = 10;
			while ((double)Main.spawnTileY > Main.worldSurface)
			{
				int num267 = WorldGen.genRand.Next(Main.maxTilesX / 2 - num266, Main.maxTilesX / 2 + num266);
				for (int num268 = 0; num268 < Main.maxTilesY; num268++)
				{
					if (Main.tile[num267, num268].active)
					{
						Main.spawnTileX = num267;
						Main.spawnTileY = num268;
						Main.tile[num267, num268 - 1].lighted = true;
						break;
					}
				}
				num266++;
			}
			int num269 = NPC.NewNPC(Main.spawnTileX * 16, Main.spawnTileY * 16, 22, 0);
			Main.npc[num269].homeTileX = Main.spawnTileX;
			Main.npc[num269].homeTileY = Main.spawnTileY;
			Main.npc[num269].direction = 1;
			Main.npc[num269].homeless = true;
			Main.statusText = "Planting sunflowers...";
			int num270 = 0;
			while ((double)num270 < (double)Main.maxTilesX * 0.002)
			{
				int num271 = 0;
				int num272 = 0;
				int arg_5EAE_0 = Main.maxTilesX / 2;
				int num273 = WorldGen.genRand.Next(Main.maxTilesX);
				num271 = num273 - WorldGen.genRand.Next(10) - 7;
				num272 = num273 + WorldGen.genRand.Next(10) + 7;
				if (num271 < 0)
				{
					num271 = 0;
				}
				if (num272 > Main.maxTilesX - 1)
				{
					num272 = Main.maxTilesX - 1;
				}
				for (int num274 = num271; num274 < num272; num274++)
				{
					int num275 = 1;
					while ((double)num275 < Main.worldSurface - 1.0)
					{
						if (Main.tile[num274, num275].type == 2 && Main.tile[num274, num275].active && !Main.tile[num274, num275 - 1].active)
						{
							WorldGen.PlaceTile(num274, num275 - 1, 27, true, false, -1, 0);
						}
						if (Main.tile[num274, num275].active)
						{
							break;
						}
						num275++;
					}
				}
				num270++;
			}
			Main.statusText = "Planting trees...";
			int num276 = 0;
			while ((double)num276 < (double)Main.maxTilesX * 0.003)
			{
				int num277 = WorldGen.genRand.Next(50, Main.maxTilesX - 50);
				int num278 = WorldGen.genRand.Next(25, 50);
				for (int num279 = num277 - num278; num279 < num277 + num278; num279++)
				{
					int num280 = 20;
					while ((double)num280 < Main.worldSurface)
					{
						WorldGen.GrowEpicTree(num279, num280);
						num280++;
					}
				}
				num276++;
			}
			WorldGen.AddTrees();
			Main.statusText = "Planting herbs...";
			int num281 = 0;
			while ((double)num281 < (double)Main.maxTilesX * 1.7)
			{
				WorldGen.PlantAlch();
				num281++;
			}
			Main.statusText = "Planting weeds...";
			WorldGen.AddPlants();
			for (int num282 = 0; num282 < Main.maxTilesX; num282++)
			{
				for (int num283 = 0; num283 < Main.maxTilesY; num283++)
				{
					if (Main.tile[num282, num283].active)
					{
						if (num283 >= (int)Main.worldSurface && Main.tile[num282, num283].type == 70 && !Main.tile[num282, num283 - 1].active)
						{
							WorldGen.GrowShroom(num282, num283);
							if (!Main.tile[num282, num283 - 1].active)
							{
								WorldGen.PlaceTile(num282, num283 - 1, 71, true, false, -1, 0);
							}
						}
						if (Main.tile[num282, num283].type == 60 && !Main.tile[num282, num283 - 1].active)
						{
							WorldGen.PlaceTile(num282, num283 - 1, 61, true, false, -1, 0);
						}
					}
				}
			}
			Main.statusText = "Growing vines...";
			for (int num284 = 0; num284 < Main.maxTilesX; num284++)
			{
				int num285 = 0;
				int num286 = 0;
				while ((double)num286 < Main.worldSurface)
				{
					if (num285 > 0 && !Main.tile[num284, num286].active)
					{
						Main.tile[num284, num286].active = true;
						Main.tile[num284, num286].type = 52;
						num285--;
					}
					else
					{
						num285 = 0;
					}
					if (Main.tile[num284, num286].active && Main.tile[num284, num286].type == 2 && WorldGen.genRand.Next(5) < 3)
					{
						num285 = WorldGen.genRand.Next(1, 10);
					}
					num286++;
				}
				num285 = 0;
				for (int num287 = 0; num287 < Main.maxTilesY; num287++)
				{
					if (num285 > 0 && !Main.tile[num284, num287].active)
					{
						Main.tile[num284, num287].active = true;
						Main.tile[num284, num287].type = 62;
						num285--;
					}
					else
					{
						num285 = 0;
					}
					if (Main.tile[num284, num287].active && Main.tile[num284, num287].type == 60 && WorldGen.genRand.Next(5) < 3)
					{
						num285 = WorldGen.genRand.Next(1, 10);
					}
				}
			}
			Main.statusText = "Planting flowers...";
			int num288 = 0;
			while ((double)num288 < (double)Main.maxTilesX * 0.005)
			{
				int num289 = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
				int num290 = WorldGen.genRand.Next(5, 15);
				int num291 = WorldGen.genRand.Next(15, 30);
				int num292 = 1;
				while ((double)num292 < Main.worldSurface - 1.0)
				{
					if (Main.tile[num289, num292].active)
					{
						for (int num293 = num289 - num290; num293 < num289 + num290; num293++)
						{
							for (int num294 = num292 - num291; num294 < num292 + num291; num294++)
							{
								if (Main.tile[num293, num294].type == 3 || Main.tile[num293, num294].type == 24)
								{
									Main.tile[num293, num294].frameX = (short)(WorldGen.genRand.Next(6, 8) * 18);
								}
							}
						}
						break;
					}
					num292++;
				}
				num288++;
			}
			Main.statusText = "Planting mushrooms...";
			int num295 = 0;
			while ((double)num295 < (double)Main.maxTilesX * 0.002)
			{
				int num296 = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
				int num297 = WorldGen.genRand.Next(4, 10);
				int num298 = WorldGen.genRand.Next(15, 30);
				int num299 = 1;
				while ((double)num299 < Main.worldSurface - 1.0)
				{
					if (Main.tile[num296, num299].active)
					{
						for (int num300 = num296 - num297; num300 < num296 + num297; num300++)
						{
							for (int num301 = num299 - num298; num301 < num299 + num298; num301++)
							{
								if (Main.tile[num300, num301].type == 3 || Main.tile[num300, num301].type == 24)
								{
									Main.tile[num300, num301].frameX = 144;
								}
							}
						}
						break;
					}
					num299++;
				}
				num295++;
			}
			WorldGen.gen = false;
		}
		public static bool GrowEpicTree(int i, int y)
		{
			int num = y;
			while (Main.tile[i, num].type == 20)
			{
				num++;
			}
			if (Main.tile[i, num].active && Main.tile[i, num].type == 2 && Main.tile[i, num - 1].wall == 0 && Main.tile[i, num - 1].liquid == 0 && Main.tile[i - 1, num].active && Main.tile[i - 1, num].type == 2 && Main.tile[i + 1, num].active && Main.tile[i + 1, num].type == 2)
			{
				int num2 = 2;
				if (WorldGen.EmptyTileCheck(i - num2, i + num2, num - 55, num - 1, 20))
				{
					bool flag = false;
					bool flag2 = false;
					int num3 = WorldGen.genRand.Next(20, 30);
					int num4;
					for (int j = num - num3; j < num; j++)
					{
						Main.tile[i, j].frameNumber = (byte)WorldGen.genRand.Next(3);
						Main.tile[i, j].active = true;
						Main.tile[i, j].type = 5;
						num4 = WorldGen.genRand.Next(3);
						int num5 = WorldGen.genRand.Next(10);
						if (j == num - 1 || j == num - num3)
						{
							num5 = 0;
						}
						while (((num5 == 5 || num5 == 7) && flag) || ((num5 == 6 || num5 == 7) && flag2))
						{
							num5 = WorldGen.genRand.Next(10);
						}
						flag = false;
						flag2 = false;
						if (num5 == 5 || num5 == 7)
						{
							flag = true;
						}
						if (num5 == 6 || num5 == 7)
						{
							flag2 = true;
						}
						if (num5 == 1)
						{
							if (num4 == 0)
							{
								Main.tile[i, j].frameX = 0;
								Main.tile[i, j].frameY = 66;
							}
							if (num4 == 1)
							{
								Main.tile[i, j].frameX = 0;
								Main.tile[i, j].frameY = 88;
							}
							if (num4 == 2)
							{
								Main.tile[i, j].frameX = 0;
								Main.tile[i, j].frameY = 110;
							}
						}
						else
						{
							if (num5 == 2)
							{
								if (num4 == 0)
								{
									Main.tile[i, j].frameX = 22;
									Main.tile[i, j].frameY = 0;
								}
								if (num4 == 1)
								{
									Main.tile[i, j].frameX = 22;
									Main.tile[i, j].frameY = 22;
								}
								if (num4 == 2)
								{
									Main.tile[i, j].frameX = 22;
									Main.tile[i, j].frameY = 44;
								}
							}
							else
							{
								if (num5 == 3)
								{
									if (num4 == 0)
									{
										Main.tile[i, j].frameX = 44;
										Main.tile[i, j].frameY = 66;
									}
									if (num4 == 1)
									{
										Main.tile[i, j].frameX = 44;
										Main.tile[i, j].frameY = 88;
									}
									if (num4 == 2)
									{
										Main.tile[i, j].frameX = 44;
										Main.tile[i, j].frameY = 110;
									}
								}
								else
								{
									if (num5 == 4)
									{
										if (num4 == 0)
										{
											Main.tile[i, j].frameX = 22;
											Main.tile[i, j].frameY = 66;
										}
										if (num4 == 1)
										{
											Main.tile[i, j].frameX = 22;
											Main.tile[i, j].frameY = 88;
										}
										if (num4 == 2)
										{
											Main.tile[i, j].frameX = 22;
											Main.tile[i, j].frameY = 110;
										}
									}
									else
									{
										if (num5 == 5)
										{
											if (num4 == 0)
											{
												Main.tile[i, j].frameX = 88;
												Main.tile[i, j].frameY = 0;
											}
											if (num4 == 1)
											{
												Main.tile[i, j].frameX = 88;
												Main.tile[i, j].frameY = 22;
											}
											if (num4 == 2)
											{
												Main.tile[i, j].frameX = 88;
												Main.tile[i, j].frameY = 44;
											}
										}
										else
										{
											if (num5 == 6)
											{
												if (num4 == 0)
												{
													Main.tile[i, j].frameX = 66;
													Main.tile[i, j].frameY = 66;
												}
												if (num4 == 1)
												{
													Main.tile[i, j].frameX = 66;
													Main.tile[i, j].frameY = 88;
												}
												if (num4 == 2)
												{
													Main.tile[i, j].frameX = 66;
													Main.tile[i, j].frameY = 110;
												}
											}
											else
											{
												if (num5 == 7)
												{
													if (num4 == 0)
													{
														Main.tile[i, j].frameX = 110;
														Main.tile[i, j].frameY = 66;
													}
													if (num4 == 1)
													{
														Main.tile[i, j].frameX = 110;
														Main.tile[i, j].frameY = 88;
													}
													if (num4 == 2)
													{
														Main.tile[i, j].frameX = 110;
														Main.tile[i, j].frameY = 110;
													}
												}
												else
												{
													if (num4 == 0)
													{
														Main.tile[i, j].frameX = 0;
														Main.tile[i, j].frameY = 0;
													}
													if (num4 == 1)
													{
														Main.tile[i, j].frameX = 0;
														Main.tile[i, j].frameY = 22;
													}
													if (num4 == 2)
													{
														Main.tile[i, j].frameX = 0;
														Main.tile[i, j].frameY = 44;
													}
												}
											}
										}
									}
								}
							}
						}
						if (num5 == 5 || num5 == 7)
						{
							Main.tile[i - 1, j].active = true;
							Main.tile[i - 1, j].type = 5;
							num4 = WorldGen.genRand.Next(3);
							if (WorldGen.genRand.Next(3) < 2)
							{
								if (num4 == 0)
								{
									Main.tile[i - 1, j].frameX = 44;
									Main.tile[i - 1, j].frameY = 198;
								}
								if (num4 == 1)
								{
									Main.tile[i - 1, j].frameX = 44;
									Main.tile[i - 1, j].frameY = 220;
								}
								if (num4 == 2)
								{
									Main.tile[i - 1, j].frameX = 44;
									Main.tile[i - 1, j].frameY = 242;
								}
							}
							else
							{
								if (num4 == 0)
								{
									Main.tile[i - 1, j].frameX = 66;
									Main.tile[i - 1, j].frameY = 0;
								}
								if (num4 == 1)
								{
									Main.tile[i - 1, j].frameX = 66;
									Main.tile[i - 1, j].frameY = 22;
								}
								if (num4 == 2)
								{
									Main.tile[i - 1, j].frameX = 66;
									Main.tile[i - 1, j].frameY = 44;
								}
							}
						}
						if (num5 == 6 || num5 == 7)
						{
							Main.tile[i + 1, j].active = true;
							Main.tile[i + 1, j].type = 5;
							num4 = WorldGen.genRand.Next(3);
							if (WorldGen.genRand.Next(3) < 2)
							{
								if (num4 == 0)
								{
									Main.tile[i + 1, j].frameX = 66;
									Main.tile[i + 1, j].frameY = 198;
								}
								if (num4 == 1)
								{
									Main.tile[i + 1, j].frameX = 66;
									Main.tile[i + 1, j].frameY = 220;
								}
								if (num4 == 2)
								{
									Main.tile[i + 1, j].frameX = 66;
									Main.tile[i + 1, j].frameY = 242;
								}
							}
							else
							{
								if (num4 == 0)
								{
									Main.tile[i + 1, j].frameX = 88;
									Main.tile[i + 1, j].frameY = 66;
								}
								if (num4 == 1)
								{
									Main.tile[i + 1, j].frameX = 88;
									Main.tile[i + 1, j].frameY = 88;
								}
								if (num4 == 2)
								{
									Main.tile[i + 1, j].frameX = 88;
									Main.tile[i + 1, j].frameY = 110;
								}
							}
						}
					}
					int num6 = WorldGen.genRand.Next(3);
					if (num6 == 0 || num6 == 1)
					{
						Main.tile[i + 1, num - 1].active = true;
						Main.tile[i + 1, num - 1].type = 5;
						num4 = WorldGen.genRand.Next(3);
						if (num4 == 0)
						{
							Main.tile[i + 1, num - 1].frameX = 22;
							Main.tile[i + 1, num - 1].frameY = 132;
						}
						if (num4 == 1)
						{
							Main.tile[i + 1, num - 1].frameX = 22;
							Main.tile[i + 1, num - 1].frameY = 154;
						}
						if (num4 == 2)
						{
							Main.tile[i + 1, num - 1].frameX = 22;
							Main.tile[i + 1, num - 1].frameY = 176;
						}
					}
					if (num6 == 0 || num6 == 2)
					{
						Main.tile[i - 1, num - 1].active = true;
						Main.tile[i - 1, num - 1].type = 5;
						num4 = WorldGen.genRand.Next(3);
						if (num4 == 0)
						{
							Main.tile[i - 1, num - 1].frameX = 44;
							Main.tile[i - 1, num - 1].frameY = 132;
						}
						if (num4 == 1)
						{
							Main.tile[i - 1, num - 1].frameX = 44;
							Main.tile[i - 1, num - 1].frameY = 154;
						}
						if (num4 == 2)
						{
							Main.tile[i - 1, num - 1].frameX = 44;
							Main.tile[i - 1, num - 1].frameY = 176;
						}
					}
					num4 = WorldGen.genRand.Next(3);
					if (num6 == 0)
					{
						if (num4 == 0)
						{
							Main.tile[i, num - 1].frameX = 88;
							Main.tile[i, num - 1].frameY = 132;
						}
						if (num4 == 1)
						{
							Main.tile[i, num - 1].frameX = 88;
							Main.tile[i, num - 1].frameY = 154;
						}
						if (num4 == 2)
						{
							Main.tile[i, num - 1].frameX = 88;
							Main.tile[i, num - 1].frameY = 176;
						}
					}
					else
					{
						if (num6 == 1)
						{
							if (num4 == 0)
							{
								Main.tile[i, num - 1].frameX = 0;
								Main.tile[i, num - 1].frameY = 132;
							}
							if (num4 == 1)
							{
								Main.tile[i, num - 1].frameX = 0;
								Main.tile[i, num - 1].frameY = 154;
							}
							if (num4 == 2)
							{
								Main.tile[i, num - 1].frameX = 0;
								Main.tile[i, num - 1].frameY = 176;
							}
						}
						else
						{
							if (num6 == 2)
							{
								if (num4 == 0)
								{
									Main.tile[i, num - 1].frameX = 66;
									Main.tile[i, num - 1].frameY = 132;
								}
								if (num4 == 1)
								{
									Main.tile[i, num - 1].frameX = 66;
									Main.tile[i, num - 1].frameY = 154;
								}
								if (num4 == 2)
								{
									Main.tile[i, num - 1].frameX = 66;
									Main.tile[i, num - 1].frameY = 176;
								}
							}
						}
					}
					if (WorldGen.genRand.Next(3) < 2)
					{
						num4 = WorldGen.genRand.Next(3);
						if (num4 == 0)
						{
							Main.tile[i, num - num3].frameX = 22;
							Main.tile[i, num - num3].frameY = 198;
						}
						if (num4 == 1)
						{
							Main.tile[i, num - num3].frameX = 22;
							Main.tile[i, num - num3].frameY = 220;
						}
						if (num4 == 2)
						{
							Main.tile[i, num - num3].frameX = 22;
							Main.tile[i, num - num3].frameY = 242;
						}
					}
					else
					{
						num4 = WorldGen.genRand.Next(3);
						if (num4 == 0)
						{
							Main.tile[i, num - num3].frameX = 0;
							Main.tile[i, num - num3].frameY = 198;
						}
						if (num4 == 1)
						{
							Main.tile[i, num - num3].frameX = 0;
							Main.tile[i, num - num3].frameY = 220;
						}
						if (num4 == 2)
						{
							Main.tile[i, num - num3].frameX = 0;
							Main.tile[i, num - num3].frameY = 242;
						}
					}
					WorldGen.RangeFrame(i - 2, num - num3 - 1, i + 2, num + 1);
					if (Main.netMode == 2)
					{
						NetMessage.SendTileSquare(-1, i, (int)((double)num - (double)num3 * 0.5), num3 + 1);
					}
					return true;
				}
			}
			return false;
		}
		public static void GrowTree(int i, int y)
		{
			int num = y;
			while (Main.tile[i, num].type == 20)
			{
				num++;
			}
			if ((Main.tile[i - 1, num - 1].liquid != 0 || Main.tile[i - 1, num - 1].liquid != 0 || Main.tile[i + 1, num - 1].liquid != 0) && Main.tile[i, num].type != 60)
			{
				return;
			}
			if (Main.tile[i, num].active && (Main.tile[i, num].type == 2 || Main.tile[i, num].type == 23 || Main.tile[i, num].type == 60) && Main.tile[i, num - 1].wall == 0 && Main.tile[i - 1, num].active && (Main.tile[i - 1, num].type == 2 || Main.tile[i - 1, num].type == 23 || Main.tile[i - 1, num].type == 60) && Main.tile[i + 1, num].active && (Main.tile[i + 1, num].type == 2 || Main.tile[i + 1, num].type == 23 || Main.tile[i + 1, num].type == 60))
			{
				int num2 = 2;
				if (WorldGen.EmptyTileCheck(i - num2, i + num2, num - 14, num - 1, 20))
				{
					bool flag = false;
					bool flag2 = false;
					int num3 = WorldGen.genRand.Next(5, 15);
					int num4;
					for (int j = num - num3; j < num; j++)
					{
						Main.tile[i, j].frameNumber = (byte)WorldGen.genRand.Next(3);
						Main.tile[i, j].active = true;
						Main.tile[i, j].type = 5;
						num4 = WorldGen.genRand.Next(3);
						int num5 = WorldGen.genRand.Next(10);
						if (j == num - 1 || j == num - num3)
						{
							num5 = 0;
						}
						while (((num5 == 5 || num5 == 7) && flag) || ((num5 == 6 || num5 == 7) && flag2))
						{
							num5 = WorldGen.genRand.Next(10);
						}
						flag = false;
						flag2 = false;
						if (num5 == 5 || num5 == 7)
						{
							flag = true;
						}
						if (num5 == 6 || num5 == 7)
						{
							flag2 = true;
						}
						if (num5 == 1)
						{
							if (num4 == 0)
							{
								Main.tile[i, j].frameX = 0;
								Main.tile[i, j].frameY = 66;
							}
							if (num4 == 1)
							{
								Main.tile[i, j].frameX = 0;
								Main.tile[i, j].frameY = 88;
							}
							if (num4 == 2)
							{
								Main.tile[i, j].frameX = 0;
								Main.tile[i, j].frameY = 110;
							}
						}
						else
						{
							if (num5 == 2)
							{
								if (num4 == 0)
								{
									Main.tile[i, j].frameX = 22;
									Main.tile[i, j].frameY = 0;
								}
								if (num4 == 1)
								{
									Main.tile[i, j].frameX = 22;
									Main.tile[i, j].frameY = 22;
								}
								if (num4 == 2)
								{
									Main.tile[i, j].frameX = 22;
									Main.tile[i, j].frameY = 44;
								}
							}
							else
							{
								if (num5 == 3)
								{
									if (num4 == 0)
									{
										Main.tile[i, j].frameX = 44;
										Main.tile[i, j].frameY = 66;
									}
									if (num4 == 1)
									{
										Main.tile[i, j].frameX = 44;
										Main.tile[i, j].frameY = 88;
									}
									if (num4 == 2)
									{
										Main.tile[i, j].frameX = 44;
										Main.tile[i, j].frameY = 110;
									}
								}
								else
								{
									if (num5 == 4)
									{
										if (num4 == 0)
										{
											Main.tile[i, j].frameX = 22;
											Main.tile[i, j].frameY = 66;
										}
										if (num4 == 1)
										{
											Main.tile[i, j].frameX = 22;
											Main.tile[i, j].frameY = 88;
										}
										if (num4 == 2)
										{
											Main.tile[i, j].frameX = 22;
											Main.tile[i, j].frameY = 110;
										}
									}
									else
									{
										if (num5 == 5)
										{
											if (num4 == 0)
											{
												Main.tile[i, j].frameX = 88;
												Main.tile[i, j].frameY = 0;
											}
											if (num4 == 1)
											{
												Main.tile[i, j].frameX = 88;
												Main.tile[i, j].frameY = 22;
											}
											if (num4 == 2)
											{
												Main.tile[i, j].frameX = 88;
												Main.tile[i, j].frameY = 44;
											}
										}
										else
										{
											if (num5 == 6)
											{
												if (num4 == 0)
												{
													Main.tile[i, j].frameX = 66;
													Main.tile[i, j].frameY = 66;
												}
												if (num4 == 1)
												{
													Main.tile[i, j].frameX = 66;
													Main.tile[i, j].frameY = 88;
												}
												if (num4 == 2)
												{
													Main.tile[i, j].frameX = 66;
													Main.tile[i, j].frameY = 110;
												}
											}
											else
											{
												if (num5 == 7)
												{
													if (num4 == 0)
													{
														Main.tile[i, j].frameX = 110;
														Main.tile[i, j].frameY = 66;
													}
													if (num4 == 1)
													{
														Main.tile[i, j].frameX = 110;
														Main.tile[i, j].frameY = 88;
													}
													if (num4 == 2)
													{
														Main.tile[i, j].frameX = 110;
														Main.tile[i, j].frameY = 110;
													}
												}
												else
												{
													if (num4 == 0)
													{
														Main.tile[i, j].frameX = 0;
														Main.tile[i, j].frameY = 0;
													}
													if (num4 == 1)
													{
														Main.tile[i, j].frameX = 0;
														Main.tile[i, j].frameY = 22;
													}
													if (num4 == 2)
													{
														Main.tile[i, j].frameX = 0;
														Main.tile[i, j].frameY = 44;
													}
												}
											}
										}
									}
								}
							}
						}
						if (num5 == 5 || num5 == 7)
						{
							Main.tile[i - 1, j].active = true;
							Main.tile[i - 1, j].type = 5;
							num4 = WorldGen.genRand.Next(3);
							if (WorldGen.genRand.Next(3) < 2)
							{
								if (num4 == 0)
								{
									Main.tile[i - 1, j].frameX = 44;
									Main.tile[i - 1, j].frameY = 198;
								}
								if (num4 == 1)
								{
									Main.tile[i - 1, j].frameX = 44;
									Main.tile[i - 1, j].frameY = 220;
								}
								if (num4 == 2)
								{
									Main.tile[i - 1, j].frameX = 44;
									Main.tile[i - 1, j].frameY = 242;
								}
							}
							else
							{
								if (num4 == 0)
								{
									Main.tile[i - 1, j].frameX = 66;
									Main.tile[i - 1, j].frameY = 0;
								}
								if (num4 == 1)
								{
									Main.tile[i - 1, j].frameX = 66;
									Main.tile[i - 1, j].frameY = 22;
								}
								if (num4 == 2)
								{
									Main.tile[i - 1, j].frameX = 66;
									Main.tile[i - 1, j].frameY = 44;
								}
							}
						}
						if (num5 == 6 || num5 == 7)
						{
							Main.tile[i + 1, j].active = true;
							Main.tile[i + 1, j].type = 5;
							num4 = WorldGen.genRand.Next(3);
							if (WorldGen.genRand.Next(3) < 2)
							{
								if (num4 == 0)
								{
									Main.tile[i + 1, j].frameX = 66;
									Main.tile[i + 1, j].frameY = 198;
								}
								if (num4 == 1)
								{
									Main.tile[i + 1, j].frameX = 66;
									Main.tile[i + 1, j].frameY = 220;
								}
								if (num4 == 2)
								{
									Main.tile[i + 1, j].frameX = 66;
									Main.tile[i + 1, j].frameY = 242;
								}
							}
							else
							{
								if (num4 == 0)
								{
									Main.tile[i + 1, j].frameX = 88;
									Main.tile[i + 1, j].frameY = 66;
								}
								if (num4 == 1)
								{
									Main.tile[i + 1, j].frameX = 88;
									Main.tile[i + 1, j].frameY = 88;
								}
								if (num4 == 2)
								{
									Main.tile[i + 1, j].frameX = 88;
									Main.tile[i + 1, j].frameY = 110;
								}
							}
						}
					}
					int num6 = WorldGen.genRand.Next(3);
					if (num6 == 0 || num6 == 1)
					{
						Main.tile[i + 1, num - 1].active = true;
						Main.tile[i + 1, num - 1].type = 5;
						num4 = WorldGen.genRand.Next(3);
						if (num4 == 0)
						{
							Main.tile[i + 1, num - 1].frameX = 22;
							Main.tile[i + 1, num - 1].frameY = 132;
						}
						if (num4 == 1)
						{
							Main.tile[i + 1, num - 1].frameX = 22;
							Main.tile[i + 1, num - 1].frameY = 154;
						}
						if (num4 == 2)
						{
							Main.tile[i + 1, num - 1].frameX = 22;
							Main.tile[i + 1, num - 1].frameY = 176;
						}
					}
					if (num6 == 0 || num6 == 2)
					{
						Main.tile[i - 1, num - 1].active = true;
						Main.tile[i - 1, num - 1].type = 5;
						num4 = WorldGen.genRand.Next(3);
						if (num4 == 0)
						{
							Main.tile[i - 1, num - 1].frameX = 44;
							Main.tile[i - 1, num - 1].frameY = 132;
						}
						if (num4 == 1)
						{
							Main.tile[i - 1, num - 1].frameX = 44;
							Main.tile[i - 1, num - 1].frameY = 154;
						}
						if (num4 == 2)
						{
							Main.tile[i - 1, num - 1].frameX = 44;
							Main.tile[i - 1, num - 1].frameY = 176;
						}
					}
					num4 = WorldGen.genRand.Next(3);
					if (num6 == 0)
					{
						if (num4 == 0)
						{
							Main.tile[i, num - 1].frameX = 88;
							Main.tile[i, num - 1].frameY = 132;
						}
						if (num4 == 1)
						{
							Main.tile[i, num - 1].frameX = 88;
							Main.tile[i, num - 1].frameY = 154;
						}
						if (num4 == 2)
						{
							Main.tile[i, num - 1].frameX = 88;
							Main.tile[i, num - 1].frameY = 176;
						}
					}
					else
					{
						if (num6 == 1)
						{
							if (num4 == 0)
							{
								Main.tile[i, num - 1].frameX = 0;
								Main.tile[i, num - 1].frameY = 132;
							}
							if (num4 == 1)
							{
								Main.tile[i, num - 1].frameX = 0;
								Main.tile[i, num - 1].frameY = 154;
							}
							if (num4 == 2)
							{
								Main.tile[i, num - 1].frameX = 0;
								Main.tile[i, num - 1].frameY = 176;
							}
						}
						else
						{
							if (num6 == 2)
							{
								if (num4 == 0)
								{
									Main.tile[i, num - 1].frameX = 66;
									Main.tile[i, num - 1].frameY = 132;
								}
								if (num4 == 1)
								{
									Main.tile[i, num - 1].frameX = 66;
									Main.tile[i, num - 1].frameY = 154;
								}
								if (num4 == 2)
								{
									Main.tile[i, num - 1].frameX = 66;
									Main.tile[i, num - 1].frameY = 176;
								}
							}
						}
					}
					if (WorldGen.genRand.Next(3) < 2)
					{
						num4 = WorldGen.genRand.Next(3);
						if (num4 == 0)
						{
							Main.tile[i, num - num3].frameX = 22;
							Main.tile[i, num - num3].frameY = 198;
						}
						if (num4 == 1)
						{
							Main.tile[i, num - num3].frameX = 22;
							Main.tile[i, num - num3].frameY = 220;
						}
						if (num4 == 2)
						{
							Main.tile[i, num - num3].frameX = 22;
							Main.tile[i, num - num3].frameY = 242;
						}
					}
					else
					{
						num4 = WorldGen.genRand.Next(3);
						if (num4 == 0)
						{
							Main.tile[i, num - num3].frameX = 0;
							Main.tile[i, num - num3].frameY = 198;
						}
						if (num4 == 1)
						{
							Main.tile[i, num - num3].frameX = 0;
							Main.tile[i, num - num3].frameY = 220;
						}
						if (num4 == 2)
						{
							Main.tile[i, num - num3].frameX = 0;
							Main.tile[i, num - num3].frameY = 242;
						}
					}
					WorldGen.RangeFrame(i - 2, num - num3 - 1, i + 2, num + 1);
					if (Main.netMode == 2)
					{
						NetMessage.SendTileSquare(-1, i, (int)((double)num - (double)num3 * 0.5), num3 + 1);
					}
				}
			}
		}
		public static void GrowShroom(int i, int y)
		{
			if (Main.tile[i - 1, y - 1].lava || Main.tile[i - 1, y - 1].lava || Main.tile[i + 1, y - 1].lava)
			{
				return;
			}
			if (Main.tile[i, y].active && Main.tile[i, y].type == 70 && Main.tile[i, y - 1].wall == 0 && Main.tile[i - 1, y].active && Main.tile[i - 1, y].type == 70 && Main.tile[i + 1, y].active && Main.tile[i + 1, y].type == 70 && WorldGen.EmptyTileCheck(i - 2, i + 2, y - 13, y - 1, 71))
			{
				int num = WorldGen.genRand.Next(4, 11);
				int num2;
				for (int j = y - num; j < y; j++)
				{
					Main.tile[i, j].frameNumber = (byte)WorldGen.genRand.Next(3);
					Main.tile[i, j].active = true;
					Main.tile[i, j].type = 72;
					num2 = WorldGen.genRand.Next(3);
					if (num2 == 0)
					{
						Main.tile[i, j].frameX = 0;
						Main.tile[i, j].frameY = 0;
					}
					if (num2 == 1)
					{
						Main.tile[i, j].frameX = 0;
						Main.tile[i, j].frameY = 18;
					}
					if (num2 == 2)
					{
						Main.tile[i, j].frameX = 0;
						Main.tile[i, j].frameY = 36;
					}
				}
				num2 = WorldGen.genRand.Next(3);
				if (num2 == 0)
				{
					Main.tile[i, y - num].frameX = 36;
					Main.tile[i, y - num].frameY = 0;
				}
				if (num2 == 1)
				{
					Main.tile[i, y - num].frameX = 36;
					Main.tile[i, y - num].frameY = 18;
				}
				if (num2 == 2)
				{
					Main.tile[i, y - num].frameX = 36;
					Main.tile[i, y - num].frameY = 36;
				}
				WorldGen.RangeFrame(i - 2, y - num - 1, i + 2, y + 1);
				if (Main.netMode == 2)
				{
					NetMessage.SendTileSquare(-1, i, (int)((double)y - (double)num * 0.5), num + 1);
				}
			}
		}
		public static void AddTrees()
		{
			for (int i = 1; i < Main.maxTilesX - 1; i++)
			{
				int num = 20;
				while ((double)num < Main.worldSurface)
				{
					WorldGen.GrowTree(i, num);
					num++;
				}
			}
		}
		public static bool EmptyTileCheck(int startX, int endX, int startY, int endY, int ignoreStyle = -1)
		{
			if (startX < 0)
			{
				return false;
			}
			if (endX >= Main.maxTilesX)
			{
				return false;
			}
			if (startY < 0)
			{
				return false;
			}
			if (endY >= Main.maxTilesY)
			{
				return false;
			}
			for (int i = startX; i < endX + 1; i++)
			{
				for (int j = startY; j < endY + 1; j++)
				{
					if (Main.tile[i, j].active)
					{
						if (ignoreStyle == -1)
						{
							return false;
						}
						if (ignoreStyle == 11 && Main.tile[i, j].type != 11)
						{
							return false;
						}
						if (ignoreStyle == 20 && Main.tile[i, j].type != 20 && Main.tile[i, j].type != 3 && Main.tile[i, j].type != 24 && Main.tile[i, j].type != 61 && Main.tile[i, j].type != 32 && Main.tile[i, j].type != 69 && Main.tile[i, j].type != 73 && Main.tile[i, j].type != 74)
						{
							return false;
						}
						if (ignoreStyle == 71 && Main.tile[i, j].type != 71)
						{
							return false;
						}
					}
				}
			}
			return true;
		}
		public static bool PlaceDoor(int i, int j, int type)
		{
			bool result;
			try
			{
				if (Main.tile[i, j - 2].active && Main.tileSolid[(int)Main.tile[i, j - 2].type] && Main.tile[i, j + 2].active && Main.tileSolid[(int)Main.tile[i, j + 2].type])
				{
					Main.tile[i, j - 1].active = true;
					Main.tile[i, j - 1].type = 10;
					Main.tile[i, j - 1].frameY = 0;
					Main.tile[i, j - 1].frameX = (short)(WorldGen.genRand.Next(3) * 18);
					Main.tile[i, j].active = true;
					Main.tile[i, j].type = 10;
					Main.tile[i, j].frameY = 18;
					Main.tile[i, j].frameX = (short)(WorldGen.genRand.Next(3) * 18);
					Main.tile[i, j + 1].active = true;
					Main.tile[i, j + 1].type = 10;
					Main.tile[i, j + 1].frameY = 36;
					Main.tile[i, j + 1].frameX = (short)(WorldGen.genRand.Next(3) * 18);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}
		public static bool CloseDoor(int i, int j, bool forced = false)
		{
			int num = 0;
			int num2 = i;
			int num3 = j;
			if (Main.tile[i, j] == null)
			{
				Main.tile[i, j] = new Tile();
			}
			int frameX = (int)Main.tile[i, j].frameX;
			int frameY = (int)Main.tile[i, j].frameY;
			if (frameX == 0)
			{
				num2 = i;
				num = 1;
			}
			else
			{
				if (frameX == 18)
				{
					num2 = i - 1;
					num = 1;
				}
				else
				{
					if (frameX == 36)
					{
						num2 = i + 1;
						num = -1;
					}
					else
					{
						if (frameX == 54)
						{
							num2 = i;
							num = -1;
						}
					}
				}
			}
			if (frameY == 0)
			{
				num3 = j;
			}
			else
			{
				if (frameY == 18)
				{
					num3 = j - 1;
				}
				else
				{
					if (frameY == 36)
					{
						num3 = j - 2;
					}
				}
			}
			int num4 = num2;
			if (num == -1)
			{
				num4 = num2 - 1;
			}
			if (!forced)
			{
				for (int k = num3; k < num3 + 3; k++)
				{
					if (!Collision.EmptyTile(num2, k, true))
					{
						return false;
					}
				}
			}
			for (int l = num4; l < num4 + 2; l++)
			{
				for (int m = num3; m < num3 + 3; m++)
				{
					if (l == num2)
					{
						if (Main.tile[l, m] == null)
						{
							Main.tile[l, m] = new Tile();
						}
						Main.tile[l, m].type = 10;
						Main.tile[l, m].frameX = (short)(WorldGen.genRand.Next(3) * 18);
					}
					else
					{
						if (Main.tile[l, m] == null)
						{
							Main.tile[l, m] = new Tile();
						}
						Main.tile[l, m].active = false;
					}
				}
			}
			for (int n = num2 - 1; n <= num2 + 1; n++)
			{
				for (int num5 = num3 - 1; num5 <= num3 + 2; num5++)
				{
					WorldGen.TileFrame(n, num5, false, false);
				}
			}
			Main.PlaySound(9, i * 16, j * 16, 1);
			return true;
		}
		public static bool AddLifeCrystal(int i, int j)
		{
			int k = j;
			while (k < Main.maxTilesY)
			{
				if (Main.tile[i, k].active && Main.tileSolid[(int)Main.tile[i, k].type])
				{
					int num = k - 1;
					if (Main.tile[i, num - 1].lava || Main.tile[i - 1, num - 1].lava)
					{
						return false;
					}
					if (!WorldGen.EmptyTileCheck(i - 1, i, num - 1, num, -1))
					{
						return false;
					}
					Main.tile[i - 1, num - 1].active = true;
					Main.tile[i - 1, num - 1].type = 12;
					Main.tile[i - 1, num - 1].frameX = 0;
					Main.tile[i - 1, num - 1].frameY = 0;
					Main.tile[i, num - 1].active = true;
					Main.tile[i, num - 1].type = 12;
					Main.tile[i, num - 1].frameX = 18;
					Main.tile[i, num - 1].frameY = 0;
					Main.tile[i - 1, num].active = true;
					Main.tile[i - 1, num].type = 12;
					Main.tile[i - 1, num].frameX = 0;
					Main.tile[i - 1, num].frameY = 18;
					Main.tile[i, num].active = true;
					Main.tile[i, num].type = 12;
					Main.tile[i, num].frameX = 18;
					Main.tile[i, num].frameY = 18;
					return true;
				}
				else
				{
					k++;
				}
			}
			return false;
		}
		public static void AddShadowOrb(int x, int y)
		{
			if (x < 10 || x > Main.maxTilesX - 10)
			{
				return;
			}
			if (y < 10 || y > Main.maxTilesY - 10)
			{
				return;
			}
			for (int i = x - 1; i < x + 1; i++)
			{
				for (int j = y - 1; j < y + 1; j++)
				{
					if (Main.tile[i, j].active && Main.tile[i, j].type == 31)
					{
						return;
					}
				}
			}
			Main.tile[x - 1, y - 1].active = true;
			Main.tile[x - 1, y - 1].type = 31;
			Main.tile[x - 1, y - 1].frameX = 0;
			Main.tile[x - 1, y - 1].frameY = 0;
			Main.tile[x, y - 1].active = true;
			Main.tile[x, y - 1].type = 31;
			Main.tile[x, y - 1].frameX = 18;
			Main.tile[x, y - 1].frameY = 0;
			Main.tile[x - 1, y].active = true;
			Main.tile[x - 1, y].type = 31;
			Main.tile[x - 1, y].frameX = 0;
			Main.tile[x - 1, y].frameY = 18;
			Main.tile[x, y].active = true;
			Main.tile[x, y].type = 31;
			Main.tile[x, y].frameX = 18;
			Main.tile[x, y].frameY = 18;
		}
		public static void AddHellHouses()
		{
			int num = (int)((double)Main.maxTilesX * 0.25);
			for (int i = num; i < Main.maxTilesX - num; i++)
			{
				int num2 = Main.maxTilesY - 40;
				while (Main.tile[i, num2].active || Main.tile[i, num2].liquid > 0)
				{
					num2--;
				}
				if (Main.tile[i, num2 + 1].active)
				{
					byte b = (byte)WorldGen.genRand.Next(75, 77);
					byte wall = 13;
					if (WorldGen.genRand.Next(5) > 0)
					{
						b = 75;
					}
					if (b == 75)
					{
						wall = 14;
					}
					WorldGen.HellHouse(i, num2, b, wall);
					i += WorldGen.genRand.Next(15, 80);
				}
			}
		}
		public static void HellHouse(int i, int j, byte type = 76, byte wall = 13)
		{
			int num = WorldGen.genRand.Next(8, 20);
			int num2 = WorldGen.genRand.Next(1, 3);
			int num3 = WorldGen.genRand.Next(4, 13);
			int num4 = j;
			for (int k = 0; k < num2; k++)
			{
				int num5 = WorldGen.genRand.Next(5, 9);
				WorldGen.HellRoom(i, num4, num, num5, type, wall);
				num4 -= num5;
			}
			num4 = j;
			for (int l = 0; l < num3; l++)
			{
				int num6 = WorldGen.genRand.Next(5, 9);
				num4 += num6;
				WorldGen.HellRoom(i, num4, num, num6, type, wall);
			}
			for (int m = i - num / 2; m <= i + num / 2; m++)
			{
				num4 = j;
				while (num4 < Main.maxTilesY && ((Main.tile[m, num4].active && (Main.tile[m, num4].type == 76 || Main.tile[m, num4].type == 75)) || Main.tile[i, num4].wall == 13 || Main.tile[i, num4].wall == 14))
				{
					num4++;
				}
				int num7 = 6 + WorldGen.genRand.Next(3);
				while (num4 < Main.maxTilesY && !Main.tile[m, num4].active)
				{
					num7--;
					Main.tile[m, num4].active = true;
					Main.tile[m, num4].type = 57;
					num4++;
					if (num7 <= 0)
					{
						break;
					}
				}
			}
			int num8 = 0;
			int num9 = 0;
			num4 = j;
			while (num4 < Main.maxTilesY && ((Main.tile[i, num4].active && (Main.tile[i, num4].type == 76 || Main.tile[i, num4].type == 75)) || Main.tile[i, num4].wall == 13 || Main.tile[i, num4].wall == 14))
			{
				num4++;
			}
			num4--;
			num9 = num4;
			while ((Main.tile[i, num4].active && (Main.tile[i, num4].type == 76 || Main.tile[i, num4].type == 75)) || Main.tile[i, num4].wall == 13 || Main.tile[i, num4].wall == 14)
			{
				num4--;
				if (Main.tile[i, num4].active && (Main.tile[i, num4].type == 76 || Main.tile[i, num4].type == 75))
				{
					int num10 = WorldGen.genRand.Next(i - num / 2 + 1, i + num / 2 - 1);
					int num11 = WorldGen.genRand.Next(i - num / 2 + 1, i + num / 2 - 1);
					if (num10 > num11)
					{
						int num12 = num10;
						num10 = num11;
						num11 = num12;
					}
					if (num10 == num11)
					{
						if (num10 < i)
						{
							num11++;
						}
						else
						{
							num10--;
						}
					}
					for (int n = num10; n <= num11; n++)
					{
						if (Main.tile[n, num4 - 1].wall == 13)
						{
							Main.tile[n, num4].wall = 13;
						}
						if (Main.tile[n, num4 - 1].wall == 14)
						{
							Main.tile[n, num4].wall = 14;
						}
						Main.tile[n, num4].type = 19;
						Main.tile[n, num4].active = true;
					}
					num4--;
				}
			}
			num8 = num4;
			float num13 = (float)((num9 - num8) * num);
			float num14 = num13 * 0.02f;
			int num15 = 0;
			while ((float)num15 < num14)
			{
				int num16 = WorldGen.genRand.Next(i - num / 2, i + num / 2 + 1);
				int num17 = WorldGen.genRand.Next(num8, num9);
				int num18 = WorldGen.genRand.Next(3, 8);
				for (int num19 = num16 - num18; num19 <= num16 + num18; num19++)
				{
					for (int num20 = num17 - num18; num20 <= num17 + num18; num20++)
					{
						float num21 = (float)Math.Abs(num19 - num16);
						float num22 = (float)Math.Abs(num20 - num17);
						double num23 = Math.Sqrt((double)(num21 * num21 + num22 * num22));
						if (num23 < (double)num18 * 0.4)
						{
							try
							{
								if (Main.tile[num19, num20].type == 76 || Main.tile[num19, num20].type == 19)
								{
									Main.tile[num19, num20].active = false;
								}
								Main.tile[num19, num20].wall = 0;
							}
							catch
							{
							}
						}
					}
				}
				num15++;
			}
		}
		public static void HellRoom(int i, int j, int width, int height, byte type = 76, byte wall = 13)
		{
			if (j > Main.maxTilesY - 40)
			{
				return;
			}
			for (int k = i - width / 2; k <= i + width / 2; k++)
			{
				for (int l = j - height; l <= j; l++)
				{
					try
					{
						Main.tile[k, l].active = true;
						Main.tile[k, l].type = type;
						Main.tile[k, l].liquid = 0;
						Main.tile[k, l].lava = false;
					}
					catch
					{
					}
				}
			}
			for (int m = i - width / 2 + 1; m <= i + width / 2 - 1; m++)
			{
				for (int n = j - height + 1; n <= j - 1; n++)
				{
					try
					{
						Main.tile[m, n].active = false;
						Main.tile[m, n].wall = wall;
						Main.tile[m, n].liquid = 0;
						Main.tile[m, n].lava = false;
					}
					catch
					{
					}
				}
			}
		}
		public static void MakeDungeon(int x, int y, int tileType = 41, int wallType = 7)
		{
			int num = WorldGen.genRand.Next(3);
			int num2 = WorldGen.genRand.Next(3);
			if (num == 1)
			{
				tileType = 43;
			}
			else
			{
				if (num == 2)
				{
					tileType = 44;
				}
			}
			if (num2 == 1)
			{
				wallType = 8;
			}
			else
			{
				if (num2 == 2)
				{
					wallType = 9;
				}
			}
			WorldGen.numDDoors = 0;
			WorldGen.numDPlats = 0;
			WorldGen.numDRooms = 0;
			WorldGen.dungeonX = x;
			WorldGen.dungeonY = y;
			WorldGen.dMinX = x;
			WorldGen.dMaxX = x;
			WorldGen.dMinY = y;
			WorldGen.dMaxY = y;
			WorldGen.dxStrength1 = (double)WorldGen.genRand.Next(25, 30);
			WorldGen.dyStrength1 = (double)WorldGen.genRand.Next(20, 25);
			WorldGen.dxStrength2 = (double)WorldGen.genRand.Next(35, 50);
			WorldGen.dyStrength2 = (double)WorldGen.genRand.Next(10, 15);
			float num3 = (float)(Main.maxTilesX / 60);
			num3 += (float)WorldGen.genRand.Next(0, (int)(num3 / 3f));
			float num4 = num3;
			int num5 = 5;
			WorldGen.DungeonRoom(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType);
			while (num3 > 0f)
			{
				if (WorldGen.dungeonX < WorldGen.dMinX)
				{
					WorldGen.dMinX = WorldGen.dungeonX;
				}
				if (WorldGen.dungeonX > WorldGen.dMaxX)
				{
					WorldGen.dMaxX = WorldGen.dungeonX;
				}
				if (WorldGen.dungeonY > WorldGen.dMaxY)
				{
					WorldGen.dMaxY = WorldGen.dungeonY;
				}
				num3 -= 1f;
				Main.statusText = "Creating dungeon: " + (int)((num4 - num3) / num4 * 60f) + "%";
				if (num5 > 0)
				{
					num5--;
				}
				if (num5 == 0 & WorldGen.genRand.Next(3) == 0)
				{
					num5 = 5;
					if (WorldGen.genRand.Next(2) == 0)
					{
						int num6 = WorldGen.dungeonX;
						int num7 = WorldGen.dungeonY;
						WorldGen.DungeonHalls(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType, false);
						if (WorldGen.genRand.Next(2) == 0)
						{
							WorldGen.DungeonHalls(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType, false);
						}
						WorldGen.DungeonRoom(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType);
						WorldGen.dungeonX = num6;
						WorldGen.dungeonY = num7;
					}
					else
					{
						WorldGen.DungeonRoom(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType);
					}
				}
				else
				{
					WorldGen.DungeonHalls(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType, false);
				}
			}
			WorldGen.DungeonRoom(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType);
			int num8 = WorldGen.dRoomX[0];
			int num9 = WorldGen.dRoomY[0];
			for (int i = 0; i < WorldGen.numDRooms; i++)
			{
				if (WorldGen.dRoomY[i] < num9)
				{
					num8 = WorldGen.dRoomX[i];
					num9 = WorldGen.dRoomY[i];
				}
			}
			WorldGen.dungeonX = num8;
			WorldGen.dungeonY = num9;
			WorldGen.dEnteranceX = num8;
			WorldGen.dSurface = false;
			num5 = 5;
			while (!WorldGen.dSurface)
			{
				if (num5 > 0)
				{
					num5--;
				}
				if ((num5 == 0 & WorldGen.genRand.Next(5) == 0) && (double)WorldGen.dungeonY > Main.worldSurface + 50.0)
				{
					num5 = 10;
					int num10 = WorldGen.dungeonX;
					int num11 = WorldGen.dungeonY;
					WorldGen.DungeonHalls(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType, true);
					WorldGen.DungeonRoom(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType);
					WorldGen.dungeonX = num10;
					WorldGen.dungeonY = num11;
				}
				WorldGen.DungeonStairs(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType);
			}
			WorldGen.DungeonEnt(WorldGen.dungeonX, WorldGen.dungeonY, tileType, wallType);
			Main.statusText = "Creating dungeon: 65%";
			for (int j = 0; j < WorldGen.numDRooms; j++)
			{
				for (int k = WorldGen.dRoomL[j]; k <= WorldGen.dRoomR[j]; k++)
				{
					if (!Main.tile[k, WorldGen.dRoomT[j] - 1].active)
					{
						WorldGen.DPlatX[WorldGen.numDPlats] = k;
						WorldGen.DPlatY[WorldGen.numDPlats] = WorldGen.dRoomT[j] - 1;
						WorldGen.numDPlats++;
						break;
					}
				}
				for (int l = WorldGen.dRoomL[j]; l <= WorldGen.dRoomR[j]; l++)
				{
					if (!Main.tile[l, WorldGen.dRoomB[j] + 1].active)
					{
						WorldGen.DPlatX[WorldGen.numDPlats] = l;
						WorldGen.DPlatY[WorldGen.numDPlats] = WorldGen.dRoomB[j] + 1;
						WorldGen.numDPlats++;
						break;
					}
				}
				for (int m = WorldGen.dRoomT[j]; m <= WorldGen.dRoomB[j]; m++)
				{
					if (!Main.tile[WorldGen.dRoomL[j] - 1, m].active)
					{
						WorldGen.DDoorX[WorldGen.numDDoors] = WorldGen.dRoomL[j] - 1;
						WorldGen.DDoorY[WorldGen.numDDoors] = m;
						WorldGen.DDoorPos[WorldGen.numDDoors] = -1;
						WorldGen.numDDoors++;
						break;
					}
				}
				for (int n = WorldGen.dRoomT[j]; n <= WorldGen.dRoomB[j]; n++)
				{
					if (!Main.tile[WorldGen.dRoomR[j] + 1, n].active)
					{
						WorldGen.DDoorX[WorldGen.numDDoors] = WorldGen.dRoomR[j] + 1;
						WorldGen.DDoorY[WorldGen.numDDoors] = n;
						WorldGen.DDoorPos[WorldGen.numDDoors] = 1;
						WorldGen.numDDoors++;
						break;
					}
				}
			}
			Main.statusText = "Creating dungeon: 70%";
			int num12 = 0;
			int num13 = 1000;
			int num14 = 0;
			while (num14 < Main.maxTilesX / 100)
			{
				num12++;
				int num15 = WorldGen.genRand.Next(WorldGen.dMinX, WorldGen.dMaxX);
				int num16 = WorldGen.genRand.Next((int)Main.worldSurface + 25, WorldGen.dMaxY);
				int num17 = num15;
				if ((int)Main.tile[num15, num16].wall == wallType && !Main.tile[num15, num16].active)
				{
					int num18 = 1;
					if (WorldGen.genRand.Next(2) == 0)
					{
						num18 = -1;
					}
					while (!Main.tile[num15, num16].active)
					{
						num16 += num18;
					}
					if (Main.tile[num15 - 1, num16].active && Main.tile[num15 + 1, num16].active && !Main.tile[num15 - 1, num16 - num18].active && !Main.tile[num15 + 1, num16 - num18].active)
					{
						num14++;
						int num19 = WorldGen.genRand.Next(5, 13);
						while (Main.tile[num15 - 1, num16].active && Main.tile[num15, num16 + num18].active && Main.tile[num15, num16].active && !Main.tile[num15, num16 - num18].active && num19 > 0)
						{
							Main.tile[num15, num16].type = 48;
							if (!Main.tile[num15 - 1, num16 - num18].active && !Main.tile[num15 + 1, num16 - num18].active)
							{
								Main.tile[num15, num16 - num18].type = 48;
								Main.tile[num15, num16 - num18].active = true;
							}
							num15--;
							num19--;
						}
						num19 = WorldGen.genRand.Next(5, 13);
						num15 = num17 + 1;
						while (Main.tile[num15 + 1, num16].active && Main.tile[num15, num16 + num18].active && Main.tile[num15, num16].active && !Main.tile[num15, num16 - num18].active && num19 > 0)
						{
							Main.tile[num15, num16].type = 48;
							if (!Main.tile[num15 - 1, num16 - num18].active && !Main.tile[num15 + 1, num16 - num18].active)
							{
								Main.tile[num15, num16 - num18].type = 48;
								Main.tile[num15, num16 - num18].active = true;
							}
							num15++;
							num19--;
						}
					}
				}
				if (num12 > num13)
				{
					num12 = 0;
					num14++;
				}
			}
			num12 = 0;
			num13 = 1000;
			num14 = 0;
			Main.statusText = "Creating dungeon: 75%";
			while (num14 < Main.maxTilesX / 100)
			{
				num12++;
				int num20 = WorldGen.genRand.Next(WorldGen.dMinX, WorldGen.dMaxX);
				int num21 = WorldGen.genRand.Next((int)Main.worldSurface + 25, WorldGen.dMaxY);
				int num22 = num21;
				if ((int)Main.tile[num20, num21].wall == wallType && !Main.tile[num20, num21].active)
				{
					int num23 = 1;
					if (WorldGen.genRand.Next(2) == 0)
					{
						num23 = -1;
					}
					while (num20 > 5 && num20 < Main.maxTilesX - 5 && !Main.tile[num20, num21].active)
					{
						num20 += num23;
					}
					if (Main.tile[num20, num21 - 1].active && Main.tile[num20, num21 + 1].active && !Main.tile[num20 - num23, num21 - 1].active && !Main.tile[num20 - num23, num21 + 1].active)
					{
						num14++;
						int num24 = WorldGen.genRand.Next(5, 13);
						while (Main.tile[num20, num21 - 1].active && Main.tile[num20 + num23, num21].active && Main.tile[num20, num21].active && !Main.tile[num20 - num23, num21].active && num24 > 0)
						{
							Main.tile[num20, num21].type = 48;
							if (!Main.tile[num20 - num23, num21 - 1].active && !Main.tile[num20 - num23, num21 + 1].active)
							{
								Main.tile[num20 - num23, num21].type = 48;
								Main.tile[num20 - num23, num21].active = true;
							}
							num21--;
							num24--;
						}
						num24 = WorldGen.genRand.Next(5, 13);
						num21 = num22 + 1;
						while (Main.tile[num20, num21 + 1].active && Main.tile[num20 + num23, num21].active && Main.tile[num20, num21].active && !Main.tile[num20 - num23, num21].active && num24 > 0)
						{
							Main.tile[num20, num21].type = 48;
							if (!Main.tile[num20 - num23, num21 - 1].active && !Main.tile[num20 - num23, num21 + 1].active)
							{
								Main.tile[num20 - num23, num21].type = 48;
								Main.tile[num20 - num23, num21].active = true;
							}
							num21++;
							num24--;
						}
					}
				}
				if (num12 > num13)
				{
					num12 = 0;
					num14++;
				}
			}
			Main.statusText = "Creating dungeon: 80%";
			for (int num25 = 0; num25 < WorldGen.numDDoors; num25++)
			{
				int num26 = WorldGen.DDoorX[num25] - 10;
				int num27 = WorldGen.DDoorX[num25] + 10;
				int num28 = 100;
				int num29 = 0;
				for (int num30 = num26; num30 < num27; num30++)
				{
					bool flag = true;
					int num31 = WorldGen.DDoorY[num25];
					while (!Main.tile[num30, num31].active)
					{
						num31--;
					}
					if (!Main.tileDungeon[(int)Main.tile[num30, num31].type])
					{
						flag = false;
					}
					int num32 = num31;
					num31 = WorldGen.DDoorY[num25];
					while (!Main.tile[num30, num31].active)
					{
						num31++;
					}
					if (!Main.tileDungeon[(int)Main.tile[num30, num31].type])
					{
						flag = false;
					}
					int num33 = num31;
					if (num33 - num32 >= 3)
					{
						int num34 = num30 - 20;
						int num35 = num30 + 20;
						int num36 = num33 - 10;
						int num37 = num33 + 10;
						for (int num38 = num34; num38 < num35; num38++)
						{
							for (int num39 = num36; num39 < num37; num39++)
							{
								if (Main.tile[num38, num39].active && Main.tile[num38, num39].type == 10)
								{
									flag = false;
									break;
								}
							}
						}
						if (flag)
						{
							for (int num40 = num33 - 3; num40 < num33; num40++)
							{
								for (int num41 = num30 - 3; num41 <= num30 + 3; num41++)
								{
									if (Main.tile[num41, num40].active)
									{
										flag = false;
										break;
									}
								}
							}
						}
						if (flag && num33 - num32 < 20)
						{
							bool flag2 = false;
							if (WorldGen.DDoorPos[num25] == 0 && num33 - num32 < num28)
							{
								flag2 = true;
							}
							if (WorldGen.DDoorPos[num25] == -1 && num30 > num29)
							{
								flag2 = true;
							}
							if (WorldGen.DDoorPos[num25] == 1 && (num30 < num29 || num29 == 0))
							{
								flag2 = true;
							}
							if (flag2)
							{
								num29 = num30;
								num28 = num33 - num32;
							}
						}
					}
				}
				if (num28 < 20)
				{
					int num42 = num29;
					int num43 = WorldGen.DDoorY[num25];
					int num44 = num43;
					while (!Main.tile[num42, num43].active)
					{
						Main.tile[num42, num43].active = false;
						num43++;
					}
					while (!Main.tile[num42, num44].active)
					{
						num44--;
					}
					num43--;
					num44++;
					for (int num45 = num44; num45 < num43 - 2; num45++)
					{
						Main.tile[num42, num45].active = true;
						Main.tile[num42, num45].type = (byte)tileType;
					}
					WorldGen.PlaceTile(num42, num43, 10, true, false, -1, 0);
					num42--;
					int num46 = num43 - 3;
					while (!Main.tile[num42, num46].active)
					{
						num46--;
					}
					if (num43 - num46 < num43 - num44 + 5 && Main.tileDungeon[(int)Main.tile[num42, num46].type])
					{
						for (int num47 = num43 - 4 - WorldGen.genRand.Next(3); num47 > num46; num47--)
						{
							Main.tile[num42, num47].active = true;
							Main.tile[num42, num47].type = (byte)tileType;
						}
					}
					num42 += 2;
					num46 = num43 - 3;
					while (!Main.tile[num42, num46].active)
					{
						num46--;
					}
					if (num43 - num46 < num43 - num44 + 5 && Main.tileDungeon[(int)Main.tile[num42, num46].type])
					{
						for (int num48 = num43 - 4 - WorldGen.genRand.Next(3); num48 > num46; num48--)
						{
							Main.tile[num42, num48].active = true;
							Main.tile[num42, num48].type = (byte)tileType;
						}
					}
					num43++;
					num42--;
					Main.tile[num42 - 1, num43].active = true;
					Main.tile[num42 - 1, num43].type = (byte)tileType;
					Main.tile[num42 + 1, num43].active = true;
					Main.tile[num42 + 1, num43].type = (byte)tileType;
				}
			}
			Main.statusText = "Creating dungeon: 85%";
			for (int num49 = 0; num49 < WorldGen.numDPlats; num49++)
			{
				int num50 = WorldGen.DPlatX[num49];
				int num51 = WorldGen.DPlatY[num49];
				int num52 = Main.maxTilesX;
				int num53 = 10;
				for (int num54 = num51 - 5; num54 <= num51 + 5; num54++)
				{
					int num55 = num50;
					int num56 = num50;
					bool flag3 = false;
					if (Main.tile[num55, num54].active)
					{
						flag3 = true;
					}
					else
					{
						while (!Main.tile[num55, num54].active)
						{
							num55--;
							if (!Main.tileDungeon[(int)Main.tile[num55, num54].type])
							{
								flag3 = true;
							}
						}
						while (!Main.tile[num56, num54].active)
						{
							num56++;
							if (!Main.tileDungeon[(int)Main.tile[num56, num54].type])
							{
								flag3 = true;
							}
						}
					}
					if (!flag3 && num56 - num55 <= num53)
					{
						bool flag4 = true;
						int num57 = num50 - num53 / 2 - 2;
						int num58 = num50 + num53 / 2 + 2;
						int num59 = num54 - 5;
						int num60 = num54 + 5;
						for (int num61 = num57; num61 <= num58; num61++)
						{
							for (int num62 = num59; num62 <= num60; num62++)
							{
								if (Main.tile[num61, num62].active && Main.tile[num61, num62].type == 19)
								{
									flag4 = false;
									break;
								}
							}
						}
						for (int num63 = num54 + 3; num63 >= num54 - 5; num63--)
						{
							if (Main.tile[num50, num63].active)
							{
								flag4 = false;
								break;
							}
						}
						if (flag4)
						{
							num52 = num54;
							break;
						}
					}
				}
				if (num52 > num51 - 10 && num52 < num51 + 10)
				{
					int num64 = num50;
					int num65 = num52;
					int num66 = num50 + 1;
					while (!Main.tile[num64, num65].active)
					{
						Main.tile[num64, num65].active = true;
						Main.tile[num64, num65].type = 19;
						num64--;
					}
					while (!Main.tile[num66, num65].active)
					{
						Main.tile[num66, num65].active = true;
						Main.tile[num66, num65].type = 19;
						num66++;
					}
				}
			}
			Main.statusText = "Creating dungeon: 90%";
			num12 = 0;
			num13 = 1000;
			num14 = 0;
			while (num14 < Main.maxTilesX / 20)
			{
				num12++;
				int num67 = WorldGen.genRand.Next(WorldGen.dMinX, WorldGen.dMaxX);
				int num68 = WorldGen.genRand.Next(WorldGen.dMinY, WorldGen.dMaxY);
				bool flag5 = true;
				if ((int)Main.tile[num67, num68].wall == wallType && !Main.tile[num67, num68].active)
				{
					int num69 = 1;
					if (WorldGen.genRand.Next(2) == 0)
					{
						num69 = -1;
					}
					while (flag5 && !Main.tile[num67, num68].active)
					{
						num67 -= num69;
						if (num67 < 5 || num67 > Main.maxTilesX - 5)
						{
							flag5 = false;
						}
						else
						{
							if (Main.tile[num67, num68].active && !Main.tileDungeon[(int)Main.tile[num67, num68].type])
							{
								flag5 = false;
							}
						}
					}
					if (flag5 && Main.tile[num67, num68].active && Main.tileDungeon[(int)Main.tile[num67, num68].type] && Main.tile[num67, num68 - 1].active && Main.tileDungeon[(int)Main.tile[num67, num68 - 1].type] && Main.tile[num67, num68 + 1].active && Main.tileDungeon[(int)Main.tile[num67, num68 + 1].type])
					{
						num67 += num69;
						for (int num70 = num67 - 3; num70 <= num67 + 3; num70++)
						{
							for (int num71 = num68 - 3; num71 <= num68 + 3; num71++)
							{
								if (Main.tile[num70, num71].active && Main.tile[num70, num71].type == 19)
								{
									flag5 = false;
									break;
								}
							}
						}
						if (flag5 && (!Main.tile[num67, num68 - 1].active & !Main.tile[num67, num68 - 2].active & !Main.tile[num67, num68 - 3].active))
						{
							int num72 = num67;
							int num73 = num67;
							while (num72 > WorldGen.dMinX && num72 < WorldGen.dMaxX && !Main.tile[num72, num68].active && !Main.tile[num72, num68 - 1].active && !Main.tile[num72, num68 + 1].active)
							{
								num72 += num69;
							}
							num72 = Math.Abs(num67 - num72);
							bool flag6 = false;
							if (WorldGen.genRand.Next(2) == 0)
							{
								flag6 = true;
							}
							if (num72 > 5)
							{
								for (int num74 = WorldGen.genRand.Next(1, 4); num74 > 0; num74--)
								{
									Main.tile[num67, num68].active = true;
									Main.tile[num67, num68].type = 19;
									if (flag6)
									{
										WorldGen.PlaceTile(num67, num68 - 1, 50, true, false, -1, 0);
										if (WorldGen.genRand.Next(50) == 0 && Main.tile[num67, num68 - 1].type == 50)
										{
											Main.tile[num67, num68 - 1].frameX = 90;
										}
									}
									num67 += num69;
								}
								num12 = 0;
								num14++;
								if (!flag6 && WorldGen.genRand.Next(2) == 0)
								{
									num67 = num73;
									num68--;
									int num75 = 0;
									if (WorldGen.genRand.Next(4) == 0)
									{
										num75 = 1;
									}
									if (num75 == 0)
									{
										num75 = 13;
									}
									else
									{
										if (num75 == 1)
										{
											num75 = 49;
										}
									}
									WorldGen.PlaceTile(num67, num68, num75, true, false, -1, 0);
									if (Main.tile[num67, num68].type == 13)
									{
										if (WorldGen.genRand.Next(2) == 0)
										{
											Main.tile[num67, num68].frameX = 18;
										}
										else
										{
											Main.tile[num67, num68].frameX = 36;
										}
									}
								}
							}
						}
					}
				}
				if (num12 > num13)
				{
					num12 = 0;
					num14++;
				}
			}
			Main.statusText = "Creating dungeon: 95%";
			int num76 = 0;
			for (int num77 = 0; num77 < WorldGen.numDRooms; num77++)
			{
				int num78 = 0;
				while (num78 < 1000)
				{
					int num79 = (int)((double)WorldGen.dRoomSize[num77] * 0.4);
					int i2 = WorldGen.dRoomX[num77] + WorldGen.genRand.Next(-num79, num79 + 1);
					int num80 = WorldGen.dRoomY[num77] + WorldGen.genRand.Next(-num79, num79 + 1);
					int num81 = 0;
					num76++;
					int style = 2;
					if (num76 == 1)
					{
						num81 = 329;
					}
					else
					{
						if (num76 == 2)
						{
							num81 = 155;
						}
						else
						{
							if (num76 == 3)
							{
								num81 = 156;
							}
							else
							{
								if (num76 == 4)
								{
									num81 = 157;
								}
								else
								{
									if (num76 == 5)
									{
										num81 = 163;
									}
									else
									{
										if (num76 == 6)
										{
											num81 = 113;
										}
										else
										{
											if (num76 == 7)
											{
												num81 = 327;
												style = 0;
											}
											else
											{
												num81 = 164;
												num76 = 0;
											}
										}
									}
								}
							}
						}
					}
					if ((double)num80 < Main.worldSurface + 50.0)
					{
						num81 = 327;
						style = 0;
					}
					if (num81 == 0 && WorldGen.genRand.Next(2) == 0)
					{
						num78 = 1000;
					}
					else
					{
						if (WorldGen.AddBuriedChest(i2, num80, num81, false, style))
						{
							num78 += 1000;
						}
						num78++;
					}
				}
			}
			WorldGen.dMinX -= 25;
			WorldGen.dMaxX += 25;
			WorldGen.dMinY -= 25;
			WorldGen.dMaxY += 25;
			if (WorldGen.dMinX < 0)
			{
				WorldGen.dMinX = 0;
			}
			if (WorldGen.dMaxX > Main.maxTilesX)
			{
				WorldGen.dMaxX = Main.maxTilesX;
			}
			if (WorldGen.dMinY < 0)
			{
				WorldGen.dMinY = 0;
			}
			if (WorldGen.dMaxY > Main.maxTilesY)
			{
				WorldGen.dMaxY = Main.maxTilesY;
			}
			num12 = 0;
			num13 = 1000;
			num14 = 0;
			while (num14 < Main.maxTilesX / 200)
			{
				num12++;
				int num82 = WorldGen.genRand.Next(WorldGen.dMinX, WorldGen.dMaxX);
				int num83 = WorldGen.genRand.Next(WorldGen.dMinY, WorldGen.dMaxY);
				if ((int)Main.tile[num82, num83].wall == wallType)
				{
					int num84 = num83;
					while (num84 > WorldGen.dMinY)
					{
						if (Main.tile[num82, num84 - 1].active && (int)Main.tile[num82, num84 - 1].type == tileType)
						{
							bool flag7 = false;
							for (int num85 = num82 - 15; num85 < num82 + 15; num85++)
							{
								for (int num86 = num84 - 15; num86 < num84 + 15; num86++)
								{
									if (num85 > 0 && num85 < Main.maxTilesX && num86 > 0 && num86 < Main.maxTilesY && Main.tile[num85, num86].type == 42)
									{
										flag7 = true;
										break;
									}
								}
							}
							if (Main.tile[num82 - 1, num84].active || Main.tile[num82 + 1, num84].active || Main.tile[num82 - 1, num84 + 1].active || Main.tile[num82 + 1, num84 + 1].active || Main.tile[num82, num84 + 2].active)
							{
								flag7 = true;
							}
							if (flag7)
							{
								break;
							}
							WorldGen.Place1x2Top(num82, num84, 42);
							if (Main.tile[num82, num84].type == 42)
							{
								num12 = 0;
								num14++;
								break;
							}
							break;
						}
						else
						{
							num84--;
						}
					}
				}
				if (num12 > num13)
				{
					num14++;
					num12 = 0;
				}
			}
		}
		public static void DungeonStairs(int i, int j, int tileType, int wallType)
		{
			Vector2 value = default(Vector2);
			double num = (double)WorldGen.genRand.Next(5, 9);
			int num2 = 1;
			Vector2 value2;
			value2.X = (float)i;
			value2.Y = (float)j;
			int k = WorldGen.genRand.Next(10, 30);
			if (i > WorldGen.dEnteranceX)
			{
				num2 = -1;
			}
			else
			{
				num2 = 1;
			}
			if (i > Main.maxTilesX - 400)
			{
				num2 = -1;
			}
			else
			{
				if (i < 400)
				{
					num2 = 1;
				}
			}
			value.Y = -1f;
			value.X = (float)num2;
			if (WorldGen.genRand.Next(3) == 0)
			{
				value.X *= 0.5f;
			}
			else
			{
				if (WorldGen.genRand.Next(3) == 0)
				{
					value.Y *= 2f;
				}
			}
			while (k > 0)
			{
				k--;
				int num3 = (int)((double)value2.X - num - 4.0 - (double)WorldGen.genRand.Next(6));
				int num4 = (int)((double)value2.X + num + 4.0 + (double)WorldGen.genRand.Next(6));
				int num5 = (int)((double)value2.Y - num - 4.0);
				int num6 = (int)((double)value2.Y + num + 4.0 + (double)WorldGen.genRand.Next(6));
				if (num3 < 0)
				{
					num3 = 0;
				}
				if (num4 > Main.maxTilesX)
				{
					num4 = Main.maxTilesX;
				}
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesY)
				{
					num6 = Main.maxTilesY;
				}
				int num7 = 1;
				if (value2.X > (float)(Main.maxTilesX / 2))
				{
					num7 = -1;
				}
				int num8 = (int)(value2.X + (float)WorldGen.dxStrength1 * 0.6f * (float)num7 + (float)WorldGen.dxStrength2 * (float)num7);
				int num9 = (int)(WorldGen.dyStrength2 * 0.5);
				if ((double)value2.Y < Main.worldSurface - 5.0 && Main.tile[num8, (int)((double)value2.Y - num - 6.0 + (double)num9)].wall == 0 && Main.tile[num8, (int)((double)value2.Y - num - 7.0 + (double)num9)].wall == 0 && Main.tile[num8, (int)((double)value2.Y - num - 8.0 + (double)num9)].wall == 0)
				{
					WorldGen.dSurface = true;
					WorldGen.TileRunner(num8, (int)((double)value2.Y - num - 6.0 + (double)num9), (double)WorldGen.genRand.Next(25, 35), WorldGen.genRand.Next(10, 20), -1, false, 0f, -1f, false, true);
				}
				for (int l = num3; l < num4; l++)
				{
					for (int m = num5; m < num6; m++)
					{
						Main.tile[l, m].liquid = 0;
						if ((int)Main.tile[l, m].wall != wallType)
						{
							Main.tile[l, m].wall = 0;
							Main.tile[l, m].active = true;
							Main.tile[l, m].type = (byte)tileType;
						}
					}
				}
				for (int n = num3 + 1; n < num4 - 1; n++)
				{
					for (int num10 = num5 + 1; num10 < num6 - 1; num10++)
					{
						WorldGen.PlaceWall(n, num10, wallType, true);
					}
				}
				int num11 = 0;
				if (WorldGen.genRand.Next((int)num) == 0)
				{
					num11 = WorldGen.genRand.Next(1, 3);
				}
				num3 = (int)((double)value2.X - num * 0.5 - (double)num11);
				num4 = (int)((double)value2.X + num * 0.5 + (double)num11);
				num5 = (int)((double)value2.Y - num * 0.5 - (double)num11);
				num6 = (int)((double)value2.Y + num * 0.5 + (double)num11);
				if (num3 < 0)
				{
					num3 = 0;
				}
				if (num4 > Main.maxTilesX)
				{
					num4 = Main.maxTilesX;
				}
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesY)
				{
					num6 = Main.maxTilesY;
				}
				for (int num12 = num3; num12 < num4; num12++)
				{
					for (int num13 = num5; num13 < num6; num13++)
					{
						Main.tile[num12, num13].active = false;
						WorldGen.PlaceWall(num12, num13, wallType, true);
					}
				}
				if (WorldGen.dSurface)
				{
					k = 0;
				}
				value2 += value;
			}
			WorldGen.dungeonX = (int)value2.X;
			WorldGen.dungeonY = (int)value2.Y;
		}
		public static void DungeonHalls(int i, int j, int tileType, int wallType, bool forceX = false)
		{
			Vector2 value = default(Vector2);
			double num = (double)WorldGen.genRand.Next(4, 6);
			Vector2 vector = default(Vector2);
			Vector2 value2 = default(Vector2);
			int num2 = 1;
			Vector2 value3;
			value3.X = (float)i;
			value3.Y = (float)j;
			int k = WorldGen.genRand.Next(35, 80);
			if (forceX)
			{
				k += 20;
				WorldGen.lastDungeonHall = default(Vector2);
			}
			else
			{
				if (WorldGen.genRand.Next(5) == 0)
				{
					num *= 2.0;
					k /= 2;
				}
			}
			bool flag = false;
			while (!flag)
			{
				if (WorldGen.genRand.Next(2) == 0)
				{
					num2 = -1;
				}
				else
				{
					num2 = 1;
				}
				bool flag2 = false;
				if (WorldGen.genRand.Next(2) == 0)
				{
					flag2 = true;
				}
				if (forceX)
				{
					flag2 = true;
				}
				if (flag2)
				{
					vector.Y = 0f;
					vector.X = (float)num2;
					value2.Y = 0f;
					value2.X = (float)(-(float)num2);
					value.Y = 0f;
					value.X = (float)num2;
					if (WorldGen.genRand.Next(3) == 0)
					{
						if (WorldGen.genRand.Next(2) == 0)
						{
							value.Y = -0.2f;
						}
						else
						{
							value.Y = 0.2f;
						}
					}
				}
				else
				{
					num += 1.0;
					value.Y = (float)num2;
					value.X = 0f;
					vector.X = 0f;
					vector.Y = (float)num2;
					value2.X = 0f;
					value2.Y = (float)(-(float)num2);
					if (WorldGen.genRand.Next(2) == 0)
					{
						if (WorldGen.genRand.Next(2) == 0)
						{
							value.X = 0.3f;
						}
						else
						{
							value.X = -0.3f;
						}
					}
					else
					{
						k /= 2;
					}
				}
				if (WorldGen.lastDungeonHall != value2)
				{
					flag = true;
				}
			}
			if (!forceX)
			{
				if (value3.X > (float)(WorldGen.lastMaxTilesX - 200))
				{
					num2 = -1;
					vector.Y = 0f;
					vector.X = (float)num2;
					value.Y = 0f;
					value.X = (float)num2;
					if (WorldGen.genRand.Next(3) == 0)
					{
						if (WorldGen.genRand.Next(2) == 0)
						{
							value.Y = -0.2f;
						}
						else
						{
							value.Y = 0.2f;
						}
					}
				}
				else
				{
					if (value3.X < 200f)
					{
						num2 = 1;
						vector.Y = 0f;
						vector.X = (float)num2;
						value.Y = 0f;
						value.X = (float)num2;
						if (WorldGen.genRand.Next(3) == 0)
						{
							if (WorldGen.genRand.Next(2) == 0)
							{
								value.Y = -0.2f;
							}
							else
							{
								value.Y = 0.2f;
							}
						}
					}
					else
					{
						if (value3.Y > (float)(WorldGen.lastMaxTilesY - 300))
						{
							num2 = -1;
							num += 1.0;
							value.Y = (float)num2;
							value.X = 0f;
							vector.X = 0f;
							vector.Y = (float)num2;
							if (WorldGen.genRand.Next(2) == 0)
							{
								if (WorldGen.genRand.Next(2) == 0)
								{
									value.X = 0.3f;
								}
								else
								{
									value.X = -0.3f;
								}
							}
						}
						else
						{
							if ((double)value3.Y < Main.rockLayer)
							{
								num2 = 1;
								num += 1.0;
								value.Y = (float)num2;
								value.X = 0f;
								vector.X = 0f;
								vector.Y = (float)num2;
								if (WorldGen.genRand.Next(2) == 0)
								{
									if (WorldGen.genRand.Next(2) == 0)
									{
										value.X = 0.3f;
									}
									else
									{
										value.X = -0.3f;
									}
								}
							}
							else
							{
								if (value3.X < (float)(Main.maxTilesX / 2) && (double)value3.X > (double)Main.maxTilesX * 0.25)
								{
									num2 = -1;
									vector.Y = 0f;
									vector.X = (float)num2;
									value.Y = 0f;
									value.X = (float)num2;
									if (WorldGen.genRand.Next(3) == 0)
									{
										if (WorldGen.genRand.Next(2) == 0)
										{
											value.Y = -0.2f;
										}
										else
										{
											value.Y = 0.2f;
										}
									}
								}
								else
								{
									if (value3.X > (float)(Main.maxTilesX / 2) && (double)value3.X < (double)Main.maxTilesX * 0.75)
									{
										num2 = 1;
										vector.Y = 0f;
										vector.X = (float)num2;
										value.Y = 0f;
										value.X = (float)num2;
										if (WorldGen.genRand.Next(3) == 0)
										{
											if (WorldGen.genRand.Next(2) == 0)
											{
												value.Y = -0.2f;
											}
											else
											{
												value.Y = 0.2f;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			if (vector.Y == 0f)
			{
				WorldGen.DDoorX[WorldGen.numDDoors] = (int)value3.X;
				WorldGen.DDoorY[WorldGen.numDDoors] = (int)value3.Y;
				WorldGen.DDoorPos[WorldGen.numDDoors] = 0;
				WorldGen.numDDoors++;
			}
			else
			{
				WorldGen.DPlatX[WorldGen.numDPlats] = (int)value3.X;
				WorldGen.DPlatY[WorldGen.numDPlats] = (int)value3.Y;
				WorldGen.numDPlats++;
			}
			WorldGen.lastDungeonHall = vector;
			while (k > 0)
			{
				if (vector.X > 0f && value3.X > (float)(Main.maxTilesX - 100))
				{
					k = 0;
				}
				else
				{
					if (vector.X < 0f && value3.X < 100f)
					{
						k = 0;
					}
					else
					{
						if (vector.Y > 0f && value3.Y > (float)(Main.maxTilesY - 100))
						{
							k = 0;
						}
						else
						{
							if (vector.Y < 0f && (double)value3.Y < Main.rockLayer + 50.0)
							{
								k = 0;
							}
						}
					}
				}
				k--;
				int num3 = (int)((double)value3.X - num - 4.0 - (double)WorldGen.genRand.Next(6));
				int num4 = (int)((double)value3.X + num + 4.0 + (double)WorldGen.genRand.Next(6));
				int num5 = (int)((double)value3.Y - num - 4.0 - (double)WorldGen.genRand.Next(6));
				int num6 = (int)((double)value3.Y + num + 4.0 + (double)WorldGen.genRand.Next(6));
				if (num3 < 0)
				{
					num3 = 0;
				}
				if (num4 > Main.maxTilesX)
				{
					num4 = Main.maxTilesX;
				}
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesY)
				{
					num6 = Main.maxTilesY;
				}
				for (int l = num3; l < num4; l++)
				{
					for (int m = num5; m < num6; m++)
					{
						Main.tile[l, m].liquid = 0;
						if (Main.tile[l, m].wall == 0)
						{
							Main.tile[l, m].active = true;
							Main.tile[l, m].type = (byte)tileType;
						}
					}
				}
				for (int n = num3 + 1; n < num4 - 1; n++)
				{
					for (int num7 = num5 + 1; num7 < num6 - 1; num7++)
					{
						WorldGen.PlaceWall(n, num7, wallType, true);
					}
				}
				int num8 = 0;
				if (value.Y == 0f && WorldGen.genRand.Next((int)num + 1) == 0)
				{
					num8 = WorldGen.genRand.Next(1, 3);
				}
				else
				{
					if (value.X == 0f && WorldGen.genRand.Next((int)num - 1) == 0)
					{
						num8 = WorldGen.genRand.Next(1, 3);
					}
					else
					{
						if (WorldGen.genRand.Next((int)num * 3) == 0)
						{
							num8 = WorldGen.genRand.Next(1, 3);
						}
					}
				}
				num3 = (int)((double)value3.X - num * 0.5 - (double)num8);
				num4 = (int)((double)value3.X + num * 0.5 + (double)num8);
				num5 = (int)((double)value3.Y - num * 0.5 - (double)num8);
				num6 = (int)((double)value3.Y + num * 0.5 + (double)num8);
				if (num3 < 0)
				{
					num3 = 0;
				}
				if (num4 > Main.maxTilesX)
				{
					num4 = Main.maxTilesX;
				}
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesY)
				{
					num6 = Main.maxTilesY;
				}
				for (int num9 = num3; num9 < num4; num9++)
				{
					for (int num10 = num5; num10 < num6; num10++)
					{
						Main.tile[num9, num10].active = false;
						Main.tile[num9, num10].wall = (byte)wallType;
					}
				}
				value3 += value;
			}
			WorldGen.dungeonX = (int)value3.X;
			WorldGen.dungeonY = (int)value3.Y;
			if (vector.Y == 0f)
			{
				WorldGen.DDoorX[WorldGen.numDDoors] = (int)value3.X;
				WorldGen.DDoorY[WorldGen.numDDoors] = (int)value3.Y;
				WorldGen.DDoorPos[WorldGen.numDDoors] = 0;
				WorldGen.numDDoors++;
				return;
			}
			WorldGen.DPlatX[WorldGen.numDPlats] = (int)value3.X;
			WorldGen.DPlatY[WorldGen.numDPlats] = (int)value3.Y;
			WorldGen.numDPlats++;
		}
		public static void DungeonRoom(int i, int j, int tileType, int wallType)
		{
			double num = (double)WorldGen.genRand.Next(15, 30);
			Vector2 value;
			value.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
			value.Y = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
			Vector2 value2;
			value2.X = (float)i;
			value2.Y = (float)j - (float)num / 2f;
			int k = WorldGen.genRand.Next(10, 20);
			double num2 = (double)value2.X;
			double num3 = (double)value2.X;
			double num4 = (double)value2.Y;
			double num5 = (double)value2.Y;
			while (k > 0)
			{
				k--;
				int num6 = (int)((double)value2.X - num * 0.800000011920929 - 5.0);
				int num7 = (int)((double)value2.X + num * 0.800000011920929 + 5.0);
				int num8 = (int)((double)value2.Y - num * 0.800000011920929 - 5.0);
				int num9 = (int)((double)value2.Y + num * 0.800000011920929 + 5.0);
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesX)
				{
					num7 = Main.maxTilesX;
				}
				if (num8 < 0)
				{
					num8 = 0;
				}
				if (num9 > Main.maxTilesY)
				{
					num9 = Main.maxTilesY;
				}
				for (int l = num6; l < num7; l++)
				{
					for (int m = num8; m < num9; m++)
					{
						Main.tile[l, m].liquid = 0;
						if (Main.tile[l, m].wall == 0)
						{
							Main.tile[l, m].active = true;
							Main.tile[l, m].type = (byte)tileType;
						}
					}
				}
				for (int n = num6 + 1; n < num7 - 1; n++)
				{
					for (int num10 = num8 + 1; num10 < num9 - 1; num10++)
					{
						WorldGen.PlaceWall(n, num10, wallType, true);
					}
				}
				num6 = (int)((double)value2.X - num * 0.5);
				num7 = (int)((double)value2.X + num * 0.5);
				num8 = (int)((double)value2.Y - num * 0.5);
				num9 = (int)((double)value2.Y + num * 0.5);
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesX)
				{
					num7 = Main.maxTilesX;
				}
				if (num8 < 0)
				{
					num8 = 0;
				}
				if (num9 > Main.maxTilesY)
				{
					num9 = Main.maxTilesY;
				}
				if ((double)num6 < num2)
				{
					num2 = (double)num6;
				}
				if ((double)num7 > num3)
				{
					num3 = (double)num7;
				}
				if ((double)num8 < num4)
				{
					num4 = (double)num8;
				}
				if ((double)num9 > num5)
				{
					num5 = (double)num9;
				}
				for (int num11 = num6; num11 < num7; num11++)
				{
					for (int num12 = num8; num12 < num9; num12++)
					{
						Main.tile[num11, num12].active = false;
						Main.tile[num11, num12].wall = (byte)wallType;
					}
				}
				value2 += value;
				value.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				value.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				if (value.X > 1f)
				{
					value.X = 1f;
				}
				if (value.X < -1f)
				{
					value.X = -1f;
				}
				if (value.Y > 1f)
				{
					value.Y = 1f;
				}
				if (value.Y < -1f)
				{
					value.Y = -1f;
				}
			}
			WorldGen.dRoomX[WorldGen.numDRooms] = (int)value2.X;
			WorldGen.dRoomY[WorldGen.numDRooms] = (int)value2.Y;
			WorldGen.dRoomSize[WorldGen.numDRooms] = (int)num;
			WorldGen.dRoomL[WorldGen.numDRooms] = (int)num2;
			WorldGen.dRoomR[WorldGen.numDRooms] = (int)num3;
			WorldGen.dRoomT[WorldGen.numDRooms] = (int)num4;
			WorldGen.dRoomB[WorldGen.numDRooms] = (int)num5;
			WorldGen.dRoomTreasure[WorldGen.numDRooms] = false;
			WorldGen.numDRooms++;
		}
		public static void DungeonEnt(int i, int j, int tileType, int wallType)
		{
			int num = 60;
			for (int k = i - num; k < i + num; k++)
			{
				for (int l = j - num; l < j + num; l++)
				{
					Main.tile[k, l].liquid = 0;
					Main.tile[k, l].lava = false;
				}
			}
			double num2 = WorldGen.dxStrength1;
			double num3 = WorldGen.dyStrength1;
			Vector2 vector;
			vector.X = (float)i;
			vector.Y = (float)j - (float)num3 / 2f;
			WorldGen.dMinY = (int)vector.Y;
			int num4 = 1;
			if (i > Main.maxTilesX / 2)
			{
				num4 = -1;
			}
			int num5 = (int)((double)vector.X - num2 * 0.60000002384185791 - (double)WorldGen.genRand.Next(2, 5));
			int num6 = (int)((double)vector.X + num2 * 0.60000002384185791 + (double)WorldGen.genRand.Next(2, 5));
			int num7 = (int)((double)vector.Y - num3 * 0.60000002384185791 - (double)WorldGen.genRand.Next(2, 5));
			int num8 = (int)((double)vector.Y + num3 * 0.60000002384185791 + (double)WorldGen.genRand.Next(8, 16));
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num6 > Main.maxTilesX)
			{
				num6 = Main.maxTilesX;
			}
			if (num7 < 0)
			{
				num7 = 0;
			}
			if (num8 > Main.maxTilesY)
			{
				num8 = Main.maxTilesY;
			}
			for (int m = num5; m < num6; m++)
			{
				for (int n = num7; n < num8; n++)
				{
					Main.tile[m, n].liquid = 0;
					if ((int)Main.tile[m, n].wall != wallType)
					{
						Main.tile[m, n].wall = 0;
						if (m > num5 + 1 && m < num6 - 2 && n > num7 + 1 && n < num8 - 2)
						{
							WorldGen.PlaceWall(m, n, wallType, true);
						}
						Main.tile[m, n].active = true;
						Main.tile[m, n].type = (byte)tileType;
					}
				}
			}
			int num9 = num5;
			int num10 = num5 + 5 + WorldGen.genRand.Next(4);
			int num11 = num7 - 3 - WorldGen.genRand.Next(3);
			int num12 = num7;
			for (int num13 = num9; num13 < num10; num13++)
			{
				for (int num14 = num11; num14 < num12; num14++)
				{
					if ((int)Main.tile[num13, num14].wall != wallType)
					{
						Main.tile[num13, num14].active = true;
						Main.tile[num13, num14].type = (byte)tileType;
					}
				}
			}
			num9 = num6 - 5 - WorldGen.genRand.Next(4);
			num10 = num6;
			num11 = num7 - 3 - WorldGen.genRand.Next(3);
			num12 = num7;
			for (int num15 = num9; num15 < num10; num15++)
			{
				for (int num16 = num11; num16 < num12; num16++)
				{
					if ((int)Main.tile[num15, num16].wall != wallType)
					{
						Main.tile[num15, num16].active = true;
						Main.tile[num15, num16].type = (byte)tileType;
					}
				}
			}
			int num17 = 1 + WorldGen.genRand.Next(2);
			int num18 = 2 + WorldGen.genRand.Next(4);
			int num19 = 0;
			for (int num20 = num5; num20 < num6; num20++)
			{
				for (int num21 = num7 - num17; num21 < num7; num21++)
				{
					if ((int)Main.tile[num20, num21].wall != wallType)
					{
						Main.tile[num20, num21].active = true;
						Main.tile[num20, num21].type = (byte)tileType;
					}
				}
				num19++;
				if (num19 >= num18)
				{
					num20 += num18;
					num19 = 0;
				}
			}
			for (int num22 = num5; num22 < num6; num22++)
			{
				for (int num23 = num8; num23 < num8 + 100; num23++)
				{
					WorldGen.PlaceWall(num22, num23, 2, true);
				}
			}
			num5 = (int)((double)vector.X - num2 * 0.60000002384185791);
			num6 = (int)((double)vector.X + num2 * 0.60000002384185791);
			num7 = (int)((double)vector.Y - num3 * 0.60000002384185791);
			num8 = (int)((double)vector.Y + num3 * 0.60000002384185791);
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num6 > Main.maxTilesX)
			{
				num6 = Main.maxTilesX;
			}
			if (num7 < 0)
			{
				num7 = 0;
			}
			if (num8 > Main.maxTilesY)
			{
				num8 = Main.maxTilesY;
			}
			for (int num24 = num5; num24 < num6; num24++)
			{
				for (int num25 = num7; num25 < num8; num25++)
				{
					WorldGen.PlaceWall(num24, num25, wallType, true);
				}
			}
			num5 = (int)((double)vector.X - num2 * 0.6 - 1.0);
			num6 = (int)((double)vector.X + num2 * 0.6 + 1.0);
			num7 = (int)((double)vector.Y - num3 * 0.6 - 1.0);
			num8 = (int)((double)vector.Y + num3 * 0.6 + 1.0);
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num6 > Main.maxTilesX)
			{
				num6 = Main.maxTilesX;
			}
			if (num7 < 0)
			{
				num7 = 0;
			}
			if (num8 > Main.maxTilesY)
			{
				num8 = Main.maxTilesY;
			}
			for (int num26 = num5; num26 < num6; num26++)
			{
				for (int num27 = num7; num27 < num8; num27++)
				{
					Main.tile[num26, num27].wall = (byte)wallType;
				}
			}
			num5 = (int)((double)vector.X - num2 * 0.5);
			num6 = (int)((double)vector.X + num2 * 0.5);
			num7 = (int)((double)vector.Y - num3 * 0.5);
			num8 = (int)((double)vector.Y + num3 * 0.5);
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num6 > Main.maxTilesX)
			{
				num6 = Main.maxTilesX;
			}
			if (num7 < 0)
			{
				num7 = 0;
			}
			if (num8 > Main.maxTilesY)
			{
				num8 = Main.maxTilesY;
			}
			for (int num28 = num5; num28 < num6; num28++)
			{
				for (int num29 = num7; num29 < num8; num29++)
				{
					Main.tile[num28, num29].active = false;
					Main.tile[num28, num29].wall = (byte)wallType;
				}
			}
			WorldGen.DPlatX[WorldGen.numDPlats] = (int)vector.X;
			WorldGen.DPlatY[WorldGen.numDPlats] = num8;
			WorldGen.numDPlats++;
			vector.X += (float)num2 * 0.6f * (float)num4;
			vector.Y += (float)num3 * 0.5f;
			num2 = WorldGen.dxStrength2;
			num3 = WorldGen.dyStrength2;
			vector.X += (float)num2 * 0.55f * (float)num4;
			vector.Y -= (float)num3 * 0.5f;
			num5 = (int)((double)vector.X - num2 * 0.60000002384185791 - (double)WorldGen.genRand.Next(1, 3));
			num6 = (int)((double)vector.X + num2 * 0.60000002384185791 + (double)WorldGen.genRand.Next(1, 3));
			num7 = (int)((double)vector.Y - num3 * 0.60000002384185791 - (double)WorldGen.genRand.Next(1, 3));
			num8 = (int)((double)vector.Y + num3 * 0.60000002384185791 + (double)WorldGen.genRand.Next(6, 16));
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num6 > Main.maxTilesX)
			{
				num6 = Main.maxTilesX;
			}
			if (num7 < 0)
			{
				num7 = 0;
			}
			if (num8 > Main.maxTilesY)
			{
				num8 = Main.maxTilesY;
			}
			for (int num30 = num5; num30 < num6; num30++)
			{
				for (int num31 = num7; num31 < num8; num31++)
				{
					if ((int)Main.tile[num30, num31].wall != wallType)
					{
						bool flag = true;
						if (num4 < 0)
						{
							if ((double)num30 < (double)vector.X - num2 * 0.5)
							{
								flag = false;
							}
						}
						else
						{
							if ((double)num30 > (double)vector.X + num2 * 0.5 - 1.0)
							{
								flag = false;
							}
						}
						if (flag)
						{
							Main.tile[num30, num31].wall = 0;
							Main.tile[num30, num31].active = true;
							Main.tile[num30, num31].type = (byte)tileType;
						}
					}
				}
			}
			for (int num32 = num5; num32 < num6; num32++)
			{
				for (int num33 = num8; num33 < num8 + 100; num33++)
				{
					WorldGen.PlaceWall(num32, num33, 2, true);
				}
			}
			num5 = (int)((double)vector.X - num2 * 0.5);
			num6 = (int)((double)vector.X + num2 * 0.5);
			num9 = num5;
			if (num4 < 0)
			{
				num9++;
			}
			num10 = num9 + 5 + WorldGen.genRand.Next(4);
			num11 = num7 - 3 - WorldGen.genRand.Next(3);
			num12 = num7;
			for (int num34 = num9; num34 < num10; num34++)
			{
				for (int num35 = num11; num35 < num12; num35++)
				{
					if ((int)Main.tile[num34, num35].wall != wallType)
					{
						Main.tile[num34, num35].active = true;
						Main.tile[num34, num35].type = (byte)tileType;
					}
				}
			}
			num9 = num6 - 5 - WorldGen.genRand.Next(4);
			num10 = num6;
			num11 = num7 - 3 - WorldGen.genRand.Next(3);
			num12 = num7;
			for (int num36 = num9; num36 < num10; num36++)
			{
				for (int num37 = num11; num37 < num12; num37++)
				{
					if ((int)Main.tile[num36, num37].wall != wallType)
					{
						Main.tile[num36, num37].active = true;
						Main.tile[num36, num37].type = (byte)tileType;
					}
				}
			}
			num17 = 1 + WorldGen.genRand.Next(2);
			num18 = 2 + WorldGen.genRand.Next(4);
			num19 = 0;
			if (num4 < 0)
			{
				num6++;
			}
			for (int num38 = num5 + 1; num38 < num6 - 1; num38++)
			{
				for (int num39 = num7 - num17; num39 < num7; num39++)
				{
					if ((int)Main.tile[num38, num39].wall != wallType)
					{
						Main.tile[num38, num39].active = true;
						Main.tile[num38, num39].type = (byte)tileType;
					}
				}
				num19++;
				if (num19 >= num18)
				{
					num38 += num18;
					num19 = 0;
				}
			}
			num5 = (int)((double)vector.X - num2 * 0.6);
			num6 = (int)((double)vector.X + num2 * 0.6);
			num7 = (int)((double)vector.Y - num3 * 0.6);
			num8 = (int)((double)vector.Y + num3 * 0.6);
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num6 > Main.maxTilesX)
			{
				num6 = Main.maxTilesX;
			}
			if (num7 < 0)
			{
				num7 = 0;
			}
			if (num8 > Main.maxTilesY)
			{
				num8 = Main.maxTilesY;
			}
			for (int num40 = num5; num40 < num6; num40++)
			{
				for (int num41 = num7; num41 < num8; num41++)
				{
					Main.tile[num40, num41].wall = 0;
				}
			}
			num5 = (int)((double)vector.X - num2 * 0.5);
			num6 = (int)((double)vector.X + num2 * 0.5);
			num7 = (int)((double)vector.Y - num3 * 0.5);
			num8 = (int)((double)vector.Y + num3 * 0.5);
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num6 > Main.maxTilesX)
			{
				num6 = Main.maxTilesX;
			}
			if (num7 < 0)
			{
				num7 = 0;
			}
			if (num8 > Main.maxTilesY)
			{
				num8 = Main.maxTilesY;
			}
			for (int num42 = num5; num42 < num6; num42++)
			{
				for (int num43 = num7; num43 < num8; num43++)
				{
					Main.tile[num42, num43].active = false;
					Main.tile[num42, num43].wall = 0;
				}
			}
			for (int num44 = num5; num44 < num6; num44++)
			{
				if (!Main.tile[num44, num8].active)
				{
					Main.tile[num44, num8].active = true;
					Main.tile[num44, num8].type = 19;
				}
			}
			Main.dungeonX = (int)vector.X;
			Main.dungeonY = num8;
			int num45 = NPC.NewNPC(Main.dungeonX * 16 + 8, Main.dungeonY * 16, 37, 0);
			Main.npc[num45].homeless = false;
			Main.npc[num45].homeTileX = Main.dungeonX;
			Main.npc[num45].homeTileY = Main.dungeonY;
			if (num4 == 1)
			{
				int num46 = 0;
				for (int num47 = num6; num47 < num6 + 25; num47++)
				{
					num46++;
					for (int num48 = num8 + num46; num48 < num8 + 25; num48++)
					{
						Main.tile[num47, num48].active = true;
						Main.tile[num47, num48].type = (byte)tileType;
					}
				}
			}
			else
			{
				int num49 = 0;
				for (int num50 = num5; num50 > num5 - 25; num50--)
				{
					num49++;
					for (int num51 = num8 + num49; num51 < num8 + 25; num51++)
					{
						Main.tile[num50, num51].active = true;
						Main.tile[num50, num51].type = (byte)tileType;
					}
				}
			}
			num17 = 1 + WorldGen.genRand.Next(2);
			num18 = 2 + WorldGen.genRand.Next(4);
			num19 = 0;
			num5 = (int)((double)vector.X - num2 * 0.5);
			num6 = (int)((double)vector.X + num2 * 0.5);
			num5 += 2;
			num6 -= 2;
			for (int num52 = num5; num52 < num6; num52++)
			{
				for (int num53 = num7; num53 < num8; num53++)
				{
					WorldGen.PlaceWall(num52, num53, wallType, true);
				}
				num19++;
				if (num19 >= num18)
				{
					num52 += num18 * 2;
					num19 = 0;
				}
			}
			vector.X -= (float)num2 * 0.6f * (float)num4;
			vector.Y += (float)num3 * 0.5f;
			num2 = 15.0;
			num3 = 3.0;
			vector.Y -= (float)num3 * 0.5f;
			num5 = (int)((double)vector.X - num2 * 0.5);
			num6 = (int)((double)vector.X + num2 * 0.5);
			num7 = (int)((double)vector.Y - num3 * 0.5);
			num8 = (int)((double)vector.Y + num3 * 0.5);
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num6 > Main.maxTilesX)
			{
				num6 = Main.maxTilesX;
			}
			if (num7 < 0)
			{
				num7 = 0;
			}
			if (num8 > Main.maxTilesY)
			{
				num8 = Main.maxTilesY;
			}
			for (int num54 = num5; num54 < num6; num54++)
			{
				for (int num55 = num7; num55 < num8; num55++)
				{
					Main.tile[num54, num55].active = false;
				}
			}
			if (num4 < 0)
			{
				vector.X -= 1f;
			}
			WorldGen.PlaceTile((int)vector.X, (int)vector.Y + 1, 10, false, false, -1, 0);
		}
		public static bool AddBuriedChest(int i, int j, int contain = 0, bool notNearOtherChests = false, int Style = -1)
		{
			if (WorldGen.genRand == null)
			{
				WorldGen.genRand = new Random((int)DateTime.Now.Ticks);
			}
			int k = j;
			while (k < Main.maxTilesY)
			{
				if (Main.tile[i, k].active && Main.tileSolid[(int)Main.tile[i, k].type])
				{
					bool flag = false;
					int num = k;
					int style = 0;
					if ((double)num >= Main.worldSurface + 25.0 || contain > 0)
					{
						style = 1;
					}
					if (Style >= 0)
					{
						style = Style;
					}
					if (num > Main.maxTilesY - 205 && contain == 0)
					{
						if (WorldGen.hellChest == 0)
						{
							contain = 274;
							style = 4;
							flag = true;
						}
						else
						{
							if (WorldGen.hellChest == 1)
							{
								contain = 220;
								style = 4;
								flag = true;
							}
							else
							{
								if (WorldGen.hellChest == 2)
								{
									contain = 112;
									style = 4;
									flag = true;
								}
								else
								{
									if (WorldGen.hellChest == 3)
									{
										contain = 218;
										style = 4;
										flag = true;
										WorldGen.hellChest = 0;
									}
								}
							}
						}
					}
					int num2 = WorldGen.PlaceChest(i - 1, num - 1, 21, notNearOtherChests, style);
					if (num2 >= 0)
					{
						if (flag)
						{
							WorldGen.hellChest++;
						}
						int num3 = 0;
						while (num3 == 0)
						{
							if ((double)num < Main.worldSurface + 25.0)
							{
								if (contain > 0)
								{
									Main.chest[num2].item[num3].SetDefaults(contain, false);
									num3++;
								}
								else
								{
									int num4 = WorldGen.genRand.Next(6);
									if (num4 == 0)
									{
										Main.chest[num2].item[num3].SetDefaults(280, false);
									}
									if (num4 == 1)
									{
										Main.chest[num2].item[num3].SetDefaults(281, false);
									}
									if (num4 == 2)
									{
										Main.chest[num2].item[num3].SetDefaults(284, false);
									}
									if (num4 == 3)
									{
										Main.chest[num2].item[num3].SetDefaults(282, false);
										Main.chest[num2].item[num3].stack = WorldGen.genRand.Next(50, 75);
									}
									if (num4 == 4)
									{
										Main.chest[num2].item[num3].SetDefaults(279, false);
										Main.chest[num2].item[num3].stack = WorldGen.genRand.Next(25, 50);
									}
									if (num4 == 5)
									{
										Main.chest[num2].item[num3].SetDefaults(285, false);
									}
									num3++;
								}
								if (WorldGen.genRand.Next(3) == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(168, false);
									Main.chest[num2].item[num3].stack = WorldGen.genRand.Next(3, 6);
									num3++;
								}
								if (WorldGen.genRand.Next(2) == 0)
								{
									int num5 = WorldGen.genRand.Next(2);
									int stack = WorldGen.genRand.Next(8) + 3;
									if (num5 == 0)
									{
										Main.chest[num2].item[num3].SetDefaults(20, false);
									}
									if (num5 == 1)
									{
										Main.chest[num2].item[num3].SetDefaults(22, false);
									}
									Main.chest[num2].item[num3].stack = stack;
									num3++;
								}
								if (WorldGen.genRand.Next(2) == 0)
								{
									int num6 = WorldGen.genRand.Next(2);
									int stack2 = WorldGen.genRand.Next(26) + 25;
									if (num6 == 0)
									{
										Main.chest[num2].item[num3].SetDefaults(40, false);
									}
									if (num6 == 1)
									{
										Main.chest[num2].item[num3].SetDefaults(42, false);
									}
									Main.chest[num2].item[num3].stack = stack2;
									num3++;
								}
								if (WorldGen.genRand.Next(2) == 0)
								{
									int num7 = WorldGen.genRand.Next(1);
									int stack3 = WorldGen.genRand.Next(3) + 3;
									if (num7 == 0)
									{
										Main.chest[num2].item[num3].SetDefaults(28, false);
									}
									Main.chest[num2].item[num3].stack = stack3;
									num3++;
								}
								if (WorldGen.genRand.Next(3) > 0)
								{
									int num8 = WorldGen.genRand.Next(4);
									int stack4 = WorldGen.genRand.Next(1, 3);
									if (num8 == 0)
									{
										Main.chest[num2].item[num3].SetDefaults(292, false);
									}
									if (num8 == 1)
									{
										Main.chest[num2].item[num3].SetDefaults(298, false);
									}
									if (num8 == 2)
									{
										Main.chest[num2].item[num3].SetDefaults(299, false);
									}
									if (num8 == 3)
									{
										Main.chest[num2].item[num3].SetDefaults(290, false);
									}
									Main.chest[num2].item[num3].stack = stack4;
									num3++;
								}
								if (WorldGen.genRand.Next(2) == 0)
								{
									int num9 = WorldGen.genRand.Next(2);
									int stack5 = WorldGen.genRand.Next(11) + 10;
									if (num9 == 0)
									{
										Main.chest[num2].item[num3].SetDefaults(8, false);
									}
									if (num9 == 1)
									{
										Main.chest[num2].item[num3].SetDefaults(31, false);
									}
									Main.chest[num2].item[num3].stack = stack5;
									num3++;
								}
								if (WorldGen.genRand.Next(2) == 0)
								{
									Main.chest[num2].item[num3].SetDefaults(72, false);
									Main.chest[num2].item[num3].stack = WorldGen.genRand.Next(10, 30);
									num3++;
								}
							}
							else
							{
								if ((double)num < Main.rockLayer)
								{
									if (contain > 0)
									{
										Main.chest[num2].item[num3].SetDefaults(contain, false);
										num3++;
									}
									else
									{
										int num10 = WorldGen.genRand.Next(7);
										if (num10 == 0)
										{
											Main.chest[num2].item[num3].SetDefaults(49, false);
										}
										if (num10 == 1)
										{
											Main.chest[num2].item[num3].SetDefaults(50, false);
										}
										if (num10 == 2)
										{
											Main.chest[num2].item[num3].SetDefaults(52, false);
										}
										if (num10 == 3)
										{
											Main.chest[num2].item[num3].SetDefaults(53, false);
										}
										if (num10 == 4)
										{
											Main.chest[num2].item[num3].SetDefaults(54, false);
										}
										if (num10 == 5)
										{
											Main.chest[num2].item[num3].SetDefaults(55, false);
										}
										if (num10 == 6)
										{
											Main.chest[num2].item[num3].SetDefaults(51, false);
											Main.chest[num2].item[num3].stack = WorldGen.genRand.Next(26) + 25;
										}
										num3++;
									}
									if (WorldGen.genRand.Next(3) == 0)
									{
										Main.chest[num2].item[num3].SetDefaults(166, false);
										Main.chest[num2].item[num3].stack = WorldGen.genRand.Next(10, 20);
										num3++;
									}
									if (WorldGen.genRand.Next(2) == 0)
									{
										int num11 = WorldGen.genRand.Next(2);
										int stack6 = WorldGen.genRand.Next(10) + 5;
										if (num11 == 0)
										{
											Main.chest[num2].item[num3].SetDefaults(22, false);
										}
										if (num11 == 1)
										{
											Main.chest[num2].item[num3].SetDefaults(21, false);
										}
										Main.chest[num2].item[num3].stack = stack6;
										num3++;
									}
									if (WorldGen.genRand.Next(2) == 0)
									{
										int num12 = WorldGen.genRand.Next(2);
										int stack7 = WorldGen.genRand.Next(25) + 25;
										if (num12 == 0)
										{
											Main.chest[num2].item[num3].SetDefaults(40, false);
										}
										if (num12 == 1)
										{
											Main.chest[num2].item[num3].SetDefaults(42, false);
										}
										Main.chest[num2].item[num3].stack = stack7;
										num3++;
									}
									if (WorldGen.genRand.Next(2) == 0)
									{
										int num13 = WorldGen.genRand.Next(1);
										int stack8 = WorldGen.genRand.Next(3) + 3;
										if (num13 == 0)
										{
											Main.chest[num2].item[num3].SetDefaults(28, false);
										}
										Main.chest[num2].item[num3].stack = stack8;
										num3++;
									}
									if (WorldGen.genRand.Next(3) > 0)
									{
										int num14 = WorldGen.genRand.Next(7);
										int stack9 = WorldGen.genRand.Next(1, 3);
										if (num14 == 0)
										{
											Main.chest[num2].item[num3].SetDefaults(289, false);
										}
										if (num14 == 1)
										{
											Main.chest[num2].item[num3].SetDefaults(298, false);
										}
										if (num14 == 2)
										{
											Main.chest[num2].item[num3].SetDefaults(299, false);
										}
										if (num14 == 3)
										{
											Main.chest[num2].item[num3].SetDefaults(290, false);
										}
										if (num14 == 4)
										{
											Main.chest[num2].item[num3].SetDefaults(303, false);
										}
										if (num14 == 5)
										{
											Main.chest[num2].item[num3].SetDefaults(291, false);
										}
										if (num14 == 6)
										{
											Main.chest[num2].item[num3].SetDefaults(304, false);
										}
										Main.chest[num2].item[num3].stack = stack9;
										num3++;
									}
									if (WorldGen.genRand.Next(2) == 0)
									{
										int stack10 = WorldGen.genRand.Next(11) + 10;
										Main.chest[num2].item[num3].SetDefaults(8, false);
										Main.chest[num2].item[num3].stack = stack10;
										num3++;
									}
									if (WorldGen.genRand.Next(2) == 0)
									{
										Main.chest[num2].item[num3].SetDefaults(72, false);
										Main.chest[num2].item[num3].stack = WorldGen.genRand.Next(50, 90);
										num3++;
									}
								}
								else
								{
									if (num < Main.maxTilesY - 250)
									{
										if (contain > 0)
										{
											Main.chest[num2].item[num3].SetDefaults(contain, false);
											num3++;
										}
										else
										{
											int num15 = WorldGen.genRand.Next(7);
											if (num15 == 2 && WorldGen.genRand.Next(2) == 0)
											{
												num15 = WorldGen.genRand.Next(7);
											}
											if (num15 == 0)
											{
												Main.chest[num2].item[num3].SetDefaults(49, false);
											}
											if (num15 == 1)
											{
												Main.chest[num2].item[num3].SetDefaults(50, false);
											}
											if (num15 == 2)
											{
												Main.chest[num2].item[num3].SetDefaults(52, false);
											}
											if (num15 == 3)
											{
												Main.chest[num2].item[num3].SetDefaults(53, false);
											}
											if (num15 == 4)
											{
												Main.chest[num2].item[num3].SetDefaults(54, false);
											}
											if (num15 == 5)
											{
												Main.chest[num2].item[num3].SetDefaults(55, false);
											}
											if (num15 == 6)
											{
												Main.chest[num2].item[num3].SetDefaults(51, false);
												Main.chest[num2].item[num3].stack = WorldGen.genRand.Next(26) + 25;
											}
											num3++;
										}
										if (WorldGen.genRand.Next(5) == 0)
										{
											Main.chest[num2].item[num3].SetDefaults(43, false);
											num3++;
										}
										if (WorldGen.genRand.Next(3) == 0)
										{
											Main.chest[num2].item[num3].SetDefaults(167, false);
											num3++;
										}
										if (WorldGen.genRand.Next(2) == 0)
										{
											int num16 = WorldGen.genRand.Next(2);
											int stack11 = WorldGen.genRand.Next(8) + 3;
											if (num16 == 0)
											{
												Main.chest[num2].item[num3].SetDefaults(19, false);
											}
											if (num16 == 1)
											{
												Main.chest[num2].item[num3].SetDefaults(21, false);
											}
											Main.chest[num2].item[num3].stack = stack11;
											num3++;
										}
										if (WorldGen.genRand.Next(2) == 0)
										{
											int num17 = WorldGen.genRand.Next(2);
											int stack12 = WorldGen.genRand.Next(26) + 25;
											if (num17 == 0)
											{
												Main.chest[num2].item[num3].SetDefaults(41, false);
											}
											if (num17 == 1)
											{
												Main.chest[num2].item[num3].SetDefaults(279, false);
											}
											Main.chest[num2].item[num3].stack = stack12;
											num3++;
										}
										if (WorldGen.genRand.Next(2) == 0)
										{
											int num18 = WorldGen.genRand.Next(1);
											int stack13 = WorldGen.genRand.Next(3) + 3;
											if (num18 == 0)
											{
												Main.chest[num2].item[num3].SetDefaults(188, false);
											}
											Main.chest[num2].item[num3].stack = stack13;
											num3++;
										}
										if (WorldGen.genRand.Next(3) > 0)
										{
											int num19 = WorldGen.genRand.Next(6);
											int stack14 = WorldGen.genRand.Next(1, 3);
											if (num19 == 0)
											{
												Main.chest[num2].item[num3].SetDefaults(296, false);
											}
											if (num19 == 1)
											{
												Main.chest[num2].item[num3].SetDefaults(295, false);
											}
											if (num19 == 2)
											{
												Main.chest[num2].item[num3].SetDefaults(299, false);
											}
											if (num19 == 3)
											{
												Main.chest[num2].item[num3].SetDefaults(302, false);
											}
											if (num19 == 4)
											{
												Main.chest[num2].item[num3].SetDefaults(303, false);
											}
											if (num19 == 5)
											{
												Main.chest[num2].item[num3].SetDefaults(305, false);
											}
											Main.chest[num2].item[num3].stack = stack14;
											num3++;
										}
										if (WorldGen.genRand.Next(3) > 1)
										{
											int num20 = WorldGen.genRand.Next(4);
											int stack15 = WorldGen.genRand.Next(1, 3);
											if (num20 == 0)
											{
												Main.chest[num2].item[num3].SetDefaults(301, false);
											}
											if (num20 == 1)
											{
												Main.chest[num2].item[num3].SetDefaults(302, false);
											}
											if (num20 == 2)
											{
												Main.chest[num2].item[num3].SetDefaults(297, false);
											}
											if (num20 == 3)
											{
												Main.chest[num2].item[num3].SetDefaults(304, false);
											}
											Main.chest[num2].item[num3].stack = stack15;
											num3++;
										}
										if (WorldGen.genRand.Next(2) == 0)
										{
											int num21 = WorldGen.genRand.Next(2);
											int stack16 = WorldGen.genRand.Next(15) + 15;
											if (num21 == 0)
											{
												Main.chest[num2].item[num3].SetDefaults(8, false);
											}
											if (num21 == 1)
											{
												Main.chest[num2].item[num3].SetDefaults(282, false);
											}
											Main.chest[num2].item[num3].stack = stack16;
											num3++;
										}
										if (WorldGen.genRand.Next(2) == 0)
										{
											Main.chest[num2].item[num3].SetDefaults(73, false);
											Main.chest[num2].item[num3].stack = WorldGen.genRand.Next(1, 3);
											num3++;
										}
									}
									else
									{
										if (contain > 0)
										{
											Main.chest[num2].item[num3].SetDefaults(contain, false);
											num3++;
										}
										else
										{
											int num22 = WorldGen.genRand.Next(4);
											if (num22 == 0)
											{
												Main.chest[num2].item[num3].SetDefaults(49, false);
											}
											if (num22 == 1)
											{
												Main.chest[num2].item[num3].SetDefaults(50, false);
											}
											if (num22 == 2)
											{
												Main.chest[num2].item[num3].SetDefaults(53, false);
											}
											if (num22 == 3)
											{
												Main.chest[num2].item[num3].SetDefaults(54, false);
											}
											num3++;
										}
										if (WorldGen.genRand.Next(3) == 0)
										{
											Main.chest[num2].item[num3].SetDefaults(167, false);
											num3++;
										}
										if (WorldGen.genRand.Next(2) == 0)
										{
											int num23 = WorldGen.genRand.Next(2);
											int stack17 = WorldGen.genRand.Next(15) + 15;
											if (num23 == 0)
											{
												Main.chest[num2].item[num3].SetDefaults(117, false);
											}
											if (num23 == 1)
											{
												Main.chest[num2].item[num3].SetDefaults(19, false);
											}
											Main.chest[num2].item[num3].stack = stack17;
											num3++;
										}
										if (WorldGen.genRand.Next(2) == 0)
										{
											int num24 = WorldGen.genRand.Next(2);
											int stack18 = WorldGen.genRand.Next(25) + 50;
											if (num24 == 0)
											{
												Main.chest[num2].item[num3].SetDefaults(265, false);
											}
											if (num24 == 1)
											{
												Main.chest[num2].item[num3].SetDefaults(278, false);
											}
											Main.chest[num2].item[num3].stack = stack18;
											num3++;
										}
										if (WorldGen.genRand.Next(2) == 0)
										{
											int num25 = WorldGen.genRand.Next(2);
											int stack19 = WorldGen.genRand.Next(15) + 15;
											if (num25 == 0)
											{
												Main.chest[num2].item[num3].SetDefaults(226, false);
											}
											if (num25 == 1)
											{
												Main.chest[num2].item[num3].SetDefaults(227, false);
											}
											Main.chest[num2].item[num3].stack = stack19;
											num3++;
										}
										if (WorldGen.genRand.Next(4) > 0)
										{
											int num26 = WorldGen.genRand.Next(7);
											int stack20 = WorldGen.genRand.Next(1, 3);
											if (num26 == 0)
											{
												Main.chest[num2].item[num3].SetDefaults(296, false);
											}
											if (num26 == 1)
											{
												Main.chest[num2].item[num3].SetDefaults(295, false);
											}
											if (num26 == 2)
											{
												Main.chest[num2].item[num3].SetDefaults(293, false);
											}
											if (num26 == 3)
											{
												Main.chest[num2].item[num3].SetDefaults(288, false);
											}
											if (num26 == 4)
											{
												Main.chest[num2].item[num3].SetDefaults(294, false);
											}
											if (num26 == 5)
											{
												Main.chest[num2].item[num3].SetDefaults(297, false);
											}
											if (num26 == 6)
											{
												Main.chest[num2].item[num3].SetDefaults(304, false);
											}
											Main.chest[num2].item[num3].stack = stack20;
											num3++;
										}
										if (WorldGen.genRand.Next(3) > 0)
										{
											int num27 = WorldGen.genRand.Next(5);
											int stack21 = WorldGen.genRand.Next(1, 3);
											if (num27 == 0)
											{
												Main.chest[num2].item[num3].SetDefaults(305, false);
											}
											if (num27 == 1)
											{
												Main.chest[num2].item[num3].SetDefaults(301, false);
											}
											if (num27 == 2)
											{
												Main.chest[num2].item[num3].SetDefaults(302, false);
											}
											if (num27 == 3)
											{
												Main.chest[num2].item[num3].SetDefaults(288, false);
											}
											if (num27 == 4)
											{
												Main.chest[num2].item[num3].SetDefaults(300, false);
											}
											Main.chest[num2].item[num3].stack = stack21;
											num3++;
										}
										if (WorldGen.genRand.Next(2) == 0)
										{
											int num28 = WorldGen.genRand.Next(2);
											int stack22 = WorldGen.genRand.Next(15) + 15;
											if (num28 == 0)
											{
												Main.chest[num2].item[num3].SetDefaults(8, false);
											}
											if (num28 == 1)
											{
												Main.chest[num2].item[num3].SetDefaults(282, false);
											}
											Main.chest[num2].item[num3].stack = stack22;
											num3++;
										}
										if (WorldGen.genRand.Next(2) == 0)
										{
											Main.chest[num2].item[num3].SetDefaults(73, false);
											Main.chest[num2].item[num3].stack = WorldGen.genRand.Next(2, 5);
											num3++;
										}
									}
								}
							}
						}
						return true;
					}
					return false;
				}
				else
				{
					k++;
				}
			}
			return false;
		}
		public static bool OpenDoor(int i, int j, int direction)
		{
			int num = 0;
			if (Main.tile[i, j - 1] == null)
			{
				Main.tile[i, j - 1] = new Tile();
			}
			if (Main.tile[i, j - 2] == null)
			{
				Main.tile[i, j - 2] = new Tile();
			}
			if (Main.tile[i, j + 1] == null)
			{
				Main.tile[i, j + 1] = new Tile();
			}
			if (Main.tile[i, j] == null)
			{
				Main.tile[i, j] = new Tile();
			}
			if (Main.tile[i, j - 1].frameY == 0 && Main.tile[i, j - 1].type == Main.tile[i, j].type)
			{
				num = j - 1;
			}
			else
			{
				if (Main.tile[i, j - 2].frameY == 0 && Main.tile[i, j - 2].type == Main.tile[i, j].type)
				{
					num = j - 2;
				}
				else
				{
					if (Main.tile[i, j + 1].frameY == 0 && Main.tile[i, j + 1].type == Main.tile[i, j].type)
					{
						num = j + 1;
					}
					else
					{
						num = j;
					}
				}
			}
			int num2 = i;
			short num3 = 0;
			int num4;
			if (direction == -1)
			{
				num2 = i - 1;
				num3 = 36;
				num4 = i - 1;
			}
			else
			{
				num2 = i;
				num4 = i + 1;
			}
			bool flag = true;
			for (int k = num; k < num + 3; k++)
			{
				if (Main.tile[num4, k] == null)
				{
					Main.tile[num4, k] = new Tile();
				}
				if (Main.tile[num4, k].active)
				{
					if (Main.tile[num4, k].type != 3 && Main.tile[num4, k].type != 24 && Main.tile[num4, k].type != 52 && Main.tile[num4, k].type != 61 && Main.tile[num4, k].type != 62 && Main.tile[num4, k].type != 69 && Main.tile[num4, k].type != 71 && Main.tile[num4, k].type != 73 && Main.tile[num4, k].type != 74)
					{
						flag = false;
						break;
					}
					WorldGen.KillTile(num4, k, false, false, false);
				}
			}
			if (flag)
			{
				Main.PlaySound(8, i * 16, j * 16, 1);
				Main.tile[num2, num].active = true;
				Main.tile[num2, num].type = 11;
				Main.tile[num2, num].frameY = 0;
				Main.tile[num2, num].frameX = num3;
				if (Main.tile[num2 + 1, num] == null)
				{
					Main.tile[num2 + 1, num] = new Tile();
				}
				Main.tile[num2 + 1, num].active = true;
				Main.tile[num2 + 1, num].type = 11;
				Main.tile[num2 + 1, num].frameY = 0;
                Main.tile[num2 + 1, num].frameX = (short)(num3 + 18);
				if (Main.tile[num2, num + 1] == null)
				{
					Main.tile[num2, num + 1] = new Tile();
				}
				Main.tile[num2, num + 1].active = true;
				Main.tile[num2, num + 1].type = 11;
				Main.tile[num2, num + 1].frameY = 18;
				Main.tile[num2, num + 1].frameX = num3;
				if (Main.tile[num2 + 1, num + 1] == null)
				{
					Main.tile[num2 + 1, num + 1] = new Tile();
				}
				Main.tile[num2 + 1, num + 1].active = true;
				Main.tile[num2 + 1, num + 1].type = 11;
				Main.tile[num2 + 1, num + 1].frameY = 18;
				Main.tile[num2 + 1, num + 1].frameX = (short)(num3 + 18);
				if (Main.tile[num2, num + 2] == null)
				{
					Main.tile[num2, num + 2] = new Tile();
				}
				Main.tile[num2, num + 2].active = true;
				Main.tile[num2, num + 2].type = 11;
				Main.tile[num2, num + 2].frameY = 36;
				Main.tile[num2, num + 2].frameX = num3;
				if (Main.tile[num2 + 1, num + 2] == null)
				{
					Main.tile[num2 + 1, num + 2] = new Tile();
				}
				Main.tile[num2 + 1, num + 2].active = true;
				Main.tile[num2 + 1, num + 2].type = 11;
				Main.tile[num2 + 1, num + 2].frameY = 36;
                Main.tile[num2 + 1, num + 2].frameX = (short)(num3 + 18);
				for (int l = num2 - 1; l <= num2 + 2; l++)
				{
					for (int m = num - 1; m <= num + 2; m++)
					{
						WorldGen.TileFrame(l, m, false, false);
					}
				}
			}
			return flag;
		}
		public static void Check1xX(int x, int j, byte type)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			int num = j - (int)(Main.tile[x, j].frameY / 18);
			int frameX = (int)Main.tile[x, j].frameX;
			int num2 = 3;
			if (type == 92)
			{
				num2 = 6;
			}
			bool flag = false;
			for (int i = 0; i < num2; i++)
			{
				if (Main.tile[x, num + i] == null)
				{
					Main.tile[x, num + i] = new Tile();
				}
				if (!Main.tile[x, num + i].active)
				{
					flag = true;
				}
				else
				{
					if (Main.tile[x, num + i].type != type)
					{
						flag = true;
					}
					else
					{
						if ((int)Main.tile[x, num + i].frameY != i * 18)
						{
							flag = true;
						}
						else
						{
							if ((int)Main.tile[x, num + i].frameX != frameX)
							{
								flag = true;
							}
						}
					}
				}
			}
			if (Main.tile[x, num + num2] == null)
			{
				Main.tile[x, num + num2] = new Tile();
			}
			if (!Main.tile[x, num + num2].active)
			{
				flag = true;
			}
			if (!Main.tileSolid[(int)Main.tile[x, num + num2].type])
			{
				flag = true;
			}
			if (flag)
			{
				WorldGen.destroyObject = true;
				for (int k = 0; k < num2; k++)
				{
					if (Main.tile[x, num + k].type == type)
					{
						WorldGen.KillTile(x, num + k, false, false, false);
					}
				}
				if (type == 92)
				{
					Item.NewItem(x * 16, j * 16, 32, 32, 341, 1, false);
				}
				if (type == 93)
				{
					Item.NewItem(x * 16, j * 16, 32, 32, 342, 1, false);
				}
				WorldGen.destroyObject = false;
			}
		}
		public static void Check2xX(int i, int j, byte type)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			int num = i;
			if (Main.tile[i, j].frameX == 18)
			{
				num--;
			}
			int num2 = j - (int)(Main.tile[num, j].frameY / 18);
			int frameX = (int)Main.tile[num, num2].frameX;
			int num3 = 3;
			if (type == 104)
			{
				num3 = 5;
			}
			bool flag = false;
			for (int k = 0; k < num3; k++)
			{
				if (Main.tile[num, num2 + k] == null)
				{
					Main.tile[num, num2 + k] = new Tile();
				}
				if (!Main.tile[num, num2 + k].active)
				{
					flag = true;
				}
				else
				{
					if (Main.tile[num, num2 + k].type != type)
					{
						flag = true;
					}
					else
					{
						if ((int)Main.tile[num, num2 + k].frameY != k * 18)
						{
							flag = true;
						}
						else
						{
							if ((int)Main.tile[num, num2 + k].frameX != frameX)
							{
								flag = true;
							}
						}
					}
				}
				if (Main.tile[num + 1, num2 + k] == null)
				{
					Main.tile[num + 1, num2 + k] = new Tile();
				}
				if (!Main.tile[num + 1, num2 + k].active)
				{
					flag = true;
				}
				else
				{
					if (Main.tile[num + 1, num2 + k].type != type)
					{
						flag = true;
					}
					else
					{
						if ((int)Main.tile[num + 1, num2 + k].frameY != k * 18)
						{
							flag = true;
						}
						else
						{
							if ((int)Main.tile[num + 1, num2 + k].frameX != frameX + 18)
							{
								flag = true;
							}
						}
					}
				}
			}
			if (Main.tile[num, num2 + num3] == null)
			{
				Main.tile[num, num2 + num3] = new Tile();
			}
			if (!Main.tile[num, num2 + num3].active)
			{
				flag = true;
			}
			if (!Main.tileSolid[(int)Main.tile[num, num2 + num3].type])
			{
				flag = true;
			}
			if (Main.tile[num + 1, num2 + num3] == null)
			{
				Main.tile[num + 1, num2 + num3] = new Tile();
			}
			if (!Main.tile[num + 1, num2 + num3].active)
			{
				flag = true;
			}
			if (!Main.tileSolid[(int)Main.tile[num + 1, num2 + num3].type])
			{
				flag = true;
			}
			if (flag)
			{
				WorldGen.destroyObject = true;
				for (int l = 0; l < num3; l++)
				{
					if (Main.tile[num, num2 + l].type == type)
					{
						WorldGen.KillTile(num, num2 + l, false, false, false);
					}
					if (Main.tile[num + 1, num2 + l].type == type)
					{
						WorldGen.KillTile(num + 1, num2 + l, false, false, false);
					}
				}
				if (type == 104)
				{
					Item.NewItem(num * 16, j * 16, 32, 32, 359, 1, false);
				}
				if (type == 105)
				{
					Item.NewItem(num * 16, j * 16, 32, 32, 360, 1, false);
				}
				WorldGen.destroyObject = false;
			}
		}
		public static void Place1xX(int x, int y, int type, int style = 0)
		{
			int num = style * 18;
			int num2 = 3;
			if (type == 92)
			{
				num2 = 6;
			}
			bool flag = true;
			for (int i = y - num2 + 1; i < y + 1; i++)
			{
				if (Main.tile[x, i] == null)
				{
					Main.tile[x, i] = new Tile();
				}
				if (Main.tile[x, i].active)
				{
					flag = false;
				}
				if (type == 93 && Main.tile[x, i].liquid > 0)
				{
					flag = false;
				}
			}
			if (flag && Main.tile[x, y + 1].active && Main.tileSolid[(int)Main.tile[x, y + 1].type])
			{
				for (int j = 0; j < num2; j++)
				{
					Main.tile[x, y - num2 + 1 + j].active = true;
					Main.tile[x, y - num2 + 1 + j].frameY = (short)(j * 18);
					Main.tile[x, y - num2 + 1 + j].frameX = (short)num;
					Main.tile[x, y - num2 + 1 + j].type = (byte)type;
				}
			}
		}
		public static void Place2xX(int x, int y, int type, int style = 0)
		{
			int num = style * 18;
			int num2 = 3;
			if (type == 104)
			{
				num2 = 5;
			}
			bool flag = true;
			for (int i = y - num2 + 1; i < y + 1; i++)
			{
				if (Main.tile[x, i] == null)
				{
					Main.tile[x, i] = new Tile();
				}
				if (Main.tile[x, i].active)
				{
					flag = false;
				}
				if (Main.tile[x + 1, i] == null)
				{
					Main.tile[x + 1, i] = new Tile();
				}
				if (Main.tile[x + 1, i].active)
				{
					flag = false;
				}
			}
			if (flag && Main.tile[x, y + 1].active && Main.tileSolid[(int)Main.tile[x, y + 1].type] && Main.tile[x + 1, y + 1].active && Main.tileSolid[(int)Main.tile[x + 1, y + 1].type])
			{
				for (int j = 0; j < num2; j++)
				{
					Main.tile[x, y - num2 + 1 + j].active = true;
					Main.tile[x, y - num2 + 1 + j].frameY = (short)(j * 18);
					Main.tile[x, y - num2 + 1 + j].frameX = (short)num;
					Main.tile[x, y - num2 + 1 + j].type = (byte)type;
					Main.tile[x + 1, y - num2 + 1 + j].active = true;
					Main.tile[x + 1, y - num2 + 1 + j].frameY = (short)(j * 18);
					Main.tile[x + 1, y - num2 + 1 + j].frameX = (short)(num + 18);
					Main.tile[x + 1, y - num2 + 1 + j].type = (byte)type;
				}
			}
		}
		public static void Check1x2(int x, int j, byte type)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			int num = j;
			bool flag = true;
			if (Main.tile[x, num] == null)
			{
				Main.tile[x, num] = new Tile();
			}
			if (Main.tile[x, num + 1] == null)
			{
				Main.tile[x, num + 1] = new Tile();
			}
			int i = (int)Main.tile[x, num].frameY;
			int num2 = 0;
			while (i >= 40)
			{
				i -= 40;
				num2++;
			}
			if (i == 18)
			{
				num--;
			}
			if (Main.tile[x, num] == null)
			{
				Main.tile[x, num] = new Tile();
			}
			if ((int)Main.tile[x, num].frameY == 40 * num2 && (int)Main.tile[x, num + 1].frameY == 40 * num2 + 18 && Main.tile[x, num].type == type && Main.tile[x, num + 1].type == type)
			{
				flag = false;
			}
			if (Main.tile[x, num + 2] == null)
			{
				Main.tile[x, num + 2] = new Tile();
			}
			if (!Main.tile[x, num + 2].active || !Main.tileSolid[(int)Main.tile[x, num + 2].type])
			{
				flag = true;
			}
			if (Main.tile[x, num + 2].type != 2 && Main.tile[x, num].type == 20)
			{
				flag = true;
			}
			if (flag)
			{
				WorldGen.destroyObject = true;
				if (Main.tile[x, num].type == type)
				{
					WorldGen.KillTile(x, num, false, false, false);
				}
				if (Main.tile[x, num + 1].type == type)
				{
					WorldGen.KillTile(x, num + 1, false, false, false);
				}
				if (type == 15)
				{
					if (num2 == 1)
					{
						Item.NewItem(x * 16, num * 16, 32, 32, 358, 1, false);
					}
					else
					{
						Item.NewItem(x * 16, num * 16, 32, 32, 34, 1, false);
					}
				}
				WorldGen.destroyObject = false;
			}
		}
		public static void CheckOnTable1x1(int x, int y, int type)
		{
			if (Main.tile[x, y + 1] != null && (!Main.tile[x, y + 1].active || !Main.tileTable[(int)Main.tile[x, y + 1].type]))
			{
				if (type == 78)
				{
					if (!Main.tile[x, y + 1].active || !Main.tileSolid[(int)Main.tile[x, y + 1].type])
					{
						WorldGen.KillTile(x, y, false, false, false);
						return;
					}
				}
				else
				{
					WorldGen.KillTile(x, y, false, false, false);
				}
			}
		}
		public static void CheckSign(int x, int y, int type)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			int num = x - 2;
			int num2 = x + 3;
			int num3 = y - 2;
			int num4 = y + 3;
			if (num < 0)
			{
				return;
			}
			if (num2 > Main.maxTilesX)
			{
				return;
			}
			if (num3 < 0)
			{
				return;
			}
			if (num4 > Main.maxTilesY)
			{
				return;
			}
			bool flag = false;
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
				}
			}
			int k = (int)(Main.tile[x, y].frameX / 18);
			int num5 = (int)(Main.tile[x, y].frameY / 18);
			while (k > 1)
			{
				k -= 2;
			}
			int num6 = x - k;
			int num7 = y - num5;
			int num8 = (int)(Main.tile[num6, num7].frameX / 18 / 2);
			num = num6;
			num2 = num6 + 2;
			num3 = num7;
			num4 = num7 + 2;
			k = 0;
			for (int l = num; l < num2; l++)
			{
				num5 = 0;
				for (int m = num3; m < num4; m++)
				{
					if (!Main.tile[l, m].active || (int)Main.tile[l, m].type != type)
					{
						flag = true;
						break;
					}
					if ((int)(Main.tile[l, m].frameX / 18) != k + num8 * 2 || (int)(Main.tile[l, m].frameY / 18) != num5)
					{
						flag = true;
						break;
					}
					num5++;
				}
				k++;
			}
			if (!flag)
			{
				if (type == 85)
				{
					if (Main.tile[num6, num7 + 2].active && Main.tileSolid[(int)Main.tile[num6, num7 + 2].type] && Main.tile[num6 + 1, num7 + 2].active && Main.tileSolid[(int)Main.tile[num6 + 1, num7 + 2].type])
					{
						num8 = 0;
					}
					else
					{
						flag = true;
					}
				}
				else
				{
					if (Main.tile[num6, num7 + 2].active && Main.tileSolid[(int)Main.tile[num6, num7 + 2].type] && Main.tile[num6 + 1, num7 + 2].active && Main.tileSolid[(int)Main.tile[num6 + 1, num7 + 2].type])
					{
						num8 = 0;
					}
					else
					{
						if (Main.tile[num6, num7 - 1].active && Main.tileSolid[(int)Main.tile[num6, num7 - 1].type] && !Main.tileSolidTop[(int)Main.tile[num6, num7 - 1].type] && Main.tile[num6 + 1, num7 - 1].active && Main.tileSolid[(int)Main.tile[num6 + 1, num7 - 1].type] && !Main.tileSolidTop[(int)Main.tile[num6 + 1, num7 - 1].type])
						{
							num8 = 1;
						}
						else
						{
							if (Main.tile[num6 - 1, num7].active && Main.tileSolid[(int)Main.tile[num6 - 1, num7].type] && !Main.tileSolidTop[(int)Main.tile[num6 - 1, num7].type] && Main.tile[num6 - 1, num7 + 1].active && Main.tileSolid[(int)Main.tile[num6 - 1, num7 + 1].type] && !Main.tileSolidTop[(int)Main.tile[num6 - 1, num7 + 1].type])
							{
								num8 = 2;
							}
							else
							{
								if (Main.tile[num6 + 2, num7].active && Main.tileSolid[(int)Main.tile[num6 + 2, num7].type] && !Main.tileSolidTop[(int)Main.tile[num6 + 2, num7].type] && Main.tile[num6 + 2, num7 + 1].active && Main.tileSolid[(int)Main.tile[num6 + 2, num7 + 1].type] && !Main.tileSolidTop[(int)Main.tile[num6 + 2, num7 + 1].type])
								{
									num8 = 3;
								}
								else
								{
									flag = true;
								}
							}
						}
					}
				}
			}
			if (flag)
			{
				WorldGen.destroyObject = true;
				for (int n = num; n < num2; n++)
				{
					for (int num9 = num3; num9 < num4; num9++)
					{
						if ((int)Main.tile[n, num9].type == type)
						{
							WorldGen.KillTile(n, num9, false, false, false);
						}
					}
				}
				Sign.KillSign(num6, num7);
				if (type == 85)
				{
					Item.NewItem(x * 16, y * 16, 32, 32, 321, 1, false);
				}
				else
				{
					Item.NewItem(x * 16, y * 16, 32, 32, 171, 1, false);
				}
				WorldGen.destroyObject = false;
				return;
			}
			int num10 = 36 * num8;
			for (int num11 = 0; num11 < 2; num11++)
			{
				for (int num12 = 0; num12 < 2; num12++)
				{
					Main.tile[num6 + num11, num7 + num12].active = true;
					Main.tile[num6 + num11, num7 + num12].type = (byte)type;
					Main.tile[num6 + num11, num7 + num12].frameX = (short)(num10 + 18 * num11);
					Main.tile[num6 + num11, num7 + num12].frameY = (short)(18 * num12);
				}
			}
		}
		public static bool PlaceSign(int x, int y, int type)
		{
			int num = x - 2;
			int num2 = x + 3;
			int num3 = y - 2;
			int num4 = y + 3;
			if (num < 0)
			{
				return false;
			}
			if (num2 > Main.maxTilesX)
			{
				return false;
			}
			if (num3 < 0)
			{
				return false;
			}
			if (num4 > Main.maxTilesY)
			{
				return false;
			}
			for (int i = num; i < num2; i++)
			{
				for (int j = num3; j < num4; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
				}
			}
			int num5 = x;
			int num6 = y;
			int num7 = 0;
			if (type == 55)
			{
				if (Main.tile[x, y + 1].active && Main.tileSolid[(int)Main.tile[x, y + 1].type] && Main.tile[x + 1, y + 1].active && Main.tileSolid[(int)Main.tile[x + 1, y + 1].type])
				{
					num6--;
					num7 = 0;
				}
				else
				{
					if (Main.tile[x, y - 1].active && Main.tileSolid[(int)Main.tile[x, y - 1].type] && !Main.tileSolidTop[(int)Main.tile[x, y - 1].type] && Main.tile[x + 1, y - 1].active && Main.tileSolid[(int)Main.tile[x + 1, y - 1].type] && !Main.tileSolidTop[(int)Main.tile[x + 1, y - 1].type])
					{
						num7 = 1;
					}
					else
					{
						if (Main.tile[x - 1, y].active && Main.tileSolid[(int)Main.tile[x - 1, y].type] && !Main.tileSolidTop[(int)Main.tile[x - 1, y].type] && Main.tile[x - 1, y + 1].active && Main.tileSolid[(int)Main.tile[x - 1, y + 1].type] && !Main.tileSolidTop[(int)Main.tile[x - 1, y + 1].type])
						{
							num7 = 2;
						}
						else
						{
							if (!Main.tile[x + 1, y].active || !Main.tileSolid[(int)Main.tile[x + 1, y].type] || Main.tileSolidTop[(int)Main.tile[x + 1, y].type] || !Main.tile[x + 1, y + 1].active || !Main.tileSolid[(int)Main.tile[x + 1, y + 1].type] || Main.tileSolidTop[(int)Main.tile[x + 1, y + 1].type])
							{
								return false;
							}
							num5--;
							num7 = 3;
						}
					}
				}
			}
			else
			{
				if (type == 85)
				{
					if (!Main.tile[x, y + 1].active || !Main.tileSolid[(int)Main.tile[x, y + 1].type] || !Main.tile[x + 1, y + 1].active || !Main.tileSolid[(int)Main.tile[x + 1, y + 1].type])
					{
						return false;
					}
					num6--;
					num7 = 0;
				}
			}
			if (Main.tile[num5, num6].active || Main.tile[num5 + 1, num6].active || Main.tile[num5, num6 + 1].active || Main.tile[num5 + 1, num6 + 1].active)
			{
				return false;
			}
			int num8 = 36 * num7;
			for (int k = 0; k < 2; k++)
			{
				for (int l = 0; l < 2; l++)
				{
					Main.tile[num5 + k, num6 + l].active = true;
					Main.tile[num5 + k, num6 + l].type = (byte)type;
					Main.tile[num5 + k, num6 + l].frameX = (short)(num8 + 18 * k);
					Main.tile[num5 + k, num6 + l].frameY = (short)(18 * l);
				}
			}
			return true;
		}
		public static void PlaceOnTable1x1(int x, int y, int type, int style = 0)
		{
			bool flag = false;
			if (Main.tile[x, y] == null)
			{
				Main.tile[x, y] = new Tile();
			}
			if (Main.tile[x, y + 1] == null)
			{
				Main.tile[x, y + 1] = new Tile();
			}
			if (!Main.tile[x, y].active && Main.tile[x, y + 1].active && Main.tileTable[(int)Main.tile[x, y + 1].type])
			{
				flag = true;
			}
			if (type == 78 && !Main.tile[x, y].active && Main.tile[x, y + 1].active && Main.tileSolid[(int)Main.tile[x, y + 1].type])
			{
				flag = true;
			}
			if (flag)
			{
				Main.tile[x, y].active = true;
				Main.tile[x, y].frameX = (short)(style * 18);
				Main.tile[x, y].frameY = 0;
				Main.tile[x, y].type = (byte)type;
				if (type == 50)
				{
					Main.tile[x, y].frameX = (short)(18 * WorldGen.genRand.Next(5));
				}
			}
		}
		public static bool PlaceAlch(int x, int y, int style)
		{
			if (Main.tile[x, y] == null)
			{
				Main.tile[x, y] = new Tile();
			}
			if (Main.tile[x, y + 1] == null)
			{
				Main.tile[x, y + 1] = new Tile();
			}
			if (!Main.tile[x, y].active && Main.tile[x, y + 1].active)
			{
				bool flag = false;
				if (style == 0)
				{
					if (Main.tile[x, y + 1].type != 2 && Main.tile[x, y + 1].type != 78)
					{
						flag = true;
					}
					if (Main.tile[x, y].liquid > 0)
					{
						flag = true;
					}
				}
				else
				{
					if (style == 1)
					{
						if (Main.tile[x, y + 1].type != 60 && Main.tile[x, y + 1].type != 78)
						{
							flag = true;
						}
						if (Main.tile[x, y].liquid > 0)
						{
							flag = true;
						}
					}
					else
					{
						if (style == 2)
						{
							if (Main.tile[x, y + 1].type != 0 && Main.tile[x, y + 1].type != 59 && Main.tile[x, y + 1].type != 78)
							{
								flag = true;
							}
							if (Main.tile[x, y].liquid > 0)
							{
								flag = true;
							}
						}
						else
						{
							if (style == 3)
							{
								if (Main.tile[x, y + 1].type != 23 && Main.tile[x, y + 1].type != 25 && Main.tile[x, y + 1].type != 78)
								{
									flag = true;
								}
								if (Main.tile[x, y].liquid > 0)
								{
									flag = true;
								}
							}
							else
							{
								if (style == 4)
								{
									if (Main.tile[x, y + 1].type != 53 && Main.tile[x, y + 1].type != 78)
									{
										flag = true;
									}
									if (Main.tile[x, y].liquid > 0 && Main.tile[x, y].lava)
									{
										flag = true;
									}
								}
								else
								{
									if (style == 5)
									{
										if (Main.tile[x, y + 1].type != 57 && Main.tile[x, y + 1].type != 78)
										{
											flag = true;
										}
										if (Main.tile[x, y].liquid > 0 && !Main.tile[x, y].lava)
										{
											flag = true;
										}
									}
								}
							}
						}
					}
				}
				if (!flag)
				{
					Main.tile[x, y].active = true;
					Main.tile[x, y].type = 82;
					Main.tile[x, y].frameX = (short)(18 * style);
					Main.tile[x, y].frameY = 0;
					return true;
				}
			}
			return false;
		}
		public static void GrowAlch(int x, int y)
		{
			if (Main.tile[x, y].active)
			{
				if (Main.tile[x, y].type == 82 && WorldGen.genRand.Next(50) == 0)
				{
					Main.tile[x, y].type = 83;
					if (Main.netMode == 2)
					{
						NetMessage.SendTileSquare(-1, x, y, 1);
					}
					WorldGen.SquareTileFrame(x, y, true);
					return;
				}
				if (Main.tile[x, y].frameX == 36)
				{
					if (Main.tile[x, y].type == 83)
					{
						Main.tile[x, y].type = 84;
					}
					else
					{
						Main.tile[x, y].type = 83;
					}
					if (Main.netMode == 2)
					{
						NetMessage.SendTileSquare(-1, x, y, 1);
					}
				}
			}
		}
		public static void PlantAlch()
		{
			int num = WorldGen.genRand.Next(20, Main.maxTilesX - 20);
			int num2 = 0;
			if (WorldGen.genRand.Next(40) == 0)
			{
				num2 = WorldGen.genRand.Next((int)(Main.rockLayer + (double)Main.maxTilesY) / 2, Main.maxTilesY - 20);
			}
			else
			{
				if (WorldGen.genRand.Next(10) == 0)
				{
					num2 = WorldGen.genRand.Next(0, Main.maxTilesY - 20);
				}
				else
				{
					num2 = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 20);
				}
			}
			while (num2 < Main.maxTilesY - 20 && !Main.tile[num, num2].active)
			{
				num2++;
			}
			if (Main.tile[num, num2].active && !Main.tile[num, num2 - 1].active && Main.tile[num, num2 - 1].liquid == 0)
			{
				if (Main.tile[num, num2].type == 2)
				{
					WorldGen.PlaceAlch(num, num2 - 1, 0);
				}
				if (Main.tile[num, num2].type == 60)
				{
					WorldGen.PlaceAlch(num, num2 - 1, 1);
				}
				if (Main.tile[num, num2].type == 0 || Main.tile[num, num2].type == 59)
				{
					WorldGen.PlaceAlch(num, num2 - 1, 2);
				}
				if (Main.tile[num, num2].type == 23 || Main.tile[num, num2].type == 25)
				{
					WorldGen.PlaceAlch(num, num2 - 1, 3);
				}
				if (Main.tile[num, num2].type == 53)
				{
					WorldGen.PlaceAlch(num, num2 - 1, 4);
				}
				if (Main.tile[num, num2].type == 57)
				{
					WorldGen.PlaceAlch(num, num2 - 1, 5);
				}
				if (Main.tile[num, num2 - 1].active && Main.netMode == 2)
				{
					NetMessage.SendTileSquare(-1, num, num2 - 1, 1);
				}
			}
		}
		public static void CheckAlch(int x, int y)
		{
			if (Main.tile[x, y] == null)
			{
				Main.tile[x, y] = new Tile();
			}
			if (Main.tile[x, y + 1] == null)
			{
				Main.tile[x, y + 1] = new Tile();
			}
			bool flag = false;
			if (!Main.tile[x, y + 1].active)
			{
				flag = true;
			}
			int num = (int)(Main.tile[x, y].frameX / 18);
			Main.tile[x, y].frameY = 0;
			if (!flag)
			{
				if (num == 0)
				{
					if (Main.tile[x, y + 1].type != 2 && Main.tile[x, y + 1].type != 78)
					{
						flag = true;
					}
					if (Main.tile[x, y].liquid > 0 && Main.tile[x, y].lava)
					{
						flag = true;
					}
				}
				else
				{
					if (num == 1)
					{
						if (Main.tile[x, y + 1].type != 60 && Main.tile[x, y + 1].type != 78)
						{
							flag = true;
						}
						if (Main.tile[x, y].liquid > 0 && Main.tile[x, y].lava)
						{
							flag = true;
						}
					}
					else
					{
						if (num == 2)
						{
							if (Main.tile[x, y + 1].type != 0 && Main.tile[x, y + 1].type != 59 && Main.tile[x, y + 1].type != 78)
							{
								flag = true;
							}
							if (Main.tile[x, y].liquid > 0 && Main.tile[x, y].lava)
							{
								flag = true;
							}
						}
						else
						{
							if (num == 3)
							{
								if (Main.tile[x, y + 1].type != 23 && Main.tile[x, y + 1].type != 25 && Main.tile[x, y + 1].type != 78)
								{
									flag = true;
								}
								if (Main.tile[x, y].liquid > 0 && Main.tile[x, y].lava)
								{
									flag = true;
								}
							}
							else
							{
								if (num == 4)
								{
									if (Main.tile[x, y + 1].type != 53 && Main.tile[x, y + 1].type != 78)
									{
										flag = true;
									}
									if (Main.tile[x, y].liquid > 0 && Main.tile[x, y].lava)
									{
										flag = true;
									}
									if (Main.tile[x, y].type != 82 && !Main.tile[x, y].lava && Main.netMode != 1)
									{
										if (Main.tile[x, y].liquid > 16)
										{
											if (Main.tile[x, y].type == 83)
											{
												Main.tile[x, y].type = 84;
												if (Main.netMode == 2)
												{
													NetMessage.SendTileSquare(-1, x, y, 1);
												}
											}
										}
										else
										{
											if (Main.tile[x, y].type == 84)
											{
												Main.tile[x, y].type = 83;
												if (Main.netMode == 2)
												{
													NetMessage.SendTileSquare(-1, x, y, 1);
												}
											}
										}
									}
								}
								else
								{
									if (num == 5)
									{
										if (Main.tile[x, y + 1].type != 57 && Main.tile[x, y + 1].type != 78)
										{
											flag = true;
										}
										if (Main.tile[x, y].liquid > 0 && !Main.tile[x, y].lava)
										{
											flag = true;
										}
										if (Main.tile[x, y].type != 82 && Main.tile[x, y].lava && Main.tile[x, y].type != 82 && Main.tile[x, y].lava && Main.netMode != 1)
										{
											if (Main.tile[x, y].liquid > 16)
											{
												if (Main.tile[x, y].type == 83)
												{
													Main.tile[x, y].type = 84;
													if (Main.netMode == 2)
													{
														NetMessage.SendTileSquare(-1, x, y, 1);
													}
												}
											}
											else
											{
												if (Main.tile[x, y].type == 84)
												{
													Main.tile[x, y].type = 83;
													if (Main.netMode == 2)
													{
														NetMessage.SendTileSquare(-1, x, y, 1);
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
			if (flag)
			{
				WorldGen.KillTile(x, y, false, false, false);
			}
		}
		public static void CheckBanner(int x, int j, byte type)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			int num = j - (int)(Main.tile[x, j].frameY / 18);
			int frameX = (int)Main.tile[x, j].frameX;
			bool flag = false;
			for (int i = 0; i < 3; i++)
			{
				if (Main.tile[x, num + i] == null)
				{
					Main.tile[x, num + i] = new Tile();
				}
				if (!Main.tile[x, num + i].active)
				{
					flag = true;
				}
				else
				{
					if (Main.tile[x, num + i].type != type)
					{
						flag = true;
					}
					else
					{
						if ((int)Main.tile[x, num + i].frameY != i * 18)
						{
							flag = true;
						}
						else
						{
							if ((int)Main.tile[x, num + i].frameX != frameX)
							{
								flag = true;
							}
						}
					}
				}
			}
			if (Main.tile[x, num - 1] == null)
			{
				Main.tile[x, num - 1] = new Tile();
			}
			if (!Main.tile[x, num - 1].active)
			{
				flag = true;
			}
			if (!Main.tileSolid[(int)Main.tile[x, num - 1].type])
			{
				flag = true;
			}
			if (Main.tileSolidTop[(int)Main.tile[x, num - 1].type])
			{
				flag = true;
			}
			if (flag)
			{
				WorldGen.destroyObject = true;
				for (int k = 0; k < 3; k++)
				{
					if (Main.tile[x, num + k].type == type)
					{
						WorldGen.KillTile(x, num + k, false, false, false);
					}
				}
				if (type == 91)
				{
					int num2 = frameX / 18;
					Item.NewItem(x * 16, (num + 1) * 16, 32, 32, 337 + num2, 1, false);
				}
				WorldGen.destroyObject = false;
			}
		}
		public static void PlaceBanner(int x, int y, int type, int style = 0)
		{
			int num = style * 18;
			if (Main.tile[x, y - 1] == null)
			{
				Main.tile[x, y - 1] = new Tile();
			}
			if (Main.tile[x, y] == null)
			{
				Main.tile[x, y] = new Tile();
			}
			if (Main.tile[x, y + 1] == null)
			{
				Main.tile[x, y + 1] = new Tile();
			}
			if (Main.tile[x, y + 2] == null)
			{
				Main.tile[x, y + 2] = new Tile();
			}
			if (Main.tile[x, y - 1].active && Main.tileSolid[(int)Main.tile[x, y - 1].type] && !Main.tileSolidTop[(int)Main.tile[x, y - 1].type] && !Main.tile[x, y].active && !Main.tile[x, y + 1].active && !Main.tile[x, y + 2].active)
			{
				Main.tile[x, y].active = true;
				Main.tile[x, y].frameY = 0;
				Main.tile[x, y].frameX = (short)num;
				Main.tile[x, y].type = (byte)type;
				Main.tile[x, y + 1].active = true;
				Main.tile[x, y + 1].frameY = 18;
				Main.tile[x, y + 1].frameX = (short)num;
				Main.tile[x, y + 1].type = (byte)type;
				Main.tile[x, y + 2].active = true;
				Main.tile[x, y + 2].frameY = 36;
				Main.tile[x, y + 2].frameX = (short)num;
				Main.tile[x, y + 2].type = (byte)type;
			}
		}
		public static void Place1x2(int x, int y, int type, int style)
		{
			short frameX = 0;
			if (type == 20)
			{
				frameX = (short)(WorldGen.genRand.Next(3) * 18);
			}
			if (Main.tile[x, y - 1] == null)
			{
				Main.tile[x, y - 1] = new Tile();
			}
			if (Main.tile[x, y + 1] == null)
			{
				Main.tile[x, y + 1] = new Tile();
			}
			if (Main.tile[x, y + 1].active && Main.tileSolid[(int)Main.tile[x, y + 1].type] && !Main.tile[x, y - 1].active)
			{
				short num = (short)(style * 40);
				Main.tile[x, y - 1].active = true;
				Main.tile[x, y - 1].frameY = num;
				Main.tile[x, y - 1].frameX = frameX;
				Main.tile[x, y - 1].type = (byte)type;
				Main.tile[x, y].active = true;
                Main.tile[x, y].frameY = (short)(num + 18);
				Main.tile[x, y].frameX = frameX;
				Main.tile[x, y].type = (byte)type;
			}
		}
		public static void Place1x2Top(int x, int y, int type)
		{
			short frameX = 0;
			if (Main.tile[x, y - 1] == null)
			{
				Main.tile[x, y - 1] = new Tile();
			}
			if (Main.tile[x, y + 1] == null)
			{
				Main.tile[x, y + 1] = new Tile();
			}
			if (Main.tile[x, y - 1].active && Main.tileSolid[(int)Main.tile[x, y - 1].type] && !Main.tileSolidTop[(int)Main.tile[x, y - 1].type] && !Main.tile[x, y + 1].active)
			{
				Main.tile[x, y].active = true;
				Main.tile[x, y].frameY = 0;
				Main.tile[x, y].frameX = frameX;
				Main.tile[x, y].type = (byte)type;
				Main.tile[x, y + 1].active = true;
				Main.tile[x, y + 1].frameY = 18;
				Main.tile[x, y + 1].frameX = frameX;
				Main.tile[x, y + 1].type = (byte)type;
			}
		}
		public static void Check1x2Top(int x, int j, byte type)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			int num = j;
			bool flag = true;
			if (Main.tile[x, num] == null)
			{
				Main.tile[x, num] = new Tile();
			}
			if (Main.tile[x, num + 1] == null)
			{
				Main.tile[x, num + 1] = new Tile();
			}
			if (Main.tile[x, num].frameY == 18)
			{
				num--;
			}
			if (Main.tile[x, num] == null)
			{
				Main.tile[x, num] = new Tile();
			}
			if (Main.tile[x, num].frameY == 0 && Main.tile[x, num + 1].frameY == 18 && Main.tile[x, num].type == type && Main.tile[x, num + 1].type == type)
			{
				flag = false;
			}
			if (Main.tile[x, num - 1] == null)
			{
				Main.tile[x, num - 1] = new Tile();
			}
			if (!Main.tile[x, num - 1].active || !Main.tileSolid[(int)Main.tile[x, num - 1].type] || Main.tileSolidTop[(int)Main.tile[x, num - 1].type])
			{
				flag = true;
			}
			if (flag)
			{
				WorldGen.destroyObject = true;
				if (Main.tile[x, num].type == type)
				{
					WorldGen.KillTile(x, num, false, false, false);
				}
				if (Main.tile[x, num + 1].type == type)
				{
					WorldGen.KillTile(x, num + 1, false, false, false);
				}
				if (type == 42)
				{
					Item.NewItem(x * 16, num * 16, 32, 32, 136, 1, false);
				}
				WorldGen.destroyObject = false;
			}
		}
		public static void Check2x1(int i, int y, byte type)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			int num = i;
			bool flag = true;
			if (Main.tile[num, y] == null)
			{
				Main.tile[num, y] = new Tile();
			}
			if (Main.tile[num + 1, y] == null)
			{
				Main.tile[num + 1, y] = new Tile();
			}
			if (Main.tile[num, y + 1] == null)
			{
				Main.tile[num, y + 1] = new Tile();
			}
			if (Main.tile[num + 1, y + 1] == null)
			{
				Main.tile[num + 1, y + 1] = new Tile();
			}
			if (Main.tile[num, y].frameX == 18)
			{
				num--;
			}
			if (Main.tile[num, y].frameX == 0 && Main.tile[num + 1, y].frameX == 18 && Main.tile[num, y].type == type && Main.tile[num + 1, y].type == type)
			{
				flag = false;
			}
			if (type == 29 || type == 103)
			{
				if (!Main.tile[num, y + 1].active || !Main.tileTable[(int)Main.tile[num, y + 1].type])
				{
					flag = true;
				}
				if (!Main.tile[num + 1, y + 1].active || !Main.tileTable[(int)Main.tile[num + 1, y + 1].type])
				{
					flag = true;
				}
			}
			else
			{
				if (!Main.tile[num, y + 1].active || !Main.tileSolid[(int)Main.tile[num, y + 1].type])
				{
					flag = true;
				}
				if (!Main.tile[num + 1, y + 1].active || !Main.tileSolid[(int)Main.tile[num + 1, y + 1].type])
				{
					flag = true;
				}
			}
			if (flag)
			{
				WorldGen.destroyObject = true;
				if (Main.tile[num, y].type == type)
				{
					WorldGen.KillTile(num, y, false, false, false);
				}
				if (Main.tile[num + 1, y].type == type)
				{
					WorldGen.KillTile(num + 1, y, false, false, false);
				}
				if (type == 16)
				{
					Item.NewItem(num * 16, y * 16, 32, 32, 35, 1, false);
				}
				if (type == 18)
				{
					Item.NewItem(num * 16, y * 16, 32, 32, 36, 1, false);
				}
				if (type == 29)
				{
					Item.NewItem(num * 16, y * 16, 32, 32, 87, 1, false);
					Main.PlaySound(13, i * 16, y * 16, 1);
				}
				if (type == 103)
				{
					Item.NewItem(num * 16, y * 16, 32, 32, 356, 1, false);
					Main.PlaySound(13, i * 16, y * 16, 1);
				}
				WorldGen.destroyObject = false;
				WorldGen.SquareTileFrame(num, y, true);
				WorldGen.SquareTileFrame(num + 1, y, true);
			}
		}
		public static void Place2x1(int x, int y, int type)
		{
			if (Main.tile[x, y] == null)
			{
				Main.tile[x, y] = new Tile();
			}
			if (Main.tile[x + 1, y] == null)
			{
				Main.tile[x + 1, y] = new Tile();
			}
			if (Main.tile[x, y + 1] == null)
			{
				Main.tile[x, y + 1] = new Tile();
			}
			if (Main.tile[x + 1, y + 1] == null)
			{
				Main.tile[x + 1, y + 1] = new Tile();
			}
			bool flag = false;
			if (type != 29 && type != 103 && Main.tile[x, y + 1].active && Main.tile[x + 1, y + 1].active && Main.tileSolid[(int)Main.tile[x, y + 1].type] && Main.tileSolid[(int)Main.tile[x + 1, y + 1].type] && !Main.tile[x, y].active && !Main.tile[x + 1, y].active)
			{
				flag = true;
			}
			else
			{
				if ((type == 29 || type == 103) && Main.tile[x, y + 1].active && Main.tile[x + 1, y + 1].active && Main.tileTable[(int)Main.tile[x, y + 1].type] && Main.tileTable[(int)Main.tile[x + 1, y + 1].type] && !Main.tile[x, y].active && !Main.tile[x + 1, y].active)
				{
					flag = true;
				}
			}
			if (flag)
			{
				Main.tile[x, y].active = true;
				Main.tile[x, y].frameY = 0;
				Main.tile[x, y].frameX = 0;
				Main.tile[x, y].type = (byte)type;
				Main.tile[x + 1, y].active = true;
				Main.tile[x + 1, y].frameY = 0;
				Main.tile[x + 1, y].frameX = 18;
				Main.tile[x + 1, y].type = (byte)type;
			}
		}
		public static void Check4x2(int i, int j, int type)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			bool flag = false;
			int num = i;
			num += (int)(Main.tile[i, j].frameX / 18 * -1);
			if ((type == 79 || type == 90) && Main.tile[i, j].frameX >= 72)
			{
				num += 4;
			}
			int num2 = j + (int)(Main.tile[i, j].frameY / 18 * -1);
			for (int k = num; k < num + 4; k++)
			{
				for (int l = num2; l < num2 + 2; l++)
				{
					int num3 = (k - num) * 18;
					if ((type == 79 || type == 90) && Main.tile[i, j].frameX >= 72)
					{
						num3 = (k - num + 4) * 18;
					}
					if (Main.tile[k, l] == null)
					{
						Main.tile[k, l] = new Tile();
					}
					if (!Main.tile[k, l].active || (int)Main.tile[k, l].type != type || (int)Main.tile[k, l].frameX != num3 || (int)Main.tile[k, l].frameY != (l - num2) * 18)
					{
						flag = true;
					}
				}
				if (Main.tile[k, num2 + 2] == null)
				{
					Main.tile[k, num2 + 2] = new Tile();
				}
				if (!Main.tile[k, num2 + 2].active || !Main.tileSolid[(int)Main.tile[k, num2 + 2].type])
				{
					flag = true;
				}
			}
			if (flag)
			{
				WorldGen.destroyObject = true;
				for (int m = num; m < num + 4; m++)
				{
					for (int n = num2; n < num2 + 3; n++)
					{
						if ((int)Main.tile[m, n].type == type && Main.tile[m, n].active)
						{
							WorldGen.KillTile(m, n, false, false, false);
						}
					}
				}
				if (type == 79)
				{
					Item.NewItem(i * 16, j * 16, 32, 32, 224, 1, false);
				}
				if (type == 90)
				{
					Item.NewItem(i * 16, j * 16, 32, 32, 336, 1, false);
				}
				WorldGen.destroyObject = false;
				for (int num4 = num - 1; num4 < num + 4; num4++)
				{
					for (int num5 = num2 - 1; num5 < num2 + 4; num5++)
					{
						WorldGen.TileFrame(num4, num5, false, false);
					}
				}
			}
		}
		public static void Check2x2(int i, int j, int type)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			bool flag = false;
			int num = i + (int)(Main.tile[i, j].frameX / 18 * -1);
			int num2 = j + (int)(Main.tile[i, j].frameY / 18 * -1);
			for (int k = num; k < num + 2; k++)
			{
				for (int l = num2; l < num2 + 2; l++)
				{
					if (Main.tile[k, l] == null)
					{
						Main.tile[k, l] = new Tile();
					}
					if (!Main.tile[k, l].active || (int)Main.tile[k, l].type != type || (int)Main.tile[k, l].frameX != (k - num) * 18 || (int)Main.tile[k, l].frameY != (l - num2) * 18)
					{
						flag = true;
					}
				}
				if (type == 95)
				{
					if (Main.tile[k, num2 - 1] == null)
					{
						Main.tile[k, num2 - 1] = new Tile();
					}
					if (!Main.tile[k, num2 - 1].active || !Main.tileSolid[(int)Main.tile[k, num2 - 1].type] || Main.tileSolidTop[(int)Main.tile[k, num2 - 1].type])
					{
						flag = true;
					}
				}
				else
				{
					if (Main.tile[k, num2 + 2] == null)
					{
						Main.tile[k, num2 + 2] = new Tile();
					}
					if (!Main.tile[k, num2 + 2].active || (!Main.tileSolid[(int)Main.tile[k, num2 + 2].type] && !Main.tileTable[(int)Main.tile[k, num2 + 2].type]))
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
				WorldGen.destroyObject = true;
				for (int m = num; m < num + 2; m++)
				{
					for (int n = num2; n < num2 + 3; n++)
					{
						if ((int)Main.tile[m, n].type == type && Main.tile[m, n].active)
						{
							WorldGen.KillTile(m, n, false, false, false);
						}
					}
				}
				if (type == 85)
				{
					Item.NewItem(i * 16, j * 16, 32, 32, 321, 1, false);
				}
				if (type == 94)
				{
					Item.NewItem(i * 16, j * 16, 32, 32, 352, 1, false);
				}
				if (type == 95)
				{
					Item.NewItem(i * 16, j * 16, 32, 32, 344, 1, false);
				}
				if (type == 96)
				{
					Item.NewItem(i * 16, j * 16, 32, 32, 345, 1, false);
				}
				if (type == 97)
				{
					Item.NewItem(i * 16, j * 16, 32, 32, 346, 1, false);
				}
				if (type == 98)
				{
					Item.NewItem(i * 16, j * 16, 32, 32, 347, 1, false);
				}
				if (type == 99)
				{
					Item.NewItem(i * 16, j * 16, 32, 32, 348, 1, false);
				}
				if (type == 100)
				{
					Item.NewItem(i * 16, j * 16, 32, 32, 349, 1, false);
				}
				WorldGen.destroyObject = false;
				for (int num3 = num - 1; num3 < num + 3; num3++)
				{
					for (int num4 = num2 - 1; num4 < num2 + 3; num4++)
					{
						WorldGen.TileFrame(num3, num4, false, false);
					}
				}
			}
		}
		public static void Check3x2(int i, int j, int type)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			bool flag = false;
			int num = i + (int)(Main.tile[i, j].frameX / 18 * -1);
			int num2 = j + (int)(Main.tile[i, j].frameY / 18 * -1);
			for (int k = num; k < num + 3; k++)
			{
				for (int l = num2; l < num2 + 2; l++)
				{
					if (Main.tile[k, l] == null)
					{
						Main.tile[k, l] = new Tile();
					}
					if (!Main.tile[k, l].active || (int)Main.tile[k, l].type != type || (int)Main.tile[k, l].frameX != (k - num) * 18 || (int)Main.tile[k, l].frameY != (l - num2) * 18)
					{
						flag = true;
					}
				}
				if (Main.tile[k, num2 + 2] == null)
				{
					Main.tile[k, num2 + 2] = new Tile();
				}
				if (!Main.tile[k, num2 + 2].active || !Main.tileSolid[(int)Main.tile[k, num2 + 2].type])
				{
					flag = true;
				}
			}
			if (flag)
			{
				WorldGen.destroyObject = true;
				for (int m = num; m < num + 3; m++)
				{
					for (int n = num2; n < num2 + 3; n++)
					{
						if ((int)Main.tile[m, n].type == type && Main.tile[m, n].active)
						{
							WorldGen.KillTile(m, n, false, false, false);
						}
					}
				}
				if (type == 14)
				{
					Item.NewItem(i * 16, j * 16, 32, 32, 32, 1, false);
				}
				else
				{
					if (type == 17)
					{
						Item.NewItem(i * 16, j * 16, 32, 32, 33, 1, false);
					}
					else
					{
						if (type == 77)
						{
							Item.NewItem(i * 16, j * 16, 32, 32, 221, 1, false);
						}
						else
						{
							if (type == 86)
							{
								Item.NewItem(i * 16, j * 16, 32, 32, 332, 1, false);
							}
							else
							{
								if (type == 87)
								{
									Item.NewItem(i * 16, j * 16, 32, 32, 333, 1, false);
								}
								else
								{
									if (type == 88)
									{
										Item.NewItem(i * 16, j * 16, 32, 32, 334, 1, false);
									}
									else
									{
										if (type == 89)
										{
											Item.NewItem(i * 16, j * 16, 32, 32, 335, 1, false);
										}
									}
								}
							}
						}
					}
				}
				WorldGen.destroyObject = false;
				for (int num3 = num - 1; num3 < num + 4; num3++)
				{
					for (int num4 = num2 - 1; num4 < num2 + 4; num4++)
					{
						WorldGen.TileFrame(num3, num4, false, false);
					}
				}
			}
		}
		public static void Check3x4(int i, int j, int type)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			bool flag = false;
			int num = i + (int)(Main.tile[i, j].frameX / 18 * -1);
			int num2 = j + (int)(Main.tile[i, j].frameY / 18 * -1);
			for (int k = num; k < num + 3; k++)
			{
				for (int l = num2; l < num2 + 4; l++)
				{
					if (Main.tile[k, l] == null)
					{
						Main.tile[k, l] = new Tile();
					}
					if (!Main.tile[k, l].active || (int)Main.tile[k, l].type != type || (int)Main.tile[k, l].frameX != (k - num) * 18 || (int)Main.tile[k, l].frameY != (l - num2) * 18)
					{
						flag = true;
					}
				}
				if (Main.tile[k, num2 + 4] == null)
				{
					Main.tile[k, num2 + 4] = new Tile();
				}
				if (!Main.tile[k, num2 + 4].active || !Main.tileSolid[(int)Main.tile[k, num2 + 4].type])
				{
					flag = true;
				}
			}
			if (flag)
			{
				WorldGen.destroyObject = true;
				for (int m = num; m < num + 3; m++)
				{
					for (int n = num2; n < num2 + 4; n++)
					{
						if ((int)Main.tile[m, n].type == type && Main.tile[m, n].active)
						{
							WorldGen.KillTile(m, n, false, false, false);
						}
					}
				}
				if (type == 101)
				{
					Item.NewItem(i * 16, j * 16, 32, 32, 354, 1, false);
				}
				else
				{
					if (type == 102)
					{
						Item.NewItem(i * 16, j * 16, 32, 32, 355, 1, false);
					}
				}
				WorldGen.destroyObject = false;
				for (int num3 = num - 1; num3 < num + 4; num3++)
				{
					for (int num4 = num2 - 1; num4 < num2 + 4; num4++)
					{
						WorldGen.TileFrame(num3, num4, false, false);
					}
				}
			}
		}
		public static void Place4x2(int x, int y, int type, int direction = -1)
		{
			if (x < 5 || x > Main.maxTilesX - 5 || y < 5 || y > Main.maxTilesY - 5)
			{
				return;
			}
			bool flag = true;
			for (int i = x - 1; i < x + 3; i++)
			{
				for (int j = y - 1; j < y + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
					if (Main.tile[i, j].active)
					{
						flag = false;
					}
				}
				if (Main.tile[i, y + 1] == null)
				{
					Main.tile[i, y + 1] = new Tile();
				}
				if (!Main.tile[i, y + 1].active || !Main.tileSolid[(int)Main.tile[i, y + 1].type])
				{
					flag = false;
				}
			}
			short num = 0;
			if (direction == 1)
			{
				num = 72;
			}
			if (flag)
			{
				Main.tile[x - 1, y - 1].active = true;
				Main.tile[x - 1, y - 1].frameY = 0;
				Main.tile[x - 1, y - 1].frameX = num;
				Main.tile[x - 1, y - 1].type = (byte)type;
				Main.tile[x, y - 1].active = true;
				Main.tile[x, y - 1].frameY = 0;
                Main.tile[x, y - 1].frameX = (short)(18 + num);
				Main.tile[x, y - 1].type = (byte)type;
				Main.tile[x + 1, y - 1].active = true;
				Main.tile[x + 1, y - 1].frameY = 0;
                Main.tile[x + 1, y - 1].frameX = (short)(36 + num);
				Main.tile[x + 1, y - 1].type = (byte)type;
				Main.tile[x + 2, y - 1].active = true;
				Main.tile[x + 2, y - 1].frameY = 0;
                Main.tile[x + 2, y - 1].frameX = (short)(54 + num);
				Main.tile[x + 2, y - 1].type = (byte)type;
				Main.tile[x - 1, y].active = true;
				Main.tile[x - 1, y].frameY = 18;
				Main.tile[x - 1, y].frameX = num;
				Main.tile[x - 1, y].type = (byte)type;
				Main.tile[x, y].active = true;
				Main.tile[x, y].frameY = 18;
                Main.tile[x, y].frameX = (short)(18 + num);
				Main.tile[x, y].type = (byte)type;
				Main.tile[x + 1, y].active = true;
				Main.tile[x + 1, y].frameY = 18;
                Main.tile[x + 1, y].frameX = (short)(36 + num);
				Main.tile[x + 1, y].type = (byte)type;
				Main.tile[x + 2, y].active = true;
				Main.tile[x + 2, y].frameY = 18;
                Main.tile[x + 2, y].frameX = (short)(54 + num);
				Main.tile[x + 2, y].type = (byte)type;
			}
		}
		public static void Place2x2(int x, int superY, int type)
		{
			int num = superY;
			if (type == 95)
			{
				num++;
			}
			if (x < 5 || x > Main.maxTilesX - 5 || num < 5 || num > Main.maxTilesY - 5)
			{
				return;
			}
			bool flag = true;
			for (int i = x - 1; i < x + 1; i++)
			{
				for (int j = num - 1; j < num + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
					if (Main.tile[i, j].active)
					{
						flag = false;
					}
					if (type == 98 && Main.tile[i, j].liquid > 0)
					{
						flag = false;
					}
				}
				if (type == 95)
				{
					if (Main.tile[i, num - 2] == null)
					{
						Main.tile[i, num - 2] = new Tile();
					}
					if (!Main.tile[i, num - 2].active || !Main.tileSolid[(int)Main.tile[i, num - 2].type] || Main.tileSolidTop[(int)Main.tile[i, num - 2].type])
					{
						flag = false;
					}
				}
				else
				{
					if (Main.tile[i, num + 1] == null)
					{
						Main.tile[i, num + 1] = new Tile();
					}
					if (!Main.tile[i, num + 1].active || (!Main.tileSolid[(int)Main.tile[i, num + 1].type] && !Main.tileTable[(int)Main.tile[i, num + 1].type]))
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				Main.tile[x - 1, num - 1].active = true;
				Main.tile[x - 1, num - 1].frameY = 0;
				Main.tile[x - 1, num - 1].frameX = 0;
				Main.tile[x - 1, num - 1].type = (byte)type;
				Main.tile[x, num - 1].active = true;
				Main.tile[x, num - 1].frameY = 0;
				Main.tile[x, num - 1].frameX = 18;
				Main.tile[x, num - 1].type = (byte)type;
				Main.tile[x - 1, num].active = true;
				Main.tile[x - 1, num].frameY = 18;
				Main.tile[x - 1, num].frameX = 0;
				Main.tile[x - 1, num].type = (byte)type;
				Main.tile[x, num].active = true;
				Main.tile[x, num].frameY = 18;
				Main.tile[x, num].frameX = 18;
				Main.tile[x, num].type = (byte)type;
			}
		}
		public static void Place3x4(int x, int y, int type)
		{
			if (x < 5 || x > Main.maxTilesX - 5 || y < 5 || y > Main.maxTilesY - 5)
			{
				return;
			}
			bool flag = true;
			for (int i = x - 1; i < x + 2; i++)
			{
				for (int j = y - 3; j < y + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
					if (Main.tile[i, j].active)
					{
						flag = false;
					}
				}
				if (Main.tile[i, y + 1] == null)
				{
					Main.tile[i, y + 1] = new Tile();
				}
				if (!Main.tile[i, y + 1].active || !Main.tileSolid[(int)Main.tile[i, y + 1].type])
				{
					flag = false;
				}
			}
			if (flag)
			{
				for (int k = -3; k <= 0; k++)
				{
					short frameY = (short)((3 + k) * 18);
					Main.tile[x - 1, y + k].active = true;
					Main.tile[x - 1, y + k].frameY = frameY;
					Main.tile[x - 1, y + k].frameX = 0;
					Main.tile[x - 1, y + k].type = (byte)type;
					Main.tile[x, y + k].active = true;
					Main.tile[x, y + k].frameY = frameY;
					Main.tile[x, y + k].frameX = 18;
					Main.tile[x, y + k].type = (byte)type;
					Main.tile[x + 1, y + k].active = true;
					Main.tile[x + 1, y + k].frameY = frameY;
					Main.tile[x + 1, y + k].frameX = 36;
					Main.tile[x + 1, y + k].type = (byte)type;
				}
			}
		}
		public static void Place3x2(int x, int y, int type)
		{
			if (x < 5 || x > Main.maxTilesX - 5 || y < 5 || y > Main.maxTilesY - 5)
			{
				return;
			}
			bool flag = true;
			for (int i = x - 1; i < x + 2; i++)
			{
				for (int j = y - 1; j < y + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
					if (Main.tile[i, j].active)
					{
						flag = false;
					}
				}
				if (Main.tile[i, y + 1] == null)
				{
					Main.tile[i, y + 1] = new Tile();
				}
				if (!Main.tile[i, y + 1].active || !Main.tileSolid[(int)Main.tile[i, y + 1].type])
				{
					flag = false;
				}
			}
			if (flag)
			{
				Main.tile[x - 1, y - 1].active = true;
				Main.tile[x - 1, y - 1].frameY = 0;
				Main.tile[x - 1, y - 1].frameX = 0;
				Main.tile[x - 1, y - 1].type = (byte)type;
				Main.tile[x, y - 1].active = true;
				Main.tile[x, y - 1].frameY = 0;
				Main.tile[x, y - 1].frameX = 18;
				Main.tile[x, y - 1].type = (byte)type;
				Main.tile[x + 1, y - 1].active = true;
				Main.tile[x + 1, y - 1].frameY = 0;
				Main.tile[x + 1, y - 1].frameX = 36;
				Main.tile[x + 1, y - 1].type = (byte)type;
				Main.tile[x - 1, y].active = true;
				Main.tile[x - 1, y].frameY = 18;
				Main.tile[x - 1, y].frameX = 0;
				Main.tile[x - 1, y].type = (byte)type;
				Main.tile[x, y].active = true;
				Main.tile[x, y].frameY = 18;
				Main.tile[x, y].frameX = 18;
				Main.tile[x, y].type = (byte)type;
				Main.tile[x + 1, y].active = true;
				Main.tile[x + 1, y].frameY = 18;
				Main.tile[x + 1, y].frameX = 36;
				Main.tile[x + 1, y].type = (byte)type;
			}
		}
		public static void Check3x3(int i, int j, int type)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			bool flag = false;
			int num = i + (int)(Main.tile[i, j].frameX / 18 * -1);
			int num2 = j + (int)(Main.tile[i, j].frameY / 18 * -1);
			for (int k = num; k < num + 3; k++)
			{
				for (int l = num2; l < num2 + 3; l++)
				{
					if (Main.tile[k, l] == null)
					{
						Main.tile[k, l] = new Tile();
					}
					if (!Main.tile[k, l].active || (int)Main.tile[k, l].type != type || (int)Main.tile[k, l].frameX != (k - num) * 18 || (int)Main.tile[k, l].frameY != (l - num2) * 18)
					{
						flag = true;
					}
				}
			}
			if (type == 106)
			{
				for (int m = num; m < num + 3; m++)
				{
					if (Main.tile[m, num2 + 3] == null)
					{
						Main.tile[m, num2 + 3] = new Tile();
					}
					if (!Main.tile[m, num2 + 3].active || !Main.tileSolid[(int)Main.tile[m, num2 + 3].type])
					{
						flag = true;
						break;
					}
				}
			}
			else
			{
				if (Main.tile[num + 1, num2 - 1] == null)
				{
					Main.tile[num + 1, num2 - 1] = new Tile();
				}
				if (!Main.tile[num + 1, num2 - 1].active || !Main.tileSolid[(int)Main.tile[num + 1, num2 - 1].type] || Main.tileSolidTop[(int)Main.tile[num + 1, num2 - 1].type])
				{
					flag = true;
				}
			}
			if (flag)
			{
				WorldGen.destroyObject = true;
				for (int n = num; n < num + 3; n++)
				{
					for (int num3 = num2; num3 < num2 + 3; num3++)
					{
						if ((int)Main.tile[n, num3].type == type && Main.tile[n, num3].active)
						{
							WorldGen.KillTile(n, num3, false, false, false);
						}
					}
				}
				if (type == 34)
				{
					Item.NewItem(i * 16, j * 16, 32, 32, 106, 1, false);
				}
				else
				{
					if (type == 35)
					{
						Item.NewItem(i * 16, j * 16, 32, 32, 107, 1, false);
					}
					else
					{
						if (type == 36)
						{
							Item.NewItem(i * 16, j * 16, 32, 32, 108, 1, false);
						}
						else
						{
							if (type == 106)
							{
								Item.NewItem(i * 16, j * 16, 32, 32, 363, 1, false);
							}
						}
					}
				}
				WorldGen.destroyObject = false;
				for (int num4 = num - 1; num4 < num + 4; num4++)
				{
					for (int num5 = num2 - 1; num5 < num2 + 4; num5++)
					{
						WorldGen.TileFrame(num4, num5, false, false);
					}
				}
			}
		}
		public static void Place3x3(int x, int y, int type)
		{
			bool flag = true;
			int num = 0;
			if (type == 106)
			{
				num = -2;
				for (int i = x - 1; i < x + 2; i++)
				{
					for (int j = y - 2; j < y + 1; j++)
					{
						if (Main.tile[i, j] == null)
						{
							Main.tile[i, j] = new Tile();
						}
						if (Main.tile[i, j].active)
						{
							flag = false;
						}
					}
				}
				for (int k = x - 1; k < x + 2; k++)
				{
					if (Main.tile[k, y + 1] == null)
					{
						Main.tile[k, y + 1] = new Tile();
					}
					if (!Main.tile[k, y + 1].active || !Main.tileSolid[(int)Main.tile[k, y + 1].type])
					{
						flag = false;
						break;
					}
				}
			}
			else
			{
				for (int l = x - 1; l < x + 2; l++)
				{
					for (int m = y; m < y + 3; m++)
					{
						if (Main.tile[l, m] == null)
						{
							Main.tile[l, m] = new Tile();
						}
						if (Main.tile[l, m].active)
						{
							flag = false;
						}
					}
				}
				if (Main.tile[x, y - 1] == null)
				{
					Main.tile[x, y - 1] = new Tile();
				}
				if (!Main.tile[x, y - 1].active || !Main.tileSolid[(int)Main.tile[x, y - 1].type] || Main.tileSolidTop[(int)Main.tile[x, y - 1].type])
				{
					flag = false;
				}
			}
			if (flag)
			{
				Main.tile[x - 1, y + num].active = true;
				Main.tile[x - 1, y + num].frameY = 0;
				Main.tile[x - 1, y + num].frameX = 0;
				Main.tile[x - 1, y + num].type = (byte)type;
				Main.tile[x, y + num].active = true;
				Main.tile[x, y + num].frameY = 0;
				Main.tile[x, y + num].frameX = 18;
				Main.tile[x, y + num].type = (byte)type;
				Main.tile[x + 1, y + num].active = true;
				Main.tile[x + 1, y + num].frameY = 0;
				Main.tile[x + 1, y + num].frameX = 36;
				Main.tile[x + 1, y + num].type = (byte)type;
				Main.tile[x - 1, y + 1 + num].active = true;
				Main.tile[x - 1, y + 1 + num].frameY = 18;
				Main.tile[x - 1, y + 1 + num].frameX = 0;
				Main.tile[x - 1, y + 1 + num].type = (byte)type;
				Main.tile[x, y + 1 + num].active = true;
				Main.tile[x, y + 1 + num].frameY = 18;
				Main.tile[x, y + 1 + num].frameX = 18;
				Main.tile[x, y + 1 + num].type = (byte)type;
				Main.tile[x + 1, y + 1 + num].active = true;
				Main.tile[x + 1, y + 1 + num].frameY = 18;
				Main.tile[x + 1, y + 1 + num].frameX = 36;
				Main.tile[x + 1, y + 1 + num].type = (byte)type;
				Main.tile[x - 1, y + 2 + num].active = true;
				Main.tile[x - 1, y + 2 + num].frameY = 36;
				Main.tile[x - 1, y + 2 + num].frameX = 0;
				Main.tile[x - 1, y + 2 + num].type = (byte)type;
				Main.tile[x, y + 2 + num].active = true;
				Main.tile[x, y + 2 + num].frameY = 36;
				Main.tile[x, y + 2 + num].frameX = 18;
				Main.tile[x, y + 2 + num].type = (byte)type;
				Main.tile[x + 1, y + 2 + num].active = true;
				Main.tile[x + 1, y + 2 + num].frameY = 36;
				Main.tile[x + 1, y + 2 + num].frameX = 36;
				Main.tile[x + 1, y + 2 + num].type = (byte)type;
			}
		}
		public static void PlaceSunflower(int x, int y, int type = 27)
		{
			if ((double)y > Main.worldSurface - 1.0)
			{
				return;
			}
			bool flag = true;
			for (int i = x; i < x + 2; i++)
			{
				for (int j = y - 3; j < y + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
					if (Main.tile[i, j].active || Main.tile[i, j].wall > 0)
					{
						flag = false;
					}
				}
				if (Main.tile[i, y + 1] == null)
				{
					Main.tile[i, y + 1] = new Tile();
				}
				if (!Main.tile[i, y + 1].active || Main.tile[i, y + 1].type != 2)
				{
					flag = false;
				}
			}
			if (flag)
			{
				for (int k = 0; k < 2; k++)
				{
					for (int l = -3; l < 1; l++)
					{
						int num = k * 18 + WorldGen.genRand.Next(3) * 36;
						int num2 = (l + 3) * 18;
						Main.tile[x + k, y + l].active = true;
						Main.tile[x + k, y + l].frameX = (short)num;
						Main.tile[x + k, y + l].frameY = (short)num2;
						Main.tile[x + k, y + l].type = (byte)type;
					}
				}
			}
		}
		public static void CheckSunflower(int i, int j, int type = 27)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			bool flag = false;
			int k = 0;
			k += (int)(Main.tile[i, j].frameX / 18);
			int num = j + (int)(Main.tile[i, j].frameY / 18 * -1);
			while (k > 1)
			{
				k -= 2;
			}
			k *= -1;
			k += i;
			for (int l = k; l < k + 2; l++)
			{
				for (int m = num; m < num + 4; m++)
				{
					if (Main.tile[l, m] == null)
					{
						Main.tile[l, m] = new Tile();
					}
					int n;
					for (n = (int)(Main.tile[l, m].frameX / 18); n > 1; n -= 2)
					{
					}
					if (!Main.tile[l, m].active || (int)Main.tile[l, m].type != type || n != l - k || (int)Main.tile[l, m].frameY != (m - num) * 18)
					{
						flag = true;
					}
				}
				if (Main.tile[l, num + 4] == null)
				{
					Main.tile[l, num + 4] = new Tile();
				}
				if (!Main.tile[l, num + 4].active || Main.tile[l, num + 4].type != 2)
				{
					flag = true;
				}
			}
			if (flag)
			{
				WorldGen.destroyObject = true;
				for (int num2 = k; num2 < k + 2; num2++)
				{
					for (int num3 = num; num3 < num + 4; num3++)
					{
						if ((int)Main.tile[num2, num3].type == type && Main.tile[num2, num3].active)
						{
							WorldGen.KillTile(num2, num3, false, false, false);
						}
					}
				}
				Item.NewItem(i * 16, j * 16, 32, 32, 63, 1, false);
				WorldGen.destroyObject = false;
			}
		}
		public static bool PlacePot(int x, int y, int type = 28)
		{
			bool flag = true;
			for (int i = x; i < x + 2; i++)
			{
				for (int j = y - 1; j < y + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
					if (Main.tile[i, j].active)
					{
						flag = false;
					}
				}
				if (Main.tile[i, y + 1] == null)
				{
					Main.tile[i, y + 1] = new Tile();
				}
				if (!Main.tile[i, y + 1].active || !Main.tileSolid[(int)Main.tile[i, y + 1].type])
				{
					flag = false;
				}
			}
			if (flag)
			{
				for (int k = 0; k < 2; k++)
				{
					for (int l = -1; l < 1; l++)
					{
						int num = k * 18 + WorldGen.genRand.Next(3) * 36;
						int num2 = (l + 1) * 18;
						Main.tile[x + k, y + l].active = true;
						Main.tile[x + k, y + l].frameX = (short)num;
						Main.tile[x + k, y + l].frameY = (short)num2;
						Main.tile[x + k, y + l].type = (byte)type;
					}
				}
				return true;
			}
			return false;
		}
		public static bool CheckCactus(int i, int j)
		{
			int num = j;
			int num2 = i;
			while (Main.tile[num2, num].active && Main.tile[num2, num].type == 80)
			{
				num++;
				if (!Main.tile[num2, num].active || Main.tile[num2, num].type != 80)
				{
					if (Main.tile[num2 - 1, num].active && Main.tile[num2 - 1, num].type == 80 && Main.tile[num2 - 1, num - 1].active && Main.tile[num2 - 1, num - 1].type == 80 && num2 >= i)
					{
						num2--;
					}
					if (Main.tile[num2 + 1, num].active && Main.tile[num2 + 1, num].type == 80 && Main.tile[num2 + 1, num - 1].active && Main.tile[num2 + 1, num - 1].type == 80 && num2 <= i)
					{
						num2++;
					}
				}
			}
			if (!Main.tile[num2, num].active || Main.tile[num2, num].type != 53)
			{
				WorldGen.KillTile(i, j, false, false, false);
				return true;
			}
			if (i != num2)
			{
				if ((!Main.tile[i, j + 1].active || Main.tile[i, j + 1].type != 80) && (!Main.tile[i - 1, j].active || Main.tile[i - 1, j].type != 80) && (!Main.tile[i + 1, j].active || Main.tile[i + 1, j].type != 80))
				{
					WorldGen.KillTile(i, j, false, false, false);
					return true;
				}
			}
			else
			{
				if (i == num2 && (!Main.tile[i, j + 1].active || (Main.tile[i, j + 1].type != 80 && Main.tile[i, j + 1].type != 53)))
				{
					WorldGen.KillTile(i, j, false, false, false);
					return true;
				}
			}
			return false;
		}
		public static void PlantCactus(int i, int j)
		{
			WorldGen.GrowCactus(i, j);
			for (int k = 0; k < 150; k++)
			{
				int i2 = WorldGen.genRand.Next(i - 1, i + 2);
				int j2 = WorldGen.genRand.Next(j - 10, j + 2);
				WorldGen.GrowCactus(i2, j2);
			}
		}
		public static void CactusFrame(int i, int j)
		{
			try
			{
				int num = j;
				int num2 = i;
				if (!WorldGen.CheckCactus(i, j))
				{
					while (Main.tile[num2, num].active && Main.tile[num2, num].type == 80)
					{
						num++;
						if (!Main.tile[num2, num].active || Main.tile[num2, num].type != 80)
						{
							if (Main.tile[num2 - 1, num].active && Main.tile[num2 - 1, num].type == 80 && Main.tile[num2 - 1, num - 1].active && Main.tile[num2 - 1, num - 1].type == 80 && num2 >= i)
							{
								num2--;
							}
							if (Main.tile[num2 + 1, num].active && Main.tile[num2 + 1, num].type == 80 && Main.tile[num2 + 1, num - 1].active && Main.tile[num2 + 1, num - 1].type == 80 && num2 <= i)
							{
								num2++;
							}
						}
					}
					num--;
					int num3 = i - num2;
					num2 = i;
					num = j;
					int type = (int)Main.tile[i - 2, j].type;
					int num4 = (int)Main.tile[i - 1, j].type;
					int num5 = (int)Main.tile[i + 1, j].type;
					int num6 = (int)Main.tile[i, j - 1].type;
					int num7 = (int)Main.tile[i, j + 1].type;
					int num8 = (int)Main.tile[i - 1, j + 1].type;
					int num9 = (int)Main.tile[i + 1, j + 1].type;
					if (!Main.tile[i - 1, j].active)
					{
						num4 = -1;
					}
					if (!Main.tile[i + 1, j].active)
					{
						num5 = -1;
					}
					if (!Main.tile[i, j - 1].active)
					{
						num6 = -1;
					}
					if (!Main.tile[i, j + 1].active)
					{
						num7 = -1;
					}
					if (!Main.tile[i - 1, j + 1].active)
					{
						num8 = -1;
					}
					if (!Main.tile[i + 1, j + 1].active)
					{
						num9 = -1;
					}
					short num10 = Main.tile[i, j].frameX;
					short num11 = Main.tile[i, j].frameY;
					if (num3 == 0)
					{
						if (num6 != 80)
						{
							if (num4 == 80 && num5 == 80 && num8 != 80 && num9 != 80 && type != 80)
							{
								num10 = 90;
								num11 = 0;
							}
							else
							{
								if (num4 == 80 && num8 != 80 && type != 80)
								{
									num10 = 72;
									num11 = 0;
								}
								else
								{
									if (num5 == 80 && num9 != 80)
									{
										num10 = 18;
										num11 = 0;
									}
									else
									{
										num10 = 0;
										num11 = 0;
									}
								}
							}
						}
						else
						{
							if (num4 == 80 && num5 == 80 && num8 != 80 && num9 != 80 && type != 80)
							{
								num10 = 90;
								num11 = 36;
							}
							else
							{
								if (num4 == 80 && num8 != 80 && type != 80)
								{
									num10 = 72;
									num11 = 36;
								}
								else
								{
									if (num5 == 80 && num9 != 80)
									{
										num10 = 18;
										num11 = 36;
									}
									else
									{
										if (num7 >= 0 && Main.tileSolid[num7])
										{
											num10 = 0;
											num11 = 36;
										}
										else
										{
											num10 = 0;
											num11 = 18;
										}
									}
								}
							}
						}
					}
					else
					{
						if (num3 == -1)
						{
							if (num5 == 80)
							{
								if (num6 != 80 && num7 != 80)
								{
									num10 = 108;
									num11 = 36;
								}
								else
								{
									if (num7 != 80)
									{
										num10 = 54;
										num11 = 36;
									}
									else
									{
										if (num6 != 80)
										{
											num10 = 54;
											num11 = 0;
										}
										else
										{
											num10 = 54;
											num11 = 18;
										}
									}
								}
							}
							else
							{
								if (num6 != 80)
								{
									num10 = 54;
									num11 = 0;
								}
								else
								{
									num10 = 54;
									num11 = 18;
								}
							}
						}
						else
						{
							if (num3 == 1)
							{
								if (num4 == 80)
								{
									if (num6 != 80 && num7 != 80)
									{
										num10 = 108;
										num11 = 16;
									}
									else
									{
										if (num7 != 80)
										{
											num10 = 36;
											num11 = 36;
										}
										else
										{
											if (num6 != 80)
											{
												num10 = 36;
												num11 = 0;
											}
											else
											{
												num10 = 36;
												num11 = 18;
											}
										}
									}
								}
								else
								{
									if (num6 != 80)
									{
										num10 = 36;
										num11 = 0;
									}
									else
									{
										num10 = 36;
										num11 = 18;
									}
								}
							}
						}
					}
					if (num10 != Main.tile[i, j].frameX || num11 != Main.tile[i, j].frameY)
					{
						Main.tile[i, j].frameX = num10;
						Main.tile[i, j].frameY = num11;
						WorldGen.SquareTileFrame(i, j, true);
					}
				}
			}
			catch
			{
				Main.tile[i, j].frameX = 0;
				Main.tile[i, j].frameY = 0;
			}
		}
		public static void GrowCactus(int i, int j)
		{
			int num = j;
			int num2 = i;
			if (!Main.tile[i, j].active)
			{
				return;
			}
			if (Main.tile[i, j - 1].liquid > 0)
			{
				return;
			}
			if (Main.tile[i, j].type != 53 && Main.tile[i, j].type != 80)
			{
				return;
			}
			if (Main.tile[i, j].type == 53)
			{
				if (Main.tile[i, j - 1].active || Main.tile[i - 1, j - 1].active || Main.tile[i + 1, j - 1].active)
				{
					return;
				}
				int num3 = 0;
				int num4 = 0;
				for (int k = i - 6; k <= i + 6; k++)
				{
					for (int l = j - 3; l <= j + 1; l++)
					{
						try
						{
							if (Main.tile[k, l].active)
							{
								if (Main.tile[k, l].type == 80)
								{
									num3++;
									if (num3 >= 4)
									{
										return;
									}
								}
								if (Main.tile[k, l].type == 53)
								{
									num4++;
								}
							}
						}
						catch
						{
						}
					}
				}
				if (num4 > 10)
				{
					Main.tile[i, j - 1].active = true;
					Main.tile[i, j - 1].type = 80;
					if (Main.netMode == 2)
					{
						NetMessage.SendTileSquare(-1, i, j - 1, 1);
					}
					WorldGen.SquareTileFrame(num2, num - 1, true);
					return;
				}
				return;
			}
			else
			{
				if (Main.tile[i, j].type != 80)
				{
					return;
				}
				while (Main.tile[num2, num].active && Main.tile[num2, num].type == 80)
				{
					num++;
					if (!Main.tile[num2, num].active || Main.tile[num2, num].type != 80)
					{
						if (Main.tile[num2 - 1, num].active && Main.tile[num2 - 1, num].type == 80 && Main.tile[num2 - 1, num - 1].active && Main.tile[num2 - 1, num - 1].type == 80 && num2 >= i)
						{
							num2--;
						}
						if (Main.tile[num2 + 1, num].active && Main.tile[num2 + 1, num].type == 80 && Main.tile[num2 + 1, num - 1].active && Main.tile[num2 + 1, num - 1].type == 80 && num2 <= i)
						{
							num2++;
						}
					}
				}
				num--;
				int num5 = num - j;
				int num6 = i - num2;
				num2 = i - num6;
				num = j;
				int num7 = 11 - num5;
				int num8 = 0;
				for (int m = num2 - 2; m <= num2 + 2; m++)
				{
					for (int n = num - num7; n <= num + num5; n++)
					{
						if (Main.tile[m, n].active && Main.tile[m, n].type == 80)
						{
							num8++;
						}
					}
				}
				if (num8 < WorldGen.genRand.Next(11, 13))
				{
					num2 = i;
					num = j;
					if (num6 == 0)
					{
						if (num5 == 0)
						{
							if (Main.tile[num2, num - 1].active)
							{
								return;
							}
							Main.tile[num2, num - 1].active = true;
							Main.tile[num2, num - 1].type = 80;
							WorldGen.SquareTileFrame(num2, num - 1, true);
							if (Main.netMode == 2)
							{
								NetMessage.SendTileSquare(-1, num2, num - 1, 1);
								return;
							}
							return;
						}
						else
						{
							bool flag = false;
							bool flag2 = false;
							if (Main.tile[num2, num - 1].active && Main.tile[num2, num - 1].type == 80)
							{
								if (!Main.tile[num2 - 1, num].active && !Main.tile[num2 - 2, num + 1].active && !Main.tile[num2 - 1, num - 1].active && !Main.tile[num2 - 1, num + 1].active && !Main.tile[num2 - 2, num].active)
								{
									flag = true;
								}
								if (!Main.tile[num2 + 1, num].active && !Main.tile[num2 + 2, num + 1].active && !Main.tile[num2 + 1, num - 1].active && !Main.tile[num2 + 1, num + 1].active && !Main.tile[num2 + 2, num].active)
								{
									flag2 = true;
								}
							}
							int num9 = WorldGen.genRand.Next(3);
							if (num9 == 0 && flag)
							{
								Main.tile[num2 - 1, num].active = true;
								Main.tile[num2 - 1, num].type = 80;
								WorldGen.SquareTileFrame(num2 - 1, num, true);
								if (Main.netMode == 2)
								{
									NetMessage.SendTileSquare(-1, num2 - 1, num, 1);
									return;
								}
								return;
							}
							else
							{
								if (num9 == 1 && flag2)
								{
									Main.tile[num2 + 1, num].active = true;
									Main.tile[num2 + 1, num].type = 80;
									WorldGen.SquareTileFrame(num2 + 1, num, true);
									if (Main.netMode == 2)
									{
										NetMessage.SendTileSquare(-1, num2 + 1, num, 1);
										return;
									}
									return;
								}
								else
								{
									if (num5 >= WorldGen.genRand.Next(2, 8))
									{
										return;
									}
									if (Main.tile[num2 - 1, num - 1].active)
									{
										byte arg_5E0_0 = Main.tile[num2 - 1, num - 1].type;
									}
									if (Main.tile[num2 + 1, num - 1].active && Main.tile[num2 + 1, num - 1].type == 80)
									{
										return;
									}
									if (Main.tile[num2, num - 1].active)
									{
										return;
									}
									Main.tile[num2, num - 1].active = true;
									Main.tile[num2, num - 1].type = 80;
									WorldGen.SquareTileFrame(num2, num - 1, true);
									if (Main.netMode == 2)
									{
										NetMessage.SendTileSquare(-1, num2, num - 1, 1);
										return;
									}
									return;
								}
							}
						}
					}
					else
					{
						if (Main.tile[num2, num - 1].active || Main.tile[num2, num - 2].active || Main.tile[num2 + num6, num - 1].active || !Main.tile[num2 - num6, num - 1].active || Main.tile[num2 - num6, num - 1].type != 80)
						{
							return;
						}
						Main.tile[num2, num - 1].active = true;
						Main.tile[num2, num - 1].type = 80;
						WorldGen.SquareTileFrame(num2, num - 1, true);
						if (Main.netMode == 2)
						{
							NetMessage.SendTileSquare(-1, num2, num - 1, 1);
							return;
						}
						return;
					}
				}
			}
		}
		public static void CheckPot(int i, int j, int type = 28)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			bool flag = false;
			int k = 0;
			k += (int)(Main.tile[i, j].frameX / 18);
			int num = j + (int)(Main.tile[i, j].frameY / 18 * -1);
			while (k > 1)
			{
				k -= 2;
			}
			k *= -1;
			k += i;
			for (int l = k; l < k + 2; l++)
			{
				for (int m = num; m < num + 2; m++)
				{
					if (Main.tile[l, m] == null)
					{
						Main.tile[l, m] = new Tile();
					}
					int n;
					for (n = (int)(Main.tile[l, m].frameX / 18); n > 1; n -= 2)
					{
					}
					if (!Main.tile[l, m].active || (int)Main.tile[l, m].type != type || n != l - k || (int)Main.tile[l, m].frameY != (m - num) * 18)
					{
						flag = true;
					}
				}
				if (Main.tile[l, num + 2] == null)
				{
					Main.tile[l, num + 2] = new Tile();
				}
				if (!Main.tile[l, num + 2].active || !Main.tileSolid[(int)Main.tile[l, num + 2].type])
				{
					flag = true;
				}
			}
			if (flag)
			{
				WorldGen.destroyObject = true;
				Main.PlaySound(13, i * 16, j * 16, 1);
				for (int num2 = k; num2 < k + 2; num2++)
				{
					for (int num3 = num; num3 < num + 2; num3++)
					{
						if ((int)Main.tile[num2, num3].type == type && Main.tile[num2, num3].active)
						{
							WorldGen.KillTile(num2, num3, false, false, false);
						}
					}
				}
				Gore.NewGore(new Vector2((float)(i * 16), (float)(j * 16)), default(Vector2), 51, 1f);
				Gore.NewGore(new Vector2((float)(i * 16), (float)(j * 16)), default(Vector2), 52, 1f);
				Gore.NewGore(new Vector2((float)(i * 16), (float)(j * 16)), default(Vector2), 53, 1f);
				if (WorldGen.genRand.Next(45) == 0 && (Main.tile[k, num].wall == 7 || Main.tile[k, num].wall == 8 || Main.tile[k, num].wall == 9))
				{
					Item.NewItem(i * 16, j * 16, 16, 16, 327, 1, false);
				}
				else
				{
					if (WorldGen.genRand.Next(45) == 0)
					{
						if ((double)j < Main.worldSurface)
						{
							int num4 = WorldGen.genRand.Next(4);
							if (num4 == 0)
							{
								Item.NewItem(i * 16, j * 16, 16, 16, 292, 1, false);
							}
							if (num4 == 1)
							{
								Item.NewItem(i * 16, j * 16, 16, 16, 298, 1, false);
							}
							if (num4 == 2)
							{
								Item.NewItem(i * 16, j * 16, 16, 16, 299, 1, false);
							}
							if (num4 == 3)
							{
								Item.NewItem(i * 16, j * 16, 16, 16, 290, 1, false);
							}
						}
						else
						{
							if ((double)j < Main.rockLayer)
							{
								int num5 = WorldGen.genRand.Next(7);
								if (num5 == 0)
								{
									Item.NewItem(i * 16, j * 16, 16, 16, 289, 1, false);
								}
								if (num5 == 1)
								{
									Item.NewItem(i * 16, j * 16, 16, 16, 298, 1, false);
								}
								if (num5 == 2)
								{
									Item.NewItem(i * 16, j * 16, 16, 16, 299, 1, false);
								}
								if (num5 == 3)
								{
									Item.NewItem(i * 16, j * 16, 16, 16, 290, 1, false);
								}
								if (num5 == 4)
								{
									Item.NewItem(i * 16, j * 16, 16, 16, 303, 1, false);
								}
								if (num5 == 5)
								{
									Item.NewItem(i * 16, j * 16, 16, 16, 291, 1, false);
								}
								if (num5 == 6)
								{
									Item.NewItem(i * 16, j * 16, 16, 16, 304, 1, false);
								}
							}
							else
							{
								if (j < Main.maxTilesY - 200)
								{
									int num6 = WorldGen.genRand.Next(10);
									if (num6 == 0)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 296, 1, false);
									}
									if (num6 == 1)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 295, 1, false);
									}
									if (num6 == 2)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 299, 1, false);
									}
									if (num6 == 3)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 302, 1, false);
									}
									if (num6 == 4)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 303, 1, false);
									}
									if (num6 == 5)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 305, 1, false);
									}
									if (num6 == 6)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 301, 1, false);
									}
									if (num6 == 7)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 302, 1, false);
									}
									if (num6 == 8)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 297, 1, false);
									}
									if (num6 == 9)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 304, 1, false);
									}
								}
								else
								{
									int num7 = WorldGen.genRand.Next(12);
									if (num7 == 0)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 296, 1, false);
									}
									if (num7 == 1)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 295, 1, false);
									}
									if (num7 == 2)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 293, 1, false);
									}
									if (num7 == 3)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 288, 1, false);
									}
									if (num7 == 4)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 294, 1, false);
									}
									if (num7 == 5)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 297, 1, false);
									}
									if (num7 == 6)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 304, 1, false);
									}
									if (num7 == 7)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 305, 1, false);
									}
									if (num7 == 8)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 301, 1, false);
									}
									if (num7 == 9)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 302, 1, false);
									}
									if (num7 == 10)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 288, 1, false);
									}
									if (num7 == 11)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 300, 1, false);
									}
								}
							}
						}
					}
					else
					{
						int num8 = Main.rand.Next(8);
						if (num8 == 0 && Main.player[(int)Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16)].statLife < Main.player[(int)Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16)].statLifeMax)
						{
							Item.NewItem(i * 16, j * 16, 16, 16, 58, 1, false);
						}
						else
						{
							if (num8 == 1 && Main.player[(int)Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16)].statMana < Main.player[(int)Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16)].statManaMax)
							{
								Item.NewItem(i * 16, j * 16, 16, 16, 184, 1, false);
							}
							else
							{
								if (num8 == 2)
								{
									int stack = Main.rand.Next(1, 6);
									if (Main.tile[i, j].liquid > 0)
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 282, stack, false);
									}
									else
									{
										Item.NewItem(i * 16, j * 16, 16, 16, 8, stack, false);
									}
								}
								else
								{
									if (num8 == 3)
									{
										int stack2 = Main.rand.Next(8) + 3;
										int type2 = 40;
										if (j > Main.maxTilesY - 200)
										{
											type2 = 265;
										}
										if ((double)j < Main.rockLayer && WorldGen.genRand.Next(2) == 0)
										{
											type2 = 42;
										}
										Item.NewItem(i * 16, j * 16, 16, 16, type2, stack2, false);
									}
									else
									{
										if (num8 == 4)
										{
											int type3 = 28;
											if (j > Main.maxTilesY - 200)
											{
												type3 = 188;
											}
											Item.NewItem(i * 16, j * 16, 16, 16, type3, 1, false);
										}
										else
										{
											if (num8 == 5 && (double)j > Main.rockLayer)
											{
												int stack3 = Main.rand.Next(4) + 1;
												Item.NewItem(i * 16, j * 16, 16, 16, 166, stack3, false);
											}
											else
											{
												float num9 = (float)(200 + WorldGen.genRand.Next(-100, 101));
												if ((double)j < Main.worldSurface)
												{
													num9 *= 0.5f;
												}
												else
												{
													if ((double)j < Main.rockLayer)
													{
														num9 *= 0.75f;
													}
													else
													{
														if (j > Main.maxTilesY - 250)
														{
															num9 *= 1.25f;
														}
													}
												}
												num9 *= 1f + (float)Main.rand.Next(-20, 21) * 0.01f;
												if (Main.rand.Next(5) == 0)
												{
													num9 *= 1f + (float)Main.rand.Next(5, 11) * 0.01f;
												}
												if (Main.rand.Next(10) == 0)
												{
													num9 *= 1f + (float)Main.rand.Next(10, 21) * 0.01f;
												}
												if (Main.rand.Next(15) == 0)
												{
													num9 *= 1f + (float)Main.rand.Next(20, 41) * 0.01f;
												}
												if (Main.rand.Next(20) == 0)
												{
													num9 *= 1f + (float)Main.rand.Next(40, 81) * 0.01f;
												}
												if (Main.rand.Next(25) == 0)
												{
													num9 *= 1f + (float)Main.rand.Next(50, 101) * 0.01f;
												}
												while ((int)num9 > 0)
												{
													if (num9 > 1000000f)
													{
														int num10 = (int)(num9 / 1000000f);
														if (num10 > 50 && Main.rand.Next(2) == 0)
														{
															num10 /= Main.rand.Next(3) + 1;
														}
														if (Main.rand.Next(2) == 0)
														{
															num10 /= Main.rand.Next(3) + 1;
														}
														num9 -= (float)(1000000 * num10);
														Item.NewItem(i * 16, j * 16, 16, 16, 74, num10, false);
													}
													else
													{
														if (num9 > 10000f)
														{
															int num11 = (int)(num9 / 10000f);
															if (num11 > 50 && Main.rand.Next(2) == 0)
															{
																num11 /= Main.rand.Next(3) + 1;
															}
															if (Main.rand.Next(2) == 0)
															{
																num11 /= Main.rand.Next(3) + 1;
															}
															num9 -= (float)(10000 * num11);
															Item.NewItem(i * 16, j * 16, 16, 16, 73, num11, false);
														}
														else
														{
															if (num9 > 100f)
															{
																int num12 = (int)(num9 / 100f);
																if (num12 > 50 && Main.rand.Next(2) == 0)
																{
																	num12 /= Main.rand.Next(3) + 1;
																}
																if (Main.rand.Next(2) == 0)
																{
																	num12 /= Main.rand.Next(3) + 1;
																}
																num9 -= (float)(100 * num12);
																Item.NewItem(i * 16, j * 16, 16, 16, 72, num12, false);
															}
															else
															{
																int num13 = (int)num9;
																if (num13 > 50 && Main.rand.Next(2) == 0)
																{
																	num13 /= Main.rand.Next(3) + 1;
																}
																if (Main.rand.Next(2) == 0)
																{
																	num13 /= Main.rand.Next(4) + 1;
																}
																if (num13 < 1)
																{
																	num13 = 1;
																}
																num9 -= (float)num13;
																Item.NewItem(i * 16, j * 16, 16, 16, 71, num13, false);
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
				WorldGen.destroyObject = false;
			}
		}
		public static int PlaceChest(int x, int y, int type = 21, bool notNearOtherChests = false, int style = 0)
		{
			bool flag = true;
			int num = -1;
			for (int i = x; i < x + 2; i++)
			{
				for (int j = y - 1; j < y + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
					if (Main.tile[i, j].active)
					{
						flag = false;
					}
					if (Main.tile[i, j].lava)
					{
						flag = false;
					}
				}
				if (Main.tile[i, y + 1] == null)
				{
					Main.tile[i, y + 1] = new Tile();
				}
				if (!Main.tile[i, y + 1].active || !Main.tileSolid[(int)Main.tile[i, y + 1].type])
				{
					flag = false;
				}
			}
			if (flag && notNearOtherChests)
			{
				for (int k = x - 25; k < x + 25; k++)
				{
					for (int l = y - 8; l < y + 8; l++)
					{
						try
						{
							if (Main.tile[k, l].active && Main.tile[k, l].type == 21)
							{
								flag = false;
								return -1;
							}
						}
						catch
						{
						}
					}
				}
			}
			if (flag)
			{
				num = Chest.CreateChest(x, y - 1);
				if (num == -1)
				{
					flag = false;
				}
			}
			if (flag)
			{
				Main.tile[x, y - 1].active = true;
				Main.tile[x, y - 1].frameY = 0;
				Main.tile[x, y - 1].frameX = (short)(36 * style);
				Main.tile[x, y - 1].type = (byte)type;
				Main.tile[x + 1, y - 1].active = true;
				Main.tile[x + 1, y - 1].frameY = 0;
				Main.tile[x + 1, y - 1].frameX = (short)(18 + 36 * style);
				Main.tile[x + 1, y - 1].type = (byte)type;
				Main.tile[x, y].active = true;
				Main.tile[x, y].frameY = 18;
				Main.tile[x, y].frameX = (short)(36 * style);
				Main.tile[x, y].type = (byte)type;
				Main.tile[x + 1, y].active = true;
				Main.tile[x + 1, y].frameY = 18;
				Main.tile[x + 1, y].frameX = (short)(18 + 36 * style);
				Main.tile[x + 1, y].type = (byte)type;
			}
			return num;
		}
		public static void CheckChest(int i, int j, int type)
		{
			if (WorldGen.destroyObject)
			{
				return;
			}
			bool flag = false;
			int k = 0;
			k += (int)(Main.tile[i, j].frameX / 18);
			int num = j + (int)(Main.tile[i, j].frameY / 18 * -1);
			while (k > 1)
			{
				k -= 2;
			}
			k *= -1;
			k += i;
			for (int l = k; l < k + 2; l++)
			{
				for (int m = num; m < num + 2; m++)
				{
					if (Main.tile[l, m] == null)
					{
						Main.tile[l, m] = new Tile();
					}
					int n;
					for (n = (int)(Main.tile[l, m].frameX / 18); n > 1; n -= 2)
					{
					}
					if (!Main.tile[l, m].active || (int)Main.tile[l, m].type != type || n != l - k || (int)Main.tile[l, m].frameY != (m - num) * 18)
					{
						flag = true;
					}
				}
				if (Main.tile[l, num + 2] == null)
				{
					Main.tile[l, num + 2] = new Tile();
				}
				if (!Main.tile[l, num + 2].active || !Main.tileSolid[(int)Main.tile[l, num + 2].type])
				{
					flag = true;
				}
			}
			if (flag)
			{
				int type2 = 48;
				if (Main.tile[i, j].frameX >= 216)
				{
					type2 = 348;
				}
				else
				{
					if (Main.tile[i, j].frameX >= 180)
					{
						type2 = 343;
					}
					else
					{
						if (Main.tile[i, j].frameX >= 108)
						{
							type2 = 328;
						}
						else
						{
							if (Main.tile[i, j].frameX >= 36)
							{
								type2 = 306;
							}
						}
					}
				}
				WorldGen.destroyObject = true;
				for (int num2 = k; num2 < k + 2; num2++)
				{
					for (int num3 = num; num3 < num + 3; num3++)
					{
						if ((int)Main.tile[num2, num3].type == type && Main.tile[num2, num3].active)
						{
							Chest.DestroyChest(num2, num3);
							WorldGen.KillTile(num2, num3, false, false, false);
						}
					}
				}
				Item.NewItem(i * 16, j * 16, 32, 32, type2, 1, false);
				WorldGen.destroyObject = false;
			}
		}
		public static bool PlaceTile(int i, int j, int type, bool mute = false, bool forced = false, int plr = -1, int style = 0)
		{
			if (type >= 107)
			{
				return false;
			}
			bool result = false;
			if (i >= 0 && j >= 0 && i < Main.maxTilesX && j < Main.maxTilesY)
			{
				if (Main.tile[i, j] == null)
				{
					Main.tile[i, j] = new Tile();
				}
				if (forced || Collision.EmptyTile(i, j, false) || !Main.tileSolid[type] || (type == 23 && Main.tile[i, j].type == 0 && Main.tile[i, j].active) || (type == 2 && Main.tile[i, j].type == 0 && Main.tile[i, j].active) || (type == 60 && Main.tile[i, j].type == 59 && Main.tile[i, j].active) || (type == 70 && Main.tile[i, j].type == 59 && Main.tile[i, j].active))
				{
					if (type == 23 && (Main.tile[i, j].type != 0 || !Main.tile[i, j].active))
					{
						return false;
					}
					if (type == 2 && (Main.tile[i, j].type != 0 || !Main.tile[i, j].active))
					{
						return false;
					}
					if (type == 60 && (Main.tile[i, j].type != 59 || !Main.tile[i, j].active))
					{
						return false;
					}
					if (type == 81)
					{
						if (Main.tile[i - 1, j] == null)
						{
							Main.tile[i - 1, j] = new Tile();
						}
						if (Main.tile[i + 1, j] == null)
						{
							Main.tile[i + 1, j] = new Tile();
						}
						if (Main.tile[i, j - 1] == null)
						{
							Main.tile[i, j - 1] = new Tile();
						}
						if (Main.tile[i, j + 1] == null)
						{
							Main.tile[i, j + 1] = new Tile();
						}
						if (Main.tile[i - 1, j].active || Main.tile[i + 1, j].active || Main.tile[i, j - 1].active)
						{
							return false;
						}
						if (!Main.tile[i, j + 1].active || !Main.tileSolid[(int)Main.tile[i, j + 1].type])
						{
							return false;
						}
					}
					if (Main.tile[i, j].liquid > 0 && (type == 3 || type == 4 || type == 20 || type == 24 || type == 27 || type == 32 || type == 51 || type == 69 || type == 72))
					{
						return false;
					}
					Main.tile[i, j].frameY = 0;
					Main.tile[i, j].frameX = 0;
					if (type == 3 || type == 24)
					{
						if (j + 1 < Main.maxTilesY && Main.tile[i, j + 1].active && ((Main.tile[i, j + 1].type == 2 && type == 3) || (Main.tile[i, j + 1].type == 23 && type == 24) || (Main.tile[i, j + 1].type == 78 && type == 3)))
						{
							if (type == 24 && WorldGen.genRand.Next(13) == 0)
							{
								Main.tile[i, j].active = true;
								Main.tile[i, j].type = 32;
								WorldGen.SquareTileFrame(i, j, true);
							}
							else
							{
								if (Main.tile[i, j + 1].type == 78)
								{
									Main.tile[i, j].active = true;
									Main.tile[i, j].type = (byte)type;
									Main.tile[i, j].frameX = (short)(WorldGen.genRand.Next(2) * 18 + 108);
								}
								else
								{
									if (Main.tile[i, j].wall == 0 && Main.tile[i, j + 1].wall == 0)
									{
										if (WorldGen.genRand.Next(50) == 0 || (type == 24 && WorldGen.genRand.Next(40) == 0))
										{
											Main.tile[i, j].active = true;
											Main.tile[i, j].type = (byte)type;
											Main.tile[i, j].frameX = 144;
										}
										else
										{
											if (WorldGen.genRand.Next(35) == 0)
											{
												Main.tile[i, j].active = true;
												Main.tile[i, j].type = (byte)type;
												Main.tile[i, j].frameX = (short)(WorldGen.genRand.Next(2) * 18 + 108);
											}
											else
											{
												Main.tile[i, j].active = true;
												Main.tile[i, j].type = (byte)type;
												Main.tile[i, j].frameX = (short)(WorldGen.genRand.Next(6) * 18);
											}
										}
									}
								}
							}
						}
					}
					else
					{
						if (type == 61)
						{
							if (j + 1 < Main.maxTilesY && Main.tile[i, j + 1].active && Main.tile[i, j + 1].type == 60)
							{
								if (WorldGen.genRand.Next(16) == 0 && (double)j > Main.worldSurface)
								{
									Main.tile[i, j].active = true;
									Main.tile[i, j].type = 69;
									WorldGen.SquareTileFrame(i, j, true);
								}
								else
								{
									if (WorldGen.genRand.Next(60) == 0 && (double)j > Main.rockLayer)
									{
										Main.tile[i, j].active = true;
										Main.tile[i, j].type = (byte)type;
										Main.tile[i, j].frameX = 144;
									}
									else
									{
										if (WorldGen.genRand.Next(1000) == 0 && (double)j > Main.rockLayer)
										{
											Main.tile[i, j].active = true;
											Main.tile[i, j].type = (byte)type;
											Main.tile[i, j].frameX = 162;
										}
										else
										{
											if (WorldGen.genRand.Next(15) == 0)
											{
												Main.tile[i, j].active = true;
												Main.tile[i, j].type = (byte)type;
												Main.tile[i, j].frameX = (short)(WorldGen.genRand.Next(2) * 18 + 108);
											}
											else
											{
												Main.tile[i, j].active = true;
												Main.tile[i, j].type = (byte)type;
												Main.tile[i, j].frameX = (short)(WorldGen.genRand.Next(6) * 18);
											}
										}
									}
								}
							}
						}
						else
						{
							if (type == 71)
							{
								if (j + 1 < Main.maxTilesY && Main.tile[i, j + 1].active && Main.tile[i, j + 1].type == 70)
								{
									Main.tile[i, j].active = true;
									Main.tile[i, j].type = (byte)type;
									Main.tile[i, j].frameX = (short)(WorldGen.genRand.Next(5) * 18);
								}
							}
							else
							{
								if (type == 4)
								{
									if (Main.tile[i - 1, j] == null)
									{
										Main.tile[i - 1, j] = new Tile();
									}
									if (Main.tile[i + 1, j] == null)
									{
										Main.tile[i + 1, j] = new Tile();
									}
									if (Main.tile[i, j + 1] == null)
									{
										Main.tile[i, j + 1] = new Tile();
									}
									if ((Main.tile[i - 1, j].active && (Main.tileSolid[(int)Main.tile[i - 1, j].type] || (Main.tile[i - 1, j].type == 5 && Main.tile[i - 1, j - 1].type == 5 && Main.tile[i - 1, j + 1].type == 5))) || (Main.tile[i + 1, j].active && (Main.tileSolid[(int)Main.tile[i + 1, j].type] || (Main.tile[i + 1, j].type == 5 && Main.tile[i + 1, j - 1].type == 5 && Main.tile[i + 1, j + 1].type == 5))) || (Main.tile[i, j + 1].active && Main.tileSolid[(int)Main.tile[i, j + 1].type]))
									{
										Main.tile[i, j].active = true;
										Main.tile[i, j].type = (byte)type;
										WorldGen.SquareTileFrame(i, j, true);
									}
								}
								else
								{
									if (type == 10)
									{
										if (Main.tile[i, j - 1] == null)
										{
											Main.tile[i, j - 1] = new Tile();
										}
										if (Main.tile[i, j - 2] == null)
										{
											Main.tile[i, j - 2] = new Tile();
										}
										if (Main.tile[i, j - 3] == null)
										{
											Main.tile[i, j - 3] = new Tile();
										}
										if (Main.tile[i, j + 1] == null)
										{
											Main.tile[i, j + 1] = new Tile();
										}
										if (Main.tile[i, j + 2] == null)
										{
											Main.tile[i, j + 2] = new Tile();
										}
										if (Main.tile[i, j + 3] == null)
										{
											Main.tile[i, j + 3] = new Tile();
										}
										if (!Main.tile[i, j - 1].active && !Main.tile[i, j - 2].active && Main.tile[i, j - 3].active && Main.tileSolid[(int)Main.tile[i, j - 3].type])
										{
											WorldGen.PlaceDoor(i, j - 1, type);
											WorldGen.SquareTileFrame(i, j, true);
										}
										else
										{
											if (Main.tile[i, j + 1].active || Main.tile[i, j + 2].active || !Main.tile[i, j + 3].active || !Main.tileSolid[(int)Main.tile[i, j + 3].type])
											{
												return false;
											}
											WorldGen.PlaceDoor(i, j + 1, type);
											WorldGen.SquareTileFrame(i, j, true);
										}
									}
									else
									{
										if (type == 34 || type == 35 || type == 36 || type == 106)
										{
											WorldGen.Place3x3(i, j, type);
											WorldGen.SquareTileFrame(i, j, true);
										}
										else
										{
											if (type == 13 || type == 33 || type == 49 || type == 50 || type == 78)
											{
												WorldGen.PlaceOnTable1x1(i, j, type, style);
												WorldGen.SquareTileFrame(i, j, true);
											}
											else
											{
												if (type == 14 || type == 26 || type == 86 || type == 87 || type == 88 || type == 89)
												{
													WorldGen.Place3x2(i, j, type);
													WorldGen.SquareTileFrame(i, j, true);
												}
												else
												{
													if (type == 20)
													{
														if (Main.tile[i, j + 1] == null)
														{
															Main.tile[i, j + 1] = new Tile();
														}
														if (Main.tile[i, j + 1].active && Main.tile[i, j + 1].type == 2)
														{
															WorldGen.Place1x2(i, j, type, style);
															WorldGen.SquareTileFrame(i, j, true);
														}
													}
													else
													{
														if (type == 15)
														{
															if (Main.tile[i, j - 1] == null)
															{
																Main.tile[i, j - 1] = new Tile();
															}
															if (Main.tile[i, j] == null)
															{
																Main.tile[i, j] = new Tile();
															}
															WorldGen.Place1x2(i, j, type, style);
															WorldGen.SquareTileFrame(i, j, true);
														}
														else
														{
															if (type == 16 || type == 18 || type == 29 || type == 103)
															{
																WorldGen.Place2x1(i, j, type);
																WorldGen.SquareTileFrame(i, j, true);
															}
															else
															{
																if (type == 92 || type == 93)
																{
																	WorldGen.Place1xX(i, j, type, 0);
																	WorldGen.SquareTileFrame(i, j, true);
																}
																else
																{
																	if (type == 104 || type == 105)
																	{
																		WorldGen.Place2xX(i, j, type, 0);
																		WorldGen.SquareTileFrame(i, j, true);
																	}
																	else
																	{
																		if (type == 17 || type == 77)
																		{
																			WorldGen.Place3x2(i, j, type);
																			WorldGen.SquareTileFrame(i, j, true);
																		}
																		else
																		{
																			if (type == 21)
																			{
																				WorldGen.PlaceChest(i, j, type, false, style);
																				WorldGen.SquareTileFrame(i, j, true);
																			}
																			else
																			{
																				if (type == 91)
																				{
																					WorldGen.PlaceBanner(i, j, type, style);
																					WorldGen.SquareTileFrame(i, j, true);
																				}
																				else
																				{
																					if (type == 101 || type == 102)
																					{
																						WorldGen.Place3x4(i, j, type);
																						WorldGen.SquareTileFrame(i, j, true);
																					}
																					else
																					{
																						if (type == 27)
																						{
																							WorldGen.PlaceSunflower(i, j, 27);
																							WorldGen.SquareTileFrame(i, j, true);
																						}
																						else
																						{
																							if (type == 28)
																							{
																								WorldGen.PlacePot(i, j, 28);
																								WorldGen.SquareTileFrame(i, j, true);
																							}
																							else
																							{
																								if (type == 42)
																								{
																									WorldGen.Place1x2Top(i, j, type);
																									WorldGen.SquareTileFrame(i, j, true);
																								}
																								else
																								{
																									if (type == 55 || type == 85)
																									{
																										WorldGen.PlaceSign(i, j, type);
																									}
																									else
																									{
																										if (Main.tileAlch[type])
																										{
																											WorldGen.PlaceAlch(i, j, style);
																										}
																										else
																										{
																											if (type == 94 || type == 95 || type == 96 || type == 97 || type == 98 || type == 99 || type == 100)
																											{
																												WorldGen.Place2x2(i, j, type);
																											}
																											else
																											{
																												if (type == 79 || type == 90)
																												{
																													int direction = 1;
																													if (plr > -1)
																													{
																														direction = Main.player[plr].direction;
																													}
																													WorldGen.Place4x2(i, j, type, direction);
																												}
																												else
																												{
																													if (type == 81)
																													{
																														Main.tile[i, j].frameX = (short)(26 * WorldGen.genRand.Next(6));
																														Main.tile[i, j].active = true;
																														Main.tile[i, j].type = (byte)type;
																													}
																													else
																													{
																														Main.tile[i, j].active = true;
																														Main.tile[i, j].type = (byte)type;
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
					if (Main.tile[i, j].active && !mute)
					{
						WorldGen.SquareTileFrame(i, j, true);
						result = true;
						Main.PlaySound(0, i * 16, j * 16, 1);
						if (type == 22)
						{
							for (int k = 0; k < 3; k++)
							{
								Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, 14, 0f, 0f, 0, default(Color), 1f);
							}
						}
					}
				}
			}
			return result;
		}
		public static void KillWall(int i, int j, bool fail = false)
		{
			if (i >= 0 && j >= 0 && i < Main.maxTilesX && j < Main.maxTilesY)
			{
				if (Main.tile[i, j] == null)
				{
					Main.tile[i, j] = new Tile();
				}
				if (Main.tile[i, j].wall > 0)
				{
					WorldGen.genRand.Next(3);
					Main.PlaySound(0, i * 16, j * 16, 1);
					int num = 10;
					if (fail)
					{
						num = 3;
					}
					for (int k = 0; k < num; k++)
					{
						int type = 0;
						if (Main.tile[i, j].wall == 1 || Main.tile[i, j].wall == 5 || Main.tile[i, j].wall == 6 || Main.tile[i, j].wall == 7 || Main.tile[i, j].wall == 8 || Main.tile[i, j].wall == 9)
						{
							type = 1;
						}
						if (Main.tile[i, j].wall == 3)
						{
							if (WorldGen.genRand.Next(2) == 0)
							{
								type = 14;
							}
							else
							{
								type = 1;
							}
						}
						if (Main.tile[i, j].wall == 4)
						{
							type = 7;
						}
						if (Main.tile[i, j].wall == 12)
						{
							type = 9;
						}
						if (Main.tile[i, j].wall == 10)
						{
							type = 10;
						}
						if (Main.tile[i, j].wall == 11)
						{
							type = 11;
						}
						Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, type, 0f, 0f, 0, default(Color), 1f);
					}
					if (fail)
					{
						WorldGen.SquareWallFrame(i, j, true);
						return;
					}
					int num2 = 0;
					if (Main.tile[i, j].wall == 1)
					{
						num2 = 26;
					}
					if (Main.tile[i, j].wall == 4)
					{
						num2 = 93;
					}
					if (Main.tile[i, j].wall == 5)
					{
						num2 = 130;
					}
					if (Main.tile[i, j].wall == 6)
					{
						num2 = 132;
					}
					if (Main.tile[i, j].wall == 7)
					{
						num2 = 135;
					}
					if (Main.tile[i, j].wall == 8)
					{
						num2 = 138;
					}
					if (Main.tile[i, j].wall == 9)
					{
						num2 = 140;
					}
					if (Main.tile[i, j].wall == 10)
					{
						num2 = 142;
					}
					if (Main.tile[i, j].wall == 11)
					{
						num2 = 144;
					}
					if (Main.tile[i, j].wall == 12)
					{
						num2 = 146;
					}
					if (Main.tile[i, j].wall == 14)
					{
						num2 = 330;
					}
					if (Main.tile[i, j].wall == 16)
					{
						num2 = 30;
					}
					if (Main.tile[i, j].wall == 17)
					{
						num2 = 135;
					}
					if (Main.tile[i, j].wall == 18)
					{
						num2 = 138;
					}
					if (Main.tile[i, j].wall == 19)
					{
						num2 = 140;
					}
					if (Main.tile[i, j].wall == 20)
					{
						num2 = 330;
					}
					if (num2 > 0)
					{
						Item.NewItem(i * 16, j * 16, 16, 16, num2, 1, false);
					}
					Main.tile[i, j].wall = 0;
					WorldGen.SquareWallFrame(i, j, true);
				}
			}
		}
		public static void KillTile(int i, int j, bool fail = false, bool effectOnly = false, bool noItem = false)
		{
			if (i >= 0 && j >= 0 && i < Main.maxTilesX && j < Main.maxTilesY)
			{
				if (Main.tile[i, j] == null)
				{
					Main.tile[i, j] = new Tile();
				}
				if (Main.tile[i, j].active)
				{
					if (j >= 1 && Main.tile[i, j - 1] == null)
					{
						Main.tile[i, j - 1] = new Tile();
					}
					if (j >= 1 && Main.tile[i, j - 1].active && ((Main.tile[i, j - 1].type == 5 && Main.tile[i, j].type != 5) || (Main.tile[i, j - 1].type == 21 && Main.tile[i, j].type != 21) || (Main.tile[i, j - 1].type == 26 && Main.tile[i, j].type != 26) || (Main.tile[i, j - 1].type == 72 && Main.tile[i, j].type != 72) || (Main.tile[i, j - 1].type == 12 && Main.tile[i, j].type != 12)) && (Main.tile[i, j - 1].type != 5 || ((Main.tile[i, j - 1].frameX != 66 || Main.tile[i, j - 1].frameY < 0 || Main.tile[i, j - 1].frameY > 44) && (Main.tile[i, j - 1].frameX != 88 || Main.tile[i, j - 1].frameY < 66 || Main.tile[i, j - 1].frameY > 110) && Main.tile[i, j - 1].frameY < 198)))
					{
						return;
					}
					if (!effectOnly && !WorldGen.stopDrops)
					{
						if (Main.tile[i, j].type == 3)
						{
							Main.PlaySound(6, i * 16, j * 16, 1);
							if (Main.tile[i, j].frameX == 144)
							{
								Item.NewItem(i * 16, j * 16, 16, 16, 5, 1, false);
							}
						}
						else
						{
							if (Main.tile[i, j].type == 24)
							{
								Main.PlaySound(6, i * 16, j * 16, 1);
								if (Main.tile[i, j].frameX == 144)
								{
									Item.NewItem(i * 16, j * 16, 16, 16, 60, 1, false);
								}
							}
							else
							{
								if (Main.tileAlch[(int)Main.tile[i, j].type] || Main.tile[i, j].type == 32 || Main.tile[i, j].type == 51 || Main.tile[i, j].type == 52 || Main.tile[i, j].type == 61 || Main.tile[i, j].type == 62 || Main.tile[i, j].type == 69 || Main.tile[i, j].type == 71 || Main.tile[i, j].type == 73 || Main.tile[i, j].type == 74)
								{
									Main.PlaySound(6, i * 16, j * 16, 1);
								}
								else
								{
									if (Main.tile[i, j].type == 1 || Main.tile[i, j].type == 6 || Main.tile[i, j].type == 7 || Main.tile[i, j].type == 8 || Main.tile[i, j].type == 9 || Main.tile[i, j].type == 22 || Main.tile[i, j].type == 25 || Main.tile[i, j].type == 37 || Main.tile[i, j].type == 38 || Main.tile[i, j].type == 39 || Main.tile[i, j].type == 41 || Main.tile[i, j].type == 43 || Main.tile[i, j].type == 44 || Main.tile[i, j].type == 45 || Main.tile[i, j].type == 46 || Main.tile[i, j].type == 47 || Main.tile[i, j].type == 48 || Main.tile[i, j].type == 56 || Main.tile[i, j].type == 58 || Main.tile[i, j].type == 63 || Main.tile[i, j].type == 64 || Main.tile[i, j].type == 65 || Main.tile[i, j].type == 66 || Main.tile[i, j].type == 67 || Main.tile[i, j].type == 68 || Main.tile[i, j].type == 75 || Main.tile[i, j].type == 76)
									{
										Main.PlaySound(21, i * 16, j * 16, 1);
									}
									else
									{
										Main.PlaySound(0, i * 16, j * 16, 1);
									}
								}
							}
						}
					}
					int num = 10;
					if (fail)
					{
						num = 3;
					}
					for (int k = 0; k < num; k++)
					{
						int num2 = 0;
						if (Main.tile[i, j].type == 0)
						{
							num2 = 0;
						}
						if (Main.tile[i, j].type == 1 || Main.tile[i, j].type == 16 || Main.tile[i, j].type == 17 || Main.tile[i, j].type == 38 || Main.tile[i, j].type == 39 || Main.tile[i, j].type == 41 || Main.tile[i, j].type == 43 || Main.tile[i, j].type == 44 || Main.tile[i, j].type == 48 || Main.tileStone[(int)Main.tile[i, j].type] || Main.tile[i, j].type == 85 || Main.tile[i, j].type == 90 || Main.tile[i, j].type == 92 || Main.tile[i, j].type == 96 || Main.tile[i, j].type == 97 || Main.tile[i, j].type == 99 || Main.tile[i, j].type == 105)
						{
							num2 = 1;
						}
						if (Main.tile[i, j].type == 4 || Main.tile[i, j].type == 33 || Main.tile[i, j].type == 95 || Main.tile[i, j].type == 98 || Main.tile[i, j].type == 100)
						{
							num2 = 6;
						}
						if (Main.tile[i, j].type == 5 || Main.tile[i, j].type == 10 || Main.tile[i, j].type == 11 || Main.tile[i, j].type == 14 || Main.tile[i, j].type == 15 || Main.tile[i, j].type == 19 || Main.tile[i, j].type == 30 || Main.tile[i, j].type == 86 || Main.tile[i, j].type == 87 || Main.tile[i, j].type == 88 || Main.tile[i, j].type == 89 || Main.tile[i, j].type == 93 || Main.tile[i, j].type == 94 || Main.tile[i, j].type == 104 || Main.tile[i, j].type == 106)
						{
							num2 = 7;
						}
						if (Main.tile[i, j].type == 21)
						{
							if (Main.tile[i, j].frameX >= 108)
							{
								num2 = 37;
							}
							else
							{
								if (Main.tile[i, j].frameX >= 36)
								{
									num2 = 10;
								}
								else
								{
									num2 = 7;
								}
							}
						}
						if (Main.tile[i, j].type == 2)
						{
							if (WorldGen.genRand.Next(2) == 0)
							{
								num2 = 0;
							}
							else
							{
								num2 = 2;
							}
						}
						if (Main.tile[i, j].type == 91)
						{
							num2 = -1;
						}
						if (Main.tile[i, j].type == 6 || Main.tile[i, j].type == 26)
						{
							num2 = 8;
						}
						if (Main.tile[i, j].type == 7 || Main.tile[i, j].type == 34 || Main.tile[i, j].type == 47)
						{
							num2 = 9;
						}
						if (Main.tile[i, j].type == 8 || Main.tile[i, j].type == 36 || Main.tile[i, j].type == 45 || Main.tile[i, j].type == 102)
						{
							num2 = 10;
						}
						if (Main.tile[i, j].type == 9 || Main.tile[i, j].type == 35 || Main.tile[i, j].type == 42 || Main.tile[i, j].type == 46)
						{
							num2 = 11;
						}
						if (Main.tile[i, j].type == 12)
						{
							num2 = 12;
						}
						if (Main.tile[i, j].type == 3 || Main.tile[i, j].type == 73)
						{
							num2 = 3;
						}
						if (Main.tile[i, j].type == 13 || Main.tile[i, j].type == 54)
						{
							num2 = 13;
						}
						if (Main.tile[i, j].type == 22)
						{
							num2 = 14;
						}
						if (Main.tile[i, j].type == 28 || Main.tile[i, j].type == 78)
						{
							num2 = 22;
						}
						if (Main.tile[i, j].type == 29)
						{
							num2 = 23;
						}
						if (Main.tile[i, j].type == 40 || Main.tile[i, j].type == 103)
						{
							num2 = 28;
						}
						if (Main.tile[i, j].type == 49)
						{
							num2 = 29;
						}
						if (Main.tile[i, j].type == 50)
						{
							num2 = 22;
						}
						if (Main.tile[i, j].type == 51)
						{
							num2 = 30;
						}
						if (Main.tile[i, j].type == 52)
						{
							num2 = 3;
						}
						if (Main.tile[i, j].type == 53 || Main.tile[i, j].type == 81)
						{
							num2 = 32;
						}
						if (Main.tile[i, j].type == 56 || Main.tile[i, j].type == 75)
						{
							num2 = 37;
						}
						if (Main.tile[i, j].type == 57)
						{
							num2 = 36;
						}
						if (Main.tile[i, j].type == 59)
						{
							num2 = 38;
						}
						if (Main.tile[i, j].type == 61 || Main.tile[i, j].type == 62 || Main.tile[i, j].type == 74 || Main.tile[i, j].type == 80)
						{
							num2 = 40;
						}
						if (Main.tile[i, j].type == 69)
						{
							num2 = 7;
						}
						if (Main.tile[i, j].type == 71 || Main.tile[i, j].type == 72)
						{
							num2 = 26;
						}
						if (Main.tile[i, j].type == 70)
						{
							num2 = 17;
						}
						if (Main.tileAlch[(int)Main.tile[i, j].type])
						{
							int num3 = (int)(Main.tile[i, j].frameX / 18);
							if (num3 == 0)
							{
								num2 = 3;
							}
							if (num3 == 1)
							{
								num2 = 3;
							}
							if (num3 == 2)
							{
								num2 = 7;
							}
							if (num3 == 3)
							{
								num2 = 17;
							}
							if (num3 == 4)
							{
								num2 = 3;
							}
							if (num3 == 5)
							{
								num2 = 6;
							}
						}
						if (Main.tile[i, j].type == 61)
						{
							if (WorldGen.genRand.Next(2) == 0)
							{
								num2 = 38;
							}
							else
							{
								num2 = 39;
							}
						}
						if (Main.tile[i, j].type == 58 || Main.tile[i, j].type == 76 || Main.tile[i, j].type == 77)
						{
							if (WorldGen.genRand.Next(2) == 0)
							{
								num2 = 6;
							}
							else
							{
								num2 = 25;
							}
						}
						if (Main.tile[i, j].type == 37)
						{
							if (WorldGen.genRand.Next(2) == 0)
							{
								num2 = 6;
							}
							else
							{
								num2 = 23;
							}
						}
						if (Main.tile[i, j].type == 32)
						{
							if (WorldGen.genRand.Next(2) == 0)
							{
								num2 = 14;
							}
							else
							{
								num2 = 24;
							}
						}
						if (Main.tile[i, j].type == 23 || Main.tile[i, j].type == 24)
						{
							if (WorldGen.genRand.Next(2) == 0)
							{
								num2 = 14;
							}
							else
							{
								num2 = 17;
							}
						}
						if (Main.tile[i, j].type == 25 || Main.tile[i, j].type == 31)
						{
							if (WorldGen.genRand.Next(2) == 0)
							{
								num2 = 14;
							}
							else
							{
								num2 = 1;
							}
						}
						if (Main.tile[i, j].type == 20)
						{
							if (WorldGen.genRand.Next(2) == 0)
							{
								num2 = 7;
							}
							else
							{
								num2 = 2;
							}
						}
						if (Main.tile[i, j].type == 27)
						{
							if (WorldGen.genRand.Next(2) == 0)
							{
								num2 = 3;
							}
							else
							{
								num2 = 19;
							}
						}
						if ((Main.tile[i, j].type == 34 || Main.tile[i, j].type == 35 || Main.tile[i, j].type == 36 || Main.tile[i, j].type == 42) && Main.rand.Next(2) == 0)
						{
							num2 = 6;
						}
						if (num2 >= 0)
						{
							Dust.NewDust(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16, num2, 0f, 0f, 0, default(Color), 1f);
						}
					}
					if (effectOnly)
					{
						return;
					}
					if (fail)
					{
						if (Main.tile[i, j].type == 2 || Main.tile[i, j].type == 23)
						{
							Main.tile[i, j].type = 0;
						}
						if (Main.tile[i, j].type == 60 || Main.tile[i, j].type == 70)
						{
							Main.tile[i, j].type = 59;
						}
						WorldGen.SquareTileFrame(i, j, true);
						return;
					}
					if (Main.tile[i, j].type == 21 && Main.netMode != 1)
					{
						int l = (int)(Main.tile[i, j].frameX / 18);
						int y = j - (int)(Main.tile[i, j].frameY / 18);
						while (l > 1)
						{
							l -= 2;
						}
						l = i - l;
						if (!Chest.DestroyChest(l, y))
						{
							return;
						}
					}
					if (!noItem && !WorldGen.stopDrops && Main.netMode != 1)
					{
						int num4 = 0;
						if (Main.tile[i, j].type == 0 || Main.tile[i, j].type == 2)
						{
							num4 = 2;
						}
						else
						{
							if (Main.tile[i, j].type == 1)
							{
								num4 = 3;
							}
							else
							{
								if (Main.tile[i, j].type == 3 || Main.tile[i, j].type == 73)
								{
									if (Main.rand.Next(2) == 0 && Main.player[(int)Player.FindClosest(new Vector2((float)(i * 16), (float)(j * 16)), 16, 16)].HasItem(281))
									{
										num4 = 283;
									}
								}
								else
								{
									if (Main.tile[i, j].type == 4)
									{
										num4 = 8;
									}
									else
									{
										if (Main.tile[i, j].type == 5)
										{
											if (Main.tile[i, j].frameX >= 22 && Main.tile[i, j].frameY >= 198)
											{
												if (Main.netMode != 1)
												{
													if (WorldGen.genRand.Next(2) == 0)
													{
														int num5 = j;
														while (Main.tile[i, num5] != null && (!Main.tile[i, num5].active || !Main.tileSolid[(int)Main.tile[i, num5].type] || Main.tileSolidTop[(int)Main.tile[i, num5].type]))
														{
															num5++;
														}
														if (Main.tile[i, num5] != null)
														{
															if (Main.tile[i, num5].type == 2)
															{
																num4 = 27;
															}
															else
															{
																num4 = 9;
															}
														}
													}
													else
													{
														num4 = 9;
													}
												}
											}
											else
											{
												num4 = 9;
											}
										}
										else
										{
											if (Main.tile[i, j].type == 6)
											{
												num4 = 11;
											}
											else
											{
												if (Main.tile[i, j].type == 7)
												{
													num4 = 12;
												}
												else
												{
													if (Main.tile[i, j].type == 8)
													{
														num4 = 13;
													}
													else
													{
														if (Main.tile[i, j].type == 9)
														{
															num4 = 14;
														}
														else
														{
															if (Main.tile[i, j].type == 13)
															{
																Main.PlaySound(13, i * 16, j * 16, 1);
																if (Main.tile[i, j].frameX == 18)
																{
																	num4 = 28;
																}
																else
																{
																	if (Main.tile[i, j].frameX == 36)
																	{
																		num4 = 110;
																	}
																	else
																	{
																		if (Main.tile[i, j].frameX == 54)
																		{
																			num4 = 350;
																		}
																		else
																		{
																			if (Main.tile[i, j].frameX == 72)
																			{
																				num4 = 351;
																			}
																			else
																			{
																				num4 = 31;
																			}
																		}
																	}
																}
															}
															else
															{
																if (Main.tile[i, j].type == 19)
																{
																	num4 = 94;
																}
																else
																{
																	if (Main.tile[i, j].type == 22)
																	{
																		num4 = 56;
																	}
																	else
																	{
																		if (Main.tile[i, j].type == 23)
																		{
																			num4 = 2;
																		}
																		else
																		{
																			if (Main.tile[i, j].type == 25)
																			{
																				num4 = 61;
																			}
																			else
																			{
																				if (Main.tile[i, j].type == 30)
																				{
																					num4 = 9;
																				}
																				else
																				{
																					if (Main.tile[i, j].type == 33)
																					{
																						num4 = 105;
																					}
																					else
																					{
																						if (Main.tile[i, j].type == 37)
																						{
																							num4 = 116;
																						}
																						else
																						{
																							if (Main.tile[i, j].type == 38)
																							{
																								num4 = 129;
																							}
																							else
																							{
																								if (Main.tile[i, j].type == 39)
																								{
																									num4 = 131;
																								}
																								else
																								{
																									if (Main.tile[i, j].type == 40)
																									{
																										num4 = 133;
																									}
																									else
																									{
																										if (Main.tile[i, j].type == 41)
																										{
																											num4 = 134;
																										}
																										else
																										{
																											if (Main.tile[i, j].type == 43)
																											{
																												num4 = 137;
																											}
																											else
																											{
																												if (Main.tile[i, j].type == 44)
																												{
																													num4 = 139;
																												}
																												else
																												{
																													if (Main.tile[i, j].type == 45)
																													{
																														num4 = 141;
																													}
																													else
																													{
																														if (Main.tile[i, j].type == 46)
																														{
																															num4 = 143;
																														}
																														else
																														{
																															if (Main.tile[i, j].type == 47)
																															{
																																num4 = 145;
																															}
																															else
																															{
																																if (Main.tile[i, j].type == 48)
																																{
																																	num4 = 147;
																																}
																																else
																																{
																																	if (Main.tile[i, j].type == 49)
																																	{
																																		num4 = 148;
																																	}
																																	else
																																	{
																																		if (Main.tile[i, j].type == 51)
																																		{
																																			num4 = 150;
																																		}
																																		else
																																		{
																																			if (Main.tile[i, j].type == 53)
																																			{
																																				num4 = 169;
																																			}
																																			else
																																			{
																																				if (Main.tile[i, j].type == 54)
																																				{
																																					Main.PlaySound(13, i * 16, j * 16, 1);
																																				}
																																				else
																																				{
																																					if (Main.tile[i, j].type == 56)
																																					{
																																						num4 = 173;
																																					}
																																					else
																																					{
																																						if (Main.tile[i, j].type == 57)
																																						{
																																							num4 = 172;
																																						}
																																						else
																																						{
																																							if (Main.tile[i, j].type == 58)
																																							{
																																								num4 = 174;
																																							}
																																							else
																																							{
																																								if (Main.tile[i, j].type == 60)
																																								{
																																									num4 = 176;
																																								}
																																								else
																																								{
																																									if (Main.tile[i, j].type == 70)
																																									{
																																										num4 = 176;
																																									}
																																									else
																																									{
																																										if (Main.tile[i, j].type == 75)
																																										{
																																											num4 = 192;
																																										}
																																										else
																																										{
																																											if (Main.tile[i, j].type == 76)
																																											{
																																												num4 = 214;
																																											}
																																											else
																																											{
																																												if (Main.tile[i, j].type == 78)
																																												{
																																													num4 = 222;
																																												}
																																												else
																																												{
																																													if (Main.tile[i, j].type == 81)
																																													{
																																														num4 = 275;
																																													}
																																													else
																																													{
																																														if (Main.tile[i, j].type == 80)
																																														{
																																															num4 = 276;
																																														}
																																														else
																																														{
																																															if (Main.tile[i, j].type == 61 || Main.tile[i, j].type == 74)
																																															{
																																																if (Main.tile[i, j].frameX == 144)
																																																{
																																																	Item.NewItem(i * 16, j * 16, 16, 16, 331, WorldGen.genRand.Next(1, 3), false);
																																																}
																																																else
																																																{
																																																	if (Main.tile[i, j].frameX == 162)
																																																	{
																																																		num4 = 223;
																																																	}
																																																	else
																																																	{
																																																		if (Main.tile[i, j].frameX >= 108 && Main.tile[i, j].frameX <= 126 && WorldGen.genRand.Next(100) == 0)
																																																		{
																																																			num4 = 208;
																																																		}
																																																		else
																																																		{
																																																			if (WorldGen.genRand.Next(100) == 0)
																																																			{
																																																				num4 = 195;
																																																			}
																																																		}
																																																	}
																																																}
																																															}
																																															else
																																															{
																																																if (Main.tile[i, j].type == 59 || Main.tile[i, j].type == 60)
																																																{
																																																	num4 = 176;
																																																}
																																																else
																																																{
																																																	if (Main.tile[i, j].type == 71 || Main.tile[i, j].type == 72)
																																																	{
																																																		if (WorldGen.genRand.Next(50) == 0)
																																																		{
																																																			num4 = 194;
																																																		}
																																																		else
																																																		{
																																																			if (WorldGen.genRand.Next(2) == 0)
																																																			{
																																																				num4 = 183;
																																																			}
																																																		}
																																																	}
																																																	else
																																																	{
																																																		if (Main.tile[i, j].type >= 63 && Main.tile[i, j].type <= 68)
																																																		{
																																																			num4 = (int)(Main.tile[i, j].type - 63 + 177);
																																																		}
																																																		else
																																																		{
																																																			if (Main.tile[i, j].type == 50)
																																																			{
																																																				if (Main.tile[i, j].frameX == 90)
																																																				{
																																																					num4 = 165;
																																																				}
																																																				else
																																																				{
																																																					num4 = 149;
																																																				}
																																																			}
																																																			else
																																																			{
																																																				if (Main.tileAlch[(int)Main.tile[i, j].type] && Main.tile[i, j].type > 82)
																																																				{
																																																					int num6 = (int)(Main.tile[i, j].frameX / 18);
																																																					bool flag = false;
																																																					if (Main.tile[i, j].type == 84)
																																																					{
																																																						flag = true;
																																																					}
																																																					if (num6 == 0 && Main.dayTime)
																																																					{
																																																						flag = true;
																																																					}
																																																					if (num6 == 1 && !Main.dayTime)
																																																					{
																																																						flag = true;
																																																					}
																																																					if (num6 == 3 && Main.bloodMoon)
																																																					{
																																																						flag = true;
																																																					}
																																																					num4 = 313 + num6;
																																																					if (flag)
																																																					{
																																																						Item.NewItem(i * 16, j * 16, 16, 16, 307 + num6, WorldGen.genRand.Next(1, 4), false);
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
						if (num4 > 0)
						{
							Item.NewItem(i * 16, j * 16, 16, 16, num4, 1, false);
						}
					}
					Main.tile[i, j].active = false;
					if (Main.tileSolid[(int)Main.tile[i, j].type])
					{
						Main.tile[i, j].lighted = false;
					}
					Main.tile[i, j].frameX = -1;
					Main.tile[i, j].frameY = -1;
					Main.tile[i, j].frameNumber = 0;
					if (Main.tile[i, j].type == 58 && j > Main.maxTilesY - 200)
					{
						Main.tile[i, j].lava = true;
						Main.tile[i, j].liquid = 128;
					}
					Main.tile[i, j].type = 0;
					WorldGen.SquareTileFrame(i, j, true);
				}
			}
		}
		public static bool PlayerLOS(int x, int y)
		{
			Rectangle rectangle = new Rectangle(x * 16, y * 16, 16, 16);
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					Rectangle value = new Rectangle((int)((double)Main.player[i].position.X + (double)Main.player[i].width * 0.5 - (double)NPC.sWidth * 0.6), (int)((double)Main.player[i].position.Y + (double)Main.player[i].height * 0.5 - (double)NPC.sHeight * 0.6), (int)((double)NPC.sWidth * 1.2), (int)((double)NPC.sHeight * 1.2));
					if (rectangle.Intersects(value))
					{
						return true;
					}
				}
			}
			return false;
		}
		public static void UpdateWorld()
		{
			Liquid.skipCount++;
			if (Liquid.skipCount > 1)
			{
				Liquid.UpdateLiquid();
				Liquid.skipCount = 0;
			}
			float num = 3E-05f;
			float num2 = 1.5E-05f;
			bool flag = false;
			WorldGen.spawnDelay++;
			if (Main.invasionType > 0)
			{
				WorldGen.spawnDelay = 0;
			}
			if (WorldGen.spawnDelay >= 20)
			{
				flag = true;
				WorldGen.spawnDelay = 0;
				if (WorldGen.spawnNPC != 37)
				{
					for (int i = 0; i < 1000; i++)
					{
						if (Main.npc[i].active && Main.npc[i].homeless && Main.npc[i].townNPC)
						{
							WorldGen.spawnNPC = Main.npc[i].type;
							break;
						}
					}
				}
			}
			int num3 = 0;
			while ((float)num3 < (float)(Main.maxTilesX * Main.maxTilesY) * num)
			{
				int num4 = WorldGen.genRand.Next(10, Main.maxTilesX - 10);
				int num5 = WorldGen.genRand.Next(10, (int)Main.worldSurface - 1);
				int num6 = num4 - 1;
				int num7 = num4 + 2;
				int num8 = num5 - 1;
				int num9 = num5 + 2;
				if (num6 < 10)
				{
					num6 = 10;
				}
				if (num7 > Main.maxTilesX - 10)
				{
					num7 = Main.maxTilesX - 10;
				}
				if (num8 < 10)
				{
					num8 = 10;
				}
				if (num9 > Main.maxTilesY - 10)
				{
					num9 = Main.maxTilesY - 10;
				}
				if (Main.tile[num4, num5] != null)
				{
					if (Main.tileAlch[(int)Main.tile[num4, num5].type])
					{
						WorldGen.GrowAlch(num4, num5);
					}
					if (Main.tile[num4, num5].liquid > 32)
					{
						if (Main.tile[num4, num5].active && (Main.tile[num4, num5].type == 3 || Main.tile[num4, num5].type == 20 || Main.tile[num4, num5].type == 24 || Main.tile[num4, num5].type == 27 || Main.tile[num4, num5].type == 73))
						{
							WorldGen.KillTile(num4, num5, false, false, false);
							if (Main.netMode == 2)
							{
								NetMessage.SendData(17, -1, -1, "", 0, (float)num4, (float)num5, 0f, 0);
							}
						}
					}
					else
					{
						if (Main.tile[num4, num5].active)
						{
							if (Main.tile[num4, num5].type == 80)
							{
								if (WorldGen.genRand.Next(15) == 0)
								{
									WorldGen.GrowCactus(num4, num5);
								}
							}
							else
							{
								if (Main.tile[num4, num5].type == 53)
								{
									if (!Main.tile[num4, num8].active)
									{
										if (num4 < 250 || num4 > Main.maxTilesX - 250)
										{
											if (WorldGen.genRand.Next(500) == 0 && Main.tile[num4, num8].liquid == 255 && Main.tile[num4, num8 - 1].liquid == 255 && Main.tile[num4, num8 - 2].liquid == 255 && Main.tile[num4, num8 - 3].liquid == 255 && Main.tile[num4, num8 - 4].liquid == 255)
											{
												WorldGen.PlaceTile(num4, num8, 81, true, false, -1, 0);
												if (Main.netMode == 2 && Main.tile[num4, num8].active)
												{
													NetMessage.SendTileSquare(-1, num4, num8, 1);
												}
											}
										}
										else
										{
											if (num4 > 400 && num4 < Main.maxTilesX - 400 && WorldGen.genRand.Next(300) == 0)
											{
												WorldGen.GrowCactus(num4, num5);
											}
										}
									}
								}
								else
								{
									if (Main.tile[num4, num5].type == 78)
									{
										if (!Main.tile[num4, num8].active)
										{
											WorldGen.PlaceTile(num4, num8, 3, true, false, -1, 0);
											if (Main.netMode == 2 && Main.tile[num4, num8].active)
											{
												NetMessage.SendTileSquare(-1, num4, num8, 1);
											}
										}
									}
									else
									{
										if (Main.tile[num4, num5].type == 2 || Main.tile[num4, num5].type == 23 || Main.tile[num4, num5].type == 32)
										{
											int num10 = (int)Main.tile[num4, num5].type;
											if (!Main.tile[num4, num8].active && WorldGen.genRand.Next(12) == 0 && num10 == 2)
											{
												WorldGen.PlaceTile(num4, num8, 3, true, false, -1, 0);
												if (Main.netMode == 2 && Main.tile[num4, num8].active)
												{
													NetMessage.SendTileSquare(-1, num4, num8, 1);
												}
											}
											if (!Main.tile[num4, num8].active && WorldGen.genRand.Next(10) == 0 && num10 == 23)
											{
												WorldGen.PlaceTile(num4, num8, 24, true, false, -1, 0);
												if (Main.netMode == 2 && Main.tile[num4, num8].active)
												{
													NetMessage.SendTileSquare(-1, num4, num8, 1);
												}
											}
											bool flag2 = false;
											for (int j = num6; j < num7; j++)
											{
												for (int k = num8; k < num9; k++)
												{
													if ((num4 != j || num5 != k) && Main.tile[j, k].active)
													{
														if (num10 == 32)
														{
															num10 = 23;
														}
														if (Main.tile[j, k].type == 0 || (num10 == 23 && Main.tile[j, k].type == 2))
														{
															WorldGen.SpreadGrass(j, k, 0, num10, false);
															if (num10 == 23)
															{
																WorldGen.SpreadGrass(j, k, 2, num10, false);
															}
															if ((int)Main.tile[j, k].type == num10)
															{
																WorldGen.SquareTileFrame(j, k, true);
																flag2 = true;
															}
														}
													}
												}
											}
											if (Main.netMode == 2 && flag2)
											{
												NetMessage.SendTileSquare(-1, num4, num5, 3);
											}
										}
										else
										{
											if (Main.tile[num4, num5].type == 20 && WorldGen.genRand.Next(20) == 0 && !WorldGen.PlayerLOS(num4, num5))
											{
												WorldGen.GrowTree(num4, num5);
											}
										}
									}
								}
							}
							if (Main.tile[num4, num5].type == 3 && WorldGen.genRand.Next(20) == 0 && Main.tile[num4, num5].frameX < 144)
							{
								Main.tile[num4, num5].type = 73;
								if (Main.netMode == 2)
								{
									NetMessage.SendTileSquare(-1, num4, num5, 3);
								}
							}
							if (Main.tile[num4, num5].type == 32 && WorldGen.genRand.Next(3) == 0)
							{
								int num11 = num4;
								int num12 = num5;
								int num13 = 0;
								if (Main.tile[num11 + 1, num12].active && Main.tile[num11 + 1, num12].type == 32)
								{
									num13++;
								}
								if (Main.tile[num11 - 1, num12].active && Main.tile[num11 - 1, num12].type == 32)
								{
									num13++;
								}
								if (Main.tile[num11, num12 + 1].active && Main.tile[num11, num12 + 1].type == 32)
								{
									num13++;
								}
								if (Main.tile[num11, num12 - 1].active && Main.tile[num11, num12 - 1].type == 32)
								{
									num13++;
								}
								if (num13 < 3 || Main.tile[num4, num5].type == 23)
								{
									int num14 = WorldGen.genRand.Next(4);
									if (num14 == 0)
									{
										num12--;
									}
									else
									{
										if (num14 == 1)
										{
											num12++;
										}
										else
										{
											if (num14 == 2)
											{
												num11--;
											}
											else
											{
												if (num14 == 3)
												{
													num11++;
												}
											}
										}
									}
									if (!Main.tile[num11, num12].active)
									{
										num13 = 0;
										if (Main.tile[num11 + 1, num12].active && Main.tile[num11 + 1, num12].type == 32)
										{
											num13++;
										}
										if (Main.tile[num11 - 1, num12].active && Main.tile[num11 - 1, num12].type == 32)
										{
											num13++;
										}
										if (Main.tile[num11, num12 + 1].active && Main.tile[num11, num12 + 1].type == 32)
										{
											num13++;
										}
										if (Main.tile[num11, num12 - 1].active && Main.tile[num11, num12 - 1].type == 32)
										{
											num13++;
										}
										if (num13 < 2)
										{
											int num15 = 7;
											int num16 = num11 - num15;
											int num17 = num11 + num15;
											int num18 = num12 - num15;
											int num19 = num12 + num15;
											bool flag3 = false;
											for (int l = num16; l < num17; l++)
											{
												for (int m = num18; m < num19; m++)
												{
													if (Math.Abs(l - num11) * 2 + Math.Abs(m - num12) < 9 && Main.tile[l, m].active && Main.tile[l, m].type == 23 && Main.tile[l, m - 1].active && Main.tile[l, m - 1].type == 32 && Main.tile[l, m - 1].liquid == 0)
													{
														flag3 = true;
														break;
													}
												}
											}
											if (flag3)
											{
												Main.tile[num11, num12].type = 32;
												Main.tile[num11, num12].active = true;
												WorldGen.SquareTileFrame(num11, num12, true);
												if (Main.netMode == 2)
												{
													NetMessage.SendTileSquare(-1, num11, num12, 3);
												}
											}
										}
									}
								}
							}
						}
						else
						{
							if (flag && WorldGen.spawnNPC > 0)
							{
								WorldGen.SpawnNPC(num4, num5);
							}
						}
					}
					if (Main.tile[num4, num5].active)
					{
						if ((Main.tile[num4, num5].type == 2 || Main.tile[num4, num5].type == 52) && WorldGen.genRand.Next(40) == 0 && !Main.tile[num4, num5 + 1].active && !Main.tile[num4, num5 + 1].lava)
						{
							bool flag4 = false;
							for (int n = num5; n > num5 - 10; n--)
							{
								if (Main.tile[num4, n].active && Main.tile[num4, n].type == 2)
								{
									flag4 = true;
									break;
								}
							}
							if (flag4)
							{
								int num20 = num4;
								int num21 = num5 + 1;
								Main.tile[num20, num21].type = 52;
								Main.tile[num20, num21].active = true;
								WorldGen.SquareTileFrame(num20, num21, true);
								if (Main.netMode == 2)
								{
									NetMessage.SendTileSquare(-1, num20, num21, 3);
								}
							}
						}
						if (Main.tile[num4, num5].type == 60)
						{
							int type = (int)Main.tile[num4, num5].type;
							if (!Main.tile[num4, num8].active && WorldGen.genRand.Next(7) == 0)
							{
								WorldGen.PlaceTile(num4, num8, 61, true, false, -1, 0);
								if (Main.netMode == 2 && Main.tile[num4, num8].active)
								{
									NetMessage.SendTileSquare(-1, num4, num8, 1);
								}
							}
							else
							{
								if (WorldGen.genRand.Next(500) == 0 && (!Main.tile[num4, num8].active || Main.tile[num4, num8].type == 61 || Main.tile[num4, num8].type == 74 || Main.tile[num4, num8].type == 69) && !WorldGen.PlayerLOS(num4, num5))
								{
									WorldGen.GrowTree(num4, num5);
								}
							}
							bool flag5 = false;
							for (int num22 = num6; num22 < num7; num22++)
							{
								for (int num23 = num8; num23 < num9; num23++)
								{
									if ((num4 != num22 || num5 != num23) && Main.tile[num22, num23].active && Main.tile[num22, num23].type == 59)
									{
										WorldGen.SpreadGrass(num22, num23, 59, type, false);
										if ((int)Main.tile[num22, num23].type == type)
										{
											WorldGen.SquareTileFrame(num22, num23, true);
											flag5 = true;
										}
									}
								}
							}
							if (Main.netMode == 2 && flag5)
							{
								NetMessage.SendTileSquare(-1, num4, num5, 3);
							}
						}
						if (Main.tile[num4, num5].type == 61 && WorldGen.genRand.Next(3) == 0 && Main.tile[num4, num5].frameX < 144)
						{
							Main.tile[num4, num5].type = 74;
							if (Main.netMode == 2)
							{
								NetMessage.SendTileSquare(-1, num4, num5, 3);
							}
						}
						if ((Main.tile[num4, num5].type == 60 || Main.tile[num4, num5].type == 62) && WorldGen.genRand.Next(15) == 0 && !Main.tile[num4, num5 + 1].active && !Main.tile[num4, num5 + 1].lava)
						{
							bool flag6 = false;
							for (int num24 = num5; num24 > num5 - 10; num24--)
							{
								if (Main.tile[num4, num24].active && Main.tile[num4, num24].type == 60)
								{
									flag6 = true;
									break;
								}
							}
							if (flag6)
							{
								int num25 = num4;
								int num26 = num5 + 1;
								Main.tile[num25, num26].type = 62;
								Main.tile[num25, num26].active = true;
								WorldGen.SquareTileFrame(num25, num26, true);
								if (Main.netMode == 2)
								{
									NetMessage.SendTileSquare(-1, num25, num26, 3);
								}
							}
						}
					}
				}
				num3++;
			}
			int num27 = 0;
			while ((float)num27 < (float)(Main.maxTilesX * Main.maxTilesY) * num2)
			{
				int num28 = WorldGen.genRand.Next(10, Main.maxTilesX - 10);
				int num29 = WorldGen.genRand.Next((int)Main.worldSurface + 2, Main.maxTilesY - 20);
				int num30 = num28 - 1;
				int num31 = num28 + 2;
				int num32 = num29 - 1;
				int num33 = num29 + 2;
				if (num30 < 10)
				{
					num30 = 10;
				}
				if (num31 > Main.maxTilesX - 10)
				{
					num31 = Main.maxTilesX - 10;
				}
				if (num32 < 10)
				{
					num32 = 10;
				}
				if (num33 > Main.maxTilesY - 10)
				{
					num33 = Main.maxTilesY - 10;
				}
				if (Main.tile[num28, num29] != null)
				{
					if (Main.tileAlch[(int)Main.tile[num28, num29].type])
					{
						WorldGen.GrowAlch(num28, num29);
					}
					if (Main.tile[num28, num29].liquid <= 32)
					{
						if (Main.tile[num28, num29].active)
						{
							if (Main.tile[num28, num29].type == 60)
							{
								int type2 = (int)Main.tile[num28, num29].type;
								if (!Main.tile[num28, num32].active && WorldGen.genRand.Next(10) == 0)
								{
									WorldGen.PlaceTile(num28, num32, 61, true, false, -1, 0);
									if (Main.netMode == 2 && Main.tile[num28, num32].active)
									{
										NetMessage.SendTileSquare(-1, num28, num32, 1);
									}
								}
								bool flag7 = false;
								for (int num34 = num30; num34 < num31; num34++)
								{
									for (int num35 = num32; num35 < num33; num35++)
									{
										if ((num28 != num34 || num29 != num35) && Main.tile[num34, num35].active && Main.tile[num34, num35].type == 59)
										{
											WorldGen.SpreadGrass(num34, num35, 59, type2, false);
											if ((int)Main.tile[num34, num35].type == type2)
											{
												WorldGen.SquareTileFrame(num34, num35, true);
												flag7 = true;
											}
										}
									}
								}
								if (Main.netMode == 2 && flag7)
								{
									NetMessage.SendTileSquare(-1, num28, num29, 3);
								}
							}
							if (Main.tile[num28, num29].type == 61 && WorldGen.genRand.Next(3) == 0 && Main.tile[num28, num29].frameX < 144)
							{
								Main.tile[num28, num29].type = 74;
								if (Main.netMode == 2)
								{
									NetMessage.SendTileSquare(-1, num28, num29, 3);
								}
							}
							if ((Main.tile[num28, num29].type == 60 || Main.tile[num28, num29].type == 62) && WorldGen.genRand.Next(5) == 0 && !Main.tile[num28, num29 + 1].active && !Main.tile[num28, num29 + 1].lava)
							{
								bool flag8 = false;
								for (int num36 = num29; num36 > num29 - 10; num36--)
								{
									if (Main.tile[num28, num36].active && Main.tile[num28, num36].type == 60)
									{
										flag8 = true;
										break;
									}
								}
								if (flag8)
								{
									int num37 = num28;
									int num38 = num29 + 1;
									Main.tile[num37, num38].type = 62;
									Main.tile[num37, num38].active = true;
									WorldGen.SquareTileFrame(num37, num38, true);
									if (Main.netMode == 2)
									{
										NetMessage.SendTileSquare(-1, num37, num38, 3);
									}
								}
							}
							if (Main.tile[num28, num29].type == 69 && WorldGen.genRand.Next(3) == 0)
							{
								int num39 = num28;
								int num40 = num29;
								int num41 = 0;
								if (Main.tile[num39 + 1, num40].active && Main.tile[num39 + 1, num40].type == 69)
								{
									num41++;
								}
								if (Main.tile[num39 - 1, num40].active && Main.tile[num39 - 1, num40].type == 69)
								{
									num41++;
								}
								if (Main.tile[num39, num40 + 1].active && Main.tile[num39, num40 + 1].type == 69)
								{
									num41++;
								}
								if (Main.tile[num39, num40 - 1].active && Main.tile[num39, num40 - 1].type == 69)
								{
									num41++;
								}
								if (num41 < 3 || Main.tile[num28, num29].type == 60)
								{
									int num42 = WorldGen.genRand.Next(4);
									if (num42 == 0)
									{
										num40--;
									}
									else
									{
										if (num42 == 1)
										{
											num40++;
										}
										else
										{
											if (num42 == 2)
											{
												num39--;
											}
											else
											{
												if (num42 == 3)
												{
													num39++;
												}
											}
										}
									}
									if (!Main.tile[num39, num40].active)
									{
										num41 = 0;
										if (Main.tile[num39 + 1, num40].active && Main.tile[num39 + 1, num40].type == 69)
										{
											num41++;
										}
										if (Main.tile[num39 - 1, num40].active && Main.tile[num39 - 1, num40].type == 69)
										{
											num41++;
										}
										if (Main.tile[num39, num40 + 1].active && Main.tile[num39, num40 + 1].type == 69)
										{
											num41++;
										}
										if (Main.tile[num39, num40 - 1].active && Main.tile[num39, num40 - 1].type == 69)
										{
											num41++;
										}
										if (num41 < 2)
										{
											int num43 = 7;
											int num44 = num39 - num43;
											int num45 = num39 + num43;
											int num46 = num40 - num43;
											int num47 = num40 + num43;
											bool flag9 = false;
											for (int num48 = num44; num48 < num45; num48++)
											{
												for (int num49 = num46; num49 < num47; num49++)
												{
													if (Math.Abs(num48 - num39) * 2 + Math.Abs(num49 - num40) < 9 && Main.tile[num48, num49].active && Main.tile[num48, num49].type == 60 && Main.tile[num48, num49 - 1].active && Main.tile[num48, num49 - 1].type == 69 && Main.tile[num48, num49 - 1].liquid == 0)
													{
														flag9 = true;
														break;
													}
												}
											}
											if (flag9)
											{
												Main.tile[num39, num40].type = 69;
												Main.tile[num39, num40].active = true;
												WorldGen.SquareTileFrame(num39, num40, true);
												if (Main.netMode == 2)
												{
													NetMessage.SendTileSquare(-1, num39, num40, 3);
												}
											}
										}
									}
								}
							}
							if (Main.tile[num28, num29].type == 70)
							{
								int type3 = (int)Main.tile[num28, num29].type;
								if (!Main.tile[num28, num32].active && WorldGen.genRand.Next(10) == 0)
								{
									WorldGen.PlaceTile(num28, num32, 71, true, false, -1, 0);
									if (Main.netMode == 2 && Main.tile[num28, num32].active)
									{
										NetMessage.SendTileSquare(-1, num28, num32, 1);
									}
								}
								if (WorldGen.genRand.Next(200) == 0 && !WorldGen.PlayerLOS(num28, num29))
								{
									WorldGen.GrowShroom(num28, num29);
								}
								bool flag10 = false;
								for (int num50 = num30; num50 < num31; num50++)
								{
									for (int num51 = num32; num51 < num33; num51++)
									{
										if ((num28 != num50 || num29 != num51) && Main.tile[num50, num51].active && Main.tile[num50, num51].type == 59)
										{
											WorldGen.SpreadGrass(num50, num51, 59, type3, false);
											if ((int)Main.tile[num50, num51].type == type3)
											{
												WorldGen.SquareTileFrame(num50, num51, true);
												flag10 = true;
											}
										}
									}
								}
								if (Main.netMode == 2 && flag10)
								{
									NetMessage.SendTileSquare(-1, num28, num29, 3);
								}
							}
						}
						else
						{
							if (flag && WorldGen.spawnNPC > 0)
							{
								WorldGen.SpawnNPC(num28, num29);
							}
						}
					}
				}
				num27++;
			}
			if (Main.rand.Next(100) == 0)
			{
				WorldGen.PlantAlch();
			}
			if (!Main.dayTime)
			{
				float num52 = (float)(Main.maxTilesX / 4200);
				if ((float)Main.rand.Next(8000) < 10f * num52)
				{
					int num53 = 12;
					int num54 = Main.rand.Next(Main.maxTilesX - 50) + 100;
					num54 *= 16;
					int num55 = Main.rand.Next((int)((double)Main.maxTilesY * 0.05));
					num55 *= 16;
					Vector2 vector = new Vector2((float)num54, (float)num55);
					float num56 = (float)Main.rand.Next(-100, 101);
					float num57 = (float)(Main.rand.Next(200) + 100);
					float num58 = (float)Math.Sqrt((double)(num56 * num56 + num57 * num57));
					num58 = (float)num53 / num58;
					num56 *= num58;
					num57 *= num58;
					Projectile.NewProjectile(vector.X, vector.Y, num56, num57, 12, 1000, 10f, Main.myPlayer);
				}
			}
		}
		public static void PlaceWall(int i, int j, int type, bool mute = false)
		{
			if (i <= 1 || j <= 1 || i >= Main.maxTilesX - 2 || j >= Main.maxTilesY - 2)
			{
				return;
			}
			if (Main.tile[i, j] == null)
			{
				Main.tile[i, j] = new Tile();
			}
			if ((int)Main.tile[i, j].wall != type)
			{
				for (int k = i - 1; k < i + 2; k++)
				{
					for (int l = j - 1; l < j + 2; l++)
					{
						if (Main.tile[k, l] == null)
						{
							Main.tile[k, l] = new Tile();
						}
						if (Main.tile[k, l].wall > 0 && (int)Main.tile[k, l].wall != type)
						{
							bool flag = false;
							if (Main.tile[i, j].wall == 0 && (type == 2 || type == 16) && (Main.tile[k, l].wall == 2 || Main.tile[k, l].wall == 16))
							{
								flag = true;
							}
							if (!flag)
							{
								return;
							}
						}
					}
				}
				Main.tile[i, j].wall = (byte)type;
				WorldGen.SquareWallFrame(i, j, true);
				if (!mute)
				{
					Main.PlaySound(0, i * 16, j * 16, 1);
				}
			}
		}
		public static void AddPlants()
		{
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				for (int j = 1; j < Main.maxTilesY; j++)
				{
					if (Main.tile[i, j].type == 2 && Main.tile[i, j].active)
					{
						if (!Main.tile[i, j - 1].active)
						{
							WorldGen.PlaceTile(i, j - 1, 3, true, false, -1, 0);
						}
					}
					else
					{
						if (Main.tile[i, j].type == 23 && Main.tile[i, j].active && !Main.tile[i, j - 1].active)
						{
							WorldGen.PlaceTile(i, j - 1, 24, true, false, -1, 0);
						}
					}
				}
			}
		}
		public static void SpreadGrass(int i, int j, int dirt = 0, int grass = 2, bool repeat = true)
		{
			if ((int)Main.tile[i, j].type != dirt || !Main.tile[i, j].active || ((double)j < Main.worldSurface && grass == 70) || ((double)j >= Main.worldSurface && dirt == 0))
			{
				return;
			}
			int num = i - 1;
			int num2 = i + 2;
			int num3 = j - 1;
			int num4 = j + 2;
			if (num < 0)
			{
				num = 0;
			}
			if (num2 > Main.maxTilesX)
			{
				num2 = Main.maxTilesX;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num4 > Main.maxTilesY)
			{
				num4 = Main.maxTilesY;
			}
			bool flag = true;
			for (int k = num; k < num2; k++)
			{
				for (int l = num3; l < num4; l++)
				{
					if (!Main.tile[k, l].active || !Main.tileSolid[(int)Main.tile[k, l].type])
					{
						flag = false;
						break;
					}
				}
			}
			if (!flag)
			{
				if (grass == 23 && Main.tile[i, j - 1].type == 27)
				{
					return;
				}
				Main.tile[i, j].type = (byte)grass;
				for (int m = num; m < num2; m++)
				{
					for (int n = num3; n < num4; n++)
					{
						if (Main.tile[m, n].active && (int)Main.tile[m, n].type == dirt && repeat)
						{
							WorldGen.SpreadGrass(m, n, dirt, grass, true);
						}
					}
				}
			}
		}
		public static void ChasmRunnerSideways(int i, int j, int direction, int steps)
		{
			float num = (float)steps;
			Vector2 value;
			value.X = (float)i;
			value.Y = (float)j;
			Vector2 value2;
			value2.X = (float)WorldGen.genRand.Next(10, 21) * 0.1f * (float)direction;
			value2.Y = (float)WorldGen.genRand.Next(-10, 10) * 0.01f;
			double num2 = (double)(WorldGen.genRand.Next(5) + 7);
			while (num2 > 0.0)
			{
				if (num > 0f)
				{
					num2 += (double)WorldGen.genRand.Next(3);
					num2 -= (double)WorldGen.genRand.Next(3);
					if (num2 < 7.0)
					{
						num2 = 7.0;
					}
					if (num2 > 20.0)
					{
						num2 = 20.0;
					}
					if (num == 1f && num2 < 10.0)
					{
						num2 = 10.0;
					}
				}
				else
				{
					num2 -= (double)WorldGen.genRand.Next(4);
				}
				if ((double)value.Y > Main.rockLayer && num > 0f)
				{
					num = 0f;
				}
				num -= 1f;
				int num3 = (int)((double)value.X - num2 * 0.5);
				int num4 = (int)((double)value.X + num2 * 0.5);
				int num5 = (int)((double)value.Y - num2 * 0.5);
				int num6 = (int)((double)value.Y + num2 * 0.5);
				if (num3 < 0)
				{
					num3 = 0;
				}
				if (num4 > Main.maxTilesX - 1)
				{
					num4 = Main.maxTilesX - 1;
				}
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesY)
				{
					num6 = Main.maxTilesY;
				}
				for (int k = num3; k < num4; k++)
				{
					for (int l = num5; l < num6; l++)
					{
						if ((double)(Math.Abs((float)k - value.X) + Math.Abs((float)l - value.Y)) < num2 * 0.5 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015) && Main.tile[k, l].type != 31 && Main.tile[k, l].type != 22)
						{
							Main.tile[k, l].active = false;
						}
					}
				}
				value += value2;
				value2.Y += (float)WorldGen.genRand.Next(-10, 10) * 0.1f;
				if (value.Y < (float)(j - 20))
				{
					value2.Y += (float)WorldGen.genRand.Next(20) * 0.01f;
				}
				if (value.Y > (float)(j + 20))
				{
					value2.Y -= (float)WorldGen.genRand.Next(20) * 0.01f;
				}
				if ((double)value2.Y < -0.5)
				{
					value2.Y = -0.5f;
				}
				if ((double)value2.Y > 0.5)
				{
					value2.Y = 0.5f;
				}
				value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.01f;
				if (direction == -1)
				{
					if ((double)value2.X > -0.5)
					{
						value2.X = -0.5f;
					}
					if (value2.X < -2f)
					{
						value2.X = -2f;
					}
				}
				else
				{
					if (direction == 1)
					{
						if ((double)value2.X < 0.5)
						{
							value2.X = 0.5f;
						}
						if (value2.X > 2f)
						{
							value2.X = 2f;
						}
					}
				}
				num3 = (int)((double)value.X - num2 * 1.1);
				num4 = (int)((double)value.X + num2 * 1.1);
				num5 = (int)((double)value.Y - num2 * 1.1);
				num6 = (int)((double)value.Y + num2 * 1.1);
				if (num3 < 1)
				{
					num3 = 1;
				}
				if (num4 > Main.maxTilesX - 1)
				{
					num4 = Main.maxTilesX - 1;
				}
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesY)
				{
					num6 = Main.maxTilesY;
				}
				for (int m = num3; m < num4; m++)
				{
					for (int n = num5; n < num6; n++)
					{
						if ((double)(Math.Abs((float)m - value.X) + Math.Abs((float)n - value.Y)) < num2 * 1.1 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015) && Main.tile[m, n].wall != 3)
						{
							if (Main.tile[m, n].type != 25 && n > j + WorldGen.genRand.Next(3, 20))
							{
								Main.tile[m, n].active = true;
							}
							Main.tile[m, n].active = true;
							if (Main.tile[m, n].type != 31 && Main.tile[m, n].type != 22)
							{
								Main.tile[m, n].type = 25;
							}
							if (Main.tile[m, n].wall == 2)
							{
								Main.tile[m, n].wall = 0;
							}
						}
					}
				}
				for (int num7 = num3; num7 < num4; num7++)
				{
					for (int num8 = num5; num8 < num6; num8++)
					{
						if ((double)(Math.Abs((float)num7 - value.X) + Math.Abs((float)num8 - value.Y)) < num2 * 1.1 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015) && Main.tile[num7, num8].wall != 3)
						{
							if (Main.tile[num7, num8].type != 31 && Main.tile[num7, num8].type != 22)
							{
								Main.tile[num7, num8].type = 25;
							}
							Main.tile[num7, num8].active = true;
							WorldGen.PlaceWall(num7, num8, 3, true);
						}
					}
				}
			}
			if (WorldGen.genRand.Next(3) == 0)
			{
				int num9 = (int)value.X;
				int num10 = (int)value.Y;
				while (!Main.tile[num9, num10].active)
				{
					num10++;
				}
				WorldGen.TileRunner(num9, num10, (double)WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(3, 7), 22, false, 0f, 0f, false, true);
			}
		}
		public static void ChasmRunner(int i, int j, int steps, bool makeOrb = false)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (!makeOrb)
			{
				flag2 = true;
			}
			float num = (float)steps;
			Vector2 value;
			value.X = (float)i;
			value.Y = (float)j;
			Vector2 value2;
			value2.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
			value2.Y = (float)WorldGen.genRand.Next(11) * 0.2f + 0.5f;
			int num2 = 5;
			double num3 = (double)(WorldGen.genRand.Next(5) + 7);
			while (num3 > 0.0)
			{
				if (num > 0f)
				{
					num3 += (double)WorldGen.genRand.Next(3);
					num3 -= (double)WorldGen.genRand.Next(3);
					if (num3 < 7.0)
					{
						num3 = 7.0;
					}
					if (num3 > 20.0)
					{
						num3 = 20.0;
					}
					if (num == 1f && num3 < 10.0)
					{
						num3 = 10.0;
					}
				}
				else
				{
					if ((double)value.Y > Main.worldSurface + 45.0)
					{
						num3 -= (double)WorldGen.genRand.Next(4);
					}
				}
				if ((double)value.Y > Main.rockLayer && num > 0f)
				{
					num = 0f;
				}
				num -= 1f;
				if (!flag && (double)value.Y > Main.worldSurface + 20.0)
				{
					flag = true;
					WorldGen.ChasmRunnerSideways((int)value.X, (int)value.Y, -1, WorldGen.genRand.Next(20, 40));
					WorldGen.ChasmRunnerSideways((int)value.X, (int)value.Y, 1, WorldGen.genRand.Next(20, 40));
				}
				int num4;
				int num5;
				int num6;
				int num7;
				if (num > (float)num2)
				{
					num4 = (int)((double)value.X - num3 * 0.5);
					num5 = (int)((double)value.X + num3 * 0.5);
					num6 = (int)((double)value.Y - num3 * 0.5);
					num7 = (int)((double)value.Y + num3 * 0.5);
					if (num4 < 0)
					{
						num4 = 0;
					}
					if (num5 > Main.maxTilesX - 1)
					{
						num5 = Main.maxTilesX - 1;
					}
					if (num6 < 0)
					{
						num6 = 0;
					}
					if (num7 > Main.maxTilesY)
					{
						num7 = Main.maxTilesY;
					}
					for (int k = num4; k < num5; k++)
					{
						for (int l = num6; l < num7; l++)
						{
							if ((double)(Math.Abs((float)k - value.X) + Math.Abs((float)l - value.Y)) < num3 * 0.5 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015) && Main.tile[k, l].type != 31 && Main.tile[k, l].type != 22)
							{
								Main.tile[k, l].active = false;
							}
						}
					}
				}
				if (num <= 2f && (double)value.Y < Main.worldSurface + 45.0)
				{
					num = 2f;
				}
				if (num <= 0f)
				{
					if (!flag2)
					{
						flag2 = true;
						WorldGen.AddShadowOrb((int)value.X, (int)value.Y);
					}
					else
					{
						if (!flag3)
						{
							flag3 = false;
							bool flag4 = false;
							int num8 = 0;
							while (!flag4)
							{
								int num9 = WorldGen.genRand.Next((int)value.X - 25, (int)value.X + 25);
								int num10 = WorldGen.genRand.Next((int)value.Y - 50, (int)value.Y);
								if (num9 < 5)
								{
									num9 = 5;
								}
								if (num9 > Main.maxTilesX - 5)
								{
									num9 = Main.maxTilesX - 5;
								}
								if (num10 < 5)
								{
									num10 = 5;
								}
								if (num10 > Main.maxTilesY - 5)
								{
									num10 = Main.maxTilesY - 5;
								}
								if ((double)num10 > Main.worldSurface)
								{
									WorldGen.Place3x2(num9, num10, 26);
									if (Main.tile[num9, num10].type == 26)
									{
										flag4 = true;
									}
									else
									{
										num8++;
										if (num8 >= 10000)
										{
											flag4 = true;
										}
									}
								}
								else
								{
									flag4 = true;
								}
							}
						}
					}
				}
				value += value2;
				value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.01f;
				if ((double)value2.X > 0.3)
				{
					value2.X = 0.3f;
				}
				if ((double)value2.X < -0.3)
				{
					value2.X = -0.3f;
				}
				num4 = (int)((double)value.X - num3 * 1.1);
				num5 = (int)((double)value.X + num3 * 1.1);
				num6 = (int)((double)value.Y - num3 * 1.1);
				num7 = (int)((double)value.Y + num3 * 1.1);
				if (num4 < 1)
				{
					num4 = 1;
				}
				if (num5 > Main.maxTilesX - 1)
				{
					num5 = Main.maxTilesX - 1;
				}
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesY)
				{
					num7 = Main.maxTilesY;
				}
				for (int m = num4; m < num5; m++)
				{
					for (int n = num6; n < num7; n++)
					{
						if ((double)(Math.Abs((float)m - value.X) + Math.Abs((float)n - value.Y)) < num3 * 1.1 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015))
						{
							if (Main.tile[m, n].type != 25 && n > j + WorldGen.genRand.Next(3, 20))
							{
								Main.tile[m, n].active = true;
							}
							if (steps <= num2)
							{
								Main.tile[m, n].active = true;
							}
							if (Main.tile[m, n].type != 31)
							{
								Main.tile[m, n].type = 25;
							}
							if (Main.tile[m, n].wall == 2)
							{
								Main.tile[m, n].wall = 0;
							}
						}
					}
				}
				for (int num11 = num4; num11 < num5; num11++)
				{
					for (int num12 = num6; num12 < num7; num12++)
					{
						if ((double)(Math.Abs((float)num11 - value.X) + Math.Abs((float)num12 - value.Y)) < num3 * 1.1 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015))
						{
							if (Main.tile[num11, num12].type != 31)
							{
								Main.tile[num11, num12].type = 25;
							}
							if (steps <= num2)
							{
								Main.tile[num11, num12].active = true;
							}
							if (num12 > j + WorldGen.genRand.Next(3, 20))
							{
								WorldGen.PlaceWall(num11, num12, 3, true);
							}
						}
					}
				}
			}
		}
		public static void JungleRunner(int i, int j)
		{
			double num = (double)WorldGen.genRand.Next(5, 11);
			Vector2 value;
			value.X = (float)i;
			value.Y = (float)j;
			Vector2 value2;
			value2.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
			value2.Y = (float)WorldGen.genRand.Next(10, 20) * 0.1f;
			int num2 = 0;
			bool flag = true;
			while (flag)
			{
				if ((double)value.Y < Main.worldSurface)
				{
					int num3 = (int)value.X;
					int num4 = (int)value.Y;
					if (Main.tile[num3, num4].wall == 0 && !Main.tile[num3, num4].active && Main.tile[num3, num4 - 3].wall == 0 && !Main.tile[num3, num4 - 3].active && Main.tile[num3, num4 - 1].wall == 0 && !Main.tile[num3, num4 - 1].active && Main.tile[num3, num4 - 4].wall == 0 && !Main.tile[num3, num4 - 4].active && Main.tile[num3, num4 - 2].wall == 0 && !Main.tile[num3, num4 - 2].active && Main.tile[num3, num4 - 5].wall == 0 && !Main.tile[num3, num4 - 5].active)
					{
						flag = false;
					}
				}
				WorldGen.JungleX = (int)value.X;
				num += (double)((float)WorldGen.genRand.Next(-20, 21) * 0.1f);
				if (num < 5.0)
				{
					num = 5.0;
				}
				if (num > 10.0)
				{
					num = 10.0;
				}
				int num5 = (int)((double)value.X - num * 0.5);
				int num6 = (int)((double)value.X + num * 0.5);
				int num7 = (int)((double)value.Y - num * 0.5);
				int num8 = (int)((double)value.Y + num * 0.5);
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesX)
				{
					num6 = Main.maxTilesX;
				}
				if (num7 < 0)
				{
					num7 = 0;
				}
				if (num8 > Main.maxTilesY)
				{
					num8 = Main.maxTilesY;
				}
				for (int k = num5; k < num6; k++)
				{
					for (int l = num7; l < num8; l++)
					{
						if ((double)(Math.Abs((float)k - value.X) + Math.Abs((float)l - value.Y)) < num * 0.5 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015))
						{
							WorldGen.KillTile(k, l, false, false, false);
						}
					}
				}
				num2++;
				if (num2 > 10 && WorldGen.genRand.Next(50) < num2)
				{
					num2 = 0;
					int num9 = -2;
					if (WorldGen.genRand.Next(2) == 0)
					{
						num9 = 2;
					}
					WorldGen.TileRunner((int)value.X, (int)value.Y, (double)WorldGen.genRand.Next(3, 20), WorldGen.genRand.Next(10, 100), -1, false, (float)num9, 0f, false, true);
				}
				value += value2;
				value2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.01f;
				if (value2.Y > 0f)
				{
					value2.Y = 0f;
				}
				if (value2.Y < -2f)
				{
					value2.Y = -2f;
				}
				value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
				if (value.X < (float)(i - 200))
				{
					value2.X += (float)WorldGen.genRand.Next(5, 21) * 0.1f;
				}
				if (value.X > (float)(i + 200))
				{
					value2.X -= (float)WorldGen.genRand.Next(5, 21) * 0.1f;
				}
				if ((double)value2.X > 1.5)
				{
					value2.X = 1.5f;
				}
				if ((double)value2.X < -1.5)
				{
					value2.X = -1.5f;
				}
			}
		}
		public static void TileRunner(int i, int j, double strength, int steps, int type, bool addTile = false, float speedX = 0f, float speedY = 0f, bool noYChange = false, bool overRide = true)
		{
			double num = strength;
			float num2 = (float)steps;
			Vector2 value;
			value.X = (float)i;
			value.Y = (float)j;
			Vector2 value2;
			value2.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
			value2.Y = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
			if (speedX != 0f || speedY != 0f)
			{
				value2.X = speedX;
				value2.Y = speedY;
			}
			while (num > 0.0 && num2 > 0f)
			{
				if (value.Y < 0f && num2 > 0f && type == 59)
				{
					num2 = 0f;
				}
				num = strength * (double)(num2 / (float)steps);
				num2 -= 1f;
				int num3 = (int)((double)value.X - num * 0.5);
				int num4 = (int)((double)value.X + num * 0.5);
				int num5 = (int)((double)value.Y - num * 0.5);
				int num6 = (int)((double)value.Y + num * 0.5);
				if (num3 < 0)
				{
					num3 = 0;
				}
				if (num4 > Main.maxTilesX)
				{
					num4 = Main.maxTilesX;
				}
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesY)
				{
					num6 = Main.maxTilesY;
				}
				for (int k = num3; k < num4; k++)
				{
					for (int l = num5; l < num6; l++)
					{
						if ((double)(Math.Abs((float)k - value.X) + Math.Abs((float)l - value.Y)) < strength * 0.5 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015))
						{
							if (WorldGen.mudWall && (double)l > Main.worldSurface && l < Main.maxTilesY - 210 - WorldGen.genRand.Next(3))
							{
								WorldGen.PlaceWall(k, l, 15, true);
							}
							if (type < 0)
							{
								if (type == -2 && Main.tile[k, l].active && (l < WorldGen.waterLine || l > WorldGen.lavaLine))
								{
									Main.tile[k, l].liquid = 255;
									if (l > WorldGen.lavaLine)
									{
										Main.tile[k, l].lava = true;
									}
								}
								Main.tile[k, l].active = false;
							}
							else
							{
								if ((overRide || !Main.tile[k, l].active) && (type != 40 || Main.tile[k, l].type != 53) && (!Main.tileStone[type] || Main.tile[k, l].type == 1) && Main.tile[k, l].type != 45 && (Main.tile[k, l].type != 1 || type != 59 || (double)l >= Main.worldSurface + (double)WorldGen.genRand.Next(-50, 50)))
								{
									if (Main.tile[k, l].type != 53 || (double)l >= Main.worldSurface)
									{
										Main.tile[k, l].type = (byte)type;
									}
									else
									{
										if (type == 59)
										{
											Main.tile[k, l].type = (byte)type;
										}
									}
								}
								if (addTile)
								{
									Main.tile[k, l].active = true;
									Main.tile[k, l].liquid = 0;
									Main.tile[k, l].lava = false;
								}
								if (noYChange && (double)l < Main.worldSurface && type != 59)
								{
									Main.tile[k, l].wall = 2;
								}
								if (type == 59 && l > WorldGen.waterLine && Main.tile[k, l].liquid > 0)
								{
									Main.tile[k, l].lava = false;
									Main.tile[k, l].liquid = 0;
								}
							}
						}
					}
				}
				value += value2;
				if (num > 50.0)
				{
					value += value2;
					num2 -= 1f;
					value2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
					value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
					if (num > 100.0)
					{
						value += value2;
						num2 -= 1f;
						value2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
						value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
						if (num > 150.0)
						{
							value += value2;
							num2 -= 1f;
							value2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
							value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
							if (num > 200.0)
							{
								value += value2;
								num2 -= 1f;
								value2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
								value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
								if (num > 250.0)
								{
									value += value2;
									num2 -= 1f;
									value2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
									value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
									if (num > 300.0)
									{
										value += value2;
										num2 -= 1f;
										value2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
										value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
										if (num > 400.0)
										{
											value += value2;
											num2 -= 1f;
											value2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
											value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
											if (num > 500.0)
											{
												value += value2;
												num2 -= 1f;
												value2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
												value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
												if (num > 600.0)
												{
													value += value2;
													num2 -= 1f;
													value2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
													value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
													if (num > 700.0)
													{
														value += value2;
														num2 -= 1f;
														value2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
														value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
														if (num > 800.0)
														{
															value += value2;
															num2 -= 1f;
															value2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
															value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
															if (num > 900.0)
															{
																value += value2;
																num2 -= 1f;
																value2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
																value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
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
				value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				if (value2.X > 1f)
				{
					value2.X = 1f;
				}
				if (value2.X < -1f)
				{
					value2.X = -1f;
				}
				if (!noYChange)
				{
					value2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
					if (value2.Y > 1f)
					{
						value2.Y = 1f;
					}
					if (value2.Y < -1f)
					{
						value2.Y = -1f;
					}
				}
				else
				{
					if (type != 59 && num < 3.0)
					{
						if (value2.Y > 1f)
						{
							value2.Y = 1f;
						}
						if (value2.Y < -1f)
						{
							value2.Y = -1f;
						}
					}
				}
				if (type == 59 && !noYChange)
				{
					if ((double)value2.Y > 0.5)
					{
						value2.Y = 0.5f;
					}
					if ((double)value2.Y < -0.5)
					{
						value2.Y = -0.5f;
					}
					if ((double)value.Y < Main.rockLayer + 100.0)
					{
						value2.Y = 1f;
					}
					if (value.Y > (float)(Main.maxTilesY - 300))
					{
						value2.Y = -1f;
					}
				}
			}
		}
		public static void MudWallRunner(int i, int j)
		{
			double num = (double)WorldGen.genRand.Next(5, 15);
			float num2 = (float)WorldGen.genRand.Next(5, 20);
			float num3 = num2;
			Vector2 value;
			value.X = (float)i;
			value.Y = (float)j;
			Vector2 value2;
			value2.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
			value2.Y = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
			while (num > 0.0 && num3 > 0f)
			{
				double num4 = num * (double)(num3 / num2);
				num3 -= 1f;
				int num5 = (int)((double)value.X - num4 * 0.5);
				int num6 = (int)((double)value.X + num4 * 0.5);
				int num7 = (int)((double)value.Y - num4 * 0.5);
				int num8 = (int)((double)value.Y + num4 * 0.5);
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesX)
				{
					num6 = Main.maxTilesX;
				}
				if (num7 < 0)
				{
					num7 = 0;
				}
				if (num8 > Main.maxTilesY)
				{
					num8 = Main.maxTilesY;
				}
				for (int k = num5; k < num6; k++)
				{
					for (int l = num7; l < num8; l++)
					{
						if ((double)(Math.Abs((float)k - value.X) + Math.Abs((float)l - value.Y)) < num * 0.5 * (1.0 + (double)WorldGen.genRand.Next(-10, 11) * 0.015))
						{
							Main.tile[k, l].wall = 0;
						}
					}
				}
				value += value2;
				value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				if (value2.X > 1f)
				{
					value2.X = 1f;
				}
				if (value2.X < -1f)
				{
					value2.X = -1f;
				}
				value2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				if (value2.Y > 1f)
				{
					value2.Y = 1f;
				}
				if (value2.Y < -1f)
				{
					value2.Y = -1f;
				}
			}
		}
		public static void FloatingIsland(int i, int j)
		{
			double num = (double)WorldGen.genRand.Next(80, 120);
			float num2 = (float)WorldGen.genRand.Next(20, 25);
			Vector2 value;
			value.X = (float)i;
			value.Y = (float)j;
			Vector2 value2;
			value2.X = (float)WorldGen.genRand.Next(-20, 21) * 0.2f;
			while (value2.X > -2f && value2.X < 2f)
			{
				value2.X = (float)WorldGen.genRand.Next(-20, 21) * 0.2f;
			}
			value2.Y = (float)WorldGen.genRand.Next(-20, -10) * 0.02f;
			while (num > 0.0 && num2 > 0f)
			{
				num -= (double)WorldGen.genRand.Next(4);
				num2 -= 1f;
				int num3 = (int)((double)value.X - num * 0.5);
				int num4 = (int)((double)value.X + num * 0.5);
				int num5 = (int)((double)value.Y - num * 0.5);
				int num6 = (int)((double)value.Y + num * 0.5);
				if (num3 < 0)
				{
					num3 = 0;
				}
				if (num4 > Main.maxTilesX)
				{
					num4 = Main.maxTilesX;
				}
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesY)
				{
					num6 = Main.maxTilesY;
				}
				double num7 = num * (double)WorldGen.genRand.Next(80, 120) * 0.01;
				float num8 = value.Y + 1f;
				for (int k = num3; k < num4; k++)
				{
					if (WorldGen.genRand.Next(2) == 0)
					{
						num8 += (float)WorldGen.genRand.Next(-1, 2);
					}
					if (num8 < value.Y)
					{
						num8 = value.Y;
					}
					if (num8 > value.Y + 2f)
					{
						num8 = value.Y + 2f;
					}
					for (int l = num5; l < num6; l++)
					{
						if ((float)l > num8)
						{
							float num9 = Math.Abs((float)k - value.X);
							float num10 = Math.Abs((float)l - value.Y) * 2f;
							double num11 = Math.Sqrt((double)(num9 * num9 + num10 * num10));
							if (num11 < num7 * 0.4)
							{
								Main.tile[k, l].active = true;
								if (Main.tile[k, l].type == 59)
								{
									Main.tile[k, l].type = 0;
								}
							}
						}
					}
				}
				WorldGen.TileRunner(WorldGen.genRand.Next(num3 + 10, num4 - 10), (int)((double)value.Y + num7 * 0.1 + 5.0), (double)WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(10, 15), 0, true, 0f, 2f, true, true);
				num3 = (int)((double)value.X - num * 0.4);
				num4 = (int)((double)value.X + num * 0.4);
				num5 = (int)((double)value.Y - num * 0.4);
				num6 = (int)((double)value.Y + num * 0.4);
				if (num3 < 0)
				{
					num3 = 0;
				}
				if (num4 > Main.maxTilesX)
				{
					num4 = Main.maxTilesX;
				}
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesY)
				{
					num6 = Main.maxTilesY;
				}
				num7 = num * (double)WorldGen.genRand.Next(80, 120) * 0.01;
				for (int m = num3; m < num4; m++)
				{
					for (int n = num5; n < num6; n++)
					{
						if ((float)n > value.Y + 2f)
						{
							float num12 = Math.Abs((float)m - value.X);
							float num13 = Math.Abs((float)n - value.Y) * 2f;
							double num14 = Math.Sqrt((double)(num12 * num12 + num13 * num13));
							if (num14 < num7 * 0.4)
							{
								Main.tile[m, n].wall = 2;
							}
						}
					}
				}
				value += value2;
				value2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				if (value2.X > 1f)
				{
					value2.X = 1f;
				}
				if (value2.X < -1f)
				{
					value2.X = -1f;
				}
				if ((double)value2.Y > 0.2)
				{
					value2.Y = -0.2f;
				}
				if ((double)value2.Y < -0.2)
				{
					value2.Y = -0.2f;
				}
			}
		}
		public static void IslandHouse(int i, int j)
		{
			byte type = (byte)WorldGen.genRand.Next(45, 48);
			byte wall = (byte)WorldGen.genRand.Next(10, 13);
			Vector2 vector = new Vector2((float)i, (float)j);
			int num = 1;
			if (WorldGen.genRand.Next(2) == 0)
			{
				num = -1;
			}
			int num2 = WorldGen.genRand.Next(7, 12);
			int num3 = WorldGen.genRand.Next(5, 7);
			vector.X = (float)(i + (num2 + 2) * num);
			for (int k = j - 15; k < j + 30; k++)
			{
				if (Main.tile[(int)vector.X, k].active)
				{
					vector.Y = (float)(k - 1);
					break;
				}
			}
			vector.X = (float)i;
			int num4 = (int)(vector.X - (float)num2 - 2f);
			int num5 = (int)(vector.X + (float)num2 + 2f);
			int num6 = (int)(vector.Y - (float)num3 - 2f);
			int num7 = (int)(vector.Y + 2f + (float)WorldGen.genRand.Next(3, 5));
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num5 > Main.maxTilesX)
			{
				num5 = Main.maxTilesX;
			}
			if (num6 < 0)
			{
				num6 = 0;
			}
			if (num7 > Main.maxTilesY)
			{
				num7 = Main.maxTilesY;
			}
			for (int l = num4; l <= num5; l++)
			{
				for (int m = num6; m < num7; m++)
				{
					Main.tile[l, m].active = true;
					Main.tile[l, m].type = type;
					Main.tile[l, m].wall = 0;
				}
			}
			num4 = (int)(vector.X - (float)num2);
			num5 = (int)(vector.X + (float)num2);
			num6 = (int)(vector.Y - (float)num3);
			num7 = (int)(vector.Y + 1f);
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num5 > Main.maxTilesX)
			{
				num5 = Main.maxTilesX;
			}
			if (num6 < 0)
			{
				num6 = 0;
			}
			if (num7 > Main.maxTilesY)
			{
				num7 = Main.maxTilesY;
			}
			for (int n = num4; n <= num5; n++)
			{
				for (int num8 = num6; num8 < num7; num8++)
				{
					if (Main.tile[n, num8].wall == 0)
					{
						Main.tile[n, num8].active = false;
						Main.tile[n, num8].wall = wall;
					}
				}
			}
			int num9 = i + (num2 + 1) * num;
			int num10 = (int)vector.Y;
			for (int num11 = num9 - 2; num11 <= num9 + 2; num11++)
			{
				Main.tile[num11, num10].active = false;
				Main.tile[num11, num10 - 1].active = false;
				Main.tile[num11, num10 - 2].active = false;
			}
			WorldGen.PlaceTile(num9, num10, 10, true, false, -1, 0);
			int contain = 0;
			int num12 = WorldGen.houseCount;
			if (num12 > 2)
			{
				num12 = WorldGen.genRand.Next(3);
			}
			if (num12 == 0)
			{
				contain = 159;
			}
			else
			{
				if (num12 == 1)
				{
					contain = 65;
				}
				else
				{
					if (num12 == 2)
					{
						contain = 158;
					}
				}
			}
			WorldGen.AddBuriedChest(i, num10 - 3, contain, false, 2);
			WorldGen.houseCount++;
		}
		public static void Mountinater(int i, int j)
		{
			double num = (double)WorldGen.genRand.Next(80, 120);
			float num2 = (float)WorldGen.genRand.Next(40, 55);
			Vector2 value;
			value.X = (float)i;
			value.Y = (float)j + num2 / 2f;
			Vector2 value2;
			value2.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
			value2.Y = (float)WorldGen.genRand.Next(-20, -10) * 0.1f;
			while (num > 0.0 && num2 > 0f)
			{
				num -= (double)WorldGen.genRand.Next(4);
				num2 -= 1f;
				int num3 = (int)((double)value.X - num * 0.5);
				int num4 = (int)((double)value.X + num * 0.5);
				int num5 = (int)((double)value.Y - num * 0.5);
				int num6 = (int)((double)value.Y + num * 0.5);
				if (num3 < 0)
				{
					num3 = 0;
				}
				if (num4 > Main.maxTilesX)
				{
					num4 = Main.maxTilesX;
				}
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesY)
				{
					num6 = Main.maxTilesY;
				}
				double num7 = num * (double)WorldGen.genRand.Next(80, 120) * 0.01;
				for (int k = num3; k < num4; k++)
				{
					for (int l = num5; l < num6; l++)
					{
						float num8 = Math.Abs((float)k - value.X);
						float num9 = Math.Abs((float)l - value.Y);
						double num10 = Math.Sqrt((double)(num8 * num8 + num9 * num9));
						if (num10 < num7 * 0.4 && !Main.tile[k, l].active)
						{
							Main.tile[k, l].active = true;
							Main.tile[k, l].type = 0;
						}
					}
				}
				value += value2;
				value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				value2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				if ((double)value2.X > 0.5)
				{
					value2.X = 0.5f;
				}
				if ((double)value2.X < -0.5)
				{
					value2.X = -0.5f;
				}
				if ((double)value2.Y > -0.5)
				{
					value2.Y = -0.5f;
				}
				if ((double)value2.Y < -1.5)
				{
					value2.Y = -1.5f;
				}
			}
		}
		public static void Lakinater(int i, int j)
		{
			double num = (double)WorldGen.genRand.Next(25, 50);
			double num2 = num;
			float num3 = (float)WorldGen.genRand.Next(30, 80);
			if (WorldGen.genRand.Next(5) == 0)
			{
				num *= 1.5;
				num2 *= 1.5;
				num3 *= 1.2f;
			}
			Vector2 value;
			value.X = (float)i;
			value.Y = (float)j - num3 * 0.3f;
			Vector2 value2;
			value2.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
			value2.Y = (float)WorldGen.genRand.Next(-20, -10) * 0.1f;
			while (num > 0.0 && num3 > 0f)
			{
				if ((double)value.Y + num2 * 0.5 > Main.worldSurface)
				{
					num3 = 0f;
				}
				num -= (double)WorldGen.genRand.Next(3);
				num3 -= 1f;
				int num4 = (int)((double)value.X - num * 0.5);
				int num5 = (int)((double)value.X + num * 0.5);
				int num6 = (int)((double)value.Y - num * 0.5);
				int num7 = (int)((double)value.Y + num * 0.5);
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num5 > Main.maxTilesX)
				{
					num5 = Main.maxTilesX;
				}
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesY)
				{
					num7 = Main.maxTilesY;
				}
				num2 = num * (double)WorldGen.genRand.Next(80, 120) * 0.01;
				for (int k = num4; k < num5; k++)
				{
					for (int l = num6; l < num7; l++)
					{
						float num8 = Math.Abs((float)k - value.X);
						float num9 = Math.Abs((float)l - value.Y);
						double num10 = Math.Sqrt((double)(num8 * num8 + num9 * num9));
						if (num10 < num2 * 0.4)
						{
							if (Main.tile[k, l].active)
							{
								Main.tile[k, l].liquid = 255;
							}
							Main.tile[k, l].active = false;
						}
					}
				}
				value += value2;
				value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				value2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				if ((double)value2.X > 0.5)
				{
					value2.X = 0.5f;
				}
				if ((double)value2.X < -0.5)
				{
					value2.X = -0.5f;
				}
				if ((double)value2.Y > 1.5)
				{
					value2.Y = 1.5f;
				}
				if ((double)value2.Y < 0.5)
				{
					value2.Y = 0.5f;
				}
			}
		}
		public static void ShroomPatch(int i, int j)
		{
			double num = (double)WorldGen.genRand.Next(40, 70);
			double num2 = num;
			float num3 = (float)WorldGen.genRand.Next(10, 20);
			if (WorldGen.genRand.Next(5) == 0)
			{
				num *= 1.5;
				num2 *= 1.5;
				num3 *= 1.2f;
			}
			Vector2 value;
			value.X = (float)i;
			value.Y = (float)j - num3 * 0.3f;
			Vector2 value2;
			value2.X = (float)WorldGen.genRand.Next(-10, 11) * 0.1f;
			value2.Y = (float)WorldGen.genRand.Next(-20, -10) * 0.1f;
			while (num > 0.0 && num3 > 0f)
			{
				num -= (double)WorldGen.genRand.Next(3);
				num3 -= 1f;
				int num4 = (int)((double)value.X - num * 0.5);
				int num5 = (int)((double)value.X + num * 0.5);
				int num6 = (int)((double)value.Y - num * 0.5);
				int num7 = (int)((double)value.Y + num * 0.5);
				if (num4 < 0)
				{
					num4 = 0;
				}
				if (num5 > Main.maxTilesX)
				{
					num5 = Main.maxTilesX;
				}
				if (num6 < 0)
				{
					num6 = 0;
				}
				if (num7 > Main.maxTilesY)
				{
					num7 = Main.maxTilesY;
				}
				num2 = num * (double)WorldGen.genRand.Next(80, 120) * 0.01;
				for (int k = num4; k < num5; k++)
				{
					for (int l = num6; l < num7; l++)
					{
						float num8 = Math.Abs((float)k - value.X);
						float num9 = Math.Abs(((float)l - value.Y) * 2.3f);
						double num10 = Math.Sqrt((double)(num8 * num8 + num9 * num9));
						if (num10 < num2 * 0.4)
						{
							if ((double)l < (double)value.Y + num2 * 0.05)
							{
								if (Main.tile[k, l].type != 59)
								{
									Main.tile[k, l].active = false;
								}
							}
							else
							{
								Main.tile[k, l].type = 59;
							}
							Main.tile[k, l].liquid = 0;
							Main.tile[k, l].lava = false;
						}
					}
				}
				value += value2;
				value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				value2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				if (value2.X > 1f)
				{
					value2.X = 0.1f;
				}
				if (value2.X < -1f)
				{
					value2.X = -1f;
				}
				if (value2.Y > 1f)
				{
					value2.Y = 1f;
				}
				if (value2.Y < -1f)
				{
					value2.Y = -1f;
				}
			}
		}
		public static void Cavinator(int i, int j, int steps)
		{
			double num = (double)WorldGen.genRand.Next(7, 15);
			int num2 = 1;
			if (WorldGen.genRand.Next(2) == 0)
			{
				num2 = -1;
			}
			Vector2 value;
			value.X = (float)i;
			value.Y = (float)j;
			int k = WorldGen.genRand.Next(20, 40);
			Vector2 value2;
			value2.Y = (float)WorldGen.genRand.Next(10, 20) * 0.01f;
			value2.X = (float)num2;
			while (k > 0)
			{
				k--;
				int num3 = (int)((double)value.X - num * 0.5);
				int num4 = (int)((double)value.X + num * 0.5);
				int num5 = (int)((double)value.Y - num * 0.5);
				int num6 = (int)((double)value.Y + num * 0.5);
				if (num3 < 0)
				{
					num3 = 0;
				}
				if (num4 > Main.maxTilesX)
				{
					num4 = Main.maxTilesX;
				}
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesY)
				{
					num6 = Main.maxTilesY;
				}
				double num7 = num * (double)WorldGen.genRand.Next(80, 120) * 0.01;
				for (int l = num3; l < num4; l++)
				{
					for (int m = num5; m < num6; m++)
					{
						float num8 = Math.Abs((float)l - value.X);
						float num9 = Math.Abs((float)m - value.Y);
						double num10 = Math.Sqrt((double)(num8 * num8 + num9 * num9));
						if (num10 < num7 * 0.4)
						{
							Main.tile[l, m].active = false;
						}
					}
				}
				value += value2;
				value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				value2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				if (value2.X > (float)num2 + 0.5f)
				{
					value2.X = (float)num2 + 0.5f;
				}
				if (value2.X < (float)num2 - 0.5f)
				{
					value2.X = (float)num2 - 0.5f;
				}
				if (value2.Y > 2f)
				{
					value2.Y = 2f;
				}
				if (value2.Y < 0f)
				{
					value2.Y = 0f;
				}
			}
			if (steps > 0 && (double)((int)value.Y) < Main.rockLayer + 50.0)
			{
				WorldGen.Cavinator((int)value.X, (int)value.Y, steps - 1);
			}
		}
		public static void CaveOpenater(int i, int j)
		{
			double num = (double)WorldGen.genRand.Next(7, 12);
			int num2 = 1;
			if (WorldGen.genRand.Next(2) == 0)
			{
				num2 = -1;
			}
			Vector2 value;
			value.X = (float)i;
			value.Y = (float)j;
			int k = 100;
			Vector2 value2;
			value2.Y = 0f;
			value2.X = (float)num2;
			while (k > 0)
			{
				if (Main.tile[(int)value.X, (int)value.Y].wall == 0)
				{
					k = 0;
				}
				k--;
				int num3 = (int)((double)value.X - num * 0.5);
				int num4 = (int)((double)value.X + num * 0.5);
				int num5 = (int)((double)value.Y - num * 0.5);
				int num6 = (int)((double)value.Y + num * 0.5);
				if (num3 < 0)
				{
					num3 = 0;
				}
				if (num4 > Main.maxTilesX)
				{
					num4 = Main.maxTilesX;
				}
				if (num5 < 0)
				{
					num5 = 0;
				}
				if (num6 > Main.maxTilesY)
				{
					num6 = Main.maxTilesY;
				}
				double num7 = num * (double)WorldGen.genRand.Next(80, 120) * 0.01;
				for (int l = num3; l < num4; l++)
				{
					for (int m = num5; m < num6; m++)
					{
						float num8 = Math.Abs((float)l - value.X);
						float num9 = Math.Abs((float)m - value.Y);
						double num10 = Math.Sqrt((double)(num8 * num8 + num9 * num9));
						if (num10 < num7 * 0.4)
						{
							Main.tile[l, m].active = false;
						}
					}
				}
				value += value2;
				value2.X += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				value2.Y += (float)WorldGen.genRand.Next(-10, 11) * 0.05f;
				if (value2.X > (float)num2 + 0.5f)
				{
					value2.X = (float)num2 + 0.5f;
				}
				if (value2.X < (float)num2 - 0.5f)
				{
					value2.X = (float)num2 - 0.5f;
				}
				if (value2.Y > 0f)
				{
					value2.Y = 0f;
				}
				if ((double)value2.Y < -0.5)
				{
					value2.Y = -0.5f;
				}
			}
		}
		public static void SquareTileFrame(int i, int j, bool resetFrame = true)
		{
			WorldGen.TileFrame(i - 1, j - 1, false, false);
			WorldGen.TileFrame(i - 1, j, false, false);
			WorldGen.TileFrame(i - 1, j + 1, false, false);
			WorldGen.TileFrame(i, j - 1, false, false);
			WorldGen.TileFrame(i, j, resetFrame, false);
			WorldGen.TileFrame(i, j + 1, false, false);
			WorldGen.TileFrame(i + 1, j - 1, false, false);
			WorldGen.TileFrame(i + 1, j, false, false);
			WorldGen.TileFrame(i + 1, j + 1, false, false);
		}
		public static void SquareWallFrame(int i, int j, bool resetFrame = true)
		{
			WorldGen.WallFrame(i - 1, j - 1, false);
			WorldGen.WallFrame(i - 1, j, false);
			WorldGen.WallFrame(i - 1, j + 1, false);
			WorldGen.WallFrame(i, j - 1, false);
			WorldGen.WallFrame(i, j, resetFrame);
			WorldGen.WallFrame(i, j + 1, false);
			WorldGen.WallFrame(i + 1, j - 1, false);
			WorldGen.WallFrame(i + 1, j, false);
			WorldGen.WallFrame(i + 1, j + 1, false);
		}
		public static void SectionTileFrame(int startX, int startY, int endX, int endY)
		{
			int num = startX * 200;
			int num2 = (endX + 1) * 200;
			int num3 = startY * 150;
			int num4 = (endY + 1) * 150;
			if (num < 1)
			{
				num = 1;
			}
			if (num3 < 1)
			{
				num3 = 1;
			}
			if (num > Main.maxTilesX - 2)
			{
				num = Main.maxTilesX - 2;
			}
			if (num3 > Main.maxTilesY - 2)
			{
				num3 = Main.maxTilesY - 2;
			}
			for (int i = num - 1; i < num2 + 1; i++)
			{
				for (int j = num3 - 1; j < num4 + 1; j++)
				{
					if (Main.tile[i, j] == null)
					{
						Main.tile[i, j] = new Tile();
					}
					WorldGen.TileFrame(i, j, true, true);
					WorldGen.WallFrame(i, j, true);
				}
			}
		}
		public static void RangeFrame(int startX, int startY, int endX, int endY)
		{
			int num = endX + 1;
			int num2 = endY + 1;
			for (int i = startX - 1; i < num + 1; i++)
			{
				for (int j = startY - 1; j < num2 + 1; j++)
				{
					WorldGen.TileFrame(i, j, false, false);
					WorldGen.WallFrame(i, j, false);
				}
			}
		}
		public static void WaterCheck()
		{
			Liquid.numLiquid = 0;
			LiquidBuffer.numLiquidBuffer = 0;
			for (int i = 1; i < Main.maxTilesX - 1; i++)
			{
				for (int j = Main.maxTilesY - 2; j > 0; j--)
				{
					Main.tile[i, j].checkingLiquid = false;
					if (Main.tile[i, j].liquid > 0 && Main.tile[i, j].active && Main.tileSolid[(int)Main.tile[i, j].type] && !Main.tileSolidTop[(int)Main.tile[i, j].type])
					{
						Main.tile[i, j].liquid = 0;
					}
					else
					{
						if (Main.tile[i, j].liquid > 0)
						{
							if (Main.tile[i, j].active)
							{
								if (Main.tileWaterDeath[(int)Main.tile[i, j].type])
								{
									WorldGen.KillTile(i, j, false, false, false);
								}
								if (Main.tile[i, j].lava && Main.tileLavaDeath[(int)Main.tile[i, j].type])
								{
									WorldGen.KillTile(i, j, false, false, false);
								}
							}
							if ((!Main.tile[i, j + 1].active || !Main.tileSolid[(int)Main.tile[i, j + 1].type] || Main.tileSolidTop[(int)Main.tile[i, j + 1].type]) && Main.tile[i, j + 1].liquid < 255)
							{
								if (Main.tile[i, j + 1].liquid > 250)
								{
									Main.tile[i, j + 1].liquid = 255;
								}
								else
								{
									Liquid.AddWater(i, j);
								}
							}
							if ((!Main.tile[i - 1, j].active || !Main.tileSolid[(int)Main.tile[i - 1, j].type] || Main.tileSolidTop[(int)Main.tile[i - 1, j].type]) && Main.tile[i - 1, j].liquid != Main.tile[i, j].liquid)
							{
								Liquid.AddWater(i, j);
							}
							else
							{
								if ((!Main.tile[i + 1, j].active || !Main.tileSolid[(int)Main.tile[i + 1, j].type] || Main.tileSolidTop[(int)Main.tile[i + 1, j].type]) && Main.tile[i + 1, j].liquid != Main.tile[i, j].liquid)
								{
									Liquid.AddWater(i, j);
								}
							}
							if (Main.tile[i, j].lava)
							{
								if (Main.tile[i - 1, j].liquid > 0 && !Main.tile[i - 1, j].lava)
								{
									Liquid.AddWater(i, j);
								}
								else
								{
									if (Main.tile[i + 1, j].liquid > 0 && !Main.tile[i + 1, j].lava)
									{
										Liquid.AddWater(i, j);
									}
									else
									{
										if (Main.tile[i, j - 1].liquid > 0 && !Main.tile[i, j - 1].lava)
										{
											Liquid.AddWater(i, j);
										}
										else
										{
											if (Main.tile[i, j + 1].liquid > 0 && !Main.tile[i, j + 1].lava)
											{
												Liquid.AddWater(i, j);
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
		public static void EveryTileFrame()
		{
			WorldGen.noLiquidCheck = true;
			WorldGen.noTileActions = true;
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				float num = (float)i / (float)Main.maxTilesX;
				Main.statusText = "Finding tile frames: " + (int)(num * 100f + 1f) + "%";
				for (int j = 0; j < Main.maxTilesY; j++)
				{
					WorldGen.TileFrame(i, j, true, false);
					WorldGen.WallFrame(i, j, true);
				}
			}
			WorldGen.noLiquidCheck = false;
			WorldGen.noTileActions = false;
		}
		public static void PlantCheck(int i, int j)
		{
			int num = -1;
			int type = (int)Main.tile[i, j].type;
			int arg_19_0 = i - 1;
			int arg_23_0 = i + 1;
			int arg_22_0 = Main.maxTilesX;
			int arg_29_0 = j - 1;
			if (j + 1 >= Main.maxTilesY)
			{
				num = type;
			}
			if (i - 1 >= 0 && Main.tile[i - 1, j] != null && Main.tile[i - 1, j].active)
			{
				byte arg_74_0 = Main.tile[i - 1, j].type;
			}
			if (i + 1 < Main.maxTilesX && Main.tile[i + 1, j] != null && Main.tile[i + 1, j].active)
			{
				byte arg_B7_0 = Main.tile[i + 1, j].type;
			}
			if (j - 1 >= 0 && Main.tile[i, j - 1] != null && Main.tile[i, j - 1].active)
			{
				byte arg_F6_0 = Main.tile[i, j - 1].type;
			}
			if (j + 1 < Main.maxTilesY && Main.tile[i, j + 1] != null && Main.tile[i, j + 1].active)
			{
				num = (int)Main.tile[i, j + 1].type;
			}
			if (i - 1 >= 0 && j - 1 >= 0 && Main.tile[i - 1, j - 1] != null && Main.tile[i - 1, j - 1].active)
			{
				byte arg_184_0 = Main.tile[i - 1, j - 1].type;
			}
			if (i + 1 < Main.maxTilesX && j - 1 >= 0 && Main.tile[i + 1, j - 1] != null && Main.tile[i + 1, j - 1].active)
			{
				byte arg_1D3_0 = Main.tile[i + 1, j - 1].type;
			}
			if (i - 1 >= 0 && j + 1 < Main.maxTilesY && Main.tile[i - 1, j + 1] != null && Main.tile[i - 1, j + 1].active)
			{
				byte arg_222_0 = Main.tile[i - 1, j + 1].type;
			}
			if (i + 1 < Main.maxTilesX && j + 1 < Main.maxTilesY && Main.tile[i + 1, j + 1] != null && Main.tile[i + 1, j + 1].active)
			{
				byte arg_275_0 = Main.tile[i + 1, j + 1].type;
			}
			if ((type == 3 && num != 2 && num != 78) || (type == 24 && num != 23) || (type == 61 && num != 60) || (type == 71 && num != 70) || (type == 73 && num != 2 && num != 78) || (type == 74 && num != 60))
			{
				WorldGen.KillTile(i, j, false, false, false);
			}
		}
		public static void WallFrame(int i, int j, bool resetFrame = false)
		{
			if (i >= 0 && j >= 0 && i < Main.maxTilesX && j < Main.maxTilesY && Main.tile[i, j] != null && Main.tile[i, j].wall > 0)
			{
				int num = -1;
				int num2 = -1;
				int num3 = -1;
				int num4 = -1;
				int num5 = -1;
				int num6 = -1;
				int num7 = -1;
				int num8 = -1;
				int wall = (int)Main.tile[i, j].wall;
				if (wall == 0)
				{
					return;
				}
				byte arg_89_0 = Main.tile[i, j].wallFrameX;
				byte arg_9B_0 = Main.tile[i, j].wallFrameY;
				Rectangle rectangle;
				rectangle.X = -1;
				rectangle.Y = -1;
				if (i - 1 < 0)
				{
					num = wall;
					num4 = wall;
					num6 = wall;
				}
				if (i + 1 >= Main.maxTilesX)
				{
					num3 = wall;
					num5 = wall;
					num8 = wall;
				}
				if (j - 1 < 0)
				{
					num = wall;
					num2 = wall;
					num3 = wall;
				}
				if (j + 1 >= Main.maxTilesY)
				{
					num6 = wall;
					num7 = wall;
					num8 = wall;
				}
				if (i - 1 >= 0 && Main.tile[i - 1, j] != null)
				{
					num4 = (int)Main.tile[i - 1, j].wall;
				}
				if (i + 1 < Main.maxTilesX && Main.tile[i + 1, j] != null)
				{
					num5 = (int)Main.tile[i + 1, j].wall;
				}
				if (j - 1 >= 0 && Main.tile[i, j - 1] != null)
				{
					num2 = (int)Main.tile[i, j - 1].wall;
				}
				if (j + 1 < Main.maxTilesY && Main.tile[i, j + 1] != null)
				{
					num7 = (int)Main.tile[i, j + 1].wall;
				}
				if (i - 1 >= 0 && j - 1 >= 0 && Main.tile[i - 1, j - 1] != null)
				{
					num = (int)Main.tile[i - 1, j - 1].wall;
				}
				if (i + 1 < Main.maxTilesX && j - 1 >= 0 && Main.tile[i + 1, j - 1] != null)
				{
					num3 = (int)Main.tile[i + 1, j - 1].wall;
				}
				if (i - 1 >= 0 && j + 1 < Main.maxTilesY && Main.tile[i - 1, j + 1] != null)
				{
					num6 = (int)Main.tile[i - 1, j + 1].wall;
				}
				if (i + 1 < Main.maxTilesX && j + 1 < Main.maxTilesY && Main.tile[i + 1, j + 1] != null)
				{
					num8 = (int)Main.tile[i + 1, j + 1].wall;
				}
				if (wall == 2)
				{
					if (j == (int)Main.worldSurface)
					{
						num7 = wall;
						num6 = wall;
						num8 = wall;
					}
					else
					{
						if (j >= (int)Main.worldSurface)
						{
							num7 = wall;
							num6 = wall;
							num8 = wall;
							num2 = wall;
							num = wall;
							num3 = wall;
							num4 = wall;
							num5 = wall;
						}
					}
				}
				if (num7 > 0)
				{
					num7 = wall;
				}
				if (num6 > 0)
				{
					num6 = wall;
				}
				if (num8 > 0)
				{
					num8 = wall;
				}
				if (num2 > 0)
				{
					num2 = wall;
				}
				if (num > 0)
				{
					num = wall;
				}
				if (num3 > 0)
				{
					num3 = wall;
				}
				if (num4 > 0)
				{
					num4 = wall;
				}
				if (num5 > 0)
				{
					num5 = wall;
				}
				int num9 = 0;
				if (resetFrame)
				{
					num9 = WorldGen.genRand.Next(0, 3);
					Main.tile[i, j].wallFrameNumber = (byte)num9;
				}
				else
				{
					num9 = (int)Main.tile[i, j].wallFrameNumber;
				}
				if (rectangle.X < 0 || rectangle.Y < 0)
				{
					if (num2 == wall && num7 == wall && (num4 == wall & num5 == wall))
					{
						if (num != wall && num3 != wall)
						{
							if (num9 == 0)
							{
								rectangle.X = 108;
								rectangle.Y = 18;
							}
							if (num9 == 1)
							{
								rectangle.X = 126;
								rectangle.Y = 18;
							}
							if (num9 == 2)
							{
								rectangle.X = 144;
								rectangle.Y = 18;
							}
						}
						else
						{
							if (num6 != wall && num8 != wall)
							{
								if (num9 == 0)
								{
									rectangle.X = 108;
									rectangle.Y = 36;
								}
								if (num9 == 1)
								{
									rectangle.X = 126;
									rectangle.Y = 36;
								}
								if (num9 == 2)
								{
									rectangle.X = 144;
									rectangle.Y = 36;
								}
							}
							else
							{
								if (num != wall && num6 != wall)
								{
									if (num9 == 0)
									{
										rectangle.X = 180;
										rectangle.Y = 0;
									}
									if (num9 == 1)
									{
										rectangle.X = 180;
										rectangle.Y = 18;
									}
									if (num9 == 2)
									{
										rectangle.X = 180;
										rectangle.Y = 36;
									}
								}
								else
								{
									if (num3 != wall && num8 != wall)
									{
										if (num9 == 0)
										{
											rectangle.X = 198;
											rectangle.Y = 0;
										}
										if (num9 == 1)
										{
											rectangle.X = 198;
											rectangle.Y = 18;
										}
										if (num9 == 2)
										{
											rectangle.X = 198;
											rectangle.Y = 36;
										}
									}
									else
									{
										if (num9 == 0)
										{
											rectangle.X = 18;
											rectangle.Y = 18;
										}
										if (num9 == 1)
										{
											rectangle.X = 36;
											rectangle.Y = 18;
										}
										if (num9 == 2)
										{
											rectangle.X = 54;
											rectangle.Y = 18;
										}
									}
								}
							}
						}
					}
					else
					{
						if (num2 != wall && num7 == wall && (num4 == wall & num5 == wall))
						{
							if (num9 == 0)
							{
								rectangle.X = 18;
								rectangle.Y = 0;
							}
							if (num9 == 1)
							{
								rectangle.X = 36;
								rectangle.Y = 0;
							}
							if (num9 == 2)
							{
								rectangle.X = 54;
								rectangle.Y = 0;
							}
						}
						else
						{
							if (num2 == wall && num7 != wall && (num4 == wall & num5 == wall))
							{
								if (num9 == 0)
								{
									rectangle.X = 18;
									rectangle.Y = 36;
								}
								if (num9 == 1)
								{
									rectangle.X = 36;
									rectangle.Y = 36;
								}
								if (num9 == 2)
								{
									rectangle.X = 54;
									rectangle.Y = 36;
								}
							}
							else
							{
								if (num2 == wall && num7 == wall && (num4 != wall & num5 == wall))
								{
									if (num9 == 0)
									{
										rectangle.X = 0;
										rectangle.Y = 0;
									}
									if (num9 == 1)
									{
										rectangle.X = 0;
										rectangle.Y = 18;
									}
									if (num9 == 2)
									{
										rectangle.X = 0;
										rectangle.Y = 36;
									}
								}
								else
								{
									if (num2 == wall && num7 == wall && (num4 == wall & num5 != wall))
									{
										if (num9 == 0)
										{
											rectangle.X = 72;
											rectangle.Y = 0;
										}
										if (num9 == 1)
										{
											rectangle.X = 72;
											rectangle.Y = 18;
										}
										if (num9 == 2)
										{
											rectangle.X = 72;
											rectangle.Y = 36;
										}
									}
									else
									{
										if (num2 != wall && num7 == wall && (num4 != wall & num5 == wall))
										{
											if (num9 == 0)
											{
												rectangle.X = 0;
												rectangle.Y = 54;
											}
											if (num9 == 1)
											{
												rectangle.X = 36;
												rectangle.Y = 54;
											}
											if (num9 == 2)
											{
												rectangle.X = 72;
												rectangle.Y = 54;
											}
										}
										else
										{
											if (num2 != wall && num7 == wall && (num4 == wall & num5 != wall))
											{
												if (num9 == 0)
												{
													rectangle.X = 18;
													rectangle.Y = 54;
												}
												if (num9 == 1)
												{
													rectangle.X = 54;
													rectangle.Y = 54;
												}
												if (num9 == 2)
												{
													rectangle.X = 90;
													rectangle.Y = 54;
												}
											}
											else
											{
												if (num2 == wall && num7 != wall && (num4 != wall & num5 == wall))
												{
													if (num9 == 0)
													{
														rectangle.X = 0;
														rectangle.Y = 72;
													}
													if (num9 == 1)
													{
														rectangle.X = 36;
														rectangle.Y = 72;
													}
													if (num9 == 2)
													{
														rectangle.X = 72;
														rectangle.Y = 72;
													}
												}
												else
												{
													if (num2 == wall && num7 != wall && (num4 == wall & num5 != wall))
													{
														if (num9 == 0)
														{
															rectangle.X = 18;
															rectangle.Y = 72;
														}
														if (num9 == 1)
														{
															rectangle.X = 54;
															rectangle.Y = 72;
														}
														if (num9 == 2)
														{
															rectangle.X = 90;
															rectangle.Y = 72;
														}
													}
													else
													{
														if (num2 == wall && num7 == wall && (num4 != wall & num5 != wall))
														{
															if (num9 == 0)
															{
																rectangle.X = 90;
																rectangle.Y = 0;
															}
															if (num9 == 1)
															{
																rectangle.X = 90;
																rectangle.Y = 18;
															}
															if (num9 == 2)
															{
																rectangle.X = 90;
																rectangle.Y = 36;
															}
														}
														else
														{
															if (num2 != wall && num7 != wall && (num4 == wall & num5 == wall))
															{
																if (num9 == 0)
																{
																	rectangle.X = 108;
																	rectangle.Y = 72;
																}
																if (num9 == 1)
																{
																	rectangle.X = 126;
																	rectangle.Y = 72;
																}
																if (num9 == 2)
																{
																	rectangle.X = 144;
																	rectangle.Y = 72;
																}
															}
															else
															{
																if (num2 != wall && num7 == wall && (num4 != wall & num5 != wall))
																{
																	if (num9 == 0)
																	{
																		rectangle.X = 108;
																		rectangle.Y = 0;
																	}
																	if (num9 == 1)
																	{
																		rectangle.X = 126;
																		rectangle.Y = 0;
																	}
																	if (num9 == 2)
																	{
																		rectangle.X = 144;
																		rectangle.Y = 0;
																	}
																}
																else
																{
																	if (num2 == wall && num7 != wall && (num4 != wall & num5 != wall))
																	{
																		if (num9 == 0)
																		{
																			rectangle.X = 108;
																			rectangle.Y = 54;
																		}
																		if (num9 == 1)
																		{
																			rectangle.X = 126;
																			rectangle.Y = 54;
																		}
																		if (num9 == 2)
																		{
																			rectangle.X = 144;
																			rectangle.Y = 54;
																		}
																	}
																	else
																	{
																		if (num2 != wall && num7 != wall && (num4 != wall & num5 == wall))
																		{
																			if (num9 == 0)
																			{
																				rectangle.X = 162;
																				rectangle.Y = 0;
																			}
																			if (num9 == 1)
																			{
																				rectangle.X = 162;
																				rectangle.Y = 18;
																			}
																			if (num9 == 2)
																			{
																				rectangle.X = 162;
																				rectangle.Y = 36;
																			}
																		}
																		else
																		{
																			if (num2 != wall && num7 != wall && (num4 == wall & num5 != wall))
																			{
																				if (num9 == 0)
																				{
																					rectangle.X = 216;
																					rectangle.Y = 0;
																				}
																				if (num9 == 1)
																				{
																					rectangle.X = 216;
																					rectangle.Y = 18;
																				}
																				if (num9 == 2)
																				{
																					rectangle.X = 216;
																					rectangle.Y = 36;
																				}
																			}
																			else
																			{
																				if (num2 != wall && num7 != wall && (num4 != wall & num5 != wall))
																				{
																					if (num9 == 0)
																					{
																						rectangle.X = 162;
																						rectangle.Y = 54;
																					}
																					if (num9 == 1)
																					{
																						rectangle.X = 180;
																						rectangle.Y = 54;
																					}
																					if (num9 == 2)
																					{
																						rectangle.X = 198;
																						rectangle.Y = 54;
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
				if (rectangle.X <= -1 || rectangle.Y <= -1)
				{
					if (num9 <= 0)
					{
						rectangle.X = 18;
						rectangle.Y = 18;
					}
					if (num9 == 1)
					{
						rectangle.X = 36;
						rectangle.Y = 18;
					}
					if (num9 >= 2)
					{
						rectangle.X = 54;
						rectangle.Y = 18;
					}
				}
				Main.tile[i, j].wallFrameX = (byte)rectangle.X;
				Main.tile[i, j].wallFrameY = (byte)rectangle.Y;
			}
		}
		public static void TileFrame(int i, int j, bool resetFrame = false, bool noBreak = false)
		{
			if (i > 5 && j > 5 && i < Main.maxTilesX - 5 && j < Main.maxTilesY - 5 && Main.tile[i, j] != null)
			{
				if (Main.tile[i, j].liquid > 0 && Main.netMode != 1 && !WorldGen.noLiquidCheck)
				{
					Liquid.AddWater(i, j);
				}
				if (Main.tile[i, j].active)
				{
					if (noBreak && Main.tileFrameImportant[(int)Main.tile[i, j].type])
					{
						return;
					}
					int num = -1;
					int num2 = -1;
					int num3 = -1;
					int num4 = -1;
					int num5 = -1;
					int num6 = -1;
					int num7 = -1;
					int num8 = -1;
					int num9 = (int)Main.tile[i, j].type;
					if (Main.tileStone[num9])
					{
						num9 = 1;
					}
					int frameX = (int)Main.tile[i, j].frameX;
					int frameY = (int)Main.tile[i, j].frameY;
					Rectangle rectangle;
					rectangle.X = -1;
					rectangle.Y = -1;
					WorldGen.mergeUp = false;
					WorldGen.mergeDown = false;
					WorldGen.mergeLeft = false;
					WorldGen.mergeRight = false;
					if (Main.tile[i - 1, j] != null && Main.tile[i - 1, j].active)
					{
						num4 = (int)Main.tile[i - 1, j].type;
					}
					if (Main.tile[i + 1, j] != null && Main.tile[i + 1, j].active)
					{
						num5 = (int)Main.tile[i + 1, j].type;
					}
					if (Main.tile[i, j - 1] != null && Main.tile[i, j - 1].active)
					{
						num2 = (int)Main.tile[i, j - 1].type;
					}
					if (Main.tile[i, j + 1] != null && Main.tile[i, j + 1].active)
					{
						num7 = (int)Main.tile[i, j + 1].type;
					}
					if (Main.tile[i - 1, j - 1] != null && Main.tile[i - 1, j - 1].active)
					{
						num = (int)Main.tile[i - 1, j - 1].type;
					}
					if (Main.tile[i + 1, j - 1] != null && Main.tile[i + 1, j - 1].active)
					{
						num3 = (int)Main.tile[i + 1, j - 1].type;
					}
					if (Main.tile[i - 1, j + 1] != null && Main.tile[i - 1, j + 1].active)
					{
						num6 = (int)Main.tile[i - 1, j + 1].type;
					}
					if (Main.tile[i + 1, j + 1] != null && Main.tile[i + 1, j + 1].active)
					{
						num8 = (int)Main.tile[i + 1, j + 1].type;
					}
					if (num4 >= 0 && Main.tileStone[num4])
					{
						num4 = 1;
					}
					if (num5 >= 0 && Main.tileStone[num5])
					{
						num5 = 1;
					}
					if (num2 >= 0 && Main.tileStone[num2])
					{
						num2 = 1;
					}
					if (num7 >= 0 && Main.tileStone[num7])
					{
						num7 = 1;
					}
					if (num >= 0 && Main.tileStone[num])
					{
						num = 1;
					}
					if (num3 >= 0 && Main.tileStone[num3])
					{
						num3 = 1;
					}
					if (num6 >= 0 && Main.tileStone[num6])
					{
						num6 = 1;
					}
					if (num8 >= 0 && Main.tileStone[num8])
					{
						num8 = 1;
					}
					if (num9 != 0 && num9 != 1)
					{
						if (num9 == 3 || num9 == 24 || num9 == 61 || num9 == 71 || num9 == 73 || num9 == 74)
						{
							WorldGen.PlantCheck(i, j);
							return;
						}
						if (num9 == 4)
						{
							if (num7 >= 0 && Main.tileSolid[num7] && !Main.tileNoAttach[num7])
							{
								Main.tile[i, j].frameX = 0;
								return;
							}
							if ((num4 >= 0 && Main.tileSolid[num4] && !Main.tileNoAttach[num4]) || (num4 == 5 && num == 5 && num6 == 5))
							{
								Main.tile[i, j].frameX = 22;
								return;
							}
							if ((num5 >= 0 && Main.tileSolid[num5] && !Main.tileNoAttach[num5]) || (num5 == 5 && num3 == 5 && num8 == 5))
							{
								Main.tile[i, j].frameX = 44;
								return;
							}
							WorldGen.KillTile(i, j, false, false, false);
							return;
						}
						else
						{
							if (num9 == 80)
							{
								WorldGen.CactusFrame(i, j);
								return;
							}
							if (num9 == 12 || num9 == 31)
							{
								if (!WorldGen.destroyObject)
								{
									int num10 = i;
									int num11 = j;
									if (Main.tile[i, j].frameX == 0)
									{
										num10 = i;
									}
									else
									{
										num10 = i - 1;
									}
									if (Main.tile[i, j].frameY == 0)
									{
										num11 = j;
									}
									else
									{
										num11 = j - 1;
									}
									if (Main.tile[num10, num11] != null && Main.tile[num10 + 1, num11] != null && Main.tile[num10, num11 + 1] != null && Main.tile[num10 + 1, num11 + 1] != null && (!Main.tile[num10, num11].active || (int)Main.tile[num10, num11].type != num9 || !Main.tile[num10 + 1, num11].active || (int)Main.tile[num10 + 1, num11].type != num9 || !Main.tile[num10, num11 + 1].active || (int)Main.tile[num10, num11 + 1].type != num9 || !Main.tile[num10 + 1, num11 + 1].active || (int)Main.tile[num10 + 1, num11 + 1].type != num9))
									{
										WorldGen.destroyObject = true;
										if ((int)Main.tile[num10, num11].type == num9)
										{
											WorldGen.KillTile(num10, num11, false, false, false);
										}
										if ((int)Main.tile[num10 + 1, num11].type == num9)
										{
											WorldGen.KillTile(num10 + 1, num11, false, false, false);
										}
										if ((int)Main.tile[num10, num11 + 1].type == num9)
										{
											WorldGen.KillTile(num10, num11 + 1, false, false, false);
										}
										if ((int)Main.tile[num10 + 1, num11 + 1].type == num9)
										{
											WorldGen.KillTile(num10 + 1, num11 + 1, false, false, false);
										}
										if (Main.netMode != 1 && !WorldGen.noTileActions)
										{
											if (num9 == 12)
											{
												Item.NewItem(num10 * 16, num11 * 16, 32, 32, 29, 1, false);
											}
											else
											{
												if (num9 == 31)
												{
													if (WorldGen.genRand.Next(2) == 0)
													{
														WorldGen.spawnMeteor = true;
													}
													int num12 = Main.rand.Next(5);
													if (!WorldGen.shadowOrbSmashed)
													{
														num12 = 0;
													}
													if (num12 == 0)
													{
														Item.NewItem(num10 * 16, num11 * 16, 32, 32, 96, 1, false);
														int stack = WorldGen.genRand.Next(25, 51);
														Item.NewItem(num10 * 16, num11 * 16, 32, 32, 97, stack, false);
													}
													else
													{
														if (num12 == 1)
														{
															Item.NewItem(num10 * 16, num11 * 16, 32, 32, 64, 1, false);
														}
														else
														{
															if (num12 == 2)
															{
																Item.NewItem(num10 * 16, num11 * 16, 32, 32, 162, 1, false);
															}
															else
															{
																if (num12 == 3)
																{
																	Item.NewItem(num10 * 16, num11 * 16, 32, 32, 115, 1, false);
																}
																else
																{
																	if (num12 == 4)
																	{
																		Item.NewItem(num10 * 16, num11 * 16, 32, 32, 111, 1, false);
																	}
																}
															}
														}
													}
													WorldGen.shadowOrbSmashed = true;
													WorldGen.shadowOrbCount++;
													if (WorldGen.shadowOrbCount >= 3)
													{
														WorldGen.shadowOrbCount = 0;
														float num13 = (float)(num10 * 16);
														float num14 = (float)(num11 * 16);
														float num15 = -1f;
														int plr = 0;
														for (int k = 0; k < 255; k++)
														{
															float num16 = Math.Abs(Main.player[k].position.X - num13) + Math.Abs(Main.player[k].position.Y - num14);
															if (num16 < num15 || num15 == -1f)
															{
																plr = 0;
																num15 = num16;
															}
														}
														NPC.SpawnOnPlayer(plr, 13);
													}
													else
													{
														string text = "A horrible chill goes down your spine...";
														if (WorldGen.shadowOrbCount == 2)
														{
															text = "Screams echo around you...";
														}
														if (Main.netMode == 0)
														{
															Main.NewText(text, 50, 255, 130);
														}
														else
														{
															if (Main.netMode == 2)
															{
																NetMessage.SendData(25, -1, -1, text, 255, 50f, 255f, 130f, 0);
															}
														}
													}
												}
											}
										}
										Main.PlaySound(13, i * 16, j * 16, 1);
										WorldGen.destroyObject = false;
									}
								}
								return;
							}
							if (num9 == 19)
							{
								if (num4 == num9 && num5 == num9)
								{
									if (Main.tile[i, j].frameNumber == 0)
									{
										rectangle.X = 0;
										rectangle.Y = 0;
									}
									if (Main.tile[i, j].frameNumber == 1)
									{
										rectangle.X = 0;
										rectangle.Y = 18;
									}
									if (Main.tile[i, j].frameNumber == 2)
									{
										rectangle.X = 0;
										rectangle.Y = 36;
									}
								}
								else
								{
									if (num4 == num9 && num5 == -1)
									{
										if (Main.tile[i, j].frameNumber == 0)
										{
											rectangle.X = 18;
											rectangle.Y = 0;
										}
										if (Main.tile[i, j].frameNumber == 1)
										{
											rectangle.X = 18;
											rectangle.Y = 18;
										}
										if (Main.tile[i, j].frameNumber == 2)
										{
											rectangle.X = 18;
											rectangle.Y = 36;
										}
									}
									else
									{
										if (num4 == -1 && num5 == num9)
										{
											if (Main.tile[i, j].frameNumber == 0)
											{
												rectangle.X = 36;
												rectangle.Y = 0;
											}
											if (Main.tile[i, j].frameNumber == 1)
											{
												rectangle.X = 36;
												rectangle.Y = 18;
											}
											if (Main.tile[i, j].frameNumber == 2)
											{
												rectangle.X = 36;
												rectangle.Y = 36;
											}
										}
										else
										{
											if (num4 != num9 && num5 == num9)
											{
												if (Main.tile[i, j].frameNumber == 0)
												{
													rectangle.X = 54;
													rectangle.Y = 0;
												}
												if (Main.tile[i, j].frameNumber == 1)
												{
													rectangle.X = 54;
													rectangle.Y = 18;
												}
												if (Main.tile[i, j].frameNumber == 2)
												{
													rectangle.X = 54;
													rectangle.Y = 36;
												}
											}
											else
											{
												if (num4 == num9 && num5 != num9)
												{
													if (Main.tile[i, j].frameNumber == 0)
													{
														rectangle.X = 72;
														rectangle.Y = 0;
													}
													if (Main.tile[i, j].frameNumber == 1)
													{
														rectangle.X = 72;
														rectangle.Y = 18;
													}
													if (Main.tile[i, j].frameNumber == 2)
													{
														rectangle.X = 72;
														rectangle.Y = 36;
													}
												}
												else
												{
													if (num4 != num9 && num4 != -1 && num5 == -1)
													{
														if (Main.tile[i, j].frameNumber == 0)
														{
															rectangle.X = 108;
															rectangle.Y = 0;
														}
														if (Main.tile[i, j].frameNumber == 1)
														{
															rectangle.X = 108;
															rectangle.Y = 18;
														}
														if (Main.tile[i, j].frameNumber == 2)
														{
															rectangle.X = 108;
															rectangle.Y = 36;
														}
													}
													else
													{
														if (num4 == -1 && num5 != num9 && num5 != -1)
														{
															if (Main.tile[i, j].frameNumber == 0)
															{
																rectangle.X = 126;
																rectangle.Y = 0;
															}
															if (Main.tile[i, j].frameNumber == 1)
															{
																rectangle.X = 126;
																rectangle.Y = 18;
															}
															if (Main.tile[i, j].frameNumber == 2)
															{
																rectangle.X = 126;
																rectangle.Y = 36;
															}
														}
														else
														{
															if (Main.tile[i, j].frameNumber == 0)
															{
																rectangle.X = 90;
																rectangle.Y = 0;
															}
															if (Main.tile[i, j].frameNumber == 1)
															{
																rectangle.X = 90;
																rectangle.Y = 18;
															}
															if (Main.tile[i, j].frameNumber == 2)
															{
																rectangle.X = 90;
																rectangle.Y = 36;
															}
														}
													}
												}
											}
										}
									}
								}
							}
							else
							{
								if (num9 == 10)
								{
									if (!WorldGen.destroyObject)
									{
										int frameY2 = (int)Main.tile[i, j].frameY;
										int num17 = j;
										bool flag = false;
										if (frameY2 == 0)
										{
											num17 = j;
										}
										if (frameY2 == 18)
										{
											num17 = j - 1;
										}
										if (frameY2 == 36)
										{
											num17 = j - 2;
										}
										if (Main.tile[i, num17 - 1] == null)
										{
											Main.tile[i, num17 - 1] = new Tile();
										}
										if (Main.tile[i, num17 + 3] == null)
										{
											Main.tile[i, num17 + 3] = new Tile();
										}
										if (Main.tile[i, num17 + 2] == null)
										{
											Main.tile[i, num17 + 2] = new Tile();
										}
										if (Main.tile[i, num17 + 1] == null)
										{
											Main.tile[i, num17 + 1] = new Tile();
										}
										if (Main.tile[i, num17] == null)
										{
											Main.tile[i, num17] = new Tile();
										}
										if (!Main.tile[i, num17 - 1].active || !Main.tileSolid[(int)Main.tile[i, num17 - 1].type])
										{
											flag = true;
										}
										if (!Main.tile[i, num17 + 3].active || !Main.tileSolid[(int)Main.tile[i, num17 + 3].type])
										{
											flag = true;
										}
										if (!Main.tile[i, num17].active || (int)Main.tile[i, num17].type != num9)
										{
											flag = true;
										}
										if (!Main.tile[i, num17 + 1].active || (int)Main.tile[i, num17 + 1].type != num9)
										{
											flag = true;
										}
										if (!Main.tile[i, num17 + 2].active || (int)Main.tile[i, num17 + 2].type != num9)
										{
											flag = true;
										}
										if (flag)
										{
											WorldGen.destroyObject = true;
											WorldGen.KillTile(i, num17, false, false, false);
											WorldGen.KillTile(i, num17 + 1, false, false, false);
											WorldGen.KillTile(i, num17 + 2, false, false, false);
											Item.NewItem(i * 16, j * 16, 16, 16, 25, 1, false);
										}
										WorldGen.destroyObject = false;
									}
									return;
								}
								if (num9 == 11)
								{
									if (!WorldGen.destroyObject)
									{
										int num18 = 0;
										int num19 = i;
										int num20 = j;
										int frameX2 = (int)Main.tile[i, j].frameX;
										int frameY3 = (int)Main.tile[i, j].frameY;
										bool flag2 = false;
										if (frameX2 == 0)
										{
											num19 = i;
											num18 = 1;
										}
										else
										{
											if (frameX2 == 18)
											{
												num19 = i - 1;
												num18 = 1;
											}
											else
											{
												if (frameX2 == 36)
												{
													num19 = i + 1;
													num18 = -1;
												}
												else
												{
													if (frameX2 == 54)
													{
														num19 = i;
														num18 = -1;
													}
												}
											}
										}
										if (frameY3 == 0)
										{
											num20 = j;
										}
										else
										{
											if (frameY3 == 18)
											{
												num20 = j - 1;
											}
											else
											{
												if (frameY3 == 36)
												{
													num20 = j - 2;
												}
											}
										}
										if (Main.tile[num19, num20 + 3] == null)
										{
											Main.tile[num19, num20 + 3] = new Tile();
										}
										if (Main.tile[num19, num20 - 1] == null)
										{
											Main.tile[num19, num20 - 1] = new Tile();
										}
										if (!Main.tile[num19, num20 - 1].active || !Main.tileSolid[(int)Main.tile[num19, num20 - 1].type] || !Main.tile[num19, num20 + 3].active || !Main.tileSolid[(int)Main.tile[num19, num20 + 3].type])
										{
											flag2 = true;
											WorldGen.destroyObject = true;
											Item.NewItem(i * 16, j * 16, 16, 16, 25, 1, false);
										}
										int num21 = num19;
										if (num18 == -1)
										{
											num21 = num19 - 1;
										}
										for (int l = num21; l < num21 + 2; l++)
										{
											for (int m = num20; m < num20 + 3; m++)
											{
												if (!flag2 && (Main.tile[l, m].type != 11 || !Main.tile[l, m].active))
												{
													WorldGen.destroyObject = true;
													Item.NewItem(i * 16, j * 16, 16, 16, 25, 1, false);
													flag2 = true;
													l = num21;
													m = num20;
												}
												if (flag2)
												{
													WorldGen.KillTile(l, m, false, false, false);
												}
											}
										}
										WorldGen.destroyObject = false;
									}
									return;
								}
								if (num9 == 34 || num9 == 35 || num9 == 36 || num9 == 106)
								{
									WorldGen.Check3x3(i, j, (int)((byte)num9));
									return;
								}
								if (num9 == 15 || num9 == 20)
								{
									WorldGen.Check1x2(i, j, (byte)num9);
									return;
								}
								if (num9 == 14 || num9 == 17 || num9 == 26 || num9 == 77 || num9 == 86 || num9 == 87 || num9 == 88 || num9 == 89)
								{
									WorldGen.Check3x2(i, j, (int)((byte)num9));
									return;
								}
								if (num9 == 16 || num9 == 18 || num9 == 29 || num9 == 103)
								{
									WorldGen.Check2x1(i, j, (byte)num9);
									return;
								}
								if (num9 == 13 || num9 == 33 || num9 == 49 || num9 == 50 || num9 == 78)
								{
									WorldGen.CheckOnTable1x1(i, j, (int)((byte)num9));
									return;
								}
								if (num9 == 21)
								{
									WorldGen.CheckChest(i, j, (int)((byte)num9));
									return;
								}
								if (num9 == 27)
								{
									WorldGen.CheckSunflower(i, j, 27);
									return;
								}
								if (num9 == 28)
								{
									WorldGen.CheckPot(i, j, 28);
									return;
								}
								if (num9 == 91)
								{
									WorldGen.CheckBanner(i, j, (byte)num9);
									return;
								}
								if (num9 == 92 || num9 == 93)
								{
									WorldGen.Check1xX(i, j, (byte)num9);
									return;
								}
								if (num9 == 104 || num9 == 105)
								{
									WorldGen.Check2xX(i, j, (byte)num9);
								}
								else
								{
									if (num9 == 101 || num9 == 102)
									{
										WorldGen.Check3x4(i, j, (int)((byte)num9));
										return;
									}
									if (num9 == 42)
									{
										WorldGen.Check1x2Top(i, j, (byte)num9);
										return;
									}
									if (num9 == 55 || num9 == 85)
									{
										WorldGen.CheckSign(i, j, num9);
										return;
									}
									if (num9 == 79 || num9 == 90)
									{
										WorldGen.Check4x2(i, j, num9);
										return;
									}
									if (num9 == 85 || num9 == 94 || num9 == 95 || num9 == 96 || num9 == 97 || num9 == 98 || num9 == 99 || num9 == 100)
									{
										WorldGen.Check2x2(i, j, num9);
										return;
									}
									if (num9 == 81)
									{
										if (num4 != -1 || num2 != -1 || num5 != -1)
										{
											WorldGen.KillTile(i, j, false, false, false);
											return;
										}
										if (num7 < 0 || !Main.tileSolid[num7])
										{
											WorldGen.KillTile(i, j, false, false, false);
										}
										return;
									}
									else
									{
										if (Main.tileAlch[num9])
										{
											WorldGen.CheckAlch(i, j);
											return;
										}
										if (num9 == 72)
										{
											if (num7 != num9 && num7 != 70)
											{
												WorldGen.KillTile(i, j, false, false, false);
											}
											else
											{
												if (num2 != num9 && Main.tile[i, j].frameX == 0)
												{
													Main.tile[i, j].frameNumber = (byte)WorldGen.genRand.Next(3);
													if (Main.tile[i, j].frameNumber == 0)
													{
														Main.tile[i, j].frameX = 18;
														Main.tile[i, j].frameY = 0;
													}
													if (Main.tile[i, j].frameNumber == 1)
													{
														Main.tile[i, j].frameX = 18;
														Main.tile[i, j].frameY = 18;
													}
													if (Main.tile[i, j].frameNumber == 2)
													{
														Main.tile[i, j].frameX = 18;
														Main.tile[i, j].frameY = 36;
													}
												}
											}
										}
										else
										{
											if (num9 == 5)
											{
												if (num7 == 23)
												{
													num7 = 2;
												}
												if (num7 == 60)
												{
													num7 = 2;
												}
												if (Main.tile[i, j].frameX >= 22 && Main.tile[i, j].frameX <= 44 && Main.tile[i, j].frameY >= 132 && Main.tile[i, j].frameY <= 176)
												{
													if ((num4 != num9 && num5 != num9) || num7 != 2)
													{
														WorldGen.KillTile(i, j, false, false, false);
													}
												}
												else
												{
													if ((Main.tile[i, j].frameX == 88 && Main.tile[i, j].frameY >= 0 && Main.tile[i, j].frameY <= 44) || (Main.tile[i, j].frameX == 66 && Main.tile[i, j].frameY >= 66 && Main.tile[i, j].frameY <= 130) || (Main.tile[i, j].frameX == 110 && Main.tile[i, j].frameY >= 66 && Main.tile[i, j].frameY <= 110) || (Main.tile[i, j].frameX == 132 && Main.tile[i, j].frameY >= 0 && Main.tile[i, j].frameY <= 176))
													{
														if (num4 == num9 && num5 == num9)
														{
															if (Main.tile[i, j].frameNumber == 0)
															{
																Main.tile[i, j].frameX = 110;
																Main.tile[i, j].frameY = 66;
															}
															if (Main.tile[i, j].frameNumber == 1)
															{
																Main.tile[i, j].frameX = 110;
																Main.tile[i, j].frameY = 88;
															}
															if (Main.tile[i, j].frameNumber == 2)
															{
																Main.tile[i, j].frameX = 110;
																Main.tile[i, j].frameY = 110;
															}
														}
														else
														{
															if (num4 == num9)
															{
																if (Main.tile[i, j].frameNumber == 0)
																{
																	Main.tile[i, j].frameX = 88;
																	Main.tile[i, j].frameY = 0;
																}
																if (Main.tile[i, j].frameNumber == 1)
																{
																	Main.tile[i, j].frameX = 88;
																	Main.tile[i, j].frameY = 22;
																}
																if (Main.tile[i, j].frameNumber == 2)
																{
																	Main.tile[i, j].frameX = 88;
																	Main.tile[i, j].frameY = 44;
																}
															}
															else
															{
																if (num5 == num9)
																{
																	if (Main.tile[i, j].frameNumber == 0)
																	{
																		Main.tile[i, j].frameX = 66;
																		Main.tile[i, j].frameY = 66;
																	}
																	if (Main.tile[i, j].frameNumber == 1)
																	{
																		Main.tile[i, j].frameX = 66;
																		Main.tile[i, j].frameY = 88;
																	}
																	if (Main.tile[i, j].frameNumber == 2)
																	{
																		Main.tile[i, j].frameX = 66;
																		Main.tile[i, j].frameY = 110;
																	}
																}
																else
																{
																	if (Main.tile[i, j].frameNumber == 0)
																	{
																		Main.tile[i, j].frameX = 0;
																		Main.tile[i, j].frameY = 0;
																	}
																	if (Main.tile[i, j].frameNumber == 1)
																	{
																		Main.tile[i, j].frameX = 0;
																		Main.tile[i, j].frameY = 22;
																	}
																	if (Main.tile[i, j].frameNumber == 2)
																	{
																		Main.tile[i, j].frameX = 0;
																		Main.tile[i, j].frameY = 44;
																	}
																}
															}
														}
													}
												}
												if (Main.tile[i, j].frameY >= 132 && Main.tile[i, j].frameY <= 176 && (Main.tile[i, j].frameX == 0 || Main.tile[i, j].frameX == 66 || Main.tile[i, j].frameX == 88))
												{
													if (num7 != 2)
													{
														WorldGen.KillTile(i, j, false, false, false);
													}
													if (num4 != num9 && num5 != num9)
													{
														if (Main.tile[i, j].frameNumber == 0)
														{
															Main.tile[i, j].frameX = 0;
															Main.tile[i, j].frameY = 0;
														}
														if (Main.tile[i, j].frameNumber == 1)
														{
															Main.tile[i, j].frameX = 0;
															Main.tile[i, j].frameY = 22;
														}
														if (Main.tile[i, j].frameNumber == 2)
														{
															Main.tile[i, j].frameX = 0;
															Main.tile[i, j].frameY = 44;
														}
													}
													else
													{
														if (num4 != num9)
														{
															if (Main.tile[i, j].frameNumber == 0)
															{
																Main.tile[i, j].frameX = 0;
																Main.tile[i, j].frameY = 132;
															}
															if (Main.tile[i, j].frameNumber == 1)
															{
																Main.tile[i, j].frameX = 0;
																Main.tile[i, j].frameY = 154;
															}
															if (Main.tile[i, j].frameNumber == 2)
															{
																Main.tile[i, j].frameX = 0;
																Main.tile[i, j].frameY = 176;
															}
														}
														else
														{
															if (num5 != num9)
															{
																if (Main.tile[i, j].frameNumber == 0)
																{
																	Main.tile[i, j].frameX = 66;
																	Main.tile[i, j].frameY = 132;
																}
																if (Main.tile[i, j].frameNumber == 1)
																{
																	Main.tile[i, j].frameX = 66;
																	Main.tile[i, j].frameY = 154;
																}
																if (Main.tile[i, j].frameNumber == 2)
																{
																	Main.tile[i, j].frameX = 66;
																	Main.tile[i, j].frameY = 176;
																}
															}
															else
															{
																if (Main.tile[i, j].frameNumber == 0)
																{
																	Main.tile[i, j].frameX = 88;
																	Main.tile[i, j].frameY = 132;
																}
																if (Main.tile[i, j].frameNumber == 1)
																{
																	Main.tile[i, j].frameX = 88;
																	Main.tile[i, j].frameY = 154;
																}
																if (Main.tile[i, j].frameNumber == 2)
																{
																	Main.tile[i, j].frameX = 88;
																	Main.tile[i, j].frameY = 176;
																}
															}
														}
													}
												}
												if ((Main.tile[i, j].frameX == 66 && (Main.tile[i, j].frameY == 0 || Main.tile[i, j].frameY == 22 || Main.tile[i, j].frameY == 44)) || (Main.tile[i, j].frameX == 88 && (Main.tile[i, j].frameY == 66 || Main.tile[i, j].frameY == 88 || Main.tile[i, j].frameY == 110)) || (Main.tile[i, j].frameX == 44 && (Main.tile[i, j].frameY == 198 || Main.tile[i, j].frameY == 220 || Main.tile[i, j].frameY == 242)) || (Main.tile[i, j].frameX == 66 && (Main.tile[i, j].frameY == 198 || Main.tile[i, j].frameY == 220 || Main.tile[i, j].frameY == 242)))
												{
													if (num4 != num9 && num5 != num9)
													{
														WorldGen.KillTile(i, j, false, false, false);
													}
												}
												else
												{
													if (num7 == -1 || num7 == 23)
													{
														WorldGen.KillTile(i, j, false, false, false);
													}
													else
													{
														if (num2 != num9 && Main.tile[i, j].frameY < 198 && ((Main.tile[i, j].frameX != 22 && Main.tile[i, j].frameX != 44) || Main.tile[i, j].frameY < 132))
														{
															if (num4 == num9 || num5 == num9)
															{
																if (num7 == num9)
																{
																	if (num4 == num9 && num5 == num9)
																	{
																		if (Main.tile[i, j].frameNumber == 0)
																		{
																			Main.tile[i, j].frameX = 132;
																			Main.tile[i, j].frameY = 132;
																		}
																		if (Main.tile[i, j].frameNumber == 1)
																		{
																			Main.tile[i, j].frameX = 132;
																			Main.tile[i, j].frameY = 154;
																		}
																		if (Main.tile[i, j].frameNumber == 2)
																		{
																			Main.tile[i, j].frameX = 132;
																			Main.tile[i, j].frameY = 176;
																		}
																	}
																	else
																	{
																		if (num4 == num9)
																		{
																			if (Main.tile[i, j].frameNumber == 0)
																			{
																				Main.tile[i, j].frameX = 132;
																				Main.tile[i, j].frameY = 0;
																			}
																			if (Main.tile[i, j].frameNumber == 1)
																			{
																				Main.tile[i, j].frameX = 132;
																				Main.tile[i, j].frameY = 22;
																			}
																			if (Main.tile[i, j].frameNumber == 2)
																			{
																				Main.tile[i, j].frameX = 132;
																				Main.tile[i, j].frameY = 44;
																			}
																		}
																		else
																		{
																			if (num5 == num9)
																			{
																				if (Main.tile[i, j].frameNumber == 0)
																				{
																					Main.tile[i, j].frameX = 132;
																					Main.tile[i, j].frameY = 66;
																				}
																				if (Main.tile[i, j].frameNumber == 1)
																				{
																					Main.tile[i, j].frameX = 132;
																					Main.tile[i, j].frameY = 88;
																				}
																				if (Main.tile[i, j].frameNumber == 2)
																				{
																					Main.tile[i, j].frameX = 132;
																					Main.tile[i, j].frameY = 110;
																				}
																			}
																		}
																	}
																}
																else
																{
																	if (num4 == num9 && num5 == num9)
																	{
																		if (Main.tile[i, j].frameNumber == 0)
																		{
																			Main.tile[i, j].frameX = 154;
																			Main.tile[i, j].frameY = 132;
																		}
																		if (Main.tile[i, j].frameNumber == 1)
																		{
																			Main.tile[i, j].frameX = 154;
																			Main.tile[i, j].frameY = 154;
																		}
																		if (Main.tile[i, j].frameNumber == 2)
																		{
																			Main.tile[i, j].frameX = 154;
																			Main.tile[i, j].frameY = 176;
																		}
																	}
																	else
																	{
																		if (num4 == num9)
																		{
																			if (Main.tile[i, j].frameNumber == 0)
																			{
																				Main.tile[i, j].frameX = 154;
																				Main.tile[i, j].frameY = 0;
																			}
																			if (Main.tile[i, j].frameNumber == 1)
																			{
																				Main.tile[i, j].frameX = 154;
																				Main.tile[i, j].frameY = 22;
																			}
																			if (Main.tile[i, j].frameNumber == 2)
																			{
																				Main.tile[i, j].frameX = 154;
																				Main.tile[i, j].frameY = 44;
																			}
																		}
																		else
																		{
																			if (num5 == num9)
																			{
																				if (Main.tile[i, j].frameNumber == 0)
																				{
																					Main.tile[i, j].frameX = 154;
																					Main.tile[i, j].frameY = 66;
																				}
																				if (Main.tile[i, j].frameNumber == 1)
																				{
																					Main.tile[i, j].frameX = 154;
																					Main.tile[i, j].frameY = 88;
																				}
																				if (Main.tile[i, j].frameNumber == 2)
																				{
																					Main.tile[i, j].frameX = 154;
																					Main.tile[i, j].frameY = 110;
																				}
																			}
																		}
																	}
																}
															}
															else
															{
																if (Main.tile[i, j].frameNumber == 0)
																{
																	Main.tile[i, j].frameX = 110;
																	Main.tile[i, j].frameY = 0;
																}
																if (Main.tile[i, j].frameNumber == 1)
																{
																	Main.tile[i, j].frameX = 110;
																	Main.tile[i, j].frameY = 22;
																}
																if (Main.tile[i, j].frameNumber == 2)
																{
																	Main.tile[i, j].frameX = 110;
																	Main.tile[i, j].frameY = 44;
																}
															}
														}
													}
												}
												rectangle.X = (int)Main.tile[i, j].frameX;
												rectangle.Y = (int)Main.tile[i, j].frameY;
											}
										}
									}
								}
							}
						}
					}
					if (Main.tileFrameImportant[(int)Main.tile[i, j].type])
					{
						return;
					}
					int num22 = 0;
					if (resetFrame)
					{
						num22 = WorldGen.genRand.Next(0, 3);
						Main.tile[i, j].frameNumber = (byte)num22;
					}
					else
					{
						num22 = (int)Main.tile[i, j].frameNumber;
					}
					if (num9 == 0)
					{
						if (num2 >= 0 && Main.tileMergeDirt[num2])
						{
							WorldGen.TileFrame(i, j - 1, false, false);
							if (WorldGen.mergeDown)
							{
								num2 = num9;
							}
						}
						if (num7 >= 0 && Main.tileMergeDirt[num7])
						{
							WorldGen.TileFrame(i, j + 1, false, false);
							if (WorldGen.mergeUp)
							{
								num7 = num9;
							}
						}
						if (num4 >= 0 && Main.tileMergeDirt[num4])
						{
							WorldGen.TileFrame(i - 1, j, false, false);
							if (WorldGen.mergeRight)
							{
								num4 = num9;
							}
						}
						if (num5 >= 0 && Main.tileMergeDirt[num5])
						{
							WorldGen.TileFrame(i + 1, j, false, false);
							if (WorldGen.mergeLeft)
							{
								num5 = num9;
							}
						}
						if (num >= 0 && Main.tileMergeDirt[num])
						{
							num = num9;
						}
						if (num3 >= 0 && Main.tileMergeDirt[num3])
						{
							num3 = num9;
						}
						if (num6 >= 0 && Main.tileMergeDirt[num6])
						{
							num6 = num9;
						}
						if (num8 >= 0 && Main.tileMergeDirt[num8])
						{
							num8 = num9;
						}
						if (num2 == 2)
						{
							num2 = num9;
						}
						if (num7 == 2)
						{
							num7 = num9;
						}
						if (num4 == 2)
						{
							num4 = num9;
						}
						if (num5 == 2)
						{
							num5 = num9;
						}
						if (num == 2)
						{
							num = num9;
						}
						if (num3 == 2)
						{
							num3 = num9;
						}
						if (num6 == 2)
						{
							num6 = num9;
						}
						if (num8 == 2)
						{
							num8 = num9;
						}
						if (num2 == 23)
						{
							num2 = num9;
						}
						if (num7 == 23)
						{
							num7 = num9;
						}
						if (num4 == 23)
						{
							num4 = num9;
						}
						if (num5 == 23)
						{
							num5 = num9;
						}
						if (num == 23)
						{
							num = num9;
						}
						if (num3 == 23)
						{
							num3 = num9;
						}
						if (num6 == 23)
						{
							num6 = num9;
						}
						if (num8 == 23)
						{
							num8 = num9;
						}
					}
					else
					{
						if (num9 == 57)
						{
							if (num2 == 58)
							{
								WorldGen.TileFrame(i, j - 1, false, false);
								if (WorldGen.mergeDown)
								{
									num2 = num9;
								}
							}
							if (num7 == 58)
							{
								WorldGen.TileFrame(i, j + 1, false, false);
								if (WorldGen.mergeUp)
								{
									num7 = num9;
								}
							}
							if (num4 == 58)
							{
								WorldGen.TileFrame(i - 1, j, false, false);
								if (WorldGen.mergeRight)
								{
									num4 = num9;
								}
							}
							if (num5 == 58)
							{
								WorldGen.TileFrame(i + 1, j, false, false);
								if (WorldGen.mergeLeft)
								{
									num5 = num9;
								}
							}
							if (num == 58)
							{
								num = num9;
							}
							if (num3 == 58)
							{
								num3 = num9;
							}
							if (num6 == 58)
							{
								num6 = num9;
							}
							if (num8 == 58)
							{
								num8 = num9;
							}
						}
						else
						{
							if (num9 == 59)
							{
								if (num2 == 60)
								{
									num2 = num9;
								}
								if (num7 == 60)
								{
									num7 = num9;
								}
								if (num4 == 60)
								{
									num4 = num9;
								}
								if (num5 == 60)
								{
									num5 = num9;
								}
								if (num == 60)
								{
									num = num9;
								}
								if (num3 == 60)
								{
									num3 = num9;
								}
								if (num6 == 60)
								{
									num6 = num9;
								}
								if (num8 == 60)
								{
									num8 = num9;
								}
								if (num2 == 70)
								{
									num2 = num9;
								}
								if (num7 == 70)
								{
									num7 = num9;
								}
								if (num4 == 70)
								{
									num4 = num9;
								}
								if (num5 == 70)
								{
									num5 = num9;
								}
								if (num == 70)
								{
									num = num9;
								}
								if (num3 == 70)
								{
									num3 = num9;
								}
								if (num6 == 70)
								{
									num6 = num9;
								}
								if (num8 == 70)
								{
									num8 = num9;
								}
							}
						}
					}
					if (Main.tileMergeDirt[num9])
					{
						if (num2 == 0)
						{
							num2 = -2;
						}
						if (num7 == 0)
						{
							num7 = -2;
						}
						if (num4 == 0)
						{
							num4 = -2;
						}
						if (num5 == 0)
						{
							num5 = -2;
						}
						if (num == 0)
						{
							num = -2;
						}
						if (num3 == 0)
						{
							num3 = -2;
						}
						if (num6 == 0)
						{
							num6 = -2;
						}
						if (num8 == 0)
						{
							num8 = -2;
						}
					}
					else
					{
						if (num9 == 58)
						{
							if (num2 == 57)
							{
								num2 = -2;
							}
							if (num7 == 57)
							{
								num7 = -2;
							}
							if (num4 == 57)
							{
								num4 = -2;
							}
							if (num5 == 57)
							{
								num5 = -2;
							}
							if (num == 57)
							{
								num = -2;
							}
							if (num3 == 57)
							{
								num3 = -2;
							}
							if (num6 == 57)
							{
								num6 = -2;
							}
							if (num8 == 57)
							{
								num8 = -2;
							}
						}
						else
						{
							if (num9 == 59)
							{
								if (num2 == 1)
								{
									num2 = -2;
								}
								if (num7 == 1)
								{
									num7 = -2;
								}
								if (num4 == 1)
								{
									num4 = -2;
								}
								if (num5 == 1)
								{
									num5 = -2;
								}
								if (num == 1)
								{
									num = -2;
								}
								if (num3 == 1)
								{
									num3 = -2;
								}
								if (num6 == 1)
								{
									num6 = -2;
								}
								if (num8 == 1)
								{
									num8 = -2;
								}
							}
						}
					}
					if (num9 == 32)
					{
						if (num7 == 23)
						{
							num7 = num9;
						}
					}
					else
					{
						if (num9 == 69)
						{
							if (num7 == 60)
							{
								num7 = num9;
							}
						}
						else
						{
							if (num9 == 51)
							{
								if (num2 > -1 && !Main.tileNoAttach[num2])
								{
									num2 = num9;
								}
								if (num7 > -1 && !Main.tileNoAttach[num7])
								{
									num7 = num9;
								}
								if (num4 > -1 && !Main.tileNoAttach[num4])
								{
									num4 = num9;
								}
								if (num5 > -1 && !Main.tileNoAttach[num5])
								{
									num5 = num9;
								}
								if (num > -1 && !Main.tileNoAttach[num])
								{
									num = num9;
								}
								if (num3 > -1 && !Main.tileNoAttach[num3])
								{
									num3 = num9;
								}
								if (num6 > -1 && !Main.tileNoAttach[num6])
								{
									num6 = num9;
								}
								if (num8 > -1 && !Main.tileNoAttach[num8])
								{
									num8 = num9;
								}
							}
						}
					}
					if (num2 > -1 && !Main.tileSolid[num2] && num2 != num9)
					{
						num2 = -1;
					}
					if (num7 > -1 && !Main.tileSolid[num7] && num7 != num9)
					{
						num7 = -1;
					}
					if (num4 > -1 && !Main.tileSolid[num4] && num4 != num9)
					{
						num4 = -1;
					}
					if (num5 > -1 && !Main.tileSolid[num5] && num5 != num9)
					{
						num5 = -1;
					}
					if (num > -1 && !Main.tileSolid[num] && num != num9)
					{
						num = -1;
					}
					if (num3 > -1 && !Main.tileSolid[num3] && num3 != num9)
					{
						num3 = -1;
					}
					if (num6 > -1 && !Main.tileSolid[num6] && num6 != num9)
					{
						num6 = -1;
					}
					if (num8 > -1 && !Main.tileSolid[num8] && num8 != num9)
					{
						num8 = -1;
					}
					if (num9 == 2 || num9 == 23 || num9 == 60 || num9 == 70)
					{
						int num23 = 0;
						if (num9 == 60 || num9 == 70)
						{
							num23 = 59;
						}
						else
						{
							if (num9 == 2)
							{
								if (num2 == 23)
								{
									num2 = num23;
								}
								if (num7 == 23)
								{
									num7 = num23;
								}
								if (num4 == 23)
								{
									num4 = num23;
								}
								if (num5 == 23)
								{
									num5 = num23;
								}
								if (num == 23)
								{
									num = num23;
								}
								if (num3 == 23)
								{
									num3 = num23;
								}
								if (num6 == 23)
								{
									num6 = num23;
								}
								if (num8 == 23)
								{
									num8 = num23;
								}
							}
							else
							{
								if (num9 == 23)
								{
									if (num2 == 2)
									{
										num2 = num23;
									}
									if (num7 == 2)
									{
										num7 = num23;
									}
									if (num4 == 2)
									{
										num4 = num23;
									}
									if (num5 == 2)
									{
										num5 = num23;
									}
									if (num == 2)
									{
										num = num23;
									}
									if (num3 == 2)
									{
										num3 = num23;
									}
									if (num6 == 2)
									{
										num6 = num23;
									}
									if (num8 == 2)
									{
										num8 = num23;
									}
								}
							}
						}
						if (num2 != num9 && num2 != num23 && (num7 == num9 || num7 == num23))
						{
							if (num4 == num23 && num5 == num9)
							{
								if (num22 == 0)
								{
									rectangle.X = 0;
									rectangle.Y = 198;
								}
								if (num22 == 1)
								{
									rectangle.X = 18;
									rectangle.Y = 198;
								}
								if (num22 == 2)
								{
									rectangle.X = 36;
									rectangle.Y = 198;
								}
							}
							else
							{
								if (num4 == num9 && num5 == num23)
								{
									if (num22 == 0)
									{
										rectangle.X = 54;
										rectangle.Y = 198;
									}
									if (num22 == 1)
									{
										rectangle.X = 72;
										rectangle.Y = 198;
									}
									if (num22 == 2)
									{
										rectangle.X = 90;
										rectangle.Y = 198;
									}
								}
							}
						}
						else
						{
							if (num7 != num9 && num7 != num23 && (num2 == num9 || num2 == num23))
							{
								if (num4 == num23 && num5 == num9)
								{
									if (num22 == 0)
									{
										rectangle.X = 0;
										rectangle.Y = 216;
									}
									if (num22 == 1)
									{
										rectangle.X = 18;
										rectangle.Y = 216;
									}
									if (num22 == 2)
									{
										rectangle.X = 36;
										rectangle.Y = 216;
									}
								}
								else
								{
									if (num4 == num9 && num5 == num23)
									{
										if (num22 == 0)
										{
											rectangle.X = 54;
											rectangle.Y = 216;
										}
										if (num22 == 1)
										{
											rectangle.X = 72;
											rectangle.Y = 216;
										}
										if (num22 == 2)
										{
											rectangle.X = 90;
											rectangle.Y = 216;
										}
									}
								}
							}
							else
							{
								if (num4 != num9 && num4 != num23 && (num5 == num9 || num5 == num23))
								{
									if (num2 == num23 && num7 == num9)
									{
										if (num22 == 0)
										{
											rectangle.X = 72;
											rectangle.Y = 144;
										}
										if (num22 == 1)
										{
											rectangle.X = 72;
											rectangle.Y = 162;
										}
										if (num22 == 2)
										{
											rectangle.X = 72;
											rectangle.Y = 180;
										}
									}
									else
									{
										if (num7 == num9 && num5 == num2)
										{
											if (num22 == 0)
											{
												rectangle.X = 72;
												rectangle.Y = 90;
											}
											if (num22 == 1)
											{
												rectangle.X = 72;
												rectangle.Y = 108;
											}
											if (num22 == 2)
											{
												rectangle.X = 72;
												rectangle.Y = 126;
											}
										}
									}
								}
								else
								{
									if (num5 != num9 && num5 != num23 && (num4 == num9 || num4 == num23))
									{
										if (num2 == num23 && num7 == num9)
										{
											if (num22 == 0)
											{
												rectangle.X = 90;
												rectangle.Y = 144;
											}
											if (num22 == 1)
											{
												rectangle.X = 90;
												rectangle.Y = 162;
											}
											if (num22 == 2)
											{
												rectangle.X = 90;
												rectangle.Y = 180;
											}
										}
										else
										{
											if (num7 == num9 && num5 == num2)
											{
												if (num22 == 0)
												{
													rectangle.X = 90;
													rectangle.Y = 90;
												}
												if (num22 == 1)
												{
													rectangle.X = 90;
													rectangle.Y = 108;
												}
												if (num22 == 2)
												{
													rectangle.X = 90;
													rectangle.Y = 126;
												}
											}
										}
									}
									else
									{
										if (num2 == num9 && num7 == num9 && num4 == num9 && num5 == num9)
										{
											if (num != num9 && num3 != num9 && num6 != num9 && num8 != num9)
											{
												if (num8 == num23)
												{
													if (num22 == 0)
													{
														rectangle.X = 108;
														rectangle.Y = 324;
													}
													if (num22 == 1)
													{
														rectangle.X = 126;
														rectangle.Y = 324;
													}
													if (num22 == 2)
													{
														rectangle.X = 144;
														rectangle.Y = 324;
													}
												}
												else
												{
													if (num3 == num23)
													{
														if (num22 == 0)
														{
															rectangle.X = 108;
															rectangle.Y = 342;
														}
														if (num22 == 1)
														{
															rectangle.X = 126;
															rectangle.Y = 342;
														}
														if (num22 == 2)
														{
															rectangle.X = 144;
															rectangle.Y = 342;
														}
													}
													else
													{
														if (num6 == num23)
														{
															if (num22 == 0)
															{
																rectangle.X = 108;
																rectangle.Y = 360;
															}
															if (num22 == 1)
															{
																rectangle.X = 126;
																rectangle.Y = 360;
															}
															if (num22 == 2)
															{
																rectangle.X = 144;
																rectangle.Y = 360;
															}
														}
														else
														{
															if (num == num23)
															{
																if (num22 == 0)
																{
																	rectangle.X = 108;
																	rectangle.Y = 378;
																}
																if (num22 == 1)
																{
																	rectangle.X = 126;
																	rectangle.Y = 378;
																}
																if (num22 == 2)
																{
																	rectangle.X = 144;
																	rectangle.Y = 378;
																}
															}
															else
															{
																if (num22 == 0)
																{
																	rectangle.X = 144;
																	rectangle.Y = 234;
																}
																if (num22 == 1)
																{
																	rectangle.X = 198;
																	rectangle.Y = 234;
																}
																if (num22 == 2)
																{
																	rectangle.X = 252;
																	rectangle.Y = 234;
																}
															}
														}
													}
												}
											}
											else
											{
												if (num != num9 && num8 != num9)
												{
													if (num22 == 0)
													{
														rectangle.X = 36;
														rectangle.Y = 306;
													}
													if (num22 == 1)
													{
														rectangle.X = 54;
														rectangle.Y = 306;
													}
													if (num22 == 2)
													{
														rectangle.X = 72;
														rectangle.Y = 306;
													}
												}
												else
												{
													if (num3 != num9 && num6 != num9)
													{
														if (num22 == 0)
														{
															rectangle.X = 90;
															rectangle.Y = 306;
														}
														if (num22 == 1)
														{
															rectangle.X = 108;
															rectangle.Y = 306;
														}
														if (num22 == 2)
														{
															rectangle.X = 126;
															rectangle.Y = 306;
														}
													}
													else
													{
														if (num != num9 && num3 == num9 && num6 == num9 && num8 == num9)
														{
															if (num22 == 0)
															{
																rectangle.X = 54;
																rectangle.Y = 108;
															}
															if (num22 == 1)
															{
																rectangle.X = 54;
																rectangle.Y = 144;
															}
															if (num22 == 2)
															{
																rectangle.X = 54;
																rectangle.Y = 180;
															}
														}
														else
														{
															if (num == num9 && num3 != num9 && num6 == num9 && num8 == num9)
															{
																if (num22 == 0)
																{
																	rectangle.X = 36;
																	rectangle.Y = 108;
																}
																if (num22 == 1)
																{
																	rectangle.X = 36;
																	rectangle.Y = 144;
																}
																if (num22 == 2)
																{
																	rectangle.X = 36;
																	rectangle.Y = 180;
																}
															}
															else
															{
																if (num == num9 && num3 == num9 && num6 != num9 && num8 == num9)
																{
																	if (num22 == 0)
																	{
																		rectangle.X = 54;
																		rectangle.Y = 90;
																	}
																	if (num22 == 1)
																	{
																		rectangle.X = 54;
																		rectangle.Y = 126;
																	}
																	if (num22 == 2)
																	{
																		rectangle.X = 54;
																		rectangle.Y = 162;
																	}
																}
																else
																{
																	if (num == num9 && num3 == num9 && num6 == num9 && num8 != num9)
																	{
																		if (num22 == 0)
																		{
																			rectangle.X = 36;
																			rectangle.Y = 90;
																		}
																		if (num22 == 1)
																		{
																			rectangle.X = 36;
																			rectangle.Y = 126;
																		}
																		if (num22 == 2)
																		{
																			rectangle.X = 36;
																			rectangle.Y = 162;
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
										else
										{
											if (num2 == num9 && num7 == num23 && num4 == num9 && num5 == num9 && num == -1 && num3 == -1)
											{
												if (num22 == 0)
												{
													rectangle.X = 108;
													rectangle.Y = 18;
												}
												if (num22 == 1)
												{
													rectangle.X = 126;
													rectangle.Y = 18;
												}
												if (num22 == 2)
												{
													rectangle.X = 144;
													rectangle.Y = 18;
												}
											}
											else
											{
												if (num2 == num23 && num7 == num9 && num4 == num9 && num5 == num9 && num6 == -1 && num8 == -1)
												{
													if (num22 == 0)
													{
														rectangle.X = 108;
														rectangle.Y = 36;
													}
													if (num22 == 1)
													{
														rectangle.X = 126;
														rectangle.Y = 36;
													}
													if (num22 == 2)
													{
														rectangle.X = 144;
														rectangle.Y = 36;
													}
												}
												else
												{
													if (num2 == num9 && num7 == num9 && num4 == num23 && num5 == num9 && num3 == -1 && num8 == -1)
													{
														if (num22 == 0)
														{
															rectangle.X = 198;
															rectangle.Y = 0;
														}
														if (num22 == 1)
														{
															rectangle.X = 198;
															rectangle.Y = 18;
														}
														if (num22 == 2)
														{
															rectangle.X = 198;
															rectangle.Y = 36;
														}
													}
													else
													{
														if (num2 == num9 && num7 == num9 && num4 == num9 && num5 == num23 && num == -1 && num6 == -1)
														{
															if (num22 == 0)
															{
																rectangle.X = 180;
																rectangle.Y = 0;
															}
															if (num22 == 1)
															{
																rectangle.X = 180;
																rectangle.Y = 18;
															}
															if (num22 == 2)
															{
																rectangle.X = 180;
																rectangle.Y = 36;
															}
														}
														else
														{
															if (num2 == num9 && num7 == num23 && num4 == num9 && num5 == num9)
															{
																if (num3 != -1)
																{
																	if (num22 == 0)
																	{
																		rectangle.X = 54;
																		rectangle.Y = 108;
																	}
																	if (num22 == 1)
																	{
																		rectangle.X = 54;
																		rectangle.Y = 144;
																	}
																	if (num22 == 2)
																	{
																		rectangle.X = 54;
																		rectangle.Y = 180;
																	}
																}
																else
																{
																	if (num != -1)
																	{
																		if (num22 == 0)
																		{
																			rectangle.X = 36;
																			rectangle.Y = 108;
																		}
																		if (num22 == 1)
																		{
																			rectangle.X = 36;
																			rectangle.Y = 144;
																		}
																		if (num22 == 2)
																		{
																			rectangle.X = 36;
																			rectangle.Y = 180;
																		}
																	}
																}
															}
															else
															{
																if (num2 == num23 && num7 == num9 && num4 == num9 && num5 == num9)
																{
																	if (num8 != -1)
																	{
																		if (num22 == 0)
																		{
																			rectangle.X = 54;
																			rectangle.Y = 90;
																		}
																		if (num22 == 1)
																		{
																			rectangle.X = 54;
																			rectangle.Y = 126;
																		}
																		if (num22 == 2)
																		{
																			rectangle.X = 54;
																			rectangle.Y = 162;
																		}
																	}
																	else
																	{
																		if (num6 != -1)
																		{
																			if (num22 == 0)
																			{
																				rectangle.X = 36;
																				rectangle.Y = 90;
																			}
																			if (num22 == 1)
																			{
																				rectangle.X = 36;
																				rectangle.Y = 126;
																			}
																			if (num22 == 2)
																			{
																				rectangle.X = 36;
																				rectangle.Y = 162;
																			}
																		}
																	}
																}
																else
																{
																	if (num2 == num9 && num7 == num9 && num4 == num9 && num5 == num23)
																	{
																		if (num != -1)
																		{
																			if (num22 == 0)
																			{
																				rectangle.X = 54;
																				rectangle.Y = 90;
																			}
																			if (num22 == 1)
																			{
																				rectangle.X = 54;
																				rectangle.Y = 126;
																			}
																			if (num22 == 2)
																			{
																				rectangle.X = 54;
																				rectangle.Y = 162;
																			}
																		}
																		else
																		{
																			if (num6 != -1)
																			{
																				if (num22 == 0)
																				{
																					rectangle.X = 54;
																					rectangle.Y = 108;
																				}
																				if (num22 == 1)
																				{
																					rectangle.X = 54;
																					rectangle.Y = 144;
																				}
																				if (num22 == 2)
																				{
																					rectangle.X = 54;
																					rectangle.Y = 180;
																				}
																			}
																		}
																	}
																	else
																	{
																		if (num2 == num9 && num7 == num9 && num4 == num23 && num5 == num9)
																		{
																			if (num3 != -1)
																			{
																				if (num22 == 0)
																				{
																					rectangle.X = 36;
																					rectangle.Y = 90;
																				}
																				if (num22 == 1)
																				{
																					rectangle.X = 36;
																					rectangle.Y = 126;
																				}
																				if (num22 == 2)
																				{
																					rectangle.X = 36;
																					rectangle.Y = 162;
																				}
																			}
																			else
																			{
																				if (num8 != -1)
																				{
																					if (num22 == 0)
																					{
																						rectangle.X = 36;
																						rectangle.Y = 108;
																					}
																					if (num22 == 1)
																					{
																						rectangle.X = 36;
																						rectangle.Y = 144;
																					}
																					if (num22 == 2)
																					{
																						rectangle.X = 36;
																						rectangle.Y = 180;
																					}
																				}
																			}
																		}
																		else
																		{
																			if ((num2 == num23 && num7 == num9 && num4 == num9 && num5 == num9) || (num2 == num9 && num7 == num23 && num4 == num9 && num5 == num9) || (num2 == num9 && num7 == num9 && num4 == num23 && num5 == num9) || (num2 == num9 && num7 == num9 && num4 == num9 && num5 == num23))
																			{
																				if (num22 == 0)
																				{
																					rectangle.X = 18;
																					rectangle.Y = 18;
																				}
																				if (num22 == 1)
																				{
																					rectangle.X = 36;
																					rectangle.Y = 18;
																				}
																				if (num22 == 2)
																				{
																					rectangle.X = 54;
																					rectangle.Y = 18;
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
						if ((num2 == num9 || num2 == num23) && (num7 == num9 || num7 == num23) && (num4 == num9 || num4 == num23) && (num5 == num9 || num5 == num23))
						{
							if (num != num9 && num != num23 && (num3 == num9 || num3 == num23) && (num6 == num9 || num6 == num23) && (num8 == num9 || num8 == num23))
							{
								if (num22 == 0)
								{
									rectangle.X = 54;
									rectangle.Y = 108;
								}
								if (num22 == 1)
								{
									rectangle.X = 54;
									rectangle.Y = 144;
								}
								if (num22 == 2)
								{
									rectangle.X = 54;
									rectangle.Y = 180;
								}
							}
							else
							{
								if (num3 != num9 && num3 != num23 && (num == num9 || num == num23) && (num6 == num9 || num6 == num23) && (num8 == num9 || num8 == num23))
								{
									if (num22 == 0)
									{
										rectangle.X = 36;
										rectangle.Y = 108;
									}
									if (num22 == 1)
									{
										rectangle.X = 36;
										rectangle.Y = 144;
									}
									if (num22 == 2)
									{
										rectangle.X = 36;
										rectangle.Y = 180;
									}
								}
								else
								{
									if (num6 != num9 && num6 != num23 && (num == num9 || num == num23) && (num3 == num9 || num3 == num23) && (num8 == num9 || num8 == num23))
									{
										if (num22 == 0)
										{
											rectangle.X = 54;
											rectangle.Y = 90;
										}
										if (num22 == 1)
										{
											rectangle.X = 54;
											rectangle.Y = 126;
										}
										if (num22 == 2)
										{
											rectangle.X = 54;
											rectangle.Y = 162;
										}
									}
									else
									{
										if (num8 != num9 && num8 != num23 && (num == num9 || num == num23) && (num6 == num9 || num6 == num23) && (num3 == num9 || num3 == num23))
										{
											if (num22 == 0)
											{
												rectangle.X = 36;
												rectangle.Y = 90;
											}
											if (num22 == 1)
											{
												rectangle.X = 36;
												rectangle.Y = 126;
											}
											if (num22 == 2)
											{
												rectangle.X = 36;
												rectangle.Y = 162;
											}
										}
									}
								}
							}
						}
						if (num2 != num23 && num2 != num9 && num7 == num9 && num4 != num23 && num4 != num9 && num5 == num9 && num8 != num23 && num8 != num9)
						{
							if (num22 == 0)
							{
								rectangle.X = 90;
								rectangle.Y = 270;
							}
							if (num22 == 1)
							{
								rectangle.X = 108;
								rectangle.Y = 270;
							}
							if (num22 == 2)
							{
								rectangle.X = 126;
								rectangle.Y = 270;
							}
						}
						else
						{
							if (num2 != num23 && num2 != num9 && num7 == num9 && num4 == num9 && num5 != num23 && num5 != num9 && num6 != num23 && num6 != num9)
							{
								if (num22 == 0)
								{
									rectangle.X = 144;
									rectangle.Y = 270;
								}
								if (num22 == 1)
								{
									rectangle.X = 162;
									rectangle.Y = 270;
								}
								if (num22 == 2)
								{
									rectangle.X = 180;
									rectangle.Y = 270;
								}
							}
							else
							{
								if (num7 != num23 && num7 != num9 && num2 == num9 && num4 != num23 && num4 != num9 && num5 == num9 && num3 != num23 && num3 != num9)
								{
									if (num22 == 0)
									{
										rectangle.X = 90;
										rectangle.Y = 288;
									}
									if (num22 == 1)
									{
										rectangle.X = 108;
										rectangle.Y = 288;
									}
									if (num22 == 2)
									{
										rectangle.X = 126;
										rectangle.Y = 288;
									}
								}
								else
								{
									if (num7 != num23 && num7 != num9 && num2 == num9 && num4 == num9 && num5 != num23 && num5 != num9 && num != num23 && num != num9)
									{
										if (num22 == 0)
										{
											rectangle.X = 144;
											rectangle.Y = 288;
										}
										if (num22 == 1)
										{
											rectangle.X = 162;
											rectangle.Y = 288;
										}
										if (num22 == 2)
										{
											rectangle.X = 180;
											rectangle.Y = 288;
										}
									}
									else
									{
										if (num2 != num9 && num2 != num23 && num7 == num9 && num4 == num9 && num5 == num9 && num6 != num9 && num6 != num23 && num8 != num9 && num8 != num23)
										{
											if (num22 == 0)
											{
												rectangle.X = 144;
												rectangle.Y = 216;
											}
											if (num22 == 1)
											{
												rectangle.X = 198;
												rectangle.Y = 216;
											}
											if (num22 == 2)
											{
												rectangle.X = 252;
												rectangle.Y = 216;
											}
										}
										else
										{
											if (num7 != num9 && num7 != num23 && num2 == num9 && num4 == num9 && num5 == num9 && num != num9 && num != num23 && num3 != num9 && num3 != num23)
											{
												if (num22 == 0)
												{
													rectangle.X = 144;
													rectangle.Y = 252;
												}
												if (num22 == 1)
												{
													rectangle.X = 198;
													rectangle.Y = 252;
												}
												if (num22 == 2)
												{
													rectangle.X = 252;
													rectangle.Y = 252;
												}
											}
											else
											{
												if (num4 != num9 && num4 != num23 && num7 == num9 && num2 == num9 && num5 == num9 && num3 != num9 && num3 != num23 && num8 != num9 && num8 != num23)
												{
													if (num22 == 0)
													{
														rectangle.X = 126;
														rectangle.Y = 234;
													}
													if (num22 == 1)
													{
														rectangle.X = 180;
														rectangle.Y = 234;
													}
													if (num22 == 2)
													{
														rectangle.X = 234;
														rectangle.Y = 234;
													}
												}
												else
												{
													if (num5 != num9 && num5 != num23 && num7 == num9 && num2 == num9 && num4 == num9 && num != num9 && num != num23 && num6 != num9 && num6 != num23)
													{
														if (num22 == 0)
														{
															rectangle.X = 162;
															rectangle.Y = 234;
														}
														if (num22 == 1)
														{
															rectangle.X = 216;
															rectangle.Y = 234;
														}
														if (num22 == 2)
														{
															rectangle.X = 270;
															rectangle.Y = 234;
														}
													}
													else
													{
														if (num2 != num23 && num2 != num9 && (num7 == num23 || num7 == num9) && num4 == num23 && num5 == num23)
														{
															if (num22 == 0)
															{
																rectangle.X = 36;
																rectangle.Y = 270;
															}
															if (num22 == 1)
															{
																rectangle.X = 54;
																rectangle.Y = 270;
															}
															if (num22 == 2)
															{
																rectangle.X = 72;
																rectangle.Y = 270;
															}
														}
														else
														{
															if (num7 != num23 && num7 != num9 && (num2 == num23 || num2 == num9) && num4 == num23 && num5 == num23)
															{
																if (num22 == 0)
																{
																	rectangle.X = 36;
																	rectangle.Y = 288;
																}
																if (num22 == 1)
																{
																	rectangle.X = 54;
																	rectangle.Y = 288;
																}
																if (num22 == 2)
																{
																	rectangle.X = 72;
																	rectangle.Y = 288;
																}
															}
															else
															{
																if (num4 != num23 && num4 != num9 && (num5 == num23 || num5 == num9) && num2 == num23 && num7 == num23)
																{
																	if (num22 == 0)
																	{
																		rectangle.X = 0;
																		rectangle.Y = 270;
																	}
																	if (num22 == 1)
																	{
																		rectangle.X = 0;
																		rectangle.Y = 288;
																	}
																	if (num22 == 2)
																	{
																		rectangle.X = 0;
																		rectangle.Y = 306;
																	}
																}
																else
																{
																	if (num5 != num23 && num5 != num9 && (num4 == num23 || num4 == num9) && num2 == num23 && num7 == num23)
																	{
																		if (num22 == 0)
																		{
																			rectangle.X = 18;
																			rectangle.Y = 270;
																		}
																		if (num22 == 1)
																		{
																			rectangle.X = 18;
																			rectangle.Y = 288;
																		}
																		if (num22 == 2)
																		{
																			rectangle.X = 18;
																			rectangle.Y = 306;
																		}
																	}
																	else
																	{
																		if (num2 == num9 && num7 == num23 && num4 == num23 && num5 == num23)
																		{
																			if (num22 == 0)
																			{
																				rectangle.X = 198;
																				rectangle.Y = 288;
																			}
																			if (num22 == 1)
																			{
																				rectangle.X = 216;
																				rectangle.Y = 288;
																			}
																			if (num22 == 2)
																			{
																				rectangle.X = 234;
																				rectangle.Y = 288;
																			}
																		}
																		else
																		{
																			if (num2 == num23 && num7 == num9 && num4 == num23 && num5 == num23)
																			{
																				if (num22 == 0)
																				{
																					rectangle.X = 198;
																					rectangle.Y = 270;
																				}
																				if (num22 == 1)
																				{
																					rectangle.X = 216;
																					rectangle.Y = 270;
																				}
																				if (num22 == 2)
																				{
																					rectangle.X = 234;
																					rectangle.Y = 270;
																				}
																			}
																			else
																			{
																				if (num2 == num23 && num7 == num23 && num4 == num9 && num5 == num23)
																				{
																					if (num22 == 0)
																					{
																						rectangle.X = 198;
																						rectangle.Y = 306;
																					}
																					if (num22 == 1)
																					{
																						rectangle.X = 216;
																						rectangle.Y = 306;
																					}
																					if (num22 == 2)
																					{
																						rectangle.X = 234;
																						rectangle.Y = 306;
																					}
																				}
																				else
																				{
																					if (num2 == num23 && num7 == num23 && num4 == num23 && num5 == num9)
																					{
																						if (num22 == 0)
																						{
																							rectangle.X = 144;
																							rectangle.Y = 306;
																						}
																						if (num22 == 1)
																						{
																							rectangle.X = 162;
																							rectangle.Y = 306;
																						}
																						if (num22 == 2)
																						{
																							rectangle.X = 180;
																							rectangle.Y = 306;
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
						if (num2 != num9 && num2 != num23 && num7 == num9 && num4 == num9 && num5 == num9)
						{
							if ((num6 == num23 || num6 == num9) && num8 != num23 && num8 != num9)
							{
								if (num22 == 0)
								{
									rectangle.X = 0;
									rectangle.Y = 324;
								}
								if (num22 == 1)
								{
									rectangle.X = 18;
									rectangle.Y = 324;
								}
								if (num22 == 2)
								{
									rectangle.X = 36;
									rectangle.Y = 324;
								}
							}
							else
							{
								if ((num8 == num23 || num8 == num9) && num6 != num23 && num6 != num9)
								{
									if (num22 == 0)
									{
										rectangle.X = 54;
										rectangle.Y = 324;
									}
									if (num22 == 1)
									{
										rectangle.X = 72;
										rectangle.Y = 324;
									}
									if (num22 == 2)
									{
										rectangle.X = 90;
										rectangle.Y = 324;
									}
								}
							}
						}
						else
						{
							if (num7 != num9 && num7 != num23 && num2 == num9 && num4 == num9 && num5 == num9)
							{
								if ((num == num23 || num == num9) && num3 != num23 && num3 != num9)
								{
									if (num22 == 0)
									{
										rectangle.X = 0;
										rectangle.Y = 342;
									}
									if (num22 == 1)
									{
										rectangle.X = 18;
										rectangle.Y = 342;
									}
									if (num22 == 2)
									{
										rectangle.X = 36;
										rectangle.Y = 342;
									}
								}
								else
								{
									if ((num3 == num23 || num3 == num9) && num != num23 && num != num9)
									{
										if (num22 == 0)
										{
											rectangle.X = 54;
											rectangle.Y = 342;
										}
										if (num22 == 1)
										{
											rectangle.X = 72;
											rectangle.Y = 342;
										}
										if (num22 == 2)
										{
											rectangle.X = 90;
											rectangle.Y = 342;
										}
									}
								}
							}
							else
							{
								if (num4 != num9 && num4 != num23 && num2 == num9 && num7 == num9 && num5 == num9)
								{
									if ((num3 == num23 || num3 == num9) && num8 != num23 && num8 != num9)
									{
										if (num22 == 0)
										{
											rectangle.X = 54;
											rectangle.Y = 360;
										}
										if (num22 == 1)
										{
											rectangle.X = 72;
											rectangle.Y = 360;
										}
										if (num22 == 2)
										{
											rectangle.X = 90;
											rectangle.Y = 360;
										}
									}
									else
									{
										if ((num8 == num23 || num8 == num9) && num3 != num23 && num3 != num9)
										{
											if (num22 == 0)
											{
												rectangle.X = 0;
												rectangle.Y = 360;
											}
											if (num22 == 1)
											{
												rectangle.X = 18;
												rectangle.Y = 360;
											}
											if (num22 == 2)
											{
												rectangle.X = 36;
												rectangle.Y = 360;
											}
										}
									}
								}
								else
								{
									if (num5 != num9 && num5 != num23 && num2 == num9 && num7 == num9 && num4 == num9)
									{
										if ((num == num23 || num == num9) && num6 != num23 && num6 != num9)
										{
											if (num22 == 0)
											{
												rectangle.X = 0;
												rectangle.Y = 378;
											}
											if (num22 == 1)
											{
												rectangle.X = 18;
												rectangle.Y = 378;
											}
											if (num22 == 2)
											{
												rectangle.X = 36;
												rectangle.Y = 378;
											}
										}
										else
										{
											if ((num6 == num23 || num6 == num9) && num != num23 && num != num9)
											{
												if (num22 == 0)
												{
													rectangle.X = 54;
													rectangle.Y = 378;
												}
												if (num22 == 1)
												{
													rectangle.X = 72;
													rectangle.Y = 378;
												}
												if (num22 == 2)
												{
													rectangle.X = 90;
													rectangle.Y = 378;
												}
											}
										}
									}
								}
							}
						}
						if ((num2 == num9 || num2 == num23) && (num7 == num9 || num7 == num23) && (num4 == num9 || num4 == num23) && (num5 == num9 || num5 == num23) && num != -1 && num3 != -1 && num6 != -1 && num8 != -1)
						{
							if (num22 == 0)
							{
								rectangle.X = 18;
								rectangle.Y = 18;
							}
							if (num22 == 1)
							{
								rectangle.X = 36;
								rectangle.Y = 18;
							}
							if (num22 == 2)
							{
								rectangle.X = 54;
								rectangle.Y = 18;
							}
						}
						if (num2 == num23)
						{
							num2 = -2;
						}
						if (num7 == num23)
						{
							num7 = -2;
						}
						if (num4 == num23)
						{
							num4 = -2;
						}
						if (num5 == num23)
						{
							num5 = -2;
						}
						if (num == num23)
						{
							num = -2;
						}
						if (num3 == num23)
						{
							num3 = -2;
						}
						if (num6 == num23)
						{
							num6 = -2;
						}
						if (num8 == num23)
						{
							num8 = -2;
						}
					}
					if ((num9 == 1 || num9 == 2 || num9 == 6 || num9 == 7 || num9 == 8 || num9 == 9 || num9 == 22 || num9 == 23 || num9 == 25 || num9 == 37 || num9 == 40 || num9 == 53 || num9 == 56 || num9 == 58 || num9 == 59 || num9 == 60 || num9 == 70) && rectangle.X == -1 && rectangle.Y == -1)
					{
						if (num2 >= 0 && num2 != num9)
						{
							num2 = -1;
						}
						if (num7 >= 0 && num7 != num9)
						{
							num7 = -1;
						}
						if (num4 >= 0 && num4 != num9)
						{
							num4 = -1;
						}
						if (num5 >= 0 && num5 != num9)
						{
							num5 = -1;
						}
						if (num2 != -1 && num7 != -1 && num4 != -1 && num5 != -1)
						{
							if (num2 == -2 && num7 == num9 && num4 == num9 && num5 == num9)
							{
								if (num22 == 0)
								{
									rectangle.X = 144;
									rectangle.Y = 108;
								}
								if (num22 == 1)
								{
									rectangle.X = 162;
									rectangle.Y = 108;
								}
								if (num22 == 2)
								{
									rectangle.X = 180;
									rectangle.Y = 108;
								}
								WorldGen.mergeUp = true;
							}
							else
							{
								if (num2 == num9 && num7 == -2 && num4 == num9 && num5 == num9)
								{
									if (num22 == 0)
									{
										rectangle.X = 144;
										rectangle.Y = 90;
									}
									if (num22 == 1)
									{
										rectangle.X = 162;
										rectangle.Y = 90;
									}
									if (num22 == 2)
									{
										rectangle.X = 180;
										rectangle.Y = 90;
									}
									WorldGen.mergeDown = true;
								}
								else
								{
									if (num2 == num9 && num7 == num9 && num4 == -2 && num5 == num9)
									{
										if (num22 == 0)
										{
											rectangle.X = 162;
											rectangle.Y = 126;
										}
										if (num22 == 1)
										{
											rectangle.X = 162;
											rectangle.Y = 144;
										}
										if (num22 == 2)
										{
											rectangle.X = 162;
											rectangle.Y = 162;
										}
										WorldGen.mergeLeft = true;
									}
									else
									{
										if (num2 == num9 && num7 == num9 && num4 == num9 && num5 == -2)
										{
											if (num22 == 0)
											{
												rectangle.X = 144;
												rectangle.Y = 126;
											}
											if (num22 == 1)
											{
												rectangle.X = 144;
												rectangle.Y = 144;
											}
											if (num22 == 2)
											{
												rectangle.X = 144;
												rectangle.Y = 162;
											}
											WorldGen.mergeRight = true;
										}
										else
										{
											if (num2 == -2 && num7 == num9 && num4 == -2 && num5 == num9)
											{
												if (num22 == 0)
												{
													rectangle.X = 36;
													rectangle.Y = 90;
												}
												if (num22 == 1)
												{
													rectangle.X = 36;
													rectangle.Y = 126;
												}
												if (num22 == 2)
												{
													rectangle.X = 36;
													rectangle.Y = 162;
												}
												WorldGen.mergeUp = true;
												WorldGen.mergeLeft = true;
											}
											else
											{
												if (num2 == -2 && num7 == num9 && num4 == num9 && num5 == -2)
												{
													if (num22 == 0)
													{
														rectangle.X = 54;
														rectangle.Y = 90;
													}
													if (num22 == 1)
													{
														rectangle.X = 54;
														rectangle.Y = 126;
													}
													if (num22 == 2)
													{
														rectangle.X = 54;
														rectangle.Y = 162;
													}
													WorldGen.mergeUp = true;
													WorldGen.mergeRight = true;
												}
												else
												{
													if (num2 == num9 && num7 == -2 && num4 == -2 && num5 == num9)
													{
														if (num22 == 0)
														{
															rectangle.X = 36;
															rectangle.Y = 108;
														}
														if (num22 == 1)
														{
															rectangle.X = 36;
															rectangle.Y = 144;
														}
														if (num22 == 2)
														{
															rectangle.X = 36;
															rectangle.Y = 180;
														}
														WorldGen.mergeDown = true;
														WorldGen.mergeLeft = true;
													}
													else
													{
														if (num2 == num9 && num7 == -2 && num4 == num9 && num5 == -2)
														{
															if (num22 == 0)
															{
																rectangle.X = 54;
																rectangle.Y = 108;
															}
															if (num22 == 1)
															{
																rectangle.X = 54;
																rectangle.Y = 144;
															}
															if (num22 == 2)
															{
																rectangle.X = 54;
																rectangle.Y = 180;
															}
															WorldGen.mergeDown = true;
															WorldGen.mergeRight = true;
														}
														else
														{
															if (num2 == num9 && num7 == num9 && num4 == -2 && num5 == -2)
															{
																if (num22 == 0)
																{
																	rectangle.X = 180;
																	rectangle.Y = 126;
																}
																if (num22 == 1)
																{
																	rectangle.X = 180;
																	rectangle.Y = 144;
																}
																if (num22 == 2)
																{
																	rectangle.X = 180;
																	rectangle.Y = 162;
																}
																WorldGen.mergeLeft = true;
																WorldGen.mergeRight = true;
															}
															else
															{
																if (num2 == -2 && num7 == -2 && num4 == num9 && num5 == num9)
																{
																	if (num22 == 0)
																	{
																		rectangle.X = 144;
																		rectangle.Y = 180;
																	}
																	if (num22 == 1)
																	{
																		rectangle.X = 162;
																		rectangle.Y = 180;
																	}
																	if (num22 == 2)
																	{
																		rectangle.X = 180;
																		rectangle.Y = 180;
																	}
																	WorldGen.mergeUp = true;
																	WorldGen.mergeDown = true;
																}
																else
																{
																	if (num2 == -2 && num7 == num9 && num4 == -2 && num5 == -2)
																	{
																		if (num22 == 0)
																		{
																			rectangle.X = 198;
																			rectangle.Y = 90;
																		}
																		if (num22 == 1)
																		{
																			rectangle.X = 198;
																			rectangle.Y = 108;
																		}
																		if (num22 == 2)
																		{
																			rectangle.X = 198;
																			rectangle.Y = 126;
																		}
																		WorldGen.mergeUp = true;
																		WorldGen.mergeLeft = true;
																		WorldGen.mergeRight = true;
																	}
																	else
																	{
																		if (num2 == num9 && num7 == -2 && num4 == -2 && num5 == -2)
																		{
																			if (num22 == 0)
																			{
																				rectangle.X = 198;
																				rectangle.Y = 144;
																			}
																			if (num22 == 1)
																			{
																				rectangle.X = 198;
																				rectangle.Y = 162;
																			}
																			if (num22 == 2)
																			{
																				rectangle.X = 198;
																				rectangle.Y = 180;
																			}
																			WorldGen.mergeDown = true;
																			WorldGen.mergeLeft = true;
																			WorldGen.mergeRight = true;
																		}
																		else
																		{
																			if (num2 == -2 && num7 == -2 && num4 == num9 && num5 == -2)
																			{
																				if (num22 == 0)
																				{
																					rectangle.X = 216;
																					rectangle.Y = 144;
																				}
																				if (num22 == 1)
																				{
																					rectangle.X = 216;
																					rectangle.Y = 162;
																				}
																				if (num22 == 2)
																				{
																					rectangle.X = 216;
																					rectangle.Y = 180;
																				}
																				WorldGen.mergeUp = true;
																				WorldGen.mergeDown = true;
																				WorldGen.mergeRight = true;
																			}
																			else
																			{
																				if (num2 == -2 && num7 == -2 && num4 == -2 && num5 == num9)
																				{
																					if (num22 == 0)
																					{
																						rectangle.X = 216;
																						rectangle.Y = 90;
																					}
																					if (num22 == 1)
																					{
																						rectangle.X = 216;
																						rectangle.Y = 108;
																					}
																					if (num22 == 2)
																					{
																						rectangle.X = 216;
																						rectangle.Y = 126;
																					}
																					WorldGen.mergeUp = true;
																					WorldGen.mergeDown = true;
																					WorldGen.mergeLeft = true;
																				}
																				else
																				{
																					if (num2 == -2 && num7 == -2 && num4 == -2 && num5 == -2)
																					{
																						if (num22 == 0)
																						{
																							rectangle.X = 108;
																							rectangle.Y = 198;
																						}
																						if (num22 == 1)
																						{
																							rectangle.X = 126;
																							rectangle.Y = 198;
																						}
																						if (num22 == 2)
																						{
																							rectangle.X = 144;
																							rectangle.Y = 198;
																						}
																						WorldGen.mergeUp = true;
																						WorldGen.mergeDown = true;
																						WorldGen.mergeLeft = true;
																						WorldGen.mergeRight = true;
																					}
																					else
																					{
																						if (num2 == num9 && num7 == num9 && num4 == num9 && num5 == num9)
																						{
																							if (num == -2)
																							{
																								if (num22 == 0)
																								{
																									rectangle.X = 18;
																									rectangle.Y = 108;
																								}
																								if (num22 == 1)
																								{
																									rectangle.X = 18;
																									rectangle.Y = 144;
																								}
																								if (num22 == 2)
																								{
																									rectangle.X = 18;
																									rectangle.Y = 180;
																								}
																							}
																							if (num3 == -2)
																							{
																								if (num22 == 0)
																								{
																									rectangle.X = 0;
																									rectangle.Y = 108;
																								}
																								if (num22 == 1)
																								{
																									rectangle.X = 0;
																									rectangle.Y = 144;
																								}
																								if (num22 == 2)
																								{
																									rectangle.X = 0;
																									rectangle.Y = 180;
																								}
																							}
																							if (num6 == -2)
																							{
																								if (num22 == 0)
																								{
																									rectangle.X = 18;
																									rectangle.Y = 90;
																								}
																								if (num22 == 1)
																								{
																									rectangle.X = 18;
																									rectangle.Y = 126;
																								}
																								if (num22 == 2)
																								{
																									rectangle.X = 18;
																									rectangle.Y = 162;
																								}
																							}
																							if (num8 == -2)
																							{
																								if (num22 == 0)
																								{
																									rectangle.X = 0;
																									rectangle.Y = 90;
																								}
																								if (num22 == 1)
																								{
																									rectangle.X = 0;
																									rectangle.Y = 126;
																								}
																								if (num22 == 2)
																								{
																									rectangle.X = 0;
																									rectangle.Y = 162;
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
						else
						{
							if (num9 != 2 && num9 != 23 && num9 != 60 && num9 != 70)
							{
								if (num2 == -1 && num7 == -2 && num4 == num9 && num5 == num9)
								{
									if (num22 == 0)
									{
										rectangle.X = 234;
										rectangle.Y = 0;
									}
									if (num22 == 1)
									{
										rectangle.X = 252;
										rectangle.Y = 0;
									}
									if (num22 == 2)
									{
										rectangle.X = 270;
										rectangle.Y = 0;
									}
									WorldGen.mergeDown = true;
								}
								else
								{
									if (num2 == -2 && num7 == -1 && num4 == num9 && num5 == num9)
									{
										if (num22 == 0)
										{
											rectangle.X = 234;
											rectangle.Y = 18;
										}
										if (num22 == 1)
										{
											rectangle.X = 252;
											rectangle.Y = 18;
										}
										if (num22 == 2)
										{
											rectangle.X = 270;
											rectangle.Y = 18;
										}
										WorldGen.mergeUp = true;
									}
									else
									{
										if (num2 == num9 && num7 == num9 && num4 == -1 && num5 == -2)
										{
											if (num22 == 0)
											{
												rectangle.X = 234;
												rectangle.Y = 36;
											}
											if (num22 == 1)
											{
												rectangle.X = 252;
												rectangle.Y = 36;
											}
											if (num22 == 2)
											{
												rectangle.X = 270;
												rectangle.Y = 36;
											}
											WorldGen.mergeRight = true;
										}
										else
										{
											if (num2 == num9 && num7 == num9 && num4 == -2 && num5 == -1)
											{
												if (num22 == 0)
												{
													rectangle.X = 234;
													rectangle.Y = 54;
												}
												if (num22 == 1)
												{
													rectangle.X = 252;
													rectangle.Y = 54;
												}
												if (num22 == 2)
												{
													rectangle.X = 270;
													rectangle.Y = 54;
												}
												WorldGen.mergeLeft = true;
											}
										}
									}
								}
							}
							if (num2 != -1 && num7 != -1 && num4 == -1 && num5 == num9)
							{
								if (num2 == -2 && num7 == num9)
								{
									if (num22 == 0)
									{
										rectangle.X = 72;
										rectangle.Y = 144;
									}
									if (num22 == 1)
									{
										rectangle.X = 72;
										rectangle.Y = 162;
									}
									if (num22 == 2)
									{
										rectangle.X = 72;
										rectangle.Y = 180;
									}
									WorldGen.mergeUp = true;
								}
								else
								{
									if (num7 == -2 && num2 == num9)
									{
										if (num22 == 0)
										{
											rectangle.X = 72;
											rectangle.Y = 90;
										}
										if (num22 == 1)
										{
											rectangle.X = 72;
											rectangle.Y = 108;
										}
										if (num22 == 2)
										{
											rectangle.X = 72;
											rectangle.Y = 126;
										}
										WorldGen.mergeDown = true;
									}
								}
							}
							else
							{
								if (num2 != -1 && num7 != -1 && num4 == num9 && num5 == -1)
								{
									if (num2 == -2 && num7 == num9)
									{
										if (num22 == 0)
										{
											rectangle.X = 90;
											rectangle.Y = 144;
										}
										if (num22 == 1)
										{
											rectangle.X = 90;
											rectangle.Y = 162;
										}
										if (num22 == 2)
										{
											rectangle.X = 90;
											rectangle.Y = 180;
										}
										WorldGen.mergeUp = true;
									}
									else
									{
										if (num7 == -2 && num2 == num9)
										{
											if (num22 == 0)
											{
												rectangle.X = 90;
												rectangle.Y = 90;
											}
											if (num22 == 1)
											{
												rectangle.X = 90;
												rectangle.Y = 108;
											}
											if (num22 == 2)
											{
												rectangle.X = 90;
												rectangle.Y = 126;
											}
											WorldGen.mergeDown = true;
										}
									}
								}
								else
								{
									if (num2 == -1 && num7 == num9 && num4 != -1 && num5 != -1)
									{
										if (num4 == -2 && num5 == num9)
										{
											if (num22 == 0)
											{
												rectangle.X = 0;
												rectangle.Y = 198;
											}
											if (num22 == 1)
											{
												rectangle.X = 18;
												rectangle.Y = 198;
											}
											if (num22 == 2)
											{
												rectangle.X = 36;
												rectangle.Y = 198;
											}
											WorldGen.mergeLeft = true;
										}
										else
										{
											if (num5 == -2 && num4 == num9)
											{
												if (num22 == 0)
												{
													rectangle.X = 54;
													rectangle.Y = 198;
												}
												if (num22 == 1)
												{
													rectangle.X = 72;
													rectangle.Y = 198;
												}
												if (num22 == 2)
												{
													rectangle.X = 90;
													rectangle.Y = 198;
												}
												WorldGen.mergeRight = true;
											}
										}
									}
									else
									{
										if (num2 == num9 && num7 == -1 && num4 != -1 && num5 != -1)
										{
											if (num4 == -2 && num5 == num9)
											{
												if (num22 == 0)
												{
													rectangle.X = 0;
													rectangle.Y = 216;
												}
												if (num22 == 1)
												{
													rectangle.X = 18;
													rectangle.Y = 216;
												}
												if (num22 == 2)
												{
													rectangle.X = 36;
													rectangle.Y = 216;
												}
												WorldGen.mergeLeft = true;
											}
											else
											{
												if (num5 == -2 && num4 == num9)
												{
													if (num22 == 0)
													{
														rectangle.X = 54;
														rectangle.Y = 216;
													}
													if (num22 == 1)
													{
														rectangle.X = 72;
														rectangle.Y = 216;
													}
													if (num22 == 2)
													{
														rectangle.X = 90;
														rectangle.Y = 216;
													}
													WorldGen.mergeRight = true;
												}
											}
										}
										else
										{
											if (num2 != -1 && num7 != -1 && num4 == -1 && num5 == -1)
											{
												if (num2 == -2 && num7 == -2)
												{
													if (num22 == 0)
													{
														rectangle.X = 108;
														rectangle.Y = 216;
													}
													if (num22 == 1)
													{
														rectangle.X = 108;
														rectangle.Y = 234;
													}
													if (num22 == 2)
													{
														rectangle.X = 108;
														rectangle.Y = 252;
													}
													WorldGen.mergeUp = true;
													WorldGen.mergeDown = true;
												}
												else
												{
													if (num2 == -2)
													{
														if (num22 == 0)
														{
															rectangle.X = 126;
															rectangle.Y = 144;
														}
														if (num22 == 1)
														{
															rectangle.X = 126;
															rectangle.Y = 162;
														}
														if (num22 == 2)
														{
															rectangle.X = 126;
															rectangle.Y = 180;
														}
														WorldGen.mergeUp = true;
													}
													else
													{
														if (num7 == -2)
														{
															if (num22 == 0)
															{
																rectangle.X = 126;
																rectangle.Y = 90;
															}
															if (num22 == 1)
															{
																rectangle.X = 126;
																rectangle.Y = 108;
															}
															if (num22 == 2)
															{
																rectangle.X = 126;
																rectangle.Y = 126;
															}
															WorldGen.mergeDown = true;
														}
													}
												}
											}
											else
											{
												if (num2 == -1 && num7 == -1 && num4 != -1 && num5 != -1)
												{
													if (num4 == -2 && num5 == -2)
													{
														if (num22 == 0)
														{
															rectangle.X = 162;
															rectangle.Y = 198;
														}
														if (num22 == 1)
														{
															rectangle.X = 180;
															rectangle.Y = 198;
														}
														if (num22 == 2)
														{
															rectangle.X = 198;
															rectangle.Y = 198;
														}
														WorldGen.mergeLeft = true;
														WorldGen.mergeRight = true;
													}
													else
													{
														if (num4 == -2)
														{
															if (num22 == 0)
															{
																rectangle.X = 0;
																rectangle.Y = 252;
															}
															if (num22 == 1)
															{
																rectangle.X = 18;
																rectangle.Y = 252;
															}
															if (num22 == 2)
															{
																rectangle.X = 36;
																rectangle.Y = 252;
															}
															WorldGen.mergeLeft = true;
														}
														else
														{
															if (num5 == -2)
															{
																if (num22 == 0)
																{
																	rectangle.X = 54;
																	rectangle.Y = 252;
																}
																if (num22 == 1)
																{
																	rectangle.X = 72;
																	rectangle.Y = 252;
																}
																if (num22 == 2)
																{
																	rectangle.X = 90;
																	rectangle.Y = 252;
																}
																WorldGen.mergeRight = true;
															}
														}
													}
												}
												else
												{
													if (num2 == -2 && num7 == -1 && num4 == -1 && num5 == -1)
													{
														if (num22 == 0)
														{
															rectangle.X = 108;
															rectangle.Y = 144;
														}
														if (num22 == 1)
														{
															rectangle.X = 108;
															rectangle.Y = 162;
														}
														if (num22 == 2)
														{
															rectangle.X = 108;
															rectangle.Y = 180;
														}
														WorldGen.mergeUp = true;
													}
													else
													{
														if (num2 == -1 && num7 == -2 && num4 == -1 && num5 == -1)
														{
															if (num22 == 0)
															{
																rectangle.X = 108;
																rectangle.Y = 90;
															}
															if (num22 == 1)
															{
																rectangle.X = 108;
																rectangle.Y = 108;
															}
															if (num22 == 2)
															{
																rectangle.X = 108;
																rectangle.Y = 126;
															}
															WorldGen.mergeDown = true;
														}
														else
														{
															if (num2 == -1 && num7 == -1 && num4 == -2 && num5 == -1)
															{
																if (num22 == 0)
																{
																	rectangle.X = 0;
																	rectangle.Y = 234;
																}
																if (num22 == 1)
																{
																	rectangle.X = 18;
																	rectangle.Y = 234;
																}
																if (num22 == 2)
																{
																	rectangle.X = 36;
																	rectangle.Y = 234;
																}
																WorldGen.mergeLeft = true;
															}
															else
															{
																if (num2 == -1 && num7 == -1 && num4 == -1 && num5 == -2)
																{
																	if (num22 == 0)
																	{
																		rectangle.X = 54;
																		rectangle.Y = 234;
																	}
																	if (num22 == 1)
																	{
																		rectangle.X = 72;
																		rectangle.Y = 234;
																	}
																	if (num22 == 2)
																	{
																		rectangle.X = 90;
																		rectangle.Y = 234;
																	}
																	WorldGen.mergeRight = true;
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
					if (rectangle.X < 0 || rectangle.Y < 0)
					{
						if (num9 == 2 || num9 == 23 || num9 == 60 || num9 == 70)
						{
							if (num2 == -2)
							{
								num2 = num9;
							}
							if (num7 == -2)
							{
								num7 = num9;
							}
							if (num4 == -2)
							{
								num4 = num9;
							}
							if (num5 == -2)
							{
								num5 = num9;
							}
							if (num == -2)
							{
								num = num9;
							}
							if (num3 == -2)
							{
								num3 = num9;
							}
							if (num6 == -2)
							{
								num6 = num9;
							}
							if (num8 == -2)
							{
								num8 = num9;
							}
						}
						if (num2 == num9 && num7 == num9 && (num4 == num9 & num5 == num9))
						{
							if (num != num9 && num3 != num9)
							{
								if (num22 == 0)
								{
									rectangle.X = 108;
									rectangle.Y = 18;
								}
								if (num22 == 1)
								{
									rectangle.X = 126;
									rectangle.Y = 18;
								}
								if (num22 == 2)
								{
									rectangle.X = 144;
									rectangle.Y = 18;
								}
							}
							else
							{
								if (num6 != num9 && num8 != num9)
								{
									if (num22 == 0)
									{
										rectangle.X = 108;
										rectangle.Y = 36;
									}
									if (num22 == 1)
									{
										rectangle.X = 126;
										rectangle.Y = 36;
									}
									if (num22 == 2)
									{
										rectangle.X = 144;
										rectangle.Y = 36;
									}
								}
								else
								{
									if (num != num9 && num6 != num9)
									{
										if (num22 == 0)
										{
											rectangle.X = 180;
											rectangle.Y = 0;
										}
										if (num22 == 1)
										{
											rectangle.X = 180;
											rectangle.Y = 18;
										}
										if (num22 == 2)
										{
											rectangle.X = 180;
											rectangle.Y = 36;
										}
									}
									else
									{
										if (num3 != num9 && num8 != num9)
										{
											if (num22 == 0)
											{
												rectangle.X = 198;
												rectangle.Y = 0;
											}
											if (num22 == 1)
											{
												rectangle.X = 198;
												rectangle.Y = 18;
											}
											if (num22 == 2)
											{
												rectangle.X = 198;
												rectangle.Y = 36;
											}
										}
										else
										{
											if (num22 == 0)
											{
												rectangle.X = 18;
												rectangle.Y = 18;
											}
											if (num22 == 1)
											{
												rectangle.X = 36;
												rectangle.Y = 18;
											}
											if (num22 == 2)
											{
												rectangle.X = 54;
												rectangle.Y = 18;
											}
										}
									}
								}
							}
						}
						else
						{
							if (num2 != num9 && num7 == num9 && (num4 == num9 & num5 == num9))
							{
								if (num22 == 0)
								{
									rectangle.X = 18;
									rectangle.Y = 0;
								}
								if (num22 == 1)
								{
									rectangle.X = 36;
									rectangle.Y = 0;
								}
								if (num22 == 2)
								{
									rectangle.X = 54;
									rectangle.Y = 0;
								}
							}
							else
							{
								if (num2 == num9 && num7 != num9 && (num4 == num9 & num5 == num9))
								{
									if (num22 == 0)
									{
										rectangle.X = 18;
										rectangle.Y = 36;
									}
									if (num22 == 1)
									{
										rectangle.X = 36;
										rectangle.Y = 36;
									}
									if (num22 == 2)
									{
										rectangle.X = 54;
										rectangle.Y = 36;
									}
								}
								else
								{
									if (num2 == num9 && num7 == num9 && (num4 != num9 & num5 == num9))
									{
										if (num22 == 0)
										{
											rectangle.X = 0;
											rectangle.Y = 0;
										}
										if (num22 == 1)
										{
											rectangle.X = 0;
											rectangle.Y = 18;
										}
										if (num22 == 2)
										{
											rectangle.X = 0;
											rectangle.Y = 36;
										}
									}
									else
									{
										if (num2 == num9 && num7 == num9 && (num4 == num9 & num5 != num9))
										{
											if (num22 == 0)
											{
												rectangle.X = 72;
												rectangle.Y = 0;
											}
											if (num22 == 1)
											{
												rectangle.X = 72;
												rectangle.Y = 18;
											}
											if (num22 == 2)
											{
												rectangle.X = 72;
												rectangle.Y = 36;
											}
										}
										else
										{
											if (num2 != num9 && num7 == num9 && (num4 != num9 & num5 == num9))
											{
												if (num22 == 0)
												{
													rectangle.X = 0;
													rectangle.Y = 54;
												}
												if (num22 == 1)
												{
													rectangle.X = 36;
													rectangle.Y = 54;
												}
												if (num22 == 2)
												{
													rectangle.X = 72;
													rectangle.Y = 54;
												}
											}
											else
											{
												if (num2 != num9 && num7 == num9 && (num4 == num9 & num5 != num9))
												{
													if (num22 == 0)
													{
														rectangle.X = 18;
														rectangle.Y = 54;
													}
													if (num22 == 1)
													{
														rectangle.X = 54;
														rectangle.Y = 54;
													}
													if (num22 == 2)
													{
														rectangle.X = 90;
														rectangle.Y = 54;
													}
												}
												else
												{
													if (num2 == num9 && num7 != num9 && (num4 != num9 & num5 == num9))
													{
														if (num22 == 0)
														{
															rectangle.X = 0;
															rectangle.Y = 72;
														}
														if (num22 == 1)
														{
															rectangle.X = 36;
															rectangle.Y = 72;
														}
														if (num22 == 2)
														{
															rectangle.X = 72;
															rectangle.Y = 72;
														}
													}
													else
													{
														if (num2 == num9 && num7 != num9 && (num4 == num9 & num5 != num9))
														{
															if (num22 == 0)
															{
																rectangle.X = 18;
																rectangle.Y = 72;
															}
															if (num22 == 1)
															{
																rectangle.X = 54;
																rectangle.Y = 72;
															}
															if (num22 == 2)
															{
																rectangle.X = 90;
																rectangle.Y = 72;
															}
														}
														else
														{
															if (num2 == num9 && num7 == num9 && (num4 != num9 & num5 != num9))
															{
																if (num22 == 0)
																{
																	rectangle.X = 90;
																	rectangle.Y = 0;
																}
																if (num22 == 1)
																{
																	rectangle.X = 90;
																	rectangle.Y = 18;
																}
																if (num22 == 2)
																{
																	rectangle.X = 90;
																	rectangle.Y = 36;
																}
															}
															else
															{
																if (num2 != num9 && num7 != num9 && (num4 == num9 & num5 == num9))
																{
																	if (num22 == 0)
																	{
																		rectangle.X = 108;
																		rectangle.Y = 72;
																	}
																	if (num22 == 1)
																	{
																		rectangle.X = 126;
																		rectangle.Y = 72;
																	}
																	if (num22 == 2)
																	{
																		rectangle.X = 144;
																		rectangle.Y = 72;
																	}
																}
																else
																{
																	if (num2 != num9 && num7 == num9 && (num4 != num9 & num5 != num9))
																	{
																		if (num22 == 0)
																		{
																			rectangle.X = 108;
																			rectangle.Y = 0;
																		}
																		if (num22 == 1)
																		{
																			rectangle.X = 126;
																			rectangle.Y = 0;
																		}
																		if (num22 == 2)
																		{
																			rectangle.X = 144;
																			rectangle.Y = 0;
																		}
																	}
																	else
																	{
																		if (num2 == num9 && num7 != num9 && (num4 != num9 & num5 != num9))
																		{
																			if (num22 == 0)
																			{
																				rectangle.X = 108;
																				rectangle.Y = 54;
																			}
																			if (num22 == 1)
																			{
																				rectangle.X = 126;
																				rectangle.Y = 54;
																			}
																			if (num22 == 2)
																			{
																				rectangle.X = 144;
																				rectangle.Y = 54;
																			}
																		}
																		else
																		{
																			if (num2 != num9 && num7 != num9 && (num4 != num9 & num5 == num9))
																			{
																				if (num22 == 0)
																				{
																					rectangle.X = 162;
																					rectangle.Y = 0;
																				}
																				if (num22 == 1)
																				{
																					rectangle.X = 162;
																					rectangle.Y = 18;
																				}
																				if (num22 == 2)
																				{
																					rectangle.X = 162;
																					rectangle.Y = 36;
																				}
																			}
																			else
																			{
																				if (num2 != num9 && num7 != num9 && (num4 == num9 & num5 != num9))
																				{
																					if (num22 == 0)
																					{
																						rectangle.X = 216;
																						rectangle.Y = 0;
																					}
																					if (num22 == 1)
																					{
																						rectangle.X = 216;
																						rectangle.Y = 18;
																					}
																					if (num22 == 2)
																					{
																						rectangle.X = 216;
																						rectangle.Y = 36;
																					}
																				}
																				else
																				{
																					if (num2 != num9 && num7 != num9 && (num4 != num9 & num5 != num9))
																					{
																						if (num22 == 0)
																						{
																							rectangle.X = 162;
																							rectangle.Y = 54;
																						}
																						if (num22 == 1)
																						{
																							rectangle.X = 180;
																							rectangle.Y = 54;
																						}
																						if (num22 == 2)
																						{
																							rectangle.X = 198;
																							rectangle.Y = 54;
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
					if (rectangle.X <= -1 || rectangle.Y <= -1)
					{
						if (num22 <= 0)
						{
							rectangle.X = 18;
							rectangle.Y = 18;
						}
						if (num22 == 1)
						{
							rectangle.X = 36;
							rectangle.Y = 18;
						}
						if (num22 >= 2)
						{
							rectangle.X = 54;
							rectangle.Y = 18;
						}
					}
					Main.tile[i, j].frameX = (short)rectangle.X;
					Main.tile[i, j].frameY = (short)rectangle.Y;
					if (num9 == 52 || num9 == 62)
					{
						if (Main.tile[i, j - 1] != null)
						{
							if (!Main.tile[i, j - 1].active)
							{
								num2 = -1;
							}
							else
							{
								num2 = (int)Main.tile[i, j - 1].type;
							}
						}
						else
						{
							num2 = num9;
						}
						if (num2 != num9 && num2 != 2 && num2 != 60)
						{
							WorldGen.KillTile(i, j, false, false, false);
						}
					}
					if (!WorldGen.noTileActions && num9 == 53)
					{
						if (Main.netMode == 0)
						{
							if (Main.tile[i, j + 1] != null && !Main.tile[i, j + 1].active)
							{
								bool flag3 = true;
								if (Main.tile[i, j - 1].active && Main.tile[i, j - 1].type == 21)
								{
									flag3 = false;
								}
								if (flag3)
								{
									int type = 31;
									if (num9 == 59)
									{
										type = 39;
									}
									if (num9 == 57)
									{
										type = 40;
									}
									Main.tile[i, j].active = false;
									int num24 = Projectile.NewProjectile((float)(i * 16 + 8), (float)(j * 16 + 8), 0f, 0.41f, type, 10, 0f, Main.myPlayer);
									Main.projectile[num24].ai[0] = 1f;
									WorldGen.SquareTileFrame(i, j, true);
								}
							}
						}
						else
						{
							if (Main.netMode == 2 && Main.tile[i, j + 1] != null && !Main.tile[i, j + 1].active)
							{
								bool flag4 = true;
								if (Main.tile[i, j - 1].active && Main.tile[i, j - 1].type == 21)
								{
									flag4 = false;
								}
								if (flag4)
								{
									int type2 = 31;
									if (num9 == 59)
									{
										type2 = 39;
									}
									if (num9 == 57)
									{
										type2 = 40;
									}
									Main.tile[i, j].active = false;
									int num25 = Projectile.NewProjectile((float)(i * 16 + 8), (float)(j * 16 + 8), 0f, 0.41f, type2, 10, 0f, Main.myPlayer);
									Main.projectile[num25].velocity.Y = 0.5f;
									Projectile expr_6511_cp_0 = Main.projectile[num25];
									expr_6511_cp_0.position.Y = expr_6511_cp_0.position.Y + 2f;
									Main.projectile[num25].ai[0] = 1f;
									NetMessage.SendTileSquare(-1, i, j, 1);
									WorldGen.SquareTileFrame(i, j, true);
								}
							}
						}
					}
					if (rectangle.X != frameX && rectangle.Y != frameY && frameX >= 0 && frameY >= 0)
					{
						bool flag5 = WorldGen.mergeUp;
						bool flag6 = WorldGen.mergeDown;
						bool flag7 = WorldGen.mergeLeft;
						bool flag8 = WorldGen.mergeRight;
						WorldGen.TileFrame(i - 1, j, false, false);
						WorldGen.TileFrame(i + 1, j, false, false);
						WorldGen.TileFrame(i, j - 1, false, false);
						WorldGen.TileFrame(i, j + 1, false, false);
						WorldGen.mergeUp = flag5;
						WorldGen.mergeDown = flag6;
						WorldGen.mergeLeft = flag7;
						WorldGen.mergeRight = flag8;
					}
				}
			}
		}
	}
}
