using Microsoft.Xna.Framework;
using System;
namespace Freeria
{
	public class Projectile
	{
		public bool wet;
		public byte wetCount;
		public bool lavaWet;
		public int whoAmI;
		public static int maxAI = 2;
		public Vector2 position;
		public Vector2 velocity;
		public int width;
		public int height;
		public float scale = 1f;
		public float rotation;
		public int type;
		public int alpha;
		public int owner = 255;
		public bool active;
		public string name = "";
		public float[] ai = new float[Projectile.maxAI];
		public int aiStyle;
		public int timeLeft;
		public int soundDelay;
		public int damage;
		public int direction;
		public bool hostile;
		public float knockBack;
		public bool friendly;
		public int penetrate = 1;
		public int identity;
		public float light;
		public bool netUpdate;
		public int restrikeDelay;
		public bool tileCollide;
		public int maxUpdates;
		public int numUpdates;
		public bool ignoreWater;
		public bool hide;
		public bool ownerHitCheck;
		public int[] playerImmune = new int[255];
		public string miscText = "";
		public bool melee;
		public bool ranged;
		public bool magic;
		public void SetDefaults(int Type)
		{
			for (int i = 0; i < Projectile.maxAI; i++)
			{
				this.ai[i] = 0f;
			}
			for (int j = 0; j < 255; j++)
			{
				this.playerImmune[j] = 0;
			}
			this.melee = false;
			this.ranged = false;
			this.magic = false;
			this.ownerHitCheck = false;
			this.hide = false;
			this.lavaWet = false;
			this.wetCount = 0;
			this.wet = false;
			this.ignoreWater = false;
			this.hostile = false;
			this.netUpdate = false;
			this.numUpdates = 0;
			this.maxUpdates = 0;
			this.identity = 0;
			this.restrikeDelay = 0;
			this.light = 0f;
			this.penetrate = 1;
			this.tileCollide = true;
			this.position = default(Vector2);
			this.velocity = default(Vector2);
			this.aiStyle = 0;
			this.alpha = 0;
			this.type = Type;
			this.active = true;
			this.rotation = 0f;
			this.scale = 1f;
			this.owner = 255;
			this.timeLeft = 3600;
			this.name = "";
			this.friendly = false;
			this.damage = 0;
			this.knockBack = 0f;
			this.miscText = "";
			if (this.type == 1)
			{
				this.name = "Wooden Arrow";
				this.width = 10;
				this.height = 10;
				this.aiStyle = 1;
				this.friendly = true;
				this.ranged = true;
			}
			else
			{
				if (this.type == 2)
				{
					this.name = "Fire Arrow";
					this.width = 10;
					this.height = 10;
					this.aiStyle = 1;
					this.friendly = true;
					this.light = 1f;
					this.ranged = true;
				}
				else
				{
					if (this.type == 3)
					{
						this.name = "Shuriken";
						this.width = 22;
						this.height = 22;
						this.aiStyle = 2;
						this.friendly = true;
						this.penetrate = 4;
						this.ranged = true;
					}
					else
					{
						if (this.type == 4)
						{
							this.name = "Unholy Arrow";
							this.width = 10;
							this.height = 10;
							this.aiStyle = 1;
							this.friendly = true;
							this.light = 0.2f;
							this.penetrate = 5;
							this.ranged = true;
						}
						else
						{
							if (this.type == 5)
							{
								this.name = "Jester's Arrow";
								this.width = 10;
								this.height = 10;
								this.aiStyle = 1;
								this.friendly = true;
								this.light = 0.4f;
								this.penetrate = -1;
								this.timeLeft = 40;
								this.alpha = 100;
								this.ignoreWater = true;
								this.ranged = true;
							}
							else
							{
								if (this.type == 6)
								{
									this.name = "Enchanted Boomerang";
									this.width = 22;
									this.height = 22;
									this.aiStyle = 3;
									this.friendly = true;
									this.penetrate = -1;
									this.ranged = true;
								}
								else
								{
									if (this.type == 7 || this.type == 8)
									{
										this.name = "Vilethorn";
										this.width = 28;
										this.height = 28;
										this.aiStyle = 4;
										this.friendly = true;
										this.penetrate = -1;
										this.tileCollide = false;
										this.alpha = 255;
										this.ignoreWater = true;
										this.magic = true;
									}
									else
									{
										if (this.type == 9)
										{
											this.name = "Starfury";
											this.width = 24;
											this.height = 24;
											this.aiStyle = 5;
											this.friendly = true;
											this.penetrate = 2;
											this.alpha = 50;
											this.scale = 0.8f;
											this.tileCollide = false;
											this.magic = true;
										}
										else
										{
											if (this.type == 10)
											{
												this.name = "Purification Powder";
												this.width = 64;
												this.height = 64;
												this.aiStyle = 6;
												this.friendly = true;
												this.tileCollide = false;
												this.penetrate = -1;
												this.alpha = 255;
												this.ignoreWater = true;
											}
											else
											{
												if (this.type == 11)
												{
													this.name = "Vile Powder";
													this.width = 48;
													this.height = 48;
													this.aiStyle = 6;
													this.friendly = true;
													this.tileCollide = false;
													this.penetrate = -1;
													this.alpha = 255;
													this.ignoreWater = true;
												}
												else
												{
													if (this.type == 12)
													{
														this.name = "Fallen Star";
														this.width = 16;
														this.height = 16;
														this.aiStyle = 5;
														this.friendly = true;
														this.penetrate = -1;
														this.alpha = 50;
														this.light = 1f;
													}
													else
													{
														if (this.type == 13)
														{
															this.name = "Hook";
															this.width = 18;
															this.height = 18;
															this.aiStyle = 7;
															this.friendly = true;
															this.penetrate = -1;
															this.tileCollide = false;
															this.timeLeft *= 10;
														}
														else
														{
															if (this.type == 14)
															{
																this.name = "Musket Ball";
																this.width = 4;
																this.height = 4;
																this.aiStyle = 1;
																this.friendly = true;
																this.penetrate = 1;
																this.light = 0.5f;
																this.alpha = 255;
																this.maxUpdates = 1;
																this.scale = 1.2f;
																this.timeLeft = 600;
																this.ranged = true;
															}
															else
															{
																if (this.type == 15)
																{
																	this.name = "Ball of Fire";
																	this.width = 16;
																	this.height = 16;
																	this.aiStyle = 8;
																	this.friendly = true;
																	this.light = 0.8f;
																	this.alpha = 100;
																	this.magic = true;
																}
																else
																{
																	if (this.type == 16)
																	{
																		this.name = "Magic Missile";
																		this.width = 10;
																		this.height = 10;
																		this.aiStyle = 9;
																		this.friendly = true;
																		this.light = 0.8f;
																		this.alpha = 100;
																		this.magic = true;
																	}
																	else
																	{
																		if (this.type == 17)
																		{
																			this.name = "Dirt Ball";
																			this.width = 10;
																			this.height = 10;
																			this.aiStyle = 10;
																			this.friendly = true;
																		}
																		else
																		{
																			if (this.type == 18)
																			{
																				this.name = "Orb of Light";
																				this.width = 32;
																				this.height = 32;
																				this.aiStyle = 11;
																				this.friendly = true;
																				this.light = 0.65f;
																				this.alpha = 150;
																				this.tileCollide = false;
																				this.penetrate = -1;
																				this.timeLeft *= 5;
																				this.ignoreWater = true;
																				this.scale = 0.8f;
																			}
																			else
																			{
																				if (this.type == 19)
																				{
																					this.name = "Flamarang";
																					this.width = 22;
																					this.height = 22;
																					this.aiStyle = 3;
																					this.friendly = true;
																					this.penetrate = -1;
																					this.light = 1f;
																					this.ranged = true;
																				}
																				else
																				{
																					if (this.type == 20)
																					{
																						this.name = "Green Laser";
																						this.width = 4;
																						this.height = 4;
																						this.aiStyle = 1;
																						this.friendly = true;
																						this.penetrate = 3;
																						this.light = 0.75f;
																						this.alpha = 255;
																						this.maxUpdates = 2;
																						this.scale = 1.4f;
																						this.timeLeft = 600;
																						this.magic = true;
																					}
																					else
																					{
																						if (this.type == 21)
																						{
																							this.name = "Bone";
																							this.width = 16;
																							this.height = 16;
																							this.aiStyle = 2;
																							this.scale = 1.2f;
																							this.friendly = true;
																							this.ranged = true;
																						}
																						else
																						{
																							if (this.type == 22)
																							{
																								this.name = "Water Stream";
																								this.width = 18;
																								this.height = 18;
																								this.aiStyle = 12;
																								this.friendly = true;
																								this.alpha = 255;
																								this.penetrate = -1;
																								this.maxUpdates = 2;
																								this.ignoreWater = true;
																								this.magic = true;
																							}
																							else
																							{
																								if (this.type == 23)
																								{
																									this.name = "Harpoon";
																									this.width = 4;
																									this.height = 4;
																									this.aiStyle = 13;
																									this.friendly = true;
																									this.penetrate = -1;
																									this.alpha = 255;
																									this.ranged = true;
																								}
																								else
																								{
																									if (this.type == 24)
																									{
																										this.name = "Spiky Ball";
																										this.width = 14;
																										this.height = 14;
																										this.aiStyle = 14;
																										this.friendly = true;
																										this.penetrate = 6;
																										this.ranged = true;
																									}
																									else
																									{
																										if (this.type == 25)
																										{
																											this.name = "Ball 'O Hurt";
																											this.width = 22;
																											this.height = 22;
																											this.aiStyle = 15;
																											this.friendly = true;
																											this.penetrate = -1;
																											this.melee = true;
																											this.scale = 0.8f;
																										}
																										else
																										{
																											if (this.type == 26)
																											{
																												this.name = "Blue Moon";
																												this.width = 22;
																												this.height = 22;
																												this.aiStyle = 15;
																												this.friendly = true;
																												this.penetrate = -1;
																												this.melee = true;
																												this.scale = 0.8f;
																											}
																											else
																											{
																												if (this.type == 27)
																												{
																													this.name = "Water Bolt";
																													this.width = 16;
																													this.height = 16;
																													this.aiStyle = 8;
																													this.friendly = true;
																													this.light = 0.8f;
																													this.alpha = 200;
																													this.timeLeft /= 2;
																													this.penetrate = 10;
																													this.magic = true;
																												}
																												else
																												{
																													if (this.type == 28)
																													{
																														this.name = "Bomb";
																														this.width = 22;
																														this.height = 22;
																														this.aiStyle = 16;
																														this.friendly = true;
																														this.penetrate = -1;
																													}
																													else
																													{
																														if (this.type == 29)
																														{
																															this.name = "Dynamite";
																															this.width = 10;
																															this.height = 10;
																															this.aiStyle = 16;
																															this.friendly = true;
																															this.penetrate = -1;
																														}
																														else
																														{
																															if (this.type == 30)
																															{
																																this.name = "Grenade";
																																this.width = 14;
																																this.height = 14;
																																this.aiStyle = 16;
																																this.friendly = true;
																																this.penetrate = -1;
																																this.ranged = true;
																															}
																															else
																															{
																																if (this.type == 31)
																																{
																																	this.name = "Sand Ball";
																																	this.knockBack = 6f;
																																	this.width = 10;
																																	this.height = 10;
																																	this.aiStyle = 10;
																																	this.friendly = true;
																																	this.hostile = true;
																																	this.penetrate = -1;
																																}
																																else
																																{
																																	if (this.type == 32)
																																	{
																																		this.name = "Ivy Whip";
																																		this.width = 18;
																																		this.height = 18;
																																		this.aiStyle = 7;
																																		this.friendly = true;
																																		this.penetrate = -1;
																																		this.tileCollide = false;
																																		this.timeLeft *= 10;
																																	}
																																	else
																																	{
																																		if (this.type == 33)
																																		{
																																			this.name = "Thorn Chakrum";
																																			this.width = 28;
																																			this.height = 28;
																																			this.aiStyle = 3;
																																			this.friendly = true;
																																			this.scale = 0.9f;
																																			this.penetrate = -1;
																																			this.ranged = true;
																																		}
																																		else
																																		{
																																			if (this.type == 34)
																																			{
																																				this.name = "Flamelash";
																																				this.width = 14;
																																				this.height = 14;
																																				this.aiStyle = 9;
																																				this.friendly = true;
																																				this.light = 0.8f;
																																				this.alpha = 100;
																																				this.penetrate = 1;
																																				this.magic = true;
																																			}
																																			else
																																			{
																																				if (this.type == 35)
																																				{
																																					this.name = "Sunfury";
																																					this.width = 22;
																																					this.height = 22;
																																					this.aiStyle = 15;
																																					this.friendly = true;
																																					this.penetrate = -1;
																																					this.melee = true;
																																					this.scale = 0.8f;
																																				}
																																				else
																																				{
																																					if (this.type == 36)
																																					{
																																						this.name = "Meteor Shot";
																																						this.width = 4;
																																						this.height = 4;
																																						this.aiStyle = 1;
																																						this.friendly = true;
																																						this.penetrate = 2;
																																						this.light = 0.6f;
																																						this.alpha = 255;
																																						this.maxUpdates = 1;
																																						this.scale = 1.4f;
																																						this.timeLeft = 600;
																																						this.ranged = true;
																																					}
																																					else
																																					{
																																						if (this.type == 37)
																																						{
																																							this.name = "Sticky Bomb";
																																							this.width = 22;
																																							this.height = 22;
																																							this.aiStyle = 16;
																																							this.friendly = true;
																																							this.penetrate = -1;
																																							this.tileCollide = false;
																																						}
																																						else
																																						{
																																							if (this.type == 38)
																																							{
																																								this.name = "Harpy Feather";
																																								this.width = 14;
																																								this.height = 14;
																																								this.aiStyle = 0;
																																								this.hostile = true;
																																								this.penetrate = -1;
																																								this.aiStyle = 1;
																																								this.tileCollide = true;
																																							}
																																							else
																																							{
																																								if (this.type == 39)
																																								{
																																									this.name = "Mud Ball";
																																									this.knockBack = 6f;
																																									this.width = 10;
																																									this.height = 10;
																																									this.aiStyle = 10;
																																									this.friendly = true;
																																									this.hostile = true;
																																									this.penetrate = -1;
																																								}
																																								else
																																								{
																																									if (this.type == 40)
																																									{
																																										this.name = "Ash Ball";
																																										this.knockBack = 6f;
																																										this.width = 10;
																																										this.height = 10;
																																										this.aiStyle = 10;
																																										this.friendly = true;
																																										this.hostile = true;
																																										this.penetrate = -1;
																																									}
																																									else
																																									{
																																										if (this.type == 41)
																																										{
																																											this.name = "Hellfire Arrow";
																																											this.width = 10;
																																											this.height = 10;
																																											this.aiStyle = 1;
																																											this.friendly = true;
																																											this.penetrate = -1;
																																											this.ranged = true;
																																										}
																																										else
																																										{
																																											if (this.type == 42)
																																											{
																																												this.name = "Sand Ball";
																																												this.knockBack = 8f;
																																												this.width = 10;
																																												this.height = 10;
																																												this.aiStyle = 10;
																																												this.friendly = true;
																																												this.maxUpdates = 0;
																																											}
																																											else
																																											{
																																												if (this.type == 43)
																																												{
																																													this.name = "Tombstone";
																																													this.knockBack = 12f;
																																													this.width = 24;
																																													this.height = 24;
																																													this.aiStyle = 17;
																																													this.penetrate = -1;
																																													this.friendly = true;
																																												}
																																												else
																																												{
																																													if (this.type == 44)
																																													{
																																														this.name = "Demon Sickle";
																																														this.width = 48;
																																														this.height = 48;
																																														this.alpha = 100;
																																														this.light = 0.2f;
																																														this.aiStyle = 18;
																																														this.hostile = true;
																																														this.penetrate = -1;
																																														this.tileCollide = true;
																																														this.scale = 0.9f;
																																													}
																																													else
																																													{
																																														if (this.type == 45)
																																														{
																																															this.name = "Demon Scythe";
																																															this.width = 48;
																																															this.height = 48;
																																															this.alpha = 100;
																																															this.light = 0.2f;
																																															this.aiStyle = 18;
																																															this.friendly = true;
																																															this.penetrate = 5;
																																															this.tileCollide = true;
																																															this.scale = 0.9f;
																																															this.magic = true;
																																														}
																																														else
																																														{
																																															if (this.type == 46)
																																															{
																																																this.name = "Dark Lance";
																																																this.width = 20;
																																																this.height = 20;
																																																this.aiStyle = 19;
																																																this.friendly = true;
																																																this.penetrate = -1;
																																																this.tileCollide = false;
																																																this.scale = 1.1f;
																																																this.hide = true;
																																																this.ownerHitCheck = true;
																																																this.melee = true;
																																															}
																																															else
																																															{
																																																if (this.type == 47)
																																																{
																																																	this.name = "Trident";
																																																	this.width = 18;
																																																	this.height = 18;
																																																	this.aiStyle = 19;
																																																	this.friendly = true;
																																																	this.penetrate = -1;
																																																	this.tileCollide = false;
																																																	this.scale = 1.1f;
																																																	this.hide = true;
																																																	this.ownerHitCheck = true;
																																																	this.melee = true;
																																																}
																																																else
																																																{
																																																	if (this.type == 48)
																																																	{
																																																		this.name = "Throwing Knife";
																																																		this.width = 12;
																																																		this.height = 12;
																																																		this.aiStyle = 2;
																																																		this.friendly = true;
																																																		this.penetrate = 2;
																																																		this.ranged = true;
																																																	}
																																																	else
																																																	{
																																																		if (this.type == 49)
																																																		{
																																																			this.name = "Spear";
																																																			this.width = 18;
																																																			this.height = 18;
																																																			this.aiStyle = 19;
																																																			this.friendly = true;
																																																			this.penetrate = -1;
																																																			this.tileCollide = false;
																																																			this.scale = 1.2f;
																																																			this.hide = true;
																																																			this.ownerHitCheck = true;
																																																			this.melee = true;
																																																		}
																																																		else
																																																		{
																																																			if (this.type == 50)
																																																			{
																																																				this.name = "Glowstick";
																																																				this.width = 6;
																																																				this.height = 6;
																																																				this.aiStyle = 14;
																																																				this.penetrate = -1;
																																																				this.alpha = 75;
																																																				this.light = 1f;
																																																				this.timeLeft *= 5;
																																																			}
																																																			else
																																																			{
																																																				if (this.type == 51)
																																																				{
																																																					this.name = "Seed";
																																																					this.width = 8;
																																																					this.height = 8;
																																																					this.aiStyle = 1;
																																																					this.friendly = true;
																																																				}
																																																				else
																																																				{
																																																					if (this.type == 52)
																																																					{
																																																						this.name = "Wooden Boomerang";
																																																						this.width = 22;
																																																						this.height = 22;
																																																						this.aiStyle = 3;
																																																						this.friendly = true;
																																																						this.penetrate = -1;
																																																						this.ranged = true;
																																																					}
																																																					else
																																																					{
																																																						if (this.type == 53)
																																																						{
																																																							this.name = "Sticky Glowstick";
																																																							this.width = 6;
																																																							this.height = 6;
																																																							this.aiStyle = 14;
																																																							this.penetrate = -1;
																																																							this.alpha = 75;
																																																							this.light = 1f;
																																																							this.timeLeft *= 5;
																																																							this.tileCollide = false;
																																																						}
																																																						else
																																																						{
																																																							if (this.type == 54)
																																																							{
																																																								this.name = "Poisoned Knife";
																																																								this.width = 12;
																																																								this.height = 12;
																																																								this.aiStyle = 2;
																																																								this.friendly = true;
																																																								this.penetrate = 2;
																																																								this.ranged = true;
																																																							}
																																																							else
																																																							{
																																																								if (this.type == 55)
																																																								{
																																																									this.name = "Stinger";
																																																									this.width = 10;
																																																									this.height = 10;
																																																									this.aiStyle = 0;
																																																									this.hostile = true;
																																																									this.penetrate = -1;
																																																									this.aiStyle = 1;
																																																									this.tileCollide = true;
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
																																									}
																																								}
																																							}
																																						}
																																					}
																																				}
																																			}
																																		}
																																	}
																																}
																															}
																														}
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			this.width = (int)((float)this.width * this.scale);
			this.height = (int)((float)this.height * this.scale);
		}
		public static int NewProjectile(float X, float Y, float SpeedX, float SpeedY, int Type, int Damage, float KnockBack, int Owner = 255)
		{
			int num = 1000;
			for (int i = 0; i < 1000; i++)
			{
				if (!Main.projectile[i].active)
				{
					num = i;
					break;
				}
			}
			if (num == 1000)
			{
				return num;
			}
			Main.projectile[num].SetDefaults(Type);
			Main.projectile[num].position.X = X - (float)Main.projectile[num].width * 0.5f;
			Main.projectile[num].position.Y = Y - (float)Main.projectile[num].height * 0.5f;
			Main.projectile[num].owner = Owner;
			Main.projectile[num].velocity.X = SpeedX;
			Main.projectile[num].velocity.Y = SpeedY;
			Main.projectile[num].damage = Damage;
			Main.projectile[num].knockBack = KnockBack;
			Main.projectile[num].identity = num;
			Main.projectile[num].wet = Collision.WetCollision(Main.projectile[num].position, Main.projectile[num].width, Main.projectile[num].height);
			if (Main.netMode != 0 && Owner == Main.myPlayer)
			{
				NetMessage.SendData(27, -1, -1, "", num, 0f, 0f, 0f, 0);
			}
			if (Owner == Main.myPlayer)
			{
				if (Type == 28)
				{
					Main.projectile[num].timeLeft = 180;
				}
				if (Type == 29)
				{
					Main.projectile[num].timeLeft = 300;
				}
				if (Type == 30)
				{
					Main.projectile[num].timeLeft = 180;
				}
				if (Type == 37)
				{
					Main.projectile[num].timeLeft = 180;
				}
			}
			return num;
		}
		public void StatusNPC(int i)
		{
			if (this.type == 2)
			{
				if (Main.rand.Next(3) == 0)
				{
					Main.npc[i].AddBuff(24, 180, false);
					return;
				}
			}
			else
			{
				if (this.type == 15)
				{
					if (Main.rand.Next(2) == 0)
					{
						Main.npc[i].AddBuff(24, 300, false);
						return;
					}
				}
				else
				{
					if (this.type == 19)
					{
						if (Main.rand.Next(5) == 0)
						{
							Main.npc[i].AddBuff(24, 180, false);
							return;
						}
					}
					else
					{
						if (this.type == 33)
						{
							if (Main.rand.Next(5) == 0)
							{
								Main.npc[i].AddBuff(20, 420, false);
								return;
							}
						}
						else
						{
							if (this.type == 34)
							{
								if (Main.rand.Next(2) == 0)
								{
									Main.npc[i].AddBuff(24, 240, false);
									return;
								}
							}
							else
							{
								if (this.type == 35)
								{
									if (Main.rand.Next(4) == 0)
									{
										Main.npc[i].AddBuff(24, 180, false);
										return;
									}
								}
								else
								{
									if (this.type == 54 && Main.rand.Next(2) == 0)
									{
										Main.npc[i].AddBuff(20, 600, false);
									}
								}
							}
						}
					}
				}
			}
		}
		public void StatusPvP(int i)
		{
			if (this.type == 2)
			{
				if (Main.rand.Next(3) == 0)
				{
					Main.player[i].AddBuff(24, 180, false);
					return;
				}
			}
			else
			{
				if (this.type == 15)
				{
					if (Main.rand.Next(2) == 0)
					{
						Main.player[i].AddBuff(24, 300, false);
						return;
					}
				}
				else
				{
					if (this.type == 19)
					{
						if (Main.rand.Next(5) == 0)
						{
							Main.player[i].AddBuff(24, 180, false);
							return;
						}
					}
					else
					{
						if (this.type == 33)
						{
							if (Main.rand.Next(5) == 0)
							{
								Main.player[i].AddBuff(20, 420, false);
								return;
							}
						}
						else
						{
							if (this.type == 34)
							{
								if (Main.rand.Next(2) == 0)
								{
									Main.player[i].AddBuff(24, 240, false);
									return;
								}
							}
							else
							{
								if (this.type == 35)
								{
									if (Main.rand.Next(4) == 0)
									{
										Main.player[i].AddBuff(24, 180, false);
										return;
									}
								}
								else
								{
									if (this.type == 54 && Main.rand.Next(2) == 0)
									{
										Main.player[i].AddBuff(20, 600, false);
									}
								}
							}
						}
					}
				}
			}
		}
		public void StatusPlayer(int i)
		{
			if (this.type == 55 && Main.rand.Next(3) == 0)
			{
				Main.player[i].AddBuff(20, 600, true);
			}
			if (this.type == 44 && Main.rand.Next(3) == 0)
			{
				Main.player[i].AddBuff(22, 900, true);
			}
		}
		public void Damage()
		{
			Rectangle rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height);
			if (this.friendly && this.type != 18)
			{
				if (this.owner == Main.myPlayer)
				{
					if ((this.aiStyle == 16 || this.type == 41) && this.timeLeft <= 1)
					{
						int myPlayer = Main.myPlayer;
						if (Main.player[myPlayer].active && !Main.player[myPlayer].dead && !Main.player[myPlayer].immune && (!this.ownerHitCheck || Collision.CanHit(Main.player[this.owner].position, Main.player[this.owner].width, Main.player[this.owner].height, Main.player[myPlayer].position, Main.player[myPlayer].width, Main.player[myPlayer].height)))
						{
							Rectangle value = new Rectangle((int)Main.player[myPlayer].position.X, (int)Main.player[myPlayer].position.Y, Main.player[myPlayer].width, Main.player[myPlayer].height);
							if (rectangle.Intersects(value))
							{
								if (Main.player[myPlayer].position.X + (float)(Main.player[myPlayer].width / 2) < this.position.X + (float)(this.width / 2))
								{
									this.direction = -1;
								}
								else
								{
									this.direction = 1;
								}
								int num = Main.DamageVar((float)this.damage);
								this.StatusPlayer(myPlayer);
								Main.player[myPlayer].Hurt(num, this.direction, true, false, Player.getDeathMessage(this.owner, -1, this.whoAmI, -1), false);
								if (Main.netMode != 0)
								{
									NetMessage.SendData(26, -1, -1, Player.getDeathMessage(this.owner, -1, this.whoAmI, -1), myPlayer, (float)this.direction, (float)num, 1f, 0);
								}
							}
						}
					}
					int num2 = (int)(this.position.X / 16f);
					int num3 = (int)((this.position.X + (float)this.width) / 16f) + 1;
					int num4 = (int)(this.position.Y / 16f);
					int num5 = (int)((this.position.Y + (float)this.height) / 16f) + 1;
					if (num2 < 0)
					{
						num2 = 0;
					}
					if (num3 > Main.maxTilesX)
					{
						num3 = Main.maxTilesX;
					}
					if (num4 < 0)
					{
						num4 = 0;
					}
					if (num5 > Main.maxTilesY)
					{
						num5 = Main.maxTilesY;
					}
					for (int i = num2; i < num3; i++)
					{
						for (int j = num4; j < num5; j++)
						{
							if (Main.tile[i, j] != null && Main.tileCut[(int)Main.tile[i, j].type] && Main.tile[i, j + 1] != null && Main.tile[i, j + 1].type != 78)
							{
								WorldGen.KillTile(i, j, false, false, false);
								if (Main.netMode != 0)
								{
									NetMessage.SendData(17, -1, -1, "", 0, (float)i, (float)j, 0f, 0);
								}
							}
						}
					}
					if (this.damage > 0)
					{
						for (int k = 0; k < 1000; k++)
						{
							if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && (!Main.npc[k].friendly || (Main.npc[k].type == 22 && this.owner < 255 && Main.player[this.owner].killGuide)) && (this.owner < 0 || Main.npc[k].immune[this.owner] == 0))
							{
								bool flag = false;
								if (this.type == 11 && (Main.npc[k].type == 47 || Main.npc[k].type == 57))
								{
									flag = true;
								}
								else
								{
									if (this.type == 31 && Main.npc[k].type == 69)
									{
										flag = true;
									}
								}
								if (!flag && (Main.npc[k].noTileCollide || !this.ownerHitCheck || Collision.CanHit(Main.player[this.owner].position, Main.player[this.owner].width, Main.player[this.owner].height, Main.npc[k].position, Main.npc[k].width, Main.npc[k].height)))
								{
									Rectangle value2 = new Rectangle((int)Main.npc[k].position.X, (int)Main.npc[k].position.Y, Main.npc[k].width, Main.npc[k].height);
									if (rectangle.Intersects(value2))
									{
										if (this.aiStyle == 3)
										{
											if (this.ai[0] == 0f)
											{
												this.velocity.X = -this.velocity.X;
												this.velocity.Y = -this.velocity.Y;
												this.netUpdate = true;
											}
											this.ai[0] = 1f;
										}
										else
										{
											if (this.aiStyle == 16)
											{
												if (this.timeLeft > 3)
												{
													this.timeLeft = 3;
												}
												if (Main.npc[k].position.X + (float)(Main.npc[k].width / 2) < this.position.X + (float)(this.width / 2))
												{
													this.direction = -1;
												}
												else
												{
													this.direction = 1;
												}
											}
										}
										if (this.type == 41 && this.timeLeft > 1)
										{
											this.timeLeft = 1;
										}
										bool flag2 = false;
										if (this.melee && Main.rand.Next(1, 101) <= Main.player[this.owner].meleeCrit)
										{
											flag2 = true;
										}
										if (this.ranged && Main.rand.Next(1, 101) <= Main.player[this.owner].rangedCrit)
										{
											flag2 = true;
										}
										if (this.magic && Main.rand.Next(1, 101) <= Main.player[this.owner].magicCrit)
										{
											flag2 = true;
										}
										int num6 = Main.DamageVar((float)this.damage);
										this.StatusNPC(k);
										Main.npc[k].StrikeNPC(num6, this.knockBack, this.direction, flag2);
										if (Main.netMode != 0)
										{
											if (flag2)
											{
												NetMessage.SendData(28, -1, -1, "", k, (float)num6, this.knockBack, (float)this.direction, 1);
											}
											else
											{
												NetMessage.SendData(28, -1, -1, "", k, (float)num6, this.knockBack, (float)this.direction, 0);
											}
										}
										if (this.penetrate != 1)
										{
											Main.npc[k].immune[this.owner] = 10;
										}
										if (this.penetrate > 0)
										{
											this.penetrate--;
											if (this.penetrate == 0)
											{
												break;
											}
										}
										if (this.aiStyle == 7)
										{
											this.ai[0] = 1f;
											this.damage = 0;
											this.netUpdate = true;
										}
										else
										{
											if (this.aiStyle == 13)
											{
												this.ai[0] = 1f;
												this.netUpdate = true;
											}
										}
									}
								}
							}
						}
					}
					if (this.damage > 0 && Main.player[Main.myPlayer].hostile)
					{
						for (int l = 0; l < 255; l++)
						{
							if (l != this.owner && Main.player[l].active && !Main.player[l].dead && !Main.player[l].immune && Main.player[l].hostile && this.playerImmune[l] <= 0 && (Main.player[Main.myPlayer].team == 0 || Main.player[Main.myPlayer].team != Main.player[l].team) && (!this.ownerHitCheck || Collision.CanHit(Main.player[this.owner].position, Main.player[this.owner].width, Main.player[this.owner].height, Main.player[l].position, Main.player[l].width, Main.player[l].height)))
							{
								Rectangle value3 = new Rectangle((int)Main.player[l].position.X, (int)Main.player[l].position.Y, Main.player[l].width, Main.player[l].height);
								if (rectangle.Intersects(value3))
								{
									if (this.aiStyle == 3)
									{
										if (this.ai[0] == 0f)
										{
											this.velocity.X = -this.velocity.X;
											this.velocity.Y = -this.velocity.Y;
											this.netUpdate = true;
										}
										this.ai[0] = 1f;
									}
									else
									{
										if (this.aiStyle == 16)
										{
											if (this.timeLeft > 3)
											{
												this.timeLeft = 3;
											}
											if (Main.player[l].position.X + (float)(Main.player[l].width / 2) < this.position.X + (float)(this.width / 2))
											{
												this.direction = -1;
											}
											else
											{
												this.direction = 1;
											}
										}
									}
									if (this.type == 41 && this.timeLeft > 1)
									{
										this.timeLeft = 1;
									}
									bool flag3 = false;
									if (this.melee && Main.rand.Next(1, 101) <= Main.player[this.owner].meleeCrit)
									{
										flag3 = true;
									}
									int num7 = Main.DamageVar((float)this.damage);
									if (!Main.player[l].immune)
									{
										this.StatusPvP(l);
									}
									Main.player[l].Hurt(num7, this.direction, true, false, Player.getDeathMessage(this.owner, -1, this.whoAmI, -1), flag3);
									if (Main.netMode != 0)
									{
										if (flag3)
										{
											NetMessage.SendData(26, -1, -1, Player.getDeathMessage(this.owner, -1, this.whoAmI, -1), l, (float)this.direction, (float)num7, 1f, 1);
										}
										else
										{
											NetMessage.SendData(26, -1, -1, Player.getDeathMessage(this.owner, -1, this.whoAmI, -1), l, (float)this.direction, (float)num7, 1f, 0);
										}
									}
									this.playerImmune[l] = 40;
									if (this.penetrate > 0)
									{
										this.penetrate--;
										if (this.penetrate == 0)
										{
											break;
										}
									}
									if (this.aiStyle == 7)
									{
										this.ai[0] = 1f;
										this.damage = 0;
										this.netUpdate = true;
									}
									else
									{
										if (this.aiStyle == 13)
										{
											this.ai[0] = 1f;
											this.netUpdate = true;
										}
									}
								}
							}
						}
					}
				}
				if (this.type == 11 && Main.netMode != 1)
				{
					for (int m = 0; m < 1000; m++)
					{
						if (Main.npc[m].active)
						{
							if (Main.npc[m].type == 46)
							{
								Rectangle value4 = new Rectangle((int)Main.npc[m].position.X, (int)Main.npc[m].position.Y, Main.npc[m].width, Main.npc[m].height);
								if (rectangle.Intersects(value4))
								{
									Main.npc[m].Transform(47);
								}
							}
							else
							{
								if (Main.npc[m].type == 55)
								{
									Rectangle value5 = new Rectangle((int)Main.npc[m].position.X, (int)Main.npc[m].position.Y, Main.npc[m].width, Main.npc[m].height);
									if (rectangle.Intersects(value5))
									{
										Main.npc[m].Transform(57);
									}
								}
							}
						}
					}
				}
			}
			if (Main.netMode != 2 && this.hostile && Main.myPlayer < 255 && this.damage > 0)
			{
				int myPlayer2 = Main.myPlayer;
				if (Main.player[myPlayer2].active && !Main.player[myPlayer2].dead && !Main.player[myPlayer2].immune)
				{
					Rectangle value6 = new Rectangle((int)Main.player[myPlayer2].position.X, (int)Main.player[myPlayer2].position.Y, Main.player[myPlayer2].width, Main.player[myPlayer2].height);
					if (rectangle.Intersects(value6))
					{
						int hitDirection = this.direction;
						if (Main.player[myPlayer2].position.X + (float)(Main.player[myPlayer2].width / 2) < this.position.X + (float)(this.width / 2))
						{
							hitDirection = -1;
						}
						else
						{
							hitDirection = 1;
						}
						int num8 = Main.DamageVar((float)this.damage);
						if (!Main.player[myPlayer2].immune)
						{
							this.StatusPlayer(myPlayer2);
						}
						Main.player[myPlayer2].Hurt(num8 * 2, hitDirection, false, false, " was slain...", false);
					}
				}
			}
		}
		public void Update(int i)
		{
			if (this.active)
			{
				Vector2 value = this.velocity;
				if (this.position.X <= Main.leftWorld || this.position.X + (float)this.width >= Main.rightWorld || this.position.Y <= Main.topWorld || this.position.Y + (float)this.height >= Main.bottomWorld)
				{
					this.active = false;
					return;
				}
				this.whoAmI = i;
				if (this.soundDelay > 0)
				{
					this.soundDelay--;
				}
				this.netUpdate = false;
				for (int j = 0; j < 255; j++)
				{
					if (this.playerImmune[j] > 0)
					{
						this.playerImmune[j]--;
					}
				}
				this.AI();
				if (this.owner < 255 && !Main.player[this.owner].active)
				{
					this.Kill();
				}
				if (!this.ignoreWater)
				{
					bool flag;
					bool flag2;
					try
					{
						flag = Collision.LavaCollision(this.position, this.width, this.height);
						flag2 = Collision.WetCollision(this.position, this.width, this.height);
						if (flag)
						{
							this.lavaWet = true;
						}
					}
					catch
					{
						this.active = false;
						return;
					}
					if (flag2)
					{
						if (this.wetCount == 0)
						{
							this.wetCount = 10;
							if (!this.wet)
							{
								if (!flag)
								{
									for (int k = 0; k < 10; k++)
									{
										int num = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f), this.width + 12, 24, 33, 0f, 0f, 0, default(Color), 1f);
										Dust expr_1E9_cp_0 = Main.dust[num];
										expr_1E9_cp_0.velocity.Y = expr_1E9_cp_0.velocity.Y - 4f;
										Dust expr_207_cp_0 = Main.dust[num];
										expr_207_cp_0.velocity.X = expr_207_cp_0.velocity.X * 2.5f;
										Main.dust[num].scale = 1.3f;
										Main.dust[num].alpha = 100;
										Main.dust[num].noGravity = true;
									}
									Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1);
								}
								else
								{
									for (int l = 0; l < 10; l++)
									{
										int num2 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f), this.width + 12, 24, 35, 0f, 0f, 0, default(Color), 1f);
										Dust expr_2EF_cp_0 = Main.dust[num2];
										expr_2EF_cp_0.velocity.Y = expr_2EF_cp_0.velocity.Y - 1.5f;
										Dust expr_30D_cp_0 = Main.dust[num2];
										expr_30D_cp_0.velocity.X = expr_30D_cp_0.velocity.X * 2.5f;
										Main.dust[num2].scale = 1.3f;
										Main.dust[num2].alpha = 100;
										Main.dust[num2].noGravity = true;
									}
									Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1);
								}
							}
							this.wet = true;
						}
					}
					else
					{
						if (this.wet)
						{
							this.wet = false;
							if (this.wetCount == 0)
							{
								this.wetCount = 10;
								if (!this.lavaWet)
								{
									for (int m = 0; m < 10; m++)
									{
										int num3 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2)), this.width + 12, 24, 33, 0f, 0f, 0, default(Color), 1f);
										Dust expr_426_cp_0 = Main.dust[num3];
										expr_426_cp_0.velocity.Y = expr_426_cp_0.velocity.Y - 4f;
										Dust expr_444_cp_0 = Main.dust[num3];
										expr_444_cp_0.velocity.X = expr_444_cp_0.velocity.X * 2.5f;
										Main.dust[num3].scale = 1.3f;
										Main.dust[num3].alpha = 100;
										Main.dust[num3].noGravity = true;
									}
									Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1);
								}
								else
								{
									for (int n = 0; n < 10; n++)
									{
										int num4 = Dust.NewDust(new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f), this.width + 12, 24, 35, 0f, 0f, 0, default(Color), 1f);
										Dust expr_52C_cp_0 = Main.dust[num4];
										expr_52C_cp_0.velocity.Y = expr_52C_cp_0.velocity.Y - 1.5f;
										Dust expr_54A_cp_0 = Main.dust[num4];
										expr_54A_cp_0.velocity.X = expr_54A_cp_0.velocity.X * 2.5f;
										Main.dust[num4].scale = 1.3f;
										Main.dust[num4].alpha = 100;
										Main.dust[num4].noGravity = true;
									}
									Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1);
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
				}
				if (this.tileCollide)
				{
					Vector2 value2 = this.velocity;
					bool flag3 = true;
					if (this.type == 9 || this.type == 12 || this.type == 15 || this.type == 13 || this.type == 31 || this.type == 39 || this.type == 40)
					{
						flag3 = false;
					}
					if (this.aiStyle == 10)
					{
						if (this.type == 42 || (this.type == 31 && this.ai[0] == 2f))
						{
							this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, flag3, flag3);
						}
						else
						{
							this.velocity = Collision.AnyCollision(this.position, this.velocity, this.width, this.height);
						}
					}
					else
					{
						if (this.aiStyle == 18)
						{
							int num5 = this.width - 36;
							int num6 = this.height - 36;
							Vector2 vector = new Vector2(this.position.X + (float)(this.width / 2) - (float)(num5 / 2), this.position.Y + (float)(this.height / 2) - (float)(num6 / 2));
							this.velocity = Collision.TileCollision(vector, this.velocity, num5, num6, flag3, flag3);
						}
						else
						{
							if (this.wet)
							{
								Vector2 vector2 = this.velocity;
								this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, flag3, flag3);
								value = this.velocity * 0.5f;
								if (this.velocity.X != vector2.X)
								{
									value.X = this.velocity.X;
								}
								if (this.velocity.Y != vector2.Y)
								{
									value.Y = this.velocity.Y;
								}
							}
							else
							{
								this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, flag3, flag3);
							}
						}
					}
					if (value2 != this.velocity)
					{
						if (this.type == 36)
						{
							if (this.penetrate > 1)
							{
								Collision.HitTiles(this.position, this.velocity, this.width, this.height);
								Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 10);
								this.penetrate--;
								if (this.velocity.X != value2.X)
								{
									this.velocity.X = -value2.X;
								}
								if (this.velocity.Y != value2.Y)
								{
									this.velocity.Y = -value2.Y;
								}
							}
							else
							{
								this.Kill();
							}
						}
						else
						{
							if (this.aiStyle == 17)
							{
								if (this.velocity.X != value2.X)
								{
									this.velocity.X = value2.X * -0.75f;
								}
								if (this.velocity.Y != value2.Y && (double)value2.Y > 1.5)
								{
									this.velocity.Y = value2.Y * -0.7f;
								}
							}
							else
							{
								if (this.aiStyle == 15)
								{
									bool flag4 = false;
									if (value2.X != this.velocity.X)
									{
										if (Math.Abs(value2.X) > 4f)
										{
											flag4 = true;
										}
										this.position.X = this.position.X + this.velocity.X;
										this.velocity.X = -value2.X * 0.2f;
									}
									if (value2.Y != this.velocity.Y)
									{
										if (Math.Abs(value2.Y) > 4f)
										{
											flag4 = true;
										}
										this.position.Y = this.position.Y + this.velocity.Y;
										this.velocity.Y = -value2.Y * 0.2f;
									}
									this.ai[0] = 1f;
									if (flag4)
									{
										this.netUpdate = true;
										Collision.HitTiles(this.position, this.velocity, this.width, this.height);
										Main.PlaySound(0, (int)this.position.X, (int)this.position.Y, 1);
									}
								}
								else
								{
									if (this.aiStyle == 3 || this.aiStyle == 13)
									{
										Collision.HitTiles(this.position, this.velocity, this.width, this.height);
										if (this.type == 33)
										{
											if (this.velocity.X != value2.X)
											{
												this.velocity.X = -value2.X;
											}
											if (this.velocity.Y != value2.Y)
											{
												this.velocity.Y = -value2.Y;
											}
										}
										else
										{
											this.ai[0] = 1f;
											if (this.aiStyle == 3)
											{
												this.velocity.X = -value2.X;
												this.velocity.Y = -value2.Y;
											}
										}
										this.netUpdate = true;
										Main.PlaySound(0, (int)this.position.X, (int)this.position.Y, 1);
									}
									else
									{
										if (this.aiStyle == 8)
										{
											Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 10);
											this.ai[0] += 1f;
											if (this.ai[0] >= 5f)
											{
												this.position += this.velocity;
												this.Kill();
											}
											else
											{
												if (this.type == 15 && this.velocity.Y > 4f)
												{
													if (this.velocity.Y != value2.Y)
													{
														this.velocity.Y = -value2.Y * 0.8f;
													}
												}
												else
												{
													if (this.velocity.Y != value2.Y)
													{
														this.velocity.Y = -value2.Y;
													}
												}
												if (this.velocity.X != value2.X)
												{
													this.velocity.X = -value2.X;
												}
											}
										}
										else
										{
											if (this.aiStyle == 14)
											{
												if (this.type == 50)
												{
													if (this.velocity.X != value2.X)
													{
														this.velocity.X = value2.X * -0.2f;
													}
													if (this.velocity.Y != value2.Y && (double)value2.Y > 1.5)
													{
														this.velocity.Y = value2.Y * -0.2f;
													}
												}
												else
												{
													if (this.velocity.X != value2.X)
													{
														this.velocity.X = value2.X * -0.5f;
													}
													if (this.velocity.Y != value2.Y && value2.Y > 1f)
													{
														this.velocity.Y = value2.Y * -0.5f;
													}
												}
											}
											else
											{
												if (this.aiStyle == 16)
												{
													if (this.velocity.X != value2.X)
													{
														this.velocity.X = value2.X * -0.4f;
														if (this.type == 29)
														{
															this.velocity.X = this.velocity.X * 0.8f;
														}
													}
													if (this.velocity.Y != value2.Y && (double)value2.Y > 0.7)
													{
														this.velocity.Y = value2.Y * -0.4f;
														if (this.type == 29)
														{
															this.velocity.Y = this.velocity.Y * 0.8f;
														}
													}
												}
												else
												{
													this.position += this.velocity;
													this.Kill();
												}
											}
										}
									}
								}
							}
						}
					}
				}
				if (this.type == 7 || this.type == 8)
				{
					goto IL_EB5;
				}
				if (this.wet)
				{
					this.position += value;
					goto IL_EB5;
				}
				this.position += this.velocity;
				IL_EB5:
				if ((this.aiStyle != 3 || this.ai[0] != 1f) && (this.aiStyle != 7 || this.ai[0] != 1f) && (this.aiStyle != 13 || this.ai[0] != 1f) && (this.aiStyle != 15 || this.ai[0] != 1f) && this.aiStyle != 15)
				{
					if (this.velocity.X < 0f)
					{
						this.direction = -1;
					}
					else
					{
						this.direction = 1;
					}
				}
				if (!this.active)
				{
					return;
				}
				if (this.light > 0f)
				{
					Lighting.addLight((int)((this.position.X + (float)(this.width / 2)) / 16f), (int)((this.position.Y + (float)(this.height / 2)) / 16f), this.light);
				}
				if (this.type == 2)
				{
					Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 6, 0f, 0f, 100, default(Color), 1f);
				}
				else
				{
					if (this.type == 4)
					{
						if (Main.rand.Next(5) == 0)
						{
							Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 14, 0f, 0f, 150, default(Color), 1.1f);
						}
					}
					else
					{
						if (this.type == 5)
						{
							Dust.NewDust(this.position, this.width, this.height, 15, this.velocity.X * 0.5f, this.velocity.Y * 0.5f, 150, default(Color), 1.2f);
						}
					}
				}
				this.Damage();
				this.timeLeft--;
				if (this.timeLeft <= 0)
				{
					this.Kill();
				}
				if (this.penetrate == 0)
				{
					this.Kill();
				}
				if (this.active && this.netUpdate && this.owner == Main.myPlayer)
				{
					NetMessage.SendData(27, -1, -1, "", i, 0f, 0f, 0f, 0);
				}
				if (this.active && this.maxUpdates > 0)
				{
					this.numUpdates--;
					if (this.numUpdates >= 0)
					{
						this.Update(i);
					}
					else
					{
						this.numUpdates = this.maxUpdates;
					}
				}
				this.netUpdate = false;
			}
		}
		public void AI()
		{
			if (this.aiStyle == 1)
			{
				if (this.type == 41)
				{
					Vector2 arg_5D_0 = new Vector2(this.position.X, this.position.Y);
					int arg_5D_1 = this.width;
					int arg_5D_2 = this.height;
					int arg_5D_3 = 31;
					float arg_5D_4 = 0f;
					float arg_5D_5 = 0f;
					int arg_5D_6 = 100;
					Color newColor = default(Color);
					int num = Dust.NewDust(arg_5D_0, arg_5D_1, arg_5D_2, arg_5D_3, arg_5D_4, arg_5D_5, arg_5D_6, newColor, 1.6f);
					Main.dust[num].noGravity = true;
					Vector2 arg_B3_0 = new Vector2(this.position.X, this.position.Y);
					int arg_B3_1 = this.width;
					int arg_B3_2 = this.height;
					int arg_B3_3 = 6;
					float arg_B3_4 = 0f;
					float arg_B3_5 = 0f;
					int arg_B3_6 = 100;
					newColor = default(Color);
					num = Dust.NewDust(arg_B3_0, arg_B3_1, arg_B3_2, arg_B3_3, arg_B3_4, arg_B3_5, arg_B3_6, newColor, 2f);
					Main.dust[num].noGravity = true;
				}
				else
				{
					if (this.type == 55)
					{
						Vector2 arg_115_0 = new Vector2(this.position.X, this.position.Y);
						int arg_115_1 = this.width;
						int arg_115_2 = this.height;
						int arg_115_3 = 18;
						float arg_115_4 = 0f;
						float arg_115_5 = 0f;
						int arg_115_6 = 0;
						Color newColor = default(Color);
						int num2 = Dust.NewDust(arg_115_0, arg_115_1, arg_115_2, arg_115_3, arg_115_4, arg_115_5, arg_115_6, newColor, 0.9f);
						Main.dust[num2].noGravity = true;
					}
				}
				if (this.type == 20 || this.type == 14 || this.type == 36)
				{
					if (this.alpha > 0)
					{
						this.alpha -= 15;
					}
					if (this.alpha < 0)
					{
						this.alpha = 0;
					}
				}
				if (this.type != 5 && this.type != 14 && this.type != 20 && this.type != 36 && this.type != 38 && this.type != 55)
				{
					this.ai[0] += 1f;
				}
				if (this.ai[0] >= 15f)
				{
					this.ai[0] = 15f;
					this.velocity.Y = this.velocity.Y + 0.1f;
				}
				this.rotation = (float)Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) + 1.57f;
				if (this.velocity.Y > 16f)
				{
					this.velocity.Y = 16f;
					return;
				}
			}
			else
			{
				if (this.aiStyle == 2)
				{
					this.rotation += (Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y)) * 0.03f * (float)this.direction;
					this.ai[0] += 1f;
					if (this.ai[0] >= 20f)
					{
						this.velocity.Y = this.velocity.Y + 0.4f;
						this.velocity.X = this.velocity.X * 0.97f;
					}
					else
					{
						if (this.type == 48 || this.type == 54)
						{
							this.rotation = (float)Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) + 1.57f;
						}
					}
					if (this.velocity.Y > 16f)
					{
						this.velocity.Y = 16f;
					}
					if (this.type == 54 && Main.rand.Next(20) == 0)
					{
						Vector2 arg_3C6_0 = new Vector2(this.position.X, this.position.Y);
						int arg_3C6_1 = this.width;
						int arg_3C6_2 = this.height;
						int arg_3C6_3 = 40;
						float arg_3C6_4 = this.velocity.X * 0.1f;
						float arg_3C6_5 = this.velocity.Y * 0.1f;
						int arg_3C6_6 = 0;
						Color newColor = default(Color);
						Dust.NewDust(arg_3C6_0, arg_3C6_1, arg_3C6_2, arg_3C6_3, arg_3C6_4, arg_3C6_5, arg_3C6_6, newColor, 0.75f);
						return;
					}
				}
				else
				{
					if (this.aiStyle == 3)
					{
						if (this.soundDelay == 0)
						{
							this.soundDelay = 8;
							Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 7);
						}
						if (this.type == 19)
						{
							for (int i = 0; i < 2; i++)
							{
								Vector2 arg_476_0 = new Vector2(this.position.X, this.position.Y);
								int arg_476_1 = this.width;
								int arg_476_2 = this.height;
								int arg_476_3 = 6;
								float arg_476_4 = this.velocity.X * 0.2f;
								float arg_476_5 = this.velocity.Y * 0.2f;
								int arg_476_6 = 100;
								Color newColor = default(Color);
								int num3 = Dust.NewDust(arg_476_0, arg_476_1, arg_476_2, arg_476_3, arg_476_4, arg_476_5, arg_476_6, newColor, 2f);
								Main.dust[num3].noGravity = true;
								Dust expr_495_cp_0 = Main.dust[num3];
								expr_495_cp_0.velocity.X = expr_495_cp_0.velocity.X * 0.3f;
								Dust expr_4B2_cp_0 = Main.dust[num3];
								expr_4B2_cp_0.velocity.Y = expr_4B2_cp_0.velocity.Y * 0.3f;
							}
						}
						else
						{
							if (this.type == 33)
							{
								if (Main.rand.Next(1) == 0)
								{
									Vector2 arg_533_0 = this.position;
									int arg_533_1 = this.width;
									int arg_533_2 = this.height;
									int arg_533_3 = 40;
									float arg_533_4 = this.velocity.X * 0.25f;
									float arg_533_5 = this.velocity.Y * 0.25f;
									int arg_533_6 = 0;
									Color newColor = default(Color);
									int num4 = Dust.NewDust(arg_533_0, arg_533_1, arg_533_2, arg_533_3, arg_533_4, arg_533_5, arg_533_6, newColor, 1.4f);
									Main.dust[num4].noGravity = true;
								}
							}
							else
							{
								if (this.type == 6 && Main.rand.Next(5) == 0)
								{
									Vector2 arg_5AA_0 = this.position;
									int arg_5AA_1 = this.width;
									int arg_5AA_2 = this.height;
									int arg_5AA_3 = 15;
									float arg_5AA_4 = this.velocity.X * 0.5f;
									float arg_5AA_5 = this.velocity.Y * 0.5f;
									int arg_5AA_6 = 150;
									Color newColor = default(Color);
									Dust.NewDust(arg_5AA_0, arg_5AA_1, arg_5AA_2, arg_5AA_3, arg_5AA_4, arg_5AA_5, arg_5AA_6, newColor, 0.9f);
								}
							}
						}
						if (this.ai[0] == 0f)
						{
							this.ai[1] += 1f;
							if (this.ai[1] >= 30f)
							{
								this.ai[0] = 1f;
								this.ai[1] = 0f;
								this.netUpdate = true;
							}
						}
						else
						{
							this.tileCollide = false;
							float num5 = 9f;
							float num6 = 0.4f;
							if (this.type == 19)
							{
								num5 = 13f;
								num6 = 0.6f;
							}
							else
							{
								if (this.type == 33)
								{
									num5 = 15f;
									num6 = 0.8f;
								}
							}
							Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
							float num7 = Main.player[this.owner].position.X + (float)(Main.player[this.owner].width / 2) - vector.X;
							float num8 = Main.player[this.owner].position.Y + (float)(Main.player[this.owner].height / 2) - vector.Y;
							float num9 = (float)Math.Sqrt((double)(num7 * num7 + num8 * num8));
							if (num9 > 3000f)
							{
								this.Kill();
							}
							num9 = num5 / num9;
							num7 *= num9;
							num8 *= num9;
							if (this.velocity.X < num7)
							{
								this.velocity.X = this.velocity.X + num6;
								if (this.velocity.X < 0f && num7 > 0f)
								{
									this.velocity.X = this.velocity.X + num6;
								}
							}
							else
							{
								if (this.velocity.X > num7)
								{
									this.velocity.X = this.velocity.X - num6;
									if (this.velocity.X > 0f && num7 < 0f)
									{
										this.velocity.X = this.velocity.X - num6;
									}
								}
							}
							if (this.velocity.Y < num8)
							{
								this.velocity.Y = this.velocity.Y + num6;
								if (this.velocity.Y < 0f && num8 > 0f)
								{
									this.velocity.Y = this.velocity.Y + num6;
								}
							}
							else
							{
								if (this.velocity.Y > num8)
								{
									this.velocity.Y = this.velocity.Y - num6;
									if (this.velocity.Y > 0f && num8 < 0f)
									{
										this.velocity.Y = this.velocity.Y - num6;
									}
								}
							}
							if (Main.myPlayer == this.owner)
							{
								Rectangle rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height);
								Rectangle value = new Rectangle((int)Main.player[this.owner].position.X, (int)Main.player[this.owner].position.Y, Main.player[this.owner].width, Main.player[this.owner].height);
								if (rectangle.Intersects(value))
								{
									this.Kill();
								}
							}
						}
						this.rotation += 0.4f * (float)this.direction;
						return;
					}
					if (this.aiStyle == 4)
					{
						this.rotation = (float)Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) + 1.57f;
						if (this.ai[0] == 0f)
						{
							this.alpha -= 50;
							if (this.alpha <= 0)
							{
								this.alpha = 0;
								this.ai[0] = 1f;
								if (this.ai[1] == 0f)
								{
									this.ai[1] += 1f;
									this.position += this.velocity * 1f;
								}
								if (this.type == 7 && Main.myPlayer == this.owner)
								{
									int num10 = this.type;
									if (this.ai[1] >= 6f)
									{
										num10++;
									}
									int num11 = Projectile.NewProjectile(this.position.X + this.velocity.X + (float)(this.width / 2), this.position.Y + this.velocity.Y + (float)(this.height / 2), this.velocity.X, this.velocity.Y, num10, this.damage, this.knockBack, this.owner);
									Main.projectile[num11].damage = this.damage;
									Main.projectile[num11].ai[1] = this.ai[1] + 1f;
									NetMessage.SendData(27, -1, -1, "", num11, 0f, 0f, 0f, 0);
									return;
								}
							}
						}
						else
						{
							if (this.alpha < 170 && this.alpha + 5 >= 170)
							{
								Color newColor;
								for (int j = 0; j < 3; j++)
								{
									Vector2 arg_B72_0 = this.position;
									int arg_B72_1 = this.width;
									int arg_B72_2 = this.height;
									int arg_B72_3 = 18;
									float arg_B72_4 = this.velocity.X * 0.025f;
									float arg_B72_5 = this.velocity.Y * 0.025f;
									int arg_B72_6 = 170;
									newColor = default(Color);
									Dust.NewDust(arg_B72_0, arg_B72_1, arg_B72_2, arg_B72_3, arg_B72_4, arg_B72_5, arg_B72_6, newColor, 1.2f);
								}
								Vector2 arg_BB5_0 = this.position;
								int arg_BB5_1 = this.width;
								int arg_BB5_2 = this.height;
								int arg_BB5_3 = 14;
								float arg_BB5_4 = 0f;
								float arg_BB5_5 = 0f;
								int arg_BB5_6 = 170;
								newColor = default(Color);
								Dust.NewDust(arg_BB5_0, arg_BB5_1, arg_BB5_2, arg_BB5_3, arg_BB5_4, arg_BB5_5, arg_BB5_6, newColor, 1.1f);
							}
							this.alpha += 5;
							if (this.alpha >= 255)
							{
								this.Kill();
								return;
							}
						}
					}
					else
					{
						if (this.aiStyle == 5)
						{
							if (this.ai[1] == 0f && !Collision.SolidCollision(this.position, this.width, this.height))
							{
								this.ai[1] = 1f;
								this.netUpdate = true;
							}
							if (this.ai[1] != 0f)
							{
								this.tileCollide = true;
							}
							if (this.soundDelay == 0)
							{
								this.soundDelay = 20 + Main.rand.Next(40);
								Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 9);
							}
							if (this.ai[0] == 0f)
							{
								this.ai[0] = 1f;
							}
							this.alpha += (int)(25f * this.ai[0]);
							if (this.alpha > 200)
							{
								this.alpha = 200;
								this.ai[0] = -1f;
							}
							if (this.alpha < 0)
							{
								this.alpha = 0;
								this.ai[0] = 1f;
							}
							this.rotation += (Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y)) * 0.01f * (float)this.direction;
							if (this.ai[1] == 1f)
							{
								this.light = 0.9f;
								if (Main.rand.Next(10) == 0)
								{
									Vector2 arg_DA6_0 = this.position;
									int arg_DA6_1 = this.width;
									int arg_DA6_2 = this.height;
									int arg_DA6_3 = 15;
									float arg_DA6_4 = this.velocity.X * 0.5f;
									float arg_DA6_5 = this.velocity.Y * 0.5f;
									int arg_DA6_6 = 150;
									Color newColor = default(Color);
									Dust.NewDust(arg_DA6_0, arg_DA6_1, arg_DA6_2, arg_DA6_3, arg_DA6_4, arg_DA6_5, arg_DA6_6, newColor, 1.2f);
								}
								if (Main.rand.Next(20) == 0)
								{
									Gore.NewGore(this.position, new Vector2(this.velocity.X * 0.2f, this.velocity.Y * 0.2f), Main.rand.Next(16, 18), 1f);
									return;
								}
							}
						}
						else
						{
							if (this.aiStyle == 6)
							{
								this.velocity *= 0.95f;
								this.ai[0] += 1f;
								if (this.ai[0] == 180f)
								{
									this.Kill();
								}
								if (this.ai[1] == 0f)
								{
									this.ai[1] = 1f;
									for (int k = 0; k < 30; k++)
									{
										Vector2 arg_EBB_0 = this.position;
										int arg_EBB_1 = this.width;
										int arg_EBB_2 = this.height;
										int arg_EBB_3 = 10 + this.type;
										float arg_EBB_4 = this.velocity.X;
										float arg_EBB_5 = this.velocity.Y;
										int arg_EBB_6 = 50;
										Color newColor = default(Color);
										Dust.NewDust(arg_EBB_0, arg_EBB_1, arg_EBB_2, arg_EBB_3, arg_EBB_4, arg_EBB_5, arg_EBB_6, newColor, 1f);
									}
								}
								if (this.type == 10)
								{
									int num12 = (int)(this.position.X / 16f) - 1;
									int num13 = (int)((this.position.X + (float)this.width) / 16f) + 2;
									int num14 = (int)(this.position.Y / 16f) - 1;
									int num15 = (int)((this.position.Y + (float)this.height) / 16f) + 2;
									if (num12 < 0)
									{
										num12 = 0;
									}
									if (num13 > Main.maxTilesX)
									{
										num13 = Main.maxTilesX;
									}
									if (num14 < 0)
									{
										num14 = 0;
									}
									if (num15 > Main.maxTilesY)
									{
										num15 = Main.maxTilesY;
									}
									for (int l = num12; l < num13; l++)
									{
										for (int m = num14; m < num15; m++)
										{
											Vector2 vector2;
											vector2.X = (float)(l * 16);
											vector2.Y = (float)(m * 16);
											if (this.position.X + (float)this.width > vector2.X && this.position.X < vector2.X + 16f && this.position.Y + (float)this.height > vector2.Y && this.position.Y < vector2.Y + 16f && Main.myPlayer == this.owner && Main.tile[l, m].active)
											{
												if (Main.tile[l, m].type == 23)
												{
													Main.tile[l, m].type = 2;
													WorldGen.SquareTileFrame(l, m, true);
													if (Main.netMode == 1)
													{
														NetMessage.SendTileSquare(-1, l - 1, m - 1, 3);
													}
												}
												if (Main.tile[l, m].type == 25)
												{
													Main.tile[l, m].type = 1;
													WorldGen.SquareTileFrame(l, m, true);
													if (Main.netMode == 1)
													{
														NetMessage.SendTileSquare(-1, l - 1, m - 1, 3);
													}
												}
											}
										}
									}
									return;
								}
							}
							else
							{
								if (this.aiStyle == 7)
								{
									if (Main.player[this.owner].dead)
									{
										this.Kill();
										return;
									}
									Vector2 vector3 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
									float num16 = Main.player[this.owner].position.X + (float)(Main.player[this.owner].width / 2) - vector3.X;
									float num17 = Main.player[this.owner].position.Y + (float)(Main.player[this.owner].height / 2) - vector3.Y;
									float num18 = (float)Math.Sqrt((double)(num16 * num16 + num17 * num17));
									this.rotation = (float)Math.Atan2((double)num17, (double)num16) - 1.57f;
									if (this.ai[0] == 0f)
									{
										if ((num18 > 300f && this.type == 13) || (num18 > 400f && this.type == 32))
										{
											this.ai[0] = 1f;
										}
										int num19 = (int)(this.position.X / 16f) - 1;
										int num20 = (int)((this.position.X + (float)this.width) / 16f) + 2;
										int num21 = (int)(this.position.Y / 16f) - 1;
										int num22 = (int)((this.position.Y + (float)this.height) / 16f) + 2;
										if (num19 < 0)
										{
											num19 = 0;
										}
										if (num20 > Main.maxTilesX)
										{
											num20 = Main.maxTilesX;
										}
										if (num21 < 0)
										{
											num21 = 0;
										}
										if (num22 > Main.maxTilesY)
										{
											num22 = Main.maxTilesY;
										}
										for (int n = num19; n < num20; n++)
										{
											int num23 = num21;
											while (num23 < num22)
											{
												if (Main.tile[n, num23] == null)
												{
													Main.tile[n, num23] = new Tile();
												}
												Vector2 vector4;
												vector4.X = (float)(n * 16);
												vector4.Y = (float)(num23 * 16);
												if (this.position.X + (float)this.width > vector4.X && this.position.X < vector4.X + 16f && this.position.Y + (float)this.height > vector4.Y && this.position.Y < vector4.Y + 16f && Main.tile[n, num23].active && Main.tileSolid[(int)Main.tile[n, num23].type])
												{
													if (Main.player[this.owner].grapCount < 10)
													{
														Main.player[this.owner].grappling[Main.player[this.owner].grapCount] = this.whoAmI;
														Main.player[this.owner].grapCount++;
													}
													if (Main.myPlayer == this.owner)
													{
														int num24 = 0;
														int num25 = -1;
														int num26 = 100000;
														for (int num27 = 0; num27 < 1000; num27++)
														{
															if (Main.projectile[num27].active && Main.projectile[num27].owner == this.owner && Main.projectile[num27].aiStyle == 7)
															{
																if (Main.projectile[num27].timeLeft < num26)
																{
																	num25 = num27;
																	num26 = Main.projectile[num27].timeLeft;
																}
																num24++;
															}
														}
														if (num24 > 3)
														{
															Main.projectile[num25].Kill();
														}
													}
													WorldGen.KillTile(n, num23, true, true, false);
													Main.PlaySound(0, n * 16, num23 * 16, 1);
													this.velocity.X = 0f;
													this.velocity.Y = 0f;
													this.ai[0] = 2f;
													this.position.X = (float)(n * 16 + 8 - this.width / 2);
													this.position.Y = (float)(num23 * 16 + 8 - this.height / 2);
													this.damage = 0;
													this.netUpdate = true;
													if (Main.myPlayer == this.owner)
													{
														NetMessage.SendData(13, -1, -1, "", this.owner, 0f, 0f, 0f, 0);
														break;
													}
													break;
												}
												else
												{
													num23++;
												}
											}
											if (this.ai[0] == 2f)
											{
												return;
											}
										}
										return;
									}
									if (this.ai[0] == 1f)
									{
										float num28 = 11f;
										if (this.type == 32)
										{
											num28 = 15f;
										}
										if (num18 < 24f)
										{
											this.Kill();
										}
										num18 = num28 / num18;
										num16 *= num18;
										num17 *= num18;
										this.velocity.X = num16;
										this.velocity.Y = num17;
										return;
									}
									if (this.ai[0] == 2f)
									{
										int num29 = (int)(this.position.X / 16f) - 1;
										int num30 = (int)((this.position.X + (float)this.width) / 16f) + 2;
										int num31 = (int)(this.position.Y / 16f) - 1;
										int num32 = (int)((this.position.Y + (float)this.height) / 16f) + 2;
										if (num29 < 0)
										{
											num29 = 0;
										}
										if (num30 > Main.maxTilesX)
										{
											num30 = Main.maxTilesX;
										}
										if (num31 < 0)
										{
											num31 = 0;
										}
										if (num32 > Main.maxTilesY)
										{
											num32 = Main.maxTilesY;
										}
										bool flag = true;
										for (int num33 = num29; num33 < num30; num33++)
										{
											for (int num34 = num31; num34 < num32; num34++)
											{
												if (Main.tile[num33, num34] == null)
												{
													Main.tile[num33, num34] = new Tile();
												}
												Vector2 vector5;
												vector5.X = (float)(num33 * 16);
												vector5.Y = (float)(num34 * 16);
												if (this.position.X + (float)(this.width / 2) > vector5.X && this.position.X + (float)(this.width / 2) < vector5.X + 16f && this.position.Y + (float)(this.height / 2) > vector5.Y && this.position.Y + (float)(this.height / 2) < vector5.Y + 16f && Main.tile[num33, num34].active && Main.tileSolid[(int)Main.tile[num33, num34].type])
												{
													flag = false;
												}
											}
										}
										if (flag)
										{
											this.ai[0] = 1f;
											return;
										}
										if (Main.player[this.owner].grapCount < 10)
										{
											Main.player[this.owner].grappling[Main.player[this.owner].grapCount] = this.whoAmI;
											Main.player[this.owner].grapCount++;
											return;
										}
									}
								}
								else
								{
									if (this.aiStyle == 8)
									{
										if (this.type == 27)
										{
											Vector2 arg_18D6_0 = new Vector2(this.position.X + this.velocity.X, this.position.Y + this.velocity.Y);
											int arg_18D6_1 = this.width;
											int arg_18D6_2 = this.height;
											int arg_18D6_3 = 29;
											float arg_18D6_4 = this.velocity.X;
											float arg_18D6_5 = this.velocity.Y;
											int arg_18D6_6 = 100;
											Color newColor = default(Color);
											int num35 = Dust.NewDust(arg_18D6_0, arg_18D6_1, arg_18D6_2, arg_18D6_3, arg_18D6_4, arg_18D6_5, arg_18D6_6, newColor, 3f);
											Main.dust[num35].noGravity = true;
											if (Main.rand.Next(10) == 0)
											{
												Vector2 arg_194C_0 = new Vector2(this.position.X, this.position.Y);
												int arg_194C_1 = this.width;
												int arg_194C_2 = this.height;
												int arg_194C_3 = 29;
												float arg_194C_4 = this.velocity.X;
												float arg_194C_5 = this.velocity.Y;
												int arg_194C_6 = 100;
												newColor = default(Color);
												num35 = Dust.NewDust(arg_194C_0, arg_194C_1, arg_194C_2, arg_194C_3, arg_194C_4, arg_194C_5, arg_194C_6, newColor, 1.4f);
											}
										}
										else
										{
											for (int num36 = 0; num36 < 2; num36++)
											{
												Vector2 arg_19BB_0 = new Vector2(this.position.X, this.position.Y);
												int arg_19BB_1 = this.width;
												int arg_19BB_2 = this.height;
												int arg_19BB_3 = 6;
												float arg_19BB_4 = this.velocity.X * 0.2f;
												float arg_19BB_5 = this.velocity.Y * 0.2f;
												int arg_19BB_6 = 100;
												Color newColor = default(Color);
												int num37 = Dust.NewDust(arg_19BB_0, arg_19BB_1, arg_19BB_2, arg_19BB_3, arg_19BB_4, arg_19BB_5, arg_19BB_6, newColor, 2f);
												Main.dust[num37].noGravity = true;
												Dust expr_19DD_cp_0 = Main.dust[num37];
												expr_19DD_cp_0.velocity.X = expr_19DD_cp_0.velocity.X * 0.3f;
												Dust expr_19FB_cp_0 = Main.dust[num37];
												expr_19FB_cp_0.velocity.Y = expr_19FB_cp_0.velocity.Y * 0.3f;
											}
										}
										if (this.type != 27)
										{
											this.ai[1] += 1f;
										}
										if (this.ai[1] >= 20f)
										{
											this.velocity.Y = this.velocity.Y + 0.2f;
										}
										this.rotation += 0.3f * (float)this.direction;
										if (this.velocity.Y > 16f)
										{
											this.velocity.Y = 16f;
											return;
										}
									}
									else
									{
										if (this.aiStyle == 9)
										{
											if (this.type == 34)
											{
												Vector2 arg_1B1C_0 = new Vector2(this.position.X, this.position.Y);
												int arg_1B1C_1 = this.width;
												int arg_1B1C_2 = this.height;
												int arg_1B1C_3 = 6;
												float arg_1B1C_4 = this.velocity.X * 0.2f;
												float arg_1B1C_5 = this.velocity.Y * 0.2f;
												int arg_1B1C_6 = 100;
												Color newColor = default(Color);
												int num38 = Dust.NewDust(arg_1B1C_0, arg_1B1C_1, arg_1B1C_2, arg_1B1C_3, arg_1B1C_4, arg_1B1C_5, arg_1B1C_6, newColor, 3.5f);
												Main.dust[num38].noGravity = true;
												Dust expr_1B39 = Main.dust[num38];
												expr_1B39.velocity *= 1.4f;
												Vector2 arg_1BA9_0 = new Vector2(this.position.X, this.position.Y);
												int arg_1BA9_1 = this.width;
												int arg_1BA9_2 = this.height;
												int arg_1BA9_3 = 6;
												float arg_1BA9_4 = this.velocity.X * 0.2f;
												float arg_1BA9_5 = this.velocity.Y * 0.2f;
												int arg_1BA9_6 = 100;
												newColor = default(Color);
												num38 = Dust.NewDust(arg_1BA9_0, arg_1BA9_1, arg_1BA9_2, arg_1BA9_3, arg_1BA9_4, arg_1BA9_5, arg_1BA9_6, newColor, 1.5f);
											}
											else
											{
												if (this.soundDelay == 0 && Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y) > 2f)
												{
													this.soundDelay = 10;
													Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 9);
												}
												Vector2 arg_1C51_0 = new Vector2(this.position.X, this.position.Y);
												int arg_1C51_1 = this.width;
												int arg_1C51_2 = this.height;
												int arg_1C51_3 = 15;
												float arg_1C51_4 = 0f;
												float arg_1C51_5 = 0f;
												int arg_1C51_6 = 100;
												Color newColor = default(Color);
												int num39 = Dust.NewDust(arg_1C51_0, arg_1C51_1, arg_1C51_2, arg_1C51_3, arg_1C51_4, arg_1C51_5, arg_1C51_6, newColor, 2f);
												Dust expr_1C60 = Main.dust[num39];
												expr_1C60.velocity *= 0.3f;
												Main.dust[num39].position.X = this.position.X + (float)(this.width / 2) + 4f + (float)Main.rand.Next(-4, 5);
												Main.dust[num39].position.Y = this.position.Y + (float)(this.height / 2) + (float)Main.rand.Next(-4, 5);
												Main.dust[num39].noGravity = true;
											}
											if (Main.myPlayer == this.owner && this.ai[0] == 0f)
											{
												if (Main.player[this.owner].channel)
												{
													float num40 = 12f;
													if (this.type == 16)
													{
														num40 = 15f;
													}
													Vector2 vector6 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
													float num41 = (float)Main.mouseState.X + Main.screenPosition.X - vector6.X;
													float num42 = (float)Main.mouseState.Y + Main.screenPosition.Y - vector6.Y;
													float num43 = (float)Math.Sqrt((double)(num41 * num41 + num42 * num42));
													num43 = (float)Math.Sqrt((double)(num41 * num41 + num42 * num42));
													if (num43 > num40)
													{
														num43 = num40 / num43;
														num41 *= num43;
														num42 *= num43;
														if (num41 != this.velocity.X || num42 != this.velocity.Y)
														{
															this.netUpdate = true;
														}
														this.velocity.X = num41;
														this.velocity.Y = num42;
													}
													else
													{
														if (num41 != this.velocity.X || num42 != this.velocity.Y)
														{
															this.netUpdate = true;
														}
														this.velocity.X = num41;
														this.velocity.Y = num42;
													}
												}
												else
												{
													if (this.ai[0] == 0f)
													{
														this.ai[0] = 1f;
														this.netUpdate = true;
														float num44 = 12f;
														Vector2 vector7 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
														float num45 = (float)Main.mouseState.X + Main.screenPosition.X - vector7.X;
														float num46 = (float)Main.mouseState.Y + Main.screenPosition.Y - vector7.Y;
														float num47 = (float)Math.Sqrt((double)(num45 * num45 + num46 * num46));
														if (num47 == 0f)
														{
															vector7 = new Vector2(Main.player[this.owner].position.X + (float)(Main.player[this.owner].width / 2), Main.player[this.owner].position.Y + (float)(Main.player[this.owner].height / 2));
															num45 = this.position.X + (float)this.width * 0.5f - vector7.X;
															num46 = this.position.Y + (float)this.height * 0.5f - vector7.Y;
															num47 = (float)Math.Sqrt((double)(num45 * num45 + num46 * num46));
														}
														num47 = num44 / num47;
														num45 *= num47;
														num46 *= num47;
														this.velocity.X = num45;
														this.velocity.Y = num46;
														if (this.velocity.X == 0f && this.velocity.Y == 0f)
														{
															this.Kill();
														}
													}
												}
											}
											if (this.type == 34)
											{
												this.rotation += 0.3f * (float)this.direction;
											}
											else
											{
												if (this.velocity.X != 0f || this.velocity.Y != 0f)
												{
													this.rotation = (float)Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) - 2.355f;
												}
											}
											if (this.velocity.Y > 16f)
											{
												this.velocity.Y = 16f;
												return;
											}
										}
										else
										{
											if (this.aiStyle == 10)
											{
												if (this.type == 31 && this.ai[0] != 2f)
												{
													if (Main.rand.Next(2) == 0)
													{
														Vector2 arg_2184_0 = new Vector2(this.position.X, this.position.Y);
														int arg_2184_1 = this.width;
														int arg_2184_2 = this.height;
														int arg_2184_3 = 32;
														float arg_2184_4 = 0f;
														float arg_2184_5 = this.velocity.Y / 2f;
														int arg_2184_6 = 0;
														Color newColor = default(Color);
														int num48 = Dust.NewDust(arg_2184_0, arg_2184_1, arg_2184_2, arg_2184_3, arg_2184_4, arg_2184_5, arg_2184_6, newColor, 1f);
														Dust expr_2198_cp_0 = Main.dust[num48];
														expr_2198_cp_0.velocity.X = expr_2198_cp_0.velocity.X * 0.4f;
													}
												}
												else
												{
													if (this.type == 39)
													{
														if (Main.rand.Next(2) == 0)
														{
															Vector2 arg_221A_0 = new Vector2(this.position.X, this.position.Y);
															int arg_221A_1 = this.width;
															int arg_221A_2 = this.height;
															int arg_221A_3 = 38;
															float arg_221A_4 = 0f;
															float arg_221A_5 = this.velocity.Y / 2f;
															int arg_221A_6 = 0;
															Color newColor = default(Color);
															int num49 = Dust.NewDust(arg_221A_0, arg_221A_1, arg_221A_2, arg_221A_3, arg_221A_4, arg_221A_5, arg_221A_6, newColor, 1f);
															Dust expr_222E_cp_0 = Main.dust[num49];
															expr_222E_cp_0.velocity.X = expr_222E_cp_0.velocity.X * 0.4f;
														}
													}
													else
													{
														if (this.type == 40)
														{
															if (Main.rand.Next(2) == 0)
															{
																Vector2 arg_22B0_0 = new Vector2(this.position.X, this.position.Y);
																int arg_22B0_1 = this.width;
																int arg_22B0_2 = this.height;
																int arg_22B0_3 = 36;
																float arg_22B0_4 = 0f;
																float arg_22B0_5 = this.velocity.Y / 2f;
																int arg_22B0_6 = 0;
																Color newColor = default(Color);
																int num50 = Dust.NewDust(arg_22B0_0, arg_22B0_1, arg_22B0_2, arg_22B0_3, arg_22B0_4, arg_22B0_5, arg_22B0_6, newColor, 1f);
																Dust expr_22BF = Main.dust[num50];
																expr_22BF.velocity *= 0.4f;
															}
														}
														else
														{
															if (this.type == 42 || this.type == 31)
															{
																if (Main.rand.Next(2) == 0)
																{
																	Vector2 arg_2340_0 = new Vector2(this.position.X, this.position.Y);
																	int arg_2340_1 = this.width;
																	int arg_2340_2 = this.height;
																	int arg_2340_3 = 32;
																	float arg_2340_4 = 0f;
																	float arg_2340_5 = 0f;
																	int arg_2340_6 = 0;
																	Color newColor = default(Color);
																	int num51 = Dust.NewDust(arg_2340_0, arg_2340_1, arg_2340_2, arg_2340_3, arg_2340_4, arg_2340_5, arg_2340_6, newColor, 1f);
																	Dust expr_2354_cp_0 = Main.dust[num51];
																	expr_2354_cp_0.velocity.X = expr_2354_cp_0.velocity.X * 0.4f;
																}
															}
															else
															{
																if (Main.rand.Next(20) == 0)
																{
																	Vector2 arg_23B7_0 = new Vector2(this.position.X, this.position.Y);
																	int arg_23B7_1 = this.width;
																	int arg_23B7_2 = this.height;
																	int arg_23B7_3 = 0;
																	float arg_23B7_4 = 0f;
																	float arg_23B7_5 = 0f;
																	int arg_23B7_6 = 0;
																	Color newColor = default(Color);
																	Dust.NewDust(arg_23B7_0, arg_23B7_1, arg_23B7_2, arg_23B7_3, arg_23B7_4, arg_23B7_5, arg_23B7_6, newColor, 1f);
																}
															}
														}
													}
												}
												if (Main.myPlayer == this.owner && this.ai[0] == 0f)
												{
													if (Main.player[this.owner].channel)
													{
														float num52 = 12f;
														Vector2 vector8 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
														float num53 = (float)Main.mouseState.X + Main.screenPosition.X - vector8.X;
														float num54 = (float)Main.mouseState.Y + Main.screenPosition.Y - vector8.Y;
														float num55 = (float)Math.Sqrt((double)(num53 * num53 + num54 * num54));
														num55 = (float)Math.Sqrt((double)(num53 * num53 + num54 * num54));
														if (num55 > num52)
														{
															num55 = num52 / num55;
															num53 *= num55;
															num54 *= num55;
															if (num53 != this.velocity.X || num54 != this.velocity.Y)
															{
																this.netUpdate = true;
															}
															this.velocity.X = num53;
															this.velocity.Y = num54;
														}
														else
														{
															if (num53 != this.velocity.X || num54 != this.velocity.Y)
															{
																this.netUpdate = true;
															}
															this.velocity.X = num53;
															this.velocity.Y = num54;
														}
													}
													else
													{
														this.ai[0] = 1f;
														this.netUpdate = true;
													}
												}
												if (this.ai[0] == 1f)
												{
													if (this.type == 42)
													{
														this.ai[1] += 1f;
														if (this.ai[1] >= 15f)
														{
															this.ai[1] = 15f;
															this.velocity.Y = this.velocity.Y + 0.2f;
														}
													}
													else
													{
														this.velocity.Y = this.velocity.Y + 0.41f;
													}
												}
												else
												{
													if (this.ai[0] == 2f)
													{
														this.velocity.Y = this.velocity.Y + 0.2f;
														if ((double)this.velocity.X < -0.04)
														{
															this.velocity.X = this.velocity.X + 0.04f;
														}
														else
														{
															if ((double)this.velocity.X > 0.04)
															{
																this.velocity.X = this.velocity.X - 0.04f;
															}
															else
															{
																this.velocity.X = 0f;
															}
														}
													}
												}
												this.rotation += 0.1f;
												if (this.velocity.Y > 10f)
												{
													this.velocity.Y = 10f;
													return;
												}
											}
											else
											{
												if (this.aiStyle == 11)
												{
													this.rotation += 0.02f;
													if (Main.myPlayer == this.owner)
													{
														if (Main.player[Main.myPlayer].lightOrb)
														{
															this.timeLeft = 2;
														}
														if (Main.player[this.owner].dead)
														{
															this.Kill();
															return;
														}
														float num56 = 2.5f;
														Vector2 vector9 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
														float num57 = Main.player[this.owner].position.X + (float)(Main.player[this.owner].width / 2) - vector9.X;
														float num58 = Main.player[this.owner].position.Y + (float)(Main.player[this.owner].height / 2) - vector9.Y;
														float num59 = (float)Math.Sqrt((double)(num57 * num57 + num58 * num58));
														num59 = (float)Math.Sqrt((double)(num57 * num57 + num58 * num58));
														if (num59 > 800f)
														{
															this.position.X = Main.player[this.owner].position.X + (float)(Main.player[this.owner].width / 2) - (float)(this.width / 2);
															this.position.Y = Main.player[this.owner].position.Y + (float)(Main.player[this.owner].height / 2) - (float)(this.height / 2);
															return;
														}
														if (num59 > 64f)
														{
															num59 = num56 / num59;
															num57 *= num59;
															num58 *= num59;
															if (num57 != this.velocity.X || num58 != this.velocity.Y)
															{
																this.netUpdate = true;
															}
															this.velocity.X = num57;
															this.velocity.Y = num58;
															return;
														}
														if (this.velocity.X != 0f || this.velocity.Y != 0f)
														{
															this.netUpdate = true;
														}
														this.velocity.X = 0f;
														this.velocity.Y = 0f;
														return;
													}
												}
												else
												{
													if (this.aiStyle == 12)
													{
														this.scale -= 0.04f;
														if (this.scale <= 0f)
														{
															this.Kill();
														}
														if (this.ai[0] > 4f)
														{
															this.alpha = 150;
															this.light = 0.8f;
															Vector2 arg_29C4_0 = new Vector2(this.position.X, this.position.Y);
															int arg_29C4_1 = this.width;
															int arg_29C4_2 = this.height;
															int arg_29C4_3 = 29;
															float arg_29C4_4 = this.velocity.X;
															float arg_29C4_5 = this.velocity.Y;
															int arg_29C4_6 = 100;
															Color newColor = default(Color);
															int num60 = Dust.NewDust(arg_29C4_0, arg_29C4_1, arg_29C4_2, arg_29C4_3, arg_29C4_4, arg_29C4_5, arg_29C4_6, newColor, 2.5f);
															Main.dust[num60].noGravity = true;
															Vector2 arg_2A29_0 = new Vector2(this.position.X, this.position.Y);
															int arg_2A29_1 = this.width;
															int arg_2A29_2 = this.height;
															int arg_2A29_3 = 29;
															float arg_2A29_4 = this.velocity.X;
															float arg_2A29_5 = this.velocity.Y;
															int arg_2A29_6 = 100;
															newColor = default(Color);
															Dust.NewDust(arg_2A29_0, arg_2A29_1, arg_2A29_2, arg_2A29_3, arg_2A29_4, arg_2A29_5, arg_2A29_6, newColor, 1.5f);
														}
														else
														{
															this.ai[0] += 1f;
														}
														this.rotation += 0.3f * (float)this.direction;
														return;
													}
													if (this.aiStyle == 13)
													{
														if (Main.player[this.owner].dead)
														{
															this.Kill();
															return;
														}
														Main.player[this.owner].itemAnimation = 5;
														Main.player[this.owner].itemTime = 5;
														if (this.position.X + (float)(this.width / 2) > Main.player[this.owner].position.X + (float)(Main.player[this.owner].width / 2))
														{
															Main.player[this.owner].direction = 1;
														}
														else
														{
															Main.player[this.owner].direction = -1;
														}
														Vector2 vector10 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
														float num61 = Main.player[this.owner].position.X + (float)(Main.player[this.owner].width / 2) - vector10.X;
														float num62 = Main.player[this.owner].position.Y + (float)(Main.player[this.owner].height / 2) - vector10.Y;
														float num63 = (float)Math.Sqrt((double)(num61 * num61 + num62 * num62));
														if (this.ai[0] == 0f)
														{
															if (num63 > 700f)
															{
																this.ai[0] = 1f;
															}
															this.rotation = (float)Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) + 1.57f;
															this.ai[1] += 1f;
															if (this.ai[1] > 2f)
															{
																this.alpha = 0;
															}
															if (this.ai[1] >= 10f)
															{
																this.ai[1] = 15f;
																this.velocity.Y = this.velocity.Y + 0.3f;
																return;
															}
														}
														else
														{
															if (this.ai[0] == 1f)
															{
																this.tileCollide = false;
																this.rotation = (float)Math.Atan2((double)num62, (double)num61) - 1.57f;
																float num64 = 20f;
																if (num63 < 50f)
																{
																	this.Kill();
																}
																num63 = num64 / num63;
																num61 *= num63;
																num62 *= num63;
																this.velocity.X = num61;
																this.velocity.Y = num62;
																return;
															}
														}
													}
													else
													{
														if (this.aiStyle == 14)
														{
															if (this.type == 53)
															{
																try
																{
																	Vector2 value2 = Collision.TileCollision(this.position, this.velocity, this.width, this.height, false, false);
																	this.velocity = -value2;
																	int num65 = (int)(this.position.X / 16f) - 1;
																	int num66 = (int)((this.position.X + (float)this.width) / 16f) + 2;
																	int num67 = (int)(this.position.Y / 16f) - 1;
																	int num68 = (int)((this.position.Y + (float)this.height) / 16f) + 2;
																	if (num65 < 0)
																	{
																		num65 = 0;
																	}
																	if (num66 > Main.maxTilesX)
																	{
																		num66 = Main.maxTilesX;
																	}
																	if (num67 < 0)
																	{
																		num67 = 0;
																	}
																	if (num68 > Main.maxTilesY)
																	{
																		num68 = Main.maxTilesY;
																	}
																	for (int num69 = num65; num69 < num66; num69++)
																	{
																		for (int num70 = num67; num70 < num68; num70++)
																		{
																			if (Main.tile[num69, num70] != null && Main.tile[num69, num70].active && (Main.tileSolid[(int)Main.tile[num69, num70].type] || (Main.tileSolidTop[(int)Main.tile[num69, num70].type] && Main.tile[num69, num70].frameY == 0)))
																			{
																				Vector2 vector11;
																				vector11.X = (float)(num69 * 16);
																				vector11.Y = (float)(num70 * 16);
																				if (this.position.X + (float)this.width > vector11.X && this.position.X < vector11.X + 16f && this.position.Y + (float)this.height > vector11.Y && this.position.Y < vector11.Y + 16f)
																				{
																					this.velocity.X = 0f;
																					this.velocity.Y = -0.2f;
																				}
																			}
																		}
																	}
																}
																catch
																{
																}
															}
															this.ai[0] += 1f;
															if (this.ai[0] > 5f)
															{
																this.ai[0] = 5f;
																if (this.velocity.Y == 0f && this.velocity.X != 0f)
																{
																	this.velocity.X = this.velocity.X * 0.97f;
																	if ((double)this.velocity.X > -0.01 && (double)this.velocity.X < 0.01)
																	{
																		this.velocity.X = 0f;
																		this.netUpdate = true;
																	}
																}
																this.velocity.Y = this.velocity.Y + 0.2f;
															}
															this.rotation += this.velocity.X * 0.1f;
															return;
														}
														if (this.aiStyle == 15)
														{
															if (this.type == 25)
															{
																if (Main.rand.Next(15) == 0)
																{
																	Vector2 arg_308A_0 = this.position;
																	int arg_308A_1 = this.width;
																	int arg_308A_2 = this.height;
																	int arg_308A_3 = 14;
																	float arg_308A_4 = 0f;
																	float arg_308A_5 = 0f;
																	int arg_308A_6 = 150;
																	Color newColor = default(Color);
																	Dust.NewDust(arg_308A_0, arg_308A_1, arg_308A_2, arg_308A_3, arg_308A_4, arg_308A_5, arg_308A_6, newColor, 1.3f);
																}
															}
															else
															{
																if (this.type == 26)
																{
																	Vector2 arg_30E9_0 = this.position;
																	int arg_30E9_1 = this.width;
																	int arg_30E9_2 = this.height;
																	int arg_30E9_3 = 29;
																	float arg_30E9_4 = this.velocity.X * 0.4f;
																	float arg_30E9_5 = this.velocity.Y * 0.4f;
																	int arg_30E9_6 = 100;
																	Color newColor = default(Color);
																	int num71 = Dust.NewDust(arg_30E9_0, arg_30E9_1, arg_30E9_2, arg_30E9_3, arg_30E9_4, arg_30E9_5, arg_30E9_6, newColor, 2.5f);
																	Main.dust[num71].noGravity = true;
																	Dust expr_310B_cp_0 = Main.dust[num71];
																	expr_310B_cp_0.velocity.X = expr_310B_cp_0.velocity.X / 2f;
																	Dust expr_3129_cp_0 = Main.dust[num71];
																	expr_3129_cp_0.velocity.Y = expr_3129_cp_0.velocity.Y / 2f;
																}
																else
																{
																	if (this.type == 35)
																	{
																		Vector2 arg_3192_0 = this.position;
																		int arg_3192_1 = this.width;
																		int arg_3192_2 = this.height;
																		int arg_3192_3 = 6;
																		float arg_3192_4 = this.velocity.X * 0.4f;
																		float arg_3192_5 = this.velocity.Y * 0.4f;
																		int arg_3192_6 = 100;
																		Color newColor = default(Color);
																		int num72 = Dust.NewDust(arg_3192_0, arg_3192_1, arg_3192_2, arg_3192_3, arg_3192_4, arg_3192_5, arg_3192_6, newColor, 3f);
																		Main.dust[num72].noGravity = true;
																		Dust expr_31B4_cp_0 = Main.dust[num72];
																		expr_31B4_cp_0.velocity.X = expr_31B4_cp_0.velocity.X * 2f;
																		Dust expr_31D2_cp_0 = Main.dust[num72];
																		expr_31D2_cp_0.velocity.Y = expr_31D2_cp_0.velocity.Y * 2f;
																	}
																}
															}
															if (Main.player[this.owner].dead)
															{
																this.Kill();
																return;
															}
															Main.player[this.owner].itemAnimation = 10;
															Main.player[this.owner].itemTime = 10;
															if (this.position.X + (float)(this.width / 2) > Main.player[this.owner].position.X + (float)(Main.player[this.owner].width / 2))
															{
																Main.player[this.owner].direction = 1;
																this.direction = 1;
															}
															else
															{
																Main.player[this.owner].direction = -1;
																this.direction = -1;
															}
															Vector2 vector12 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
															float num73 = Main.player[this.owner].position.X + (float)(Main.player[this.owner].width / 2) - vector12.X;
															float num74 = Main.player[this.owner].position.Y + (float)(Main.player[this.owner].height / 2) - vector12.Y;
															float num75 = (float)Math.Sqrt((double)(num73 * num73 + num74 * num74));
															if (this.ai[0] == 0f)
															{
																this.tileCollide = true;
																if (num75 > 160f)
																{
																	this.ai[0] = 1f;
																	this.netUpdate = true;
																}
																else
																{
																	if (!Main.player[this.owner].channel)
																	{
																		if (this.velocity.Y < 0f)
																		{
																			this.velocity.Y = this.velocity.Y * 0.9f;
																		}
																		this.velocity.Y = this.velocity.Y + 1f;
																		this.velocity.X = this.velocity.X * 0.9f;
																	}
																}
															}
															else
															{
																if (this.ai[0] == 1f)
																{
																	float num76 = 14f / Main.player[this.owner].meleeSpeed;
																	float num77 = 0.9f / Main.player[this.owner].meleeSpeed;
																	Math.Abs(num73);
																	Math.Abs(num74);
																	if (this.ai[1] == 1f)
																	{
																		this.tileCollide = false;
																	}
																	if (!Main.player[this.owner].channel || num75 > 300f || !this.tileCollide)
																	{
																		this.ai[1] = 1f;
																		if (this.tileCollide)
																		{
																			this.netUpdate = true;
																		}
																		this.tileCollide = false;
																		if (num75 < 20f)
																		{
																			this.Kill();
																		}
																	}
																	if (!this.tileCollide)
																	{
																		num77 *= 2f;
																	}
																	if (num75 > 60f || !this.tileCollide)
																	{
																		num75 = num76 / num75;
																		num73 *= num75;
																		num74 *= num75;
																		new Vector2(this.velocity.X, this.velocity.Y);
																		float num78 = num73 - this.velocity.X;
																		float num79 = num74 - this.velocity.Y;
																		float num80 = (float)Math.Sqrt((double)(num78 * num78 + num79 * num79));
																		num80 = num77 / num80;
																		num78 *= num80;
																		num79 *= num80;
																		this.velocity.X = this.velocity.X * 0.98f;
																		this.velocity.Y = this.velocity.Y * 0.98f;
																		this.velocity.X = this.velocity.X + num78;
																		this.velocity.Y = this.velocity.Y + num79;
																	}
																	else
																	{
																		if (Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y) < 6f)
																		{
																			this.velocity.X = this.velocity.X * 0.96f;
																			this.velocity.Y = this.velocity.Y + 0.2f;
																		}
																		if (Main.player[this.owner].velocity.X == 0f)
																		{
																			this.velocity.X = this.velocity.X * 0.96f;
																		}
																	}
																}
															}
															this.rotation = (float)Math.Atan2((double)num74, (double)num73) - this.velocity.X * 0.1f;
															return;
														}
														else
														{
															if (this.aiStyle == 16)
															{
																if (this.type == 37)
																{
																	try
																	{
																		int num81 = (int)(this.position.X / 16f) - 1;
																		int num82 = (int)((this.position.X + (float)this.width) / 16f) + 2;
																		int num83 = (int)(this.position.Y / 16f) - 1;
																		int num84 = (int)((this.position.Y + (float)this.height) / 16f) + 2;
																		if (num81 < 0)
																		{
																			num81 = 0;
																		}
																		if (num82 > Main.maxTilesX)
																		{
																			num82 = Main.maxTilesX;
																		}
																		if (num83 < 0)
																		{
																			num83 = 0;
																		}
																		if (num84 > Main.maxTilesY)
																		{
																			num84 = Main.maxTilesY;
																		}
																		for (int num85 = num81; num85 < num82; num85++)
																		{
																			for (int num86 = num83; num86 < num84; num86++)
																			{
																				if (Main.tile[num85, num86] != null && Main.tile[num85, num86].active && (Main.tileSolid[(int)Main.tile[num85, num86].type] || (Main.tileSolidTop[(int)Main.tile[num85, num86].type] && Main.tile[num85, num86].frameY == 0)))
																				{
																					Vector2 vector13;
																					vector13.X = (float)(num85 * 16);
																					vector13.Y = (float)(num86 * 16);
																					if (this.position.X + (float)this.width - 4f > vector13.X && this.position.X + 4f < vector13.X + 16f && this.position.Y + (float)this.height - 4f > vector13.Y && this.position.Y + 4f < vector13.Y + 16f)
																					{
																						this.velocity.X = 0f;
																						this.velocity.Y = -0.2f;
																					}
																				}
																			}
																		}
																	}
																	catch
																	{
																	}
																}
																if (this.owner == Main.myPlayer && this.timeLeft <= 3)
																{
																	this.ai[1] = 0f;
																	this.alpha = 255;
																	if (this.type == 28 || this.type == 37)
																	{
																		this.position.X = this.position.X + (float)(this.width / 2);
																		this.position.Y = this.position.Y + (float)(this.height / 2);
																		this.width = 128;
																		this.height = 128;
																		this.position.X = this.position.X - (float)(this.width / 2);
																		this.position.Y = this.position.Y - (float)(this.height / 2);
																		this.damage = 100;
																		this.knockBack = 8f;
																	}
																	else
																	{
																		if (this.type == 29)
																		{
																			this.position.X = this.position.X + (float)(this.width / 2);
																			this.position.Y = this.position.Y + (float)(this.height / 2);
																			this.width = 250;
																			this.height = 250;
																			this.position.X = this.position.X - (float)(this.width / 2);
																			this.position.Y = this.position.Y - (float)(this.height / 2);
																			this.damage = 250;
																			this.knockBack = 10f;
																		}
																		else
																		{
																			if (this.type == 30)
																			{
																				this.position.X = this.position.X + (float)(this.width / 2);
																				this.position.Y = this.position.Y + (float)(this.height / 2);
																				this.width = 128;
																				this.height = 128;
																				this.position.X = this.position.X - (float)(this.width / 2);
																				this.position.Y = this.position.Y - (float)(this.height / 2);
																				this.knockBack = 8f;
																			}
																		}
																	}
																}
																else
																{
																	if (this.type != 30 && Main.rand.Next(4) == 0)
																	{
																		if (this.type != 30)
																		{
																			this.damage = 0;
																		}
																		Vector2 arg_3B21_0 = new Vector2(this.position.X, this.position.Y);
																		int arg_3B21_1 = this.width;
																		int arg_3B21_2 = this.height;
																		int arg_3B21_3 = 6;
																		float arg_3B21_4 = 0f;
																		float arg_3B21_5 = 0f;
																		int arg_3B21_6 = 100;
																		Color newColor = default(Color);
																		Dust.NewDust(arg_3B21_0, arg_3B21_1, arg_3B21_2, arg_3B21_3, arg_3B21_4, arg_3B21_5, arg_3B21_6, newColor, 1f);
																	}
																}
																this.ai[0] += 1f;
																if ((this.type == 30 && this.ai[0] > 10f) || (this.type != 30 && this.ai[0] > 5f))
																{
																	this.ai[0] = 10f;
																	if (this.velocity.Y == 0f && this.velocity.X != 0f)
																	{
																		this.velocity.X = this.velocity.X * 0.97f;
																		if (this.type == 29)
																		{
																			this.velocity.X = this.velocity.X * 0.99f;
																		}
																		if ((double)this.velocity.X > -0.01 && (double)this.velocity.X < 0.01)
																		{
																			this.velocity.X = 0f;
																			this.netUpdate = true;
																		}
																	}
																	this.velocity.Y = this.velocity.Y + 0.2f;
																}
																this.rotation += this.velocity.X * 0.1f;
																return;
															}
															if (this.aiStyle == 17)
															{
																if (this.velocity.Y == 0f)
																{
																	this.velocity.X = this.velocity.X * 0.98f;
																}
																this.rotation += this.velocity.X * 0.1f;
																this.velocity.Y = this.velocity.Y + 0.2f;
																if (this.owner == Main.myPlayer)
																{
																	int num87 = (int)((this.position.X + (float)this.width) / 16f);
																	int num88 = (int)((this.position.Y + (float)this.height) / 16f);
																	if (Main.tile[num87, num88] != null && !Main.tile[num87, num88].active)
																	{
																		WorldGen.PlaceTile(num87, num88, 85, false, false, -1, 0);
																		if (Main.tile[num87, num88].active)
																		{
																			if (Main.netMode != 0)
																			{
																				NetMessage.SendData(17, -1, -1, "", 1, (float)num87, (float)num88, 85f, 0);
																			}
																			int num89 = Sign.ReadSign(num87, num88);
																			if (num89 >= 0)
																			{
																				Sign.TextSign(num89, this.miscText);
																			}
																			this.Kill();
																			return;
																		}
																	}
																}
															}
															else
															{
																if (this.aiStyle == 18)
																{
																	if (this.ai[1] == 0f && this.type == 44)
																	{
																		this.ai[1] = 1f;
																		Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 8);
																	}
																	this.rotation += (float)this.direction * 0.8f;
																	this.ai[0] += 1f;
																	if (this.ai[0] >= 30f)
																	{
																		if (this.ai[0] < 100f)
																		{
																			this.velocity *= 1.06f;
																		}
																		else
																		{
																			this.ai[0] = 200f;
																		}
																	}
																	for (int num90 = 0; num90 < 2; num90++)
																	{
																		Vector2 arg_3EC4_0 = new Vector2(this.position.X, this.position.Y);
																		int arg_3EC4_1 = this.width;
																		int arg_3EC4_2 = this.height;
																		int arg_3EC4_3 = 27;
																		float arg_3EC4_4 = 0f;
																		float arg_3EC4_5 = 0f;
																		int arg_3EC4_6 = 100;
																		Color newColor = default(Color);
																		int num91 = Dust.NewDust(arg_3EC4_0, arg_3EC4_1, arg_3EC4_2, arg_3EC4_3, arg_3EC4_4, arg_3EC4_5, arg_3EC4_6, newColor, 1f);
																		Main.dust[num91].noGravity = true;
																	}
																	return;
																}
																if (this.aiStyle == 19)
																{
																	this.direction = Main.player[this.owner].direction;
																	Main.player[this.owner].heldProj = this.whoAmI;
																	Main.player[this.owner].itemTime = Main.player[this.owner].itemAnimation;
																	this.position.X = Main.player[this.owner].position.X + (float)(Main.player[this.owner].width / 2) - (float)(this.width / 2);
																	this.position.Y = Main.player[this.owner].position.Y + (float)(Main.player[this.owner].height / 2) - (float)(this.height / 2);
																	if (this.type == 46)
																	{
																		if (this.ai[0] == 0f)
																		{
																			this.ai[0] = 3f;
																			this.netUpdate = true;
																		}
																		if (Main.player[this.owner].itemAnimation < Main.player[this.owner].itemAnimationMax / 3)
																		{
																			this.ai[0] -= 1.6f;
																		}
																		else
																		{
																			this.ai[0] += 1.4f;
																		}
																	}
																	else
																	{
																		if (this.type == 47)
																		{
																			if (this.ai[0] == 0f)
																			{
																				this.ai[0] = 4f;
																				this.netUpdate = true;
																			}
																			if (Main.player[this.owner].itemAnimation < Main.player[this.owner].itemAnimationMax / 3)
																			{
																				this.ai[0] -= 1.2f;
																			}
																			else
																			{
																				this.ai[0] += 0.9f;
																			}
																		}
																		else
																		{
																			if (this.type == 49)
																			{
																				if (this.ai[0] == 0f)
																				{
																					this.ai[0] = 4f;
																					this.netUpdate = true;
																				}
																				if (Main.player[this.owner].itemAnimation < Main.player[this.owner].itemAnimationMax / 3)
																				{
																					this.ai[0] -= 1.1f;
																				}
																				else
																				{
																					this.ai[0] += 0.85f;
																				}
																			}
																		}
																	}
																	this.position += this.velocity * this.ai[0];
																	if (Main.player[this.owner].itemAnimation == 0)
																	{
																		this.Kill();
																	}
																	this.rotation = (float)Math.Atan2((double)this.velocity.Y, (double)this.velocity.X) + 2.355f;
																	if (this.type == 46)
																	{
																		Color newColor;
																		if (Main.rand.Next(5) == 0)
																		{
																			Vector2 arg_423B_0 = this.position;
																			int arg_423B_1 = this.width;
																			int arg_423B_2 = this.height;
																			int arg_423B_3 = 14;
																			float arg_423B_4 = 0f;
																			float arg_423B_5 = 0f;
																			int arg_423B_6 = 150;
																			newColor = default(Color);
																			Dust.NewDust(arg_423B_0, arg_423B_1, arg_423B_2, arg_423B_3, arg_423B_4, arg_423B_5, arg_423B_6, newColor, 1.4f);
																		}
																		Vector2 arg_4292_0 = this.position;
																		int arg_4292_1 = this.width;
																		int arg_4292_2 = this.height;
																		int arg_4292_3 = 27;
																		float arg_4292_4 = this.velocity.X * 0.2f + (float)(this.direction * 3);
																		float arg_4292_5 = this.velocity.Y * 0.2f;
																		int arg_4292_6 = 100;
																		newColor = default(Color);
																		int num92 = Dust.NewDust(arg_4292_0, arg_4292_1, arg_4292_2, arg_4292_3, arg_4292_4, arg_4292_5, arg_4292_6, newColor, 1.2f);
																		Main.dust[num92].noGravity = true;
																		Dust expr_42B4_cp_0 = Main.dust[num92];
																		expr_42B4_cp_0.velocity.X = expr_42B4_cp_0.velocity.X / 2f;
																		Dust expr_42D2_cp_0 = Main.dust[num92];
																		expr_42D2_cp_0.velocity.Y = expr_42D2_cp_0.velocity.Y / 2f;
																		Vector2 arg_432A_0 = this.position - this.velocity * 2f;
																		int arg_432A_1 = this.width;
																		int arg_432A_2 = this.height;
																		int arg_432A_3 = 27;
																		float arg_432A_4 = 0f;
																		float arg_432A_5 = 0f;
																		int arg_432A_6 = 150;
																		newColor = default(Color);
																		num92 = Dust.NewDust(arg_432A_0, arg_432A_1, arg_432A_2, arg_432A_3, arg_432A_4, arg_432A_5, arg_432A_6, newColor, 1.4f);
																		Dust expr_433E_cp_0 = Main.dust[num92];
																		expr_433E_cp_0.velocity.X = expr_433E_cp_0.velocity.X / 5f;
																		Dust expr_435C_cp_0 = Main.dust[num92];
																		expr_435C_cp_0.velocity.Y = expr_435C_cp_0.velocity.Y / 5f;
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		public void Kill()
		{
			if (!this.active)
			{
				return;
			}
			this.timeLeft = 0;
			if (this.type == 1)
			{
				Main.PlaySound(0, (int)this.position.X, (int)this.position.Y, 1);
				for (int i = 0; i < 10; i++)
				{
					Vector2 arg_7E_0 = new Vector2(this.position.X, this.position.Y);
					int arg_7E_1 = this.width;
					int arg_7E_2 = this.height;
					int arg_7E_3 = 7;
					float arg_7E_4 = 0f;
					float arg_7E_5 = 0f;
					int arg_7E_6 = 0;
					Color newColor = default(Color);
					Dust.NewDust(arg_7E_0, arg_7E_1, arg_7E_2, arg_7E_3, arg_7E_4, arg_7E_5, arg_7E_6, newColor, 1f);
				}
			}
			else
			{
				if (this.type == 55)
				{
					for (int j = 0; j < 5; j++)
					{
						Vector2 arg_E3_0 = new Vector2(this.position.X, this.position.Y);
						int arg_E3_1 = this.width;
						int arg_E3_2 = this.height;
						int arg_E3_3 = 18;
						float arg_E3_4 = 0f;
						float arg_E3_5 = 0f;
						int arg_E3_6 = 0;
						Color newColor = default(Color);
						int num = Dust.NewDust(arg_E3_0, arg_E3_1, arg_E3_2, arg_E3_3, arg_E3_4, arg_E3_5, arg_E3_6, newColor, 1.5f);
						Main.dust[num].noGravity = true;
					}
				}
				else
				{
					if (this.type == 51)
					{
						Main.PlaySound(0, (int)this.position.X, (int)this.position.Y, 1);
						for (int k = 0; k < 5; k++)
						{
							Vector2 arg_172_0 = new Vector2(this.position.X, this.position.Y);
							int arg_172_1 = this.width;
							int arg_172_2 = this.height;
							int arg_172_3 = 0;
							float arg_172_4 = 0f;
							float arg_172_5 = 0f;
							int arg_172_6 = 0;
							Color newColor = default(Color);
							Dust.NewDust(arg_172_0, arg_172_1, arg_172_2, arg_172_3, arg_172_4, arg_172_5, arg_172_6, newColor, 0.7f);
						}
					}
					else
					{
						if (this.type == 2)
						{
							Main.PlaySound(0, (int)this.position.X, (int)this.position.Y, 1);
							for (int l = 0; l < 20; l++)
							{
								Vector2 arg_1F5_0 = new Vector2(this.position.X, this.position.Y);
								int arg_1F5_1 = this.width;
								int arg_1F5_2 = this.height;
								int arg_1F5_3 = 6;
								float arg_1F5_4 = 0f;
								float arg_1F5_5 = 0f;
								int arg_1F5_6 = 100;
								Color newColor = default(Color);
								Dust.NewDust(arg_1F5_0, arg_1F5_1, arg_1F5_2, arg_1F5_3, arg_1F5_4, arg_1F5_5, arg_1F5_6, newColor, 1f);
							}
						}
						else
						{
							if (this.type == 3 || this.type == 48 || this.type == 54)
							{
								Main.PlaySound(0, (int)this.position.X, (int)this.position.Y, 1);
								for (int m = 0; m < 10; m++)
								{
									Vector2 arg_2AA_0 = new Vector2(this.position.X, this.position.Y);
									int arg_2AA_1 = this.width;
									int arg_2AA_2 = this.height;
									int arg_2AA_3 = 1;
									float arg_2AA_4 = this.velocity.X * 0.1f;
									float arg_2AA_5 = this.velocity.Y * 0.1f;
									int arg_2AA_6 = 0;
									Color newColor = default(Color);
									Dust.NewDust(arg_2AA_0, arg_2AA_1, arg_2AA_2, arg_2AA_3, arg_2AA_4, arg_2AA_5, arg_2AA_6, newColor, 0.75f);
								}
							}
							else
							{
								if (this.type == 4)
								{
									Main.PlaySound(0, (int)this.position.X, (int)this.position.Y, 1);
									for (int n = 0; n < 10; n++)
									{
										Vector2 arg_338_0 = new Vector2(this.position.X, this.position.Y);
										int arg_338_1 = this.width;
										int arg_338_2 = this.height;
										int arg_338_3 = 14;
										float arg_338_4 = 0f;
										float arg_338_5 = 0f;
										int arg_338_6 = 150;
										Color newColor = default(Color);
										Dust.NewDust(arg_338_0, arg_338_1, arg_338_2, arg_338_3, arg_338_4, arg_338_5, arg_338_6, newColor, 1.1f);
									}
								}
								else
								{
									if (this.type == 5)
									{
										Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 10);
										for (int num2 = 0; num2 < 60; num2++)
										{
											Vector2 arg_3CA_0 = this.position;
											int arg_3CA_1 = this.width;
											int arg_3CA_2 = this.height;
											int arg_3CA_3 = 15;
											float arg_3CA_4 = this.velocity.X * 0.5f;
											float arg_3CA_5 = this.velocity.Y * 0.5f;
											int arg_3CA_6 = 150;
											Color newColor = default(Color);
											Dust.NewDust(arg_3CA_0, arg_3CA_1, arg_3CA_2, arg_3CA_3, arg_3CA_4, arg_3CA_5, arg_3CA_6, newColor, 1.5f);
										}
									}
									else
									{
										if (this.type == 9 || this.type == 12)
										{
											Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 10);
											for (int num3 = 0; num3 < 10; num3++)
											{
												Vector2 arg_467_0 = this.position;
												int arg_467_1 = this.width;
												int arg_467_2 = this.height;
												int arg_467_3 = 15;
												float arg_467_4 = this.velocity.X * 0.1f;
												float arg_467_5 = this.velocity.Y * 0.1f;
												int arg_467_6 = 150;
												Color newColor = default(Color);
												Dust.NewDust(arg_467_0, arg_467_1, arg_467_2, arg_467_3, arg_467_4, arg_467_5, arg_467_6, newColor, 1.2f);
											}
											for (int num4 = 0; num4 < 3; num4++)
											{
												Gore.NewGore(this.position, new Vector2(this.velocity.X * 0.05f, this.velocity.Y * 0.05f), Main.rand.Next(16, 18), 1f);
											}
											if (this.type == 12 && this.damage < 100)
											{
												for (int num5 = 0; num5 < 10; num5++)
												{
													Vector2 arg_538_0 = this.position;
													int arg_538_1 = this.width;
													int arg_538_2 = this.height;
													int arg_538_3 = 15;
													float arg_538_4 = this.velocity.X * 0.1f;
													float arg_538_5 = this.velocity.Y * 0.1f;
													int arg_538_6 = 150;
													Color newColor = default(Color);
													Dust.NewDust(arg_538_0, arg_538_1, arg_538_2, arg_538_3, arg_538_4, arg_538_5, arg_538_6, newColor, 1.2f);
												}
												for (int num6 = 0; num6 < 3; num6++)
												{
													Gore.NewGore(this.position, new Vector2(this.velocity.X * 0.05f, this.velocity.Y * 0.05f), Main.rand.Next(16, 18), 1f);
												}
											}
										}
										else
										{
											if (this.type == 14 || this.type == 20 || this.type == 36)
											{
												Collision.HitTiles(this.position, this.velocity, this.width, this.height);
												Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 10);
											}
											else
											{
												if (this.type == 15 || this.type == 34)
												{
													Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 10);
													for (int num7 = 0; num7 < 20; num7++)
													{
														Vector2 arg_6A1_0 = new Vector2(this.position.X, this.position.Y);
														int arg_6A1_1 = this.width;
														int arg_6A1_2 = this.height;
														int arg_6A1_3 = 6;
														float arg_6A1_4 = -this.velocity.X * 0.2f;
														float arg_6A1_5 = -this.velocity.Y * 0.2f;
														int arg_6A1_6 = 100;
														Color newColor = default(Color);
														int num8 = Dust.NewDust(arg_6A1_0, arg_6A1_1, arg_6A1_2, arg_6A1_3, arg_6A1_4, arg_6A1_5, arg_6A1_6, newColor, 2f);
														Main.dust[num8].noGravity = true;
														Dust expr_6BE = Main.dust[num8];
														expr_6BE.velocity *= 2f;
														Vector2 arg_730_0 = new Vector2(this.position.X, this.position.Y);
														int arg_730_1 = this.width;
														int arg_730_2 = this.height;
														int arg_730_3 = 6;
														float arg_730_4 = -this.velocity.X * 0.2f;
														float arg_730_5 = -this.velocity.Y * 0.2f;
														int arg_730_6 = 100;
														newColor = default(Color);
														num8 = Dust.NewDust(arg_730_0, arg_730_1, arg_730_2, arg_730_3, arg_730_4, arg_730_5, arg_730_6, newColor, 1f);
														Dust expr_73F = Main.dust[num8];
														expr_73F.velocity *= 2f;
													}
												}
												else
												{
													if (this.type == 16)
													{
														Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 10);
														for (int num9 = 0; num9 < 20; num9++)
														{
															Vector2 arg_7F9_0 = new Vector2(this.position.X - this.velocity.X, this.position.Y - this.velocity.Y);
															int arg_7F9_1 = this.width;
															int arg_7F9_2 = this.height;
															int arg_7F9_3 = 15;
															float arg_7F9_4 = 0f;
															float arg_7F9_5 = 0f;
															int arg_7F9_6 = 100;
															Color newColor = default(Color);
															int num10 = Dust.NewDust(arg_7F9_0, arg_7F9_1, arg_7F9_2, arg_7F9_3, arg_7F9_4, arg_7F9_5, arg_7F9_6, newColor, 2f);
															Main.dust[num10].noGravity = true;
															Dust expr_816 = Main.dust[num10];
															expr_816.velocity *= 2f;
															Vector2 arg_887_0 = new Vector2(this.position.X - this.velocity.X, this.position.Y - this.velocity.Y);
															int arg_887_1 = this.width;
															int arg_887_2 = this.height;
															int arg_887_3 = 15;
															float arg_887_4 = 0f;
															float arg_887_5 = 0f;
															int arg_887_6 = 100;
															newColor = default(Color);
															num10 = Dust.NewDust(arg_887_0, arg_887_1, arg_887_2, arg_887_3, arg_887_4, arg_887_5, arg_887_6, newColor, 1f);
														}
													}
													else
													{
														if (this.type == 17)
														{
															Main.PlaySound(0, (int)this.position.X, (int)this.position.Y, 1);
															for (int num11 = 0; num11 < 5; num11++)
															{
																Vector2 arg_912_0 = new Vector2(this.position.X, this.position.Y);
																int arg_912_1 = this.width;
																int arg_912_2 = this.height;
																int arg_912_3 = 0;
																float arg_912_4 = 0f;
																float arg_912_5 = 0f;
																int arg_912_6 = 0;
																Color newColor = default(Color);
																Dust.NewDust(arg_912_0, arg_912_1, arg_912_2, arg_912_3, arg_912_4, arg_912_5, arg_912_6, newColor, 1f);
															}
														}
														else
														{
															if (this.type == 31 || this.type == 42)
															{
																Main.PlaySound(0, (int)this.position.X, (int)this.position.Y, 1);
																for (int num12 = 0; num12 < 5; num12++)
																{
																	Vector2 arg_9A6_0 = new Vector2(this.position.X, this.position.Y);
																	int arg_9A6_1 = this.width;
																	int arg_9A6_2 = this.height;
																	int arg_9A6_3 = 32;
																	float arg_9A6_4 = 0f;
																	float arg_9A6_5 = 0f;
																	int arg_9A6_6 = 0;
																	Color newColor = default(Color);
																	int num13 = Dust.NewDust(arg_9A6_0, arg_9A6_1, arg_9A6_2, arg_9A6_3, arg_9A6_4, arg_9A6_5, arg_9A6_6, newColor, 1f);
																	Dust expr_9B5 = Main.dust[num13];
																	expr_9B5.velocity *= 0.6f;
																}
															}
															else
															{
																if (this.type == 39)
																{
																	Main.PlaySound(0, (int)this.position.X, (int)this.position.Y, 1);
																	for (int num14 = 0; num14 < 5; num14++)
																	{
																		Vector2 arg_A4E_0 = new Vector2(this.position.X, this.position.Y);
																		int arg_A4E_1 = this.width;
																		int arg_A4E_2 = this.height;
																		int arg_A4E_3 = 38;
																		float arg_A4E_4 = 0f;
																		float arg_A4E_5 = 0f;
																		int arg_A4E_6 = 0;
																		Color newColor = default(Color);
																		int num15 = Dust.NewDust(arg_A4E_0, arg_A4E_1, arg_A4E_2, arg_A4E_3, arg_A4E_4, arg_A4E_5, arg_A4E_6, newColor, 1f);
																		Dust expr_A5D = Main.dust[num15];
																		expr_A5D.velocity *= 0.6f;
																	}
																}
																else
																{
																	if (this.type == 40)
																	{
																		Main.PlaySound(0, (int)this.position.X, (int)this.position.Y, 1);
																		for (int num16 = 0; num16 < 5; num16++)
																		{
																			Vector2 arg_AF6_0 = new Vector2(this.position.X, this.position.Y);
																			int arg_AF6_1 = this.width;
																			int arg_AF6_2 = this.height;
																			int arg_AF6_3 = 36;
																			float arg_AF6_4 = 0f;
																			float arg_AF6_5 = 0f;
																			int arg_AF6_6 = 0;
																			Color newColor = default(Color);
																			int num17 = Dust.NewDust(arg_AF6_0, arg_AF6_1, arg_AF6_2, arg_AF6_3, arg_AF6_4, arg_AF6_5, arg_AF6_6, newColor, 1f);
																			Dust expr_B05 = Main.dust[num17];
																			expr_B05.velocity *= 0.6f;
																		}
																	}
																	else
																	{
																		if (this.type == 21)
																		{
																			Main.PlaySound(0, (int)this.position.X, (int)this.position.Y, 1);
																			for (int num18 = 0; num18 < 10; num18++)
																			{
																				Vector2 arg_B9B_0 = new Vector2(this.position.X, this.position.Y);
																				int arg_B9B_1 = this.width;
																				int arg_B9B_2 = this.height;
																				int arg_B9B_3 = 26;
																				float arg_B9B_4 = 0f;
																				float arg_B9B_5 = 0f;
																				int arg_B9B_6 = 0;
																				Color newColor = default(Color);
																				Dust.NewDust(arg_B9B_0, arg_B9B_1, arg_B9B_2, arg_B9B_3, arg_B9B_4, arg_B9B_5, arg_B9B_6, newColor, 0.8f);
																			}
																		}
																		else
																		{
																			if (this.type == 24)
																			{
																				for (int num19 = 0; num19 < 10; num19++)
																				{
																					Vector2 arg_C1B_0 = new Vector2(this.position.X, this.position.Y);
																					int arg_C1B_1 = this.width;
																					int arg_C1B_2 = this.height;
																					int arg_C1B_3 = 1;
																					float arg_C1B_4 = this.velocity.X * 0.1f;
																					float arg_C1B_5 = this.velocity.Y * 0.1f;
																					int arg_C1B_6 = 0;
																					Color newColor = default(Color);
																					Dust.NewDust(arg_C1B_0, arg_C1B_1, arg_C1B_2, arg_C1B_3, arg_C1B_4, arg_C1B_5, arg_C1B_6, newColor, 0.75f);
																				}
																			}
																			else
																			{
																				if (this.type == 27)
																				{
																					Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 10);
																					for (int num20 = 0; num20 < 30; num20++)
																					{
																						Vector2 arg_CC3_0 = new Vector2(this.position.X, this.position.Y);
																						int arg_CC3_1 = this.width;
																						int arg_CC3_2 = this.height;
																						int arg_CC3_3 = 29;
																						float arg_CC3_4 = this.velocity.X * 0.1f;
																						float arg_CC3_5 = this.velocity.Y * 0.1f;
																						int arg_CC3_6 = 100;
																						Color newColor = default(Color);
																						int num21 = Dust.NewDust(arg_CC3_0, arg_CC3_1, arg_CC3_2, arg_CC3_3, arg_CC3_4, arg_CC3_5, arg_CC3_6, newColor, 3f);
																						Main.dust[num21].noGravity = true;
																						Vector2 arg_D34_0 = new Vector2(this.position.X, this.position.Y);
																						int arg_D34_1 = this.width;
																						int arg_D34_2 = this.height;
																						int arg_D34_3 = 29;
																						float arg_D34_4 = this.velocity.X * 0.1f;
																						float arg_D34_5 = this.velocity.Y * 0.1f;
																						int arg_D34_6 = 100;
																						newColor = default(Color);
																						Dust.NewDust(arg_D34_0, arg_D34_1, arg_D34_2, arg_D34_3, arg_D34_4, arg_D34_5, arg_D34_6, newColor, 2f);
																					}
																				}
																				else
																				{
																					if (this.type == 38)
																					{
																						for (int num22 = 0; num22 < 10; num22++)
																						{
																							Vector2 arg_DB8_0 = new Vector2(this.position.X, this.position.Y);
																							int arg_DB8_1 = this.width;
																							int arg_DB8_2 = this.height;
																							int arg_DB8_3 = 42;
																							float arg_DB8_4 = this.velocity.X * 0.1f;
																							float arg_DB8_5 = this.velocity.Y * 0.1f;
																							int arg_DB8_6 = 0;
																							Color newColor = default(Color);
																							Dust.NewDust(arg_DB8_0, arg_DB8_1, arg_DB8_2, arg_DB8_3, arg_DB8_4, arg_DB8_5, arg_DB8_6, newColor, 1f);
																						}
																					}
																					else
																					{
																						if (this.type == 44 || this.type == 45)
																						{
																							Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 10);
																							for (int num23 = 0; num23 < 30; num23++)
																							{
																								Vector2 arg_E5E_0 = new Vector2(this.position.X, this.position.Y);
																								int arg_E5E_1 = this.width;
																								int arg_E5E_2 = this.height;
																								int arg_E5E_3 = 27;
																								float arg_E5E_4 = this.velocity.X;
																								float arg_E5E_5 = this.velocity.Y;
																								int arg_E5E_6 = 100;
																								Color newColor = default(Color);
																								int num24 = Dust.NewDust(arg_E5E_0, arg_E5E_1, arg_E5E_2, arg_E5E_3, arg_E5E_4, arg_E5E_5, arg_E5E_6, newColor, 1.7f);
																								Main.dust[num24].noGravity = true;
																								Vector2 arg_EC3_0 = new Vector2(this.position.X, this.position.Y);
																								int arg_EC3_1 = this.width;
																								int arg_EC3_2 = this.height;
																								int arg_EC3_3 = 27;
																								float arg_EC3_4 = this.velocity.X;
																								float arg_EC3_5 = this.velocity.Y;
																								int arg_EC3_6 = 100;
																								newColor = default(Color);
																								Dust.NewDust(arg_EC3_0, arg_EC3_1, arg_EC3_2, arg_EC3_3, arg_EC3_4, arg_EC3_5, arg_EC3_6, newColor, 1f);
																							}
																						}
																						else
																						{
																							if (this.type == 41)
																							{
																								Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 14);
																								for (int num25 = 0; num25 < 10; num25++)
																								{
																									Vector2 arg_F53_0 = new Vector2(this.position.X, this.position.Y);
																									int arg_F53_1 = this.width;
																									int arg_F53_2 = this.height;
																									int arg_F53_3 = 31;
																									float arg_F53_4 = 0f;
																									float arg_F53_5 = 0f;
																									int arg_F53_6 = 100;
																									Color newColor = default(Color);
																									Dust.NewDust(arg_F53_0, arg_F53_1, arg_F53_2, arg_F53_3, arg_F53_4, arg_F53_5, arg_F53_6, newColor, 1.5f);
																								}
																								for (int num26 = 0; num26 < 5; num26++)
																								{
																									Vector2 arg_FB0_0 = new Vector2(this.position.X, this.position.Y);
																									int arg_FB0_1 = this.width;
																									int arg_FB0_2 = this.height;
																									int arg_FB0_3 = 6;
																									float arg_FB0_4 = 0f;
																									float arg_FB0_5 = 0f;
																									int arg_FB0_6 = 100;
																									Color newColor = default(Color);
																									int num27 = Dust.NewDust(arg_FB0_0, arg_FB0_1, arg_FB0_2, arg_FB0_3, arg_FB0_4, arg_FB0_5, arg_FB0_6, newColor, 2.5f);
																									Main.dust[num27].noGravity = true;
																									Dust expr_FCD = Main.dust[num27];
																									expr_FCD.velocity *= 3f;
																									Vector2 arg_1025_0 = new Vector2(this.position.X, this.position.Y);
																									int arg_1025_1 = this.width;
																									int arg_1025_2 = this.height;
																									int arg_1025_3 = 6;
																									float arg_1025_4 = 0f;
																									float arg_1025_5 = 0f;
																									int arg_1025_6 = 100;
																									newColor = default(Color);
																									num27 = Dust.NewDust(arg_1025_0, arg_1025_1, arg_1025_2, arg_1025_3, arg_1025_4, arg_1025_5, arg_1025_6, newColor, 1.5f);
																									Dust expr_1034 = Main.dust[num27];
																									expr_1034.velocity *= 2f;
																								}
																								Vector2 arg_108F_0 = new Vector2(this.position.X, this.position.Y);
																								Vector2 vector = default(Vector2);
																								int num28 = Gore.NewGore(arg_108F_0, vector, Main.rand.Next(61, 64), 1f);
																								Gore expr_109E = Main.gore[num28];
																								expr_109E.velocity *= 0.4f;
																								Gore expr_10C0_cp_0 = Main.gore[num28];
																								expr_10C0_cp_0.velocity.X = expr_10C0_cp_0.velocity.X + (float)Main.rand.Next(-10, 11) * 0.1f;
																								Gore expr_10EE_cp_0 = Main.gore[num28];
																								expr_10EE_cp_0.velocity.Y = expr_10EE_cp_0.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.1f;
																								Vector2 arg_1147_0 = new Vector2(this.position.X, this.position.Y);
																								vector = default(Vector2);
																								num28 = Gore.NewGore(arg_1147_0, vector, Main.rand.Next(61, 64), 1f);
																								Gore expr_1156 = Main.gore[num28];
																								expr_1156.velocity *= 0.4f;
																								Gore expr_1178_cp_0 = Main.gore[num28];
																								expr_1178_cp_0.velocity.X = expr_1178_cp_0.velocity.X + (float)Main.rand.Next(-10, 11) * 0.1f;
																								Gore expr_11A6_cp_0 = Main.gore[num28];
																								expr_11A6_cp_0.velocity.Y = expr_11A6_cp_0.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.1f;
																								if (this.owner == Main.myPlayer)
																								{
																									this.penetrate = -1;
																									this.position.X = this.position.X + (float)(this.width / 2);
																									this.position.Y = this.position.Y + (float)(this.height / 2);
																									this.width = 64;
																									this.height = 64;
																									this.position.X = this.position.X - (float)(this.width / 2);
																									this.position.Y = this.position.Y - (float)(this.height / 2);
																									this.Damage();
																								}
																							}
																							else
																							{
																								if (this.type == 28 || this.type == 30 || this.type == 37)
																								{
																									Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 14);
																									this.position.X = this.position.X + (float)(this.width / 2);
																									this.position.Y = this.position.Y + (float)(this.height / 2);
																									this.width = 22;
																									this.height = 22;
																									this.position.X = this.position.X - (float)(this.width / 2);
																									this.position.Y = this.position.Y - (float)(this.height / 2);
																									for (int num29 = 0; num29 < 20; num29++)
																									{
																										Vector2 arg_136B_0 = new Vector2(this.position.X, this.position.Y);
																										int arg_136B_1 = this.width;
																										int arg_136B_2 = this.height;
																										int arg_136B_3 = 31;
																										float arg_136B_4 = 0f;
																										float arg_136B_5 = 0f;
																										int arg_136B_6 = 100;
																										Color newColor = default(Color);
																										int num30 = Dust.NewDust(arg_136B_0, arg_136B_1, arg_136B_2, arg_136B_3, arg_136B_4, arg_136B_5, arg_136B_6, newColor, 1.5f);
																										Dust expr_137A = Main.dust[num30];
																										expr_137A.velocity *= 1.4f;
																									}
																									for (int num31 = 0; num31 < 10; num31++)
																									{
																										Vector2 arg_13E6_0 = new Vector2(this.position.X, this.position.Y);
																										int arg_13E6_1 = this.width;
																										int arg_13E6_2 = this.height;
																										int arg_13E6_3 = 6;
																										float arg_13E6_4 = 0f;
																										float arg_13E6_5 = 0f;
																										int arg_13E6_6 = 100;
																										Color newColor = default(Color);
																										int num32 = Dust.NewDust(arg_13E6_0, arg_13E6_1, arg_13E6_2, arg_13E6_3, arg_13E6_4, arg_13E6_5, arg_13E6_6, newColor, 2.5f);
																										Main.dust[num32].noGravity = true;
																										Dust expr_1403 = Main.dust[num32];
																										expr_1403.velocity *= 5f;
																										Vector2 arg_145B_0 = new Vector2(this.position.X, this.position.Y);
																										int arg_145B_1 = this.width;
																										int arg_145B_2 = this.height;
																										int arg_145B_3 = 6;
																										float arg_145B_4 = 0f;
																										float arg_145B_5 = 0f;
																										int arg_145B_6 = 100;
																										newColor = default(Color);
																										num32 = Dust.NewDust(arg_145B_0, arg_145B_1, arg_145B_2, arg_145B_3, arg_145B_4, arg_145B_5, arg_145B_6, newColor, 1.5f);
																										Dust expr_146A = Main.dust[num32];
																										expr_146A.velocity *= 3f;
																									}
																									Vector2 arg_14C6_0 = new Vector2(this.position.X, this.position.Y);
																									Vector2 vector = default(Vector2);
																									int num33 = Gore.NewGore(arg_14C6_0, vector, Main.rand.Next(61, 64), 1f);
																									Gore expr_14D5 = Main.gore[num33];
																									expr_14D5.velocity *= 0.4f;
																									Gore expr_14F7_cp_0 = Main.gore[num33];
																									expr_14F7_cp_0.velocity.X = expr_14F7_cp_0.velocity.X + 1f;
																									Gore expr_1515_cp_0 = Main.gore[num33];
																									expr_1515_cp_0.velocity.Y = expr_1515_cp_0.velocity.Y + 1f;
																									Vector2 arg_155E_0 = new Vector2(this.position.X, this.position.Y);
																									vector = default(Vector2);
																									num33 = Gore.NewGore(arg_155E_0, vector, Main.rand.Next(61, 64), 1f);
																									Gore expr_156D = Main.gore[num33];
																									expr_156D.velocity *= 0.4f;
																									Gore expr_158F_cp_0 = Main.gore[num33];
																									expr_158F_cp_0.velocity.X = expr_158F_cp_0.velocity.X - 1f;
																									Gore expr_15AD_cp_0 = Main.gore[num33];
																									expr_15AD_cp_0.velocity.Y = expr_15AD_cp_0.velocity.Y + 1f;
																									Vector2 arg_15F6_0 = new Vector2(this.position.X, this.position.Y);
																									vector = default(Vector2);
																									num33 = Gore.NewGore(arg_15F6_0, vector, Main.rand.Next(61, 64), 1f);
																									Gore expr_1605 = Main.gore[num33];
																									expr_1605.velocity *= 0.4f;
																									Gore expr_1627_cp_0 = Main.gore[num33];
																									expr_1627_cp_0.velocity.X = expr_1627_cp_0.velocity.X + 1f;
																									Gore expr_1645_cp_0 = Main.gore[num33];
																									expr_1645_cp_0.velocity.Y = expr_1645_cp_0.velocity.Y - 1f;
																									Vector2 arg_168E_0 = new Vector2(this.position.X, this.position.Y);
																									vector = default(Vector2);
																									num33 = Gore.NewGore(arg_168E_0, vector, Main.rand.Next(61, 64), 1f);
																									Gore expr_169D = Main.gore[num33];
																									expr_169D.velocity *= 0.4f;
																									Gore expr_16BF_cp_0 = Main.gore[num33];
																									expr_16BF_cp_0.velocity.X = expr_16BF_cp_0.velocity.X - 1f;
																									Gore expr_16DD_cp_0 = Main.gore[num33];
																									expr_16DD_cp_0.velocity.Y = expr_16DD_cp_0.velocity.Y - 1f;
																								}
																								else
																								{
																									if (this.type == 29)
																									{
																										Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 14);
																										this.position.X = this.position.X + (float)(this.width / 2);
																										this.position.Y = this.position.Y + (float)(this.height / 2);
																										this.width = 200;
																										this.height = 200;
																										this.position.X = this.position.X - (float)(this.width / 2);
																										this.position.Y = this.position.Y - (float)(this.height / 2);
																										for (int num34 = 0; num34 < 50; num34++)
																										{
																											Vector2 arg_17EB_0 = new Vector2(this.position.X, this.position.Y);
																											int arg_17EB_1 = this.width;
																											int arg_17EB_2 = this.height;
																											int arg_17EB_3 = 31;
																											float arg_17EB_4 = 0f;
																											float arg_17EB_5 = 0f;
																											int arg_17EB_6 = 100;
																											Color newColor = default(Color);
																											int num35 = Dust.NewDust(arg_17EB_0, arg_17EB_1, arg_17EB_2, arg_17EB_3, arg_17EB_4, arg_17EB_5, arg_17EB_6, newColor, 2f);
																											Dust expr_17FA = Main.dust[num35];
																											expr_17FA.velocity *= 1.4f;
																										}
																										for (int num36 = 0; num36 < 80; num36++)
																										{
																											Vector2 arg_1866_0 = new Vector2(this.position.X, this.position.Y);
																											int arg_1866_1 = this.width;
																											int arg_1866_2 = this.height;
																											int arg_1866_3 = 6;
																											float arg_1866_4 = 0f;
																											float arg_1866_5 = 0f;
																											int arg_1866_6 = 100;
																											Color newColor = default(Color);
																											int num37 = Dust.NewDust(arg_1866_0, arg_1866_1, arg_1866_2, arg_1866_3, arg_1866_4, arg_1866_5, arg_1866_6, newColor, 3f);
																											Main.dust[num37].noGravity = true;
																											Dust expr_1883 = Main.dust[num37];
																											expr_1883.velocity *= 5f;
																											Vector2 arg_18DB_0 = new Vector2(this.position.X, this.position.Y);
																											int arg_18DB_1 = this.width;
																											int arg_18DB_2 = this.height;
																											int arg_18DB_3 = 6;
																											float arg_18DB_4 = 0f;
																											float arg_18DB_5 = 0f;
																											int arg_18DB_6 = 100;
																											newColor = default(Color);
																											num37 = Dust.NewDust(arg_18DB_0, arg_18DB_1, arg_18DB_2, arg_18DB_3, arg_18DB_4, arg_18DB_5, arg_18DB_6, newColor, 2f);
																											Dust expr_18EA = Main.dust[num37];
																											expr_18EA.velocity *= 3f;
																										}
																										for (int num38 = 0; num38 < 2; num38++)
																										{
																											Vector2 arg_196E_0 = new Vector2(this.position.X + (float)(this.width / 2) - 24f, this.position.Y + (float)(this.height / 2) - 24f);
																											Vector2 vector = default(Vector2);
																											int num39 = Gore.NewGore(arg_196E_0, vector, Main.rand.Next(61, 64), 1f);
																											Main.gore[num39].scale = 1.5f;
																											Gore expr_1994_cp_0 = Main.gore[num39];
																											expr_1994_cp_0.velocity.X = expr_1994_cp_0.velocity.X + 1.5f;
																											Gore expr_19B2_cp_0 = Main.gore[num39];
																											expr_19B2_cp_0.velocity.Y = expr_19B2_cp_0.velocity.Y + 1.5f;
																											Vector2 arg_1A1B_0 = new Vector2(this.position.X + (float)(this.width / 2) - 24f, this.position.Y + (float)(this.height / 2) - 24f);
																											vector = default(Vector2);
																											num39 = Gore.NewGore(arg_1A1B_0, vector, Main.rand.Next(61, 64), 1f);
																											Main.gore[num39].scale = 1.5f;
																											Gore expr_1A41_cp_0 = Main.gore[num39];
																											expr_1A41_cp_0.velocity.X = expr_1A41_cp_0.velocity.X - 1.5f;
																											Gore expr_1A5F_cp_0 = Main.gore[num39];
																											expr_1A5F_cp_0.velocity.Y = expr_1A5F_cp_0.velocity.Y + 1.5f;
																											Vector2 arg_1AC8_0 = new Vector2(this.position.X + (float)(this.width / 2) - 24f, this.position.Y + (float)(this.height / 2) - 24f);
																											vector = default(Vector2);
																											num39 = Gore.NewGore(arg_1AC8_0, vector, Main.rand.Next(61, 64), 1f);
																											Main.gore[num39].scale = 1.5f;
																											Gore expr_1AEE_cp_0 = Main.gore[num39];
																											expr_1AEE_cp_0.velocity.X = expr_1AEE_cp_0.velocity.X + 1.5f;
																											Gore expr_1B0C_cp_0 = Main.gore[num39];
																											expr_1B0C_cp_0.velocity.Y = expr_1B0C_cp_0.velocity.Y - 1.5f;
																											Vector2 arg_1B75_0 = new Vector2(this.position.X + (float)(this.width / 2) - 24f, this.position.Y + (float)(this.height / 2) - 24f);
																											vector = default(Vector2);
																											num39 = Gore.NewGore(arg_1B75_0, vector, Main.rand.Next(61, 64), 1f);
																											Main.gore[num39].scale = 1.5f;
																											Gore expr_1B9B_cp_0 = Main.gore[num39];
																											expr_1B9B_cp_0.velocity.X = expr_1B9B_cp_0.velocity.X - 1.5f;
																											Gore expr_1BB9_cp_0 = Main.gore[num39];
																											expr_1BB9_cp_0.velocity.Y = expr_1BB9_cp_0.velocity.Y - 1.5f;
																										}
																										this.position.X = this.position.X + (float)(this.width / 2);
																										this.position.Y = this.position.Y + (float)(this.height / 2);
																										this.width = 10;
																										this.height = 10;
																										this.position.X = this.position.X - (float)(this.width / 2);
																										this.position.Y = this.position.Y - (float)(this.height / 2);
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			if (this.owner == Main.myPlayer)
			{
				if (this.type == 28 || this.type == 29 || this.type == 37)
				{
					int num40 = 3;
					if (this.type == 29)
					{
						num40 = 7;
					}
					int num41 = (int)(this.position.X / 16f - (float)num40);
					int num42 = (int)(this.position.X / 16f + (float)num40);
					int num43 = (int)(this.position.Y / 16f - (float)num40);
					int num44 = (int)(this.position.Y / 16f + (float)num40);
					if (num41 < 0)
					{
						num41 = 0;
					}
					if (num42 > Main.maxTilesX)
					{
						num42 = Main.maxTilesX;
					}
					if (num43 < 0)
					{
						num43 = 0;
					}
					if (num44 > Main.maxTilesY)
					{
						num44 = Main.maxTilesY;
					}
					bool flag = false;
					for (int num45 = num41; num45 <= num42; num45++)
					{
						for (int num46 = num43; num46 <= num44; num46++)
						{
							float num47 = Math.Abs((float)num45 - this.position.X / 16f);
							float num48 = Math.Abs((float)num46 - this.position.Y / 16f);
							double num49 = Math.Sqrt((double)(num47 * num47 + num48 * num48));
							if (num49 < (double)num40 && Main.tile[num45, num46] != null && Main.tile[num45, num46].wall == 0)
							{
								flag = true;
								break;
							}
						}
					}
					for (int num50 = num41; num50 <= num42; num50++)
					{
						for (int num51 = num43; num51 <= num44; num51++)
						{
							float num52 = Math.Abs((float)num50 - this.position.X / 16f);
							float num53 = Math.Abs((float)num51 - this.position.Y / 16f);
							double num54 = Math.Sqrt((double)(num52 * num52 + num53 * num53));
							if (num54 < (double)num40)
							{
								bool flag2 = true;
								if (Main.tile[num50, num51] != null && Main.tile[num50, num51].active)
								{
									flag2 = false;
									if (this.type == 28 || this.type == 37)
									{
										if (!Main.tileSolid[(int)Main.tile[num50, num51].type] || Main.tileSolidTop[(int)Main.tile[num50, num51].type] || Main.tile[num50, num51].type == 0 || Main.tile[num50, num51].type == 1 || Main.tile[num50, num51].type == 2 || Main.tile[num50, num51].type == 23 || Main.tile[num50, num51].type == 30 || Main.tile[num50, num51].type == 40 || Main.tile[num50, num51].type == 6 || Main.tile[num50, num51].type == 7 || Main.tile[num50, num51].type == 8 || Main.tile[num50, num51].type == 9 || Main.tile[num50, num51].type == 10 || Main.tile[num50, num51].type == 53 || Main.tile[num50, num51].type == 54 || Main.tile[num50, num51].type == 57 || Main.tile[num50, num51].type == 59 || Main.tile[num50, num51].type == 60 || Main.tile[num50, num51].type == 63 || Main.tile[num50, num51].type == 64 || Main.tile[num50, num51].type == 65 || Main.tile[num50, num51].type == 66 || Main.tile[num50, num51].type == 67 || Main.tile[num50, num51].type == 68 || Main.tile[num50, num51].type == 70 || Main.tile[num50, num51].type == 37)
										{
											flag2 = true;
										}
									}
									else
									{
										if (this.type == 29)
										{
											flag2 = true;
										}
									}
									if (Main.tileDungeon[(int)Main.tile[num50, num51].type] || Main.tile[num50, num51].type == 26 || Main.tile[num50, num51].type == 58 || Main.tile[num50, num51].type == 21)
									{
										flag2 = false;
									}
									if (flag2)
									{
										WorldGen.KillTile(num50, num51, false, false, false);
										if (!Main.tile[num50, num51].active && Main.netMode == 1)
										{
											NetMessage.SendData(17, -1, -1, "", 0, (float)num50, (float)num51, 0f, 0);
										}
									}
								}
								if (flag2 && Main.tile[num50, num51] != null && Main.tile[num50, num51].wall > 0 && flag)
								{
									WorldGen.KillWall(num50, num51, false);
									if (Main.tile[num50, num51].wall == 0 && Main.netMode == 1)
									{
										NetMessage.SendData(17, -1, -1, "", 2, (float)num50, (float)num51, 0f, 0);
									}
								}
							}
						}
					}
				}
				if (Main.netMode != 0)
				{
					NetMessage.SendData(29, -1, -1, "", this.identity, (float)this.owner, 0f, 0f, 0);
				}
				int num55 = -1;
				if (this.aiStyle == 10)
				{
					int num56 = (int)(this.position.X + (float)(this.width / 2)) / 16;
					int num57 = (int)(this.position.Y + (float)(this.width / 2)) / 16;
					int num58 = 0;
					int num59 = 2;
					if (this.type == 31)
					{
						num58 = 53;
						num59 = 0;
					}
					if (this.type == 42)
					{
						num58 = 53;
						num59 = 0;
					}
					else
					{
						if (this.type == 39)
						{
							num58 = 59;
							num59 = 176;
						}
						else
						{
							if (this.type == 40)
							{
								num58 = 57;
								num59 = 172;
							}
						}
					}
					if (!Main.tile[num56, num57].active)
					{
						WorldGen.PlaceTile(num56, num57, num58, false, true, -1, 0);
						if (Main.tile[num56, num57].active && (int)Main.tile[num56, num57].type == num58)
						{
							NetMessage.SendData(17, -1, -1, "", 1, (float)num56, (float)num57, (float)num58, 0);
						}
						else
						{
							if (num59 > 0)
							{
								num55 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, num59, 1, false);
							}
						}
					}
					else
					{
						if (num59 > 0)
						{
							num55 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, num59, 1, false);
						}
					}
				}
				if (this.type == 1 && Main.rand.Next(2) == 0)
				{
					num55 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 40, 1, false);
				}
				if (this.type == 2 && Main.rand.Next(2) == 0)
				{
					if (Main.rand.Next(3) == 0)
					{
						num55 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 41, 1, false);
					}
					else
					{
						num55 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 40, 1, false);
					}
				}
				if (this.type == 50 && Main.rand.Next(3) == 0)
				{
					num55 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 282, 1, false);
				}
				if (this.type == 53 && Main.rand.Next(3) == 0)
				{
					num55 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 286, 1, false);
				}
				if (this.type == 48 && Main.rand.Next(2) == 0)
				{
					num55 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 279, 1, false);
				}
				if (this.type == 54 && Main.rand.Next(2) == 0)
				{
					num55 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 287, 1, false);
				}
				if (this.type == 3 && Main.rand.Next(2) == 0)
				{
					num55 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 42, 1, false);
				}
				if (this.type == 4 && Main.rand.Next(2) == 0)
				{
					num55 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 47, 1, false);
				}
				if (this.type == 12 && this.damage > 100)
				{
					num55 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 75, 1, false);
				}
				if (this.type == 21 && Main.rand.Next(2) == 0)
				{
					num55 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, 154, 1, false);
				}
				if (Main.netMode == 1 && num55 >= 0)
				{
					NetMessage.SendData(21, -1, -1, "", num55, 0f, 0f, 0f, 0);
				}
			}
			this.active = false;
		}
		public Color GetAlpha(Color newColor)
		{
			int r;
			int g;
			int b;
			if (this.type == 9 || this.type == 15 || this.type == 34 || this.type == 50 || this.type == 53)
			{
				r = (int)newColor.R - this.alpha / 3;
				g = (int)newColor.G - this.alpha / 3;
				b = (int)newColor.B - this.alpha / 3;
			}
			else
			{
				if (this.type == 16 || this.type == 18 || this.type == 44 || this.type == 45)
				{
					r = (int)newColor.R;
					g = (int)newColor.G;
					b = (int)newColor.B;
				}
				else
				{
					r = (int)newColor.R - this.alpha;
					g = (int)newColor.G - this.alpha;
					b = (int)newColor.B - this.alpha;
				}
			}
			int num = (int)newColor.A - this.alpha;
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
	}
}
