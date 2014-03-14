using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace Freeria
{
	public class Player
	{
		public const int maxBuffs = 10;
		public bool male = true;
		public bool ghost;
		public int ghostFrame;
		public int ghostFrameCounter;
		public bool pvpDeath;
		public bool zoneDungeon;
		public bool zoneEvil;
		public bool zoneMeteor;
		public bool zoneJungle;
		public bool boneArmor;
		public float townNPCs;
		public Vector2 position;
		public Vector2 velocity;
		public Vector2 oldVelocity;
		public double headFrameCounter;
		public double bodyFrameCounter;
		public double legFrameCounter;
		public bool immune;
		public int immuneTime;
		public int immuneAlphaDirection;
		public int immuneAlpha;
		public int team;
		public bool hbLocked;
		public static int nameLen = 20;
		private float maxRegenDelay;
		public string chatText = "";
		public int sign = -1;
		public int chatShowTime;
		public float activeNPCs;
		public bool mouseInterface;
		public int changeItem = -1;
		public int selectedItem;
		public Item[] armor = new Item[11];
		public Item[] ammo = new Item[4];
		public int itemAnimation;
		public int itemAnimationMax;
		public int itemTime;
		public float itemRotation;
		public int itemWidth;
		public int itemHeight;
		public Vector2 itemLocation;
		public int[] buffType = new int[10];
		public int[] buffTime = new int[10];
		public int heldProj = -1;
		public int breathCD;
		public int breathMax = 200;
		public int breath = 200;
		public bool socialShadow;
		public string setBonus = "";
		public Item[] inventory = new Item[44];
		public Item[] bank = new Item[Chest.maxItems];
		public Item[] bank2 = new Item[Chest.maxItems];
		public float headRotation;
		public float bodyRotation;
		public float legRotation;
		public Vector2 headPosition;
		public Vector2 bodyPosition;
		public Vector2 legPosition;
		public Vector2 headVelocity;
		public Vector2 bodyVelocity;
		public Vector2 legVelocity;
		public static bool deadForGood = false;
		public bool dead;
		public int respawnTimer;
		public string name = "";
		public int attackCD;
		public int potionDelay;
		public byte difficulty;
		public bool wet;
		public byte wetCount;
		public bool lavaWet;
		public int hitTile;
		public int hitTileX;
		public int hitTileY;
		public int jump;
		public int head = -1;
		public int body = -1;
		public int legs = -1;
		public Rectangle headFrame;
		public Rectangle bodyFrame;
		public Rectangle legFrame;
		public Rectangle hairFrame;
		public bool controlLeft;
		public bool controlRight;
		public bool controlUp;
		public bool controlDown;
		public bool controlJump;
		public bool controlUseItem;
		public bool controlUseTile;
		public bool controlThrow;
		public bool controlInv;
		public bool controlHook;
		public bool releaseJump;
		public bool releaseUseItem;
		public bool releaseUseTile;
		public bool releaseInventory;
		public bool releaseHook;
		public bool releaseThrow;
		public bool releaseQuickMana;
		public bool releaseQuickHeal;
		public bool delayUseItem;
		public bool active;
		public int width = 20;
		public int height = 42;
		public int direction = 1;
		public bool showItemIcon;
		public int showItemIcon2;
		public int whoAmi;
		public int runSoundDelay;
		public float shadow;
		public float manaCost = 1f;
		public bool fireWalk;
		public Vector2[] shadowPos = new Vector2[3];
		public int shadowCount;
		public bool channel;
		public int step = -1;
		public int statDefense;
		public int statAttack;
		public int statLifeMax = 100;
		public int statLife = 100;
		public int statMana;
		public int statManaMax;
		public int statManaMax2;
		public int lifeRegen;
		public int lifeRegenCount;
		public int lifeRegenTime;
		public int manaRegen;
		public int manaRegenCount;
		public int manaRegenDelay;
		public bool manaRegenBuff;
		public bool noKnockback;
		public bool spaceGun;
		public float gravDir = 1f;
		public bool ammoCost80;
		public int stickyBreak;
		public bool lightOrb;
		public bool archery;
		public bool poisoned;
		public bool blind;
		public bool onFire;
		public bool noItems;
		public int meleeCrit = 4;
		public int rangedCrit = 4;
		public int magicCrit = 4;
		public float meleeDamage = 1f;
		public float rangedDamage = 1f;
		public float magicDamage = 1f;
		public float meleeSpeed = 1f;
		public float moveSpeed = 1f;
		public int SpawnX = -1;
		public int SpawnY = -1;
		public int[] spX = new int[200];
		public int[] spY = new int[200];
		public string[] spN = new string[200];
		public int[] spI = new int[200];
		public static int tileRangeX = 5;
		public static int tileRangeY = 4;
		private static int tileTargetX;
		private static int tileTargetY;
		private static int jumpHeight = 15;
		private static float jumpSpeed = 5.01f;
		public bool adjWater;
		public bool oldAdjWater;
		public bool[] adjTile = new bool[107];
		public bool[] oldAdjTile = new bool[107];
		private static int itemGrabRange = 38;
		private static float itemGrabSpeed = 0.45f;
		private static float itemGrabSpeedMax = 4f;
		public Color hairColor = new Color(215, 90, 55);
		public Color skinColor = new Color(255, 125, 90);
		public Color eyeColor = new Color(105, 90, 75);
		public Color shirtColor = new Color(175, 165, 140);
		public Color underShirtColor = new Color(160, 180, 215);
		public Color pantsColor = new Color(255, 230, 175);
		public Color shoeColor = new Color(160, 105, 60);
		public int hair;
		public bool hostile;
		public int accWatch;
		public int accDepthMeter;
		public bool accFlipper;
		public bool doubleJump;
		public bool jumpAgain;
		public bool spawnMax;
		public int[] grappling = new int[20];
		public int grapCount;
		public int rocketTime;
		public int rocketTimeMax = 7;
		public int rocketDelay;
		public int rocketDelay2;
		public bool rocketRelease;
		public bool rocketFrame;
		public bool rocketBoots;
		public bool canRocket;
		public bool jumpBoost;
		public bool noFallDmg;
		public int swimTime;
		public bool killGuide;
		public bool lavaImmune;
		public bool gills;
		public bool slowFall;
		public bool findTreasure;
		public bool invis;
		public bool detectCreature;
		public bool nightVision;
		public bool enemySpawns;
		public bool thorns;
		public bool waterWalk;
		public bool gravControl;
		public int chest = -1;
		public int chestX;
		public int chestY;
		public int talkNPC = -1;
		public int fallStart;
		public int slowCount;
		public void HealEffect(int healAmount)
		{
			CombatText.NewText(new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height), new Color(100, 255, 100, 255), string.Concat(healAmount), false);
			if (Main.netMode == 1 && this.whoAmi == Main.myPlayer)
			{
				NetMessage.SendData(35, -1, -1, "", this.whoAmi, (float)healAmount, 0f, 0f, 0);
			}
		}
		public void ManaEffect(int manaAmount)
		{
			CombatText.NewText(new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height), new Color(100, 100, 255, 255), string.Concat(manaAmount), false);
			if (Main.netMode == 1 && this.whoAmi == Main.myPlayer)
			{
				NetMessage.SendData(43, -1, -1, "", this.whoAmi, (float)manaAmount, 0f, 0f, 0);
			}
		}
		public static byte FindClosest(Vector2 Position, int Width, int Height)
		{
			byte result = 0;
			for (int i = 0; i < 255; i++)
			{
				if (Main.player[i].active)
				{
					result = (byte)i;
					break;
				}
			}
			float num = -1f;
			for (int j = 0; j < 255; j++)
			{
				if (Main.player[j].active && !Main.player[j].dead && (num == -1f || Math.Abs(Main.player[j].position.X + (float)(Main.player[j].width / 2) - Position.X + (float)(Width / 2)) + Math.Abs(Main.player[j].position.Y + (float)(Main.player[j].height / 2) - Position.Y + (float)(Height / 2)) < num))
				{
					num = Math.Abs(Main.player[j].position.X + (float)(Main.player[j].width / 2) - Position.X + (float)(Width / 2)) + Math.Abs(Main.player[j].position.Y + (float)(Main.player[j].height / 2) - Position.Y + (float)(Height / 2));
					result = (byte)j;
				}
			}
			return result;
		}
		public void checkArmor()
		{
		}
		public void toggleInv()
		{
			if (this.talkNPC >= 0)
			{
				this.talkNPC = -1;
				Main.npcChatText = "";
				Main.PlaySound(11, -1, -1, 1);
				return;
			}
			if (this.sign >= 0)
			{
				this.sign = -1;
				Main.editSign = false;
				Main.npcChatText = "";
				Main.PlaySound(11, -1, -1, 1);
				return;
			}
			if (!Main.playerInventory)
			{
				Recipe.FindRecipes();
				Main.playerInventory = true;
				Main.PlaySound(10, -1, -1, 1);
				return;
			}
			Main.playerInventory = false;
			Main.PlaySound(11, -1, -1, 1);
		}
		public void dropItemCheck()
		{
			if (!Main.craftGuide && Main.guideItem.type > 0)
			{
				int num = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, Main.guideItem.type, 1, false);
				Main.guideItem.position = Main.item[num].position;
				Main.item[num] = Main.guideItem;
				Main.guideItem = new Item();
				if (Main.netMode == 0)
				{
					Main.item[num].noGrabDelay = 100;
				}
				Main.item[num].velocity.Y = -2f;
				Main.item[num].velocity.X = (float)(4 * this.direction) + this.velocity.X;
				if (Main.netMode == 1)
				{
					NetMessage.SendData(21, -1, -1, "", num, 0f, 0f, 0f, 0);
				}
			}
			if ((this.controlThrow && this.releaseThrow && this.inventory[this.selectedItem].type > 0 && !Main.chatMode) || (((Main.mouseState.LeftButton == ButtonState.Pressed && !this.mouseInterface && Main.mouseLeftRelease) || !Main.playerInventory) && Main.mouseItem.type > 0))
			{
				Item item = new Item();
				bool flag = false;
				if (((Main.mouseState.LeftButton == ButtonState.Pressed && !this.mouseInterface && Main.mouseLeftRelease) || !Main.playerInventory) && Main.mouseItem.type > 0)
				{
					item = this.inventory[this.selectedItem];
					this.inventory[this.selectedItem] = Main.mouseItem;
					this.delayUseItem = true;
					this.controlUseItem = false;
					flag = true;
				}
				int num2 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, this.inventory[this.selectedItem].type, 1, false);
				if (!flag && this.inventory[this.selectedItem].type == 8 && this.inventory[this.selectedItem].stack > 1)
				{
					this.inventory[this.selectedItem].stack--;
				}
				else
				{
					this.inventory[this.selectedItem].position = Main.item[num2].position;
					Main.item[num2] = this.inventory[this.selectedItem];
					this.inventory[this.selectedItem] = new Item();
				}
				if (Main.netMode == 0)
				{
					Main.item[num2].noGrabDelay = 100;
				}
				Main.item[num2].velocity.Y = -2f;
				Main.item[num2].velocity.X = (float)(4 * this.direction) + this.velocity.X;
				if (((Main.mouseState.LeftButton == ButtonState.Pressed && !this.mouseInterface) || !Main.playerInventory) && Main.mouseItem.type > 0)
				{
					this.inventory[this.selectedItem] = item;
					Main.mouseItem = new Item();
				}
				else
				{
					this.itemAnimation = 10;
					this.itemAnimationMax = 10;
				}
				Recipe.FindRecipes();
				if (Main.netMode == 1)
				{
					NetMessage.SendData(21, -1, -1, "", num2, 0f, 0f, 0f, 0);
				}
			}
		}
		public void AddBuff(int type, int time, bool quiet = true)
		{
			if (!quiet && Main.netMode == 1)
			{
				NetMessage.SendData(55, -1, -1, "", this.whoAmi, (float)type, (float)time, 0f, 0);
			}
			int num = -1;
			for (int i = 0; i < 10; i++)
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
				for (int j = 0; j < 10; j++)
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
				for (int k = num2; k < 10; k++)
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
			for (int i = 0; i < 9; i++)
			{
				if (this.buffTime[i] == 0 || this.buffType[i] == 0)
				{
					for (int j = i + 1; j < 10; j++)
					{
						this.buffTime[j - 1] = this.buffTime[j];
						this.buffType[j - 1] = this.buffType[j];
						this.buffTime[j] = 0;
						this.buffType[j] = 0;
					}
				}
			}
		}
		public void QuickHeal()
		{
			if (this.noItems)
			{
				return;
			}
			if (this.statLife == this.statLifeMax || this.potionDelay > 0)
			{
				return;
			}
			for (int i = 0; i < 44; i++)
			{
				if (this.inventory[i].stack > 0 && this.inventory[i].type > 0 && this.inventory[i].potion && this.inventory[i].healLife > 0)
				{
					Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, this.inventory[i].useSound);
					if (this.inventory[i].potion)
					{
						this.potionDelay = Item.potionDelay;
						this.AddBuff(21, this.potionDelay, true);
					}
					this.statLife += this.inventory[i].healLife;
					this.statMana += this.inventory[i].healMana;
					if (this.statLife > this.statLifeMax)
					{
						this.statLife = this.statLifeMax;
					}
					if (this.statMana > this.statManaMax2)
					{
						this.statMana = this.statManaMax2;
					}
					if (this.inventory[i].healLife > 0 && Main.myPlayer == this.whoAmi)
					{
						this.HealEffect(this.inventory[i].healLife);
					}
					if (this.inventory[i].healMana > 0 && Main.myPlayer == this.whoAmi)
					{
						this.ManaEffect(this.inventory[i].healMana);
					}
					this.inventory[i].stack--;
					if (this.inventory[i].stack <= 0)
					{
						this.inventory[i].type = 0;
						this.inventory[i].name = "";
					}
					Recipe.FindRecipes();
					return;
				}
			}
		}
		public void QuickMana()
		{
			if (this.noItems)
			{
				return;
			}
			if (this.statMana == this.statManaMax2)
			{
				return;
			}
			for (int i = 0; i < 44; i++)
			{
				if (this.inventory[i].stack > 0 && this.inventory[i].type > 0 && this.inventory[i].healMana > 0 && (this.potionDelay == 0 || !this.inventory[i].potion))
				{
					Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, this.inventory[i].useSound);
					if (this.inventory[i].potion)
					{
						this.potionDelay = Item.potionDelay;
						this.AddBuff(21, this.potionDelay, true);
					}
					this.statLife += this.inventory[i].healLife;
					this.statMana += this.inventory[i].healMana;
					if (this.statLife > this.statLifeMax)
					{
						this.statLife = this.statLifeMax;
					}
					if (this.statMana > this.statManaMax2)
					{
						this.statMana = this.statManaMax2;
					}
					if (this.inventory[i].healLife > 0 && Main.myPlayer == this.whoAmi)
					{
						this.HealEffect(this.inventory[i].healLife);
					}
					if (this.inventory[i].healMana > 0 && Main.myPlayer == this.whoAmi)
					{
						this.ManaEffect(this.inventory[i].healMana);
					}
					this.inventory[i].stack--;
					if (this.inventory[i].stack <= 0)
					{
						this.inventory[i].type = 0;
						this.inventory[i].name = "";
					}
					Recipe.FindRecipes();
					return;
				}
			}
		}
		public int countBuffs()
		{
			int num = 0;
			for (int i = 0; i < 10; i++)
			{
				if (this.buffType[num] > 0)
				{
					num++;
				}
			}
			return num;
		}
		public void QuickBuff()
		{
			if (this.noItems)
			{
				return;
			}
			int num = 0;
			for (int i = 0; i < 44; i++)
			{
				if (this.countBuffs() == 10)
				{
					return;
				}
				if (this.inventory[i].stack > 0 && this.inventory[i].type > 0 && this.inventory[i].buffType > 0)
				{
					bool flag = true;
					for (int j = 0; j < 10; j++)
					{
						if (this.buffType[j] == this.inventory[i].buffType)
						{
							flag = false;
							break;
						}
					}
					if (this.inventory[i].mana > 0 && flag)
					{
						if (this.statMana >= (int)((float)this.inventory[i].mana * this.manaCost))
						{
							this.manaRegenDelay = (int)this.maxRegenDelay;
							this.statMana -= (int)((float)this.inventory[i].mana * this.manaCost);
						}
						else
						{
							flag = false;
						}
					}
					if (flag)
					{
						num = this.inventory[i].useSound;
						this.AddBuff(this.inventory[i].buffType, this.inventory[i].buffTime, true);
						if (this.inventory[i].consumable)
						{
							this.inventory[i].stack--;
							if (this.inventory[i].stack <= 0)
							{
								this.inventory[i].type = 0;
								this.inventory[i].name = "";
							}
						}
					}
				}
			}
			if (num > 0)
			{
				Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, num);
				Recipe.FindRecipes();
			}
		}
		public void StatusNPC(int type, int i)
		{
			if (type == 121)
			{
				if (Main.rand.Next(2) == 0)
				{
					Main.npc[i].AddBuff(24, 180, false);
					return;
				}
			}
			else
			{
				if (type == 122)
				{
					if (Main.rand.Next(10) == 0)
					{
						Main.npc[i].AddBuff(24, 180, false);
						return;
					}
				}
				else
				{
					if (type == 190)
					{
						if (Main.rand.Next(4) == 0)
						{
							Main.npc[i].AddBuff(20, 420, false);
							return;
						}
					}
					else
					{
						if (type == 217 && Main.rand.Next(5) == 0)
						{
							Main.npc[i].AddBuff(24, 180, false);
						}
					}
				}
			}
		}
		public void StatusPvP(int type, int i)
		{
			if (type == 121)
			{
				if (Main.rand.Next(2) == 0)
				{
					Main.player[i].AddBuff(24, 180, false);
					return;
				}
			}
			else
			{
				if (type == 122)
				{
					if (Main.rand.Next(10) == 0)
					{
						Main.player[i].AddBuff(24, 180, false);
						return;
					}
				}
				else
				{
					if (type == 190)
					{
						if (Main.rand.Next(4) == 0)
						{
							Main.player[i].AddBuff(20, 420, false);
							return;
						}
					}
					else
					{
						if (type == 217 && Main.rand.Next(5) == 0)
						{
							Main.player[i].AddBuff(24, 180, false);
						}
					}
				}
			}
		}
		public void Ghost()
		{
			this.immune = false;
			this.immuneAlpha = 0;
			this.controlUp = false;
			this.controlLeft = false;
			this.controlDown = false;
			this.controlRight = false;
			this.controlJump = false;
			if (Main.hasFocus && !Main.chatMode && !Main.editSign)
			{
				Keys[] pressedKeys = Main.keyState.GetPressedKeys();
				for (int i = 0; i < pressedKeys.Length; i++)
				{
					string a = string.Concat(pressedKeys[i]);
					if (a == Main.cUp)
					{
						this.controlUp = true;
					}
					if (a == Main.cLeft)
					{
						this.controlLeft = true;
					}
					if (a == Main.cDown)
					{
						this.controlDown = true;
					}
					if (a == Main.cRight)
					{
						this.controlRight = true;
					}
					if (a == Main.cJump)
					{
						this.controlJump = true;
					}
				}
			}
			if (this.controlUp || this.controlJump)
			{
				if (this.velocity.Y > 0f)
				{
					this.velocity.Y = this.velocity.Y * 0.9f;
				}
				this.velocity.Y = this.velocity.Y - 0.1f;
				if (this.velocity.Y < -3f)
				{
					this.velocity.Y = -3f;
				}
			}
			else
			{
				if (this.controlDown)
				{
					if (this.velocity.Y < 0f)
					{
						this.velocity.Y = this.velocity.Y * 0.9f;
					}
					this.velocity.Y = this.velocity.Y + 0.1f;
					if (this.velocity.Y > 3f)
					{
						this.velocity.Y = 3f;
					}
				}
				else
				{
					if ((double)this.velocity.Y < -0.1 || (double)this.velocity.Y > 0.1)
					{
						this.velocity.Y = this.velocity.Y * 0.9f;
					}
					else
					{
						this.velocity.Y = 0f;
					}
				}
			}
			if (this.controlLeft && !this.controlRight)
			{
				if (this.velocity.X > 0f)
				{
					this.velocity.X = this.velocity.X * 0.9f;
				}
				this.velocity.X = this.velocity.X - 0.1f;
				if (this.velocity.X < -3f)
				{
					this.velocity.X = -3f;
				}
			}
			else
			{
				if (this.controlRight && !this.controlLeft)
				{
					if (this.velocity.X < 0f)
					{
						this.velocity.X = this.velocity.X * 0.9f;
					}
					this.velocity.X = this.velocity.X + 0.1f;
					if (this.velocity.X > 3f)
					{
						this.velocity.X = 3f;
					}
				}
				else
				{
					if ((double)this.velocity.X < -0.1 || (double)this.velocity.X > 0.1)
					{
						this.velocity.X = this.velocity.X * 0.9f;
					}
					else
					{
						this.velocity.X = 0f;
					}
				}
			}
			this.position += this.velocity;
			this.ghostFrameCounter++;
			if (this.velocity.X < 0f)
			{
				this.direction = -1;
			}
			else
			{
				if (this.velocity.X > 0f)
				{
					this.direction = 1;
				}
			}
			if (this.ghostFrameCounter >= 8)
			{
				this.ghostFrameCounter = 0;
				this.ghostFrame++;
				if (this.ghostFrame >= 4)
				{
					this.ghostFrame = 0;
				}
			}
			if (this.position.X < Main.leftWorld + 336f + 16f)
			{
				this.position.X = Main.leftWorld + 336f + 16f;
				this.velocity.X = 0f;
			}
			if (this.position.X + (float)this.width > Main.rightWorld - 336f - 32f)
			{
				this.position.X = Main.rightWorld - 336f - 32f - (float)this.width;
				this.velocity.X = 0f;
			}
			if (this.position.Y < Main.topWorld + 336f + 16f)
			{
				this.position.Y = Main.topWorld + 336f + 16f;
				if ((double)this.velocity.Y < -0.1)
				{
					this.velocity.Y = -0.1f;
				}
			}
			if (this.position.Y > Main.bottomWorld - 336f - 32f - (float)this.height)
			{
				this.position.Y = Main.bottomWorld - 336f - 32f - (float)this.height;
				this.velocity.Y = 0f;
			}
		}
		public void UpdatePlayer(int i)
		{
			float num = 10f;
			float num2 = 0.4f;
			Player.jumpHeight = 15;
			Player.jumpSpeed = 5.01f;
			if (this.wet)
			{
				num2 = 0.2f;
				num = 5f;
				Player.jumpHeight = 30;
				Player.jumpSpeed = 6.01f;
			}
			float num3 = 3f;
			float num4 = 0.08f;
			float num5 = 0.2f;
			float num6 = num3;
			this.heldProj = -1;
			if (this.active)
			{
				this.maxRegenDelay = (1f - (float)this.statMana / (float)this.statManaMax2) * 60f * 4f + 45f;
				this.shadowCount++;
				if (this.shadowCount == 1)
				{
					this.shadowPos[2] = this.shadowPos[1];
				}
				else
				{
					if (this.shadowCount == 2)
					{
						this.shadowPos[1] = this.shadowPos[0];
					}
					else
					{
						if (this.shadowCount >= 3)
						{
							this.shadowCount = 0;
							this.shadowPos[0] = this.position;
						}
					}
				}
				this.whoAmi = i;
				if (this.runSoundDelay > 0)
				{
					this.runSoundDelay--;
				}
				if (this.attackCD > 0)
				{
					this.attackCD--;
				}
				if (this.itemAnimation == 0)
				{
					this.attackCD = 0;
				}
				if (this.chatShowTime > 0)
				{
					this.chatShowTime--;
				}
				if (this.potionDelay > 0)
				{
					this.potionDelay--;
				}
				if (i == Main.myPlayer)
				{
					this.zoneEvil = false;
					if (Main.evilTiles >= 500)
					{
						this.zoneEvil = true;
					}
					this.zoneMeteor = false;
					if (Main.meteorTiles >= 50)
					{
						this.zoneMeteor = true;
					}
					this.zoneDungeon = false;
					if (Main.dungeonTiles >= 250 && (double)this.position.Y > Main.worldSurface * 16.0)
					{
						int num7 = (int)this.position.X / 16;
						int num8 = (int)this.position.Y / 16;
						if (Main.tile[num7, num8].wall > 0 && !Main.wallHouse[(int)Main.tile[num7, num8].wall])
						{
							this.zoneDungeon = true;
						}
					}
					this.zoneJungle = false;
					if (Main.jungleTiles >= 150)
					{
						this.zoneJungle = true;
					}
				}
				if (this.ghost)
				{
					this.Ghost();
					return;
				}
				if (!this.dead)
				{
					if (i == Main.myPlayer)
					{
						this.controlUp = false;
						this.controlLeft = false;
						this.controlDown = false;
						this.controlRight = false;
						this.controlJump = false;
						this.controlUseItem = false;
						this.controlUseTile = false;
						this.controlThrow = false;
						this.controlInv = false;
						this.controlHook = false;
						if (Main.hasFocus)
						{
							if (!Main.chatMode && !Main.editSign)
							{
								Keys[] pressedKeys = Main.keyState.GetPressedKeys();
								bool flag = false;
								bool flag2 = false;
								for (int j = 0; j < pressedKeys.Length; j++)
								{
									string a = string.Concat(pressedKeys[j]);
									if (a == Main.cUp)
									{
										this.controlUp = true;
									}
									if (a == Main.cLeft)
									{
										this.controlLeft = true;
									}
									if (a == Main.cDown)
									{
										this.controlDown = true;
									}
									if (a == Main.cRight)
									{
										this.controlRight = true;
									}
									if (a == Main.cJump)
									{
										this.controlJump = true;
									}
									if (a == Main.cThrowItem)
									{
										this.controlThrow = true;
									}
									if (a == Main.cInv)
									{
										this.controlInv = true;
									}
									if (a == Main.cBuff)
									{
										this.QuickBuff();
									}
									if (a == Main.cHeal)
									{
										flag2 = true;
									}
									if (a == Main.cMana)
									{
										flag = true;
									}
									if (a == Main.cHook)
									{
										this.controlHook = true;
									}
								}
								if (flag2)
								{
									if (this.releaseQuickHeal)
									{
										this.QuickHeal();
									}
									this.releaseQuickHeal = false;
								}
								else
								{
									this.releaseQuickHeal = true;
								}
								if (flag)
								{
									if (this.releaseQuickMana)
									{
										this.QuickMana();
									}
									this.releaseQuickMana = false;
								}
								else
								{
									this.releaseQuickMana = true;
								}
								if (this.controlLeft && this.controlRight)
								{
									this.controlLeft = false;
									this.controlRight = false;
								}
							}
							if (Main.mouseState.LeftButton == ButtonState.Pressed && !this.mouseInterface)
							{
								this.controlUseItem = true;
							}
							if (Main.mouseState.RightButton == ButtonState.Pressed && !this.mouseInterface)
							{
								this.controlUseTile = true;
							}
							if (this.controlInv)
							{
								if (this.releaseInventory)
								{
									this.toggleInv();
								}
								this.releaseInventory = false;
							}
							else
							{
								this.releaseInventory = true;
							}
							if (this.delayUseItem)
							{
								if (!this.controlUseItem)
								{
									this.delayUseItem = false;
								}
								this.controlUseItem = false;
							}
							if (this.itemAnimation == 0 && this.itemTime == 0)
							{
								this.dropItemCheck();
								int num9 = this.selectedItem;
								if (!Main.chatMode)
								{
									if (Main.keyState.IsKeyDown(Keys.D1))
									{
										this.selectedItem = 0;
									}
									if (Main.keyState.IsKeyDown(Keys.D2))
									{
										this.selectedItem = 1;
									}
									if (Main.keyState.IsKeyDown(Keys.D3))
									{
										this.selectedItem = 2;
									}
									if (Main.keyState.IsKeyDown(Keys.D4))
									{
										this.selectedItem = 3;
									}
									if (Main.keyState.IsKeyDown(Keys.D5))
									{
										this.selectedItem = 4;
									}
									if (Main.keyState.IsKeyDown(Keys.D6))
									{
										this.selectedItem = 5;
									}
									if (Main.keyState.IsKeyDown(Keys.D7))
									{
										this.selectedItem = 6;
									}
									if (Main.keyState.IsKeyDown(Keys.D8))
									{
										this.selectedItem = 7;
									}
									if (Main.keyState.IsKeyDown(Keys.D9))
									{
										this.selectedItem = 8;
									}
									if (Main.keyState.IsKeyDown(Keys.D0))
									{
										this.selectedItem = 9;
									}
								}
								if (num9 != this.selectedItem)
								{
									Main.PlaySound(12, -1, -1, 1);
								}
								if (!Main.playerInventory)
								{
									int k;
									for (k = (Main.mouseState.ScrollWheelValue - Main.oldMouseState.ScrollWheelValue) / 120; k > 9; k -= 10)
									{
									}
									while (k < 0)
									{
										k += 10;
									}
									this.selectedItem -= k;
									if (k != 0)
									{
										Main.PlaySound(12, -1, -1, 1);
									}
									if (this.changeItem >= 0)
									{
										if (this.selectedItem != this.changeItem)
										{
											Main.PlaySound(12, -1, -1, 1);
										}
										this.selectedItem = this.changeItem;
										this.changeItem = -1;
									}
									while (this.selectedItem > 9)
									{
										this.selectedItem -= 10;
									}
									while (this.selectedItem < 0)
									{
										this.selectedItem += 10;
									}
								}
								else
								{
									int num10 = (Main.mouseState.ScrollWheelValue - Main.oldMouseState.ScrollWheelValue) / 120;
									Main.focusRecipe += num10;
									if (Main.focusRecipe > Main.numAvailableRecipes - 1)
									{
										Main.focusRecipe = Main.numAvailableRecipes - 1;
									}
									if (Main.focusRecipe < 0)
									{
										Main.focusRecipe = 0;
									}
								}
							}
						}
						if (!this.controlThrow)
						{
							this.releaseThrow = true;
						}
						else
						{
							this.releaseThrow = false;
						}
						if (Main.netMode == 1)
						{
							bool flag3 = false;
							if (this.statLife != Main.clientPlayer.statLife || this.statLifeMax != Main.clientPlayer.statLifeMax)
							{
								NetMessage.SendData(16, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
								flag3 = true;
							}
							if (this.statMana != Main.clientPlayer.statMana || this.statManaMax != Main.clientPlayer.statManaMax)
							{
								NetMessage.SendData(42, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
								flag3 = true;
							}
							if (this.controlUp != Main.clientPlayer.controlUp)
							{
								flag3 = true;
							}
							if (this.controlDown != Main.clientPlayer.controlDown)
							{
								flag3 = true;
							}
							if (this.controlLeft != Main.clientPlayer.controlLeft)
							{
								flag3 = true;
							}
							if (this.controlRight != Main.clientPlayer.controlRight)
							{
								flag3 = true;
							}
							if (this.controlJump != Main.clientPlayer.controlJump)
							{
								flag3 = true;
							}
							if (this.controlUseItem != Main.clientPlayer.controlUseItem)
							{
								flag3 = true;
							}
							if (this.selectedItem != Main.clientPlayer.selectedItem)
							{
								flag3 = true;
							}
							if (flag3)
							{
								NetMessage.SendData(13, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
							}
						}
						if (Main.playerInventory)
						{
							this.AdjTiles();
						}
						if (this.chest != -1)
						{
							int num11 = (int)(((double)this.position.X + (double)this.width * 0.5) / 16.0);
							int num12 = (int)(((double)this.position.Y + (double)this.height * 0.5) / 16.0);
							if (num11 < this.chestX - 5 || num11 > this.chestX + 6 || num12 < this.chestY - 4 || num12 > this.chestY + 5)
							{
								if (this.chest != -1)
								{
									Main.PlaySound(11, -1, -1, 1);
								}
								this.chest = -1;
							}
							if (!Main.tile[this.chestX, this.chestY].active)
							{
								Main.PlaySound(11, -1, -1, 1);
								this.chest = -1;
							}
						}
						if (this.velocity.Y == 0f)
						{
							int num13 = (int)(this.position.Y / 16f) - this.fallStart;
							if (((this.gravDir == 1f && num13 > 25) || (this.gravDir == -1f && num13 < -25)) && !this.noFallDmg)
							{
								int damage = (int)((float)num13 * this.gravDir - 25f) * 10;
								this.immune = false;
								this.Hurt(damage, 0, false, false, Player.getDeathMessage(-1, -1, -1, 0), false);
							}
							this.fallStart = (int)(this.position.Y / 16f);
						}
						if (this.jump > 0 || this.rocketDelay > 0 || this.wet || this.slowFall)
						{
							this.fallStart = (int)(this.position.Y / 16f);
						}
					}
					if (this.mouseInterface)
					{
						this.delayUseItem = true;
					}
					Player.tileTargetX = (int)(((float)Main.mouseState.X + Main.screenPosition.X) / 16f);
					Player.tileTargetY = (int)(((float)Main.mouseState.Y + Main.screenPosition.Y) / 16f);
					if (this.immune)
					{
						this.immuneTime--;
						if (this.immuneTime <= 0)
						{
							this.immune = false;
						}
						this.immuneAlpha += this.immuneAlphaDirection * 50;
						if (this.immuneAlpha <= 50)
						{
							this.immuneAlphaDirection = 1;
						}
						else
						{
							if (this.immuneAlpha >= 205)
							{
								this.immuneAlphaDirection = -1;
							}
						}
					}
					else
					{
						this.immuneAlpha = 0;
					}
					this.statDefense = 0;
					this.accWatch = 0;
					this.accDepthMeter = 0;
					this.lifeRegen = 0;
					this.manaCost = 1f;
					this.meleeSpeed = 1f;
					this.meleeDamage = 1f;
					this.rangedDamage = 1f;
					this.magicDamage = 1f;
					this.moveSpeed = 1f;
					this.boneArmor = false;
					this.rocketBoots = false;
					this.fireWalk = false;
					this.noKnockback = false;
					this.jumpBoost = false;
					this.noFallDmg = false;
					this.accFlipper = false;
					this.spawnMax = false;
					this.spaceGun = false;
					this.killGuide = false;
					this.lavaImmune = false;
					this.gills = false;
					this.slowFall = false;
					this.findTreasure = false;
					this.invis = false;
					this.nightVision = false;
					this.enemySpawns = false;
					this.thorns = false;
					this.waterWalk = false;
					this.detectCreature = false;
					this.gravControl = false;
					this.statManaMax2 = this.statManaMax;
					this.ammoCost80 = false;
					this.manaRegenBuff = false;
					this.meleeCrit = 4;
					this.rangedCrit = 4;
					this.magicCrit = 4;
					this.lightOrb = false;
					this.archery = false;
					this.poisoned = false;
					this.blind = false;
					this.onFire = false;
					this.noItems = false;
					for (int l = 0; l < 10; l++)
					{
						if (this.buffType[l] > 0 && this.buffTime[l] > 0)
						{
							if (this.whoAmi == Main.myPlayer)
							{
								this.buffTime[l]--;
							}
							if (this.buffType[l] == 1)
							{
								this.lavaImmune = true;
								this.fireWalk = true;
							}
							else
							{
								if (this.buffType[l] == 2)
								{
									this.lifeRegen += 2;
								}
								else
								{
									if (this.buffType[l] == 3)
									{
										this.moveSpeed += 0.25f;
									}
									else
									{
										if (this.buffType[l] == 4)
										{
											this.gills = true;
										}
										else
										{
											if (this.buffType[l] == 5)
											{
												this.statDefense += 10;
											}
											else
											{
												if (this.buffType[l] == 6)
												{
													this.manaRegenBuff = true;
												}
												else
												{
													if (this.buffType[l] == 7)
													{
														this.magicDamage += 0.2f;
													}
													else
													{
														if (this.buffType[l] == 8)
														{
															this.slowFall = true;
														}
														else
														{
															if (this.buffType[l] == 9)
															{
																this.findTreasure = true;
															}
															else
															{
																if (this.buffType[l] == 10)
																{
																	this.invis = true;
																}
																else
																{
																	if (this.buffType[l] == 11)
																	{
																		Lighting.addLight((int)(this.position.X + (float)(this.width / 2)) / 16, (int)(this.position.Y + (float)(this.height / 2)) / 16, 1f);
																	}
																	else
																	{
																		if (this.buffType[l] == 12)
																		{
																			this.nightVision = true;
																		}
																		else
																		{
																			if (this.buffType[l] == 13)
																			{
																				this.enemySpawns = true;
																			}
																			else
																			{
																				if (this.buffType[l] == 14)
																				{
																					this.thorns = true;
																				}
																				else
																				{
																					if (this.buffType[l] == 15)
																					{
																						this.waterWalk = true;
																					}
																					else
																					{
																						if (this.buffType[l] == 16)
																						{
																							this.archery = true;
																						}
																						else
																						{
																							if (this.buffType[l] == 17)
																							{
																								this.detectCreature = true;
																							}
																							else
																							{
																								if (this.buffType[l] == 18)
																								{
																									this.gravControl = true;
																								}
																								else
																								{
																									if (this.buffType[l] == 19)
																									{
																										this.lightOrb = true;
																										bool flag4 = true;
																										for (int m = 0; m < 1000; m++)
																										{
																											if (Main.projectile[m].active && Main.projectile[m].owner == this.whoAmi && Main.projectile[m].type == 18)
																											{
																												flag4 = false;
																												break;
																											}
																										}
																										if (flag4)
																										{
																											Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.height / 2), 0f, 0f, 18, 0, 0f, this.whoAmi);
																										}
																									}
																									else
																									{
																										if (this.buffType[l] == 20)
																										{
																											this.poisoned = true;
																										}
																										else
																										{
																											if (this.buffType[l] == 21)
																											{
																												this.potionDelay = this.buffTime[l];
																											}
																											else
																											{
																												if (this.buffType[l] == 22)
																												{
																													this.blind = true;
																												}
																												else
																												{
																													if (this.buffType[l] == 23)
																													{
																														this.noItems = true;
																													}
																													else
																													{
																														if (this.buffType[l] == 24)
																														{
																															this.onFire = true;
																														}
																														else
																														{
																															if (this.buffType[l] == 25)
																															{
																																this.statDefense -= 4;
																																this.meleeCrit += 2;
																																this.meleeDamage += 0.1f;
																																this.meleeSpeed += 0.1f;
																															}
																															else
																															{
																																if (this.buffType[l] == 26)
																																{
																																	this.statDefense++;
																																	this.meleeCrit++;
																																	this.meleeDamage += 0.05f;
																																	this.meleeSpeed += 0.05f;
																																	this.magicCrit++;
																																	this.magicDamage += 0.05f;
																																	this.rangedCrit++;
																																	this.magicDamage += 0.05f;
																																	this.moveSpeed += 0.1f;
																																}
																															}
																														}
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					if (this.whoAmi == Main.myPlayer)
					{
						for (int n = 0; n < 10; n++)
						{
							if (this.buffType[n] > 0 && this.buffTime[n] <= 0)
							{
								this.DelBuff(n);
							}
						}
					}
					if (this.manaRegenDelay > 0 && !this.channel)
					{
						this.manaRegenDelay--;
						if ((this.velocity.X == 0f && this.velocity.Y == 0f) || this.grappling[0] >= 0 || this.manaRegenBuff)
						{
							this.manaRegenDelay--;
						}
					}
					if (this.manaRegenBuff && this.manaRegenDelay > 20)
					{
						this.manaRegenDelay = 20;
					}
					if (this.manaRegenDelay <= 0)
					{
						this.manaRegenDelay = 0;
						this.manaRegen = this.statManaMax2 / 7 + 1;
						if ((this.velocity.X == 0f && this.velocity.Y == 0f) || this.grappling[0] >= 0 || this.manaRegenBuff)
						{
							this.manaRegen += this.statManaMax2 / 2;
						}
						float num14 = (float)this.statMana / (float)this.statManaMax2 * 0.8f + 0.2f;
						if (this.manaRegenBuff)
						{
							num14 = 1f;
						}
						this.manaRegen = (int)((float)this.manaRegen * num14);
					}
					else
					{
						this.manaRegen = 0;
					}
					this.doubleJump = false;
					for (int num15 = 0; num15 < 8; num15++)
					{
						this.statDefense += this.armor[num15].defense;
						this.lifeRegen += this.armor[num15].lifeRegen;
						if (this.armor[num15].type == 193)
						{
							this.fireWalk = true;
						}
						if (this.armor[num15].type == 238)
						{
							this.magicDamage += 0.15f;
						}
						if (this.armor[num15].type == 123 || this.armor[num15].type == 124 || this.armor[num15].type == 125)
						{
							this.magicDamage += 0.05f;
						}
						if (this.armor[num15].type == 151 || this.armor[num15].type == 152 || this.armor[num15].type == 153)
						{
							this.rangedDamage += 0.05f;
						}
						if (this.armor[num15].type == 111 || this.armor[num15].type == 228 || this.armor[num15].type == 229 || this.armor[num15].type == 230)
						{
							this.statManaMax2 += 20;
						}
						if (this.armor[num15].type == 228 || this.armor[num15].type == 229 || this.armor[num15].type == 230)
						{
							this.magicCrit += 3;
						}
						if (this.armor[num15].type == 100 || this.armor[num15].type == 101 || this.armor[num15].type == 102)
						{
							this.meleeSpeed += 0.07f;
						}
					}
					this.head = this.armor[0].headSlot;
					this.body = this.armor[1].bodySlot;
					this.legs = this.armor[2].legSlot;
					for (int num16 = 3; num16 < 8; num16++)
					{
						if (this.armor[num16].type == 15 && this.accWatch < 1)
						{
							this.accWatch = 1;
						}
						if (this.armor[num16].type == 16 && this.accWatch < 2)
						{
							this.accWatch = 2;
						}
						if (this.armor[num16].type == 17 && this.accWatch < 3)
						{
							this.accWatch = 3;
						}
						if (this.armor[num16].type == 18 && this.accDepthMeter < 1)
						{
							this.accDepthMeter = 1;
						}
						if (this.armor[num16].type == 53)
						{
							this.doubleJump = true;
						}
						if (this.armor[num16].type == 54)
						{
							num6 = 6f;
						}
						if (this.armor[num16].type == 128)
						{
							this.rocketBoots = true;
						}
						if (this.armor[num16].type == 156)
						{
							this.noKnockback = true;
						}
						if (this.armor[num16].type == 158)
						{
							this.noFallDmg = true;
						}
						if (this.armor[num16].type == 159)
						{
							this.jumpBoost = true;
						}
						if (this.armor[num16].type == 187)
						{
							this.accFlipper = true;
						}
						if (this.armor[num16].type == 211)
						{
							this.meleeSpeed += 0.12f;
						}
						if (this.armor[num16].type == 223)
						{
							this.manaCost -= 0.06f;
						}
						if (this.armor[num16].type == 285)
						{
							this.moveSpeed += 0.05f;
						}
						if (this.armor[num16].type == 212)
						{
							this.moveSpeed += 0.1f;
						}
						if (this.armor[num16].type == 267)
						{
							this.killGuide = true;
						}
					}
					if (this.head == 11)
					{
						int i2 = (int)(this.position.X + (float)(this.width / 2) + (float)(8 * this.direction)) / 16;
						int j2 = (int)(this.position.Y + 2f) / 16;
						Lighting.addLight(i2, j2, 0.8f);
					}
					this.setBonus = "";
					if ((this.head == 1 && this.body == 1 && this.legs == 1) || (this.head == 2 && this.body == 2 && this.legs == 2))
					{
						this.setBonus = "2 defense";
						this.statDefense += 2;
					}
					if ((this.head == 3 && this.body == 3 && this.legs == 3) || (this.head == 4 && this.body == 4 && this.legs == 4))
					{
						this.setBonus = "3 defense";
						this.statDefense += 3;
					}
					if (this.head == 5 && this.body == 5 && this.legs == 5)
					{
						this.setBonus = "15% increased movement speed";
						this.moveSpeed += 0.15f;
					}
					if (this.head == 6 && this.body == 6 && this.legs == 6)
					{
						this.setBonus = "Space Gun costs 0 mana";
						this.spaceGun = true;
					}
					if (this.head == 7 && this.body == 7 && this.legs == 7)
					{
						this.setBonus = "20% chance to not consume ammo";
						this.ammoCost80 = true;
					}
					if (this.head == 8 && this.body == 8 && this.legs == 8)
					{
						this.setBonus = "16% reduced mana usage";
						this.manaCost -= 0.16f;
					}
					if (this.head == 9 && this.body == 9 && this.legs == 9)
					{
						this.setBonus = "17% extra melee damage";
						this.meleeDamage += 0.17f;
					}
					if (this.meleeSpeed > 4f)
					{
						this.meleeSpeed = 4f;
					}
					if ((double)this.moveSpeed > 1.4)
					{
						this.moveSpeed = 1.4f;
					}
					if (this.statManaMax2 > 400)
					{
						this.statManaMax2 = 400;
					}
					if (this.statDefense < 0)
					{
						this.statDefense = 0;
					}
					this.meleeSpeed = 1f / this.meleeSpeed;
					if (this.poisoned)
					{
						this.lifeRegenTime = 0;
						this.lifeRegen = -4;
					}
					if (this.onFire)
					{
						this.lifeRegenTime = 0;
						this.lifeRegen = -8;
					}
					this.lifeRegenTime++;
					float num17 = 0f;
					if (this.lifeRegenTime >= 300)
					{
						num17 += 1f;
					}
					if (this.lifeRegenTime >= 600)
					{
						num17 += 1f;
					}
					if (this.lifeRegenTime >= 900)
					{
						num17 += 1f;
					}
					if (this.lifeRegenTime >= 1200)
					{
						num17 += 1f;
					}
					if (this.lifeRegenTime >= 1500)
					{
						num17 += 1f;
					}
					if (this.lifeRegenTime >= 1800)
					{
						num17 += 1f;
					}
					if (this.lifeRegenTime >= 2400)
					{
						num17 += 1f;
					}
					if (this.lifeRegenTime >= 3000)
					{
						num17 += 1f;
					}
					if (this.lifeRegenTime >= 3600)
					{
						num17 += 1f;
						this.lifeRegenTime = 3600;
					}
					if (this.velocity.X == 0f || this.grappling[0] > 0)
					{
						num17 *= 1.25f;
					}
					else
					{
						num17 *= 0.5f;
					}
					float num18 = (float)this.statLifeMax / 400f * 0.75f + 0.25f;
					num17 *= num18;
					this.lifeRegen += (int)Math.Round((double)num17);
					this.lifeRegenCount += this.lifeRegen;
					while (this.lifeRegenCount >= 120)
					{
						this.lifeRegenCount -= 120;
						if (this.statLife < this.statLifeMax)
						{
							this.statLife++;
						}
						if (this.statLife > this.statLifeMax)
						{
							this.statLife = this.statLifeMax;
						}
					}
					while (this.lifeRegenCount <= -120)
					{
						this.lifeRegenCount += 120;
						this.statLife--;
						if (this.statLife <= 0)
						{
							if (this.poisoned)
							{
								this.KillMe(10.0, 0, false, " couldn't find the antidote");
							}
							else
							{
								if (this.onFire)
								{
									this.KillMe(10.0, 0, false, " couldn't put the fire out");
								}
							}
						}
					}
					this.manaRegenCount += this.manaRegen;
					while (this.manaRegenCount >= 120)
					{
						bool flag5 = false;
						this.manaRegenCount -= 120;
						if (this.statMana < this.statManaMax2)
						{
							this.statMana++;
							flag5 = true;
						}
						if (this.statMana >= this.statManaMax2)
						{
							if (this.whoAmi == Main.myPlayer && flag5)
							{
								Main.PlaySound(25, -1, -1, 1);
								for (int num19 = 0; num19 < 5; num19++)
								{
									Vector2 arg_1EFE_0 = this.position;
									int arg_1EFE_1 = this.width;
									int arg_1EFE_2 = this.height;
									int arg_1EFE_3 = 45;
									float arg_1EFE_4 = 0f;
									float arg_1EFE_5 = 0f;
									int arg_1EFE_6 = 255;
									Color newColor = default(Color);
									int num20 = Dust.NewDust(arg_1EFE_0, arg_1EFE_1, arg_1EFE_2, arg_1EFE_3, arg_1EFE_4, arg_1EFE_5, arg_1EFE_6, newColor, (float)Main.rand.Next(20, 26) * 0.1f);
									Main.dust[num20].noLight = true;
									Main.dust[num20].noGravity = true;
									Dust expr_1F29 = Main.dust[num20];
									expr_1F29.velocity *= 0.5f;
								}
							}
							this.statMana = this.statManaMax2;
						}
					}
					if (this.manaRegenCount < 0)
					{
						this.manaRegenCount = 0;
					}
					num4 *= this.moveSpeed;
					num3 *= this.moveSpeed;
					if (this.jumpBoost)
					{
						Player.jumpHeight = 20;
						Player.jumpSpeed = 6.51f;
					}
					if (!this.doubleJump)
					{
						this.jumpAgain = false;
					}
					else
					{
						if (this.velocity.Y == 0f)
						{
							this.jumpAgain = true;
						}
					}
					if (this.grappling[0] == -1)
					{
						if (this.controlLeft && this.velocity.X > -num3)
						{
							if (this.velocity.X > num5)
							{
								this.velocity.X = this.velocity.X - num5;
							}
							this.velocity.X = this.velocity.X - num4;
							if (this.itemAnimation == 0 || this.inventory[this.selectedItem].useTurn)
							{
								this.direction = -1;
							}
						}
						else
						{
							if (this.controlRight && this.velocity.X < num3)
							{
								if (this.velocity.X < -num5)
								{
									this.velocity.X = this.velocity.X + num5;
								}
								this.velocity.X = this.velocity.X + num4;
								if (this.itemAnimation == 0 || this.inventory[this.selectedItem].useTurn)
								{
									this.direction = 1;
								}
							}
							else
							{
								if (this.controlLeft && this.velocity.X > -num6)
								{
									if (this.itemAnimation == 0 || this.inventory[this.selectedItem].useTurn)
									{
										this.direction = -1;
									}
									if (this.velocity.Y == 0f)
									{
										if (this.velocity.X > num5)
										{
											this.velocity.X = this.velocity.X - num5;
										}
										this.velocity.X = this.velocity.X - num4 * 0.2f;
									}
									if (this.velocity.X < -(num6 + num3) / 2f && this.velocity.Y == 0f)
									{
										int num21 = 0;
										if (this.gravDir == -1f)
										{
											num21 -= this.height;
										}
										if (this.runSoundDelay == 0 && this.velocity.Y == 0f)
										{
											Main.PlaySound(17, (int)this.position.X, (int)this.position.Y, 1);
											this.runSoundDelay = 9;
										}
										Vector2 arg_2250_0 = new Vector2(this.position.X - 4f, this.position.Y + (float)this.height + (float)num21);
										int arg_2250_1 = this.width + 8;
										int arg_2250_2 = 4;
										int arg_2250_3 = 16;
										float arg_2250_4 = -this.velocity.X * 0.5f;
										float arg_2250_5 = this.velocity.Y * 0.5f;
										int arg_2250_6 = 50;
										Color newColor = default(Color);
										int num22 = Dust.NewDust(arg_2250_0, arg_2250_1, arg_2250_2, arg_2250_3, arg_2250_4, arg_2250_5, arg_2250_6, newColor, 1.5f);
										Main.dust[num22].velocity.X = Main.dust[num22].velocity.X * 0.2f;
										Main.dust[num22].velocity.Y = Main.dust[num22].velocity.Y * 0.2f;
									}
								}
								else
								{
									if (this.controlRight && this.velocity.X < num6)
									{
										if (this.itemAnimation == 0 || this.inventory[this.selectedItem].useTurn)
										{
											this.direction = 1;
										}
										if (this.velocity.Y == 0f)
										{
											if (this.velocity.X < -num5)
											{
												this.velocity.X = this.velocity.X + num5;
											}
											this.velocity.X = this.velocity.X + num4 * 0.2f;
										}
										if (this.velocity.X > (num6 + num3) / 2f && this.velocity.Y == 0f)
										{
											int num23 = 0;
											if (this.gravDir == -1f)
											{
												num23 -= this.height;
											}
											if (this.runSoundDelay == 0 && this.velocity.Y == 0f)
											{
												Main.PlaySound(17, (int)this.position.X, (int)this.position.Y, 1);
												this.runSoundDelay = 9;
											}
											Vector2 arg_2437_0 = new Vector2(this.position.X - 4f, this.position.Y + (float)this.height + (float)num23);
											int arg_2437_1 = this.width + 8;
											int arg_2437_2 = 4;
											int arg_2437_3 = 16;
											float arg_2437_4 = -this.velocity.X * 0.5f;
											float arg_2437_5 = this.velocity.Y * 0.5f;
											int arg_2437_6 = 50;
											Color newColor = default(Color);
											int num24 = Dust.NewDust(arg_2437_0, arg_2437_1, arg_2437_2, arg_2437_3, arg_2437_4, arg_2437_5, arg_2437_6, newColor, 1.5f);
											Main.dust[num24].velocity.X = Main.dust[num24].velocity.X * 0.2f;
											Main.dust[num24].velocity.Y = Main.dust[num24].velocity.Y * 0.2f;
										}
									}
									else
									{
										if (this.velocity.Y == 0f)
										{
											if (this.velocity.X > num5)
											{
												this.velocity.X = this.velocity.X - num5;
											}
											else
											{
												if (this.velocity.X < -num5)
												{
													this.velocity.X = this.velocity.X + num5;
												}
												else
												{
													this.velocity.X = 0f;
												}
											}
										}
										else
										{
											if ((double)this.velocity.X > (double)num5 * 0.5)
											{
												this.velocity.X = this.velocity.X - num5 * 0.5f;
											}
											else
											{
												if ((double)this.velocity.X < (double)(-(double)num5) * 0.5)
												{
													this.velocity.X = this.velocity.X + num5 * 0.5f;
												}
												else
												{
													this.velocity.X = 0f;
												}
											}
										}
									}
								}
							}
						}
						if (this.gravControl)
						{
							if (this.controlUp && this.gravDir == 1f)
							{
								this.gravDir = -1f;
								this.fallStart = (int)(this.position.Y / 16f);
								this.jump = 0;
								Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 8);
							}
							if (this.controlDown && this.gravDir == -1f)
							{
								this.gravDir = 1f;
								this.fallStart = (int)(this.position.Y / 16f);
								this.jump = 0;
								Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 8);
							}
						}
						else
						{
							this.gravDir = 1f;
						}
						if (this.controlJump)
						{
							if (this.jump > 0)
							{
								if (this.velocity.Y == 0f)
								{
									this.jump = 0;
								}
								else
								{
									this.velocity.Y = -Player.jumpSpeed * this.gravDir;
									this.jump--;
								}
							}
							else
							{
								if ((this.velocity.Y == 0f || this.jumpAgain || (this.wet && this.accFlipper)) && this.releaseJump)
								{
									bool flag6 = false;
									if (this.wet && this.accFlipper)
									{
										if (this.swimTime == 0)
										{
											this.swimTime = 30;
										}
										flag6 = true;
									}
									this.jumpAgain = false;
									this.canRocket = false;
									this.rocketRelease = false;
									if (this.velocity.Y == 0f && this.doubleJump)
									{
										this.jumpAgain = true;
									}
									if (this.velocity.Y == 0f || flag6)
									{
										this.velocity.Y = -Player.jumpSpeed * this.gravDir;
										this.jump = Player.jumpHeight;
									}
									else
									{
										int num25 = this.height;
										if (this.gravDir == -1f)
										{
											num25 = 0;
										}
										Main.PlaySound(16, (int)this.position.X, (int)this.position.Y, 1);
										this.velocity.Y = -Player.jumpSpeed * this.gravDir;
										this.jump = Player.jumpHeight / 2;
										for (int num26 = 0; num26 < 10; num26++)
										{
											Vector2 arg_285B_0 = new Vector2(this.position.X - 34f, this.position.Y + (float)num25 - 16f);
											int arg_285B_1 = 102;
											int arg_285B_2 = 32;
											int arg_285B_3 = 16;
											float arg_285B_4 = -this.velocity.X * 0.5f;
											float arg_285B_5 = this.velocity.Y * 0.5f;
											int arg_285B_6 = 100;
											Color newColor = default(Color);
											int num27 = Dust.NewDust(arg_285B_0, arg_285B_1, arg_285B_2, arg_285B_3, arg_285B_4, arg_285B_5, arg_285B_6, newColor, 1.5f);
											Main.dust[num27].velocity.X = Main.dust[num27].velocity.X * 0.5f - this.velocity.X * 0.1f;
											Main.dust[num27].velocity.Y = Main.dust[num27].velocity.Y * 0.5f - this.velocity.Y * 0.3f;
										}
										int num28 = Gore.NewGore(new Vector2(this.position.X + (float)(this.width / 2) - 16f, this.position.Y + (float)num25 - 16f), new Vector2(-this.velocity.X, -this.velocity.Y), Main.rand.Next(11, 14), 1f);
										Main.gore[num28].velocity.X = Main.gore[num28].velocity.X * 0.1f - this.velocity.X * 0.1f;
										Main.gore[num28].velocity.Y = Main.gore[num28].velocity.Y * 0.1f - this.velocity.Y * 0.05f;
										num28 = Gore.NewGore(new Vector2(this.position.X - 36f, this.position.Y + (float)num25 - 16f), new Vector2(-this.velocity.X, -this.velocity.Y), Main.rand.Next(11, 14), 1f);
										Main.gore[num28].velocity.X = Main.gore[num28].velocity.X * 0.1f - this.velocity.X * 0.1f;
										Main.gore[num28].velocity.Y = Main.gore[num28].velocity.Y * 0.1f - this.velocity.Y * 0.05f;
										num28 = Gore.NewGore(new Vector2(this.position.X + (float)this.width + 4f, this.position.Y + (float)num25 - 16f), new Vector2(-this.velocity.X, -this.velocity.Y), Main.rand.Next(11, 14), 1f);
										Main.gore[num28].velocity.X = Main.gore[num28].velocity.X * 0.1f - this.velocity.X * 0.1f;
										Main.gore[num28].velocity.Y = Main.gore[num28].velocity.Y * 0.1f - this.velocity.Y * 0.05f;
									}
								}
							}
							this.releaseJump = false;
						}
						else
						{
							this.jump = 0;
							this.releaseJump = true;
							this.rocketRelease = true;
						}
						if (this.doubleJump && !this.jumpAgain && ((this.gravDir == 1f && this.velocity.Y < 0f) || (this.gravDir == -1f && this.velocity.Y > 0f)) && !this.rocketBoots && !this.accFlipper)
						{
							int num29 = this.height;
							if (this.gravDir == -1f)
							{
								num29 = -6;
							}
							Vector2 arg_2C94_0 = new Vector2(this.position.X - 4f, this.position.Y + (float)num29);
							int arg_2C94_1 = this.width + 8;
							int arg_2C94_2 = 4;
							int arg_2C94_3 = 16;
							float arg_2C94_4 = -this.velocity.X * 0.5f;
							float arg_2C94_5 = this.velocity.Y * 0.5f;
							int arg_2C94_6 = 100;
							Color newColor = default(Color);
							int num30 = Dust.NewDust(arg_2C94_0, arg_2C94_1, arg_2C94_2, arg_2C94_3, arg_2C94_4, arg_2C94_5, arg_2C94_6, newColor, 1.5f);
							Main.dust[num30].velocity.X = Main.dust[num30].velocity.X * 0.5f - this.velocity.X * 0.1f;
							Main.dust[num30].velocity.Y = Main.dust[num30].velocity.Y * 0.5f - this.velocity.Y * 0.3f;
						}
						if (((this.gravDir == 1f && this.velocity.Y > -Player.jumpSpeed) || (this.gravDir == -1f && this.velocity.Y < Player.jumpSpeed)) && this.velocity.Y != 0f)
						{
							this.canRocket = true;
						}
						if (this.velocity.Y == 0f)
						{
							this.rocketTime = this.rocketTimeMax;
						}
						if (this.rocketBoots && this.controlJump && this.rocketDelay == 0 && this.canRocket && this.rocketRelease && !this.jumpAgain)
						{
							if (this.rocketTime > 0)
							{
								this.rocketTime--;
								this.rocketDelay = 10;
								if (this.rocketDelay2 <= 0)
								{
									Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, 13);
									this.rocketDelay2 = 30;
								}
							}
							else
							{
								this.canRocket = false;
							}
						}
						if (this.rocketDelay2 > 0)
						{
							this.rocketDelay2--;
						}
						if (this.rocketDelay == 0)
						{
							this.rocketFrame = false;
						}
						if (this.rocketDelay > 0)
						{
							int num31 = this.height;
							if (this.gravDir == -1f)
							{
								num31 = 4;
							}
							this.rocketFrame = true;
							for (int num32 = 0; num32 < 2; num32++)
							{
								int num33 = 6;
								float scale = 2.5f;
								if (this.socialShadow)
								{
									num33 = 27;
									scale = 1.5f;
								}
								if (num32 == 0)
								{
									Vector2 arg_2ED9_0 = new Vector2(this.position.X - 4f, this.position.Y + (float)num31 - 10f);
									int arg_2ED9_1 = 8;
									int arg_2ED9_2 = 8;
									int arg_2ED9_3 = num33;
									float arg_2ED9_4 = 0f;
									float arg_2ED9_5 = 0f;
									int arg_2ED9_6 = 100;
									Color newColor = default(Color);
									int num34 = Dust.NewDust(arg_2ED9_0, arg_2ED9_1, arg_2ED9_2, arg_2ED9_3, arg_2ED9_4, arg_2ED9_5, arg_2ED9_6, newColor, scale);
									Main.dust[num34].noGravity = true;
									Main.dust[num34].velocity.X = Main.dust[num34].velocity.X * 1f - 2f - this.velocity.X * 0.3f;
									Main.dust[num34].velocity.Y = Main.dust[num34].velocity.Y * 1f + 2f * this.gravDir - this.velocity.Y * 0.3f;
								}
								else
								{
									Vector2 arg_2FCD_0 = new Vector2(this.position.X + (float)this.width - 4f, this.position.Y + (float)num31 - 10f);
									int arg_2FCD_1 = 8;
									int arg_2FCD_2 = 8;
									int arg_2FCD_3 = num33;
									float arg_2FCD_4 = 0f;
									float arg_2FCD_5 = 0f;
									int arg_2FCD_6 = 100;
									Color newColor = default(Color);
									int num35 = Dust.NewDust(arg_2FCD_0, arg_2FCD_1, arg_2FCD_2, arg_2FCD_3, arg_2FCD_4, arg_2FCD_5, arg_2FCD_6, newColor, scale);
									Main.dust[num35].noGravity = true;
									Main.dust[num35].velocity.X = Main.dust[num35].velocity.X * 1f + 2f - this.velocity.X * 0.3f;
									Main.dust[num35].velocity.Y = Main.dust[num35].velocity.Y * 1f + 2f * this.gravDir - this.velocity.Y * 0.3f;
								}
							}
							if (this.rocketDelay == 0)
							{
								this.releaseJump = true;
							}
							this.rocketDelay--;
							this.velocity.Y = this.velocity.Y - 0.1f * this.gravDir;
							if (this.gravDir == 1f)
							{
								if (this.velocity.Y > 0f)
								{
									this.velocity.Y = this.velocity.Y - 0.5f;
								}
								else
								{
									if ((double)this.velocity.Y > (double)(-(double)Player.jumpSpeed) * 0.5)
									{
										this.velocity.Y = this.velocity.Y - 0.1f;
									}
								}
								if (this.velocity.Y < -Player.jumpSpeed * 1.5f)
								{
									this.velocity.Y = -Player.jumpSpeed * 1.5f;
								}
							}
							else
							{
								if (this.velocity.Y < 0f)
								{
									this.velocity.Y = this.velocity.Y + 0.5f;
								}
								else
								{
									if ((double)this.velocity.Y < (double)Player.jumpSpeed * 0.5)
									{
										this.velocity.Y = this.velocity.Y + 0.1f;
									}
								}
								if (this.velocity.Y > Player.jumpSpeed * 1.5f)
								{
									this.velocity.Y = Player.jumpSpeed * 1.5f;
								}
							}
						}
						else
						{
							if (this.slowFall && ((!this.controlDown && this.gravDir == 1f) || (!this.controlUp && this.gravDir == -1f)))
							{
								if ((this.controlUp && this.gravDir == 1f) || (this.controlDown && this.gravDir == -1f))
								{
									this.velocity.Y = this.velocity.Y + num2 / 10f * this.gravDir;
								}
								else
								{
									this.velocity.Y = this.velocity.Y + num2 / 3f * this.gravDir;
								}
							}
							else
							{
								this.velocity.Y = this.velocity.Y + num2 * this.gravDir;
							}
						}
						if (this.gravDir == 1f)
						{
							if (this.velocity.Y > num)
							{
								this.velocity.Y = num;
							}
							if (this.slowFall && this.velocity.Y > num / 3f && !this.controlDown)
							{
								this.velocity.Y = num / 3f;
							}
							if (this.slowFall && this.velocity.Y > num / 5f && this.controlUp)
							{
								this.velocity.Y = num / 10f;
							}
						}
						else
						{
							if (this.velocity.Y < -num)
							{
								this.velocity.Y = -num;
							}
							if (this.slowFall && this.velocity.Y < -num / 3f && !this.controlUp)
							{
								this.velocity.Y = -num / 3f;
							}
							if (this.slowFall && this.velocity.Y < -num / 5f && this.controlDown)
							{
								this.velocity.Y = -num / 10f;
							}
						}
					}
					for (int num36 = 0; num36 < 200; num36++)
					{
						if (Main.item[num36].active && Main.item[num36].noGrabDelay == 0 && Main.item[num36].owner == i)
						{
							Rectangle rectangle = new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height);
							if (rectangle.Intersects(new Rectangle((int)Main.item[num36].position.X, (int)Main.item[num36].position.Y, Main.item[num36].width, Main.item[num36].height)))
							{
								if (i == Main.myPlayer && (this.inventory[this.selectedItem].type != 0 || this.itemAnimation <= 0))
								{
									if (Main.item[num36].type == 58)
									{
										Main.PlaySound(7, (int)this.position.X, (int)this.position.Y, 1);
										this.statLife += 20;
										if (Main.myPlayer == this.whoAmi)
										{
											this.HealEffect(20);
										}
										if (this.statLife > this.statLifeMax)
										{
											this.statLife = this.statLifeMax;
										}
										Main.item[num36] = new Item();
										if (Main.netMode == 1)
										{
											NetMessage.SendData(21, -1, -1, "", num36, 0f, 0f, 0f, 0);
										}
									}
									else
									{
										if (Main.item[num36].type == 184)
										{
											Main.PlaySound(7, (int)this.position.X, (int)this.position.Y, 1);
											this.statMana += 100;
											if (Main.myPlayer == this.whoAmi)
											{
												this.ManaEffect(100);
											}
											if (this.statMana > this.statManaMax2)
											{
												this.statMana = this.statManaMax2;
											}
											Main.item[num36] = new Item();
											if (Main.netMode == 1)
											{
												NetMessage.SendData(21, -1, -1, "", num36, 0f, 0f, 0f, 0);
											}
										}
										else
										{
											Main.item[num36] = this.GetItem(i, Main.item[num36]);
											if (Main.netMode == 1)
											{
												NetMessage.SendData(21, -1, -1, "", num36, 0f, 0f, 0f, 0);
											}
										}
									}
								}
							}
							else
							{
								rectangle = new Rectangle((int)this.position.X - Player.itemGrabRange, (int)this.position.Y - Player.itemGrabRange, this.width + Player.itemGrabRange * 2, this.height + Player.itemGrabRange * 2);
								if (rectangle.Intersects(new Rectangle((int)Main.item[num36].position.X, (int)Main.item[num36].position.Y, Main.item[num36].width, Main.item[num36].height)) && this.ItemSpace(Main.item[num36]))
								{
									Main.item[num36].beingGrabbed = true;
									if ((double)this.position.X + (double)this.width * 0.5 > (double)Main.item[num36].position.X + (double)Main.item[num36].width * 0.5)
									{
										if (Main.item[num36].velocity.X < Player.itemGrabSpeedMax + this.velocity.X)
										{
											Item expr_37A8_cp_0 = Main.item[num36];
											expr_37A8_cp_0.velocity.X = expr_37A8_cp_0.velocity.X + Player.itemGrabSpeed;
										}
										if (Main.item[num36].velocity.X < 0f)
										{
											Item expr_37E2_cp_0 = Main.item[num36];
											expr_37E2_cp_0.velocity.X = expr_37E2_cp_0.velocity.X + Player.itemGrabSpeed * 0.75f;
										}
									}
									else
									{
										if (Main.item[num36].velocity.X > -Player.itemGrabSpeedMax + this.velocity.X)
										{
											Item expr_3831_cp_0 = Main.item[num36];
											expr_3831_cp_0.velocity.X = expr_3831_cp_0.velocity.X - Player.itemGrabSpeed;
										}
										if (Main.item[num36].velocity.X > 0f)
										{
											Item expr_3868_cp_0 = Main.item[num36];
											expr_3868_cp_0.velocity.X = expr_3868_cp_0.velocity.X - Player.itemGrabSpeed * 0.75f;
										}
									}
									if ((double)this.position.Y + (double)this.height * 0.5 > (double)Main.item[num36].position.Y + (double)Main.item[num36].height * 0.5)
									{
										if (Main.item[num36].velocity.Y < Player.itemGrabSpeedMax)
										{
											Item expr_38F1_cp_0 = Main.item[num36];
											expr_38F1_cp_0.velocity.Y = expr_38F1_cp_0.velocity.Y + Player.itemGrabSpeed;
										}
										if (Main.item[num36].velocity.Y < 0f)
										{
											Item expr_392B_cp_0 = Main.item[num36];
											expr_392B_cp_0.velocity.Y = expr_392B_cp_0.velocity.Y + Player.itemGrabSpeed * 0.75f;
										}
									}
									else
									{
										if (Main.item[num36].velocity.Y > -Player.itemGrabSpeedMax)
										{
											Item expr_396B_cp_0 = Main.item[num36];
											expr_396B_cp_0.velocity.Y = expr_396B_cp_0.velocity.Y - Player.itemGrabSpeed;
										}
										if (Main.item[num36].velocity.Y > 0f)
										{
											Item expr_39A2_cp_0 = Main.item[num36];
											expr_39A2_cp_0.velocity.Y = expr_39A2_cp_0.velocity.Y - Player.itemGrabSpeed * 0.75f;
										}
									}
								}
							}
						}
					}
					if (this.position.X / 16f - (float)Player.tileRangeX <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX - 1f >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY - 2f >= (float)Player.tileTargetY && Main.tile[Player.tileTargetX, Player.tileTargetY].active)
					{
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 104)
						{
							this.showItemIcon = true;
							this.showItemIcon2 = 359;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 79)
						{
							this.showItemIcon = true;
							this.showItemIcon2 = 224;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 21)
						{
							this.showItemIcon = true;
							if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX >= 216)
							{
								this.showItemIcon2 = 348;
							}
							else
							{
								if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX >= 180)
								{
									this.showItemIcon2 = 343;
								}
								else
								{
									if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX >= 144)
									{
										this.showItemIcon2 = 329;
									}
									else
									{
										if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX >= 108)
										{
											this.showItemIcon2 = 328;
										}
										else
										{
											if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX >= 72)
											{
												this.showItemIcon2 = 327;
											}
											else
											{
												if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX >= 36)
												{
													this.showItemIcon2 = 306;
												}
												else
												{
													this.showItemIcon2 = 48;
												}
											}
										}
									}
								}
							}
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 4)
						{
							this.showItemIcon = true;
							this.showItemIcon2 = 8;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 13)
						{
							this.showItemIcon = true;
							if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX == 72)
							{
								this.showItemIcon2 = 351;
							}
							else
							{
								if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX == 54)
								{
									this.showItemIcon2 = 350;
								}
								else
								{
									if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX == 18)
									{
										this.showItemIcon2 = 28;
									}
									else
									{
										if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX == 36)
										{
											this.showItemIcon2 = 110;
										}
										else
										{
											this.showItemIcon2 = 31;
										}
									}
								}
							}
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 29)
						{
							this.showItemIcon = true;
							this.showItemIcon2 = 87;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 97)
						{
							this.showItemIcon = true;
							this.showItemIcon2 = 346;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 33)
						{
							this.showItemIcon = true;
							this.showItemIcon2 = 105;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 49)
						{
							this.showItemIcon = true;
							this.showItemIcon2 = 148;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 50 && Main.tile[Player.tileTargetX, Player.tileTargetY].frameX == 90)
						{
							this.showItemIcon = true;
							this.showItemIcon2 = 165;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 55 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 85)
						{
							int num37 = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18);
							int num38 = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 18);
							while (num37 > 1)
							{
								num37 -= 2;
							}
							int num39 = Player.tileTargetX - num37;
							int num40 = Player.tileTargetY - num38;
							Main.signBubble = true;
							Main.signX = num39 * 16 + 16;
							Main.signY = num40 * 16;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 10 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 11)
						{
							this.showItemIcon = true;
							this.showItemIcon2 = 25;
						}
						if (this.controlUseTile)
						{
							if (this.releaseUseTile)
							{
								if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 4 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 13 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 33 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 49 || (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 50 && Main.tile[Player.tileTargetX, Player.tileTargetY].frameX == 90))
								{
									WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, false, false, false);
									if (Main.netMode == 1)
									{
										NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f, 0);
									}
								}
								else
								{
									if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 79)
									{
										int num41 = Player.tileTargetX;
										int num42 = Player.tileTargetY;
										num41 += (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18 * -1);
										if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX >= 72)
										{
											num41 += 4;
											num41++;
										}
										else
										{
											num41 += 2;
										}
										num42 += (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 18 * -1);
										num42 += 2;
										if (Player.CheckSpawn(num41, num42))
										{
											this.ChangeSpawn(num41, num42);
											Main.NewText("Spawn point set!", 255, 240, 20);
										}
									}
									else
									{
										if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 55 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 85)
										{
											bool flag7 = true;
											if (this.sign >= 0)
											{
												int num43 = Sign.ReadSign(Player.tileTargetX, Player.tileTargetY);
												if (num43 == this.sign)
												{
													this.sign = -1;
													Main.npcChatText = "";
													Main.editSign = false;
													Main.PlaySound(11, -1, -1, 1);
													flag7 = false;
												}
											}
											if (flag7)
											{
												if (Main.netMode == 0)
												{
													this.talkNPC = -1;
													Main.playerInventory = false;
													Main.editSign = false;
													Main.PlaySound(10, -1, -1, 1);
													int num44 = Sign.ReadSign(Player.tileTargetX, Player.tileTargetY);
													this.sign = num44;
													Main.npcChatText = Main.sign[num44].text;
												}
												else
												{
													int num45 = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18);
													int num46 = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 18);
													while (num45 > 1)
													{
														num45 -= 2;
													}
													int num47 = Player.tileTargetX - num45;
													int num48 = Player.tileTargetY - num46;
													if (Main.tile[num47, num48].type == 55 || Main.tile[num47, num48].type == 85)
													{
														NetMessage.SendData(46, -1, -1, "", num47, (float)num48, 0f, 0f, 0);
													}
												}
											}
										}
										else
										{
											if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 104)
											{
												string text = "AM";
												double num49 = Main.time;
												if (!Main.dayTime)
												{
													num49 += 54000.0;
												}
												num49 = num49 / 86400.0 * 24.0;
												double num50 = 7.5;
												num49 = num49 - num50 - 12.0;
												if (num49 < 0.0)
												{
													num49 += 24.0;
												}
												if (num49 >= 12.0)
												{
													text = "PM";
												}
												int num51 = (int)num49;
												double num52 = num49 - (double)num51;
												num52 = (double)((int)(num52 * 60.0));
												string text2 = string.Concat(num52);
												if (num52 < 10.0)
												{
													text2 = "0" + text2;
												}
												if (num51 > 12)
												{
													num51 -= 12;
												}
												if (num51 == 0)
												{
													num51 = 12;
												}
												string newText = string.Concat(new object[]
												{
													"Time: ", 
													num51, 
													":", 
													text2, 
													" ", 
													text
												});
												Main.NewText(newText, 255, 240, 20);
											}
											else
											{
												if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 10)
												{
													WorldGen.OpenDoor(Player.tileTargetX, Player.tileTargetY, this.direction);
													NetMessage.SendData(19, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, (float)this.direction, 0);
												}
												else
												{
													if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 11)
													{
														if (WorldGen.CloseDoor(Player.tileTargetX, Player.tileTargetY, false))
														{
															NetMessage.SendData(19, -1, -1, "", 1, (float)Player.tileTargetX, (float)Player.tileTargetY, (float)this.direction, 0);
														}
													}
													else
													{
														if ((Main.tile[Player.tileTargetX, Player.tileTargetY].type == 21 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 29 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 97) && this.talkNPC == -1)
														{
															int num53 = 0;
															int num54;
															for (num54 = (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18); num54 > 1; num54 -= 2)
															{
															}
															num54 = Player.tileTargetX - num54;
															int num55 = Player.tileTargetY - (int)(Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 18);
															if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 29)
															{
																num53 = 1;
															}
															else
															{
																if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 97)
																{
																	num53 = 2;
																}
																else
																{
																	if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX >= 216)
																	{
																		Main.chestText = "Trash Can";
																	}
																	else
																	{
																		if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameX >= 180)
																		{
																			Main.chestText = "Barrel";
																		}
																		else
																		{
																			Main.chestText = "Chest";
																		}
																	}
																}
															}
															if (Main.netMode == 1 && num53 == 0 && (Main.tile[num54, num55].frameX < 72 || Main.tile[num54, num55].frameX > 106) && (Main.tile[num54, num55].frameX < 144 || Main.tile[num54, num55].frameX > 178))
															{
																if (num54 == this.chestX && num55 == this.chestY && this.chest != -1)
																{
																	this.chest = -1;
																	Main.PlaySound(11, -1, -1, 1);
																}
																else
																{
																	NetMessage.SendData(31, -1, -1, "", num54, (float)num55, 0f, 0f, 0);
																}
															}
															else
															{
																int num56 = -1;
																if (num53 == 1)
																{
																	num56 = -2;
																}
																else
																{
																	if (num53 == 2)
																	{
																		num56 = -3;
																	}
																	else
																	{
																		bool flag8 = false;
																		if ((Main.tile[num54, num55].frameX >= 72 && Main.tile[num54, num55].frameX <= 106) || (Main.tile[num54, num55].frameX >= 144 && Main.tile[num54, num55].frameX <= 178))
																		{
																			int num57 = 327;
																			if (Main.tile[num54, num55].frameX >= 144 && Main.tile[num54, num55].frameX <= 178)
																			{
																				num57 = 329;
																			}
																			flag8 = true;
																			for (int num58 = 0; num58 < 44; num58++)
																			{
																				if (this.inventory[num58].type == num57 && this.inventory[num58].stack > 0)
																				{
																					if (num57 != 329)
																					{
																						this.inventory[num58].stack--;
																						if (this.inventory[num58].stack <= 0)
																						{
																							this.inventory[num58] = new Item();
																						}
																					}
																					Chest.Unlock(num54, num55);
																					if (Main.netMode == 1)
																					{
																						NetMessage.SendData(52, -1, -1, "", this.whoAmi, 1f, (float)num54, (float)num55, 0);
																					}
																				}
																			}
																		}
																		if (!flag8)
																		{
																			num56 = Chest.FindChest(num54, num55);
																		}
																	}
																}
																if (num56 != -1)
																{
																	if (num56 == this.chest)
																	{
																		this.chest = -1;
																		Main.PlaySound(11, -1, -1, 1);
																	}
																	else
																	{
																		if (num56 != this.chest && this.chest == -1)
																		{
																			this.chest = num56;
																			Main.playerInventory = true;
																			Main.PlaySound(10, -1, -1, 1);
																			this.chestX = num54;
																			this.chestY = num55;
																		}
																		else
																		{
																			this.chest = num56;
																			Main.playerInventory = true;
																			Main.PlaySound(12, -1, -1, 1);
																			this.chestX = num54;
																			this.chestY = num55;
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
							this.releaseUseTile = false;
						}
						else
						{
							this.releaseUseTile = true;
						}
					}
					if (Main.myPlayer == this.whoAmi)
					{
						if (this.controlHook)
						{
							if (this.releaseHook)
							{
								this.QuickGrapple();
							}
							this.releaseHook = false;
						}
						else
						{
							this.releaseHook = true;
						}
						if (this.talkNPC >= 0)
						{
							Rectangle rectangle2 = new Rectangle((int)(this.position.X + (float)(this.width / 2) - (float)(Player.tileRangeX * 16)), (int)(this.position.Y + (float)(this.height / 2) - (float)(Player.tileRangeY * 16)), Player.tileRangeX * 16 * 2, Player.tileRangeY * 16 * 2);
							Rectangle value = new Rectangle((int)Main.npc[this.talkNPC].position.X, (int)Main.npc[this.talkNPC].position.Y, Main.npc[this.talkNPC].width, Main.npc[this.talkNPC].height);
							if (!rectangle2.Intersects(value) || this.chest != -1 || !Main.npc[this.talkNPC].active)
							{
								if (this.chest == -1)
								{
									Main.PlaySound(11, -1, -1, 1);
								}
								this.talkNPC = -1;
								Main.npcChatText = "";
							}
						}
						if (this.sign >= 0)
						{
							Rectangle rectangle3 = new Rectangle((int)(this.position.X + (float)(this.width / 2) - (float)(Player.tileRangeX * 16)), (int)(this.position.Y + (float)(this.height / 2) - (float)(Player.tileRangeY * 16)), Player.tileRangeX * 16 * 2, Player.tileRangeY * 16 * 2);
							try
							{
								Rectangle value2 = new Rectangle(Main.sign[this.sign].x * 16, Main.sign[this.sign].y * 16, 32, 32);
								if (!rectangle3.Intersects(value2))
								{
									Main.PlaySound(11, -1, -1, 1);
									this.sign = -1;
									Main.editSign = false;
									Main.npcChatText = "";
								}
							}
							catch
							{
								Main.PlaySound(11, -1, -1, 1);
								this.sign = -1;
								Main.editSign = false;
								Main.npcChatText = "";
							}
						}
						if (Main.editSign)
						{
							if (this.sign == -1)
							{
								Main.editSign = false;
							}
							else
							{
								Main.npcChatText = Main.GetInputText(Main.npcChatText);
								if (Main.inputTextEnter)
								{
									byte[] bytes = new byte[]
									{
										10
									};
									Main.npcChatText += Encoding.ASCII.GetString(bytes);
								}
							}
						}
						if (!this.immune)
						{
							Rectangle rectangle4 = new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height);
							for (int num59 = 0; num59 < 1000; num59++)
							{
								if (Main.npc[num59].active && !Main.npc[num59].friendly && Main.npc[num59].damage > 0 && rectangle4.Intersects(new Rectangle((int)Main.npc[num59].position.X, (int)Main.npc[num59].position.Y, Main.npc[num59].width, Main.npc[num59].height)))
								{
									int num60 = -1;
									if (Main.npc[num59].position.X + (float)(Main.npc[num59].width / 2) < this.position.X + (float)(this.width / 2))
									{
										num60 = 1;
									}
									int num61 = Main.DamageVar((float)Main.npc[num59].damage);
									if (this.whoAmi == Main.myPlayer && this.thorns && !this.immune)
									{
										int num62 = num61 / 3;
										int num63 = 10;
										Main.npc[num59].StrikeNPC(num62, (float)num63, -num60, false);
										if (Main.netMode != 0)
										{
											NetMessage.SendData(28, -1, -1, "", num59, (float)num62, (float)num63, (float)(-(float)num60), 0);
										}
									}
									if (!this.immune)
									{
										if (Main.npc[num59].type == 1 && Main.npc[num59].name == "Black Slime" && Main.rand.Next(4) == 0)
										{
											this.AddBuff(22, 900, true);
										}
										if ((Main.npc[num59].type == 23 || Main.npc[num59].type == 25) && Main.rand.Next(3) == 0)
										{
											this.AddBuff(24, 420, true);
										}
										if (Main.npc[num59].type == 34 && Main.rand.Next(3) == 0)
										{
											this.AddBuff(23, 240, true);
										}
									}
									this.Hurt(num61, num60, false, false, Player.getDeathMessage(-1, num59, -1, -1), false);
								}
							}
						}
						Vector2 vector = Collision.HurtTiles(this.position, this.velocity, this.width, this.height, this.fireWalk);
						if (vector.Y != 0f)
						{
							int damage2 = Main.DamageVar(vector.Y);
							this.Hurt(damage2, 0, false, false, Player.getDeathMessage(-1, -1, -1, 3), false);
						}
					}
					if (this.grappling[0] >= 0)
					{
						this.rocketTime = this.rocketTimeMax;
						this.rocketDelay = 0;
						this.rocketFrame = false;
						this.canRocket = false;
						this.rocketRelease = false;
						this.fallStart = (int)(this.position.Y / 16f);
						float num64 = 0f;
						float num65 = 0f;
						for (int num66 = 0; num66 < this.grapCount; num66++)
						{
							num64 += Main.projectile[this.grappling[num66]].position.X + (float)(Main.projectile[this.grappling[num66]].width / 2);
							num65 += Main.projectile[this.grappling[num66]].position.Y + (float)(Main.projectile[this.grappling[num66]].height / 2);
						}
						num64 /= (float)this.grapCount;
						num65 /= (float)this.grapCount;
						Vector2 vector2 = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
						float num67 = num64 - vector2.X;
						float num68 = num65 - vector2.Y;
						float num69 = (float)Math.Sqrt((double)(num67 * num67 + num68 * num68));
						float num70 = 11f;
						float num71 = num69;
						if (num69 > num70)
						{
							num71 = num70 / num69;
						}
						else
						{
							num71 = 1f;
						}
						num67 *= num71;
						num68 *= num71;
						this.velocity.X = num67;
						this.velocity.Y = num68;
						if (this.itemAnimation == 0)
						{
							if (this.velocity.X > 0f)
							{
								this.direction = 1;
							}
							if (this.velocity.X < 0f)
							{
								this.direction = -1;
							}
						}
						if (this.controlJump)
						{
							if (this.releaseJump)
							{
								if ((this.velocity.Y == 0f || (this.wet && (double)this.velocity.Y > -0.02 && (double)this.velocity.Y < 0.02)) && !this.controlDown)
								{
									this.velocity.Y = -Player.jumpSpeed;
									this.jump = Player.jumpHeight / 2;
									this.releaseJump = false;
								}
								else
								{
									this.velocity.Y = this.velocity.Y + 0.01f;
									this.releaseJump = false;
								}
								if (this.doubleJump)
								{
									this.jumpAgain = true;
								}
								this.grappling[0] = 0;
								this.grapCount = 0;
								for (int num72 = 0; num72 < 1000; num72++)
								{
									if (Main.projectile[num72].active && Main.projectile[num72].owner == i && Main.projectile[num72].aiStyle == 7)
									{
										Main.projectile[num72].Kill();
									}
								}
							}
						}
						else
						{
							this.releaseJump = true;
						}
					}
					int num73 = this.width / 2;
					int num74 = this.height / 2;
					new Vector2(this.position.X + (float)(this.width / 2) - (float)(num73 / 2), this.position.Y + (float)(this.height / 2) - (float)(num74 / 2));
					Vector2 vector3 = Collision.StickyTiles(this.position, this.velocity, this.width, this.height);
					if (vector3.Y != -1f && vector3.X != -1f)
					{
						if (this.whoAmi == Main.myPlayer && (this.velocity.X != 0f || this.velocity.Y != 0f))
						{
							this.stickyBreak++;
							if (this.stickyBreak > Main.rand.Next(20, 100))
							{
								this.stickyBreak = 0;
								int num75 = (int)vector3.X;
								int num76 = (int)vector3.Y;
								WorldGen.KillTile(num75, num76, false, false, false);
								if (Main.netMode == 1 && !Main.tile[num75, num76].active && Main.netMode == 1)
								{
									NetMessage.SendData(17, -1, -1, "", 0, (float)num75, (float)num76, 0f, 0);
								}
							}
						}
						this.fallStart = (int)(this.position.Y / 16f);
						this.jump = 0;
						if (this.velocity.X > 1f)
						{
							this.velocity.X = 1f;
						}
						if (this.velocity.X < -1f)
						{
							this.velocity.X = -1f;
						}
						if (this.velocity.Y > 1f)
						{
							this.velocity.Y = 1f;
						}
						if (this.velocity.Y < -5f)
						{
							this.velocity.Y = -5f;
						}
						if ((double)this.velocity.X > 0.75 || (double)this.velocity.X < -0.75)
						{
							this.velocity.X = this.velocity.X * 0.85f;
						}
						else
						{
							this.velocity.X = this.velocity.X * 0.6f;
						}
						if (this.velocity.Y < 0f)
						{
							this.velocity.Y = this.velocity.Y * 0.96f;
						}
						else
						{
							this.velocity.Y = this.velocity.Y * 0.3f;
						}
					}
					else
					{
						this.stickyBreak = 0;
					}
					bool flag9 = Collision.DrownCollision(this.position, this.width, this.height, this.gravDir);
					if (this.armor[0].type == 250)
					{
						flag9 = true;
					}
					if (this.inventory[this.selectedItem].type == 186)
					{
						try
						{
							int num77 = (int)((this.position.X + (float)(this.width / 2) + (float)(6 * this.direction)) / 16f);
							int num78 = 0;
							if (this.gravDir == -1f)
							{
								num78 = this.height;
							}
							int num79 = (int)((this.position.Y + (float)num78 - 44f * this.gravDir) / 16f);
							if (Main.tile[num77, num79].liquid < 128)
							{
								if (Main.tile[num77, num79] == null)
								{
									Main.tile[num77, num79] = new Tile();
								}
								if (!Main.tile[num77, num79].active || !Main.tileSolid[(int)Main.tile[num77, num79].type] || Main.tileSolidTop[(int)Main.tile[num77, num79].type])
								{
									flag9 = false;
								}
							}
						}
						catch
						{
						}
					}
					if (this.gills)
					{
						flag9 = !flag9;
					}
					if (Main.myPlayer == i)
					{
						if (flag9)
						{
							this.breathCD++;
							int num80 = 7;
							if (this.inventory[this.selectedItem].type == 186)
							{
								num80 *= 2;
							}
							if (this.armor[0].type == 268)
							{
								num80 *= 4;
							}
							if (this.breathCD >= num80)
							{
								this.breathCD = 0;
								this.breath--;
								if (this.breath == 0)
								{
									Main.PlaySound(23, -1, -1, 1);
								}
								if (this.breath <= 0)
								{
									this.lifeRegenTime = 0;
									this.breath = 0;
									this.statLife -= 2;
									if (this.statLife <= 0)
									{
										this.statLife = 0;
										this.KillMe(10.0, 0, false, Player.getDeathMessage(-1, -1, -1, 1));
									}
								}
							}
						}
						else
						{
							this.breath += 3;
							if (this.breath > this.breathMax)
							{
								this.breath = this.breathMax;
							}
							this.breathCD = 0;
						}
					}
					if (flag9 && Main.rand.Next(20) == 0 && !this.lavaWet)
					{
						int num81 = 0;
						if (this.gravDir == -1f)
						{
							num81 += this.height - 12;
						}
						if (this.inventory[this.selectedItem].type == 186)
						{
							Vector2 arg_5715_0 = new Vector2(this.position.X + (float)(10 * this.direction) + 4f, this.position.Y + (float)num81 - 54f * this.gravDir);
							int arg_5715_1 = this.width - 8;
							int arg_5715_2 = 8;
							int arg_5715_3 = 34;
							float arg_5715_4 = 0f;
							float arg_5715_5 = 0f;
							int arg_5715_6 = 0;
							Color newColor = default(Color);
							Dust.NewDust(arg_5715_0, arg_5715_1, arg_5715_2, arg_5715_3, arg_5715_4, arg_5715_5, arg_5715_6, newColor, 1.2f);
						}
						else
						{
							Vector2 arg_5779_0 = new Vector2(this.position.X + (float)(12 * this.direction), this.position.Y + (float)num81 + 4f * this.gravDir);
							int arg_5779_1 = this.width - 8;
							int arg_5779_2 = 8;
							int arg_5779_3 = 34;
							float arg_5779_4 = 0f;
							float arg_5779_5 = 0f;
							int arg_5779_6 = 0;
							Color newColor = default(Color);
							Dust.NewDust(arg_5779_0, arg_5779_1, arg_5779_2, arg_5779_3, arg_5779_4, arg_5779_5, arg_5779_6, newColor, 1.2f);
						}
					}
					int num82 = this.height;
					if (this.waterWalk)
					{
						num82 -= 6;
					}
					bool flag10 = Collision.LavaCollision(this.position, this.width, num82);
					if (flag10)
					{
						if (!this.lavaImmune && Main.myPlayer == i && !this.immune)
						{
							this.AddBuff(24, 420, true);
							this.Hurt(80, 0, false, false, Player.getDeathMessage(-1, -1, -1, 2), false);
						}
						this.lavaWet = true;
					}
					bool flag11 = Collision.WetCollision(this.position, this.width, this.height);
					if (flag11)
					{
						if (this.onFire && !this.lavaWet)
						{
							for (int num83 = 0; num83 < 10; num83++)
							{
								if (this.buffType[num83] == 24)
								{
									this.DelBuff(num83);
								}
							}
						}
						if (!this.wet)
						{
							if (this.wetCount == 0)
							{
								this.wetCount = 10;
								if (!flag10)
								{
									for (int num84 = 0; num84 < 50; num84++)
									{
										Vector2 arg_58CC_0 = new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f);
										int arg_58CC_1 = this.width + 12;
										int arg_58CC_2 = 24;
										int arg_58CC_3 = 33;
										float arg_58CC_4 = 0f;
										float arg_58CC_5 = 0f;
										int arg_58CC_6 = 0;
										Color newColor = default(Color);
										int num85 = Dust.NewDust(arg_58CC_0, arg_58CC_1, arg_58CC_2, arg_58CC_3, arg_58CC_4, arg_58CC_5, arg_58CC_6, newColor, 1f);
										Dust expr_58E0_cp_0 = Main.dust[num85];
										expr_58E0_cp_0.velocity.Y = expr_58E0_cp_0.velocity.Y - 4f;
										Dust expr_58FE_cp_0 = Main.dust[num85];
										expr_58FE_cp_0.velocity.X = expr_58FE_cp_0.velocity.X * 2.5f;
										Main.dust[num85].scale = 1.3f;
										Main.dust[num85].alpha = 100;
										Main.dust[num85].noGravity = true;
									}
									Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 0);
								}
								else
								{
									for (int num86 = 0; num86 < 20; num86++)
									{
										Vector2 arg_59D2_0 = new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f);
										int arg_59D2_1 = this.width + 12;
										int arg_59D2_2 = 24;
										int arg_59D2_3 = 35;
										float arg_59D2_4 = 0f;
										float arg_59D2_5 = 0f;
										int arg_59D2_6 = 0;
										Color newColor = default(Color);
										int num87 = Dust.NewDust(arg_59D2_0, arg_59D2_1, arg_59D2_2, arg_59D2_3, arg_59D2_4, arg_59D2_5, arg_59D2_6, newColor, 1f);
										Dust expr_59E6_cp_0 = Main.dust[num87];
										expr_59E6_cp_0.velocity.Y = expr_59E6_cp_0.velocity.Y - 1.5f;
										Dust expr_5A04_cp_0 = Main.dust[num87];
										expr_5A04_cp_0.velocity.X = expr_5A04_cp_0.velocity.X * 2.5f;
										Main.dust[num87].scale = 1.3f;
										Main.dust[num87].alpha = 100;
										Main.dust[num87].noGravity = true;
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
							if (this.jump > Player.jumpHeight / 5)
							{
								this.jump = Player.jumpHeight / 5;
							}
							if (this.wetCount == 0)
							{
								this.wetCount = 10;
								if (!this.lavaWet)
								{
									for (int num88 = 0; num88 < 50; num88++)
									{
										Vector2 arg_5B25_0 = new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2));
										int arg_5B25_1 = this.width + 12;
										int arg_5B25_2 = 24;
										int arg_5B25_3 = 33;
										float arg_5B25_4 = 0f;
										float arg_5B25_5 = 0f;
										int arg_5B25_6 = 0;
										Color newColor = default(Color);
										int num89 = Dust.NewDust(arg_5B25_0, arg_5B25_1, arg_5B25_2, arg_5B25_3, arg_5B25_4, arg_5B25_5, arg_5B25_6, newColor, 1f);
										Dust expr_5B39_cp_0 = Main.dust[num89];
										expr_5B39_cp_0.velocity.Y = expr_5B39_cp_0.velocity.Y - 4f;
										Dust expr_5B57_cp_0 = Main.dust[num89];
										expr_5B57_cp_0.velocity.X = expr_5B57_cp_0.velocity.X * 2.5f;
										Main.dust[num89].scale = 1.3f;
										Main.dust[num89].alpha = 100;
										Main.dust[num89].noGravity = true;
									}
									Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 0);
								}
								else
								{
									for (int num90 = 0; num90 < 20; num90++)
									{
										Vector2 arg_5C2B_0 = new Vector2(this.position.X - 6f, this.position.Y + (float)(this.height / 2) - 8f);
										int arg_5C2B_1 = this.width + 12;
										int arg_5C2B_2 = 24;
										int arg_5C2B_3 = 35;
										float arg_5C2B_4 = 0f;
										float arg_5C2B_5 = 0f;
										int arg_5C2B_6 = 0;
										Color newColor = default(Color);
										int num91 = Dust.NewDust(arg_5C2B_0, arg_5C2B_1, arg_5C2B_2, arg_5C2B_3, arg_5C2B_4, arg_5C2B_5, arg_5C2B_6, newColor, 1f);
										Dust expr_5C3F_cp_0 = Main.dust[num91];
										expr_5C3F_cp_0.velocity.Y = expr_5C3F_cp_0.velocity.Y - 1.5f;
										Dust expr_5C5D_cp_0 = Main.dust[num91];
										expr_5C5D_cp_0.velocity.X = expr_5C5D_cp_0.velocity.X * 2.5f;
										Main.dust[num91].scale = 1.3f;
										Main.dust[num91].alpha = 100;
										Main.dust[num91].noGravity = true;
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
					if (this.wet)
					{
						Vector2 vector4 = this.velocity;
						this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, this.controlDown, false);
						Vector2 value3 = this.velocity * 0.5f;
						if (this.velocity.X != vector4.X)
						{
							value3.X = this.velocity.X;
						}
						if (this.velocity.Y != vector4.Y)
						{
							value3.Y = this.velocity.Y;
						}
						this.position += value3;
					}
					else
					{
						this.velocity = Collision.TileCollision(this.position, this.velocity, this.width, this.height, this.controlDown, false);
						if (this.waterWalk)
						{
							this.velocity = Collision.WaterCollision(this.position, this.velocity, this.width, this.height, this.controlDown, false);
						}
						this.position += this.velocity;
					}
					if (this.velocity.Y == 0f)
					{
						if (this.gravDir == 1f && Collision.up)
						{
							this.velocity.Y = 0.01f;
							this.jump = 0;
						}
						else
						{
							if (this.gravDir == -1f && Collision.down)
							{
								this.velocity.Y = -0.01f;
								this.jump = 0;
							}
						}
					}
					if (this.position.X < Main.leftWorld + 336f + 16f)
					{
						this.position.X = Main.leftWorld + 336f + 16f;
						this.velocity.X = 0f;
					}
					if (this.position.X + (float)this.width > Main.rightWorld - 336f - 32f)
					{
						this.position.X = Main.rightWorld - 336f - 32f - (float)this.width;
						this.velocity.X = 0f;
					}
					if (this.position.Y < Main.topWorld + 336f + 16f)
					{
						this.position.Y = Main.topWorld + 336f + 16f;
						if ((double)this.velocity.Y < -0.1)
						{
							this.velocity.Y = -0.1f;
						}
					}
					if (this.position.Y > Main.bottomWorld - 336f - 32f - (float)this.height)
					{
						this.position.Y = Main.bottomWorld - 336f - 32f - (float)this.height;
						this.velocity.Y = 0f;
					}
					if (Main.ignoreErrors)
					{
						try
						{
							this.ItemCheck(i);
							goto IL_5FF9;
						}
						catch
						{
							goto IL_5FF9;
						}
						goto IL_5FF2;
					}
					goto IL_5FF2;
					IL_5FF9:
					this.PlayerFrame();
					if (this.statLife > this.statLifeMax)
					{
						this.statLife = this.statLifeMax;
					}
					this.grappling[0] = -1;
					this.grapCount = 0;
					return;
					IL_5FF2:
					this.ItemCheck(i);
					goto IL_5FF9;
				}
				this.poisoned = false;
				this.onFire = false;
				this.blind = false;
				this.gravDir = 1f;
				for (int num92 = 0; num92 < 10; num92++)
				{
					this.buffTime[num92] = 0;
					this.buffType[num92] = 0;
				}
				if (i == Main.myPlayer)
				{
					Main.npcChatText = "";
					Main.editSign = false;
				}
				this.grappling[0] = -1;
				this.grappling[1] = -1;
				this.grappling[2] = -1;
				this.sign = -1;
				this.talkNPC = -1;
				this.statLife = 0;
				this.channel = false;
				this.potionDelay = 0;
				this.chest = -1;
				this.changeItem = -1;
				this.itemAnimation = 0;
				this.immuneAlpha += 2;
				if (this.immuneAlpha > 255)
				{
					this.immuneAlpha = 255;
				}
				this.headPosition += this.headVelocity;
				this.bodyPosition += this.bodyVelocity;
				this.legPosition += this.legVelocity;
				this.headRotation += this.headVelocity.X * 0.1f;
				this.bodyRotation += this.bodyVelocity.X * 0.1f;
				this.legRotation += this.legVelocity.X * 0.1f;
				this.headVelocity.Y = this.headVelocity.Y + 0.1f;
				this.bodyVelocity.Y = this.bodyVelocity.Y + 0.1f;
				this.legVelocity.Y = this.legVelocity.Y + 0.1f;
				this.headVelocity.X = this.headVelocity.X * 0.99f;
				this.bodyVelocity.X = this.bodyVelocity.X * 0.99f;
				this.legVelocity.X = this.legVelocity.X * 0.99f;
				if (this.difficulty == 2)
				{
					if (this.respawnTimer > 0)
					{
						this.respawnTimer--;
						return;
					}
					if (this.whoAmi == Main.myPlayer || Main.netMode == 2)
					{
						this.ghost = true;
						return;
					}
				}
				else
				{
					this.respawnTimer--;
					if (this.respawnTimer <= 0 && Main.myPlayer == this.whoAmi)
					{
						this.Spawn();
						return;
					}
				}
			}
		}
		public bool SellItem(int price, int stack)
		{
			if (price <= 0)
			{
				return false;
			}
			Item[] array = new Item[44];
			for (int i = 0; i < 44; i++)
			{
				array[i] = new Item();
				array[i] = (Item)this.inventory[i].Clone();
			}
			int j = price / 5;
			j *= stack;
			if (j < 1)
			{
				j = 1;
			}
			bool flag = false;
			while (j >= 1000000)
			{
				if (flag)
				{
					break;
				}
				int num = -1;
				for (int k = 43; k >= 0; k--)
				{
					if (num == -1 && (this.inventory[k].type == 0 || this.inventory[k].stack == 0))
					{
						num = k;
					}
					while (this.inventory[k].type == 74 && this.inventory[k].stack < this.inventory[k].maxStack && j >= 1000000)
					{
						this.inventory[k].stack++;
						j -= 1000000;
						this.DoCoins(k);
						if (this.inventory[k].stack == 0 && num == -1)
						{
							num = k;
						}
					}
				}
				if (j >= 1000000)
				{
					if (num == -1)
					{
						flag = true;
					}
					else
					{
						this.inventory[num].SetDefaults(74, false);
						j -= 1000000;
					}
				}
			}
			while (j >= 10000)
			{
				if (flag)
				{
					break;
				}
				int num2 = -1;
				for (int l = 43; l >= 0; l--)
				{
					if (num2 == -1 && (this.inventory[l].type == 0 || this.inventory[l].stack == 0))
					{
						num2 = l;
					}
					while (this.inventory[l].type == 73 && this.inventory[l].stack < this.inventory[l].maxStack && j >= 10000)
					{
						this.inventory[l].stack++;
						j -= 10000;
						this.DoCoins(l);
						if (this.inventory[l].stack == 0 && num2 == -1)
						{
							num2 = l;
						}
					}
				}
				if (j >= 10000)
				{
					if (num2 == -1)
					{
						flag = true;
					}
					else
					{
						this.inventory[num2].SetDefaults(73, false);
						j -= 10000;
					}
				}
			}
			while (j >= 100)
			{
				if (flag)
				{
					break;
				}
				int num3 = -1;
				for (int m = 43; m >= 0; m--)
				{
					if (num3 == -1 && (this.inventory[m].type == 0 || this.inventory[m].stack == 0))
					{
						num3 = m;
					}
					while (this.inventory[m].type == 72 && this.inventory[m].stack < this.inventory[m].maxStack && j >= 100)
					{
						this.inventory[m].stack++;
						j -= 100;
						this.DoCoins(m);
						if (this.inventory[m].stack == 0 && num3 == -1)
						{
							num3 = m;
						}
					}
				}
				if (j >= 100)
				{
					if (num3 == -1)
					{
						flag = true;
					}
					else
					{
						this.inventory[num3].SetDefaults(72, false);
						j -= 100;
					}
				}
			}
			while (j >= 1 && !flag)
			{
				int num4 = -1;
				for (int n = 43; n >= 0; n--)
				{
					if (num4 == -1 && (this.inventory[n].type == 0 || this.inventory[n].stack == 0))
					{
						num4 = n;
					}
					while (this.inventory[n].type == 71 && this.inventory[n].stack < this.inventory[n].maxStack && j >= 1)
					{
						this.inventory[n].stack++;
						j--;
						this.DoCoins(n);
						if (this.inventory[n].stack == 0 && num4 == -1)
						{
							num4 = n;
						}
					}
				}
				if (j >= 1)
				{
					if (num4 == -1)
					{
						flag = true;
					}
					else
					{
						this.inventory[num4].SetDefaults(71, false);
						j--;
					}
				}
			}
			if (flag)
			{
				for (int num5 = 0; num5 < 44; num5++)
				{
					this.inventory[num5] = (Item)array[num5].Clone();
				}
				return false;
			}
			return true;
		}
		public bool BuyItem(int price)
		{
			if (price == 0)
			{
				return false;
			}
			int num = 0;
			int i = price;
			Item[] array = new Item[44];
			for (int j = 0; j < 44; j++)
			{
				array[j] = new Item();
				array[j] = (Item)this.inventory[j].Clone();
				if (this.inventory[j].type == 71)
				{
					num += this.inventory[j].stack;
				}
				if (this.inventory[j].type == 72)
				{
					num += this.inventory[j].stack * 100;
				}
				if (this.inventory[j].type == 73)
				{
					num += this.inventory[j].stack * 10000;
				}
				if (this.inventory[j].type == 74)
				{
					num += this.inventory[j].stack * 1000000;
				}
			}
			if (num >= price)
			{
				i = price;
				while (i > 0)
				{
					if (i >= 1000000)
					{
						for (int k = 0; k < 44; k++)
						{
							if (this.inventory[k].type == 74)
							{
								while (this.inventory[k].stack > 0 && i >= 1000000)
								{
									i -= 1000000;
									this.inventory[k].stack--;
									if (this.inventory[k].stack == 0)
									{
										this.inventory[k].type = 0;
									}
								}
							}
						}
					}
					if (i >= 10000)
					{
						for (int l = 0; l < 44; l++)
						{
							if (this.inventory[l].type == 73)
							{
								while (this.inventory[l].stack > 0 && i >= 10000)
								{
									i -= 10000;
									this.inventory[l].stack--;
									if (this.inventory[l].stack == 0)
									{
										this.inventory[l].type = 0;
									}
								}
							}
						}
					}
					if (i >= 100)
					{
						for (int m = 0; m < 44; m++)
						{
							if (this.inventory[m].type == 72)
							{
								while (this.inventory[m].stack > 0 && i >= 100)
								{
									i -= 100;
									this.inventory[m].stack--;
									if (this.inventory[m].stack == 0)
									{
										this.inventory[m].type = 0;
									}
								}
							}
						}
					}
					if (i >= 1)
					{
						for (int n = 0; n < 44; n++)
						{
							if (this.inventory[n].type == 71)
							{
								while (this.inventory[n].stack > 0 && i >= 1)
								{
									i--;
									this.inventory[n].stack--;
									if (this.inventory[n].stack == 0)
									{
										this.inventory[n].type = 0;
									}
								}
							}
						}
					}
					if (i > 0)
					{
						int num2 = -1;
						for (int num3 = 43; num3 >= 0; num3--)
						{
							if (this.inventory[num3].type == 0 || this.inventory[num3].stack == 0)
							{
								num2 = num3;
								break;
							}
						}
						if (num2 < 0)
						{
							for (int num4 = 0; num4 < 44; num4++)
							{
								this.inventory[num4] = (Item)array[num4].Clone();
							}
							return false;
						}
						bool flag = true;
						if (i >= 10000)
						{
							for (int num5 = 0; num5 < 44; num5++)
							{
								if (this.inventory[num5].type == 74 && this.inventory[num5].stack >= 1)
								{
									this.inventory[num5].stack--;
									if (this.inventory[num5].stack == 0)
									{
										this.inventory[num5].type = 0;
									}
									this.inventory[num2].SetDefaults(73, false);
									this.inventory[num2].stack = 100;
									flag = false;
									break;
								}
							}
						}
						else
						{
							if (i >= 100)
							{
								for (int num6 = 0; num6 < 44; num6++)
								{
									if (this.inventory[num6].type == 73 && this.inventory[num6].stack >= 1)
									{
										this.inventory[num6].stack--;
										if (this.inventory[num6].stack == 0)
										{
											this.inventory[num6].type = 0;
										}
										this.inventory[num2].SetDefaults(72, false);
										this.inventory[num2].stack = 100;
										flag = false;
										break;
									}
								}
							}
							else
							{
								if (i >= 1)
								{
									for (int num7 = 0; num7 < 44; num7++)
									{
										if (this.inventory[num7].type == 72 && this.inventory[num7].stack >= 1)
										{
											this.inventory[num7].stack--;
											if (this.inventory[num7].stack == 0)
											{
												this.inventory[num7].type = 0;
											}
											this.inventory[num2].SetDefaults(71, false);
											this.inventory[num2].stack = 100;
											flag = false;
											break;
										}
									}
								}
							}
						}
						if (flag)
						{
							if (i < 10000)
							{
								for (int num8 = 0; num8 < 44; num8++)
								{
									if (this.inventory[num8].type == 73 && this.inventory[num8].stack >= 1)
									{
										this.inventory[num8].stack--;
										if (this.inventory[num8].stack == 0)
										{
											this.inventory[num8].type = 0;
										}
										this.inventory[num2].SetDefaults(72, false);
										this.inventory[num2].stack = 100;
										flag = false;
										break;
									}
								}
							}
							if (flag && i < 1000000)
							{
								for (int num9 = 0; num9 < 44; num9++)
								{
									if (this.inventory[num9].type == 74 && this.inventory[num9].stack >= 1)
									{
										this.inventory[num9].stack--;
										if (this.inventory[num9].stack == 0)
										{
											this.inventory[num9].type = 0;
										}
										this.inventory[num2].SetDefaults(73, false);
										this.inventory[num2].stack = 100;
										flag = false;
										break;
									}
								}
							}
						}
					}
				}
				return true;
			}
			return false;
		}
		public void AdjTiles()
		{
			int num = 4;
			int num2 = 3;
			for (int i = 0; i < 107; i++)
			{
				this.oldAdjTile[i] = this.adjTile[i];
				this.adjTile[i] = false;
			}
			this.oldAdjWater = this.adjWater;
			this.adjWater = false;
			int num3 = (int)((this.position.X + (float)(this.width / 2)) / 16f);
			int num4 = (int)((this.position.Y + (float)this.height) / 16f);
			for (int j = num3 - num; j <= num3 + num; j++)
			{
				for (int k = num4 - num2; k < num4 + num2; k++)
				{
					if (Main.tile[j, k].active)
					{
						this.adjTile[(int)Main.tile[j, k].type] = true;
						if (Main.tile[j, k].type == 77)
						{
							this.adjTile[17] = true;
						}
					}
					if (Main.tile[j, k].liquid > 200 && !Main.tile[j, k].lava)
					{
						this.adjWater = true;
					}
				}
			}
			if (Main.playerInventory)
			{
				bool flag = false;
				for (int l = 0; l < 107; l++)
				{
					if (this.oldAdjTile[l] != this.adjTile[l])
					{
						flag = true;
						break;
					}
				}
				if (this.adjWater != this.oldAdjWater)
				{
					flag = true;
				}
				if (flag)
				{
					Recipe.FindRecipes();
				}
			}
		}
		public void PlayerFrame()
		{
			if (this.swimTime > 0)
			{
				this.swimTime--;
				if (!this.wet)
				{
					this.swimTime = 0;
				}
			}
			this.head = this.armor[0].headSlot;
			this.body = this.armor[1].bodySlot;
			this.legs = this.armor[2].legSlot;
			if (!this.hostile)
			{
				if (this.armor[8].headSlot >= 0)
				{
					this.head = this.armor[8].headSlot;
				}
				if (this.armor[9].bodySlot >= 0)
				{
					this.body = this.armor[9].bodySlot;
				}
				if (this.armor[10].legSlot >= 0)
				{
					this.legs = this.armor[10].legSlot;
				}
			}
			this.socialShadow = false;
			if (this.head == 5 && this.body == 5 && this.legs == 5)
			{
				this.socialShadow = true;
			}
			if (this.head == 5 && this.body == 5 && this.legs == 5 && Main.rand.Next(10) == 0)
			{
				Dust.NewDust(new Vector2(this.position.X, this.position.Y), this.width, this.height, 14, 0f, 0f, 200, default(Color), 1.2f);
			}
			if (this.head == 6 && this.body == 6 && this.legs == 6 && Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y) > 1f && !this.rocketFrame)
			{
				for (int i = 0; i < 2; i++)
				{
					int num = Dust.NewDust(new Vector2(this.position.X - this.velocity.X * 2f, this.position.Y - 2f - this.velocity.Y * 2f), this.width, this.height, 6, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num].noGravity = true;
					Main.dust[num].noLight = true;
					Dust expr_26D_cp_0 = Main.dust[num];
					expr_26D_cp_0.velocity.X = expr_26D_cp_0.velocity.X - this.velocity.X * 0.5f;
					Dust expr_296_cp_0 = Main.dust[num];
					expr_296_cp_0.velocity.Y = expr_296_cp_0.velocity.Y - this.velocity.Y * 0.5f;
				}
			}
			if (this.head == 7 && this.body == 7 && this.legs == 7)
			{
				this.boneArmor = true;
			}
			if (this.head == 8 && this.body == 8 && this.legs == 8 && Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y) > 1f)
			{
				int num2 = Dust.NewDust(new Vector2(this.position.X - this.velocity.X * 2f, this.position.Y - 2f - this.velocity.Y * 2f), this.width, this.height, 40, 0f, 0f, 50, default(Color), 1.4f);
				Main.dust[num2].noGravity = true;
				Main.dust[num2].velocity.X = this.velocity.X * 0.25f;
				Main.dust[num2].velocity.Y = this.velocity.Y * 0.25f;
			}
			if (this.head == 9 && this.body == 9 && this.legs == 9 && Math.Abs(this.velocity.X) + Math.Abs(this.velocity.Y) > 1f && !this.rocketFrame)
			{
				for (int j = 0; j < 2; j++)
				{
					int num3 = Dust.NewDust(new Vector2(this.position.X - this.velocity.X * 2f, this.position.Y - 2f - this.velocity.Y * 2f), this.width, this.height, 6, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num3].noGravity = true;
					Main.dust[num3].noLight = true;
					Dust expr_4F5_cp_0 = Main.dust[num3];
					expr_4F5_cp_0.velocity.X = expr_4F5_cp_0.velocity.X - this.velocity.X * 0.5f;
					Dust expr_51F_cp_0 = Main.dust[num3];
					expr_51F_cp_0.velocity.Y = expr_51F_cp_0.velocity.Y - this.velocity.Y * 0.5f;
				}
			}
			this.bodyFrame.Width = 40;
			this.bodyFrame.Height = 56;
			this.legFrame.Width = 40;
			this.legFrame.Height = 56;
			this.bodyFrame.X = 0;
			this.legFrame.X = 0;
			if (this.itemAnimation > 0 && this.inventory[this.selectedItem].useStyle != 10)
			{
				if (this.inventory[this.selectedItem].useStyle == 1 || this.inventory[this.selectedItem].type == 0)
				{
					if ((double)this.itemAnimation < (double)this.itemAnimationMax * 0.333)
					{
						this.bodyFrame.Y = this.bodyFrame.Height * 3;
					}
					else
					{
						if ((double)this.itemAnimation < (double)this.itemAnimationMax * 0.666)
						{
							this.bodyFrame.Y = this.bodyFrame.Height * 2;
						}
						else
						{
							this.bodyFrame.Y = this.bodyFrame.Height;
						}
					}
				}
				else
				{
					if (this.inventory[this.selectedItem].useStyle == 2)
					{
						if ((double)this.itemAnimation > (double)this.itemAnimationMax * 0.5)
						{
							this.bodyFrame.Y = this.bodyFrame.Height * 3;
						}
						else
						{
							this.bodyFrame.Y = this.bodyFrame.Height * 2;
						}
					}
					else
					{
						if (this.inventory[this.selectedItem].useStyle == 3)
						{
							if ((double)this.itemAnimation > (double)this.itemAnimationMax * 0.666)
							{
								this.bodyFrame.Y = this.bodyFrame.Height * 3;
							}
							else
							{
								this.bodyFrame.Y = this.bodyFrame.Height * 3;
							}
						}
						else
						{
							if (this.inventory[this.selectedItem].useStyle == 4)
							{
								this.bodyFrame.Y = this.bodyFrame.Height * 2;
							}
							else
							{
								if (this.inventory[this.selectedItem].useStyle == 5)
								{
									if (this.inventory[this.selectedItem].type == 281)
									{
										this.bodyFrame.Y = this.bodyFrame.Height * 2;
									}
									else
									{
										float num4 = this.itemRotation * (float)this.direction;
										this.bodyFrame.Y = this.bodyFrame.Height * 3;
										if ((double)num4 < -0.75)
										{
											this.bodyFrame.Y = this.bodyFrame.Height * 2;
											if (this.gravDir == -1f)
											{
												this.bodyFrame.Y = this.bodyFrame.Height * 4;
											}
										}
										if ((double)num4 > 0.6)
										{
											this.bodyFrame.Y = this.bodyFrame.Height * 4;
											if (this.gravDir == -1f)
											{
												this.bodyFrame.Y = this.bodyFrame.Height * 2;
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
				if (this.inventory[this.selectedItem].holdStyle == 1 && (!this.wet || !this.inventory[this.selectedItem].noWet))
				{
					this.bodyFrame.Y = this.bodyFrame.Height * 3;
				}
				else
				{
					if (this.inventory[this.selectedItem].holdStyle == 2 && (!this.wet || !this.inventory[this.selectedItem].noWet))
					{
						this.bodyFrame.Y = this.bodyFrame.Height * 2;
					}
					else
					{
						if (this.grappling[0] >= 0)
						{
							Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
							float num5 = 0f;
							float num6 = 0f;
							for (int k = 0; k < this.grapCount; k++)
							{
								num5 += Main.projectile[this.grappling[k]].position.X + (float)(Main.projectile[this.grappling[k]].width / 2);
								num6 += Main.projectile[this.grappling[k]].position.Y + (float)(Main.projectile[this.grappling[k]].height / 2);
							}
							num5 /= (float)this.grapCount;
							num6 /= (float)this.grapCount;
							num5 -= vector.X;
							num6 -= vector.Y;
							if (num6 < 0f && Math.Abs(num6) > Math.Abs(num5))
							{
								this.bodyFrame.Y = this.bodyFrame.Height * 2;
								if (this.gravDir == -1f)
								{
									this.bodyFrame.Y = this.bodyFrame.Height * 4;
								}
							}
							else
							{
								if (num6 > 0f && Math.Abs(num6) > Math.Abs(num5))
								{
									this.bodyFrame.Y = this.bodyFrame.Height * 4;
									if (this.gravDir == -1f)
									{
										this.bodyFrame.Y = this.bodyFrame.Height * 2;
									}
								}
								else
								{
									this.bodyFrame.Y = this.bodyFrame.Height * 3;
								}
							}
						}
						else
						{
							if (this.swimTime > 0)
							{
								if (this.swimTime > 20)
								{
									this.bodyFrame.Y = 0;
								}
								else
								{
									if (this.swimTime > 10)
									{
										this.bodyFrame.Y = this.bodyFrame.Height * 5;
									}
									else
									{
										this.bodyFrame.Y = 0;
									}
								}
							}
							else
							{
								if (this.velocity.Y != 0f)
								{
									this.bodyFrameCounter = 0.0;
									this.bodyFrame.Y = this.bodyFrame.Height * 5;
								}
								else
								{
									if (this.velocity.X != 0f)
									{
										this.bodyFrameCounter += (double)Math.Abs(this.velocity.X) * 1.5;
										this.bodyFrame.Y = this.legFrame.Y;
									}
									else
									{
										this.bodyFrameCounter = 0.0;
										this.bodyFrame.Y = 0;
									}
								}
							}
						}
					}
				}
			}
			if (this.swimTime > 0)
			{
				this.legFrameCounter += 2.0;
				while (this.legFrameCounter > 8.0)
				{
					this.legFrameCounter -= 8.0;
					this.legFrame.Y = this.legFrame.Y + this.legFrame.Height;
				}
				if (this.legFrame.Y < this.legFrame.Height * 7)
				{
					this.legFrame.Y = this.legFrame.Height * 19;
					return;
				}
				if (this.legFrame.Y > this.legFrame.Height * 19)
				{
					this.legFrame.Y = this.legFrame.Height * 7;
					return;
				}
			}
			else
			{
				if (this.velocity.Y != 0f || this.grappling[0] > -1)
				{
					this.legFrameCounter = 0.0;
					this.legFrame.Y = this.legFrame.Height * 5;
					return;
				}
				if (this.velocity.X != 0f)
				{
					this.legFrameCounter += (double)Math.Abs(this.velocity.X) * 1.3;
					while (this.legFrameCounter > 8.0)
					{
						this.legFrameCounter -= 8.0;
						this.legFrame.Y = this.legFrame.Y + this.legFrame.Height;
					}
					if (this.legFrame.Y < this.legFrame.Height * 7)
					{
						this.legFrame.Y = this.legFrame.Height * 19;
						return;
					}
					if (this.legFrame.Y > this.legFrame.Height * 19)
					{
						this.legFrame.Y = this.legFrame.Height * 7;
						return;
					}
				}
				else
				{
					this.legFrameCounter = 0.0;
					this.legFrame.Y = 0;
				}
			}
		}
		public void Spawn()
		{
			if (this.whoAmi == Main.myPlayer)
			{
				this.FindSpawn();
				if (!Player.CheckSpawn(this.SpawnX, this.SpawnY))
				{
					this.SpawnX = -1;
					this.SpawnY = -1;
				}
			}
			if (Main.netMode == 1 && this.whoAmi == Main.myPlayer)
			{
				NetMessage.SendData(12, -1, -1, "", Main.myPlayer, 0f, 0f, 0f, 0);
				Main.gameMenu = false;
			}
			this.headPosition = default(Vector2);
			this.bodyPosition = default(Vector2);
			this.legPosition = default(Vector2);
			this.headRotation = 0f;
			this.bodyRotation = 0f;
			this.legRotation = 0f;
			if (this.statLife <= 0)
			{
				this.statLife = 100;
				this.breath = this.breathMax;
				if (this.spawnMax)
				{
					this.statLife = this.statLifeMax;
					this.statMana = this.statManaMax2;
				}
			}
			this.immune = true;
			this.dead = false;
			this.immuneTime = 0;
			this.active = true;
			if (this.SpawnX >= 0 && this.SpawnY >= 0)
			{
				this.position.X = (float)(this.SpawnX * 16 + 8 - this.width / 2);
				this.position.Y = (float)(this.SpawnY * 16 - this.height);
			}
			else
			{
				this.position.X = (float)(Main.spawnTileX * 16 + 8 - this.width / 2);
				this.position.Y = (float)(Main.spawnTileY * 16 - this.height);
				for (int i = Main.spawnTileX - 1; i < Main.spawnTileX + 2; i++)
				{
					for (int j = Main.spawnTileY - 3; j < Main.spawnTileY; j++)
					{
						if (Main.tileSolid[(int)Main.tile[i, j].type] && !Main.tileSolidTop[(int)Main.tile[i, j].type])
						{
							WorldGen.KillTile(i, j, false, false, false);
						}
						if (Main.tile[i, j].liquid > 0)
						{
							Main.tile[i, j].lava = false;
							Main.tile[i, j].liquid = 0;
							WorldGen.SquareTileFrame(i, j, true);
						}
					}
				}
			}
			this.wet = false;
			this.wetCount = 0;
			this.lavaWet = false;
			this.fallStart = (int)(this.position.Y / 16f);
			this.velocity.X = 0f;
			this.velocity.Y = 0f;
			this.talkNPC = -1;
			if (this.pvpDeath)
			{
				this.pvpDeath = false;
				this.immuneTime = 300;
				this.statLife = this.statLifeMax;
			}
			else
			{
				this.immuneTime = 60;
			}
			if (this.whoAmi == Main.myPlayer)
			{
				if (Main.netMode == 1)
				{
					Netplay.newRecent();
				}
				Lighting.lightCounter = Lighting.lightSkip + 1;
				Main.screenPosition.X = this.position.X + (float)(this.width / 2) - (float)(Main.screenWidth / 2);
				Main.screenPosition.Y = this.position.Y + (float)(this.height / 2) - (float)(Main.screenHeight / 2);
			}
		}
		public static string getDeathMessage(int plr = -1, int npc = -1, int proj = -1, int other = -1)
		{
			string result = "";
			int num = Main.rand.Next(11);
			string text = "";
			if (num == 0)
			{
				text = " was slain";
			}
			else
			{
				if (num == 1)
				{
					text = " was eviscerated";
				}
				else
				{
					if (num == 2)
					{
						text = " was murdered";
					}
					else
					{
						if (num == 3)
						{
							text = "'s face was torn off";
						}
						else
						{
							if (num == 4)
							{
								text = "'s entrails were ripped out";
							}
							else
							{
								if (num == 5)
								{
									text = " was destroyed";
								}
								else
								{
									if (num == 6)
									{
										text = "'s skull was crushed";
									}
									else
									{
										if (num == 7)
										{
											text = " got massacred";
										}
										else
										{
											if (num == 8)
											{
												text = " got impaled";
											}
											else
											{
												if (num == 9)
												{
													text = " was torn in half";
												}
												else
												{
													if (num == 10)
													{
														text = " was decapitated";
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			if (plr >= 0 && plr < 255)
			{
				if (proj >= 0 && Main.projectile[proj].name != "")
				{
					result = string.Concat(new string[]
					{
						text, 
						" by ", 
						Main.player[plr].name, 
						"'s ", 
						Main.projectile[proj].name, 
						"."
					});
				}
				else
				{
					result = string.Concat(new string[]
					{
						text, 
						" by ", 
						Main.player[plr].name, 
						"'s ", 
						Main.player[plr].inventory[Main.player[plr].selectedItem].name, 
						"."
					});
				}
			}
			else
			{
				if (npc >= 0 && Main.npc[npc].name != "")
				{
					result = text + " by " + Main.npc[npc].name + ".";
				}
				else
				{
					if (proj >= 0 && Main.projectile[proj].name != "")
					{
						result = text + " by " + Main.projectile[proj].name + ".";
					}
					else
					{
						if (other >= 0)
						{
							if (other == 0)
							{
								int num2 = Main.rand.Next(5);
								if (num2 == 0)
								{
									result = " fell to their death.";
								}
								else
								{
									if (num2 == 1)
									{
										result = " faceplanted the ground.";
									}
									else
									{
										if (num2 == 2)
										{
											result = " fell victim to gravity.";
										}
										else
										{
											if (num2 == 3)
											{
												result = " left a small crater.";
											}
											else
											{
												if (num2 == 4)
												{
													result = " didn't bounce.";
												}
											}
										}
									}
								}
							}
							else
							{
								if (other == 1)
								{
									int num3 = Main.rand.Next(4);
									if (num3 == 0)
									{
										result = " forgot to breathe.";
									}
									else
									{
										if (num3 == 1)
										{
											result = " is sleeping with the fish.";
										}
										else
										{
											if (num3 == 2)
											{
												result = " drowned.";
											}
											else
											{
												if (num3 == 3)
												{
													result = " is shark food.";
												}
											}
										}
									}
								}
								else
								{
									if (other == 2)
									{
										int num4 = Main.rand.Next(4);
										if (num4 == 0)
										{
											result = " got melted.";
										}
										else
										{
											if (num4 == 1)
											{
												result = " was incinerated.";
											}
											else
											{
												if (num4 == 2)
												{
													result = " tried to swim in lava.";
												}
												else
												{
													if (num4 == 3)
													{
														result = " likes to play in magma.";
													}
												}
											}
										}
									}
									else
									{
										if (other == 3)
										{
											result = text + ".";
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
		public double Hurt(int Damage, int hitDirection, bool pvp = false, bool quiet = false, string deathText = " was slain...", bool Crit = false)
		{
			if (!this.immune)
			{
				int num = Damage;
				if (pvp)
				{
					num *= 2;
				}
				double num2 = Main.CalculateDamage(num, this.statDefense);
				if (Crit)
				{
					num *= 2;
				}
				if (num2 >= 1.0)
				{
					if (Main.netMode == 1 && this.whoAmi == Main.myPlayer && !quiet)
					{
						int num3 = 0;
						if (pvp)
						{
							num3 = 1;
						}
						NetMessage.SendData(13, -1, -1, "", this.whoAmi, 0f, 0f, 0f, 0);
						NetMessage.SendData(16, -1, -1, "", this.whoAmi, 0f, 0f, 0f, 0);
						NetMessage.SendData(26, -1, -1, "", this.whoAmi, (float)hitDirection, (float)Damage, (float)num3, 0);
					}
					CombatText.NewText(new Rectangle((int)this.position.X, (int)this.position.Y, this.width, this.height), new Color(255, 80, 90, 255), string.Concat((int)num2), Crit);
					this.statLife -= (int)num2;
					this.immune = true;
					this.immuneTime = 40;
					this.lifeRegenTime = 0;
					if (pvp)
					{
						this.immuneTime = 8;
					}
					if (!this.noKnockback && hitDirection != 0)
					{
						this.velocity.X = 4.5f * (float)hitDirection;
						this.velocity.Y = -3.5f;
					}
					if (this.boneArmor)
					{
						Main.PlaySound(3, (int)this.position.X, (int)this.position.Y, 2);
					}
					else
					{
						if (!this.male)
						{
							Main.PlaySound(20, (int)this.position.X, (int)this.position.Y, 1);
						}
						else
						{
							Main.PlaySound(1, (int)this.position.X, (int)this.position.Y, 1);
						}
					}
					if (this.statLife > 0)
					{
						int num4 = 0;
						while ((double)num4 < num2 / (double)this.statLifeMax * 100.0)
						{
							if (this.boneArmor)
							{
								Dust.NewDust(this.position, this.width, this.height, 26, (float)(2 * hitDirection), -2f, 0, default(Color), 1f);
							}
							else
							{
								Dust.NewDust(this.position, this.width, this.height, 5, (float)(2 * hitDirection), -2f, 0, default(Color), 1f);
							}
							num4++;
						}
					}
					else
					{
						this.statLife = 0;
						if (this.whoAmi == Main.myPlayer)
						{
							this.KillMe(num2, hitDirection, pvp, deathText);
						}
					}
				}
				if (pvp)
				{
					num2 = Main.CalculateDamage(num, this.statDefense);
				}
				return num2;
			}
			return 0.0;
		}
		public void KillMeForGood()
		{
			if (File.Exists(Main.playerPathName))
			{
				File.Delete(Main.playerPathName);
			}
			if (File.Exists(Main.playerPathName + ".bak"))
			{
				File.Delete(Main.playerPathName + ".bak");
			}
			if (File.Exists(Main.playerPathName + ".dat"))
			{
				File.Delete(Main.playerPathName + ".dat");
			}
			Main.playerPathName = "";
		}
		public void KillMe(double dmg, int hitDirection, bool pvp = false, string deathText = " was slain...")
		{
			if (this.dead)
			{
				return;
			}
			if (pvp)
			{
				this.pvpDeath = true;
			}
			if (this.difficulty > 0)
			{
				if (Main.netMode != 1)
				{
					float num = (float)Main.rand.Next(-35, 36) * 0.1f;
					while (num < 2f && num > -2f)
					{
						num += (float)Main.rand.Next(-30, 31) * 0.1f;
					}
					int num2 = Projectile.NewProjectile(this.position.X + (float)(this.width / 2), this.position.Y + (float)(this.head / 2), (float)Main.rand.Next(10, 30) * 0.1f * (float)hitDirection + num, (float)Main.rand.Next(-40, -20) * 0.1f, 43, 50, 0f, Main.myPlayer);
					Main.projectile[num2].miscText = this.name + deathText;
				}
				if (Main.myPlayer == this.whoAmi)
				{
					if (this.difficulty == 1)
					{
						this.DropItems();
					}
					else
					{
						if (this.difficulty == 2)
						{
							this.DropItems();
							this.KillMeForGood();
						}
					}
				}
			}
			Main.PlaySound(5, (int)this.position.X, (int)this.position.Y, 1);
			this.headVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
			this.bodyVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
			this.legVelocity.Y = (float)Main.rand.Next(-40, -10) * 0.1f;
			this.headVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + (float)(2 * hitDirection);
			this.bodyVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + (float)(2 * hitDirection);
			this.legVelocity.X = (float)Main.rand.Next(-20, 21) * 0.1f + (float)(2 * hitDirection);
			int num3 = 0;
			while ((double)num3 < 20.0 + dmg / (double)this.statLifeMax * 100.0)
			{
				if (this.boneArmor)
				{
					Dust.NewDust(this.position, this.width, this.height, 26, (float)(2 * hitDirection), -2f, 0, default(Color), 1f);
				}
				else
				{
					Dust.NewDust(this.position, this.width, this.height, 5, (float)(2 * hitDirection), -2f, 0, default(Color), 1f);
				}
				num3++;
			}
			this.dead = true;
			this.respawnTimer = 600;
			this.immuneAlpha = 0;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(25, -1, -1, this.name + deathText, 255, 225f, 25f, 25f, 0);
			}
			else
			{
				if (Main.netMode == 0)
				{
					Main.NewText(this.name + deathText, 225, 25, 25);
				}
			}
			if (Main.netMode == 1 && this.whoAmi == Main.myPlayer)
			{
				int num4 = 0;
				if (pvp)
				{
					num4 = 1;
				}
				NetMessage.SendData(44, -1, -1, deathText, this.whoAmi, (float)hitDirection, (float)((int)dmg), (float)num4, 0);
			}
			if (!pvp && this.whoAmi == Main.myPlayer && this.difficulty == 0)
			{
				this.DropCoins();
			}
			if (this.whoAmi == Main.myPlayer)
			{
				try
				{
					WorldGen.saveToonWhilePlaying();
				}
				catch
				{
				}
			}
		}
		public bool ItemSpace(Item newItem)
		{
			if (newItem.type == 58)
			{
				return true;
			}
			if (newItem.type == 184)
			{
				return true;
			}
			int num = 40;
			if (newItem.type == 71 || newItem.type == 72 || newItem.type == 73 || newItem.type == 74)
			{
				num = 44;
			}
			for (int i = 0; i < num; i++)
			{
				if (this.inventory[i].type == 0)
				{
					return true;
				}
			}
			for (int j = 0; j < num; j++)
			{
				if (this.inventory[j].type > 0 && this.inventory[j].stack < this.inventory[j].maxStack && newItem.IsTheSameAs(this.inventory[j]))
				{
					return true;
				}
			}
			return false;
		}
		public void DoCoins(int i)
		{
			if (this.inventory[i].stack == 100 && (this.inventory[i].type == 71 || this.inventory[i].type == 72 || this.inventory[i].type == 73))
			{
				this.inventory[i].SetDefaults(this.inventory[i].type + 1, false);
				for (int j = 0; j < 44; j++)
				{
					if (this.inventory[j].IsTheSameAs(this.inventory[i]) && j != i && this.inventory[j].stack < this.inventory[j].maxStack)
					{
						this.inventory[j].stack++;
						this.inventory[i].SetDefaults("");
						this.inventory[i].active = false;
						this.inventory[i].name = "";
						this.inventory[i].type = 0;
						this.inventory[i].stack = 0;
						this.DoCoins(j);
					}
				}
			}
		}
		public Item FillAmmo(int plr, Item newItem)
		{
			for (int i = 0; i < 4; i++)
			{
				if (this.ammo[i].type > 0 && this.ammo[i].stack < this.ammo[i].maxStack && newItem.IsTheSameAs(this.ammo[i]))
				{
					Main.PlaySound(7, (int)this.position.X, (int)this.position.Y, 1);
					if (newItem.stack + this.ammo[i].stack <= this.ammo[i].maxStack)
					{
						this.ammo[i].stack += newItem.stack;
						ItemText.NewText(newItem, newItem.stack);
						this.DoCoins(i);
						if (plr == Main.myPlayer)
						{
							Recipe.FindRecipes();
						}
						return new Item();
					}
					newItem.stack -= this.ammo[i].maxStack - this.ammo[i].stack;
					ItemText.NewText(newItem, this.ammo[i].maxStack - this.ammo[i].stack);
					this.ammo[i].stack = this.ammo[i].maxStack;
					this.DoCoins(i);
					if (plr == Main.myPlayer)
					{
						Recipe.FindRecipes();
					}
				}
			}
			if (newItem.type != 169 && newItem.type != 75)
			{
				for (int j = 0; j < 4; j++)
				{
					if (this.ammo[j].type == 0)
					{
						this.ammo[j] = newItem;
						ItemText.NewText(newItem, newItem.stack);
						this.DoCoins(j);
						Main.PlaySound(7, (int)this.position.X, (int)this.position.Y, 1);
						if (plr == Main.myPlayer)
						{
							Recipe.FindRecipes();
						}
						return new Item();
					}
				}
			}
			return newItem;
		}
		public Item GetItem(int plr, Item newItem)
		{
			Item item = newItem;
			int num = 40;
			if (newItem.noGrabDelay > 0)
			{
				return item;
			}
			int num2 = 0;
			if (newItem.type == 71 || newItem.type == 72 || newItem.type == 73 || newItem.type == 74)
			{
				num2 = -4;
				num = 44;
			}
			if (item.ammo > 0)
			{
				item = this.FillAmmo(plr, item);
				if (item.type == 0 || item.stack == 0)
				{
					return new Item();
				}
			}
			for (int i = num2; i < 40; i++)
			{
				int num3 = i;
				if (num3 < 0)
				{
					num3 = 44 + i;
				}
				if (this.inventory[num3].type > 0 && this.inventory[num3].stack < this.inventory[num3].maxStack && item.IsTheSameAs(this.inventory[num3]))
				{
					Main.PlaySound(7, (int)this.position.X, (int)this.position.Y, 1);
					if (item.stack + this.inventory[num3].stack <= this.inventory[num3].maxStack)
					{
						this.inventory[num3].stack += item.stack;
						ItemText.NewText(newItem, item.stack);
						this.DoCoins(num3);
						if (plr == Main.myPlayer)
						{
							Recipe.FindRecipes();
						}
						return new Item();
					}
					item.stack -= this.inventory[num3].maxStack - this.inventory[num3].stack;
					ItemText.NewText(newItem, this.inventory[num3].maxStack - this.inventory[num3].stack);
					this.inventory[num3].stack = this.inventory[num3].maxStack;
					this.DoCoins(num3);
					if (plr == Main.myPlayer)
					{
						Recipe.FindRecipes();
					}
				}
			}
			for (int j = num - 1; j >= 0; j--)
			{
				if (this.inventory[j].type == 0)
				{
					this.inventory[j] = item;
					ItemText.NewText(newItem, newItem.stack);
					this.DoCoins(j);
					Main.PlaySound(7, (int)this.position.X, (int)this.position.Y, 1);
					if (plr == Main.myPlayer)
					{
						Recipe.FindRecipes();
					}
					return new Item();
				}
			}
			return item;
		}
		public void ItemCheck(int i)
		{
			int num = this.inventory[this.selectedItem].damage;
			if (num > 0)
			{
				if (this.inventory[this.selectedItem].melee)
				{
					num = (int)((float)num * this.meleeDamage);
				}
				else
				{
					if (this.inventory[this.selectedItem].ranged)
					{
						num = (int)((float)num * this.rangedDamage);
					}
					else
					{
						if (this.inventory[this.selectedItem].magic)
						{
							num = (int)((float)num * this.magicDamage);
						}
					}
				}
			}
			if (this.inventory[this.selectedItem].autoReuse && !this.noItems)
			{
				this.releaseUseItem = true;
				if (this.itemAnimation == 1 && this.inventory[this.selectedItem].stack > 0)
				{
					this.itemAnimation = 0;
				}
			}
			if (this.controlUseItem && this.itemAnimation == 0 && this.releaseUseItem && this.inventory[this.selectedItem].useStyle > 0)
			{
				bool flag = true;
				if (this.noItems)
				{
					flag = false;
				}
				if (this.inventory[this.selectedItem].shoot == 6 || this.inventory[this.selectedItem].shoot == 19 || this.inventory[this.selectedItem].shoot == 33 || this.inventory[this.selectedItem].shoot == 52)
				{
					for (int j = 0; j < 1000; j++)
					{
						if (Main.projectile[j].active && Main.projectile[j].owner == Main.myPlayer && Main.projectile[j].type == this.inventory[this.selectedItem].shoot)
						{
							flag = false;
						}
					}
				}
				if (this.inventory[this.selectedItem].shoot == 13 || this.inventory[this.selectedItem].shoot == 32)
				{
					for (int k = 0; k < 1000; k++)
					{
						if (Main.projectile[k].active && Main.projectile[k].owner == Main.myPlayer && Main.projectile[k].type == this.inventory[this.selectedItem].shoot && Main.projectile[k].ai[0] != 2f)
						{
							flag = false;
						}
					}
				}
				if (this.inventory[this.selectedItem].potion && flag)
				{
					if (this.potionDelay <= 0)
					{
						this.potionDelay = Item.potionDelay;
						this.AddBuff(21, this.potionDelay, true);
					}
					else
					{
						flag = false;
					}
				}
				if (this.inventory[this.selectedItem].mana > 0 && flag)
				{
					if (this.inventory[this.selectedItem].type != 127 || !this.spaceGun)
					{
						if (this.statMana >= (int)((float)this.inventory[this.selectedItem].mana * this.manaCost))
						{
							this.statMana -= (int)((float)this.inventory[this.selectedItem].mana * this.manaCost);
						}
						else
						{
							flag = false;
						}
					}
					if (this.whoAmi == Main.myPlayer && this.inventory[this.selectedItem].buffType != 0)
					{
						this.AddBuff(this.inventory[this.selectedItem].buffType, this.inventory[this.selectedItem].buffTime, true);
					}
				}
				if (this.inventory[this.selectedItem].type == 43 && Main.dayTime)
				{
					flag = false;
				}
				if (this.inventory[this.selectedItem].type == 70 && !this.zoneEvil)
				{
					flag = false;
				}
				if (this.inventory[this.selectedItem].shoot == 17 && flag && i == Main.myPlayer)
				{
					int num2 = (int)((float)Main.mouseState.X + Main.screenPosition.X) / 16;
					int num3 = (int)((float)Main.mouseState.Y + Main.screenPosition.Y) / 16;
					if (Main.tile[num2, num3].active && (Main.tile[num2, num3].type == 0 || Main.tile[num2, num3].type == 2 || Main.tile[num2, num3].type == 23))
					{
						WorldGen.KillTile(num2, num3, false, false, true);
						if (!Main.tile[num2, num3].active)
						{
							if (Main.netMode == 1)
							{
								NetMessage.SendData(17, -1, -1, "", 4, (float)num2, (float)num3, 0f, 0);
							}
						}
						else
						{
							flag = false;
						}
					}
					else
					{
						flag = false;
					}
				}
				if (flag && this.inventory[this.selectedItem].useAmmo > 0)
				{
					flag = false;
					for (int l = 0; l < 44; l++)
					{
						if (l < 4 && this.ammo[l].ammo == this.inventory[this.selectedItem].useAmmo && this.ammo[l].stack > 0)
						{
							flag = true;
							break;
						}
						if (this.inventory[l].ammo == this.inventory[this.selectedItem].useAmmo && this.inventory[l].stack > 0)
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					if (this.grappling[0] > -1)
					{
						if (this.controlRight)
						{
							this.direction = 1;
						}
						else
						{
							if (this.controlLeft)
							{
								this.direction = -1;
							}
						}
					}
					this.channel = this.inventory[this.selectedItem].channel;
					this.attackCD = 0;
					if (this.inventory[this.selectedItem].melee)
					{
						this.itemAnimation = (int)((float)this.inventory[this.selectedItem].useAnimation * this.meleeSpeed);
						this.itemAnimationMax = (int)((float)this.inventory[this.selectedItem].useAnimation * this.meleeSpeed);
					}
					else
					{
						this.itemAnimation = this.inventory[this.selectedItem].useAnimation;
						this.itemAnimationMax = this.inventory[this.selectedItem].useAnimation;
					}
					if (this.inventory[this.selectedItem].useSound > 0)
					{
						Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, this.inventory[this.selectedItem].useSound);
					}
				}
				if (flag && this.inventory[this.selectedItem].shoot == 18)
				{
					for (int m = 0; m < 1000; m++)
					{
						if (Main.projectile[m].active && Main.projectile[m].owner == i && Main.projectile[m].type == this.inventory[this.selectedItem].shoot)
						{
							Main.projectile[m].Kill();
						}
					}
				}
			}
			if (!this.controlUseItem)
			{
				bool arg_6E8_0 = this.channel;
				this.channel = false;
			}
			if (this.itemAnimation > 0)
			{
				if (this.inventory[this.selectedItem].mana > 0)
				{
					this.manaRegenDelay = (int)this.maxRegenDelay;
				}
				if (Main.dedServ)
				{
					this.itemHeight = this.inventory[this.selectedItem].height;
					this.itemWidth = this.inventory[this.selectedItem].width;
				}
				else
				{
					this.itemHeight = Main.itemTexture[this.inventory[this.selectedItem].type].Height;
					this.itemWidth = Main.itemTexture[this.inventory[this.selectedItem].type].Width;
				}
				this.itemAnimation--;
				if (!Main.dedServ)
				{
					if (this.inventory[this.selectedItem].useStyle == 1)
					{
						if ((double)this.itemAnimation < (double)this.itemAnimationMax * 0.333)
						{
							float num4 = 10f;
							if (Main.itemTexture[this.inventory[this.selectedItem].type].Width > 32)
							{
								num4 = 14f;
							}
							this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - num4) * (float)this.direction;
							this.itemLocation.Y = this.position.Y + 24f;
						}
						else
						{
							if ((double)this.itemAnimation < (double)this.itemAnimationMax * 0.666)
							{
								float num5 = 10f;
								if (Main.itemTexture[this.inventory[this.selectedItem].type].Width > 32)
								{
									num5 = 18f;
								}
								this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - num5) * (float)this.direction;
								num5 = 10f;
								if (Main.itemTexture[this.inventory[this.selectedItem].type].Height > 32)
								{
									num5 = 8f;
								}
								this.itemLocation.Y = this.position.Y + num5;
							}
							else
							{
								float num6 = 6f;
								if (Main.itemTexture[this.inventory[this.selectedItem].type].Width > 32)
								{
									num6 = 14f;
								}
								this.itemLocation.X = this.position.X + (float)this.width * 0.5f - ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - num6) * (float)this.direction;
								num6 = 10f;
								if (Main.itemTexture[this.inventory[this.selectedItem].type].Height > 32)
								{
									num6 = 10f;
								}
								this.itemLocation.Y = this.position.Y + num6;
							}
						}
						this.itemRotation = ((float)this.itemAnimation / (float)this.itemAnimationMax - 0.5f) * (float)(-(float)this.direction) * 3.5f - (float)this.direction * 0.3f;
						if (this.gravDir == -1f)
						{
							this.itemRotation = -this.itemRotation;
							this.itemLocation.Y = this.position.Y + (float)this.height + (this.position.Y - this.itemLocation.Y);
						}
					}
					else
					{
						if (this.inventory[this.selectedItem].useStyle == 2)
						{
							this.itemRotation = (float)this.itemAnimation / (float)this.itemAnimationMax * (float)this.direction * 2f + -1.4f * (float)this.direction;
							if ((double)this.itemAnimation < (double)this.itemAnimationMax * 0.5)
							{
								this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - 9f - this.itemRotation * 12f * (float)this.direction) * (float)this.direction;
								this.itemLocation.Y = this.position.Y + 38f + this.itemRotation * (float)this.direction * 4f;
							}
							else
							{
								this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - 9f - this.itemRotation * 16f * (float)this.direction) * (float)this.direction;
								this.itemLocation.Y = this.position.Y + 38f + this.itemRotation * (float)this.direction;
							}
							if (this.gravDir == -1f)
							{
								this.itemRotation = -this.itemRotation;
								this.itemLocation.Y = this.position.Y + (float)this.height + (this.position.Y - this.itemLocation.Y);
							}
						}
						else
						{
							if (this.inventory[this.selectedItem].useStyle == 3)
							{
								if ((double)this.itemAnimation > (double)this.itemAnimationMax * 0.666)
								{
									this.itemLocation.X = -1000f;
									this.itemLocation.Y = -1000f;
									this.itemRotation = -1.3f * (float)this.direction;
								}
								else
								{
									this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - 4f) * (float)this.direction;
									this.itemLocation.Y = this.position.Y + 24f;
									float num7 = (float)this.itemAnimation / (float)this.itemAnimationMax * (float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * (float)this.direction * this.inventory[this.selectedItem].scale * 1.2f - (float)(10 * this.direction);
									if (num7 > -4f && this.direction == -1)
									{
										num7 = -8f;
									}
									if (num7 < 4f && this.direction == 1)
									{
										num7 = 8f;
									}
									this.itemLocation.X = this.itemLocation.X - num7;
									this.itemRotation = 0.8f * (float)this.direction;
								}
								if (this.gravDir == -1f)
								{
									this.itemRotation = -this.itemRotation;
									this.itemLocation.Y = this.position.Y + (float)this.height + (this.position.Y - this.itemLocation.Y);
								}
							}
							else
							{
								if (this.inventory[this.selectedItem].useStyle == 4)
								{
									this.itemRotation = 0f;
									this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - 9f - this.itemRotation * 14f * (float)this.direction) * (float)this.direction;
									this.itemLocation.Y = this.position.Y + (float)Main.itemTexture[this.inventory[this.selectedItem].type].Height * 0.5f;
									if (this.gravDir == -1f)
									{
										this.itemRotation = -this.itemRotation;
										this.itemLocation.Y = this.position.Y + (float)this.height + (this.position.Y - this.itemLocation.Y);
									}
								}
								else
								{
									if (this.inventory[this.selectedItem].useStyle == 5)
									{
										this.itemLocation.X = this.position.X + (float)this.width * 0.5f - (float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f - (float)(this.direction * 2);
										this.itemLocation.Y = this.position.Y + (float)this.height * 0.5f - (float)Main.itemTexture[this.inventory[this.selectedItem].type].Height * 0.5f;
									}
								}
							}
						}
					}
				}
			}
			else
			{
				if (this.inventory[this.selectedItem].holdStyle == 1)
				{
					if (Main.dedServ)
					{
						this.itemLocation.X = this.position.X + (float)this.width * 0.5f + 20f * (float)this.direction;
					}
					else
					{
						this.itemLocation.X = this.position.X + (float)this.width * 0.5f + ((float)Main.itemTexture[this.inventory[this.selectedItem].type].Width * 0.5f + 4f) * (float)this.direction;
						if (this.inventory[this.selectedItem].type == 282 || this.inventory[this.selectedItem].type == 286)
						{
							this.itemLocation.X = this.itemLocation.X - (float)(this.direction * 4);
							this.itemLocation.Y = this.itemLocation.Y + 4f;
						}
					}
					this.itemLocation.Y = this.position.Y + 24f;
					this.itemRotation = 0f;
					if (this.gravDir == -1f)
					{
						this.itemRotation = -this.itemRotation;
						this.itemLocation.Y = this.position.Y + (float)this.height + (this.position.Y - this.itemLocation.Y);
					}
				}
				else
				{
					if (this.inventory[this.selectedItem].holdStyle == 2)
					{
						this.itemLocation.X = this.position.X + (float)this.width * 0.5f + (float)(6 * this.direction);
						this.itemLocation.Y = this.position.Y + 16f;
						this.itemRotation = 0.79f * (float)(-(float)this.direction);
						if (this.gravDir == -1f)
						{
							this.itemRotation = -this.itemRotation;
							this.itemLocation.Y = this.position.Y + (float)this.height + (this.position.Y - this.itemLocation.Y);
						}
					}
				}
			}
			if (this.inventory[this.selectedItem].type == 8 && !this.wet)
			{
				int maxValue = 20;
				if (this.itemAnimation > 0)
				{
					maxValue = 7;
				}
				if (this.direction == -1)
				{
					if (Main.rand.Next(maxValue) == 0)
					{
						Vector2 arg_1381_0 = new Vector2(this.itemLocation.X - 16f, this.itemLocation.Y - 14f * this.gravDir);
						int arg_1381_1 = 4;
						int arg_1381_2 = 4;
						int arg_1381_3 = 6;
						float arg_1381_4 = 0f;
						float arg_1381_5 = 0f;
						int arg_1381_6 = 100;
						Color newColor = default(Color);
						Dust.NewDust(arg_1381_0, arg_1381_1, arg_1381_2, arg_1381_3, arg_1381_4, arg_1381_5, arg_1381_6, newColor, 1f);
					}
					Lighting.addLight((int)((this.itemLocation.X - 16f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 1f);
				}
				else
				{
					if (Main.rand.Next(maxValue) == 0)
					{
						Vector2 arg_142C_0 = new Vector2(this.itemLocation.X + 6f, this.itemLocation.Y - 14f * this.gravDir);
						int arg_142C_1 = 4;
						int arg_142C_2 = 4;
						int arg_142C_3 = 6;
						float arg_142C_4 = 0f;
						float arg_142C_5 = 0f;
						int arg_142C_6 = 100;
						Color newColor = default(Color);
						Dust.NewDust(arg_142C_0, arg_142C_1, arg_142C_2, arg_142C_3, arg_142C_4, arg_142C_5, arg_142C_6, newColor, 1f);
					}
					Lighting.addLight((int)((this.itemLocation.X + 6f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 1f);
				}
			}
			else
			{
				if (this.inventory[this.selectedItem].type == 105 && !this.wet)
				{
					int maxValue2 = 20;
					if (this.itemAnimation > 0)
					{
						maxValue2 = 7;
					}
					if (this.direction == -1)
					{
						if (Main.rand.Next(maxValue2) == 0)
						{
							Vector2 arg_1517_0 = new Vector2(this.itemLocation.X - 12f, this.itemLocation.Y - 20f * this.gravDir);
							int arg_1517_1 = 4;
							int arg_1517_2 = 4;
							int arg_1517_3 = 6;
							float arg_1517_4 = 0f;
							float arg_1517_5 = 0f;
							int arg_1517_6 = 100;
							Color newColor = default(Color);
							Dust.NewDust(arg_1517_0, arg_1517_1, arg_1517_2, arg_1517_3, arg_1517_4, arg_1517_5, arg_1517_6, newColor, 1f);
						}
						Lighting.addLight((int)((this.itemLocation.X - 16f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 1f);
					}
					else
					{
						if (Main.rand.Next(maxValue2) == 0)
						{
							Vector2 arg_15C2_0 = new Vector2(this.itemLocation.X + 4f, this.itemLocation.Y - 20f * this.gravDir);
							int arg_15C2_1 = 4;
							int arg_15C2_2 = 4;
							int arg_15C2_3 = 6;
							float arg_15C2_4 = 0f;
							float arg_15C2_5 = 0f;
							int arg_15C2_6 = 100;
							Color newColor = default(Color);
							Dust.NewDust(arg_15C2_0, arg_15C2_1, arg_15C2_2, arg_15C2_3, arg_15C2_4, arg_15C2_5, arg_15C2_6, newColor, 1f);
						}
						Lighting.addLight((int)((this.itemLocation.X + 6f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 1f);
					}
				}
				else
				{
					if (this.inventory[this.selectedItem].type == 148 && !this.wet)
					{
						int maxValue3 = 10;
						if (this.itemAnimation > 0)
						{
							maxValue3 = 7;
						}
						if (this.direction == -1)
						{
							if (Main.rand.Next(maxValue3) == 0)
							{
								Vector2 arg_16B1_0 = new Vector2(this.itemLocation.X - 12f, this.itemLocation.Y - 20f * this.gravDir);
								int arg_16B1_1 = 4;
								int arg_16B1_2 = 4;
								int arg_16B1_3 = 29;
								float arg_16B1_4 = 0f;
								float arg_16B1_5 = 0f;
								int arg_16B1_6 = 100;
								Color newColor = default(Color);
								Dust.NewDust(arg_16B1_0, arg_16B1_1, arg_16B1_2, arg_16B1_3, arg_16B1_4, arg_16B1_5, arg_16B1_6, newColor, 1f);
							}
							Lighting.addLight((int)((this.itemLocation.X - 16f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 1f);
						}
						else
						{
							if (Main.rand.Next(maxValue3) == 0)
							{
								Vector2 arg_175D_0 = new Vector2(this.itemLocation.X + 4f, this.itemLocation.Y - 20f * this.gravDir);
								int arg_175D_1 = 4;
								int arg_175D_2 = 4;
								int arg_175D_3 = 29;
								float arg_175D_4 = 0f;
								float arg_175D_5 = 0f;
								int arg_175D_6 = 100;
								Color newColor = default(Color);
								Dust.NewDust(arg_175D_0, arg_175D_1, arg_175D_2, arg_175D_3, arg_175D_4, arg_175D_5, arg_175D_6, newColor, 1f);
							}
							Lighting.addLight((int)((this.itemLocation.X + 6f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 1f);
						}
					}
				}
			}
			if (this.inventory[this.selectedItem].type == 282 || this.inventory[this.selectedItem].type == 286)
			{
				if (this.direction == -1)
				{
					Lighting.addLight((int)((this.itemLocation.X - 16f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 1f);
				}
				else
				{
					Lighting.addLight((int)((this.itemLocation.X + 6f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 1f);
				}
			}
			if (this.controlUseItem)
			{
				this.releaseUseItem = false;
			}
			else
			{
				this.releaseUseItem = true;
			}
			if (this.itemTime > 0)
			{
				this.itemTime--;
			}
			if (i == Main.myPlayer)
			{
				if (this.inventory[this.selectedItem].shoot > 0 && this.itemAnimation > 0 && this.itemTime == 0)
				{
					int num8 = this.inventory[this.selectedItem].shoot;
					float num9 = this.inventory[this.selectedItem].shootSpeed;
					if (this.inventory[this.selectedItem].melee && num8 != 25 && num8 != 26 && num8 != 35)
					{
						num9 /= this.meleeSpeed;
					}
					bool flag2 = false;
					int num10 = num;
					float num11 = this.inventory[this.selectedItem].knockBack;
					if (num8 == 13 || num8 == 32)
					{
						this.grappling[0] = -1;
						this.grapCount = 0;
						for (int n = 0; n < 1000; n++)
						{
							if (Main.projectile[n].active && Main.projectile[n].owner == i && Main.projectile[n].type == 13)
							{
								Main.projectile[n].Kill();
							}
						}
					}
					if (this.inventory[this.selectedItem].useAmmo > 0)
					{
						Item item = new Item();
						bool flag3 = false;
						for (int num12 = 0; num12 < 4; num12++)
						{
							if (this.ammo[num12].ammo == this.inventory[this.selectedItem].useAmmo && this.ammo[num12].stack > 0)
							{
								item = this.ammo[num12];
								flag2 = true;
								flag3 = true;
								break;
							}
						}
						if (!flag3)
						{
							for (int num13 = 0; num13 < 44; num13++)
							{
								if (this.inventory[num13].ammo == this.inventory[this.selectedItem].useAmmo && this.inventory[num13].stack > 0)
								{
									item = this.inventory[num13];
									flag2 = true;
									break;
								}
							}
						}
						if (flag2)
						{
							if (item.shoot > 0)
							{
								num8 = item.shoot;
							}
							num9 += item.shootSpeed;
							if (item.ranged)
							{
								num10 += (int)((float)item.damage * this.rangedDamage);
							}
							else
							{
								num10 += item.damage;
							}
							if (this.inventory[this.selectedItem].useAmmo == 1 && this.archery)
							{
								if (num9 < 20f)
								{
									num9 *= 1.2f;
									if (num9 > 20f)
									{
										num9 = 20f;
									}
								}
								num10 = (int)((double)((float)num10) * 1.2);
							}
							num11 += item.knockBack;
							bool flag4 = false;
							if (this.inventory[this.selectedItem].type == 98 && Main.rand.Next(3) == 0)
							{
								flag4 = true;
							}
							if (this.ammoCost80 && Main.rand.Next(5) == 0)
							{
								flag4 = true;
							}
							if (!flag4)
							{
								item.stack--;
								if (item.stack <= 0)
								{
									item.active = false;
									item.name = "";
									item.type = 0;
								}
							}
						}
					}
					else
					{
						flag2 = true;
					}
					if (flag2)
					{
						if (num8 == 1 && this.inventory[this.selectedItem].type == 120)
						{
							num8 = 2;
						}
						this.itemTime = this.inventory[this.selectedItem].useTime;
						if ((float)Main.mouseState.X + Main.screenPosition.X > this.position.X + (float)this.width * 0.5f)
						{
							this.direction = 1;
						}
						else
						{
							this.direction = -1;
						}
						Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
						if (num8 == 9)
						{
							vector = new Vector2(this.position.X + (float)this.width * 0.5f + (float)(Main.rand.Next(601) * -(float)this.direction), this.position.Y + (float)this.height * 0.5f - 300f - (float)Main.rand.Next(100));
							num11 = 0f;
						}
						else
						{
							if (num8 == 51)
							{
								vector.Y -= 6f * this.gravDir;
							}
						}
						float num14 = (float)Main.mouseState.X + Main.screenPosition.X - vector.X;
						float num15 = (float)Main.mouseState.Y + Main.screenPosition.Y - vector.Y;
						float num16 = (float)Math.Sqrt((double)(num14 * num14 + num15 * num15));
						num16 = num9 / num16;
						num14 *= num16;
						num15 *= num16;
						if (num8 == 12)
						{
							vector.X += num14 * 3f;
							vector.Y += num15 * 3f;
						}
						if (this.inventory[this.selectedItem].useStyle == 5)
						{
							this.itemRotation = (float)Math.Atan2((double)(num15 * (float)this.direction), (double)(num14 * (float)this.direction));
							NetMessage.SendData(13, -1, -1, "", this.whoAmi, 0f, 0f, 0f, 0);
							NetMessage.SendData(41, -1, -1, "", this.whoAmi, 0f, 0f, 0f, 0);
						}
						if (num8 == 17)
						{
							vector.X = (float)Main.mouseState.X + Main.screenPosition.X;
							vector.Y = (float)Main.mouseState.Y + Main.screenPosition.Y;
						}
						Projectile.NewProjectile(vector.X, vector.Y, num14, num15, num8, num10, num11, i);
					}
					else
					{
						if (this.inventory[this.selectedItem].useStyle == 5)
						{
							this.itemRotation = 0f;
							NetMessage.SendData(41, -1, -1, "", this.whoAmi, 0f, 0f, 0f, 0);
						}
					}
				}
				if (this.inventory[this.selectedItem].type >= 205 && this.inventory[this.selectedItem].type <= 207 && this.position.X / 16f - (float)Player.tileRangeX - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX + (float)this.inventory[this.selectedItem].tileBoost - 1f >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY + (float)this.inventory[this.selectedItem].tileBoost - 2f >= (float)Player.tileTargetY)
				{
					this.showItemIcon = true;
					if (this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem)
					{
						if (this.inventory[this.selectedItem].type == 205)
						{
							bool lava = Main.tile[Player.tileTargetX, Player.tileTargetY].lava;
							int num17 = 0;
							for (int num18 = Player.tileTargetX - 1; num18 <= Player.tileTargetX + 1; num18++)
							{
								for (int num19 = Player.tileTargetY - 1; num19 <= Player.tileTargetY + 1; num19++)
								{
									if (Main.tile[num18, num19].lava == lava)
									{
										num17 += (int)Main.tile[num18, num19].liquid;
									}
								}
							}
							if (Main.tile[Player.tileTargetX, Player.tileTargetY].liquid > 0 && num17 > 100)
							{
								bool lava2 = Main.tile[Player.tileTargetX, Player.tileTargetY].lava;
								if (!Main.tile[Player.tileTargetX, Player.tileTargetY].lava)
								{
									this.inventory[this.selectedItem].SetDefaults(206, false);
								}
								else
								{
									this.inventory[this.selectedItem].SetDefaults(207, false);
								}
								Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1);
								this.itemTime = this.inventory[this.selectedItem].useTime;
								int num20 = (int)Main.tile[Player.tileTargetX, Player.tileTargetY].liquid;
								Main.tile[Player.tileTargetX, Player.tileTargetY].liquid = 0;
								Main.tile[Player.tileTargetX, Player.tileTargetY].lava = false;
								WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, false);
								if (Main.netMode == 1)
								{
									NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
								}
								else
								{
									Liquid.AddWater(Player.tileTargetX, Player.tileTargetY);
								}
								for (int num21 = Player.tileTargetX - 1; num21 <= Player.tileTargetX + 1; num21++)
								{
									for (int num22 = Player.tileTargetY - 1; num22 <= Player.tileTargetY + 1; num22++)
									{
										if (num20 < 256 && Main.tile[num21, num22].lava == lava)
										{
											int num23 = (int)Main.tile[num21, num22].liquid;
											if (num23 + num20 > 255)
											{
												num23 = 255 - num20;
											}
											num20 += num23;
											Tile expr_2292 = Main.tile[num21, num22];
											expr_2292.liquid -= (byte)num23;
											Main.tile[num21, num22].lava = lava2;
											if (Main.tile[num21, num22].liquid == 0)
											{
												Main.tile[num21, num22].lava = false;
											}
											WorldGen.SquareTileFrame(num21, num22, false);
											if (Main.netMode == 1)
											{
												NetMessage.sendWater(num21, num22);
											}
											else
											{
												Liquid.AddWater(num21, num22);
											}
										}
									}
								}
							}
						}
						else
						{
							if (Main.tile[Player.tileTargetX, Player.tileTargetY].liquid < 200 && (!Main.tile[Player.tileTargetX, Player.tileTargetY].active || !Main.tileSolid[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type] || Main.tileSolidTop[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type]))
							{
								if (this.inventory[this.selectedItem].type == 207)
								{
									if (Main.tile[Player.tileTargetX, Player.tileTargetY].liquid == 0 || Main.tile[Player.tileTargetX, Player.tileTargetY].lava)
									{
										Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1);
										Main.tile[Player.tileTargetX, Player.tileTargetY].lava = true;
										Main.tile[Player.tileTargetX, Player.tileTargetY].liquid = 255;
										WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, true);
										this.inventory[this.selectedItem].SetDefaults(205, false);
										this.itemTime = this.inventory[this.selectedItem].useTime;
										if (Main.netMode == 1)
										{
											NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
										}
									}
								}
								else
								{
									if (Main.tile[Player.tileTargetX, Player.tileTargetY].liquid == 0 || !Main.tile[Player.tileTargetX, Player.tileTargetY].lava)
									{
										Main.PlaySound(19, (int)this.position.X, (int)this.position.Y, 1);
										Main.tile[Player.tileTargetX, Player.tileTargetY].lava = false;
										Main.tile[Player.tileTargetX, Player.tileTargetY].liquid = 255;
										WorldGen.SquareTileFrame(Player.tileTargetX, Player.tileTargetY, true);
										this.inventory[this.selectedItem].SetDefaults(205, false);
										this.itemTime = this.inventory[this.selectedItem].useTime;
										if (Main.netMode == 1)
										{
											NetMessage.sendWater(Player.tileTargetX, Player.tileTargetY);
										}
									}
								}
							}
						}
					}
				}
				if ((this.inventory[this.selectedItem].pick > 0 || this.inventory[this.selectedItem].axe > 0 || this.inventory[this.selectedItem].hammer > 0) && this.position.X / 16f - (float)Player.tileRangeX - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX + (float)this.inventory[this.selectedItem].tileBoost - 1f >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY + (float)this.inventory[this.selectedItem].tileBoost - 2f >= (float)Player.tileTargetY)
				{
					this.showItemIcon = true;
					if (Main.tile[Player.tileTargetX, Player.tileTargetY].active && this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem)
					{
						if (this.hitTileX != Player.tileTargetX || this.hitTileY != Player.tileTargetY)
						{
							this.hitTile = 0;
							this.hitTileX = Player.tileTargetX;
							this.hitTileY = Player.tileTargetY;
						}
						if (Main.tileNoFail[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type])
						{
							this.hitTile = 100;
						}
						if (Main.tile[Player.tileTargetX, Player.tileTargetY].type != 27)
						{
							if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 4 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 10 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 11 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 12 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 13 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 14 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 15 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 16 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 17 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 18 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 19 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 21 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 26 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 28 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 29 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 31 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 33 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 34 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 35 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 36 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 42 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 48 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 49 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 50 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 54 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 55 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 77 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 78 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 79 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 81 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 85 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 86 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 87 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 88 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 89 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 90 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 91 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 92 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 93 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 94 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 95 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 96 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 97 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 98 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 99 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 100 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 101 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 102 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 103 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 104 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 105 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 106)
							{
								if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 48)
								{
									this.hitTile += this.inventory[this.selectedItem].hammer / 2;
								}
								else
								{
									this.hitTile += this.inventory[this.selectedItem].hammer;
								}
								if ((double)Player.tileTargetY > Main.rockLayer && Main.tile[Player.tileTargetX, Player.tileTargetY].type == 77 && this.inventory[this.selectedItem].hammer < 60)
								{
									this.hitTile = 0;
								}
								if (this.inventory[this.selectedItem].hammer > 0)
								{
									if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 26)
									{
										this.Hurt(this.statLife / 2, -this.direction, false, false, Player.getDeathMessage(-1, -1, -1, 4), false);
										WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, true, false, false);
										if (Main.netMode == 1)
										{
											NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 1f, 0);
										}
									}
									else
									{
										if (this.hitTile >= 100)
										{
											if (Main.netMode == 1 && Main.tile[Player.tileTargetX, Player.tileTargetY].type == 21)
											{
												WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, true, false, false);
												NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 1f, 0);
												NetMessage.SendData(34, -1, -1, "", Player.tileTargetX, (float)Player.tileTargetY, 0f, 0f, 0);
											}
											else
											{
												this.hitTile = 0;
												WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, false, false, false);
												if (Main.netMode == 1)
												{
													NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f, 0);
												}
											}
										}
										else
										{
											WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, true, false, false);
											if (Main.netMode == 1)
											{
												NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 1f, 0);
											}
										}
									}
									this.itemTime = this.inventory[this.selectedItem].useTime;
								}
							}
							else
							{
								if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 5 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 30 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 72 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 80)
								{
									if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 30)
									{
										this.hitTile += this.inventory[this.selectedItem].axe * 5;
									}
									else
									{
										if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 80)
										{
											this.hitTile += this.inventory[this.selectedItem].axe * 3;
										}
										else
										{
											this.hitTile += this.inventory[this.selectedItem].axe;
										}
									}
									if (this.inventory[this.selectedItem].axe > 0)
									{
										if (this.hitTile >= 100)
										{
											this.hitTile = 0;
											WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, false, false, false);
											if (Main.netMode == 1)
											{
												NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f, 0);
											}
										}
										else
										{
											WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, true, false, false);
											if (Main.netMode == 1)
											{
												NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 1f, 0);
											}
										}
										this.itemTime = this.inventory[this.selectedItem].useTime;
									}
								}
								else
								{
									if (this.inventory[this.selectedItem].pick > 0)
									{
										if (Main.tileDungeon[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type] || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 37 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 25 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 58)
										{
											this.hitTile += this.inventory[this.selectedItem].pick / 2;
										}
										else
										{
											this.hitTile += this.inventory[this.selectedItem].pick;
										}
										if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 25 && this.inventory[this.selectedItem].pick < 65)
										{
											this.hitTile = 0;
										}
										else
										{
											if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 37 && this.inventory[this.selectedItem].pick < 55)
											{
												this.hitTile = 0;
											}
											else
											{
												if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 22 && (double)Player.tileTargetY > Main.worldSurface && this.inventory[this.selectedItem].pick < 55)
												{
													this.hitTile = 0;
												}
												else
												{
													if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 56 && this.inventory[this.selectedItem].pick < 65)
													{
														this.hitTile = 0;
													}
													else
													{
														if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 58 && this.inventory[this.selectedItem].pick < 65)
														{
															this.hitTile = 0;
														}
														else
														{
															if (Main.tileDungeon[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].type] && this.inventory[this.selectedItem].pick < 65 && ((double)Player.tileTargetX < (double)Main.maxTilesX * 0.25 || (double)Player.tileTargetX > (double)Main.maxTilesX * 0.75))
															{
																this.hitTile = 0;
															}
														}
													}
												}
											}
										}
										if (Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 40 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 53 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 57 || Main.tile[Player.tileTargetX, Player.tileTargetY].type == 59)
										{
											this.hitTile += this.inventory[this.selectedItem].pick;
										}
										if (this.hitTile >= 100)
										{
											this.hitTile = 0;
											WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, false, false, false);
											if (Main.netMode == 1)
											{
												NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f, 0);
											}
										}
										else
										{
											WorldGen.KillTile(Player.tileTargetX, Player.tileTargetY, true, false, false);
											if (Main.netMode == 1)
											{
												NetMessage.SendData(17, -1, -1, "", 0, (float)Player.tileTargetX, (float)Player.tileTargetY, 1f, 0);
											}
										}
										this.itemTime = this.inventory[this.selectedItem].useTime;
									}
								}
							}
						}
					}
					if (Main.tile[Player.tileTargetX, Player.tileTargetY].wall > 0 && this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem && this.inventory[this.selectedItem].hammer > 0)
					{
						bool flag5 = true;
						if (!Main.wallHouse[(int)Main.tile[Player.tileTargetX, Player.tileTargetY].wall])
						{
							flag5 = false;
							for (int num24 = Player.tileTargetX - 1; num24 < Player.tileTargetX + 2; num24++)
							{
								for (int num25 = Player.tileTargetY - 1; num25 < Player.tileTargetY + 2; num25++)
								{
									if (Main.tile[num24, num25].wall != Main.tile[Player.tileTargetX, Player.tileTargetY].wall)
									{
										flag5 = true;
										break;
									}
								}
							}
						}
						if (flag5)
						{
							if (this.hitTileX != Player.tileTargetX || this.hitTileY != Player.tileTargetY)
							{
								this.hitTile = 0;
								this.hitTileX = Player.tileTargetX;
								this.hitTileY = Player.tileTargetY;
							}
							this.hitTile += (int)((float)this.inventory[this.selectedItem].hammer * 1.5f);
							if (this.hitTile >= 100)
							{
								this.hitTile = 0;
								WorldGen.KillWall(Player.tileTargetX, Player.tileTargetY, false);
								if (Main.netMode == 1)
								{
									NetMessage.SendData(17, -1, -1, "", 2, (float)Player.tileTargetX, (float)Player.tileTargetY, 0f, 0);
								}
							}
							else
							{
								WorldGen.KillWall(Player.tileTargetX, Player.tileTargetY, true);
								if (Main.netMode == 1)
								{
									NetMessage.SendData(17, -1, -1, "", 2, (float)Player.tileTargetX, (float)Player.tileTargetY, 1f, 0);
								}
							}
							this.itemTime = this.inventory[this.selectedItem].useTime / 2;
						}
					}
				}
				if (this.inventory[this.selectedItem].type == 29 && this.itemAnimation > 0 && this.statLifeMax < 400 && this.itemTime == 0)
				{
					this.itemTime = this.inventory[this.selectedItem].useTime;
					this.statLifeMax += 20;
					this.statLife += 20;
					if (Main.myPlayer == this.whoAmi)
					{
						this.HealEffect(20);
					}
				}
				if (this.inventory[this.selectedItem].type == 109 && this.itemAnimation > 0 && this.statManaMax < 200 && this.itemTime == 0)
				{
					this.itemTime = this.inventory[this.selectedItem].useTime;
					this.statManaMax += 20;
					this.statMana += 20;
					if (Main.myPlayer == this.whoAmi)
					{
						this.ManaEffect(20);
					}
				}
				if (this.inventory[this.selectedItem].createTile >= 0 && this.position.X / 16f - (float)Player.tileRangeX - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX + (float)this.inventory[this.selectedItem].tileBoost - 1f >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY + (float)this.inventory[this.selectedItem].tileBoost - 2f >= (float)Player.tileTargetY)
				{
					this.showItemIcon = true;
					bool flag6 = false;
					if (Main.tile[Player.tileTargetX, Player.tileTargetY].liquid > 0 && Main.tile[Player.tileTargetX, Player.tileTargetY].lava)
					{
						if (Main.tileSolid[this.inventory[this.selectedItem].createTile])
						{
							flag6 = true;
						}
						else
						{
							if (Main.tileLavaDeath[this.inventory[this.selectedItem].createTile])
							{
								flag6 = true;
							}
						}
					}
					if (((!Main.tile[Player.tileTargetX, Player.tileTargetY].active && !flag6) || this.inventory[this.selectedItem].createTile == 23 || this.inventory[this.selectedItem].createTile == 2 || this.inventory[this.selectedItem].createTile == 60 || this.inventory[this.selectedItem].createTile == 70) && this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem)
					{
						bool flag7 = false;
						if (this.inventory[this.selectedItem].createTile == 23 || this.inventory[this.selectedItem].createTile == 2)
						{
							if (Main.tile[Player.tileTargetX, Player.tileTargetY].active && Main.tile[Player.tileTargetX, Player.tileTargetY].type == 0)
							{
								flag7 = true;
							}
						}
						else
						{
							if (this.inventory[this.selectedItem].createTile == 60 || this.inventory[this.selectedItem].createTile == 70)
							{
								if (Main.tile[Player.tileTargetX, Player.tileTargetY].active && Main.tile[Player.tileTargetX, Player.tileTargetY].type == 59)
								{
									flag7 = true;
								}
							}
							else
							{
								if (this.inventory[this.selectedItem].createTile == 4)
								{
									int num26 = (int)Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type;
									int num27 = (int)Main.tile[Player.tileTargetX - 1, Player.tileTargetY].type;
									int num28 = (int)Main.tile[Player.tileTargetX + 1, Player.tileTargetY].type;
									int num29 = (int)Main.tile[Player.tileTargetX - 1, Player.tileTargetY - 1].type;
									int num30 = (int)Main.tile[Player.tileTargetX + 1, Player.tileTargetY - 1].type;
									int num31 = (int)Main.tile[Player.tileTargetX - 1, Player.tileTargetY - 1].type;
									int num32 = (int)Main.tile[Player.tileTargetX + 1, Player.tileTargetY + 1].type;
									if (!Main.tile[Player.tileTargetX, Player.tileTargetY + 1].active)
									{
										num26 = -1;
									}
									if (!Main.tile[Player.tileTargetX - 1, Player.tileTargetY].active)
									{
										num27 = -1;
									}
									if (!Main.tile[Player.tileTargetX + 1, Player.tileTargetY].active)
									{
										num28 = -1;
									}
									if (!Main.tile[Player.tileTargetX - 1, Player.tileTargetY - 1].active)
									{
										num29 = -1;
									}
									if (!Main.tile[Player.tileTargetX + 1, Player.tileTargetY - 1].active)
									{
										num30 = -1;
									}
									if (!Main.tile[Player.tileTargetX - 1, Player.tileTargetY + 1].active)
									{
										num31 = -1;
									}
									if (!Main.tile[Player.tileTargetX + 1, Player.tileTargetY + 1].active)
									{
										num32 = -1;
									}
									if (num26 >= 0 && Main.tileSolid[num26] && !Main.tileNoAttach[num26])
									{
										flag7 = true;
									}
									else
									{
										if ((num27 >= 0 && Main.tileSolid[num27] && !Main.tileNoAttach[num27]) || (num27 == 5 && num29 == 5 && num31 == 5))
										{
											flag7 = true;
										}
										else
										{
											if ((num28 >= 0 && Main.tileSolid[num28] && !Main.tileNoAttach[num28]) || (num28 == 5 && num30 == 5 && num32 == 5))
											{
												flag7 = true;
											}
										}
									}
								}
								else
								{
									if (this.inventory[this.selectedItem].createTile == 78 || this.inventory[this.selectedItem].createTile == 98 || this.inventory[this.selectedItem].createTile == 100)
									{
										if (Main.tile[Player.tileTargetX, Player.tileTargetY + 1].active && (Main.tileSolid[(int)Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type] || Main.tileTable[(int)Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type]))
										{
											flag7 = true;
										}
									}
									else
									{
										if (this.inventory[this.selectedItem].createTile == 13 || this.inventory[this.selectedItem].createTile == 29 || this.inventory[this.selectedItem].createTile == 33 || this.inventory[this.selectedItem].createTile == 49 || this.inventory[this.selectedItem].createTile == 50 || this.inventory[this.selectedItem].createTile == 103)
										{
											if (Main.tile[Player.tileTargetX, Player.tileTargetY + 1].active && Main.tileTable[(int)Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type])
											{
												flag7 = true;
											}
										}
										else
										{
											if (this.inventory[this.selectedItem].createTile == 51)
											{
												if (Main.tile[Player.tileTargetX + 1, Player.tileTargetY].active || Main.tile[Player.tileTargetX + 1, Player.tileTargetY].wall > 0 || Main.tile[Player.tileTargetX - 1, Player.tileTargetY].active || Main.tile[Player.tileTargetX - 1, Player.tileTargetY].wall > 0 || Main.tile[Player.tileTargetX, Player.tileTargetY + 1].active || Main.tile[Player.tileTargetX, Player.tileTargetY + 1].wall > 0 || Main.tile[Player.tileTargetX, Player.tileTargetY - 1].active || Main.tile[Player.tileTargetX, Player.tileTargetY - 1].wall > 0)
												{
													flag7 = true;
												}
											}
											else
											{
												if ((Main.tile[Player.tileTargetX + 1, Player.tileTargetY].active && Main.tileSolid[(int)Main.tile[Player.tileTargetX + 1, Player.tileTargetY].type]) || (Main.tile[Player.tileTargetX + 1, Player.tileTargetY].wall > 0 || (Main.tile[Player.tileTargetX - 1, Player.tileTargetY].active && Main.tileSolid[(int)Main.tile[Player.tileTargetX - 1, Player.tileTargetY].type])) || (Main.tile[Player.tileTargetX - 1, Player.tileTargetY].wall > 0 || (Main.tile[Player.tileTargetX, Player.tileTargetY + 1].active && Main.tileSolid[(int)Main.tile[Player.tileTargetX, Player.tileTargetY + 1].type])) || (Main.tile[Player.tileTargetX, Player.tileTargetY + 1].wall > 0 || (Main.tile[Player.tileTargetX, Player.tileTargetY - 1].active && Main.tileSolid[(int)Main.tile[Player.tileTargetX, Player.tileTargetY - 1].type])) || Main.tile[Player.tileTargetX, Player.tileTargetY - 1].wall > 0)
												{
													flag7 = true;
												}
											}
										}
									}
								}
							}
						}
						if (Main.tileAlch[this.inventory[this.selectedItem].createTile])
						{
							flag7 = true;
						}
						if (flag7 && WorldGen.PlaceTile(Player.tileTargetX, Player.tileTargetY, this.inventory[this.selectedItem].createTile, false, false, this.whoAmi, this.inventory[this.selectedItem].placeStyle))
						{
							this.itemTime = this.inventory[this.selectedItem].useTime;
							if (Main.netMode == 1)
							{
								NetMessage.SendData(17, -1, -1, "", 1, (float)Player.tileTargetX, (float)Player.tileTargetY, (float)this.inventory[this.selectedItem].createTile, this.inventory[this.selectedItem].placeStyle);
							}
							if (this.inventory[this.selectedItem].createTile == 15)
							{
								if (this.direction == 1)
								{
									Tile expr_439E = Main.tile[Player.tileTargetX, Player.tileTargetY];
									expr_439E.frameX += 18;
									Tile expr_43C3 = Main.tile[Player.tileTargetX, Player.tileTargetY - 1];
									expr_43C3.frameX += 18;
								}
								if (Main.netMode == 1)
								{
									NetMessage.SendTileSquare(-1, Player.tileTargetX - 1, Player.tileTargetY - 1, 3);
								}
							}
							else
							{
								if ((this.inventory[this.selectedItem].createTile == 79 || this.inventory[this.selectedItem].createTile == 90) && Main.netMode == 1)
								{
									NetMessage.SendTileSquare(-1, Player.tileTargetX, Player.tileTargetY, 5);
								}
							}
						}
					}
				}
				if (this.inventory[this.selectedItem].createWall >= 0)
				{
					Player.tileTargetX = (int)(((float)Main.mouseState.X + Main.screenPosition.X) / 16f);
					Player.tileTargetY = (int)(((float)Main.mouseState.Y + Main.screenPosition.Y) / 16f);
					if (this.position.X / 16f - (float)Player.tileRangeX - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetX && (this.position.X + (float)this.width) / 16f + (float)Player.tileRangeX + (float)this.inventory[this.selectedItem].tileBoost - 1f >= (float)Player.tileTargetX && this.position.Y / 16f - (float)Player.tileRangeY - (float)this.inventory[this.selectedItem].tileBoost <= (float)Player.tileTargetY && (this.position.Y + (float)this.height) / 16f + (float)Player.tileRangeY + (float)this.inventory[this.selectedItem].tileBoost - 2f >= (float)Player.tileTargetY)
					{
						this.showItemIcon = true;
						if (this.itemTime == 0 && this.itemAnimation > 0 && this.controlUseItem && (Main.tile[Player.tileTargetX + 1, Player.tileTargetY].active || Main.tile[Player.tileTargetX + 1, Player.tileTargetY].wall > 0 || Main.tile[Player.tileTargetX - 1, Player.tileTargetY].active || Main.tile[Player.tileTargetX - 1, Player.tileTargetY].wall > 0 || Main.tile[Player.tileTargetX, Player.tileTargetY + 1].active || Main.tile[Player.tileTargetX, Player.tileTargetY + 1].wall > 0 || Main.tile[Player.tileTargetX, Player.tileTargetY - 1].active || Main.tile[Player.tileTargetX, Player.tileTargetY - 1].wall > 0) && (int)Main.tile[Player.tileTargetX, Player.tileTargetY].wall != this.inventory[this.selectedItem].createWall)
						{
							WorldGen.PlaceWall(Player.tileTargetX, Player.tileTargetY, this.inventory[this.selectedItem].createWall, false);
							if ((int)Main.tile[Player.tileTargetX, Player.tileTargetY].wall == this.inventory[this.selectedItem].createWall)
							{
								this.itemTime = this.inventory[this.selectedItem].useTime;
								if (Main.netMode == 1)
								{
									NetMessage.SendData(17, -1, -1, "", 3, (float)Player.tileTargetX, (float)Player.tileTargetY, (float)this.inventory[this.selectedItem].createWall, 0);
								}
								if (this.inventory[this.selectedItem].stack > 1)
								{
									int createWall = this.inventory[this.selectedItem].createWall;
									for (int num33 = 0; num33 < 4; num33++)
									{
										int num34 = Player.tileTargetX;
										int num35 = Player.tileTargetY;
										if (num33 == 0)
										{
											num34--;
										}
										if (num33 == 1)
										{
											num34++;
										}
										if (num33 == 2)
										{
											num35--;
										}
										if (num33 == 3)
										{
											num35++;
										}
										if (Main.tile[num34, num35].wall == 0)
										{
											int num36 = 0;
											for (int num37 = 0; num37 < 4; num37++)
											{
												int num38 = num34;
												int num39 = num35;
												if (num37 == 0)
												{
													num38--;
												}
												if (num37 == 1)
												{
													num38++;
												}
												if (num37 == 2)
												{
													num39--;
												}
												if (num37 == 3)
												{
													num39++;
												}
												if ((int)Main.tile[num38, num39].wall == createWall)
												{
													num36++;
												}
											}
											if (num36 == 4)
											{
												WorldGen.PlaceWall(num34, num35, createWall, false);
												if ((int)Main.tile[num34, num35].wall == createWall)
												{
													this.inventory[this.selectedItem].stack--;
													if (this.inventory[this.selectedItem].stack == 0)
													{
														this.inventory[this.selectedItem].SetDefaults(0, false);
													}
													if (Main.netMode == 1)
													{
														NetMessage.SendData(17, -1, -1, "", 3, (float)num34, (float)num35, (float)createWall, 0);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			if (this.inventory[this.selectedItem].damage >= 0 && this.inventory[this.selectedItem].type > 0 && !this.inventory[this.selectedItem].noMelee && this.itemAnimation > 0)
			{
				bool flag8 = false;
				Rectangle rectangle = new Rectangle((int)this.itemLocation.X, (int)this.itemLocation.Y, 32, 32);
				if (!Main.dedServ)
				{
					rectangle = new Rectangle((int)this.itemLocation.X, (int)this.itemLocation.Y, Main.itemTexture[this.inventory[this.selectedItem].type].Width, Main.itemTexture[this.inventory[this.selectedItem].type].Height);
				}
				rectangle.Width = (int)((float)rectangle.Width * this.inventory[this.selectedItem].scale);
				rectangle.Height = (int)((float)rectangle.Height * this.inventory[this.selectedItem].scale);
				if (this.direction == -1)
				{
					rectangle.X -= rectangle.Width;
				}
				if (this.gravDir == 1f)
				{
					rectangle.Y -= rectangle.Height;
				}
				if (this.inventory[this.selectedItem].useStyle == 1)
				{
					if ((double)this.itemAnimation < (double)this.itemAnimationMax * 0.333)
					{
						if (this.direction == -1)
						{
							rectangle.X -= (int)((double)rectangle.Width * 1.4 - (double)rectangle.Width);
						}
						rectangle.Width = (int)((double)rectangle.Width * 1.4);
						rectangle.Y += (int)((double)rectangle.Height * 0.5 * (double)this.gravDir);
						rectangle.Height = (int)((double)rectangle.Height * 1.1);
					}
					else
					{
						if ((double)this.itemAnimation >= (double)this.itemAnimationMax * 0.666)
						{
							if (this.direction == 1)
							{
								rectangle.X -= (int)((double)rectangle.Width * 1.2);
							}
							rectangle.Width *= 2;
							rectangle.Y -= (int)(((double)rectangle.Height * 1.4 - (double)rectangle.Height) * (double)this.gravDir);
							rectangle.Height = (int)((double)rectangle.Height * 1.4);
						}
					}
				}
				else
				{
					if (this.inventory[this.selectedItem].useStyle == 3)
					{
						if ((double)this.itemAnimation > (double)this.itemAnimationMax * 0.666)
						{
							flag8 = true;
						}
						else
						{
							if (this.direction == -1)
							{
								rectangle.X -= (int)((double)rectangle.Width * 1.4 - (double)rectangle.Width);
							}
							rectangle.Width = (int)((double)rectangle.Width * 1.4);
							rectangle.Y += (int)((double)rectangle.Height * 0.6);
							rectangle.Height = (int)((double)rectangle.Height * 0.6);
						}
					}
				}
				float arg_4C9F_0 = this.gravDir;
				if (!flag8)
				{
					if ((this.inventory[this.selectedItem].type == 44 || this.inventory[this.selectedItem].type == 45 || this.inventory[this.selectedItem].type == 46 || this.inventory[this.selectedItem].type == 103 || this.inventory[this.selectedItem].type == 104) && Main.rand.Next(15) == 0)
					{
						Vector2 arg_4D6A_0 = new Vector2((float)rectangle.X, (float)rectangle.Y);
						int arg_4D6A_1 = rectangle.Width;
						int arg_4D6A_2 = rectangle.Height;
						int arg_4D6A_3 = 14;
						float arg_4D6A_4 = (float)(this.direction * 2);
						float arg_4D6A_5 = 0f;
						int arg_4D6A_6 = 150;
						Color newColor = default(Color);
						Dust.NewDust(arg_4D6A_0, arg_4D6A_1, arg_4D6A_2, arg_4D6A_3, arg_4D6A_4, arg_4D6A_5, arg_4D6A_6, newColor, 1.3f);
					}
					if (this.inventory[this.selectedItem].type == 273)
					{
						Color newColor;
						if (Main.rand.Next(5) == 0)
						{
							Vector2 arg_4DE0_0 = new Vector2((float)rectangle.X, (float)rectangle.Y);
							int arg_4DE0_1 = rectangle.Width;
							int arg_4DE0_2 = rectangle.Height;
							int arg_4DE0_3 = 14;
							float arg_4DE0_4 = (float)(this.direction * 2);
							float arg_4DE0_5 = 0f;
							int arg_4DE0_6 = 150;
							newColor = default(Color);
							Dust.NewDust(arg_4DE0_0, arg_4DE0_1, arg_4DE0_2, arg_4DE0_3, arg_4DE0_4, arg_4DE0_5, arg_4DE0_6, newColor, 1.4f);
						}
						Vector2 arg_4E48_0 = new Vector2((float)rectangle.X, (float)rectangle.Y);
						int arg_4E48_1 = rectangle.Width;
						int arg_4E48_2 = rectangle.Height;
						int arg_4E48_3 = 27;
						float arg_4E48_4 = this.velocity.X * 0.2f + (float)(this.direction * 3);
						float arg_4E48_5 = this.velocity.Y * 0.2f;
						int arg_4E48_6 = 100;
						newColor = default(Color);
						int num40 = Dust.NewDust(arg_4E48_0, arg_4E48_1, arg_4E48_2, arg_4E48_3, arg_4E48_4, arg_4E48_5, arg_4E48_6, newColor, 1.2f);
						Main.dust[num40].noGravity = true;
						Dust expr_4E6A_cp_0 = Main.dust[num40];
						expr_4E6A_cp_0.velocity.X = expr_4E6A_cp_0.velocity.X / 2f;
						Dust expr_4E88_cp_0 = Main.dust[num40];
						expr_4E88_cp_0.velocity.Y = expr_4E88_cp_0.velocity.Y / 2f;
					}
					if (this.inventory[this.selectedItem].type == 65)
					{
						if (Main.rand.Next(5) == 0)
						{
							Vector2 arg_4F02_0 = new Vector2((float)rectangle.X, (float)rectangle.Y);
							int arg_4F02_1 = rectangle.Width;
							int arg_4F02_2 = rectangle.Height;
							int arg_4F02_3 = 15;
							float arg_4F02_4 = 0f;
							float arg_4F02_5 = 0f;
							int arg_4F02_6 = 150;
							Color newColor = default(Color);
							Dust.NewDust(arg_4F02_0, arg_4F02_1, arg_4F02_2, arg_4F02_3, arg_4F02_4, arg_4F02_5, arg_4F02_6, newColor, 1.2f);
						}
						if (Main.rand.Next(10) == 0)
						{
							Gore.NewGore(new Vector2((float)rectangle.X, (float)rectangle.Y), default(Vector2), Main.rand.Next(16, 18), 1f);
						}
					}
					if (this.inventory[this.selectedItem].type == 190 || this.inventory[this.selectedItem].type == 213)
					{
						Vector2 arg_4FE1_0 = new Vector2((float)rectangle.X, (float)rectangle.Y);
						int arg_4FE1_1 = rectangle.Width;
						int arg_4FE1_2 = rectangle.Height;
						int arg_4FE1_3 = 40;
						float arg_4FE1_4 = this.velocity.X * 0.2f + (float)(this.direction * 3);
						float arg_4FE1_5 = this.velocity.Y * 0.2f;
						int arg_4FE1_6 = 0;
						Color newColor = default(Color);
						int num41 = Dust.NewDust(arg_4FE1_0, arg_4FE1_1, arg_4FE1_2, arg_4FE1_3, arg_4FE1_4, arg_4FE1_5, arg_4FE1_6, newColor, 1.2f);
						Main.dust[num41].noGravity = true;
					}
					if (this.inventory[this.selectedItem].type == 121)
					{
						for (int num42 = 0; num42 < 2; num42++)
						{
							Vector2 arg_5078_0 = new Vector2((float)rectangle.X, (float)rectangle.Y);
							int arg_5078_1 = rectangle.Width;
							int arg_5078_2 = rectangle.Height;
							int arg_5078_3 = 6;
							float arg_5078_4 = this.velocity.X * 0.2f + (float)(this.direction * 3);
							float arg_5078_5 = this.velocity.Y * 0.2f;
							int arg_5078_6 = 100;
							Color newColor = default(Color);
							int num43 = Dust.NewDust(arg_5078_0, arg_5078_1, arg_5078_2, arg_5078_3, arg_5078_4, arg_5078_5, arg_5078_6, newColor, 2.5f);
							Main.dust[num43].noGravity = true;
							Dust expr_509A_cp_0 = Main.dust[num43];
							expr_509A_cp_0.velocity.X = expr_509A_cp_0.velocity.X * 2f;
							Dust expr_50B8_cp_0 = Main.dust[num43];
							expr_50B8_cp_0.velocity.Y = expr_50B8_cp_0.velocity.Y * 2f;
						}
					}
					if (this.inventory[this.selectedItem].type == 122 || this.inventory[this.selectedItem].type == 217)
					{
						Vector2 arg_5167_0 = new Vector2((float)rectangle.X, (float)rectangle.Y);
						int arg_5167_1 = rectangle.Width;
						int arg_5167_2 = rectangle.Height;
						int arg_5167_3 = 6;
						float arg_5167_4 = this.velocity.X * 0.2f + (float)(this.direction * 3);
						float arg_5167_5 = this.velocity.Y * 0.2f;
						int arg_5167_6 = 100;
						Color newColor = default(Color);
						int num44 = Dust.NewDust(arg_5167_0, arg_5167_1, arg_5167_2, arg_5167_3, arg_5167_4, arg_5167_5, arg_5167_6, newColor, 1.9f);
						Main.dust[num44].noGravity = true;
					}
					if (this.inventory[this.selectedItem].type == 155)
					{
						Vector2 arg_51FA_0 = new Vector2((float)rectangle.X, (float)rectangle.Y);
						int arg_51FA_1 = rectangle.Width;
						int arg_51FA_2 = rectangle.Height;
						int arg_51FA_3 = 29;
						float arg_51FA_4 = this.velocity.X * 0.2f + (float)(this.direction * 3);
						float arg_51FA_5 = this.velocity.Y * 0.2f;
						int arg_51FA_6 = 100;
						Color newColor = default(Color);
						int num45 = Dust.NewDust(arg_51FA_0, arg_51FA_1, arg_51FA_2, arg_51FA_3, arg_51FA_4, arg_51FA_5, arg_51FA_6, newColor, 2f);
						Main.dust[num45].noGravity = true;
						Dust expr_521C_cp_0 = Main.dust[num45];
						expr_521C_cp_0.velocity.X = expr_521C_cp_0.velocity.X / 2f;
						Dust expr_523A_cp_0 = Main.dust[num45];
						expr_523A_cp_0.velocity.Y = expr_523A_cp_0.velocity.Y / 2f;
					}
					if (this.inventory[this.selectedItem].type >= 198 && this.inventory[this.selectedItem].type <= 203)
					{
						Lighting.addLight((int)((this.itemLocation.X + 6f + this.velocity.X) / 16f), (int)((this.itemLocation.Y - 14f) / 16f), 0.5f);
					}
					if (Main.myPlayer == i)
					{
						int num46 = (int)((float)this.inventory[this.selectedItem].damage * this.meleeDamage);
						float knockBack = this.inventory[this.selectedItem].knockBack;
						int num47 = rectangle.X / 16;
						int num48 = (rectangle.X + rectangle.Width) / 16 + 1;
						int num49 = rectangle.Y / 16;
						int num50 = (rectangle.Y + rectangle.Height) / 16 + 1;
						for (int num51 = num47; num51 < num48; num51++)
						{
							for (int num52 = num49; num52 < num50; num52++)
							{
								if (Main.tile[num51, num52] != null && Main.tileCut[(int)Main.tile[num51, num52].type] && Main.tile[num51, num52 + 1] != null && Main.tile[num51, num52 + 1].type != 78)
								{
									WorldGen.KillTile(num51, num52, false, false, false);
									if (Main.netMode == 1)
									{
										NetMessage.SendData(17, -1, -1, "", 0, (float)num51, (float)num52, 0f, 0);
									}
								}
							}
						}
						for (int num53 = 0; num53 < 1000; num53++)
						{
							if (Main.npc[num53].active && Main.npc[num53].immune[i] == 0 && this.attackCD == 0 && !Main.npc[num53].dontTakeDamage && (!Main.npc[num53].friendly || (Main.npc[num53].type == 22 && this.killGuide)))
							{
								Rectangle value = new Rectangle((int)Main.npc[num53].position.X, (int)Main.npc[num53].position.Y, Main.npc[num53].width, Main.npc[num53].height);
								if (rectangle.Intersects(value) && (Main.npc[num53].noTileCollide || Collision.CanHit(this.position, this.width, this.height, Main.npc[num53].position, Main.npc[num53].width, Main.npc[num53].height)))
								{
									bool flag9 = false;
									if (Main.rand.Next(1, 101) <= this.meleeCrit)
									{
										flag9 = true;
									}
									int num54 = Main.DamageVar((float)num46);
									this.StatusNPC(this.inventory[this.selectedItem].type, num53);
									Main.npc[num53].StrikeNPC(num54, knockBack, this.direction, flag9);
									if (Main.netMode != 0)
									{
										if (flag9)
										{
											NetMessage.SendData(28, -1, -1, "", num53, (float)num54, knockBack, (float)this.direction, 1);
										}
										else
										{
											NetMessage.SendData(28, -1, -1, "", num53, (float)num54, knockBack, (float)this.direction, 0);
										}
									}
									Main.npc[num53].immune[i] = this.itemAnimation;
									this.attackCD = (int)((double)this.itemAnimationMax * 0.33);
								}
							}
						}
						if (this.hostile)
						{
							for (int num55 = 0; num55 < 255; num55++)
							{
								if (num55 != i && Main.player[num55].active && Main.player[num55].hostile && !Main.player[num55].immune && !Main.player[num55].dead && (Main.player[i].team == 0 || Main.player[i].team != Main.player[num55].team))
								{
									Rectangle value2 = new Rectangle((int)Main.player[num55].position.X, (int)Main.player[num55].position.Y, Main.player[num55].width, Main.player[num55].height);
									if (rectangle.Intersects(value2) && Collision.CanHit(this.position, this.width, this.height, Main.player[num55].position, Main.player[num55].width, Main.player[num55].height))
									{
										bool flag10 = false;
										if (Main.rand.Next(1, 101) <= 10)
										{
											flag10 = true;
										}
										int num56 = Main.DamageVar((float)num46);
										this.StatusPvP(this.inventory[this.selectedItem].type, num55);
										Main.player[num55].Hurt(num56, this.direction, true, false, "", flag10);
										if (Main.netMode != 0)
										{
											if (flag10)
											{
												NetMessage.SendData(26, -1, -1, Player.getDeathMessage(this.whoAmi, -1, -1, -1), num55, (float)this.direction, (float)num56, 1f, 1);
											}
											else
											{
												NetMessage.SendData(26, -1, -1, Player.getDeathMessage(this.whoAmi, -1, -1, -1), num55, (float)this.direction, (float)num56, 1f, 0);
											}
										}
										this.attackCD = (int)((double)this.itemAnimationMax * 0.33);
									}
								}
							}
						}
					}
				}
			}
			if (this.itemTime == 0 && this.itemAnimation > 0)
			{
				if (this.inventory[this.selectedItem].healLife > 0)
				{
					this.statLife += this.inventory[this.selectedItem].healLife;
					this.itemTime = this.inventory[this.selectedItem].useTime;
					if (Main.myPlayer == this.whoAmi)
					{
						this.HealEffect(this.inventory[this.selectedItem].healLife);
					}
				}
				if (this.inventory[this.selectedItem].healMana > 0)
				{
					this.statMana += this.inventory[this.selectedItem].healMana;
					this.itemTime = this.inventory[this.selectedItem].useTime;
					if (Main.myPlayer == this.whoAmi)
					{
						this.ManaEffect(this.inventory[this.selectedItem].healMana);
					}
				}
				if (this.inventory[this.selectedItem].buffType > 0)
				{
					if (this.whoAmi == Main.myPlayer)
					{
						this.AddBuff(this.inventory[this.selectedItem].buffType, this.inventory[this.selectedItem].buffTime, true);
					}
					this.itemTime = this.inventory[this.selectedItem].useTime;
				}
			}
			if (this.itemTime == 0 && this.itemAnimation > 0 && this.inventory[this.selectedItem].type == 361)
			{
				this.itemTime = this.inventory[this.selectedItem].useTime;
				Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
				if (Main.netMode != 1 && Main.invasionType == 0)
				{
					Main.invasionDelay = 0;
					Main.StartInvasion();
				}
			}
			if (this.itemTime == 0 && this.itemAnimation > 0 && (this.inventory[this.selectedItem].type == 43 || this.inventory[this.selectedItem].type == 70))
			{
				this.itemTime = this.inventory[this.selectedItem].useTime;
				bool flag11 = false;
				int num57 = 4;
				if (this.inventory[this.selectedItem].type == 43)
				{
					num57 = 4;
				}
				else
				{
					if (this.inventory[this.selectedItem].type == 70)
					{
						num57 = 13;
					}
				}
				for (int num58 = 0; num58 < 1000; num58++)
				{
					if (Main.npc[num58].active && Main.npc[num58].type == num57)
					{
						flag11 = true;
						break;
					}
				}
				if (flag11)
				{
					if (Main.myPlayer == this.whoAmi)
					{
						this.Hurt(this.statLife * (this.statDefense + 1), -this.direction, false, false, Player.getDeathMessage(-1, -1, -1, 3), false);
					}
				}
				else
				{
					if (this.inventory[this.selectedItem].type == 43)
					{
						if (!Main.dayTime)
						{
							Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
							if (Main.netMode != 1)
							{
								NPC.SpawnOnPlayer(i, 4);
							}
						}
					}
					else
					{
						if (this.inventory[this.selectedItem].type == 70 && this.zoneEvil)
						{
							Main.PlaySound(15, (int)this.position.X, (int)this.position.Y, 0);
							if (Main.netMode != 1)
							{
								NPC.SpawnOnPlayer(i, 13);
							}
						}
					}
				}
			}
			if (this.inventory[this.selectedItem].type == 50 && this.itemAnimation > 0)
			{
				if (Main.rand.Next(2) == 0)
				{
					Vector2 arg_5BEF_0 = this.position;
					int arg_5BEF_1 = this.width;
					int arg_5BEF_2 = this.height;
					int arg_5BEF_3 = 15;
					float arg_5BEF_4 = 0f;
					float arg_5BEF_5 = 0f;
					int arg_5BEF_6 = 150;
					Color newColor = default(Color);
					Dust.NewDust(arg_5BEF_0, arg_5BEF_1, arg_5BEF_2, arg_5BEF_3, arg_5BEF_4, arg_5BEF_5, arg_5BEF_6, newColor, 1.1f);
				}
				if (this.itemTime == 0)
				{
					this.itemTime = this.inventory[this.selectedItem].useTime;
				}
				else
				{
					if (this.itemTime == this.inventory[this.selectedItem].useTime / 2)
					{
						for (int num59 = 0; num59 < 70; num59++)
						{
							Vector2 arg_5C88_0 = this.position;
							int arg_5C88_1 = this.width;
							int arg_5C88_2 = this.height;
							int arg_5C88_3 = 15;
							float arg_5C88_4 = this.velocity.X * 0.5f;
							float arg_5C88_5 = this.velocity.Y * 0.5f;
							int arg_5C88_6 = 150;
							Color newColor = default(Color);
							Dust.NewDust(arg_5C88_0, arg_5C88_1, arg_5C88_2, arg_5C88_3, arg_5C88_4, arg_5C88_5, arg_5C88_6, newColor, 1.5f);
						}
						this.grappling[0] = -1;
						this.grapCount = 0;
						for (int num60 = 0; num60 < 1000; num60++)
						{
							if (Main.projectile[num60].active && Main.projectile[num60].owner == i && Main.projectile[num60].aiStyle == 7)
							{
								Main.projectile[num60].Kill();
							}
						}
						this.Spawn();
						for (int num61 = 0; num61 < 70; num61++)
						{
							Vector2 arg_5D37_0 = this.position;
							int arg_5D37_1 = this.width;
							int arg_5D37_2 = this.height;
							int arg_5D37_3 = 15;
							float arg_5D37_4 = 0f;
							float arg_5D37_5 = 0f;
							int arg_5D37_6 = 150;
							Color newColor = default(Color);
							Dust.NewDust(arg_5D37_0, arg_5D37_1, arg_5D37_2, arg_5D37_3, arg_5D37_4, arg_5D37_5, arg_5D37_6, newColor, 1.5f);
						}
					}
				}
			}
			if (i == Main.myPlayer)
			{
				if (this.itemTime == this.inventory[this.selectedItem].useTime && this.inventory[this.selectedItem].consumable)
				{
					bool flag12 = true;
					if (this.inventory[this.selectedItem].ranged && this.ammoCost80 && Main.rand.Next(5) == 0)
					{
						flag12 = false;
					}
					if (flag12)
					{
						this.inventory[this.selectedItem].stack--;
						if (this.inventory[this.selectedItem].stack <= 0)
						{
							this.itemTime = this.itemAnimation;
						}
					}
				}
				if (this.inventory[this.selectedItem].stack <= 0 && this.itemAnimation == 0)
				{
					this.inventory[this.selectedItem] = new Item();
				}
			}
		}
		public Color GetImmuneAlpha(Color newColor)
		{
			float num = (float)(255 - this.immuneAlpha) / 255f;
			if (this.shadow > 0f)
			{
				num *= 1f - this.shadow;
			}
			int r = (int)((float)newColor.R * num);
			int g = (int)((float)newColor.G * num);
			int b = (int)((float)newColor.B * num);
			int num2 = (int)((float)newColor.A * num);
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num2 > 255)
			{
				num2 = 255;
			}
			return new Color(r, g, b, num2);
		}
		public Color GetDeathAlpha(Color newColor)
		{
			int r = (int)newColor.R + (int)((double)this.immuneAlpha * 0.9);
			int g = (int)newColor.G + (int)((double)this.immuneAlpha * 0.5);
			int b = (int)newColor.B + (int)((double)this.immuneAlpha * 0.5);
			int num = (int)newColor.A + (int)((double)this.immuneAlpha * 0.4);
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
		public void DropCoins()
		{
			for (int i = 0; i < 44; i++)
			{
				if (this.inventory[i].type >= 71 && this.inventory[i].type <= 74)
				{
					int num = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, this.inventory[i].type, 1, false);
					int num2 = this.inventory[i].stack / 2;
					num2 = this.inventory[i].stack - num2;
					this.inventory[i].stack -= num2;
					if (this.inventory[i].stack <= 0)
					{
						this.inventory[i] = new Item();
					}
					Main.item[num].stack = num2;
					Main.item[num].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
					Main.item[num].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
					Main.item[num].noGrabDelay = 100;
					if (Main.netMode == 1)
					{
						NetMessage.SendData(21, -1, -1, "", num, 0f, 0f, 0f, 0);
					}
				}
			}
		}
		public void DropItems()
		{
			for (int i = 0; i < 44; i++)
			{
				if (this.inventory[i].stack > 0 && this.inventory[i].name != "Copper Pickaxe" && this.inventory[i].name != "Copper Axe" && this.inventory[i].name != "Copper Shortsword")
				{
					int num = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, this.inventory[i].type, 1, false);
					Main.item[num].SetDefaults(this.inventory[i].name);
					Main.item[num].stack = this.inventory[i].stack;
					Main.item[num].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
					Main.item[num].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
					Main.item[num].noGrabDelay = 100;
					if (Main.netMode == 1)
					{
						NetMessage.SendData(21, -1, -1, "", num, 0f, 0f, 0f, 0);
					}
				}
				this.inventory[i] = new Item();
				if (i < 11)
				{
					if (this.armor[i].stack > 0)
					{
						int num2 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, this.armor[i].type, 1, false);
						Main.item[num2].SetDefaults(this.armor[i].name);
						Main.item[num2].stack = this.armor[i].stack;
						Main.item[num2].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
						Main.item[num2].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
						Main.item[num2].noGrabDelay = 100;
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, "", num2, 0f, 0f, 0f, 0);
						}
					}
					this.armor[i] = new Item();
				}
				if (i < 4)
				{
					if (this.ammo[i].stack > 0)
					{
						int num3 = Item.NewItem((int)this.position.X, (int)this.position.Y, this.width, this.height, this.ammo[i].type, 1, false);
						Main.item[num3].SetDefaults(this.ammo[i].name);
						Main.item[num3].stack = this.ammo[i].stack;
						Main.item[num3].velocity.Y = (float)Main.rand.Next(-20, 1) * 0.2f;
						Main.item[num3].velocity.X = (float)Main.rand.Next(-20, 21) * 0.2f;
						Main.item[num3].noGrabDelay = 100;
						if (Main.netMode == 1)
						{
							NetMessage.SendData(21, -1, -1, "", num3, 0f, 0f, 0f, 0);
						}
					}
					this.ammo[i] = new Item();
				}
			}
			this.inventory[0].SetDefaults("Copper Shortsword");
			this.inventory[1].SetDefaults("Copper Pickaxe");
			this.inventory[2].SetDefaults("Copper Axe");
		}
		public object Clone()
		{
			return base.MemberwiseClone();
		}
		public object clientClone()
		{
			Player player = new Player();
			player.zoneEvil = this.zoneEvil;
			player.zoneMeteor = this.zoneMeteor;
			player.zoneDungeon = this.zoneDungeon;
			player.zoneJungle = this.zoneJungle;
			player.direction = this.direction;
			player.selectedItem = this.selectedItem;
			player.controlUp = this.controlUp;
			player.controlDown = this.controlDown;
			player.controlLeft = this.controlLeft;
			player.controlRight = this.controlRight;
			player.controlJump = this.controlJump;
			player.controlUseItem = this.controlUseItem;
			player.statLife = this.statLife;
			player.statLifeMax = this.statLifeMax;
			player.statMana = this.statMana;
			player.statManaMax = this.statManaMax;
			player.position.X = this.position.X;
			player.chest = this.chest;
			player.talkNPC = this.talkNPC;
			for (int i = 0; i < 44; i++)
			{
				player.inventory[i] = (Item)this.inventory[i].Clone();
				if (i < 11)
				{
					player.armor[i] = (Item)this.armor[i].Clone();
				}
			}
			for (int j = 0; j < 10; j++)
			{
				player.buffType[j] = this.buffType[j];
				player.buffTime[j] = this.buffTime[j];
			}
			return player;
		}
		private static void EncryptFile(string inputFile, string outputFile)
		{
			string s = "h3y_gUyZ";
			UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
			byte[] bytes = unicodeEncoding.GetBytes(s);
			FileStream fileStream = new FileStream(outputFile, FileMode.Create);
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			CryptoStream cryptoStream = new CryptoStream(fileStream, rijndaelManaged.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
			FileStream fileStream2 = new FileStream(inputFile, FileMode.Open);
			int num;
			while ((num = fileStream2.ReadByte()) != -1)
			{
				cryptoStream.WriteByte((byte)num);
			}
			fileStream2.Close();
			cryptoStream.Close();
			fileStream.Close();
		}
		private static bool DecryptFile(string inputFile, string outputFile)
		{
			string s = "h3y_gUyZ";
			UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
			byte[] bytes = unicodeEncoding.GetBytes(s);
			FileStream fileStream = new FileStream(inputFile, FileMode.Open);
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			CryptoStream cryptoStream = new CryptoStream(fileStream, rijndaelManaged.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
			FileStream fileStream2 = new FileStream(outputFile, FileMode.Create);
			try
			{
				int num;
				while ((num = cryptoStream.ReadByte()) != -1)
				{
					fileStream2.WriteByte((byte)num);
				}
				fileStream2.Close();
				cryptoStream.Close();
				fileStream.Close();
			}
			catch
			{
				fileStream2.Close();
				fileStream.Close();
				File.Delete(outputFile);
				return true;
			}
			return false;
		}
		public static bool CheckSpawn(int x, int y)
		{
			if (x < 10 || x > Main.maxTilesX - 10 || y < 10 || y > Main.maxTilesX - 10)
			{
				return false;
			}
			if (Main.tile[x, y - 1] == null)
			{
				return false;
			}
			if (!Main.tile[x, y - 1].active || Main.tile[x, y - 1].type != 79)
			{
				return false;
			}
			for (int i = x - 1; i <= x + 1; i++)
			{
				for (int j = y - 3; j < y; j++)
				{
					if (Main.tile[i, j] == null)
					{
						return false;
					}
					if (Main.tile[i, j].active && Main.tileSolid[(int)Main.tile[i, j].type] && !Main.tileSolidTop[(int)Main.tile[i, j].type])
					{
						return false;
					}
				}
			}
			return WorldGen.StartRoomCheck(x, y - 1);
		}
		public void FindSpawn()
		{
			for (int i = 0; i < 200; i++)
			{
				if (this.spN[i] == null)
				{
					this.SpawnX = -1;
					this.SpawnY = -1;
					return;
				}
				if (this.spN[i] == Main.worldName && this.spI[i] == Main.worldID)
				{
					this.SpawnX = this.spX[i];
					this.SpawnY = this.spY[i];
					return;
				}
			}
		}
		public void ChangeSpawn(int x, int y)
		{
			int num = 0;
			while (num < 200 && this.spN[num] != null)
			{
				if (this.spN[num] == Main.worldName && this.spI[num] == Main.worldID)
				{
					for (int i = num; i > 0; i--)
					{
						this.spN[i] = this.spN[i - 1];
						this.spI[i] = this.spI[i - 1];
						this.spX[i] = this.spX[i - 1];
						this.spY[i] = this.spY[i - 1];
					}
					this.spN[0] = Main.worldName;
					this.spI[0] = Main.worldID;
					this.spX[0] = x;
					this.spY[0] = y;
					return;
				}
				num++;
			}
			for (int j = 199; j > 0; j--)
			{
				if (this.spN[j - 1] != null)
				{
					this.spN[j] = this.spN[j - 1];
					this.spI[j] = this.spI[j - 1];
					this.spX[j] = this.spX[j - 1];
					this.spY[j] = this.spY[j - 1];
				}
			}
			this.spN[0] = Main.worldName;
			this.spI[0] = Main.worldID;
			this.spX[0] = x;
			this.spY[0] = y;
		}
		public static void SavePlayer(Player newPlayer, string playerPath)
		{
			try
			{
				Directory.CreateDirectory(Main.PlayerPath);
			}
			catch
			{
			}
			if (playerPath == null || playerPath == "")
			{
				return;
			}
			string destFileName = playerPath + ".bak";
			if (File.Exists(playerPath))
			{
				File.Copy(playerPath, destFileName, true);
			}
			string text = playerPath + ".dat";
			using (FileStream fileStream = new FileStream(text, FileMode.Create))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
				{
					binaryWriter.Write(Main.curRelease);
					binaryWriter.Write(newPlayer.name);
					binaryWriter.Write(newPlayer.difficulty);
					binaryWriter.Write(newPlayer.hair);
					binaryWriter.Write(newPlayer.male);
					binaryWriter.Write(newPlayer.statLife);
					binaryWriter.Write(newPlayer.statLifeMax);
					binaryWriter.Write(newPlayer.statMana);
					binaryWriter.Write(newPlayer.statManaMax);
					binaryWriter.Write(newPlayer.hairColor.R);
					binaryWriter.Write(newPlayer.hairColor.G);
					binaryWriter.Write(newPlayer.hairColor.B);
					binaryWriter.Write(newPlayer.skinColor.R);
					binaryWriter.Write(newPlayer.skinColor.G);
					binaryWriter.Write(newPlayer.skinColor.B);
					binaryWriter.Write(newPlayer.eyeColor.R);
					binaryWriter.Write(newPlayer.eyeColor.G);
					binaryWriter.Write(newPlayer.eyeColor.B);
					binaryWriter.Write(newPlayer.shirtColor.R);
					binaryWriter.Write(newPlayer.shirtColor.G);
					binaryWriter.Write(newPlayer.shirtColor.B);
					binaryWriter.Write(newPlayer.underShirtColor.R);
					binaryWriter.Write(newPlayer.underShirtColor.G);
					binaryWriter.Write(newPlayer.underShirtColor.B);
					binaryWriter.Write(newPlayer.pantsColor.R);
					binaryWriter.Write(newPlayer.pantsColor.G);
					binaryWriter.Write(newPlayer.pantsColor.B);
					binaryWriter.Write(newPlayer.shoeColor.R);
					binaryWriter.Write(newPlayer.shoeColor.G);
					binaryWriter.Write(newPlayer.shoeColor.B);
					for (int i = 0; i < 11; i++)
					{
						if (newPlayer.armor[i].name == null)
						{
							newPlayer.armor[i].name = "";
						}
						binaryWriter.Write(newPlayer.armor[i].name);
					}
					for (int j = 0; j < 44; j++)
					{
						if (newPlayer.inventory[j].name == null)
						{
							newPlayer.inventory[j].name = "";
						}
						binaryWriter.Write(newPlayer.inventory[j].name);
						binaryWriter.Write(newPlayer.inventory[j].stack);
					}
					for (int k = 0; k < 4; k++)
					{
						if (newPlayer.ammo[k].name == null)
						{
							newPlayer.ammo[k].name = "";
						}
						binaryWriter.Write(newPlayer.ammo[k].name);
						binaryWriter.Write(newPlayer.ammo[k].stack);
					}
					for (int l = 0; l < Chest.maxItems; l++)
					{
						if (newPlayer.bank[l].name == null)
						{
							newPlayer.bank[l].name = "";
						}
						binaryWriter.Write(newPlayer.bank[l].name);
						binaryWriter.Write(newPlayer.bank[l].stack);
					}
					for (int m = 0; m < Chest.maxItems; m++)
					{
						if (newPlayer.bank2[m].name == null)
						{
							newPlayer.bank2[m].name = "";
						}
						binaryWriter.Write(newPlayer.bank2[m].name);
						binaryWriter.Write(newPlayer.bank2[m].stack);
					}
					for (int n = 0; n < 10; n++)
					{
						binaryWriter.Write(newPlayer.buffType[n]);
						binaryWriter.Write(newPlayer.buffTime[n]);
					}
					for (int num = 0; num < 200; num++)
					{
						if (newPlayer.spN[num] == null)
						{
							binaryWriter.Write(-1);
							break;
						}
						binaryWriter.Write(newPlayer.spX[num]);
						binaryWriter.Write(newPlayer.spY[num]);
						binaryWriter.Write(newPlayer.spI[num]);
						binaryWriter.Write(newPlayer.spN[num]);
					}
					binaryWriter.Write(newPlayer.hbLocked);
					binaryWriter.Close();
				}
			}
			Player.EncryptFile(text, playerPath);
			File.Delete(text);
		}
		public static Player LoadPlayer(string playerPath)
		{
			bool flag = false;
			if (Main.rand == null)
			{
				Main.rand = new Random((int)DateTime.Now.Ticks);
			}
			Player player = new Player();
			try
			{
				string text = playerPath + ".dat";
				flag = Player.DecryptFile(playerPath, text);
				if (!flag)
				{
					using (FileStream fileStream = new FileStream(text, FileMode.Open))
					{
						using (BinaryReader binaryReader = new BinaryReader(fileStream))
						{
							int num = binaryReader.ReadInt32();
							player.name = binaryReader.ReadString();
							if (num >= 10)
							{
								if (num >= 17)
								{
									player.difficulty = binaryReader.ReadByte();
								}
								else
								{
									bool flag2 = binaryReader.ReadBoolean();
									if (flag2)
									{
										player.difficulty = 2;
									}
								}
							}
							player.hair = binaryReader.ReadInt32();
							if (num <= 17)
							{
								if (player.hair == 5 || player.hair == 6 || player.hair == 9 || player.hair == 11)
								{
									player.male = false;
								}
								else
								{
									player.male = true;
								}
							}
							else
							{
								player.male = binaryReader.ReadBoolean();
							}
							player.statLife = binaryReader.ReadInt32();
							player.statLifeMax = binaryReader.ReadInt32();
							if (player.statLife > player.statLifeMax)
							{
								player.statLife = player.statLifeMax;
							}
							player.statMana = binaryReader.ReadInt32();
							player.statManaMax = binaryReader.ReadInt32();
							if (player.statMana > 400)
							{
								player.statMana = 400;
							}
							player.hairColor.R = binaryReader.ReadByte();
							player.hairColor.G = binaryReader.ReadByte();
							player.hairColor.B = binaryReader.ReadByte();
							player.skinColor.R = binaryReader.ReadByte();
							player.skinColor.G = binaryReader.ReadByte();
							player.skinColor.B = binaryReader.ReadByte();
							player.eyeColor.R = binaryReader.ReadByte();
							player.eyeColor.G = binaryReader.ReadByte();
							player.eyeColor.B = binaryReader.ReadByte();
							player.shirtColor.R = binaryReader.ReadByte();
							player.shirtColor.G = binaryReader.ReadByte();
							player.shirtColor.B = binaryReader.ReadByte();
							player.underShirtColor.R = binaryReader.ReadByte();
							player.underShirtColor.G = binaryReader.ReadByte();
							player.underShirtColor.B = binaryReader.ReadByte();
							player.pantsColor.R = binaryReader.ReadByte();
							player.pantsColor.G = binaryReader.ReadByte();
							player.pantsColor.B = binaryReader.ReadByte();
							player.shoeColor.R = binaryReader.ReadByte();
							player.shoeColor.G = binaryReader.ReadByte();
							player.shoeColor.B = binaryReader.ReadByte();
							Main.player[Main.myPlayer].shirtColor = player.shirtColor;
							Main.player[Main.myPlayer].pantsColor = player.pantsColor;
							Main.player[Main.myPlayer].hairColor = player.hairColor;
							for (int i = 0; i < 8; i++)
							{
								player.armor[i].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
							}
							if (num >= 6)
							{
								for (int j = 8; j < 11; j++)
								{
									player.armor[j].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
								}
							}
							for (int k = 0; k < 44; k++)
							{
								player.inventory[k].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
								player.inventory[k].stack = binaryReader.ReadInt32();
							}
							if (num >= 15)
							{
								for (int l = 0; l < 4; l++)
								{
									player.ammo[l].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
									player.ammo[l].stack = binaryReader.ReadInt32();
								}
							}
							for (int m = 0; m < Chest.maxItems; m++)
							{
								player.bank[m].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
								player.bank[m].stack = binaryReader.ReadInt32();
							}
							if (num >= 20)
							{
								for (int n = 0; n < Chest.maxItems; n++)
								{
									player.bank2[n].SetDefaults(Item.VersionName(binaryReader.ReadString(), num));
									player.bank2[n].stack = binaryReader.ReadInt32();
								}
							}
							if (num >= 11)
							{
								for (int num2 = 0; num2 < 10; num2++)
								{
									player.buffType[num2] = binaryReader.ReadInt32();
									player.buffTime[num2] = binaryReader.ReadInt32();
								}
							}
							for (int num3 = 0; num3 < 200; num3++)
							{
								int num4 = binaryReader.ReadInt32();
								if (num4 == -1)
								{
									break;
								}
								player.spX[num3] = num4;
								player.spY[num3] = binaryReader.ReadInt32();
								player.spI[num3] = binaryReader.ReadInt32();
								player.spN[num3] = binaryReader.ReadString();
							}
							if (num >= 16)
							{
								player.hbLocked = binaryReader.ReadBoolean();
							}
							binaryReader.Close();
						}
					}
					player.PlayerFrame();
					File.Delete(text);
					Player result = player;
					return result;
				}
			}
			catch
			{
				flag = true;
			}
			if (!flag)
			{
				return new Player();
			}
			string text2 = playerPath + ".bak";
			if (File.Exists(text2))
			{
				File.Delete(playerPath);
				File.Move(text2, playerPath);
				return Player.LoadPlayer(playerPath);
			}
			return new Player();
		}
		public bool HasItem(int type)
		{
			for (int i = 0; i < 44; i++)
			{
				if (type == this.inventory[i].type)
				{
					return true;
				}
			}
			return false;
		}
		public void QuickGrapple()
		{
			if (this.noItems)
			{
				return;
			}
			int num = -1;
			for (int i = 0; i < 44; i++)
			{
				if (this.inventory[i].shoot == 13 || this.inventory[i].shoot == 32)
				{
					num = i;
					break;
				}
			}
			if (num >= 0)
			{
				for (int j = 0; j < 1000; j++)
				{
					if (Main.projectile[j].active && Main.projectile[j].owner == Main.myPlayer && Main.projectile[j].type == this.inventory[num].shoot && Main.projectile[j].ai[0] != 2f)
					{
						num = -1;
						break;
					}
				}
			}
			if (num >= 0)
			{
				Main.PlaySound(2, (int)this.position.X, (int)this.position.Y, this.inventory[num].useSound);
				if (Main.netMode == 1 && this.whoAmi == Main.myPlayer)
				{
					NetMessage.SendData(51, -1, -1, "", this.whoAmi, 2f, 0f, 0f, 0);
				}
				int shoot = this.inventory[num].shoot;
				float shootSpeed = this.inventory[num].shootSpeed;
				int damage = this.inventory[num].damage;
				float knockBack = this.inventory[num].knockBack;
				if (shoot == 13 || shoot == 32)
				{
					this.grappling[0] = -1;
					this.grapCount = 0;
					for (int k = 0; k < 1000; k++)
					{
						if (Main.projectile[k].active && Main.projectile[k].owner == this.whoAmi && Main.projectile[k].type == 13)
						{
							Main.projectile[k].Kill();
						}
					}
				}
				Vector2 vector = new Vector2(this.position.X + (float)this.width * 0.5f, this.position.Y + (float)this.height * 0.5f);
				float num2 = (float)Main.mouseState.X + Main.screenPosition.X - vector.X;
				float num3 = (float)Main.mouseState.Y + Main.screenPosition.Y - vector.Y;
				float num4 = (float)Math.Sqrt((double)(num2 * num2 + num3 * num3));
				num4 = shootSpeed / num4;
				num2 *= num4;
				num3 *= num4;
				Projectile.NewProjectile(vector.X, vector.Y, num2, num3, shoot, damage, knockBack, this.whoAmi);
			}
		}
		public Player()
		{
			for (int i = 0; i < 44; i++)
			{
				if (i < 11)
				{
					this.armor[i] = new Item();
					this.armor[i].name = "";
				}
				this.inventory[i] = new Item();
				this.inventory[i].name = "";
			}
			for (int j = 0; j < Chest.maxItems; j++)
			{
				this.bank[j] = new Item();
				this.bank[j].name = "";
				this.bank2[j] = new Item();
				this.bank2[j].name = "";
			}
			for (int k = 0; k < 4; k++)
			{
				this.ammo[k] = new Item();
				this.ammo[k].name = "";
			}
			this.grappling[0] = -1;
			this.inventory[0].SetDefaults("Copper Shortsword");
			this.inventory[1].SetDefaults("Copper Pickaxe");
			this.inventory[2].SetDefaults("Copper Axe");
			for (int l = 0; l < 107; l++)
			{
				this.adjTile[l] = false;
				this.oldAdjTile[l] = false;
			}
		}
	}
}
