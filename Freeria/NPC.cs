using Microsoft.Xna.Framework;
using System;
namespace Freeria
{
	public class NPC
	{
		public const int maxBuffs = 5;
		public static int immuneTime = 20;
		public static int maxAI = 4;
		private static int spawnSpaceX = 3;
		private static int spawnSpaceY = 3;
		public static int sWidth = 1680;
		public static int sHeight = 1050;
		private static int spawnRangeX = (int)((double)(NPC.sWidth / 16) * 0.7);
		private static int spawnRangeY = (int)((double)(NPC.sHeight / 16) * 0.7);
		public static int safeRangeX = (int)((double)(NPC.sWidth / 16) * 0.52);
		public static int safeRangeY = (int)((double)(NPC.sHeight / 16) * 0.52);
		private static int activeRangeX = (int)((double)NPC.sWidth * 1.7);
		private static int activeRangeY = (int)((double)NPC.sHeight * 1.7);
		private static int townRangeX = NPC.sWidth;
		private static int townRangeY = NPC.sHeight;
		private float npcSlots = 1f;
		private static bool noSpawnCycle = false;
		private static int activeTime = 750;
		private static int defaultSpawnRate = 600;
		private static int defaultMaxSpawns = 5;
		public bool wet;
		public byte wetCount;
		public bool lavaWet;
		public int[] buffType = new int[5];
		public int[] buffTime = new int[5];
		public bool[] buffImmune = new bool[27];
		public bool onFire;
		public bool poisoned;
		public int lifeRegen;
		public int lifeRegenCount;
		public static bool downedBoss1 = false;
		public static bool downedBoss2 = false;
		public static bool downedBoss3 = false;
		private static int spawnRate = NPC.defaultSpawnRate;
		private static int maxSpawns = NPC.defaultMaxSpawns;
		public int soundDelay;
		public Vector2 position;
		public Vector2 velocity;
		public Vector2 oldPosition;
		public Vector2 oldVelocity;
		public int width;
		public int height;
		public bool active;
		public int[] immune = new int[256];
		public int direction = 1;
		public int directionY = 1;
		public int type;
		public float[] ai = new float[NPC.maxAI];
		public int aiAction;
		public int aiStyle;
		public bool justHit;
		public int timeLeft;
		public int target = -1;
		public int damage;
		public int defense;
		public int soundHit;
		public int soundKilled;
		public int life;
		public int lifeMax;
		public Rectangle targetRect;
		public double frameCounter;
		public Rectangle frame;
		public string name;
		public Color color;
		public int alpha;
		public float scale = 1f;
		public float knockBackResist = 1f;
		public int oldDirection;
		public int oldDirectionY;
		public int oldTarget;
		public int whoAmI;
		public float rotation;
		public bool noGravity;
		public bool noTileCollide;
		public bool netUpdate;
		public bool collideX;
		public bool collideY;
		public bool boss;
		public int spriteDirection = -1;
		public bool behindTiles;
		public bool lavaImmune;
		public float value;
		public bool dontTakeDamage;
		public bool townNPC;
		public bool homeless;
		public int homeTileX = -1;
		public int homeTileY = -1;
		public bool friendly;
		public bool closeDoor;
		public int doorX;
		public int doorY;
		public int friendlyRegen;
		public void SetDefaults(string Name)
		{
			this.SetDefaults(0, -1f);
			if (Name == "Green Slime")
			{
				this.SetDefaults(1, 0.9f);
				this.name = Name;
				this.damage = 6;
				this.defense = 0;
				this.life = 14;
				this.knockBackResist = 1.2f;
				this.color = new Color(0, 220, 40, 100);
				this.value = 3f;
			}
			else
			{
				if (Name == "Pinky")
				{
					this.SetDefaults(1, 0.6f);
					this.name = Name;
					this.damage = 5;
					this.defense = 5;
					this.life = 150;
					this.knockBackResist = 1.4f;
					this.color = new Color(250, 30, 90, 90);
					this.value = 10000f;
				}
				else
				{
					if (Name == "Baby Slime")
					{
						this.SetDefaults(1, 0.9f);
						this.name = Name;
						this.damage = 13;
						this.defense = 4;
						this.life = 30;
						this.knockBackResist = 0.95f;
						this.alpha = 120;
						this.color = new Color(0, 0, 0, 50);
						this.value = 10f;
					}
					else
					{
						if (Name == "Black Slime")
						{
							this.SetDefaults(1, -1f);
							this.name = Name;
							this.damage = 15;
							this.defense = 4;
							this.life = 45;
							this.color = new Color(0, 0, 0, 50);
							this.value = 20f;
						}
						else
						{
							if (Name == "Purple Slime")
							{
								this.SetDefaults(1, 1.2f);
								this.name = Name;
								this.damage = 12;
								this.defense = 6;
								this.life = 40;
								this.knockBackResist = 0.9f;
								this.color = new Color(200, 0, 255, 150);
								this.value = 10f;
							}
							else
							{
								if (Name == "Red Slime")
								{
									this.SetDefaults(1, -1f);
									this.name = Name;
									this.damage = 12;
									this.defense = 4;
									this.life = 35;
									this.color = new Color(255, 30, 0, 100);
									this.value = 8f;
								}
								else
								{
									if (Name == "Yellow Slime")
									{
										this.SetDefaults(1, 1.2f);
										this.name = Name;
										this.damage = 15;
										this.defense = 7;
										this.life = 45;
										this.color = new Color(255, 255, 0, 100);
										this.value = 10f;
									}
									else
									{
										if (Name == "Jungle Slime")
										{
											this.SetDefaults(1, 1.1f);
											this.name = Name;
											this.damage = 18;
											this.defense = 6;
											this.life = 60;
											this.color = new Color(143, 215, 93, 100);
											this.value = 500f;
										}
										else
										{
											if (Name == "Little Eater")
											{
												this.SetDefaults(6, 0.85f);
												this.name = Name;
												this.defense = (int)((float)this.defense * this.scale);
												this.damage = (int)((float)this.damage * this.scale);
												this.life = (int)((float)this.life * this.scale);
												this.value = (float)((int)(this.value * this.scale));
												this.npcSlots *= this.scale;
												this.knockBackResist *= 2f - this.scale;
											}
											else
											{
												if (Name == "Big Eater")
												{
													this.SetDefaults(6, 1.15f);
													this.name = Name;
													this.defense = (int)((float)this.defense * this.scale);
													this.damage = (int)((float)this.damage * this.scale);
													this.life = (int)((float)this.life * this.scale);
													this.value = (float)((int)(this.value * this.scale));
													this.npcSlots *= this.scale;
													this.knockBackResist *= 2f - this.scale;
												}
												else
												{
													if (Name == "Short Bones")
													{
														this.SetDefaults(31, 0.9f);
														this.name = Name;
														this.defense = (int)((float)this.defense * this.scale);
														this.damage = (int)((float)this.damage * this.scale);
														this.life = (int)((float)this.life * this.scale);
														this.value = (float)((int)(this.value * this.scale));
													}
													else
													{
														if (Name == "Big Boned")
														{
															this.SetDefaults(31, 1.15f);
															this.name = Name;
															this.defense = (int)((float)this.defense * this.scale);
															this.damage = (int)((double)((float)this.damage * this.scale) * 1.1);
															this.life = (int)((double)((float)this.life * this.scale) * 1.1);
															this.value = (float)((int)(this.value * this.scale));
															this.npcSlots = 2f;
															this.knockBackResist *= 2f - this.scale;
														}
														else
														{
															if (Name == "Little Stinger")
															{
																this.SetDefaults(42, 0.85f);
																this.name = Name;
																this.defense = (int)((float)this.defense * this.scale);
																this.damage = (int)((float)this.damage * this.scale);
																this.life = (int)((float)this.life * this.scale);
																this.value = (float)((int)(this.value * this.scale));
																this.npcSlots *= this.scale;
																this.knockBackResist *= 2f - this.scale;
															}
															else
															{
																if (Name == "Big Stinger")
																{
																	this.SetDefaults(42, 1.2f);
																	this.name = Name;
																	this.defense = (int)((float)this.defense * this.scale);
																	this.damage = (int)((float)this.damage * this.scale);
																	this.life = (int)((float)this.life * this.scale);
																	this.value = (float)((int)(this.value * this.scale));
																	this.npcSlots *= this.scale;
																	this.knockBackResist *= 2f - this.scale;
																}
																else
																{
																	if (Name != "")
																	{
																		for (int i = 1; i < 74; i++)
																		{
																			this.SetDefaults(i, -1f);
																			if (this.name == Name)
																			{
																				break;
																			}
																			if (i == 73)
																			{
																				this.SetDefaults(0, -1f);
																				this.active = false;
																			}
																		}
																	}
																	else
																	{
																		this.active = false;
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
			this.lifeMax = this.life;
		}
		public void SetDefaults(int Type, float scaleOverride = -1f)
		{
			for (int i = 0; i < 5; i++)
			{
				this.buffTime[i] = 0;
				this.buffType[i] = 0;
			}
			for (int j = 0; j < 27; j++)
			{
				this.buffImmune[j] = false;
			}
			this.lifeRegen = 0;
			this.lifeRegenCount = 0;
			this.poisoned = false;
			this.onFire = false;
			this.justHit = false;
			this.dontTakeDamage = false;
			this.npcSlots = 1f;
			this.lavaImmune = false;
			this.lavaWet = false;
			this.wetCount = 0;
			this.wet = false;
			this.townNPC = false;
			this.homeless = false;
			this.homeTileX = -1;
			this.homeTileY = -1;
			this.friendly = false;
			this.behindTiles = false;
			this.boss = false;
			this.noTileCollide = false;
			this.rotation = 0f;
			this.active = true;
			this.alpha = 0;
			this.color = default(Color);
			this.collideX = false;
			this.collideY = false;
			this.direction = 0;
			this.oldDirection = this.direction;
			this.frameCounter = 0.0;
			this.netUpdate = false;
			this.knockBackResist = 1f;
			this.name = "";
			this.noGravity = false;
			this.scale = 1f;
			this.soundHit = 0;
			this.soundKilled = 0;
			this.spriteDirection = -1;
			this.target = 255;
			this.oldTarget = this.target;
			this.targetRect = default(Rectangle);
			this.timeLeft = NPC.activeTime;
			this.type = Type;
			this.value = 0f;
			for (int k = 0; k < NPC.maxAI; k++)
			{
				this.ai[k] = 0f;
			}
			if (this.type == 1)
			{
				this.name = "Blue Slime";
				this.width = 24;
				this.height = 18;
				this.aiStyle = 1;
				this.damage = 7;
				this.defense = 2;
				this.lifeMax = 25;
				this.soundHit = 1;
				this.soundKilled = 1;
				this.alpha = 175;
				this.color = new Color(0, 80, 255, 100);
				this.value = 25f;
				this.buffImmune[20] = true;
			}
			else
			{
				if (this.type == 2)
				{
					this.name = "Demon Eye";
					this.width = 30;
					this.height = 32;
					this.aiStyle = 2;
					this.damage = 18;
					this.defense = 2;
					this.lifeMax = 60;
					this.soundHit = 1;
					this.knockBackResist = 0.8f;
					this.soundKilled = 1;
					this.value = 75f;
				}
				else
				{
					if (this.type == 3)
					{
						this.name = "Zombie";
						this.width = 18;
						this.height = 40;
						this.aiStyle = 3;
						this.damage = 14;
						this.defense = 6;
						this.lifeMax = 45;
						this.soundHit = 1;
						this.soundKilled = 2;
						this.knockBackResist = 0.5f;
						this.value = 60f;
					}
					else
					{
						if (this.type == 4)
						{
							this.name = "Eye of Cthulhu";
							this.width = 100;
							this.height = 110;
							this.aiStyle = 4;
							this.damage = 15;
							this.defense = 12;
							this.lifeMax = 2800;
							this.soundHit = 1;
							this.soundKilled = 1;
							this.knockBackResist = 0f;
							this.noGravity = true;
							this.noTileCollide = true;
							this.timeLeft = NPC.activeTime * 30;
							this.boss = true;
							this.value = 30000f;
							this.npcSlots = 5f;
						}
						else
						{
							if (this.type == 5)
							{
								this.name = "Servant of Cthulhu";
								this.width = 20;
								this.height = 20;
								this.aiStyle = 5;
								this.damage = 12;
								this.defense = 0;
								this.lifeMax = 8;
								this.soundHit = 1;
								this.soundKilled = 1;
								this.noGravity = true;
								this.noTileCollide = true;
							}
							else
							{
								if (this.type == 6)
								{
									this.npcSlots = 1f;
									this.name = "Eater of Souls";
									this.width = 30;
									this.height = 30;
									this.aiStyle = 5;
									this.damage = 22;
									this.defense = 8;
									this.lifeMax = 40;
									this.soundHit = 1;
									this.soundKilled = 1;
									this.noGravity = true;
									this.knockBackResist = 0.5f;
									this.value = 90f;
								}
								else
								{
									if (this.type == 7)
									{
										this.npcSlots = 3.5f;
										this.name = "Devourer Head";
										this.width = 22;
										this.height = 22;
										this.aiStyle = 6;
										this.damage = 31;
										this.defense = 2;
										this.lifeMax = 20;
										this.soundHit = 1;
										this.soundKilled = 1;
										this.noGravity = true;
										this.noTileCollide = true;
										this.knockBackResist = 0f;
										this.behindTiles = true;
										this.value = 140f;
									}
									else
									{
										if (this.type == 8)
										{
											this.name = "Devourer Body";
											this.width = 22;
											this.height = 22;
											this.aiStyle = 6;
											this.damage = 16;
											this.defense = 6;
											this.lifeMax = 40;
											this.soundHit = 1;
											this.soundKilled = 1;
											this.noGravity = true;
											this.noTileCollide = true;
											this.knockBackResist = 0f;
											this.behindTiles = true;
											this.value = 140f;
										}
										else
										{
											if (this.type == 9)
											{
												this.name = "Devourer Tail";
												this.width = 22;
												this.height = 22;
												this.aiStyle = 6;
												this.damage = 13;
												this.defense = 10;
												this.lifeMax = 50;
												this.soundHit = 1;
												this.soundKilled = 1;
												this.noGravity = true;
												this.noTileCollide = true;
												this.knockBackResist = 0f;
												this.behindTiles = true;
												this.value = 140f;
											}
											else
											{
												if (this.type == 10)
												{
													this.name = "Giant Worm Head";
													this.width = 14;
													this.height = 14;
													this.aiStyle = 6;
													this.damage = 8;
													this.defense = 0;
													this.lifeMax = 10;
													this.soundHit = 1;
													this.soundKilled = 1;
													this.noGravity = true;
													this.noTileCollide = true;
													this.knockBackResist = 0f;
													this.behindTiles = true;
													this.value = 40f;
												}
												else
												{
													if (this.type == 11)
													{
														this.name = "Giant Worm Body";
														this.width = 14;
														this.height = 14;
														this.aiStyle = 6;
														this.damage = 4;
														this.defense = 4;
														this.lifeMax = 15;
														this.soundHit = 1;
														this.soundKilled = 1;
														this.noGravity = true;
														this.noTileCollide = true;
														this.knockBackResist = 0f;
														this.behindTiles = true;
														this.value = 40f;
													}
													else
													{
														if (this.type == 12)
														{
															this.name = "Giant Worm Tail";
															this.width = 14;
															this.height = 14;
															this.aiStyle = 6;
															this.damage = 4;
															this.defense = 6;
															this.lifeMax = 20;
															this.soundHit = 1;
															this.soundKilled = 1;
															this.noGravity = true;
															this.noTileCollide = true;
															this.knockBackResist = 0f;
															this.behindTiles = true;
															this.value = 40f;
														}
														else
														{
															if (this.type == 13)
															{
																this.npcSlots = 5f;
																this.name = "Eater of Worlds Head";
																this.width = 38;
																this.height = 38;
																this.aiStyle = 6;
																this.damage = 22;
																this.defense = 2;
																this.lifeMax = 65;
																this.soundHit = 1;
																this.soundKilled = 1;
																this.noGravity = true;
																this.noTileCollide = true;
																this.knockBackResist = 0f;
																this.behindTiles = true;
																this.value = 300f;
																this.scale = 1f;
															}
															else
															{
																if (this.type == 14)
																{
																	this.name = "Eater of Worlds Body";
																	this.width = 38;
																	this.height = 38;
																	this.aiStyle = 6;
																	this.damage = 13;
																	this.defense = 4;
																	this.lifeMax = 150;
																	this.soundHit = 1;
																	this.soundKilled = 1;
																	this.noGravity = true;
																	this.noTileCollide = true;
																	this.knockBackResist = 0f;
																	this.behindTiles = true;
																	this.value = 300f;
																	this.scale = 1f;
																}
																else
																{
																	if (this.type == 15)
																	{
																		this.name = "Eater of Worlds Tail";
																		this.width = 38;
																		this.height = 38;
																		this.aiStyle = 6;
																		this.damage = 11;
																		this.defense = 8;
																		this.lifeMax = 220;
																		this.soundHit = 1;
																		this.soundKilled = 1;
																		this.noGravity = true;
																		this.noTileCollide = true;
																		this.knockBackResist = 0f;
																		this.behindTiles = true;
																		this.value = 300f;
																		this.scale = 1f;
																	}
																	else
																	{
																		if (this.type == 16)
																		{
																			this.npcSlots = 2f;
																			this.name = "Mother Slime";
																			this.width = 36;
																			this.height = 24;
																			this.aiStyle = 1;
																			this.damage = 20;
																			this.defense = 7;
																			this.lifeMax = 90;
																			this.soundHit = 1;
																			this.soundKilled = 1;
																			this.alpha = 120;
																			this.color = new Color(0, 0, 0, 50);
																			this.value = 75f;
																			this.scale = 1.25f;
																			this.knockBackResist = 0.6f;
																			this.buffImmune[20] = true;
																		}
																		else
																		{
																			if (this.type == 17)
																			{
																				this.townNPC = true;
																				this.friendly = true;
																				this.name = "Merchant";
																				this.width = 18;
																				this.height = 40;
																				this.aiStyle = 7;
																				this.damage = 10;
																				this.defense = 15;
																				this.lifeMax = 250;
																				this.soundHit = 1;
																				this.soundKilled = 1;
																				this.knockBackResist = 0.5f;
																			}
																			else
																			{
																				if (this.type == 18)
																				{
																					this.townNPC = true;
																					this.friendly = true;
																					this.name = "Nurse";
																					this.width = 18;
																					this.height = 40;
																					this.aiStyle = 7;
																					this.damage = 10;
																					this.defense = 15;
																					this.lifeMax = 250;
																					this.soundHit = 1;
																					this.soundKilled = 1;
																					this.knockBackResist = 0.5f;
																				}
																				else
																				{
																					if (this.type == 19)
																					{
																						this.townNPC = true;
																						this.friendly = true;
																						this.name = "Arms Dealer";
																						this.width = 18;
																						this.height = 40;
																						this.aiStyle = 7;
																						this.damage = 10;
																						this.defense = 15;
																						this.lifeMax = 250;
																						this.soundHit = 1;
																						this.soundKilled = 1;
																						this.knockBackResist = 0.5f;
																					}
																					else
																					{
																						if (this.type == 20)
																						{
																							this.townNPC = true;
																							this.friendly = true;
																							this.name = "Dryad";
																							this.width = 18;
																							this.height = 40;
																							this.aiStyle = 7;
																							this.damage = 10;
																							this.defense = 15;
																							this.lifeMax = 250;
																							this.soundHit = 1;
																							this.soundKilled = 1;
																							this.knockBackResist = 0.5f;
																						}
																						else
																						{
																							if (this.type == 21)
																							{
																								this.name = "Skeleton";
																								this.width = 18;
																								this.height = 40;
																								this.aiStyle = 3;
																								this.damage = 20;
																								this.defense = 8;
																								this.lifeMax = 60;
																								this.soundHit = 2;
																								this.soundKilled = 2;
																								this.knockBackResist = 0.5f;
																								this.value = 100f;
																								this.buffImmune[20] = true;
																							}
																							else
																							{
																								if (this.type == 22)
																								{
																									this.townNPC = true;
																									this.friendly = true;
																									this.name = "Guide";
																									this.width = 18;
																									this.height = 40;
																									this.aiStyle = 7;
																									this.damage = 10;
																									this.defense = 15;
																									this.lifeMax = 250;
																									this.soundHit = 1;
																									this.soundKilled = 1;
																									this.knockBackResist = 0.5f;
																								}
																								else
																								{
																									if (this.type == 23)
																									{
																										this.name = "Meteor Head";
																										this.width = 22;
																										this.height = 22;
																										this.aiStyle = 5;
																										this.damage = 40;
																										this.defense = 6;
																										this.lifeMax = 26;
																										this.soundHit = 3;
																										this.soundKilled = 3;
																										this.noGravity = true;
																										this.noTileCollide = true;
																										this.value = 80f;
																										this.knockBackResist = 0.4f;
																										this.buffImmune[20] = true;
																										this.buffImmune[24] = true;
																									}
																									else
																									{
																										if (this.type == 24)
																										{
																											this.npcSlots = 3f;
																											this.name = "Fire Imp";
																											this.width = 18;
																											this.height = 40;
																											this.aiStyle = 8;
																											this.damage = 30;
																											this.defense = 16;
																											this.lifeMax = 70;
																											this.soundHit = 1;
																											this.soundKilled = 1;
																											this.knockBackResist = 0.5f;
																											this.lavaImmune = true;
																											this.value = 350f;
																											this.buffImmune[24] = true;
																										}
																										else
																										{
																											if (this.type == 25)
																											{
																												this.name = "Burning Sphere";
																												this.width = 16;
																												this.height = 16;
																												this.aiStyle = 9;
																												this.damage = 30;
																												this.defense = 0;
																												this.lifeMax = 1;
																												this.soundHit = 3;
																												this.soundKilled = 3;
																												this.noGravity = true;
																												this.noTileCollide = true;
																												this.knockBackResist = 0f;
																												this.alpha = 100;
																											}
																											else
																											{
																												if (this.type == 26)
																												{
																													this.name = "Goblin Peon";
																													this.scale = 0.9f;
																													this.width = 18;
																													this.height = 40;
																													this.aiStyle = 3;
																													this.damage = 12;
																													this.defense = 4;
																													this.lifeMax = 60;
																													this.soundHit = 1;
																													this.soundKilled = 1;
																													this.knockBackResist = 0.8f;
																													this.value = 100f;
																												}
																												else
																												{
																													if (this.type == 27)
																													{
																														this.name = "Goblin Thief";
																														this.scale = 0.95f;
																														this.width = 18;
																														this.height = 40;
																														this.aiStyle = 3;
																														this.damage = 20;
																														this.defense = 6;
																														this.lifeMax = 80;
																														this.soundHit = 1;
																														this.soundKilled = 1;
																														this.knockBackResist = 0.7f;
																														this.value = 200f;
																													}
																													else
																													{
																														if (this.type == 28)
																														{
																															this.name = "Goblin Warrior";
																															this.scale = 1.1f;
																															this.width = 18;
																															this.height = 40;
																															this.aiStyle = 3;
																															this.damage = 25;
																															this.defense = 8;
																															this.lifeMax = 110;
																															this.soundHit = 1;
																															this.soundKilled = 1;
																															this.knockBackResist = 0.5f;
																															this.value = 150f;
																														}
																														else
																														{
																															if (this.type == 29)
																															{
																																this.name = "Goblin Sorcerer";
																																this.width = 18;
																																this.height = 40;
																																this.aiStyle = 8;
																																this.damage = 20;
																																this.defense = 2;
																																this.lifeMax = 40;
																																this.soundHit = 1;
																																this.soundKilled = 1;
																																this.knockBackResist = 0.6f;
																																this.value = 200f;
																															}
																															else
																															{
																																if (this.type == 30)
																																{
																																	this.name = "Chaos Ball";
																																	this.width = 16;
																																	this.height = 16;
																																	this.aiStyle = 9;
																																	this.damage = 20;
																																	this.defense = 0;
																																	this.lifeMax = 1;
																																	this.soundHit = 3;
																																	this.soundKilled = 3;
																																	this.noGravity = true;
																																	this.noTileCollide = true;
																																	this.alpha = 100;
																																	this.knockBackResist = 0f;
																																}
																																else
																																{
																																	if (this.type == 31)
																																	{
																																		this.name = "Angry Bones";
																																		this.width = 18;
																																		this.height = 40;
																																		this.aiStyle = 3;
																																		this.damage = 26;
																																		this.defense = 8;
																																		this.lifeMax = 80;
																																		this.soundHit = 2;
																																		this.soundKilled = 2;
																																		this.knockBackResist = 0.8f;
																																		this.value = 130f;
																																		this.buffImmune[20] = true;
																																		this.buffImmune[24] = true;
																																	}
																																	else
																																	{
																																		if (this.type == 32)
																																		{
																																			this.name = "Dark Caster";
																																			this.width = 18;
																																			this.height = 40;
																																			this.aiStyle = 8;
																																			this.damage = 20;
																																			this.defense = 2;
																																			this.lifeMax = 50;
																																			this.soundHit = 2;
																																			this.soundKilled = 2;
																																			this.knockBackResist = 0.6f;
																																			this.value = 140f;
																																			this.npcSlots = 2f;
																																			this.buffImmune[20] = true;
																																			this.buffImmune[24] = true;
																																		}
																																		else
																																		{
																																			if (this.type == 33)
																																			{
																																				this.name = "Water Sphere";
																																				this.width = 16;
																																				this.height = 16;
																																				this.aiStyle = 9;
																																				this.damage = 20;
																																				this.defense = 0;
																																				this.lifeMax = 1;
																																				this.soundHit = 3;
																																				this.soundKilled = 3;
																																				this.noGravity = true;
																																				this.noTileCollide = true;
																																				this.alpha = 100;
																																				this.knockBackResist = 0f;
																																			}
																																			else
																																			{
																																				if (this.type == 34)
																																				{
																																					this.name = "Cursed Skull";
																																					this.width = 26;
																																					this.height = 28;
																																					this.aiStyle = 10;
																																					this.damage = 35;
																																					this.defense = 6;
																																					this.lifeMax = 40;
																																					this.soundHit = 2;
																																					this.soundKilled = 2;
																																					this.noGravity = true;
																																					this.noTileCollide = true;
																																					this.value = 150f;
																																					this.knockBackResist = 0.2f;
																																					this.npcSlots = 0.75f;
																																					this.buffImmune[20] = true;
																																					this.buffImmune[24] = true;
																																				}
																																				else
																																				{
																																					if (this.type == 35)
																																					{
																																						this.name = "Skeletron Head";
																																						this.width = 80;
																																						this.height = 102;
																																						this.aiStyle = 11;
																																						this.damage = 32;
																																						this.defense = 10;
																																						this.lifeMax = 4400;
																																						this.soundHit = 2;
																																						this.soundKilled = 2;
																																						this.noGravity = true;
																																						this.noTileCollide = true;
																																						this.value = 50000f;
																																						this.knockBackResist = 0f;
																																						this.boss = true;
																																						this.npcSlots = 6f;
																																						this.buffImmune[20] = true;
																																						this.buffImmune[24] = true;
																																					}
																																					else
																																					{
																																						if (this.type == 36)
																																						{
																																							this.name = "Skeletron Hand";
																																							this.width = 52;
																																							this.height = 52;
																																							this.aiStyle = 12;
																																							this.damage = 20;
																																							this.defense = 14;
																																							this.lifeMax = 600;
																																							this.soundHit = 2;
																																							this.soundKilled = 2;
																																							this.noGravity = true;
																																							this.noTileCollide = true;
																																							this.knockBackResist = 0f;
																																							this.buffImmune[20] = true;
																																							this.buffImmune[24] = true;
																																						}
																																						else
																																						{
																																							if (this.type == 37)
																																							{
																																								this.townNPC = true;
																																								this.friendly = true;
																																								this.name = "Old Man";
																																								this.width = 18;
																																								this.height = 40;
																																								this.aiStyle = 7;
																																								this.damage = 10;
																																								this.defense = 15;
																																								this.lifeMax = 250;
																																								this.soundHit = 1;
																																								this.soundKilled = 1;
																																								this.knockBackResist = 0.5f;
																																							}
																																							else
																																							{
																																								if (this.type == 38)
																																								{
																																									this.townNPC = true;
																																									this.friendly = true;
																																									this.name = "Demolitionist";
																																									this.width = 18;
																																									this.height = 40;
																																									this.aiStyle = 7;
																																									this.damage = 10;
																																									this.defense = 15;
																																									this.lifeMax = 250;
																																									this.soundHit = 1;
																																									this.soundKilled = 1;
																																									this.knockBackResist = 0.5f;
																																								}
																																								else
																																								{
																																									if (this.type == 39)
																																									{
																																										this.npcSlots = 6f;
																																										this.name = "Bone Serpent Head";
																																										this.width = 22;
																																										this.height = 22;
																																										this.aiStyle = 6;
																																										this.damage = 30;
																																										this.defense = 10;
																																										this.lifeMax = 70;
																																										this.soundHit = 2;
																																										this.soundKilled = 5;
																																										this.noGravity = true;
																																										this.noTileCollide = true;
																																										this.knockBackResist = 0f;
																																										this.behindTiles = true;
																																										this.value = 1200f;
																																										this.buffImmune[20] = true;
																																										this.buffImmune[24] = true;
																																									}
																																									else
																																									{
																																										if (this.type == 40)
																																										{
																																											this.name = "Bone Serpent Body";
																																											this.width = 22;
																																											this.height = 22;
																																											this.aiStyle = 6;
																																											this.damage = 15;
																																											this.defense = 12;
																																											this.lifeMax = 150;
																																											this.soundHit = 2;
																																											this.soundKilled = 5;
																																											this.noGravity = true;
																																											this.noTileCollide = true;
																																											this.knockBackResist = 0f;
																																											this.behindTiles = true;
																																											this.value = 1200f;
																																											this.buffImmune[20] = true;
																																											this.buffImmune[24] = true;
																																										}
																																										else
																																										{
																																											if (this.type == 41)
																																											{
																																												this.name = "Bone Serpent Tail";
																																												this.width = 22;
																																												this.height = 22;
																																												this.aiStyle = 6;
																																												this.damage = 10;
																																												this.defense = 18;
																																												this.lifeMax = 200;
																																												this.soundHit = 2;
																																												this.soundKilled = 5;
																																												this.noGravity = true;
																																												this.noTileCollide = true;
																																												this.knockBackResist = 0f;
																																												this.behindTiles = true;
																																												this.value = 1200f;
																																												this.buffImmune[20] = true;
																																												this.buffImmune[24] = true;
																																											}
																																											else
																																											{
																																												if (this.type == 42)
																																												{
																																													this.name = "Hornet";
																																													this.width = 34;
																																													this.height = 32;
																																													this.aiStyle = 5;
																																													this.damage = 38;
																																													this.defense = 12;
																																													this.lifeMax = 50;
																																													this.soundHit = 1;
																																													this.knockBackResist = 0.5f;
																																													this.soundKilled = 1;
																																													this.value = 200f;
																																													this.noGravity = true;
																																													this.buffImmune[20] = true;
																																												}
																																												else
																																												{
																																													if (this.type == 43)
																																													{
																																														this.noGravity = true;
																																														this.noTileCollide = true;
																																														this.name = "Man Eater";
																																														this.width = 30;
																																														this.height = 30;
																																														this.aiStyle = 13;
																																														this.damage = 45;
																																														this.defense = 14;
																																														this.lifeMax = 130;
																																														this.soundHit = 1;
																																														this.knockBackResist = 0f;
																																														this.soundKilled = 1;
																																														this.value = 350f;
																																														this.buffImmune[20] = true;
																																													}
																																													else
																																													{
																																														if (this.type == 44)
																																														{
																																															this.name = "Undead Miner";
																																															this.width = 18;
																																															this.height = 40;
																																															this.aiStyle = 3;
																																															this.damage = 22;
																																															this.defense = 9;
																																															this.lifeMax = 70;
																																															this.soundHit = 2;
																																															this.soundKilled = 2;
																																															this.knockBackResist = 0.5f;
																																															this.value = 250f;
																																															this.buffImmune[20] = true;
																																															this.buffImmune[24] = true;
																																														}
																																														else
																																														{
																																															if (this.type == 45)
																																															{
																																																this.name = "Tim";
																																																this.width = 18;
																																																this.height = 40;
																																																this.aiStyle = 8;
																																																this.damage = 20;
																																																this.defense = 4;
																																																this.lifeMax = 200;
																																																this.soundHit = 2;
																																																this.soundKilled = 2;
																																																this.knockBackResist = 0.6f;
																																																this.value = 5000f;
																																																this.buffImmune[20] = true;
																																																this.buffImmune[24] = true;
																																															}
																																															else
																																															{
																																																if (this.type == 46)
																																																{
																																																	this.name = "Chicken";
																																																	this.friendly = true;
																																																	this.width = 18;
																																																	this.height = 20;
																																																	this.aiStyle = 7;
																																																	this.damage = 0;
																																																	this.defense = 0;
																																																	this.lifeMax = 5;
																																																	this.soundHit = 1;
																																																	this.soundKilled = 1;
																																																}
																																																else
																																																{
																																																	if (this.type == 47)
																																																	{
																																																		this.name = "Corrupt Chicken";
																																																		this.width = 18;
																																																		this.height = 20;
																																																		this.aiStyle = 3;
																																																		this.damage = 20;
																																																		this.defense = 4;
																																																		this.lifeMax = 70;
																																																		this.soundHit = 1;
																																																		this.soundKilled = 1;
																																																		this.value = 500f;
																																																	}
																																																	else
																																																	{
																																																		if (this.type == 48)
																																																		{
																																																			this.name = "Harpy";
																																																			this.width = 24;
																																																			this.height = 34;
																																																			this.aiStyle = 14;
																																																			this.damage = 25;
																																																			this.defense = 8;
																																																			this.lifeMax = 100;
																																																			this.soundHit = 1;
																																																			this.knockBackResist = 0.6f;
																																																			this.soundKilled = 1;
																																																			this.value = 300f;
																																																		}
																																																		else
																																																		{
																																																			if (this.type == 49)
																																																			{
																																																				this.npcSlots = 0.5f;
																																																				this.name = "Cave Bat";
																																																				this.width = 22;
																																																				this.height = 18;
																																																				this.aiStyle = 14;
																																																				this.damage = 13;
																																																				this.defense = 2;
																																																				this.lifeMax = 16;
																																																				this.soundHit = 1;
																																																				this.knockBackResist = 0.8f;
																																																				this.soundKilled = 4;
																																																				this.value = 90f;
																																																			}
																																																			else
																																																			{
																																																				if (this.type == 50)
																																																				{
																																																					this.boss = true;
																																																					this.name = "King Slime";
																																																					this.width = 98;
																																																					this.height = 92;
																																																					this.aiStyle = 15;
																																																					this.damage = 40;
																																																					this.defense = 10;
																																																					this.lifeMax = 2000;
																																																					this.knockBackResist = 0f;
																																																					this.soundHit = 1;
																																																					this.soundKilled = 1;
																																																					this.alpha = 30;
																																																					this.value = 10000f;
																																																					this.scale = 1.25f;
																																																					this.buffImmune[20] = true;
																																																				}
																																																				else
																																																				{
																																																					if (this.type == 51)
																																																					{
																																																						this.npcSlots = 0.5f;
																																																						this.name = "Jungle Bat";
																																																						this.width = 22;
																																																						this.height = 18;
																																																						this.aiStyle = 14;
																																																						this.damage = 20;
																																																						this.defense = 4;
																																																						this.lifeMax = 34;
																																																						this.soundHit = 1;
																																																						this.knockBackResist = 0.8f;
																																																						this.soundKilled = 4;
																																																						this.value = 80f;
																																																					}
																																																					else
																																																					{
																																																						if (this.type == 52)
																																																						{
																																																							this.name = "Doctor Bones";
																																																							this.width = 18;
																																																							this.height = 40;
																																																							this.aiStyle = 3;
																																																							this.damage = 20;
																																																							this.defense = 10;
																																																							this.lifeMax = 500;
																																																							this.soundHit = 1;
																																																							this.soundKilled = 2;
																																																							this.knockBackResist = 0.5f;
																																																							this.value = 1000f;
																																																						}
																																																						else
																																																						{
																																																							if (this.type == 53)
																																																							{
																																																								this.name = "The Groom";
																																																								this.width = 18;
																																																								this.height = 40;
																																																								this.aiStyle = 3;
																																																								this.damage = 14;
																																																								this.defense = 8;
																																																								this.lifeMax = 200;
																																																								this.soundHit = 1;
																																																								this.soundKilled = 2;
																																																								this.knockBackResist = 0.5f;
																																																								this.value = 1000f;
																																																							}
																																																							else
																																																							{
																																																								if (this.type == 54)
																																																								{
																																																									this.townNPC = true;
																																																									this.friendly = true;
																																																									this.name = "Clothier";
																																																									this.width = 18;
																																																									this.height = 40;
																																																									this.aiStyle = 7;
																																																									this.damage = 10;
																																																									this.defense = 15;
																																																									this.lifeMax = 250;
																																																									this.soundHit = 1;
																																																									this.soundKilled = 1;
																																																									this.knockBackResist = 0.5f;
																																																								}
																																																								else
																																																								{
																																																									if (this.type == 55)
																																																									{
																																																										this.friendly = true;
																																																										this.noGravity = true;
																																																										this.name = "Goldfish";
																																																										this.width = 20;
																																																										this.height = 18;
																																																										this.aiStyle = 16;
																																																										this.damage = 0;
																																																										this.defense = 0;
																																																										this.lifeMax = 5;
																																																										this.soundHit = 1;
																																																										this.soundKilled = 1;
																																																										this.knockBackResist = 0.5f;
																																																									}
																																																									else
																																																									{
																																																										if (this.type == 56)
																																																										{
																																																											this.noTileCollide = true;
																																																											this.noGravity = true;
																																																											this.name = "Snatcher";
																																																											this.width = 30;
																																																											this.height = 30;
																																																											this.aiStyle = 13;
																																																											this.damage = 25;
																																																											this.defense = 10;
																																																											this.lifeMax = 60;
																																																											this.soundHit = 1;
																																																											this.knockBackResist = 0f;
																																																											this.soundKilled = 1;
																																																											this.value = 90f;
																																																											this.buffImmune[20] = true;
																																																										}
																																																										else
																																																										{
																																																											if (this.type == 57)
																																																											{
																																																												this.noGravity = true;
																																																												this.name = "Corrupt Goldfish";
																																																												this.width = 18;
																																																												this.height = 20;
																																																												this.aiStyle = 16;
																																																												this.damage = 30;
																																																												this.defense = 6;
																																																												this.lifeMax = 100;
																																																												this.soundHit = 1;
																																																												this.soundKilled = 1;
																																																												this.value = 500f;
																																																											}
																																																											else
																																																											{
																																																												if (this.type == 58)
																																																												{
																																																													this.npcSlots = 0.5f;
																																																													this.noGravity = true;
																																																													this.name = "Piranha";
																																																													this.width = 18;
																																																													this.height = 20;
																																																													this.aiStyle = 16;
																																																													this.damage = 25;
																																																													this.defense = 2;
																																																													this.lifeMax = 30;
																																																													this.soundHit = 1;
																																																													this.soundKilled = 1;
																																																													this.value = 50f;
																																																												}
																																																												else
																																																												{
																																																													if (this.type == 59)
																																																													{
																																																														this.name = "Lava Slime";
																																																														this.width = 24;
																																																														this.height = 18;
																																																														this.aiStyle = 1;
																																																														this.damage = 15;
																																																														this.defense = 10;
																																																														this.lifeMax = 50;
																																																														this.soundHit = 1;
																																																														this.soundKilled = 1;
																																																														this.scale = 1.1f;
																																																														this.alpha = 50;
																																																														this.lavaImmune = true;
																																																														this.value = 120f;
																																																														this.buffImmune[20] = true;
																																																														this.buffImmune[24] = true;
																																																													}
																																																													else
																																																													{
																																																														if (this.type == 60)
																																																														{
																																																															this.npcSlots = 0.5f;
																																																															this.name = "Hellbat";
																																																															this.width = 22;
																																																															this.height = 18;
																																																															this.aiStyle = 14;
																																																															this.damage = 35;
																																																															this.defense = 8;
																																																															this.lifeMax = 46;
																																																															this.soundHit = 1;
																																																															this.knockBackResist = 0.8f;
																																																															this.soundKilled = 4;
																																																															this.value = 120f;
																																																															this.scale = 1.1f;
																																																															this.lavaImmune = true;
																																																															this.buffImmune[24] = true;
																																																														}
																																																														else
																																																														{
																																																															if (this.type == 61)
																																																															{
																																																																this.name = "Vulture";
																																																																this.width = 36;
																																																																this.height = 36;
																																																																this.aiStyle = 17;
																																																																this.damage = 15;
																																																																this.defense = 4;
																																																																this.lifeMax = 40;
																																																																this.soundHit = 1;
																																																																this.knockBackResist = 0.8f;
																																																																this.soundKilled = 1;
																																																																this.value = 60f;
																																																															}
																																																															else
																																																															{
																																																																if (this.type == 62)
																																																																{
																																																																	this.npcSlots = 2f;
																																																																	this.name = "Demon";
																																																																	this.width = 28;
																																																																	this.height = 48;
																																																																	this.aiStyle = 14;
																																																																	this.damage = 32;
																																																																	this.defense = 8;
																																																																	this.lifeMax = 120;
																																																																	this.soundHit = 1;
																																																																	this.knockBackResist = 0.8f;
																																																																	this.soundKilled = 1;
																																																																	this.value = 300f;
																																																																	this.lavaImmune = true;
																																																																	this.buffImmune[24] = true;
																																																																}
																																																																else
																																																																{
																																																																	if (this.type == 63)
																																																																	{
																																																																		this.noGravity = true;
																																																																		this.name = "Blue Jellyfish";
																																																																		this.width = 26;
																																																																		this.height = 26;
																																																																		this.aiStyle = 18;
																																																																		this.damage = 20;
																																																																		this.defense = 2;
																																																																		this.lifeMax = 30;
																																																																		this.soundHit = 1;
																																																																		this.soundKilled = 1;
																																																																		this.value = 100f;
																																																																		this.alpha = 20;
																																																																	}
																																																																	else
																																																																	{
																																																																		if (this.type == 64)
																																																																		{
																																																																			this.noGravity = true;
																																																																			this.name = "Pink Jellyfish";
																																																																			this.width = 26;
																																																																			this.height = 26;
																																																																			this.aiStyle = 18;
																																																																			this.damage = 30;
																																																																			this.defense = 6;
																																																																			this.lifeMax = 70;
																																																																			this.soundHit = 1;
																																																																			this.soundKilled = 1;
																																																																			this.value = 100f;
																																																																			this.alpha = 20;
																																																																		}
																																																																		else
																																																																		{
																																																																			if (this.type == 65)
																																																																			{
																																																																				this.noGravity = true;
																																																																				this.name = "Shark";
																																																																				this.width = 100;
																																																																				this.height = 24;
																																																																				this.aiStyle = 16;
																																																																				this.damage = 40;
																																																																				this.defense = 2;
																																																																				this.lifeMax = 300;
																																																																				this.soundHit = 1;
																																																																				this.soundKilled = 1;
																																																																				this.value = 400f;
																																																																				this.knockBackResist = 0.7f;
																																																																			}
																																																																			else
																																																																			{
																																																																				if (this.type == 66)
																																																																				{
																																																																					this.npcSlots = 2f;
																																																																					this.name = "Voodoo Demon";
																																																																					this.width = 28;
																																																																					this.height = 48;
																																																																					this.aiStyle = 14;
																																																																					this.damage = 32;
																																																																					this.defense = 8;
																																																																					this.lifeMax = 140;
																																																																					this.soundHit = 1;
																																																																					this.knockBackResist = 0.8f;
																																																																					this.soundKilled = 1;
																																																																					this.value = 1000f;
																																																																					this.lavaImmune = true;
																																																																					this.buffImmune[24] = true;
																																																																				}
																																																																				else
																																																																				{
																																																																					if (this.type == 67)
																																																																					{
																																																																						this.name = "Crab";
																																																																						this.width = 28;
																																																																						this.height = 20;
																																																																						this.aiStyle = 3;
																																																																						this.damage = 20;
																																																																						this.defense = 10;
																																																																						this.lifeMax = 40;
																																																																						this.soundHit = 1;
																																																																						this.soundKilled = 1;
																																																																						this.value = 60f;
																																																																					}
																																																																					else
																																																																					{
																																																																						if (this.type == 68)
																																																																						{
																																																																							this.name = "Dungeon Guardian";
																																																																							this.width = 80;
																																																																							this.height = 102;
																																																																							this.aiStyle = 11;
																																																																							this.damage = 9000;
																																																																							this.defense = 9000;
																																																																							this.lifeMax = 9999;
																																																																							this.soundHit = 2;
																																																																							this.soundKilled = 2;
																																																																							this.noGravity = true;
																																																																							this.noTileCollide = true;
																																																																							this.knockBackResist = 0f;
																																																																							this.buffImmune[20] = true;
																																																																							this.buffImmune[24] = true;
																																																																						}
																																																																						else
																																																																						{
																																																																							if (this.type == 69)
																																																																							{
																																																																								this.name = "Antlion";
																																																																								this.width = 24;
																																																																								this.height = 24;
																																																																								this.aiStyle = 19;
																																																																								this.damage = 10;
																																																																								this.defense = 6;
																																																																								this.lifeMax = 45;
																																																																								this.soundHit = 1;
																																																																								this.soundKilled = 1;
																																																																								this.knockBackResist = 0f;
																																																																								this.value = 60f;
																																																																								this.behindTiles = true;
																																																																							}
																																																																							else
																																																																							{
																																																																								if (this.type == 70)
																																																																								{
																																																																									this.npcSlots = 0.3f;
																																																																									this.name = "Spike Ball";
																																																																									this.width = 34;
																																																																									this.height = 34;
																																																																									this.aiStyle = 20;
																																																																									this.damage = 32;
																																																																									this.defense = 100;
																																																																									this.lifeMax = 100;
																																																																									this.soundHit = 1;
																																																																									this.soundKilled = 1;
																																																																									this.knockBackResist = 0f;
																																																																									this.noGravity = true;
																																																																									this.noTileCollide = true;
																																																																									this.dontTakeDamage = true;
																																																																									this.scale = 1.5f;
																																																																								}
																																																																								else
																																																																								{
																																																																									if (this.type == 71)
																																																																									{
																																																																										this.npcSlots = 2f;
																																																																										this.name = "Dungeon Slime";
																																																																										this.width = 36;
																																																																										this.height = 24;
																																																																										this.aiStyle = 1;
																																																																										this.damage = 30;
																																																																										this.defense = 7;
																																																																										this.lifeMax = 150;
																																																																										this.soundHit = 1;
																																																																										this.soundKilled = 1;
																																																																										this.alpha = 60;
																																																																										this.value = 150f;
																																																																										this.scale = 1.25f;
																																																																										this.knockBackResist = 0.6f;
																																																																										this.buffImmune[20] = true;
																																																																									}
																																																																									else
																																																																									{
																																																																										if (this.type == 72)
																																																																										{
																																																																											this.npcSlots = 0.3f;
																																																																											this.name = "Blazing Wheel";
																																																																											this.width = 34;
																																																																											this.height = 34;
																																																																											this.aiStyle = 21;
																																																																											this.damage = 24;
																																																																											this.defense = 100;
																																																																											this.lifeMax = 100;
																																																																											this.alpha = 100;
																																																																											this.behindTiles = true;
																																																																											this.soundHit = 1;
																																																																											this.soundKilled = 1;
																																																																											this.knockBackResist = 0f;
																																																																											this.noGravity = true;
																																																																											this.dontTakeDamage = true;
																																																																											this.scale = 1.2f;
																																																																										}
																																																																										else
																																																																										{
																																																																											if (this.type == 73)
																																																																											{
																																																																												this.name = "Goblin Scout";
																																																																												this.scale = 0.95f;
																																																																												this.width = 18;
																																																																												this.height = 40;
																																																																												this.aiStyle = 3;
																																																																												this.damage = 20;
																																																																												this.defense = 6;
																																																																												this.lifeMax = 80;
																																																																												this.soundHit = 1;
																																																																												this.soundKilled = 1;
																																																																												this.knockBackResist = 0.7f;
																																																																												this.value = 200f;
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
			if (Main.dedServ)
			{
				this.frame = default(Rectangle);
			}
			else
			{
				this.frame = new Rectangle(0, 0, Main.npcTexture[this.type].Width, Main.npcTexture[this.type].Height / Main.npcFrameCount[this.type]);
			}
			if (scaleOverride > 0f)
			{
				int num = (int)((float)this.width * this.scale);
				int num2 = (int)((float)this.height * this.scale);
				this.position.X = this.position.X + (float)(num / 2);
				this.position.Y = this.position.Y + (float)num2;
				this.scale = scaleOverride;
				this.width = (int)((float)this.width * this.scale);
				this.height = (int)((float)this.height * this.scale);
				if (this.height == 16 || this.height == 32)
				{
					this.height++;
				}
				this.position.X = this.position.X - (float)(this.width / 2);
				this.position.Y = this.position.Y - (float)this.height;
			}
			else
			{
				this.width = (int)((float)this.width * this.scale);
				this.height = (int)((float)this.height * this.scale);
			}
			this.life = this.lifeMax;
		}
		public void AI()
		{
			if (this.aiStyle == 0)
			{
				this.velocity.X = this.velocity.X * 0.93f;
				if ((double)this.velocity.X > -0.1 && (double)this.velocity.X < 0.1)
				{
					this.velocity.X = 0f;
					return;
				}
			}
			else
			{
				if (this.aiStyle == 1)
				{
					bool flag = false;
					if (!Main.dayTime || this.life != this.lifeMax || (double)this.position.Y > Main.worldSurface * 16.0)
					{
						flag = true;
					}
					if (this.type == 59)
					{
						Vector2 arg_10B_0 = new Vector2(this.position.X, this.position.Y);
						int arg_10B_1 = this.width;
						int arg_10B_2 = this.height;
						int arg_10B_3 = 6;
						float arg_10B_4 = this.velocity.X * 0.2f;
						float arg_10B_5 = this.velocity.Y * 0.2f;
						int arg_10B_6 = 100;
						Color newColor = default(Color);
						int num = Dust.NewDust(arg_10B_0, arg_10B_1, arg_10B_2, arg_10B_3, arg_10B_4, arg_10B_5, arg_10B_6, newColor, 1.7f);
						Main.dust[num].noGravity = true;
					}
					if (this.ai[2] > 1f)
					{
						this.ai[2] -= 1f;
					}
					if (this.wet)
					{
						if (this.velocity.Y < 0f && this.ai[3] == this.position.X)
						{
							this.direction *= -1;
							this.ai[2] = 200f;
						}
						if (this.velocity.Y > 0f)
						{
							this.ai[3] = this.position.X;
						}
						if (this.type == 59)
						{
							if (this.velocity.Y > 2f)
							{
								this.velocity.Y = this.velocity.Y * 0.9f;
							}
							else
							{
								if (this.directionY < 0)
								{
									this.velocity.Y = this.velocity.Y - 0.8f;
								}
							}
							this.velocity.Y = this.velocity.Y - 0.5f;
							if (this.velocity.Y < -10f)
							{
								this.velocity.Y = -10f;
							}
						}
						else
						{
							if (this.velocity.Y > 2f)
							{
								this.velocity.Y = this.velocity.Y * 0.9f;
							}
							this.velocity.Y = this.velocity.Y - 0.5f;
							if (this.velocity.Y < -4f)
							{
								this.velocity.Y = -4f;
							}
						}
						if (this.ai[2] == 1f && flag)
						{
							this.TargetClosest(true);
						}
					}
					this.aiAction = 0;
					if (this.ai[2] == 0f)
					{
						this.ai[0] = -100f;
						this.ai[2] = 1f;
						this.TargetClosest(true);
					}
					if (this.velocity.Y == 0f)
					{
						if (this.ai[3] == this.position.X)
						{
							this.direction *= -1;
							this.ai[2] = 200f;
						}
						this.ai[3] = 0f;
						this.velocity.X = this.velocity.X * 0.8f;
						if ((double)this.velocity.X > -0.1 && (double)this.velocity.X < 0.1)
						{
							this.velocity.X = 0f;
						}
						if (flag)
						{
							this.ai[0] += 1f;
						}
						this.ai[0] += 1f;
						if (this.type == 59)
						{
							this.ai[0] += 2f;
						}
						if (this.type == 71)
						{
							this.ai[0] += 3f;
						}
						if (this.ai[0] >= 0f)
						{
							this.netUpdate = true;
							if (flag && this.ai[2] == 1f)
							{
								this.TargetClosest(true);
							}
							if (this.ai[1] == 2f)
							{
								this.velocity.Y = -8f;
								if (this.type == 59)
								{
									this.velocity.Y = this.velocity.Y - 2f;
								}
								this.velocity.X = this.velocity.X + (float)(3 * this.direction);
								if (this.type == 59)
								{
									this.velocity.X = this.velocity.X + 0.5f * (float)this.direction;
								}
								this.ai[0] = -200f;
								this.ai[1] = 0f;
								this.ai[3] = this.position.X;
								return;
							}
							this.velocity.Y = -6f;
							this.velocity.X = this.velocity.X + (float)(2 * this.direction);
							if (this.type == 59)
							{
								this.velocity.X = this.velocity.X + (float)(2 * this.direction);
							}
							this.ai[0] = -120f;
							this.ai[1] += 1f;
							return;
						}
						else
						{
							if (this.ai[0] >= -30f)
							{
								this.aiAction = 1;
								return;
							}
						}
					}
					else
					{
						if (this.target < 255 && ((this.direction == 1 && this.velocity.X < 3f) || (this.direction == -1 && this.velocity.X > -3f)))
						{
							if ((this.direction == -1 && (double)this.velocity.X < 0.1) || (this.direction == 1 && (double)this.velocity.X > -0.1))
							{
								this.velocity.X = this.velocity.X + 0.2f * (float)this.direction;
								return;
							}
							this.velocity.X = this.velocity.X * 0.93f;
							return;
						}
					}
				}
				else
				{
					if (this.aiStyle == 2)
					{
						this.noGravity = true;
						if (this.collideX)
						{
							this.velocity.X = this.oldVelocity.X * -0.5f;
							if (this.direction == -1 && this.velocity.X > 0f && this.velocity.X < 2f)
							{
								this.velocity.X = 2f;
							}
							if (this.direction == 1 && this.velocity.X < 0f && this.velocity.X > -2f)
							{
								this.velocity.X = -2f;
							}
						}
						if (this.collideY)
						{
							this.velocity.Y = this.oldVelocity.Y * -0.5f;
							if (this.velocity.Y > 0f && this.velocity.Y < 1f)
							{
								this.velocity.Y = 1f;
							}
							if (this.velocity.Y < 0f && this.velocity.Y > -1f)
							{
								this.velocity.Y = -1f;
							}
						}
						if (Main.dayTime && (double)this.position.Y <= Main.worldSurface * 16.0 && this.type == 2)
						{
							if (this.timeLeft > 10)
							{
								this.timeLeft = 10;
							}
							this.directionY = -1;
							if (this.velocity.Y > 0f)
							{
								this.direction = 1;
							}
							this.direction = -1;
							if (this.velocity.X > 0f)
							{
								this.direction = 1;
							}
						}
						else
						{
							this.TargetClosest(true);
						}
						if (this.direction == -1 && this.velocity.X > -4f)
						{
							this.velocity.X = this.velocity.X - 0.1f;
							if (this.velocity.X > 4f)
							{
								this.velocity.X = this.velocity.X - 0.1f;
							}
							else
							{
								if (this.velocity.X > 0f)
								{
									this.velocity.X = this.velocity.X + 0.05f;
								}
							}
							if (this.velocity.X < -4f)
							{
								this.velocity.X = -4f;
							}
						}
						else
						{
							if (this.direction == 1 && this.velocity.X < 4f)
							{
								this.velocity.X = this.velocity.X + 0.1f;
								if (this.velocity.X < -4f)
								{
									this.velocity.X = this.velocity.X + 0.1f;
								}
								else
								{
									if (this.velocity.X < 0f)
									{
										this.velocity.X = this.velocity.X - 0.05f;
									}
								}
								if (this.velocity.X > 4f)
								{
									this.velocity.X = 4f;
								}
							}
						}
						if (this.directionY == -1 && (double)this.velocity.Y > -1.5)
						{
							this.velocity.Y = this.velocity.Y - 0.04f;
							if ((double)this.velocity.Y > 1.5)
							{
								this.velocity.Y = this.velocity.Y - 0.05f;
							}
							else
							{
								if (this.velocity.Y > 0f)
								{
									this.velocity.Y = this.velocity.Y + 0.03f;
								}
							}
							if ((double)this.velocity.Y < -1.5)
							{
								this.velocity.Y = -1.5f;
							}
						}
						else
						{
							if (this.directionY == 1 && (double)this.velocity.Y < 1.5)
							{
								this.velocity.Y = this.velocity.Y + 0.04f;
								if ((double)this.velocity.Y < -1.5)
								{
									this.velocity.Y = this.velocity.Y + 0.05f;
								}
								else
								{
									if (this.velocity.Y < 0f)
									{
										this.velocity.Y = this.velocity.Y - 0.03f;
									}
								}
								if ((double)this.velocity.Y > 1.5)
								{
									this.velocity.Y = 1.5f;
								}
							}
						}
						if (this.type == 2 && Main.rand.Next(40) == 0)
						{
							Vector2 arg_B9F_0 = new Vector2(this.position.X, this.position.Y + (float)this.height * 0.25f);
							int arg_B9F_1 = this.width;
							int arg_B9F_2 = (int)((float)this.height * 0.5f);
							int arg_B9F_3 = 5;
							float arg_B9F_4 = this.velocity.X;
							float arg_B9F_5 = 2f;
							int arg_B9F_6 = 0;
							Color newColor = default(Color);
							int num2 = Dust.NewDust(arg_B9F_0, arg_B9F_1, arg_B9F_2, arg_B9F_3, arg_B9F_4, arg_B9F_5, arg_B9F_6, newColor, 1f);
							Dust expr_BB1_cp_0 = Main.dust[num2];
							expr_BB1_cp_0.velocity.X = expr_BB1_cp_0.velocity.X * 0.5f;
							Dust expr_BCE_cp_0 = Main.dust[num2];
							expr_BCE_cp_0.velocity.Y = expr_BCE_cp_0.velocity.Y * 0.1f;
						}
						if (this.wet)
						{
							if (this.velocity.Y > 0f)
							{
								this.velocity.Y = this.velocity.Y * 0.95f;
							}
							this.velocity.Y = this.velocity.Y - 0.5f;
							if (this.velocity.Y < -4f)
							{
								this.velocity.Y = -4f;
							}
							this.TargetClosest(true);
							return;
						}
					}
					else
					{
						if (this.aiStyle == 3)
						{
							int num3 = 60;
							bool flag2 = false;
							if (this.velocity.Y == 0f && ((this.velocity.X > 0f && this.direction < 0) || (this.velocity.X < 0f && this.direction > 0)))
							{
								flag2 = true;
							}
							if (this.position.X == this.oldPosition.X || this.ai[3] >= (float)num3 || flag2)
							{
								this.ai[3] += 1f;
							}
							else
							{
								if ((double)Math.Abs(this.velocity.X) > 0.9 && this.ai[3] > 0f)
								{
									this.ai[3] -= 1f;
								}
							}
							if (this.ai[3] > (float)(num3 * 10))
							{
								this.ai[3] = 0f;
							}
							if (this.justHit)
							{
								this.ai[3] = 0f;
							}
							if (this.ai[3] == (float)num3)
							{
								this.netUpdate = true;
							}
							if ((!Main.dayTime || (double)this.position.Y > Main.worldSurface * 16.0 || this.type == 26 || this.type == 27 || this.type == 28 || this.type == 31 || this.type == 47 || this.type == 67 || this.type == 73) && this.ai[3] < (float)num3)
							{
								if ((this.type == 3 || this.type == 21 || this.type == 31) && Main.rand.Next(1000) == 0)
								{
									Main.PlaySound(14, (int)this.position.X, (int)this.position.Y, 1);
								}
								this.TargetClosest(true);
							}
							else
							{
								if (Main.dayTime && (double)(this.position.Y / 16f) < Main.worldSurface && this.timeLeft > 10)
								{
									this.timeLeft = 10;
								}
								if (this.velocity.X == 0f)
								{
									if (this.velocity.Y == 0f)
									{
										this.ai[0] += 1f;
										if (this.ai[0] >= 2f)
										{
											this.direction *= -1;
											this.spriteDirection = this.direction;
											this.ai[0] = 0f;
										}
									}
								}
								else
								{
									this.ai[0] = 0f;
								}
								if (this.direction == 0)
								{
									this.direction = 1;
								}
							}
							if (this.type == 27)
							{
								if (this.velocity.X < -2f || this.velocity.X > 2f)
								{
									if (this.velocity.Y == 0f)
									{
										this.velocity *= 0.8f;
									}
								}
								else
								{
									if (this.velocity.X < 2f && this.direction == 1)
									{
										this.velocity.X = this.velocity.X + 0.07f;
										if (this.velocity.X > 2f)
										{
											this.velocity.X = 2f;
										}
									}
									else
									{
										if (this.velocity.X > -2f && this.direction == -1)
										{
											this.velocity.X = this.velocity.X - 0.07f;
											if (this.velocity.X < -2f)
											{
												this.velocity.X = -2f;
											}
										}
									}
								}
							}
							else
							{
								if (this.type == 21 || this.type == 26 || this.type == 31 || this.type == 47 || this.type == 73)
								{
									if (this.velocity.X < -1.5f || this.velocity.X > 1.5f)
									{
										if (this.velocity.Y == 0f)
										{
											this.velocity *= 0.8f;
										}
									}
									else
									{
										if (this.velocity.X < 1.5f && this.direction == 1)
										{
											this.velocity.X = this.velocity.X + 0.07f;
											if (this.velocity.X > 1.5f)
											{
												this.velocity.X = 1.5f;
											}
										}
										else
										{
											if (this.velocity.X > -1.5f && this.direction == -1)
											{
												this.velocity.X = this.velocity.X - 0.07f;
												if (this.velocity.X < -1.5f)
												{
													this.velocity.X = -1.5f;
												}
											}
										}
									}
								}
								else
								{
									if (this.type == 67)
									{
										if (this.velocity.X < -0.5f || this.velocity.X > 0.5f)
										{
											if (this.velocity.Y == 0f)
											{
												this.velocity *= 0.7f;
											}
										}
										else
										{
											if (this.velocity.X < 0.5f && this.direction == 1)
											{
												this.velocity.X = this.velocity.X + 0.03f;
												if (this.velocity.X > 0.5f)
												{
													this.velocity.X = 0.5f;
												}
											}
											else
											{
												if (this.velocity.X > -0.5f && this.direction == -1)
												{
													this.velocity.X = this.velocity.X - 0.03f;
													if (this.velocity.X < -0.5f)
													{
														this.velocity.X = -0.5f;
													}
												}
											}
										}
									}
									else
									{
										if (this.velocity.X < -1f || this.velocity.X > 1f)
										{
											if (this.velocity.Y == 0f)
											{
												this.velocity *= 0.8f;
											}
										}
										else
										{
											if (this.velocity.X < 1f && this.direction == 1)
											{
												this.velocity.X = this.velocity.X + 0.07f;
												if (this.velocity.X > 1f)
												{
													this.velocity.X = 1f;
												}
											}
											else
											{
												if (this.velocity.X > -1f && this.direction == -1)
												{
													this.velocity.X = this.velocity.X - 0.07f;
													if (this.velocity.X < -1f)
													{
														this.velocity.X = -1f;
													}
												}
											}
										}
									}
								}
							}
							if (this.velocity.Y != 0f)
							{
								this.ai[1] = 0f;
								this.ai[2] = 0f;
								return;
							}
							int num4 = (int)((this.position.X + (float)(this.width / 2) + (float)(15 * this.direction)) / 16f);
							int num5 = (int)((this.position.Y + (float)this.height - 15f) / 16f);
							if (Main.tile[num4, num5] == null)
							{
								Main.tile[num4, num5] = new Tile();
							}
							if (Main.tile[num4, num5 - 1] == null)
							{
								Main.tile[num4, num5 - 1] = new Tile();
							}
							if (Main.tile[num4, num5 - 2] == null)
							{
								Main.tile[num4, num5 - 2] = new Tile();
							}
							if (Main.tile[num4, num5 - 3] == null)
							{
								Main.tile[num4, num5 - 3] = new Tile();
							}
							if (Main.tile[num4, num5 + 1] == null)
							{
								Main.tile[num4, num5 + 1] = new Tile();
							}
							if (Main.tile[num4 + this.direction, num5 - 1] == null)
							{
								Main.tile[num4 + this.direction, num5 - 1] = new Tile();
							}
							if (Main.tile[num4 + this.direction, num5 + 1] == null)
							{
								Main.tile[num4 + this.direction, num5 + 1] = new Tile();
							}
							bool flag3 = true;
							if (this.type == 47 || this.type == 67)
							{
								flag3 = false;
							}
							if (Main.tile[num4, num5 - 1].active && Main.tile[num4, num5 - 1].type == 10 && flag3)
							{
								this.ai[2] += 1f;
								this.ai[3] = 0f;
								if (this.ai[2] >= 60f)
								{
									if (!Main.bloodMoon && this.type == 3)
									{
										this.ai[1] = 0f;
									}
									this.velocity.X = 0.5f * (float)(-(float)this.direction);
									this.ai[1] += 1f;
									if (this.type == 27)
									{
										this.ai[1] += 1f;
									}
									if (this.type == 31)
									{
										this.ai[1] += 6f;
									}
									this.ai[2] = 0f;
									bool flag4 = false;
									if (this.ai[1] >= 10f)
									{
										flag4 = true;
										this.ai[1] = 10f;
									}
									WorldGen.KillTile(num4, num5 - 1, true, false, false);
									if ((Main.netMode != 1 || !flag4) && flag4 && Main.netMode != 1)
									{
										if (this.type == 26)
										{
											WorldGen.KillTile(num4, num5 - 1, false, false, false);
											if (Main.netMode == 2)
											{
												NetMessage.SendData(17, -1, -1, "", 0, (float)num4, (float)(num5 - 1), 0f, 0);
												return;
											}
										}
										else
										{
											bool flag5 = WorldGen.OpenDoor(num4, num5, this.direction);
											if (!flag5)
											{
												this.ai[3] = (float)num3;
												this.netUpdate = true;
											}
											if (Main.netMode == 2 && flag5)
											{
												NetMessage.SendData(19, -1, -1, "", 0, (float)num4, (float)num5, (float)this.direction, 0);
												return;
											}
										}
									}
								}
							}
							else
							{
								if ((this.velocity.X < 0f && this.spriteDirection == -1) || (this.velocity.X > 0f && this.spriteDirection == 1))
								{
									if (Main.tile[num4, num5 - 2].active && Main.tileSolid[(int)Main.tile[num4, num5 - 2].type])
									{
										if (Main.tile[num4, num5 - 3].active && Main.tileSolid[(int)Main.tile[num4, num5 - 3].type])
										{
											this.velocity.Y = -8f;
											this.netUpdate = true;
										}
										else
										{
											this.velocity.Y = -7f;
											this.netUpdate = true;
										}
									}
									else
									{
										if (Main.tile[num4, num5 - 1].active && Main.tileSolid[(int)Main.tile[num4, num5 - 1].type])
										{
											this.velocity.Y = -6f;
											this.netUpdate = true;
										}
										else
										{
											if (Main.tile[num4, num5].active && Main.tileSolid[(int)Main.tile[num4, num5].type])
											{
												this.velocity.Y = -5f;
												this.netUpdate = true;
											}
											else
											{
												if (this.directionY < 0 && this.type != 67 && (!Main.tile[num4, num5 + 1].active || !Main.tileSolid[(int)Main.tile[num4, num5 + 1].type]) && (!Main.tile[num4 + this.direction, num5 + 1].active || !Main.tileSolid[(int)Main.tile[num4 + this.direction, num5 + 1].type]))
												{
													this.velocity.Y = -8f;
													this.velocity.X = this.velocity.X * 1.5f;
													this.netUpdate = true;
												}
												else
												{
													this.ai[1] = 0f;
													this.ai[2] = 0f;
												}
											}
										}
									}
								}
								if ((this.type == 31 || this.type == 47) && this.velocity.Y == 0f && Math.Abs(this.position.X + (float)(this.width / 2) - (Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2))) < 100f && Math.Abs(this.position.Y + (float)(this.height / 2) - (Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2))) < 50f && ((this.direction > 0 && this.velocity.X >= 1f) || (this.direction < 0 && this.velocity.X <= -1f)))
								{
									this.velocity.X = this.velocity.X * 2f;
									if (this.velocity.X > 3f)
									{
										this.velocity.X = 3f;
									}
									if (this.velocity.X < -3f)
									{
										this.velocity.X = -3f;
									}
									this.velocity.Y = -4f;
									this.netUpdate = true;
									return;
								}
							}
						}
						else
						{
							if (this.aiStyle == 4)
							{
								if (this.target < 0 || this.target == 255 || Main.player[this.target].dead || !Main.player[this.target].active)
								{
									this.TargetClosest(true);
								}
								bool dead = Main.player[this.target].dead;
								float num6 = this.position.X + (float)(this.width / 2) - Main.player[this.target].position.X - (float)(Main.player[this.target].width / 2);
								float num7 = this.position.Y + (float)this.height - 59f - Main.player[this.target].position.Y - (float)(Main.player[this.target].height / 2);
								float num8 = (float)Math.Atan2((double)num7, (double)num6) + 1.57f;
								if (num8 < 0f)
								{
									num8 += 6.283f;
								}
								else
								{
									if ((double)num8 > 6.283)
									{
										num8 -= 6.283f;
									}
								}
								float num9 = 0f;
								if (this.ai[0] == 0f && this.ai[1] == 0f)
								{
									num9 = 0.02f;
								}
								if (this.ai[0] == 0f && this.ai[1] == 2f && this.ai[2] > 40f)
								{
									num9 = 0.05f;
								}
								if (this.ai[0] == 3f && this.ai[1] == 0f)
								{
									num9 = 0.05f;
								}
								if (this.ai[0] == 3f && this.ai[1] == 2f && this.ai[2] > 40f)
								{
									num9 = 0.08f;
								}
								if (this.rotation < num8)
								{
									if ((double)(num8 - this.rotation) > 3.1415)
									{
										this.rotation -= num9;
									}
									else
									{
										this.rotation += num9;
									}
								}
								else
								{
									if (this.rotation > num8)
									{
										if ((double)(this.rotation - num8) > 3.1415)
										{
											this.rotation += num9;
										}
										else
										{
											this.rotation -= num9;
										}
									}
								}
								if (this.rotation > num8 - num9 && this.rotation < num8 + num9)
								{
									this.rotation = num8;
								}
								if (this.rotation < 0f)
								{
									this.rotation += 6.283f;
								}
								else
								{
									if ((double)this.rotation > 6.283)
									{
										this.rotation -= 6.283f;
									}
								}
								if (this.rotation > num8 - num9 && this.rotation < num8 + num9)
								{
									this.rotation = num8;
								}
								if (Main.rand.Next(5) == 0)
								{
									Vector2 arg_1E97_0 = new Vector2(this.position.X, this.position.Y + (float)this.height * 0.25f);
									int arg_1E97_1 = this.width;
									int arg_1E97_2 = (int)((float)this.height * 0.5f);
									int arg_1E97_3 = 5;
									float arg_1E97_4 = this.velocity.X;
									float arg_1E97_5 = 2f;
									int arg_1E97_6 = 0;
									Color newColor = default(Color);
									int num10 = Dust.NewDust(arg_1E97_0, arg_1E97_1, arg_1E97_2, arg_1E97_3, arg_1E97_4, arg_1E97_5, arg_1E97_6, newColor, 1f);
									Dust expr_1EAB_cp_0 = Main.dust[num10];
									expr_1EAB_cp_0.velocity.X = expr_1EAB_cp_0.velocity.X * 0.5f;
									Dust expr_1EC9_cp_0 = Main.dust[num10];
									expr_1EC9_cp_0.velocity.Y = expr_1EC9_cp_0.velocity.Y * 0.1f;
								}
								if (Main.dayTime || dead)
								{
									this.velocity.Y = this.velocity.Y - 0.04f;
									if (this.timeLeft > 10)
									{
										this.timeLeft = 10;
										return;
									}
								}
								else
								{
									if (this.ai[0] == 0f)
									{
										if (this.ai[1] == 0f)
										{
											float num11 = 5f;
											float num12 = 0.04f;
											Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
											float num13 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector.X;
											float num14 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - 200f - vector.Y;
											float num15 = (float)Math.Sqrt((double)(num13 * num13 + num14 * num14));
											float num16 = num15;
											num15 = num11 / num15;
											num13 *= num15;
											num14 *= num15;
											if (this.velocity.X < num13)
											{
												this.velocity.X = this.velocity.X + num12;
												if (this.velocity.X < 0f && num13 > 0f)
												{
													this.velocity.X = this.velocity.X + num12;
												}
											}
											else
											{
												if (this.velocity.X > num13)
												{
													this.velocity.X = this.velocity.X - num12;
													if (this.velocity.X > 0f && num13 < 0f)
													{
														this.velocity.X = this.velocity.X - num12;
													}
												}
											}
											if (this.velocity.Y < num14)
											{
												this.velocity.Y = this.velocity.Y + num12;
												if (this.velocity.Y < 0f && num14 > 0f)
												{
													this.velocity.Y = this.velocity.Y + num12;
												}
											}
											else
											{
												if (this.velocity.Y > num14)
												{
													this.velocity.Y = this.velocity.Y - num12;
													if (this.velocity.Y > 0f && num14 < 0f)
													{
														this.velocity.Y = this.velocity.Y - num12;
													}
												}
											}
											this.ai[2] += 1f;
											if (this.ai[2] >= 600f)
											{
												this.ai[1] = 1f;
												this.ai[2] = 0f;
												this.ai[3] = 0f;
												this.target = 255;
												this.netUpdate = true;
											}
											else
											{
												if (this.position.Y + (float)this.height < Main.player[this.target].position.Y && num16 < 500f)
												{
													if (!Main.player[this.target].dead)
													{
														this.ai[3] += 1f;
													}
													if (this.ai[3] >= 110f)
													{
														this.ai[3] = 0f;
														this.rotation = num8;
														float num17 = 5f;
														float num18 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector.X;
														float num19 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector.Y;
														float num20 = (float)Math.Sqrt((double)(num18 * num18 + num19 * num19));
														num20 = num17 / num20;
														Vector2 vector2 = vector;
														Vector2 vector3;
														vector3.X = num18 * num20;
														vector3.Y = num19 * num20;
														vector2.X += vector3.X * 10f;
														vector2.Y += vector3.Y * 10f;
														if (Main.netMode != 1)
														{
															int num21 = NPC.NewNPC((int)vector2.X, (int)vector2.Y, 5, 0);
															Main.npc[num21].velocity.X = vector3.X;
															Main.npc[num21].velocity.Y = vector3.Y;
															if (Main.netMode == 2 && num21 < 1000)
															{
																NetMessage.SendData(23, -1, -1, "", num21, 0f, 0f, 0f, 0);
															}
														}
														Main.PlaySound(3, (int)vector2.X, (int)vector2.Y, 1);
														for (int i = 0; i < 10; i++)
														{
															Vector2 arg_2410_0 = vector2;
															int arg_2410_1 = 20;
															int arg_2410_2 = 20;
															int arg_2410_3 = 5;
															float arg_2410_4 = vector3.X * 0.4f;
															float arg_2410_5 = vector3.Y * 0.4f;
															int arg_2410_6 = 0;
															Color newColor = default(Color);
															Dust.NewDust(arg_2410_0, arg_2410_1, arg_2410_2, arg_2410_3, arg_2410_4, arg_2410_5, arg_2410_6, newColor, 1f);
														}
													}
												}
											}
										}
										else
										{
											if (this.ai[1] == 1f)
											{
												this.rotation = num8;
												float num22 = 6f;
												Vector2 vector4 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
												float num23 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector4.X;
												float num24 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector4.Y;
												float num25 = (float)Math.Sqrt((double)(num23 * num23 + num24 * num24));
												num25 = num22 / num25;
												this.velocity.X = num23 * num25;
												this.velocity.Y = num24 * num25;
												this.ai[1] = 2f;
											}
											else
											{
												if (this.ai[1] == 2f)
												{
													this.ai[2] += 1f;
													if (this.ai[2] >= 40f)
													{
														this.velocity.X = this.velocity.X * 0.98f;
														this.velocity.Y = this.velocity.Y * 0.98f;
														if ((double)this.velocity.X > -0.1 && (double)this.velocity.X < 0.1)
														{
															this.velocity.X = 0f;
														}
														if ((double)this.velocity.Y > -0.1 && (double)this.velocity.Y < 0.1)
														{
															this.velocity.Y = 0f;
														}
													}
													else
													{
														this.rotation = (float)Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) - 1.57f;
													}
													if (this.ai[2] >= 150f)
													{
														this.ai[3] += 1f;
														this.ai[2] = 0f;
														this.target = 255;
														this.rotation = num8;
														if (this.ai[3] >= 3f)
														{
															this.ai[1] = 0f;
															this.ai[3] = 0f;
														}
														else
														{
															this.ai[1] = 1f;
														}
													}
												}
											}
										}
										if ((double)this.life < (double)this.lifeMax * 0.5)
										{
											this.ai[0] = 1f;
											this.ai[1] = 0f;
											this.ai[2] = 0f;
											this.ai[3] = 0f;
											this.netUpdate = true;
											return;
										}
									}
									else
									{
										if (this.ai[0] == 1f || this.ai[0] == 2f)
										{
											if (this.ai[0] == 1f)
											{
												this.ai[2] += 0.005f;
												if ((double)this.ai[2] > 0.5)
												{
													this.ai[2] = 0.5f;
												}
											}
											else
											{
												this.ai[2] -= 0.005f;
												if (this.ai[2] < 0f)
												{
													this.ai[2] = 0f;
												}
											}
											this.rotation += this.ai[2];
											this.ai[1] += 1f;
											Color newColor;
											if (this.ai[1] == 100f)
											{
												this.ai[0] += 1f;
												this.ai[1] = 0f;
												if (this.ai[0] == 3f)
												{
													this.ai[2] = 0f;
												}
												else
												{
													Main.PlaySound(3, (int)this.position.X, (int)this.position.Y, 1);
													for (int j = 0; j < 2; j++)
													{
														Gore.NewGore(this.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 8, 1f);
														Gore.NewGore(this.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 7, 1f);
														Gore.NewGore(this.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 6, 1f);
													}
													for (int k = 0; k < 20; k++)
													{
														Vector2 arg_29AE_0 = this.position;
														int arg_29AE_1 = this.width;
														int arg_29AE_2 = this.height;
														int arg_29AE_3 = 5;
														float arg_29AE_4 = (float)Main.rand.Next(-30, 31) * 0.2f;
														float arg_29AE_5 = (float)Main.rand.Next(-30, 31) * 0.2f;
														int arg_29AE_6 = 0;
														newColor = default(Color);
														Dust.NewDust(arg_29AE_0, arg_29AE_1, arg_29AE_2, arg_29AE_3, arg_29AE_4, arg_29AE_5, arg_29AE_6, newColor, 1f);
													}
													Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
												}
											}
											Vector2 arg_2A2D_0 = this.position;
											int arg_2A2D_1 = this.width;
											int arg_2A2D_2 = this.height;
											int arg_2A2D_3 = 5;
											float arg_2A2D_4 = (float)Main.rand.Next(-30, 31) * 0.2f;
											float arg_2A2D_5 = (float)Main.rand.Next(-30, 31) * 0.2f;
											int arg_2A2D_6 = 0;
											newColor = default(Color);
											Dust.NewDust(arg_2A2D_0, arg_2A2D_1, arg_2A2D_2, arg_2A2D_3, arg_2A2D_4, arg_2A2D_5, arg_2A2D_6, newColor, 1f);
											this.velocity.X = this.velocity.X * 0.98f;
											this.velocity.Y = this.velocity.Y * 0.98f;
											if ((double)this.velocity.X > -0.1 && (double)this.velocity.X < 0.1)
											{
												this.velocity.X = 0f;
											}
											if ((double)this.velocity.Y > -0.1 && (double)this.velocity.Y < 0.1)
											{
												this.velocity.Y = 0f;
												return;
											}
										}
										else
										{
											this.damage = 23;
											this.defense = 0;
											if (this.ai[1] == 0f)
											{
												float num26 = 6f;
												float num27 = 0.07f;
												Vector2 vector5 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
												float num28 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector5.X;
												float num29 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - 120f - vector5.Y;
												float num30 = (float)Math.Sqrt((double)(num28 * num28 + num29 * num29));
												num30 = num26 / num30;
												num28 *= num30;
												num29 *= num30;
												if (this.velocity.X < num28)
												{
													this.velocity.X = this.velocity.X + num27;
													if (this.velocity.X < 0f && num28 > 0f)
													{
														this.velocity.X = this.velocity.X + num27;
													}
												}
												else
												{
													if (this.velocity.X > num28)
													{
														this.velocity.X = this.velocity.X - num27;
														if (this.velocity.X > 0f && num28 < 0f)
														{
															this.velocity.X = this.velocity.X - num27;
														}
													}
												}
												if (this.velocity.Y < num29)
												{
													this.velocity.Y = this.velocity.Y + num27;
													if (this.velocity.Y < 0f && num29 > 0f)
													{
														this.velocity.Y = this.velocity.Y + num27;
													}
												}
												else
												{
													if (this.velocity.Y > num29)
													{
														this.velocity.Y = this.velocity.Y - num27;
														if (this.velocity.Y > 0f && num29 < 0f)
														{
															this.velocity.Y = this.velocity.Y - num27;
														}
													}
												}
												this.ai[2] += 1f;
												if (this.ai[2] >= 200f)
												{
													this.ai[1] = 1f;
													this.ai[2] = 0f;
													this.ai[3] = 0f;
													this.target = 255;
													this.netUpdate = true;
													return;
												}
											}
											else
											{
												if (this.ai[1] == 1f)
												{
													Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
													this.rotation = num8;
													float num31 = 6.8f;
													Vector2 vector6 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
													float num32 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector6.X;
													float num33 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector6.Y;
													float num34 = (float)Math.Sqrt((double)(num32 * num32 + num33 * num33));
													num34 = num31 / num34;
													this.velocity.X = num32 * num34;
													this.velocity.Y = num33 * num34;
													this.ai[1] = 2f;
													return;
												}
												if (this.ai[1] == 2f)
												{
													this.ai[2] += 1f;
													if (this.ai[2] >= 40f)
													{
														this.velocity.X = this.velocity.X * 0.97f;
														this.velocity.Y = this.velocity.Y * 0.97f;
														if ((double)this.velocity.X > -0.1 && (double)this.velocity.X < 0.1)
														{
															this.velocity.X = 0f;
														}
														if ((double)this.velocity.Y > -0.1 && (double)this.velocity.Y < 0.1)
														{
															this.velocity.Y = 0f;
														}
													}
													else
													{
														this.rotation = (float)Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) - 1.57f;
													}
													if (this.ai[2] >= 130f)
													{
														this.ai[3] += 1f;
														this.ai[2] = 0f;
														this.target = 255;
														this.rotation = num8;
														if (this.ai[3] >= 3f)
														{
															this.ai[1] = 0f;
															this.ai[3] = 0f;
															return;
														}
														this.ai[1] = 1f;
														return;
													}
												}
											}
										}
									}
								}
							}
							else
							{
								if (this.aiStyle == 5)
								{
									if (this.target < 0 || this.target == 255 || Main.player[this.target].dead)
									{
										this.TargetClosest(true);
									}
									float num35 = 6f;
									float num36 = 0.05f;
									if (this.type == 6)
									{
										num35 = 4f;
										num36 = 0.02f;
									}
									else
									{
										if (this.type == 42)
										{
											num35 = 3.5f;
											num36 = 0.021f;
										}
										else
										{
											if (this.type == 23)
											{
												num35 = 1f;
												num36 = 0.03f;
											}
											else
											{
												if (this.type == 5)
												{
													num35 = 5f;
													num36 = 0.03f;
												}
											}
										}
									}
									Vector2 vector7 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
									float num37 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector7.X;
									float num38 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector7.Y;
									float num39 = (float)Math.Sqrt((double)(num37 * num37 + num38 * num38));
									float num40 = num39;
									num39 = num35 / num39;
									num37 *= num39;
									num38 *= num39;
									if (this.type == 6 || this.type == 42)
									{
										if (num40 > 100f || this.type == 42)
										{
											this.ai[0] += 1f;
											if (this.ai[0] > 0f)
											{
												this.velocity.Y = this.velocity.Y + 0.023f;
											}
											else
											{
												this.velocity.Y = this.velocity.Y - 0.023f;
											}
											if (this.ai[0] < -100f || this.ai[0] > 100f)
											{
												this.velocity.X = this.velocity.X + 0.023f;
											}
											else
											{
												this.velocity.X = this.velocity.X - 0.023f;
											}
											if (this.ai[0] > 200f)
											{
												this.ai[0] = -200f;
											}
										}
										if (num40 < 150f && this.type == 6)
										{
											this.velocity.X = this.velocity.X + num37 * 0.007f;
											this.velocity.Y = this.velocity.Y + num38 * 0.007f;
										}
									}
									if (Main.player[this.target].dead)
									{
										num37 = (float)this.direction * num35 / 2f;
										num38 = -num35 / 2f;
									}
									if (this.velocity.X < num37)
									{
										this.velocity.X = this.velocity.X + num36;
										if (this.type != 6 && this.type != 42 && this.velocity.X < 0f && num37 > 0f)
										{
											this.velocity.X = this.velocity.X + num36;
										}
									}
									else
									{
										if (this.velocity.X > num37)
										{
											this.velocity.X = this.velocity.X - num36;
											if (this.type != 6 && this.type != 42 && this.velocity.X > 0f && num37 < 0f)
											{
												this.velocity.X = this.velocity.X - num36;
											}
										}
									}
									if (this.velocity.Y < num38)
									{
										this.velocity.Y = this.velocity.Y + num36;
										if (this.type != 6 && this.type != 42 && this.velocity.Y < 0f && num38 > 0f)
										{
											this.velocity.Y = this.velocity.Y + num36;
										}
									}
									else
									{
										if (this.velocity.Y > num38)
										{
											this.velocity.Y = this.velocity.Y - num36;
											if (this.type != 6 && this.type != 42 && this.velocity.Y > 0f && num38 < 0f)
											{
												this.velocity.Y = this.velocity.Y - num36;
											}
										}
									}
									if (this.type == 23)
									{
										if (num37 > 0f)
										{
											this.spriteDirection = 1;
											this.rotation = (float)Math.Atan2((double)num38, (double)num37);
										}
										else
										{
											if (num37 < 0f)
											{
												this.spriteDirection = -1;
												this.rotation = (float)Math.Atan2((double)num38, (double)num37) + 3.14f;
											}
										}
									}
									else
									{
										if (this.type == 6)
										{
											this.rotation = (float)Math.Atan2((double)num38, (double)num37) - 1.57f;
										}
										else
										{
											if (this.type == 42)
											{
												if (num37 > 0f)
												{
													this.spriteDirection = 1;
												}
												if (num37 < 0f)
												{
													this.spriteDirection = -1;
												}
												this.rotation = this.velocity.X * 0.1f;
											}
											else
											{
												this.rotation = (float)Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) - 1.57f;
											}
										}
									}
									if (this.type == 6 || this.type == 23 || this.type == 42)
									{
										float num41 = 0.7f;
										if (this.type == 6)
										{
											num41 = 0.4f;
										}
										if (this.collideX)
										{
											this.netUpdate = true;
											this.velocity.X = this.oldVelocity.X * -num41;
											if (this.direction == -1 && this.velocity.X > 0f && this.velocity.X < 2f)
											{
												this.velocity.X = 2f;
											}
											if (this.direction == 1 && this.velocity.X < 0f && this.velocity.X > -2f)
											{
												this.velocity.X = -2f;
											}
											this.netUpdate = true;
										}
										if (this.collideY)
										{
											this.netUpdate = true;
											this.velocity.Y = this.oldVelocity.Y * -num41;
											if (this.velocity.Y > 0f && (double)this.velocity.Y < 1.5)
											{
												this.velocity.Y = 2f;
											}
											if (this.velocity.Y < 0f && (double)this.velocity.Y > -1.5)
											{
												this.velocity.Y = -2f;
											}
										}
										if (this.type == 23)
										{
											Vector2 arg_37E8_0 = new Vector2(this.position.X - this.velocity.X, this.position.Y - this.velocity.Y);
											int arg_37E8_1 = this.width;
											int arg_37E8_2 = this.height;
											int arg_37E8_3 = 6;
											float arg_37E8_4 = this.velocity.X * 0.2f;
											float arg_37E8_5 = this.velocity.Y * 0.2f;
											int arg_37E8_6 = 100;
											Color newColor = default(Color);
											int num42 = Dust.NewDust(arg_37E8_0, arg_37E8_1, arg_37E8_2, arg_37E8_3, arg_37E8_4, arg_37E8_5, arg_37E8_6, newColor, 2f);
											Main.dust[num42].noGravity = true;
											Dust expr_380A_cp_0 = Main.dust[num42];
											expr_380A_cp_0.velocity.X = expr_380A_cp_0.velocity.X * 0.3f;
											Dust expr_3828_cp_0 = Main.dust[num42];
											expr_3828_cp_0.velocity.Y = expr_3828_cp_0.velocity.Y * 0.3f;
										}
										else
										{
											if (this.type != 42 && Main.rand.Next(20) == 0)
											{
												int num43 = Dust.NewDust(new Vector2(this.position.X, this.position.Y + (float)this.height * 0.25f), this.width, (int)((float)this.height * 0.5f), 18, this.velocity.X, 2f, this.alpha, this.color, this.scale);
												Dust expr_38D1_cp_0 = Main.dust[num43];
												expr_38D1_cp_0.velocity.X = expr_38D1_cp_0.velocity.X * 0.5f;
												Dust expr_38EF_cp_0 = Main.dust[num43];
												expr_38EF_cp_0.velocity.Y = expr_38EF_cp_0.velocity.Y * 0.1f;
											}
										}
									}
									else
									{
										if (Main.rand.Next(40) == 0)
										{
											Vector2 arg_3974_0 = new Vector2(this.position.X, this.position.Y + (float)this.height * 0.25f);
											int arg_3974_1 = this.width;
											int arg_3974_2 = (int)((float)this.height * 0.5f);
											int arg_3974_3 = 5;
											float arg_3974_4 = this.velocity.X;
											float arg_3974_5 = 2f;
											int arg_3974_6 = 0;
											Color newColor = default(Color);
											int num44 = Dust.NewDust(arg_3974_0, arg_3974_1, arg_3974_2, arg_3974_3, arg_3974_4, arg_3974_5, arg_3974_6, newColor, 1f);
											Dust expr_3988_cp_0 = Main.dust[num44];
											expr_3988_cp_0.velocity.X = expr_3988_cp_0.velocity.X * 0.5f;
											Dust expr_39A6_cp_0 = Main.dust[num44];
											expr_39A6_cp_0.velocity.Y = expr_39A6_cp_0.velocity.Y * 0.1f;
										}
									}
									if (this.type == 6 && this.wet)
									{
										if (this.velocity.Y > 0f)
										{
											this.velocity.Y = this.velocity.Y * 0.95f;
										}
										this.velocity.Y = this.velocity.Y - 0.3f;
										if (this.velocity.Y < -2f)
										{
											this.velocity.Y = -2f;
										}
									}
									if (this.type == 42)
									{
										if (this.wet)
										{
											if (this.velocity.Y > 0f)
											{
												this.velocity.Y = this.velocity.Y * 0.95f;
											}
											this.velocity.Y = this.velocity.Y - 0.5f;
											if (this.velocity.Y < -4f)
											{
												this.velocity.Y = -4f;
											}
											this.TargetClosest(true);
										}
										if (this.ai[1] == 101f)
										{
											Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 17);
											this.ai[1] = 0f;
										}
										if (Main.netMode != 1)
										{
											this.ai[1] += (float)Main.rand.Next(5, 20) * 0.1f * this.scale;
											if (this.ai[1] >= 100f)
											{
												if (Collision.CanHit(this.position, this.width, this.height, Main.player[this.target].position, Main.player[this.target].width, Main.player[this.target].height))
												{
													float num45 = 8f;
													Vector2 vector8 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)(this.height / 2));
													float num46 = Main.player[this.target].position.X + (float)Main.player[this.target].width * 0.5f - vector8.X + (float)Main.rand.Next(-20, 21);
													float num47 = Main.player[this.target].position.Y + (float)Main.player[this.target].height * 0.5f - vector8.Y + (float)Main.rand.Next(-20, 21);
													if ((num46 < 0f && this.velocity.X < 0f) || (num46 > 0f && this.velocity.X > 0f))
													{
														float num48 = (float)Math.Sqrt((double)(num46 * num46 + num47 * num47));
														num48 = num45 / num48;
														num46 *= num48;
														num47 *= num48;
														int num49 = (int)(14f * this.scale);
														int num50 = 55;
														int num51 = Projectile.NewProjectile(vector8.X, vector8.Y, num46, num47, num50, num49, 0f, Main.myPlayer);
														Main.projectile[num51].timeLeft = 300;
														this.ai[1] = 101f;
														this.netUpdate = true;
													}
													else
													{
														this.ai[1] = 0f;
													}
												}
												else
												{
													this.ai[1] = 0f;
												}
											}
										}
									}
									if ((Main.dayTime && this.type != 6 && this.type != 23 && this.type != 42) || Main.player[this.target].dead)
									{
										this.velocity.Y = this.velocity.Y - num36 * 2f;
										if (this.timeLeft > 10)
										{
											this.timeLeft = 10;
											return;
										}
									}
								}
								else
								{
									if (this.aiStyle == 6)
									{
										if (this.target < 0 || this.target == 255 || Main.player[this.target].dead)
										{
											this.TargetClosest(true);
										}
										if (Main.player[this.target].dead && this.timeLeft > 10)
										{
											this.timeLeft = 10;
										}
										if (Main.netMode != 1)
										{
											if ((this.type == 7 || this.type == 8 || this.type == 10 || this.type == 11 || this.type == 13 || this.type == 14 || this.type == 39 || this.type == 40) && this.ai[0] == 0f)
											{
												if (this.type == 7 || this.type == 10 || this.type == 13 || this.type == 39)
												{
													this.ai[2] = (float)Main.rand.Next(8, 13);
													if (this.type == 10)
													{
														this.ai[2] = (float)Main.rand.Next(4, 7);
													}
													if (this.type == 13)
													{
														this.ai[2] = (float)Main.rand.Next(45, 56);
													}
													if (this.type == 39)
													{
														this.ai[2] = (float)Main.rand.Next(12, 19);
													}
													this.ai[0] = (float)NPC.NewNPC((int)(this.position.X + (float)(this.width / 2)), (int)(this.position.Y + (float)this.height), this.type + 1, this.whoAmI);
												}
												else
												{
													if ((this.type == 8 || this.type == 11 || this.type == 14 || this.type == 40) && this.ai[2] > 0f)
													{
														this.ai[0] = (float)NPC.NewNPC((int)(this.position.X + (float)(this.width / 2)), (int)(this.position.Y + (float)this.height), this.type, this.whoAmI);
													}
													else
													{
														this.ai[0] = (float)NPC.NewNPC((int)(this.position.X + (float)(this.width / 2)), (int)(this.position.Y + (float)this.height), this.type + 1, this.whoAmI);
													}
												}
												Main.npc[(int)this.ai[0]].ai[1] = (float)this.whoAmI;
												Main.npc[(int)this.ai[0]].ai[2] = this.ai[2] - 1f;
												this.netUpdate = true;
											}
											if ((this.type == 8 || this.type == 9 || this.type == 11 || this.type == 12 || this.type == 40 || this.type == 41) && (!Main.npc[(int)this.ai[1]].active || Main.npc[(int)this.ai[1]].aiStyle != this.aiStyle))
											{
												this.life = 0;
												this.HitEffect(0, 10.0);
												this.active = false;
											}
											if ((this.type == 7 || this.type == 8 || this.type == 10 || this.type == 11 || this.type == 39 || this.type == 40) && !Main.npc[(int)this.ai[0]].active)
											{
												this.life = 0;
												this.HitEffect(0, 10.0);
												this.active = false;
											}
											if (this.type == 13 || this.type == 14 || this.type == 15)
											{
												if (!Main.npc[(int)this.ai[1]].active && !Main.npc[(int)this.ai[0]].active)
												{
													this.life = 0;
													this.HitEffect(0, 10.0);
													this.active = false;
												}
												if (this.type == 13 && !Main.npc[(int)this.ai[0]].active)
												{
													this.life = 0;
													this.HitEffect(0, 10.0);
													this.active = false;
												}
												if (this.type == 15 && !Main.npc[(int)this.ai[1]].active)
												{
													this.life = 0;
													this.HitEffect(0, 10.0);
													this.active = false;
												}
												if (this.type == 14 && !Main.npc[(int)this.ai[1]].active)
												{
													this.type = 13;
													int num52 = this.whoAmI;
													float num53 = (float)this.life / (float)this.lifeMax;
													float num54 = this.ai[0];
													this.SetDefaults(this.type, -1f);
													this.life = (int)((float)this.lifeMax * num53);
													this.ai[0] = num54;
													this.TargetClosest(true);
													this.netUpdate = true;
													this.whoAmI = num52;
												}
												if (this.type == 14 && !Main.npc[(int)this.ai[0]].active)
												{
													int num55 = this.whoAmI;
													float num56 = (float)this.life / (float)this.lifeMax;
													float num57 = this.ai[1];
													this.SetDefaults(this.type, -1f);
													this.life = (int)((float)this.lifeMax * num56);
													this.ai[1] = num57;
													this.TargetClosest(true);
													this.netUpdate = true;
													this.whoAmI = num55;
												}
												if (this.life == 0)
												{
													bool flag6 = true;
													for (int l = 0; l < 1000; l++)
													{
														if (Main.npc[l].active && (Main.npc[l].type == 13 || Main.npc[l].type == 14 || Main.npc[l].type == 15))
														{
															flag6 = false;
															break;
														}
													}
													if (flag6)
													{
														this.boss = true;
														this.NPCLoot();
													}
												}
											}
											if (!this.active && Main.netMode == 2)
											{
												NetMessage.SendData(28, -1, -1, "", this.whoAmI, -1f, 0f, 0f, 0);
											}
										}
										int num58 = (int)(this.position.X / 16f) - 1;
										int num59 = (int)((this.position.X + (float)this.width) / 16f) + 2;
										int num60 = (int)(this.position.Y / 16f) - 1;
										int num61 = (int)((this.position.Y + (float)this.height) / 16f) + 2;
										if (num58 < 0)
										{
											num58 = 0;
										}
										if (num59 > Main.maxTilesX)
										{
											num59 = Main.maxTilesX;
										}
										if (num60 < 0)
										{
											num60 = 0;
										}
										if (num61 > Main.maxTilesY)
										{
											num61 = Main.maxTilesY;
										}
										bool flag7 = false;
										for (int m = num58; m < num59; m++)
										{
											for (int n = num60; n < num61; n++)
											{
												if (Main.tile[m, n] != null && ((Main.tile[m, n].active && (Main.tileSolid[(int)Main.tile[m, n].type] || (Main.tileSolidTop[(int)Main.tile[m, n].type] && Main.tile[m, n].frameY == 0))) || Main.tile[m, n].liquid > 64))
												{
													Vector2 vector9;
													vector9.X = (float)(m * 16);
													vector9.Y = (float)(n * 16);
													if (this.position.X + (float)this.width > vector9.X && this.position.X < vector9.X + 16f && this.position.Y + (float)this.height > vector9.Y && this.position.Y < vector9.Y + 16f)
													{
														flag7 = true;
														if (Main.rand.Next(40) == 0 && Main.tile[m, n].active)
														{
															WorldGen.KillTile(m, n, true, true, false);
														}
														if (Main.netMode != 1 && Main.tile[m, n].type == 2)
														{
															byte arg_4634_0 = Main.tile[m, n - 1].type;
														}
													}
												}
											}
										}
										if (!flag7 && (this.type == 7 || this.type == 10 || this.type == 13 || this.type == 39))
										{
											Rectangle rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height);
											int num62 = 1000;
											bool flag8 = true;
											for (int num63 = 0; num63 < 255; num63++)
											{
												if (Main.player[num63].active)
												{
													Rectangle rectangle2 = new Rectangle((int)Main.player[num63].position.X - num62, (int)Main.player[num63].position.Y - num62, num62 * 2, num62 * 2);
													if (rectangle.Intersects(rectangle2))
													{
														flag8 = false;
														break;
													}
												}
											}
											if (flag8)
											{
												flag7 = true;
											}
										}
										float num64 = 8f;
										float num65 = 0.07f;
										if (this.type == 10)
										{
											num64 = 6f;
											num65 = 0.05f;
										}
										if (this.type == 13)
										{
											num64 = 10f;
											num65 = 0.07f;
										}
										Vector2 vector10 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
										float num66 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector10.X;
										float num67 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector10.Y;
										float num68 = (float)Math.Sqrt((double)(num66 * num66 + num67 * num67));
										if (this.ai[1] > 0f)
										{
											num66 = Main.npc[(int)this.ai[1]].position.X + (float)(Main.npc[(int)this.ai[1]].width / 2) - vector10.X;
											num67 = Main.npc[(int)this.ai[1]].position.Y + (float)(Main.npc[(int)this.ai[1]].height / 2) - vector10.Y;
											this.rotation = (float)Math.Atan2((double)num67, (double)num66) + 1.57f;
											num68 = (float)Math.Sqrt((double)(num66 * num66 + num67 * num67));
											num68 = (num68 - (float)this.width) / num68;
											num66 *= num68;
											num67 *= num68;
											this.velocity = default(Vector2);
											this.position.X = this.position.X + num66;
											this.position.Y = this.position.Y + num67;
											return;
										}
										if (!flag7)
										{
											this.TargetClosest(true);
											this.velocity.Y = this.velocity.Y + 0.11f;
											if (this.velocity.Y > num64)
											{
												this.velocity.Y = num64;
											}
											if ((double)(Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y)) < (double)num64 * 0.4)
											{
												if (this.velocity.X < 0f)
												{
													this.velocity.X = this.velocity.X - num65 * 1.1f;
												}
												else
												{
													this.velocity.X = this.velocity.X + num65 * 1.1f;
												}
											}
											else
											{
												if (this.velocity.Y == num64)
												{
													if (this.velocity.X < num66)
													{
														this.velocity.X = this.velocity.X + num65;
													}
													else
													{
														if (this.velocity.X > num66)
														{
															this.velocity.X = this.velocity.X - num65;
														}
													}
												}
												else
												{
													if (this.velocity.Y > 4f)
													{
														if (this.velocity.X < 0f)
														{
															this.velocity.X = this.velocity.X + num65 * 0.9f;
														}
														else
														{
															this.velocity.X = this.velocity.X - num65 * 0.9f;
														}
													}
												}
											}
										}
										else
										{
											if (this.soundDelay == 0)
											{
												float num69 = num68 / 40f;
												if (num69 < 10f)
												{
													num69 = 10f;
												}
												if (num69 > 20f)
												{
													num69 = 20f;
												}
												this.soundDelay = (int)num69;
												Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 1);
											}
											num68 = (float)Math.Sqrt((double)(num66 * num66 + num67 * num67));
											float num70 = Math.Abs(num66);
											float num71 = Math.Abs(num67);
											num68 = num64 / num68;
											num66 *= num68;
											num67 *= num68;
											if ((this.type == 13 || this.type == 7) && !Main.player[this.target].zoneEvil)
											{
												bool flag9 = true;
												for (int num72 = 0; num72 < 255; num72++)
												{
													if (Main.player[num72].active && !Main.player[num72].dead && Main.player[num72].zoneEvil)
													{
														flag9 = false;
													}
												}
												if (flag9)
												{
													if (Main.netMode != 1 && (double)(this.position.Y / 16f) > (Main.rockLayer + (double)Main.maxTilesY) / 2.0)
													{
														this.active = false;
														int num73 = (int)this.ai[0];
														while (num73 > 0 && num73 < 1000 && Main.npc[num73].active && Main.npc[num73].aiStyle == this.aiStyle)
														{
															int num74 = (int)Main.npc[num73].ai[0];
															Main.npc[num73].active = false;
															this.life = 0;
															if (Main.netMode == 2)
															{
																NetMessage.SendData(23, -1, -1, "", num73, 0f, 0f, 0f, 0);
															}
															num73 = num74;
														}
														if (Main.netMode == 2)
														{
															NetMessage.SendData(23, -1, -1, "", this.whoAmI, 0f, 0f, 0f, 0);
														}
													}
													num66 = 0f;
													num67 = num64;
												}
											}
											if ((this.velocity.X > 0f && num66 > 0f) || (this.velocity.X < 0f && num66 < 0f) || (this.velocity.Y > 0f && num67 > 0f) || (this.velocity.Y < 0f && num67 < 0f))
											{
												if (this.velocity.X < num66)
												{
													this.velocity.X = this.velocity.X + num65;
												}
												else
												{
													if (this.velocity.X > num66)
													{
														this.velocity.X = this.velocity.X - num65;
													}
												}
												if (this.velocity.Y < num67)
												{
													this.velocity.Y = this.velocity.Y + num65;
												}
												else
												{
													if (this.velocity.Y > num67)
													{
														this.velocity.Y = this.velocity.Y - num65;
													}
												}
											}
											else
											{
												if (num70 > num71)
												{
													if (this.velocity.X < num66)
													{
														this.velocity.X = this.velocity.X + num65 * 1.1f;
													}
													else
													{
														if (this.velocity.X > num66)
														{
															this.velocity.X = this.velocity.X - num65 * 1.1f;
														}
													}
													if ((double)(Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y)) < (double)num64 * 0.5)
													{
														if (this.velocity.Y > 0f)
														{
															this.velocity.Y = this.velocity.Y + num65;
														}
														else
														{
															this.velocity.Y = this.velocity.Y - num65;
														}
													}
												}
												else
												{
													if (this.velocity.Y < num67)
													{
														this.velocity.Y = this.velocity.Y + num65 * 1.1f;
													}
													else
													{
														if (this.velocity.Y > num67)
														{
															this.velocity.Y = this.velocity.Y - num65 * 1.1f;
														}
													}
													if ((double)(Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y)) < (double)num64 * 0.5)
													{
														if (this.velocity.X > 0f)
														{
															this.velocity.X = this.velocity.X + num65;
														}
														else
														{
															this.velocity.X = this.velocity.X - num65;
														}
													}
												}
											}
										}
										this.rotation = (float)Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) + 1.57f;
										return;
									}
									else
									{
										if (this.aiStyle == 7)
										{
											int num75 = (int)(this.position.X + (float)(this.width / 2)) / 16;
											int num76 = (int)(this.position.Y + (float)this.height + 1f) / 16;
											if (Main.netMode == 1 || !this.townNPC)
											{
												this.homeTileX = num75;
												this.homeTileY = num76;
											}
											if (this.type == 46 && this.target == 255)
											{
												this.TargetClosest(true);
											}
											bool flag10 = false;
											this.directionY = -1;
											if (this.direction == 0)
											{
												this.direction = 1;
											}
											for (int num77 = 0; num77 < 255; num77++)
											{
												if (Main.player[num77].active && Main.player[num77].talkNPC == this.whoAmI)
												{
													flag10 = true;
													if (this.ai[0] != 0f)
													{
														this.netUpdate = true;
													}
													this.ai[0] = 0f;
													this.ai[1] = 300f;
													this.ai[2] = 100f;
													if (Main.player[num77].position.X + (float)(Main.player[num77].width / 2) < this.position.X + (float)(this.width / 2))
													{
														this.direction = -1;
													}
													else
													{
														this.direction = 1;
													}
												}
											}
											if (this.ai[3] > 0f)
											{
												this.life = -1;
												this.HitEffect(0, 10.0);
												this.active = false;
												if (this.type == 37)
												{
													Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
												}
											}
											if (this.type == 37 && Main.netMode != 1)
											{
												this.homeless = false;
												this.homeTileX = Main.dungeonX;
												this.homeTileY = Main.dungeonY;
												if (NPC.downedBoss3)
												{
													this.ai[3] = 1f;
													this.netUpdate = true;
												}
											}
											if (Main.netMode != 1 && this.townNPC && !Main.dayTime && (num75 != this.homeTileX || num76 != this.homeTileY) && !this.homeless)
											{
												bool flag11 = true;
												for (int num78 = 0; num78 < 2; num78++)
												{
													Rectangle rectangle3 = new Rectangle((int)(this.position.X + (float)(this.width / 2) - (float)(NPC.sWidth / 2) - (float)NPC.safeRangeX), (int)(this.position.Y + (float)(this.height / 2) - (float)(NPC.sHeight / 2) - (float)NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
													if (num78 == 1)
													{
														rectangle3 = new Rectangle(this.homeTileX * 16 + 8 - NPC.sWidth / 2 - NPC.safeRangeX, this.homeTileY * 16 + 8 - NPC.sHeight / 2 - NPC.safeRangeY, NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
													}
													for (int num79 = 0; num79 < 255; num79++)
													{
														if (Main.player[num79].active)
														{
															Rectangle rectangle4 = new Rectangle((int)Main.player[num79].position.X, (int)Main.player[num79].position.Y, Main.player[num79].width, Main.player[num79].height);
															if (rectangle4.Intersects(rectangle3))
															{
																flag11 = false;
																break;
															}
														}
														if (!flag11)
														{
															break;
														}
													}
												}
												if (flag11)
												{
													if (this.type == 37 || !Collision.SolidTiles(this.homeTileX - 1, this.homeTileX + 1, this.homeTileY - 3, this.homeTileY - 1))
													{
														this.velocity.X = 0f;
														this.velocity.Y = 0f;
														this.position.X = (float)(this.homeTileX * 16 + 8 - this.width / 2);
														this.position.Y = (float)(this.homeTileY * 16 - this.height) - 0.1f;
														this.netUpdate = true;
													}
													else
													{
														this.homeless = true;
														WorldGen.QuickFindHome(this.whoAmI);
													}
												}
											}
											if (this.ai[0] == 0f)
											{
												if (this.ai[2] > 0f)
												{
													this.ai[2] -= 1f;
												}
												if (!Main.dayTime && !flag10)
												{
													if (Main.netMode != 1)
													{
														if (num75 == this.homeTileX && num76 == this.homeTileY)
														{
															if (this.velocity.X != 0f)
															{
																this.netUpdate = true;
															}
															if ((double)this.velocity.X > 0.1)
															{
																this.velocity.X = this.velocity.X - 0.1f;
															}
															else
															{
																if ((double)this.velocity.X < -0.1)
																{
																	this.velocity.X = this.velocity.X + 0.1f;
																}
																else
																{
																	this.velocity.X = 0f;
																}
															}
														}
														else
														{
															if (!flag10)
															{
																if (num75 > this.homeTileX)
																{
																	this.direction = -1;
																}
																else
																{
																	this.direction = 1;
																}
																this.ai[0] = 1f;
																this.ai[1] = (float)(200 + Main.rand.Next(200));
																this.ai[2] = 0f;
																this.netUpdate = true;
															}
														}
													}
												}
												else
												{
													if ((double)this.velocity.X > 0.1)
													{
														this.velocity.X = this.velocity.X - 0.1f;
													}
													else
													{
														if ((double)this.velocity.X < -0.1)
														{
															this.velocity.X = this.velocity.X + 0.1f;
														}
														else
														{
															this.velocity.X = 0f;
														}
													}
													if (Main.netMode != 1)
													{
														if (this.ai[1] > 0f)
														{
															this.ai[1] -= 1f;
														}
														if (this.ai[1] <= 0f)
														{
															this.ai[0] = 1f;
															this.ai[1] = (float)(200 + Main.rand.Next(200));
															if (this.type == 46)
															{
																this.ai[1] += (float)Main.rand.Next(200, 400);
															}
															this.ai[2] = 0f;
															this.netUpdate = true;
														}
													}
												}
												if (Main.netMode != 1 && (Main.dayTime || (num75 == this.homeTileX && num76 == this.homeTileY)))
												{
													if (num75 < this.homeTileX - 25 || num75 > this.homeTileX + 25)
													{
														if (this.ai[2] == 0f)
														{
															if (num75 < this.homeTileX - 50 && this.direction == -1)
															{
																this.direction = 1;
																this.netUpdate = true;
																return;
															}
															if (num75 > this.homeTileX + 50 && this.direction == 1)
															{
																this.direction = -1;
																this.netUpdate = true;
																return;
															}
														}
													}
													else
													{
														if (Main.rand.Next(80) == 0 && this.ai[2] == 0f)
														{
															this.ai[2] = 200f;
															this.direction *= -1;
															this.netUpdate = true;
															return;
														}
													}
												}
											}
											else
											{
												if (this.ai[0] == 1f)
												{
													if (Main.netMode != 1 && !Main.dayTime && num75 == this.homeTileX && num76 == this.homeTileY)
													{
														this.ai[0] = 0f;
														this.ai[1] = (float)(200 + Main.rand.Next(200));
														this.ai[2] = 60f;
														this.netUpdate = true;
														return;
													}
													if (Main.netMode != 1 && !this.homeless && (num75 < this.homeTileX - 35 || num75 > this.homeTileX + 35))
													{
														if (this.position.X < (float)(this.homeTileX * 16) && this.direction == -1)
														{
															this.direction = 1;
															this.netUpdate = true;
															this.ai[1] = 0f;
														}
														else
														{
															if (this.position.X > (float)(this.homeTileX * 16) && this.direction == 1)
															{
																this.direction = -1;
																this.netUpdate = true;
																this.ai[1] = 0f;
															}
														}
													}
													this.ai[1] -= 1f;
													if (this.ai[1] <= 0f)
													{
														this.ai[0] = 0f;
														this.ai[1] = (float)(300 + Main.rand.Next(300));
														if (this.type == 46)
														{
															this.ai[1] -= (float)Main.rand.Next(100);
														}
														this.ai[2] = 60f;
														this.netUpdate = true;
													}
													if (this.closeDoor && ((this.position.X + (float)(this.width / 2)) / 16f > (float)(this.doorX + 2) || (this.position.X + (float)(this.width / 2)) / 16f < (float)(this.doorX - 2)))
													{
														bool flag12 = WorldGen.CloseDoor(this.doorX, this.doorY, false);
														if (flag12)
														{
															this.closeDoor = false;
															NetMessage.SendData(19, -1, -1, "", 1, (float)this.doorX, (float)this.doorY, (float)this.direction, 0);
														}
														if ((this.position.X + (float)(this.width / 2)) / 16f > (float)(this.doorX + 4) || (this.position.X + (float)(this.width / 2)) / 16f < (float)(this.doorX - 4) || (this.position.Y + (float)(this.height / 2)) / 16f > (float)(this.doorY + 4) || (this.position.Y + (float)(this.height / 2)) / 16f < (float)(this.doorY - 4))
														{
															this.closeDoor = false;
														}
													}
													if (this.velocity.X < -1f || this.velocity.X > 1f)
													{
														if (this.velocity.Y == 0f)
														{
															this.velocity *= 0.8f;
														}
													}
													else
													{
														if ((double)this.velocity.X < 1.15 && this.direction == 1)
														{
															this.velocity.X = this.velocity.X + 0.07f;
															if (this.velocity.X > 1f)
															{
																this.velocity.X = 1f;
															}
														}
														else
														{
															if (this.velocity.X > -1f && this.direction == -1)
															{
																this.velocity.X = this.velocity.X - 0.07f;
																if (this.velocity.X > 1f)
																{
																	this.velocity.X = 1f;
																}
															}
														}
													}
													if (this.velocity.Y == 0f)
													{
														if (this.position.X == this.ai[2])
														{
															this.direction *= -1;
														}
														this.ai[2] = -1f;
														int num80 = (int)((this.position.X + (float)(this.width / 2) + (float)(15 * this.direction)) / 16f);
														int num81 = (int)((this.position.Y + (float)this.height - 16f) / 16f);
														if (Main.tile[num80, num81] == null)
														{
															Main.tile[num80, num81] = new Tile();
														}
														if (Main.tile[num80, num81 - 1] == null)
														{
															Main.tile[num80, num81 - 1] = new Tile();
														}
														if (Main.tile[num80, num81 - 2] == null)
														{
															Main.tile[num80, num81 - 2] = new Tile();
														}
														if (Main.tile[num80, num81 - 3] == null)
														{
															Main.tile[num80, num81 - 3] = new Tile();
														}
														if (Main.tile[num80, num81 + 1] == null)
														{
															Main.tile[num80, num81 + 1] = new Tile();
														}
														if (Main.tile[num80 + this.direction, num81 - 1] == null)
														{
															Main.tile[num80 + this.direction, num81 - 1] = new Tile();
														}
														if (Main.tile[num80 + this.direction, num81 + 1] == null)
														{
															Main.tile[num80 + this.direction, num81 + 1] = new Tile();
														}
														if (this.townNPC && Main.tile[num80, num81 - 2].active && Main.tile[num80, num81 - 2].type == 10 && (Main.rand.Next(10) == 0 || !Main.dayTime))
														{
															if (Main.netMode != 1)
															{
																bool flag13 = WorldGen.OpenDoor(num80, num81 - 2, this.direction);
																if (flag13)
																{
																	this.closeDoor = true;
																	this.doorX = num80;
																	this.doorY = num81 - 2;
																	NetMessage.SendData(19, -1, -1, "", 0, (float)num80, (float)(num81 - 2), (float)this.direction, 0);
																	this.netUpdate = true;
																	this.ai[1] += 80f;
																	return;
																}
																if (WorldGen.OpenDoor(num80, num81 - 2, -this.direction))
																{
																	this.closeDoor = true;
																	this.doorX = num80;
																	this.doorY = num81 - 2;
																	NetMessage.SendData(19, -1, -1, "", 0, (float)num80, (float)(num81 - 2), (float)(-(float)this.direction), 0);
																	this.netUpdate = true;
																	this.ai[1] += 80f;
																	return;
																}
																this.direction *= -1;
																this.netUpdate = true;
																return;
															}
														}
														else
														{
															if ((this.velocity.X < 0f && this.spriteDirection == -1) || (this.velocity.X > 0f && this.spriteDirection == 1))
															{
																if (Main.tile[num80, num81 - 2].active && Main.tileSolid[(int)Main.tile[num80, num81 - 2].type] && !Main.tileSolidTop[(int)Main.tile[num80, num81 - 2].type])
																{
																	if ((this.direction == 1 && !Collision.SolidTiles(num80 - 2, num80 - 1, num81 - 5, num81 - 1)) || (this.direction == -1 && !Collision.SolidTiles(num80 + 1, num80 + 2, num81 - 5, num81 - 1)))
																	{
																		if (!Collision.SolidTiles(num80, num80, num81 - 5, num81 - 3))
																		{
																			this.velocity.Y = -6f;
																			this.netUpdate = true;
																		}
																		else
																		{
																			this.direction *= -1;
																			this.netUpdate = true;
																		}
																	}
																	else
																	{
																		this.direction *= -1;
																		this.netUpdate = true;
																	}
																}
																else
																{
																	if (Main.tile[num80, num81 - 1].active && Main.tileSolid[(int)Main.tile[num80, num81 - 1].type] && !Main.tileSolidTop[(int)Main.tile[num80, num81 - 1].type])
																	{
																		if ((this.direction == 1 && !Collision.SolidTiles(num80 - 2, num80 - 1, num81 - 4, num81 - 1)) || (this.direction == -1 && !Collision.SolidTiles(num80 + 1, num80 + 2, num81 - 4, num81 - 1)))
																		{
																			if (!Collision.SolidTiles(num80, num80, num81 - 4, num81 - 2))
																			{
																				this.velocity.Y = -5f;
																				this.netUpdate = true;
																			}
																			else
																			{
																				this.direction *= -1;
																				this.netUpdate = true;
																			}
																		}
																		else
																		{
																			this.direction *= -1;
																			this.netUpdate = true;
																		}
																	}
																	else
																	{
																		if (Main.tile[num80, num81].active && Main.tileSolid[(int)Main.tile[num80, num81].type] && !Main.tileSolidTop[(int)Main.tile[num80, num81].type])
																		{
																			if ((this.direction == 1 && !Collision.SolidTiles(num80 - 2, num80, num81 - 3, num81 - 1)) || (this.direction == -1 && !Collision.SolidTiles(num80, num80 + 2, num81 - 3, num81 - 1)))
																			{
																				this.velocity.Y = -3.6f;
																				this.netUpdate = true;
																			}
																			else
																			{
																				this.direction *= -1;
																				this.netUpdate = true;
																			}
																		}
																	}
																}
																try
																{
																	if (Main.tile[num80, num81 + 1] == null)
																	{
																		Main.tile[num80, num81 + 1] = new Tile();
																	}
																	if (Main.tile[num80 - this.direction, num81 + 1] == null)
																	{
																		Main.tile[num80 - this.direction, num81 + 1] = new Tile();
																	}
																	if (Main.tile[num80, num81 + 2] == null)
																	{
																		Main.tile[num80, num81 + 2] = new Tile();
																	}
																	if (Main.tile[num80 - this.direction, num81 + 2] == null)
																	{
																		Main.tile[num80 - this.direction, num81 + 2] = new Tile();
																	}
																	if (Main.tile[num80, num81 + 3] == null)
																	{
																		Main.tile[num80, num81 + 3] = new Tile();
																	}
																	if (Main.tile[num80 - this.direction, num81 + 3] == null)
																	{
																		Main.tile[num80 - this.direction, num81 + 3] = new Tile();
																	}
																	if (Main.tile[num80, num81 + 4] == null)
																	{
																		Main.tile[num80, num81 + 4] = new Tile();
																	}
																	if (Main.tile[num80 - this.direction, num81 + 4] == null)
																	{
																		Main.tile[num80 - this.direction, num81 + 4] = new Tile();
																	}
																	else
																	{
																		if (num75 >= this.homeTileX - 35 && num75 <= this.homeTileX + 35 && (!Main.tile[num80, num81 + 1].active || !Main.tileSolid[(int)Main.tile[num80, num81 + 1].type]) && (!Main.tile[num80 - this.direction, num81 + 1].active || !Main.tileSolid[(int)Main.tile[num80 - this.direction, num81 + 1].type]) && (!Main.tile[num80, num81 + 2].active || !Main.tileSolid[(int)Main.tile[num80, num81 + 2].type]) && (!Main.tile[num80 - this.direction, num81 + 2].active || !Main.tileSolid[(int)Main.tile[num80 - this.direction, num81 + 2].type]) && (!Main.tile[num80, num81 + 3].active || !Main.tileSolid[(int)Main.tile[num80, num81 + 3].type]) && (!Main.tile[num80 - this.direction, num81 + 3].active || !Main.tileSolid[(int)Main.tile[num80 - this.direction, num81 + 3].type]) && (!Main.tile[num80, num81 + 4].active || !Main.tileSolid[(int)Main.tile[num80, num81 + 4].type]) && (!Main.tile[num80 - this.direction, num81 + 4].active || !Main.tileSolid[(int)Main.tile[num80 - this.direction, num81 + 4].type]) && this.type != 46)
																		{
																			this.direction *= -1;
																			this.velocity.X = this.velocity.X * -1f;
																			this.netUpdate = true;
																		}
																	}
																}
																catch
																{
																}
																if (this.velocity.Y < 0f)
																{
																	this.ai[2] = this.position.X;
																}
															}
															if (this.velocity.Y < 0f && this.wet)
															{
																this.velocity.Y = this.velocity.Y * 1.2f;
															}
															if (this.velocity.Y < 0f && this.type == 46)
															{
																this.velocity.Y = this.velocity.Y * 1.2f;
																return;
															}
														}
													}
												}
											}
										}
										else
										{
											if (this.aiStyle == 8)
											{
												this.TargetClosest(true);
												this.velocity.X = this.velocity.X * 0.93f;
												if ((double)this.velocity.X > -0.1 && (double)this.velocity.X < 0.1)
												{
													this.velocity.X = 0f;
												}
												if (this.ai[0] == 0f)
												{
													this.ai[0] = 500f;
												}
												if (this.ai[2] != 0f && this.ai[3] != 0f)
												{
													Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 8);
													for (int num82 = 0; num82 < 50; num82++)
													{
														if (this.type == 29 || this.type == 45)
														{
															Vector2 arg_66FE_0 = new Vector2(this.position.X, this.position.Y);
															int arg_66FE_1 = this.width;
															int arg_66FE_2 = this.height;
															int arg_66FE_3 = 27;
															float arg_66FE_4 = 0f;
															float arg_66FE_5 = 0f;
															int arg_66FE_6 = 100;
															Color newColor = default(Color);
															int num83 = Dust.NewDust(arg_66FE_0, arg_66FE_1, arg_66FE_2, arg_66FE_3, arg_66FE_4, arg_66FE_5, arg_66FE_6, newColor, (float)Main.rand.Next(1, 3));
															Dust expr_670D = Main.dust[num83];
															expr_670D.velocity *= 3f;
															if (Main.dust[num83].scale > 1f)
															{
																Main.dust[num83].noGravity = true;
															}
														}
														else
														{
															if (this.type == 32)
															{
																Vector2 arg_679A_0 = new Vector2(this.position.X, this.position.Y);
																int arg_679A_1 = this.width;
																int arg_679A_2 = this.height;
																int arg_679A_3 = 29;
																float arg_679A_4 = 0f;
																float arg_679A_5 = 0f;
																int arg_679A_6 = 100;
																Color newColor = default(Color);
																int num84 = Dust.NewDust(arg_679A_0, arg_679A_1, arg_679A_2, arg_679A_3, arg_679A_4, arg_679A_5, arg_679A_6, newColor, 2.5f);
																Dust expr_67A9 = Main.dust[num84];
																expr_67A9.velocity *= 3f;
																Main.dust[num84].noGravity = true;
															}
															else
															{
																Vector2 arg_6811_0 = new Vector2(this.position.X, this.position.Y);
																int arg_6811_1 = this.width;
																int arg_6811_2 = this.height;
																int arg_6811_3 = 6;
																float arg_6811_4 = 0f;
																float arg_6811_5 = 0f;
																int arg_6811_6 = 100;
																Color newColor = default(Color);
																int num85 = Dust.NewDust(arg_6811_0, arg_6811_1, arg_6811_2, arg_6811_3, arg_6811_4, arg_6811_5, arg_6811_6, newColor, 2.5f);
																Dust expr_6820 = Main.dust[num85];
																expr_6820.velocity *= 3f;
																Main.dust[num85].noGravity = true;
															}
														}
													}
													this.position.X = this.ai[2] * 16f - (float)(this.width / 2) + 8f;
													this.position.Y = this.ai[3] * 16f - (float)this.height;
													this.velocity.X = 0f;
													this.velocity.Y = 0f;
													this.ai[2] = 0f;
													this.ai[3] = 0f;
													Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 8);
													for (int num86 = 0; num86 < 50; num86++)
													{
														if (this.type == 29 || this.type == 45)
														{
															Vector2 arg_6960_0 = new Vector2(this.position.X, this.position.Y);
															int arg_6960_1 = this.width;
															int arg_6960_2 = this.height;
															int arg_6960_3 = 27;
															float arg_6960_4 = 0f;
															float arg_6960_5 = 0f;
															int arg_6960_6 = 100;
															Color newColor = default(Color);
															int num87 = Dust.NewDust(arg_6960_0, arg_6960_1, arg_6960_2, arg_6960_3, arg_6960_4, arg_6960_5, arg_6960_6, newColor, (float)Main.rand.Next(1, 3));
															Dust expr_696F = Main.dust[num87];
															expr_696F.velocity *= 3f;
															if (Main.dust[num87].scale > 1f)
															{
																Main.dust[num87].noGravity = true;
															}
														}
														else
														{
															if (this.type == 32)
															{
																Vector2 arg_69FC_0 = new Vector2(this.position.X, this.position.Y);
																int arg_69FC_1 = this.width;
																int arg_69FC_2 = this.height;
																int arg_69FC_3 = 29;
																float arg_69FC_4 = 0f;
																float arg_69FC_5 = 0f;
																int arg_69FC_6 = 100;
																Color newColor = default(Color);
																int num88 = Dust.NewDust(arg_69FC_0, arg_69FC_1, arg_69FC_2, arg_69FC_3, arg_69FC_4, arg_69FC_5, arg_69FC_6, newColor, 2.5f);
																Dust expr_6A0B = Main.dust[num88];
																expr_6A0B.velocity *= 3f;
																Main.dust[num88].noGravity = true;
															}
															else
															{
																Vector2 arg_6A73_0 = new Vector2(this.position.X, this.position.Y);
																int arg_6A73_1 = this.width;
																int arg_6A73_2 = this.height;
																int arg_6A73_3 = 6;
																float arg_6A73_4 = 0f;
																float arg_6A73_5 = 0f;
																int arg_6A73_6 = 100;
																Color newColor = default(Color);
																int num89 = Dust.NewDust(arg_6A73_0, arg_6A73_1, arg_6A73_2, arg_6A73_3, arg_6A73_4, arg_6A73_5, arg_6A73_6, newColor, 2.5f);
																Dust expr_6A82 = Main.dust[num89];
																expr_6A82.velocity *= 3f;
																Main.dust[num89].noGravity = true;
															}
														}
													}
												}
												this.ai[0] += 1f;
												if (this.ai[0] == 100f || this.ai[0] == 200f || this.ai[0] == 300f)
												{
													this.ai[1] = 30f;
													this.netUpdate = true;
												}
												else
												{
													if (this.ai[0] >= 650f && Main.netMode != 1)
													{
														this.ai[0] = 1f;
														int num90 = (int)Main.player[this.target].position.X / 16;
														int num91 = (int)Main.player[this.target].position.Y / 16;
														int num92 = (int)this.position.X / 16;
														int num93 = (int)this.position.Y / 16;
														int num94 = 20;
														int num95 = 0;
														bool flag14 = false;
														if (Math.Abs(this.position.X - Main.player[this.target].position.X) + Math.Abs(this.position.Y - Main.player[this.target].position.Y) > 2000f)
														{
															num95 = 100;
															flag14 = true;
														}
														while (!flag14 && num95 < 100)
														{
															num95++;
															int num96 = Main.rand.Next(num90 - num94, num90 + num94);
															int num97 = Main.rand.Next(num91 - num94, num91 + num94);
															for (int num98 = num97; num98 < num91 + num94; num98++)
															{
																if ((num98 < num91 - 4 || num98 > num91 + 4 || num96 < num90 - 4 || num96 > num90 + 4) && (num98 < num93 - 1 || num98 > num93 + 1 || num96 < num92 - 1 || num96 > num92 + 1) && Main.tile[num96, num98].active)
																{
																	bool flag15 = true;
																	if (this.type == 32 && Main.tile[num96, num98 - 1].wall == 0)
																	{
																		flag15 = false;
																	}
																	else
																	{
																		if (Main.tile[num96, num98 - 1].lava)
																		{
																			flag15 = false;
																		}
																	}
																	if (flag15 && Main.tileSolid[(int)Main.tile[num96, num98].type] && !Collision.SolidTiles(num96 - 1, num96 + 1, num98 - 4, num98 - 1))
																	{
																		this.ai[1] = 20f;
																		this.ai[2] = (float)num96;
																		this.ai[3] = (float)num98;
																		flag14 = true;
																		break;
																	}
																}
															}
														}
														this.netUpdate = true;
													}
												}
												if (this.ai[1] > 0f)
												{
													this.ai[1] -= 1f;
													if (this.ai[1] == 25f)
													{
														Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 8);
														if (Main.netMode != 1)
														{
															if (this.type == 29 || this.type == 45)
															{
																NPC.NewNPC((int)this.position.X + this.width / 2, (int)this.position.Y - 8, 30, 0);
															}
															else
															{
																if (this.type == 32)
																{
																	NPC.NewNPC((int)this.position.X + this.width / 2, (int)this.position.Y - 8, 33, 0);
																}
																else
																{
																	NPC.NewNPC((int)this.position.X + this.width / 2 + this.direction * 8, (int)this.position.Y + 20, 25, 0);
																}
															}
														}
													}
												}
												if (this.type == 29 || this.type == 45)
												{
													if (Main.rand.Next(5) == 0)
													{
														Vector2 arg_6F0E_0 = new Vector2(this.position.X, this.position.Y + 2f);
														int arg_6F0E_1 = this.width;
														int arg_6F0E_2 = this.height;
														int arg_6F0E_3 = 27;
														float arg_6F0E_4 = this.velocity.X * 0.2f;
														float arg_6F0E_5 = this.velocity.Y * 0.2f;
														int arg_6F0E_6 = 100;
														Color newColor = default(Color);
														int num99 = Dust.NewDust(arg_6F0E_0, arg_6F0E_1, arg_6F0E_2, arg_6F0E_3, arg_6F0E_4, arg_6F0E_5, arg_6F0E_6, newColor, 1.5f);
														Main.dust[num99].noGravity = true;
														Dust expr_6F30_cp_0 = Main.dust[num99];
														expr_6F30_cp_0.velocity.X = expr_6F30_cp_0.velocity.X * 0.5f;
														Main.dust[num99].velocity.Y = -2f;
														return;
													}
												}
												else
												{
													if (this.type == 32)
													{
														if (Main.rand.Next(2) == 0)
														{
															Vector2 arg_6FD8_0 = new Vector2(this.position.X, this.position.Y + 2f);
															int arg_6FD8_1 = this.width;
															int arg_6FD8_2 = this.height;
															int arg_6FD8_3 = 29;
															float arg_6FD8_4 = this.velocity.X * 0.2f;
															float arg_6FD8_5 = this.velocity.Y * 0.2f;
															int arg_6FD8_6 = 100;
															Color newColor = default(Color);
															int num100 = Dust.NewDust(arg_6FD8_0, arg_6FD8_1, arg_6FD8_2, arg_6FD8_3, arg_6FD8_4, arg_6FD8_5, arg_6FD8_6, newColor, 2f);
															Main.dust[num100].noGravity = true;
															Dust expr_6FFA_cp_0 = Main.dust[num100];
															expr_6FFA_cp_0.velocity.X = expr_6FFA_cp_0.velocity.X * 1f;
															Dust expr_7018_cp_0 = Main.dust[num100];
															expr_7018_cp_0.velocity.Y = expr_7018_cp_0.velocity.Y * 1f;
															return;
														}
													}
													else
													{
														if (Main.rand.Next(2) == 0)
														{
															Vector2 arg_709B_0 = new Vector2(this.position.X, this.position.Y + 2f);
															int arg_709B_1 = this.width;
															int arg_709B_2 = this.height;
															int arg_709B_3 = 6;
															float arg_709B_4 = this.velocity.X * 0.2f;
															float arg_709B_5 = this.velocity.Y * 0.2f;
															int arg_709B_6 = 100;
															Color newColor = default(Color);
															int num101 = Dust.NewDust(arg_709B_0, arg_709B_1, arg_709B_2, arg_709B_3, arg_709B_4, arg_709B_5, arg_709B_6, newColor, 2f);
															Main.dust[num101].noGravity = true;
															Dust expr_70BD_cp_0 = Main.dust[num101];
															expr_70BD_cp_0.velocity.X = expr_70BD_cp_0.velocity.X * 1f;
															Dust expr_70DB_cp_0 = Main.dust[num101];
															expr_70DB_cp_0.velocity.Y = expr_70DB_cp_0.velocity.Y * 1f;
															return;
														}
													}
												}
											}
											else
											{
												if (this.aiStyle == 9)
												{
													if (this.target == 255)
													{
														this.TargetClosest(true);
														float num102 = 6f;
														if (this.type == 25)
														{
															num102 = 5f;
														}
														Vector2 vector11 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
														float num103 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector11.X;
														float num104 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector11.Y;
														float num105 = (float)Math.Sqrt((double)(num103 * num103 + num104 * num104));
														num105 = num102 / num105;
														this.velocity.X = num103 * num105;
														this.velocity.Y = num104 * num105;
													}
													if (this.timeLeft > 100)
													{
														this.timeLeft = 100;
													}
													for (int num106 = 0; num106 < 2; num106++)
													{
														if (this.type == 30)
														{
															Vector2 arg_7290_0 = new Vector2(this.position.X, this.position.Y + 2f);
															int arg_7290_1 = this.width;
															int arg_7290_2 = this.height;
															int arg_7290_3 = 27;
															float arg_7290_4 = this.velocity.X * 0.2f;
															float arg_7290_5 = this.velocity.Y * 0.2f;
															int arg_7290_6 = 100;
															Color newColor = default(Color);
															int num107 = Dust.NewDust(arg_7290_0, arg_7290_1, arg_7290_2, arg_7290_3, arg_7290_4, arg_7290_5, arg_7290_6, newColor, 2f);
															Main.dust[num107].noGravity = true;
															Dust expr_72AD = Main.dust[num107];
															expr_72AD.velocity *= 0.3f;
															Dust expr_72CF_cp_0 = Main.dust[num107];
															expr_72CF_cp_0.velocity.X = expr_72CF_cp_0.velocity.X - this.velocity.X * 0.2f;
															Dust expr_72F9_cp_0 = Main.dust[num107];
															expr_72F9_cp_0.velocity.Y = expr_72F9_cp_0.velocity.Y - this.velocity.Y * 0.2f;
														}
														else
														{
															if (this.type == 33)
															{
																Vector2 arg_738A_0 = new Vector2(this.position.X, this.position.Y + 2f);
																int arg_738A_1 = this.width;
																int arg_738A_2 = this.height;
																int arg_738A_3 = 29;
																float arg_738A_4 = this.velocity.X * 0.2f;
																float arg_738A_5 = this.velocity.Y * 0.2f;
																int arg_738A_6 = 100;
																Color newColor = default(Color);
																int num108 = Dust.NewDust(arg_738A_0, arg_738A_1, arg_738A_2, arg_738A_3, arg_738A_4, arg_738A_5, arg_738A_6, newColor, 2f);
																Main.dust[num108].noGravity = true;
																Dust expr_73AC_cp_0 = Main.dust[num108];
																expr_73AC_cp_0.velocity.X = expr_73AC_cp_0.velocity.X * 0.3f;
																Dust expr_73CA_cp_0 = Main.dust[num108];
																expr_73CA_cp_0.velocity.Y = expr_73CA_cp_0.velocity.Y * 0.3f;
															}
															else
															{
																Vector2 arg_7441_0 = new Vector2(this.position.X, this.position.Y + 2f);
																int arg_7441_1 = this.width;
																int arg_7441_2 = this.height;
																int arg_7441_3 = 6;
																float arg_7441_4 = this.velocity.X * 0.2f;
																float arg_7441_5 = this.velocity.Y * 0.2f;
																int arg_7441_6 = 100;
																Color newColor = default(Color);
																int num109 = Dust.NewDust(arg_7441_0, arg_7441_1, arg_7441_2, arg_7441_3, arg_7441_4, arg_7441_5, arg_7441_6, newColor, 2f);
																Main.dust[num109].noGravity = true;
																Dust expr_7463_cp_0 = Main.dust[num109];
																expr_7463_cp_0.velocity.X = expr_7463_cp_0.velocity.X * 0.3f;
																Dust expr_7481_cp_0 = Main.dust[num109];
																expr_7481_cp_0.velocity.Y = expr_7481_cp_0.velocity.Y * 0.3f;
															}
														}
													}
													this.rotation += 0.4f * (float)this.direction;
													return;
												}
												if (this.aiStyle == 10)
												{
													float num110 = 1f;
													float num111 = 0.011f;
													this.TargetClosest(true);
													Vector2 vector12 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
													float num112 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector12.X;
													float num113 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector12.Y;
													float num114 = (float)Math.Sqrt((double)(num112 * num112 + num113 * num113));
													float num115 = num114;
													this.ai[1] += 1f;
													if (this.ai[1] > 600f)
													{
														num111 *= 8f;
														num110 = 4f;
														if (this.ai[1] > 650f)
														{
															this.ai[1] = 0f;
														}
													}
													else
													{
														if (num115 < 250f)
														{
															this.ai[0] += 0.9f;
															if (this.ai[0] > 0f)
															{
																this.velocity.Y = this.velocity.Y + 0.019f;
															}
															else
															{
																this.velocity.Y = this.velocity.Y - 0.019f;
															}
															if (this.ai[0] < -100f || this.ai[0] > 100f)
															{
																this.velocity.X = this.velocity.X + 0.019f;
															}
															else
															{
																this.velocity.X = this.velocity.X - 0.019f;
															}
															if (this.ai[0] > 200f)
															{
																this.ai[0] = -200f;
															}
														}
													}
													if (num115 > 350f)
													{
														num110 = 5f;
														num111 = 0.3f;
													}
													else
													{
														if (num115 > 300f)
														{
															num110 = 3f;
															num111 = 0.2f;
														}
														else
														{
															if (num115 > 250f)
															{
																num110 = 1.5f;
																num111 = 0.1f;
															}
														}
													}
													num114 = num110 / num114;
													num112 *= num114;
													num113 *= num114;
													if (Main.player[this.target].dead)
													{
														num112 = (float)this.direction * num110 / 2f;
														num113 = -num110 / 2f;
													}
													if (this.velocity.X < num112)
													{
														this.velocity.X = this.velocity.X + num111;
													}
													else
													{
														if (this.velocity.X > num112)
														{
															this.velocity.X = this.velocity.X - num111;
														}
													}
													if (this.velocity.Y < num113)
													{
														this.velocity.Y = this.velocity.Y + num111;
													}
													else
													{
														if (this.velocity.Y > num113)
														{
															this.velocity.Y = this.velocity.Y - num111;
														}
													}
													if (num112 > 0f)
													{
														this.spriteDirection = -1;
														this.rotation = (float)Math.Atan2((double)num113, (double)num112);
													}
													if (num112 < 0f)
													{
														this.spriteDirection = 1;
														this.rotation = (float)Math.Atan2((double)num113, (double)num112) + 3.14f;
														return;
													}
												}
												else
												{
													if (this.aiStyle == 11)
													{
														if (this.ai[0] == 0f && Main.netMode != 1)
														{
															this.TargetClosest(true);
															this.ai[0] = 1f;
															if (this.type != 68)
															{
																int num116 = NPC.NewNPC((int)(this.position.X + (float)(this.width / 2)), (int)this.position.Y + this.height / 2, 36, this.whoAmI);
																Main.npc[num116].ai[0] = -1f;
																Main.npc[num116].ai[1] = (float)this.whoAmI;
																Main.npc[num116].target = this.target;
																Main.npc[num116].netUpdate = true;
																num116 = NPC.NewNPC((int)(this.position.X + (float)(this.width / 2)), (int)this.position.Y + this.height / 2, 36, this.whoAmI);
																Main.npc[num116].ai[0] = 1f;
																Main.npc[num116].ai[1] = (float)this.whoAmI;
																Main.npc[num116].ai[3] = 150f;
																Main.npc[num116].target = this.target;
																Main.npc[num116].netUpdate = true;
															}
														}
														if (this.type == 68 && this.ai[1] != 3f && this.ai[1] != 2f)
														{
															Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
															this.ai[1] = 2f;
														}
														if (Main.player[this.target].dead || Math.Abs(this.position.X - Main.player[this.target].position.X) > 2000f || Math.Abs(this.position.Y - Main.player[this.target].position.Y) > 2000f)
														{
															this.TargetClosest(true);
															if (Main.player[this.target].dead || Math.Abs(this.position.X - Main.player[this.target].position.X) > 2000f || Math.Abs(this.position.Y - Main.player[this.target].position.Y) > 2000f)
															{
																this.ai[1] = 3f;
															}
														}
														if (Main.dayTime && this.ai[1] != 3f && this.ai[1] != 2f)
														{
															this.ai[1] = 2f;
															Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
														}
														if (this.ai[1] == 0f)
														{
															this.defense = 10;
															this.ai[2] += 1f;
															if (this.ai[2] >= 800f)
															{
																this.ai[2] = 0f;
																this.ai[1] = 1f;
																this.TargetClosest(true);
																this.netUpdate = true;
															}
															this.rotation = this.velocity.X / 15f;
															if (this.position.Y > Main.player[this.target].position.Y - 250f)
															{
																if (this.velocity.Y > 0f)
																{
																	this.velocity.Y = this.velocity.Y * 0.98f;
																}
																this.velocity.Y = this.velocity.Y - 0.02f;
																if (this.velocity.Y > 2f)
																{
																	this.velocity.Y = 2f;
																}
															}
															else
															{
																if (this.position.Y < Main.player[this.target].position.Y - 250f)
																{
																	if (this.velocity.Y < 0f)
																	{
																		this.velocity.Y = this.velocity.Y * 0.98f;
																	}
																	this.velocity.Y = this.velocity.Y + 0.02f;
																	if (this.velocity.Y < -2f)
																	{
																		this.velocity.Y = -2f;
																	}
																}
															}
															if (this.position.X + (float)(this.width / 2) > Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2))
															{
																if (this.velocity.X > 0f)
																{
																	this.velocity.X = this.velocity.X * 0.98f;
																}
																this.velocity.X = this.velocity.X - 0.05f;
																if (this.velocity.X > 8f)
																{
																	this.velocity.X = 8f;
																}
															}
															if (this.position.X + (float)(this.width / 2) < Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2))
															{
																if (this.velocity.X < 0f)
																{
																	this.velocity.X = this.velocity.X * 0.98f;
																}
																this.velocity.X = this.velocity.X + 0.05f;
																if (this.velocity.X < -8f)
																{
																	this.velocity.X = -8f;
																}
															}
														}
														else
														{
															if (this.ai[1] == 1f)
															{
																this.defense = 0;
																this.ai[2] += 1f;
																if (this.ai[2] == 2f)
																{
																	Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
																}
																if (this.ai[2] >= 400f)
																{
																	this.ai[2] = 0f;
																	this.ai[1] = 0f;
																}
																this.rotation += (float)this.direction * 0.3f;
																Vector2 vector13 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
																float num117 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector13.X;
																float num118 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector13.Y;
																float num119 = (float)Math.Sqrt((double)(num117 * num117 + num118 * num118));
																num119 = 1.5f / num119;
																this.velocity.X = num117 * num119;
																this.velocity.Y = num118 * num119;
															}
															else
															{
																if (this.ai[1] == 2f)
																{
																	this.damage = 9999;
																	this.defense = 9999;
																	this.rotation += (float)this.direction * 0.3f;
																	Vector2 vector14 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
																	float num120 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector14.X;
																	float num121 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector14.Y;
																	float num122 = (float)Math.Sqrt((double)(num120 * num120 + num121 * num121));
																	num122 = 8f / num122;
																	this.velocity.X = num120 * num122;
																	this.velocity.Y = num121 * num122;
																}
																else
																{
																	if (this.ai[1] == 3f)
																	{
																		this.velocity.Y = this.velocity.Y + 0.1f;
																		if (this.velocity.Y < 0f)
																		{
																			this.velocity.Y = this.velocity.Y * 0.95f;
																		}
																		this.velocity.X = this.velocity.X * 0.95f;
																		if (this.timeLeft > 500)
																		{
																			this.timeLeft = 500;
																		}
																	}
																}
															}
														}
														if (this.ai[1] != 2f && this.ai[1] != 3f && this.type != 68)
														{
															Vector2 arg_820B_0 = new Vector2(this.position.X + (float)(this.width / 2) - 15f - this.velocity.X * 5f, this.position.Y + (float)this.height - 2f);
															int arg_820B_1 = 30;
															int arg_820B_2 = 10;
															int arg_820B_3 = 5;
															float arg_820B_4 = -this.velocity.X * 0.2f;
															float arg_820B_5 = 3f;
															int arg_820B_6 = 0;
															Color newColor = default(Color);
															int num123 = Dust.NewDust(arg_820B_0, arg_820B_1, arg_820B_2, arg_820B_3, arg_820B_4, arg_820B_5, arg_820B_6, newColor, 2f);
															Main.dust[num123].noGravity = true;
															Dust expr_822D_cp_0 = Main.dust[num123];
															expr_822D_cp_0.velocity.X = expr_822D_cp_0.velocity.X * 1.3f;
															Dust expr_824B_cp_0 = Main.dust[num123];
															expr_824B_cp_0.velocity.X = expr_824B_cp_0.velocity.X + this.velocity.X * 0.4f;
															Dust expr_8275_cp_0 = Main.dust[num123];
															expr_8275_cp_0.velocity.Y = expr_8275_cp_0.velocity.Y + (2f + this.velocity.Y);
															for (int num124 = 0; num124 < 2; num124++)
															{
																Vector2 arg_82EA_0 = new Vector2(this.position.X, this.position.Y + 120f);
																int arg_82EA_1 = this.width;
																int arg_82EA_2 = 60;
																int arg_82EA_3 = 5;
																float arg_82EA_4 = this.velocity.X;
																float arg_82EA_5 = this.velocity.Y;
																int arg_82EA_6 = 0;
																newColor = default(Color);
																num123 = Dust.NewDust(arg_82EA_0, arg_82EA_1, arg_82EA_2, arg_82EA_3, arg_82EA_4, arg_82EA_5, arg_82EA_6, newColor, 2f);
																Main.dust[num123].noGravity = true;
																Dust expr_8307 = Main.dust[num123];
																expr_8307.velocity -= this.velocity;
																Dust expr_832A_cp_0 = Main.dust[num123];
																expr_832A_cp_0.velocity.Y = expr_832A_cp_0.velocity.Y + 5f;
															}
															return;
														}
													}
													else
													{
														if (this.aiStyle == 12)
														{
															this.spriteDirection = -(int)this.ai[0];
															if (!Main.npc[(int)this.ai[1]].active || Main.npc[(int)this.ai[1]].aiStyle != 11)
															{
																this.ai[2] += 10f;
																if (this.ai[2] > 50f || Main.netMode != 2)
																{
																	this.life = -1;
																	this.HitEffect(0, 10.0);
																	this.active = false;
																}
															}
															if (this.ai[2] == 0f || this.ai[2] == 3f)
															{
																if (Main.npc[(int)this.ai[1]].ai[1] == 3f && this.timeLeft > 10)
																{
																	this.timeLeft = 10;
																}
																if (Main.npc[(int)this.ai[1]].ai[1] != 0f)
																{
																	if (this.position.Y > Main.npc[(int)this.ai[1]].position.Y - 100f)
																	{
																		if (this.velocity.Y > 0f)
																		{
																			this.velocity.Y = this.velocity.Y * 0.96f;
																		}
																		this.velocity.Y = this.velocity.Y - 0.07f;
																		if (this.velocity.Y > 6f)
																		{
																			this.velocity.Y = 6f;
																		}
																	}
																	else
																	{
																		if (this.position.Y < Main.npc[(int)this.ai[1]].position.Y - 100f)
																		{
																			if (this.velocity.Y < 0f)
																			{
																				this.velocity.Y = this.velocity.Y * 0.96f;
																			}
																			this.velocity.Y = this.velocity.Y + 0.07f;
																			if (this.velocity.Y < -6f)
																			{
																				this.velocity.Y = -6f;
																			}
																		}
																	}
																	if (this.position.X + (float)(this.width / 2) > Main.npc[(int)this.ai[1]].position.X + (float)(Main.npc[(int)this.ai[1]].width / 2) - 120f * this.ai[0])
																	{
																		if (this.velocity.X > 0f)
																		{
																			this.velocity.X = this.velocity.X * 0.96f;
																		}
																		this.velocity.X = this.velocity.X - 0.1f;
																		if (this.velocity.X > 8f)
																		{
																			this.velocity.X = 8f;
																		}
																	}
																	if (this.position.X + (float)(this.width / 2) < Main.npc[(int)this.ai[1]].position.X + (float)(Main.npc[(int)this.ai[1]].width / 2) - 120f * this.ai[0])
																	{
																		if (this.velocity.X < 0f)
																		{
																			this.velocity.X = this.velocity.X * 0.96f;
																		}
																		this.velocity.X = this.velocity.X + 0.1f;
																		if (this.velocity.X < -8f)
																		{
																			this.velocity.X = -8f;
																		}
																	}
																}
																else
																{
																	this.ai[3] += 1f;
																	if (this.ai[3] >= 300f)
																	{
																		this.ai[2] += 1f;
																		this.ai[3] = 0f;
																		this.netUpdate = true;
																	}
																	if (this.position.Y > Main.npc[(int)this.ai[1]].position.Y + 230f)
																	{
																		if (this.velocity.Y > 0f)
																		{
																			this.velocity.Y = this.velocity.Y * 0.96f;
																		}
																		this.velocity.Y = this.velocity.Y - 0.04f;
																		if (this.velocity.Y > 3f)
																		{
																			this.velocity.Y = 3f;
																		}
																	}
																	else
																	{
																		if (this.position.Y < Main.npc[(int)this.ai[1]].position.Y + 230f)
																		{
																			if (this.velocity.Y < 0f)
																			{
																				this.velocity.Y = this.velocity.Y * 0.96f;
																			}
																			this.velocity.Y = this.velocity.Y + 0.04f;
																			if (this.velocity.Y < -3f)
																			{
																				this.velocity.Y = -3f;
																			}
																		}
																	}
																	if (this.position.X + (float)(this.width / 2) > Main.npc[(int)this.ai[1]].position.X + (float)(Main.npc[(int)this.ai[1]].width / 2) - 200f * this.ai[0])
																	{
																		if (this.velocity.X > 0f)
																		{
																			this.velocity.X = this.velocity.X * 0.96f;
																		}
																		this.velocity.X = this.velocity.X - 0.07f;
																		if (this.velocity.X > 8f)
																		{
																			this.velocity.X = 8f;
																		}
																	}
																	if (this.position.X + (float)(this.width / 2) < Main.npc[(int)this.ai[1]].position.X + (float)(Main.npc[(int)this.ai[1]].width / 2) - 200f * this.ai[0])
																	{
																		if (this.velocity.X < 0f)
																		{
																			this.velocity.X = this.velocity.X * 0.96f;
																		}
																		this.velocity.X = this.velocity.X + 0.07f;
																		if (this.velocity.X < -8f)
																		{
																			this.velocity.X = -8f;
																		}
																	}
																}
																Vector2 vector15 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
																float num125 = Main.npc[(int)this.ai[1]].position.X + (float)(Main.npc[(int)this.ai[1]].width / 2) - 200f * this.ai[0] - vector15.X;
																float num126 = Main.npc[(int)this.ai[1]].position.Y + 230f - vector15.Y;
																Math.Sqrt((double)(num125 * num125 + num126 * num126));
																this.rotation = (float)Math.Atan2((double)num126, (double)num125) + 1.57f;
																return;
															}
															if (this.ai[2] == 1f)
															{
																Vector2 vector16 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
																float num127 = Main.npc[(int)this.ai[1]].position.X + (float)(Main.npc[(int)this.ai[1]].width / 2) - 200f * this.ai[0] - vector16.X;
																float num128 = Main.npc[(int)this.ai[1]].position.Y + 230f - vector16.Y;
																float num129 = (float)Math.Sqrt((double)(num127 * num127 + num128 * num128));
																this.rotation = (float)Math.Atan2((double)num128, (double)num127) + 1.57f;
																this.velocity.X = this.velocity.X * 0.95f;
																this.velocity.Y = this.velocity.Y - 0.1f;
																if (this.velocity.Y < -8f)
																{
																	this.velocity.Y = -8f;
																}
																if (this.position.Y < Main.npc[(int)this.ai[1]].position.Y - 200f)
																{
																	this.TargetClosest(true);
																	this.ai[2] = 2f;
																	vector16 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
																	num127 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector16.X;
																	num128 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector16.Y;
																	num129 = (float)Math.Sqrt((double)(num127 * num127 + num128 * num128));
																	num129 = 18f / num129;
																	this.velocity.X = num127 * num129;
																	this.velocity.Y = num128 * num129;
																	this.netUpdate = true;
																	return;
																}
															}
															else
															{
																if (this.ai[2] == 2f)
																{
																	if (this.position.Y > Main.player[this.target].position.Y || this.velocity.Y < 0f)
																	{
																		this.ai[2] = 3f;
																		return;
																	}
																}
																else
																{
																	if (this.ai[2] == 4f)
																	{
																		Vector2 vector17 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
																		float num130 = Main.npc[(int)this.ai[1]].position.X + (float)(Main.npc[(int)this.ai[1]].width / 2) - 200f * this.ai[0] - vector17.X;
																		float num131 = Main.npc[(int)this.ai[1]].position.Y + 230f - vector17.Y;
																		float num132 = (float)Math.Sqrt((double)(num130 * num130 + num131 * num131));
																		this.rotation = (float)Math.Atan2((double)num131, (double)num130) + 1.57f;
																		this.velocity.Y = this.velocity.Y * 0.95f;
																		this.velocity.X = this.velocity.X + 0.1f * -this.ai[0];
																		if (this.velocity.X < -8f)
																		{
																			this.velocity.X = -8f;
																		}
																		if (this.velocity.X > 8f)
																		{
																			this.velocity.X = 8f;
																		}
																		if (this.position.X + (float)(this.width / 2) < Main.npc[(int)this.ai[1]].position.X + (float)(Main.npc[(int)this.ai[1]].width / 2) - 500f || this.position.X + (float)(this.width / 2) > Main.npc[(int)this.ai[1]].position.X + (float)(Main.npc[(int)this.ai[1]].width / 2) + 500f)
																		{
																			this.TargetClosest(true);
																			this.ai[2] = 5f;
																			vector17 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
																			num130 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector17.X;
																			num131 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector17.Y;
																			num132 = (float)Math.Sqrt((double)(num130 * num130 + num131 * num131));
																			num132 = 17f / num132;
																			this.velocity.X = num130 * num132;
																			this.velocity.Y = num131 * num132;
																			this.netUpdate = true;
																			return;
																		}
																	}
																	else
																	{
																		if (this.ai[2] == 5f && ((this.velocity.X > 0f && this.position.X + (float)(this.width / 2) > Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2)) || (this.velocity.X < 0f && this.position.X + (float)(this.width / 2) < Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2))))
																		{
																			this.ai[2] = 0f;
																			return;
																		}
																	}
																}
															}
														}
														else
														{
															if (this.aiStyle == 13)
															{
																if (Main.tile[(int)this.ai[0], (int)this.ai[1]] == null)
																{
																	Main.tile[(int)this.ai[0], (int)this.ai[1]] = new Tile();
																}
																if (!Main.tile[(int)this.ai[0], (int)this.ai[1]].active)
																{
																	this.life = -1;
																	this.HitEffect(0, 10.0);
																	this.active = false;
																	return;
																}
																this.TargetClosest(true);
																float num133 = 0.035f;
																float num134 = 150f;
																if (this.type == 43)
																{
																	num134 = 250f;
																}
																this.ai[2] += 1f;
																if (this.ai[2] > 300f)
																{
																	num134 = (float)((int)((double)num134 * 1.3));
																	if (this.ai[2] > 450f)
																	{
																		this.ai[2] = 0f;
																	}
																}
																Vector2 vector18 = new Vector2(this.ai[0] * 16f + 8f, this.ai[1] * 16f + 8f);
																float num135 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - (float)(this.width / 2) - vector18.X;
																float num136 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - (float)(this.height / 2) - vector18.Y;
																float num137 = (float)Math.Sqrt((double)(num135 * num135 + num136 * num136));
																if (num137 > num134)
																{
																	num137 = num134 / num137;
																	num135 *= num137;
																	num136 *= num137;
																}
																if (this.position.X < this.ai[0] * 16f + 8f + num135)
																{
																	this.velocity.X = this.velocity.X + num133;
																	if (this.velocity.X < 0f && num135 > 0f)
																	{
																		this.velocity.X = this.velocity.X + num133 * 1.5f;
																	}
																}
																else
																{
																	if (this.position.X > this.ai[0] * 16f + 8f + num135)
																	{
																		this.velocity.X = this.velocity.X - num133;
																		if (this.velocity.X > 0f && num135 < 0f)
																		{
																			this.velocity.X = this.velocity.X - num133 * 1.5f;
																		}
																	}
																}
																if (this.position.Y < this.ai[1] * 16f + 8f + num136)
																{
																	this.velocity.Y = this.velocity.Y + num133;
																	if (this.velocity.Y < 0f && num136 > 0f)
																	{
																		this.velocity.Y = this.velocity.Y + num133 * 1.5f;
																	}
																}
																else
																{
																	if (this.position.Y > this.ai[1] * 16f + 8f + num136)
																	{
																		this.velocity.Y = this.velocity.Y - num133;
																		if (this.velocity.Y > 0f && num136 < 0f)
																		{
																			this.velocity.Y = this.velocity.Y - num133 * 1.5f;
																		}
																	}
																}
																if (this.type == 43)
																{
																	if (this.velocity.X > 3f)
																	{
																		this.velocity.X = 3f;
																	}
																	if (this.velocity.X < -3f)
																	{
																		this.velocity.X = -3f;
																	}
																	if (this.velocity.Y > 3f)
																	{
																		this.velocity.Y = 3f;
																	}
																	if (this.velocity.Y < -3f)
																	{
																		this.velocity.Y = -3f;
																	}
																}
																else
																{
																	if (this.velocity.X > 2f)
																	{
																		this.velocity.X = 2f;
																	}
																	if (this.velocity.X < -2f)
																	{
																		this.velocity.X = -2f;
																	}
																	if (this.velocity.Y > 2f)
																	{
																		this.velocity.Y = 2f;
																	}
																	if (this.velocity.Y < -2f)
																	{
																		this.velocity.Y = -2f;
																	}
																}
																if (num135 > 0f)
																{
																	this.spriteDirection = 1;
																	this.rotation = (float)Math.Atan2((double)num136, (double)num135);
																}
																if (num135 < 0f)
																{
																	this.spriteDirection = -1;
																	this.rotation = (float)Math.Atan2((double)num136, (double)num135) + 3.14f;
																}
																if (this.collideX)
																{
																	this.netUpdate = true;
																	this.velocity.X = this.oldVelocity.X * -0.7f;
																	if (this.velocity.X > 0f && this.velocity.X < 2f)
																	{
																		this.velocity.X = 2f;
																	}
																	if (this.velocity.X < 0f && this.velocity.X > -2f)
																	{
																		this.velocity.X = -2f;
																	}
																}
																if (this.collideY)
																{
																	this.netUpdate = true;
																	this.velocity.Y = this.oldVelocity.Y * -0.7f;
																	if (this.velocity.Y > 0f && this.velocity.Y < 2f)
																	{
																		this.velocity.Y = 2f;
																	}
																	if (this.velocity.Y < 0f && this.velocity.Y > -2f)
																	{
																		this.velocity.Y = -2f;
																		return;
																	}
																}
															}
															else
															{
																if (this.aiStyle == 14)
																{
																	if (this.type == 60)
																	{
																		Vector2 arg_980E_0 = new Vector2(this.position.X, this.position.Y);
																		int arg_980E_1 = this.width;
																		int arg_980E_2 = this.height;
																		int arg_980E_3 = 6;
																		float arg_980E_4 = this.velocity.X * 0.2f;
																		float arg_980E_5 = this.velocity.Y * 0.2f;
																		int arg_980E_6 = 100;
																		Color newColor = default(Color);
																		int num138 = Dust.NewDust(arg_980E_0, arg_980E_1, arg_980E_2, arg_980E_3, arg_980E_4, arg_980E_5, arg_980E_6, newColor, 2f);
																		Main.dust[num138].noGravity = true;
																	}
																	this.noGravity = true;
																	if (this.collideX)
																	{
																		this.velocity.X = this.oldVelocity.X * -0.5f;
																		if (this.direction == -1 && this.velocity.X > 0f && this.velocity.X < 2f)
																		{
																			this.velocity.X = 2f;
																		}
																		if (this.direction == 1 && this.velocity.X < 0f && this.velocity.X > -2f)
																		{
																			this.velocity.X = -2f;
																		}
																	}
																	if (this.collideY)
																	{
																		this.velocity.Y = this.oldVelocity.Y * -0.5f;
																		if (this.velocity.Y > 0f && this.velocity.Y < 1f)
																		{
																			this.velocity.Y = 1f;
																		}
																		if (this.velocity.Y < 0f && this.velocity.Y > -1f)
																		{
																			this.velocity.Y = -1f;
																		}
																	}
																	this.TargetClosest(true);
																	if (this.direction == -1 && this.velocity.X > -4f)
																	{
																		this.velocity.X = this.velocity.X - 0.1f;
																		if (this.velocity.X > 4f)
																		{
																			this.velocity.X = this.velocity.X - 0.1f;
																		}
																		else
																		{
																			if (this.velocity.X > 0f)
																			{
																				this.velocity.X = this.velocity.X + 0.05f;
																			}
																		}
																		if (this.velocity.X < -4f)
																		{
																			this.velocity.X = -4f;
																		}
																	}
																	else
																	{
																		if (this.direction == 1 && this.velocity.X < 4f)
																		{
																			this.velocity.X = this.velocity.X + 0.1f;
																			if (this.velocity.X < -4f)
																			{
																				this.velocity.X = this.velocity.X + 0.1f;
																			}
																			else
																			{
																				if (this.velocity.X < 0f)
																				{
																					this.velocity.X = this.velocity.X - 0.05f;
																				}
																			}
																			if (this.velocity.X > 4f)
																			{
																				this.velocity.X = 4f;
																			}
																		}
																	}
																	if (this.directionY == -1 && (double)this.velocity.Y > -1.5)
																	{
																		this.velocity.Y = this.velocity.Y - 0.04f;
																		if ((double)this.velocity.Y > 1.5)
																		{
																			this.velocity.Y = this.velocity.Y - 0.05f;
																		}
																		else
																		{
																			if (this.velocity.Y > 0f)
																			{
																				this.velocity.Y = this.velocity.Y + 0.03f;
																			}
																		}
																		if ((double)this.velocity.Y < -1.5)
																		{
																			this.velocity.Y = -1.5f;
																		}
																	}
																	else
																	{
																		if (this.directionY == 1 && (double)this.velocity.Y < 1.5)
																		{
																			this.velocity.Y = this.velocity.Y + 0.04f;
																			if ((double)this.velocity.Y < -1.5)
																			{
																				this.velocity.Y = this.velocity.Y + 0.05f;
																			}
																			else
																			{
																				if (this.velocity.Y < 0f)
																				{
																					this.velocity.Y = this.velocity.Y - 0.03f;
																				}
																			}
																			if ((double)this.velocity.Y > 1.5)
																			{
																				this.velocity.Y = 1.5f;
																			}
																		}
																	}
																	if (this.type == 49 || this.type == 51 || this.type == 60 || this.type == 62 || this.type == 66)
																	{
																		if (this.wet)
																		{
																			if (this.velocity.Y > 0f)
																			{
																				this.velocity.Y = this.velocity.Y * 0.95f;
																			}
																			this.velocity.Y = this.velocity.Y - 0.5f;
																			if (this.velocity.Y < -4f)
																			{
																				this.velocity.Y = -4f;
																			}
																			this.TargetClosest(true);
																		}
																		if (this.type == 60)
																		{
																			if (this.direction == -1 && this.velocity.X > -4f)
																			{
																				this.velocity.X = this.velocity.X - 0.1f;
																				if (this.velocity.X > 4f)
																				{
																					this.velocity.X = this.velocity.X - 0.07f;
																				}
																				else
																				{
																					if (this.velocity.X > 0f)
																					{
																						this.velocity.X = this.velocity.X + 0.03f;
																					}
																				}
																				if (this.velocity.X < -4f)
																				{
																					this.velocity.X = -4f;
																				}
																			}
																			else
																			{
																				if (this.direction == 1 && this.velocity.X < 4f)
																				{
																					this.velocity.X = this.velocity.X + 0.1f;
																					if (this.velocity.X < -4f)
																					{
																						this.velocity.X = this.velocity.X + 0.07f;
																					}
																					else
																					{
																						if (this.velocity.X < 0f)
																						{
																							this.velocity.X = this.velocity.X - 0.03f;
																						}
																					}
																					if (this.velocity.X > 4f)
																					{
																						this.velocity.X = 4f;
																					}
																				}
																			}
																			if (this.directionY == -1 && (double)this.velocity.Y > -1.5)
																			{
																				this.velocity.Y = this.velocity.Y - 0.04f;
																				if ((double)this.velocity.Y > 1.5)
																				{
																					this.velocity.Y = this.velocity.Y - 0.03f;
																				}
																				else
																				{
																					if (this.velocity.Y > 0f)
																					{
																						this.velocity.Y = this.velocity.Y + 0.02f;
																					}
																				}
																				if ((double)this.velocity.Y < -1.5)
																				{
																					this.velocity.Y = -1.5f;
																				}
																			}
																			else
																			{
																				if (this.directionY == 1 && (double)this.velocity.Y < 1.5)
																				{
																					this.velocity.Y = this.velocity.Y + 0.04f;
																					if ((double)this.velocity.Y < -1.5)
																					{
																						this.velocity.Y = this.velocity.Y + 0.03f;
																					}
																					else
																					{
																						if (this.velocity.Y < 0f)
																						{
																							this.velocity.Y = this.velocity.Y - 0.02f;
																						}
																					}
																					if ((double)this.velocity.Y > 1.5)
																					{
																						this.velocity.Y = 1.5f;
																					}
																				}
																			}
																		}
																		else
																		{
																			if (this.direction == -1 && this.velocity.X > -4f)
																			{
																				this.velocity.X = this.velocity.X - 0.1f;
																				if (this.velocity.X > 4f)
																				{
																					this.velocity.X = this.velocity.X - 0.1f;
																				}
																				else
																				{
																					if (this.velocity.X > 0f)
																					{
																						this.velocity.X = this.velocity.X + 0.05f;
																					}
																				}
																				if (this.velocity.X < -4f)
																				{
																					this.velocity.X = -4f;
																				}
																			}
																			else
																			{
																				if (this.direction == 1 && this.velocity.X < 4f)
																				{
																					this.velocity.X = this.velocity.X + 0.1f;
																					if (this.velocity.X < -4f)
																					{
																						this.velocity.X = this.velocity.X + 0.1f;
																					}
																					else
																					{
																						if (this.velocity.X < 0f)
																						{
																							this.velocity.X = this.velocity.X - 0.05f;
																						}
																					}
																					if (this.velocity.X > 4f)
																					{
																						this.velocity.X = 4f;
																					}
																				}
																			}
																			if (this.directionY == -1 && (double)this.velocity.Y > -1.5)
																			{
																				this.velocity.Y = this.velocity.Y - 0.04f;
																				if ((double)this.velocity.Y > 1.5)
																				{
																					this.velocity.Y = this.velocity.Y - 0.05f;
																				}
																				else
																				{
																					if (this.velocity.Y > 0f)
																					{
																						this.velocity.Y = this.velocity.Y + 0.03f;
																					}
																				}
																				if ((double)this.velocity.Y < -1.5)
																				{
																					this.velocity.Y = -1.5f;
																				}
																			}
																			else
																			{
																				if (this.directionY == 1 && (double)this.velocity.Y < 1.5)
																				{
																					this.velocity.Y = this.velocity.Y + 0.04f;
																					if ((double)this.velocity.Y < -1.5)
																					{
																						this.velocity.Y = this.velocity.Y + 0.05f;
																					}
																					else
																					{
																						if (this.velocity.Y < 0f)
																						{
																							this.velocity.Y = this.velocity.Y - 0.03f;
																						}
																					}
																					if ((double)this.velocity.Y > 1.5)
																					{
																						this.velocity.Y = 1.5f;
																					}
																				}
																			}
																		}
																	}
																	this.ai[1] += 1f;
																	if (this.ai[1] > 200f)
																	{
																		if (!Main.player[this.target].wet && Collision.CanHit(this.position, this.width, this.height, Main.player[this.target].position, Main.player[this.target].width, Main.player[this.target].height))
																		{
																			this.ai[1] = 0f;
																		}
																		float num139 = 0.2f;
																		float num140 = 0.1f;
																		float num141 = 4f;
																		float num142 = 1.5f;
																		if (this.type == 48 || this.type == 62 || this.type == 66)
																		{
																			num139 = 0.12f;
																			num140 = 0.07f;
																			num141 = 3f;
																			num142 = 1.25f;
																		}
																		if (this.ai[1] > 1000f)
																		{
																			this.ai[1] = 0f;
																		}
																		this.ai[2] += 1f;
																		if (this.ai[2] > 0f)
																		{
																			if (this.velocity.Y < num142)
																			{
																				this.velocity.Y = this.velocity.Y + num140;
																			}
																		}
																		else
																		{
																			if (this.velocity.Y > -num142)
																			{
																				this.velocity.Y = this.velocity.Y - num140;
																			}
																		}
																		if (this.ai[2] < -150f || this.ai[2] > 150f)
																		{
																			if (this.velocity.X < num141)
																			{
																				this.velocity.X = this.velocity.X + num139;
																			}
																		}
																		else
																		{
																			if (this.velocity.X > -num141)
																			{
																				this.velocity.X = this.velocity.X - num139;
																			}
																		}
																		if (this.ai[2] > 300f)
																		{
																			this.ai[2] = -300f;
																		}
																	}
																	if (Main.netMode != 1)
																	{
																		if (this.type == 48)
																		{
																			this.ai[0] += 1f;
																			if (this.ai[0] == 30f || this.ai[0] == 60f || this.ai[0] == 90f)
																			{
																				if (Collision.CanHit(this.position, this.width, this.height, Main.player[this.target].position, Main.player[this.target].width, Main.player[this.target].height))
																				{
																					float num143 = 6f;
																					Vector2 vector19 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
																					float num144 = Main.player[this.target].position.X + (float)Main.player[this.target].width * 0.5f - vector19.X + (float)Main.rand.Next(-100, 101);
																					float num145 = Main.player[this.target].position.Y + (float)Main.player[this.target].height * 0.5f - vector19.Y + (float)Main.rand.Next(-100, 101);
																					float num146 = (float)Math.Sqrt((double)(num144 * num144 + num145 * num145));
																					num146 = num143 / num146;
																					num144 *= num146;
																					num145 *= num146;
																					int num147 = 15;
																					int num148 = 38;
																					int num149 = Projectile.NewProjectile(vector19.X, vector19.Y, num144, num145, num148, num147, 0f, Main.myPlayer);
																					Main.projectile[num149].timeLeft = 300;
																				}
																			}
																			else
																			{
																				if (this.ai[0] >= (float)(400 + Main.rand.Next(400)))
																				{
																					this.ai[0] = 0f;
																				}
																			}
																		}
																		if (this.type == 62 || this.type == 66)
																		{
																			this.ai[0] += 1f;
																			if (this.ai[0] == 20f || this.ai[0] == 40f || this.ai[0] == 60f || this.ai[0] == 80f)
																			{
																				if (Collision.CanHit(this.position, this.width, this.height, Main.player[this.target].position, Main.player[this.target].width, Main.player[this.target].height))
																				{
																					float num150 = 0.2f;
																					Vector2 vector20 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
																					float num151 = Main.player[this.target].position.X + (float)Main.player[this.target].width * 0.5f - vector20.X + (float)Main.rand.Next(-100, 101);
																					float num152 = Main.player[this.target].position.Y + (float)Main.player[this.target].height * 0.5f - vector20.Y + (float)Main.rand.Next(-100, 101);
																					float num153 = (float)Math.Sqrt((double)(num151 * num151 + num152 * num152));
																					num153 = num150 / num153;
																					num151 *= num153;
																					num152 *= num153;
																					int num154 = 21;
																					int num155 = 44;
																					int num156 = Projectile.NewProjectile(vector20.X, vector20.Y, num151, num152, num155, num154, 0f, Main.myPlayer);
																					Main.projectile[num156].timeLeft = 300;
																					return;
																				}
																			}
																			else
																			{
																				if (this.ai[0] >= (float)(300 + Main.rand.Next(300)))
																				{
																					this.ai[0] = 0f;
																					return;
																				}
																			}
																		}
																	}
																}
																else
																{
																	if (this.aiStyle == 15)
																	{
																		this.aiAction = 0;
																		if (this.ai[3] == 0f && this.life > 0)
																		{
																			this.ai[3] = (float)this.lifeMax;
																		}
																		if (this.ai[2] == 0f)
																		{
																			this.ai[0] = -100f;
																			this.ai[2] = 1f;
																			this.TargetClosest(true);
																		}
																		if (this.velocity.Y == 0f)
																		{
																			this.velocity.X = this.velocity.X * 0.8f;
																			if ((double)this.velocity.X > -0.1 && (double)this.velocity.X < 0.1)
																			{
																				this.velocity.X = 0f;
																			}
																			this.ai[0] += 2f;
																			if ((double)this.life < (double)this.lifeMax * 0.8)
																			{
																				this.ai[0] += 1f;
																			}
																			if ((double)this.life < (double)this.lifeMax * 0.6)
																			{
																				this.ai[0] += 1f;
																			}
																			if ((double)this.life < (double)this.lifeMax * 0.4)
																			{
																				this.ai[0] += 2f;
																			}
																			if ((double)this.life < (double)this.lifeMax * 0.2)
																			{
																				this.ai[0] += 3f;
																			}
																			if ((double)this.life < (double)this.lifeMax * 0.1)
																			{
																				this.ai[0] += 4f;
																			}
																			if (this.ai[0] >= 0f)
																			{
																				this.netUpdate = true;
																				this.TargetClosest(true);
																				if (this.ai[1] == 3f)
																				{
																					this.velocity.Y = -13f;
																					this.velocity.X = this.velocity.X + 3.5f * (float)this.direction;
																					this.ai[0] = -200f;
																					this.ai[1] = 0f;
																				}
																				else
																				{
																					if (this.ai[1] == 2f)
																					{
																						this.velocity.Y = -6f;
																						this.velocity.X = this.velocity.X + 4.5f * (float)this.direction;
																						this.ai[0] = -120f;
																						this.ai[1] += 1f;
																					}
																					else
																					{
																						this.velocity.Y = -8f;
																						this.velocity.X = this.velocity.X + 4f * (float)this.direction;
																						this.ai[0] = -120f;
																						this.ai[1] += 1f;
																					}
																				}
																			}
																			else
																			{
																				if (this.ai[0] >= -30f)
																				{
																					this.aiAction = 1;
																				}
																			}
																		}
																		else
																		{
																			if (this.target < 255 && ((this.direction == 1 && this.velocity.X < 3f) || (this.direction == -1 && this.velocity.X > -3f)))
																			{
																				if ((this.direction == -1 && (double)this.velocity.X < 0.1) || (this.direction == 1 && (double)this.velocity.X > -0.1))
																				{
																					this.velocity.X = this.velocity.X + 0.2f * (float)this.direction;
																				}
																				else
																				{
																					this.velocity.X = this.velocity.X * 0.93f;
																				}
																			}
																		}
																		int num157 = Dust.NewDust(this.position, this.width, this.height, 4, this.velocity.X, this.velocity.Y, 255, new Color(0, 80, 255, 80), this.scale * 1.2f);
																		Main.dust[num157].noGravity = true;
																		Dust expr_ADB4 = Main.dust[num157];
																		expr_ADB4.velocity *= 0.5f;
																		if (this.life > 0)
																		{
																			float num158 = (float)this.life / (float)this.lifeMax;
																			num158 = num158 * 0.5f + 0.75f;
																			if (num158 != this.scale)
																			{
																				this.position.X = this.position.X + (float)(this.width / 2);
																				this.position.Y = this.position.Y + (float)this.height;
																				this.scale = num158;
																				this.width = (int)(98f * this.scale);
																				this.height = (int)(92f * this.scale);
																				this.position.X = this.position.X - (float)(this.width / 2);
																				this.position.Y = this.position.Y - (float)this.height;
																			}
																			if (Main.netMode != 1)
																			{
																				int num159 = (int)((double)this.lifeMax * 0.05);
																				if ((float)(this.life + num159) < this.ai[3])
																				{
																					this.ai[3] = (float)this.life;
																					int num160 = Main.rand.Next(1, 4);
																					for (int num161 = 0; num161 < num160; num161++)
																					{
																						int x = (int)(this.position.X + (float)Main.rand.Next(this.width - 32));
																						int y = (int)(this.position.Y + (float)Main.rand.Next(this.height - 32));
																						int num162 = NPC.NewNPC(x, y, 1, 0);
																						Main.npc[num162].SetDefaults(1, -1f);
																						Main.npc[num162].velocity.X = (float)Main.rand.Next(-15, 16) * 0.1f;
																						Main.npc[num162].velocity.Y = (float)Main.rand.Next(-30, 1) * 0.1f;
																						Main.npc[num162].ai[1] = (float)Main.rand.Next(3);
																						if (Main.netMode == 2 && num162 < 1000)
																						{
																							NetMessage.SendData(23, -1, -1, "", num162, 0f, 0f, 0f, 0);
																						}
																					}
																					return;
																				}
																			}
																		}
																	}
																	else
																	{
																		if (this.aiStyle == 16)
																		{
																			if (this.direction == 0)
																			{
																				this.TargetClosest(true);
																			}
																			if (this.wet)
																			{
																				if (this.collideX)
																				{
																					this.velocity.X = this.velocity.X * -1f;
																					this.direction *= -1;
																				}
																				if (this.collideY)
																				{
																					if (this.velocity.Y > 0f)
																					{
																						this.velocity.Y = Math.Abs(this.velocity.Y) * -1f;
																						this.directionY = -1;
																						this.ai[0] = -1f;
																					}
																					else
																					{
																						if (this.velocity.Y < 0f)
																						{
																							this.velocity.Y = Math.Abs(this.velocity.Y);
																							this.directionY = 1;
																							this.ai[0] = 1f;
																						}
																					}
																				}
																				bool flag16 = false;
																				if (!this.friendly)
																				{
																					this.TargetClosest(false);
																					if (Main.player[this.target].wet && !Main.player[this.target].dead)
																					{
																						flag16 = true;
																					}
																				}
																				if (flag16)
																				{
																					this.TargetClosest(true);
																					if (this.type == 65)
																					{
																						this.velocity.X = this.velocity.X + (float)this.direction * 0.15f;
																						this.velocity.Y = this.velocity.Y + (float)this.directionY * 0.15f;
																						if (this.velocity.X > 5f)
																						{
																							this.velocity.X = 5f;
																						}
																						if (this.velocity.X < -5f)
																						{
																							this.velocity.X = -5f;
																						}
																						if (this.velocity.Y > 3f)
																						{
																							this.velocity.Y = 3f;
																						}
																						if (this.velocity.Y < -3f)
																						{
																							this.velocity.Y = -3f;
																						}
																					}
																					else
																					{
																						this.velocity.X = this.velocity.X + (float)this.direction * 0.1f;
																						this.velocity.Y = this.velocity.Y + (float)this.directionY * 0.1f;
																						if (this.velocity.X > 3f)
																						{
																							this.velocity.X = 3f;
																						}
																						if (this.velocity.X < -3f)
																						{
																							this.velocity.X = -3f;
																						}
																						if (this.velocity.Y > 2f)
																						{
																							this.velocity.Y = 2f;
																						}
																						if (this.velocity.Y < -2f)
																						{
																							this.velocity.Y = -2f;
																						}
																					}
																				}
																				else
																				{
																					this.velocity.X = this.velocity.X + (float)this.direction * 0.1f;
																					if (this.velocity.X < -1f || this.velocity.X > 1f)
																					{
																						this.velocity.X = this.velocity.X * 0.95f;
																					}
																					if (this.ai[0] == -1f)
																					{
																						this.velocity.Y = this.velocity.Y - 0.01f;
																						if ((double)this.velocity.Y < -0.3)
																						{
																							this.ai[0] = 1f;
																						}
																					}
																					else
																					{
																						this.velocity.Y = this.velocity.Y + 0.01f;
																						if ((double)this.velocity.Y > 0.3)
																						{
																							this.ai[0] = -1f;
																						}
																					}
																					int num163 = (int)(this.position.X + (float)(this.width / 2)) / 16;
																					int num164 = (int)(this.position.Y + (float)(this.height / 2)) / 16;
																					if (Main.tile[num163, num164 - 1] == null)
																					{
																						Main.tile[num163, num164 - 1] = new Tile();
																					}
																					if (Main.tile[num163, num164 + 1] == null)
																					{
																						Main.tile[num163, num164 + 1] = new Tile();
																					}
																					if (Main.tile[num163, num164 + 2] == null)
																					{
																						Main.tile[num163, num164 + 2] = new Tile();
																					}
																					if (Main.tile[num163, num164 - 1].liquid > 128)
																					{
																						if (Main.tile[num163, num164 + 1].active)
																						{
																							this.ai[0] = -1f;
																						}
																						else
																						{
																							if (Main.tile[num163, num164 + 2].active)
																							{
																								this.ai[0] = -1f;
																							}
																						}
																					}
																					if ((double)this.velocity.Y > 0.4 || (double)this.velocity.Y < -0.4)
																					{
																						this.velocity.Y = this.velocity.Y * 0.95f;
																					}
																				}
																			}
																			else
																			{
																				if (this.velocity.Y == 0f)
																				{
																					if (this.type == 65)
																					{
																						this.velocity.X = this.velocity.X * 0.94f;
																						if ((double)this.velocity.X > -0.2 && (double)this.velocity.X < 0.2)
																						{
																							this.velocity.X = 0f;
																						}
																					}
																					else
																					{
																						if (Main.netMode != 1)
																						{
																							this.velocity.Y = (float)Main.rand.Next(-50, -20) * 0.1f;
																							this.velocity.X = (float)Main.rand.Next(-20, 20) * 0.1f;
																							this.netUpdate = true;
																						}
																					}
																				}
																				this.velocity.Y = this.velocity.Y + 0.3f;
																				if (this.velocity.Y > 10f)
																				{
																					this.velocity.Y = 10f;
																				}
																				this.ai[0] = 1f;
																			}
																			this.rotation = this.velocity.Y * (float)this.direction * 0.1f;
																			if ((double)this.rotation < -0.2)
																			{
																				this.rotation = -0.2f;
																			}
																			if ((double)this.rotation > 0.2)
																			{
																				this.rotation = 0.2f;
																				return;
																			}
																		}
																		else
																		{
																			if (this.aiStyle == 17)
																			{
																				this.noGravity = true;
																				if (this.ai[0] == 0f)
																				{
																					this.noGravity = false;
																					this.TargetClosest(true);
																					if (Main.netMode != 1)
																					{
																						if (this.velocity.X != 0f || this.velocity.Y < 0f || (double)this.velocity.Y > 0.3)
																						{
																							this.ai[0] = 1f;
																							this.netUpdate = true;
																						}
																						else
																						{
																							Rectangle rectangle5 = new Rectangle((int)Main.player[this.target].position.X, (int)Main.player[this.target].position.Y, Main.player[this.target].width, Main.player[this.target].height);
																							Rectangle rectangle6 = new Rectangle((int)this.position.X - 100, (int)this.position.Y - 100, this.width + 200, this.height + 200);
																							if (rectangle6.Intersects(rectangle5) || this.life < this.lifeMax)
																							{
																								this.ai[0] = 1f;
																								this.velocity.Y = this.velocity.Y - 6f;
																								this.netUpdate = true;
																							}
																						}
																					}
																				}
																				else
																				{
																					if (!Main.player[this.target].dead)
																					{
																						if (this.collideX)
																						{
																							this.velocity.X = this.oldVelocity.X * -0.5f;
																							if (this.direction == -1 && this.velocity.X > 0f && this.velocity.X < 2f)
																							{
																								this.velocity.X = 2f;
																							}
																							if (this.direction == 1 && this.velocity.X < 0f && this.velocity.X > -2f)
																							{
																								this.velocity.X = -2f;
																							}
																						}
																						if (this.collideY)
																						{
																							this.velocity.Y = this.oldVelocity.Y * -0.5f;
																							if (this.velocity.Y > 0f && this.velocity.Y < 1f)
																							{
																								this.velocity.Y = 1f;
																							}
																							if (this.velocity.Y < 0f && this.velocity.Y > -1f)
																							{
																								this.velocity.Y = -1f;
																							}
																						}
																						this.TargetClosest(true);
																						if (this.direction == -1 && this.velocity.X > -3f)
																						{
																							this.velocity.X = this.velocity.X - 0.1f;
																							if (this.velocity.X > 3f)
																							{
																								this.velocity.X = this.velocity.X - 0.1f;
																							}
																							else
																							{
																								if (this.velocity.X > 0f)
																								{
																									this.velocity.X = this.velocity.X - 0.05f;
																								}
																							}
																							if (this.velocity.X < -3f)
																							{
																								this.velocity.X = -3f;
																							}
																						}
																						else
																						{
																							if (this.direction == 1 && this.velocity.X < 3f)
																							{
																								this.velocity.X = this.velocity.X + 0.1f;
																								if (this.velocity.X < -3f)
																								{
																									this.velocity.X = this.velocity.X + 0.1f;
																								}
																								else
																								{
																									if (this.velocity.X < 0f)
																									{
																										this.velocity.X = this.velocity.X + 0.05f;
																									}
																								}
																								if (this.velocity.X > 3f)
																								{
																									this.velocity.X = 3f;
																								}
																							}
																						}
																						float num165 = Math.Abs(this.position.X + (float)(this.width / 2) - (Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2)));
																						float num166 = Main.player[this.target].position.Y - (float)(this.height / 2);
																						if (num165 > 50f)
																						{
																							num166 -= 100f;
																						}
																						if (this.position.Y < num166)
																						{
																							this.velocity.Y = this.velocity.Y + 0.05f;
																							if (this.velocity.Y < 0f)
																							{
																								this.velocity.Y = this.velocity.Y + 0.01f;
																							}
																						}
																						else
																						{
																							this.velocity.Y = this.velocity.Y - 0.05f;
																							if (this.velocity.Y > 0f)
																							{
																								this.velocity.Y = this.velocity.Y - 0.01f;
																							}
																						}
																						if (this.velocity.Y < -3f)
																						{
																							this.velocity.Y = -3f;
																						}
																						if (this.velocity.Y > 3f)
																						{
																							this.velocity.Y = 3f;
																						}
																					}
																				}
																				if (this.wet)
																				{
																					if (this.velocity.Y > 0f)
																					{
																						this.velocity.Y = this.velocity.Y * 0.95f;
																					}
																					this.velocity.Y = this.velocity.Y - 0.5f;
																					if (this.velocity.Y < -4f)
																					{
																						this.velocity.Y = -4f;
																					}
																					this.TargetClosest(true);
																					return;
																				}
																			}
																			else
																			{
																				if (this.aiStyle == 18)
																				{
																					Lighting.addLight((int)(this.position.X + (float)(this.height / 2)) / 16, (int)(this.position.Y + (float)(this.height / 2)) / 16, 0.4f);
																					if (this.direction == 0)
																					{
																						this.TargetClosest(true);
																					}
																					if (!this.wet)
																					{
																						this.rotation += this.velocity.X * 0.1f;
																						if (this.velocity.Y == 0f)
																						{
																							this.velocity.X = this.velocity.X * 0.98f;
																							if ((double)this.velocity.X > -0.01 && (double)this.velocity.X < 0.01)
																							{
																								this.velocity.X = 0f;
																							}
																						}
																						this.velocity.Y = this.velocity.Y + 0.2f;
																						if (this.velocity.Y > 10f)
																						{
																							this.velocity.Y = 10f;
																						}
																						this.ai[0] = 1f;
																						return;
																					}
																					if (this.collideX)
																					{
																						this.velocity.X = this.velocity.X * -1f;
																						this.direction *= -1;
																					}
																					if (this.collideY)
																					{
																						if (this.velocity.Y > 0f)
																						{
																							this.velocity.Y = Math.Abs(this.velocity.Y) * -1f;
																							this.directionY = -1;
																							this.ai[0] = -1f;
																						}
																						else
																						{
																							if (this.velocity.Y < 0f)
																							{
																								this.velocity.Y = Math.Abs(this.velocity.Y);
																								this.directionY = 1;
																								this.ai[0] = 1f;
																							}
																						}
																					}
																					bool flag17 = false;
																					if (!this.friendly)
																					{
																						this.TargetClosest(false);
																						if (Main.player[this.target].wet && !Main.player[this.target].dead)
																						{
																							flag17 = true;
																						}
																					}
																					if (flag17)
																					{
																						this.rotation = (float)Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) + 1.57f;
																						this.velocity *= 0.98f;
																						float num167 = 0.2f;
																						if (this.velocity.X > -num167 && this.velocity.X < num167 && this.velocity.Y > -num167 && this.velocity.Y < num167)
																						{
																							this.TargetClosest(true);
																							float num168 = 7f;
																							Vector2 vector21 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
																							float num169 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector21.X;
																							float num170 = Main.player[this.target].position.Y + (float)(Main.player[this.target].height / 2) - vector21.Y;
																							float num171 = (float)Math.Sqrt((double)(num169 * num169 + num170 * num170));
																							num171 = num168 / num171;
																							num169 *= num171;
																							num170 *= num171;
																							this.velocity.X = num169;
																							this.velocity.Y = num170;
																							return;
																						}
																					}
																					else
																					{
																						this.velocity.X = this.velocity.X + (float)this.direction * 0.02f;
																						this.rotation = this.velocity.X * 0.4f;
																						if (this.velocity.X < -1f || this.velocity.X > 1f)
																						{
																							this.velocity.X = this.velocity.X * 0.95f;
																						}
																						if (this.ai[0] == -1f)
																						{
																							this.velocity.Y = this.velocity.Y - 0.01f;
																							if (this.velocity.Y < -1f)
																							{
																								this.ai[0] = 1f;
																							}
																						}
																						else
																						{
																							this.velocity.Y = this.velocity.Y + 0.01f;
																							if (this.velocity.Y > 1f)
																							{
																								this.ai[0] = -1f;
																							}
																						}
																						int num172 = (int)(this.position.X + (float)(this.width / 2)) / 16;
																						int num173 = (int)(this.position.Y + (float)(this.height / 2)) / 16;
																						if (Main.tile[num172, num173 - 1] == null)
																						{
																							Main.tile[num172, num173 - 1] = new Tile();
																						}
																						if (Main.tile[num172, num173 + 1] == null)
																						{
																							Main.tile[num172, num173 + 1] = new Tile();
																						}
																						if (Main.tile[num172, num173 + 2] == null)
																						{
																							Main.tile[num172, num173 + 2] = new Tile();
																						}
																						if (Main.tile[num172, num173 - 1].liquid > 128)
																						{
																							if (Main.tile[num172, num173 + 1].active)
																							{
																								this.ai[0] = -1f;
																							}
																							else
																							{
																								if (Main.tile[num172, num173 + 2].active)
																								{
																									this.ai[0] = -1f;
																								}
																							}
																						}
																						else
																						{
																							this.ai[0] = 1f;
																						}
																						if ((double)this.velocity.Y > 1.2 || (double)this.velocity.Y < -1.2)
																						{
																							this.velocity.Y = this.velocity.Y * 0.99f;
																							return;
																						}
																					}
																				}
																				else
																				{
																					if (this.aiStyle == 19)
																					{
																						this.TargetClosest(true);
																						float num174 = 12f;
																						Vector2 vector22 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
																						float num175 = Main.player[this.target].position.X + (float)(Main.player[this.target].width / 2) - vector22.X;
																						float num176 = Main.player[this.target].position.Y - vector22.Y;
																						float num177 = (float)Math.Sqrt((double)(num175 * num175 + num176 * num176));
																						num177 = num174 / num177;
																						num175 *= num177;
																						num176 *= num177;
																						bool flag18 = false;
																						if (this.directionY < 0)
																						{
																							this.rotation = (float)(Math.Atan2((double)num176, (double)num175) + 1.57);
																							flag18 = ((double)this.rotation >= -1.2 && (double)this.rotation <= 1.2);
																							if ((double)this.rotation < -0.8)
																							{
																								this.rotation = -0.8f;
																							}
																							else
																							{
																								if ((double)this.rotation > 0.8)
																								{
																									this.rotation = 0.8f;
																								}
																							}
																							if (this.velocity.X != 0f)
																							{
																								this.velocity.X = this.velocity.X * 0.9f;
																								if ((double)this.velocity.X > -0.1 || (double)this.velocity.X < 0.1)
																								{
																									this.netUpdate = true;
																									this.velocity.X = 0f;
																								}
																							}
																						}
																						if (this.ai[0] > 0f)
																						{
																							if (this.ai[0] == 200f)
																							{
																								Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 5);
																							}
																							this.ai[0] -= 1f;
																						}
																						if (Main.netMode != 1 && flag18 && this.ai[0] == 0f && Collision.CanHit(this.position, this.width, this.height, Main.player[this.target].position, Main.player[this.target].width, Main.player[this.target].height))
																						{
																							this.ai[0] = 200f;
																							int num178 = 10;
																							int num179 = 31;
																							int num180 = Projectile.NewProjectile(vector22.X, vector22.Y, num175, num176, num179, num178, 0f, Main.myPlayer);
																							Main.projectile[num180].ai[0] = 2f;
																							Main.projectile[num180].timeLeft = 300;
																							Main.projectile[num180].friendly = false;
																							NetMessage.SendData(27, -1, -1, "", num180, 0f, 0f, 0f, 0);
																							this.netUpdate = true;
																						}
																						try
																						{
																							int num181 = (int)this.position.X / 16;
																							int num182 = (int)(this.position.X + (float)(this.width / 2)) / 16;
																							int num183 = (int)(this.position.X + (float)this.width) / 16;
																							int num184 = (int)(this.position.Y + (float)this.height) / 16;
																							bool flag19 = false;
																							if (Main.tile[num181, num184] == null)
																							{
																								Main.tile[num181, num184] = new Tile();
																							}
																							if (Main.tile[num182, num184] == null)
																							{
																								Main.tile[num181, num184] = new Tile();
																							}
																							if (Main.tile[num183, num184] == null)
																							{
																								Main.tile[num181, num184] = new Tile();
																							}
																							if ((Main.tile[num181, num184].active && Main.tileSolid[(int)Main.tile[num181, num184].type]) || (Main.tile[num182, num184].active && Main.tileSolid[(int)Main.tile[num182, num184].type]) || (Main.tile[num183, num184].active && Main.tileSolid[(int)Main.tile[num183, num184].type]))
																							{
																								flag19 = true;
																							}
																							if (flag19)
																							{
																								this.noGravity = true;
																								this.noTileCollide = true;
																								this.velocity.Y = -0.2f;
																							}
																							else
																							{
																								this.noGravity = false;
																								this.noTileCollide = false;
																								if (Main.rand.Next(2) == 0)
																								{
																									Vector2 arg_C7BA_0 = new Vector2(this.position.X - 4f, this.position.Y + (float)this.height - 8f);
																									int arg_C7BA_1 = this.width + 8;
																									int arg_C7BA_2 = 24;
																									int arg_C7BA_3 = 32;
																									float arg_C7BA_4 = 0f;
																									float arg_C7BA_5 = this.velocity.Y / 2f;
																									int arg_C7BA_6 = 0;
																									Color newColor = default(Color);
																									int num185 = Dust.NewDust(arg_C7BA_0, arg_C7BA_1, arg_C7BA_2, arg_C7BA_3, arg_C7BA_4, arg_C7BA_5, arg_C7BA_6, newColor, 1f);
																									Dust expr_C7CE_cp_0 = Main.dust[num185];
																									expr_C7CE_cp_0.velocity.X = expr_C7CE_cp_0.velocity.X * 0.4f;
																									Dust expr_C7EC_cp_0 = Main.dust[num185];
																									expr_C7EC_cp_0.velocity.Y = expr_C7EC_cp_0.velocity.Y * -1f;
																									if (Main.rand.Next(2) == 0)
																									{
																										Main.dust[num185].noGravity = true;
																										Main.dust[num185].scale += 0.2f;
																									}
																								}
																							}
																							return;
																						}
																						catch
																						{
																							return;
																						}
																					}
																					if (this.aiStyle == 20)
																					{
																						if (this.ai[0] == 0f)
																						{
																							if (Main.netMode != 1)
																							{
																								this.TargetClosest(true);
																								this.direction *= -1;
																								this.directionY *= -1;
																								this.position.Y = this.position.Y + (float)(this.height / 2 + 8);
																								this.ai[1] = this.position.X + (float)(this.width / 2);
																								this.ai[2] = this.position.Y + (float)(this.height / 2);
																								if (this.direction == 0)
																								{
																									this.direction = 1;
																								}
																								if (this.directionY == 0)
																								{
																									this.directionY = 1;
																								}
																								this.ai[3] = 1f + (float)Main.rand.Next(15) * 0.1f;
																								this.velocity.Y = (float)(this.directionY * 6) * this.ai[3];
																								this.ai[0] += 1f;
																								this.netUpdate = true;
																								return;
																							}
																							this.ai[1] = this.position.X + (float)(this.width / 2);
																							this.ai[2] = this.position.Y + (float)(this.height / 2);
																							return;
																						}
																						else
																						{
																							float num186 = 6f * this.ai[3];
																							float num187 = 0.2f * this.ai[3];
																							float num188 = num186 / num187 / 2f;
																							if (this.ai[0] >= 1f && this.ai[0] < (float)((int)num188))
																							{
																								this.velocity.Y = (float)this.directionY * num186;
																								this.ai[0] += 1f;
																								return;
																							}
																							if (this.ai[0] >= (float)((int)num188))
																							{
																								this.netUpdate = true;
																								this.velocity.Y = 0f;
																								this.directionY *= -1;
																								this.velocity.X = num186 * (float)this.direction;
																								this.ai[0] = -1f;
																								return;
																							}
																							if (this.directionY > 0)
																							{
																								if (this.velocity.Y >= num186)
																								{
																									this.netUpdate = true;
																									this.directionY *= -1;
																									this.velocity.Y = num186;
																								}
																							}
																							else
																							{
																								if (this.directionY < 0 && this.velocity.Y <= -num186)
																								{
																									this.directionY *= -1;
																									this.velocity.Y = -num186;
																								}
																							}
																							if (this.direction > 0)
																							{
																								if (this.velocity.X >= num186)
																								{
																									this.direction *= -1;
																									this.velocity.X = num186;
																								}
																							}
																							else
																							{
																								if (this.direction < 0 && this.velocity.X <= -num186)
																								{
																									this.direction *= -1;
																									this.velocity.X = -num186;
																								}
																							}
																							this.velocity.X = this.velocity.X + num187 * (float)this.direction;
																							this.velocity.Y = this.velocity.Y + num187 * (float)this.directionY;
																							return;
																						}
																					}
																					else
																					{
																						if (this.aiStyle == 21)
																						{
																							if (this.ai[0] == 0f)
																							{
																								this.TargetClosest(true);
																								this.directionY = 1;
																								this.ai[0] = 1f;
																							}
																							int num189 = 6;
																							if (this.ai[1] == 0f)
																							{
																								this.rotation += (float)(this.direction * this.directionY) * 0.13f;
																								if (this.collideY)
																								{
																									this.ai[0] = 2f;
																								}
																								if (!this.collideY && this.ai[0] == 2f)
																								{
																									this.direction = -this.direction;
																									this.ai[1] = 1f;
																									this.ai[0] = 1f;
																								}
																								if (this.collideX)
																								{
																									this.directionY = -this.directionY;
																									this.ai[1] = 1f;
																								}
																							}
																							else
																							{
																								this.rotation -= (float)(this.direction * this.directionY) * 0.13f;
																								if (this.collideX)
																								{
																									this.ai[0] = 2f;
																								}
																								if (!this.collideX && this.ai[0] == 2f)
																								{
																									this.directionY = -this.directionY;
																									this.ai[1] = 0f;
																									this.ai[0] = 1f;
																								}
																								if (this.collideY)
																								{
																									this.direction = -this.direction;
																									this.ai[1] = 0f;
																								}
																							}
																							this.velocity.X = (float)(num189 * this.direction);
																							this.velocity.Y = (float)(num189 * this.directionY);
																							Lighting.addLight((int)(this.position.X + (float)(this.width / 2)) / 16, (int)(this.position.Y + (float)(this.height / 2)) / 16, 1f);
																							return;
																						}
																						int arg_CD73_0 = this.aiStyle;
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
		public void FindFrame()
		{
			int num = 1;
			if (!Main.dedServ)
			{
				num = Main.npcTexture[this.type].Height / Main.npcFrameCount[this.type];
			}
			int num2 = 0;
			if (this.aiAction == 0)
			{
				if (this.velocity.Y < 0f)
				{
					num2 = 2;
				}
				else
				{
					if (this.velocity.Y > 0f)
					{
						num2 = 3;
					}
					else
					{
						if (this.velocity.X != 0f)
						{
							num2 = 1;
						}
						else
						{
							num2 = 0;
						}
					}
				}
			}
			else
			{
				if (this.aiAction == 1)
				{
					num2 = 4;
				}
			}
			if (this.type == 1 || this.type == 16 || this.type == 59 || this.type == 71)
			{
				this.frameCounter += 1.0;
				if (num2 > 0)
				{
					this.frameCounter += 1.0;
				}
				if (num2 == 4)
				{
					this.frameCounter += 1.0;
				}
				if (this.frameCounter >= 8.0)
				{
					this.frame.Y = this.frame.Y + num;
					this.frameCounter = 0.0;
				}
				if (this.frame.Y >= num * Main.npcFrameCount[this.type])
				{
					this.frame.Y = 0;
				}
			}
			if (this.type == 50)
			{
				if (this.velocity.Y != 0f)
				{
					this.frame.Y = num * 4;
				}
				else
				{
					this.frameCounter += 1.0;
					if (num2 > 0)
					{
						this.frameCounter += 1.0;
					}
					if (num2 == 4)
					{
						this.frameCounter += 1.0;
					}
					if (this.frameCounter >= 8.0)
					{
						this.frame.Y = this.frame.Y + num;
						this.frameCounter = 0.0;
					}
					if (this.frame.Y >= num * 4)
					{
						this.frame.Y = 0;
					}
				}
			}
			if (this.type == 61)
			{
				this.spriteDirection = this.direction;
				this.rotation = this.velocity.X * 0.1f;
				if (this.velocity.X == 0f && this.velocity.Y == 0f)
				{
					this.frame.Y = 0;
					this.frameCounter = 0.0;
				}
				else
				{
					this.frameCounter += 1.0;
					if (this.frameCounter < 4.0)
					{
						this.frame.Y = num;
					}
					else
					{
						this.frame.Y = num * 2;
						if (this.frameCounter >= 7.0)
						{
							this.frameCounter = 0.0;
						}
					}
				}
			}
			if (this.type == 62 || this.type == 66)
			{
				this.spriteDirection = this.direction;
				this.rotation = this.velocity.X * 0.1f;
				this.frameCounter += 1.0;
				if (this.frameCounter < 6.0)
				{
					this.frame.Y = 0;
				}
				else
				{
					this.frame.Y = num;
					if (this.frameCounter >= 11.0)
					{
						this.frameCounter = 0.0;
					}
				}
			}
			if (this.type == 63 || this.type == 64)
			{
				this.frameCounter += 1.0;
				if (this.frameCounter < 6.0)
				{
					this.frame.Y = 0;
				}
				else
				{
					if (this.frameCounter < 12.0)
					{
						this.frame.Y = num;
					}
					else
					{
						if (this.frameCounter < 18.0)
						{
							this.frame.Y = num * 2;
						}
						else
						{
							this.frame.Y = num * 3;
							if (this.frameCounter >= 23.0)
							{
								this.frameCounter = 0.0;
							}
						}
					}
				}
			}
			if (this.type == 2 || this.type == 23)
			{
				if (this.type == 2)
				{
					if (this.velocity.X > 0f)
					{
						this.spriteDirection = 1;
						this.rotation = (float)Math.Atan2((double)this.velocity.Y, (double)this.velocity.X);
					}
					if (this.velocity.X < 0f)
					{
						this.spriteDirection = -1;
						this.rotation = (float)Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) + 3.14f;
					}
				}
				this.frameCounter += 1.0;
				if (this.frameCounter >= 8.0)
				{
					this.frame.Y = this.frame.Y + num;
					this.frameCounter = 0.0;
				}
				if (this.frame.Y >= num * Main.npcFrameCount[this.type])
				{
					this.frame.Y = 0;
				}
			}
			if (this.type == 55 || this.type == 57 || this.type == 58)
			{
				this.spriteDirection = this.direction;
				this.frameCounter += 1.0;
				if (this.wet)
				{
					if (this.frameCounter < 6.0)
					{
						this.frame.Y = 0;
					}
					else
					{
						if (this.frameCounter < 12.0)
						{
							this.frame.Y = num;
						}
						else
						{
							if (this.frameCounter < 18.0)
							{
								this.frame.Y = num * 2;
							}
							else
							{
								if (this.frameCounter < 24.0)
								{
									this.frame.Y = num * 3;
								}
								else
								{
									this.frameCounter = 0.0;
								}
							}
						}
					}
				}
				else
				{
					if (this.frameCounter < 6.0)
					{
						this.frame.Y = num * 4;
					}
					else
					{
						if (this.frameCounter < 12.0)
						{
							this.frame.Y = num * 5;
						}
						else
						{
							this.frameCounter = 0.0;
						}
					}
				}
			}
			if (this.type == 69)
			{
				if (this.ai[0] < 190f)
				{
					this.frameCounter += 1.0;
					if (this.frameCounter >= 6.0)
					{
						this.frameCounter = 0.0;
						this.frame.Y = this.frame.Y + num;
						if (this.frame.Y / num >= Main.npcFrameCount[this.type] - 1)
						{
							this.frame.Y = 0;
						}
					}
				}
				else
				{
					this.frameCounter = 0.0;
					this.frame.Y = num * (Main.npcFrameCount[this.type] - 1);
				}
			}
			if (this.type == 67)
			{
				if (this.velocity.Y == 0f)
				{
					this.spriteDirection = this.direction;
				}
				this.frameCounter += 1.0;
				if (this.frameCounter >= 6.0)
				{
					this.frameCounter = 0.0;
					this.frame.Y = this.frame.Y + num;
					if (this.frame.Y / num >= Main.npcFrameCount[this.type])
					{
						this.frame.Y = 0;
					}
				}
			}
			if (this.type == 72)
			{
				this.frameCounter += 1.0;
				if (this.frameCounter >= 3.0)
				{
					this.frameCounter = 0.0;
					this.frame.Y = this.frame.Y + num;
					if (this.frame.Y / num >= Main.npcFrameCount[this.type])
					{
						this.frame.Y = 0;
					}
				}
			}
			if (this.type == 65)
			{
				this.spriteDirection = this.direction;
				this.frameCounter += 1.0;
				if (this.wet)
				{
					if (this.frameCounter < 6.0)
					{
						this.frame.Y = 0;
					}
					else
					{
						if (this.frameCounter < 12.0)
						{
							this.frame.Y = num;
						}
						else
						{
							if (this.frameCounter < 18.0)
							{
								this.frame.Y = num * 2;
							}
							else
							{
								if (this.frameCounter < 24.0)
								{
									this.frame.Y = num * 3;
								}
								else
								{
									this.frameCounter = 0.0;
								}
							}
						}
					}
				}
			}
			if (this.type == 48 || this.type == 49 || this.type == 51 || this.type == 60)
			{
				if (this.velocity.X > 0f)
				{
					this.spriteDirection = 1;
				}
				if (this.velocity.X < 0f)
				{
					this.spriteDirection = -1;
				}
				this.rotation = this.velocity.X * 0.1f;
				this.frameCounter += 1.0;
				if (this.frameCounter >= 6.0)
				{
					this.frame.Y = this.frame.Y + num;
					this.frameCounter = 0.0;
				}
				if (this.frame.Y >= num * 4)
				{
					this.frame.Y = 0;
				}
			}
			if (this.type == 42)
			{
				this.frameCounter += 1.0;
				if (this.frameCounter < 2.0)
				{
					this.frame.Y = 0;
				}
				else
				{
					if (this.frameCounter < 4.0)
					{
						this.frame.Y = num;
					}
					else
					{
						if (this.frameCounter < 6.0)
						{
							this.frame.Y = num * 2;
						}
						else
						{
							if (this.frameCounter < 8.0)
							{
								this.frame.Y = num;
							}
							else
							{
								this.frameCounter = 0.0;
							}
						}
					}
				}
			}
			if (this.type == 43 || this.type == 56)
			{
				this.frameCounter += 1.0;
				if (this.frameCounter < 6.0)
				{
					this.frame.Y = 0;
				}
				else
				{
					if (this.frameCounter < 12.0)
					{
						this.frame.Y = num;
					}
					else
					{
						if (this.frameCounter < 18.0)
						{
							this.frame.Y = num * 2;
						}
						else
						{
							if (this.frameCounter < 24.0)
							{
								this.frame.Y = num;
							}
						}
					}
				}
				if (this.frameCounter == 23.0)
				{
					this.frameCounter = 0.0;
				}
			}
			if (this.type == 17 || this.type == 18 || this.type == 19 || this.type == 20 || this.type == 22 || this.type == 38 || this.type == 26 || this.type == 27 || this.type == 28 || this.type == 31 || this.type == 21 || this.type == 44 || this.type == 54 || this.type == 37 || this.type == 73)
			{
				if (this.velocity.Y == 0f)
				{
					if (this.direction == 1)
					{
						this.spriteDirection = 1;
					}
					if (this.direction == -1)
					{
						this.spriteDirection = -1;
					}
					if (this.velocity.X == 0f)
					{
						this.frame.Y = 0;
						this.frameCounter = 0.0;
					}
					else
					{
						this.frameCounter += (double)(Math.Abs(this.velocity.X) * 2f);
						this.frameCounter += 1.0;
						if (this.frameCounter > 6.0)
						{
							this.frame.Y = this.frame.Y + num;
							this.frameCounter = 0.0;
						}
						if (this.frame.Y / num >= Main.npcFrameCount[this.type])
						{
							this.frame.Y = num * 2;
						}
					}
				}
				else
				{
					this.frameCounter = 0.0;
					this.frame.Y = num;
					if (this.type == 21 || this.type == 31 || this.type == 44)
					{
						this.frame.Y = 0;
					}
				}
			}
			else
			{
				if (this.type == 3 || this.type == 52 || this.type == 53)
				{
					if (this.velocity.Y == 0f)
					{
						if (this.direction == 1)
						{
							this.spriteDirection = 1;
						}
						if (this.direction == -1)
						{
							this.spriteDirection = -1;
						}
					}
					if (this.velocity.Y != 0f || (this.direction == -1 && this.velocity.X > 0f) || (this.direction == 1 && this.velocity.X < 0f))
					{
						this.frameCounter = 0.0;
						this.frame.Y = num * 2;
					}
					else
					{
						if (this.velocity.X == 0f)
						{
							this.frameCounter = 0.0;
							this.frame.Y = 0;
						}
						else
						{
							this.frameCounter += (double)Math.Abs(this.velocity.X);
							if (this.frameCounter < 8.0)
							{
								this.frame.Y = 0;
							}
							else
							{
								if (this.frameCounter < 16.0)
								{
									this.frame.Y = num;
								}
								else
								{
									if (this.frameCounter < 24.0)
									{
										this.frame.Y = num * 2;
									}
									else
									{
										if (this.frameCounter < 32.0)
										{
											this.frame.Y = num;
										}
										else
										{
											this.frameCounter = 0.0;
										}
									}
								}
							}
						}
					}
				}
				else
				{
					if (this.type == 46 || this.type == 47)
					{
						if (this.velocity.Y == 0f)
						{
							if (this.direction == 1)
							{
								this.spriteDirection = 1;
							}
							if (this.direction == -1)
							{
								this.spriteDirection = -1;
							}
							if (this.velocity.X == 0f)
							{
								this.frame.Y = 0;
								this.frameCounter = 0.0;
							}
							else
							{
								this.frameCounter += (double)(Math.Abs(this.velocity.X) * 1f);
								this.frameCounter += 1.0;
								if (this.frameCounter > 6.0)
								{
									this.frame.Y = this.frame.Y + num;
									this.frameCounter = 0.0;
								}
								if (this.frame.Y / num >= Main.npcFrameCount[this.type])
								{
									this.frame.Y = 0;
								}
							}
						}
						else
						{
							if (this.velocity.Y < 0f)
							{
								this.frameCounter = 0.0;
								this.frame.Y = num * 4;
							}
							else
							{
								if (this.velocity.Y > 0f)
								{
									this.frameCounter = 0.0;
									this.frame.Y = num * 6;
								}
							}
						}
					}
					else
					{
						if (this.type == 4)
						{
							this.frameCounter += 1.0;
							if (this.frameCounter < 7.0)
							{
								this.frame.Y = 0;
							}
							else
							{
								if (this.frameCounter < 14.0)
								{
									this.frame.Y = num;
								}
								else
								{
									if (this.frameCounter < 21.0)
									{
										this.frame.Y = num * 2;
									}
									else
									{
										this.frameCounter = 0.0;
										this.frame.Y = 0;
									}
								}
							}
							if (this.ai[0] > 1f)
							{
								this.frame.Y = this.frame.Y + num * 3;
							}
						}
						else
						{
							if (this.type == 5)
							{
								this.frameCounter += 1.0;
								if (this.frameCounter >= 8.0)
								{
									this.frame.Y = this.frame.Y + num;
									this.frameCounter = 0.0;
								}
								if (this.frame.Y >= num * Main.npcFrameCount[this.type])
								{
									this.frame.Y = 0;
								}
							}
							else
							{
								if (this.type == 6)
								{
									this.frameCounter += 1.0;
									if (this.frameCounter >= 8.0)
									{
										this.frame.Y = this.frame.Y + num;
										this.frameCounter = 0.0;
									}
									if (this.frame.Y >= num * Main.npcFrameCount[this.type])
									{
										this.frame.Y = 0;
									}
								}
								else
								{
									if (this.type == 24)
									{
										if (this.velocity.Y == 0f)
										{
											if (this.direction == 1)
											{
												this.spriteDirection = 1;
											}
											if (this.direction == -1)
											{
												this.spriteDirection = -1;
											}
										}
										if (this.ai[1] > 0f)
										{
											if (this.frame.Y < 4)
											{
												this.frameCounter = 0.0;
											}
											this.frameCounter += 1.0;
											if (this.frameCounter <= 4.0)
											{
												this.frame.Y = num * 4;
											}
											else
											{
												if (this.frameCounter <= 8.0)
												{
													this.frame.Y = num * 5;
												}
												else
												{
													if (this.frameCounter <= 12.0)
													{
														this.frame.Y = num * 6;
													}
													else
													{
														if (this.frameCounter <= 16.0)
														{
															this.frame.Y = num * 7;
														}
														else
														{
															if (this.frameCounter <= 20.0)
															{
																this.frame.Y = num * 8;
															}
															else
															{
																this.frame.Y = num * 9;
																this.frameCounter = 100.0;
															}
														}
													}
												}
											}
										}
										else
										{
											this.frameCounter += 1.0;
											if (this.frameCounter <= 4.0)
											{
												this.frame.Y = 0;
											}
											else
											{
												if (this.frameCounter <= 8.0)
												{
													this.frame.Y = num;
												}
												else
												{
													if (this.frameCounter <= 12.0)
													{
														this.frame.Y = num * 2;
													}
													else
													{
														this.frame.Y = num * 3;
														if (this.frameCounter >= 16.0)
														{
															this.frameCounter = 0.0;
														}
													}
												}
											}
										}
									}
									else
									{
										if (this.type == 29 || this.type == 32 || this.type == 45)
										{
											if (this.velocity.Y == 0f)
											{
												if (this.direction == 1)
												{
													this.spriteDirection = 1;
												}
												if (this.direction == -1)
												{
													this.spriteDirection = -1;
												}
											}
											this.frame.Y = 0;
											if (this.velocity.Y != 0f)
											{
												this.frame.Y = this.frame.Y + num;
											}
											else
											{
												if (this.ai[1] > 0f)
												{
													this.frame.Y = this.frame.Y + num * 2;
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
			if (this.type == 34)
			{
				this.frameCounter += 1.0;
				if (this.frameCounter >= 4.0)
				{
					this.frame.Y = this.frame.Y + num;
					this.frameCounter = 0.0;
				}
				if (this.frame.Y >= num * Main.npcFrameCount[this.type])
				{
					this.frame.Y = 0;
				}
			}
		}
		public void TargetClosest(bool faceTarget = true)
		{
			float num = -1f;
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active && !Main.player[i].dead && (num == -1f || Math.Abs(Main.player[i].position.X + (float)(Main.player[i].width / 2) - this.position.X + (float)(this.width / 2)) + Math.Abs(Main.player[i].position.Y + (float)(Main.player[i].height / 2) - this.position.Y + (float)(this.height / 2)) < num))
				{
					num = Math.Abs(Main.player[i].position.X + (float)(Main.player[i].width / 2) - this.position.X + (float)(this.width / 2)) + Math.Abs(Main.player[i].position.Y + (float)(Main.player[i].height / 2) - this.position.Y + (float)(this.height / 2));
					this.target = i;
				}
			}
			if (this.target < 0 || this.target >= 255)
			{
				this.target = 0;
			}
			this.targetRect = new Rectangle((int)Main.player[this.target].position.X, (int)Main.player[this.target].position.Y, Main.player[this.target].width, Main.player[this.target].height);
			if (faceTarget)
			{
				this.direction = 1;
				if ((float)(this.targetRect.X + this.targetRect.Width / 2) < this.position.X + (float)(this.width / 2))
				{
					this.direction = -1;
				}
				this.directionY = 1;
				if ((float)(this.targetRect.Y + this.targetRect.Height / 2) < this.position.Y + (float)(this.height / 2))
				{
					this.directionY = -1;
				}
			}
			if (this.direction != this.oldDirection || this.directionY != this.oldDirectionY || this.target != this.oldTarget)
			{
				this.netUpdate = true;
			}
		}
		public void CheckActive()
		{
			if (this.active)
			{
				if (this.type == 8 || this.type == 9 || this.type == 11 || this.type == 12 || this.type == 14 || this.type == 15 || this.type == 40 || this.type == 41)
				{
					return;
				}
				if (this.townNPC)
				{
					if ((double)this.position.Y < Main.worldSurface * 18.0)
					{
						Rectangle rectangle = new Rectangle((int)(this.position.X + (float)(this.width / 2) - (float)NPC.townRangeX), (int)(this.position.Y + (float)(this.height / 2) - (float)NPC.townRangeY), NPC.townRangeX * 2, NPC.townRangeY * 2);
						for (int i = 0; i < 255; i++)
						{
							if (Main.player[i].active && rectangle.Intersects(new Rectangle((int)Main.player[i].position.X, (int)Main.player[i].position.Y, Main.player[i].width, Main.player[i].height)))
							{
								Main.player[i].townNPCs += this.npcSlots;
							}
						}
					}
					return;
				}
				bool flag = false;
				Rectangle rectangle2 = new Rectangle((int)(this.position.X + (float)(this.width / 2) - (float)NPC.activeRangeX), (int)(this.position.Y + (float)(this.height / 2) - (float)NPC.activeRangeY), NPC.activeRangeX * 2, NPC.activeRangeY * 2);
				Rectangle rectangle3 = new Rectangle((int)((double)(this.position.X + (float)(this.width / 2)) - (double)NPC.sWidth * 0.5 - (double)this.width), (int)((double)(this.position.Y + (float)(this.height / 2)) - (double)NPC.sHeight * 0.5 - (double)this.height), NPC.sWidth + this.width * 2, NPC.sHeight + this.height * 2);
				for (int j = 0; j < 255; j++)
				{
					if (Main.player[j].active)
					{
						if (rectangle2.Intersects(new Rectangle((int)Main.player[j].position.X, (int)Main.player[j].position.Y, Main.player[j].width, Main.player[j].height)))
						{
							flag = true;
							if (this.type != 25 && this.type != 30 && this.type != 33)
							{
								Main.player[j].activeNPCs += this.npcSlots;
							}
						}
						if (rectangle3.Intersects(new Rectangle((int)Main.player[j].position.X, (int)Main.player[j].position.Y, Main.player[j].width, Main.player[j].height)))
						{
							this.timeLeft = NPC.activeTime;
						}
						if (this.type == 7 || this.type == 10 || this.type == 13 || this.type == 39)
						{
							flag = true;
						}
						if (this.boss || this.type == 35 || this.type == 36)
						{
							flag = true;
						}
					}
				}
				this.timeLeft--;
				if (this.timeLeft <= 0)
				{
					flag = false;
				}
				if (!flag && Main.netMode != 1)
				{
					NPC.noSpawnCycle = true;
					this.active = false;
					if (Main.netMode == 2)
					{
						this.life = 0;
						NetMessage.SendData(23, -1, -1, "", this.whoAmI, 0f, 0f, 0f, 0);
					}
				}
			}
		}
		public static void SpawnNPC()
		{
			if (NPC.noSpawnCycle)
			{
				NPC.noSpawnCycle = false;
				return;
			}
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					num3++;
				}
			}
			for (int j = 0; j < 255; j++)
			{
				if (Main.player[j].active && !Main.player[j].dead)
				{
					bool flag3 = false;
					bool flag4 = false;
					if (Main.player[j].active && Main.invasionType > 0 && Main.invasionDelay == 0 && Main.invasionSize > 0 && (double)Main.player[j].position.Y < Main.worldSurface * 16.0 + (double)NPC.sHeight)
					{
						int num4 = 3000;
						if ((double)Main.player[j].position.X > Main.invasionX * 16.0 - (double)num4 && (double)Main.player[j].position.X < Main.invasionX * 16.0 + (double)num4)
						{
							flag3 = true;
						}
					}
					flag = false;
					NPC.spawnRate = NPC.defaultSpawnRate;
					NPC.maxSpawns = NPC.defaultMaxSpawns;
					if (Main.player[j].position.Y > (float)((Main.maxTilesY - 200) * 16))
					{
						NPC.maxSpawns = (int)((float)NPC.maxSpawns * 2f);
					}
					else
					{
						if ((double)Main.player[j].position.Y > Main.rockLayer * 16.0 + (double)NPC.sHeight)
						{
							NPC.spawnRate = (int)((double)NPC.spawnRate * 0.4);
							NPC.maxSpawns = (int)((float)NPC.maxSpawns * 1.9f);
						}
						else
						{
							if ((double)Main.player[j].position.Y > Main.worldSurface * 16.0 + (double)NPC.sHeight)
							{
								NPC.spawnRate = (int)((double)NPC.spawnRate * 0.5);
								NPC.maxSpawns = (int)((float)NPC.maxSpawns * 1.7f);
							}
							else
							{
								if (!Main.dayTime)
								{
									NPC.spawnRate = (int)((double)NPC.spawnRate * 0.6);
									NPC.maxSpawns = (int)((float)NPC.maxSpawns * 1.3f);
									if (Main.bloodMoon)
									{
										NPC.spawnRate = (int)((double)NPC.spawnRate * 0.3);
										NPC.maxSpawns = (int)((float)NPC.maxSpawns * 1.8f);
									}
								}
							}
						}
					}
					if (Main.player[j].zoneDungeon)
					{
						NPC.spawnRate = (int)((double)NPC.spawnRate * 0.35);
						NPC.maxSpawns = (int)((float)NPC.maxSpawns * 1.85f);
					}
					else
					{
						if (Main.player[j].zoneJungle)
						{
							NPC.spawnRate = (int)((double)NPC.spawnRate * 0.3);
							NPC.maxSpawns = (int)((float)NPC.maxSpawns * 1.6f);
						}
						else
						{
							if (Main.player[j].zoneEvil)
							{
								NPC.spawnRate = (int)((double)NPC.spawnRate * 0.65);
								NPC.maxSpawns = (int)((float)NPC.maxSpawns * 1.3f);
							}
							else
							{
								if (Main.player[j].zoneMeteor)
								{
									NPC.spawnRate = (int)((double)NPC.spawnRate * 0.4);
									NPC.maxSpawns = (int)((float)NPC.maxSpawns * 1.1f);
								}
							}
						}
					}
					if ((double)Main.player[j].activeNPCs < (double)NPC.maxSpawns * 0.2)
					{
						NPC.spawnRate = (int)((float)NPC.spawnRate * 0.6f);
					}
					else
					{
						if ((double)Main.player[j].activeNPCs < (double)NPC.maxSpawns * 0.4)
						{
							NPC.spawnRate = (int)((float)NPC.spawnRate * 0.7f);
						}
						else
						{
							if ((double)Main.player[j].activeNPCs < (double)NPC.maxSpawns * 0.6)
							{
								NPC.spawnRate = (int)((float)NPC.spawnRate * 0.8f);
							}
							else
							{
								if ((double)Main.player[j].activeNPCs < (double)NPC.maxSpawns * 0.8)
								{
									NPC.spawnRate = (int)((float)NPC.spawnRate * 0.9f);
								}
							}
						}
					}
					if ((double)(Main.player[j].position.Y * 16f) > (Main.worldSurface + Main.rockLayer) / 2.0 || Main.player[j].zoneEvil)
					{
						if ((double)Main.player[j].activeNPCs < (double)NPC.maxSpawns * 0.2)
						{
							NPC.spawnRate = (int)((float)NPC.spawnRate * 0.7f);
						}
						else
						{
							if ((double)Main.player[j].activeNPCs < (double)NPC.maxSpawns * 0.4)
							{
								NPC.spawnRate = (int)((float)NPC.spawnRate * 0.9f);
							}
						}
					}
					if (Main.player[j].inventory[Main.player[j].selectedItem].type == 148)
					{
						NPC.spawnRate = (int)((double)NPC.spawnRate * 0.75);
						NPC.maxSpawns = (int)((float)NPC.maxSpawns * 1.5f);
					}
					if (Main.player[j].enemySpawns)
					{
						NPC.spawnRate = (int)((double)NPC.spawnRate * 0.5);
						NPC.maxSpawns = (int)((float)NPC.maxSpawns * 2f);
					}
					if ((double)NPC.spawnRate < (double)NPC.defaultSpawnRate * 0.1)
					{
						NPC.spawnRate = (int)((double)NPC.defaultSpawnRate * 0.1);
					}
					if (NPC.maxSpawns > NPC.defaultMaxSpawns * 3)
					{
						NPC.maxSpawns = NPC.defaultMaxSpawns * 3;
					}
					if (flag3)
					{
						NPC.maxSpawns = (int)((double)NPC.defaultMaxSpawns * (1.0 + 0.4 * (double)num3));
						NPC.spawnRate = 30;
					}
					if (Main.player[j].zoneDungeon && !NPC.downedBoss3)
					{
						NPC.spawnRate = 10;
					}
					bool flag5 = false;
					if (!flag3 && (!Main.bloodMoon || Main.dayTime) && !Main.player[j].zoneDungeon && !Main.player[j].zoneEvil && !Main.player[j].zoneMeteor)
					{
						if (Main.player[j].townNPCs == 1f)
						{
							if (Main.rand.Next(3) <= 1)
							{
								flag5 = true;
								NPC.maxSpawns = (int)((double)((float)NPC.maxSpawns) * 0.6);
							}
							else
							{
								NPC.spawnRate = (int)((float)NPC.spawnRate * 2f);
							}
						}
						else
						{
							if (Main.player[j].townNPCs == 2f)
							{
								if (Main.rand.Next(3) == 0)
								{
									flag5 = true;
									NPC.maxSpawns = (int)((double)((float)NPC.maxSpawns) * 0.6);
								}
								else
								{
									NPC.spawnRate = (int)((float)NPC.spawnRate * 3f);
								}
							}
							else
							{
								if (Main.player[j].townNPCs >= 3f)
								{
									flag5 = true;
									NPC.maxSpawns = (int)((double)((float)NPC.maxSpawns) * 0.6);
								}
							}
						}
					}
					if (Main.player[j].active && !Main.player[j].dead && Main.player[j].activeNPCs < (float)NPC.maxSpawns && Main.rand.Next(NPC.spawnRate) == 0)
					{
						int num5 = (int)(Main.player[j].position.X / 16f) - NPC.spawnRangeX;
						int num6 = (int)(Main.player[j].position.X / 16f) + NPC.spawnRangeX;
						int num7 = (int)(Main.player[j].position.Y / 16f) - NPC.spawnRangeY;
						int num8 = (int)(Main.player[j].position.Y / 16f) + NPC.spawnRangeY;
						int num9 = (int)(Main.player[j].position.X / 16f) - NPC.safeRangeX;
						int num10 = (int)(Main.player[j].position.X / 16f) + NPC.safeRangeX;
						int num11 = (int)(Main.player[j].position.Y / 16f) - NPC.safeRangeY;
						int num12 = (int)(Main.player[j].position.Y / 16f) + NPC.safeRangeY;
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
						int k = 0;
						while (k < 50)
						{
							int num13 = Main.rand.Next(num5, num6);
							int num14 = Main.rand.Next(num7, num8);
							if (Main.tile[num13, num14].active && Main.tileSolid[(int)Main.tile[num13, num14].type])
							{
								goto IL_AC2;
							}
							if (!Main.wallHouse[(int)Main.tile[num13, num14].wall])
							{
								if (!flag3 && (double)num14 < Main.worldSurface * 0.30000001192092896 && !flag5 && ((double)num13 < (double)Main.maxTilesX * 0.35 || (double)num13 > (double)Main.maxTilesX * 0.65))
								{
									byte arg_97A_0 = Main.tile[num13, num14].type;
									num = num13;
									num2 = num14;
									flag = true;
									flag2 = true;
								}
								else
								{
									int l = num14;
									while (l < Main.maxTilesY)
									{
										if (Main.tile[num13, l].active && Main.tileSolid[(int)Main.tile[num13, l].type])
										{
											if (num13 < num9 || num13 > num10 || l < num11 || l > num12)
											{
												byte arg_9E8_0 = Main.tile[num13, l].type;
												num = num13;
												num2 = l;
												flag = true;
												break;
											}
											break;
										}
										else
										{
											l++;
										}
									}
								}
								if (!flag)
								{
									goto IL_AC2;
								}
								int num15 = num - NPC.spawnSpaceX / 2;
								int num16 = num + NPC.spawnSpaceX / 2;
								int num17 = num2 - NPC.spawnSpaceY;
								int num18 = num2;
								if (num15 < 0)
								{
									flag = false;
								}
								if (num16 > Main.maxTilesX)
								{
									flag = false;
								}
								if (num17 < 0)
								{
									flag = false;
								}
								if (num18 > Main.maxTilesY)
								{
									flag = false;
								}
								if (flag)
								{
									for (int m = num15; m < num16; m++)
									{
										for (int n = num17; n < num18; n++)
										{
											if (Main.tile[m, n].active && Main.tileSolid[(int)Main.tile[m, n].type])
											{
												flag = false;
												break;
											}
											if (Main.tile[m, n].lava)
											{
												flag = false;
												break;
											}
										}
									}
									goto IL_AC2;
								}
								goto IL_AC2;
							}
							IL_AC8:
							k++;
							continue;
							IL_AC2:
							if (!flag && !flag)
							{
								goto IL_AC8;
							}
							break;
						}
					}
					if (flag)
					{
						Rectangle rectangle = new Rectangle(num * 16, num2 * 16, 16, 16);
						for (int num19 = 0; num19 < 255; num19++)
						{
							if (Main.player[num19].active)
							{
								Rectangle rectangle2 = new Rectangle((int)(Main.player[num19].position.X + (float)(Main.player[num19].width / 2) - (float)(NPC.sWidth / 2) - (float)NPC.safeRangeX), (int)(Main.player[num19].position.Y + (float)(Main.player[num19].height / 2) - (float)(NPC.sHeight / 2) - (float)NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
								if (rectangle.Intersects(rectangle2))
								{
									flag = false;
								}
							}
						}
					}
					if (flag)
					{
						if (Main.player[j].zoneDungeon && (!Main.tileDungeon[(int)Main.tile[num, num2].type] || Main.tile[num, num2 - 1].wall == 0))
						{
							flag = false;
						}
						if (Main.tile[num, num2 - 1].liquid > 0 && Main.tile[num, num2 - 2].liquid > 0 && !Main.tile[num, num2 - 1].lava)
						{
							flag4 = true;
						}
					}
					if (flag)
					{
						flag = false;
						int num20 = (int)Main.tile[num, num2].type;
						int num21 = 1000;
						if (flag2)
						{
							NPC.NewNPC(num * 16 + 8, num2 * 16, 48, 0);
						}
						else
						{
							if (flag3)
							{
								if (Main.rand.Next(9) == 0)
								{
									NPC.NewNPC(num * 16 + 8, num2 * 16, 29, 0);
								}
								else
								{
									if (Main.rand.Next(5) == 0)
									{
										NPC.NewNPC(num * 16 + 8, num2 * 16, 26, 0);
									}
									else
									{
										if (Main.rand.Next(3) == 0)
										{
											NPC.NewNPC(num * 16 + 8, num2 * 16, 27, 0);
										}
										else
										{
											NPC.NewNPC(num * 16 + 8, num2 * 16, 28, 0);
										}
									}
								}
							}
							else
							{
								if (flag4 && (num < 250 || num > Main.maxTilesX - 250) && num20 == 53 && (double)num2 < Main.rockLayer)
								{
									if (Main.rand.Next(8) == 0)
									{
										NPC.NewNPC(num * 16 + 8, num2 * 16, 65, 0);
									}
									if (Main.rand.Next(3) == 0)
									{
										NPC.NewNPC(num * 16 + 8, num2 * 16, 67, 0);
									}
									else
									{
										NPC.NewNPC(num * 16 + 8, num2 * 16, 64, 0);
									}
								}
								else
								{
									if (flag4 && (((double)num2 > Main.rockLayer && Main.rand.Next(2) == 0) || num20 == 60))
									{
										NPC.NewNPC(num * 16 + 8, num2 * 16, 58, 0);
									}
									else
									{
										if (flag4 && (double)num2 > Main.worldSurface && Main.rand.Next(3) == 0)
										{
											NPC.NewNPC(num * 16 + 8, num2 * 16, 63, 0);
										}
										else
										{
											if (flag4 && Main.rand.Next(4) == 0)
											{
												if (Main.player[j].zoneEvil)
												{
													NPC.NewNPC(num * 16 + 8, num2 * 16, 57, 0);
												}
												else
												{
													NPC.NewNPC(num * 16 + 8, num2 * 16, 55, 0);
												}
											}
											else
											{
												if (flag5)
												{
													if (flag4)
													{
														NPC.NewNPC(num * 16 + 8, num2 * 16, 55, 0);
													}
													else
													{
														if (num20 != 2)
														{
															return;
														}
														NPC.NewNPC(num * 16 + 8, num2 * 16, 46, 0);
													}
												}
												else
												{
													if (Main.player[j].zoneDungeon)
													{
														if (!NPC.downedBoss3)
														{
															num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 68, 0);
														}
														else
														{
															if (Main.rand.Next(43) == 0)
															{
																num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 71, 0);
															}
															else
															{
																if (Main.rand.Next(3) == 0 && !NPC.NearSpikeBall(num, num2))
																{
																	num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 70, 0);
																}
																else
																{
																	if (Main.rand.Next(5) == 0)
																	{
																		num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 72, 0);
																	}
																	else
																	{
																		if (Main.rand.Next(7) == 0)
																		{
																			num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 34, 0);
																		}
																		else
																		{
																			if (Main.rand.Next(7) == 0)
																			{
																				num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 32, 0);
																			}
																			else
																			{
																				num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 31, 0);
																				if (Main.rand.Next(4) == 0)
																				{
																					Main.npc[num21].SetDefaults("Big Boned");
																				}
																				else
																				{
																					if (Main.rand.Next(5) == 0)
																					{
																						Main.npc[num21].SetDefaults("Short Bones");
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
														if (Main.player[j].zoneMeteor)
														{
															num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 23, 0);
														}
														else
														{
															if (Main.player[j].zoneEvil && Main.rand.Next(50) == 0)
															{
																num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 7, 1);
															}
															else
															{
																if (num20 == 60 && Main.rand.Next(500) == 0 && !Main.dayTime)
																{
																	num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 52, 0);
																}
																else
																{
																	if (num20 == 60 && (double)num2 > (Main.worldSurface + Main.rockLayer) / 2.0)
																	{
																		if (Main.rand.Next(3) == 0)
																		{
																			num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 43, 0);
																			Main.npc[num21].ai[0] = (float)num;
																			Main.npc[num21].ai[1] = (float)num2;
																			Main.npc[num21].netUpdate = true;
																		}
																		else
																		{
																			num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 42, 0);
																			if (Main.rand.Next(4) == 0)
																			{
																				Main.npc[num21].SetDefaults("Little Stinger");
																			}
																			else
																			{
																				if (Main.rand.Next(4) == 0)
																				{
																					Main.npc[num21].SetDefaults("Big Stinger");
																				}
																			}
																		}
																	}
																	else
																	{
																		if (num20 == 60 && Main.rand.Next(4) == 0)
																		{
																			num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 51, 0);
																		}
																		else
																		{
																			if (num20 == 60 && Main.rand.Next(8) == 0)
																			{
																				num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 56, 0);
																				Main.npc[num21].ai[0] = (float)num;
																				Main.npc[num21].ai[1] = (float)num2;
																				Main.npc[num21].netUpdate = true;
																			}
																			else
																			{
																				if ((num20 == 22 && Main.player[j].zoneEvil) || num20 == 23 || num20 == 25)
																				{
																					num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 6, 0);
																					if (Main.rand.Next(3) == 0)
																					{
																						Main.npc[num21].SetDefaults("Little Eater");
																					}
																					else
																					{
																						if (Main.rand.Next(3) == 0)
																						{
																							Main.npc[num21].SetDefaults("Big Eater");
																						}
																					}
																				}
																				else
																				{
																					if ((double)num2 <= Main.worldSurface)
																					{
																						if (Main.dayTime)
																						{
																							int num22 = Math.Abs(num - Main.spawnTileX);
																							if (num22 < Main.maxTilesX / 3 && Main.rand.Next(10) == 0 && num20 == 2)
																							{
																								NPC.NewNPC(num * 16 + 8, num2 * 16, 46, 0);
																							}
																							else
																							{
																								if (num22 > Main.maxTilesX / 3 && num20 == 2 && Main.rand.Next(300) == 0 && !NPC.AnyNPCs(50))
																								{
																									num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 50, 0);
																								}
																								else
																								{
																									if (num20 == 53 && Main.rand.Next(5) == 0 && !flag4)
																									{
																										num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 69, 0);
																									}
																									else
																									{
																										if (num20 == 53 && !flag4)
																										{
																											num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 61, 0);
																										}
																										else
																										{
																											if (num22 > Main.maxTilesX / 3 && Main.rand.Next(20) == 0)
																											{
																												num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 73, 0);
																											}
																											else
																											{
																												num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 1, 0);
																												if (num20 == 60)
																												{
																													Main.npc[num21].SetDefaults("Jungle Slime");
																												}
																												else
																												{
																													if (Main.rand.Next(3) == 0 || num22 < 200)
																													{
																														Main.npc[num21].SetDefaults("Green Slime");
																													}
																													else
																													{
																														if (Main.rand.Next(10) == 0 && num22 > 400)
																														{
																															Main.npc[num21].SetDefaults("Purple Slime");
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
																							if (Main.rand.Next(6) == 0 || (Main.moonPhase == 4 && Main.rand.Next(2) == 0))
																							{
																								num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 2, 0);
																							}
																							else
																							{
																								if (Main.rand.Next(250) == 0 && Main.bloodMoon)
																								{
																									NPC.NewNPC(num * 16 + 8, num2 * 16, 53, 0);
																								}
																								else
																								{
																									NPC.NewNPC(num * 16 + 8, num2 * 16, 3, 0);
																								}
																							}
																						}
																					}
																					else
																					{
																						if ((double)num2 <= Main.rockLayer)
																						{
																							if (Main.rand.Next(50) == 0)
																							{
																								num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 10, 1);
																							}
																							else
																							{
																								num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 1, 0);
																								if (Main.rand.Next(5) == 0)
																								{
																									Main.npc[num21].SetDefaults("Yellow Slime");
																								}
																								else
																								{
																									if (Main.rand.Next(2) == 0)
																									{
																										Main.npc[num21].SetDefaults("Blue Slime");
																									}
																									else
																									{
																										Main.npc[num21].SetDefaults("Red Slime");
																									}
																								}
																							}
																						}
																						else
																						{
																							if (num2 > Main.maxTilesY - 190)
																							{
																								if (Main.rand.Next(40) == 0 && !NPC.AnyNPCs(39))
																								{
																									num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 39, 1);
																								}
																								else
																								{
																									if (Main.rand.Next(14) == 0)
																									{
																										num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 24, 0);
																									}
																									else
																									{
																										if (Main.rand.Next(10) == 0)
																										{
																											if (Main.rand.Next(10) == 0)
																											{
																												num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 66, 0);
																											}
																											else
																											{
																												num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 62, 0);
																											}
																										}
																										else
																										{
																											if (Main.rand.Next(3) == 0)
																											{
																												num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 59, 0);
																											}
																											else
																											{
																												num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 60, 0);
																											}
																										}
																									}
																								}
																							}
																							else
																							{
																								if (Main.rand.Next(55) == 0)
																								{
																									num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 10, 1);
																								}
																								else
																								{
																									if (Main.rand.Next(10) == 0)
																									{
																										num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 16, 0);
																									}
																									else
																									{
																										if (Main.rand.Next(4) == 0)
																										{
																											num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 1, 0);
																											if (Main.player[j].zoneJungle)
																											{
																												Main.npc[num21].SetDefaults("Jungle Slime");
																											}
																											else
																											{
																												Main.npc[num21].SetDefaults("Black Slime");
																											}
																										}
																										else
																										{
																											if (Main.rand.Next(2) == 0)
																											{
																												if ((double)num2 > (Main.rockLayer + (double)Main.maxTilesY) / 2.0 && Main.rand.Next(700) == 0)
																												{
																													num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 45, 0);
																												}
																												else
																												{
																													if (Main.rand.Next(15) == 0)
																													{
																														num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 44, 0);
																													}
																													else
																													{
																														num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 21, 0);
																													}
																												}
																											}
																											else
																											{
																												if (Main.player[j].zoneJungle)
																												{
																													num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 51, 0);
																												}
																												else
																												{
																													num21 = NPC.NewNPC(num * 16 + 8, num2 * 16, 49, 0);
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
						if (Main.npc[num21].type == 1 && Main.rand.Next(250) == 0)
						{
							Main.npc[num21].SetDefaults("Pinky");
						}
						if (Main.netMode == 2 && num21 < 1000)
						{
							NetMessage.SendData(23, -1, -1, "", num21, 0f, 0f, 0f, 0);
							return;
						}
						break;
					}
				}
			}
		}
		public static void SpawnOnPlayer(int plr, int Type)
		{
			if (Main.netMode == 1)
			{
				return;
			}
			bool flag = false;
			int num = 0;
			int num2 = 0;
			int num3 = (int)(Main.player[plr].position.X / 16f) - NPC.spawnRangeX * 3;
			int num4 = (int)(Main.player[plr].position.X / 16f) + NPC.spawnRangeX * 3;
			int num5 = (int)(Main.player[plr].position.Y / 16f) - NPC.spawnRangeY * 3;
			int num6 = (int)(Main.player[plr].position.Y / 16f) + NPC.spawnRangeY * 3;
			int num7 = (int)(Main.player[plr].position.X / 16f) - NPC.safeRangeX;
			int num8 = (int)(Main.player[plr].position.X / 16f) + NPC.safeRangeX;
			int num9 = (int)(Main.player[plr].position.Y / 16f) - NPC.safeRangeY;
			int num10 = (int)(Main.player[plr].position.Y / 16f) + NPC.safeRangeY;
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
			for (int i = 0; i < 1000; i++)
			{
				int j = 0;
				while (j < 100)
				{
					int num11 = Main.rand.Next(num3, num4);
					int num12 = Main.rand.Next(num5, num6);
					if (Main.tile[num11, num12].active && Main.tileSolid[(int)Main.tile[num11, num12].type])
					{
						goto IL_2E1;
					}
					if (Main.tile[num11, num12].wall != 1)
					{
						int k = num12;
						while (k < Main.maxTilesY)
						{
							if (Main.tile[num11, k].active && Main.tileSolid[(int)Main.tile[num11, k].type])
							{
								if (num11 < num7 || num11 > num8 || k < num9 || k > num10)
								{
									byte arg_220_0 = Main.tile[num11, k].type;
									num = num11;
									num2 = k;
									flag = true;
									break;
								}
								break;
							}
							else
							{
								k++;
							}
						}
						if (!flag)
						{
							goto IL_2E1;
						}
						int num13 = num - NPC.spawnSpaceX / 2;
						int num14 = num + NPC.spawnSpaceX / 2;
						int num15 = num2 - NPC.spawnSpaceY;
						int num16 = num2;
						if (num13 < 0)
						{
							flag = false;
						}
						if (num14 > Main.maxTilesX)
						{
							flag = false;
						}
						if (num15 < 0)
						{
							flag = false;
						}
						if (num16 > Main.maxTilesY)
						{
							flag = false;
						}
						if (flag)
						{
							for (int l = num13; l < num14; l++)
							{
								for (int m = num15; m < num16; m++)
								{
									if (Main.tile[l, m].active && Main.tileSolid[(int)Main.tile[l, m].type])
									{
										flag = false;
										break;
									}
								}
							}
							goto IL_2E1;
						}
						goto IL_2E1;
					}
					IL_2E7:
					j++;
					continue;
					IL_2E1:
					if (!flag && !flag)
					{
						goto IL_2E7;
					}
					break;
				}
				if (flag)
				{
					Rectangle rectangle = new Rectangle(num * 16, num2 * 16, 16, 16);
					for (int n = 0; n < 255; n++)
					{
						if (Main.player[n].active)
						{
							Rectangle rectangle2 = new Rectangle((int)(Main.player[n].position.X + (float)(Main.player[n].width / 2) - (float)(NPC.sWidth / 2) - (float)NPC.safeRangeX), (int)(Main.player[n].position.Y + (float)(Main.player[n].height / 2) - (float)(NPC.sHeight / 2) - (float)NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
							if (rectangle.Intersects(rectangle2))
							{
								flag = false;
							}
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
				int num17 = NPC.NewNPC(num * 16 + 8, num2 * 16, Type, 1);
				Main.npc[num17].target = plr;
				string str = Main.npc[num17].name;
				if (Main.npc[num17].type == 13)
				{
					str = "Eater of Worlds";
				}
				if (Main.npc[num17].type == 35)
				{
					str = "Skeletron";
				}
				if (Main.netMode == 2 && num17 < 1000)
				{
					NetMessage.SendData(23, -1, -1, "", num17, 0f, 0f, 0f, 0);
				}
				if (Main.netMode == 0)
				{
					Main.NewText(str + " has awoken!", 175, 75, 255);
					return;
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(25, -1, -1, str + " has awoken!", 255, 175f, 75f, 255f, 0);
				}
			}
		}
		public static int NewNPC(int X, int Y, int Type, int Start = 0)
		{
			int num = -1;
			for (int i = Start; i < 1000; i++)
			{
				if (!Main.npc[i].active)
				{
					num = i;
					break;
				}
			}
			if (num >= 0)
			{
				Main.npc[num] = new NPC();
				Main.npc[num].SetDefaults(Type, -1f);
				Main.npc[num].position.X = (float)(X - Main.npc[num].width / 2);
				Main.npc[num].position.Y = (float)(Y - Main.npc[num].height);
				Main.npc[num].active = true;
				Main.npc[num].timeLeft = (int)((double)NPC.activeTime * 1.25);
				Main.npc[num].wet = Collision.WetCollision(Main.npc[num].position, Main.npc[num].width, Main.npc[num].height);
				if (Type == 50)
				{
					if (Main.netMode == 0)
					{
						Main.NewText(Main.npc[num].name + " has awoken!", 175, 75, 255);
					}
					else
					{
						if (Main.netMode == 2)
						{
							NetMessage.SendData(25, -1, -1, Main.npc[num].name + " has awoken!", 255, 175f, 75f, 255f, 0);
						}
					}
				}
				return num;
			}
			return 1000;
		}
		public void Transform(int newType)
		{
			if (Main.netMode != 1)
			{
				Vector2 vector = this.velocity;
				int num = this.spriteDirection;
				this.SetDefaults(newType, -1f);
				this.spriteDirection = num;
				this.TargetClosest(true);
				this.velocity = vector;
				if (Main.netMode == 2)
				{
					this.netUpdate = true;
					NetMessage.SendData(23, -1, -1, "", this.whoAmI, 0f, 0f, 0f, 0);
				}
			}
		}
		public double StrikeNPC(int Damage, float knockBack, int hitDirection, bool crit = false)
		{
			if (!this.active || this.life <= 0)
			{
				return 0.0;
			}
			double num = (double)Damage;
			num = Main.CalculateDamage((int)num, this.defense);
			if (crit)
			{
				num *= 2.0;
			}
			if (Damage != 9999 && this.lifeMax > 1)
			{
				if (this.friendly)
				{
					CombatText.NewText(new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height), new Color(255, 80, 90, 255), string.Concat((int)num), crit);
				}
				else
				{
					CombatText.NewText(new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height), new Color(255, 160, 80, 255), string.Concat((int)num), crit);
				}
			}
			if (num >= 1.0)
			{
				this.justHit = true;
				if (this.townNPC)
				{
					this.ai[0] = 1f;
					this.ai[1] = (float)(300 + Main.rand.Next(300));
					this.ai[2] = 0f;
					this.direction = hitDirection;
					this.netUpdate = true;
				}
				if (this.aiStyle == 8 && Main.netMode != 1)
				{
					this.ai[0] = 400f;
					this.TargetClosest(true);
				}
				this.life -= (int)num;
				if (knockBack > 0f && this.knockBackResist > 0f)
				{
					float num2 = knockBack * this.knockBackResist;
					if (crit)
					{
						num2 *= 1.4f;
					}
					if (num * 10.0 < (double)this.lifeMax)
					{
						if (hitDirection < 0 && this.velocity.X > -num2)
						{
							if (this.velocity.X > 0f)
							{
								this.velocity.X = this.velocity.X - num2;
							}
							this.velocity.X = this.velocity.X - num2;
							if (this.velocity.X < -num2)
							{
								this.velocity.X = -num2;
							}
						}
						else
						{
							if (hitDirection > 0 && this.velocity.X < num2)
							{
								if (this.velocity.X < 0f)
								{
									this.velocity.X = this.velocity.X + num2;
								}
								this.velocity.X = this.velocity.X + num2;
								if (this.velocity.X > num2)
								{
									this.velocity.X = num2;
								}
							}
						}
						if (!this.noGravity)
						{
							num2 *= -0.75f;
						}
						else
						{
							num2 *= -0.5f;
						}
						if (this.velocity.Y > num2)
						{
							this.velocity.Y = this.velocity.Y + num2;
							if (this.velocity.Y < num2)
							{
								this.velocity.Y = num2;
							}
						}
					}
					else
					{
						if (!this.noGravity)
						{
							this.velocity.Y = -num2 * 0.75f * this.knockBackResist;
						}
						else
						{
							this.velocity.Y = -num2 * 0.5f * this.knockBackResist;
						}
						this.velocity.X = num2 * (float)hitDirection * this.knockBackResist;
					}
				}
				this.HitEffect(hitDirection, num);
				if (this.soundHit > 0)
				{
					Main.PlaySound(3, (int)this.position.X, (int)this.position.Y, this.soundHit);
				}
				if (this.life <= 0)
				{
					NPC.noSpawnCycle = true;
					if (this.townNPC && this.type != 37)
					{
						if (Main.netMode == 0)
						{
							Main.NewText(this.name + " was slain...", 255, 25, 25);
						}
						else
						{
							if (Main.netMode == 2)
							{
								NetMessage.SendData(25, -1, -1, this.name + " was slain...", 255, 255f, 25f, 25f, 0);
							}
						}
					}
					if (this.townNPC && Main.netMode != 1 && this.homeless && WorldGen.spawnNPC == this.type)
					{
						WorldGen.spawnNPC = 0;
					}
					if (this.soundKilled > 0)
					{
						Main.PlaySound(4, (int)this.position.X, (int)this.position.Y, this.soundKilled);
					}
					this.NPCLoot();
					this.active = false;
					if (this.type == 26 || this.type == 27 || this.type == 28 || this.type == 29)
					{
						Main.invasionSize--;
					}
				}
				return num;
			}
			return 0.0;
		}
		public void NPCLoot()
		{
			if (this.type == 1 || this.type == 16)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 23, Main.rand.Next(1, 3), false);
			}
			if (this.type == 71)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 327, 1, false);
			}
			if (this.type == 2)
			{
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 38, 1, false);
				}
				else
				{
					if (Main.rand.Next(100) == 0)
					{
						Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 236, 1, false);
					}
				}
			}
			if (this.type == 58)
			{
				if (Main.rand.Next(500) == 0)
				{
					Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 263, 1, false);
				}
				else
				{
					if (Main.rand.Next(40) == 0)
					{
						Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 118, 1, false);
					}
				}
			}
			if (this.type == 3 && Main.rand.Next(50) == 0)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 216, 1, false);
			}
			if (this.type == 66)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 267, 1, false);
			}
			if (this.type == 62 && Main.rand.Next(50) == 0)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 272, 1, false);
			}
			if (this.type == 52)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 251, 1, false);
			}
			if (this.type == 53)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 239, 1, false);
			}
			if (this.type == 54)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 260, 1, false);
			}
			if (this.type == 55)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 261, 1, false);
			}
			if (this.type == 69 && Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 323, 1, false);
			}
			if (this.type == 73)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 362, 1, false);
			}
			if (this.type == 4)
			{
				int stack = Main.rand.Next(30) + 20;
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 47, stack, false);
				stack = Main.rand.Next(20) + 10;
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 56, stack, false);
				stack = Main.rand.Next(20) + 10;
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 56, stack, false);
				stack = Main.rand.Next(20) + 10;
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 56, stack, false);
				stack = Main.rand.Next(3) + 1;
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 59, stack, false);
			}
			if (this.type == 6 && Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 68, 1, false);
			}
			if (this.type == 7 || this.type == 8 || this.type == 9)
			{
				if (Main.rand.Next(3) == 0)
				{
					Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 68, Main.rand.Next(1, 3), false);
				}
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 69, Main.rand.Next(3, 9), false);
			}
			if ((this.type == 10 || this.type == 11 || this.type == 12) && Main.rand.Next(500) == 0)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 215, 1, false);
			}
			if (this.type == 47 && Main.rand.Next(75) == 0)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 243, 1, false);
			}
			if (this.type == 13 || this.type == 14 || this.type == 15)
			{
				int stack2 = Main.rand.Next(1, 3);
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 86, stack2, false);
				}
				if (Main.rand.Next(2) == 0)
				{
					stack2 = Main.rand.Next(2, 6);
					Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 56, stack2, false);
				}
				if (this.boss)
				{
					stack2 = Main.rand.Next(10, 30);
					Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 56, stack2, false);
					stack2 = Main.rand.Next(10, 31);
					Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 56, stack2, false);
				}
				if (Main.rand.Next(3) == 0 && Main.player[(int)Player.FindClosest(this.position, this.width, this.height)].statLife < Main.player[(int)Player.FindClosest(this.position, this.width, this.height)].statLifeMax)
				{
					Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 58, 1, false);
				}
			}
			if (this.type == 63 || this.type == 64)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 282, Main.rand.Next(1, 5), false);
			}
			if (this.type == 21 || this.type == 44)
			{
				if (Main.rand.Next(25) == 0)
				{
					Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 118, 1, false);
				}
				else
				{
					if (this.type == 44)
					{
						Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 166, Main.rand.Next(1, 4), false);
					}
				}
			}
			if (this.type == 45)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 238, 1, false);
			}
			if (this.type == 50)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, Main.rand.Next(256, 259), 1, false);
			}
			if (this.type == 23 && Main.rand.Next(50) == 0)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 116, 1, false);
			}
			if (this.type == 24 && Main.rand.Next(300) == 0)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 244, 1, false);
			}
			if (this.type == 31 || this.type == 32 || this.type == 34)
			{
				if (Main.rand.Next(75) == 0)
				{
					Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 327, 1, false);
				}
				else
				{
					Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 154, Main.rand.Next(1, 4), false);
				}
			}
			if (this.type == 26 || this.type == 27 || this.type == 28 || this.type == 29)
			{
				if (Main.rand.Next(400) == 0)
				{
					Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 128, 1, false);
				}
				else
				{
					if (Main.rand.Next(200) == 0)
					{
						Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 160, 1, false);
					}
					else
					{
						if (Main.rand.Next(2) == 0)
						{
							int stack3 = Main.rand.Next(1, 6);
							Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 161, stack3, false);
						}
					}
				}
			}
			if (this.type == 42 && Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 209, 1, false);
			}
			if (this.type == 43 && Main.rand.Next(4) == 0)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 210, 1, false);
			}
			if (this.type == 65)
			{
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 268, 1, false);
				}
				else
				{
					Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 319, 1, false);
				}
			}
			if (this.type == 48 && Main.rand.Next(5) == 0)
			{
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 320, 1, false);
			}
			if (this.boss)
			{
				if (this.type == 4)
				{
					NPC.downedBoss1 = true;
				}
				if (this.type == 13 || this.type == 14 || this.type == 15)
				{
					NPC.downedBoss2 = true;
					this.name = "Eater of Worlds";
				}
				if (this.type == 35)
				{
					NPC.downedBoss3 = true;
					this.name = "Skeletron";
				}
				int stack4 = Main.rand.Next(5, 16);
				Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 28, stack4, false);
				int num = Main.rand.Next(5) + 5;
				for (int i = 0; i < num; i++)
				{
					Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 58, 1, false);
				}
				if (Main.netMode == 0)
				{
					Main.NewText(this.name + " has been defeated!", 175, 75, 255);
				}
				else
				{
					if (Main.netMode == 2)
					{
						NetMessage.SendData(25, -1, -1, this.name + " has been defeated!", 255, 175f, 75f, 255f, 0);
					}
				}
			}
			if (Main.rand.Next(6) == 0 && this.lifeMax > 1)
			{
				if (Main.rand.Next(2) == 0 && Main.player[(int)Player.FindClosest(this.position, this.width, this.height)].statMana < Main.player[(int)Player.FindClosest(this.position, this.width, this.height)].statManaMax)
				{
					Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 184, 1, false);
				}
				else
				{
					if (Main.rand.Next(2) == 0 && Main.player[(int)Player.FindClosest(this.position, this.width, this.height)].statLife < Main.player[(int)Player.FindClosest(this.position, this.width, this.height)].statLifeMax)
					{
						Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 58, 1, false);
					}
				}
			}
			float num2 = this.value;
			num2 *= 1f + (float)Main.rand.Next(-20, 21) * 0.01f;
			if (Main.rand.Next(5) == 0)
			{
				num2 *= 1f + (float)Main.rand.Next(5, 11) * 0.01f;
			}
			if (Main.rand.Next(10) == 0)
			{
				num2 *= 1f + (float)Main.rand.Next(10, 21) * 0.01f;
			}
			if (Main.rand.Next(15) == 0)
			{
				num2 *= 1f + (float)Main.rand.Next(15, 31) * 0.01f;
			}
			if (Main.rand.Next(20) == 0)
			{
				num2 *= 1f + (float)Main.rand.Next(20, 41) * 0.01f;
			}
			while ((int)num2 > 0)
			{
				if (num2 > 1000000f)
				{
					int num3 = (int)(num2 / 1000000f);
					if (num3 > 50 && Main.rand.Next(2) == 0)
					{
						num3 /= Main.rand.Next(3) + 1;
					}
					if (Main.rand.Next(2) == 0)
					{
						num3 /= Main.rand.Next(3) + 1;
					}
					num2 -= (float)(1000000 * num3);
					Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 74, num3, false);
				}
				else
				{
					if (num2 > 10000f)
					{
						int num4 = (int)(num2 / 10000f);
						if (num4 > 50 && Main.rand.Next(2) == 0)
						{
							num4 /= Main.rand.Next(3) + 1;
						}
						if (Main.rand.Next(2) == 0)
						{
							num4 /= Main.rand.Next(3) + 1;
						}
						num2 -= (float)(10000 * num4);
						Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 73, num4, false);
					}
					else
					{
						if (num2 > 100f)
						{
							int num5 = (int)(num2 / 100f);
							if (num5 > 50 && Main.rand.Next(2) == 0)
							{
								num5 /= Main.rand.Next(3) + 1;
							}
							if (Main.rand.Next(2) == 0)
							{
								num5 /= Main.rand.Next(3) + 1;
							}
							num2 -= (float)(100 * num5);
							Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 72, num5, false);
						}
						else
						{
							int num6 = (int)num2;
							if (num6 > 50 && Main.rand.Next(2) == 0)
							{
								num6 /= Main.rand.Next(3) + 1;
							}
							if (Main.rand.Next(2) == 0)
							{
								num6 /= Main.rand.Next(4) + 1;
							}
							if (num6 < 1)
							{
								num6 = 1;
							}
							num2 -= (float)num6;
							Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 71, num6, false);
						}
					}
				}
			}
		}
		public void HitEffect(int hitDirection = 0, double dmg = 10.0)
		{
			if (this.type == 1 || this.type == 16 || this.type == 71)
			{
				if (this.life > 0)
				{
					int num = 0;
					while ((double)num < dmg / (double)this.lifeMax * 100.0)
					{
						Dust.NewDust(this.position, this.width, this.height, 4, (float)hitDirection, -1f, this.alpha, this.color, 1f);
						num++;
					}
				}
				else
				{
					for (int i = 0; i < 50; i++)
					{
						Dust.NewDust(this.position, this.width, this.height, 4, (float)(2 * hitDirection), -2f, this.alpha, this.color, 1f);
					}
					if (Main.netMode != 1 && this.type == 16)
					{
						int num2 = Main.rand.Next(2) + 2;
						for (int j = 0; j < num2; j++)
						{
							int num3 = NPC.NewNPC((int)(this.position.X + (float)(this.width / 2)), (int)(this.position.Y + (float)this.height), 1, 0);
							Main.npc[num3].SetDefaults("Baby Slime");
							Main.npc[num3].velocity.X = this.velocity.X * 2f;
							Main.npc[num3].velocity.Y = this.velocity.Y;
							NPC expr_17D_cp_0 = Main.npc[num3];
							expr_17D_cp_0.velocity.X = expr_17D_cp_0.velocity.X + ((float)Main.rand.Next(-20, 20) * 0.1f + (float)(j * this.direction) * 0.3f);
							NPC expr_1BB_cp_0 = Main.npc[num3];
							expr_1BB_cp_0.velocity.Y = expr_1BB_cp_0.velocity.Y - ((float)Main.rand.Next(0, 10) * 0.1f + (float)j);
							Main.npc[num3].ai[1] = (float)j;
							if (Main.netMode == 2 && num3 < 1000)
							{
								NetMessage.SendData(23, -1, -1, "", num3, 0f, 0f, 0f, 0);
							}
						}
					}
				}
			}
			if (this.type == 63 || this.type == 64)
			{
				Color newColor = new Color(50, 120, 255, 100);
				if (this.type == 64)
				{
					newColor = new Color(225, 70, 140, 100);
				}
				if (this.life > 0)
				{
					int num4 = 0;
					while ((double)num4 < dmg / (double)this.lifeMax * 50.0)
					{
						Dust.NewDust(this.position, this.width, this.height, 4, (float)hitDirection, -1f, 0, newColor, 1f);
						num4++;
					}
					return;
				}
				for (int k = 0; k < 25; k++)
				{
					Dust.NewDust(this.position, this.width, this.height, 4, (float)(2 * hitDirection), -2f, 0, newColor, 1f);
				}
				return;
			}
			else
			{
				if (this.type != 59 && this.type != 60)
				{
					if (this.type == 50)
					{
						if (this.life > 0)
						{
							int num5 = 0;
							while ((double)num5 < dmg / (double)this.lifeMax * 300.0)
							{
								Dust.NewDust(this.position, this.width, this.height, 4, (float)hitDirection, -1f, 175, new Color(0, 80, 255, 100), 1f);
								num5++;
							}
							return;
						}
						for (int l = 0; l < 200; l++)
						{
							Dust.NewDust(this.position, this.width, this.height, 4, (float)(2 * hitDirection), -2f, 175, new Color(0, 80, 255, 100), 1f);
						}
						if (Main.netMode != 1)
						{
							int num6 = Main.rand.Next(4) + 4;
							for (int m = 0; m < num6; m++)
							{
								int x = (int)(this.position.X + (float)Main.rand.Next(this.width - 32));
								int y = (int)(this.position.Y + (float)Main.rand.Next(this.height - 32));
								int num7 = NPC.NewNPC(x, y, 1, 0);
								Main.npc[num7].SetDefaults(1, -1f);
								Main.npc[num7].velocity.X = (float)Main.rand.Next(-15, 16) * 0.1f;
								Main.npc[num7].velocity.Y = (float)Main.rand.Next(-30, 1) * 0.1f;
								Main.npc[num7].ai[1] = (float)Main.rand.Next(3);
								if (Main.netMode == 2 && num7 < 1000)
								{
									NetMessage.SendData(23, -1, -1, "", num7, 0f, 0f, 0f, 0);
								}
							}
							return;
						}
					}
					else
					{
						if (this.type == 49 || this.type == 51)
						{
							if (this.life > 0)
							{
								int num8 = 0;
								while ((double)num8 < dmg / (double)this.lifeMax * 30.0)
								{
									Vector2 arg_60A_0 = this.position;
									int arg_60A_1 = this.width;
									int arg_60A_2 = this.height;
									int arg_60A_3 = 5;
									float arg_60A_4 = (float)hitDirection;
									float arg_60A_5 = -1f;
									int arg_60A_6 = 0;
									Color newColor2 = default(Color);
									Dust.NewDust(arg_60A_0, arg_60A_1, arg_60A_2, arg_60A_3, arg_60A_4, arg_60A_5, arg_60A_6, newColor2, 1f);
									num8++;
								}
								return;
							}
							for (int n = 0; n < 15; n++)
							{
								Vector2 arg_660_0 = this.position;
								int arg_660_1 = this.width;
								int arg_660_2 = this.height;
								int arg_660_3 = 5;
								float arg_660_4 = (float)(2 * hitDirection);
								float arg_660_5 = -2f;
								int arg_660_6 = 0;
								Color newColor2 = default(Color);
								Dust.NewDust(arg_660_0, arg_660_1, arg_660_2, arg_660_3, arg_660_4, arg_660_5, arg_660_6, newColor2, 1f);
							}
							if (this.type == 51)
							{
								Gore.NewGore(this.position, this.velocity, 83, 1f);
								return;
							}
							Gore.NewGore(this.position, this.velocity, 82, 1f);
							return;
						}
						else
						{
							if (this.type == 46 || this.type == 55 || this.type == 67)
							{
								if (this.life > 0)
								{
									int num9 = 0;
									while ((double)num9 < dmg / (double)this.lifeMax * 20.0)
									{
										Vector2 arg_709_0 = this.position;
										int arg_709_1 = this.width;
										int arg_709_2 = this.height;
										int arg_709_3 = 5;
										float arg_709_4 = (float)hitDirection;
										float arg_709_5 = -1f;
										int arg_709_6 = 0;
										Color newColor2 = default(Color);
										Dust.NewDust(arg_709_0, arg_709_1, arg_709_2, arg_709_3, arg_709_4, arg_709_5, arg_709_6, newColor2, 1f);
										num9++;
									}
									return;
								}
								for (int num10 = 0; num10 < 10; num10++)
								{
									Vector2 arg_75F_0 = this.position;
									int arg_75F_1 = this.width;
									int arg_75F_2 = this.height;
									int arg_75F_3 = 5;
									float arg_75F_4 = (float)(2 * hitDirection);
									float arg_75F_5 = -2f;
									int arg_75F_6 = 0;
									Color newColor2 = default(Color);
									Dust.NewDust(arg_75F_0, arg_75F_1, arg_75F_2, arg_75F_3, arg_75F_4, arg_75F_5, arg_75F_6, newColor2, 1f);
								}
								if (this.type == 46)
								{
									Gore.NewGore(this.position, this.velocity, 76, 1f);
									Gore.NewGore(new Vector2(this.position.X, this.position.Y), this.velocity, 77, 1f);
									return;
								}
								if (this.type == 67)
								{
									Gore.NewGore(this.position, this.velocity, 95, 1f);
									Gore.NewGore(this.position, this.velocity, 95, 1f);
									Gore.NewGore(this.position, this.velocity, 96, 1f);
									return;
								}
							}
							else
							{
								if (this.type == 47 || this.type == 57 || this.type == 58)
								{
									if (this.life > 0)
									{
										int num11 = 0;
										while ((double)num11 < dmg / (double)this.lifeMax * 20.0)
										{
											Vector2 arg_875_0 = this.position;
											int arg_875_1 = this.width;
											int arg_875_2 = this.height;
											int arg_875_3 = 5;
											float arg_875_4 = (float)hitDirection;
											float arg_875_5 = -1f;
											int arg_875_6 = 0;
											Color newColor2 = default(Color);
											Dust.NewDust(arg_875_0, arg_875_1, arg_875_2, arg_875_3, arg_875_4, arg_875_5, arg_875_6, newColor2, 1f);
											num11++;
										}
										return;
									}
									for (int num12 = 0; num12 < 10; num12++)
									{
										Vector2 arg_8CB_0 = this.position;
										int arg_8CB_1 = this.width;
										int arg_8CB_2 = this.height;
										int arg_8CB_3 = 5;
										float arg_8CB_4 = (float)(2 * hitDirection);
										float arg_8CB_5 = -2f;
										int arg_8CB_6 = 0;
										Color newColor2 = default(Color);
										Dust.NewDust(arg_8CB_0, arg_8CB_1, arg_8CB_2, arg_8CB_3, arg_8CB_4, arg_8CB_5, arg_8CB_6, newColor2, 1f);
									}
									if (this.type == 57)
									{
										Gore.NewGore(new Vector2(this.position.X, this.position.Y), this.velocity, 84, 1f);
										return;
									}
									if (this.type == 58)
									{
										Gore.NewGore(new Vector2(this.position.X, this.position.Y), this.velocity, 85, 1f);
										return;
									}
									Gore.NewGore(this.position, this.velocity, 78, 1f);
									Gore.NewGore(new Vector2(this.position.X, this.position.Y), this.velocity, 79, 1f);
									return;
								}
								else
								{
									if (this.type == 2)
									{
										if (this.life > 0)
										{
											int num13 = 0;
											while ((double)num13 < dmg / (double)this.lifeMax * 100.0)
											{
												Vector2 arg_9DB_0 = this.position;
												int arg_9DB_1 = this.width;
												int arg_9DB_2 = this.height;
												int arg_9DB_3 = 5;
												float arg_9DB_4 = (float)hitDirection;
												float arg_9DB_5 = -1f;
												int arg_9DB_6 = 0;
												Color newColor2 = default(Color);
												Dust.NewDust(arg_9DB_0, arg_9DB_1, arg_9DB_2, arg_9DB_3, arg_9DB_4, arg_9DB_5, arg_9DB_6, newColor2, 1f);
												num13++;
											}
											return;
										}
										for (int num14 = 0; num14 < 50; num14++)
										{
											Vector2 arg_A31_0 = this.position;
											int arg_A31_1 = this.width;
											int arg_A31_2 = this.height;
											int arg_A31_3 = 5;
											float arg_A31_4 = (float)(2 * hitDirection);
											float arg_A31_5 = -2f;
											int arg_A31_6 = 0;
											Color newColor2 = default(Color);
											Dust.NewDust(arg_A31_0, arg_A31_1, arg_A31_2, arg_A31_3, arg_A31_4, arg_A31_5, arg_A31_6, newColor2, 1f);
										}
										Gore.NewGore(this.position, this.velocity, 1, 1f);
										Gore.NewGore(new Vector2(this.position.X + 14f, this.position.Y), this.velocity, 2, 1f);
										return;
									}
									else
									{
										if (this.type == 69)
										{
											if (this.life > 0)
											{
												int num15 = 0;
												while ((double)num15 < dmg / (double)this.lifeMax * 100.0)
												{
													Vector2 arg_AD4_0 = this.position;
													int arg_AD4_1 = this.width;
													int arg_AD4_2 = this.height;
													int arg_AD4_3 = 5;
													float arg_AD4_4 = (float)hitDirection;
													float arg_AD4_5 = -1f;
													int arg_AD4_6 = 0;
													Color newColor2 = default(Color);
													Dust.NewDust(arg_AD4_0, arg_AD4_1, arg_AD4_2, arg_AD4_3, arg_AD4_4, arg_AD4_5, arg_AD4_6, newColor2, 1f);
													num15++;
												}
												return;
											}
											for (int num16 = 0; num16 < 50; num16++)
											{
												Vector2 arg_B2A_0 = this.position;
												int arg_B2A_1 = this.width;
												int arg_B2A_2 = this.height;
												int arg_B2A_3 = 5;
												float arg_B2A_4 = (float)(2 * hitDirection);
												float arg_B2A_5 = -2f;
												int arg_B2A_6 = 0;
												Color newColor2 = default(Color);
												Dust.NewDust(arg_B2A_0, arg_B2A_1, arg_B2A_2, arg_B2A_3, arg_B2A_4, arg_B2A_5, arg_B2A_6, newColor2, 1f);
											}
											Gore.NewGore(this.position, this.velocity, 97, 1f);
											Gore.NewGore(this.position, this.velocity, 98, 1f);
											return;
										}
										else
										{
											if (this.type == 61)
											{
												if (this.life > 0)
												{
													int num17 = 0;
													while ((double)num17 < dmg / (double)this.lifeMax * 100.0)
													{
														Vector2 arg_BB4_0 = this.position;
														int arg_BB4_1 = this.width;
														int arg_BB4_2 = this.height;
														int arg_BB4_3 = 5;
														float arg_BB4_4 = (float)hitDirection;
														float arg_BB4_5 = -1f;
														int arg_BB4_6 = 0;
														Color newColor2 = default(Color);
														Dust.NewDust(arg_BB4_0, arg_BB4_1, arg_BB4_2, arg_BB4_3, arg_BB4_4, arg_BB4_5, arg_BB4_6, newColor2, 1f);
														num17++;
													}
													return;
												}
												for (int num18 = 0; num18 < 50; num18++)
												{
													Vector2 arg_C0A_0 = this.position;
													int arg_C0A_1 = this.width;
													int arg_C0A_2 = this.height;
													int arg_C0A_3 = 5;
													float arg_C0A_4 = (float)(2 * hitDirection);
													float arg_C0A_5 = -2f;
													int arg_C0A_6 = 0;
													Color newColor2 = default(Color);
													Dust.NewDust(arg_C0A_0, arg_C0A_1, arg_C0A_2, arg_C0A_3, arg_C0A_4, arg_C0A_5, arg_C0A_6, newColor2, 1f);
												}
												Gore.NewGore(this.position, this.velocity, 86, 1f);
												Gore.NewGore(new Vector2(this.position.X + 14f, this.position.Y), this.velocity, 87, 1f);
												Gore.NewGore(new Vector2(this.position.X + 14f, this.position.Y), this.velocity, 88, 1f);
												return;
											}
											else
											{
												if (this.type == 65)
												{
													if (this.life > 0)
													{
														int num19 = 0;
														while ((double)num19 < dmg / (double)this.lifeMax * 150.0)
														{
															Vector2 arg_CE3_0 = this.position;
															int arg_CE3_1 = this.width;
															int arg_CE3_2 = this.height;
															int arg_CE3_3 = 5;
															float arg_CE3_4 = (float)hitDirection;
															float arg_CE3_5 = -1f;
															int arg_CE3_6 = 0;
															Color newColor2 = default(Color);
															Dust.NewDust(arg_CE3_0, arg_CE3_1, arg_CE3_2, arg_CE3_3, arg_CE3_4, arg_CE3_5, arg_CE3_6, newColor2, 1f);
															num19++;
														}
														return;
													}
													for (int num20 = 0; num20 < 75; num20++)
													{
														Vector2 arg_D39_0 = this.position;
														int arg_D39_1 = this.width;
														int arg_D39_2 = this.height;
														int arg_D39_3 = 5;
														float arg_D39_4 = (float)(2 * hitDirection);
														float arg_D39_5 = -2f;
														int arg_D39_6 = 0;
														Color newColor2 = default(Color);
														Dust.NewDust(arg_D39_0, arg_D39_1, arg_D39_2, arg_D39_3, arg_D39_4, arg_D39_5, arg_D39_6, newColor2, 1f);
													}
													Gore.NewGore(this.position, this.velocity * 0.8f, 89, 1f);
													Gore.NewGore(new Vector2(this.position.X + 14f, this.position.Y), this.velocity * 0.8f, 90, 1f);
													Gore.NewGore(new Vector2(this.position.X + 14f, this.position.Y), this.velocity * 0.8f, 91, 1f);
													Gore.NewGore(new Vector2(this.position.X + 14f, this.position.Y), this.velocity * 0.8f, 92, 1f);
													return;
												}
												else
												{
													if (this.type == 3 || this.type == 52 || this.type == 53)
													{
														if (this.life > 0)
														{
															int num21 = 0;
															while ((double)num21 < dmg / (double)this.lifeMax * 100.0)
															{
																Vector2 arg_E81_0 = this.position;
																int arg_E81_1 = this.width;
																int arg_E81_2 = this.height;
																int arg_E81_3 = 5;
																float arg_E81_4 = (float)hitDirection;
																float arg_E81_5 = -1f;
																int arg_E81_6 = 0;
																Color newColor2 = default(Color);
																Dust.NewDust(arg_E81_0, arg_E81_1, arg_E81_2, arg_E81_3, arg_E81_4, arg_E81_5, arg_E81_6, newColor2, 1f);
																num21++;
															}
															return;
														}
														for (int num22 = 0; num22 < 50; num22++)
														{
															Vector2 arg_EDB_0 = this.position;
															int arg_EDB_1 = this.width;
															int arg_EDB_2 = this.height;
															int arg_EDB_3 = 5;
															float arg_EDB_4 = 2.5f * (float)hitDirection;
															float arg_EDB_5 = -2.5f;
															int arg_EDB_6 = 0;
															Color newColor2 = default(Color);
															Dust.NewDust(arg_EDB_0, arg_EDB_1, arg_EDB_2, arg_EDB_3, arg_EDB_4, arg_EDB_5, arg_EDB_6, newColor2, 1f);
														}
														Gore.NewGore(this.position, this.velocity, 3, 1f);
														Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 4, 1f);
														Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 4, 1f);
														Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 5, 1f);
														Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 5, 1f);
														return;
													}
													else
													{
														if (this.type == 4)
														{
															if (this.life > 0)
															{
																int num23 = 0;
																while ((double)num23 < dmg / (double)this.lifeMax * 100.0)
																{
																	Vector2 arg_1016_0 = this.position;
																	int arg_1016_1 = this.width;
																	int arg_1016_2 = this.height;
																	int arg_1016_3 = 5;
																	float arg_1016_4 = (float)hitDirection;
																	float arg_1016_5 = -1f;
																	int arg_1016_6 = 0;
																	Color newColor2 = default(Color);
																	Dust.NewDust(arg_1016_0, arg_1016_1, arg_1016_2, arg_1016_3, arg_1016_4, arg_1016_5, arg_1016_6, newColor2, 1f);
																	num23++;
																}
																return;
															}
															for (int num24 = 0; num24 < 150; num24++)
															{
																Vector2 arg_106C_0 = this.position;
																int arg_106C_1 = this.width;
																int arg_106C_2 = this.height;
																int arg_106C_3 = 5;
																float arg_106C_4 = (float)(2 * hitDirection);
																float arg_106C_5 = -2f;
																int arg_106C_6 = 0;
																Color newColor2 = default(Color);
																Dust.NewDust(arg_106C_0, arg_106C_1, arg_106C_2, arg_106C_3, arg_106C_4, arg_106C_5, arg_106C_6, newColor2, 1f);
															}
															for (int num25 = 0; num25 < 2; num25++)
															{
																Gore.NewGore(this.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 2, 1f);
																Gore.NewGore(this.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 7, 1f);
																Gore.NewGore(this.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 9, 1f);
																Gore.NewGore(this.position, new Vector2((float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f), 10, 1f);
															}
															Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
															return;
														}
														else
														{
															if (this.type == 5)
															{
																if (this.life > 0)
																{
																	int num26 = 0;
																	while ((double)num26 < dmg / (double)this.lifeMax * 50.0)
																	{
																		Vector2 arg_1202_0 = this.position;
																		int arg_1202_1 = this.width;
																		int arg_1202_2 = this.height;
																		int arg_1202_3 = 5;
																		float arg_1202_4 = (float)hitDirection;
																		float arg_1202_5 = -1f;
																		int arg_1202_6 = 0;
																		Color newColor2 = default(Color);
																		Dust.NewDust(arg_1202_0, arg_1202_1, arg_1202_2, arg_1202_3, arg_1202_4, arg_1202_5, arg_1202_6, newColor2, 1f);
																		num26++;
																	}
																	return;
																}
																for (int num27 = 0; num27 < 20; num27++)
																{
																	Vector2 arg_1258_0 = this.position;
																	int arg_1258_1 = this.width;
																	int arg_1258_2 = this.height;
																	int arg_1258_3 = 5;
																	float arg_1258_4 = (float)(2 * hitDirection);
																	float arg_1258_5 = -2f;
																	int arg_1258_6 = 0;
																	Color newColor2 = default(Color);
																	Dust.NewDust(arg_1258_0, arg_1258_1, arg_1258_2, arg_1258_3, arg_1258_4, arg_1258_5, arg_1258_6, newColor2, 1f);
																}
																Gore.NewGore(this.position, this.velocity, 6, 1f);
																Gore.NewGore(this.position, this.velocity, 7, 1f);
																return;
															}
															else
															{
																if (this.type == 6)
																{
																	if (this.life > 0)
																	{
																		int num28 = 0;
																		while ((double)num28 < dmg / (double)this.lifeMax * 100.0)
																		{
																			Dust.NewDust(this.position, this.width, this.height, 18, (float)hitDirection, -1f, this.alpha, this.color, this.scale);
																			num28++;
																		}
																		return;
																	}
																	for (int num29 = 0; num29 < 50; num29++)
																	{
																		Dust.NewDust(this.position, this.width, this.height, 18, (float)hitDirection, -2f, this.alpha, this.color, this.scale);
																	}
																	int num30 = Gore.NewGore(this.position, this.velocity, 14, this.scale);
																	Main.gore[num30].alpha = this.alpha;
																	num30 = Gore.NewGore(this.position, this.velocity, 15, this.scale);
																	Main.gore[num30].alpha = this.alpha;
																	return;
																}
																else
																{
																	if (this.type == 7 || this.type == 8 || this.type == 9)
																	{
																		if (this.life > 0)
																		{
																			int num31 = 0;
																			while ((double)num31 < dmg / (double)this.lifeMax * 100.0)
																			{
																				Dust.NewDust(this.position, this.width, this.height, 18, (float)hitDirection, -1f, this.alpha, this.color, this.scale);
																				num31++;
																			}
																			return;
																		}
																		for (int num32 = 0; num32 < 50; num32++)
																		{
																			Dust.NewDust(this.position, this.width, this.height, 18, (float)hitDirection, -2f, this.alpha, this.color, this.scale);
																		}
																		int num33 = Gore.NewGore(this.position, this.velocity, this.type - 7 + 18, 1f);
																		Main.gore[num33].alpha = this.alpha;
																		return;
																	}
																	else
																	{
																		if (this.type == 10 || this.type == 11 || this.type == 12)
																		{
																			if (this.life > 0)
																			{
																				int num34 = 0;
																				while ((double)num34 < dmg / (double)this.lifeMax * 50.0)
																				{
																					Vector2 arg_14FB_0 = this.position;
																					int arg_14FB_1 = this.width;
																					int arg_14FB_2 = this.height;
																					int arg_14FB_3 = 5;
																					float arg_14FB_4 = (float)hitDirection;
																					float arg_14FB_5 = -1f;
																					int arg_14FB_6 = 0;
																					Color newColor2 = default(Color);
																					Dust.NewDust(arg_14FB_0, arg_14FB_1, arg_14FB_2, arg_14FB_3, arg_14FB_4, arg_14FB_5, arg_14FB_6, newColor2, 1f);
																					num34++;
																				}
																				return;
																			}
																			for (int num35 = 0; num35 < 10; num35++)
																			{
																				Vector2 arg_1555_0 = this.position;
																				int arg_1555_1 = this.width;
																				int arg_1555_2 = this.height;
																				int arg_1555_3 = 5;
																				float arg_1555_4 = 2.5f * (float)hitDirection;
																				float arg_1555_5 = -2.5f;
																				int arg_1555_6 = 0;
																				Color newColor2 = default(Color);
																				Dust.NewDust(arg_1555_0, arg_1555_1, arg_1555_2, arg_1555_3, arg_1555_4, arg_1555_5, arg_1555_6, newColor2, 1f);
																			}
																			Gore.NewGore(this.position, this.velocity, this.type - 7 + 18, 1f);
																			return;
																		}
																		else
																		{
																			if (this.type == 13 || this.type == 14 || this.type == 15)
																			{
																				if (this.life > 0)
																				{
																					int num36 = 0;
																					while ((double)num36 < dmg / (double)this.lifeMax * 100.0)
																					{
																						Dust.NewDust(this.position, this.width, this.height, 18, (float)hitDirection, -1f, this.alpha, this.color, this.scale);
																						num36++;
																					}
																					return;
																				}
																				for (int num37 = 0; num37 < 50; num37++)
																				{
																					Dust.NewDust(this.position, this.width, this.height, 18, (float)hitDirection, -2f, this.alpha, this.color, this.scale);
																				}
																				if (this.type == 13)
																				{
																					Gore.NewGore(this.position, this.velocity, 24, 1f);
																					Gore.NewGore(this.position, this.velocity, 25, 1f);
																					return;
																				}
																				if (this.type == 14)
																				{
																					Gore.NewGore(this.position, this.velocity, 26, 1f);
																					Gore.NewGore(this.position, this.velocity, 27, 1f);
																					return;
																				}
																				Gore.NewGore(this.position, this.velocity, 28, 1f);
																				Gore.NewGore(this.position, this.velocity, 29, 1f);
																				return;
																			}
																			else
																			{
																				if (this.type == 17)
																				{
																					if (this.life > 0)
																					{
																						int num38 = 0;
																						while ((double)num38 < dmg / (double)this.lifeMax * 100.0)
																						{
																							Vector2 arg_1741_0 = this.position;
																							int arg_1741_1 = this.width;
																							int arg_1741_2 = this.height;
																							int arg_1741_3 = 5;
																							float arg_1741_4 = (float)hitDirection;
																							float arg_1741_5 = -1f;
																							int arg_1741_6 = 0;
																							Color newColor2 = default(Color);
																							Dust.NewDust(arg_1741_0, arg_1741_1, arg_1741_2, arg_1741_3, arg_1741_4, arg_1741_5, arg_1741_6, newColor2, 1f);
																							num38++;
																						}
																						return;
																					}
																					for (int num39 = 0; num39 < 50; num39++)
																					{
																						Vector2 arg_179B_0 = this.position;
																						int arg_179B_1 = this.width;
																						int arg_179B_2 = this.height;
																						int arg_179B_3 = 5;
																						float arg_179B_4 = 2.5f * (float)hitDirection;
																						float arg_179B_5 = -2.5f;
																						int arg_179B_6 = 0;
																						Color newColor2 = default(Color);
																						Dust.NewDust(arg_179B_0, arg_179B_1, arg_179B_2, arg_179B_3, arg_179B_4, arg_179B_5, arg_179B_6, newColor2, 1f);
																					}
																					Gore.NewGore(this.position, this.velocity, 30, 1f);
																					Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 31, 1f);
																					Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 31, 1f);
																					Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 32, 1f);
																					Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 32, 1f);
																					return;
																				}
																				else
																				{
																					if (this.type == 22)
																					{
																						if (this.life > 0)
																						{
																							int num40 = 0;
																							while ((double)num40 < dmg / (double)this.lifeMax * 100.0)
																							{
																								Vector2 arg_18DC_0 = this.position;
																								int arg_18DC_1 = this.width;
																								int arg_18DC_2 = this.height;
																								int arg_18DC_3 = 5;
																								float arg_18DC_4 = (float)hitDirection;
																								float arg_18DC_5 = -1f;
																								int arg_18DC_6 = 0;
																								Color newColor2 = default(Color);
																								Dust.NewDust(arg_18DC_0, arg_18DC_1, arg_18DC_2, arg_18DC_3, arg_18DC_4, arg_18DC_5, arg_18DC_6, newColor2, 1f);
																								num40++;
																							}
																							return;
																						}
																						for (int num41 = 0; num41 < 50; num41++)
																						{
																							Vector2 arg_1936_0 = this.position;
																							int arg_1936_1 = this.width;
																							int arg_1936_2 = this.height;
																							int arg_1936_3 = 5;
																							float arg_1936_4 = 2.5f * (float)hitDirection;
																							float arg_1936_5 = -2.5f;
																							int arg_1936_6 = 0;
																							Color newColor2 = default(Color);
																							Dust.NewDust(arg_1936_0, arg_1936_1, arg_1936_2, arg_1936_3, arg_1936_4, arg_1936_5, arg_1936_6, newColor2, 1f);
																						}
																						Gore.NewGore(this.position, this.velocity, 73, 1f);
																						Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 74, 1f);
																						Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 74, 1f);
																						Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 75, 1f);
																						Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 75, 1f);
																						return;
																					}
																					else
																					{
																						if (this.type == 37 || this.type == 54)
																						{
																							if (this.life > 0)
																							{
																								int num42 = 0;
																								while ((double)num42 < dmg / (double)this.lifeMax * 100.0)
																								{
																									Vector2 arg_1A81_0 = this.position;
																									int arg_1A81_1 = this.width;
																									int arg_1A81_2 = this.height;
																									int arg_1A81_3 = 5;
																									float arg_1A81_4 = (float)hitDirection;
																									float arg_1A81_5 = -1f;
																									int arg_1A81_6 = 0;
																									Color newColor2 = default(Color);
																									Dust.NewDust(arg_1A81_0, arg_1A81_1, arg_1A81_2, arg_1A81_3, arg_1A81_4, arg_1A81_5, arg_1A81_6, newColor2, 1f);
																									num42++;
																								}
																								return;
																							}
																							for (int num43 = 0; num43 < 50; num43++)
																							{
																								Vector2 arg_1ADB_0 = this.position;
																								int arg_1ADB_1 = this.width;
																								int arg_1ADB_2 = this.height;
																								int arg_1ADB_3 = 5;
																								float arg_1ADB_4 = 2.5f * (float)hitDirection;
																								float arg_1ADB_5 = -2.5f;
																								int arg_1ADB_6 = 0;
																								Color newColor2 = default(Color);
																								Dust.NewDust(arg_1ADB_0, arg_1ADB_1, arg_1ADB_2, arg_1ADB_3, arg_1ADB_4, arg_1ADB_5, arg_1ADB_6, newColor2, 1f);
																							}
																							Gore.NewGore(this.position, this.velocity, 58, 1f);
																							Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 59, 1f);
																							Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 59, 1f);
																							Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 60, 1f);
																							Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 60, 1f);
																							return;
																						}
																						else
																						{
																							if (this.type == 18)
																							{
																								if (this.life > 0)
																								{
																									int num44 = 0;
																									while ((double)num44 < dmg / (double)this.lifeMax * 100.0)
																									{
																										Vector2 arg_1C1C_0 = this.position;
																										int arg_1C1C_1 = this.width;
																										int arg_1C1C_2 = this.height;
																										int arg_1C1C_3 = 5;
																										float arg_1C1C_4 = (float)hitDirection;
																										float arg_1C1C_5 = -1f;
																										int arg_1C1C_6 = 0;
																										Color newColor2 = default(Color);
																										Dust.NewDust(arg_1C1C_0, arg_1C1C_1, arg_1C1C_2, arg_1C1C_3, arg_1C1C_4, arg_1C1C_5, arg_1C1C_6, newColor2, 1f);
																										num44++;
																									}
																									return;
																								}
																								for (int num45 = 0; num45 < 50; num45++)
																								{
																									Vector2 arg_1C76_0 = this.position;
																									int arg_1C76_1 = this.width;
																									int arg_1C76_2 = this.height;
																									int arg_1C76_3 = 5;
																									float arg_1C76_4 = 2.5f * (float)hitDirection;
																									float arg_1C76_5 = -2.5f;
																									int arg_1C76_6 = 0;
																									Color newColor2 = default(Color);
																									Dust.NewDust(arg_1C76_0, arg_1C76_1, arg_1C76_2, arg_1C76_3, arg_1C76_4, arg_1C76_5, arg_1C76_6, newColor2, 1f);
																								}
																								Gore.NewGore(this.position, this.velocity, 33, 1f);
																								Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 34, 1f);
																								Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 34, 1f);
																								Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 35, 1f);
																								Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 35, 1f);
																								return;
																							}
																							else
																							{
																								if (this.type == 19)
																								{
																									if (this.life > 0)
																									{
																										int num46 = 0;
																										while ((double)num46 < dmg / (double)this.lifeMax * 100.0)
																										{
																											Vector2 arg_1DB7_0 = this.position;
																											int arg_1DB7_1 = this.width;
																											int arg_1DB7_2 = this.height;
																											int arg_1DB7_3 = 5;
																											float arg_1DB7_4 = (float)hitDirection;
																											float arg_1DB7_5 = -1f;
																											int arg_1DB7_6 = 0;
																											Color newColor2 = default(Color);
																											Dust.NewDust(arg_1DB7_0, arg_1DB7_1, arg_1DB7_2, arg_1DB7_3, arg_1DB7_4, arg_1DB7_5, arg_1DB7_6, newColor2, 1f);
																											num46++;
																										}
																										return;
																									}
																									for (int num47 = 0; num47 < 50; num47++)
																									{
																										Vector2 arg_1E11_0 = this.position;
																										int arg_1E11_1 = this.width;
																										int arg_1E11_2 = this.height;
																										int arg_1E11_3 = 5;
																										float arg_1E11_4 = 2.5f * (float)hitDirection;
																										float arg_1E11_5 = -2.5f;
																										int arg_1E11_6 = 0;
																										Color newColor2 = default(Color);
																										Dust.NewDust(arg_1E11_0, arg_1E11_1, arg_1E11_2, arg_1E11_3, arg_1E11_4, arg_1E11_5, arg_1E11_6, newColor2, 1f);
																									}
																									Gore.NewGore(this.position, this.velocity, 36, 1f);
																									Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 37, 1f);
																									Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 37, 1f);
																									Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 38, 1f);
																									Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 38, 1f);
																									return;
																								}
																								else
																								{
																									if (this.type == 38)
																									{
																										if (this.life > 0)
																										{
																											int num48 = 0;
																											while ((double)num48 < dmg / (double)this.lifeMax * 100.0)
																											{
																												Vector2 arg_1F52_0 = this.position;
																												int arg_1F52_1 = this.width;
																												int arg_1F52_2 = this.height;
																												int arg_1F52_3 = 5;
																												float arg_1F52_4 = (float)hitDirection;
																												float arg_1F52_5 = -1f;
																												int arg_1F52_6 = 0;
																												Color newColor2 = default(Color);
																												Dust.NewDust(arg_1F52_0, arg_1F52_1, arg_1F52_2, arg_1F52_3, arg_1F52_4, arg_1F52_5, arg_1F52_6, newColor2, 1f);
																												num48++;
																											}
																											return;
																										}
																										for (int num49 = 0; num49 < 50; num49++)
																										{
																											Vector2 arg_1FAC_0 = this.position;
																											int arg_1FAC_1 = this.width;
																											int arg_1FAC_2 = this.height;
																											int arg_1FAC_3 = 5;
																											float arg_1FAC_4 = 2.5f * (float)hitDirection;
																											float arg_1FAC_5 = -2.5f;
																											int arg_1FAC_6 = 0;
																											Color newColor2 = default(Color);
																											Dust.NewDust(arg_1FAC_0, arg_1FAC_1, arg_1FAC_2, arg_1FAC_3, arg_1FAC_4, arg_1FAC_5, arg_1FAC_6, newColor2, 1f);
																										}
																										Gore.NewGore(this.position, this.velocity, 64, 1f);
																										Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 65, 1f);
																										Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 65, 1f);
																										Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 66, 1f);
																										Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 66, 1f);
																										return;
																									}
																									else
																									{
																										if (this.type == 20)
																										{
																											if (this.life > 0)
																											{
																												int num50 = 0;
																												while ((double)num50 < dmg / (double)this.lifeMax * 100.0)
																												{
																													Vector2 arg_20ED_0 = this.position;
																													int arg_20ED_1 = this.width;
																													int arg_20ED_2 = this.height;
																													int arg_20ED_3 = 5;
																													float arg_20ED_4 = (float)hitDirection;
																													float arg_20ED_5 = -1f;
																													int arg_20ED_6 = 0;
																													Color newColor2 = default(Color);
																													Dust.NewDust(arg_20ED_0, arg_20ED_1, arg_20ED_2, arg_20ED_3, arg_20ED_4, arg_20ED_5, arg_20ED_6, newColor2, 1f);
																													num50++;
																												}
																												return;
																											}
																											for (int num51 = 0; num51 < 50; num51++)
																											{
																												Vector2 arg_2147_0 = this.position;
																												int arg_2147_1 = this.width;
																												int arg_2147_2 = this.height;
																												int arg_2147_3 = 5;
																												float arg_2147_4 = 2.5f * (float)hitDirection;
																												float arg_2147_5 = -2.5f;
																												int arg_2147_6 = 0;
																												Color newColor2 = default(Color);
																												Dust.NewDust(arg_2147_0, arg_2147_1, arg_2147_2, arg_2147_3, arg_2147_4, arg_2147_5, arg_2147_6, newColor2, 1f);
																											}
																											Gore.NewGore(this.position, this.velocity, 39, 1f);
																											Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 40, 1f);
																											Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 40, 1f);
																											Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 41, 1f);
																											Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 41, 1f);
																											return;
																										}
																										else
																										{
																											if (this.type == 21 || this.type == 31 || this.type == 32 || this.type == 44 || this.type == 45)
																											{
																												if (this.life > 0)
																												{
																													int num52 = 0;
																													while ((double)num52 < dmg / (double)this.lifeMax * 50.0)
																													{
																														Vector2 arg_22B1_0 = this.position;
																														int arg_22B1_1 = this.width;
																														int arg_22B1_2 = this.height;
																														int arg_22B1_3 = 26;
																														float arg_22B1_4 = (float)hitDirection;
																														float arg_22B1_5 = -1f;
																														int arg_22B1_6 = 0;
																														Color newColor2 = default(Color);
																														Dust.NewDust(arg_22B1_0, arg_22B1_1, arg_22B1_2, arg_22B1_3, arg_22B1_4, arg_22B1_5, arg_22B1_6, newColor2, 1f);
																														num52++;
																													}
																													return;
																												}
																												for (int num53 = 0; num53 < 20; num53++)
																												{
																													Vector2 arg_230C_0 = this.position;
																													int arg_230C_1 = this.width;
																													int arg_230C_2 = this.height;
																													int arg_230C_3 = 26;
																													float arg_230C_4 = 2.5f * (float)hitDirection;
																													float arg_230C_5 = -2.5f;
																													int arg_230C_6 = 0;
																													Color newColor2 = default(Color);
																													Dust.NewDust(arg_230C_0, arg_230C_1, arg_230C_2, arg_230C_3, arg_230C_4, arg_230C_5, arg_230C_6, newColor2, 1f);
																												}
																												Gore.NewGore(this.position, this.velocity, 42, this.scale);
																												Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 43, this.scale);
																												Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 43, this.scale);
																												Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 44, this.scale);
																												Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 44, this.scale);
																												return;
																											}
																											else
																											{
																												if (this.type == 39 || this.type == 40 || this.type == 41)
																												{
																													if (this.life > 0)
																													{
																														int num54 = 0;
																														while ((double)num54 < dmg / (double)this.lifeMax * 50.0)
																														{
																															Vector2 arg_2467_0 = this.position;
																															int arg_2467_1 = this.width;
																															int arg_2467_2 = this.height;
																															int arg_2467_3 = 26;
																															float arg_2467_4 = (float)hitDirection;
																															float arg_2467_5 = -1f;
																															int arg_2467_6 = 0;
																															Color newColor2 = default(Color);
																															Dust.NewDust(arg_2467_0, arg_2467_1, arg_2467_2, arg_2467_3, arg_2467_4, arg_2467_5, arg_2467_6, newColor2, 1f);
																															num54++;
																														}
																														return;
																													}
																													for (int num55 = 0; num55 < 20; num55++)
																													{
																														Vector2 arg_24C2_0 = this.position;
																														int arg_24C2_1 = this.width;
																														int arg_24C2_2 = this.height;
																														int arg_24C2_3 = 26;
																														float arg_24C2_4 = 2.5f * (float)hitDirection;
																														float arg_24C2_5 = -2.5f;
																														int arg_24C2_6 = 0;
																														Color newColor2 = default(Color);
																														Dust.NewDust(arg_24C2_0, arg_24C2_1, arg_24C2_2, arg_24C2_3, arg_24C2_4, arg_24C2_5, arg_24C2_6, newColor2, 1f);
																													}
																													Gore.NewGore(this.position, this.velocity, this.type - 39 + 67, 1f);
																													return;
																												}
																												else
																												{
																													if (this.type == 34)
																													{
																														if (this.life > 0)
																														{
																															int num56 = 0;
																															while ((double)num56 < dmg / (double)this.lifeMax * 30.0)
																															{
																																Vector2 arg_2577_0 = new Vector2(this.position.X, this.position.Y);
																																int arg_2577_1 = this.width;
																																int arg_2577_2 = this.height;
																																int arg_2577_3 = 15;
																																float arg_2577_4 = -this.velocity.X * 0.2f;
																																float arg_2577_5 = -this.velocity.Y * 0.2f;
																																int arg_2577_6 = 100;
																																Color newColor2 = default(Color);
																																int num57 = Dust.NewDust(arg_2577_0, arg_2577_1, arg_2577_2, arg_2577_3, arg_2577_4, arg_2577_5, arg_2577_6, newColor2, 1.8f);
																																Main.dust[num57].noLight = true;
																																Main.dust[num57].noGravity = true;
																																Dust expr_25A2 = Main.dust[num57];
																																expr_25A2.velocity *= 1.3f;
																																Vector2 arg_2614_0 = new Vector2(this.position.X, this.position.Y);
																																int arg_2614_1 = this.width;
																																int arg_2614_2 = this.height;
																																int arg_2614_3 = 26;
																																float arg_2614_4 = -this.velocity.X * 0.2f;
																																float arg_2614_5 = -this.velocity.Y * 0.2f;
																																int arg_2614_6 = 0;
																																newColor2 = default(Color);
																																num57 = Dust.NewDust(arg_2614_0, arg_2614_1, arg_2614_2, arg_2614_3, arg_2614_4, arg_2614_5, arg_2614_6, newColor2, 0.9f);
																																Main.dust[num57].noLight = true;
																																Dust expr_2631 = Main.dust[num57];
																																expr_2631.velocity *= 1.3f;
																																num56++;
																															}
																															return;
																														}
																														for (int num58 = 0; num58 < 15; num58++)
																														{
																															Vector2 arg_26CE_0 = new Vector2(this.position.X, this.position.Y);
																															int arg_26CE_1 = this.width;
																															int arg_26CE_2 = this.height;
																															int arg_26CE_3 = 15;
																															float arg_26CE_4 = -this.velocity.X * 0.2f;
																															float arg_26CE_5 = -this.velocity.Y * 0.2f;
																															int arg_26CE_6 = 100;
																															Color newColor2 = default(Color);
																															int num59 = Dust.NewDust(arg_26CE_0, arg_26CE_1, arg_26CE_2, arg_26CE_3, arg_26CE_4, arg_26CE_5, arg_26CE_6, newColor2, 1.8f);
																															Main.dust[num59].noLight = true;
																															Main.dust[num59].noGravity = true;
																															Dust expr_26F9 = Main.dust[num59];
																															expr_26F9.velocity *= 1.3f;
																															Vector2 arg_276B_0 = new Vector2(this.position.X, this.position.Y);
																															int arg_276B_1 = this.width;
																															int arg_276B_2 = this.height;
																															int arg_276B_3 = 26;
																															float arg_276B_4 = -this.velocity.X * 0.2f;
																															float arg_276B_5 = -this.velocity.Y * 0.2f;
																															int arg_276B_6 = 0;
																															newColor2 = default(Color);
																															num59 = Dust.NewDust(arg_276B_0, arg_276B_1, arg_276B_2, arg_276B_3, arg_276B_4, arg_276B_5, arg_276B_6, newColor2, 0.9f);
																															Main.dust[num59].noLight = true;
																															Dust expr_2788 = Main.dust[num59];
																															expr_2788.velocity *= 1.3f;
																														}
																														return;
																													}
																													else
																													{
																														if (this.type == 35 || this.type == 36)
																														{
																															if (this.life > 0)
																															{
																																int num60 = 0;
																																while ((double)num60 < dmg / (double)this.lifeMax * 100.0)
																																{
																																	Vector2 arg_27FD_0 = this.position;
																																	int arg_27FD_1 = this.width;
																																	int arg_27FD_2 = this.height;
																																	int arg_27FD_3 = 26;
																																	float arg_27FD_4 = (float)hitDirection;
																																	float arg_27FD_5 = -1f;
																																	int arg_27FD_6 = 0;
																																	Color newColor2 = default(Color);
																																	Dust.NewDust(arg_27FD_0, arg_27FD_1, arg_27FD_2, arg_27FD_3, arg_27FD_4, arg_27FD_5, arg_27FD_6, newColor2, 1f);
																																	num60++;
																																}
																																return;
																															}
																															for (int num61 = 0; num61 < 150; num61++)
																															{
																																Vector2 arg_2858_0 = this.position;
																																int arg_2858_1 = this.width;
																																int arg_2858_2 = this.height;
																																int arg_2858_3 = 26;
																																float arg_2858_4 = 2.5f * (float)hitDirection;
																																float arg_2858_5 = -2.5f;
																																int arg_2858_6 = 0;
																																Color newColor2 = default(Color);
																																Dust.NewDust(arg_2858_0, arg_2858_1, arg_2858_2, arg_2858_3, arg_2858_4, arg_2858_5, arg_2858_6, newColor2, 1f);
																															}
																															if (this.type == 35)
																															{
																																Gore.NewGore(this.position, this.velocity, 54, 1f);
																																Gore.NewGore(this.position, this.velocity, 55, 1f);
																																return;
																															}
																															Gore.NewGore(this.position, this.velocity, 56, 1f);
																															Gore.NewGore(this.position, this.velocity, 57, 1f);
																															Gore.NewGore(this.position, this.velocity, 57, 1f);
																															Gore.NewGore(this.position, this.velocity, 57, 1f);
																															return;
																														}
																														else
																														{
																															if (this.type == 23)
																															{
																																if (this.life > 0)
																																{
																																	int num62 = 0;
																																	while ((double)num62 < dmg / (double)this.lifeMax * 100.0)
																																	{
																																		int num63 = 25;
																																		if (Main.rand.Next(2) == 0)
																																		{
																																			num63 = 6;
																																		}
																																		Vector2 arg_296F_0 = this.position;
																																		int arg_296F_1 = this.width;
																																		int arg_296F_2 = this.height;
																																		int arg_296F_3 = num63;
																																		float arg_296F_4 = (float)hitDirection;
																																		float arg_296F_5 = -1f;
																																		int arg_296F_6 = 0;
																																		Color newColor2 = default(Color);
																																		Dust.NewDust(arg_296F_0, arg_296F_1, arg_296F_2, arg_296F_3, arg_296F_4, arg_296F_5, arg_296F_6, newColor2, 1f);
																																		Vector2 arg_29D0_0 = new Vector2(this.position.X, this.position.Y);
																																		int arg_29D0_1 = this.width;
																																		int arg_29D0_2 = this.height;
																																		int arg_29D0_3 = 6;
																																		float arg_29D0_4 = this.velocity.X * 0.2f;
																																		float arg_29D0_5 = this.velocity.Y * 0.2f;
																																		int arg_29D0_6 = 100;
																																		newColor2 = default(Color);
																																		int num64 = Dust.NewDust(arg_29D0_0, arg_29D0_1, arg_29D0_2, arg_29D0_3, arg_29D0_4, arg_29D0_5, arg_29D0_6, newColor2, 2f);
																																		Main.dust[num64].noGravity = true;
																																		num62++;
																																	}
																																	return;
																																}
																																for (int num65 = 0; num65 < 50; num65++)
																																{
																																	int num66 = 25;
																																	if (Main.rand.Next(2) == 0)
																																	{
																																		num66 = 6;
																																	}
																																	Vector2 arg_2A4D_0 = this.position;
																																	int arg_2A4D_1 = this.width;
																																	int arg_2A4D_2 = this.height;
																																	int arg_2A4D_3 = num66;
																																	float arg_2A4D_4 = (float)(2 * hitDirection);
																																	float arg_2A4D_5 = -2f;
																																	int arg_2A4D_6 = 0;
																																	Color newColor2 = default(Color);
																																	Dust.NewDust(arg_2A4D_0, arg_2A4D_1, arg_2A4D_2, arg_2A4D_3, arg_2A4D_4, arg_2A4D_5, arg_2A4D_6, newColor2, 1f);
																																}
																																for (int num67 = 0; num67 < 50; num67++)
																																{
																																	Vector2 arg_2AC2_0 = new Vector2(this.position.X, this.position.Y);
																																	int arg_2AC2_1 = this.width;
																																	int arg_2AC2_2 = this.height;
																																	int arg_2AC2_3 = 6;
																																	float arg_2AC2_4 = this.velocity.X * 0.2f;
																																	float arg_2AC2_5 = this.velocity.Y * 0.2f;
																																	int arg_2AC2_6 = 100;
																																	Color newColor2 = default(Color);
																																	int num68 = Dust.NewDust(arg_2AC2_0, arg_2AC2_1, arg_2AC2_2, arg_2AC2_3, arg_2AC2_4, arg_2AC2_5, arg_2AC2_6, newColor2, 2.5f);
																																	Dust expr_2AD1 = Main.dust[num68];
																																	expr_2AD1.velocity *= 6f;
																																	Main.dust[num68].noGravity = true;
																																}
																																return;
																															}
																															else
																															{
																																if (this.type == 24)
																																{
																																	if (this.life > 0)
																																	{
																																		int num69 = 0;
																																		while ((double)num69 < dmg / (double)this.lifeMax * 100.0)
																																		{
																																			Vector2 arg_2B71_0 = new Vector2(this.position.X, this.position.Y);
																																			int arg_2B71_1 = this.width;
																																			int arg_2B71_2 = this.height;
																																			int arg_2B71_3 = 6;
																																			float arg_2B71_4 = this.velocity.X;
																																			float arg_2B71_5 = this.velocity.Y;
																																			int arg_2B71_6 = 100;
																																			Color newColor2 = default(Color);
																																			int num70 = Dust.NewDust(arg_2B71_0, arg_2B71_1, arg_2B71_2, arg_2B71_3, arg_2B71_4, arg_2B71_5, arg_2B71_6, newColor2, 2.5f);
																																			Main.dust[num70].noGravity = true;
																																			num69++;
																																		}
																																		return;
																																	}
																																	for (int num71 = 0; num71 < 50; num71++)
																																	{
																																		Vector2 arg_2BFF_0 = new Vector2(this.position.X, this.position.Y);
																																		int arg_2BFF_1 = this.width;
																																		int arg_2BFF_2 = this.height;
																																		int arg_2BFF_3 = 6;
																																		float arg_2BFF_4 = this.velocity.X;
																																		float arg_2BFF_5 = this.velocity.Y;
																																		int arg_2BFF_6 = 100;
																																		Color newColor2 = default(Color);
																																		int num72 = Dust.NewDust(arg_2BFF_0, arg_2BFF_1, arg_2BFF_2, arg_2BFF_3, arg_2BFF_4, arg_2BFF_5, arg_2BFF_6, newColor2, 2.5f);
																																		Main.dust[num72].noGravity = true;
																																		Dust expr_2C1C = Main.dust[num72];
																																		expr_2C1C.velocity *= 2f;
																																	}
																																	Gore.NewGore(this.position, this.velocity, 45, 1f);
																																	Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 46, 1f);
																																	Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 46, 1f);
																																	Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 47, 1f);
																																	Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 47, 1f);
																																	return;
																																}
																																else
																																{
																																	if (this.type == 25)
																																	{
																																		Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 10);
																																		for (int num73 = 0; num73 < 20; num73++)
																																		{
																																			Vector2 arg_2DBC_0 = new Vector2(this.position.X, this.position.Y);
																																			int arg_2DBC_1 = this.width;
																																			int arg_2DBC_2 = this.height;
																																			int arg_2DBC_3 = 6;
																																			float arg_2DBC_4 = -this.velocity.X * 0.2f;
																																			float arg_2DBC_5 = -this.velocity.Y * 0.2f;
																																			int arg_2DBC_6 = 100;
																																			Color newColor2 = default(Color);
																																			int num74 = Dust.NewDust(arg_2DBC_0, arg_2DBC_1, arg_2DBC_2, arg_2DBC_3, arg_2DBC_4, arg_2DBC_5, arg_2DBC_6, newColor2, 2f);
																																			Main.dust[num74].noGravity = true;
																																			Dust expr_2DD9 = Main.dust[num74];
																																			expr_2DD9.velocity *= 2f;
																																			Vector2 arg_2E4B_0 = new Vector2(this.position.X, this.position.Y);
																																			int arg_2E4B_1 = this.width;
																																			int arg_2E4B_2 = this.height;
																																			int arg_2E4B_3 = 6;
																																			float arg_2E4B_4 = -this.velocity.X * 0.2f;
																																			float arg_2E4B_5 = -this.velocity.Y * 0.2f;
																																			int arg_2E4B_6 = 100;
																																			newColor2 = default(Color);
																																			num74 = Dust.NewDust(arg_2E4B_0, arg_2E4B_1, arg_2E4B_2, arg_2E4B_3, arg_2E4B_4, arg_2E4B_5, arg_2E4B_6, newColor2, 1f);
																																			Dust expr_2E5A = Main.dust[num74];
																																			expr_2E5A.velocity *= 2f;
																																		}
																																		return;
																																	}
																																	if (this.type == 33)
																																	{
																																		Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 10);
																																		for (int num75 = 0; num75 < 20; num75++)
																																		{
																																			Vector2 arg_2F12_0 = new Vector2(this.position.X, this.position.Y);
																																			int arg_2F12_1 = this.width;
																																			int arg_2F12_2 = this.height;
																																			int arg_2F12_3 = 29;
																																			float arg_2F12_4 = -this.velocity.X * 0.2f;
																																			float arg_2F12_5 = -this.velocity.Y * 0.2f;
																																			int arg_2F12_6 = 100;
																																			Color newColor2 = default(Color);
																																			int num76 = Dust.NewDust(arg_2F12_0, arg_2F12_1, arg_2F12_2, arg_2F12_3, arg_2F12_4, arg_2F12_5, arg_2F12_6, newColor2, 2f);
																																			Main.dust[num76].noGravity = true;
																																			Dust expr_2F2F = Main.dust[num76];
																																			expr_2F2F.velocity *= 2f;
																																			Vector2 arg_2FA2_0 = new Vector2(this.position.X, this.position.Y);
																																			int arg_2FA2_1 = this.width;
																																			int arg_2FA2_2 = this.height;
																																			int arg_2FA2_3 = 29;
																																			float arg_2FA2_4 = -this.velocity.X * 0.2f;
																																			float arg_2FA2_5 = -this.velocity.Y * 0.2f;
																																			int arg_2FA2_6 = 100;
																																			newColor2 = default(Color);
																																			num76 = Dust.NewDust(arg_2FA2_0, arg_2FA2_1, arg_2FA2_2, arg_2FA2_3, arg_2FA2_4, arg_2FA2_5, arg_2FA2_6, newColor2, 1f);
																																			Dust expr_2FB1 = Main.dust[num76];
																																			expr_2FB1.velocity *= 2f;
																																		}
																																		return;
																																	}
																																	if (this.type == 26 || this.type == 27 || this.type == 28 || this.type == 29 || this.type == 73)
																																	{
																																		if (this.life > 0)
																																		{
																																			int num77 = 0;
																																			while ((double)num77 < dmg / (double)this.lifeMax * 100.0)
																																			{
																																				Vector2 arg_3043_0 = this.position;
																																				int arg_3043_1 = this.width;
																																				int arg_3043_2 = this.height;
																																				int arg_3043_3 = 5;
																																				float arg_3043_4 = (float)hitDirection;
																																				float arg_3043_5 = -1f;
																																				int arg_3043_6 = 0;
																																				Color newColor2 = default(Color);
																																				Dust.NewDust(arg_3043_0, arg_3043_1, arg_3043_2, arg_3043_3, arg_3043_4, arg_3043_5, arg_3043_6, newColor2, 1f);
																																				num77++;
																																			}
																																			return;
																																		}
																																		for (int num78 = 0; num78 < 50; num78++)
																																		{
																																			Vector2 arg_309D_0 = this.position;
																																			int arg_309D_1 = this.width;
																																			int arg_309D_2 = this.height;
																																			int arg_309D_3 = 5;
																																			float arg_309D_4 = 2.5f * (float)hitDirection;
																																			float arg_309D_5 = -2.5f;
																																			int arg_309D_6 = 0;
																																			Color newColor2 = default(Color);
																																			Dust.NewDust(arg_309D_0, arg_309D_1, arg_309D_2, arg_309D_3, arg_309D_4, arg_309D_5, arg_309D_6, newColor2, 1f);
																																		}
																																		Gore.NewGore(this.position, this.velocity, 48, this.scale);
																																		Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 49, this.scale);
																																		Gore.NewGore(new Vector2(this.position.X, this.position.Y + 20f), this.velocity, 49, this.scale);
																																		Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 50, this.scale);
																																		Gore.NewGore(new Vector2(this.position.X, this.position.Y + 34f), this.velocity, 50, this.scale);
																																		return;
																																	}
																																	else
																																	{
																																		if (this.type == 30)
																																		{
																																			Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 10);
																																			for (int num79 = 0; num79 < 20; num79++)
																																			{
																																				Vector2 arg_3231_0 = new Vector2(this.position.X, this.position.Y);
																																				int arg_3231_1 = this.width;
																																				int arg_3231_2 = this.height;
																																				int arg_3231_3 = 27;
																																				float arg_3231_4 = -this.velocity.X * 0.2f;
																																				float arg_3231_5 = -this.velocity.Y * 0.2f;
																																				int arg_3231_6 = 100;
																																				Color newColor2 = default(Color);
																																				int num80 = Dust.NewDust(arg_3231_0, arg_3231_1, arg_3231_2, arg_3231_3, arg_3231_4, arg_3231_5, arg_3231_6, newColor2, 2f);
																																				Main.dust[num80].noGravity = true;
																																				Dust expr_324E = Main.dust[num80];
																																				expr_324E.velocity *= 2f;
																																				Vector2 arg_32C1_0 = new Vector2(this.position.X, this.position.Y);
																																				int arg_32C1_1 = this.width;
																																				int arg_32C1_2 = this.height;
																																				int arg_32C1_3 = 27;
																																				float arg_32C1_4 = -this.velocity.X * 0.2f;
																																				float arg_32C1_5 = -this.velocity.Y * 0.2f;
																																				int arg_32C1_6 = 100;
																																				newColor2 = default(Color);
																																				num80 = Dust.NewDust(arg_32C1_0, arg_32C1_1, arg_32C1_2, arg_32C1_3, arg_32C1_4, arg_32C1_5, arg_32C1_6, newColor2, 1f);
																																				Dust expr_32D0 = Main.dust[num80];
																																				expr_32D0.velocity *= 2f;
																																			}
																																			return;
																																		}
																																		if (this.type == 42)
																																		{
																																			if (this.life > 0)
																																			{
																																				int num81 = 0;
																																				while ((double)num81 < dmg / (double)this.lifeMax * 100.0)
																																				{
																																					Dust.NewDust(this.position, this.width, this.height, 18, (float)hitDirection, -1f, this.alpha, this.color, this.scale);
																																					num81++;
																																				}
																																				return;
																																			}
																																			for (int num82 = 0; num82 < 50; num82++)
																																			{
																																				Dust.NewDust(this.position, this.width, this.height, 18, (float)hitDirection, -2f, this.alpha, this.color, this.scale);
																																			}
																																			Gore.NewGore(this.position, this.velocity, 70, this.scale);
																																			Gore.NewGore(this.position, this.velocity, 71, this.scale);
																																			return;
																																		}
																																		else
																																		{
																																			if (this.type == 43 || this.type == 56)
																																			{
																																				if (this.life > 0)
																																				{
																																					int num83 = 0;
																																					while ((double)num83 < dmg / (double)this.lifeMax * 100.0)
																																					{
																																						Dust.NewDust(this.position, this.width, this.height, 40, (float)hitDirection, -1f, this.alpha, this.color, 1.2f);
																																						num83++;
																																					}
																																					return;
																																				}
																																				for (int num84 = 0; num84 < 50; num84++)
																																				{
																																					Dust.NewDust(this.position, this.width, this.height, 40, (float)hitDirection, -2f, this.alpha, this.color, 1.2f);
																																				}
																																				Gore.NewGore(this.position, this.velocity, 72, 1f);
																																				Gore.NewGore(this.position, this.velocity, 72, 1f);
																																				return;
																																			}
																																			else
																																			{
																																				if (this.type == 48)
																																				{
																																					if (this.life > 0)
																																					{
																																						int num85 = 0;
																																						while ((double)num85 < dmg / (double)this.lifeMax * 100.0)
																																						{
																																							Vector2 arg_350C_0 = this.position;
																																							int arg_350C_1 = this.width;
																																							int arg_350C_2 = this.height;
																																							int arg_350C_3 = 5;
																																							float arg_350C_4 = (float)hitDirection;
																																							float arg_350C_5 = -1f;
																																							int arg_350C_6 = 0;
																																							Color newColor2 = default(Color);
																																							Dust.NewDust(arg_350C_0, arg_350C_1, arg_350C_2, arg_350C_3, arg_350C_4, arg_350C_5, arg_350C_6, newColor2, 1f);
																																							num85++;
																																						}
																																						return;
																																					}
																																					for (int num86 = 0; num86 < 50; num86++)
																																					{
																																						Vector2 arg_3562_0 = this.position;
																																						int arg_3562_1 = this.width;
																																						int arg_3562_2 = this.height;
																																						int arg_3562_3 = 5;
																																						float arg_3562_4 = (float)(2 * hitDirection);
																																						float arg_3562_5 = -2f;
																																						int arg_3562_6 = 0;
																																						Color newColor2 = default(Color);
																																						Dust.NewDust(arg_3562_0, arg_3562_1, arg_3562_2, arg_3562_3, arg_3562_4, arg_3562_5, arg_3562_6, newColor2, 1f);
																																					}
																																					Gore.NewGore(this.position, this.velocity, 80, 1f);
																																					Gore.NewGore(this.position, this.velocity, 81, 1f);
																																					return;
																																				}
																																				else
																																				{
																																					if (this.type == 62 || this.type == 66)
																																					{
																																						if (this.life > 0)
																																						{
																																							int num87 = 0;
																																							while ((double)num87 < dmg / (double)this.lifeMax * 100.0)
																																							{
																																								Vector2 arg_35F6_0 = this.position;
																																								int arg_35F6_1 = this.width;
																																								int arg_35F6_2 = this.height;
																																								int arg_35F6_3 = 5;
																																								float arg_35F6_4 = (float)hitDirection;
																																								float arg_35F6_5 = -1f;
																																								int arg_35F6_6 = 0;
																																								Color newColor2 = default(Color);
																																								Dust.NewDust(arg_35F6_0, arg_35F6_1, arg_35F6_2, arg_35F6_3, arg_35F6_4, arg_35F6_5, arg_35F6_6, newColor2, 1f);
																																								num87++;
																																							}
																																							return;
																																						}
																																						for (int num88 = 0; num88 < 50; num88++)
																																						{
																																							Vector2 arg_364C_0 = this.position;
																																							int arg_364C_1 = this.width;
																																							int arg_364C_2 = this.height;
																																							int arg_364C_3 = 5;
																																							float arg_364C_4 = (float)(2 * hitDirection);
																																							float arg_364C_5 = -2f;
																																							int arg_364C_6 = 0;
																																							Color newColor2 = default(Color);
																																							Dust.NewDust(arg_364C_0, arg_364C_1, arg_364C_2, arg_364C_3, arg_364C_4, arg_364C_5, arg_364C_6, newColor2, 1f);
																																						}
																																						Gore.NewGore(this.position, this.velocity, 93, 1f);
																																						Gore.NewGore(this.position, this.velocity, 94, 1f);
																																						Gore.NewGore(this.position, this.velocity, 94, 1f);
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
					return;
				}
				if (this.life > 0)
				{
					int num89 = 0;
					while ((double)num89 < dmg / (double)this.lifeMax * 80.0)
					{
						Vector2 arg_35A_0 = this.position;
						int arg_35A_1 = this.width;
						int arg_35A_2 = this.height;
						int arg_35A_3 = 6;
						float arg_35A_4 = (float)(hitDirection * 2);
						float arg_35A_5 = -1f;
						int arg_35A_6 = this.alpha;
						Color newColor2 = default(Color);
						Dust.NewDust(arg_35A_0, arg_35A_1, arg_35A_2, arg_35A_3, arg_35A_4, arg_35A_5, arg_35A_6, newColor2, 1.5f);
						num89++;
					}
					return;
				}
				for (int num90 = 0; num90 < 40; num90++)
				{
					Vector2 arg_3B5_0 = this.position;
					int arg_3B5_1 = this.width;
					int arg_3B5_2 = this.height;
					int arg_3B5_3 = 6;
					float arg_3B5_4 = (float)(hitDirection * 2);
					float arg_3B5_5 = -1f;
					int arg_3B5_6 = this.alpha;
					Color newColor2 = default(Color);
					Dust.NewDust(arg_3B5_0, arg_3B5_1, arg_3B5_2, arg_3B5_3, arg_3B5_4, arg_3B5_5, arg_3B5_6, newColor2, 1.5f);
				}
				return;
			}
		}
		public static bool AnyNPCs(int Type)
		{
			for (int i = 0; i < 1000; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == Type)
				{
					return true;
				}
			}
			return false;
		}
		public static void SpawnSkeletron()
		{
			bool flag = true;
			bool flag2 = false;
			Vector2 vector = default(Vector2);
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < 1000; i++)
			{
				if (Main.npc[i].active && Main.npc[i].type == 35)
				{
					flag = false;
					break;
				}
			}
			for (int j = 0; j < 1000; j++)
			{
				if (Main.npc[j].active && Main.npc[j].type == 37)
				{
					flag2 = true;
					Main.npc[j].ai[3] = 1f;
					vector = Main.npc[j].position;
					num = Main.npc[j].width;
					num2 = Main.npc[j].height;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, "", j, 0f, 0f, 0f, 0);
					}
				}
			}
			if (flag && flag2)
			{
				int num3 = NPC.NewNPC((int)vector.X + num / 2, (int)vector.Y + num2 / 2, 35, 0);
				Main.npc[num3].netUpdate = true;
				string str = "Skeletron";
				if (Main.netMode == 0)
				{
					Main.NewText(str + " has awoken!", 175, 75, 255);
					return;
				}
				if (Main.netMode == 2)
				{
					NetMessage.SendData(25, -1, -1, str + " has awoken!", 255, 175f, 75f, 255f, 0);
				}
			}
		}
		public static bool NearSpikeBall(int x, int y)
		{
			Rectangle rectangle = new Rectangle(x * 16 - 200, y * 16 - 200, 400, 400);
			for (int i = 0; i < 1000; i++)
			{
				if (Main.npc[i].active && Main.npc[i].aiStyle == 20)
				{
					Rectangle rectangle2 = new Rectangle((int)Main.npc[i].ai[1], (int)Main.npc[i].ai[2], 20, 20);
					if (rectangle.Intersects(rectangle2))
					{
						return true;
					}
				}
			}
			return false;
		}
		public void AddBuff(int type, int time, bool quiet = false)
		{
			if (this.buffImmune[type])
			{
				return;
			}
			if (!quiet)
			{
				if (Main.netMode == 1)
				{
					NetMessage.SendData(53, -1, -1, "", this.whoAmI, (float)type, (float)time, 0f, 0);
				}
				else
				{
					if (Main.netMode == 2)
					{
						NetMessage.SendData(54, -1, -1, "", this.whoAmI, 0f, 0f, 0f, 0);
					}
				}
			}
			int num = -1;
			for (int i = 0; i < 5; i++)
			{
				if (this.buffType[i] == type)
				{
					if (this.buffTime[i] < time)
					{
						this.buffTime[i] = time;
					}
					return;
				}
			}
			while (num == -1)
			{
				int num2 = -1;
				for (int j = 0; j < 5; j++)
				{
					if (!Main.debuff[this.buffType[j]])
					{
						num2 = j;
						break;
					}
				}
				if (num2 == -1)
				{
					return;
				}
				for (int k = num2; k < 5; k++)
				{
					if (this.buffType[k] == 0)
					{
						num = k;
						break;
					}
				}
				if (num == -1)
				{
					this.DelBuff(num2);
				}
			}
			this.buffType[num] = type;
			this.buffTime[num] = time;
		}
		public void DelBuff(int b)
		{
			this.buffTime[b] = 0;
			this.buffType[b] = 0;
			for (int i = 0; i < 4; i++)
			{
				if (this.buffTime[i] == 0 || this.buffType[i] == 0)
				{
					for (int j = i + 1; j < 5; j++)
					{
						this.buffTime[j - 1] = this.buffTime[j];
						this.buffType[j - 1] = this.buffType[j];
						this.buffTime[j] = 0;
						this.buffType[j] = 0;
					}
				}
			}
			if (Main.netMode == 2)
			{
				NetMessage.SendData(54, -1, -1, "", this.whoAmI, 0f, 0f, 0f, 0);
			}
		}
		public void UpdateNPC(int i)
		{
			this.whoAmI = i;
			if (this.active)
			{
				this.lifeRegen = 0;
				this.poisoned = false;
				this.onFire = false;
				for (int j = 0; j < 5; j++)
				{
					if (this.buffType[j] > 0 && this.buffTime[j] > 0)
					{
						this.buffTime[j]--;
						if (this.buffType[j] == 20)
						{
							this.poisoned = true;
						}
						else
						{
							if (this.buffType[j] == 24)
							{
								this.onFire = true;
							}
						}
					}
				}
				if (Main.netMode != 1)
				{
					for (int k = 0; k < 5; k++)
					{
						if (this.buffType[k] > 0 && this.buffTime[k] <= 0)
						{
							this.DelBuff(k);
							if (Main.netMode == 2)
							{
								NetMessage.SendData(54, -1, -1, "", this.whoAmI, 0f, 0f, 0f, 0);
							}
						}
					}
				}
				if (this.poisoned)
				{
					this.lifeRegen = -4;
				}
				if (this.onFire)
				{
					this.lifeRegen = -8;
				}
				this.lifeRegenCount += this.lifeRegen;
				while (this.lifeRegenCount >= 120)
				{
					this.lifeRegenCount -= 120;
					if (this.life < this.lifeMax)
					{
						this.life++;
					}
					if (this.life > this.lifeMax)
					{
						this.life = this.lifeMax;
					}
				}
				while (this.lifeRegenCount <= -120)
				{
					this.lifeRegenCount += 120;
					this.life--;
					if (this.life <= 0)
					{
						this.life = 1;
						if (Main.netMode != 1)
						{
							this.StrikeNPC(9999, 0f, 0, false);
							if (Main.netMode == 2)
							{
								NetMessage.SendData(28, -1, -1, "", this.whoAmI, 9999f, 0f, 0f, 0);
							}
						}
					}
				}
				if (Main.netMode != 1 && Main.bloodMoon)
				{
					if (this.type == 46)
					{
						this.Transform(47);
					}
					else
					{
						if (this.type == 55)
						{
							this.Transform(57);
						}
					}
				}
				float num = 10f;
				float num2 = 0.3f;
				if (this.wet)
				{
					num2 = 0.2f;
					num = 7f;
				}
				if (this.soundDelay > 0)
				{
					this.soundDelay--;
				}
				if (this.life <= 0)
				{
					this.active = false;
				}
				this.oldTarget = this.target;
				this.oldDirection = this.direction;
				this.oldDirectionY = this.directionY;
				this.AI();
				if (this.type == 44)
				{
					Lighting.addLight((int)(this.position.X + (float)(this.width / 2)) / 16, (int)(this.position.Y + 4f) / 16, 0.6f);
				}
				for (int l = 0; l < 256; l++)
				{
					if (this.immune[l] > 0)
					{
						this.immune[l]--;
					}
				}
				if (!this.noGravity)
				{
					this.velocity.Y = this.velocity.Y + num2;
					if (this.velocity.Y > num)
					{
						this.velocity.Y = num;
					}
				}
				if ((double)this.velocity.X < 0.005 && (double)this.velocity.X > -0.005)
				{
					this.velocity.X = 0f;
				}
				if (Main.netMode != 1 && this.friendly && this.type != 37)
				{
					if (this.life < this.lifeMax)
					{
						this.friendlyRegen++;
						if (this.friendlyRegen > 300)
						{
							this.friendlyRegen = 0;
							this.life++;
							this.netUpdate = true;
						}
					}
					if (this.immune[255] == 0)
					{
						Rectangle rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height);
						for (int m = 0; m < 1000; m++)
						{
							if (Main.npc[m].active && !Main.npc[m].friendly && Main.npc[m].damage > 0)
							{
								Rectangle rectangle2 = new Rectangle((int)Main.npc[m].position.X, (int)Main.npc[m].position.Y, Main.npc[m].width, Main.npc[m].height);
								if (rectangle.Intersects(rectangle2))
								{
									int num3 = Main.npc[m].damage;
									int num4 = 6;
									int num5 = 1;
									if (Main.npc[m].position.X + (float)(Main.npc[m].width / 2) > this.position.X + (float)(this.width / 2))
									{
										num5 = -1;
									}
									Main.npc[i].StrikeNPC(num3, (float)num4, num5, false);
									if (Main.netMode != 0)
									{
										NetMessage.SendData(28, -1, -1, "", i, (float)num3, (float)num4, (float)num5, 0);
									}
									this.netUpdate = true;
									this.immune[255] = 30;
								}
							}
						}
					}
				}
				if (!this.noTileCollide)
				{
					bool flag = Collision.LavaCollision(this.position, this.width, this.height);
					if (flag)
					{
						this.lavaWet = true;
						if (!this.lavaImmune && Main.netMode != 1 && this.immune[255] == 0)
						{
							this.AddBuff(24, 420, false);
							this.immune[255] = 30;
							this.StrikeNPC(50, 0f, 0, false);
							if (Main.netMode == 2 && Main.netMode != 0)
							{
								NetMessage.SendData(28, -1, -1, "", this.whoAmI, 50f, 0f, 0f, 0);
							}
						}
					}
					bool flag2 = false;
					if (this.type == 72)
					{
						flag2 = false;
						this.wetCount = 0;
						flag = false;
					}
					else
					{
						flag2 = Collision.WetCollision(this.position, this.width, this.height);
					}
					if (flag2)
					{
						if (this.onFire && !this.lavaWet && Main.netMode != 1)
						{
							for (int n = 0; n < 5; n++)
							{
								if (this.buffType[n] == 24)
								{
									this.DelBuff(n);
								}
							}
						}
						if (!this.wet && this.wetCount == 0)
						{
							this.wetCount = 10;
							if (!flag)
							{
								for (int num6 = 0; num6 < 30; num6++)
								{
									int num7 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f), this.width + 12, 24, 33, 0f, 0f, 0, default(Color), 1f);
									Dust expr_727_cp_0 = Main.dust[num7];
									expr_727_cp_0.velocity.Y = expr_727_cp_0.velocity.Y - 4f;
									Dust expr_745_cp_0 = Main.dust[num7];
									expr_745_cp_0.velocity.X = expr_745_cp_0.velocity.X * 2.5f;
									Main.dust[num7].scale = 1.3f;
									Main.dust[num7].alpha = 100;
									Main.dust[num7].noGravity = true;
								}
								if (this.type != 1 && this.type != 59 && !this.noGravity)
								{
									Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 0);
								}
							}
							else
							{
								for (int num8 = 0; num8 < 10; num8++)
								{
									int num9 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f), this.width + 12, 24, 35, 0f, 0f, 0, default(Color), 1f);
									Dust expr_851_cp_0 = Main.dust[num9];
									expr_851_cp_0.velocity.Y = expr_851_cp_0.velocity.Y - 1.5f;
									Dust expr_86F_cp_0 = Main.dust[num9];
									expr_86F_cp_0.velocity.X = expr_86F_cp_0.velocity.X * 2.5f;
									Main.dust[num9].scale = 1.3f;
									Main.dust[num9].alpha = 100;
									Main.dust[num9].noGravity = true;
								}
								if (this.type != 1 && this.type != 59 && !this.noGravity)
								{
									Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1);
								}
							}
						}
						this.wet = true;
					}
					else
					{
						if (this.wet)
						{
							this.velocity.X = this.velocity.X * 0.5f;
							this.wet = false;
							if (this.wetCount == 0)
							{
								this.wetCount = 10;
								if (!this.lavaWet)
								{
									for (int num10 = 0; num10 < 30; num10++)
									{
										int num11 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f), this.width + 12, 24, 33, 0f, 0f, 0, default(Color), 1f);
										Dust expr_9C0_cp_0 = Main.dust[num11];
										expr_9C0_cp_0.velocity.Y = expr_9C0_cp_0.velocity.Y - 4f;
										Dust expr_9DE_cp_0 = Main.dust[num11];
										expr_9DE_cp_0.velocity.X = expr_9DE_cp_0.velocity.X * 2.5f;
										Main.dust[num11].scale = 1.3f;
										Main.dust[num11].alpha = 100;
										Main.dust[num11].noGravity = true;
									}
									if (this.type != 1 && this.type != 59 && !this.noGravity)
									{
										Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 0);
									}
								}
								else
								{
									for (int num12 = 0; num12 < 10; num12++)
									{
										int num13 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f), this.width + 12, 24, 35, 0f, 0f, 0, default(Color), 1f);
										Dust expr_AEA_cp_0 = Main.dust[num13];
										expr_AEA_cp_0.velocity.Y = expr_AEA_cp_0.velocity.Y - 1.5f;
										Dust expr_B08_cp_0 = Main.dust[num13];
										expr_B08_cp_0.velocity.X = expr_B08_cp_0.velocity.X * 2.5f;
										Main.dust[num13].scale = 1.3f;
										Main.dust[num13].alpha = 100;
										Main.dust[num13].noGravity = true;
									}
									if (this.type != 1 && this.type != 59 && !this.noGravity)
									{
										Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1);
									}
								}
							}
						}
					}
					if (!this.wet)
					{
						this.lavaWet = false;
					}
					if (this.wetCount > 0)
					{
						this.wetCount -= 1;
					}
					bool flag3 = false;
					if (this.aiStyle == 10)
					{
						flag3 = true;
					}
					if (this.aiStyle == 14)
					{
						flag3 = true;
					}
					if (this.aiStyle == 3 && this.directionY == 1)
					{
						flag3 = true;
					}
					this.oldVelocity = this.velocity;
					this.collideX = false;
					this.collideY = false;
					if (this.wet)
					{
						Vector2 vector = this.velocity;
						this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, flag3, flag3);
						if (Collision.up)
						{
							this.velocity.Y = 0.01f;
						}
						Vector2 value = this.velocity * 0.5f;
						if (this.velocity.X != vector.X)
						{
							value.X = this.velocity.X;
							this.collideX = true;
						}
						if (this.velocity.Y != vector.Y)
						{
							value.Y = this.velocity.Y;
							this.collideY = true;
						}
						this.oldPosition = this.position;
						this.position += value;
					}
					else
					{
						if (this.type == 72)
						{
							Vector2 vector2 = new Vector2(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2));
							int num14 = 12;
							int num15 = 12;
							vector2.X -= (float)(num14 / 2);
							vector2.Y -= (float)(num15 / 2);
							this.velocity = Collision.TileCollision(vector2, this.velocity, num14, num15, true, true);
						}
						else
						{
							this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, flag3, flag3);
						}
						if (Collision.up)
						{
							this.velocity.Y = 0.01f;
						}
						if (this.oldVelocity.X != this.velocity.X)
						{
							this.collideX = true;
						}
						if (this.oldVelocity.Y != this.velocity.Y)
						{
							this.collideY = true;
						}
						this.oldPosition = this.position;
						this.position += this.velocity;
					}
				}
				else
				{
					this.oldPosition = this.position;
					this.position += this.velocity;
				}
				if (!this.active)
				{
					this.netUpdate = true;
				}
				if (Main.netMode == 2 && this.netUpdate)
				{
					NetMessage.SendData(23, -1, -1, "", i, 0f, 0f, 0f, 0);
				}
				this.FindFrame();
				this.CheckActive();
				this.netUpdate = false;
				this.justHit = false;
			}
		}
		public Color GetAlpha(Color newColor)
		{
			int r = (int)newColor.R - this.alpha;
			int g = (int)newColor.G - this.alpha;
			int b = (int)newColor.B - this.alpha;
			int num = (int)newColor.A - this.alpha;
			if (this.type == 25 || this.type == 30 || this.type == 33 || this.type == 72)
			{
				r = (int)newColor.R;
				g = (int)newColor.G;
				b = (int)newColor.B;
			}
			if (num < 0)
			{
				num = 0;
			}
			if (num > 255)
			{
				num = 255;
			}
			return new Color(r, g, b, num);
		}
		public Color GetColor(Color newColor)
		{
			int num = (int)(this.color.R - (255 - newColor.R));
			int num2 = (int)(this.color.G - (255 - newColor.G));
			int num3 = (int)(this.color.B - (255 - newColor.B));
			int num4 = (int)(this.color.A - (255 - newColor.A));
			if (num < 0)
			{
				num = 0;
			}
			if (num > 255)
			{
				num = 255;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num2 > 255)
			{
				num2 = 255;
			}
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num3 > 255)
			{
				num3 = 255;
			}
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num4 > 255)
			{
				num4 = 255;
			}
			return new Color(num, num2, num3, num4);
		}
		public string GetChat()
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			for (int i = 0; i < 1000; i++)
			{
				if (Main.npc[i].type == 17)
				{
					flag = true;
				}
				else
				{
					if (Main.npc[i].type == 18)
					{
						flag2 = true;
					}
					else
					{
						if (Main.npc[i].type == 19)
						{
							flag3 = true;
						}
						else
						{
							if (Main.npc[i].type == 20)
							{
								flag4 = true;
							}
							else
							{
								if (Main.npc[i].type == 37)
								{
									flag5 = true;
								}
								else
								{
									if (Main.npc[i].type == 38)
									{
										flag6 = true;
									}
								}
							}
						}
					}
				}
			}
			string result = "";
			if (this.type == 17)
			{
				if (Main.dayTime)
				{
					if (Main.time < 16200.0)
					{
						if (Main.rand.Next(2) == 0)
						{
							result = "Sword beats paper, get one today.";
						}
						else
						{
							result = "Lovely morning, wouldn't you say? Was there something you needed?";
						}
					}
					else
					{
						if (Main.time > 37800.0)
						{
							if (Main.rand.Next(2) == 0)
							{
								result = "Night be upon us soon, friend. Make your choices while you can.";
							}
							else
							{
								result = "Ah, they will tell tales of " + Main.player[Main.myPlayer].name + " some day... good ones I'm sure.";
							}
						}
						else
						{
							int num = Main.rand.Next(3);
							if (num == 0)
							{
								result = "Check out my dirt blocks, they are extra dirty.";
							}
							if (num == 1)
							{
								result = "Boy, that sun is hot! I do have some perfectly ventilated armor.";
							}
							else
							{
								result = "The sun is high, but my prices are not.";
							}
						}
					}
				}
				else
				{
					if (Main.bloodMoon)
					{
						if (Main.rand.Next(2) == 0)
						{
							result = "Have you seen Chith...Shith.. Chat... The big eye?";
						}
						else
						{
							result = "Keep your eye on the prize, buy a lense!";
						}
					}
					else
					{
						if (Main.time < 9720.0)
						{
							if (Main.rand.Next(2) == 0)
							{
								result = "Kosh, kapleck Mog. Oh sorry, that's klingon for 'Buy something or die.'";
							}
							else
							{
								result = Main.player[Main.myPlayer].name + " is it? I've heard good things, friend!";
							}
						}
						else
						{
							if (Main.time > 22680.0)
							{
								if (Main.rand.Next(2) == 0)
								{
									result = "I hear there's a secret treasure... oh never mind.";
								}
								else
								{
									result = "Angel Statue you say? I'm sorry, I'm not a junk dealer.";
								}
							}
							else
							{
								int num2 = Main.rand.Next(3);
								if (num2 == 0)
								{
									result = "The last guy who was here left me some junk..er I mean.. treasures!";
								}
								if (num2 == 1)
								{
									result = "I wonder if the moon is made of cheese...huh, what? Oh yes, buy something!";
								}
								else
								{
									result = "Did you say gold?  I'll take that off of ya'.";
								}
							}
						}
					}
				}
			}
			else
			{
				if (this.type == 18)
				{
					if (flag6 && Main.rand.Next(4) == 0)
					{
						result = "I wish that bomb maker would be more careful.  I'm getting tired of having to sew his limbs back on every day.";
					}
					else
					{
						if ((double)Main.player[Main.myPlayer].statLife < (double)Main.player[Main.myPlayer].statLifeMax * 0.33)
						{
							int num3 = Main.rand.Next(5);
							if (num3 == 0)
							{
								result = "I think you look better this way.";
							}
							else
							{
								if (num3 == 1)
								{
									result = "Eww.. What happened to your face?";
								}
								else
								{
									if (num3 == 2)
									{
										result = "MY GOODNESS! I'm good but I'm not THAT good.";
									}
									else
									{
										if (num3 == 3)
										{
											result = "Dear friends we are gathered here today to bid farewell... oh, you'll be fine.";
										}
										else
										{
											result = "You left your arm over there. Let me get that for you..";
										}
									}
								}
							}
						}
						else
						{
							if ((double)Main.player[Main.myPlayer].statLife < (double)Main.player[Main.myPlayer].statLifeMax * 0.66)
							{
								int num4 = Main.rand.Next(4);
								if (num4 == 0)
								{
									result = "Quit being such a baby! I've seen worse.";
								}
								else
								{
									if (num4 == 1)
									{
										result = "That's gonna need stitches!";
									}
									else
									{
										if (num4 == 2)
										{
											result = "Trouble with those bullies again?";
										}
										else
										{
											result = "You look half digested. Have you been chasing slimes again?";
										}
									}
								}
							}
							else
							{
								int num5 = Main.rand.Next(3);
								if (num5 == 0)
								{
									result = "Turn your head and cough.";
								}
								else
								{
									if (num5 == 1)
									{
										result = "That's not the biggest I've ever seen... Yes, I've seen bigger wounds for sure.";
									}
									else
									{
										result = "Show me where it hurts.";
									}
								}
							}
						}
					}
				}
				else
				{
					if (this.type == 19)
					{
						if (flag2 && Main.rand.Next(4) == 0)
						{
							result = "Make it quick! I've got a date with the nurse in an hour.";
						}
						else
						{
							if (flag4 && Main.rand.Next(4) == 0)
							{
								result = "That dryad is a looker.  Too bad she's such a prude.";
							}
							else
							{
								if (flag6 && Main.rand.Next(4) == 0)
								{
									result = "Don't bother with that firework vendor, I've got all you need right here.";
								}
								else
								{
									if (Main.bloodMoon)
									{
										result = "I love nights like tonight.  There is never a shortage of things to kill!";
									}
									else
									{
										int num6 = Main.rand.Next(2);
										if (num6 == 0)
										{
											result = "I see you're eyeballin' the Minishark.. You really don't want to know how it was made.";
										}
										else
										{
											if (num6 == 1)
											{
												result = "Keep your hands off my gun, buddy!";
											}
										}
									}
								}
							}
						}
					}
					else
					{
						if (this.type == 20)
						{
							if (flag3 && Main.rand.Next(4) == 0)
							{
								result = "I wish that gun seller would stop talking to me. Doesn't he realize I'm 500 years old?";
							}
							else
							{
								if (flag && Main.rand.Next(4) == 0)
								{
									result = "That merchant keeps trying to sell me an angel statue. Everyone knows that they don't do anything.";
								}
								else
								{
									if (flag5 && Main.rand.Next(4) == 0)
									{
										result = "Have you seen the old man walking around the dungeon? He doesn't look well at all...";
									}
									else
									{
										if (Main.bloodMoon)
										{
											result = "It is an evil moon tonight. Be careful.";
										}
										else
										{
											int num7 = Main.rand.Next(6);
											if (num7 == 0)
											{
												result = "You must cleanse the world of this corruption.";
											}
											else
											{
												if (num7 == 1)
												{
													result = "Be safe; Freeria needs you!";
												}
												else
												{
													if (num7 == 2)
													{
														result = "The sands of time are flowing. And well, you are not aging very gracefully.";
													}
													else
													{
														if (num7 == 3)
														{
															result = "What's this about me having more 'bark' than bite?";
														}
														else
														{
															if (num7 == 4)
															{
																result = "So two goblins walk into a bar, and one says to the other, 'Want to get a Gobblet of beer?!'";
															}
															else
															{
																result = "Be safe; Freeria needs you!";
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
							if (this.type == 22)
							{
								if (Main.bloodMoon)
								{
									result = "You can tell a Blood Moon is out when the sky turns red. There is something about it that causes monsters to swarm.";
								}
								else
								{
									if (!Main.dayTime)
									{
										result = "You should stay indoors at night. It is very dangerous to be wandering around in the dark.";
									}
									else
									{
										int num8 = Main.rand.Next(3);
										if (num8 == 0)
										{
											result = "Greetings, " + Main.player[Main.myPlayer].name + ". Is there something I can help you with?";
										}
										else
										{
											if (num8 == 1)
											{
												result = "I am here to give you advice on what to do next.  It is recommended that you talk with me anytime you get stuck.";
											}
											else
											{
												if (num8 == 2)
												{
													result = "They say there is a person who will tell you how to survive in this land... oh wait. That's me.";
												}
											}
										}
									}
								}
							}
							else
							{
								if (this.type == 37)
								{
									if (Main.dayTime)
									{
										int num9 = Main.rand.Next(2);
										if (num9 == 0)
										{
											result = "I cannot let you enter until you free me of my curse.";
										}
										else
										{
											if (num9 == 1)
											{
												result = "Come back at night if you wish to enter.";
											}
										}
									}
									else
									{
										if (Main.player[Main.myPlayer].statLifeMax < 300 || Main.player[Main.myPlayer].statDefense < 10)
										{
											int num10 = Main.rand.Next(2);
											if (num10 == 0)
											{
												result = "You are far to weak to defeat my curse.  Come back when you aren't so worthless.";
											}
											else
											{
												if (num10 == 1)
												{
													result = "You pathetic fool.  You cannot hope to face my master as you are now.";
												}
											}
										}
										else
										{
											int num11 = Main.rand.Next(2);
											if (num11 == 0)
											{
												result = "You just might be strong enough to free me from my curse...";
											}
											else
											{
												if (num11 == 1)
												{
													result = "Stranger, do you possess the strength to defeat my master?";
												}
											}
										}
									}
								}
								else
								{
									if (this.type == 38)
									{
										if (Main.bloodMoon)
										{
											result = "I've got something for them zombies alright!";
										}
										else
										{
											if (flag3 && Main.rand.Next(4) == 0)
											{
												result = "Even the gun dealer wants what I'm selling!";
											}
											else
											{
												if (flag2 && Main.rand.Next(4) == 0)
												{
													result = "I'm sure the nurse will help if you accidentally lose a limb to these.";
												}
												else
												{
													if (flag4 && Main.rand.Next(4) == 0)
													{
														result = "Why purify the world when you can just blow it up?";
													}
													else
													{
														int num12 = Main.rand.Next(4);
														if (num12 == 0)
														{
															result = "Explosives are da' bomb these days.  Buy some now!";
														}
														else
														{
															if (num12 == 1)
															{
																result = "It's a good day to die!";
															}
															else
															{
																if (num12 == 2)
																{
																	result = "I wonder what happens if I... (BOOM!)... Oh, sorry, did you need that leg?";
																}
																else
																{
																	if (num12 == 3)
																	{
																		result = "Dynamite, my own special cure-all for what ails ya.";
																	}
																	else
																	{
																		result = "Check out my goods; they have explosive prices!";
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
										if (this.type == 54)
										{
											if (Main.bloodMoon)
											{
												result = Main.player[Main.myPlayer].name + "... we have a problem! Its a blood moon out there!";
											}
											else
											{
												if (flag2 && Main.rand.Next(4) == 0)
												{
													result = "T'were I younger, I would ask the nurse out. I used to be quite the lady killer.";
												}
												else
												{
													if (Main.player[Main.myPlayer].head == 24)
													{
														result = "That Red Hat of yours looks familiar...";
													}
													else
													{
														int num13 = Main.rand.Next(4);
														if (num13 == 0)
														{
															result = "Thanks again for freeing me from my curse. Felt like something jumped up and bit me";
														}
														else
														{
															if (num13 == 1)
															{
																result = "Mama always said I would make a great tailor.";
															}
															else
															{
																if (num13 == 2)
																{
																	result = "Life's like a box of clothes, you never know what you are gonna wear!";
																}
																else
																{
																	result = "Being cursed was lonely, so I once made a friend out of leather. I named him Wilson.";
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
			return result;
		}
		public object Clone()
		{
			return base.MemberwiseClone();
		}
	}
}
